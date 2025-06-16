# Bookmaker Price Bot

## Overview
A specialized strategy designed to analyze bookmaker price movements and trigger betting actions based on various bookmaker price difference criteria and race conditions.

## Strategy ID
3003

## Category
Custom

## Parameters

### Trigger Parameters
- **UseBookmaker1hourDifference** (Boolean, Optional): Use bookmaker 1 hour difference
- **Bookmaker1hourDifferenceMin** (Double, Optional): Minimum bookmaker 1 hour difference
- **Bookmaker1hourDifferenceMax** (Double, Optional): Maximum bookmaker 1 hour difference
- **UseBookmaker10minuteDifference** (Boolean, Optional): Use bookmaker 10 minute difference  
- **Bookmaker10minuteDifferenceMin** (Double, Optional): Minimum bookmaker 10 minute difference
- **Bookmaker10minuteDifferenceMax** (Double, Optional): Maximum bookmaker 10 minute difference
- **UseBookmakerTotalDifference** (Boolean, Optional): Use bookmaker total difference
- **BookmakerTotalDifferenceMin** (Double, Optional): Minimum bookmaker total difference
- **BookmakerTotalDifferenceMax** (Double, Optional): Maximum bookmaker total difference
- **UseRaceTimeWindifference** (Boolean, Optional): Use race time win difference
- **RaceTimeWindifferenceMin** (Double, Optional): Minimum race time win difference
- **RaceTimeWindifferenceMax** (Double, Optional): Maximum race time win difference
- **MinimumRtwIndustryPercentage** (Double, Optional): Minimum race time / industry price percentage
- **MaximumRtwIndustryPercentage** (Double, Optional): Maximum race time / industry price percentage
- **MinimumRTW** (Double, Optional): Minimum race time price
- **MaximumRTW** (Double, Optional): Maximum race time price
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
  "UseBookmaker1hourDifference": true,
  "Bookmaker1hourDifferenceMin": -0.5,
  "Bookmaker1hourDifferenceMax": 0.5,
  "UseRaceTimeWindifference": true,
  "RaceTimeWindifferenceMin": 1.0,
  "RaceTimeWindifferenceMax": 5.0,
  "MinimumRunners": 6,
  "MaximumRunners": 16,
  "Odds": 0,
  "Stake": 10.0,
  "StrategyReference": "BookmakerBot"
}
```

## Use Cases
- **Bookmaker Arbitrage**: Identify and exploit price differences between bookmakers and betting exchanges
- **Price Movement Analysis**: Track and respond to bookmaker price movements over different time intervals
- **Race Time Value Betting**: Compare race time prices with industry averages to find value bets
- **Market Analysis**: Analyze bookmaker pricing patterns for strategic betting decisions

## Best Practices
1. **Monitor Multiple Timeframes**: Use both 1-hour and 10-minute differences for comprehensive analysis
2. **Set Appropriate Ranges**: Define realistic minimum and maximum difference ranges based on market conditions
3. **Consider Race Conditions**: Factor in number of runners and horse ages for better selection criteria
4. **Risk Management**: Set appropriate stake sizes based on confidence levels in price differences
5. **Regular Calibration**: Periodically review and adjust difference thresholds based on market performance

## Notes
- This strategy requires access to bookmaker price data for comparison
- Price differences can change rapidly, so real-time data is essential
- Consider using this strategy in combination with other analysis tools for enhanced decision making
- Monitor bookmaker terms and conditions as they may affect betting strategies
