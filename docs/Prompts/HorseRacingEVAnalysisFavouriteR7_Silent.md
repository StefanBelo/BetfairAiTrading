# Horse Racing EV Analysis - Favourite R7 Silent

**SILENT MODE:** Executes strategies without generating reports or outputs. All analysis performed internally with performance tracking.

## 1. Data Retrieval & Validation Framework

1. **Get Market Data**: Use `GetActiveBetfairMarket` for `marketId` and selections with odds
2. **Get Horse Data**: Use `GetAllDataContextForBetfairMarket` with `['TimeformDataForHorsesInfo', 'RacingpostDataForHorsesInfo']`
3. **Data Validation**: Ensure ≥80% data completeness across all horses before proceeding

### Data Sources Summary:
**Timeform:** `ratingStars` (1-5), form booleans (`horseWinnerLastTimeOut`, `horseInForm`, `horseBeatenFavouriteLTO`), suitability booleans (`suitedByGoing/Course/Distance`), connections (`trainerInForm`, `jockeyInForm`), special designations (`timeformTopRated`, `timeformImprover`)

**Racing Post:** `lastRacesDescriptions` (position, beatenDistance, lastRunInDays, raceDescription), `rpRating`, `officialRating`

## 2. Semantic Analysis Framework

Analyze `raceDescription` contextually for last 3 races:
- **Positive indicators**: Evidence of strong finishing, competitive racing, overcoming adversity, or winning/losing by narrow margins
- **Negative indicators**: Signs of poor performance, lack of competitiveness, significant weakening, or failure to complete
- **Context factors**: Consider race class, distance, ground conditions, and field strength when evaluating performance
- **Scoring**: Strong contextual performance=+4, Positive competitive showing=+2, Neutral/average performance=0, Below expectations=-3, Poor showing=-4 pts per race

## 3. Scoring Framework (Internal Calculation Only)

### Combined Horse Score (Max 100 points):

**Base Rating (40%):**
- Timeform Stars: 5★=40, 4★=32, 3★=24, 2★=16, 1★=8 pts
- RP Rating: Scale proportionally to 0-40 points (RP 120+ = 40, RP 100 = 32, RP 80 = 24, RP 60 = 16, RP <60 = 8)

**Form Analysis (35%):**
- Timeform: `horseWinnerLastTimeOut`=+12, `horseInForm`=+8, `horseBeatenFavouriteLTO`=+4 pts
- Recent Performance: Win in last 3 runs=+10, Place=+5, Recent run(<30 days)=+5, Long layoff(>100 days)=-5 pts
- Semantic Analysis: Apply contextual scoring from Section 2 above

**Suitability (15%):**
- Each Timeform suitability factor (`suitedByGoing/Course/Distance`) = +5 pts

**Connections (10%):**
- Timeform: `trainerInForm`=+4, `trainerCourseRecord`=+3, `jockeyInForm`=+3, `jockeyWonOnHorse`=+2 pts

**Special Factors (5%):**
- `timeformTopRated`=+5, `timeformImprover`=+3, `timeformHorseInFocus`=+2 pts

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
- **Opposition Threat:** 3+ high threats = ×0.90, 5+ medium threats = ×0.95
- **Market Volatility:** Odds movement >5% in last 5 mins = ×0.95

## 6. Decision Logic & Strategy Execution

**IDENTIFY FAVOURITE:** Horse with lowest decimal odds

### Field-Relative EV Thresholds:
- **DOMINANT Favourite**: Back ≥+3%, Lay ≤-10%
- **STRONG Favourite**: Back ≥+5%, Lay ≤-8%
- **MODERATE Favourite**: Back ≥+7%, Lay ≤-6%
- **VULNERABLE Favourite**: Back ≥+10%, Lay ≤-4%
- **WEAK Favourite**: Back ≥+12%, Lay ≤-2%

### BACK FAVOURITE Criteria (ALL must be true):
- EV meets field-relative threshold (above)
- Both data sources support selection (≥70% confidence)
- ≥3 Timeform stars OR RP>75
- Recent positive form + semantic indicators
- Field position meets DOMINANT/STRONG criteria

### LAY FAVOURITE Criteria (ALL must be true):
- EV meets field-relative threshold (above)
- Field position meets VULNERABLE/WEAK criteria
- ≥3 quality threats OR favourite represents <20% of field scoring

### NO ACTION Criteria (ANY true):
- EV between field-relative thresholds
- EV >+45% (unreliable)
- Data completeness <80%
- Mixed field-relative signals

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

**EXECUTION:** Perform ALL analysis internally. Execute strategy tool if criteria met. NO visible output, explanations, or analysis reports. Track performance metrics silently.