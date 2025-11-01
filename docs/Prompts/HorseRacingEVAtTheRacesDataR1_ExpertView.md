# Horse Racing EV Analysis - AtTheRaces Data - the first horse that fulfills the Sentiment Score Thresholds for BACK

**SILENT MODE:** Executes strategies without generating reports or outputs. All analysis performed internally with performance tracking.

## 1. Data Retrieval & Validation Framework

1. **Get Market Data**: Use `GetActiveMarket` for `marketId` and selections with odds
2. **Get Horse Data**: Use `GetDataContextForMarket` with `dataContextName: "AtTheRacesDataForHorses"`
3. **Data Validation**: Ensure data completeness across all horses before proceeding

### Data Sources Summary:
**AtTheRaces:** `expertView` (textual analysis)

## 2. Semantic Analysis & Evolution Framework

### Expert View Analysis:
**Perform comprehensive semantic analysis on the `expertView` text to assess overall sentiment and indicators of horse performance:**

**POSITIVE SENTIMENT INDICATORS (Score: 100 pts):**
- Strong form indicators: "in good form", "in fine fettle", "well in", "progressed", "on the up"
- Success language: "won", "scored", "triumphed", "prevailed", "victorious", "beaten all", "ran out a comfortable winner", "impressive", "convincing"
- Confidence markers: "should go very well", "chances", "not without claims", "well in", "goes for the hat-trick"
- Suitability: "ideally suited", "suited by conditions", "perfect for this", "loves these conditions", "effective at distance"
- Positive progression: "progressive", "improving", "on the up", "steadily progressing", "continues to progress"
- Value indicators: "looks fairly treated", "on a fair mark", "well handicapped", "each-way chance", "value in the market"
- Trainer/jockey confidence: "significant jockey booking", "top yard", "trainer in form", "represents a top combination"

**NEGATIVE SENTIMENT INDICATORS (Score: 0 pts):**
- Poor form: "ran poor", "disappointing", "below par", "ran below par", "well held", "struggled", "failed"
- Negative results: "beaten", "outpaced", "poorly placed", "finished down the field", "never involved", "ran tamely"
- Concerns: "needs to bounce back", "not since", "form has tailed off", "poor record at track", "needs improvement"
- Unsuitability: "doesn't stay", "not ideally suited", "difficulty with conditions", "ground too fast/soft"
- Lack of confidence: "needs to prove", "difficult to assess", "limited evidence", "not convinced", "poor performance"
- Negative recent activity: "raced freely", "hit the front too soon", "made too much use of", "wasted", "never got into"

**NEUTRAL/MIXED SENTIMENT INDICATORS (Score: 50 pts):**
- Balanced assessments: "not without chance", "some way behind", "has to improve", "not without a chance", "not without hope"
- Conditional statements: "if conditions suit", "depending on trip", "on his best form", "if ground conditions allow"
- Mixed results: "ran to form", "showed promise", "modest level", "some way off", "hasn't fired", "failed to reach full potential"
- Return from absence: "returns from a break", "freshened", "after absence", "back from time off"
- Neutral context: "effective at distance", "suited by trip", "acts on any ground" without positive reinforcement

**SENTIMENT SCORING ALGORITHM:**
- Scan for positive keywords (weighted by impact: 1-3 points each)
- Scan for negative keywords (weighted by impact: 1-3 points each)
- Calculate net sentiment score: (Summed positive points - Summed negative points) / (Summed positive points + Summed negative points) if (Summed positive points + Summed negative points) > 0, else 0
- Sentiment Score = Net sentiment score (ranges from -1 to 1)

**VALIDATION STEP:** Before generating the table, calculate and list all sentiment scores with breakdowns. Ensure consistency across horses.

**CONTEXTUAL WEIGHTING (Additional modifiers):**
- Recency: Most recent race performance mentioned has highest weight
- Narrative tone: Overall text direction (positive vs negative) overrides individual phrases
- Performance context: Consider distance, ground conditions, distance suitability
- Conditional vs definitive: Phrases like "if" and "but" may indicate mixed sentiment
- Expert's final assessment: The concluding statement often represents the overall assessment

**SEMANTIC PATTERN RECOGNITION:**
- Success with improvement: "scored...and is progressive" = Strong Positive
- Poor result with positive context: "beaten but in good form" = Mixed/Neutral
- Recent success: "won recently", "scored last time" = Positive
- Poor recent form: "ran poor last time", "beaten in last run" = Negative
- Positive future outlook: "should go well", "chances on improvement" = Positive

**ADDITIONAL SENTIMENT EVALUATION CRITERIA:**

**POSITIVE SENTIMENT INTENSITY:**
- Strong positive: "should go very well", "excellent chance", "strongly fancied", "likely to go well", "faced easier opposition"
- Moderate positive: "not without chance", "some chance", "goes well here", "decent claim", "in form"
- Weak positive: "not without hope", "slight chance", "chance on improvement"

**NEGATIVE SENTIMENT INTENSITY:**
- Strong negative: "poor recent form", "needs to prove", "not since", "struggling", "failed to fire"
- Moderate negative: "ran poor last time", "not convinced", "some way behind", "not ideally suited"
- Weak negative: "needs to improve", "on return", "not without doubt"

**SENTIMENT CONTINUUM SCALING:**
- Very Positive: 0.9-1.0 (Clear positive with strong indicators)
- Positive: 0.75-0.89 (Positive with some concerns)
- Moderate Positive: 0.6-0.74 (Slightly more positive than neutral)
- Neutral: 0.4-0.59 (Balanced with equal positive and negative)
- Moderate Negative: 0.25-0.39 (Slightly more negative than neutral)
- Negative: 0.1-0.24 (Negative with few positives)
- Very Negative: -1.0-0.09 (Overwhelmingly negative)

**CONTEXTUAL ADJUSTMENTS:**
- Recent performance: Higher weight than past performance
- Ground/distance suitability: Significant impact on sentiment
- Competition class: Performance against similar standard
- Track familiarity: Previous success at venue
- Jockey/trainer combination: Reputation impact

**SENTIMENT VALIDATION CHECKS:**
- Verify sentiment aligns with performance narrative
- Check for contradictory statements
- Ensure final assessment is consistent with body content
- Confirm expert's confidence level is correctly interpreted

## 3. Scoring Framework (Internal Calculation Only)

### Combined Horse Score (Net Sentiment Score):

**Expert View Sentiment (Net Score):**
- Sentiment Score = Net sentiment score (ranges from -1 to 1)

## 4. Decision Logic & Strategy Execution

**IDENTIFY BEST HORSE:** Sort horses by Odds ascending, then select the first horse that fulfills the Sentiment Score Thresholds for BACK

### Sentiment Score Thresholds for BACK:
- Back if highest Sentiment Score >=0.8

### Decision Criteria:
**BACK Requirements (ALL must be true):**
1. Highest Sentiment Score in field >= 0.8
2. Data completeness ≥80%

### Execution Flow:
1. **Execute Strategy**: `ExecuteBfexplorerStrategySettings(marketId, bestHorseSelectionId, "Bet 10 Euro")` if BACK criteria met
3. **Silent Performance Tracking**: Log internally for optimization

## 5. Market Down Table

Based on the active market data, the following table shows EV calculations for each horse:

| Horse | Odds | Expert Analysis | Sentiment Score | Key Semantic Features | Decision |
|-------|------|-----------------|-----------------|-----------------------|----------|
{horse_rows}

### Table Generation Rules:
- **Horse**: Selection name from Betfair
- **Odds**: Decimal odds from market data
- **Expert Analysis**: Short semantic summary (Positive/Neutral/Negative)
- **Sentiment Score**: Calculated net sentiment score (-1 to 1) - MUST use the precise algorithm output; no approximations
- **Key Semantic Features**: Key phrases from expertView that contributed to the Positive/Neutral/Negative sentiment classification (do not repeat the full expertView text)
- **Decision**: BACK / NO ACTION (highlight best horse decision in **bold**)

### Field Statistics:
- **Total Horses:** {total_horses}
- **Data Completeness:** {completeness_percentage}%
- **Highest Sentiment Score:** {best_horse_score}
- **Field Average Sentiment Score:** {field_average}
- **Sentiment Score Gap:** {best_horse_score} - {field_average} = {score_gap}

### Sentiment Score Analysis Summary:
- **Best Horse Sentiment Score:** {best_horse_score}

### Strategy Logic Applied:
- **Best Horse Selection:** Highest Sentiment Score, tie broken by lowest Odds
- **Sentiment Score Threshold:** >=0.8 for BACK
- **Result:** Score {best_horse_score} meets threshold → BACK execution</content>
<parameter name="filePath">e:\Projects\BetfairAiTrading\docs\Prompts\HorseRacingEVAtTheRacesDataR1_ExpertView.md