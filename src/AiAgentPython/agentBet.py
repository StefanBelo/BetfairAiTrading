import asyncio
from mcp_agent.core.fastagent import FastAgent

# Create the application
fast = FastAgent("Bfexplorer Assistant")

# Define the agent
@fast.agent(name="BfexplorerApp", 
    instruction="You are a helpful AI Agent executing betting/trading strategies on bfexplorer.", 
    model="deepseek-chat", 
    servers=["BfexplorerApp"]
)
async def main():
    async with fast.run() as agent:
        await agent(
"""
## Objective
Silently analyze Betfair market weight of money data, identify highest confidence prediction among 3 favorites, and execute appropriate strategy. Only final execution summary output.

### Strategy Selection:
- **Shorten Prediction (DOWN)**: Execute "Back 10 Euro" 
- **Drift Prediction (UP)**: Execute "Lay 10 Euro"
- **Minimum Confidence**: >= 40%
- **Filter**: Top 3 favorites only

## BetType Analysis (CRITICAL)
- **BetType 1 (Back)**: Volume = Money available to LAY (lay offers)
- **BetType 2 (Lay)**: Volume = Money available to BACK (back offers)
- **Backing Pressure**: High BetType 2 volumes -> Odds shorten
- **Laying Pressure**: High BetType 1 volumes -> Odds drift

## Confidence Scoring (0-100%)

### Points Calculation:
- **Historical Volume Signal** (0-40): Extreme imbalance >2:1 = 40pts, Strong 1.5-2:1 = 30pts, Moderate 1.2-1.5:1 = 20pts
- **Price Deviation** (0-30): >20% = 30pts, 10-20% = 20pts, 5-10% = 15pts, <5% = 5pts
- **Offered Prices Signal** (0-30): Extreme >3:1 = 30pts, Strong 2-3:1 = 25pts, Moderate 1.5-2:1 = 18pts
- **Signal Alignment** (0-25): All align = 25pts, Two align = 18pts, One = 10pts, Conflicting = 0pts

### Volume Adjustment:
- **Total Volume** = averageBackTraded.volume + averageLayTraded.volume
- **Multipliers**: >1000 = 1.0, 500-1000 = 0.9, 100-500 = 0.8, 50-100 = 0.6, <50 = 0.4
- **Final Confidence** = Base Points x Volume Multiplier

## Strategy Parameters
- **Back 10 Euro**: For Shorten predictions (DOWN), confidence >= 40%
- **Lay 10 Euro**: For Drift predictions (UP), confidence >= 40%
- **Risk Management**: Built into strategy templates
- **Exit**: 2 minutes before race start

## Silent Execution Protocol

### Execute Silently (No Intermediate Output):
1. Get active market (GetActiveBetfairMarket)
2. Get weight of money data (GetDataContextForBetfairMarket with "WeightOfMoneyData")
3. Identify 3 favorites (lowest odds)
4. Calculate confidence scores for favorites only
5. Select highest confidence >= 40% among favorites
6. Execute strategy settings "Back 10 Euro" (Shorten) or "Lay 10 Euro" (Drift)
  ```
   ExecuteBfexplorerStrategySettings(
     strategyName: "Back 10 Euro" or "Lay 10 Euro",
     marketId: "[Market ID]", 
     selectionId: "[Selection ID of highest confidence prediction among favorites]"
   )
   ```
7. Output final result only

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
Prediction: [Shorten DOWN/Drift UP]
Strategy: [Back 10 Euro/Lay 10 Euro]
Status: [EXECUTED/NO EXECUTION]
Reason: [Success/No qualifying signals/Error description]
=====================================
```
"""
        )

if __name__ == "__main__":
    asyncio.run(main())
