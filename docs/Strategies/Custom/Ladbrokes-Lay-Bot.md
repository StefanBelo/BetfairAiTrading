# Ladbrokes Lay Bot

## Overview
A specialized laying strategy that targets selections on Ladbrokes based on price movements, market analysis, and various betting criteria to identify profitable laying opportunities.

## Strategy ID
3006

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
  "Ladbrokes1hourDifferenceMin": 0.5,
  "Ladbrokes1hourDifferenceMax": 3.0,
  "UseBetfairRaceTimeWindifference": true,
  "BetfairRaceTimeWindifferenceMin": -2.0,
  "BetfairRaceTimeWindifferenceMax": -0.5,
  "MinimumRank": 1,
  "MaximumRank": 4,
  "MinimumPercentageTotalMatched": 10.0,
  "MaximumPercentageTotalMatched": 80.0,
  "MinimumRunners": 8,
  "MaximumRunners": 16,
  "Stake": 50.0,
  "StrategyReference": "LadbrokesLay"
}
```

## Use Cases
- **Overvalued Selection Laying**: Identify and lay selections that appear overvalued
- **Price Movement Analysis**: Lay selections based on negative price movements
- **Rank-Based Laying**: Target favorites or specific rank ranges for laying
- **Liquidity-Based Strategy**: Consider market liquidity when selecting laying opportunities
- **Cross-Platform Analysis**: Compare Ladbrokes and Betfair data for laying decisions

## Laying Strategies
1. **Favorite Laying**: Target short-priced favorites with poor recent form
2. **Price Drift Laying**: Lay selections showing significant price increases
3. **Market Inefficiency**: Exploit price differences between platforms
4. **Rank-Based Laying**: Focus on specific rank ranges with historical poor performance
5. **Liquidity Analysis**: Target selections with appropriate liquidity levels

## Best Practices
1. **Risk Management**: Set appropriate stake sizes considering liability exposure
2. **Price Movement Monitoring**: Track price changes across different timeframes
3. **Rank Analysis**: Consider the selection's market ranking and historical performance
4. **Liquidity Assessment**: Ensure sufficient market liquidity for laying
5. **Cross-Platform Validation**: Use both Ladbrokes and Betfair data for confirmation

## Risk Considerations
- **Liability Management**: Laying involves potentially unlimited liability
- **Price Movements**: Quick price movements can affect profitability
- **Market Liquidity**: Insufficient liquidity can impact position management
- **Selection Performance**: Strong favorites can still win despite being overvalued

## Notes
- Laying strategies require careful risk management due to liability exposure
- Monitor both platforms for price discrepancies and opportunities
- Consider using stop-loss mechanisms to limit losses
- Track performance across different market conditions and race types
