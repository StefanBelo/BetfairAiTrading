# Show Market Data

**Category:** Data  
**Strategy ID:** 1004

## Description

A comprehensive market data visualization strategy that displays various market indicators and statistics. This strategy provides real-time insights into market behavior, price movements, and trading patterns through multiple data visualization options.

## Parameters

### Data
- **DataType** (Optional) - The data type to show
  - Options: AverageTradedPrice, AverageOfferedPrice, AverageOfferedPriceInRange, AverageOfferedPriceAddedRemoved, CumulatedAverageOfferedPriceAddedRemoved, MovingTradedVolume
- **UpdateInterval** (Optional) - Set the update interval for data sample (TimeSpan)

### Market
- **EvaluateEntryCriteriaOnlyOnce** (Optional) - Set this parameter to True to evaluate the entry criteria only once
- **StopMarketMonitoring** (Optional) - Set this parameter to True to stop market monitoring after the strategy completes execution. Monitoring will stop only if no other bot is active in the market

### Miscellaneous
- **StrategyReference** (Optional) - Set this parameter to specify which strategy sent the bet. The string is limited to 15 characters

## Usage Examples

### Price Analysis
- **DataType**: `AverageTradedPrice`
- **UpdateInterval**: `00:00:30` (30 seconds)

### Volume Monitoring
- **DataType**: `MovingTradedVolume`
- **UpdateInterval**: `00:00:15` (15 seconds)

### Liquidity Assessment
- **DataType**: `AverageOfferedPrice`
- **UpdateInterval**: `00:00:10` (10 seconds)

### Market Depth Analysis
- **DataType**: `AverageOfferedPriceInRange`
- **UpdateInterval**: `00:00:20` (20 seconds)

## Data Type Explanations

### Price-Based Indicators
- **AverageTradedPrice**: Shows average price of matched bets
- **AverageOfferedPrice**: Displays average of available odds
- **AverageOfferedPriceInRange**: Price averages within specific ranges

### Volume-Based Indicators
- **MovingTradedVolume**: Shows volume trends over time
- **AverageOfferedPriceAddedRemoved**: Tracks liquidity changes

### Advanced Indicators
- **CumulatedAverageOfferedPriceAddedRemoved**: Cumulative liquidity analysis

## Best Practices

1. **Data Selection**: Choose data types that align with your analysis goals
2. **Update Frequency**: Balance real-time needs with system performance
3. **Multiple Views**: Use different data types for comprehensive analysis
4. **Pattern Recognition**: Look for recurring patterns in market data
5. **Historical Context**: Compare current data with historical patterns
