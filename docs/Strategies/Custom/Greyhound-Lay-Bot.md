# Greyhound Lay Bot

## Overview
A specialized laying strategy for greyhound racing that analyzes price movements and market conditions to identify profitable laying opportunities based on trap position, race distance, and liquidity analysis.

## Strategy ID
3008

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
  "FirstMinuteMoveMin": 0.2,
  "FirstMinuteMoveMax": 1.0,
  "UseSecondMinuteMove": true,
  "SecondMinuteMoveMin": 0.1,
  "SecondMinuteMoveMax": 0.8,
  "MinimumTrapRange": 1,
  "MaximumTrapRange": 3,
  "MinimumDistance": 400,
  "MaximumDistance": 800,
  "MinimumOdds": 1.5,
  "MaximumOdds": 4.0,
  "MinimumPercentageTotalMatched": 30.0,
  "Stake": 25.0,
  "StrategyReference": "GreyhoundLay"
}
```

## Use Cases
- **Overvalued Selection Laying**: Target selections that have moved to unrealistically short odds
- **Trap Disadvantage Analysis**: Lay selections from unfavorable trap positions
- **Market Momentum**: Lay selections showing excessive positive momentum
- **Distance-Based Laying**: Focus on distances where certain selections underperform
- **Liquidity Analysis**: Consider market depth for optimal laying opportunities

## Laying Strategies
1. **Steam Laying**: Lay selections that have shortened dramatically in price
2. **Trap Bias Exploitation**: Target selections from disadvantageous trap positions
3. **Distance Specialization**: Focus on race distances with poor historical performance
4. **Market Correction**: Lay selections that appear overbet by the market
5. **Progressive Movement**: Use multiple time intervals to identify overreactions

## Best Practices
1. **Risk Management**: Carefully manage liability exposure in volatile greyhound markets
2. **Trap Analysis**: Understand trap disadvantages at specific tracks
3. **Time-Based Analysis**: Use multiple minute intervals for trend confirmation
4. **Distance Research**: Focus on distances where selections historically underperform
5. **Liquidity Assessment**: Ensure sufficient market depth for safe laying

## Greyhound Laying Considerations
- **Short Race Duration**: Races are typically 30-60 seconds, creating quick market movements
- **Trap Influence**: Some traps have significant disadvantages at certain tracks
- **Weather Impact**: Track conditions can dramatically affect performance
- **Form Volatility**: Greyhound form can be more volatile than horse racing
- **Market Efficiency**: Smaller markets may have more pricing inefficiencies

## Risk Management
- **Liability Control**: Set maximum liability limits per race
- **Position Sizing**: Adjust stakes based on odds and confidence levels
- **Stop Losses**: Consider automated stop-loss mechanisms
- **Market Monitoring**: Continuously monitor for adverse price movements
- **Diversification**: Spread risk across multiple races and selections

## Notes
- Greyhound racing markets can be highly volatile with rapid price movements
- Trap position advantages/disadvantages vary significantly by track
- Consider using this strategy during peak betting periods for better liquidity
- Monitor track conditions and their impact on trap performance patterns
