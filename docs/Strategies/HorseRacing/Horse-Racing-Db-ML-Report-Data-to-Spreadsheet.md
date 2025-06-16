# Horse Racing Db-ML Report Data to Spreadsheet

## Overview
Horse Racing Db-ML Report Data to Spreadsheet is a comprehensive data export strategy that extracts machine learning analysis results and historical race data from the Bfexplorer database and exports it to spreadsheet format for analysis and reporting.

## Strategy Type
- **Category**: Horse Racing
- **ID**: 10302
- **Type**: Data Export/Machine Learning Analysis

## Parameters

### Data Parameters
| Parameter | Group | Type | Required | Description |
|-----------|-------|------|----------|-------------|
| ForLastXRaces | Data | Byte | No | Get the horse race results for the last X races. |

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
This strategy extracts historical horse racing data and machine learning analysis results from the database and exports them to spreadsheet format for detailed analysis, reporting, and strategy development.

## Key Features
- **Database Integration**: Direct access to Bfexplorer horse racing database
- **Machine Learning Data**: Exports ML analysis results and predictions
- **Historical Analysis**: Retrieves data for specified number of past races
- **Spreadsheet Export**: Formats data for easy analysis in Excel or similar tools
- **Flexible Range**: Configurable number of races to analyze

## Data Export Content
The strategy typically exports:
- **Race Information**: Date, venue, race details, conditions
- **Horse Data**: Performance history, form, ratings
- **ML Predictions**: Machine learning model outputs and scores
- **Results**: Actual race outcomes for validation
- **Performance Metrics**: Accuracy measures and statistics

## Configuration Tips
1. Set `ForLastXRaces` to appropriate number based on analysis needs
2. Use smaller numbers for recent performance analysis
3. Use larger numbers for comprehensive historical studies
4. Set `EvaluateEntryCriteriaOnlyOnce` to True for one-time exports
5. Schedule regular exports for ongoing analysis

## Use Cases
- **Strategy Development**: Analyze historical data for strategy creation
- **Model Validation**: Verify machine learning model performance
- **Performance Analysis**: Review prediction accuracy over time
- **Research**: Conduct detailed racing analytics
- **Reporting**: Generate performance reports for stakeholders

## Export Format
The spreadsheet export includes:
- Structured data tables with clear headers
- Multiple sheets for different data types
- Formatted cells for easy reading
- Charts and summaries where applicable

## Historical Analysis
- **Race Results**: Complete race outcome data
- **Performance Trends**: Horse and jockey performance over time
- **Market Data**: Historical odds and market movements
- **Prediction Accuracy**: ML model performance metrics

## Best Practices
- Export data regularly for trend analysis
- Use appropriate race count for your analysis needs
- Validate exported data for completeness
- Organize exports by date or analysis type
- Backup important analysis results

## Data Sources
The strategy accesses:
- **Race Database**: Historical race results and information
- **ML Models**: Machine learning prediction results
- **Market Data**: Historical odds and betting information
- **Performance Metrics**: Calculated accuracy and statistics

## Technical Requirements
- Access to Bfexplorer horse racing database
- Sufficient storage space for export files
- Spreadsheet application for viewing exported data
- Database permissions for historical data access

## Related Strategies
- Horse Racing Db-ML Trigger Bot
- Horse Racing Db-ML Report Predictions Data to Spreadsheet
- Horse Racing Data Statistics
- Race Data to Spreadsheet

## Analysis Applications
- Model performance evaluation
- Strategy backtesting preparation
- Research and development
- Performance reporting
- Historical trend analysis

## Support
For issues with database access or export functionality, refer to Bfexplorer documentation on database integration and machine learning data access.
