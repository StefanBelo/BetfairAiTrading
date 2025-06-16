# Ladbrokes In-Running SP Bot

## Overview
A specialized in-running strategy that combines Ladbrokes price analysis with Betfair Starting Price (SP) data to execute bets during live races, focusing on value opportunities that emerge during race progression.

## Strategy ID
3010

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
- **BSPWinDifferenceMin** (Double, Optional): Minimum Betfair SP win difference
- **BSPWinDifferenceMax** (Double, Optional): Maximum Betfair SP win difference
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

### Bet Attribute Parameters
- **OfferMyBet** (Boolean, Optional): Set to 'True' to place your bet on the opposite side. If you're backing, your bet will be offered at the best lay odds

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
  "Ladbrokes1hourDifferenceMin": -0.8,
  "Ladbrokes1hourDifferenceMax": 0.8,
  "UseBetfairRaceTimeWindifference": true,
  "BetfairRaceTimeWindifferenceMin": 1.0,
  "BetfairRaceTimeWindifferenceMax": 4.0,
  "BSPWinDifferenceMin": -2.0,
  "BSPWinDifferenceMax": 2.0,
  "MinimumPercentageTotalMatched": 40.0,
  "MaximumPercentageTotalMatched": 85.0,
  "OfferMyBet": false,
  "Stake": 30.0,
  "StrategyReference": "LadbrokesInSP"
}
```

## Use Cases
- **SP Value Trading**: Identify value opportunities relative to Betfair Starting Prices
- **In-Running SP Arbitrage**: Exploit differences between live prices and SP projections
- **Cross-Platform SP Analysis**: Compare Ladbrokes prices with Betfair SP data
- **Pre-Race vs In-Running**: Analyze how pre-race prices compare to in-running and SP
- **Market Efficiency Analysis**: Identify market inefficiencies using SP data

## SP-Based Strategies
1. **SP Value Hunting**: Target selections trading above their likely SP
2. **SP Convergence Trading**: Trade on price convergence towards SP levels
3. **SP Deviation Analysis**: Identify significant deviations from expected SP
4. **Cross-Platform SP Comparison**: Compare SP estimates across platforms
5. **Historical SP Analysis**: Use historical SP data for value assessment

## Best Practices
1. **SP Understanding**: Understand how Betfair SP is calculated and influenced
2. **Time Analysis**: Monitor how prices move relative to SP throughout the betting period
3. **Volume Consideration**: Factor in total matched amounts when assessing SP value
4. **Cross-Platform Validation**: Use both Ladbrokes and Betfair data for SP analysis
5. **Risk Assessment**: Consider SP volatility and its impact on strategy performance

## SP Trading Considerations
- **SP Calculation**: Betfair SP is calculated based on unmatched bets at race start
- **SP Volatility**: SP can be influenced by large bets placed close to race start
- **Market Liquidity**: SP accuracy depends on market liquidity and participation
- **Time Sensitivity**: SP estimates can change rapidly as race start approaches
- **Platform Differences**: Different platforms may have varying SP calculation methods

## Advanced Features
- **Offer My Bet**: Option to place bets on the opposite side for better prices
- **BSP Win Difference**: Specific analysis of Betfair SP win differences
- **Multi-Timeframe Analysis**: Combine different time intervals for comprehensive analysis
- **Liquidity Filtering**: Consider market liquidity when making SP-based decisions

## Risk Management
- **SP Deviation Risk**: Large SP deviations can indicate market uncertainty
- **Timing Risk**: SP calculations can change rapidly near race start
- **Platform Risk**: Differences in SP calculation methods across platforms
- **Liquidity Risk**: Low liquidity can lead to inaccurate SP calculations
- **Market Risk**: In-running markets can be highly volatile

## Notes
- Requires access to real-time SP data and calculations
- SP-based strategies are most effective in liquid markets
- Consider regulatory differences in SP calculation across jurisdictions
- Monitor SP accuracy and adjust strategy parameters based on performance
- Use appropriate risk management given the complexity of SP-based trading
