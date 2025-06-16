# My Csharp Test Bot

## Overview
The My Csharp Test Bot is a custom C# testing strategy template for Bfexplorer. This strategy serves as a foundation for custom C# development and testing within the Bfexplorer framework.

## Strategy Type
- **Category**: Custom/Unknown
- **ID**: 200
- **Type**: Test/Development Bot

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
This strategy is designed for developers who want to test custom C# implementations within the Bfexplorer framework. It provides a minimal template with basic market monitoring and evaluation controls.

## Key Features
- **Custom Development**: Template for custom C# strategy development
- **Basic Controls**: Standard market monitoring and evaluation parameters
- **Testing Framework**: Suitable for testing custom logic and implementations

## Configuration Tips
1. Use this strategy as a starting point for custom C# development
2. Modify the core logic to implement your specific trading algorithms
3. Set `EvaluateEntryCriteriaOnlyOnce` to True if you only need one-time evaluation
4. Use `StrategyReference` to identify the strategy instance (max 15 characters)

## Best Practices
- Test thoroughly in a safe environment before live use
- Implement proper error handling in custom code
- Use appropriate logging for debugging purposes
- Follow Bfexplorer development guidelines

## Related Strategies
- C# - Update SP prices
- C# - Show Selection SP prices
- C# - No Exposure Trigger Bot

## Support
This is a custom development template. Refer to Bfexplorer documentation for C# strategy development guidelines and API references.
