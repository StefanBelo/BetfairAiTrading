# Execute on a Selection - Quick Reference

## What it does
Executes a strategy on a selection only when specific market data criteria are met.

## When to use
- Data-driven automated trading
- Conditional strategy execution
- Real-time market monitoring
- Risk-managed position entry/exit

## Key Parameters
- **SelectionCriteria**: Boolean expression (e.g., `LastPriceTraded > 2.0 AND BestOfferedAmountToBack > 100`)
- **StrategyName**: Name of strategy to execute when criteria met

## Quick Examples

### Price-based execution
```
LastPriceTraded > 3.0 AND LastPriceTraded < 8.0
```

### Liquidity-based execution
```
BestOfferedAmountToBack > 500 AND BestOfferedAmountToLay > 500
```

### Position management
```
SelectionProfitBalance > 25 OR SelectionProfitBalance < -10
```

### Favourite monitoring
```
FavouriteIndex == 1 AND WeightOfMoney > 0.6
```

## Available Data Fields
- LastPriceTraded, BestOfferedAmountToBack/ToLay
- SelectionProfitBalance, SelectionIsValidBetPosition
- FavouriteIndex, WeightOfMoney, PriceTrend
- And more...

**[📖 Full Documentation](Execute-On-A-Selection.md)**
