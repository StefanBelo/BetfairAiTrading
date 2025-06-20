# General Strategy Documentation Index

This directory contains comprehensive documentation for all general Bfexplorer strategies. Each strategy is documented with detailed parameter descriptions, use cases, and examples.

## Betting Strategies

### Basic Betting
- **[Place Bet](Place-Bet.md)** - Fundamental betting strategy with comprehensive bet placement options
- **[Place Bet - Be the First in Queue](Place-Bet-Be-the-First-in-Queue.md)** - Advanced betting with queue position management and odds chasing
- **[Place Bet - Fill or Kill](Place-Bet-Fill-or-Kill.md)** - Time-sensitive betting with immediate execution requirements
- **[Place Dutching Bets](Place-Dutching-Bets.md)** - Multi-selection betting for guaranteed equal profit
- **[Place SP Bet](Place-SP-Bet.md)** - Starting Price betting with limit protection

### Advanced Betting Techniques
- **[Place Bet By Drip Feeding](Place-Bet-By-Drip-Feeding.md)** - Gradually place large bets to minimize market impact
- **[Drip Feeding Trading](Drip-Feeding-Trading.md)** - Combine drip feeding with trailing stop loss management
- **[Trade Feeding](Trade-Feeding.md)** - Alternative trading approach with feeding techniques

### Trading Strategies

#### Position Management
- **[Close Selection Bet Position](Close-Selection-Bet-Position.md)** - Close individual selection positions based on profit/loss targets
- **[Close Selection Bet Position at Odds](Close-Selection-Bet-Position-at-Odds.md)** - Close positions when specific odds levels are reached
- **[Close Market Bet Position](Close-Market-Bet-Position.md)** - Market-wide position management and closure
- **[Trailing Stop Loss](Trailing-Stop-Loss.md)** - Dynamic profit protection with trailing stops

#### Scalping & Quick Trading
- **[Tick Offset](Tick-Offset.md)** - Scalping strategy for quick tick-based profits
- **[Scratch Trading](Scratch-Trading.md)** - Spread trading for small, consistent profits

#### Combined Strategies
- **[Place Bet and Close Selection Bet Position](Place-Bet-and-Close-Selection-Bet-Position.md)** - Complete trading cycle with entry and exit management

## Strategy Execution & Control

### Multi-Strategy Execution
- **[Execute on Selections](Execute-on-Selections.md)** - Run strategies across multiple selections
- **[Execute on a Selection](Execute-On-A-Selection.md)** - Conditional strategy execution based on real-time market data criteria
- **[Execute Strategies](Execute-Strategies.md)** - Execute multiple strategies simultaneously
- **[Sequence Execution](Sequence-Execution.md)** - Run strategies in sequential order
- **[Concurrent Execution](Concurrent-Execution.md)** - Run strategies in parallel

### Strategy Control & Management
- **[Limit Action Bot Execution](Limit-Action-Bot-Execution.md)** - Control and limit the number of strategy executions
- **[Stake Percentage Of Available Balance](Stake-Percentage-Of-Available-Balance.md)** - Dynamic stake sizing based on available balance
- **[Dutch Bet Aggregation](Dutch-Bet-Aggregation.md)** - Aggregate and manage multiple dutch betting strategies
- **[Cancel Strategies on Selection](Cancel-Strategies-on-Selection.md)** - Cancel running strategies on specific selections
- **[Execute Trigger Strategy](Execute-Trigger-Strategy.md)** - Execute strategies based on external triggers
- **[Execute Strategy Rules Bot](Execute-Strategy-Rules-Bot.md)** - Rule-based strategy execution system

### Conditional & Timing Control
- **[If Then Else](If-Then-Else.md)** - Conditional strategy execution based on criteria
- **[Execute on a Selection](Execute-On-A-Selection.md)** - Advanced conditional execution using real-time market data
- **[Execute at Time](Execute-at-Time.md)** - Time-based strategy execution control
- **[Execute Till Target Profit](Execute-Till-Target-Profit.md)** - Profit-targeting with Martingale and staking management
- **[Repeat Until](Repeat-Until.md)** - Repeat strategies until conditions are met

### Management & Control
- **[Stop Strategies and Cancel Bets](Stop-Strategies-and-Cancel-Bets.md)** - Strategy termination and bet management

## Strategy Categories

### By Complexity Level
- **Beginner**: Place Bet, Execute at Time
- **Intermediate**: Close Position strategies, Dutching, SP Betting, Execute on a Selection
- **Advanced**: Multi-strategy execution, Conditional logic combinations, Trading combinations

### By Use Case
- **Simple Betting**: Place Bet, SP Bet
- **Risk Management**: Close Position strategies, Stop Strategies, Trailing Stop
- **Trading**: Tick Offset, Scratch Trading, Position management
- **Automation**: Execute Till Target Profit, Repeat Until, Multi-strategy execution
- **Conditional Logic**: If Then Else, Execute on a Selection
- **Portfolio Management**: Close Market Position, Dutching, Multi-selection strategies

### By Market Type
- **Pre-Race**: Place Bet, SP Bet, Time-based execution
- **In-Play**: Trading strategies, Quick execution, Position management
- **Both**: Most strategies can work in both environments

## Parameter Groups Reference

### Common Parameter Groups
- **Bet**: Core betting parameters (BetType, Odds, Stake)
- **Bet Attribute**: Advanced betting options (Chasing, Improvements, Timing)
- **Stake**: Stake amounts and types
- **Stake Attribute**: Stake calculation methods
- **Profit/Loss**: Target and limit settings
- **Time**: Timing and scheduling options
- **Selection**: Selection targeting and sorting
- **Market**: Market-level controls and monitoring
- **Strategy**: Strategy execution and coordination
- **Miscellaneous**: Additional options and references

## Getting Started

1. **New Users**: Start with [Place Bet](Place-Bet.md) to understand basic betting
2. **Traders**: Explore [Tick Offset](Tick-Offset.md) and position management strategies
3. **Conditional Logic**: Try [Execute on a Selection](Execute-On-A-Selection.md) for data-driven automation
4. **Advanced Users**: Combine multiple strategies using execution control strategies
5. **Risk Management**: Implement [Stop Strategies](Stop-Strategies-and-Cancel-Bets.md) and position limits

## Best Practices

- Always set appropriate profit and loss targets
- Use time-based controls for session management
- Start with simple strategies before combining complex ones
- Test strategies in demo mode before live trading
- Monitor total exposure across multiple strategies
- Have emergency stop procedures in place

## Support

For detailed parameter explanations and advanced configurations, refer to each individual strategy documentation file. Each file contains comprehensive parameter descriptions, use cases, examples, and risk considerations.
