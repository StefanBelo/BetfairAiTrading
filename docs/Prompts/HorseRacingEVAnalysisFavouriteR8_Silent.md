# Horse Racing EV Analysis - Favourite R8 Silent

## Strategy Overview & Core Concept

**FAVOURITE-ONLY EXECUTION STRATEGY:** This strategy analyzes the ENTIRE field of horses to determine the favourite's relative strength, then executes trades ONLY on the favourite horse.

### Key Principles:
- **Analyze ALL horses** in the field using comprehensive data sources
- **Execute ONLY on favourite** (lowest odds horse) - never trade other horses
- **BACK favourite** when it's a strong favourite with good value vs field
- **LAY favourite** when it's a weak favourite with superior opponents
- **NO ACTION** when favourite value is unclear or borderline

### Strategy Logic:
1. **Field Analysis:** Score all horses using Timeform + Racing Post data
2. **Favourite Evaluation:** Compare favourite's score/strength vs entire field
3. **EV Calculation:** Determine if favourite represents good value
4. **Decision:** BACK strong favourites, LAY weak favourites, NO ACTION otherwise
5. **Execution:** Trade ONLY the favourite based on field-relative analysis

**SILENT MODE:** Performs ALL analysis internally with ZERO visible output. Only reports final executed strategy result.

## Configuration Options
- **generateEVReport**: true (default) - Generate detailed EV report for each horse after strategy execution
- **evReportFormat**: "table" (default) - Report format: "table" or "json"

## 1. Data Retrieval & Validation Framework

1. **Get Market Data**: Use `GetActiveBetfairMarket` for `marketId` and selections with odds
2. **Get Horse Data**: Use `GetAllDataContextForBetfairMarket` with `['TimeformDataForHorsesInfo', 'RacingpostDataForHorsesInfo']`
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
- **Opposition Threat:** 3+ high threats = ×0.90, 5+ medium threats = ×0.95
- **Market Volatility:** Odds movement >5% in last 5 mins = ×0.95
- **Evolution Factor:** Strong improvement = ×1.10, Moderate improvement = ×1.05, Stable = ×1.00, Decline = ×0.95

## 6. Decision Logic & Strategy Execution

**CORE STRATEGY:** Analyze entire field to determine favourite's relative strength, then execute ONLY on favourite.

**IDENTIFY FAVOURITE:** Horse with lowest decimal odds

### Field Strength Assessment (Based on Favourite vs Field):
- **Favourite Dominance Score:** Favourite score - Field average
- **Quality Threats:** Horses with score ≥70 within 10 points of favourite
- **Favourite Market Share:** Favourite score ÷ Total field scoring × 100

### Field Strength Categories:
- **DOMINANT**: ≥20 points above field average, ≤1 quality threat
- **STRONG**: 15-20 points above field average, 2 quality threats
- **MODERATE**: 10-15 points above field average, 3 quality threats
- **VULNERABLE**: 5-10 points above field average, 4+ quality threats
- **WEAK**: <5 points above field average, 5+ quality threats

### Field-Relative EV Thresholds:
- **DOMINANT Favourite**: BACK ≥+3%, LAY ≤-10%
- **STRONG Favourite**: BACK ≥+5%, LAY ≤-8%
- **MODERATE Favourite**: BACK ≥+7%, LAY ≤-6%
- **VULNERABLE Favourite**: BACK ≥+10%, LAY ≤-4%
- **WEAK Favourite**: BACK ≥+12%, LAY ≤-2%

### BACK FAVOURITE Criteria (ALL must be true):
- **EV meets field-relative threshold** (favourite shows positive value vs field)
- **Field position**: DOMINANT/STRONG (favourite clearly superior)
- **Data confidence**: Both sources support (≥70% confidence)
- **Quality metrics**: ≥3 Timeform stars OR RP rating >75
- **Form indicators**: Recent positive form + semantic analysis support
- **Evolution**: Shows improvement or stability (not decline)

### LAY FAVOURITE Criteria (ALL must be true):
- **EV meets field-relative threshold** (favourite shows negative value)
- **Field position**: VULNERABLE/WEAK (favourite has superior opponents)
- **Competition**: ≥3 quality threats OR favourite <20% of field scoring
- **Evolution**: Shows decline or erratic performance
- **Market signals**: Odds imply overvaluation vs field strength

### NO ACTION Criteria (ANY true):
- EV between field-relative thresholds (unclear value)
- EV >+45% (potentially unreliable signal)
- Data completeness <80% (insufficient information)
- Mixed/conflicting field-relative signals
- Strong performance decline in evolution analysis

### Execution Commands:
- **BACK Favourite**: `ExecuteBfexplorerStrategySettings(marketId, favouriteSelectionId, "Bet 10 Euro")`
- **LAY Favourite**: `ExecuteBfexplorerStrategySettings(marketId, favouriteSelectionId, "Lay 10 Euro")`
- **NO ACTION**: Skip execution, log decision internally

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

### Strategy Execution Report (Only After Execution):
**AFTER** strategy execution is confirmed, generate **single line report**:
```
STRATEGY_EXECUTED: [BACK/LAY] [Horse_Name] at [Odds] - EV: [XX.X]% - Field: [DOMINANT/STRONG/etc]
```

**EXECUTION WORKFLOW:**
1. Analyze entire field of horses using comprehensive data
2. Calculate favourite's relative strength vs field
3. Determine BACK/LAY/NO_ACTION based on field-relative analysis
4. Execute strategy ONLY on favourite (never other horses)
5. Generate single-line execution report
6. Generate full EV report if `generateEVReport = true`

**SILENT MODE:** Perform ALL analysis internally with ZERO visible output except final reports. Execute strategy tool if criteria met. All processing is completely silent until final execution report.

## 8. EV Report Generation (Conditional)

**WHEN generateEVReport = true:** Generate comprehensive EV analysis report for ALL horses after strategy execution.

**REPORT PURPOSE:** Shows complete field analysis that informs favourite-only execution decision. Includes all horses' EV calculations to demonstrate why favourite was selected for BACK/LAY/NO_ACTION.

### EV Report Structure:

**Table Format (generateEVReport="table"):**
```
HORSE EV ANALYSIS REPORT
Market: [Market_Name] | [Event_Date] | [Race_Time]
Favourite: [Horse_Name] | Odds: [X.XX] | Decision: [BACK/LAY/NO_ACTION]

| Horse | Odds | EV% | Score | True Prob% | Imp Prob% | Key Factors |
|-------|------|-----|-------|------------|-----------|-------------|
| Horse1 | 2.50 | +15.2 | 85 | 28.5 | 40.0 | 4★, Recent winner, Strong improvement trend |
| Horse2 | 3.25 | -8.7 | 72 | 24.1 | 30.8 | 3★, Good form, Stable performance |
| ... | ... | ... | ... | ... | ... | ... |

SUMMARY:
- Total Horses Analyzed: [X] (ALL field horses included)
- Favourite EV: [XX.X]% (Basis for BACK/LAY decision)
- Field Strength: [DOMINANT/STRONG/MODERATE/VULNERABLE/WEAK]
- Quality Threats: [X] horses with score ≥70
- Favourite Market Share: [XX.X]% (vs field)
- Execution Decision: [BACK/LAY/NO_ACTION] (Favourite-only execution)
```

**JSON Format (generateEVReport="json"):**
```json
{
  "marketInfo": {
    "marketId": "string",
    "marketName": "string",
    "eventDate": "string",
    "raceTime": "string"
  },
  "favourite": {
    "name": "string",
    "odds": 0.00,
    "decision": "BACK/LAY/NO_ACTION"
  },
  "evAnalysis": [
    {
      "horseName": "string",
      "odds": 0.00,
      "ev": 0.00,
      "score": 0,
      "trueProbability": 0.000,
      "impliedProbability": 0.000,
      "keyFactors": ["factor1", "factor2", "factor3"],
      "timeformData": {
        "stars": 0,
        "inForm": true/false,
        "winnerLastTime": true/false
      },
      "racingPostData": {
        "rating": 0,
        "lastRunDays": 0
      },
      "evolutionScore": 0
    }
  ],
  "fieldSummary": {
    "totalHorses": 0,
    "fieldStrength": "DOMINANT/STRONG/MODERATE/VULNERABLE/WEAK",
    "qualityThreats": 0,
    "favouriteDominance": 0.0
  }
}
```

### Report Generation Rules:
- **Always generate report** when `generateEVReport = true`, regardless of execution decision
- Include **ALL horses** in the field, sorted by EV descending
- **Key Factors** format: `[X]★, [semantic analysis], [additional factor]` (e.g., "4★, Strong improvement trend, Recent winner")
- **Evolution Score** should reflect the performance evolution analysis (+6 to -5)
- Report should be generated **immediately after** the Strategy Execution Report
- If `generateEVReport = false`, skip this section entirely (maintain silent mode)

**STRATEGY EXECUTION WORKFLOW:**
1. **Analyze entire field** using comprehensive Timeform + Racing Post data
2. **Score all horses** and calculate field-relative metrics
3. **Evaluate favourite's strength** vs competition using EV analysis
4. **Execute ONLY on favourite**: BACK if strong vs field, LAY if weak vs field
5. **Generate execution report** with favourite decision
6. **Generate full EV report** (if enabled) showing complete field analysis

**KEY PRINCIPLE:** Complete field analysis informs favourite-only execution. Never trade horses other than the favourite, regardless of their individual EV scores.