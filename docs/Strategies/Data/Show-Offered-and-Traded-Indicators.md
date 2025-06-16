# Show Offered and Traded Indicators

**Category:** Data  
**Strategy ID:** 2001

## Description

A data analysis strategy that displays indicators for both offered (available to back/lay) and traded (matched) amounts in the market. This strategy helps analyze market liquidity, trading activity, and price movement patterns.

## Parameters

### Data
- **UseTotalIndicator** (Optional) - Set to True if you want use total value indicator, either set False to use average value indicator (Boolean)
- **UpdateInterval** (Optional) - Set the update interval (TimeSpan)

### Market
- **EvaluateEntryCriteriaOnlyOnce** (Optional) - Set this parameter to True to evaluate the entry criteria only once
- **StopMarketMonitoring** (Optional) - Set this parameter to True to stop market monitoring after the strategy completes execution. Monitoring will stop only if no other bot is active in the market

### Miscellaneous
- **StrategyReference** (Optional) - Set this parameter to specify which strategy sent the bet. The string is limited to 15 characters

## Usage Examples

### Total Value Analysis
- **UseTotalIndicator**: `true`
- **UpdateInterval**: `00:00:10` (10 seconds)

### Average Value Monitoring
- **UseTotalIndicator**: `false`
- **UpdateInterval**: `00:00:15` (15 seconds)

### High-Frequency Trading Analysis
- **UseTotalIndicator**: `true`
- **UpdateInterval**: `00:00:05` (5 seconds)

## Best Practices

1. **Indicator Type**: Choose total vs average based on analysis needs
2. **Liquidity Assessment**: Monitor offered amounts for market depth
3. **Trading Volume**: Track traded amounts for market activity
4. **Update Timing**: Use appropriate intervals for market speed
5. **Market Context**: Consider market type and event timing
