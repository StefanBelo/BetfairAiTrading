# Tennis Show ATP Data

**Category:** Tennis  
**Strategy ID:** 6901

## Description

A tennis data strategy that displays ATP (Association of Tennis Professionals) player data and statistics for analysis purposes. This strategy focuses on loading and presenting professional tennis data to support betting decisions.

## Parameters

### Data
- **LoadAtpData** (Optional) - Load ATP data (Boolean)
- **UpdateInterval** (Optional) - Data update interval (TimeSpan)

### Market
- **EvaluateEntryCriteriaOnlyOnce** (Optional) - Set this parameter to True to evaluate the entry criteria only once
- **StopMarketMonitoring** (Optional) - Set this parameter to True to stop market monitoring after the strategy completes execution. Monitoring will stop only if no other bot is active in the market

### Miscellaneous
- **StrategyReference** (Optional) - Set this parameter to specify which strategy sent the bet. The string is limited to 15 characters

## Usage Examples

### Real-time ATP Data
- **LoadAtpData**: `true`
- **UpdateInterval**: `00:05:00` (5 minutes)

### Periodic Data Updates
- **LoadAtpData**: `true`
- **UpdateInterval**: `00:01:00` (1 minute)

## Best Practices

1. Enable ATP data loading for comprehensive player information
2. Set appropriate update intervals based on match dynamics
3. Use for pre-match analysis and player comparison
4. Combine with other tennis strategies for enhanced decision making
