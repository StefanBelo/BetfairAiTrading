# Bfexplorer Data Providers

![Bfexplorer My Data Providers](/docs/Automation/images/BfexplorerMyDataProviders.png)

## Overview

Data providers in Bfexplorer serve as accessible data sources that can be integrated into AI agent workflows and prompts. These providers can be utilized in AI agent processes through the MCP (Model Context Protocol) tool `GetDataContextForMarket`.

## Why Use Data Providers?

The "My Data Providers" section in the Bfexplorer app allows users to browse and explore data directly from the Bfexplorer REST API. This browsing capability is essential for:

- **Data Familiarization**: Understanding the structure and content of available data before incorporating it into AI agent prompts
- **Data Validation**: Verifying data quality and format for strategy development
- **Workflow Planning**: Determining which data sources are most relevant for specific AI agent tasks

By exploring these data providers in the Bfexplorer interface, users can better understand what information is available and how to effectively reference it in their AI agent workflows.

## Workflow: From Data Exploration to AI Agent Implementation

### Step 1: Explore Data in Bfexplorer
- Use "My Data Providers" to browse available data
- Examine data structure and content
- Identify relevant data sources for your strategy

### Step 2: AI Agent Integration Pattern
When working with AI agent prompts, follow this typical workflow:

1. **Retrieve Active Market**: Get the currently active Betfair market using the appropriate MCP tool
2. **Load Data Context**: 
   - For single data source: Use `GetDataContextForMarket` with specific data context name (e.g., `OlbgRaceTipsData`)
   - For multiple data sources: Use `GetDataContextForMarket` with an array of dataContextNames to combine multiple data providers in one call
3. **AI Agent Analysis**: Once the AI agent has loaded the data, it can analyze and process the information
4. **Prompt Enhancement**: Use the loaded data to build more informed and contextual prompts for strategy development

### Step 3: Strategy Development
- Combine multiple data sources for comprehensive analysis
- Create prompts that leverage the loaded data context
- Test and refine strategies based on data insights

## Available Data Providers

### General Betfair Market Data
These providers work with any Betfair market:
- **MarketSelectionsTradedPricesData** - Historical and real-time traded price information
- **MarketSelectionsPriceHistoryData** - Price movement trends and historical data

### Horse Racing Specific Data
These providers are specialized for horse racing markets:
- **HorsesBaseBetfairFormData** - Basic horse form and performance data from Betfair
- **OlbgRaceTipsData** - Community tips and predictions from OLBG platform
- **TestRaceData** - Test datasets for strategy development and backtesting
- **RacingpostDataForHorses** - Comprehensive horse information from Racing Post
- **WeightOfMoneyData** - Market sentiment and money flow analysis
- **TimeformDataForHorses** - Professional ratings and analysis from Timeform
- **BetfairSpData** - Betfair Starting Price data and calculations

## Best Practices

1. **Start Small**: Begin by exploring one or two data providers to understand their structure
2. **Combine Data Sources**: Use multiple providers together for richer analysis
3. **Validate Data**: Always check data quality and relevance before building strategies
4. **Document Findings**: Keep notes on which data providers work best for specific scenarios
5. **Iterate and Improve**: Continuously refine your data usage based on strategy performance

## Example Use Cases

- **Pre-Race Analysis**: Use `GetDataContextForMarket` with `["RacingpostDataForHorses", "TimeformDataForHorses", "OlbgRaceTipsData"]` for comprehensive horse assessment
- **Market Monitoring**: Use `GetDataContextForMarket` with `["MarketSelectionsTradedPricesData", "WeightOfMoneyData"]` for real-time market sentiment analysis
- **Strategy Testing**: Utilize `GetDataContextForMarket` with `"TestRaceData"` for backtesting before live implementation
- **Full Market Analysis**: Combine all relevant data sources using `GetDataContextForMarket` with multiple dataContextNames for the most comprehensive analysis

## Creating Custom Data Providers

New data providers can be added to Bfexplorer to extend its data capabilities. This allows for integration of additional data sources that may be specific to your trading strategies or analysis requirements.

If you're interested in creating custom data providers for Bfexplorer, a simple example can be provided to demonstrate the implementation process. Custom data providers can help integrate:

- External APIs and data sources
- Specialized market analysis tools
- Custom data processing and transformation logic
- Third-party betting intelligence services

Contact the development team for guidance on implementing custom data providers for your specific use case.
