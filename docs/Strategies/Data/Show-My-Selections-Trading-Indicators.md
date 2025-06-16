# Show My Selections Trading Indicators

## Overview
The **Show My Selections Trading Indicators** strategy displays trading indicators specifically for your selected markets or horses. This strategy provides focused analysis on selections of interest.

## Strategy Details
- **Category**: Data
- **Type**: Selection-Specific Indicators
- **Purpose**: Display trading indicators for selected/bookmarked selections

## Parameters

### Data Parameters
| Parameter | Group | Type | Required | Description |
|-----------|--------|------|----------|-------------|
| UpdateInterval | Data | TimeSpan | No | Set the update interval for indicator refresh |

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

### Real-time Selection Monitoring
```
Strategy: Show My Selections Trading Indicators
UpdateInterval: 00:00:30
EvaluateEntryCriteriaOnlyOnce: False
StopMarketMonitoring: False
```

### Fast Update Monitoring
```
Strategy: Show My Selections Trading Indicators
UpdateInterval: 00:00:10
EvaluateEntryCriteriaOnlyOnce: False
StopMarketMonitoring: False
StrategyReference: "MY_SEL_FAST"
```

### Single Assessment
```
Strategy: Show My Selections Trading Indicators
UpdateInterval: 00:01:00
EvaluateEntryCriteriaOnlyOnce: True
StopMarketMonitoring: True
StrategyReference: "MY_SEL_SNAP"
```

## Trading Indicators Displayed

### Price Indicators
- **Current Price**: Live back/lay prices
- **Price Movement**: Recent price changes
- **Price Trend**: Short and medium-term trends
- **Price Volatility**: Price stability measures

### Volume Indicators
- **Traded Volume**: Total and recent trading volume
- **Available Volume**: Liquidity at current prices
- **Volume Rate**: Trading velocity
- **Volume Trends**: Volume pattern analysis

### Market Position Indicators
- **Market Rank**: Position relative to other selections
- **Favorite Status**: Favoritism tracking
- **Market Share**: Percentage of total market volume
- **Relative Performance**: Comparison to market average

### Advanced Indicators
- **WOM (Weight of Money)**: Directional money flow
- **WAP (Weighted Average Price)**: Volume-weighted prices
- **Spread Analysis**: Back/lay spread measurements
- **Momentum Indicators**: Price/volume momentum

## Selection Management

### My Selections Features
- **Custom Watchlist**: Personal selection monitoring
- **Bookmarked Horses**: Saved selections for tracking
- **Portfolio View**: Multiple selection overview
- **Comparative Analysis**: Side-by-side selection comparison

### Filtering Options
- **Active Selections**: Currently active selections only
- **Performance Filters**: Filter by performance criteria
- **Time Filters**: Recent activity focus
- **Custom Criteria**: User-defined selection criteria

## Best Practices

1. **Selection Curation**: Maintain a focused list of relevant selections
2. **Update Frequency**: Balance detail with system performance
3. **Comparative Analysis**: Use for selection comparison and ranking
4. **Historical Tracking**: Monitor selection performance over time

## Integration

This strategy works well with:
- Portfolio management tools
- Selection research systems
- Trading decision frameworks
- Alert and notification systems

## Use Cases

### Portfolio Monitoring
- **Active Positions**: Monitor selections with open positions
- **Watchlist Tracking**: Track potential trading opportunities
- **Performance Review**: Analyze selection performance
- **Risk Assessment**: Evaluate selection-specific risks

### Research and Analysis
- **Selection Research**: Deep dive into specific selections
- **Comparative Studies**: Compare similar selections
- **Pattern Recognition**: Identify recurring patterns
- **Historical Analysis**: Study long-term performance

### Trading Support
- **Entry Timing**: Identify optimal entry points
- **Exit Planning**: Plan exit strategies based on indicators
- **Position Sizing**: Determine appropriate position sizes
- **Risk Management**: Monitor selection-specific risks

## Customization Options

### Display Preferences
- **Indicator Selection**: Choose which indicators to display
- **Layout Options**: Customize display layout
- **Color Coding**: Visual indicators for quick reference
- **Alert Thresholds**: Set custom alert levels

### Data Preferences
- **Time Frames**: Select relevant time periods
- **Calculation Methods**: Choose calculation preferences
- **Comparison Baselines**: Set comparison references
- **Historical Depth**: Define historical data scope

## Notes

- This is a data display strategy focused on selected/bookmarked items
- Provides concentrated analysis on selections of specific interest
- Useful for portfolio management and focused trading
- Can be customized to display preferred indicators
- Integrates with selection management systems
- Essential for traders focusing on specific selections or markets
