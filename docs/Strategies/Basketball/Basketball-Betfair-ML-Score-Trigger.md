# Basketball Betfair ML Score Trigger

**Category:** Basketball  
**Strategy ID:** 1400

## Description

A basketball betting strategy that uses Betfair's machine learning score predictions to trigger betting actions. This strategy analyzes basketball game dynamics and executes betting decisions based on ML-powered score predictions and analysis.

## Parameters

### Bet
- **StrategyType** (Optional) - Set the strategy type
  - Options: Score

### Strategy
- **StrategyName** (Optional) - Enter the name of the strategy you wish to execute

### Market
- **EvaluateEntryCriteriaOnlyOnce** (Optional) - Set this parameter to True to evaluate the entry criteria only once
- **StopMarketMonitoring** (Optional) - Set this parameter to True to stop market monitoring after the strategy completes execution. Monitoring will stop only if no other bot is active in the market

### Miscellaneous
- **StrategyReference** (Optional) - Set this parameter to specify which strategy sent the bet. The string is limited to 15 characters

## Usage Examples

### Score-Based Betting
- **StrategyType**: `Score`
- **StrategyName**: `"Place Bet"`

### Live Score Analysis
- **StrategyType**: `Score`
- **StrategyName**: `"Close Selection Bet Position"`

### Score Prediction Trading
- **StrategyType**: `Score`
- **StrategyName**: `"Tick Offset"`

## Best Practices

1. **Score Monitoring**: Use for live basketball games with frequent score updates
2. **Quarter Analysis**: Consider quarter-by-quarter score progressions
3. **Margin Trading**: Focus on point spread and total points markets
4. **Game Flow**: Analyze momentum shifts and scoring patterns
5. **ML Confidence**: Trust ML predictions based on historical accuracy
