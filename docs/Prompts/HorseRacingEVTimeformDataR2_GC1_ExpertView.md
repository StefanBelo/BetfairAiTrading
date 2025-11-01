# Horse Racing EV Analysis — Timeform Expert View (GC1)

---
**Title:** HorseRacingEVTimeformDataR2_GC1_ExpertView.md  
**Purpose:** Prompt for automated EV assessment using Betfair market data and Timeform expert inputs. Output: structured Markdown for execution decisions.
---

**SILENT MODE:** Execute end-to-end analysis and automation with no external chatter. Maintain internal telemetry.

## 1. Data Pipeline

### Market Snapshot
- Call `GetActiveMarket` to capture `marketId`, metadata, and `selections` (including `selectionId`, `name`, `price`, `status`).
- Treat any missing advanced pricing fields as `null`; record in provenance if unavailable.

### Timeform Enrichments
- Call `GetDataContextForMarket` with `dataContextNames = ["TimeformFullDataForHorses"]`.
- Expect `selectionsData[]` containing `expertView` text, `timeformHorseData` booleans, and `ratingStars` (0-5).

### Completeness Guard
- Compute the ratio of selections with both `expertView` and `timeformHorseData`.
- If ratio < 0.80, continue but flag in provenance (e.g., "Data completeness: 92% (12/13) — proceed with caution").
- Propagate `null` in derived metrics when data is missing.

## 2. Sentiment Scoring (Expert View)

### Section Weights
- Opening sentence: 20%
- Middle body: 50%
- Closing section: 30%
- Use first/last sentences as anchors; split remainder evenly.

### Token Scoring
- **Positive cues (Score: 100 pts):**
  - Strong form: "in good form", "in fine fettle", "well in", "progressed", "on the up", "top-rated", "speed figure improved", "good second", "merits consideration", "respected under"
  - Success language: "won", "scored", "triumphed", "prevailed", "victorious", "beaten all", "ran out a comfortable winner", "impressive", "convincing", "good first attempt", "came clear of the rest", "2 lb nudge looks more than fair", "respectable fourth"
  - Confidence markers: "should go very well", "chances", "not without claims", "well in", "goes for the hat-trick", "highly rated", "went close"
  - Suitability: "ideally suited", "suited by conditions", "perfect for this", "loves these conditions", "effective at distance", "course winner", "makes polytrack debut" (if positive context)
  - Positive progression: "progressive", "improving", "on the up", "steadily progressing", "continues to progress", "form figures rising"
  - Value indicators: "looks fairly treated", "on a fair mark", "well handicapped", "each-way chance", "value in the market", "underestimated"
  - Trainer/jockey confidence: "significant jockey booking", "top yard", "trainer in form", "represents a top combination", "stable confidence"
- **Negative cues (Score: 0 pts):**
  - Poor form: "ran poor", "disappointing", "below par", "ran below par", "well held", "struggled", "failed", "speed figure dropped", "has been very disappointing", "modest maiden", "eighth of 9", "last of 8", "poor form at best", "off since finishing last"
  - Negative results: "beaten", "outpaced", "poorly placed", "finished down the field", "never involved", "ran tamely", "sixth of 9"
  - Concerns: "needs to bounce back", "not since", "form has tailed off", "poor record at track", "needs improvement", "rating declined", "up in trip. makes handicap debut from a mark which demands improvement", "lightly-raced maiden"
  - Unsuitability: "doesn't stay", "not ideally suited", "difficulty with conditions", "ground too fast/soft", "unsuitable trip"
  - Lack of confidence: "needs to prove", "difficult to assess", "limited evidence", "not convinced", "poor performance", "question marks"
  - Negative recent activity: "raced freely", "hit the front too soon", "made too much use of", "wasted", "never got into"
- **Neutral/Mixed cues (Score: 50 pts):**
  - Balanced assessments: "not without chance", "some way behind", "has to improve", "not without a chance", "not without hope", "good fifth", "having to pick way through", "up in trip"
  - Conditional statements: "if conditions suit", "depending on trip", "on his best form", "if ground conditions allow"
  - Mixed results: "ran to form", "showed promise", "modest level", "some way off", "hasn't fired", "failed to reach full potential"
  - Return from absence: "returns from a break", "freshened", "after absence", "back from time off"
  - Neutral context: "effective at distance", "suited by trip", "acts on any ground" without positive reinforcement, "consistent rating"

### Sentiment Scoring Algorithm
1. Break the `expertView` text into sentences.
2. If 1 sentence: Treat as closing section (100% weight).
3. If 2 sentences: Opening 50%, closing 50%.
4. If 3+ sentences: Opening 20%, middle 50%, closing 30% (use first/last as anchors, split remainder evenly for middle).
5. Scan each section for keywords and assign points (100 for positive, 50 for neutral/mixed, 0 for negative).
6. Sum positive points and negative points per section (neutral contributes to both if applicable, but primarily as 50).
7. Weight the sums: Weighted Positive/Negative = Sum × Section Weight.
8. Total Weighted Positive/Negative across sections.
9. If (Total Weighted Positive + Total Weighted Negative) > 0: Score = (Total Weighted Positive - Total Weighted Negative) / (Total Weighted Positive + Total Weighted Negative); else Score = 0.
10. Clamp to [-1, 1].

### Example Calculation
- Text: "Horse is progressing well (opening); ran sound last time (middle); should win easily (closing)."
- Opening: "progressing well" → 100 pts positive.
- Middle: "ran sound" → 100 pts positive.
- Closing: "should win easily" → 100 pts positive.
- Weighted: Opening: 100×0.2=20; Middle: 100×0.5=50; Closing: 100×0.3=30.
- Total Pos: 20+50+30=100; Neg: 0.
- Sentiment Score: (100-0)/(100+0) = 1.0.
- Timeform: ratingStars=4 → 0.4; positives: horseInForm, suitedByDistance → 0.1; negatives: none.
- Timeform Score: 0.4 + 0.1 = 0.5.
- Combined Score: (1.0 + 0.5) / 2 = 0.75.

### Contextual Adjustments
- **Recency:** Prioritize most recent race mentions.
- **Narrative Tone:** Overall direction (positive/negative) takes precedence.
- **Context Factors:** Distance, ground conditions, competition class.
- **Statement Type:** Conditional phrases (e.g., "if") suggest uncertainty.
- **Final Assessment:** Closing remarks often summarize the expert's view.
- **Timeform Integration:** Factor in ratings trends, speed figures, and class levels.

### Semantic Patterns
- **Success + Improvement:** "scored...and is progressive" → Strong Positive.
- **Poor Result + Context:** "beaten but in good form" → Mixed/Neutral.
- **Recent Wins:** "won recently", "scored last time" → Positive.
- **Recent Struggles:** "ran poor last time" → Negative.
- **Future Outlook:** "should go well" → Positive.
- **Timeform Insights:** High speed figure + positive text → Very Positive; Declining ratings → Negative.

### Pre-Output Validation
- Calculate and verify all sentiment scores with detailed breakdowns.
- Ensure cross-horse consistency.
- Cross-check against Timeform quantitative data for alignment.

### Key Phrases
- Capture decisive phrases for summary (≤100 chars).

## 3. Timeform Quantitative Score

- **Base Score:** `ratingStars × 0.1` (range: 0 to 0.5).
- **Additions:** +0.05 for each true flag among the following 12: `horseWinnerLastTimeOut`, `horseInForm`, `suitedByGoing`, `suitedByCourse`, `suitedByDistance`, `trainerInForm`, `trainerCourseRecord`, `jockeyInForm`, `jockeyWonOnHorse`, `timeformTopRated`, `timeformImprover`, `timeformHorseInFocus`.
- **Subtractions:** -0.10 if `horseBeatenFavouriteLTO` is true.
- **Final Score:** Clamp to [-1.0, 1.0]. Set to `null` if `timeformHorseData` is missing.

## 4. Combined Metrics & EV

- **Combined Score:** Mean of Sentiment Score and Timeform Score (if both exist); otherwise `null`.
- **Estimated Probability:** `(Combined Score + 1) / 2` (range: [0, 1]).
- **Expected Value per €1 stake:** `estimated_prob × (price - 1) - (1 - estimated_prob) × 1.05`. Set to `null` if probability or price is missing.

## 5. Decision Logic

### Decision Criteria
- Sort selections by lowest odds first.
- Select the first horse with Combined Score ≥ 0.70 and full data availability.
- **Trigger:** Combined Score ≥ 0.70, completeness ≥ 80%, no nulls.
- **Action:** Execute €10 stake via `ExecuteBfexplorerStrategySettings(marketId, selectionId, "Bet 10 Euro")`; else NO_ACTION.

## 6. Output Specification

### Table Structure
Produce a GitHub-flavored Markdown table sorted by ascending odds:

| Horse | Odds | Sentiment Score | Timeform Score | Combined Score | Key Semantic Features | Decision |
|---|---:|---:|---:|---:|---|---|

### Output Formatting
- Numeric fields: Round to 4 decimals; `null` if unavailable.
- Key Semantic Features: Brief phrases; "No data" if none.
- Decision: `**BACK**` for chosen horse, `NO_ACTION` otherwise.

### Table Generation Guidelines
- **Horse:** Betfair selection name.
- **Odds:** Current decimal odds.
- **Sentiment Score:** Net sentiment (-1 to 1), from algorithm.
- **Timeform Score:** Quantitative score (-1 to 1), from data fields.
- **Combined Score:** Average of Sentiment and Timeform (-1 to 1).
- **Key Semantic Features:** Contributing phrases from `expertView` (concise, no full text).
- **Decision:** `**BACK**` for selected horse, else `NO_ACTION`.

### Statistics Summary
```
### Field Statistics:
- **Total Horses:** {count}
- **Data Completeness:** {percentage}% (selections with both expertView and timeformHorseData)
- **Highest Combined Score:** {score} ({horse_name})
- **Field Average Combined Score:** {average}
- **Combined Score Gap:** {best_score} - {field_average} = {gap}
```

### Recommendation Format
- **On Success:** `**Recommendation:** BACK {horse} (selectionId {selectionId}) at {odds} — Combined Score {score} ≥ 0.7 (data completeness {completeness}%)`
- **No Action:** `**Recommendation:** No BACK recommended — no selection reached Combined Score ≥ 0.7 threshold (highest: {horse} at {score})`

## 7. Quality Assurance

- Validate all score ranges and apply rounding before output.
- Ensure only one horse is marked `**BACK**`.
- Verify data completeness calculations and flag missing data provenance.
- Maintain audit trail: sentiment contributors, Timeform flags, applied assumptions.

## 8. Data Handling for Missing Inputs

- **Missing `expertView`:** Set Sentiment Score to `null`, Key Semantic Features to "No data".
- **Missing `timeformHorseData`:** Set Timeform Score to `null`.
- **Impact:** If either score is `null`, omit Combined Score, Estimated Probability, and EV calculations.

## 9. Illustrative Example

| Horse | Odds | Sentiment Score | Timeform Score | Combined Score | Key Semantic Features | Decision |
|---|---:|---:|---:|---:|---|---|
| Example Horse A | 6.2000 | 1.0000 | 0.5500 | 0.7750 | won maiden; scored cosily; one to consider | **BACK** |
| Example Horse B | 6.2000 | 0.3333 | 0.5500 | 0.4417 | close fourth; taken seriously | NO_ACTION |
| Example Horse C | 7.0000 | null | null | null | No data | NO_ACTION |
| Example Horse D | 9.0000 | -0.6000 | 0.2500 | -0.1750 | below form; more required | NO_ACTION |

### Field Statistics:
- **Total Horses:** 10
- **Data Completeness:** 90% (9/10)
- **Highest Combined Score:** 0.7750 (Example Horse A)
- **Field Average Combined Score:** 0.3542
- **Combined Score Gap:** 0.7750 - 0.3542 = 0.4208

**Recommendation:** BACK Example Horse A (selectionId 12345678_0.00) at 6.2000 — Combined Score 0.7750 ≥ 0.7 (data completeness 90%)