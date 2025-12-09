import asyncio
from fast_agent import FastAgent

# Create the application
fast = FastAgent("Bfexplorer Assistant")

# Define the agent
@fast.agent(name="BfexplorerApp",
    instruction="You are a helpful AI Agent executing betting/trading strategies on bfexplorer.",
    #model="deepseek-chat",
    #model="generic.openai/gpt-4.1",
    #model="generic.openai/gpt-5.0-mini",
    #model="generic.xai/grok-3",
    model="generic.deepseek/DeepSeek-V3-0324",
    servers=["BfexplorerApp"]
)
async def main():
    async with fast.run() as agent:
        await agent(
"""
# Favourite Bet Decision Prompt (Compact)

Objective: Select exactly one action on the current favourite (lowest price): Bet 10 Euro, Lay 10 Euro, or No Bet.

## 1. Inputs
1. Retrieve active market: GetActiveMarket
2. Candles & BLR, on the active market: GetDataContextForMarket(marketId, "MarketSelectionsCandleStickData")

## 2. Metrics (Favourite)
- Price = latest close
- OpenPrice = first candle open | CurrentPrice = last candle close
- Prob% = 100 / Price
- OpenProb% = 100 / OpenPrice; ProbDelta_pp = CurrentProb - OpenProb
- Direction: STEAM (>0), DRIFT (<0), STABLE (|Delta| < 0.2pp)
- Minutes = (lastTime - firstTime)/60
- Velocity_ppm = ProbDelta_pp / Minutes
- Volatility% = (HighProb - LowProb)/CurrentProb * 100 (High/Low from candle highs/lows -> convert to prob)
- BLR = backVolume / (backVolume + layVolume) (provided)
- OppWeight = 1 - FavouriteProbShare (sum probs others / total)

## 3. Bet Filters (must pass for any Bet/Lay)
Price 1.5-12.0 AND Volatility <= 50%.
Never Lay if ProbDelta_pp > 0.

## 4. Decision Rules
BACK (Bet 10 Euro) if:
- ProbDelta_pp >= +0.4 AND BLR >= 0.55 AND OppWeight < 0.85

LAY (Lay 10 Euro) if:
- ProbDelta_pp <= -0.4 AND BLR < 0.50 AND OppWeight >= 0.50

Else: NO BET.

Tie-break: If both sides appear (should not logically) -> No Bet.

## 5. Execution
If BACK or LAY -> immediately call:
ExecuteBfexplorerStrategySettings(marketId, selectionId, "Bet 10 Euro" | "Lay 10 Euro")
No confirmation. Optional idempotency: skip if identical (marketId, selectionId, decision) already executed in session.

End.
"""
        )

if __name__ == "__main__":
    asyncio.run(main())
