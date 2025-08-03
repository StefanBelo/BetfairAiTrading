# Betfair Market Analyzer Python

A Python application that performs comprehensive technical analysis on Betfair betting market data, generating detailed trading reports similar to professional financial market analysis.

This is the Python equivalent of the C# BetfairMarketAnalyzer project, using **TA-Lib** and **pandas** for technical analysis.

## Features

- **Technical Analysis**: RSI, MACD, Bollinger Bands, Moving Averages using TA-Lib
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

## Installation

```bash
cd src/BetfairMarketAnalyzerPython
pip install -r requirements.txt
```

## Usage

```bash
python main.py
```

The application will:
1. Load sample betting market data (simulating Bfexplorer data)
2. Perform technical analysis using TA-Lib
3. Generate a comprehensive markdown report
4. Save the report with timestamp
5. Display key metrics in console

## Dependencies

- **pandas**: Data manipulation and analysis
- **numpy**: Numerical computing
- **ta-lib**: Technical analysis library
- **plotly**: Data visualization (optional)
- **dataclasses-json**: JSON serialization support

## Sample Output

The application generates reports including:

- Market information and selection overview
- Technical indicator values (RSI, MACD, Bollinger Bands)
- Support and resistance levels
- Volume analysis and steam move detection
- Trading recommendations with risk assessment
- Professional markdown formatting

## Project Structure

```
BetfairMarketAnalyzerPython/
├── main.py                 # Entry point
├── models/
│   ├── __init__.py
│   ├── betfair_models.py   # Betfair data models
│   └── analysis_models.py  # Analysis result models
├── services/
│   ├── __init__.py
│   ├── technical_analysis_service.py  # Technical analysis logic
│   └── report_generator_service.py    # Report generation
├── requirements.txt        # Python dependencies
└── README.md              # This file
```

## License

This project is part of the BetfairAiTrading solution.
