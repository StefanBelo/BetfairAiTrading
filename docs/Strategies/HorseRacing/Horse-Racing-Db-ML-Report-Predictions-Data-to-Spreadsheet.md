# Horse Racing Db-ML Report Predictions Data to Spreadsheet

## Overview
Horse Racing Db-ML Report Predictions Data to Spreadsheet is a specialized data export strategy that extracts machine learning prediction data from the Bfexplorer database and exports it to spreadsheet format for detailed prediction analysis and model evaluation.

## Strategy Type
- **Category**: Horse Racing
- **ID**: 10303
- **Type**: ML Prediction Export/Data Analysis

## Parameters

### Data Parameters
| Parameter | Group | Type | Required | Description |
|-----------|-------|------|----------|-------------|
| ModelPrefix | Data | String | No | The model prefix, for instance RunnersData. |
| UseFavouriteModels | Data | Boolean | No | Use the Favourite ML models. |

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
This strategy extracts machine learning prediction data from the database and exports it to spreadsheet format for detailed analysis of ML model performance, prediction accuracy, and strategy development.

## Key Features
- **ML Prediction Export**: Extracts detailed machine learning prediction data
- **Model Filtering**: Filter by model prefix for specific model types
- **Favourite Models**: Option to focus on favourite-specific ML models
- **Comprehensive Data**: Includes prediction scores, confidence levels, and outcomes
- **Analysis Ready**: Formatted for immediate analysis and evaluation

## Machine Learning Data Export

### Model Types
- **General Models**: All ML models with specified prefix
- **Favourite Models**: Specialized models for favourite predictions
- **Custom Models**: Models with specific naming conventions
- **Multiple Models**: Can export data from multiple model types

### Prediction Data
The export includes:
- **Prediction Scores**: ML model output scores
- **Confidence Levels**: Model confidence in predictions
- **Feature Data**: Input features used by models
- **Outcomes**: Actual race results for validation
- **Accuracy Metrics**: Performance measurements

## Configuration Options

### Model Prefix
Set `ModelPrefix` to filter specific model types:
- "RunnersData" - General runner prediction models
- "FavouriteData" - Favourite-specific models
- "CustomModel" - Custom developed models
- Leave blank for all models

### Favourite Models
Set `UseFavouriteModels` to True to:
- Focus on favourite prediction models
- Export favourite-specific data
- Analyze favourite prediction accuracy
- Compare favourite vs. field predictions

## Export Content
The spreadsheet export contains:
- **Prediction Tables**: Organized prediction data by race/selection
- **Model Comparison**: Side-by-side model performance
- **Accuracy Analysis**: Prediction vs. outcome analysis
- **Feature Importance**: Model input feature analysis
- **Performance Metrics**: Statistical performance measures

## Use Cases
- **Model Evaluation**: Assess ML model performance over time
- **Strategy Development**: Use predictions for strategy creation
- **Research**: Analyze prediction patterns and accuracy
- **Model Comparison**: Compare different ML model performances
- **Validation**: Verify model predictions against actual outcomes

## Analysis Applications

### Performance Analysis
- Prediction accuracy by model type
- Confidence level correlation with outcomes
- Feature importance analysis
- Model comparison studies

### Strategy Development
- Use high-confidence predictions for betting strategies
- Identify model strengths and weaknesses
- Develop ensemble prediction approaches
- Create model-based trigger conditions

## Best Practices
- Export data regularly for ongoing model evaluation
- Use specific model prefixes for focused analysis
- Compare favourite models with general models
- Validate export data completeness
- Organize exports by model type and time period

## Technical Requirements
- Access to ML prediction database
- Machine learning models must be trained and active
- Sufficient storage for export files
- Spreadsheet application for analysis

## Data Validation
The strategy provides:
- Prediction accuracy calculations
- Model performance metrics
- Feature correlation analysis
- Outcome validation checks

## Related Strategies
- Horse Racing Db-ML Trigger Bot
- Horse Racing Db-ML Report Data to Spreadsheet
- Horse Racing ML Trigger Bot
- Machine Learning model training strategies

## Model Integration
Works with various ML model types:
- Neural networks
- Decision trees
- Ensemble methods
- Custom algorithms

## Support
For issues with ML data export or model access, refer to Bfexplorer documentation on machine learning integration and database access for prediction data.
