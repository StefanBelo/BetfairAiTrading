# Football Strategy

**Category:** Football  
**Strategy ID:** 20

## Description

A comprehensive football strategy that executes betting actions based on match-specific criteria including match time, score, and score differences. This strategy allows for automated betting on football markets with flexible triggering conditions and can target associated markets like Over/Under 2.5 goals.

## Parameters

### Strategy
- **StrategyName** (Required) - Enter the name of the strategy you wish to execute
- **MarketName** (Optional) - Specify if the action bot should execute on an associated market (e.g., 'OVER_UNDER_2_5')

### Trigger
- **StartCriteria** (Optional) - Define the conditions for executing the action bot ('StrategyName'). You can use 'MatchTime', 'Score', and 'ScoreDifference' as criteria. Format scores as 'X - Y' (e.g., '2 - 1')
- **StopCriteria** (Optional) - Define the conditions that will stop the football bot's execution. You can use 'MatchTime', 'Score', and 'ScoreDifference' as criteria, with scores formatted as 'X - Y' (e.g., '2 - 1')

### Time
- **ExecutionTimeout** (Optional) - This setting allows you to postpone the execution of the strategy

### Miscellaneous
- **ShowScore** (Optional) - Show the score in the output view for diagnostic purposes
- **ShowMatchCriteria** (Optional) - Show the match criteria in the output view for diagnostic purposes
- **StrategyReference** (Optional) - Set this parameter to specify which strategy sent the bet. The string is limited to 15 characters

### Market
- **EvaluateEntryCriteriaOnlyOnce** (Optional) - Set this parameter to True to evaluate the entry criteria only once
- **StopMarketMonitoring** (Optional) - Set this parameter to True to stop market monitoring after the strategy completes execution. Monitoring will stop only if no other bot is active in the market

## Usage Examples

### Score-Based Betting
- **StartCriteria**: `Score = "1 - 0"`
- **StopCriteria**: `MatchTime >= 85`

### Time and Score Combination
- **StartCriteria**: `MatchTime >= 20 && Score = "0 - 0"`
- **MarketName**: `OVER_UNDER_2_5`

### Score Difference Trigger
- **StartCriteria**: `ScoreDifference >= 2`
- **StopCriteria**: `MatchTime >= 90`

## Best Practices

1. Use clear score formatting ('X - Y') for accurate matching
2. Combine multiple criteria for more precise triggering
3. Set appropriate stop criteria to avoid unnecessary execution
4. Use diagnostic parameters (ShowScore, ShowMatchCriteria) for testing
5. Consider associated markets for Over/Under betting strategies
