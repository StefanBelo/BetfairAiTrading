# Show Selection Trading Indicator

## Overview
The Show Selection Trading Indicator strategy displays real-time trading indicators for a specific selection in a market. This tool helps traders analyze selection behavior, trading patterns, and market sentiment for individual selections.

## Strategy Type
**Category:** Data  
**Execution:** Real-time Display  
**Market Timing:** Pre-event and In-play

## Parameters

### Data Configuration
| Parameter | Group | Type | Required | Description |
|-----------|--------|------|----------|-------------|
| UpdateInterval | Data | TimeSpan | No | Set the update interval for refreshing the trading indicators |

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

### Price Indicators
- **Last Traded Price**: Most recent price at which the selection was traded
- **Best Back/Lay Prices**: Current best available backing and laying odds
- **Price Movement**: Direction and magnitude of recent price changes
- **Price Volatility**: Measure of price stability over time

### Volume Indicators
- **Total Matched**: Total amount matched on the selection
- **Recent Volume**: Trading volume over recent time periods
- **Volume Rate**: Rate of volume increase/decrease
- **Volume Distribution**: Distribution of volume across different price levels

### Market Activity
- **Number of Bets**: Count of individual bets placed
- **Average Bet Size**: Average stake size of recent bets
- **Liquidity Depth**: Available liquidity at different price levels
- **Market Share**: Selection's share of total market volume

### Technical Indicators
- **Moving Averages**: Short and long-term price averages
- **Momentum**: Price momentum indicators
- **Support/Resistance**: Key price levels based on trading activity
- **Trend Direction**: Overall price trend analysis

## How It Works

1. **Data Collection**: Continuously collects real-time market data for the selection
2. **Indicator Calculation**: Processes data to calculate various trading indicators
3. **Display Update**: Updates the display at specified intervals
4. **Historical Tracking**: Maintains historical data for trend analysis

## Use Cases

- **Selection Analysis**: Detailed analysis of individual selection behavior
- **Trading Decisions**: Support for manual trading decisions
- **Market Research**: Understanding selection-specific trading patterns
- **Strategy Development**: Data gathering for strategy creation
- **Risk Assessment**: Evaluating selection-specific risks

## Example Configuration

```json
{
  "UpdateInterval": "00:00:05",
  "EvaluateEntryCriteriaOnlyOnce": false,
  "StrategyReference": "SelTrade001"
}
```

## Interpretation Guide

### Price Analysis
- **Steady Prices**: Indicates market confidence/consensus
- **Volatile Prices**: Suggests uncertainty or news impact
- **Price Trends**: Can indicate market sentiment changes
- **Support/Resistance**: Key levels for entry/exit decisions

### Volume Analysis
- **High Volume**: Strong interest and liquidity
- **Low Volume**: Limited interest or market uncertainty
- **Volume Spikes**: Often indicate significant market events
- **Volume Distribution**: Shows where most trading occurs

### Market Activity
- **Bet Frequency**: Indicates market activity level
- **Bet Size Distribution**: Shows retail vs professional activity
- **Liquidity Changes**: Important for execution planning

## Best Practices

1. **Update Frequency**: Set appropriate update intervals based on market volatility
2. **Multiple Timeframes**: Consider indicators across different time periods
3. **Context Analysis**: Consider overall market conditions
4. **Historical Comparison**: Compare current indicators with historical patterns
5. **Integration**: Use with other analysis tools for comprehensive view

## Integration with Trading

- **Entry Signals**: Use indicators to identify optimal entry points
- **Exit Signals**: Monitor indicators for exit opportunities
- **Risk Management**: Use volatility measures for position sizing
- **Market Timing**: Use volume and activity indicators for timing

## Related Strategies

- [Show Selections Trading Indicators](Show-Selections-Trading-Indicators.md) - Multiple selections view
- [Show My Selections Trading Indicators](Show-My-Selections-Trading-Indicators.md) - Portfolio view
- [Show Market Data](Show-Market-Data.md) - Market-wide indicators
- [Show Selection Data](Show-Selection-Data.md) - Basic selection data

## Tips

- Focus on the most relevant indicators for your trading style
- Consider market phase (pre-event vs in-play) when interpreting indicators
- Use shorter update intervals for fast-moving markets
- Monitor correlation between price and volume indicators
- Set up alerts for significant indicator changes
