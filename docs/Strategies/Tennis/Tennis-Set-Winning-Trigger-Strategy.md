# Tennis Set Winning Trigger Strategy

## Overview
A specialized tennis trading strategy that triggers betting actions based on set outcomes, player rankings, and match dynamics to capitalize on set-based momentum shifts.

## Strategy ID
6910

## Category
Tennis

## Parameters

### Selection Parameters
- **ExecuteOnSelection** (Enum, Optional): The selection on which you want to execute your bot
  - Values: WinningFavourite, LosingFavourite, WinningUnderdog, LosingUnderdog, Winner, Loser

### Strategy Parameters
- **StrategyName** (String, Optional): Enter the name of the strategy you wish to execute
- **TradingStrategy** (Enum, Optional): Set the trading strategy type: Back or Lay trading session
  - Values: Back, Lay

### Trigger Parameters
- **TradeOnMalePlayer** (Boolean, Optional): Set to True if you want to start trading session only for male player match
- **MinimalAtpRankingDifference** (Int32, Optional): Set the minimal ATP ranking difference of players
- **MaximalPerformanceDifference** (Double, Optional): Set the maximal performance difference
- **StopIfAlreadyInPlay** (Boolean, Optional): Stop the strategy execution if the market is already at in-play

### Miscellaneous Parameters
- **ScoreUpdateInterval** (Double, Optional): The score update interval in seconds
- **ShowScore** (Boolean, Optional): Show score in the output view for diagnostic purposes

### Market Parameters
- **EvaluateEntryCriteriaOnlyOnce** (Boolean, Optional): Set this parameter to True to evaluate the entry criteria only once
- **StopMarketMonitoring** (Boolean, Optional): Set this parameter to True to stop market monitoring after the strategy completes execution. Monitoring will stop only if no other bot is active in the market

### Miscellaneous Parameters
- **StrategyReference** (String, Optional): Set this parameter to specify which strategy sent the bet. The string is limited to 15 characters

## Usage Example
```json
{
  "ExecuteOnSelection": "WinningUnderdog",
  "StrategyName": "Back Underdog Winner",
  "TradingStrategy": "Back",
  "TradeOnMalePlayer": true,
  "MinimalAtpRankingDifference": 20,
  "MaximalPerformanceDifference": 0.15,
  "StopIfAlreadyInPlay": false,
  "ScoreUpdateInterval": 30.0,
  "ShowScore": true,
  "StrategyReference": "TennisSetWin"
}
```

## Use Cases
- **Set Momentum Trading**: Trade based on set-winning momentum
- **Ranking-Based Analysis**: Use ATP rankings to identify trading opportunities
- **Underdog Value**: Target underdog players who win sets
- **Performance-Based Trading**: Trade based on recent performance differences

## Set-Based Trading Strategies
1. **Momentum Trading**: Back players who have won the previous set
2. **Favourite Correction**: Lay favourites who lose sets to underdogs
3. **Underdog Value**: Back underdogs who demonstrate strong set-winning ability
4. **Performance Analysis**: Trade based on recent performance indicators

## Tennis-Specific Considerations
- **Set Importance**: Different sets have varying psychological impact
- **Surface Differences**: Player performance varies by court surface
- **ATP Rankings**: Use ranking differences to assess relative strength
- **Match Format**: Consider match format (best of 3 vs best of 5 sets)
- **Player Form**: Recent performance affects set-winning probability

## Best Practices
1. **Ranking Analysis**: Use ATP ranking differences for better selection
2. **Performance Monitoring**: Track recent performance trends
3. **Match Context**: Consider tournament importance and player motivation
4. **Real-Time Updates**: Use frequent score updates for accurate analysis
5. **Gender Specificity**: Focus on male or female matches based on expertise

## Advanced Features
- **Dynamic Selection**: Choose winners/losers dynamically based on set outcomes
- **Performance Filtering**: Filter matches based on performance differences
- **Ranking Thresholds**: Set minimum ranking differences for strategy activation
- **Real-Time Scoring**: Continuous score monitoring for immediate reaction

## Risk Management
- **In-Play Timing**: Be aware of when markets turn in-play
- **Score Accuracy**: Ensure accurate and timely score updates
- **Market Volatility**: Tennis markets can be highly volatile during sets
- **Player Withdrawal**: Consider risk of player retirement or injury

## Notes
- Requires reliable tennis score feeds for optimal performance
- ATP ranking data should be current and accurate
- Strategy works best with liquid tennis markets
- Consider time zones and tournament schedules for optimal execution
