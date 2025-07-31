# Betfair Market Analyzer

A C# application that uses **Skender.Stock.Indicators** to perform comprehensive technical analysis on Betfair betting market data, generating detailed trading reports similar to professional financial market analysis.

## Features

- **Technical Analysis**: RSI, MACD, Bollinger Bands, Moving Averages using Skender.Stock.Indicators
- **Support/Resistance Detection**: Automatically identifies key price levels
- **Volume Analysis**: Detects steam moves and volume spikes
- **Market Flow Analysis**: Determines backing vs laying pressure
- **Trading Recommendations**: Generates specific entry/exit strategies
- **Comprehensive Reports**: Professional markdown reports with detailed analysis

## Technical Indicators Implemented

- **RSI (Relative Strength Index)**: 14-period RSI for overbought/oversold conditions
- **Moving Averages**: SMA and EMA (5 and 10 period)
- **MACD**: Moving Average Convergence Divergence
- **Bollinger Bands**: For volatility and price envelope analysis
- **Volume Analysis**: Volume moving averages and spike detection

## Trading Strategies Generated

1. **Back-to-Lay Trading**: Enter backing position, exit by laying at lower odds
2. **Pure Backing**: Hold position to race completion
3. **Lay-to-Back**: Counter-trend strategy for odds lengthening
4. **Scalping**: Quick profit from small price movements

## Usage

```bash
cd src/BetfairMarketAnalyzer
dotnet run
```

The application will:
1. Load sample betting market data (simulating Bfexplorer data)
2. Perform technical analysis using Skender.Stock.Indicators
3. Generate a comprehensive markdown report
4. Save the report with timestamp
5. Display key metrics in console

## Sample Output

The application generates reports including:

- Market information and selection overview
- Technical indicator values (RSI, MACD, Bollinger Bands)
- Support and resistance levels
- Volume analysis and steam move detection
- Multiple trading strategy recommendations
- Risk assessment and position sizing
- Professional summary with key findings

## Dependencies

- **Skender.Stock.Indicators**: Professional-grade technical analysis library
- **System.Text.Json**: JSON parsing for market data
- **Newtonsoft.Json**: Additional JSON support

## Architecture

- `Models/`: Data models for Betfair market data and analysis results
- `Services/TechnicalAnalysisService.cs`: Core analysis using Skender indicators
- `Services/ReportGeneratorService.cs`: Professional report generation
- `Program.cs`: Main application with sample data simulation

## Integration with Bfexplorer

In production, replace the `GenerateSampleData()` method with actual API calls to Bfexplorer:

```csharp
// Replace sample data with real API calls
var marketData = await bfexplorerClient.GetMarketSelectionData(marketId, selectionId);
var analysisResult = analysisService.AnalyzeSelection(marketData);
```

## Key Features

- **Financial-grade Analysis**: Uses the same technical indicators as professional trading platforms
- **Betfair-specific Logic**: Understands backing vs laying pressure and odds movements
- **Professional Reports**: Generate detailed markdown reports suitable for trading decisions
- **Real-time Analysis**: Designed for live market analysis with time-sensitive recommendations
- **Risk Management**: Includes position sizing and risk assessment

This application brings institutional-quality technical analysis to Betfair trading, providing the same level of analysis used in financial markets adapted specifically for betting exchanges.
