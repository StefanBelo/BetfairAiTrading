# Show Selections Traded Volume

## Overview
The **Show Selections Traded Volume** strategy displays the traded volume information for market selections. This data strategy provides insights into market liquidity and trading activity.

## Strategy Details
- **Category**: Data
- **Type**: Volume Analysis
- **Purpose**: Display traded volume data for market selections

## Parameters

### Data Parameters
| Parameter | Group | Type | Required | Description |
|-----------|--------|------|----------|-------------|
| UpdateInterval | Data | TimeSpan | No | Set the update interval for volume data |

### Market Parameters
| Parameter | Group | Type | Required | Description |
|-----------|--------|------|----------|-------------|
| EvaluateEntryCriteriaOnlyOnce | Market | Boolean | No | Set to True to evaluate the entry criteria only once |
| StopMarketMonitoring | Market | Boolean | No | Set to True to stop market monitoring after strategy completion |

### Miscellaneous Parameters
| Parameter | Group | Type | Required | Description |
|-----------|--------|------|----------|-------------|
| StrategyReference | Miscellaneous | String | No | Strategy reference identifier (max 15 characters) |

## Usage Examples

### Real-time Volume Monitoring
```
Strategy: Show Selections Traded Volume
UpdateInterval: 00:00:30
EvaluateEntryCriteriaOnlyOnce: False
StopMarketMonitoring: False
```

### Rapid Volume Updates
```
Strategy: Show Selections Traded Volume
UpdateInterval: 00:00:10
EvaluateEntryCriteriaOnlyOnce: False
StopMarketMonitoring: False
StrategyReference: "VOL_RAPID"
```

### Single Assessment Mode
```
Strategy: Show Selections Traded Volume
UpdateInterval: 00:01:00
EvaluateEntryCriteriaOnlyOnce: True
StopMarketMonitoring: True
StrategyReference: "VOL_SINGLE"
```

## Best Practices

1. **Liquidity Analysis**: Monitor volume to assess market liquidity
2. **Trading Activity**: Use to identify periods of high trading activity
3. **Market Depth**: Analyze volume patterns to understand market depth
4. **Update Frequency**: Set appropriate intervals based on market volatility

## Key Metrics Displayed

- **Total Traded Volume**: Complete volume traded for each selection
- **Recent Trading Activity**: Volume patterns over time
- **Selection Comparison**: Relative volume between selections
- **Market Liquidity**: Overall market activity levels

## Integration

This strategy works well with:
- Price movement analysis
- Market depth strategies
- Liquidity assessment tools
- Trading decision systems

## Use Cases

### Pre-Event Analysis
- Assess market liquidity before placing bets
- Identify heavily traded selections
- Monitor volume trends leading to event start

### In-Play Monitoring
- Track real-time trading activity
- Identify momentum shifts through volume
- Monitor market reaction to events

### Post-Event Analysis
- Analyze trading patterns
- Study volume correlation with price movements
- Research market behavior

## Notes

- This is a data display strategy with no betting functionality
- Provides real-time volume information for market analysis
- Useful for understanding market liquidity and trading patterns
- Can be combined with price data for comprehensive market analysis
- Essential for liquidity-based trading strategies
