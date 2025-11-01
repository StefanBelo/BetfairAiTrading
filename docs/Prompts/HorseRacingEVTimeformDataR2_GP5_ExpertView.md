# Horse Racing EV Analysis — Timeform Expert View (GP5)

---
Title: HorseRacingEVTimeformDataR2_GP5_ExpertView.md
Purpose: Prompt for automated EV assessment using Betfair market data and Timeform expert inputs. Output: structured Markdown for execution decisions.
---

**SILENT MODE:** Execute end-to-end analysis and automation with no external chatter. Maintain internal telemetry.

## 1. Data Pipeline
- **Market snapshot:** Call `GetActiveMarket` → capture `marketId`, metadata, and `selections` (`selectionId`, `name`, `price`, `status`). Treat any missing advanced pricing fields as `null`; record in provenance if unavailable.
- **Timeform enrichments:** Call `GetDataContextForMarket` with `dataContextNames = ["TimeformFullDataForHorses"]`. Expect `selectionsData[]` with `expertView` text and `timeformHorseData` booleans plus `ratingStars` (0-5).
- **Completeness guard:** Compute ratio of selections containing both `expertView` and `timeformHorseData`. If < 0.80, continue but flag in provenance. When data missing, propagate `null` in derived metrics.

## 2. Sentiment Scoring (Expert View)
- **Section weights:** Opening sentence (20%), middle body (50%), closing section (30%). Use first/last sentences as anchors; split remainder evenly.
- **Token scoring:**
  - Positive cues (+1 to +3): {strong form: won, scored, triumphant, impressive, convincing, career best (+3)}; {confidence: should go well, one to consider, not taken lightly, enters calculations (+2)}; {suitability: suited by conditions, C&D winner, course winner (+2)}; {progression: progressive, improving, on the up, good nick (+2)}; {value resilience: good account, can bounce back, needs considering (+1)}.
  - Negative cues (-1 to -3): {poor form: ran poor, disappointing, below par, struggled, failed (-3)}; {results: beaten, lost, finished down field, pulled up, tailed off (-2)}; {concern flags: needs to bounce back, more required, had the run of the race (-2)}; {losing streak: on a losing run, runs since last win (-1)}.
  - Neutral markers (0): transitional terms such as if, but, though, however; balanced phrases like some chance, not without hope.
- **Section score:** Sum positives and negatives per section, multiply by section weight. If total absolute weight is zero, set Sentiment Score = 0. Else use `(weighted_positive - weighted_negative) / (weighted_positive + weighted_negative)` and clamp to [-1, 1].
- **Key phrases:** Capture decisive phrases for later summary (≤100 chars).

## 3. Timeform Quantitative Score
- Base = `ratingStars × 0.1` (0 – 0.5).
- Add +0.05 for each true flag: `horseWinnerLastTimeOut`, `horseInForm`, `suitedByGoing`, `suitedByCourse`, `suitedByDistance`, `trainerInForm`, `trainerCourseRecord`, `jockeyInForm`, `jockeyWonOnHorse`, `timeformTopRated`, `timeformImprover`, `timeformHorseInFocus`.
- Subtract 0.10 if `horseBeatenFavouriteLTO` is true.
- Clamp final score to [-1.0, 1.0]. Set to `null` when `timeformHorseData` missing.

## 4. Combined Metrics & EV
- Combined Score = mean(Sentiment Score, Timeform Score) where both exist; else `null`.
- Estimated Probability = `(Combined Score + 1) / 2` (range [0,1]).
- Expected Value per €1 stake = `estimated_prob × (price - 1) - (1 - estimated_prob) × 1.05`. If probability or price missing, set EV = `null`.

## 5. Decision Logic
- Sort selections by ascending odds.
- Identify first runner with Combined Score ≥ 0.70 and complete data (both sentiment + Timeform available).
- **BACK** trigger (all true): Combined Score ≥ 0.70, data completeness ≥ 80%, metrics not `null`.
- On trigger: stake €10 via `ExecuteBfexplorerStrategySettings(marketId, selectionId, "Bet 10 Euro")`. Otherwise take NO_ACTION.

## 6. Output Specification
- Produce GitHub-flavored table ordered by odds:

| Horse | Odds | Sentiment Score | Timeform Score | Combined Score | Key Semantic Features | Decision |
|---|---:|---:|---:|---:|---|---|

- Formatting rules:
  - Round numeric fields to 4 decimals (`null` when unavailable).
  - `Key Semantic Features` = concise driver phrases; default "No data".
  - Decision column shows `**BACK**` for the chosen horse, else `NO_ACTION`.
- Append statistics block:

```
### Field Statistics:
- **Total Horses:** {count}
- **Data Completeness:** {percentage}% (selections with both expertView and timeformHorseData)
- **Highest Combined Score:** {score} ({horse_name})
- **Field Average Combined Score:** {average}
- **Combined Score Gap:** {best_score} - {field_average} = {gap}
```

- Finish with recommendation line:
  - Success: `**Recommendation:** BACK {horse} (selectionId {selectionId}) at {odds} — Combined Score {score} ≥ 0.7 (data completeness {completeness}%)`
  - Otherwise: `**Recommendation:** No BACK recommended — no selection reached Combined Score ≥ 0.7 threshold (highest: {horse} at {score})`

## 7. Quality Controls
- Validate score ranges and rounding before emitting output.
- Ensure ≤1 runner marked `**BACK**`.
- Confirm completeness ratio calculation and provenance notes for missing data.
- Track internal audit trail: key sentiment drivers, Timeform flags used, assumptions applied.

## 8. Handling Missing Data
- Missing `expertView` → Sentiment Score = `null`, Key Semantic Features = "No data".
- Missing `timeformHorseData` → Timeform Score = `null`.
- When either score `null`, suppress Combined Score, Estimated Probability, and EV.

## 9. Reference Example (Naas — 1m2f Hcap, marketId 1.248899369)

| Horse | Odds | Sentiment Score | Timeform Score | Combined Score | Key Semantic Features | Decision |
|---|---:|---:|---:|---:|---|---|
| So Golden | 6.2000 | 1.0000 | 0.5500 | 0.7750 | won maiden; scored cosily; one to consider | **BACK** |
| Narlita | 6.2000 | 0.3333 | 0.5500 | 0.4417 | close fourth; taken seriously | NO_ACTION |
| Annies Angel | 7.0000 | null | null | null | No data | NO_ACTION |
| Presence | 9.0000 | -0.6000 | 0.2500 | -0.1750 | below form; more required | NO_ACTION |

### Field Statistics:
- **Total Horses:** 10
- **Data Completeness:** 90% (9/10)
- **Highest Combined Score:** 0.7750 (So Golden)
- **Field Average Combined Score:** 0.3542
- **Combined Score Gap:** 0.7750 - 0.3542 = 0.4208

**Recommendation:** BACK So Golden (selectionId 10766575_0.00) at 6.2000 — Combined Score 0.7750 ≥ 0.7 (data completeness 90%)