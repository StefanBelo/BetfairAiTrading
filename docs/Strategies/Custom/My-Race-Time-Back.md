# My Race Time Back

## Overview
My Race Time Back is a specialized backing strategy designed for horse racing markets that uses race time predictions and various price movement indicators to identify backing opportunities. This strategy focuses on selections with favorable race time to industry price ratios.

## Strategy Type
- **Category**: Custom/Unknown
- **ID**: 3002
- **Type**: Horse Racing Back Strategy

## Parameters

### Trigger Parameters
| Parameter | Group | Type | Required | Description |
|-----------|-------|------|----------|-------------|
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
| MinimumRTW | Trigger | Double | No | Minimum race time price. |
| MaximumRTW | Trigger | Double | No | Maximum race time price. |
| MinimumRunners | Trigger | Byte | No | Minimum number of runners. |
| MaximumRunners | Trigger | Byte | No | Maximum number of runners. |
| MinimumAge | Trigger | Byte | No | Minimum horse age. |
| MaximumAge | Trigger | Byte | No | Maximum horse age. |
| MinimumRtwIndustryPercentage | Trigger | Double | No | Minimum race time / industry price percentage. |
| MaximumRtwIndustryPercentage | Trigger | Double | No | Maximum race time / industry price percentage. |

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
This strategy is specifically designed for horse racing markets and focuses on backing selections where race time predictions indicate value compared to industry prices and current market movements.

## Key Features
- **Race Time Analysis**: Uses race time predictions as primary selection criteria
- **Price Movement Monitoring**: Tracks positive and negative price movements
- **Percentage Movement**: Analyzes percentage-based price changes
- **Industry Price Comparison**: Compares race time predictions with industry prices
- **Age Filtering**: Considers horse age in selection criteria
- **Runner Count Control**: Filters by field size for optimal market conditions

## Race Time Integration
The strategy uses race time data to:
- **RTW Analysis**: Evaluates race time win predictions
- **Industry Comparison**: Compares RTW with industry pricing
- **Value Identification**: Identifies selections where RTW suggests value

## Trigger Conditions

### Movement Triggers
- **Positive Movement (+M)**: Back on favorable price movements
- **Negative Movement (-M)**: Back when price moves against selection (potential value)
- **Percentage Movement**: Use percentage-based movement criteria

### Race-Specific Filters
- **Race Time Range**: Set minimum/maximum race time predictions
- **Age Limits**: Filter horses by age range
- **Field Size**: Control participation based on number of runners
- **RTW/Industry Ratio**: Target specific value ranges

## Configuration Tips
1. Set RTW range based on historical analysis
2. Use industry percentage ratio to identify value bets
3. Adjust age filters based on race type preferences
4. Set runner count limits for market liquidity
5. Test movement parameters on historical data

## Best Practices
- Understand race time prediction methodology
- Monitor RTW accuracy across different race types
- Use conservative percentage thresholds initially
- Consider track conditions and race distance
- Maintain detailed records for strategy refinement

## Race Time Strategy Logic
1. Evaluate race time predictions (RTW)
2. Compare with industry prices and current odds
3. Check movement criteria and filters
4. Place back bet if all conditions are satisfied
5. Monitor position development

## Risk Management
- Set appropriate odds limits for race time bets
- Consider track conditions that might affect race times
- Monitor prediction accuracy over time
- Use position sizing based on confidence level

## Related Strategies
- My Horse Racing Strategy
- Horse Racing Data Statistics
- Race Data to Spreadsheet
- Bookmaker Price Bot

## Technical Notes
This strategy requires access to race time prediction data and industry price feeds for optimal performance.

## Support
For issues with race time data or prediction accuracy, refer to data provider documentation and Bfexplorer race time integration guides.
