# C# - No Exposure Trigger Bot

## Overview
The C# No Exposure Trigger Bot is a custom strategy that executes other strategies when there is no current market exposure. This strategy acts as a risk management tool, ensuring that new strategies only execute when no existing positions exist.

## Strategy Type
- **Category**: Custom/Unknown
- **ID**: 203
- **Type**: Risk Management/Trigger Strategy

## Parameters

### Strategy Parameters
| Parameter | Group | Type | Required | Description |
|-----------|-------|------|----------|-------------|
| StrategyName | Strategy | String | No | The strategy name you want to execute. |

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
This strategy monitors market exposure and executes a specified strategy only when no current exposure exists. It acts as a safety mechanism to prevent overlapping positions and manage risk effectively.

## Key Features
- **Exposure Monitoring**: Continuously checks for current market exposure
- **Conditional Execution**: Only executes strategies when no exposure exists
- **Risk Management**: Prevents overlapping positions and excessive exposure
- **Strategy Integration**: Can trigger any named strategy when conditions are met

## Configuration Tips
1. Set `StrategyName` to the strategy you want to execute when no exposure exists
2. Use `EvaluateEntryCriteriaOnlyOnce` to control how often exposure is checked
3. Combine with position management strategies for comprehensive risk control
4. Set appropriate `StrategyReference` for tracking purposes

## Use Cases
- **Risk Management**: Prevent excessive exposure in volatile markets
- **Strategy Coordination**: Ensure strategies don't conflict with existing positions
- **Position Control**: Maintain disciplined approach to position sizing
- **Entry Management**: Control when new positions can be opened

## Exposure Checks
The strategy evaluates:
- Current market exposure across all selections
- Outstanding bet positions
- Pending order status
- Risk exposure calculations

## Execution Logic
1. Monitor current market exposure
2. Check if exposure is zero or within acceptable limits
3. If no exposure exists, execute the specified strategy
4. Continue monitoring or stop based on configuration

## Best Practices
- Use with strategies that might create conflicting positions
- Set clear criteria for what constitutes "no exposure"
- Monitor execution to ensure proper risk management
- Test thoroughly before live implementation

## Related Strategies
- Close Selection Bet Position
- Close Market Bet Position
- Place Bet
- My Csharp Test Bot

## Risk Considerations
- Ensure exposure calculations include all relevant positions
- Consider pending orders in exposure calculations
- Monitor for edge cases where exposure might change rapidly
- Implement appropriate safeguards for system latency

## Technical Implementation
This strategy uses custom C# logic to:
- Calculate current market exposure
- Monitor position status
- Trigger strategy execution based on exposure conditions
- Maintain continuous monitoring as required

## Support
For issues with exposure calculations or strategy triggering, refer to Bfexplorer documentation on risk management and position monitoring.
