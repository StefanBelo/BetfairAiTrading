# Price Move Bot

## Overview
An advanced price movement analysis strategy that monitors price changes across multiple bookmakers and betting exchanges to identify profitable trading opportunities based on price volatility and market dynamics.

## Strategy ID
3005

## Category
Custom

## Parameters

### Trigger Parameters
- **UseLadbrokesOpenDifference** (Boolean, Optional): Use Ladbrokes open difference
- **LadbrokesOpenDifferenceMin** (Double, Optional): Minimum Ladbrokes open difference
- **LadbrokesOpenDifferenceMax** (Double, Optional): Maximum Ladbrokes open difference
- **UseBetfairOpenDifference** (Boolean, Optional): Use Betfair open difference
- **BetfairOpenDifferenceMin** (Double, Optional): Minimum Betfair open difference
- **BetfairOpenDifferenceMax** (Double, Optional): Maximum Betfair open difference
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
  "UseLadbrokesOpenDifference": true,
  "LadbrokesOpenDifferenceMin": -2.0,
  "LadbrokesOpenDifferenceMax": 2.0,
  "UseBetfairOpenDifference": true,
  "BetfairOpenDifferenceMin": -1.5,
  "BetfairOpenDifferenceMax": 1.5,
  "UseLadbrokes1hourDifference": true,
  "Ladbrokes1hourDifferenceMin": -1.0,
  "Ladbrokes1hourDifferenceMax": 1.0,
  "MinimumRunners": 6,
  "MaximumRunners": 14,
  "Stake": 20.0,
  "StrategyReference": "PriceMoveBot"
}
```

## Use Cases
- **Price Movement Analysis**: Track and analyze price movements across multiple platforms
- **Opening Price Arbitrage**: Identify discrepancies in opening prices between bookmakers and exchanges
- **Time-Based Price Tracking**: Monitor price changes over different time intervals
- **Multi-Platform Trading**: Execute trades based on price movements across platforms
- **Volatility Trading**: Capitalize on market volatility and price swings

## Best Practices
1. **Multi-Timeframe Analysis**: Use opening, 1-hour, and 10-minute differences for comprehensive analysis
2. **Cross-Platform Monitoring**: Track price movements on both Ladbrokes and Betfair
3. **Threshold Setting**: Set appropriate minimum and maximum difference ranges
4. **Race Condition Filtering**: Apply runner and age filters for better market selection
5. **Quick Execution**: Price movements can be rapid, so ensure fast execution capabilities

## Trading Strategies
- **Momentum Trading**: Follow price movements in the direction of the trend
- **Reversal Trading**: Bet against extreme price movements expecting a reversal
- **Arbitrage Trading**: Exploit price differences between platforms
- **Volatility Trading**: Benefit from increased price volatility

## Notes
- Requires real-time price feeds from multiple sources
- Price movements can be very rapid, requiring automated execution
- Market conditions and liquidity affect strategy performance
- Monitor platform-specific terms and conditions
- Consider slippage and execution delays in strategy implementation
