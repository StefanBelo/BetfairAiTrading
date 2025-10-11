# Horse Racing Strategy R3 - Streamlined Multi-Context Analysis System

## Core Strategy Directive

**SILENT EXECUTION**: Operate in complete silent mode with NO reporting or commentary. Execute all analysis internally without explanations or status updates.

**PRIMARY OBJECTIVE**: Analyze all horses using enhanced R2 criteria and execute optimal betting strategy:
- **BACK**: ExecuteBfexplorerStrategySettings with strategyName="Bet 10 Euro" when qualified selection found
- **LAY**: ExecuteBfexplorerStrategySettings with strategyName="Lay 10 Euro" when favorite overvalued
- **ANALYSIS**: No betting when no qualified opportunities exist

## Enhanced Selection Criteria (R3 Optimized)

### Core Requirements - ALL Must Be Met
1. **Prediction Score**: ≥98 (Professional racing analysis - 40% weight)
2. **Semantic Form**: ≥75 (Race description analysis - 25% weight)
3. **Enhanced EV**: ≥10% (Value assessment - 25% weight)
4. **Market Confidence**: HIGH level required (Market stability - 10% weight)
5. **Volume**: ≥10,000 Euro traded
6. **Price Range**: 2.0-12.0 decimal odds
7. **Price Movement**: NOT "drifting"
8. **Value Margin**: ≥20% above forecast price

### Composite Scoring Formula
```
Total Score = (Prediction Score × 0.40) + (Semantic Form × 0.25) + (Value Score × 0.25) + (Stability Score × 0.10)
Minimum Composite Score: 65
```

### Enhanced Expected Value Calculation
```
Win Probability = (Prediction Score × 0.6 + Semantic Form × 0.4) ÷ 100
Base EV = (Win Probability × (Odds - 1)) - ((1 - Win Probability) × 1)
Enhanced EV = Base EV × CWAF (Crowd Wisdom Adjustment Factor)
```

### CWAF Components
```
CWAF = Market Confidence Factor × Price Movement Factor × Volume Factor × Historical Success Factor

Factors Range (0.7-1.3):
- Market Confidence: Price stability >90% = 1.3, <70% = 0.8
- Price Movement: Shortening strongly = 1.3, Drifting = 0.8
- Volume: >15k Euro = 1.2, <5k Euro = 0.8
- Historical Success: Similar profiles 85+ score = 1.2, <65 = 0.8
```

## Selection Tiers & Dynamic Thresholds

### Tier 1: High Confidence (Full Stake)
- Prediction Score: 98+, Semantic Form: 85+, Enhanced EV: 15%+
- Market Confidence: HIGH only, Volume: 15,000+ Euro

### Tier 2: Medium Confidence (Standard Stake)
- Prediction Score: 95-97, Semantic Form: 75-84, Enhanced EV: 10-14%
- Market Confidence: HIGH or MEDIUM, Volume: 10,000+ Euro

### Race Type Adaptive Thresholds
```
Handicap Races: Prediction 99+, Semantic 80+, EV 12%+, Value 25%+
Conditions/Listed: Prediction 97+, Semantic 75+, EV 8%+, Value 18%+
Group Races: Prediction 100, Semantic 85+, EV 15%+, Value 30%+
```

## Enhanced Lay Strategy
Execute LAY when favorite meets ALL conditions:
- Price ≤2.5, Negative EV <-5%, Poor semantic form <70
- Poor prediction score <90, Multiple alternatives (≥2 horses with positive EV)
- Volume confirmation >5,000 Euro traded

## Risk Management & Market Intelligence

### Portfolio Limits
- Maximum 3 bets/day, 2 consecutive loss limit
- Daily loss limit: 50 Euro, Weekly profit target: 15%
- After 3 losses: Prediction threshold to 100
- After 5 losses: 24-hour analysis-only mode

### Market Timing
- Optimal Window: 25-35 minutes pre-race
- Minimum 5,000 Euro total market volume
- Avoid: 45+ minutes (volatile) or <10 minutes (insufficient time)

### Form Analysis Enhancements
```
Recency Weights: Last race 50%, Second 30%, Third 20%
Course Bonuses: 3+ wins +5pts, 2 wins +3pts, 1 win +1pt, debut -2pts
Distance Bonuses: Exact distance +3pts, similar ±2pts, unproven -3pts
```

## Execution Protocol

**SEQUENTIAL EXECUTION REQUIRED**: All steps must be executed in order, one after another. Do not proceed to the next step until the current step is completed.

1. **Retrieve Active Betfair Market** - GetActiveBetfairMarket() - Retrieves base market data including market ID, selections, current prices, and market status that will be used by all subsequent tools (SILENT)
2. **Retrieve Market Data Contexts**: 
   - GetDataContextForBetfairMarket("RacingpostDataForHorses") - Professional racing analysis data
   - GetDataContextForBetfairMarket("HorsesBaseBetfairFormData") - Historical form and forecast prices
   - GetDataContextForBetfairMarket("MarketSelectionsTradedPricesData") - Trading volume and price movement data (SILENT)
3. **Race Classification**: Determine handicap/conditions/group for dynamic thresholds (SILENT)
4. **Market Assessment**: Verify timing window and volume requirements (SILENT)
5. **Systematic Analysis**: Apply R3 criteria to all horses (SILENT)
6. **Selection Decision**: Identify highest qualifying tier or lay opportunity (SILENT)
7. **Risk Validation**: Check portfolio limits and market conditions (SILENT)
8. **Execute Strategy**: 
   - **Qualified Selection**: ExecuteBfexplorerStrategySettings("Bet 10 Euro")
   - **Lay Opportunity**: ExecuteBfexplorerStrategySettings("Lay 10 Euro")
   - **No Opportunity**: Analysis only
9. **Documentation**: SetAIAgentDataContextForBetfairMarket("HorseRacingStrategyR3_Analysis") (SILENT)

## Market Confidence Calculation

```
Market Confidence = (Price Stability × 0.4) + (Volume × 0.3) + (Movement × 0.2) + (Liquidity × 0.1)

Price Stability = 100 - ((Max-Min)/Min × 100)
Volume Score = min(100, (Volume/1000) × 10)
Movement Score: Shortening strongly=100, Stable=70, Drifting=30
Liquidity Score: Tight spreads=100, Wide spreads=25

Classification: HIGH=80-100, MEDIUM=60-79, LOW=40-59, VERY LOW=0-39
```

## Performance Targets & Data Context

### R3 Targets
- Strike Rate: 35%+, ROI: 15%+, Max Drawdown: -15%
- Bet Frequency: ~50% of races (quality focus)

### Enhanced JSON Context Structure
```json
{
  "raceAnalysis": {
    "marketId": "string", "strategyVersion": "R3",
    "enhancedCriteria": {
      "predictionThreshold": 98, "semanticThreshold": 75,
      "enhancedEVThreshold": 0.10, "volumeThreshold": 10000
    }
  },
  "selectedHorse": {
    "horseName": "string", "selectionId": "string",
    "compositeScore": 0.0, "confidenceTier": "tier1|tier2|analysis_only"
  },
  "allHorses": [{
    "horseName": "string", "currentPrice": 0.0, "predictionScore": 0,
    "semanticFormScore": 0.0, "enhancedEV": 0.0, "marketConfidence": "high|medium|low",
    "meetsAllCriteria": false, "failedCriteria": ["array"]
  }],
  "executedBet": {
    "strategy": "Bet 10 Euro|Lay 10 Euro|Analysis Only",
    "status": "executed|analysis_complete", "confidenceLevel": "high|medium"
  },
  "r3Metrics": {
    "totalAnalyzed": 0, "tier1Candidates": 0, "tier2Candidates": 0,
    "raceType": "handicap|conditions|group", "marketTiming": "optimal|suboptimal"
  }
}
```

## Key Calculation References

### Semantic Form Scoring (0-100)
```
Base Performance (0-60): Win=20pts, Place=12pts, Show=6pts per race
Trend Adjustment (±20): Improving=+15-20, Declining=-10-20
Suitability Bonus (0-15): Course winner=+8, Distance suited=+10
Negative Penalty (0-25): Pulled up=-15, Disappointing=-5
Keywords: Positive phrases +2-5pts, Negative phrases -2-4pts
```

### Price Movement Classification
```
Reference = Opening price or 30min ago price
Movement% = ((Current-Reference)/Reference) × 100
Shortening Strongly ≤-10%, Stable ±1%, Drifting Strongly ≥+10%
```

### Value Margin Assessment
```
Value Margin = ((Current Price - Forecast Price) / Forecast Price) × 100
Required: ≥20% (Good Value), Optimal: ≥30% (Excellent Value)
```

---

**Strategy**: HorseRacingStrategyR3 | **Author**: AI Agent System | **Created**: June 25, 2025  
**Status**: Active | **Risk Level**: Medium-Low | **Based On**: R1/R2 Performance Optimization

⚠️ **Disclaimer**: Real money betting involves financial risk. Past performance does not guarantee future results. Bet responsibly within your means.
