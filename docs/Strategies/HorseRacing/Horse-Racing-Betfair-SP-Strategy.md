# Horse Racing Betfair SP Strategy

**Category:** Horse Racing  
**Strategy ID:** 1021

## Description

A horse racing strategy that leverages Betfair Starting Price (SP) data to trigger trading actions. This strategy uses SP differences and offsets to identify value opportunities and execute trading decisions based on SP analysis.

## Parameters

### Strategy
- **StrategyName** (Optional) - Enter the name of the strategy you wish to execute

### Bet
- **OddsOffset** (Optional) - Odds offset from betfair SP to start trading at (SByte)

### Market
- **EvaluateEntryCriteriaOnlyOnce** (Optional) - Set this parameter to True to evaluate the entry criteria only once
- **StopMarketMonitoring** (Optional) - Set this parameter to True to stop market monitoring after the strategy completes execution. Monitoring will stop only if no other bot is active in the market

### Miscellaneous
- **StrategyReference** (Optional) - Set this parameter to specify which strategy sent the bet. The string is limited to 15 characters

## Usage Examples

### SP-Based Trading
- **StrategyName**: `"Place Bet"`
- **OddsOffset**: `2`

### Conservative SP Strategy
- **StrategyName**: `"Close Selection Bet Position"`
- **OddsOffset**: `1`

### SP Arbitrage
- **StrategyName**: `"Tick Offset"`
- **OddsOffset**: `3`

## Best Practices

1. **SP Analysis**: Understand SP formation and timing
2. **Offset Strategy**: Use appropriate odds offsets for different market conditions
3. **Value Assessment**: Compare SP with live market prices
4. **Timing**: Consider SP availability timing
5. **Risk Management**: Use appropriate position sizing for SP-based strategies
