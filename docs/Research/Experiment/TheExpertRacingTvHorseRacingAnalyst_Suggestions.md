# TheExpertRacingTvHorseRacingAnalyst — Findings & Suggestions

Date: 2026-04-23

## Data Issues — key findings

- **DaysSinceLastRun mismatch:** some entries (e.g., `Bahtiyar`) show `DaysSinceLastRun=1552` while the most recent `Performances.Date` is `2025-11-04`. Recompute recency from performance dates.
- **TimeformRating format inconsistency:** values appear as `115p`, `"115"`, `"-"`, or `0`. Normalize to numeric and capture a `Potential` flag for trailing `p`.
- **Missing / weak rating data:** many `Performances` lack numeric `Rating`. We must derive a proxy rating rather than drop the run outright.
- **Weight / distance formats vary:** weights like `11-2` (stones-pounds) and `RaceDistance` appearing as integers (yards) need consistent parsing and conversion.
- **Prompts & race hints present but unused:** per-horse `Prompts` and race-level `IpHints` exist and can meaningfully adjust `AnalystConfidence` and `PaceAdjustment`.
- **Timezone/date normalization:** `StartTime` uses offsets; ensure all date math is UTC-based before decay weighting.
- **Liquidity missing from allowed inputs:** `TotalMatched` (market and per-selection) is available and should be used to down-weight thin markets/selections.

## Proposed prompt updates (concise)

1. **Data cleaning & normalization (required pre-processing)**
   - Normalize `TimeformRating`: strip non-digits, parse numeric. If a trailing `p` exists, set `TimeformPotential=true` and store the numeric portion.
   - Treat `"-"`, empty or zero-like ratings as `null` and mark the run for proxy-rating derivation.
   - Recompute `DaysSinceLastRun` from the latest `Performances.Date` using market StartTime (in UTC). If supplied `DaysSinceLastRun` differs by >7 days, override with computed value and log.
   - Convert `Weight` from `stones-pounds` (e.g., `11-2`) to `WeightLbs` = 14*stones + pounds.
   - Convert `RaceDistance` (appears to be in yards) to `DistanceYards`, `DistanceMeters = DistanceYards * 0.9144`, and `DistanceFurlongs = DistanceYards / 220`.
   - Parse `Form` strings into a time-ordered list of placings; map codes (1,2,3,P,U,-) to numeric proxies before computing `FormStringScore` with decay.
   - If a `Performance.Rating` is missing, derive a `proxyRating` from `FinishPosition`, `RaceClass`, and `DistanceBeatenCumulative` and mark the horse `RatingDataWeak=true`.
   - Normalize all dates/times to UTC before applying decay weights.
   - Include `TotalMatched` for the market and each selection as available from `GetActiveMarket` for liquidity weighting.

2. **Use `Prompts` and `IpHints`:** incorporate jockey/stride hints and `IpHints` (overall/specific/individual pace) into `AnalystConfidence` and compute a `PaceAdjustment` that nudges `ValueScore` for pace-setup winners/closers.

3. **Liquidity weighting:** add a `LiquidityWeight` multiplier to `DataConfidence` so thinly-traded selections are down-weighted (e.g., function of selection `TotalMatched` relative to market `TotalMatched`).

4. **InPlayEffort feature:** compute `InPlayEffortAvg` across recent runs and include as a small component of the Value vector (closing ability / stamina proxy).

5. **Decay parameter clarity:** specify the decay function used (exponential) and a default `lambda` (e.g., half-life = 30 days) so downstream code has a concrete value.

## Next steps

1. Apply these prompt updates (I created a draft `IV2` — see file next).
2. Re-run `GetAllDataContextForMarket` and validate that all numeric fields parse correctly.
3. Run the analytics pipeline to produce the required output table; iterate on decay `lambda` and liquidity rule if results look unstable.

---

If you want, I can now replace the original prompt with this IV2 version in a branch, then run the analysis on the same market to produce the table output required by the prompt.
