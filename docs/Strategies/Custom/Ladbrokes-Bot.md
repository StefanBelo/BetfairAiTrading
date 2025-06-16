# Ladbrokes Bot

## Overview
A specialized betting strategy that analyzes Ladbrokes price movements and betting exchange data to identify profitable betting opportunities based on price differences and market conditions.

## Strategy ID
3004

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
- **MinimumRank** (Byte, Optional): Minimum rank
- **MaximumRank** (Byte, Optional): Maximum rank
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
- **Odds** (Double, Optional): Specify the odds you wish to bet on. To place a bet on exact odds, set the 'PlaceBetInAllowedOddsRange' parameter to 'False'

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
  "UseLadbrokes1hourDifference": true,
  "Ladbrokes1hourDifferenceMin": -1.0,
  "Ladbrokes1hourDifferenceMax": 1.0,
  "UseBetfairRaceTimeWindifference": true,
  "BetfairRaceTimeWindifferenceMin": 0.5,
  "BetfairRaceTimeWindifferenceMax": 3.0,
  "MinimumRank": 1,
  "MaximumRank": 6,
  "MinimumRunners": 8,
  "MaximumRunners": 20,
  "Stake": 25.0,
  "StrategyReference": "LadbrokesBot"
}
```

## Use Cases
- **Cross-Platform Arbitrage**: Identify price discrepancies between Ladbrokes and Betfair
- **Price Movement Tracking**: Monitor Ladbrokes price changes over different time intervals
- **Rank-Based Betting**: Target selections based on their market ranking
- **Liquidity Analysis**: Consider total matched amounts for betting decisions
- **Race Condition Filtering**: Apply race-specific criteria for better selection

## Best Practices
1. **Time-Based Analysis**: Use both 1-hour and 10-minute differences for comprehensive price tracking
2. **Rank Filtering**: Focus on selections within specific rank ranges for better predictability
3. **Liquidity Consideration**: Ensure sufficient market liquidity before placing bets
4. **Age and Runner Filters**: Apply race conditions to improve selection quality
5. **Regular Monitoring**: Continuously track price movements for optimal entry points

## Notes
- Requires access to Ladbrokes price data and Betfair exchange data
- Price differences can be short-lived, requiring quick execution
- Market conditions and liquidity can affect strategy performance
- Consider regulatory requirements when using cross-platform strategies
