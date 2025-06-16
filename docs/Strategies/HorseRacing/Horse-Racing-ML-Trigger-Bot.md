# Horse Racing ML Trigger Bot

## Overview
Horse Racing ML Trigger Bot is an advanced machine learning-based strategy that uses multiple ML models and data sources to trigger betting strategies on horse racing markets. This strategy provides sophisticated analysis and prediction capabilities for automated horse racing betting.

## Strategy Type
- **Category**: Horse Racing
- **ID**: 10201
- **Type**: Machine Learning Trigger Strategy

## Parameters

### Bet Parameters
| Parameter | Group | Type | Required | Description |
|-----------|-------|------|----------|-------------|
| MarketCriteria | Bet | String | No | Set the market criteria to execute the strategy set by the parameter StrategyName (strategy name) |
| SelectionCriteria | Bet | String | No | Set the selection criteria to execute the strategy set by the parameter StrategyName (strategy name) |
| ToBackCriteria | Bet | String | No | Set the criteria to confirm backing. |
| ToLayCriteria | Bet | String | No | Set the criteria to confirm laying. |
| EnabledStrategy | Bet | Enum | No | Set the enabled strategy type. Values: Any, Back, Lay |

### Strategy Parameters
| Parameter | Group | Type | Required | Description |
|-----------|-------|------|----------|-------------|
| StrategyName | Strategy | String | No | If criteria set by the parameter MarketCriteria and SelectionCriteria are evaluated to be True, the strategy is executed on dedicated selection/s. |
| ExecuteOnAllDedicatedSelections | Strategy | Boolean | No | Set to True if you want to execute your action bot on all dedicated selections. |

### Trigger Parameters
| Parameter | Group | Type | Required | Description |
|-----------|-------|------|----------|-------------|
| MachineLearningType | Trigger | Enum | No | The machine learning workflow type. Values: None, SelectionData, SelectionTrendData, SelectionTrendDataWinnerIndex, SelectionDataPrice |
| MachineLearningToConfirmType | Trigger | Enum | No | The machine learning workflow type to confirm the prediction. Values: None, SelectionData, SelectionTrendData, SelectionTrendDataWinnerIndex, SelectionDataPrice |
| ModelName | Trigger | String | No | Set the machine learning model name. |
| ModelNameToConfirm | Trigger | String | No | Set the machine learning model name to confirm the dedicated selection. |

### Data Parameters
| Parameter | Group | Type | Required | Description |
|-----------|-------|------|----------|-------------|
| UseSortByFactorNormalizer | Data | Boolean | No | Use sort by FactorNormalizer. |
| UseHorsesDataByFilter | Data | Boolean | No | Use the horses data by filter provided by TIMEFORM. |

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
This strategy uses machine learning models to analyze horse racing data and trigger betting strategies when specific ML-based criteria are met. It provides sophisticated prediction capabilities for automated horse racing betting.

## Key Features
- **Machine Learning Integration**: Uses advanced ML models for predictions
- **Multiple ML Workflows**: Supports various ML data types and models
- **Confirmation Models**: Secondary ML models for prediction validation
- **Flexible Criteria**: Customizable market and selection criteria
- **TIMEFORM Integration**: Access to professional racing data
- **Strategy Triggering**: Executes specified strategies when criteria are met

## Machine Learning Types

### Primary ML Workflows
- **SelectionData**: Individual selection-based ML analysis
- **SelectionTrendData**: Trend-based selection analysis
- **SelectionTrendDataWinnerIndex**: Winner-focused trend analysis
- **SelectionDataPrice**: Price-integrated selection analysis

### Confirmation ML Workflows
Use secondary ML models to validate primary predictions:
- Same workflow types available for confirmation
- Helps reduce false positives
- Increases prediction confidence

## Strategy Logic
1. **Data Collection**: Gather racing data and market information
2. **ML Analysis**: Apply primary ML model for predictions
3. **Confirmation**: Validate with secondary ML model if configured
4. **Criteria Evaluation**: Check market and selection criteria
5. **Strategy Execution**: Trigger specified strategy if all criteria met

## Configuration Tips
1. Set appropriate ML model names for your trained models
2. Use confirmation models to improve prediction accuracy
3. Configure market criteria to filter suitable races
4. Set selection criteria to identify target horses
5. Choose appropriate ML workflow type for your data

## Machine Learning Integration

### Model Selection
- Choose trained ML models appropriate for racing data
- Use different models for different race types
- Consider ensemble approaches with confirmation models

### Data Sources
- **Selection Data**: Individual horse performance metrics
- **Trend Data**: Historical performance trends
- **Price Data**: Market pricing integration
- **TIMEFORM Data**: Professional racing analysis

## Best Practices
- Train ML models on relevant historical data
- Use confirmation models to reduce false signals
- Test criteria combinations thoroughly
- Monitor ML model performance over time
- Keep models updated with recent data

## Criteria Configuration

### Market Criteria
Configure conditions for market participation:
- Race type and distance filters
- Field size requirements
- Market liquidity conditions

### Selection Criteria
Set conditions for horse selection:
- ML prediction scores
- Data quality requirements
- Performance thresholds

## Risk Management
- Set appropriate confidence thresholds
- Use confirmation models for high-stakes decisions
- Monitor ML model drift over time
- Implement position sizing based on prediction confidence

## Related Strategies
- Horse Racing Db-ML Trigger Bot
- Horse Racing Trigger Bot
- Horse Racing Data Statistics
- Race Data to Spreadsheet

## Technical Requirements
- Trained machine learning models
- Access to racing databases
- TIMEFORM data integration (optional)
- Sufficient computational resources for ML inference

## Support
For issues with ML integration or model configuration, refer to Bfexplorer documentation on machine learning strategies and racing data integration.
