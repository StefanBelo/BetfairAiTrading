# Tennis Strategy

**Category:** Tennis  
**Strategy ID:** 21

## Description

A comprehensive tennis betting strategy that triggers actions based on live tennis match data including points, games, and sets scores. This strategy can execute on specific players and uses real-time score updates to make intelligent betting decisions.

## Parameters

### Strategy
- **StrategyName** (Required) - Enter the name of the strategy you wish to execute

### Selection
- **ExecuteOnPlayer** (Optional) - Execute the action bot on the player
  - Options: Player1, Player2, WinningPlayer, LosingPlayer, ServingPlayer, ReceivingPlayer

### Trigger
- **PointsScore** (Required) - This is the mandatory points score that will trigger the action bot you've set with the 'StrategyName' parameter
- **GamesScore** (Optional) - Set this games score to trigger the action bot specified by the StrategyName parameter
- **SetsScore** (Optional) - Set this sets score to trigger the action bot specified by the StrategyName parameter
- **PreviousSetIndex** (Optional) - Specify the previous set's index (e.g., '1' for the first set). Set to '0' if not applicable (Byte)
- **PreviousSetGamesScore** (Optional) - The previous set games score to trigger the action bot defined by 'StrategyName'

### Miscellaneous
- **ScoreUpdateInterval** (Optional) - Set the score update interval in seconds (Double)
- **UseWebApi** (Optional) - Use the Betfair Web API to update live score data (Boolean)
- **ShowScore** (Optional) - Show the score in the output view for diagnostic purposes (Boolean)
- **StrategyReference** (Optional) - Set this parameter to specify which strategy sent the bet. The string is limited to 15 characters

### Market
- **EvaluateEntryCriteriaOnlyOnce** (Optional) - Set this parameter to True to evaluate the entry criteria only once
- **StopMarketMonitoring** (Optional) - Set this parameter to True to stop market monitoring after the strategy completes execution. Monitoring will stop only if no other bot is active in the market

## Usage Examples

### Service Break Strategy
- **ExecuteOnPlayer**: `ServingPlayer`
- **PointsScore**: `"0-40"`
- **StrategyName**: `"Place Bet"`

### Set Winner Betting
- **ExecuteOnPlayer**: `WinningPlayer`
- **GamesScore**: `"6-4"`
- **SetsScore**: `"1-0"`

### Critical Points Trading
- **ExecuteOnPlayer**: `LosingPlayer`
- **PointsScore**: `"30-40"`
- **UseWebApi**: `true`
- **ScoreUpdateInterval**: `2.0`

### Match Momentum
- **ExecuteOnPlayer**: `Player1`
- **PreviousSetGamesScore**: `"6-3"`
- **PreviousSetIndex**: `1`
- **GamesScore**: `"0-2"`

## Best Practices

1. Use precise score formatting for accurate triggering
2. Enable live score updates for real-time accuracy
3. Consider player-specific strategies (serving vs receiving)
4. Monitor break points and critical game situations
5. Use appropriate score update intervals for responsive trading
6. Combine multiple score criteria for sophisticated strategies
