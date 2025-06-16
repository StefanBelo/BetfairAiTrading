# Greyhound Back Bot

## Overview
A specialized backing strategy for greyhound racing that analyzes price movements over multiple time intervals to identify profitable backing opportunities based on trap position, distance, and market liquidity.

## Strategy ID
3007

## Category
Custom

## Parameters

### Trigger Parameters
- **UseFirstMinuteMove** (Boolean, Optional): Use first minute move
- **FirstMinuteMoveMin** (Double, Optional): Minimum first minute move
- **FirstMinuteMoveMax** (Double, Optional): Maximum first minute move
- **UseSecondMinuteMove** (Boolean, Optional): Use second minute move
- **SecondMinuteMoveMin** (Double, Optional): Minimum second minute move
- **SecondMinuteMoveMax** (Double, Optional): Maximum second minute move
- **UseThirdMinuteMove** (Boolean, Optional): Use third minute move
- **ThirdMinuteMoveMin** (Double, Optional): Minimum third minute move
- **ThirdMinuteMoveMax** (Double, Optional): Maximum third minute move
- **UseSecondMinuteTotalMove** (Boolean, Optional): Use second minute total move
- **SecondMinuteTotalMoveMin** (Double, Optional): Minimum second minute total move
- **SecondMinuteTotalMoveMax** (Double, Optional): Maximum second minute total move
- **UseThirdMinuteTotalMove** (Boolean, Optional): Use third minute total move
- **ThirdMinuteTotalMoveMin** (Double, Optional): Minimum third minute total move
- **ThirdMinuteTotalMoveMax** (Double, Optional): Maximum third minute total move
- **MinimumTrapRange** (Byte, Optional): Minimum trap range
- **MaximumTrapRange** (Byte, Optional): Maximum trap range
- **MinimumDistance** (Int32, Optional): Minimum distance
- **MaximumDistance** (Int32, Optional): Maximum distance
- **MinimumTotalMatched** (Double, Optional): Minimum selection total matched
- **MaximumTotalMatched** (Double, Optional): Maximum selection total matched
- **MinimumPercentageTotalMatched** (Double, Optional): Minimum selection percentage total matched
- **MaximumPercentageTotalMatched** (Double, Optional): Maximum selection percentage total matched

### Bet Parameters
- **MinimumOdds** (Double, Optional): Specify the minimum odds you are willing to accept. Setting this parameter requires 'PlaceBetInAllowedOddsRange' to be 'True'
- **MaximumOdds** (Double, Optional): Specify the maximum odds you are willing to accept. Setting this parameter requires 'PlaceBetInAllowedOddsRange' to be 'True'

### Stake Parameters
- **Stake** (Double, Optional): Enter the stake amount for your bet. Note that when laying, your liability is calculated differently; to specify liability as your stake, set the 'StakeType' parameter to 'Liability'

### Market Parameters
- **EvaluateEntryCriteriaOnlyOnce** (Boolean, Optional): Set this parameter to True to evaluate the entry criteria only once
- **StopMarketMonitoring** (Boolean, Optional): Set this parameter to True to stop market monitoring after the strategy completes execution. Monitoring will stop only if no other bot is active in the market

### Miscellaneous Parameters
- **StrategyReference** (String, Optional): Set this parameter to specify which strategy sent the bet. The string is limited to 15 characters

## Usage Example
```json
{
  "UseFirstMinuteMove": true,
  "FirstMinuteMoveMin": -0.5,
  "FirstMinuteMoveMax": -0.1,
  "UseSecondMinuteMove": true,
  "SecondMinuteMoveMin": -0.3,
  "SecondMinuteMoveMax": 0.1,
  "MinimumTrapRange": 1,
  "MaximumTrapRange": 6,
  "MinimumDistance": 300,
  "MaximumDistance": 600,
  "MinimumOdds": 2.0,
  "MaximumOdds": 8.0,
  "MinimumPercentageTotalMatched": 20.0,
  "Stake": 15.0,
  "StrategyReference": "GreyhoundBack"
}
```

## Use Cases
- **Price Movement Trading**: Back selections showing positive price movements
- **Trap Position Analysis**: Target specific trap positions with good historical performance
- **Distance-Based Strategy**: Focus on specific race distances for specialized analysis
- **Market Momentum**: Capitalize on market momentum in greyhound racing
- **Liquidity-Based Selection**: Consider market liquidity for better execution

## Trading Strategies
1. **First Minute Movement**: React to early market movements
2. **Progressive Analysis**: Use multiple minute intervals for trend confirmation
3. **Trap Bias**: Exploit trap position advantages in specific track conditions
4. **Distance Specialization**: Focus on optimal race distances
5. **Liquidity Filtering**: Ensure adequate market depth for position management

## Best Practices
1. **Multi-Minute Analysis**: Use multiple time intervals for comprehensive analysis
2. **Trap Research**: Understand trap advantages at different tracks
3. **Distance Optimization**: Focus on distances with consistent performance patterns
4. **Liquidity Management**: Ensure sufficient market liquidity for entry and exit
5. **Track Conditions**: Consider track conditions and their impact on trap performance

## Greyhound-Specific Considerations
- **Trap Draw**: Some traps have advantages at certain tracks
- **Race Distance**: Performance can vary significantly by distance
- **Track Conditions**: Weather and track surface affect performance
- **Speed Figures**: Historical speed ratings are important indicators
- **Recent Form**: Recent race performance and fitness levels

## Notes
- Greyhound racing has faster race cycles compared to horse racing
- Market movements can be more volatile due to shorter betting windows
- Trap position can be crucial depending on track layout and conditions
- Consider early prices vs. starting prices for better value opportunities
