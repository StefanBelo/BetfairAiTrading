# Greyhound Racing Dutching Strategy

**Category:** Greyhound Racing  
**Strategy ID:** 8000

## Description

A specialized greyhound racing dutching strategy that distributes stakes across multiple selections to ensure consistent profit regardless of which selection wins. This strategy is optimized for greyhound racing with specific timing and odds considerations.

## Parameters

### Bet
- **DutchingType** (Optional) - Set the dutching type, required profit or total stake
  - Options: RequiredProfit, TotalStake
- **TargetValue** (Optional) - Set the target value, so required profit or total stake depending on what you set by parameter: DutchingType (Double)
- **MinimumOdds** (Optional) - Specify the minimum odds you are willing to accept. Setting this parameter requires 'PlaceBetInAllowedOddsRange' to be 'True' (Double)

### Bet Attribute
- **ChaseOddsTimeout** (Optional) - If your bet is not fully matched and the odds change, the strategy will chase the new offered odds. You can postpone this odds chasing by setting this timeout parameter (TimeSpan)

### Selection
- **NumberOfSelections** (Optional) - Set the number of selections you want to dutch (Byte)

### Time
- **PlaceBetTimeSpan** (Optional) - Set this parameter to determine when your bet is placed, relative to the official event start time. A value like '-0:05:00' places your bet 5 minutes before the start, and '0:05:00' places it 5 minutes after (TimeSpan)

### Miscellaneous
- **UseShowCriteria** (Optional) - Show all criteria data (Boolean)
- **StrategyReference** (Optional) - Set this parameter to specify which strategy sent the bet. The string is limited to 15 characters

### Market
- **EvaluateEntryCriteriaOnlyOnce** (Optional) - Set this parameter to True to evaluate the entry criteria only once
- **StopMarketMonitoring** (Optional) - Set this parameter to True to stop market monitoring after the strategy completes execution. Monitoring will stop only if no other bot is active in the market

## Usage Examples

### Profit-Based Dutching
- **DutchingType**: `RequiredProfit`
- **TargetValue**: `5.0`
- **NumberOfSelections**: `3`
- **MinimumOdds**: `2.0`
- **PlaceBetTimeSpan**: `-00:02:00`

### Stake-Based Distribution
- **DutchingType**: `TotalStake`
- **TargetValue**: `20.0`
- **NumberOfSelections**: `4`
- **PlaceBetTimeSpan**: `-00:01:30`
- **UseShowCriteria**: `true`

### Conservative Approach
- **DutchingType**: `RequiredProfit`
- **TargetValue**: `2.0`
- **MinimumOdds**: `1.8`
- **NumberOfSelections**: `2`
- **ChaseOddsTimeout**: `00:00:30`

## Best Practices

1. **Selection Count**: Limit dutch selections (2-4) for manageable risk in greyhound racing
2. **Timing**: Place bets 1-3 minutes before race start for optimal odds
3. **Odds Filtering**: Use minimum odds to ensure value selections
4. **Profit Targets**: Set realistic profit targets based on race odds ranges
5. **Chase Timing**: Use short chase timeouts for fast-moving greyhound markets
6. **Criteria Display**: Enable UseShowCriteria for strategy debugging and optimization
