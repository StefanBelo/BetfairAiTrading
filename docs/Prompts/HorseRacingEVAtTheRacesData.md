# Horse Racing EV Analysis - AtTheRaces Data

**SILENT MODE:** Executes strategies without generating reports or outputs. All analysis performed internally with performance tracking.

## 1. Data Retrieval & Validation Framework

1. **Get Market Data**: Use `GetActiveBetfairMarket` for `marketId` and selections with odds
2. **Get Horse Data**: Use `GetDataContextForBetfairMarket` with `dataContextName: "AtTheRacesDataForHorses"`
3. **Data Validation**: Ensure data completeness across all horses before proceeding

### Data Sources Summary:
**AtTheRaces:** `expertView` (textual analysis), `rating` (numerical rating), `starRating` (1-5 stars)

## 2. Semantic Analysis & Evolution Framework

### Expert View Analysis:
**Perform comprehensive semantic analysis on the `expertView` text to assess overall sentiment and indicators of horse performance:**

**POSITIVE SENTIMENT INDICATORS (Score: 60 pts):**
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

**NEUTRAL/MIXED SENTIMENT INDICATORS (Score: 30 pts):**
- Balanced assessments: "not without chance", "some way behind", "has to improve", "not without a chance", "not without hope"
- Conditional statements: "if conditions suit", "depending on trip", "on his best form", "if ground conditions allow"
- Mixed results: "ran to form", "showed promise", "modest level", "some way off", "hasn't fired", "failed to reach full potential"
- Return from absence: "returns from a break", "freshened", "after absence", "back from time off"
- Neutral context: "effective at distance", "suited by trip", "acts on any ground" without positive reinforcement

**SENTIMENT SCORING ALGORITHM:**
- Scan for positive keywords (weighted by impact: 1-3 points each)
- Scan for negative keywords (weighted by impact: 1-3 points each)
- Calculate net sentiment score: (Positive points - Negative points) / Total points scanned
- If net sentiment ≥ 0.5: Positive (60 pts)
- If net sentiment < 0.25: Negative (0 pts) 
- Otherwise: Neutral (30 pts)

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
- Very Positive: 55-60 pts (Clear positive with strong indicators)
- Positive: 45-54 pts (Positive with some concerns)
- Moderate Positive: 35-44 pts (Slightly more positive than neutral)
- Neutral: 25-34 pts (Balanced with equal positive and negative)
- Moderate Negative: 15-24 pts (Slightly more negative than neutral)
- Negative: 5-14 pts (Negative with few positives)
- Very Negative: 0-4 pts (Overwhelmingly negative)

**SEMANTIC ANALYSIS WEIGHTS:**
- Opening sentence: 20% weight
- Middle content: 50% weight 
- Closing assessment: 30% weight (typically contains the expert's final view)

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

### Combined Horse Score (Max 100 points):

**Base Rating (20%):**
- Star Rating: 5★=20, 4★=16, 3★=12, 2★=8, 1★=4 pts

**AtTheRaces Rating (20%):**
- Scale rating proportionally to 0-20 points (rating 80+ = 20, rating 60 = 16, rating 40 = 12, rating 20 = 8, rating <20 = 4)

**Expert View Sentiment (60%):**
- Positive = 60 pts, Neutral = 30 pts, Negative = 0 pts

## 3. EV Calculation & Dynamic Adjustments

### EV Calculation Steps:
1. Calculate scores for ALL horses with data
2. Normalize scores: `(Horse Score/100) × 100`
3. Market share: Horse normalized score ÷ Sum of all normalized scores
4. True probability = Market share
5. Implied probability = `1/Decimal Odds`
6. **EV Formula:**
   - If True ≥ Implied: `((True-Implied)/Implied) × 100`
   - If True < Implied: `((True-Implied)/True) × 100`
   - Bound: `max(-100, min(100, EV))`

### Dynamic Quality Multipliers (Applied After Base EV):
- **Star Rating Multipliers:** 5★ = ×1.20, 4★ = ×1.10, 3★ = ×1.00, 2★ = ×0.95, 1★ = ×0.90
- **Rating Multipliers:** Rating >70 = ×1.15, Rating 60-70 = ×1.05, Rating <60 = ×0.95

## 4. Decision Logic & Strategy Execution

**IDENTIFY BEST HORSE:** Horse with highest EV after all adjustments

### EV Thresholds for BACK:
- Back if EV ≥+10%

### Decision Criteria:
**BACK Requirements (ALL must be true):**
1. Highest EV in field ≥ +10%
2. Data completeness ≥80%

**NO ACTION if ANY of the following conditions are met:**
- Highest EV < +10%
- Data completeness < 80%

### Execution Flow:
1. **Execute Strategy**: `ExecuteBfexplorerStrategySettings(marketId, bestHorseSelectionId, "Bet 10 Euro")` if BACK criteria met
2. **Save Decision Data**: `SetAIAgentDataContextForBetfairMarket` with comprehensive data for each horse, including base values used in EV calculation (e.g., selection IDs, scores, market shares, base EVs, final EVs, sentiments)
3. **Silent Performance Tracking**: Log internally for optimization

## 5. Market Down Table

Based on the active market data, the following table shows EV calculations for each horse:

| Horse | Odds | Expert Analysis | Score | Final EV | Key Semantic Features | Decision |
|-------|------|-----------------|-------|----------|-----------------------|----------|
{horse_rows}

### Table Generation Rules:
- **Horse**: Selection name from Betfair
- **Odds**: Decimal odds from market data
- **Expert Analysis**: Short semantic summary (Positive/Neutral/Negative)
- **Score**: Calculated horse score (0-100)
- **Final EV**: EV after multipliers
- **Key Semantic Features**: Key phrases from expertView that contributed to the Positive/Neutral/Negative sentiment classification (do not repeat the full expertView text)
- **Decision**: BACK / NO ACTION (highlight best horse decision in **bold**)

### Field Statistics:
- **Total Horses:** {total_horses}
- **Data Completeness:** {completeness_percentage}%
- **Highest Score:** {best_horse_score}
- **Field Average Score:** {field_average}
- **Score Gap:** {best_horse_score} - {field_average} = {score_gap} pts

### EV Analysis Summary:
- **Best Horse EV:** {best_horse_ev}%
- **Applied Multipliers:** Star Rating: ×{star_multiplier}, Rating: ×{rating_multiplier}
- **Final EV:** {base_ev}% × {total_multiplier} = {final_ev}%

### Strategy Logic Applied:
- **Best Horse Selection:** Highest EV after all adjustments
- **EV Threshold:** +10% for BACK
- **Result:** EV {final_ev}% meets threshold → BACK execution