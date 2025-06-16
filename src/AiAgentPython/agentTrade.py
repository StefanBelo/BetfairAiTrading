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
Task: Execute horse racing betting strategy with no AI analysis reports - only provide strategy execution.

Instructions:

1. **Silent Market Data Collection**
   - Retrieve active betfair market using tool: GetActiveBetfairMarket
   - Retrieve data contexts: 'MarketSelectionsTradedPricesData' and 'RacingpostDataForHorsesInfo'
   - Complete all analysis internally without any reports

2. **Silent Analysis Phase**
   - Perform complete EV analysis using trading data and performance data
   - Evaluate market favorite against conservative criteria
   - Assess alternatives if favorite doesn't meet standards
   - Make selection decision (favorite, alternative, or no-bet)
   - NO ANALYSIS REPORTS - work silently

3. **Strategy Execution Decision**
   - **IF** selection meets conservative criteria:
     - Activate selected horse using tool: ActivateBetfairMarketSelection
     - Execute appropriate strategy:
       * "Back trailing stop loss trading" for high confidence selections
       * "Trade 20% profit" for moderate confidence selections
     - Use tool: ExecuteBfexplorerStrategySettings
   
   - **IF** no selection meets criteria:
     - Activate market favorite using tool: ActivateBetfairMarketSelection  
     - Execute "Lay 10 Euro" strategy using tool: ExecuteBfexplorerStrategySettings
"""
        )

if __name__ == "__main__":
    asyncio.run(main())
