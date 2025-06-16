# Execute on Associated Market Strategy

## Overview
This strategy executes another strategy on an associated market (e.g., executing a strategy on the Over/Under 2.5 Goals market when analyzing a Match Odds market).

## Category
General Strategy

## Description
A market linking strategy that allows execution of strategies across related markets. This is particularly useful in football betting where multiple markets are available for the same event (Match Odds, Over/Under, Correct Score, etc.).

## Parameters

### Strategy
- **StrategyName** (Required): Enter the name of the strategy you wish to execute
- **MarketName** (Required): Specify the associated market (e.g., 'OVER_UNDER_2_5')

### Time
- **ExecutionTimeout** (Optional): Delay before executing the strategy

### Market
- **EvaluateEntryCriteriaOnlyOnce** (Optional): Evaluate entry criteria only once
- **StopMarketMonitoring** (Optional): Stop market monitoring after strategy completion

### Miscellaneous
- **StrategyReference** (Optional): Custom reference string for the strategy (max 15 characters)

## Key Features
- **Cross-Market Execution**: Execute strategies on related markets
- **Market Association**: Automatic linking between related markets
- **Delayed Execution**: Optional timeout before strategy execution
- **Event Coordination**: Coordinate strategies across multiple markets for same event

## Associated Market Examples
- **OVER_UNDER_2_5**: Total goals over/under 2.5
- **CORRECT_SCORE**: Correct score markets
- **FIRST_GOALSCORER**: First goalscorer markets
- **BOTH_TEAMS_TO_SCORE**: Both teams to score markets
- **HANDICAP**: Asian handicap markets

## Use Cases
- Football strategy coordination across multiple markets
- Cross-market arbitrage opportunities
- Related market hedging strategies
- Multi-market trading approaches
- Event-based strategy distribution

## Examples
- Analyze Match Odds, execute strategy on Over/Under 2.5
- Monitor Correct Score, place bets on Both Teams to Score
- Use Match Odds signals for First Goalscorer betting
- Coordinate handicap betting with match odds analysis

## Benefits
- **Market Expansion**: Access to multiple related markets
- **Strategy Efficiency**: Single analysis, multiple market execution
- **Risk Distribution**: Spread risk across related markets
- **Opportunity Maximization**: Capture value in associated markets
