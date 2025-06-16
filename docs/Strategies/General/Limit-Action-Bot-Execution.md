# Limit Action Bot Execution

**Category:** General Strategy  
**Strategy ID:** 2005

## Description

A control strategy that limits the number of action bots that can be executed within a specified time period. This strategy provides important risk management and resource control by preventing excessive bot execution that could lead to overexposure or system strain.

## Parameters

### Strategy
- **StrategyName** (Optional) - Enter the name of the strategy you wish to execute
- **MaximalNumberOfBots** (Optional) - Set the maximal number of action bots which can be executed per hour (Int32)

### Market
- **EvaluateEntryCriteriaOnlyOnce** (Optional) - Set this parameter to True to evaluate the entry criteria only once
- **StopMarketMonitoring** (Optional) - Set this parameter to True to stop market monitoring after the strategy completes execution. Monitoring will stop only if no other bot is active in the market

### Miscellaneous
- **StrategyReference** (Optional) - Set this parameter to specify which strategy sent the bet. The string is limited to 15 characters

## Usage Examples

### Conservative Limiting
- **StrategyName**: `"Place Bet"`
- **MaximalNumberOfBots**: `5`

### Moderate Control
- **StrategyName**: `"Close Selection Bet Position"`
- **MaximalNumberOfBots**: `10`

### High Activity Limit
- **MaximalNumberOfBots**: `20`
- **EvaluateEntryCriteriaOnlyOnce**: `true`

## Best Practices

1. **Risk Management**: Set limits based on your risk tolerance
2. **Market Conditions**: Adjust limits for different market types
3. **Time Periods**: Consider market activity patterns when setting hourly limits
4. **Strategy Specific**: Use different limits for different strategy types
5. **Monitoring**: Track execution patterns to optimize limits
