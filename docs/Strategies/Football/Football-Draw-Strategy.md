# Football Draw Strategy

**Category:** Football  
**Strategy ID:** 1104

## Description

A specialized football betting strategy designed to manage draw positions based on match time progression. This strategy is particularly useful for in-play draw betting where positions need to be closed at specific match times or under certain conditions.

## Parameters

### Strategy
- **StrategyName** (Optional) - Enter the name of the strategy you wish to execute

### Trigger
- **MatchTime** (Optional) - Close the bet position at the match time (Int32)

### Market
- **EvaluateEntryCriteriaOnlyOnce** (Optional) - Set this parameter to True to evaluate the entry criteria only once
- **StopMarketMonitoring** (Optional) - Set this parameter to True to stop market monitoring after the strategy completes execution. Monitoring will stop only if no other bot is active in the market

### Miscellaneous
- **StrategyReference** (Optional) - Set this parameter to specify which strategy sent the bet. The string is limited to 15 characters

## Usage Examples

### Early Exit Strategy
- **MatchTime**: `20`
- **StrategyName**: `"Close Selection Bet Position"`

### Half-Time Exit
- **MatchTime**: `45`
- **StrategyName**: `"Close Market Bet Position"`

### Late Game Management
- **MatchTime**: `85`
- **StrategyName**: `"Trailing Stop Loss"`

## Best Practices

1. Set realistic match time thresholds for position closure
2. Monitor draw odds throughout the match
3. Consider team performance and match dynamics
4. Use appropriate closing strategies for different scenarios
5. Account for injury time and match extensions
