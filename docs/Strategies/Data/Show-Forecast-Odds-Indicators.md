# Show Forecast Odds Indicators

## Overview
The **Show Forecast Odds Indicators** strategy displays forecast odds indicators for market analysis. This data-focused strategy provides insights into forecast odds patterns and trends.

## Strategy Details
- **Category**: Data
- **Type**: Market Analysis
- **Purpose**: Display forecast odds indicators for market research

## Parameters

### Market Parameters
| Parameter | Group | Type | Required | Description |
|-----------|--------|------|----------|-------------|
| EvaluateEntryCriteriaOnlyOnce | Market | Boolean | No | Set to True to evaluate the entry criteria only once |
| StopMarketMonitoring | Market | Boolean | No | Set to True to stop market monitoring after strategy completion |

### Miscellaneous Parameters
| Parameter | Group | Type | Required | Description |
|-----------|--------|------|----------|-------------|
| StrategyReference | Miscellaneous | String | No | Strategy reference identifier (max 15 characters) |

## Usage Examples

### Basic Forecast Odds Display
```
Strategy: Show Forecast Odds Indicators
EvaluateEntryCriteriaOnlyOnce: True
StopMarketMonitoring: False
```

### Single Execution Mode
```
Strategy: Show Forecast Odds Indicators
EvaluateEntryCriteriaOnlyOnce: True
StopMarketMonitoring: True
StrategyReference: "FORECAST_ODDS"
```

## Best Practices

1. **Market Analysis**: Use for analyzing forecast odds patterns and trends
2. **Data Collection**: Monitor forecast odds indicators for research purposes
3. **Real-time Monitoring**: Keep StopMarketMonitoring=False for continuous monitoring
4. **Historical Analysis**: Use with data recording strategies for historical analysis

## Integration

This strategy works well with:
- Data recording strategies
- Market analysis tools
- Trading decision support systems
- Risk assessment frameworks

## Notes

- This is a data display strategy with no betting functionality
- Provides forecast odds indicators for market analysis
- Useful for research and pattern analysis
- Can be combined with other data strategies for comprehensive market monitoring
