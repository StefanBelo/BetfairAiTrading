# Sequence Execution Strategy

## Overview
This strategy executes multiple strategies in sequential order, waiting for each strategy to complete before starting the next one.

## Category
General Strategy

## Description
A workflow management strategy that executes multiple strategies in a predetermined sequence. Each strategy must complete before the next one begins, ensuring ordered execution and proper workflow control.

## Parameters

### Strategy
- **StrategyNames** (Required): Enter strategy names separated by semicolons (e.g., 'strategy 1;strategy 2')

### Data
- **ShareBetPosition** (Optional): Use shared bet position data across strategies

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
- **Sequential Execution**: Strategies run one after another
- **Completion Waiting**: Each strategy must finish before next starts
- **Shared Data**: Optional bet position sharing between strategies
- **Workflow Control**: Managed execution order

## Execution Flow
1. **Start First Strategy**: Execute first strategy in the list
2. **Wait for Completion**: Monitor until strategy finishes
3. **Start Next Strategy**: Begin second strategy
4. **Continue Sequence**: Repeat until all strategies complete
5. **Final Completion**: Sequence finished when last strategy completes

## Strategy Name Format
- **Sequential Order**: Separate with semicolons (;)
- **Example**: 'Open Position;Monitor Position;Close Position'
- **Execution**: Runs in exact order specified

## Use Cases
- Multi-phase trading strategies
- Risk management workflows
- Step-by-step position management
- Complex trading sequences
- Systematic strategy execution

## Examples
- **Trading Sequence**: 'Place Bet;Monitor Position;Close Position'
- **Risk Management**: 'Open Position;Set Stop Loss;Trail Profits'
- **Market Making**: 'Place Back;Place Lay;Monitor Spread'
- **Arbitrage**: 'Find Opportunity;Place Bets;Monitor Outcome'

## Shared Bet Position
When **ShareBetPosition** is enabled:
- Position data passed between strategies
- Cumulative position tracking
- Coordinated risk management
- Unified position monitoring

## Benefits
- **Controlled Workflow**: Predictable execution order
- **Resource Management**: One strategy at a time
- **Data Continuity**: Shared position information
- **Clear Dependencies**: Each step builds on previous

## Timing Considerations
- Total execution time = sum of individual strategy times
- Strategies cannot run in parallel
- Market conditions may change during sequence
- Consider timeout scenarios

## Error Handling
- Failed strategy may stop entire sequence
- Consider alternative completion criteria
- Monitor individual strategy success
- Plan for partial completion scenarios
