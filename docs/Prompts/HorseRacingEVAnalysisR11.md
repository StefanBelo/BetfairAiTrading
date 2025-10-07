# Horse Racing EV Analysis - R11 Best Horse Strategy

**SILENT MODE:** Executes strategies without generating reports or outputs. All analysis performed internally with performance tracking.

## 1. Data Retrieval & Validation Framework

1. **Get Market Data**: Use `GetActiveBetfairMarket` for `marketId` and selections with odds
2. **Get Horse Data**: Use `GetAllDataContextForBetfairMarket` with `['TimeformDataForHorsesInfo', 'RacingpostDataForHorsesInfo']`
3. **Data Validation**: Ensure ≥80% data completeness across all horses before proceeding

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

## 2. Semantic Analysis & Evolution Framework

### Race History Analysis:
**Process all available `raceDescription` from `lastRaces[]` array (up to 6 races):**
- Recent form (last 3 races): Primary weight with `lastRunInDays` recency factor
- Historical baseline (races 4-6): Comparison reference
- **Cross-validation**: Compare `raceDescription` with `position`, `beatenDistance`, and `topspeed` for consistency
- **Weight Analysis**: Consider `weightCarried` changes between races
- **Distance Suitability**: Analyze performance at similar `distance` values

### Evolution Scoring (Apply to Form Analysis):
**Performance Trend Analysis Using Racing Post Data:**

**Position Trend Analysis (using `position` from `lastRaces[]`):**
- Improving positions (e.g., 8th→5th→3rd): +6 pts
- Consistent top-3 finishes in last 3 runs: +5 pts
- Stable mid-field positions: +2 pts
- Declining positions (e.g., 3rd→6th→9th): -3 pts
- Consistently poor positions (>8th): -5 pts

**Beaten Distance Analysis (using `beatenDistance` from `lastRaces[]`):**
- Shrinking margins in recent runs (e.g., 8L→4L→2L): +4 pts
- Consistently close finishes (<3L in last 3): +3 pts
- Expanding margins (getting further behind): -2 pts
- Large margins consistently (>10L): -4 pts

**Speed Figure Trends (using `topspeed` from `lastRaces[]`):**
- Improving topspeed trend (last 3 runs): +3 pts
- Consistent high figures (50+ in last 3): +2 pts
- Declining speed figures: -2 pts
- Inconsistent or low figures (<30): -1 pt

**Individual Race Contextual Scoring (Applied to each `raceDescription`):**
- Recent races (1-3 from `lastRaces[]`): Strong=+4, Positive=+2, Neutral=0, Poor=-3, Very poor=-4
- Historical races (4-6 from `lastRaces[]`): Same scoring × 0.6 weighting
- **Validation Requirements**: Cross-check semantic score with actual `position`, `beatenDistance`, and `topspeed`
- **Recency Weighting**: Use `lastRunInDays` to adjust influence (recent <30 days gets full weight)

**Key Analysis Factors:** Class progression, `distance` adaptation, going conditions, field strength, `weightCarried` impact, layoff periods using `lastRunInDays`

## 3. Scoring Framework (Internal Calculation Only)

### Combined Horse Score (Max 100 points):

**Base Rating (35%):**
- Timeform Stars: 5★=35, 4★=28, 3★=21, 2★=14, 1★=7 pts
- RP Rating: Scale proportionally to 0-35 points (RP 120+ = 35, RP 100 = 28, RP 80 = 21, RP 60 = 14, RP <60 = 7)

**Enhanced Form Analysis (40%) - With Racing Post Integration:**
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
- **Evolution Analysis**: Apply evolution scoring from Section 2 with Racing Post data integration
- **Semantic Analysis**: Apply contextual scoring for all available `raceDescription` entries with cross-validation

**Suitability (15%):**
- Each Timeform suitability factor (`suitedByGoing/Course/Distance`) = +5 pts

**Connections (10%):**
- Timeform: `trainerInForm`=+4, `trainerCourseRecord`=+3, `jockeyInForm`=+3, `jockeyWonOnHorse`=+2 pts

## 4. Field Analysis & Position Metrics

### Field Analysis Calculation:
- **Highest Scoring Horse**: Horse with highest combined score
- **Field Average Score**: Average score across all horses with data
- **Score Gap**: Difference between highest score and field average
- **Quality Depth**: Number of horses scoring ≥80% of highest score

### Field Strength Categories:
**ELITE**: Top≥85 + Gap≥25 → Threshold +8%  
**STRONG**: Top≥75 + Gap≥20 → Threshold +10%  
**MODERATE**: Top≥65 + Gap≥15 → Threshold +12%  
**COMPETITIVE**: Top≥55 + Gap≥10 → Threshold +15%  
**WEAK**: Top<55 OR Gap<10 → Threshold +18%

## 5. EV Calculation & Dynamic Adjustments

### EV Calculation Steps:
1. Calculate scores for ALL horses with sufficient data
2. Normalize scores: `(Horse Score/100) × 100`
3. Market share: Horse normalized score ÷ Sum of all normalized scores
4. True probability = Market share
5. Implied probability = `1/Decimal Odds`
6. **EV Formula:**
   - If True ≥ Implied: `((True-Implied)/Implied) × 100`
   - If True < Implied: `((True-Implied)/True) × 100`
   - Bound: `max(-100, min(100, EV))`

### Dynamic Quality Multipliers (Applied After Base EV):
- **Quality Multipliers:** 5★ + RP>100 = ×1.25, 4★ + RP>90 = ×1.15, Recent winner + in-form = ×1.08
- **Field Strength:** WEAK = ×1.20, COMPETITIVE = ×1.15, MODERATE = ×1.10, STRONG = ×1.05, ELITE = ×1.00
- **Evolution Factor:** Strong improvement = ×1.15, Moderate improvement = ×1.08, Stable = ×1.00, Decline = ×0.95
- **Market Position:** Odds >10.0 = ×1.10 (undervalued longshots), Odds 3.0-10.0 = ×1.00, Odds <3.0 = ×0.95

## 6. Decision Logic & Strategy Execution

**IDENTIFY BEST HORSE:** Horse with highest EV after all adjustments

### EV Thresholds for BACK:
- **ELITE Field**: Back if EV ≥+8%
- **STRONG Field**: Back if EV ≥+10%  
- **MODERATE Field**: Back if EV ≥+12%
- **COMPETITIVE Field**: Back if EV ≥+15%
- **WEAK Field**: Back if EV ≥+18%

### Decision Criteria:
**BACK Requirements (ALL must be true):**
1. Highest EV in field + meets field threshold
2. Data completeness ≥80% + ≥3★ OR RP>70
3. Evolution ≠ strong decline + odds 1.5-50.0
4. EV ≤60% (reliability check)

**NO ACTION Triggers (ANY true):**
- EV below threshold OR >60% OR data <80%
- Strong decline evolution OR odds outside 1.5-50.0

### Execution Flow:
1. **Execute Strategy**: `ExecuteBfexplorerStrategySettings(marketId, bestHorseSelectionId, "Bet 10 Euro")` if BACK criteria met
2. **Save Decision Data**: `SetAIAgentDataContextForBetfairMarket` immediately after execution
3. **Silent Performance Tracking**: Log internally for optimization

## 7. Error Handling & Data Management

**Error Protocols:**
- Data incomplete/calculation error → NO ACTION + log
- Market volatility → increase EV threshold by 3%

**EXECUTION MODE:** Silent analysis → Execute if criteria met → Save decision data → Track performance internally. NO visible outputs during execution.

### Decision Data Logging:
`SetAIAgentDataContextForBetfairMarket(marketId, "HorseRacingEVAnalysisR11_DecisionData", jsonData)`

**Required JSON Structure:**
```json
{
  "metadata": {"timestamp": "{ISO}", "version": "R11", "marketId": "{id}", "event": "{name}", "fieldSize": {n}, "dataComplete": {%}},
  "field": {"strength": "{category}", "topScore": {n}, "avgScore": {n}, "gap": {n}, "threshold": {%}},
  "horses": [{"id": "{selectionId}", "name": "{name}", "score": {n}, "racingPostAnalysis": {"recentPositions": [1,3,5], "beatenDistanceTrend": [0,2,4], "topspeedTrend": [45,48,52], "weightChange": "+3lbs", "distanceSuitability": "good"}, "marketShare": {%}, "baseEV": {%}, "finalEV": {%}, "evolution": "{pattern}", "odds": {n}, "decision": "{BACK/NO ACTION}"}],
  "execution": {"bestHorse": "{name}", "bestEV": {%}, "decision": "{action}", "time": "{ISO}"}
}
```

## 8. Post-Execution Reporting (After Strategy Execution Only)

**CONDITIONAL OUTPUT:** Generate this report ONLY when a BACK strategy is executed. Skip for NO ACTION decisions.

### Market Summary:
- **Market ID:** `{marketId}`
- **Race:** `{eventName} - {marketName}`
- **Best Horse:** `{bestHorseName} ({bestHorseOdds})`
- **Decision:** `BACK {amount}`
- **Execution Time:** `{timestamp}`

### Complete Field Analysis Table:

| Horse | Odds | Score | Market Share | EV | Evolution | Decision |
|-------|------|-------|--------------|----|-----------|----------|
{horse_rows}

### Table Generation Rules:
- **Horse**: Selection name from Betfair
- **Odds**: Decimal odds from market data
- **Score**: Calculated horse score (0-100)
- **Market Share**: Horse score ÷ total field scoring × 100
- **EV**: Expected value percentage with dynamic adjustments
- **Evolution**: Strong Improvement / Moderate Improvement / Stable / Moderate Decline / Strong Decline / Erratic
- **Decision**: BACK / NO ACTION (highlight best horse decision in **bold**)

### Field Statistics:
- **Total Horses:** {total_horses}
- **Data Completeness:** {completeness_percentage}%
- **Highest Score:** {best_horse_score}
- **Field Average Score:** {field_average}
- **Score Gap:** {best_horse_score} - {field_average} = {score_gap} pts
- **Quality Depth:** {quality_depth_count} horses ≥80% of best score

### EV Analysis Summary:
- **Best Horse EV:** {best_horse_ev}%
- **Field Strength Category:** {ELITE/STRONG/MODERATE/COMPETITIVE/WEAK}
- **Applied Multipliers:** Quality: ×{quality_multiplier}, Field: ×{field_multiplier}, Evolution: ×{evolution_multiplier}, Position: ×{position_multiplier}
- **Final EV:** {base_ev}% × {total_multiplier} = {final_ev}%

### Strategy Logic Applied:
- **Best Horse Selection:** Highest EV after all adjustments
- **EV Threshold:** {field_strength_threshold}% for {field_strength} field
- **Result:** EV {final_ev}% meets threshold → BACK execution