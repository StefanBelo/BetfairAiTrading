# Football Strategy: Match Time Criteria

This document describes how to configure and use a football trading strategy that depends on the correct match time and other match parameters in Bfexplorer.

To test this strategy on the Betfair Exchange using Bfexplorer app, copy the following URI [Open Bfexplorer](bfexplorer://testStrategy?fileName=FootballMatchTimeCriteria.json) and open it in your web broswer.

```
bfexplorer://testStrategy?fileName=FootballMatchTimeCriteria.json
```

![Bfexplorer running Open my Markets by Score!](/docs/Strategies/Football/images/OpenMyMarketsByScore.png "Bfexplorer running Open my Markets by Score")

## Overview

When using the **Football Strategy** settings, you can enable the `ShowMatchCriteria` parameter. When set, the strategy will report match information in the following format:

```
MatchTime: 52, Score: 0 - 3, Goals: 3, ScoreDifference: -3, HomeNumberOfRedCards: 1, AwayNumberOfRedCards: 0, CornersDifference: -3, Status: SecondHalfKickOff
```

## Available Match Criteria Parameters

You can use any of the following parameters in your criteria settings:

- `MatchTime` (integer, minutes)
- `Score` (home - away)
- `Goals` (total goals)
- `ScoreDifference` (home minus away)
- `HomeNumberOfRedCards`
- `AwayNumberOfRedCards`
- `CornersDifference` (home minus away)
- `Status` (e.g., FirstHalf, SecondHalfKickOff, etc.)

## Example: Triggering Strategy Execution

To trigger the strategy execution for a specific scenario, set the `StartCriteria` using the available parameters. For example:

```
[Status] = 'SecondHalfKickOff' And [MatchTime] = 45
```

This will execute the strategy when the match status is `SecondHalfKickOff` and the match time is 45 minutes.

## Usage Steps

1. In the Football Strategy settings, enable `ShowMatchCriteria` to view match parameters.
2. Define your `StartCriteria` using any combination of the available parameters.
3. Assign your desired `StrategyName` to execute when the criteria are met.

## Notes
- Criteria can be combined using logical operators (`And`, `Or`).
- All parameters are updated live as the match progresses.
- Make sure your criteria match the expected parameter values for correct triggering.