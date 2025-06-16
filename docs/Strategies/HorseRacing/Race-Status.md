# Race Status

**Category:** Horse Racing  
**Strategy ID:** 2002

## Description

A horse racing monitoring strategy that tracks and displays the current status of horse races. This strategy provides real-time updates on race conditions, timing, and status changes, making it essential for race monitoring and betting decision support.

## Parameters

### Data
- **UpdateInterval** (Optional) - Set the update interval (TimeSpan)

### Market
- **EvaluateEntryCriteriaOnlyOnce** (Optional) - Set this parameter to True to evaluate the entry criteria only once
- **StopMarketMonitoring** (Optional) - Set this parameter to True to stop market monitoring after the strategy completes execution. Monitoring will stop only if no other bot is active in the market

### Miscellaneous
- **StrategyReference** (Optional) - Set this parameter to specify which strategy sent the bet. The string is limited to 15 characters

## Usage Examples

### Real-time Race Monitoring
- **UpdateInterval**: `00:00:30` (30 seconds)

### Periodic Status Checks
- **UpdateInterval**: `00:02:00` (2 minutes)

### High-frequency Updates
- **UpdateInterval**: `00:00:10` (10 seconds)

## Best Practices

1. Set update intervals appropriate for race timing requirements
2. Use for monitoring race delays and status changes
3. Combine with other horse racing strategies for comprehensive coverage
4. Consider system resources when setting high-frequency updates
