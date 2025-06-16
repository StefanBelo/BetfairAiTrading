# Stop Strategies and Cancel Bets Strategy

## Overview
This strategy stops running strategies and cancels unmatched bets, providing comprehensive control over strategy termination and bet management.

## Category
General Strategy

## Description
A powerful management strategy that provides centralized control for stopping multiple running strategies and canceling unmatched bets. It's essential for risk management, session control, and emergency stops.

## Parameters

### Strategy
- **StrategyNames** (Optional): Names of strategies to stop, separated by semicolons (e.g., 'bot1;bot2')

### Market
- **StopRunningBots** (Optional): Stop all running bots and cancel unmatched bets
- **EvaluateEntryCriteriaOnlyOnce** (Optional): Evaluate entry criteria only once
- **StopMarketMonitoring** (Optional): Stop market monitoring after strategy completion

### Time
- **StopTimeSpan** (Optional): Stop at specific time relative to event start

### Miscellaneous
- **StrategyReference** (Optional): Custom reference string for the strategy (max 15 characters)

## Key Features
- **Selective Stopping**: Stop specific named strategies
- **Mass Termination**: Stop all running bots with one command
- **Bet Cancellation**: Cancel all unmatched bets
- **Time-Based Control**: Automatic stopping at predetermined times

## Operation Modes
1. **Selective Stop**: Target specific strategies by name
2. **Global Stop**: Stop all running strategies
3. **Time-Triggered**: Automatic stopping at set times
4. **Manual Trigger**: Immediate execution when needed

## Use Cases
- Emergency stop situations
- End-of-session cleanup
- Risk management interventions
- Time-based session management
- Market closure preparation

## Examples
- Stop specific strategies: 'Trading Bot;Scalp Bot'
- Stop all bots before market closure
- Emergency stop during volatile conditions
- Scheduled session end 5 minutes before event
- Risk intervention when exposure too high

## Strategy Name Format
- **Multiple Strategies**: Separate with semicolons (;)
- **Exact Names**: Must match strategy names exactly
- **Case Sensitive**: Strategy names are case-sensitive

## Time-Based Stopping
- **Pre-Event**: Stop before event starts (negative time)
- **In-Play**: Stop during event (positive time)
- **Market Closure**: Stop before market closes
- **Session End**: Predetermined session termination

## Risk Management Applications
- **Exposure Control**: Stop when total exposure too high
- **Volatility Response**: Emergency stops during market chaos
- **News Events**: Stop trading around news announcements
- **Technical Issues**: Stop during system problems

## Benefits
- **Centralized Control**: Single point for strategy management
- **Risk Protection**: Quick intervention capability
- **Session Management**: Controlled session termination
- **Bet Cleanup**: Automatic unmatched bet cancellation

## Integration with Other Strategies
- **If-Then-Else**: Conditional stopping based on criteria
- **Time Triggers**: Automatic execution at set times
- **Risk Monitors**: Integration with risk management systems
- **Alert Systems**: Response to external alerts

## Best Practices
- **Regular Monitoring**: Check running strategies periodically
- **Time Limits**: Set maximum session durations
- **Risk Limits**: Define maximum exposure levels
- **Emergency Plans**: Have quick-stop procedures ready
