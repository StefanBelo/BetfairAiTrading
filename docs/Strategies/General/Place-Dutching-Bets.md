# Place Dutching Bets Strategy

## Overview
This strategy places multiple bets across different selections to ensure equal profit regardless of which selection wins (dutching).

## Category
General

## Description
Dutching is a betting technique where you place bets on multiple selections in the same market to guarantee the same profit regardless of which selection wins. This strategy automates the calculation and placement of the appropriate stakes across selected runners.

## Parameters

### Bet
- **BetType** (Required): Specifies the type of your bet - 'Back' or 'Lay'
- **TargetValue** (Required): Set the target value - either required profit or total stake depending on DutchingType
- **DutchingType** (Required): Choose between 'RequiredProfit' or 'TotalStake'

### Bet Attribute
- **ChaseOddsTimeout** (Optional): Delay before chasing new odds when bets are unmatched

### Selection
- **SortSelectionsBy** (Optional): Sort selections by LastPriceTraded, TotalMatched, or DoNotSort
- **NumberOfSelections** (Optional): Set number of selections to dutch (0 for all accepted selections)
- **OnSelections** (Optional): Specify specific selections to dutch (e.g., '1,2,4')

### Market
- **EvaluateEntryCriteriaOnlyOnce** (Optional): Evaluate entry criteria only once
- **StopMarketMonitoring** (Optional): Stop market monitoring after strategy completion

### Miscellaneous
- **StrategyReference** (Optional): Custom reference string for the strategy (max 15 characters)

## Key Features
- **Automatic Stake Calculation**: Calculates optimal stakes for equal profit
- **Flexible Selection**: Choose specific selections or number of favorites
- **Two Dutching Types**: Target specific profit or distribute total stake
- **Odds Chasing**: Optional odds chasing for unmatched bets

## Dutching Types
1. **Required Profit**: Specify the profit you want to make and the strategy calculates stakes
2. **Total Stake**: Specify total amount to stake and distribute across selections

## Use Cases
- Horse racing where you fancy multiple runners
- Football markets backing multiple outcomes
- Tennis matches covering multiple set scores
- Any market where you want guaranteed profit from multiple selections
- Risk reduction by covering multiple outcomes

## Examples
- Dutch top 3 favorites in horse race for £20 profit
- Back Home/Draw in football with £100 total stake
- Dutch multiple correct scores in football match
- Cover multiple tennis set betting outcomes

## Calculation Example
If dutching 3 selections with odds 2.0, 3.0, 4.0 for £30 profit:
- Selection 1: Stake calculated to return £30 profit
- Selection 2: Stake calculated to return £30 profit  
- Selection 3: Stake calculated to return £30 profit
Total stakes automatically calculated by strategy
