# Greyhound Racing Betfair ML Ratings Trigger

**Category:** Greyhound Racing  
**Strategy ID:** 2010

## Description

A machine learning-powered greyhound racing strategy that uses Betfair's ML ratings to trigger betting actions. This strategy evaluates selections based on sophisticated rating algorithms and executes trades based on various ML-driven criteria.

## Parameters

### Bet
- **StrategyType** (Optional) - Set the strategy type
  - Options: BestRated, BestSelection, CloseToRated, PositiveCloseToRated, NegativeCloseToRated

### Strategy
- **StrategyName** (Optional) - Enter the name of the strategy you wish to execute

### Market
- **EvaluateEntryCriteriaOnlyOnce** (Optional) - Set this parameter to True to evaluate the entry criteria only once
- **StopMarketMonitoring** (Optional) - Set this parameter to True to stop market monitoring after the strategy completes execution. Monitoring will stop only if no other bot is active in the market

### Miscellaneous
- **StrategyReference** (Optional) - Set this parameter to specify which strategy sent the bet. The string is limited to 15 characters

## Usage Examples

### Best Rated Strategy
- **StrategyType**: `BestRated`
- **StrategyName**: `"Place Bet"`

### Positive Rating Deviation
- **StrategyType**: `PositiveCloseToRated`
- **StrategyName**: `"Tick Offset"`

### Market Leader Focus
- **StrategyType**: `BestSelection`
- **StrategyName**: `"Close Selection Bet Position"`

### Negative Rating Opportunity
- **StrategyType**: `NegativeCloseToRated`
- **StrategyName**: `"Place Bet"`

## Best Practices

1. **Rating Analysis**: Understand the ML rating system before implementing
2. **Strategy Alignment**: Match strategy type with betting approach (back vs lay)
3. **Rating Thresholds**: Monitor rating differences for optimal trigger points
4. **Performance Tracking**: Track success rates for different strategy types
5. **Market Conditions**: Consider race distance and track conditions in rating interpretation
