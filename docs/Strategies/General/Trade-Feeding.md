# Trade Feeding

**Category:** General Strategy  
**Strategy ID:** 1003

## Description

A sophisticated trading strategy that systematically "feeds" small amounts into the market to test liquidity and optimize trading positions. This strategy combines bet placement with profit/loss management and selection targeting.

## Parameters

### Bet
- **BetType** (Optional) - Specifies the type of your bet: 'Back' or 'Lay'
  - Options: Back, Lay

### Stake
- **Stake** (Optional) - Enter the stake amount for your bet. Note that when laying, your liability is calculated differently; to specify liability as your stake, set the 'StakeType' parameter to 'Liability' (Double)

### Profit/Loss
- **Profit** (Optional) - Set your profit target to specify when your bet position should close (Int32)
- **Loss** (Optional) - Set your loss target to specify when your bet position should close (Int32)

### Selection
- **SortSelectionsBy** (Optional) - Use this setting to sort selections by either last traded price or total matched volume
  - Options: DoNotSort, LastPriceTraded, TotalMatched
- **ExecuteOnSelection** (Optional) - The selection on which you want to execute your bot. If set to 0 the strategy is executed on the active selection in the market grid view. If set to 1 .. X, the strategy is executed on X favorite or X selection depending on how you set the parameter SortSelectionsBy (Byte)

### Market
- **StopBotExecutionOnMarketVersionChange** (Optional) - Set to True if you want to stop the strategy execution when a market version changes. The strategy execution will be stopped only when no bet placed by the strategy was matched (Boolean)
- **EvaluateEntryCriteriaOnlyOnce** (Optional) - Set this parameter to True to evaluate the entry criteria only once
- **StopMarketMonitoring** (Optional) - Set this parameter to True to stop market monitoring after the strategy completes execution. Monitoring will stop only if no other bot is active in the market

### Miscellaneous
- **StrategyReference** (Optional) - Set this parameter to specify which strategy sent the bet. The string is limited to 15 characters

## Usage Examples

### Liquidity Testing
- **BetType**: `Back`
- **Stake**: `2.0`
- **Profit**: `2`
- **Loss**: `5`
- **ExecuteOnSelection**: `1`

### Market Feeding Strategy
- **BetType**: `Lay`
- **Stake**: `5.0`
- **SortSelectionsBy**: `LastPriceTraded`
- **Profit**: `3`

### Conservative Approach
- **BetType**: `Back`
- **Stake**: `1.0`
- **Profit**: `1`
- **Loss**: `2`
- **StopBotExecutionOnMarketVersionChange**: `true`

## Best Practices

1. **Small Stakes**: Use small stakes to test market response
2. **Quick Profits**: Set achievable profit targets (1-3 ticks)
3. **Tight Stops**: Use strict loss limits to control risk
4. **Liquidity Assessment**: Monitor market response to feeding attempts
5. **Market Selection**: Choose liquid markets for better execution
6. **Position Management**: Monitor overall exposure across feeding attempts
