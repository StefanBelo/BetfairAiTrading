# Close Selection Bet Position Strategy

## Overview
This strategy monitors an existing bet position on a selection and closes it when profit/loss targets are reached or at a specific time.

## Category
Trading

## Description
A position management strategy that automatically closes bet positions based on profit/loss targets or time triggers. It's essential for risk management and profit taking in trading strategies.

## Parameters

### Profit/Loss
- **ProfitLossType** (Optional): Set target type - 'Money', 'Ticks', or 'Percentage'
- **Profit** (Required): Set profit target to specify when bet position should close
- **Loss** (Optional): Set loss target to specify when bet position should close

### Stake Attribute
- **HedgingEnabled** (Optional): Enable hedging of the bet position

### Bet Attribute
- **WaitForValidBetPosition** (Optional): Wait for a valid bet position to open before monitoring
- **CheckingLastPriceTraded** (Optional): Verify offered price was matched within specified range
- **CheckingLastPriceTradedDifference** (Optional): Maximum allowed price difference from last traded price
- **OfferMyBet** (Optional): Place closing bet on opposite side at best odds
- **ClosePositionImmediately** (Optional): Place closing bet immediately without waiting
- **BetMatchingTimeout** (Optional): Cancel closing bet if not matched within timeout

### Time
- **ClosePositionTimeSpan** (Optional): Close position at specific time relative to event start

### Data
- **UseShareBetPosition** (Optional): Use shared bet position data

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
- **Multiple Target Types**: Money, ticks, or percentage-based targets
- **Risk Management**: Automatic stop-loss and take-profit
- **Hedging Options**: Can hedge positions instead of closing
- **Time-Based Exits**: Close positions at specific times
- **Price Validation**: Verify closing prices are reasonable

## Profit/Loss Types
1. **Money**: Absolute profit/loss amounts in currency
2. **Ticks**: Profit/loss measured in price ticks
3. **Percentage**: Profit/loss as percentage of stake

## Use Cases
- Closing winning trading positions at profit targets
- Risk management with stop-loss orders
- Time-based position management
- Hedging existing positions for guaranteed profit
- Scalping strategies with quick profit taking

## Examples
- Close position when £20 profit reached
- Set stop-loss at 10 ticks loss
- Close all positions 5 minutes before event end
- Hedge position for guaranteed 5% profit
- Close position immediately if 15% profit achieved

## Trading Scenarios
- **Scalping**: Quick profit taking at small tick movements
- **Swing Trading**: Longer-term profit targets
- **Risk Management**: Automated stop-losses
- **Time Management**: Exit before market closure
