# My Back Strategy

## Overview
My Back Strategy is a comprehensive backing strategy that uses multiple market indicators and price movement triggers to identify optimal back betting opportunities. This strategy analyzes various market conditions and price movements to determine when to place back bets.

## Strategy Type
- **Category**: Custom/Unknown
- **ID**: 3001
- **Type**: Back Betting Strategy

## Parameters

### Trigger Parameters
| Parameter | Group | Type | Required | Description |
|-----------|-------|------|----------|-------------|
| PositiveDifference | Trigger | Double | No | +D min (e.g. back above 4>) |
| UsePositiveDifference | Trigger | Boolean | No | Use +D parameter. |
| PositiveOffDifference | Trigger | Double | No | +OD min (e.g. back above 6>) |
| UsePositiveOffDifference | Trigger | Boolean | No | Use +OD parameter. |
| PositiveJumpMin | Trigger | Double | No | +J min (e.g. back between 0.30> and <1.35) |
| PositiveJumpMax | Trigger | Double | No | +J max (e.g. back between 0.30> and <1.35) |
| UsePositiveJump | Trigger | Boolean | No | Use +J parameter. |
| NegativeJumpMin | Trigger | Double | No | -J min (e.g. back between -0.01> and <-2.5) |
| NegativeJumpMax | Trigger | Double | No | -J max (e.g. back between -0.01> and <-2.5) |
| UseNegativeJump | Trigger | Boolean | No | Use -J parameter. |
| PositiveMovementMin | Trigger | Double | No | +M min (e.g. back between +0.01> and <+7.5) |
| PositiveMovementMax | Trigger | Double | No | +M max (e.g. back between +0.01> and <+7.5) |
| UsePositiveMovement | Trigger | Boolean | No | Use +M parameter. |
| NegativeMovementMin | Trigger | Double | No | -M min (e.g. back between -2.5> and <-15) |
| NegativeMovementMax | Trigger | Double | No | -M max (e.g. back between -2.5> and <-15) |
| UseNegativeMovement | Trigger | Boolean | No | Use -M parameter. |
| PositiveMovementPercentageMin | Trigger | Double | No | +P min (e.g. back min 40%>) |
| PositiveMovementPercentageMax | Trigger | Double | No | +P max (e.g. back min 40%>) |
| UsePositiveMovementPercentage | Trigger | Boolean | No | Use +P parameter. |
| NegativeMovementPercentage | Trigger | Double | No | -P +max (e.g. back to max >-20%) |
| UseNegativeMovementPercentage | Trigger | Boolean | No | Use -P parameter. |
| MinimumBsp | Trigger | Double | No | Minimum betfair starting price. |
| MaximumBsp | Trigger | Double | No | Maximum betfair starting price. |
| MinimumRunners | Trigger | Int32 | No | Minimum number of runners. |
| MaximumRunners | Trigger | Int32 | No | Maximum number of runners. |

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
This strategy is designed for backing selections based on multiple price movement indicators and market conditions. It provides comprehensive trigger options for different market scenarios.

## Key Features
- **Multiple Triggers**: Various price movement and market condition triggers
- **Flexible Configuration**: Enable/disable different trigger types as needed
- **Price Movement Analysis**: Monitors positive and negative price movements
- **Jump Detection**: Identifies sudden price jumps in either direction
- **Percentage Movement**: Tracks percentage-based price changes
- **BSP Integration**: Uses Betfair Starting Price criteria
- **Runner Count Control**: Considers minimum and maximum number of runners

## Trigger Types

### Difference Triggers
- **Positive Difference (+D)**: Back when price is above specified threshold
- **Positive Off Difference (+OD)**: Extended positive difference criteria

### Jump Triggers
- **Positive Jump (+J)**: Back when sudden positive price jump occurs within range
- **Negative Jump (-J)**: Back when negative price jump occurs within range

### Movement Triggers
- **Positive Movement (+M)**: Back on positive price movement within range
- **Negative Movement (-M)**: Back on negative price movement within range
- **Percentage Movement (+P/-P)**: Back based on percentage price changes

### Market Filters
- **BSP Range**: Filter by Betfair Starting Price range
- **Runner Count**: Control market participation based on field size

## Configuration Tips
1. Enable only the triggers relevant to your strategy
2. Set appropriate ranges for jump and movement parameters
3. Use BSP filters to target specific types of selections
4. Set runner count limits to avoid unsuitable markets
5. Test trigger combinations thoroughly before live use

## Best Practices
- Start with conservative trigger settings
- Monitor strategy performance across different market types
- Use appropriate stake sizing for risk management
- Combine with position management strategies
- Keep detailed records of trigger effectiveness

## Strategy Logic
The strategy evaluates enabled triggers and places back bets when conditions are met:
1. Check each enabled trigger condition
2. Verify BSP and runner count criteria
3. Place back bet if all conditions satisfied
4. Monitor position if bet is placed

## Risk Management
- Set appropriate maximum odds to limit risk
- Use position sizing based on confidence level
- Monitor market liquidity before placing bets
- Implement stop-loss mechanisms where appropriate

## Related Strategies
- My Lay Strategy
- Close Selection Bet Position
- Tick Offset
- Place Bet

## Support
This is a comprehensive backing strategy with multiple trigger options. Test thoroughly in different market conditions before live implementation.
