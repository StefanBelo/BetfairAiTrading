# Execute Strategies Strategy

## Overview
This strategy executes multiple strategies simultaneously on the same or different selections, with optional ladder parameter integration.

## Category
General Strategy

## Description
A sophisticated strategy executor that can run multiple strategies at the same time. It provides the ability to execute several different strategies concurrently and optionally use ladder parameters to dynamically update strategy settings.

## Parameters

### Strategy
- **StrategyNames** (Required): Enter strategy names separated by semicolons (e.g., 'strategy 1;strategy 2')
- **UseLadderParameters** (Optional): Use ladder parameters (BetType, Price, Stake) to update action bots

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
- **Multiple Strategy Execution**: Run several strategies simultaneously
- **Ladder Integration**: Dynamic parameter updates from ladder interface
- **Concurrent Operations**: All strategies run in parallel
- **Flexible Configuration**: Each strategy maintains its own settings

## Strategy Name Format
- **Multiple Strategies**: Separate with semicolons (;)
- **Example**: 'Place Bet;Close Position;Trailing Stop'
- **Order**: Strategies execute in the order listed

## Ladder Parameters
When **UseLadderParameters** is enabled:
- **BetType**: Dynamically set bet type from ladder
- **Price**: Use current ladder price selection
- **Stake**: Use ladder stake settings

## Use Cases
- Complex trading strategies with multiple components
- Risk management with simultaneous position monitoring
- Multi-strategy approaches (entry + exit + stop loss)
- Portfolio management across multiple strategies
- Systematic strategy combinations

## Examples
- Execute "Place Bet" and "Trailing Stop Loss" together
- Run "Open Position", "Close Position", and "Risk Management"
- Combine "Entry Strategy" with "Exit Strategy" and "Stop Loss"
- Multi-strategy arbitrage execution

## Strategy Coordination
- All strategies share market monitoring
- Individual strategy completion tracked
- Market version changes affect all strategies
- Shared reference for tracking purposes

## Benefits
- **Efficiency**: Single strategy manages multiple operations
- **Coordination**: Strategies work together seamlessly
- **Simplification**: One configuration for multiple strategies
- **Integration**: Ladder interface integration available

## Risk Considerations
- Monitor combined strategy exposure
- Ensure strategies don't conflict
- Consider execution order dependencies
- Manage total position sizing
