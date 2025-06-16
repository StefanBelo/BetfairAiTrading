# Close Market Bet Position Strategy

## Overview
This strategy monitors and closes bet positions across an entire market rather than individual selections, providing portfolio-level position management.

## Category
Trading

## Description
A market-level position management strategy that monitors the overall profit/loss across all selections in a market and closes positions when market-wide targets are achieved. This is ideal for portfolio trading and overall risk management.

## Parameters

### Profit/Loss
- **Profit** (Required): Set profit target for the entire market position
- **Loss** (Optional): Set loss target for the entire market position
- **ProfitOrLossInPercentage** (Optional): Use percentage values for profit and loss targets

### Bet Attribute
- **WaitForValidBetPosition** (Optional): Wait for valid bet positions to be established
- **BetMatchingTimeout** (Optional): Cancel closing bets if not matched within timeout
- **CheckingLastPriceTraded** (Optional): Verify offered prices were matched within specified range
- **CheckingLastPriceTradedDifference** (Optional): Maximum allowed price difference from last traded price

### Time
- **ClosePositionTimeSpan** (Optional): Close positions at specific time relative to event start

### Strategy
- **AllowBotExecutionTermination** (Optional): Allow strategy termination before market position is closed

### Market
- **EvaluateEntryCriteriaOnlyOnce** (Optional): Evaluate entry criteria only once
- **StopMarketMonitoring** (Optional): Stop market monitoring after strategy completion

### Miscellaneous
- **StrategyReference** (Optional): Custom reference string for the strategy (max 15 characters)

## Key Features
- **Market-Wide Monitoring**: Track positions across entire market
- **Portfolio Management**: Overall profit/loss tracking
- **Percentage Targets**: Flexible profit/loss calculation methods
- **Time-Based Exits**: Market-level time management

## Market Position Calculation
- **Total Exposure**: Sum of all positions across selections
- **Net Profit/Loss**: Combined result from all selections
- **Portfolio View**: Market as single trading entity

## Profit/Loss Types
1. **Absolute Amounts**: Fixed currency amounts
2. **Percentage**: Percentage of total stakes or exposure

## Use Cases
- Portfolio trading across multiple selections
- Market-wide risk management
- Dutching strategy position management
- Overall session profit targeting
- Multi-selection arbitrage management

## Examples
- Close all positions when market shows £100 profit
- Exit market when total loss reaches 5% of stakes
- Close entire portfolio 10 minutes before event
- Market-wide stop loss at £50 loss
- Take profit on portfolio at 15% gain

## Benefits
- **Holistic View**: See entire market performance
- **Risk Control**: Market-level risk management
- **Efficiency**: Single strategy manages multiple positions
- **Flexibility**: Percentage or absolute targets

## Portfolio Scenarios
- **Dutching**: Manage multiple back bets as portfolio
- **Arbitrage**: Monitor overall arbitrage profit
- **Market Making**: Track spread trading results
- **Hedging**: Manage hedge position performance

## Risk Management
- Monitor total market exposure
- Set appropriate profit and loss limits
- Consider correlation between selections
- Plan for partial position scenarios

## Advanced Applications
- Multi-market portfolio management
- Cross-market arbitrage monitoring
- Session-level profit targeting
- Risk-adjusted position sizing
