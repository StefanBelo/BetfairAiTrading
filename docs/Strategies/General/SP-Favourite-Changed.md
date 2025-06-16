# SP Favourite Changed

## Overview
A monitoring strategy that detects changes in the Starting Price (SP) favourite and can trigger other strategies or actions when the market favourite changes based on SP calculations.

## Strategy ID
1017

## Category
General Strategy

## Parameters

### Market Parameters
- **EvaluateEntryCriteriaOnlyOnce** (Boolean, Optional): Set this parameter to True to evaluate the entry criteria only once
- **StopMarketMonitoring** (Boolean, Optional): Set this parameter to True to stop market monitoring after the strategy completes execution. Monitoring will stop only if no other bot is active in the market

### Miscellaneous Parameters
- **StrategyReference** (String, Optional): Set this parameter to specify which strategy sent the bet. The string is limited to 15 characters

## Usage Example
```json
{
  "EvaluateEntryCriteriaOnlyOnce": false,
  "StopMarketMonitoring": false,
  "StrategyReference": "SPFavChange"
}
```

## Use Cases
- **Market Monitoring**: Monitor changes in market favourite based on SP calculations
- **Strategy Triggering**: Trigger other strategies when favourite changes
- **Market Analysis**: Track favourite volatility and market dynamics
- **Alert System**: Generate alerts when significant favourite changes occur

## SP Favourite Analysis
1. **SP Calculation Monitoring**: Continuously monitor SP-based favourite calculations
2. **Change Detection**: Detect when the SP favourite changes
3. **Market Impact Analysis**: Analyze the impact of favourite changes on market dynamics
4. **Volatility Tracking**: Track how often the favourite changes in different market types

## Best Practices
1. **Continuous Monitoring**: Keep the strategy running to detect all favourite changes
2. **Market Selection**: Use in markets with sufficient liquidity for accurate SP calculations
3. **Timing Considerations**: Be aware that SP calculations change throughout the betting period
4. **Integration**: Combine with other strategies that can react to favourite changes

## SP Considerations
- **SP Calculation**: Starting Price is calculated based on unmatched bets at race start
- **Market Liquidity**: SP accuracy depends on market liquidity and betting activity
- **Timing Sensitivity**: SP estimates can change rapidly, especially close to race start
- **Favourite Volatility**: Some markets have more volatile favourites than others

## Integration Strategies
- **Conditional Execution**: Use with "If Then Else" strategy to trigger actions on favourite changes
- **Strategy Chaining**: Chain with other strategies that benefit from favourite change information
- **Alert Systems**: Integrate with notification systems for real-time alerts
- **Data Recording**: Combine with data recording strategies to track favourite changes

## Notes
- This strategy is primarily a monitoring and detection tool
- It works best in liquid markets with active betting
- SP calculations can be volatile, especially in smaller markets
- Consider using with other strategies that can act on the detected changes
