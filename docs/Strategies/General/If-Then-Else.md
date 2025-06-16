# If Then Else Strategy

## Overview
This strategy provides conditional logic execution, running different strategies based on whether specified criteria evaluate to true or false.

## Category
General Strategy

## Description
A conditional strategy that implements if-then-else logic for strategy execution. It evaluates criteria and executes different strategies based on the results, enabling complex decision-making in automated trading systems.

## Parameters

### Strategy
- **IfThenCriteria** (Optional): Define conditions for executing the IfThenBotName strategy
- **IfThenBotName** (Optional): Strategy to execute when IfThenCriteria evaluates to True
- **IfElseCriteria** (Optional): Define conditions for executing the IfElseBotName strategy
- **IfElseBotName** (Optional): Strategy to execute when IfElseCriteria evaluates to True
- **EvaluateIfCriteriaOnlyOnce** (Optional): Evaluate 'If...' criteria only once instead of continuously

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
- **Conditional Logic**: Execute different strategies based on conditions
- **Flexible Criteria**: Define custom evaluation criteria
- **Multiple Paths**: Support for if-then and if-else logic branches
- **Continuous Monitoring**: Can evaluate criteria continuously or once

## Logic Flow
1. **Evaluate IfThenCriteria**: Check if first condition is met
2. **Execute IfThenBotName**: Run strategy if condition is true
3. **Evaluate IfElseCriteria**: Check alternative condition
4. **Execute IfElseBotName**: Run alternative strategy if condition is true
5. **Continue Monitoring**: Repeat unless set to evaluate once

## Criteria Examples
- **Price-based**: 'LastTradedPrice > 2.0'
- **Volume-based**: 'TotalMatched > 10000'
- **Time-based**: 'TimeToStart < 300' (5 minutes)
- **Market-based**: 'InPlay = True'
- **Position-based**: 'ProfitLoss > 0'

## Use Cases
- Market condition-based strategy selection
- Risk management with conditional exits
- Time-based strategy switching
- Volume-triggered strategy execution
- Price level-dependent trading

## Examples
- **If odds < 2.0 then Place Back Bet, Else Place Lay Bet**
- **If InPlay = True then Close Position, Else Continue Monitoring**
- **If Volume > 50000 then Scalp Strategy, Else Wait Strategy**
- **If Profit > 100 then Take Profit, Else Trail Stop**

## Advanced Applications
- **Multi-condition Logic**: Combine multiple criteria
- **Strategy Chains**: Link conditional strategies together
- **Market Adaptation**: Adapt to changing market conditions
- **Risk Triggers**: Implement conditional risk management

## Benefits
- **Intelligent Automation**: Automated decision making
- **Flexibility**: Adapt to various market conditions
- **Risk Management**: Conditional protection strategies
- **Efficiency**: Single strategy handles multiple scenarios
