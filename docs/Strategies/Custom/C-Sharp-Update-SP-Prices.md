# C# - Update SP prices

## Overview
The C# Update SP prices strategy is a custom utility bot designed to update Starting Price (SP) information within the Bfexplorer framework. This strategy focuses on maintaining accurate SP data for betting analysis and decision-making.

## Strategy Type
- **Category**: Custom/Unknown
- **ID**: 201
- **Type**: Data Management/SP Utility

## Parameters

### Market Parameters
| Parameter | Group | Type | Required | Description |
|-----------|-------|------|----------|-------------|
| EvaluateEntryCriteriaOnlyOnce | Market | Boolean | No | Set this parameter to True to evaluate the entry criteria only once. |
| StopMarketMonitoring | Market | Boolean | No | Set this parameter to True to stop market monitoring after the strategy completes execution. Monitoring will stop only if no other bot is active in the market. |

### Miscellaneous Parameters
| Parameter | Group | Type | Required | Description |
|-----------|-------|------|----------|-------------|
| StrategyReference | Miscellaneous | String | No | Set this parameter to specify which strategy sent the bet. The string is limited to 15 characters. |

## Usage
This strategy is used to automatically update Starting Price (SP) data within the Bfexplorer system. It ensures that SP information is current and accurate for analysis and strategy execution.

## Key Features
- **SP Data Management**: Automatically updates Starting Price information
- **Real-time Updates**: Keeps SP data current during market operation
- **Integration Ready**: Works with other strategies that depend on SP data
- **Custom Implementation**: C# based for flexible customization

## Configuration Tips
1. Run this strategy to ensure SP data is current before executing other SP-dependent strategies
2. Set `EvaluateEntryCriteriaOnlyOnce` to True for one-time SP updates
3. Use in conjunction with SP-based analysis strategies
4. Monitor execution to ensure SP data is updating correctly

## Use Cases
- **Pre-race Preparation**: Update SP data before race starts
- **Historical Analysis**: Maintain accurate SP records for later analysis
- **Strategy Integration**: Provide current SP data for other strategies
- **Data Validation**: Ensure SP information consistency

## Best Practices
- Run regularly to maintain current SP data
- Monitor for any data update errors
- Coordinate with other SP-dependent strategies
- Verify SP accuracy after execution

## Related Strategies
- C# - Show Selection SP prices
- Betfair SP
- My Csharp Test Bot
- Place SP Bet

## Technical Notes
This strategy implements custom C# logic to interface with Betfair SP data feeds and update internal Bfexplorer data structures accordingly.

## Support
For technical issues with SP data updates, refer to Bfexplorer documentation on data management and Betfair API integration.
