# Tennis Record Statistics

**Category:** Tennis  
**Strategy ID:** 6902

## Description

A tennis data collection strategy that records comprehensive match statistics and ATP data for analysis and future reference. This strategy is essential for building historical databases and performance tracking.

## Parameters

### Data
- **LoadAtpData** (Optional) - Load ATP data (Boolean)
- **UpdateInterval** (Optional) - Data update interval (TimeSpan)

### Market
- **EvaluateEntryCriteriaOnlyOnce** (Optional) - Set this parameter to True to evaluate the entry criteria only once
- **StopMarketMonitoring** (Optional) - Set this parameter to True to stop market monitoring after the strategy completes execution. Monitoring will stop only if no other bot is active in the market

### Miscellaneous
- **StrategyReference** (Optional) - Set this parameter to specify which strategy sent the bet. The string is limited to 15 characters

## Usage Examples

### Comprehensive Data Recording
- **LoadAtpData**: `true`
- **UpdateInterval**: `00:02:00` (2 minutes)

### Historical Analysis Setup
- **LoadAtpData**: `true`
- **UpdateInterval**: `00:10:00` (10 minutes)

## Best Practices

1. Enable ATP data loading for complete statistical records
2. Use consistent update intervals for reliable data collection
3. Ensure sufficient storage for historical data accumulation
4. Regularly backup collected statistics
