# Betfair SP

**Category:** Data  
**Strategy ID:** 1001

## Description

A data monitoring strategy that tracks and displays Betfair Starting Price (SP) information for market selections. This strategy provides essential SP data for analysis, comparison with live prices, and post-race evaluation.

## Parameters

### Market
- **EvaluateEntryCriteriaOnlyOnce** (Optional) - Set this parameter to True to evaluate the entry criteria only once
- **StopMarketMonitoring** (Optional) - Set this parameter to True to stop market monitoring after the strategy completes execution. Monitoring will stop only if no other bot is active in the market

### Miscellaneous
- **StrategyReference** (Optional) - Set this parameter to specify which strategy sent the bet. The string is limited to 15 characters

## Usage Examples

### SP Monitoring
- **EvaluateEntryCriteriaOnlyOnce**: `true`

### Continuous SP Tracking
- **StopMarketMonitoring**: `false`

## Best Practices

1. Use for post-race analysis and strategy evaluation
2. Compare SP with pre-race prices for value assessment
3. Monitor SP trends across different market types
4. Essential for SP-based betting strategy validation
