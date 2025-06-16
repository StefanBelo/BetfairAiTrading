# Horse Racing Db-ML Trigger Bot

## Overview

The **Horse Racing Db-ML Trigger Bot** is an advanced Bfexplorer strategy that combines database-driven horse racing analysis with machine learning predictions to execute sophisticated automated trading strategies. This bot leverages comprehensive horse racing databases and advanced ML models for enhanced betting decisions.

## Parameters

### Bet Parameters
- **MarketCriteria** *(String, Optional)*: Set the market criteria to execute the strategy set by the parameter StrategyName (strategy name).
- **SelectionCriteria** *(String, Optional)*: Set the selection criteria to execute the strategy set by the parameter StrategyName (strategy name).
- **ToBackCriteria** *(String, Optional)*: Set the criteria to confirm backing.
- **ToLayCriteria** *(String, Optional)*: Set the criteria to confirm laying.
- **EnabledStrategy** *(Enum, Optional)*: Set the enabled strategy type.
  - Values: Any, Back, Lay
- **SortSelectionsBy** *(Enum, Optional)*: Set to sort selections by price, score or expected value.
  - Values: Price, PriceDescending, Score, ExpectedValue

### Strategy Parameters
- **StrategyName** *(String, Optional)*: If criteria set by the parameter MarketCriteria and SelectionCriteria are evaluated to be True, the strategy is executed on dedicated selection/s.
- **ExecuteOnAllDedicatedSelections** *(Boolean, Optional)*: Set to True if you want to execute your action bot on all dedicated selections.
- **CheckPotentialProfit** *(Boolean, Optional)*: Set to True if you want to check the potential profit when executing your action bot on all dedicated selections.

### Machine Learning Parameters
- **MachineLearningType** *(Enum, Optional)*: The machine learning workflow type.
  - Values: None, SelectionData, FavouriteSelectionData, SelectionFactorData, SelectionFactorDataPrice, CompetitorSelectionData, MlNetHorseData, MlNetJockeyData, MlNetHorseDataPosition, MlNetHorseFactorData, MlNetHorseDataGeneral
- **MachineLearningToConfirmType** *(Enum, Optional)*: The machine learning workflow type to confirm the prediction.
  - Values: None, SelectionData, FavouriteSelectionData, SelectionFactorData, SelectionFactorDataPrice, CompetitorSelectionData, MlNetHorseData, MlNetJockeyData, MlNetHorseDataPosition, MlNetHorseFactorData, MlNetHorseDataGeneral
- **ModelName** *(String, Optional)*: Set the machine learning model name.
- **ModelNameToConfirm** *(String, Optional)*: Set the machine learning model name to confirm the dedicated selection.

### Data Parameters
- **UseBetfairStartPrice** *(Boolean, Optional)*: Use the betfair start price to trigger the strategy execution.
- **UseDatabase** *(Boolean, Optional)*: Use the horse racing database.
- **UseSetBackPredictions** *(Boolean, Optional)*: Use set back predictions.
- **UseCumulativeMlScore** *(Boolean, Optional)*: Use the cumulative ML score.
- **UseLayPredictionForCumulativeMlScoreEvaluation** *(Boolean, Optional)*: Use the lay prediction for the cumulative ML score evaluation.
- **UseSaveDataService** *(Boolean, Optional)*: Use the save data service.
- **RaceType** *(Enum, Optional)*: Set the enabled race type.
  - Values: Any, Flat, Jump, AssignModel

### Market Control Parameters
- **EvaluateEntryCriteriaOnlyOnce** *(Boolean, Optional)*: Set this parameter to True to evaluate the entry criteria only once.
- **StopMarketMonitoring** *(Boolean, Optional)*: Set this parameter to True to stop market monitoring after the strategy completes execution. Monitoring will stop only if no other bot is active in the market.

### Miscellaneous Parameters
- **StrategyReference** *(String, Optional)*: Set this parameter to specify which strategy sent the bet. The string is limited to 15 characters.

## Key Features

### Advanced Machine Learning Integration
- **Multiple ML Models**: Support for various machine learning workflows
- **Model Confirmation**: Secondary ML model validation for predictions
- **Cumulative Scoring**: Advanced scoring systems for selection evaluation
- **Dynamic Model Assignment**: Race-type specific model selection

### Database-Driven Analysis
- **Comprehensive Database**: Access to extensive horse racing historical data
- **Starting Price Integration**: Betfair SP data for enhanced analysis
- **Data Service Integration**: Advanced data saving and retrieval capabilities
- **Historical Performance Tracking**: Long-term performance pattern analysis

### Sophisticated Criteria System
- **Market Criteria**: Complex market-level filtering and selection
- **Selection Criteria**: Detailed horse-level analysis and filtering
- **Back/Lay Confirmation**: Separate criteria for backing and laying decisions
- **Multi-Factor Analysis**: Integration of multiple data sources and factors

### Advanced Strategy Execution
- **Flexible Selection Targeting**: Execute on single or multiple selections
- **Profit Optimization**: Potential profit checking for portfolio decisions
- **Strategy Type Control**: Configurable back/lay/any strategy execution
- **Selection Sorting**: Multiple sorting options for selection prioritization

## Machine Learning Model Types

### Core Data Models
- **SelectionData**: Basic selection-level machine learning analysis
- **FavouriteSelectionData**: ML models focused on favourite analysis
- **SelectionFactorData**: Multi-factor analysis for selections
- **SelectionFactorDataPrice**: Price-integrated factor analysis
- **CompetitorSelectionData**: Competitive analysis between selections

### Advanced Horse-Specific Models
- **MlNetHorseData**: Neural network models for individual horse analysis
- **MlNetJockeyData**: Jockey-specific machine learning models
- **MlNetHorseDataPosition**: Position-based horse performance models
- **MlNetHorseFactorData**: Multi-factor horse analysis models
- **MlNetHorseDataGeneral**: General-purpose horse performance models

## Use Cases

### Advanced Betting Systems
- Execute sophisticated ML-driven betting strategies
- Combine multiple data sources for enhanced accuracy
- Implement advanced selection and market filtering
- Use dual-model confirmation for high-confidence bets

### Professional Racing Analysis
- Leverage comprehensive racing databases
- Apply advanced machine learning to racing data
- Implement professional-grade selection criteria
- Execute systematic betting approaches

### Portfolio Management
- Check potential profits across multiple selections
- Execute strategies on all qualifying selections
- Implement risk management through criteria filtering
- Optimize selection ordering and prioritization

## Implementation Guidelines

### Basic Setup
1. **Configure Database Access**: Ensure access to horse racing database
2. **Select ML Models**: Choose appropriate machine learning models
3. **Define Criteria**: Set market and selection criteria
4. **Configure Strategy**: Specify underlying trading strategy

### Machine Learning Configuration
```
Primary Model Setup:
- MachineLearningType: MlNetHorseData
- ModelName: "HorsePerformanceModel_v3"

Confirmation Model:
- MachineLearningToConfirmType: MlNetJockeyData
- ModelNameToConfirm: "JockeyAnalysis_v2"
```

### Criteria Examples
```
Market Criteria:
- RaceType = "Flat" AND Runners >= 8 AND Runners <= 16
- TotalPrizeValue > 10000 AND RaceClass <= 3

Selection Criteria:
- MLScore > 0.75 AND OfficialRating >= 70
- DaysOff <= 30 AND RecentForm >= 0.6
```

### Best Practices
- **Model Validation**: Regularly validate ML model performance
- **Criteria Testing**: Backtest criteria combinations for effectiveness
- **Database Maintenance**: Ensure database completeness and accuracy
- **Performance Monitoring**: Track strategy success rates over time

## Race Type Considerations

### Flat Racing
- Higher prediction accuracy for ML models
- More consistent form patterns
- Better data availability for analysis
- Optimal for general ML models

### Jump Racing
- More variables affecting performance
- Weather and ground conditions more critical
- Requires specialized ML models
- Higher variance in outcomes

### Model Assignment Mode
- Automatically assigns best model based on race characteristics
- Adapts to different race types and conditions
- Optimizes model selection for maximum accuracy
- Reduces manual model management

## Example Configuration

```json
{
  "MarketCriteria": "RaceType='Flat' AND Runners>=8 AND PrizeValue>5000",
  "SelectionCriteria": "MLScore>0.8 AND OfficialRating>=65",
  "ToBackCriteria": "BackProbability>0.6 AND ValueRating>1.2",
  "ToLayCriteria": "LayProbability>0.7 AND OverroundValue<0.9",
  "EnabledStrategy": "Any",
  "SortSelectionsBy": "ExpectedValue",
  "StrategyName": "ML Value Bet",
  "MachineLearningType": "MlNetHorseData",
  "ModelName": "ComprehensiveHorseModel",
  "MachineLearningToConfirmType": "MlNetJockeyData", 
  "ModelNameToConfirm": "JockeyPerformanceModel",
  "UseDatabase": true,
  "UseCumulativeMlScore": true,
  "RaceType": "Flat"
}
```

## Advanced Features

### Cumulative ML Scoring
- Combines multiple ML model outputs
- Weighted scoring based on model reliability
- Enhanced prediction accuracy through ensemble methods
- Configurable score thresholds for execution

### Data Service Integration
- Automated data collection and storage
- Real-time data updates during racing
- Historical data archiving for model training
- Performance tracking and analytics

### Dual Model Confirmation
- Primary model for initial selection
- Secondary model for confirmation
- Reduces false positives
- Enhances overall strategy reliability

## Performance Optimization

### Model Selection Strategy
- Choose models appropriate for race types
- Regular model performance evaluation
- Seasonal adjustments for different racing periods
- Surface and distance-specific model optimization

### Criteria Refinement
- Regular backtesting of market and selection criteria
- Seasonal and track-specific adjustments
- Performance-based criteria optimization
- Dynamic threshold adjustments

## Integration Capabilities

### Database Systems
- Compatible with major horse racing databases
- Real-time data feed integration
- Historical performance data access
- Form and rating system integration

### Machine Learning Platforms
- Support for custom ML model integration
- Model versioning and management
- Performance tracking and validation
- Automated model updating capabilities

## Notes

- Requires access to comprehensive horse racing database
- ML model performance depends on data quality and training
- Regular model validation and updating recommended
- Consider seasonal variations in racing patterns
- Monitor for changes in racing conditions and regulations
- Database maintenance critical for optimal performance
