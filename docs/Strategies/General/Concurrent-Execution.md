# Concurrent Execution Strategy

## Overview
This strategy executes multiple strategies simultaneously, with options to control termination behavior when any strategy completes.

## Category
General Strategy

## Description
A parallel execution strategy that runs multiple strategies at the same time. Unlike sequential execution, all strategies start simultaneously and can complete independently, with optional coordination for early termination.

## Parameters

### Strategy
- **StrategyNames** (Required): Enter strategy names separated by semicolons (e.g., 'strategy 1;strategy 2')
- **EndExecutionIfAnyBotEnds** (Optional): End execution of all strategies when any one completes

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
- **Parallel Execution**: All strategies run simultaneously
- **Independent Operation**: Strategies work independently
- **Coordinated Termination**: Optional early termination control
- **Resource Efficiency**: Maximum utilization of market opportunities

## Execution Flow
1. **Start All Strategies**: Launch all strategies simultaneously
2. **Parallel Monitoring**: Monitor all strategies concurrently
3. **Independent Completion**: Strategies complete on their own timeline
4. **Optional Termination**: End all if any completes (if enabled)
5. **Final Completion**: All strategies finished or terminated

## Termination Modes
- **Independent**: Each strategy runs to its own completion
- **First Completes**: Stop all when first strategy finishes
- **Coordinated**: Shared termination trigger

## Use Cases
- Multi-strategy market coverage
- Diversified trading approaches
- Risk management with multiple monitors
- Opportunity maximization
- Parallel arbitrage strategies

## Examples
- **Market Making**: 'Back Strategy;Lay Strategy;Spread Monitor'
- **Risk Management**: 'Main Strategy;Stop Loss;Profit Taking'
- **Diversification**: 'Scalp Strategy;Swing Strategy;Hedge Strategy'
- **Coverage**: 'Strategy A;Strategy B;Strategy C' on different selections

## Benefits
- **Time Efficiency**: Strategies run in parallel
- **Opportunity Capture**: Multiple strategies working simultaneously
- **Diversification**: Different approaches at same time
- **Flexibility**: Independent or coordinated operation

## Resource Considerations
- **System Load**: Multiple strategies require more resources
- **Market Impact**: Multiple strategies may affect same market
- **Position Tracking**: Monitor combined exposure
- **Execution Conflicts**: Strategies may compete for same opportunities

## Risk Management
- Monitor total exposure across all strategies
- Consider strategy interactions and conflicts
- Set appropriate position limits
- Plan for coordinated exit scenarios

## Advanced Applications
- Portfolio-style strategy execution
- Multi-timeframe trading systems
- Comprehensive market coverage
- Systematic trading with backup strategies
