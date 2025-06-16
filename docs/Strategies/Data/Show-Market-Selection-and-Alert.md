# Show Market Selection and Alert

**Category:** Data  
**Strategy ID:** 2006

## Description

A monitoring and alerting strategy that displays market selection information and can trigger alerts based on specific conditions. This strategy combines data visualization with notification capabilities for important market events or conditions.

## Parameters

### Miscellaneous
- **AlertMessage** (Optional) - Set your alert message (String)
- **StrategyReference** (Optional) - Set this parameter to specify which strategy sent the bet. The string is limited to 15 characters

### Selection
- **SortSelectionsBy** (Optional) - Use this setting to sort selections by either last traded price or total matched volume
  - Options: DoNotSort, LastPriceTraded, TotalMatched
- **ExecuteOnSelection** (Optional) - The selection on which you want to execute your bot. If set to 0 the strategy is executed on the active selection in the market grid view. If set to 1 .. X, the strategy is executed on X favorite or X selection depending on how you set the parameter SortSelectionsBy (Byte)

### Market
- **StopBotExecutionOnMarketVersionChange** (Optional) - Set to True if you want to stop the strategy execution when a market version changes. The strategy execution will be stopped only when no bet placed by the strategy was matched (Boolean)
- **EvaluateEntryCriteriaOnlyOnce** (Optional) - Set this parameter to True to evaluate the entry criteria only once
- **StopMarketMonitoring** (Optional) - Set this parameter to True to stop market monitoring after the strategy completes execution. Monitoring will stop only if no other bot is active in the market

## Usage Examples

### Price Alert
- **AlertMessage**: `"Favorite odds changed significantly"`
- **SortSelectionsBy**: `LastPriceTraded`
- **ExecuteOnSelection**: `1`

### Volume Alert
- **AlertMessage**: `"High volume detected on selection"`
- **SortSelectionsBy**: `TotalMatched`
- **ExecuteOnSelection**: `0`

### Market Change Alert
- **AlertMessage**: `"Market version updated"`
- **StopBotExecutionOnMarketVersionChange**: `true`

## Best Practices

1. **Clear Messages**: Use descriptive alert messages for quick understanding
2. **Selection Focus**: Target specific selections for relevant alerts
3. **Alert Frequency**: Consider how often alerts should trigger
4. **Message Content**: Include relevant data in alert messages
5. **Response Planning**: Have clear actions for different alert types
