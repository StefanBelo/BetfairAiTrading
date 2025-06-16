# Football Statarea Strategy

**Category:** Football  
**Strategy ID:** 1020

## Description

A specialized football betting strategy that integrates with Statarea tipster data to make informed betting decisions. This strategy filters betting opportunities based on tip confidence levels and specific betting recommendations, making it ideal for following professional tipsters.

## Parameters

### Bet
- **MinimumOdds** (Optional) - Specify the minimum odds you are willing to accept. Setting this parameter requires 'PlaceBetInAllowedOddsRange' to be 'True'
- **MaximumOdds** (Optional) - Specify the maximum odds you are willing to accept. Setting this parameter requires 'PlaceBetInAllowedOddsRange' to be 'True'

### Strategy
- **StrategyName** (Optional) - Enter the name of the strategy you wish to execute
- **Tip** (Optional) - Allow the action bot execution for this tip
- **TipConfidence** (Optional) - The minimal value of the tip confidence (Byte)

### Market
- **EvaluateEntryCriteriaOnlyOnce** (Optional) - Set this parameter to True to evaluate the entry criteria only once
- **StopMarketMonitoring** (Optional) - Set this parameter to True to stop market monitoring after the strategy completes execution. Monitoring will stop only if no other bot is active in the market

### Miscellaneous
- **StrategyReference** (Optional) - Set this parameter to specify which strategy sent the bet. The string is limited to 15 characters

## Usage Examples

### High Confidence Tips
- **TipConfidence**: `80`
- **MinimumOdds**: `1.5`
- **MaximumOdds**: `3.0`

### Specific Tip Type
- **Tip**: `"Over 2.5 Goals"`
- **TipConfidence**: `70`
- **StrategyName**: `"Place Bet"`

### Conservative Betting
- **MinimumOdds**: `1.8`
- **MaximumOdds**: `2.5`
- **TipConfidence**: `85`

## Best Practices

1. Set appropriate confidence thresholds (70+ recommended)
2. Use odds filters to manage risk
3. Focus on specific tip types that match your strategy
4. Monitor tip performance over time
5. Combine with other market analysis for best results
