# Weight of Money Strategy R1 - Silent Execution

## Objective
Silently analyze Betfair market weight of money data, identify highest confidence prediction among 3 favorites, and execute appropriate strategy. Only final execution summary output.

### Strategy Selection:
- **Shorten Prediction (⬇️)**: Execute "Back Trade" 
- **Drift Prediction (⬆️)**: Execute "Lay Trade"
- **Minimum Confidence**: ≥ 40%
- **Filter**: Top 3 favorites only

## Data Analysis (CRITICAL)

### BetType Interpretation:
- **BetType 1 (Back)**: Volume = Money available to LAY (lay offers)
- **BetType 2 (Lay)**: Volume = Money available to BACK (back offers)
- **Backing Pressure**: High BetType 2 volumes → Odds shorten
- **Laying Pressure**: High BetType 1 volumes → Odds drift

### Traded Prices Analysis:
- **tradedPrices**: Complete trading history (price + volume pairs)
- **Volume-Weighted Average Price (VWAP)**: Σ(Price × Volume) / Σ(Volume)
- **Volume Distribution**: Concentration of trading activity
- **Price Migration**: Recent vs historical trading patterns

## Confidence Scoring (0-100%)

### Points Calculation:
- **Historical Volume Signal** (0-30): Extreme imbalance >2:1 = 30pts, Strong 1.5-2:1 = 25pts, Moderate 1.2-1.5:1 = 15pts
- **Price Deviation** (0-25): >20% from average = 25pts, 10-20% = 18pts, 5-10% = 12pts, <5% = 5pts
- **Traded Distribution** (0-20): Strong concentration >60% in top 3 prices = 20pts, Moderate 40-60% = 15pts, Balanced 20-40% = 10pts, Wide <20% = 5pts
- **Offered Prices Signal** (0-25): Extreme >3:1 = 25pts, Strong 2-3:1 = 20pts, Moderate 1.5-2:1 = 15pts
- **Signal Alignment** (0-25): All 4 signals = 25pts, 3 signals = 20pts, 2 signals = 15pts, 1 signal = 8pts

### Volume Adjustment:
- **Total Volume** = Sum of all tradedPrices volumes
- **Multipliers**: >1000 = 1.0, 500-1000 = 0.9, 100-500 = 0.8, 50-100 = 0.6, <50 = 0.4
- **Final Confidence** = Base Points × Volume Multiplier

## Strategy Parameters
- **Back Trade**: For Shorten predictions (⬇️), confidence ≥ 40%
- **Lay Trade**: For Drift predictions (⬆️), confidence ≥ 40%
- **Risk Management**: Built into strategy templates
- **Exit**: 2 minutes before race start

## Silent Execution Protocol

### Execute Silently (No Intermediate Output):
1. Get active market (GetActiveMarket)
2. Get weight of money data (GetDataContextForMarket with "WeightOfMoneyData")
3. Parse complete data: averageBackTraded, averageLayTraded, tradedPrices, offeredPrices
4. Identify 3 favorites (lowest odds)
5. For each favorite, calculate:
   - Volume ratios from historical data
   - Price deviation from averages and VWAP
   - Trading volume distribution analysis
   - Offered prices imbalance
   - Signal alignment across all 4 factors
6. Select highest confidence ≥ 40% among favorites
7. Execute strategy settings: "Back Trade" (Shorten) or "Lay Trade" (Drift)
   ```
   ExecuteBfexplorerStrategySettings(
     strategyName: "Back Trade" or "Lay Trade",
     marketId: "[Market ID]", 
     selectionId: "[Selection ID of highest confidence prediction among favorites]"
   )
   ```
8. Output final result only

## Final Output (ONLY OUTPUT REQUIRED)

```
WEIGHT OF MONEY STRATEGY R1 EXECUTION
=====================================
Market: [Market Name]
Market ID: [Market ID]
Selection: [Selection Name] 
Selection ID: [Selection ID]
Position: [#X Favorite]
Confidence: [XX]%
Prediction: [Shorten ⬇️/Drift ⬆️]
Strategy: [Back Trade/Lay Trade]
Status: [EXECUTED/NO EXECUTION]
Reason: [Success/No qualifying signals/Error description]
=====================================
```

### No Execution Cases:
- No active market found
- Weight of Money data not available  
- No predictions ≥40% confidence among top 3 favorites
- Highest confidence signal not among top 3 favorites
- Strategy execution failure
