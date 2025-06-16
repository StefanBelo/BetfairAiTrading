# Place Bet - Fill or Kill Strategy

## Overview
This strategy places a bet that must be immediately matched or it will be automatically cancelled after a specified timeout period.

## Category
General

## Description
A time-sensitive betting strategy that places bets with a strict matching requirement. If the bet is not fully matched within the specified timeout period, it will be automatically cancelled. This is ideal for situations where you only want to place bets that can be immediately executed.

## Parameters

### Bet
- **BetType** (Required): Specifies the type of your bet - 'Back' or 'Lay'
- **Odds** (Optional): The odds at which you want to place your bet. Set to 0 to offer your bet at the best available odds

### Bet Attribute
- **AtInPlayKeepBet** (Optional): Keep bets on the market when it turns in-play (not supported on all markets)
- **BetMatchingTimeout** (Optional): If your bet isn't fully matched, it will be canceled once this timeout expires

### Stake
- **Stake** (Required): Enter the stake amount for your bet

### Stake Attribute
- **StakeType** (Optional): Choose stake type - Stake, Liability, Payout, TickProfit, or NetTickProfit

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
- **Immediate Execution**: Bet must be matched immediately or cancelled
- **Timeout Control**: Configurable timeout period for matching
- **Best Odds Option**: Can place at best available odds
- **Risk Management**: Prevents hanging unmatched bets

## Use Cases
- Scalping strategies requiring immediate execution
- News-based trading where timing is critical
- Markets with rapid price movements
- Risk management for automated systems
- Arbitrage opportunities with time constraints

## Examples
- Place £50 back bet that must be matched within 5 seconds
- Lay bet at best odds with 10-second timeout
- Scalping strategy with immediate execution requirement
- News trading with strict timing requirements
