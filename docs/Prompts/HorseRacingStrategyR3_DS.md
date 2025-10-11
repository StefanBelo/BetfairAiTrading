# Horse Racing Strategy R3 - Optimized Data Structure

## Strategy Overview
Silent mode operation analyzing all horses to determine optimal action:
- **BACK**: If qualified (semantic form ≥75, composite score ≥65, EV ≥10%)
- **LAY**: If favorite is overvalued (negative EV, semantic form <70)
- **NO ACTION**: If no qualified selections

**Strategy Names**:
- BACK: "Bet 10 Euro"
- LAY: "Lay 10 Euro"

## Required Data Contexts
### 1. RacingpostDataForHorses
- **Purpose**: Professional racing analysis and prediction scores
- **Key Fields**:
  - Prediction scores (0-100 scale)
  - Recent race descriptions
  - Professional insights

### 2. HorsesBaseBetfairFormData  
- **Purpose**: Official racing form and ratings
- **Key Fields**:
  - Official ratings (OR)
  - Recent form figures
  - Forecast prices
  - Weight carried

### 3. MarketSelectionsTradedPricesData
- **Purpose**: Market behavior and price movements  
- **Key Fields**:
  - Price stability (min/max ranges)
  - Traded volumes
  - Price drift patterns
  - Market confidence indicators

## Data Context Format
All analysis stored using SetAIAgentDataContextForBetfairMarket with:
- **Context Name**: "HorseRacingStrategyR3_Analysis"  
- **Market ID**: Current race market identifier
- **JSON Data**: Optimized analysis structure

### Optimized JSON Structure
```json
{
  "raceAnalysis": {
    "marketId": "1.123456789",
    "strategyVersion": "R3_DS",
    "enhancedCriteria": {
      "predictionScoreThreshold": 98,
      "semanticFormThreshold": 75,
      "compositeScoreThreshold": 65,
      "enhancedEVThreshold": 0.10,
      "volumeThreshold": 2000
    }
  },
  "selectedHorse": {
    "horseName": "Selected Horse",
    "selectionId": "selection_id",
    "compositeScore": 0.0,
    "confidenceTier": "tier1|tier2|analysis_only"
  },
  "allHorses": [
    {
      "horseName": "Horse Name",
      "selectionId": "selection_id",
      "currentPrice": 0.0,
      "predictionScore": 0,
      "compositeScore": 0.0,
      "enhancedValueAssessment": {
        "enhancedExpectedValue": 0.0,
        "crowdWisdomFactor": 0.0
      }
    }
  ],
  "executedBet": {
    "strategy": "Bet 10 Euro|Lay 10 Euro|Analysis Only",
    "confidenceLevel": "high|medium|analysis_only"
  }
}
```

## Core Calculation Methods
### Composite Score
```
Total Score = (Prediction Score × 0.40) + (Semantic Form Score × 0.25) + (Value Score × 0.25) + (Stability Score × 0.10)
```

### Enhanced EV Formula
```
Base EV = (Win Probability × (Decimal Odds - 1)) - ((1 - Win Probability) × 1)
Enhanced EV = Base EV × Crowd Wisdom Adjustment Factor (CWAF)
```

### CWAF Components
```
CWAF = Market Confidence × Price Movement × Volume × Historical Success
```

## Selection Criteria
1. Prediction Score ≥98
2. Semantic Form Score ≥75  
3. Composite Score ≥65
4. Enhanced EV ≥10%
5. Volume ≥10,000 Euro

## Strategy Execution Protocol

### Pre-Execution Checklist
1. Retrieve active market using GetActiveBetfairMarket (SILENT)
2. Retrieve all three data contexts (SILENT)
3. Calculate semantic form scores (SILENT)
4. Apply prediction score threshold (98+) (SILENT)
5. Calculate composite scores (SILENT)
6. Calculate Enhanced EV (SILENT)
7. Apply selection filters (SILENT)
8. Identify tier-based selections (SILENT)
9. Validate all enhanced criteria (SILENT)

### Execution Steps
1. Active Market Identification
2. Data Collection
3. Race Type Classification  
4. Market Timing Assessment
5. Portfolio Risk Check
6. Semantic Analysis
7. Course/Distance Specialization
8. Market Intelligence Analysis
9. Dynamic Threshold Application
10. Prediction Filtering
11. Enhanced Scoring
12. Advanced EV Analysis
13. Market Intelligence Filters
14. Tiered Selection
15. Risk Management Validation
16. Strategy Decision
17. Betting Execution:
   - Qualified Selection: ExecuteBfexplorerStrategySettings with strategyName="Bet 10 Euro"  
   - Favorite Lay Opportunity: ExecuteBfexplorerStrategySettings with strategyName="Lay 10 Euro"
   - No Qualified Selection: Analysis only
18. Documentation in "HorseRacingStrategyR3_Analysis" context (single update)
19. Portfolio Tracking

### Lay Strategy Criteria
Execute Lay when favorite meets ALL:
1. Price ≤2.5
2. Negative EV < -5%
3. Semantic Form <70
4. Prediction Score <90
5. ≥2 alternatives with positive EV
6. Volume >5,000