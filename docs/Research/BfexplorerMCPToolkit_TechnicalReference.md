---
title: "Bfexplorer MCP Strategy Building Toolkit - Technical Reference"
description: "Complete technical inventory of strategy building capabilities in Bfexplorer"
date: 2026-04-28
tags: [technical-reference, mcp-tools, strategy-templates, bfexplorer, architecture]
---

# Bfexplorer MCP Strategy Building Toolkit - Technical Reference

## Overview

This document provides a complete technical inventory of the 113 strategy templates available through Bfexplorer's MCP (Model Context Protocol) server, organized by category and composability.

---

## 1. Core Betting Strategies (14 templates)

### 1.1 Basic Betting

#### ID: 0 - "Place Bet"
**Category**: General  
**Parameters**: 21  
**Composable**: Yes (Used in 80%+ of compositions)

```json
{
  "name": "Back Bet 10 EUR",
  "template": "Place Bet",
  "parameters": {
    "BetType": "Back|Lay",
    "Stake": 10.0,
    "StakeType": "Stake|Liability|Payout|TickProfit|NetTickProfit|BankrollPercentage",
    "Odds": 0.0,
    "MinimumOdds": 1.01,
    "MaximumOdds": 1000.0,
    "PlaceBetInAllowedOddsRange": true,
    "PlaceBetTimeSpan": "-0:05:00|null",
    "AllowPlacingBetInPlay": true,
    "AtInPlayKeepBet": false,
    "OfferMyBet": false,
    "OddsImprovement": 0,
    "ChaseOddsTimeout": null,
    "MinimumOddsDifference": 0,
    "MaximumOddsDifference": 0,
    "SortSelectionsBy": "DoNotSort|LastPriceTraded|TotalMatched",
    "ExecuteOnSelection": 0,
    "EvaluateEntryCriteriaOnlyOnce": false,
    "StopBotExecutionOnMarketVersionChange": false,
    "StopMarketMonitoring": false,
    "StrategyReference": ""
  }
}
```

**Key Features**:
- ✓ Time-based execution (PlaceBetTimeSpan: "-0:05:00")
- ✓ Odds range matching (MinimumOdds, MaximumOdds)
- ✓ Odds chasing (ChaseOddsTimeout)
- ✓ Multiple stake types (Stake, Liability, Profit-based)
- ✓ Selection sorting (by odds or volume)

**Common Compositions**:
```
Place Bet → Trailing Stop Loss
Place Bet → Close Selection Bet Position
Place Bet → Close Market Bet Position
```

---

#### ID: 2 - "Place Bet - Fill or Kill"
**Category**: General  
**Parameters**: 12  
**Composable**: Yes

**Difference from Place Bet**: Automatically cancels unmatched bet after timeout

```json
{
  "parameters": {
    "BetType": "Back|Lay",
    "Stake": 10.0,
    "Odds": 0.0,
    "BetMatchingTimeout": "00:00:05",
    "AtInPlayKeepBet": false
  }
}
```

**Use Case**: High-frequency matching without long hangs

---

### 1.2 Position Management

#### ID: 3 - "Close Selection Bet Position"
**Category**: Trading  
**Parameters**: 18  
**Composable**: Yes (Common exit strategy)

```json
{
  "name": "Close Position - 5 Tick Profit",
  "template": "Close Selection Bet Position",
  "parameters": {
    "ProfitLossType": "Money|Ticks|Percentage",
    "Profit": 5.0,
    "Loss": 3.0,
    "HedgingEnabled": true,
    "CheckingLastPriceTraded": false,
    "OfferMyBet": false,
    "ClosePositionImmediately": false,
    "BetMatchingTimeout": "00:00:02",
    "ClosePositionTimeSpan": null
  }
}
```

**Key Features**:
- ✓ Asymmetric P&L targets (different profit vs loss)
- ✓ Hedging support (lay/back against position)
- ✓ Time-based closing (e.g., "5 minutes into match")
- ✓ Tick-based, money-based, or percentage-based targets

**Common Flows**:
```
Place Bet → Close Position (forms complete trade cycle)
Trailing Stop Loss → Close Position (backup exit)
```

---

#### ID: 9 - "Close Market Bet Position"
**Category**: Trading  
**Parameters**: 12  
**Composable**: Yes

**Difference**: Closes ALL positions on market (not just selection)

```json
{
  "parameters": {
    "Profit": 10.0,
    "Loss": 5.0,
    "ProfitOrLossInPercentage": false,
    "WaitForValidBetPosition": true,
    "AllowBotExecutionTermination": false
  }
}
```

---

### 1.3 Trading Strategies

#### ID: 6 - "Tick Offset"
**Category**: Trading  
**Parameters**: 11  
**Composable**: Limited

Micro-scalping strategy: Back/Lay and close at profit target

```json
{
  "parameters": {
    "BetType": "Back",
    "Stake": 10.0,
    "Profit": 3,  // Close after 3 ticks profit
    "SortSelectionsBy": "DoNotSort"
  }
}
```

---

#### ID: 7 - "Scratch Trading"
**Category**: Trading  
**Parameters**: 10  
**Composable**: Limited

Zero-profit trading - close position with exact hedge

```json
{
  "parameters": {
    "Stake": 10.0,
    "Liquidity": 5.0,  // Min available to trade
    "Scratch": 2.0     // Tolerance for "break even"
  }
}
```

---

#### ID: 8 - "Trailing Stop Loss"
**Category**: Trading  
**Parameters**: 9  
**Composable**: Yes (Very common)

Dynamic loss protection - moves stop with profit

```json
{
  "name": "Trailing Stop Loss 2 Ticks",
  "template": "Trailing Stop Loss",
  "parameters": {
    "Loss": 2,           // Ticks to protect profit
    "HedgingEnabled": true,
    "BetMatchingTimeout": "00:00:02",
    "ExecuteOnSelection": 0
  }
}
```

**Use Case**: 
```
Place Bet → Trailing Stop Loss → Close Position
(Entry) → (Protect Gains) → (Exit if target met)
```

---

#### ID: 5 (Advanced Ladder Strategies)
**Category**: Trading (Ladder)  
**Parameters**: Complex nested structure

**Sub-components**:
- PlaceBetLadder: Multiple bets at different odds/stakes
- CloseBetPosition: Profit/loss targets with hedging
- SortSelectionsBy: Selection filtering

```json
{
  "name": "Ladder Strategy Example",
  "template": "Place Bet - Ladder",
  "parameters": {
    "PlaceBets": [
      {
        "BetType": "Back",
        "Stake": 5.0,
        "Odds": 2.0,
        "PlaceBetTimeSpan": "-0:01:00"
      },
      {
        "BetType": "Back",
        "Stake": 5.0,
        "Odds": 2.5,
        "PlaceBetTimeSpan": "0:00:00"
      },
      {
        "BetType": "Back",
        "Stake": 5.0,
        "Odds": 3.0,
        "PlaceBetTimeSpan": "0:01:00"
      }
    ],
    "CloseBetPosition": {
      "Profit": 10.0,
      "Loss": 5.0,
      "HedgingEnabled": false
    }
  }
}
```

**Use Case**: Grid trading, staggered entries, DCA strategies

---

## 2. Control Flow Strategies (9 templates) ⭐

These are **critical for composability** - they enable strategy combinations.

### ID: 10 - "Execute on Selections"
**Category**: Control Flow  
**Parameters**: 6  
**Composable**: Yes

Applies a single strategy to multiple selections

```json
{
  "name": "Back Top 3 Selections",
  "template": "Execute on Selections",
  "parameters": {
    "StrategyName": "Back Bet 10 EUR",      // Reference to existing strategy
    "OnSelections": "1,2,3",                // Selection indices (1-based)
    "SortSelectionsBy": "LastPriceTraded",  // Sort before executing
    "EvaluateEntryCriteriaOnlyOnce": false
  }
}
```

**Execution Model**:
```
For each selection in [1,2,3]:
  Execute "Back Bet 10 EUR" strategy
```

---

### ID: 11 - "Execute Strategies"
**Category**: Control Flow  
**Parameters**: 8  
**Composable**: Yes

Sequential execution of multiple strategies

```json
{
  "name": "Back and Trail",
  "template": "Execute Strategies",
  "parameters": {
    "StrategyNames": "Back Bet 10 EUR;Trailing Stop Loss 2 Ticks",
    "UseLadderParameters": false,
    "ExecuteOnSelection": 0,
    "StopBotExecutionOnMarketVersionChange": false
  }
}
```

**Execution Model**:
```
1. Execute "Back Bet 10 EUR"
2. Wait for completion or condition
3. Execute "Trailing Stop Loss 2 Ticks"
```

**Key Difference from Sequence Execution**: No shared bet position state

---

### ID: 15 - "Sequence Execution" ⭐ MOST FLEXIBLE
**Category**: Control Flow  
**Parameters**: 8  
**Composable**: Yes

Ordered execution with optional shared state

```json
{
  "name": "Complete Trading Cycle",
  "template": "Sequence Execution",
  "parameters": {
    "StrategyNames": "Back Bet 10 EUR;Trailing Stop Loss 2 Ticks;Close Position 5 Tick",
    "ShareBetPosition": true,              // Critical: share position data
    "ExecuteOnSelection": 0
  }
}
```

**Execution Model**:
```
1. Place Bet (creates position)
2. Trailing Stop Loss (monitors position, adjusts stop)
3. Close Position (closes when target hit)
  └─ All strategies access same position data
```

**This is the IDEAL pattern for most trading strategies**

---

### ID: 16 - "Concurrent Execution"
**Category**: Control Flow  
**Parameters**: 8  
**Composable**: Yes

Parallel execution of multiple strategies

```json
{
  "name": "Run Multiple Trades",
  "template": "Concurrent Execution",
  "parameters": {
    "StrategyNames": "Back Selection 1;Back Selection 2;Back Selection 3",
    "EndExecutionIfAnyBotEnds": false,   // Continue if one finishes
    "ExecuteOnSelection": 0
  }
}
```

**Execution Model**:
```
├─ Execute "Back Selection 1" (parallel)
├─ Execute "Back Selection 2" (parallel)
└─ Execute "Back Selection 3" (parallel)
  └─ All run simultaneously
```

**Use Case**: Multi-selection, multi-market strategies, hedging

---

### ID: 14 - "If Then Else"
**Category**: Control Flow  
**Parameters**: 11  
**Composable**: Yes

Conditional branching based on criteria

```json
{
  "name": "Smart Entry",
  "template": "If Then Else",
  "parameters": {
    "IfThenCriteria": "LastPriceTraded > 3.0 AND MarketVolume > 10000",
    "IfThenStrategyName": "Aggressive Back Bet",    // Execute if true
    "IfElseCriteria": "LastPriceTraded < 1.5",
    "IfElseStrategyName": "Conservative Back Bet",  // Execute if true
    "EvaluateIfCriteriaOnlyOnce": true
  }
}
```

**Execution Model**:
```
IF [Criteria 1 evaluates to True]
  THEN Execute [Strategy 1]
ELSE IF [Criteria 2 evaluates to True]
  THEN Execute [Strategy 2]
```

**Available Criteria**: LastPriceTraded, MarketVolume, Liquidity, OpenInterest, etc.

---

### ID: 13 - "Execute Till Target Profit"
**Category**: Control Flow  
**Parameters**: 15  
**Composable**: Limited

Loop execution until profit/loss target reached

```json
{
  "name": "Keep Betting Till Profit",
  "template": "Execute Till Target Profit",
  "parameters": {
    "StrategyName": "Back Bet 10 EUR",
    "TargetProfit": 50.0,                 // Stop when profit = 50
    "TargetLoss": -20.0,                  // Stop when loss = -20
    "ResetStakingPlan": 1,                // Reset after each loss
    "MartingaleStakeFactor": 1.5,         // Increase stake by 50% after loss
    "ExecuteAtTime": "-0:05:00"           // Start 5 min before event
  }
}
```

**Execution Model**:
```
Iteration 1: Execute strategy
  ├─ Profit = 10 EUR (< 50, continue)
Iteration 2: Execute strategy
  ├─ Profit = 35 EUR (< 50, continue)
Iteration 3: Execute strategy
  ├─ Profit = 52 EUR (≥ 50, STOP)
```

**Common Patterns**:
- Daily profit targets
- Session management
- Staking plan execution
- Martingale sequences

---

### ID: 17 - "Repeat Until"
**Category**: Control Flow  
**Parameters**: 11  
**Composable**: Yes

General-purpose loop with multiple termination conditions

```json
{
  "name": "Repeat 5 Times",
  "template": "Repeat Until",
  "parameters": {
    "StrategyName": "Back Bet 10 EUR",
    "RepeatUntilParameter": "NumberOfIterations",
    "TargetValue": 5,                     // Repeat 5 times
    "NextIterationTimeout": "00:00:05"    // Wait 5 sec between iterations
  }
}
```

**Alternative Termination Conditions**:
```
"RepeatUntilParameter": "NumberOfIterations" → Target: Number
"RepeatUntilParameter": "TotalStake"         → Target: Money amount
"RepeatUntilParameter": "ProfitTarget"       → Target: Money amount
"RepeatUntilParameter": "LossTarget"         → Target: Money amount
```

---

### ID: 12 - "Execute on Associated Market"
**Category**: Control Flow  
**Parameters**: 6  
**Composable**: Limited

Cross-market strategy execution

```json
{
  "name": "Back Main, Lay Opposite",
  "template": "Execute on Associated Market",
  "parameters": {
    "StrategyName": "Close Position",
    "MarketName": "OVER_UNDER_2_5",        // Execute on related market
    "ExecutionTimeout": "0:00:30"          // Execute 30 sec after trigger
  }
}
```

**Use Case**: Correlated markets (Match Odds + Over/Under)

---

### ID: 18 - "Execute Trigger Strategy"
**Category**: Control Flow  
**Parameters**: 13  
**Composable**: Limited

Execute custom DLL-based trigger logic

```json
{
  "parameters": {
    "TriggerFilePathName": "C:\\MyTriggers\\MyCustomTrigger.dll",
    "TriggerParameters": "Stake:100.0;BetType:Back;MinOdds:1.5",
    "SelectionCriteria": "Favorites only"
  }
}
```

**For Advanced Users**: Implement custom decision logic in C#

---

## 3. Data Context Providers (29 available)

Data sources for decision-making in strategies

### Horse Racing Data (17 providers)
```
AtTheRacesBookmakersOdds
AtTheRacesDataForHorses
BetfairSpData
DbHorsesResults
DbJockeysResults
DbJockeysStatisticsData
DbTrainersResults
HorseRacingWinToBePlacedData
HorsesBaseBetfairFormData
HorsesBetfairRaceInfoData
OddscheckerBookmakersOdds
OlbgRaceTipsData
PedigreeDataForHorses
RacesResultsForRacingStattoData
RacesWinnersForRacingStattoData
RacingStattoData
RacingTvBookmakersOdds
RacingTvDataForHorses
RacingpostDataForHorses
TestRaceData
TimeformDataForHorses
TimeformFullDataForHorses
WeightOfMoneyData
```

### Football Data (2 providers)
```
FootballMatchScoreData
PlayingFootballMatchScoresData
```

### Tennis Data (1 provider)
```
PlayingTennisMatchScoresData
```

### General Market Data (9 providers)
```
MarketSelectionsCandleStickData
MarketSelectionsPriceHistoryData
MarketSelectionsTradedPricesData
WeightOfMoneyData
```

---

## 4. Strategy Composition Rules

### Valid Compositions

```
✓ SEQUENCE (most common):
Place Bet → Trailing Stop Loss → Close Position
  (Entry)     (Protect)          (Exit)

✓ CONDITIONAL:
IF [Odds > 3.0] THEN Back Aggressively
IF [EV > 10%] THEN Place Bet ELSE Do Nothing

✓ REPEAT/LOOP:
Repeat { Back Bet } Until Profit > 100 EUR

✓ CONCURRENT (parallel):
Run [ Back Selection 1, Back Selection 2, Back Selection 3 ] in parallel

✓ MULTI-LEVEL NESTING:
Sequence [
  Bet Placement,
  If-Then [Condition → Trailing Stop],
  Close Position
]
```

### Invalid Compositions (AI should flag these)

```
✗ Cannot reference undefined strategy
✗ Cannot have circular dependencies (A → B → A)
✗ Cannot mix incompatible data contexts
  (Horse Racing data with Tennis strategies)
✗ Cannot have unresolvable conditions
  (If criteria references non-existent market field)
✗ Cannot close position before placing bet
  (logical impossibility)
✗ Cannot execute Concurrent + Sequence on same bet
  (conflicting execution models)
```

---

## 5. Validation Rules for AI Agent

### Parameter Type Validation
```python
# Example validation rules the AI should apply

if strategy_type == "Place Bet":
    # BetType must be one of: Back, Lay
    validate(bet_type in ["Back", "Lay"])
    
    # Odds must be positive
    validate(min_odds > 1.0)
    validate(max_odds > min_odds)
    
    # Stake must be positive
    validate(stake > 0)
    
    # If StakeType is BankrollPercentage, stake must be 0-100
    if stake_type == "BankrollPercentage":
        validate(0 <= stake <= 100)

if strategy_type == "If Then Else":
    # Both criteria or both strategy pairs must exist
    validate(
        (if_then_criteria and if_then_strategy) or
        (if_else_criteria and if_else_strategy)
    )
    
    # Criteria must reference valid market fields
    for field in extract_fields(criteria):
        validate(field in available_market_fields)

if strategy_type == "Execute Strategies":
    # All referenced strategies must exist
    for strategy_name in strategy_names.split(";"):
        validate(strategy_exists(strategy_name))
```

### Composability Validation
```python
def validate_composition(parent_strategy, child_strategy):
    # Check if child can be composed into parent
    
    # Some strategies can't be nested
    if parent_strategy in ["Tick Offset", "Scratch Trading"]:
        raise CompositionError("Cannot nest into micro-scalping strategies")
    
    # Data context compatibility
    if parent_strategy == "Sequence Execution":
        validate_shared_context(child_strategies)
    
    # No circular dependencies
    validate_no_cycles(strategy_graph)
    
    return True
```

---

## 6. MCP Tool Reference for AI Agents

### Primary Tools

```python
# 1. Get all available templates
templates = get_all_strategy_templates()
# Returns: List[StrategyTemplate] with 113 items
# Use: Understand what's available, validate feasibility

# 2. Get specific template details
template = get_strategy_template("Place Bet")
# Returns: StrategyTemplate with parameter definitions
# Use: Understand parameter requirements for given template

# 3. Get all data contexts
data_sources = get_available_data_context_providers()
# Returns: List[DataContextProvider] with 29 items
# Use: Check what data is available for decision-making

# 4. Create strategy settings from templates
strategy = create_strategy_settings(
    strategyTemplateParameters = JSON([
        {
            "name": "Back Bet 10 EUR",
            "template": "Place Bet",
            "parameters": {"BetType": "Back", "Stake": 10.0}
        },
        {
            "name": "Trailing Stop Loss 2 Ticks",
            "template": "Trailing Stop Loss",
            "parameters": {"Loss": 2, "HedgingEnabled": True}
        },
        {
            "name": "Back and Trail",
            "template": "Sequence Execution",
            "parameters": {
                "StrategyNames": "Back Bet 10 EUR;Trailing Stop Loss 2 Ticks"
            }
        }
    ])
)
# Use: Persist strategy configuration for later execution

# 5. Execute strategy on market/selection
execute_strategy_settings(
    strategyName="Back and Trail",
    marketId="1.123456",
    selectionId="7890123"
)
# Use: Actually run the strategy on a live market

# 6. Get market data for decision-making
market_data = get_all_data_context_for_market(
    dataContextNames=["MarketSelectionsTradedPricesData", "WeightOfMoneyData"],
    marketId="1.123456"
)
# Use: Make informed decisions about strategy parameters
```

---

## 7. Practical Examples

### Example 1: Simple Back Bet

```json
{
  "strategies": [
    {
      "name": "Back Horse #3",
      "template": "Place Bet",
      "parameters": {
        "BetType": "Back",
        "Stake": 20.0,
        "MinimumOdds": 2.0,
        "MaximumOdds": 5.0,
        "PlaceBetInAllowedOddsRange": true,
        "AllowPlacingBetInPlay": false
      }
    }
  ],
  "dataContext": ["HorsesBaseBetfairFormData", "RacingTvData"],
  "riskManagement": {
    "maxConcurrentBets": 3,
    "dailyLossLimit": 100.0
  }
}
```

---

### Example 2: EV-Based Trading Strategy

```json
{
  "strategies": [
    {
      "name": "Back If EV Positive",
      "template": "If Then Else",
      "parameters": {
        "IfThenCriteria": "ExpectedValue > 0.1",
        "IfThenStrategyName": "Aggressive Back Bet",
        "IfElseCriteria": "ExpectedValue < -0.1",
        "IfElseStrategyName": "Lay Against"
      }
    },
    {
      "name": "Aggressive Back Bet",
      "template": "Place Bet",
      "parameters": {
        "BetType": "Back",
        "Stake": 50.0,
        "MinimumOdds": 1.5,
        "MaximumOdds": 10.0
      }
    },
    {
      "name": "Lay Against",
      "template": "Place Bet",
      "parameters": {
        "BetType": "Lay",
        "Stake": 30.0,
        "MinimumOdds": 1.5,
        "MaximumOdds": 10.0
      }
    }
  ],
  "dataContext": ["WeightOfMoneyData", "MarketSelectionsTradedPricesData"],
  "riskManagement": {
    "dailyLossLimit": 200.0,
    "maxConcurrentBets": 5
  }
}
```

---

### Example 3: Betting Ladder with Risk Management

```json
{
  "strategies": [
    {
      "name": "Betting Ladder 1-2-3",
      "template": "Place Bet - Ladder",
      "parameters": {
        "PlaceBets": [
          {"BetType": "Back", "Stake": 10, "Odds": 2.0, "PlaceBetTimeSpan": "-0:02:00"},
          {"BetType": "Back", "Stake": 20, "Odds": 3.0, "PlaceBetTimeSpan": "-0:01:00"},
          {"BetType": "Back", "Stake": 30, "Odds": 4.0, "PlaceBetTimeSpan": "0:00:00"}
        ],
        "CloseBetPosition": {
          "Profit": 100.0,
          "Loss": 50.0,
          "HedgingEnabled": true
        }
      }
    }
  ],
  "controlFlow": "Sequence Execution",
  "dataContext": ["RacingTvData", "WeightOfMoneyData"]
}
```

---

### Example 4: Daily P&L Management with Martingale

```json
{
  "strategies": [
    {
      "name": "Daily Betting Session",
      "template": "Execute Till Target Profit",
      "parameters": {
        "StrategyName": "Standard Back Bet",
        "TargetProfit": 100.0,
        "TargetLoss": -50.0,
        "MartingaleStakeFactor": 1.5,
        "ResetStakingPlan": 1,
        "ExecuteAtTime": "-0:05:00"
      }
    },
    {
      "name": "Standard Back Bet",
      "template": "Sequence Execution",
      "parameters": {
        "StrategyNames": "Place Back;Trailing Stop;Close Position",
        "ShareBetPosition": true
      }
    }
  ],
  "dataContext": ["MarketSelectionsTradedPricesData"]
}
```

---

## 8. Composition Complexity Levels

### Level 1: Beginner (No composition)
```
Single Strategy:
Place Bet (with automatic close)
```

### Level 2: Intermediate (Basic composition)
```
Sequence:
Place Bet → Close Position

Or

Conditional:
IF [condition] THEN Place Bet
```

### Level 3: Advanced (Complex composition)
```
Sequence with nested Conditional:
Place Bet 
  → IF [profit > 10] THEN Close Position ELSE Trailing Stop Loss
  → Close Position
```

### Level 4: Expert (Multi-level nesting)
```
Execute Till Target [
  Repeat [
    Concurrent [
      Sequence [Back → Trail → Close],
      Sequence [Lay → Trail → Close]
    ]
  ]
]
```

---

## 9. Performance Considerations

### Execution Speed Requirements

| Strategy Type | Typical Execution Time | Critical for |
|---|---|---|
| Place Bet | 500ms - 2s | Pre-off, in-play |
| Conditional (If-Then) | 100-500ms | Market-dependent |
| Close Position | 1-5s | Exit management |
| Sequence | Sum of children | Overall workflow |
| Concurrent | Max of children | Multi-selection |

### Optimization Tips for AI Agent

1. **Pre-validate parameters** before MCP calls
2. **Cache template definitions** (change rarely)
3. **Batch multiple strategy creation** into single MCP call
4. **Validate composition** locally before sending to Bfexplorer
5. **Use concurrent execution** for independent strategies

---

## 10. Error Handling Patterns

### Common Validation Errors

```python
# Missing required parameter
"BetType is required for Place Bet template"

# Invalid parameter value
"BetType must be 'Back' or 'Lay', got 'Long'"

# Undefined strategy reference
"Strategy 'Aggressive Back Bet' not found in parameters"

# Circular dependency
"Strategy composition creates cycle: A → B → A"

# Missing data context
"Strategy requires WeightOfMoneyData but not available"

# Invalid odds range
"MaximumOdds (3.0) must be greater than MinimumOdds (3.5)"

# Impossible composition
"Cannot have Concurrent execution inside If-Then without proper state sharing"
```

---

## Summary: What's Available for Strategy Building

| Category | Count | Composable | Best For |
|----------|-------|-----------|---------|
| Betting | 2 | Yes | Basic bets |
| Position Mgmt | 2 | Yes | Exits/hedging |
| Trading | 3 | Limited | Scalping |
| Ladder | 1 | Medium | Grid trading |
| Control Flow | 9 | Yes | All compositions |
| **TOTAL** | **17** | - | - |
| Advanced templates | 96 | Varies | Specialized |

**Key Insight**: Your 9 control flow templates enable composition of 113+ strategies into near-infinite variations. This is your **differentiator** vs simpler betting tools.

---

**Document Version**: 1.0  
**Tool Inventory Date**: April 28, 2026  
**Total Templates Documented**: 17+ (core) + 96+ (advanced)
