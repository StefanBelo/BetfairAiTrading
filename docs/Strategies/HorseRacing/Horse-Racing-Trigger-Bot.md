# Horse Racing Trigger Bot

## Overview

The **Horse Racing Trigger Bot** is a comprehensive Bfexplorer strategy that analyzes multiple racing factors to execute automated betting strategies. This bot combines statistical analysis, form factors, and machine learning capabilities to make informed betting decisions on horse racing markets.

## Parameters

### Bet Parameters
- **MarketCriteria** *(String, Optional)*: Set the market criteria to execute the strategy set by the parameter StrategyName (strategy name).
- **SelectionCriteria** *(String, Optional)*: Set the selection criteria to execute the strategy set by the parameter StrategyName (strategy name).
- **StrategyType** *(Enum, Optional)*: Set the strategy factor type.
  - Values: FactorValue, Favourite, MachineLearning
- **EnabledStrategy** *(Enum, Optional)*: Set the enabled strategy type.
  - Values: Any, Back, Lay
- **ExecuteOnFavouriteOnly** *(Boolean, Optional)*: Set to True to execute the strategy on a favourite only.
- **UseFactorFavouriteIndexToSetBetType** *(Boolean, Optional)*: Set to True to use the FactorIndex and FavouriteIndex for bet type evaluation.
- **MinimumNumberOfOppositePredictions** *(Byte, Optional)*: Set the minimum number of opposite predictions.
- **MinimumFactorValueDifference** *(Double, Optional)*: Set the minimum factor value difference.

### Time Parameters
- **ConfirmPrediction** *(TimeSpan, Optional)*: Confirm the prediction in the time span.

### Strategy Parameters
- **StrategyName** *(String, Optional)*: If criteria set by the parameter MarketCriteria and SelectionCriteria are evaluated to be True, the strategy is executed on dedicated selection/s.

### Factor Data Parameters
- **UsePriceFactor** *(Boolean, Optional)*: Use the price factor.
- **UsePriceDifferenceFactor** *(Boolean, Optional)*: Use the price difference factor.
- **UseForecastPriceFactor** *(Boolean, Optional)*: Use the forecast price factor.
- **UseOfficialRatingFactor** *(Boolean, Optional)*: Use the official rating factor.
- **UseWeightFactor** *(Boolean, Optional)*: Use the weight factor.
- **UseDaysOffFactor** *(Boolean, Optional)*: Use the days off factor.
- **UseTopSpeedFactor** *(Boolean, Optional)*: Use the top speed factor.
- **UseRatingFactor** *(Boolean, Optional)*: Use the rating factor.
- **UseAverageBeatenDistanceFactor** *(Boolean, Optional)*: Use the average beaten distance factor.
- **UseBeatenDistanceTrendFactor** *(Boolean, Optional)*: Use the beaten distance trend factor.
- **UsePredictionScoreFactor** *(Boolean, Optional)*: Use the predictionScore factor.

### Tipster and Expert Data Parameters
- **UseOlgbWinConfidenceFactor** *(Boolean, Optional)*: Use the olgb win confidence factor.
- **UseJockeyWinPercentangeFactor** *(Boolean, Optional)*: Use the jockey win percentange factor.
- **UseTrainerWinPercentageFactor** *(Boolean, Optional)*: Use the trainer win percentage factor.
- **UseRatingStarsFactor** *(Boolean, Optional)*: Use the rating stars.

### Form Factors
- **UseHorseWinnerLastTimeOutFactor** *(Boolean, Optional)*: Use the horse winner last time out.
- **UseHorseInFormFactor** *(Boolean, Optional)*: Use the horse in form.
- **UseHorseBeatenFavouriteLTOFactor** *(Boolean, Optional)*: Use the horse beaten favourite LTO.

### Track and Distance Factors
- **UseSuitedByGoingFactor** *(Boolean, Optional)*: Use the suited by going.
- **UseSuitedByCourseFactor** *(Boolean, Optional)*: Use the suited by course.
- **UseSuitedByDistanceFactor** *(Boolean, Optional)*: Use the suited by distance.

### Connections Factors
- **UseTrainerInFormFactor** *(Boolean, Optional)*: Use the trainer in form.
- **UseTrainerCourseRecordFactor** *(Boolean, Optional)*: Use the trainer course record.
- **UseJockeyInFormFactor** *(Boolean, Optional)*: Use the jockey in form.
- **UseJockeyWonOnHorseFactor** *(Boolean, Optional)*: Use the jockey won on horse.

### Timeform Factors
- **UseTimeformTopRatedFactor** *(Boolean, Optional)*: Use the timeform top rated.
- **UseTimeformImproverFactor** *(Boolean, Optional)*: Use the timeform improver.
- **UseTimeformHorseInFocusFactor** *(Boolean, Optional)*: Use the timeform horse in focus.

### Machine Learning Parameters
- **MachineLearningType** *(Enum, Optional)*: The machine learning workflow type.
  - Values: None, SelectionData, SelectionTrendData, SelectionTrendDataWinnerIndex, SelectionDataPrice
- **ModelName** *(String, Optional)*: Set the machine learning model name.

### Miscellaneous Parameters
- **ReportDataToSpreadsheet** *(Boolean, Optional)*: Set to True to report data to spreadsheet.

### Market Control Parameters
- **EvaluateEntryCriteriaOnlyOnce** *(Boolean, Optional)*: Set this parameter to True to evaluate the entry criteria only once.
- **StopMarketMonitoring** *(Boolean, Optional)*: Set this parameter to True to stop market monitoring after the strategy completes execution. Monitoring will stop only if no other bot is active in the market.

### Miscellaneous Parameters
- **StrategyReference** *(String, Optional)*: Set this parameter to specify which strategy sent the bet. The string is limited to 15 characters.

## Key Features

### Comprehensive Factor Analysis
- **Price Factors**: Analysis of current odds, price differences, and forecast prices
- **Performance Metrics**: Official ratings, speed figures, and form analysis
- **Track Conditions**: Surface, distance, and course suitability analysis
- **Connections**: Trainer and jockey performance factors

### Multi-Strategy Approach
- **Factor-Based**: Analysis using multiple racing factors
- **Favourite-Focused**: Strategies targeting market favourites
- **Machine Learning**: AI-driven prediction models
- **Flexible Execution**: Configurable back/lay/any strategy types

### Advanced Form Analysis
- **Recent Performance**: Last time out results and current form
- **Trend Analysis**: Performance patterns and improvement trends
- **Beaten Distance**: Average margins and trend analysis
- **Historical Context**: Long-term performance patterns

### Expert Integration
- **OLGB Confidence**: Professional tipster confidence ratings
- **Timeform Data**: Industry-standard form analysis
- **Rating Systems**: Multiple rating and scoring systems
- **Prediction Scores**: Composite prediction algorithms

## Strategy Types

### Factor Value Strategy
Analyzes multiple factors to identify value opportunities:
- Combines multiple data points for comprehensive analysis
- Uses weighted factor scoring for selection evaluation
- Identifies horses with strong factor combinations
- Targets value opportunities missed by markets

### Favourite Strategy
Focuses on market favourites with enhanced analysis:
- Analyzes favourite performance patterns
- Identifies vulnerable favourites for laying
- Confirms strong favourites for backing
- Uses favourite-specific statistical models

### Machine Learning Strategy
Leverages AI models for prediction:
- Uses trained models for outcome prediction
- Analyzes complex pattern recognition
- Provides probability-based assessments
- Continuous learning from new data

## Implementation Guidelines

### Basic Setup
1. **Select Strategy Type**: Choose between Factor, Favourite, or ML approach
2. **Configure Factors**: Enable relevant factor analysis
3. **Set Criteria**: Define market and selection criteria
4. **Configure Execution**: Set back/lay preferences and timing

### Factor Configuration Examples

#### Comprehensive Analysis
```
Enable All Key Factors:
- UsePriceFactor: true
- UseOfficialRatingFactor: true
- UseJockeyWinPercentangeFactor: true
- UseTrainerInFormFactor: true
- UseSuitedByCourseFactor: true
- UseTimeformTopRatedFactor: true
```

#### Speed-Focused Strategy
```
Speed and Performance Focus:
- UseTopSpeedFactor: true
- UseRatingFactor: true
- UseAverageBeatenDistanceFactor: true
- UseBeatenDistanceTrendFactor: true
```

#### Connections-Based Approach
```
Trainer and Jockey Focus:
- UseJockeyWinPercentangeFactor: true
- UseTrainerWinPercentageFactor: true
- UseJockeyInFormFactor: true
- UseTrainerInFormFactor: true
- UseJockeyWonOnHorseFactor: true
```

### Best Practices
- **Factor Selection**: Choose factors relevant to race type and conditions
- **Criteria Testing**: Backtest market and selection criteria combinations
- **Seasonal Adjustments**: Modify factor weights for different racing seasons
- **Performance Monitoring**: Track factor effectiveness over time

## Factor Analysis Details

### Price and Value Factors
- **Price Factor**: Current market odds analysis
- **Price Difference Factor**: Odds movement and trends
- **Forecast Price Factor**: Morning line vs. current odds comparison

### Form and Performance Factors
- **Official Rating**: Official handicap ratings
- **Top Speed Factor**: Speed figure analysis
- **Rating Factor**: Composite rating analysis
- **Beaten Distance Factors**: Margin analysis and trends

### Course and Distance Factors
- **Suited by Going**: Track condition preferences
- **Suited by Course**: Course-specific performance
- **Suited by Distance**: Distance preference analysis

### Recent Form Factors
- **Winner Last Time Out**: Last race victory
- **Horse in Form**: Recent performance trends
- **Beaten Favourite LTO**: Last race performance vs. market expectations

## Example Configuration

```json
{
  "MarketCriteria": "RaceType='Flat' AND Runners>=6 AND PrizeValue>2000",
  "SelectionCriteria": "OfficialRating>=60 AND DaysOff<=60",
  "StrategyType": "FactorValue",
  "EnabledStrategy": "Back",
  "StrategyName": "Multi-Factor Value",
  "UsePriceFactor": true,
  "UseOfficialRatingFactor": true,
  "UseJockeyWinPercentangeFactor": true,
  "UseTrainerInFormFactor": true,
  "UseSuitedByCourseFactor": true,
  "UseTimeformTopRatedFactor": true,
  "MinimumFactorValueDifference": 0.15,
  "ReportDataToSpreadsheet": true
}
```

## Machine Learning Integration

### Model Types
- **Selection Data**: Basic selection-level ML analysis
- **Selection Trend Data**: Trend-based prediction models
- **Winner Index Models**: Models focused on winner prediction
- **Price-Integrated Models**: ML models incorporating price data

### Model Configuration
```
Machine Learning Setup:
- MachineLearningType: SelectionTrendData
- ModelName: "TrendAnalysisModel_v2"
- MinimumNumberOfOppositePredictions: 2
- ConfirmPrediction: 00:05:00
```

## Advanced Features

### Prediction Confirmation
- **Time-based Confirmation**: Validate predictions over time
- **Multiple Model Validation**: Cross-reference different models
- **Opposite Prediction Filtering**: Require consensus for execution
- **Factor Value Thresholds**: Minimum difference requirements

### Data Export and Analysis
- **Spreadsheet Reporting**: Export analysis data for review
- **Performance Tracking**: Monitor factor effectiveness
- **Historical Analysis**: Long-term pattern identification
- **Strategy Optimization**: Data-driven parameter tuning

## Performance Optimization

### Factor Weight Optimization
- Regular analysis of factor predictive power
- Seasonal adjustments for different racing periods
- Track and distance-specific factor tuning
- Form cycle adjustments for training patterns

### Criteria Refinement
- Backtest market criteria for profitability
- Optimize selection criteria thresholds
- Adjust for different race types and classes
- Monitor changing market conditions

## Notes

- Factor selection should match race type and conditions
- Regular factor effectiveness analysis recommended
- Machine learning models require sufficient training data
- Timeform data enhances analysis significantly
- Consider seasonal variations in factor reliability
- Track changes in trainer and jockey performance patterns
