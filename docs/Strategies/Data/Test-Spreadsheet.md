# Test Spreadsheet

## Overview
The **Test Spreadsheet** strategy is designed for testing and validating spreadsheet data integration with various market indicators. This testing strategy helps verify data flow and spreadsheet connectivity.

## Strategy Details
- **Category**: Data
- **Type**: Testing/Validation
- **Purpose**: Test spreadsheet integration and data connectivity

## Parameters

### Data Parameters
| Parameter | Group | Type | Required | Description |
|-----------|--------|------|----------|-------------|
| DataType | Data | Enum | No | The data type to show |
| UpdateInterval | Data | TimeSpan | No | Set the update interval for data sample |

### Data Type Options
- AverageTradedPrice
- AverageOfferedPrice
- AverageOfferedPriceInRange
- AverageOfferedPriceAddedRemoved
- CumulatedAverageOfferedPriceAddedRemoved
- MovingTradedVolume

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

### Average Traded Price Test
```
Strategy: Test Spreadsheet
DataType: AverageTradedPrice
UpdateInterval: 00:00:30
EvaluateEntryCriteriaOnlyOnce: False
StopMarketMonitoring: False
```

### Moving Volume Analysis Test
```
Strategy: Test Spreadsheet
DataType: MovingTradedVolume
UpdateInterval: 00:01:00
EvaluateEntryCriteriaOnlyOnce: True
StopMarketMonitoring: True
StrategyReference: "VOL_TEST"
```

### Price Range Testing
```
Strategy: Test Spreadsheet
DataType: AverageOfferedPriceInRange
UpdateInterval: 00:00:15
EvaluateEntryCriteriaOnlyOnce: False
StopMarketMonitoring: False
StrategyReference: "RANGE_TEST"
```

## Best Practices

1. **Development Testing**: Use during development to verify spreadsheet integration
2. **Data Validation**: Test different data types to ensure proper connectivity
3. **Update Intervals**: Adjust intervals based on testing requirements
4. **Performance Testing**: Monitor system performance during data transfer

## Integration

This strategy works well with:
- Spreadsheet applications (Excel, Google Sheets)
- Data analysis tools
- Development environments
- Quality assurance testing

## Notes

- This is a testing strategy for development and validation purposes
- Provides various data types for comprehensive testing
- Useful for verifying spreadsheet connectivity and data flow
- Can be used to test system performance with different update intervals
- Essential for quality assurance in data-driven trading systems
