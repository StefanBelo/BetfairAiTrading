# Show Race Steamers &amp; Drifters

## Overview
The **Show Race Steamers &amp; Drifters** strategy identifies and displays selections that are experiencing significant price movements (steamers moving in/drifters moving out). This is essential for spotting market trends and momentum.

## Strategy Details
- **Category**: Data
- **Type**: Price Movement Analysis
- **Purpose**: Identify selections with significant price movements (steamers and drifters)

## Parameters

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

### Continuous Monitoring
```
Strategy: Show Race Steamers & Drifters
EvaluateEntryCriteriaOnlyOnce: False
StopMarketMonitoring: False
```

### Single Analysis
```
Strategy: Show Race Steamers & Drifters
EvaluateEntryCriteriaOnlyOnce: True
StopMarketMonitoring: True
StrategyReference: "STEAM_DRIFT"
```

## Key Features

### Steamers (Price Shortening)
- **Definition**: Selections whose odds are decreasing (becoming more favored)
- **Indicators**: Strong market support, increased backing activity
- **Significance**: Suggests positive market sentiment or insider information

### Drifters (Price Lengthening)
- **Definition**: Selections whose odds are increasing (becoming less favored)
- **Indicators**: Market rejection, increased laying activity
- **Significance**: Suggests negative market sentiment or lack of support

## Analysis Metrics

### Price Movement Tracking
- **Percentage Change**: Calculate price movement as percentage
- **Rate of Change**: Speed of price movement
- **Volume Correlation**: Link price movement to trading volume
- **Time Analysis**: Track movement patterns over time

### Market Signals
- **Strong Steamers**: Significant price shortening with volume
- **Weak Drifters**: Gradual price lengthening
- **Momentum Indicators**: Acceleration or deceleration of movement
- **Reversal Signals**: Changes in movement direction

## Best Practices

1. **Real-time Monitoring**: Keep continuous monitoring enabled for live events
2. **Volume Correlation**: Always consider volume alongside price movement
3. **Time Context**: Analyze movements relative to event start time
4. **Market Conditions**: Consider overall market volatility

## Integration

This strategy works well with:
- Volume analysis strategies
- Market depth indicators
- Trading decision systems
- Alert mechanisms

## Use Cases

### Pre-Event Analysis
- Identify market favorites and outsiders
- Spot early market trends
- Monitor sentiment changes

### Trading Opportunities
- **Steamer Following**: Back selections gaining market support
- **Drifter Laying**: Lay selections losing market confidence
- **Reversal Trading**: Trade against extreme movements
- **Momentum Trading**: Follow strong directional movements

### Research and Analysis
- Study market efficiency
- Analyze information flow
- Research crowd behavior patterns

## Alerts and Notifications

### Steamer Alerts
- Rapid price shortening
- High volume steamers
- Late market steamers

### Drifter Alerts
- Significant price drifting
- Volume-backed drifting
- Sudden market rejection

## Notes

- This is a data analysis strategy with no betting functionality
- Provides real-time identification of price movements
- Essential for understanding market sentiment and trends
- Can be used as foundation for various trading strategies
- Particularly valuable in horse racing and sports betting markets
- Helps identify market inefficiencies and opportunities
