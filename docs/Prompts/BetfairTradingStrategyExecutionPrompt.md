# Betfair Trading Strategy Execution Prompt

## Objective
Analyze the currently active Betfair market, identify trading opportunities, and execute the "Trading Strategy" with fully optimized parameters using only the following tools:
- **GetActiveBetfairMarket** (to retrieve market and selection data)
- **GetDataContextForBetfairMarketSelection** (to retrieve odds history for a selection, specifically with dataContextName: "MarketSelectionsPriceHistoryData")
- **ExecuteBfexplorerStrategySettingsWithParameters** (to execute the strategy with optimized parameters)

**CRITICAL**: All strategy parameters (BetType, Profit targets, Loss limits) are ADAPTIVE and must be determined through analysis. There are no fixed values - every parameter should be optimized based on market conditions, volatility, selection characteristics, and risk assessment.

## Betfair Trading Fundamentals
**Core Profit Mechanism**: On Betfair, profit is made by backing at higher odds and laying at lower odds. This means:
- **Back then Lay**: Back a selection at higher odds (e.g., 3.0), then lay the same selection at lower odds (e.g., 2.5) for guaranteed profit
- **Lay then Back**: Lay a selection at lower odds, then back at higher odds to close the position profitably
- **Odds vs Probability**: In betting data, "price" = "odds". Probability is calculated as P = 1/Odds (or P = 1/Price)
- **Price Direction**: Odds shortening (decreasing numbers) = increasing probability, odds lengthening (increasing numbers) = decreasing probability
- **Trading Logic**: Look for selections likely to move from higher odds to lower odds (shortening) for back-to-lay opportunities

## Analysis and Execution Steps

### Step 1: Retrieve Active Market
Use **GetActiveBetfairMarket** to retrieve:
- Market ID
- Market name and event details
- All available selections with current odds
- Market status and timing

### Step 2: Market Analysis and Selection Identification
From the active market data, analyze and identify:
- **Market Favorite**: Selection with shortest odds (highest probability)
- **Value Selections**: Selections with potential for odds movement
- **Liquidity Assessment**: Available backing/laying amounts
- **Odds Trends**: Recent movement patterns
- **Market Sentiment**: Overall backing vs laying pressure

**Note:** In this step, retrieve detailed data only for the market favorite (the selection with the shortest odds). Do not retrieve odds history for all selections.

### Step 3: Get Favorite Selection Odds History Data
For the favorite selection identified in Step 2, use **GetDataContextForBetfairMarketSelection** with:
- dataContextName: "MarketSelectionsPriceHistoryData"
- marketId: {from Step 1}
- selectionId: {favorite selection ID from Step 2}

This will provide detailed historical odds data ("price" field contains the betting odds, not probability).

### Step 4: Analyze Favorite Selection Odds History Data
Analyze the retrieved odds history data for the favorite selection to identify:
- **Trend Direction**: Are odds trending up, down, or sideways?
- **Volatility**: How stable or fluctuating are the odds?
- **Support/Resistance Levels**: Key odds levels where price bounces or breaks through
- **Momentum**: Rate and direction of odds change
- **Volume and Liquidity**: Amount matched at different odds levels
- **Backing/Laying Pressure**: Is the favorite being heavily backed or laid?
- **Pattern Recognition**: Look for breakouts, reversals, consolidations, and steam moves

Use these insights to inform your selection strategy assessment and parameter optimization in the following steps.

### Step 5: Parameter Optimization
Based on analysis, optimize the allowed parameters for the strategy. All parameters must be adaptive and justified by the market and selection analysis.

#### Core Strategy Parameters (Adaptive Structure)
```json
{
  "OpenBetPosition": {
    "BetType": "Back" | "Lay",
    "Stake": 100.0,
    "StakeType": "Stake"
  },
  "CloseBetPosition": {
    "ProfitLossType": "Percentage", 
    "Profit": 5.0 - 25.0,
    "Loss": 10.0 - 30.0,
    "HedgingEnabled": true,
    "ClosePositionImmediately": true
  }
}
```

### Step 6: Execute Trading Strategy
Use **ExecuteBfexplorerStrategySettingsWithParameters** with:
- strategyName: "Trading Strategy"
- marketId: {from Step 1}
- selectionId: {chosen selection from analysis}
- parameters: {optimized JSON parameters}

### Step 7: Post-Execution Analysis
Document the execution results:
- Selection chosen, entry odds, and rationale
- Strategy status and confirmation
- Market conditions at execution time
- Expected timeline and monitoring plan

## Expected Output Format

### Market Analysis Report
```
Market: {Market Name}
Event: {Event Name}  
Market ID: {Market ID}
Status: {Market Status}
Analysis Time: {Current Timestamp}
Selections Available: {Number of selections}
```

### Selection Analysis
For each analyzed selection:
- **Selection Name**: {Name}
- **Current Odds**: {Back/Lay prices}
- **Implied Probability**: {P = 1/Odds}
- **Liquidity**: {Available amounts}
- **Movement Assessment**: {Shortening/lengthening potential}
- **Strategy Suitability**: {Rating for back-to-lay trading}

### Recommended Selection
- **Chosen Selection**: {Name and reasoning}
- **Current Odds**: {Entry price}
- **Strategy Rationale**: {Why this selection was chosen}
- **Risk Assessment**: {Potential downsides}
- **Expected Movement**: {Anticipated odds direction}

### Strategy Execution
```json
{
  "strategyName": "Trading Strategy",
  "marketId": "{Market ID}",
  "selectionId": "{Selection ID}",
  "parameters": {
    "OpenBetPosition": {
      "BetType": "Back" | "Lay",
      "Stake": 100.0,
      "StakeType": "Stake"
    },
    "CloseBetPosition": {
      "ProfitLossType": "Percentage",
      "Profit": "{5.0-25.0 based on analysis}",
      "Loss": "{10.0-30.0 based on risk assessment}",
      "HedgingEnabled": true,
      "ClosePositionImmediately": true
    }
  }
}
```

### Execution Results
- **Status**: {Success/Failure}
- **Entry Confirmation**: {Actual execution details}
- **Active Monitoring**: {Strategy running confirmation}
- **Next Steps**: {What happens automatically}

## Key Reminders
- **Only use the following tools**: GetActiveBetfairMarket, GetDataContextForBetfairMarketSelection (with dataContextName: "MarketSelectionsPriceHistoryData"), and ExecuteBfexplorerStrategySettingsWithParameters.
- **Adaptive Parameters**: Both BetType ("Back" or "Lay") and profit/loss targets are determined by market analysis
- **Analysis-Driven Targets**: Profit targets (5-25%) and loss limits (10-30%) must be set based on market conditions, not fixed values
- **Selection Focus**: Choose the best selection and optimal BetType based on comprehensive analysis
- **Risk Awareness**: Maximum loss varies based on analysis-determined loss limit
- **BetType Decision**: Use "Back" for expected shortening, "Lay" for expected lengthening
- **Target Optimization**: Consider volatility, time, liquidity, and selection characteristics when setting targets
- **Laying Risk**: Remember laying has unlimited liability if selection wins
- **Automation**: Strategy runs automatically once executed with analysis-optimized parameters
- **Documentation**: Record all analysis, BetType choice reasoning, target setting rationale, and execution details for review
