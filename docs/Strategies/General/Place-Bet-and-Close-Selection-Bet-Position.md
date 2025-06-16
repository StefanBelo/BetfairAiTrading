# Place Bet and Close Selection Bet Position Strategy

## Overview
This strategy combines opening and closing operations by placing a bet and then automatically monitoring and closing the position based on profit/loss targets.

## Category
Trading

## Description
A comprehensive trading strategy that handles both position entry and exit. It places an initial bet according to specified parameters and then monitors the position for closure based on profit/loss targets, time limits, or other criteria.

## Parameters

### Bet Attribute
- **OpenBetPosition** (Required): Configuration for placing the initial bet
  - All Place Bet parameters including BetType, Odds, Stake, etc.
- **CloseBetPosition** (Required): Configuration for closing the position
  - All Close Position parameters including profit/loss targets, timing, etc.

### Selection
- **SortSelectionsBy** (Optional): Sort selections by LastPriceTraded, TotalMatched, or DoNotSort
- **ExecuteOnSelection** (Optional): Specify which selection to execute on

### Market
- **StopBotExecutionOnMarketVersionChange** (Optional): Stop strategy when market version changes
- **EvaluateEntryCriteriaOnlyOnce** (Optional): Evaluate entry criteria only once
- **StopMarketMonitoring** (Optional): Stop market monitoring after strategy completion

### Miscellaneous
- **StrategyReference** (Optional): Custom reference string for the strategy (max 15 characters)

## Key Features
- **Complete Trading Cycle**: Entry and exit in one strategy
- **Comprehensive Configuration**: Full control over both phases
- **Integrated Monitoring**: Seamless transition from entry to exit
- **Risk Management**: Built-in position closure controls

## Open Bet Position Parameters
Includes all Place Bet strategy parameters:
- **Bet Configuration**: BetType, Odds, Stake, etc.
- **Timing Controls**: When to place the bet
- **Risk Parameters**: Odds ranges, improvements, etc.
- **Market Conditions**: In-play settings, chase options

## Close Bet Position Parameters
Includes all Close Position strategy parameters:
- **Profit Targets**: Money, ticks, or percentage
- **Loss Limits**: Stop-loss configurations
- **Time Exits**: Time-based position closure
- **Hedging Options**: Position hedging controls

## Trading Flow
1. **Entry Phase**: Place initial bet according to OpenBetPosition settings
2. **Monitoring Phase**: Wait for bet to be matched and position established
3. **Management Phase**: Monitor position according to CloseBetPosition settings
4. **Exit Phase**: Close position when targets/conditions are met

## Use Cases
- Complete automated trading strategies
- Scalping with defined entry and exit
- Swing trading with profit targets
- Risk-managed position trading
- Systematic trading approaches

## Examples
- **Scalping**: Back at current odds, take 2-tick profit
- **Swing Trade**: Lay favorite, close at 20% profit or 10% loss
- **Time Trade**: Open position, close 5 minutes before event
- **Risk Trade**: Enter with stop-loss and profit target

## Strategy Benefits
- **Simplicity**: One strategy handles complete trade cycle
- **Consistency**: Integrated approach ensures coordination
- **Risk Control**: Built-in exit management
- **Automation**: Complete hands-off trading

## Configuration Example
- **Open**: Back selection at 2.0 with £50 stake
- **Close**: Take profit at £20 or stop loss at £10
- **Timing**: Open 5 minutes before start, close if not profitable after 10 minutes

## Risk Management
- Always configure both profit and loss targets
- Set appropriate time limits for position closure
- Monitor total exposure across multiple positions
- Consider market conditions for entry/exit timing
