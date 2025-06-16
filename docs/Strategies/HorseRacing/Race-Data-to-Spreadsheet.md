# Race Data to Spreadsheet

**Category:** Horse Racing  
**Strategy ID:** 1011

## Description

A comprehensive data export strategy that captures horse racing market and selection data and exports it to spreadsheet format for analysis. This strategy is essential for building historical databases and conducting detailed race analysis.

## Parameters

### Market
- **EvaluateEntryCriteriaOnlyOnce** (Optional) - Set this parameter to True to evaluate the entry criteria only once
- **StopMarketMonitoring** (Optional) - Set this parameter to True to stop market monitoring after the strategy completes execution. Monitoring will stop only if no other bot is active in the market

### Miscellaneous
- **StrategyReference** (Optional) - Set this parameter to specify which strategy sent the bet. The string is limited to 15 characters

## Usage Examples

### Complete Race Export
- **EvaluateEntryCriteriaOnlyOnce**: `true`
- **StrategyReference**: `"RaceExport"`

### Continuous Monitoring
- **StopMarketMonitoring**: `false`

## Best Practices

1. **Data Quality**: Ensure clean and consistent data export
2. **File Management**: Organize exported files by date and race type
3. **Analysis Preparation**: Structure data for easy spreadsheet analysis
4. **Historical Building**: Use for building comprehensive race databases
5. **Regular Exports**: Schedule regular exports for ongoing analysis
