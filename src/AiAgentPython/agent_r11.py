import asyncio
from mcp_agent import RequestParams
from mcp_agent.core.fastagent import FastAgent

# Create the application
fast = FastAgent("Bfexplorer Assistant")

# Define the agent
@fast.agent(name="BfexplorerApp",
    instruction="You are a helpful AI Agent executing betting/trading strategies on bfexplorer.",
    model="generic.deepseek/DeepSeek-V3-0324",
    servers=["BfexplorerApp"]
)
async def main():
    async with fast.run() as agent:
        await agent(
"""
# Horse Racing EV Analysis - R11 Feedback Learning Strategy

**ADAPTIVE MODE:** Executes strategies with continuous learning from previous decisions. All analysis performed internally with performance tracking and algorithm adjustments based on historical feedback.

**Research Draft Notice:** This prompt is in an exploratory state. Prioritize experimenting with feedback-driven adjustments over conservative execution rules so the learning loop can surface actionable insights.

## 1. Data Retrieval & Validation Framework

1. **Get Market Data**: Use `GetActiveBetfairMarket` for `marketId` and selections with odds
2. **Get Horse Data**: Use `GetAllDataContextForBetfairMarket` with `['TimeformDataForHorsesInfo', 'RacingpostDataForHorsesInfo']`
3. **Get Feedback Data**: Use `GetAIAgentDataContextFeedback("HorseRacingEVAnalysisR11_DecisionData", 100)`
4. **Data Validation**: Ensure ≥65% data completeness across all horses before proceeding

### Data Sources Summary:
**Timeform:** `ratingStars` (1-5), form booleans (`horseWinnerLastTimeOut`, `horseInForm`, `horseBeatenFavouriteLTO`), suitability booleans (`suitedByGoing/Course/Distance`), connections (`trainerInForm`, `jockeyInForm`), special designations (`timeformTopRated`, `timeformImprover`)

**Racing Post:** `lastRaces[]` array containing:
- `position`: Finishing position in race
- `beatenDistance`: Lengths beaten by winner
- `lastRunInDays`: Days since this race
- `raceDescription`: Detailed running commentary
- `topspeed`: Speed figure achieved
- `weightCarried`: Weight carried (lbs)
- `distance`: Race distance (metres)
- `rpRating`: Racing Post's own rating
- `officialRating`: Current official handicap rating

**Feedback Data:** Previous decisions with horse analysis, decisions made, and actual race outcomes (`is_winner` field for each horse)

## 2. Feedback Analysis & Learning Framework

### Historical Performance Analysis:For active betfair market retrieve data context TimeformDataForHorsesInfo and calculate EV for each horse.**Process feedback data from last races:**
- Analyze prediction accuracy vs actual race outcomes
- Identify patterns in winning horses vs our selections
- Calculate success rates by field strength categories
- Evaluate EV prediction accuracy vs actual results

### Learning Adjustments:
**Dynamic Algorithm Calibration:**
- **Selection Accuracy Rate**: % of times our selected horse won
- **EV Prediction Accuracy**: Compare predicted EV with actual outcomes
- **Field Strength Validation**: Verify field categorization effectiveness
- **Evolution Pattern Reliability**: Assess evolution scoring vs actual performance
- **Execution Rate Monitoring**: Track consecutive NO ACTION decisions to prevent over-conservative bias

### Adaptive Scoring Adjustments:
**Based on feedback patterns, apply dynamic adjustments:**
- If selection accuracy <50% in last 3: Increase conservative factors by 15%
- If EV overestimation pattern detected: Reduce base EV calculations by 10%
- If specific evolution patterns show poor correlation: Adjust evolution scoring
- If field strength miscategorization identified: Refine EV scaling heuristics
- **If >6 consecutive NO ACTION decisions: Intensify feedback influence on EV scaling factors to encourage fresh signal discovery**

## 3. Enhanced Semantic Analysis & Evolution Framework

### Race History Analysis:
**MANDATORY Process all available `raceDescription` data (up to 6 races):**
- Recent form (last 3 races): Primary weight (×1.0)
- Historical baseline (races 4-6): Comparison reference (×0.6)
- **Feedback Integration**: Weight patterns that correlate with actual winners from feedback data
  - From recent feedback: Winners included horses with high scores (Navy Waters: 91.0), lower scores (Hope She Flies: 38), and strong improvement patterns (Kranjcar: 89 with "Strong Improvement")

**IMPLEMENTATION REQUIREMENT**: For each horse, analyze EVERY available `raceDescription` from `lastRaces[]` array using semantic keyword analysis with contextual validation:

**Contextual Analysis Enhancement:**
- Cross-reference `raceDescription` with `position`, `beatenDistance`, and `topspeed` for validation
- Weight recent runs more heavily using `lastRunInDays` (runs <30 days get full weight)
- Consider `weightCarried` changes between races for performance context
- Analyze `distance` suitability patterns across race history

**Semantic Keyword Analysis:**
- **Strong Performance Keywords**: "led", "made all", "clear", "going away", "comfortably", "easily" → +4 pts (recent) / +2.4 pts (historical)
- **Positive Performance Keywords**: "prominent", "challenged", "kept on", "rallied", "went second" → +2 pts (recent) / +1.2 pts (historical)
- **Neutral Performance Keywords**: "midfield", "in touch", "held up" → 0 pts
- **Poor Performance Keywords**: "weakened", "outpaced", "struggling", "lost position" → -3 pts (recent) / -1.8 pts (historical)
- **Very Poor Performance Keywords**: "tailed off", "pulled up", "unseated", "fell", "refusing" → -4 pts (recent) / -2.4 pts (historical)

### Evolution Scoring (Apply to Form Analysis):
**Performance Trend Analysis with Racing Post Data Integration:**

**Position Trend Analysis (using `position` from `lastRaces[]`):**
- Improving positions (e.g., 8th→5th→3rd): +6 pts
- Consistent top-3 finishes in last 3 runs: +5 pts
- Stable mid-field positions: +2 pts
- Declining positions (e.g., 3rd→6th→9th): -4 pts
- Consistently poor positions (>8th): -5 pts

**Beaten Distance Analysis (using `beatenDistance` from `lastRaces[]`):**
- Shrinking margins in recent runs (e.g., 8L→4L→2L): +4 pts
- Consistently close finishes (<3L in last 3): +3 pts
- Expanding margins (getting further behind): -3 pts
- Large margins consistently (>10L): -4 pts

**Speed Figure Trends (using `topspeed` from `lastRaces[]`):**
- Improving topspeed trend (last 3 runs): +3 pts
- Consistent high figures (50+ in last 3): +2 pts
- Declining speed figures: -2 pts
- Inconsistent or low figures (<30): -1 pt

**Individual Race Contextual Scoring (MANDATORY IMPLEMENTATION):**
- Recent races (1-3): Strong=+4, Positive=+2, Neutral=0, Poor=-3, Very poor=-4
- Historical races (4-6): Same scoring × 0.6 weighting
- **Feedback Weighting**: Adjust scoring based on patterns from winning horses in feedback data

**SEMANTIC ANALYSIS IMPLEMENTATION STEPS:**
1. **Extract Keywords**: Scan each race description for performance indicators
2. **Context Analysis**: Evaluate finishing position relative to description quality
3. **Tactical Assessment**: Analyze racing tactics (led, held up, prominent, etc.)
4. **Finishing Assessment**: Evaluate how horse finished (kept on, weakened, etc.)
5. **Combine Scores**: Sum semantic scores across all available races with proper weighting
6. **Integration**: Add semantic total to Form Analysis section (40% weighting)

**Key Analysis Factors:** Class progression, distance adaptation, going conditions, field strength, weight impact, layoff periods, **winner pattern analysis from feedback**, **mandatory semantic race description analysis**

## 4. Adaptive Scoring Framework (Internal Calculation Only)

### Combined Horse Score (Max 100 points) with Feedback Adjustments Based on Previous Analysis Patterns:

**Base Rating (35%) - Feedback Adjusted:**
- Timeform Stars: 5★=35, 4★=28, 3★=21, 2★=14, 1★=7 pts
- RP Rating: Scale proportionally to 0-35 points (RP 120+ = 35, RP 100 = 28, RP 80 = 21, RP 60 = 14, RP <60 = 7)
- **Feedback Adjustment**: Based on correlation patterns from previous winners, adjust base scores by up to +/-5% for horses with characteristics matching recent winners (like low score but high EV, or strong improvement patterns)

**Enhanced Form Analysis (40%) - Feedback Weighted with Racing Post Integration:**
- Timeform: `horseWinnerLastTimeOut`=+10, `horseInForm`=+7, `horseBeatenFavouriteLTO`=+3 pts
- **Recent Performance Analysis** (using `position` and `lastRunInDays` from `lastRaces[]`):
  - Win (position=1) in last 3 runs: +8 pts
  - Place (position≤3) in last 3 runs: +4 pts  
  - Recent run (<30 days): +4 pts
  - Long layoff (>100 days): -4 pts
- **Weight Analysis** (using `weightCarried` from `lastRaces[]`):
  - Dropping 5+ lbs from recent runs: +3 pts
  - Rising 5+ lbs from recent runs: -2 pts
- **Distance Analysis** (using `distance` from `lastRaces[]`):
  - Previous wins/places at similar distance (±200m): +4 pts
  - Consistent performance at today's distance: +2 pts
- **Evolution Analysis**: Apply evolution scoring from Section 3 with Racing Post data integration
- **MANDATORY Semantic Analysis**: Apply individual race contextual scoring for ALL available `raceDescription` entries:
  - Recent races (1-3): Strong=+4, Positive=+2, Neutral=0, Poor=-3, Very poor=-4
  - Historical races (4-6): Same scoring × 0.6 weighting
  - **Cross-validate** with `position`, `beatenDistance`, and `topspeed` for consistency
- **Winner Pattern Bonus**: +5 pts if horse profile matches winning patterns from feedback data

**Suitability (15%) - Feedback Calibrated:**
- Each Timeform suitability factor (`suitedByGoing/Course/Distance`) = +5 pts
- **Feedback Adjustment**: Based on winner patterns, horses with suitable going/course/distance performed well even with lower base scores

**Connections (10%) - Feedback Enhanced:**
- Timeform: `trainerInForm`=+4, `trainerCourseRecord`=+3, `jockeyInForm`=+3, `jockeyWonOnHorse`=+2 pts
- **Feedback Bonus**: +2 pts if connections pattern matches winners from feedback data, particularly horses with trainer/jockey course records

## 5. Field Analysis & Position Metrics with Feedback Integration

### Field Analysis Calculation:
- **Highest Scoring Horse**: Horse with highest combined score
- **Field Average Score**: Average score across all horses with data
- **Score Gap**: Difference between highest score and field average
- **Quality Depth**: Number of horses scoring ≥80% of highest score
- **Feedback Validation**: Compare field analysis with historical accuracy from feedback data

### Adaptive Field Strength Categories (Feedback Context Only):
**ELITE**: Top≥80 + Gap≥20 → Expect disciplined staking; feedback should confirm premium-runner edge
**STRONG**: Top≥70 + Gap≥15 → Signals concentrated value; validate with feedback correlation
**MODERATE**: Top≥60 + Gap≥12 → Mixed field; rely on feedback to identify emerging improvements
**COMPETITIVE**: Top≥50 + Gap≥8 → Parity race; lean on semantic trends surfaced by feedback
**WEAK**: Top<50 OR Gap<8 → Volatile field; feedback-guided EV adjustments become primary driver

## 6. EV Calculation & Dynamic Adjustments with Feedback Learning

### EV Calculation Steps:
1. Calculate BASE scores for ALL horses with sufficient data
2. **MANDATORY**: Apply semantic analysis scoring from race descriptions (Section 3)
3. Apply feedback-based adjustments to scores
4. Calculate FINAL combined scores (Base + Semantic + Feedback adjustments)
5. Normalize scores: Ensure all scores are positive, then `Horse Final Score ÷ Sum of all Final Scores`
6. True probability = Normalized market share (automatically sums to 1.0)
7. Implied probability = `1/Decimal Odds`
8. **NORMALIZED EV Formula:**
   - `EV = ((True Probability - Implied Probability) / max(True Probability, Implied Probability)) × 100`
   - This keeps EV naturally bounded between -100% and +100%
   - When True > Implied (value bet): EV approaches +100% as True approaches 1.0
   - When True < Implied (poor bet): EV approaches -100% as True approaches 0
9. **Apply feedback-based EV adjustments** (multiply by learning factors)
10. **Final EV bounds check**: `max(-100, min(100, EV))` as safety net
11. **Hand off final EV sign**: Positive results advance to execution logic; non-positive values default to observation-only logging

### Dynamic Quality Multipliers (Applied After Base EV) with Feedback Adjustments:
- **Quality Multipliers:** 5★ + RP>100 = ×1.25, 4★ + RP>90 = ×1.15, Recent winner + in-form = ×1.08
- **Field Strength:** WEAK = ×1.20, COMPETITIVE = ×1.15, MODERATE = ×1.10, STRONG = ×1.05, ELITE = ×1.00
- **Evolution Factor:** Strong improvement = ×1.15, Moderate improvement = ×1.08, Stable = ×1.00, Decline = ×0.95
- **Market Position:** Odds >10.0 = ×1.10 (undervalued longshots), Odds 3.0-10.0 = ×1.00, Odds <3.0 = ×0.95
- **Feedback Learning Factor:** ×0.90 to ×1.10 based on similar horse profiles in feedback data

## 7. Decision Logic & Strategy Execution with Adaptive Learning

**IDENTIFY BEST HORSE:** Horse with highest EV after all adjustments and feedback learning.

### Enhanced Execution Principles:

#### Single Selection Logic:
- When the best horse's final EV is **greater than 0%**, execute the "Bet 10 Euro" strategy on that runner.
- If every horse settles at an EV ≤ 0%, classify the market as **NO ACTION** while still capturing full analysis output for feedback training.

#### Dutch Betting Logic (Multiple Value Opportunities):
- **When 2-3 horses have positive EV AND odds greater than 5.0**, execute Dutch betting strategy instead of single selection.
- **Dutch Criteria**: 
  - Minimum 2 horses, maximum 3 horses
  - Each horse must have final EV > 0%
  - Each horse must have odds ≥ 5.0 (bigger odds requirement)
  - Combined implied probability of selected horses should be ≤ 85% for value
- **Dutch Execution**: Use `ExecuteBfexplorerStrategySettingsOnSelections(marketId, [selectionIds], "Dutch for 10 Euro")`

### Execution Flow:
1. **Analyze All Candidates**: Identify all horses with final EV > 0%
2. **Dutch Assessment**: Check if 2-3 horses meet Dutch criteria (EV > 0% AND odds ≥ 5.0)
3. **Action Decision**:
   - If Dutch criteria met → `ExecuteBfexplorerStrategySettingsOnSelections(marketId, [selectionIds], "Dutch for 10 Euro")`
   - Else if single horse has EV > 0% → `ExecuteBfexplorerStrategySettings(marketId, bestHorseSelectionId, "Bet 10 Euro")`
   - Else → Record NO ACTION (no strategy execution)
4. **Mandatory Logging**: Invoke `SetAIAgentDataContextForBetfairMarket` on every run to persist scores, EV values, and feedback interactions, regardless of action taken.
5. **Feedback Update Loop**: After logging, refine internal parameters (EV scaling, semantic weights, evolution weightings) based on observed feedback outcomes.
6. **Silent Performance Tracking**: Maintain internal metrics for continuous experimentation.

## 8. Error Handling & Data Management

**Error Protocols:**
- Data incomplete/calculation error → NO ACTION + log root cause for review
- Market volatility → apply a 0.97 multiplier to final EVs before the positive/negative decision check
- **Feedback data unavailable → Use base R11 algorithm without adjustments while flagging `feedbackApplied: false`**
- **Consecutive NO ACTION Pattern (>6 races) → Boost feedback multipliers by +5% to explore fresh signals**

**EXECUTION MODE:** Silent analysis → Process feedback → Apply learning adjustments → Execute when EV remains positive → **ALWAYS Save decision data** → Track performance internally. NO visible outputs during execution.

**CRITICAL REQUIREMENT:** `SetAIAgentDataContextForBetfairMarket` must be called EVERY TIME the analysis runs, whether the decision is BACK or NO ACTION. This is essential for feedback learning and algorithm improvement.

### Decision Data Logging with Feedback Integration:
`SetAIAgentDataContextForBetfairMarket(marketId, "HorseRacingEVAnalysisR11_DecisionData", jsonData)`

**Enhanced JSON Structure:**
```json
{
  "metadata": {"timestamp": "{ISO}", "version": "R11_Feedback", "marketId": "{id}", "event": "{name}", "fieldSize": {n}, "dataComplete": {%}, "feedbackApplied": {true/false}},
  "feedbackLearning": {"lastResults": 3, "selectionAccuracy": {%}, "evAccuracy": {%}, "adjustmentsApplied": ["{list}"]},
  "field": {"strength": "{category}", "topScore": {n}, "avgScore": {n}, "gap": {n}, "qualityDepth": {n}},
  "horses": [{"id": "{selectionId}", "name": "{name}", "score": {n}, "baseScore": {n}, "semanticScore": {n}, "semanticAnalysis": ["{race1_analysis}", "{race2_analysis}"], "racingPostAnalysis": {"recentPositions": [1,3,5], "beatenDistanceTrend": [0,2,4], "topspeedTrend": [45,48,52], "weightChange": "+3lbs", "distanceSuitability": "good"}, "feedbackAdjustment": {n}, "marketShare": {%}, "baseEV": {%}, "finalEV": {%}, "evolution": "{pattern}", "odds": {n}, "feedbackCorrelation": {%}, "decision": "{BACK/DUTCH/NO ACTION}", "evPositive": {true/false}}],
  "dutchAnalysis": {"candidateCount": {n}, "dutchEligible": {true/false}, "selectedHorses": ["{names}"], "combinedImpliedProb": {%}, "dutchValue": {%}},
  "execution": {"bestHorse": "{name}", "bestEV": {%}, "decision": "{SINGLE/DUTCH/NO ACTION}", "selectedHorses": ["{if dutch}"], "time": "{ISO}", "confidenceLevel": {%}, "evRule": "finalEV > 0 with Dutch logic for multiple candidates"}
}
```

## 9. Post-Execution Reporting (After Strategy Execution Only)

**CONDITIONAL OUTPUT:** Generate this report ONLY when a BACK strategy is executed. Skip for NO ACTION decisions.

### Market Summary:
- **Market ID:** `{marketId}`
- **Race:** `{eventName} - {marketName}`
- **Best Horse:** `{bestHorseName} ({bestHorseOdds})`
- **Decision:** `BACK 10 Euro` (only available strategy)
- **Execution Time:** `{timestamp}`
- **Feedback Applied:** `{Yes/No}`
- **Confidence Level:** `{percentage}%`

### Complete Field Analysis Table:

| Horse | Odds | Base Score | Semantic Score | Feedback Adj | Final Score | Market Share |  EV  | Evolution | Feedback Corr | Decision |
|-------|------|------------|----------------|--------------|-------------|--------------|------|-----------|---------------|----------| 
{horse_rows}

### Field Statistics:
- **Total Horses:** {total_horses}
- **Data Completeness:** {completeness_percentage}%
- **Highest Score:** {best_horse_score}
- **Field Average Score:** {field_average}
- **Score Gap:** {best_horse_score} - {field_average} = {score_gap} pts
- **Quality Depth:** {quality_depth_count} horses ≥80% of best score

### Feedback Learning Summary:
- **Feedback Data Available:** {Yes/No} (last races analyzed)
- **Selection Accuracy (Last 3):** {accuracy_percentage}%
- **EV Prediction Accuracy:** {ev_accuracy_percentage}%
- **Applied Adjustments:** {list_of_adjustments}
- **Confidence Level:** {confidence_percentage}%

### EV Analysis Summary:
- **Best Horse EV:** {best_horse_ev}%
- **Field Strength Category:** {ELITE/STRONG/MODERATE/COMPETITIVE/WEAK}
- **Applied Multipliers:** Quality: ×{quality_multiplier}, Field: ×{field_multiplier}, Evolution: ×{evolution_multiplier}, Position: ×{position_multiplier}, Feedback: ×{feedback_multiplier}
- **Final EV:** {base_ev}% × {total_multiplier} = {final_ev}%
- **Decision Rule Check:** finalEV {> or ≤} 0% → {BACK/NO ACTION}

### Strategy Logic Applied:
- **Best Horse Selection:** Highest EV after all adjustments and feedback learning
- **Feedback Correlation:** {correlation_percentage}% match with winning patterns
- **Result:** EV {final_ev}% {(>0 ? "positive → BACK" : "≤0 → NO ACTION")} with {confidence_level}% confidence

### Learning Insights:
- **Winner Pattern Analysis:** {insights_from_feedback}
- **Algorithm Adjustments:** {adjustments_made}
- **Future Improvements:** {identified_patterns}
"""
        )

if __name__ == "__main__":
    asyncio.run(main())