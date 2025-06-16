# My Show Offered And Traded Indicators

## Overview
My Show Offered And Traded Indicators is a customizable data visualization strategy that displays both offered liquidity and traded volume indicators in a personalized format. This strategy provides comprehensive market depth analysis and trading activity monitoring.

## Strategy Type
**Category:** Data  
**Execution:** Real-time Display  
**Market Timing:** Pre-event and In-play

## Parameters

### Data Configuration
| Parameter | Group | Type | Required | Description |
|-----------|--------|------|----------|-------------|
| UpdateInterval | Data | TimeSpan | No | Set the update interval for refreshing the indicators |

### Market Control
| Parameter | Group | Type | Required | Description |
|-----------|--------|------|----------|-------------|
| EvaluateEntryCriteriaOnlyOnce | Market | Boolean | No | Set this parameter to True to evaluate the entry criteria only once |
| StopMarketMonitoring | Market | Boolean | No | Set this parameter to True to stop market monitoring after the strategy completes execution |

### Miscellaneous
| Parameter | Group | Type | Required | Description |
|-----------|--------|------|----------|-------------|
| StrategyReference | Miscellaneous | String | No | Set this parameter to specify which strategy sent the bet. The string is limited to 15 characters |

## Displayed Indicators

### Offered Liquidity Indicators
- **Back Offers**: Available backing liquidity at different price levels
- **Lay Offers**: Available laying liquidity at different price levels
- **Offer Depth**: Number of price levels with significant liquidity
- **Offer Distribution**: How liquidity is spread across price levels
- **Offer Changes**: Real-time changes in offered amounts

### Traded Volume Indicators
- **Total Traded**: Cumulative volume traded in the market
- **Recent Trading**: Trading activity over recent time periods
- **Trading Rate**: Rate of trading activity
- **Price-Volume Analysis**: Relationship between price levels and trading volume
- **Trading Patterns**: Identification of trading patterns and trends

### Combined Analysis
- **Liquidity Pressure**: Ratio of offered to traded volume
- **Market Momentum**: Direction and strength of market movement
- **Support/Resistance**: Key levels based on offered and traded data
- **Market Efficiency**: How quickly offers are taken by traders

### Custom Metrics
- **Personalized Views**: Customizable display formats and metrics
- **Filtered Data**: Focus on specific price ranges or time periods
- **Alert Conditions**: Custom alerts based on specific indicator combinations
- **Historical Comparison**: Compare current data with historical patterns

## How It Works

1. **Data Aggregation**: Collects both offered liquidity and trading data
2. **Indicator Calculation**: Processes data to create meaningful indicators
3. **Custom Display**: Presents data in personalized format
4. **Real-time Updates**: Continuously updates indicators at specified intervals

## Key Features

### Advanced Visualization
- **Custom Layout**: Personalized arrangement of indicators
- **Color Coding**: Visual indicators for different market conditions
- **Trend Lines**: Display of trend information
- **Multi-Timeframe**: Analysis across different time periods

### Market Analysis
- **Liquidity Analysis**: Deep dive into market depth
- **Trading Analysis**: Understanding of trading patterns
- **Combined Insights**: Integration of offered and traded data
- **Predictive Indicators**: Forward-looking market indicators

## Use Cases

- **Professional Trading**: Advanced market analysis for professional traders
- **Liquidity Assessment**: Understanding market depth and execution costs
- **Market Making**: Supporting market making strategies
- **Research**: Market microstructure research and analysis
- **Strategy Development**: Data for developing trading strategies

## Example Configuration

```json
{
  "UpdateInterval": "00:00:03",
  "EvaluateEntryCriteriaOnlyOnce": false,
  "StrategyReference": "MyOfTrd001"
}
```

## Interpretation Guide

### Offered Liquidity Analysis
- **Deep Liquidity**: Multiple price levels with substantial offers
- **Thin Liquidity**: Limited offers, potential for price gaps
- **Offer Imbalance**: More backing or laying offers indicating sentiment
- **Liquidity Changes**: Dynamic changes in offered amounts

### Trading Activity Analysis
- **High Activity**: Frequent trading indicating active market
- **Low Activity**: Limited trading suggesting uncertainty
- **Volume Spikes**: Sudden increases in trading activity
- **Price-Volume Correlation**: Relationship between price movement and volume

### Combined Insights
- **Liquidity Consumption**: How quickly offers are being taken
- **Market Pressure**: Direction of predominant trading pressure
- **Efficiency Measures**: How well the market is functioning
- **Prediction Signals**: Early indicators of price movement

## Best Practices

1. **Update Frequency**: Balance between responsiveness and system performance
2. **Data Filtering**: Focus on relevant price ranges and time periods
3. **Alert Setup**: Configure meaningful alerts for trading opportunities
4. **Historical Context**: Always consider historical patterns
5. **Integration**: Use with other analysis tools for comprehensive view

## Advanced Features

- **Custom Calculations**: Create personalized indicator formulas
- **Data Export**: Export data for external analysis
- **Alert System**: Sophisticated alerting based on multiple conditions
- **Pattern Recognition**: Automatic identification of trading patterns

## Related Strategies

- [Show Offered and Traded Indicators](Show-Offered-and-Traded-Indicators.md) - Standard version
- [Show Selections Trading Indicators](Show-Selections-Trading-Indicators.md) - Focus on selections
- [Show Market Data](Show-Market-Data.md) - Market-wide view
- [Trading Data Recorder](Trading-Data-Recorder.md) - Data recording

## Tips

- Customize the display to focus on your most important metrics
- Use appropriate update intervals for different market conditions
- Set up alerts for unusual liquidity or trading patterns
- Monitor both absolute and relative indicators
- Consider market phase when interpreting indicators
- Use historical data to establish normal ranges for indicators
