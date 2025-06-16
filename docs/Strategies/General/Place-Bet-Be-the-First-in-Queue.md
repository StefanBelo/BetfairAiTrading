# Place Bet - Be the First in Queue Strategy

## Overview
This strategy places a bet and automatically chases the odds to keep your bet at the front of the queue, ensuring maximum chance of getting matched.

## Category
General

## Description
An advanced version of the basic Place Bet strategy that includes queue position management. When odds change and your bet is no longer at the front of the queue, this strategy will automatically update your bet to maintain the best position for matching.

## Parameters

### Bet
- **BetType** (Required): Specifies the type of your bet - 'Back' or 'Lay'
- **MinimumOdds** (Optional): Specify the minimum odds you are willing to accept
- **MaximumOdds** (Optional): Specify the maximum odds you are willing to accept
- **PlaceBetInAllowedOddsRange** (Optional): Set to True to place your bet only within the allowed odds range

### Bet Attribute
- **AllowPlacingBetInPlay** (Optional): Enable in-play betting by setting this to True
- **AtInPlayKeepBet** (Optional): Keep bets on the market when it turns in-play
- **MinimumOddsDifference** (Optional): Define minimum difference in ticks between best lay and back odds
- **MaximumOddsDifference** (Optional): Define maximum difference in ticks between best lay and back odds
- **PriceImprovement** (Optional): Improve odds by specified number of ticks
- **MinimumBetStakeToChase** (Optional): Prevent odds chasing for bet offers below this stake amount
- **ChaseOddsTimeout** (Optional): Delay before chasing new odds when bet is unmatched

### Stake
- **Stake** (Required): Enter the stake amount for your bet

### Stake Attribute
- **StakeType** (Optional): Choose stake type - Stake, Liability, Payout, TickProfit, or NetTickProfit

### Time
- **PlaceBetTimeSpan** (Optional): Set when bet is placed relative to event start time

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
- **Queue Management**: Automatically chases odds to maintain front position
- **Minimum Stake Control**: Prevents chasing for small stakes
- **Price Improvement**: Can improve odds while maintaining queue position

## Use Cases
- High-volume markets where queue position is critical
- Scalping strategies requiring quick execution
- Markets with frequent price movements
- Situations where getting matched is more important than exact odds

## Examples
- Place £100 back bet on favorite and chase odds to stay first in queue
- Lay betting with automatic queue position maintenance
- Pre-race betting with odds chasing enabled above £50 stakes
