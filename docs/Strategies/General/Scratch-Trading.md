# Scratch Trading Strategy

## Overview
This strategy places simultaneous back and lay bets to profit from bid-ask spreads, closing positions for small profits or minimal losses.

## Category
Trading

## Description
Scratch trading involves placing both back and lay bets on the same selection to profit from the spread between the prices. The strategy aims to "scratch" small profits by closing both positions at favorable prices.

## Parameters

### Stake
- **Stake** (Required): Enter the stake amount for your bet

### Stake Attribute
- **StakeType** (Optional): Choose stake type - Stake, Liability, Payout, TickProfit, or NetTickProfit

### Bet Attribute
- **Liquidity** (Optional): Place bet only when specified amount exists on both back and lay sides
- **Scratch** (Optional): The trade will be scratched if the closing bet position has an offered amount

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
- **Spread Trading**: Profit from bid-ask spreads
- **Liquidity Checks**: Ensure sufficient market depth
- **Scratch Management**: Automatic position closure
- **Risk Control**: Minimal exposure to directional risk

## Trading Mechanics
1. **Entry**: Place back and lay bets simultaneously
2. **Monitor**: Watch for favorable price movements
3. **Exit**: Close both positions for small profit
4. **Scratch**: If no profit available, minimize loss

## Liquidity Requirements
- Sufficient volume on both sides for entry
- Adequate depth for exit trades
- Stable market conditions for execution

## Use Cases
- Market making strategies
- Spread arbitrage opportunities
- Low-risk trading approaches
- High-frequency trading systems
- Stable market profit generation

## Examples
- Back at 2.02, lay at 2.00, profit from spread
- Place £100 on both sides with 2-tick spread
- Scratch trades when spread narrows
- Multiple small profits throughout the day

## Market Conditions
- **Best Markets**: High liquidity, stable prices
- **Avoid**: Volatile markets, news events
- **Ideal**: Pre-race horse racing, stable football markets
- **Timing**: Quiet periods with good depth

## Risk Management
- **Liquidity Checks**: Ensure market depth
- **Position Sizing**: Match stakes on both sides
- **Quick Exits**: Don't hold positions long
- **Spread Monitoring**: Close when spread disappears

## Profit Expectations
- Small but frequent profits
- Typically 1-3 ticks per trade
- Multiple trades per hour
- Consistent returns in stable markets
