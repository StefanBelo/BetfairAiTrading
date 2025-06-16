# Trailing Stop Loss Strategy

## Overview
This strategy protects profitable positions by automatically closing them if they lose a specified number of ticks from their best profit level.

## Category
Trading

## Description
A dynamic risk management strategy that trails profitable positions and closes them if they give back a predetermined number of ticks from their peak profit. This allows profits to run while protecting against giving back gains.

## Parameters

### Profit/Loss
- **Loss** (Required): Number of ticks to prevent further loss from a profitable position

### Stake Attribute
- **HedgingEnabled** (Optional): Enable hedging of the bet position

### Bet Attribute
- **BetMatchingTimeout** (Optional): Cancel closing bet if not matched within timeout

### Selection
- **SortSelectionsBy** (Optional): Sort selections by LastPriceTraded, TotalMatched, or DoNotSort
- **ExecuteOnSelection** (Optional): Specify which selection to execute on

### Market
- **StopBotExecutionOnMarketVersionChange** (Optional): Stop strategy when market version changes
- **EvaluateEntryCriteriaOnlyOnce** (Optional): Evaluate entry criteria only once
- **StopMarketMonitoring** (Optional): Stop market monitoring after strategy completion

### Miscellaneous
- **StrategyReference** (Optional): Custom reference string for the strategy (max 15 characters)

## Key Features
- **Profit Protection**: Locks in gains while allowing further upside
- **Dynamic Trailing**: Adjusts stop level as profits increase
- **Tick-Based Control**: Precise control over profit give-back
- **Position Monitoring**: Continuous monitoring of best profit achieved

## How Trailing Works
1. **Initial Position**: Monitor for profitability
2. **Profit Achievement**: Record best profit level
3. **Trail Adjustment**: Update stop level as profits increase
4. **Trigger**: Close if position gives back specified ticks
5. **Protection**: Only triggers while still in profit

## Example Scenarios
- **Entry**: Back at 3.0 with 3-tick trailing stop
- **Profit**: Price moves to 2.5 (profitable)
- **Trail**: Stop set at 2.8 (3 ticks from best)
- **Movement**: Price improves to 2.2
- **New Trail**: Stop moves to 2.5 (3 ticks from new best)
- **Trigger**: Close if price moves back to 2.5

## Use Cases
- Protecting profits in trending markets
- Swing trading with profit protection
- Letting winners run with downside protection
- Dynamic risk management
- Momentum trading strategies

## Benefits
- **Profit Maximization**: Allows profits to grow
- **Risk Control**: Protects against profit erosion
- **Automatic Management**: No manual intervention needed
- **Flexible Protection**: Adjusts with market movements

## Risk Considerations
- Only works on profitable positions
- Requires price movement to establish trail
- Can be triggered by temporary retracements
- Best suited for trending markets

## Strategy Combinations
- Use with position opening strategies
- Combine with time-based exits
- Integrate with technical indicators
- Part of larger trading systems
