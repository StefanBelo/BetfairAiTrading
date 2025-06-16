# Ladbrokes In-Running Bot

## Overview
An advanced in-running betting strategy that monitors live race conditions and price movements to execute bets during races based on Ladbrokes price analysis and real-time market data.

## Strategy ID
3009

## Category
Custom

## Parameters

### Trigger Parameters
- **UseLadbrokes1hourDifference** (Boolean, Optional): Use Ladbrokes 1 hour difference
- **Ladbrokes1hourDifferenceMin** (Double, Optional): Minimum Ladbrokes 1 hour difference
- **Ladbrokes1hourDifferenceMax** (Double, Optional): Maximum Ladbrokes 1 hour difference
- **UseLadbrokes10minuteDifference** (Boolean, Optional): Use Ladbrokes 10 minute difference
- **Ladbrokes10minuteDifferenceMin** (Double, Optional): Minimum Ladbrokes 10 minute difference
- **Ladbrokes10minuteDifferenceMax** (Double, Optional): Maximum Ladbrokes 10 minute difference
- **UseBetfairRaceTimeWindifference** (Boolean, Optional): Use betfair race time win difference
- **BetfairRaceTimeWindifferenceMin** (Double, Optional): Minimum betfair race time win difference
- **BetfairRaceTimeWindifferenceMax** (Double, Optional): Maximum betfair race time win difference
- **UseLadbrokesRaceTimeWinDifference** (Boolean, Optional): Use Ladbrokes race time win difference
- **LadbrokesRaceTimeWinDifferenceMin** (Double, Optional): Minimum Ladbrokes race time win difference
- **LadbrokesRaceTimeWinDifferenceMax** (Double, Optional): Maximum Ladbrokes race time win difference
- **MinimumRtwIndustryPercentage** (Double, Optional): Minimum race time / industry price percentage
- **MaximumRtwIndustryPercentage** (Double, Optional): Maximum race time / industry price percentage
- **MinimumRTW** (Double, Optional): Minimum race time price
- **MaximumRTW** (Double, Optional): Maximum race time price
- **MinimumTotalMatched** (Double, Optional): Minimum selection total matched
- **MaximumTotalMatched** (Double, Optional): Maximum selection total matched
- **MinimumPercentageTotalMatched** (Double, Optional): Minimum selection percentage total matched
- **MaximumPercentageTotalMatched** (Double, Optional): Maximum selection percentage total matched
- **MinimumRunners** (Byte, Optional): Minimum number of runners
- **MaximumRunners** (Byte, Optional): Maximum number of runners
- **MinimumAge** (Byte, Optional): Minimum horse age
- **MaximumAge** (Byte, Optional): Maximum horse age

### Bet Parameters
- **TriggerOdds** (Double, Optional): The maximum in-running odds/price target
- **MinimumOdds** (Double, Optional): Specify the minimum odds you are willing to accept. Setting this parameter requires 'PlaceBetInAllowedOddsRange' to be 'True'
- **MaximumOdds** (Double, Optional): Specify the maximum odds you are willing to accept. Setting this parameter requires 'PlaceBetInAllowedOddsRange' to be 'True'

### Stake Parameters
- **Stake** (Double, Optional): Enter the stake amount for your bet. Note that when laying, your liability is calculated differently; to specify liability as your stake, set the 'StakeType' parameter to 'Liability'

### Time Parameters
- **StopActionBotExecutionAtTime** (TimeSpan, Optional): Stop the action bot/s execution in time interval starting from a race start time

### Market Parameters
- **EvaluateEntryCriteriaOnlyOnce** (Boolean, Optional): Set this parameter to True to evaluate the entry criteria only once
- **StopMarketMonitoring** (Boolean, Optional): Set this parameter to True to stop market monitoring after the strategy completes execution. Monitoring will stop only if no other bot is active in the market

### Miscellaneous Parameters
- **StrategyReference** (String, Optional): Set this parameter to specify which strategy sent the bet. The string is limited to 15 characters

## Usage Example
```json
{
  "UseLadbrokes1hourDifference": true,
  "Ladbrokes1hourDifferenceMin": -1.0,
  "Ladbrokes1hourDifferenceMax": 1.0,
  "UseBetfairRaceTimeWindifference": true,
  "BetfairRaceTimeWindifferenceMin": 0.5,
  "BetfairRaceTimeWindifferenceMax": 3.0,
  "TriggerOdds": 5.0,
  "MinimumOdds": 2.0,
  "MaximumOdds": 10.0,
  "MinimumPercentageTotalMatched": 25.0,
  "Stake": 20.0,
  "StopActionBotExecutionAtTime": "00:02:00",
  "StrategyReference": "LadbrokesInRun"
}
```

## Use Cases
- **Live Race Trading**: Execute trades during live races based on race progress
- **In-Running Arbitrage**: Exploit price differences during live betting
- **Race Position Trading**: React to changing race positions and momentum
- **Cross-Platform Analysis**: Compare live prices across Ladbrokes and Betfair
- **Time-Sensitive Trading**: Execute trades within specific time windows during races

## In-Running Strategies
1. **Early Position Trading**: Trade based on early race positions and pace
2. **Final Furlong Trading**: Execute trades in the closing stages of races
3. **Pace Analysis**: Trade based on race pace and tactical positioning
4. **Market Reaction Trading**: React to live market movements during races
5. **Exit Strategy Trading**: Plan exit points based on race development

## Best Practices
1. **Fast Execution**: In-running markets move extremely quickly, requiring fast execution
2. **Race Monitoring**: Continuously monitor race progress and positioning
3. **Time Management**: Set appropriate stop times to avoid late-race volatility
4. **Risk Control**: Implement strict risk controls due to rapid price movements
5. **Market Understanding**: Understand how live race events affect pricing

## In-Running Considerations
- **Speed of Execution**: Trades must be executed within seconds
- **Race Commentary**: Live race information affects market movements
- **Position Changes**: Race positions can change rapidly affecting prices
- **Market Volatility**: In-running markets are highly volatile
- **Technical Requirements**: Requires reliable, fast data feeds and execution

## Risk Management
- **Position Limits**: Set maximum exposure limits per race
- **Time Stops**: Implement automatic stops at specific race times
- **Market Volatility**: Account for extreme price movements during races
- **Technical Risk**: Prepare for potential technical failures during live trading
- **Liquidity Risk**: In-running liquidity can disappear rapidly

## Notes
- In-running betting requires sophisticated technical infrastructure
- Latency and execution speed are critical for success
- Market conditions can change dramatically within seconds
- Consider using this strategy only with sufficient technical capabilities
- Regulatory requirements may vary for in-running betting in different jurisdictions
