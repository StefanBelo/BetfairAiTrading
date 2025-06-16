# Show Selection Data

**Category:** Data  
**Strategy ID:** 1005

## Description

A data visualization strategy that displays detailed information about individual selections within a market. This strategy provides comprehensive selection-specific data including odds, volume, and trading activity for analysis and decision-making.

## Parameters

### Market
- **EvaluateEntryCriteriaOnlyOnce** (Optional) - Set this parameter to True to evaluate the entry criteria only once
- **StopMarketMonitoring** (Optional) - Set this parameter to True to stop market monitoring after the strategy completes execution. Monitoring will stop only if no other bot is active in the market

### Miscellaneous
- **StrategyReference** (Optional) - Set this parameter to specify which strategy sent the bet. The string is limited to 15 characters

## Usage Examples

### Selection Analysis
- **EvaluateEntryCriteriaOnlyOnce**: `false`

### Quick Selection Check
- **EvaluateEntryCriteriaOnlyOnce**: `true`
- **StrategyReference**: `"SelCheck"`

### Continuous Monitoring
- **StopMarketMonitoring**: `false`

## Best Practices

1. **Individual Focus**: Use for detailed analysis of specific selections
2. **Comparison**: Compare data across multiple selections
3. **Pattern Recognition**: Identify selection-specific trading patterns
4. **Decision Support**: Use data to inform betting and trading decisions
5. **Performance Tracking**: Monitor selection performance over time
