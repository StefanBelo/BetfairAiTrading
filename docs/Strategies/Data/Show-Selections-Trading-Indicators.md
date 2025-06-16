# Show Selections Trading Indicators

**Category:** Data  
**Strategy ID:** 2000

## Description

A data visualization strategy that displays comprehensive trading indicators for all selections in a market. This strategy provides real-time monitoring of key trading metrics, price movements, and market activity across all selections.

## Parameters

### Data
- **UpdateInterval** (Optional) - Set the update interval (TimeSpan)

### Market
- **EvaluateEntryCriteriaOnlyOnce** (Optional) - Set this parameter to True to evaluate the entry criteria only once
- **StopMarketMonitoring** (Optional) - Set this parameter to True to stop market monitoring after the strategy completes execution. Monitoring will stop only if no other bot is active in the market

### Miscellaneous
- **StrategyReference** (Optional) - Set this parameter to specify which strategy sent the bet. The string is limited to 15 characters

## Usage Examples

### Real-time Monitoring
- **UpdateInterval**: `00:00:05` (5 seconds)

### Periodic Analysis
- **UpdateInterval**: `00:00:30` (30 seconds)

### Conservative Updates
- **UpdateInterval**: `00:01:00` (1 minute)

## Best Practices

1. **Update Frequency**: Balance between data freshness and system resources
2. **Market Overview**: Use for comprehensive market monitoring
3. **Pattern Recognition**: Identify trading patterns across selections
4. **Data Export**: Combine with recording strategies for historical analysis
5. **Performance Monitoring**: Track indicator accuracy and relevance
