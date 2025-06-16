# My Horse Racing Strategy

**Category:** Horse Racing  
**Strategy ID:** 1007

## Description

A comprehensive horse racing betting strategy that incorporates multiple factors including odds ranges, race conditions, runner characteristics, and Timeform selections. This strategy is designed for both individual bets and dutching multiple selections based on sophisticated filtering criteria.

## Parameters

### Bet
- **BetType** (Optional) - Specifies the type of your bet: 'Back' or 'Lay'
  - Options: Back, Lay
- **MinimumOdds** (Optional) - Set the minimum odds to place your dutching bets on (Double)
- **MaximumOdds** (Optional) - Set the maximum odds to place your dutching bets on (Double)
- **MinimumOddsDifference** (Optional) - Set the minimum odds difference between dutched selections (Double)

### Stake
- **Stake** (Optional) - Total stake to place dutch bets (Double)

### Bet Attribute
- **MinimumNumberOfRunners** (Optional) - Set the minimum number of runners (Byte)
- **MaximumNumberOfRunners** (Optional) - Set the maximum number of runners (Byte)
- **MinimumRaceDistance** (Optional) - Set the minimum race distance (Int32)
- **IsAmongFavourites** (Optional) - Check whether my selection is among favourites (Boolean)
- **PlaceOnlyDutchBets** (Optional) - Set to place only dutch bets (Boolean)
- **NumberOfDutchBets** (Optional) - Set the number of dutch bets (Byte)
- **PriceImprovement** (Optional) - Use this parameter to improve your odds (price). For instance, if you are backing a selection and the best lay odds are 2.02, setting 'PriceImprovement' to 1 (in ticks) will adjust your odds to 2.04 (Byte)
- **ChaseOddsTimeout** (Optional) - If your bet is not fully matched and the odds change, the strategy will chase the new offered odds. You can postpone this odds chasing by setting this timeout parameter (TimeSpan)
- **UseTimeformSelections** (Optional) - Set if you want to use timeform selections (Boolean)

### Market
- **EvaluateEntryCriteriaOnlyOnce** (Optional) - Set this parameter to True to evaluate the entry criteria only once
- **StopMarketMonitoring** (Optional) - Set this parameter to True to stop market monitoring after the strategy completes execution. Monitoring will stop only if no other bot is active in the market

### Miscellaneous
- **StrategyReference** (Optional) - Set this parameter to specify which strategy sent the bet. The string is limited to 15 characters

## Usage Examples

### Favorite Dutching Strategy
- **BetType**: `Back`
- **MinimumOdds**: `2.0`
- **MaximumOdds**: `6.0`
- **IsAmongFavourites**: `true`
- **PlaceOnlyDutchBets**: `true`
- **NumberOfDutchBets**: `3`
- **Stake**: `10.0`

### Timeform-Based Betting
- **BetType**: `Back`
- **UseTimeformSelections**: `true`
- **MinimumNumberOfRunners**: `8`
- **MaximumNumberOfRunners**: `16`
- **MinimumRaceDistance**: `1600`

### Lay Strategy on Long Shots
- **BetType**: `Lay`
- **MinimumOdds**: `10.0`
- **MaximumOdds**: `50.0`
- **MinimumOddsDifference**: `2.0`
- **PriceImprovement**: `1`

### Distance-Specific Strategy
- **MinimumRaceDistance**: `2000`
- **MinimumNumberOfRunners**: `10`
- **BetType**: `Back`
- **MinimumOdds**: `3.0`
- **MaximumOdds**: `12.0`

## Best Practices

1. **Odds Management**: Use MinimumOddsDifference to ensure value in dutching strategies
2. **Race Filtering**: Set appropriate runner and distance filters for your strategy focus
3. **Favorite Focus**: Use IsAmongFavourites for strategies targeting market leaders
4. **Timeform Integration**: Enable UseTimeformSelections for expert analysis inclusion
5. **Risk Management**: Set appropriate stake levels and odds ranges
6. **Price Improvement**: Use small price improvements (1-2 ticks) for better queue position
7. **Dutch Betting**: Ensure sufficient odds differences when placing multiple bets
