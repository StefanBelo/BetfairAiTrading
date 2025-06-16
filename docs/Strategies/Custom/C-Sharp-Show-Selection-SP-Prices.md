# C# - Show Selection SP prices

## Overview
The C# Show Selection SP prices strategy is a display utility that presents Starting Price (SP) information for selections in an accessible format. This strategy helps traders visualize and analyze SP data for betting decisions.

## Strategy Type
- **Category**: Custom/Unknown
- **ID**: 202
- **Type**: Data Display/SP Visualization

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
This strategy displays Starting Price (SP) information for market selections, allowing traders to view and analyze SP data in real-time or after market settlement.

## Key Features
- **SP Visualization**: Clear display of Starting Price data for all selections
- **Real-time Display**: Shows current SP information as it becomes available
- **Selection Focus**: Displays SP prices for specific selections
- **Custom Interface**: C# implementation allows for flexible display formatting

## Configuration Tips
1. Use after market settlement to view final SP prices
2. Set `EvaluateEntryCriteriaOnlyOnce` to True for one-time SP display
3. Combine with other analysis tools for comprehensive market review
4. Monitor output for SP price accuracy

## Use Cases
- **Post-race Analysis**: Review SP prices after market settlement
- **Price Comparison**: Compare SP prices with pre-race odds
- **Strategy Validation**: Verify SP-based strategy performance
- **Market Research**: Analyze SP patterns across markets

## Display Information
The strategy typically shows:
- Selection names
- Starting Price (SP) values
- SP availability status
- Comparison with final traded prices
- Settlement information

## Best Practices
- Run after market settlement for accurate SP data
- Use in combination with other data display strategies
- Save or export SP data for historical analysis
- Verify SP accuracy against official sources

## Related Strategies
- C# - Update SP prices
- Betfair SP
- Show Selection Data
- My Csharp Test Bot

## Technical Implementation
This strategy uses custom C# code to retrieve and format SP data from Bfexplorer's internal data structures and present it in a user-friendly format.

## Output Format
The strategy provides formatted output showing:
- Clear SP price information
- Selection identification
- Market context
- Timestamp information

## Support
For issues with SP display or data formatting, refer to Bfexplorer documentation on data visualization and C# strategy development.
