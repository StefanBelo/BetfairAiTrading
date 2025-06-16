# Execute Till Target Profit Strategy

## Overview
This strategy repeatedly executes another strategy until a target profit or loss is achieved, with optional staking plan management and Martingale progression.

## Category
General Strategy

## Description
A systematic profit-targeting strategy that continues executing a specified strategy until predetermined profit or loss targets are reached. It includes advanced features like staking plan resets, Martingale progression, and time-based controls.

## Parameters

### Strategy
- **StrategyName** (Required): Enter the name of the strategy you wish to execute repeatedly

### Profit/Loss
- **TargetProfit** (Required): Continue running strategy until this profit is achieved
- **TargetLoss** (Optional): Stop running strategy if this loss is incurred
- **ResetStakingPlan** (Optional): Reset staking plan after losses (number of losses)
- **MartingaleStakeFactor** (Optional): Increase stake by previous losses using this multiplier

### Time
- **ExecuteAtTime** (Optional): Execute strategy at specific time relative to event start
- **StopExecutionAtTime** (Optional): Stop if betting result not confirmed by this time

### Selection
- **SortSelectionsBy** (Optional): Sort selections by LastPriceTraded, TotalMatched, or DoNotSort
- **ExecuteOnSelection** (Optional): Specify which selection to execute on

### Market
- **StopBotExecutionOnMarketVersionChange** (Optional): Stop strategy when market version changes
- **EvaluateEntryCriteriaOnlyOnce** (Optional): Evaluate entry criteria only once
- **StopMarketMonitoring** (Optional): Stop market monitoring after strategy completion

### Miscellaneous
- **StrategyID** (Optional): Unique ID for strategies running simultaneously
- **UseShowResultSummary** (Optional): Show result summary in output for diagnostics
- **StrategyReference** (Optional): Custom reference string for the strategy (max 15 characters)

## Key Features
- **Target-Based Execution**: Run until profit/loss targets met
- **Staking Management**: Martingale and reset options
- **Time Controls**: Execution timing and timeouts
- **Result Tracking**: Comprehensive profit/loss monitoring
- **Multiple Instances**: Support for parallel execution with unique IDs

## Staking Plan Features
- **Martingale Progression**: Increase stakes after losses
- **Reset Mechanism**: Reset to base stake after specified losses
- **Progressive Recovery**: Systematic loss recovery approach

## Execution Cycle
1. **Execute Strategy**: Run the specified strategy
2. **Check Results**: Evaluate profit/loss outcome
3. **Update Stakes**: Apply Martingale or reset if needed
4. **Check Targets**: Compare against profit/loss targets
5. **Continue/Stop**: Repeat until targets met or time expires

## Use Cases
- Systematic profit generation
- Loss recovery strategies
- Martingale betting systems
- Target-based trading
- Automated session management

## Examples
- Execute "Place Bet" until £100 profit achieved
- Run "Scalp Strategy" with Martingale until target reached
- Systematic betting with £500 profit target and £200 loss limit
- Daily trading session with specific profit goals

## Martingale Example
- **Base Stake**: £10
- **Target Profit**: £100
- **Martingale Factor**: 2.0
- **Loss Sequence**: £10 loss → £20 stake → £20 loss → £40 stake
- **Win**: £40 win recovers previous losses plus profit

## Time Management
- **ExecuteAtTime**: Start strategy at specific event time
- **StopExecutionAtTime**: Safety timeout for completion
- **Session Control**: Manage trading session duration

## Risk Considerations
- Martingale can lead to large stakes
- Target loss provides essential protection
- Time limits prevent indefinite execution
- Monitor total exposure carefully

## Benefits
- **Systematic Approach**: Consistent profit targeting
- **Loss Recovery**: Martingale helps recover losses
- **Flexibility**: Configurable targets and timing
- **Automation**: Hands-off profit generation
