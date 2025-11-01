---
Title: HorseRacingEVTimeformDataR2_CS45_ExpertView.md
Purpose: AI agent prompt for horse-racing EV analysis using Betfair market + Timeform data. Output: structured Markdown table for decision-making.
---

# Horse Racing EV Analysis - Timeform Data (Structured Prompt)

**SILENT MODE:** Executes strategies without generating reports or outputs. All analysis performed internally with performance tracking.

## 1. Data Retrieval & Validation Framework

### Step 1: Get Market Data
Use `GetActiveMarket` to retrieve the active market object. This endpoint returns:
- Market metadata: `marketId`, `eventName`, `marketName`, `startTime`, `status`
- `selections` array where each selection contains:
  - `selectionId` (string)
  - `name` (string) — horse name
  - `price` (float) — current displayed price (decimal odds)
  - `status` (string)

**Note:** The market endpoint does NOT reliably include matched volumes, multi-layer best-back/best-lay prices, or detailed odds history. If those fields are present, use them; otherwise set to `null` and note in provenance.

### Step 2: Get Timeform Horse Data
Use `GetDataContextForMarket` with `dataContextName = "TimeformFullDataForHorses"` for the same `marketId`. This endpoint returns a `selectionsData` array where each element contains:
- `selectionId` (string)
- `name` (string)
- `data` (object) with:
  - `expertView` (string) — textual analysis from Timeform experts
  - `timeformHorseData` (object) containing:
    - `ratingStars` (integer 0-5)
    - Boolean indicators: `horseWinnerLastTimeOut`, `horseInForm`, `horseBeatenFavouriteLTO`, `suitedByGoing`, `suitedByCourse`, `suitedByDistance`, `trainerInForm`, `trainerCourseRecord`, `jockeyInForm`, `jockeyWonOnHorse`, `timeformTopRated`, `timeformImprover`, `timeformHorseInFocus`

**Data Validation:** Ensure data completeness ≥ 80% before proceeding (count selections with both `expertView` and `timeformHorseData`). If a selection lacks `expertView` or `timeformHorseData`, set related fields to `null` and note in provenance.

---

## 2. Semantic Analysis & Scoring Framework

### Expert View Sentiment Analysis

Perform comprehensive semantic analysis on the `expertView` text to assess overall sentiment and performance indicators. Use a sectional weighted approach:

**Sentiment Scoring Algorithm:**
1. **Split text into three sections:**
   - Opening sentence: First sentence or first 20% of text (20% weight)
   - Middle content: Middle portion or 50% of text (50% weight)
   - Closing assessment: Final sentence or last 30% of text (30% weight)

2. **Assign keyword points per section:**
   
   **POSITIVE indicators (+1 to +3 points each):**
   - Strong form: "won", "scored", "triumphed", "victorious", "impressive", "convincing", "career best" (+3)
   - Confidence: "should go well", "very much one to consider", "not taken lightly", "enters calculations" (+2)
   - Suitability: "suited by conditions", "loves these conditions", "C&D winner", "course winner" (+2)
   - Progression: "progressive", "improving", "on the up", "progressing recently", "good nick" (+2)
   - Value: "good account", "can bounce back", "needs considering" (+1)
   
   **NEGATIVE indicators (-1 to -3 points each):**
   - Poor form: "ran poor", "disappointing", "below par", "below form", "struggled", "failed" (-3)
   - Negative results: "beaten", "lost", "finished down field", "pulled up", "tailed off" (-2)
   - Concerns: "needs to bounce back", "more required", "had the run of the race" (-2)
   - Absence: "on a losing run", "runs since last win" (-1)
   
   **NEUTRAL/MIXED indicators (0 points):**
   - Conditional: "if", "but", "though", "however" (context-dependent)
   - Balanced: "some chance", "not without hope"

3. **Calculate weighted scores:**
   - For each section: Sum positive points, sum negative points
   - Weighted Positive = Section Positive × Section Weight %
   - Weighted Negative = Section Negative × Section Weight %

4. **Compute Net Sentiment Score:**
   - Total Weighted Positive = Sum of all section weighted positives
   - Total Weighted Negative = Sum of all section weighted negatives
   - If (Total Weighted Positive + Total Weighted Negative) > 0:
     - Sentiment Score = (Total Weighted Positive - Total Weighted Negative) / (Total Weighted Positive + Total Weighted Negative)
   - Else: Sentiment Score = 0
   - Range: -1 (very negative) to +1 (very positive)

### Timeform Quantitative Score

Calculate a quantitative score from Timeform structured data:

**Formula:**
- Base score from ratingStars: `ratingStars × 0.1` (range 0 to 0.5)
- Positive indicators (+0.05 each): `horseWinnerLastTimeOut`, `horseInForm`, `suitedByGoing`, `suitedByCourse`, `suitedByDistance`, `trainerInForm`, `trainerCourseRecord`, `jockeyInForm`, `jockeyWonOnHorse`, `timeformTopRated`, `timeformImprover`, `timeformHorseInFocus`
- Negative indicators (-0.1 each): `horseBeatenFavouriteLTO`
- Timeform Score = Base + Sum of positive indicators - Sum of negative indicators
- Cap at 1.0 maximum, floor at -1.0 minimum

### Combined Score

**Combined Score = (Sentiment Score + Timeform Score) / 2**
- Range: -1 to +1
- This is the primary metric for decision-making

### Estimated Probability & Expected Value

- **Estimated Probability:** `(Combined Score + 1) / 2` — maps Combined Score from [-1,1] to probability [0,1]
- **Expected Value:** `estimated_prob × (price - 1) - (1 - estimated_prob) × 1.05` — EV per unit stake after 5% commission
  - If price or estimated_prob is null, set EV to null

---

## 3. Decision Logic & Strategy Execution

### Selection Criteria

**IDENTIFY BEST HORSE:** 
- Sort horses by `price` ascending (lowest odds first)
- Select the first horse with Combined Score ≥ 0.7

### Action Recommendation

**BACK Requirements (ALL must be true):**
1. Combined Score ≥ 0.7
2. Data completeness ≥ 80%
3. Horse has both `expertView` and `timeformHorseData` available

**If criteria met:**
- Action: BACK
- Stake: Fixed 10 EUR

**Otherwise:**
- Action: NO_ACTION

### Execution Flow

1. Compute scores for all selections
2. Identify best horse by criteria above
3. If BACK criteria met: Execute `ExecuteBfexplorerStrategySettings(marketId, selectionId, "Bet 10 Euro")`
4. Generate output table (see below)

---

## 4. Output Format: Markdown Table

Generate a GitHub-flavored Markdown table with the following columns (in this exact order):

| Horse | Odds | Sentiment Score | Timeform Score | Combined Score | Key Semantic Features | Decision |
|---|---:|---:|---:|---:|---|---|

### Column Specifications

- **Horse** (string): Selection name from market data
- **Odds** (float): Decimal price from market data, rounded to 4 decimals
- **Sentiment Score** (float): Net sentiment score from expertView analysis, rounded to 4 decimals. Set to `null` if expertView missing.
- **Timeform Score** (float): Quantitative score from timeformHorseData, rounded to 4 decimals. Set to `null` if timeformHorseData missing.
- **Combined Score** (float): Average of Sentiment and Timeform scores, rounded to 4 decimals. Set to `null` if either component is null.
- **Key Semantic Features** (string): Short summary of key phrases from expertView that contributed to sentiment score (max 100 chars). Do NOT repeat full expertView. If expertView missing, set to "No data".
- **Decision** (string): "**BACK**" (in bold) if this horse meets BACK criteria and is selected; "NO_ACTION" otherwise.

### Table Generation Rules

- Round all numeric values to 4 decimal places
- For missing data: display `null` in numeric columns, "No data" in text columns
- Bold the Decision cell for the selected horse (use `**BACK**`)
- Sort rows by odds ascending (favourites first)

### Field Statistics (after table)

Include these summary statistics after the table:

```
### Field Statistics:
- **Total Horses:** {count}
- **Data Completeness:** {percentage}% (selections with both expertView and timeformHorseData)
- **Highest Combined Score:** {score} ({horse_name})
- **Field Average Combined Score:** {average}
- **Combined Score Gap:** {best_score} - {field_average} = {gap}
```

### One-Line Summary

End with a one-line summary:

```
**Recommendation:** BACK {horse_name} (selectionId {selectionId}) at {odds} — Combined Score {score} ≥ 0.7 (data completeness {completeness}%)
```

Or if no BACK:

```
**Recommendation:** No BACK recommended — no selection reached Combined Score ≥ 0.7 threshold (highest: {horse_name} at {score})
```

---

## 5. Example Output

### Market: Naas — 1m2f Hcap (marketId 1.248899369)

| Horse | Odds | Sentiment Score | Timeform Score | Combined Score | Key Semantic Features | Decision |
|---|---:|---:|---:|---:|---|---|
| So Golden | 6.2000 | 1.0000 | 0.5500 | 0.7750 | won maiden; scored cosily; very much one to consider | **BACK** |
| Narlita | 6.2000 | 0.3333 | 0.5500 | 0.4417 | close fourth; has to be taken seriously | NO_ACTION |
| Annies Angel | 7.0000 | null | null | null | No data | NO_ACTION |
| Presence | 9.0000 | -0.6000 | 0.2500 | -0.1750 | below form sixth; more required | NO_ACTION |

### Field Statistics:
- **Total Horses:** 10
- **Data Completeness:** 90% (9 of 10 selections with complete data)
- **Highest Combined Score:** 0.7750 (So Golden)
- **Field Average Combined Score:** 0.3542
- **Combined Score Gap:** 0.7750 - 0.3542 = 0.4208

**Recommendation:** BACK So Golden (selectionId 10766575_0.00) at 6.2000 — Combined Score 0.7750 ≥ 0.7 (data completeness 90%)

---

## 6. Implementation Notes

### Handling Missing Data

- If `expertView` is missing: Set Sentiment Score to `null`, use only Timeform Score (Combined = null)
- If `timeformHorseData` is missing: Set Timeform Score to `null`, use only Sentiment Score (Combined = null)
- If both missing: All scores null, Decision = NO_ACTION, Key Features = "No data"

### Provenance Tracking

While not displayed in the table, internally track:
- Which keywords/phrases contributed to sentiment scores
- Which Timeform booleans were true/false
- Any fallbacks or missing data points

### Quality Checks

Before outputting the table:
1. Verify all numeric values are in expected ranges
2. Ensure exactly one horse (or none) has Decision = "**BACK**"
3. Confirm data completeness calculation is correct
4. Validate that Combined Score calculation matches formula

---