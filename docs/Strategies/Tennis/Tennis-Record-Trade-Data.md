# Tennis Record Trade Data

**Category:** Tennis  
**Strategy ID:** 6900

## Description

A tennis trading strategy that records and analyzes trading data while executing automated trading decisions based on odds movements and liability thresholds. This strategy is designed for data collection and systematic trading in tennis matches.

## Parameters

### Bet
- **TriggerOdds** (Optional) - The odds to trigger trading on a selection (Double)
- **MaximalLiability** (Optional) - The maximal liability to open the trading session (Double)

### Strategy
- **StrategyName** (Optional) - Enter the name of the strategy you wish to execute

### Miscellaneous
- **SaveMarketGraphs** (Optional) - Set to True if you want to save the market graphs (Boolean)
- **TradeOnMalePlayer** (Optional) - Set to True if you want to start trading session only for male player match (Boolean)
- **StrategyReference** (Optional) - Set this parameter to specify which strategy sent the bet. The string is limited to 15 characters

### Market
- **EvaluateEntryCriteriaOnlyOnce** (Optional) - Set this parameter to True to evaluate the entry criteria only once
- **StopMarketMonitoring** (Optional) - Set this parameter to True to stop market monitoring after the strategy completes execution. Monitoring will stop only if no other bot is active in the market

## Usage Examples

### High Stakes Trading
- **TriggerOdds**: `2.0`
- **MaximalLiability**: `100.0`
- **SaveMarketGraphs**: `true`

### Male Player Focus
- **TradeOnMalePlayer**: `true`
- **TriggerOdds**: `1.8`
- **StrategyName**: `"Tick Offset"`

### Data Collection Mode
- **SaveMarketGraphs**: `true`
- **MaximalLiability**: `50.0`
- **StrategyName**: `"Record Market Selection Data"`

## Best Practices

1. Set appropriate liability limits to manage risk
2. Use trigger odds that provide good trading opportunities
3. Enable graph saving for post-match analysis
4. Focus on specific match types (male/female) for consistency
5. Combine with other tennis analysis strategies
