# Betfair Market Analyzer (F# Version)

An advanced technical analysis tool for Betfair betting markets, implemented in F# using functional programming principles.

## Overview

This F# version of the Betfair Market Analyzer provides the same comprehensive technical analysis capabilities as the C# version but leverages F#'s functional programming features for more concise and expressive code.

## Features

- **Technical Analysis**: RSI, SMA, EMA, MACD, Bollinger Bands using Skender.Stock.Indicators
- **Market Flow Analysis**: Backing vs Laying pressure detection
- **Support/Resistance Levels**: Automatic identification of key price levels
- **Volume Analysis**: Steam move detection and volume spike identification
- **Trading Recommendations**: Multiple strategy suggestions with confidence levels
- **Comprehensive Reporting**: Detailed markdown reports with analysis results

## Key Differences from C# Version

### Functional Programming Benefits
- **Immutable Data**: All data structures are immutable by default
- **Pattern Matching**: Extensive use of pattern matching for cleaner code
- **Option Types**: Null-safe handling of optional values
- **Pipeline Operations**: Functional composition for data transformations
- **Type Safety**: F#'s type system provides additional safety guarantees

### Code Structure
- **Modules vs Classes**: Services implemented as modules with pure functions
- **Discriminated Unions**: Type-safe modeling of market states
- **Record Types**: Immutable data structures with structural equality
- **Function Composition**: Composable analysis pipeline

## Project Structure

```
BetfairMarketAnalyzerFSharp/
├── Models/
│   ├── BetfairModels.fs      # Market and selection data types
│   └── AnalysisModels.fs     # Analysis result and indicator types
├── Services/
│   ├── TechnicalAnalysisService.fs  # Core analysis functions
│   └── ReportGeneratorService.fs    # Report generation functions
├── Program.fs                # Main application entry point
├── BetfairMarketAnalyzerFSharp.fsproj
└── README.md
```

## Technical Analysis Features

### Technical Indicators (Skender.Stock.Indicators)
- **RSI (Relative Strength Index)**: Momentum oscillator (14-period)
- **Moving Averages**: SMA and EMA (5 and 10 period)
- **MACD**: Moving Average Convergence Divergence
- **Bollinger Bands**: Volatility indicators
- **Volume Analysis**: Volume-based momentum indicators

### Market Flow Analysis
- **Backing Pressure**: Percentage of volume driving odds down
- **Laying Pressure**: Percentage of volume driving odds up
- **Steam Moves**: Detection of large informed money movements
- **Market Sentiment**: Overall market direction assessment

### Trading Strategy Recommendations
1. **Back-to-Lay Trading**: Profit from odds shortening
2. **Pure Backing**: Hold-to-win strategies
3. **Lay-to-Back Trading**: Counter-trend opportunities
4. **Scalping**: Quick profit opportunities

## Usage

### Building the Project
```bash
dotnet build
```

### Running the Application
```bash
dotnet run
```

### Sample Output
The application generates:
- Real-time console output with key metrics
- Comprehensive markdown report saved to file
- Trading recommendations with confidence levels
- Risk assessment and position sizing guidance

## Dependencies

- **.NET 8.0**: Target framework
- **Skender.Stock.Indicators**: Technical analysis library
- **Newtonsoft.Json**: JSON serialization
- **System.Text.Json**: Alternative JSON handling

## F# Language Features Utilized

### Pattern Matching
```fsharp
match analysis.Indicators.RSI with
| Some rsi when rsi < 30m -> "Oversold"
| Some rsi when rsi > 70m -> "Overbought"
| Some _ -> "Neutral"
| None -> "N/A"
```

### Option Types
```fsharp
let rsiText = 
    analysis.Indicators.RSI 
    |> Option.map (fun x -> x.ToString("F1")) 
    |> Option.defaultValue "N/A"
```

### Pipeline Operations
```fsharp
priceData
|> List.groupBy (fun x -> Math.Round(x.Price, 2))
|> List.filter (fun (_, group) -> group.Length >= 3)
|> List.sortByDescending (fun (_, group) -> group |> List.sumBy (_.Volume))
|> List.take 10
```

### Record Types with Pattern Matching
```fsharp
type TradingRecommendation = {
    Strategy: string
    Action: string
    EntryPrice: decimal
    // ... other fields
}
```

## Integration with Bfexplorer

This F# version maintains full compatibility with the Bfexplorer ecosystem:
- Uses the same data models as the C# version
- Generates identical analysis reports
- Provides the same technical indicators
- Compatible with MCP (Model Context Protocol) integration

## Performance Considerations

### F# Advantages
- **Immutability**: Eliminates many categories of bugs
- **Tail Recursion**: Efficient recursive operations
- **Type Inference**: Reduced boilerplate code
- **Lazy Evaluation**: On-demand computation where beneficial

### Memory Efficiency
- Structural sharing of immutable data
- Efficient list operations
- Optimized pattern matching compilation

## Future Enhancements

1. **Advanced Pattern Recognition**: Chart pattern detection
2. **Machine Learning Integration**: ML.NET integration for predictions
3. **Real-time Streaming**: Live market data processing
4. **Portfolio Analysis**: Multi-selection analysis
5. **Custom Indicators**: User-defined technical indicators

## Development Notes

### Functional Design Principles
- **Pure Functions**: No side effects in core analysis functions
- **Immutable Data**: All data structures are immutable
- **Composability**: Functions can be easily combined
- **Type Safety**: Compile-time guarantees about data flow

### Error Handling
- Option types for nullable values
- Result types for operations that can fail
- Comprehensive exception handling in main application

## Contributing

When contributing to the F# version:
1. Maintain functional programming principles
2. Use immutable data structures
3. Prefer pattern matching over conditional logic
4. Utilize F#'s type system for safety
5. Keep functions pure when possible

## License

Same license as the main BetfairAiTrading project.
