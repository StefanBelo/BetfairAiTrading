# Clear Markers

**Category:** Data  
**Strategy ID:** 1000

## Description

A utility strategy that clears visual markers and indicators from the trading interface. This strategy is essential for maintaining a clean workspace and resetting visual elements before starting new analysis or trading sessions.

## Parameters

### Market
- **EvaluateEntryCriteriaOnlyOnce** (Optional) - Set this parameter to True to evaluate the entry criteria only once
- **StopMarketMonitoring** (Optional) - Set this parameter to True to stop market monitoring after the strategy completes execution. Monitoring will stop only if no other bot is active in the market

### Miscellaneous
- **StrategyReference** (Optional) - Set this parameter to specify which strategy sent the bet. The string is limited to 15 characters

## Usage Examples

### Clean Workspace
- **EvaluateEntryCriteriaOnlyOnce**: `true`

### Reset Before Analysis
- **StrategyReference**: `"Reset"`

## Best Practices

1. Use before starting new analysis sessions
2. Execute when switching between different strategies
3. Clear markers before important trading decisions
4. Regular cleanup for better interface visibility
