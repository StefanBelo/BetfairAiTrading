# Tick Offset Strategy

## Overview
This strategy places a bet and then automatically closes the position when a specified number of ticks profit is achieved.

## Category
Trading

## Description
A scalping strategy that opens a position and immediately sets a profit target measured in ticks. It's designed for quick, small profits from short-term price movements.

## Parameters

### Bet
- **BetType** (Required): Specifies the type of your bet - 'Back' or 'Lay'
- **Odds** (Optional): The odds at which you want to place your bet. Set to 0 to offer at best available odds

### Stake
- **Stake** (Required): Enter the stake amount for your bet

### Stake Attribute
- **StakeType** (Optional): Choose stake type - Stake, Liability, Payout, TickProfit, or NetTickProfit

### Profit/Loss
- **Profit** (Required): Set profit target in ticks to specify when bet position should close

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
- **Tick-Based Profits**: Precise profit targeting in price ticks
- **Scalping Focus**: Designed for quick, small profits
- **Automatic Execution**: Opens and closes positions automatically
- **Best Odds Option**: Can trade at best available prices

## Tick Profit Calculation
- **1 Tick**: Smallest price movement (varies by odds range)
- **2 Ticks**: Double the minimum price movement
- **Higher Ticks**: Larger profit targets for bigger moves

## Tick Values by Odds Range
- **1.01-2.00**: 1 tick = 0.01
- **2.00-3.00**: 1 tick = 0.02
- **3.00-4.00**: 1 tick = 0.05
- **4.00-6.00**: 1 tick = 0.1
- **6.00-10.00**: 1 tick = 0.2
- **10.00+**: 1 tick = 0.5

## Use Cases
- High-frequency scalping strategies
- Market making activities
- Quick profit taking from price movements
- Automated trading systems
- Risk-controlled trading with defined exits

## Examples
- Back at 2.0, close when price moves to 1.98 (1 tick profit)
- Lay at 3.0, close when price moves to 3.05 (1 tick profit)
- Scalp 2 ticks profit on market movements
- Quick trade for 5 ticks profit on news

## Scalping Strategy Benefits
- **Defined Risk**: Know exact profit target
- **Quick Execution**: Fast in-and-out trades
- **Multiple Opportunities**: Many small profits accumulate
- **Market Neutral**: Profit from any price direction

## Risk Considerations
- Requires liquid markets for quick execution
- Commission costs can impact small profits
- Market gaps can prevent target achievement
- High-frequency trading requires reliable connection
