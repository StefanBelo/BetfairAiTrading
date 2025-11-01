# BFExplorer AI Agent: Your Betfair Trading Assistant


BFExplorer's AI agent serves as your intelligent interface to Betfair markets, offering seamless access to trading data and strategy execution. Using the MCP Server methods, the agent can:

## Core Capabilities

- **Market Information Retrieval**: Access real-time data from active Betfair markets, including selections, prices, and event details
- **Strategy Management**: View and implement pre-configured trading strategies
- **Market Interaction**: Activate selections and execute strategic trades

## Available Methods

- `GetActiveMarket`: Retrieve comprehensive data about the currently active market (e.g., "Horse Racing - Ayr 21st May")
- `GetBetfairMonitoredMarkets`: Access the complete list of markets being monitored
- `GetBfexplorerStrategyTemplates`: View available strategy templates for different market scenarios
- `ActivateBetfairMarketSelection`: Select specific market options for trading focus
- `ExecuteBfexplorerStrategySettings`: Implement and run configured trading strategies

The agent streamlines your Betfair trading workflow by handling data retrieval, analysis, and execution tasks through natural language commands, allowing for efficient market monitoring and strategy deployment without manual navigation of the BFExplorer interface.

![BFExplorer AI Agent: Your Betfair Trading Assistant!](/docs/Automation/images/AIAgentYourBetfairTradingAssistant.png "BFExplorer AI Agent: Your Betfair Trading Assistant")

```text
StefanBelo: Retrieve the active Betfair market and show its selection data.

GitHub Copilot: I'll help you retrieve information about the active Betfair market from BFExplorer. Let me get that data for you. 

Here's the information about the currently active Betfair market:

Event: Horse Racing - Ayr 21st May
Market: 5f Hcap
Market ID: 1.243978636
Start Time: 21st May 2025 15:00:00 (CEST)
Status: Open

Selections and their current prices:
1. Eternal Sunshine - 2.64
2. Copper Knight - 3.90
3. Le Beau Garcon - 6.60
4. Nelson Gay - 9.00
5. Classy Al - 19.00
6. Pockley - 24.00

This appears to be a 5-furlong handicap horse race at Ayr racecourse, with six runners. Eternal Sunshine is currently the favorite at odds of 2.64.

StefanBelo: Can you get more data from the internet?

GitHub Copilot: I don't have direct access to extra betting data or odds from the internet through the available tools. However, I can see that this workspace has several interesting features for working with Betfair data:

1. There are racing-specific strategy files in Strategies like `HorseRacingBookmakersOdds.json` and `HorseRacingRaceDistance.json`
2. There are horse racing event files under BetEvents
3. There are F# script files for horse racing strategies in HorseRacing

Would you like me to:
1. Look at how the horse racing strategies are configured?
2. Get the list of all monitored markets to see other available races?
3. Or would you prefer to focus on something specific about the current race at Ayr?

Let me know what interests you and I'll help explore that aspect further using the tools available in the workspace.
```