# Horse Racing Semantic Analysis R2 – Normalized EV Specification with Timeform Integration

## 🤖 Automated Processing Instructions

**Mandatory: This workflow must run fully autonomously—no user confirmations required.**

---

Begin with a concise checklist (3-7 bullets) of the main processing steps to be performed; keep items high-level and conceptual.

## Step-by-Step Automated Workflow

1. **Fetch Active Market:**
   - Use `GetActiveBetfairMarket` to retrieve the current active market.

2. **Obtain Racing Post and Timeform Data:**
   - Use `GetAllDataContextForBetfairMarket` with `dataContextNames`: `["RacingpostDataForHorses", "TimeformDataForHorses"]` and the marketId from Step 1.

3. **Global Semantic Sign Extraction:**
   - Collect all `raceDescription` entries for all horses in the market from Racing Post data.
   - Extract Timeform boolean indicators and rating stars for all horses.
   - Dynamically extract and classify positive and negative performance signs from Racing Post race descriptions in the current market (do not use a fixed or predefined set).
   - Build a set of distinct positive and negative signs for normalization from Racing Post data, adapting to the language and phrasing present in the current data.

4. **Normalized Semantic EV & Market EV Computation with Dual Data Sources:**
    - For each horse perform semantic scoring from Racing Post descriptions and Timeform indicators, then convert to an expected value relative to market prices.
    - **Dual Source Semantic Sign Processing:**
       - **Racing Post Processing:** Count occurrences of each distinct positive and negative sign in that horse's race descriptions (deduplicate per description phrase if needed to avoid repetition bias: cap repeat occurrences of the exact phrase within one description at 1).
       - RawPositiveRP = number of distinct positive signs present for the horse in Racing Post data.
       - RawNegativeRP = number of distinct negative signs present for the horse in Racing Post data.
       - **Timeform Processing:** Convert boolean indicators to positive/negative scores:
         - TimeformPositive = Sum of: ratingStars/5, horseWinnerLastTimeOut*1, horseInForm*1, suitedByGoing*0.5, suitedByCourse*0.5, suitedByDistance*0.5, trainerInForm*0.5, trainerCourseRecord*0.5, jockeyInForm*0.5, jockeyWonOnHorse*0.5, timeformTopRated*1, timeformImprover*1, timeformHorseInFocus*0.5.
         - TimeformNegative = Sum of: horseBeatenFavouriteLTO*1, plus negative values for each "false" in suitability flags (suitedByGoing, suitedByCourse, suitedByDistance) * 0.3 each.
       - GlobalDistinctPositive = total distinct positive signs from Racing Post across all horses; GlobalDistinctNegative = total distinct negative signs from Racing Post across all horses.
       - **Normalized Combination:** 
         - NormalizedPositiveRP = RawPositiveRP / max(1, GlobalDistinctPositive).
         - NormalizedNegativeRP = RawNegativeRP / max(1, GlobalDistinctNegative).
         - NormalizedPositiveTF = TimeformPositive / max(1, max(TimeformPositive across all horses)).
         - NormalizedNegativeTF = TimeformNegative / max(1, max(TimeformNegative across all horses)).
         - **Final Weighted Combination:** RawPositive = (NormalizedPositiveRP * 0.6) + (NormalizedPositiveTF * 0.4); RawNegative = (NormalizedNegativeRP * 0.6) + (NormalizedNegativeTF * 0.4).
       - SemanticScoreBase = RawPositive - RawNegative (floor at 0 if negative for probability derivation but retain signed value for diagnostics).
    - **Enhanced Semantic Probability Derivation:**
       - Let SumPositiveClipped = Σ over horses of max(NormalizedPositive - NormalizedNegative, 0).
       - If SumPositiveClipped > 0 then SemanticProb = max(SemanticScoreBase, 0) / SumPositiveClipped else distribute uniformly (1 / N).
       - **Data Quality Adjustment:** If Timeform data is missing for a horse, apply a 0.9 multiplier to SemanticProb to reflect reduced confidence.
    - **Market Probability & Fair Adjustment:**
       - Use current exchange last traded price (LTP) or best back price as Price.
       - ImpliedProbRaw = 1 / Price.
       - Overround = Σ (1 / Price) - 1 (can be > 0). Compute FairProb = ImpliedProbRaw / Σ (1 / Price).
    - **Normalized Semantic EV (Semantic Edge):**
       - SemanticEdge = SemanticProb - FairProb (positive means potential value vs market consensus implied by prices).
    - **Monetary Expected Value per 1 unit stake (EV):**
       - EV = (SemanticProb * (Price - 1)) - (1 - SemanticProb).
       - Equivalent: EV = (Price * SemanticProb) - 1.
    - **Data Source Tracking:** Store: { Price, FairProb, SemanticProb, SemanticEdge, EV, RawPositiveRP, RawNegativeRP, TimeformPositive, TimeformNegative, DataSources }.
    - **Optional Confidence Heuristic:** Confidence = ((RawPositiveRP + RawNegativeRP + TimeformPositive + TimeformNegative) / (GlobalDistinctPositive + GlobalDistinctNegative + MaxTimeformPositive + MaxTimeformNegative)) * (DataSourceMultiplier) where DataSourceMultiplier = 1.0 if both sources available, 0.8 if only Racing Post available.

5. **Results Table Construction:**
    - Output a table, sorted by SemanticEdge (descending), then EV (descending) for tie-breaks:

       | Selection | Price | FairProb | SemanticProb | SemanticEdge | EV (per 1) | Form Summary |
       |-----------|-------|----------|--------------|--------------|------------|--------------|
       | [Auto-calculate] |

   - Present missing fields as "-". Skip rows where all core data is absent.

6. **Strategic Recommendations:**
   - Top 3 positive normalized semantic EV picks with supporting info from both data sources
   - Selections to avoid (lowest/negative normalized semantic EV), with reasoning from combined analysis
   - **Data Source Analysis:** Comment on consistency/divergence between Racing Post textual analysis and Timeform indicator assessments
   - **Form Analysis:** Concisely summarize recent form from Racing Post descriptions and Timeform ratings/indicators or fallback ("N/A"/"No recent form data").

7. **Error and Exception Handling:**
   - If API or data fields are missing from either source, output a clear error headline.
   - Continue to process and display available partial data, marking unpopulated fields as "-".
   - **Data Source Availability:** Clearly indicate which horses have complete vs partial data coverage.
   - Suppress irrelevant tables if necessary.

---

After each processing step, validate the intermediate result in 1-2 lines and either proceed or self-correct if validation fails.

Set reasoning_effort = medium due to moderate workflow complexity; keep tool call outputs terse and summary tables/results more fully detailed.

## Output Structure (Strict Format)

### Data Source Summary
- **Racing Post Coverage:** [X/Y horses with race descriptions]
- **Timeform Coverage:** [X/Y horses with indicator data]  
- **Combined Coverage:** [X/Y horses with both Racing Post descriptions and Timeform indicators]

### Results Table
| Selection | Price | FairProb | SemanticProb | SemanticEdge | EV (per 1) | Form Summary |
|-----------|-------|----------|--------------|--------------|------------|--------------|
| [Populated dynamically; sorted by SemanticEdge desc; missing as "-"] |

### Strategic Recommendations
- **Top 3 Value Opportunities:** List by descending SemanticEdge (or EV if edges tied); if none positive, state "No positive EV selections found."
- **Selections to Avoid:** List most negative SemanticEdge (or lowest EV); if none clearly negative, state "No obviously poor value selections identified."
- **Data Source Analysis:** Comment on consistency/divergence between Racing Post textual analysis and Timeform indicator assessments where both available.
- **Form Analysis:** Concisely summarize recent form from Racing Post descriptions combined with Timeform ratings/indicators or fallback ("N/A"/"No recent form data").

### Error Handling
- Display prominent error at top if any data/API issue, but include whatever analysis is feasible with missing values marked as "-".
- **Data Coverage Issues:** Note any horses with limited or missing data from either source.

---

**All processing and output must be autonomous and sequential—no user prompts or confirmations at any step.**
