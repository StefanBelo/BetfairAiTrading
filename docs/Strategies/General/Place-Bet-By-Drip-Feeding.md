# Place Bet By Drip Feeding

## Overview
The Place Bet By Drip Feeding strategy allows you to gradually place bets over time by drip feeding smaller stakes instead of placing one large bet all at once. This approach can help reduce market impact and achieve better average odds.

## Strategy Type
**Category:** General Strategy  
**Execution:** Bet Placement with Drip Feeding  
**Market Timing:** Pre-event and In-play

## Parameters

### Bet Configuration
| Parameter | Group | Type | Required | Description |
|-----------|--------|------|----------|-------------|
| BetType | Bet | Enum | No | Specifies the type of your bet: 'Back' or 'Lay' |
| MinimumOdds | Bet | Double | No | Specify the minimum odds you are willing to accept |
| MaximumOdds | Bet | Double | No | Specify the maximum odds you are willing to accept |

### Bet Attributes
| Parameter | Group | Type | Required | Description |
|-----------|--------|------|----------|-------------|
| ChaseOddsTimeout | Bet Attribute | TimeSpan | No | If your bet is not fully matched and the odds change, the strategy will chase the new offered odds. You can postpone this odds chasing by setting this timeout parameter |

### Stake Configuration
| Parameter | Group | Type | Required | Description |
|-----------|--------|------|----------|-------------|
| Stake | Stake | Double | No | Enter the stake amount for your bet. Note that when laying, your liability is calculated differently |
| StakeType | Stake Attribute | Enum | No | Stake type: Stake, Liability, TickProfit, NetTickProfit |

### Timing
| Parameter | Group | Type | Required | Description |
|-----------|--------|------|----------|-------------|
| PlaceBetTimeSpan | Time | TimeSpan | No | Set this parameter to determine when your bet is placed, relative to the official event start time |

### Selection
| Parameter | Group | Type | Required | Description |
|-----------|--------|------|----------|-------------|
| SortSelectionsBy | Selection | Enum | No | Use this setting to sort selections by either last traded price or total matched volume |
| ExecuteOnSelection | Selection | Byte | No | The selection on which you want to execute your bot |

### Market Control
| Parameter | Group | Type | Required | Description |
|-----------|--------|------|----------|-------------|
| StopBotExecutionOnMarketVersionChange | Market | Boolean | No | Set to True if you want to stop the strategy execution when a market version changes |
| EvaluateEntryCriteriaOnlyOnce | Market | Boolean | No | Set this parameter to True to evaluate the entry criteria only once |
| StopMarketMonitoring | Market | Boolean | No | Set this parameter to True to stop market monitoring after the strategy completes execution |

### Miscellaneous
| Parameter | Group | Type | Required | Description |
|-----------|--------|------|----------|-------------|
| StrategyReference | Miscellaneous | String | No | Set this parameter to specify which strategy sent the bet. The string is limited to 15 characters |

## How It Works

1. **Drip Feeding Process**: The strategy splits your total stake into smaller portions and places them gradually over time
2. **Market Impact Reduction**: By placing smaller bets, you reduce the impact on market liquidity and odds movement
3. **Better Odds**: Can potentially achieve better average odds by spreading the betting action
4. **Timing Control**: You can control when the drip feeding process starts relative to the event start time

## Use Cases

- **Large Stakes**: When you want to place a large bet without moving the market significantly
- **Volatile Markets**: In markets where odds are changing rapidly
- **Liquidity Management**: When market liquidity is limited and you need to avoid large price movements
- **Professional Trading**: For traders who want to minimize market impact

## Example Configuration

```json
{
  "BetType": "Back",
  "Stake": 100.0,
  "StakeType": "Stake",
  "MinimumOdds": 2.0,
  "MaximumOdds": 5.0,
  "PlaceBetTimeSpan": "-00:05:00",
  "ExecuteOnSelection": 1
}
```

## Best Practices

1. **Stake Size**: Consider the total market liquidity when setting your stake amount
2. **Timing**: Allow sufficient time for the drip feeding process to complete
3. **Odds Monitoring**: Monitor how your betting affects the odds and adjust accordingly
4. **Market Conditions**: Use drip feeding when markets have limited liquidity
5. **Risk Management**: Set appropriate minimum and maximum odds to control risk

## Related Strategies

- [Place Bet](Place-Bet.md) - Standard bet placement
- [Drip Feeding Trading](Drip-Feeding-Trading.md) - Trading with drip feeding
- [Trade Feeding](Trade-Feeding.md) - Alternative feeding strategy

## Tips

- Start with smaller stakes to test the drip feeding effect
- Monitor market depth before implementing large drip feeds
- Consider market timing and volatility
- Use chase odds timeout to manage rapid odds changes
