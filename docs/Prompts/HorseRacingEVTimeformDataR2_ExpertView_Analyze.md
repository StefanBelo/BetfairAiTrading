# Horse Racing Analysis — Timeform Expert View (Research Mode)

---
**Title:** HorseRacingEVTimeformDataR2_ExpertView_Analyze.md  
**Purpose:** Comprehensive analysis of Betfair market data and Timeform expert inputs to identify which factors are most predictive of race winners. **Output:** JSON data saved via `SetAIAgentDataContextForBetfairMarket` for research and analysis.
---

**ANALYSIS MODE:** Perform detailed calculations for each horse with full transparency. Show all intermediate values to help identify the most important predictive factors.

## 1. Data Pipeline

### Market Snapshot
- Call `GetActiveMarket` to capture `marketId`, metadata, and `selections` (including `selectionId`, `name`, `price`, `status`).
- Treat any missing advanced pricing fields as `null`; record in provenance if unavailable.

### Timeform Enrichments
- Call `GetDataContextForMarket` with `dataContextName = "TimeformFullDataForHorses"`.
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

### Grouped Flag Analysis
Group the 12 timeform flags into three logical categories for more nuanced scoring:

#### Form & Performance Group (40% weight)
- `horseWinnerLastTimeOut`, `horseInForm`, `timeformTopRated`, `timeformImprover`, `timeformHorseInFocus`
- **Subscore:** (count of true flags × 0.05) × 0.4
- **Purpose:** Recent form and rating trends

#### Suitability Group (35% weight)  
- `suitedByGoing`, `suitedByCourse`, `suitedByDistance`
- **Subscore:** (count of true flags × 0.05) × 0.35
- **Purpose:** Match to race conditions

#### Trainer/Jockey Group (25% weight)
- `trainerInForm`, `trainerCourseRecord`, `jockeyInForm`, `jockeyWonOnHorse`
- **Subscore:** (count of true flags × 0.05) × 0.25
- **Purpose:** Handler quality and experience

### Final Timeform Score Calculation
- **Base Score:** `ratingStars × 0.1` (range: 0 to 0.5)
- **Additions:** Sum of the three group subscores
- **Subtractions:** -0.10 if `horseBeatenFavouriteLTO` is true
- **Final Score:** Clamp to [-1.0, 1.0]. Set to `null` if `timeformHorseData` is missing.

## 4. Combined Metrics & EV

- **Combined Score:** Mean of Sentiment Score and Timeform Score (if both exist); otherwise `null`.
- **Estimated Probability:** `(Combined Score + 1) / 2` (range: [0, 1]).
- **Expected Value per €1 stake:** `estimated_prob × (price - 1) - (1 - estimated_prob) × 1.05`. Set to `null` if probability or price is missing.

## 5. Output Specification

### JSON Output for AI Agent Data Context

After performing all calculations, save the analysis results as JSON data using the `SetAIAgentDataContextForBetfairMarket` tool with `dataContextName = "HorseRacingEVTimeformAnalysis"`.

The JSON structure should be:

```json
{
  "marketId": "string",
  "eventName": "string",
  "marketName": "string",
  "dataCompleteness": number,
  "horses": [
    {
      "name": "string",
      "selectionId": "string",
      "odds": number,
      "sentimentScore": number,
      "timeformScore": number,
      "combinedScore": number,
      "estimatedProbability": number,
      "expectedValue": number,
      "sentimentBreakdown": {
        "openingWeightedPos": number,
        "middleWeightedPos": number,
        "closingWeightedPos": number,
        "totalPosPoints": number,
        "totalNegPoints": number
      },
      "timeformBreakdown": {
        "ratingStars": number,
        "baseScore": number,
        "formGroupCount": number,
        "formSubscore": number,
        "suitabilityGroupCount": number,
        "suitabilitySubscore": number,
        "trainerJockeyGroupCount": number,
        "trainerJockeySubscore": number,
        "negativeFlagsCount": number,
        "negativeAdjustment": number
      }
    }
  ],
  "fieldStatistics": {
    "totalHorses": number,
    "dataCompleteness": "string",
    "highestCombinedScore": number,
    "highestHorseName": "string",
    "fieldAverageCombinedScore": number,
    "combinedScoreGap": number,
    "sentimentTimeformCorrelation": number
  }
}
```

### Output Formatting
- All numeric fields: Round to 4 decimals; `null` if unavailable
- Ensure JSON is valid and complete

### Research Insights Section
- **Factor Importance Analysis:** Identify which individual flags and groups have the strongest correlation with high combined scores
- **Group Performance Analysis:** Compare the predictive power of Form, Suitability, and Trainer/Jockey groups across different race types
- **Score Distribution Analysis:** Show how scores cluster and identify thresholds for different performance levels
- **Data Quality Impact:** Analyze how completeness affects score reliability and predictive power
- **Cross-Validation:** Test grouped vs individual flag scoring to determine which approach provides better predictive accuracy

## 6. Quality Assurance

- Validate all score ranges and apply rounding before output.
- Ensure cross-horse consistency in calculations.
- Verify data completeness calculations and flag missing data provenance.
- Maintain audit trail: sentiment contributors, Timeform flags, applied assumptions.
- Analyze correlations between different data sources to identify predictive patterns.
- Track which combinations of factors lead to highest scores for future model refinement.

## 7. Data Handling for Missing Inputs

- **Missing `expertView`:** Set `sentimentScore` and `sentimentBreakdown` to `null`.
- **Missing `timeformHorseData`:** Set `timeformScore` and `timeformBreakdown` to `null`.
- **Impact:** If either score is `null`, set `combinedScore`, `estimatedProbability`, and `expectedValue` to `null`.
- **Analysis Note:** Track patterns in missing data to identify potential data quality issues.

## 8. Illustrative Example

```json
{
  "marketId": "1.123456789",
  "eventName": "Example Racecourse",
  "marketName": "2m Hurdle",
  "dataCompleteness": 90.0,
  "horses": [
    {
      "name": "Example Horse A",
      "selectionId": "12345678_0.00",
      "odds": 6.2000,
      "sentimentScore": 1.0000,
      "timeformScore": 0.4700,
      "combinedScore": 0.7350,
      "estimatedProbability": 0.8675,
      "expectedValue": 1.3125,
      "sentimentBreakdown": {
        "openingWeightedPos": 20.0000,
        "middleWeightedPos": 50.0000,
        "closingWeightedPos": 30.0000,
        "totalPosPoints": 100,
        "totalNegPoints": 0
      },
      "timeformBreakdown": {
        "ratingStars": 4,
        "baseScore": 0.4000,
        "formGroupCount": 2,
        "formSubscore": 0.0400,
        "suitabilityGroupCount": 1,
        "suitabilitySubscore": 0.0175,
        "trainerJockeyGroupCount": 1,
        "trainerJockeySubscore": 0.0125,
        "negativeFlagsCount": 0,
        "negativeAdjustment": 0.0000
      }
    },
    {
      "name": "Example Horse B",
      "selectionId": "87654321_0.00",
      "odds": 6.2000,
      "sentimentScore": 0.3333,
      "timeformScore": 0.4500,
      "combinedScore": 0.3917,
      "estimatedProbability": 0.6958,
      "expectedValue": 0.6458,
      "sentimentBreakdown": {
        "openingWeightedPos": 10.0000,
        "middleWeightedPos": 25.0000,
        "closingWeightedPos": 15.0000,
        "totalPosPoints": 50,
        "totalNegPoints": 0
      },
      "timeformBreakdown": {
        "ratingStars": 4,
        "baseScore": 0.4000,
        "formGroupCount": 1,
        "formSubscore": 0.0200,
        "suitabilityGroupCount": 1,
        "suitabilitySubscore": 0.0175,
        "trainerJockeyGroupCount": 1,
        "trainerJockeySubscore": 0.0125,
        "negativeFlagsCount": 0,
        "negativeAdjustment": 0.0000
      }
    }
  ],
  "fieldStatistics": {
    "totalHorses": 10,
    "dataCompleteness": "90% (9/10)",
    "highestCombinedScore": 0.7500,
    "highestHorseName": "Example Horse A",
    "fieldAverageCombinedScore": 0.3542,
    "combinedScoreGap": 0.3958,
    "sentimentTimeformCorrelation": 0.65
  }
}
```