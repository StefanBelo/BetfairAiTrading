# Trading Data Recorder

**Category:** Data  
**Strategy ID:** 1002

## Description

A comprehensive data recording strategy that captures detailed trading information for later analysis. This strategy records market movements, price changes, volume data, and trading activity to build historical databases for strategy development and backtesting.

## Parameters

### Market
- **EvaluateEntryCriteriaOnlyOnce** (Optional) - Set this parameter to True to evaluate the entry criteria only once
- **StopMarketMonitoring** (Optional) - Set this parameter to True to stop market monitoring after the strategy completes execution. Monitoring will stop only if no other bot is active in the market

### Miscellaneous
- **StrategyReference** (Optional) - Set this parameter to specify which strategy sent the bet. The string is limited to 15 characters

## Usage Examples

### Continuous Recording
- **EvaluateEntryCriteriaOnlyOnce**: `false`
- **StopMarketMonitoring**: `false`

### Session-Based Recording
- **EvaluateEntryCriteriaOnlyOnce**: `true`
- **StrategyReference**: `"TradingRec"`

## Best Practices

1. **Storage Planning**: Ensure adequate disk space for continuous recording
2. **Data Quality**: Regular validation of recorded data integrity
3. **Backup Strategy**: Implement regular backups of trading data
4. **Performance**: Monitor system performance during intensive recording
5. **Analysis Preparation**: Structure data for easy analysis and backtesting
