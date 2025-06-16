# Trailing Stop Loss on Market

## Overview
The **Trailing Stop Loss on Market** strategy implements a market-wide trailing stop loss mechanism that protects profitable positions across the entire market. This advanced strategy monitors the overall market profit/loss and closes positions when trailing conditions are met.

## Strategy Details
- **Category**: Trading
- **Type**: Market-Level Risk Management
- **Purpose**: Implement trailing stop loss across entire market position

## Parameters

### Profit/Loss Parameters
| Parameter | Group | Type | Required | Description |
|-----------|--------|------|----------|-------------|
| Loss | Profit/Loss | Double | Yes | Trailing stop loss amount to prevent further loss on profitable position |
| ProfitOrLossInPercentage | Profit/Loss | Boolean | No | Set to True to use percentage values for profit and loss targets |

### Bet Attributes
| Parameter | Group | Type | Required | Description |
|-----------|--------|------|----------|-------------|
| WaitForValidBetPosition | Bet Attribute | Boolean | No | Set to True to wait for valid bet position to open |
| BetMatchingTimeout | Bet Attribute | TimeSpan | No | Timeout for bet matching before cancellation |

### Timing Parameters
| Parameter | Group | Type | Required | Description |
|-----------|--------|------|----------|-------------|
| ClosePositionTimeSpan | Time | TimeSpan | No | Close position at specific time relative to event start |

### Debug Parameters
| Parameter | Group | Type | Required | Description |
|-----------|--------|------|----------|-------------|
| ShowDebugInfo | Miscellaneous | Boolean | No | Show debug information for diagnostic purposes |

### Market Parameters
| Parameter | Group | Type | Required | Description |
|-----------|--------|------|----------|-------------|
| EvaluateEntryCriteriaOnlyOnce | Market | Boolean | No | Evaluate entry criteria only once |
| StopMarketMonitoring | Market | Boolean | No | Stop market monitoring after completion |

### Miscellaneous Parameters
| Parameter | Group | Type | Required | Description |
|-----------|--------|------|----------|-------------|
| StrategyReference | Miscellaneous | String | No | Strategy reference identifier (max 15 characters) |

## Usage Examples

### Basic Market Trailing Stop
```
Strategy: Trailing Stop Loss on Market
Loss: 10.0
ProfitOrLossInPercentage: False
WaitForValidBetPosition: True
EvaluateEntryCriteriaOnlyOnce: True
```

### Percentage-Based Trailing Stop
```
Strategy: Trailing Stop Loss on Market
Loss: 15.0
ProfitOrLossInPercentage: True
BetMatchingTimeout: 00:00:30
ShowDebugInfo: True
StrategyReference: "TSL_MARKET"
```

### Timed Market Stop Loss
```
Strategy: Trailing Stop Loss on Market
Loss: 25.0
ClosePositionTimeSpan: -0:05:00
WaitForValidBetPosition: True
ShowDebugInfo: False
StopMarketMonitoring: True
```

## Trailing Stop Mechanics

### Market-Level Monitoring
- **Overall Position**: Monitors entire market position
- **Profit Tracking**: Tracks best profit achieved across market
- **Loss Protection**: Protects against profit deterioration
- **Dynamic Adjustment**: Adjusts stop level as profit increases

### Trailing Logic
1. **Position Establishment**: Wait for valid market position
2. **Profit Monitoring**: Track maximum profit achieved
3. **Trailing Adjustment**: Adjust stop loss as profit increases
4. **Trigger Execution**: Close position when trailing stop hit
5. **Position Closure**: Execute market-wide position closure

### Stop Loss Calculation
- **Fixed Amount**: Absolute loss amount from peak profit
- **Percentage-Based**: Percentage loss from peak profit
- **Dynamic Trailing**: Stop level follows profit increases
- **Minimum Protection**: Ensure minimum profit protection

## Risk Management Features

### Position Protection
- **Profit Preservation**: Protect achieved profits
- **Downside Limitation**: Limit potential losses
- **Risk Control**: Systematic risk management
- **Automated Execution**: Remove emotional trading decisions

### Market-Wide Coverage
- **Complete Protection**: Protect entire market exposure
- **Unified Management**: Single strategy for all positions
- **Consistent Application**: Consistent stop loss across selections
- **Comprehensive Monitoring**: Monitor all market positions

### Advanced Controls
- **Time-Based Stops**: Additional time-based protection
- **Bet Matching Control**: Control bet execution timing
- **Debug Monitoring**: Detailed execution monitoring
- **Flexible Configuration**: Adaptable to various scenarios

## Best Practices

1. **Position Validation**: Ensure valid positions before activation
2. **Appropriate Loss Levels**: Set realistic loss thresholds
3. **Market Awareness**: Consider market-specific characteristics
4. **Performance Monitoring**: Monitor strategy effectiveness
5. **Debug Analysis**: Use debug mode for optimization

## Integration

This strategy works well with:
- Market-wide trading strategies
- Portfolio management systems
- Risk management frameworks
- Position monitoring tools

## Use Cases

### Portfolio Protection
- **Multi-Selection Portfolios**: Protect complex portfolios
- **Market-Wide Positions**: Manage entire market exposure
- **Risk Limitation**: Limit overall market risk
- **Profit Preservation**: Preserve portfolio profits

### Systematic Trading
- **Automated Risk Management**: Systematic stop loss execution
- **Consistent Application**: Uniform risk management
- **Emotional Discipline**: Remove emotional trading decisions
- **Performance Optimization**: Optimize risk-adjusted returns

### Market Making
- **Liquidity Provision**: Protect market making activities
- **Inventory Management**: Manage inventory risk
- **Profit Protection**: Protect market making profits
- **Risk Control**: Control overall exposure

## Advanced Applications

### Dynamic Stop Adjustment
- **Volatility-Based**: Adjust stops based on market volatility
- **Time-Based**: Modify stops based on time factors
- **Market-Based**: Adapt to market conditions
- **Performance-Based**: Adjust based on strategy performance

### Multi-Timeframe Analysis
- **Short-term Protection**: Immediate profit protection
- **Medium-term Management**: Strategic position management
- **Long-term Preservation**: Capital preservation focus
- **Adaptive Timeframes**: Dynamic timeframe adjustment

## Performance Considerations

### Execution Efficiency
- **Fast Execution**: Rapid stop loss execution
- **Market Impact**: Minimize market impact
- **Slippage Control**: Control execution slippage
- **Cost Management**: Manage execution costs

### Monitoring Requirements
- **Real-time Tracking**: Continuous position monitoring
- **Performance Analysis**: Ongoing performance evaluation
- **Risk Assessment**: Continuous risk evaluation
- **Strategy Adjustment**: Periodic strategy refinement

## Notes

- This is an advanced risk management strategy for market-wide positions
- Monitors and protects entire market position, not individual selections
- Trailing stop follows profit increases to maximize profit capture
- Can use either fixed amounts or percentage-based calculations
- Includes time-based stop options for additional protection
- Requires careful configuration for optimal performance
- Essential for systematic risk management in complex trading strategies
