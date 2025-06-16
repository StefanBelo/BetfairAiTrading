import asyncio
from mcp_agent import RequestParams
from mcp_agent.core.fastagent import FastAgent

# Create the application
fast = FastAgent("Bfexplorer Assistant")

# Define the agent
@fast.agent(name="BfexplorerApp", 
    instruction="You are a helpful AI Agent executing betting/trading strategies on bfexplorer.", 
    model="deepseek-chat",
    request_params=RequestParams(
      maxTokens=8192,
      use_history=False,
      max_iterations=10
    ), 
    servers=["BfexplorerApp"]
)
async def main():
    async with fast.run() as agent:
        await agent(
"""
Task: Perform silent Expected Value (EV) analysis for horse racing with conservative betting criteria. Execute "Bet 10 Euro" strategy on favorite if criteria met, otherwise execute "Lay 10 Euro" strategy. Report only final strategy execution.

Instructions:

1. **Silent Data Collection**
   - Retrieve active betfair market using tool: GetActiveBetfairMarket
   - Retrieve racing data using tool: GetDataContextForBetfairMarket with 'RacingpostDataForHorsesInfo'
   - No interim reports during data collection

2. **Silent Analysis**
   - Analyze each horse's 'lastRacesDescription' field semantically
   - Ignore 'predictionScore' field completely   - Assign win probabilities based on recent form patterns
   - Calculate EV for each horse: EV = (Win Probability * (Decimal Odds - 1)) - (1 - Win Probability)
   - Identify market favorite (lowest price)

3. **Conservative Criteria Check (Silent)**
   - Market Favorite Status: Lowest price in market
   - Minimum EV Standard: EV >= -0.05 ("Fair" rating or better)
   - Form Reliability: No major recent performance concerns
   - Probability Dominance: Favorite's win probability exceeds second-best horse by at least 8%

4. **Strategy Execution**
   - **IF** favorite meets ALL criteria:
     - Activate favorite using tool: ActivateBetfairMarketSelection
     - Execute "Bet 10 Euro" strategy using tool: ExecuteBfexplorerStrategySettings
   - **IF** favorite does NOT meet criteria:
     - Activate favorite using tool: ActivateBetfairMarketSelection
     - Execute "Lay 10 Euro" strategy using tool: ExecuteBfexplorerStrategySettings

5. **Final Report**
   - Report only: Horse name, strategy executed (Bet/Lay), and execution status
"""
        )

if __name__ == "__main__":
    asyncio.run(main())
