# Place SP Bet Strategy

## Overview
This strategy places Starting Price (SP) bets that are matched at the official Betfair Starting Price when the market turns in-play.

## Category
General

## Description
Starting Price bets are a special type of bet that gets matched at the official Betfair Starting Price calculated when the market goes in-play. This strategy allows you to place SP bets with various options for odds limits and persistence types.

## Parameters

### Bet
- **BetType** (Required): Specifies the type of your bet - 'Back' or 'Lay'
- **Odds** (Optional): Specify the odds at which you wish to place your bet
- **LimitOdds** (Optional): Sets limit odds for SP bet - bet matched only if SP is better than this limit
- **PersistenceType** (Optional): How bet persists - 'Cancel', 'Keep', or 'TakeSP'

### Stake
- **Stake** (Required): Specify the liability for your SP bet stake

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
- **Starting Price Matching**: Bets matched at official Betfair SP
- **Limit Protection**: Set minimum acceptable starting price
- **Persistence Options**: Control what happens to unmatched bets
- **No Price Risk**: Get the true starting price of the event

## Persistence Types
1. **Cancel**: Cancel unmatched bets when market turns in-play
2. **Keep**: Keep unmatched bets active in-play
3. **TakeSP**: Take the Starting Price regardless of limit

## Limit Odds Logic
- **Back Bets**: Matched if SP is greater than limit odds
- **Lay Bets**: Matched if SP is less than limit odds

## Use Cases
- Getting true starting prices without pre-race price risk
- Betting on favorites without watching pre-race drift
- Laying short-priced favorites with SP protection
- Setting bets for events you can't monitor live
- Avoiding pre-race market manipulation

## Examples
- Back favorite at SP with minimum limit of 2.0
- Lay horse at SP with maximum limit of 1.5
- Multiple SP bets on different selections
- SP dutching across multiple runners

## Advantages
- No pre-race price movement risk
- Get true market price at start
- No need to monitor pre-race markets
- Protection from market manipulation
- Automated execution at event start
