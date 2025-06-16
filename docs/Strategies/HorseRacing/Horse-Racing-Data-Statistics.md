# Horse Racing Data Statistics

**Category:** Horse Racing  
**Strategy ID:** 10300

## Description

A comprehensive horse racing data analysis strategy that collects and analyzes statistical information about horses, races, and performance metrics. This strategy serves as a foundation for data-driven betting decisions and performance evaluation.

## Parameters

### Strategy
- **StrategyName** (Optional) - The strategy is executed on dedicated selection/s

### Market
- **EvaluateEntryCriteriaOnlyOnce** (Optional) - Set this parameter to True to evaluate the entry criteria only once
- **StopMarketMonitoring** (Optional) - Set this parameter to True to stop market monitoring after the strategy completes execution. Monitoring will stop only if no other bot is active in the market

### Miscellaneous
- **StrategyReference** (Optional) - Set this parameter to specify which strategy sent the bet. The string is limited to 15 characters

## Usage Examples

### Statistical Analysis
- **StrategyName**: `"Show Selection Data"`

### Performance Tracking
- **StrategyName**: `"Record Market Selection Data"`

### Data Collection
- **StrategyName**: `"Race Data to Spreadsheet"`

## Best Practices

1. Use for comprehensive race and horse performance analysis
2. Combine with other data collection strategies
3. Ensure adequate data storage for historical analysis
4. Regular data validation and cleaning
5. Use collected statistics to inform betting strategies
