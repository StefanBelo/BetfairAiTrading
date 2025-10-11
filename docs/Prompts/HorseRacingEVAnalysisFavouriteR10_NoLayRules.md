# Horse Racing EV Analysis - Favourite R10 No LAY Rules

**SILENT MODE:** Executes strategies without generating reports or outputs. All analysis performed internally with performance tracking.

## 1. Data Retrieval & Validation Framework

1. **Get Market Data**: Use `GetActiveBetfairMarket` for `marketId` and selections with odds
2. **Get Horse Data**: Use `GetAllDataContextForBetfairMarket` with `['TimeformDataForHorses', 'RacingpostDataForHorses']`
3. **Data Validation**: Ensure ≥80% data completeness across all horses before proceeding

### Data Sources Summary:
**Timeform:** `ratingStars` (1-5), form booleans (`horseWinnerLastTimeOut`, `horseInForm`, `horseBeatenFavouriteLTO`), suitability booleans (`suitedByGoing/Course/Distance`), connections (`trainerInForm`, `jockeyInForm`), special designations (`timeformTopRated`, `timeformImprover`)

**Racing Post:** `lastRacesDescriptions` (position, beatenDistance, lastRunInDays, raceDescription), `rpRating`, `officialRating`

## 2. Enhanced Semantic Analysis Framework

### Race History Analysis (Up to 6 Races):
Analyze `raceDescription` contextually for **all available races** (up to 6 races per horse if data exists):
- **Last 3 races** (most recent): Primary focus for current form
- **Previous 3 races** (4-6 races back): Historical baseline for evolution analysis

### Performance Evolution Comparison:
Compare performance patterns between historical (races 4-6) and recent (races 1-3) periods:

**Positive Evolution Indicators:**
- **Improving form**: Better finishing positions in recent races vs historical
- **Rising competitiveness**: Narrower margins of defeat in recent races
- **Adapting to conditions**: Better performance in similar race conditions recently
- **Overcoming adversity**: Recovery from poor historical races to competitive recent form

**Negative Evolution Indicators:**
- **Declining form**: Worse finishing positions in recent races vs historical
- **Losing competitiveness**: Wider margins of defeat in recent races
- **Struggling with conditions**: Worse performance in similar conditions recently
- **Inconsistent performance**: Erratic results without clear pattern improvement

**Stable Performance Patterns:**
- **Consistent performer**: Similar performance levels across all periods
- **Class-appropriate**: Performance matches expected level for race class
- **Condition specialist**: Strong in specific going/course/distance combinations

### Contextual Analysis Factors:
- **Race class progression/regression**: Moving up/down in class appropriately
- **Distance adaptation**: Performance changes with distance variations
- **Ground condition performance**: Adaptation to different going types
- **Field strength relativity**: Performance vs quality of opposition
- **Weight carried impact**: Performance changes with different weights
- **Rest between races**: Impact of layoff periods on performance

### Evolution Scoring Matrix:
**Historical vs Recent Performance Comparison:**
- **Strong improvement**: Recent races significantly better than historical (+6 pts)
- **Moderate improvement**: Recent races noticeably better (+4 pts)
- **Stable performance**: Consistent across periods (+2 pts)
- **Moderate decline**: Recent races noticeably worse (-3 pts)
- **Strong decline**: Recent races significantly worse (-5 pts)
- **Erratic performance**: No clear pattern (-2 pts)

**Individual Race Scoring (per race):**
- **Last 3 races**: Strong contextual performance=+4, Positive competitive showing=+2, Neutral/average=0, Below expectations=-3, Poor showing=-4 pts
- **Previous 3 races**: Same scoring but weighted at 60% (historical baseline)

## 3. Scoring Framework (Internal Calculation Only)

### Combined Horse Score (Max 100 points):

**Base Rating (35%):**
- Timeform Stars: 5★=35, 4★=28, 3★=21, 2★=14, 1★=7 pts
- RP Rating: Scale proportionally to 0-35 points (RP 120+ = 35, RP 100 = 28, RP 80 = 21, RP 60 = 14, RP <60 = 7)

**Enhanced Form Analysis (40%):**
- Timeform: `horseWinnerLastTimeOut`=+10, `horseInForm`=+7, `horseBeatenFavouriteLTO`=+3 pts
- Recent Performance: Win in last 3 runs=+8, Place=+4, Recent run(<30 days)=+4, Long layoff(>100 days)=-4 pts
- **Evolution Analysis**: Apply evolution scoring from Section 2 (+6 to -5 pts)
- **Semantic Analysis**: Apply contextual scoring from Section 2 for all available races

**Suitability (15%):**
- Each Timeform suitability factor (`suitedByGoing/Course/Distance`) = +5 pts

**Connections (10%):**
- Timeform: `trainerInForm`=+4, `trainerCourseRecord`=+3, `jockeyInForm`=+3, `jockeyWonOnHorse`=+2 pts

## 4. Field Analysis & Position Metrics

### Field Position Calculation:
- **Favourite Dominance Score**: Favourite score minus field average
- **Quality Threat Count**: Horses with score ≥70 (within 10 points of favourite)
- **Favourite Market Share**: Favourite score ÷ total field scoring × 100

### Field Strength Categories:
- **DOMINANT**: ≥20 points above field average, ≤1 quality threat
- **STRONG**: 15-20 points above field average, 2 quality threats
- **MODERATE**: 10-15 points above field average, 3 quality threats
- **VULNERABLE**: 5-10 points above field average, 4+ quality threats
- **WEAK**: <5 points above field average, 5+ quality threats

## 5. EV Calculation & Dynamic Adjustments

### EV Calculation Steps:
1. Normalize scores: `(Horse Score/100) × 100`
2. Market share: Horse normalized score ÷ Sum of all normalized scores
3. True probability = Market share
4. Implied probability = `1/Decimal Odds`
5. **EV Formula:**
   - If True ≥ Implied: `((True-Implied)/Implied) × 100`
   - If True < Implied: `((True-Implied)/True) × 100`
   - Bound: `max(-100, min(100, EV))`

### Dynamic Quality Multipliers (Applied After Base EV):
- **Quality Multipliers:** Top-rated + RP>90 = ×1.20, Top-rated OR RP>100 = ×1.15, Recent winner + in-form = ×1.08
- **Field Strength:** WEAK = ×1.15, MODERATE = ×1.00, STRONG = ×0.85
- **Opposition Threat:** 2+ high threats = ×0.92, 3+ medium threats = ×0.96, 4+ threats = ×0.90
- **Market Volatility:** Odds movement >5% in last 5 mins = ×0.95
- **Evolution Factor:** Strong improvement = ×1.10, Moderate improvement = ×1.05, Stable = ×1.00, Decline = ×0.95

## 6. Decision Logic & Strategy Execution

**IDENTIFY FAVOURITE:** Horse with lowest decimal odds

### Field-Relative EV Thresholds:
- **DOMINANT Favourite**: Back ≥+2%
- **STRONG Favourite**: Back ≥+3%
- **MODERATE Favourite**: Back ≥+5%
- **VULNERABLE Favourite**: Back ≥+7%
- **WEAK Favourite**: Back ≥+9%

### BACK FAVOURITE Criteria (ALL must be true):
- EV meets field-relative threshold (above)
- Both data sources support selection (≥70% confidence)
- ≥3 Timeform stars OR RP>75
- Recent positive form + semantic indicators
- Field position meets DOMINANT/STRONG criteria
- **Evolution shows improvement or stability** (not strong decline)

### SIMPLIFIED LAY STRATEGY:
**If BACK criteria are NOT met, execute LAY strategy automatically**
- **No additional LAY-specific criteria required**
- **Execute LAY regardless of field strength, evolution, or opposition**

### NO ACTION Criteria (ANY true, overrides LAY):
- EV >+45% (unreliable)
- Data completeness <70%
- Both EV >+15% AND EV <-15% (neutral zone for extreme volatility)

### Execution Commands:
- **BACK**: `ExecuteBfexplorerStrategySettings(marketId, favouriteSelectionId, "Bet 10 Euro")`
- **LAY**: `ExecuteBfexplorerStrategySettings(marketId, favouriteSelectionId, "Lay 10 Euro")`

## 7. Error Handling & Performance Monitoring

### Error Handling:
- If data incomplete: Skip market, log internally
- If calculation error: Default to NO ACTION
- If market volatility high: Increase EV threshold by 2%

### Performance Monitoring (Internal):
- Track EV accuracy vs actual outcomes
- Monitor win rate by field strength
- Adjust thresholds based on rolling 50-market performance
- Log execution decisions for optimization
- **Track evolution accuracy**: Monitor if improvement/decline predictions correlate with results

**EXECUTION:** Perform ALL analysis internally. Execute strategy tool if criteria met. NO visible output, explanations, or analysis reports. Track performance metrics silently.

## 8. Post-Execution Reporting (After Strategy Execution Only)

**CONDITIONAL OUTPUT:** Generate this report ONLY when a BACK or LAY strategy is executed. Skip for NO ACTION decisions.

### Market Summary:
- **Market ID:** `{marketId}`
- **Race:** `{eventName} - {marketName}`
- **Favourite:** `{favouriteName} ({favouriteOdds})`
- **Decision:** `{BACK/LAY} {amount}`
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
- **Decision**: BACK / LAY / NO ACTION (highlight favourite decision in **bold**)

### Field Statistics:
- **Total Horses:** {total_horses}
- **Data Completeness:** {completeness_percentage}%
- **Field Average Score:** {field_average}
- **Favourite Dominance:** {favourite_score} - {field_average} = {dominance_points} pts
- **Quality Threats:** {quality_threat_count} (≥70 pts)
- **Favourite Market Share:** {favourite_market_share}%

### EV Analysis Summary:
- **Favourite EV:** {favourite_ev}%
- **Field Strength Category:** {DOMINANT/STRONG/MODERATE/VULNERABLE/WEAK}
- **Applied Multipliers:** Quality: ×{quality_multiplier}, Field: ×{field_multiplier}, Evolution: ×{evolution_multiplier}, Opposition: ×{opposition_multiplier}
- **Final EV:** {base_ev}% × {total_multiplier} = {final_ev}%

### Strategy Logic Applied:
- **BACK Criteria Check:** {MET/NOT MET}
- **Result:** {If BACK criteria not met → Automatic LAY execution}