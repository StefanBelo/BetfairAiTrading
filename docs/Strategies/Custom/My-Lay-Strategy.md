# My Lay Strategy

## Overview
My Lay Strategy is a comprehensive laying strategy that uses multiple market indicators and price movement triggers to identify optimal lay betting opportunities. This strategy analyzes various market conditions and price movements to determine when to place lay bets.

## Strategy Type
- **Category**: Custom/Unknown
- **ID**: 3000
- **Type**: Lay Betting Strategy

## Parameters

### Trigger Parameters
| Parameter | Group | Type | Required | Description |
|-----------|-------|------|----------|-------------|
| PositiveDifference | Trigger | Double | No | +D min (e.g. lay above 4>) |
| UsePositiveDifference | Trigger | Boolean | No | Use +D parameter. |
| PositiveOffDifference | Trigger | Double | No | +OD min (e.g. lay above 6>) |
| UsePositiveOffDifference | Trigger | Boolean | No | Use +OD parameter. |
| PositiveJumpMin | Trigger | Double | No | +J min (e.g. lay between 0.30> and <1.35) |
| PositiveJumpMax | Trigger | Double | No | +J max (e.g. lay between 0.30> and <1.35) |
| UsePositiveJump | Trigger | Boolean | No | Use +J parameter. |
| NegativeJumpMin | Trigger | Double | No | -J min (e.g. lay between -0.01> and <-2.5) |
| NegativeJumpMax | Trigger | Double | No | -J max (e.g. lay between -0.01> and <-2.5) |
| UseNegativeJump | Trigger | Boolean | No | Use -J parameter. |
| PositiveMovementMin | Trigger | Double | No | +M min (e.g. lay between +0.01> and <+7.5) |
| PositiveMovementMax | Trigger | Double | No | +M max (e.g. lay between +0.01> and <+7.5) |
| UsePositiveMovement | Trigger | Boolean | No | Use +M parameter. |
| NegativeMovementMin | Trigger | Double | No | -M min (e.g. lay between -2.5> and <-15) |
| NegativeMovementMax | Trigger | Double | No | -M max (e.g. lay between -2.5> and <-15) |
| UseNegativeMovement | Trigger | Boolean | No | Use -M parameter. |
| PositiveMovementPercentageMin | Trigger | Double | No | +P min (e.g. lay min 40%>) |
| PositiveMovementPercentageMax | Trigger | Double | No | +P max (e.g. lay min 40%>) |
| UsePositiveMovementPercentage | Trigger | Boolean | No | Use +P parameter. |
| NegativeMovementPercentage | Trigger | Double | No | -P +max (e.g. lay to max >-20%) |
| UseNegativeMovementPercentage | Trigger | Boolean | No | Use -P parameter. |
| MinimumBsp | Trigger | Double | No | Minimum betfair starting price. |
| MaximumBsp | Trigger | Double | No | Maximum betfair starting price. |
| MinimumRunners | Trigger | Int32 | No | Minimum number of runners. |

### Bet Parameters
| Parameter | Group | Type | Required | Description |
|-----------|-------|------|----------|-------------|
| Odds | Bet | Double | No | Specify the odds you wish to bet on. To place a bet on exact odds, set the 'PlaceBetInAllowedOddsRange' parameter to 'False'. |

### Stake Parameters
| Parameter | Group | Type | Required | Description |
|-----------|-------|------|----------|-------------|
| Stake | Stake | Double | No | Enter the stake amount for your bet. Note that when laying, your liability is calculated differently; to specify liability as your stake, set the 'StakeType' parameter to 'Liability'. |

### Market Parameters
| Parameter | Group | Type | Required | Description |
|-----------|-------|------|----------|-------------|
| EvaluateEntryCriteriaOnlyOnce | Market | Boolean | No | Set this parameter to True to evaluate the entry criteria only once. |
| StopMarketMonitoring | Market | Boolean | No | Set this parameter to True to stop market monitoring after the strategy completes execution. Monitoring will stop only if no other bot is active in the market. |

### Miscellaneous Parameters
| Parameter | Group | Type | Required | Description |
|-----------|-------|------|----------|-------------|
| StrategyReference | Miscellaneous | String | No | Set this parameter to specify which strategy sent the bet. The string is limited to 15 characters. |

## Usage
This strategy is designed for laying selections based on multiple price movement indicators and market conditions. It provides comprehensive trigger options for different market scenarios.

## Key Features
- **Multiple Triggers**: Various price movement and market condition triggers
- **Flexible Configuration**: Enable/disable different trigger types as needed
- **Price Movement Analysis**: Monitors positive and negative price movements
- **Jump Detection**: Identifies sudden price jumps in either direction
- **Percentage Movement**: Tracks percentage-based price changes
- **BSP Integration**: Uses Betfair Starting Price criteria
- **Runner Count**: Considers number of runners in trigger logic

## Trigger Types

### Difference Triggers
- **Positive Difference (+D)**: Lay when price is above specified threshold
- **Positive Off Difference (+OD)**: Extended positive difference criteria

### Jump Triggers
- **Positive Jump (+J)**: Lay when sudden positive price jump occurs within range
- **Negative Jump (-J)**: Lay when negative price jump occurs within range

### Movement Triggers
- **Positive Movement (+M)**: Lay on positive price movement within range
- **Negative Movement (-M)**: Lay on negative price movement within range
- **Percentage Movement (+P/-P)**: Lay based on percentage price changes

## Configuration Tips
1. Enable only the triggers relevant to your strategy
2. Set appropriate ranges for jump and movement parameters
3. Use BSP filters to target specific types of selections
4. Consider minimum runners to avoid thin markets
5. Test trigger combinations thoroughly before live use

## Best Practices
- Start with conservative trigger settings
- Monitor strategy performance across different market types
- Use appropriate stake sizing for lay liability management
- Combine with position management strategies
- Keep detailed records of trigger effectiveness

## Risk Management
- Lay betting involves unlimited liability - manage stakes carefully
- Set maximum odds limits to control potential losses
- Monitor market liquidity before placing lay bets
- Use stop-loss mechanisms where appropriate

## Related Strategies
- My Back Strategy
- Close Selection Bet Position
- Trailing Stop Loss
- Place Bet

## Support
This is a comprehensive laying strategy with multiple trigger options. Test thoroughly and understand lay betting risks before live implementation.
