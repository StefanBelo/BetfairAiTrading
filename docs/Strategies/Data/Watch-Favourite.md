# Watch Favourite

**Category:** Data  
**Strategy ID:** 2004

## Description

A monitoring strategy that specifically tracks the market favorite, providing real-time updates on price movements, position changes, and trading activity. This strategy is essential for strategies that depend on favorite behavior and market leadership changes.

## Parameters

### Data
- **UpdateInterval** (Optional) - Set the update interval (TimeSpan)

### Market
- **EvaluateEntryCriteriaOnlyOnce** (Optional) - Set this parameter to True to evaluate the entry criteria only once
- **StopMarketMonitoring** (Optional) - Set this parameter to True to stop market monitoring after the strategy completes execution. Monitoring will stop only if no other bot is active in the market

### Miscellaneous
- **StrategyReference** (Optional) - Set this parameter to specify which strategy sent the bet. The string is limited to 15 characters

## Usage Examples

### Continuous Favorite Monitoring
- **UpdateInterval**: `00:00:05` (5 seconds)

### Periodic Favorite Checks
- **UpdateInterval**: `00:00:30` (30 seconds)

### Low-frequency Monitoring
- **UpdateInterval**: `00:02:00` (2 minutes)

## Best Practices

1. **Update Frequency**: Use frequent updates for volatile markets
2. **Favorite Changes**: Monitor for favorite shifts and their implications
3. **Price Tracking**: Watch for significant favorite price movements
4. **Market Leadership**: Understand favorite's influence on market sentiment
5. **Strategy Integration**: Combine with other strategies that react to favorite behavior
