# Place Bet Strategy

## Overview
The Place Bet strategy is a fundamental betting strategy that allows you to place a single bet on a selection with various configuration options for odds, timing, and bet attributes.

## Category
General

## Description
This strategy provides comprehensive control over placing a bet on Betfair markets. It supports both back and lay bets with extensive customization options for odds ranges, timing, stake calculations, and market conditions.

## Parameters

### Bet
- **BetType** (Required): Specifies the type of your bet - 'Back' or 'Lay'
- **Odds** (Optional): Specify the exact odds you wish to bet on. To place a bet on exact odds, set 'PlaceBetInAllowedOddsRange' to False
- **MinimumOdds** (Optional): Specify the minimum odds you are willing to accept. Requires 'PlaceBetInAllowedOddsRange' to be True
- **MaximumOdds** (Optional): Specify the maximum odds you are willing to accept. Requires 'PlaceBetInAllowedOddsRange' to be True
- **PlaceBetInAllowedOddsRange** (Optional): Set to True to place your bet only within the allowed odds range

### Bet Attribute
- **AllowPlacingBetInPlay** (Optional): Enable in-play betting by setting this to True
- **AtInPlayKeepBet** (Optional): Keep bets on the market when it turns in-play (not supported on all markets)
- **MinimumOddsDifference** (Optional): Define minimum difference in ticks between best lay and back odds
- **MaximumOddsDifference** (Optional): Define maximum difference in ticks between best lay and back odds
- **OfferMyBet** (Optional): Place bet on opposite side - backing at best lay odds
- **PriceImprovement** (Optional): Improve odds by specified number of ticks
- **ChaseOddsTimeout** (Optional): Delay before chasing new odds when bet is unmatched

### Stake
- **Stake** (Required): Enter the stake amount for your bet

### Stake Attribute
- **StakeType** (Optional): Choose stake type - Stake, Liability, Payout, TickProfit, or NetTickProfit

### Time
- **PlaceBetTimeSpan** (Optional): Set when bet is placed relative to event start time (e.g., -0:05:00 for 5 minutes before)

### Selection
- **SortSelectionsBy** (Optional): Sort selections by LastPriceTraded, TotalMatched, or DoNotSort
- **ExecuteOnSelection** (Optional): Specify which selection to execute on (0 for active selection, 1+ for ranked selection)

### Market
- **StopBotExecutionOnMarketVersionChange** (Optional): Stop strategy when market version changes
- **EvaluateEntryCriteriaOnlyOnce** (Optional): Evaluate entry criteria only once
- **StopMarketMonitoring** (Optional): Stop market monitoring after strategy completion

### Miscellaneous
- **StrategyReference** (Optional): Custom reference string for the strategy (max 15 characters)

## Use Cases
- Simple back or lay bets on any selection
- Automated betting at specific times before/after event start
- Betting within specific odds ranges
- Price improvement strategies
- In-play betting scenarios

## Examples
- Place a £10 back bet at 3.0 odds on the favorite
- Lay a selection at best available odds with price improvement
- Place bets 5 minutes before race start with odds chasing enabled
