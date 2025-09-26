# Horse Racing EV Analysis - Favourite R6 Silent

**SILENT MODE:** Executes strategies without generating reports or outputs. All analysis performed internally.

## Data Retrieval & Analysis Framework

1. **Get Market Data**: Use `GetActiveBetfairMarket` for `marketId` and selections with odds
2. **Get Horse Data**: Use `GetAllDataContextForBetfairMarket` with `['TimeformDataForHorsesInfo', 'RacingpostDataForHorsesInfo']`

### Data Sources Summary:
**Timeform:** `ratingStars` (1-5), form booleans (`horseWinnerLastTimeOut`, `horseInForm`, `horseBeatenFavouriteLTO`), suitability booleans (`suitedByGoing/Course/Distance`), connections (`trainerInForm`, `jockeyInForm`), special designations (`timeformTopRated`, `timeformImprover`)

**Racing Post:** `lastRacesDescriptions` (position, beatenDistance, lastRunInDays, raceDescription), `rpRating`, `officialRating`

**Semantic Analysis of raceDescription:** Extract positive ("made all", "won easily", "kept on well") vs negative ("weakened", "never dangerous", "no extra") performance indicators.

## Scoring Framework (Internal Calculation Only)

### Combined Horse Score (Max 255 points):

**Base Rating (35%):**
- Timeform Stars: 5★=30, 4★=24, 3★=18, 2★=12, 1★=6 pts
- RP Rating: >100=40, 90-100=32, 80-89=24, 70-79=16, <70=8 pts (if available)

**Form Analysis (30%):**
- Timeform: `horseWinnerLastTimeOut`=+15, `horseInForm`=+10, `horseBeatenFavouriteLTO`=+5 pts
- Recent Performance: Win in last 3 runs=+10, Place=+5, Recent run(<30 days)=+5, Long layoff(>100 days)=-5 pts
- Semantic Analysis: Strong performance=+3, Positive finish=+2, Trouble excuse=+1, Poor performance=-2, Non-completion=-3 pts per race (last 3 runs)

**Suitability (20%):**
- Each Timeform suitability factor (`suitedByGoing/Course/Distance`) = +5 pts

**Connections (15%):**
- Timeform: `trainerInForm`=+4, `trainerCourseRecord`=+3, `jockeyInForm`=+3, `jockeyWonOnHorse`=+2 pts

**Special Factors (5%):**
- `timeformTopRated`=+5, `timeformImprover`=+3, `timeformHorseInFocus`=+2 pts

## Field Strength & EV Calculation (Internal)

### Field Assessment:
- **WEAK**: Avg field rating <60% of favourite, ≤1 horse within 80% of favourite score, ≤2 quality horses
- **MODERATE**: Avg 60-75%, 2-3 within 80%, 3-5 quality horses  
- **STRONG**: Avg >75%, ≥4 within 80%, ≥6 quality horses

### EV Calculation Steps:
1. Normalize scores: `(Horse Score/255) × 100`
2. Market share: Horse normalized score ÷ Sum of all normalized scores
3. True probability = Market share
4. Implied probability = `1/Decimal Odds`
5. **EV Formula:**
   - If True ≥ Implied: `((True-Implied)/Implied) × 100`
   - If True < Implied: `((True-Implied)/True) × 100`
   - Bound: `max(-100, min(100, EV))`

### Field Adjustments:
- **Quality Multipliers:** Top-rated + RP>90 = ×1.20, Top-rated OR RP>100 = ×1.15, Recent winner + in-form = ×1.08
- **Field Strength:** WEAK = ×1.15, MODERATE = ×1.00, STRONG = ×0.85
- **Opposition Threat:** 3+ high threats = ×0.90, 5+ medium threats = ×0.95

## Silent Strategy Execution

**IDENTIFY FAVOURITE:** Horse with lowest decimal odds

### Decision Logic (Internal):

**BACK FAVOURITE** if ALL true:
- EV: +8% to +50%
- Both data sources support selection
- ≥3 Timeform stars OR RP>75
- Recent positive form + semantic indicators
- WEAK/MODERATE field, ≤2 high threats
- Execute: `ExecuteBfexplorerStrategySettings(marketId, favouriteSelectionId, "Bet 10 Euro")`

**LAY FAVOURITE** if ALL true:
- EV: ≤-8% (more negative = better)
- STRONG field with ≥3 high threats OR poor semantic analysis OR odds <1.5 with poor value
- Execute: `ExecuteBfexplorerStrategySettings(marketId, favouriteSelectionId, "Lay 10 Euro")`

**NO ACTION** if:
- EV between -8% and +8%
- EV >+50% (unreliable)
- Criteria not met

**Field-Specific EV Thresholds:**
- WEAK: Back ≥+6%, Lay ≤-10%
- MODERATE: Back ≥+8%, Lay ≤-8%  
- STRONG: Back ≥+10%, Lay ≤-6%

**EXECUTION:** Perform ALL analysis internally. Execute strategy tool if criteria met. NO visible output, explanations, or analysis reports.
