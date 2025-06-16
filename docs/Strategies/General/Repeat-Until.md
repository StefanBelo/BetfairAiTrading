# Repeat Until Strategy

## Overview
This strategy repeatedly executes another strategy until specified conditions are met, such as number of iterations, total stake reached, or profit/loss targets achieved.

## Category
General Strategy

## Description
A flexible looping strategy that continues executing a specified strategy based on various termination criteria. It provides comprehensive control over repetition conditions including iterations, stakes, profits, losses, and time limits.

## Parameters

### Strategy
- **StrategyName** (Required): Enter the name of the strategy you wish to execute repeatedly
- **RepeatUntilParameter** (Optional): Set condition type - NumberOfIterations, TotalStake, ProfitTarget, or LossTarget
- **TargetValue** (Required): Target value corresponding to the RepeatUntilParameter
- **NextIterationTimeout** (Optional): Delay between iterations

### Time
- **StopTimeSpan** (Optional): Stop operations at specific time relative to event start

### Selection
- **SortSelectionsBy** (Optional): Sort selections by LastPriceTraded, TotalMatched, or DoNotSort
- **ExecuteOnSelection** (Optional): Specify which selection to execute on

### Market
- **StopBotExecutionOnMarketVersionChange** (Optional): Stop strategy when market version changes
- **EvaluateEntryCriteriaOnlyOnce** (Optional): Evaluate entry criteria only once
- **StopMarketMonitoring** (Optional): Stop market monitoring after strategy completion

### Miscellaneous
- **StrategyReference** (Optional): Custom reference string for the strategy (max 15 characters)

## Key Features
- **Multiple Termination Criteria**: Various ways to control repetition
- **Flexible Timing**: Delays between iterations and time-based stops
- **Target Tracking**: Monitor progress toward termination conditions
- **Time Safety**: Automatic stopping at specified times

## Repeat Until Parameters
1. **NumberOfIterations**: Execute strategy X number of times
2. **TotalStake**: Continue until total stake amount reached
3. **ProfitTarget**: Repeat until profit target achieved
4. **LossTarget**: Stop if loss target reached

## Execution Flow
1. **Execute Strategy**: Run the specified strategy
2. **Check Conditions**: Evaluate termination criteria
3. **Wait Period**: Optional delay before next iteration
4. **Check Time**: Verify time limits not exceeded
5. **Repeat/Stop**: Continue or terminate based on conditions

## Use Cases
- Fixed number of betting iterations
- Budget-based betting (total stake control)
- Profit-seeking strategies
- Loss limitation strategies
- Time-controlled sessions

## Examples
- **10 Iterations**: Execute "Place Bet" strategy 10 times
- **£1000 Budget**: Continue until £1000 total stake reached
- **£200 Profit**: Repeat until £200 profit achieved
- **£100 Loss Limit**: Stop if £100 loss incurred
- **Pre-Race Session**: Stop 5 minutes before event start

## Timing Controls
- **NextIterationTimeout**: Pause between executions
- **StopTimeSpan**: Hard time limit for session
- **Event-Relative**: Times calculated from event start

## Budget Management
- **Total Stake**: Control maximum investment
- **Progressive Staking**: Stakes can increase with iterations
- **Risk Control**: Loss targets provide protection

## Benefits
- **Systematic Execution**: Controlled repetition
- **Multiple Exit Criteria**: Flexible termination conditions
- **Time Management**: Session duration control
- **Risk Control**: Built-in loss protection

## Risk Considerations
- Monitor cumulative exposure
- Set appropriate loss targets
- Consider market changes over time
- Plan for various termination scenarios

## Advanced Applications
- **Session Management**: Control trading session length
- **Progressive Strategies**: Increase stakes over iterations
- **Market Coverage**: Systematic market participation
- **Risk-Controlled Automation**: Automated trading with limits
