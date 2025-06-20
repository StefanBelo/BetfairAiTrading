# Execute on a Selection Strategy

## Overview

The "Execute on a Selection" strategy is a powerful conditional execution tool that belongs to the General Strategy category in Bfexplorer. This strategy allows you to execute another strategy on a selection only when specific market data criteria are met, providing sophisticated automated decision-making capabilities based on real-time market conditions.

## Purpose

This strategy acts as an intelligent trigger mechanism that evaluates market data in real-time and executes a specified strategy when predefined conditions are satisfied. It's ideal for creating data-driven trading systems that respond to market dynamics automatically.

## Key Features

- **Conditional Execution**: Execute strategies based on complex Boolean expressions
- **Real-time Market Analysis**: Evaluate multiple market data points simultaneously
- **Flexible Criteria**: Support for various data types including prices, volumes, and positions
- **Automated Decision Making**: Eliminate manual monitoring and intervention
- **Risk Management**: Execute strategies only when optimal conditions are present

## Parameters

### Required Parameters

**SelectionCriteria** (String)
- **Description**: Boolean expression that defines when the strategy should execute
- **Usage**: Combine multiple conditions using logical operators (AND, OR, NOT)
- **Example**: `LastPriceTraded > 2.0 AND BestOfferedAmountToBack > 100`

**StrategyName** (String)
- **Description**: Name of the strategy to execute when criteria are met
- **Usage**: Must reference an existing, saved strategy in Bfexplorer
- **Example**: `"My Backing Strategy"`

### Optional Parameters

**SortSelectionsBy** (Enum)
- **Description**: Determines how selections are sorted for evaluation
- **Values**:
  - `DoNotSort`: Use natural order
  - `LastPriceTraded`: Sort by current price
  - `TotalMatched`: Sort by trading volume
- **Default**: DoNotSort

**EvaluateEntryCriteriaOnlyOnce** (Boolean)
- **Description**: If True, criteria are evaluated only once at strategy start
- **Usage**: Set to True for one-time execution, False for continuous monitoring
- **Default**: False

**StopMarketMonitoring** (Boolean)
- **Description**: Stop monitoring market after strategy execution completes
- **Usage**: Helps conserve resources when no other strategies are active
- **Default**: False

**StrategyReference** (String)
- **Description**: Custom reference identifier for tracking (max 15 characters)
- **Usage**: Useful for logging and identifying strategy executions
- **Example**: `"EOS_001"`

## Available Data Fields for Criteria

### Boolean Fields

**SelectionIsValidBetPosition**
- Indicates if the selection has a valid betting position
- Use case: Execute only when a position exists

**SelectionCanCloseBetPosition**
- Indicates if an existing position can be closed
- Use case: Execute exit strategies when positions are closeable

### Numeric Fields

**SelectionProfitBalance** (Double)
- Current profit/loss for the selection
- Use case: Execute based on profit thresholds

**LastPriceTraded** (Double)
- Most recent traded price for the selection
- Use case: Price-based strategy triggers

**BestOfferedAmountToBack** (Double)
- Highest amount available to back at best odds
- Use case: Liquidity-based execution

**BestOfferedAmountToLay** (Double)
- Highest amount available to lay at best odds
- Use case: Ensure sufficient lay liquidity

**OfferedAmountToBack** (Double)
- Total amount offered for backing
- Use case: Overall market depth analysis

**OfferedAmountToLay** (Double)
- Total amount offered for laying
- Use case: Market liquidity assessment

**PriceTrend** (Int32)
- Price movement indicator (-1, 0, 1)
- Use case: Trend-following strategies

**FavouriteIndex** (Int32)
- Selection's position in market rankings (1 = favourite)
- Use case: Favourite-based strategies

**WeightOfMoney** (Double)
- Market sentiment indicator
- Use case: Sentiment-driven execution

## Criteria Expression Examples

### Basic Price Conditions

```
LastPriceTraded > 3.0
```
Execute when price is above 3.0

```
LastPriceTraded >= 2.0 AND LastPriceTraded <= 5.0
```
Execute when price is between 2.0 and 5.0

### Liquidity-Based Conditions

```
BestOfferedAmountToBack > 500 AND BestOfferedAmountToLay > 500
```
Execute when both sides have substantial liquidity

```
OfferedAmountToBack > OfferedAmountToLay * 2
```
Execute when back money significantly exceeds lay money

### Position-Based Conditions

```
SelectionIsValidBetPosition AND SelectionProfitBalance > 10
```
Execute when position exists and is profitable above £10

```
SelectionCanCloseBetPosition AND SelectionProfitBalance < -20
```
Execute when position can be closed and loss exceeds £20

### Market Position Conditions

```
FavouriteIndex == 1 AND WeightOfMoney > 0.6
```
Execute on favourite when money flow is positive

```
FavouriteIndex > 3 AND PriceTrend == 1
```
Execute on outsiders showing upward price movement

### Complex Multi-Criteria Conditions

```
(LastPriceTraded > 2.0 AND LastPriceTraded < 10.0) AND (BestOfferedAmountToBack > 100) AND (PriceTrend >= 0)
```
Execute when price is in range, has liquidity, and trend is neutral or positive

```
FavouriteIndex <= 2 OR (WeightOfMoney > 0.7 AND BestOfferedAmountToBack > 200)
```
Execute on top 2 favourites OR when strong money flow with good liquidity

## Use Cases

### Entry Signal Detection

Monitor market conditions and execute entry strategies when optimal opportunities arise:

```
LastPriceTraded > 4.0 AND PriceTrend == -1 AND BestOfferedAmountToLay > 300
```
Strategy: Execute backing strategy when price is above 4.0, trending down, with good lay liquidity

### Exit Signal Management

Automatically close positions when profit targets or stop losses are hit:

```
SelectionProfitBalance > 25 OR SelectionProfitBalance < -10
```
Strategy: Execute position closing strategy for 25+ profit or 10+ loss

### Liquidity-Based Execution

Execute strategies only when sufficient market depth is available:

```
BestOfferedAmountToBack > 1000 AND BestOfferedAmountToLay > 1000
```
Strategy: Execute high-volume strategy only in liquid markets

### Favourite Monitoring

Target specific market positions with conditional logic:

```
FavouriteIndex == 1 AND LastPriceTraded > 1.5 AND WeightOfMoney < 0.3
```
Strategy: Execute lay strategy on favourite when odds drift with negative sentiment

### Arbitrage Detection

Identify and act on pricing inefficiencies:

```
(OfferedAmountToBack > OfferedAmountToLay * 3) AND (PriceTrend == 1)
```
Strategy: Execute when backing volume dominates and price is rising

## Best Practices

### Criteria Design

- **Keep expressions simple**: Complex logic can be hard to debug and maintain
- **Test incrementally**: Start with basic conditions and add complexity gradually
- **Use parentheses**: Group related conditions for clarity
- **Consider edge cases**: Account for market anomalies and data gaps

### Performance Optimization

- **Limit field usage**: Only include necessary fields in expressions
- **Set appropriate evaluation frequency**: Use `EvaluateEntryCriteriaOnlyOnce` when suitable
- **Monitor resource usage**: Complex criteria consume more processing power

### Risk Management

- **Include safety checks**: Always verify position validity before execution
- **Set boundaries**: Use minimum/maximum price ranges to avoid extreme conditions
- **Test thoroughly**: Validate criteria logic in paper trading before live execution

### Strategy Integration

- **Prepare base strategies**: Ensure referenced strategies are properly configured and tested
- **Use meaningful names**: Choose descriptive strategy names for easy identification
- **Document criteria**: Maintain clear documentation of what each condition represents

## Common Patterns

### Trend Following
```
PriceTrend == 1 AND LastPriceTraded > 2.0 AND BestOfferedAmountToBack > 200
```

### Mean Reversion
```
PriceTrend == -1 AND FavouriteIndex <= 2 AND WeightOfMoney > 0.5
```

### Breakout Detection
```
LastPriceTraded > 5.0 AND PriceTrend == 1 AND OfferedAmountToBack > OfferedAmountToLay
```

### Support/Resistance
```
LastPriceTraded <= 2.0 AND PriceTrend == 0 AND BestOfferedAmountToLay > 500
```

## Troubleshooting

### Common Issues

**Criteria Never Trigger**
- Check field names are spelled correctly
- Verify logical operators are appropriate
- Ensure numeric ranges are realistic for the market

**Strategy Doesn't Execute**
- Confirm referenced strategy name exists and is correct
- Verify strategy is properly saved and accessible
- Check strategy parameters are valid

**Excessive Executions**
- Consider using `EvaluateEntryCriteriaOnlyOnce` for one-time triggers
- Add cooling-off periods or additional constraints
- Review criteria logic for unintended matches

### Testing Recommendations

1. **Use Market Replay**: Test criteria against historical data
2. **Start with Paper Trading**: Validate logic without financial risk
3. **Monitor Execution Logs**: Track when and why criteria trigger
4. **Gradual Complexity**: Begin with simple conditions and expand

## Integration with Other Strategies

### Sequential Execution

Combine with "Sequence Execution" strategy to create multi-step workflows:

1. Use "Execute on a Selection" as entry trigger
2. Follow with position management strategies
3. End with exit condition monitoring

### Conditional Branching

Pair with "If Then Else" strategy for complex decision trees:

1. Primary condition evaluation
2. Alternative strategy paths based on outcomes
3. Fallback execution strategies

### Profit Targeting

Integrate with "Execute Till Target Profit" for systematic profit accumulation:

1. Define entry criteria
2. Set profit targets
3. Repeat until overall target achieved

## Conclusion

The "Execute on a Selection" strategy provides sophisticated conditional execution capabilities that enable data-driven automated trading. By leveraging real-time market data and flexible Boolean expressions, traders can create intelligent systems that respond to market conditions automatically, improving timing and reducing manual intervention requirements.

Success with this strategy depends on thoughtful criteria design, thorough testing, and integration with complementary strategies to create comprehensive trading workflows.
