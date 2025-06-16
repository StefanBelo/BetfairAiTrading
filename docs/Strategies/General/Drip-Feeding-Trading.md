# Drip Feeding Trading

## Overview
The Drip Feeding Trading strategy combines drip feeding bet placement with trailing stop loss functionality. It gradually builds a trading position and then manages it with automated profit protection using a trailing stop mechanism.

## Strategy Type
**Category:** General Strategy  
**Execution:** Trading with Position Management  
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
| ChaseOddsTimeout | Bet Attribute | TimeSpan | No | If your bet is not fully matched and the odds change, the strategy will chase the new offered odds |

### Stake Configuration
| Parameter | Group | Type | Required | Description |
|-----------|--------|------|----------|-------------|
| Stake | Stake | Double | No | Enter the stake amount for your bet |
| StakeType | Stake Attribute | Enum | No | Stake type: Stake, Liability, TickProfit, NetTickProfit |

### Timing
| Parameter | Group | Type | Required | Description |
|-----------|--------|------|----------|-------------|
| PlaceBetTimeSpan | Time | TimeSpan | No | Set this parameter to determine when your bet is placed, relative to the official event start time |

### Profit/Loss Management
| Parameter | Group | Type | Required | Description |
|-----------|--------|------|----------|-------------|
| Loss | Profit/Loss | Int32 | No | The number of ticks to prevent further loss of a profitable bet position. Once a bet position loses its best profit position then your bet position is closed, but only when in a profit |

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

1. **Drip Feed Entry**: Gradually builds a trading position using drip feeding technique
2. **Position Monitoring**: Continuously monitors the position for profit and loss
3. **Trailing Stop**: Implements a trailing stop loss that moves with profitable price movements
4. **Automatic Exit**: Closes the position when the trailing stop is triggered or maximum loss is reached

## Trading Process

1. **Entry Phase**: 
   - Places initial small bets using drip feeding
   - Builds position gradually to avoid market impact
   - Monitors odds movement and adjusts timing

2. **Management Phase**:
   - Tracks position profit/loss in real-time
   - Adjusts trailing stop as profit increases
   - Protects against sudden adverse movements

3. **Exit Phase**:
   - Closes position when trailing stop is hit
   - Ensures profit protection while allowing for growth

## Use Cases

- **Scalping**: Quick trades with small but consistent profits
- **Market Making**: Providing liquidity while managing risk
- **Trend Following**: Building positions gradually in trending markets
- **Risk Management**: Professional trading with automated exit strategies

## Example Configuration

```json
{
  "BetType": "Back",
  "Stake": 50.0,
  "StakeType": "Stake",
  "MinimumOdds": 1.8,
  "MaximumOdds": 3.0,
  "Loss": 3,
  "PlaceBetTimeSpan": "-00:10:00",
  "ExecuteOnSelection": 1
}
```

## Best Practices

1. **Position Sizing**: Start with smaller stakes to understand market behavior
2. **Trailing Distance**: Set appropriate trailing stop distance based on market volatility
3. **Market Timing**: Consider market liquidity and volatility when timing entries
4. **Risk Management**: Always set maximum loss limits
5. **Monitoring**: Actively monitor the strategy during execution

## Risk Considerations

- **Market Volatility**: High volatility can trigger stops prematurely
- **Liquidity Risk**: Ensure sufficient market liquidity for both entry and exit
- **Slippage**: Account for potential slippage in fast-moving markets
- **Timing Risk**: Market conditions can change during the drip feeding process

## Related Strategies

- [Place Bet By Drip Feeding](Place-Bet-By-Drip-Feeding.md) - Basic drip feeding
- [Trailing Stop Loss](../Trading/Trailing-Stop-Loss-on-Market.md) - Stop loss management
- [Trade Feeding](Trade-Feeding.md) - Alternative feeding approach

## Tips

- Test with small stakes first to understand the strategy behavior
- Monitor how drip feeding affects market odds
- Adjust trailing stop distance based on market conditions
- Consider pre-event vs in-play market characteristics
- Use appropriate chase odds timeout for volatile markets
