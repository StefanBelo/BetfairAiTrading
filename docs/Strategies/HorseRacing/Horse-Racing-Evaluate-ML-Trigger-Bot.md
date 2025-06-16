# Horse Racing Evaluate ML Trigger Bot

## Overview
The **Horse Racing Evaluate ML Trigger Bot** strategy implements advanced machine learning evaluation for horse racing selections with sophisticated prediction assessment and alternative strategy execution based on ML scores.

## Strategy Details
- **Category**: Horse Racing
- **Type**: Machine Learning Evaluation
- **Purpose**: Evaluate ML predictions and execute strategies based on comprehensive analysis

## Parameters

### Selection Evaluation Parameters
| Parameter | Group | Type | Required | Description |
|-----------|--------|------|----------|-------------|
| SelectionEvaluationType | Bet | Enum | No | The selection evaluation type |
| IncludePotentialSelectionMlScore | Bet | Double | No | Minimum ML score for potential selection inclusion |
| CheckNumberOfBetTypesRule | Bet | Boolean | No | Check the number of bet types rule |
| CheckNumberOfAppliedMLsRule | Bet | Boolean | No | Check the number of applied MLs |
| AllowedNumberOfFalseSelections | Bet | Byte | No | Number of allowed false selections |

**SelectionEvaluationType Options:**
- DefaultEvaluation
- AddFavouriteAndBestCombinedPredictionScore

### Alternative Strategy Parameters
| Parameter | Group | Type | Required | Description |
|-----------|--------|------|----------|-------------|
| MinimumMlScore | Bet | Double | No | Minimum ML score to execute alternative strategy |
| IfElseBotName | Strategy | String | No | Alternative strategy to execute on dedicated selections |

### Core ML Parameters
| Parameter | Group | Type | Required | Description |
|-----------|--------|------|----------|-------------|
| MarketCriteria | Bet | String | No | Market criteria for strategy execution |
| SelectionCriteria | Bet | String | No | Selection criteria for strategy execution |
| ToBackCriteria | Bet | String | No | Criteria to confirm backing |
| ToLayCriteria | Bet | String | No | Criteria to confirm laying |
| EnabledStrategy | Bet | Enum | No | Enabled strategy type |

**EnabledStrategy Options:**
- Any
- Back
- Lay

### Strategy Execution Parameters
| Parameter | Group | Type | Required | Description |
|-----------|--------|------|----------|-------------|
| StrategyName | Strategy | String | No | Primary strategy name for execution |
| ExecuteOnAllDedicatedSelections | Strategy | Boolean | No | Execute on all dedicated selections |

### Machine Learning Configuration
| Parameter | Group | Type | Required | Description |
|-----------|--------|------|----------|-------------|
| MachineLearningType | Trigger | Enum | No | ML workflow type |
| MachineLearningToConfirmType | Trigger | Enum | No | ML workflow type for confirmation |
| ModelName | Trigger | String | No | Machine learning model name |
| ModelNameToConfirm | Trigger | String | No | ML model name for confirmation |

**MachineLearningType Options:**
- None
- SelectionData
- SelectionTrendData
- SelectionTrendDataWinnerIndex
- SelectionDataPrice

### Data Configuration
| Parameter | Group | Type | Required | Description |
|-----------|--------|------|----------|-------------|
| UseSortByFactorNormalizer | Data | Boolean | No | Use sort by FactorNormalizer |
| UseHorsesDataByFilter | Data | Boolean | No | Use horses data by filter provided by TIMEFORM |

### Market Parameters
| Parameter | Group | Type | Required | Description |
|-----------|--------|------|----------|-------------|
| EvaluateEntryCriteriaOnlyOnce | Market | Boolean | No | Evaluate entry criteria only once |
| StopMarketMonitoring | Market | Boolean | No | Stop market monitoring after completion |

### Miscellaneous Parameters
| Parameter | Group | Type | Required | Description |
|-----------|--------|------|----------|-------------|
| StrategyReference | Miscellaneous | String | No | Strategy reference identifier (max 15 characters) |

## Usage Examples

### Advanced ML Evaluation
```
Strategy: Horse Racing Evaluate ML Trigger Bot
SelectionEvaluationType: AddFavouriteAndBestCombinedPredictionScore
IncludePotentialSelectionMlScore: 0.75
CheckNumberOfBetTypesRule: True
CheckNumberOfAppliedMLsRule: True
AllowedNumberOfFalseSelections: 2
MinimumMlScore: 0.80
StrategyName: "Place Bet"
IfElseBotName: "Alternative Strategy"
MachineLearningType: SelectionData
ModelName: "HorseRacingModel_v2"
```

### Conservative ML Approach
```
Strategy: Horse Racing Evaluate ML Trigger Bot
SelectionEvaluationType: DefaultEvaluation
IncludePotentialSelectionMlScore: 0.65
MinimumMlScore: 0.70
StrategyName: "Conservative Betting"
EnabledStrategy: Back
MachineLearningType: SelectionTrendData
ModelName: "ConservativeModel"
UseHorsesDataByFilter: True
```

### Comprehensive ML Analysis
```
Strategy: Horse Racing Evaluate ML Trigger Bot
MarketCriteria: "MinRunners >= 8 AND MaxRunners <= 16"
SelectionCriteria: "Odds >= 2.0 AND Odds <= 10.0"
ToBackCriteria: "MLScore >= 0.8"
ToLayCriteria: "MLScore <= 0.3"
StrategyName: "ML Trading Strategy"
MachineLearningType: SelectionData
MachineLearningToConfirmType: SelectionTrendData
ModelName: "PrimaryModel"
ModelNameToConfirm: "ConfirmationModel"
ExecuteOnAllDedicatedSelections: True
```

## Advanced ML Features

### Multi-Model Evaluation
- **Primary Model**: Main prediction model
- **Confirmation Model**: Secondary validation model
- **Model Comparison**: Compare predictions across models
- **Ensemble Methods**: Combine multiple model outputs

### Selection Evaluation Methods
- **Default Evaluation**: Standard ML-based selection
- **Combined Scoring**: Favorite and best prediction combination
- **Threshold Filtering**: ML score-based filtering
- **Rule-based Validation**: Multiple validation rules

### Alternative Strategy Framework
- **Conditional Execution**: Execute alternative based on ML scores
- **Fallback Strategies**: Backup strategies for low confidence
- **Strategy Switching**: Dynamic strategy selection
- **Performance Optimization**: Optimize strategy selection

## Machine Learning Integration

### Model Management
- **Model Selection**: Choose appropriate models
- **Model Validation**: Validate model performance
- **Model Updates**: Handle model version updates
- **Performance Monitoring**: Monitor model accuracy

### Data Processing
- **Feature Engineering**: Advanced feature processing
- **Data Normalization**: Factor-based normalization
- **Filter Integration**: TIMEFORM data filtering
- **Real-time Processing**: Live data processing

### Prediction Validation
- **Confidence Scoring**: Assess prediction confidence
- **Cross-validation**: Multiple model validation
- **Threshold Management**: Dynamic threshold adjustment
- **Error Handling**: Handle prediction errors

## Best Practices

1. **Model Selection**: Choose appropriate ML models for conditions
2. **Threshold Tuning**: Optimize ML score thresholds
3. **Validation Rules**: Implement comprehensive validation
4. **Alternative Strategies**: Prepare fallback strategies
5. **Performance Monitoring**: Continuously monitor performance

## Integration

This strategy works well with:
- Machine learning platforms
- Data analysis tools
- Horse racing databases
- Performance tracking systems

## Advanced Applications

### Research and Development
- **Model Testing**: Test new ML models
- **Strategy Optimization**: Optimize strategy parameters
- **Performance Analysis**: Analyze prediction accuracy
- **Market Research**: Study market patterns

### Live Trading
- **Real-time Decisions**: Make live trading decisions
- **Risk Management**: Manage risk through ML insights
- **Opportunity Detection**: Identify trading opportunities
- **Performance Tracking**: Track live performance

## Notes

- This is an advanced ML strategy requiring sophisticated setup
- Requires trained machine learning models
- Provides comprehensive evaluation and alternative strategy execution
- Suitable for experienced users with ML knowledge
- Integrates with multiple data sources and models
- Enables sophisticated horse racing analysis and trading
- Performance depends on quality of ML models and data
