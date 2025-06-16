# Execute at Time Strategy

## Overview
This strategy executes another strategy at a specific time relative to the event start or from when the strategy begins.

## Category
General

## Description
A wrapper strategy that provides precise timing control for executing other strategies. It can schedule strategy execution at specific times before or after the official event start time, or after a delay from when the strategy is initiated.

## Parameters

### Strategy
- **StrategyName** (Required): Enter the name of the strategy you wish to execute

### Time
- **TimeSpanType** (Optional): Choose timing reference - 'RelativeToStartTime' or 'FromBotStart'
- **StartTimeSpan** (Optional): When to start strategy execution (e.g., -0:05:00 for 5 minutes before start)
- **StopTimeSpan** (Optional): When to stop strategy execution

### Selection
- **SortSelectionsBy** (Optional): Sort selections by LastPriceTraded, TotalMatched, or DoNotSort
- **ExecuteOnSelection** (Optional): Specify which selection to execute on

### Market
- **StopBotExecutionOnMarketVersionChange** (Optional): Stop strategy when market version changes
- **EvaluateEntryCriteriaOnlyOnce** (Optional): Evaluate entry criteria only once
- **StopMarketMonitoring** (Optional): Stop market monitoring after strategy completion

### Miscellaneous
- **UseExecuteImmediately** (Optional): Execute immediately without timing delays
- **UseExecuteOnNewMarketsOnly** (Optional): Only execute on new markets (for Market Auto Open tool)
- **UseSetMarketInactive** (Optional): Set market to inactive state after execution
- **StrategyReference** (Optional): Custom reference string for the strategy (max 15 characters)

## Key Features
- **Precise Timing**: Execute strategies at exact times
- **Flexible Timing**: Relative to event start or strategy start
- **Strategy Wrapper**: Can execute any other strategy
- **Market Control**: Options for market state management

## Timing Types
1. **RelativeToStartTime**: Time calculated from official event start
2. **FromBotStart**: Time calculated from when this strategy starts

## Time Format Examples
- **-0:05:00**: 5 minutes before event start
- **0:10:00**: 10 minutes after event start
- **-0:00:30**: 30 seconds before event start
- **1:30:00**: 1 hour 30 minutes after event start

## Use Cases
- Pre-race betting at specific times
- In-play strategies triggered at game milestones
- Closing positions at predetermined times
- News-based trading with time delays
- Risk management with time-based exits

## Examples
- Execute "Place Bet" strategy 2 minutes before race start
- Start trading strategy 10 minutes into football match
- Close all positions 5 minutes before event end
- Begin scalping strategy 30 seconds before market turns in-play

## Advanced Features
- **Immediate Execution**: Override timing for manual triggers
- **New Markets Only**: Filter for fresh markets in automated systems
- **Market State Control**: Manage market activity status
