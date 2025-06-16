# Execute on Selections Strategy

## Overview
This strategy executes another specified strategy on multiple selections within the same market.

## Category
General Strategy

## Description
A powerful wrapper strategy that allows you to execute any other strategy across multiple selections in a market. You can specify exactly which selections to target or run the strategy on all selections.

## Parameters

### Strategy
- **StrategyName** (Required): Enter the name of the strategy you wish to execute

### Selection
- **SortSelectionsBy** (Optional): Sort selections by LastPriceTraded, TotalMatched, or DoNotSort
- **OnSelections** (Optional): Specify selections to execute on (e.g., '1,2,4'). If empty, runs on all selections

### Market
- **EvaluateEntryCriteriaOnlyOnce** (Optional): Evaluate entry criteria only once
- **StopMarketMonitoring** (Optional): Stop market monitoring after strategy completion

### Miscellaneous
- **StrategyReference** (Optional): Custom reference string for the strategy (max 15 characters)

## Key Features
- **Multi-Selection Execution**: Run strategies on multiple selections simultaneously
- **Flexible Selection**: Choose specific selections or all selections
- **Strategy Wrapper**: Can execute any other strategy
- **Sorting Options**: Control selection order

## Selection Specification
- **Specific Selections**: Use format '1,2,4' for selections 1, 2, and 4
- **All Selections**: Leave OnSelections empty to target all
- **Sorted Order**: Selections chosen based on SortSelectionsBy parameter

## Use Cases
- Dutching strategies across multiple selections
- Risk management across portfolio of bets
- Systematic trading on multiple runners
- Closing positions on specific selections
- Multi-selection arbitrage opportunities

## Examples
- Execute "Place Bet" strategy on selections 1, 3, and 5
- Run "Close Position" strategy on all selections
- Apply "Tick Offset" strategy to top 3 favorites
- Execute custom strategy on specific horses

## Selection Sorting
- **LastPriceTraded**: Selections ordered by current odds
- **TotalMatched**: Selections ordered by volume traded
- **DoNotSort**: Use natural market order

## Strategy Combinations
- Combine with time-based triggers
- Use with conditional logic strategies
- Integrate with market monitoring tools
- Part of larger trading systems

## Risk Management
- Monitor total exposure across selections
- Consider market correlation between selections
- Set appropriate stakes for each selection
- Implement position limits
