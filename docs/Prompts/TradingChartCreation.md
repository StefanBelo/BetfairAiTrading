# Trading Chart Creation Prompt

Please create an interactive financial chart using the time series data from my Bfexplorer MCP server. Follow these specific requirements:

## Chart Requirements

### Data Processing
- Parse the JSON time series data returned from the MCP tool call
- Handle timestamps properly (convert to JavaScript Date objects)
- Validate data integrity (check for missing values, outliers)
- Sort data chronologically if needed

### Chart Type & Features
- Create a **candlestick chart** for price data (if OHLC available) OR **line chart** for simple price points
- Include a **separate volume chart** below the price chart
- Add **hover tooltips** showing exact timestamp, price, and volume values
- Enable **zoom and pan** functionality for detailed analysis
- Include **crosshair cursor** for precise data point identification

### Visual Design
- Use a **dark theme** suitable for financial data analysis
- Apply **professional color scheme**: 
  - Green for price increases/bullish movements
  - Red for price decreases/bearish movements  
  - Blue/gray for volume bars
- Ensure **responsive design** that works on different screen sizes
- Add **grid lines** for easier value reading

### Technical Features
- **Dual Y-axes**: Price scale on left, volume scale on right
- **Time-based X-axis** with appropriate interval labeling
- **Legend** showing what each data series represents
- **Loading states** while processing data
- **Error handling** for malformed or missing data

### Additional Analysis (if data supports)
- **Moving averages** (e.g., 20-period, 50-period) as overlay lines
- **Volume-weighted average price (VWAP)** if sufficient data
- **Price trend lines** connecting significant highs/lows
- **Support/resistance levels** if identifiable from data

## Implementation Instructions

### Preferred Libraries
1. **Chart.js** with financial plugin for candlestick charts
2. **Plotly.js** for advanced interactive features
3. **D3.js** for custom financial visualizations (if complex analysis needed)

### Chart Structure
```
[Price Chart - Top Panel]
- Candlesticks/Line showing price movement
- Moving averages as overlay lines
- Volume indicators if combined view

[Volume Chart - Bottom Panel]  
- Bar chart showing trading volume
- Color-coded bars matching price movement direction
- Separate scale optimized for volume range
```

### Data Format Handling
- Support common time formats: ISO 8601, Unix timestamps, formatted strings
- Handle different price formats: decimal numbers, formatted currency
- Process volume data: integer counts, formatted numbers with K/M suffixes
- Manage missing data points gracefully

### User Interaction Features
- **Click to highlight** specific data points
- **Drag to select** time ranges for detailed analysis  
- **Right-click context menu** for additional options
- **Export functionality** to save chart as image
- **Print-friendly** styling option

## Expected Output

Create a complete HTML artifact with embedded JavaScript that:
1. **Processes the MCP data** automatically when loaded
2. **Renders immediately** without external dependencies (except CDN libraries)
3. **Looks professional** and suitable for financial analysis
4. **Functions smoothly** with good performance even with large datasets
5. **Provides insights** through visual presentation of the trading data

## Sample Usage Context

This chart will be used to visualize data from calls like:
- `GetDataContextForMarketSelection("MarketSelectionsPriceHistoryData", marketId, selectionId)`
- `GetDataContextForMarket("MarketSelectionsPriceHistoryData", marketId)`

The resulting chart should help users:
- Identify price trends and patterns
- Analyze volume correlation with price movements
- Make informed trading decisions based on historical data
- Spot potential entry/exit points for strategies

Please ensure the chart is **production-ready** and **visually impressive** while maintaining **functional accuracy** for financial data analysis.