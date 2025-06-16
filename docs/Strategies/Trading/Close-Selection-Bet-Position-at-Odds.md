# Close Selection Bet Position at Odds Strategy

## Overview
This strategy closes an existing bet position when the selection reaches specific odds, regardless of profit or loss.

## Category
Trading

## Description
A position management strategy that monitors selection odds and closes positions when predetermined price levels are reached. This is useful for technical analysis-based trading and price-level exits.

## Parameters

### Profit/Loss
- **Odds** (Optional): Set target odds level to close the bet position

### Stake Attribute
- **HedgingEnabled** (Optional): Enable hedging of the bet position

### Bet Attribute
- **CheckingLastPriceTraded** (Optional): Verify offered price was matched within specified range
- **CheckingLastPriceTradedDifference** (Optional): Maximum allowed price difference from last traded price
- **BetMatchingTimeout** (Optional): Cancel closing bet if not matched within timeout

### Selection
- **SortSelectionsBy** (Optional): Sort selections by LastPriceTraded, TotalMatched, or DoNotSort
- **ExecuteOnSelection** (Optional): Specify which selection to execute on

### Market
- **StopBotExecutionOnMarketVersionChange** (Optional): Stop strategy when market version changes
- **EvaluateEntryCriteriaOnlyOnce** (Optional): Evaluate entry criteria only once
- **StopMarketMonitoring** (Optional): Stop market monitoring after strategy completion

### Miscellaneous
- **StrategyReference** (Optional): Custom reference string for the strategy (max 15 characters)

## Key Features
- **Price-Level Exits**: Close positions at specific odds levels
- **Hedging Options**: Hedge instead of closing for guaranteed profit
- **Price Validation**: Verify closing prices are reasonable
- **Technical Trading**: Support for technical analysis strategies

## Use Cases
- Technical analysis-based trading exits
- Support and resistance level trading
- Closing positions at predetermined price targets
- Momentum trading strategies
- Price breakout strategies

## Examples
- Close back position when odds reach 2.0
- Exit lay position if odds drift to 5.0
- Close position at resistance level of 3.5
- Technical trading exit at support level
- Momentum strategy exit at breakout level

## Trading Applications
- **Technical Analysis**: Exit at key price levels
- **Momentum Trading**: Close on price breakouts
- **Mean Reversion**: Exit when price returns to mean
- **Support/Resistance**: Trade bounces off key levels

## Comparison with Profit/Loss Strategy
Unlike the Close Selection Bet Position strategy that focuses on profit/loss amounts, this strategy focuses purely on price levels, making it ideal for:
- Technical analysis approaches
- Chart-based trading strategies
- Price level discipline
- Market structure trading
