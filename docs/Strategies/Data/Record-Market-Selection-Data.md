# Record Market Selection Data

**Category:** Data  
**Strategy ID:** 2003

## Description

A comprehensive data recording strategy that captures detailed market selection data at specified intervals. This strategy is essential for creating historical databases, backtesting strategies, and analyzing market behavior patterns over time.

## Parameters

### Data
- **RecordingInterval** (Optional) - Set the recoding interval for pre event data (TimeSpan)
- **InPlayRecordingInterval** (Optional) - Set the recoding interval for in-play data (TimeSpan)

### Market
- **EvaluateEntryCriteriaOnlyOnce** (Optional) - Set this parameter to True to evaluate the entry criteria only once
- **StopMarketMonitoring** (Optional) - Set this parameter to True to stop market monitoring after the strategy completes execution. Monitoring will stop only if no other bot is active in the market

### Miscellaneous
- **StrategyReference** (Optional) - Set this parameter to specify which strategy sent the bet. The string is limited to 15 characters

## Usage Examples

### Comprehensive Recording
- **RecordingInterval**: `00:01:00` (1 minute pre-event)
- **InPlayRecordingInterval**: `00:00:30` (30 seconds in-play)

### High-Frequency Capture
- **RecordingInterval**: `00:00:30` (30 seconds pre-event)
- **InPlayRecordingInterval**: `00:00:10` (10 seconds in-play)

### Conservative Recording
- **RecordingInterval**: `00:05:00` (5 minutes pre-event)
- **InPlayRecordingInterval**: `00:01:00` (1 minute in-play)

## Best Practices

1. **Interval Selection**: Balance data granularity with storage requirements
2. **In-Play Focus**: Use shorter intervals for in-play due to rapid changes
3. **Storage Planning**: Ensure adequate storage for long-term data collection
4. **Data Quality**: Regularly validate recorded data for accuracy
5. **Backup Strategy**: Implement regular backups of collected data
6. **Performance Impact**: Monitor system performance with intensive recording
