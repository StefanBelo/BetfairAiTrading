---
title: "Strategy Builder Approaches: Research & Analysis"
description: "Comprehensive research comparing strategy builder paradigms in betting and trading industries"
date: 2026-04-28
tags: [research, strategy-builder, UI-design, architecture, bfexplorer]
---

# Strategy Builder Approaches: Research & Analysis

## Executive Summary

This research examines **three primary paradigms** for enabling users to build trading strategies:

1. **Natural Language + AI (Current Bfexplorer Approach)**
   - User describes strategy in plain English
   - AI agent judges feasibility using available templates/blocks
   - Pros: Intuitive, no learning curve, powerful composition
   - Cons: Requires AI LLM, slower execution, black-box decisions

2. **Visual/Graphical Strategy Builders**
   - Drag-and-drop UI components, nodes, or blocks
   - Pros: Clear visual feedback, lower complexity ceiling, faster execution
   - Cons: Steeper learning curve, limited expressiveness

3. **Hybrid Approach (Recommended)**
   - Combine plain text/English with visual validation and parameter tweaking
   - Pros: Best of both worlds, accessible for different user types

---

## Part 1: MCP Tools Available in Bfexplorer

### Strategy Building Capabilities

Bfexplorer exposes **113 strategy templates** through MCP tools with the following key categories:

#### Core Betting Strategies (14 templates)
- **Place Bet** - Basic back/lay betting (21 parameters)
- **Place Bet - Fill or Kill** - Time-limited orders
- **Close Selection/Market Bet Position** - Position management
- **Tick Offset** - Micro-scalping strategies
- **Scratch Trading** - Zero-profit closing
- **Trailing Stop Loss** - Dynamic loss management

#### Control Flow Strategies (9 templates) ⭐ KEY FOR COMPOSITION
These enable strategy composition and are critical for AI-driven building:

1. **Execute Strategies** (ID: 11) - Sequential execution of multiple strategies
   - Parameters: StrategyNames (semicolon-separated), UseLadderParameters, ExecuteOnSelection
   - Example: `"Back Bet;Trailing Stop Loss;Close Position"`

2. **Sequence Execution** (ID: 15) - Ordered chain with data sharing
   - Parameters: StrategyNames, ShareBetPosition, ExecuteOnSelection
   - Enables state sharing between strategies

3. **Concurrent Execution** (ID: 16) - Parallel strategy execution
   - Parameters: StrategyNames, EndExecutionIfAnyBotEnds
   - Example: Run multiple ladders simultaneously

4. **If Then Else** (ID: 14) - Conditional branching
   - Parameters: IfThenCriteria, IfThenStrategyName, IfElseCriteria, IfElseStrategyName
   - Example: `"LastPriceTraded > 3.0" -> Execute "Aggressive Back Bet"`

5. **Execute Till Target Profit** (ID: 13) - Loop until condition
   - Parameters: StrategyName, TargetProfit, TargetLoss, MartingaleStakeFactor
   - Enables staking plan strategies

6. **Repeat Until** (ID: 17) - General loop control
   - Parameters: StrategyName, RepeatUntilParameter (Iterations/TotalStake/Profit/Loss), TargetValue

7. **Execute on Selections** (ID: 10) - Multi-selection execution
   - Applies strategy to specific selections (e.g., "1,2,4")

8. **Execute on Associated Market** (ID: 12) - Cross-market coordination
   - Example: "Execute Close Position strategy on OVER_UNDER_2_5 when Back Bet matches"

9. **Execute Trigger Strategy** (ID: 18) - DLL-based custom triggers
   - For advanced custom logic

#### Data Context Providers (29 sources)
Available data for decision-making:
- **Horse Racing**: AtTheRacesBookmakersOdds, BetfairSpData, RacingTvData, TimeformData, PedigreeData
- **Football**: FootballMatchScoreData, PlayingFootballMatchScoresData
- **Market**: CandleStickData, PriceHistoryData, TradedPricesData, WeightOfMoneyData

### MCP Tools for AI Integration

**Key tools for strategy building:**

```
mcp_bfexplorerapp_get_all_strategy_templates()
  → Returns all 113 templates with parameter definitions

mcp_bfexplorerapp_create_strategy_settings(strategyTemplateParameters: JSON)
  → Create new strategy settings from templates
  → Format: [{"name":"Back Bet 10 EUR","template":"Place Bet","parameters":{...}}]

mcp_bfexplorerapp_execute_strategy_settings(strategyName, marketId, selectionId)
  → Execute a pre-configured strategy

mcp_bfexplorerapp_get_all_data_context_for_market(dataContextNames, marketId)
  → Get market data for decision-making

mcp_bfexplorerapp_get_strategy_template(botName)
  → Inspect specific template structure
```

**Current AI Agent Capabilities:**
- ✅ List all available templates
- ✅ Understand parameter requirements
- ✅ Validate strategy composition feasibility
- ✅ Create strategy settings as JSON
- ✅ Execute strategies
- ✅ (C# agent) Execute tool calls through AI models
- ✅ (Python FastAgent) Full MCP integration working

---

## Part 2: Industry Analysis - Strategy Builders

### 2.1 Betting Industry

#### TradingView Strategy Builder
**Type**: Visual + Code Hybrid

```
Architecture:
├─ Visual Pine Script Editor (Graphical)
├─ Pine Script Code Editor (Text-based)
└─ Parameter Panel (Auto-generated UI)

Strengths:
✓ Massive library of ready-made strategies
✓ Community-driven templates
✓ Backtesting integration
✓ Real-time chart visualization
✓ Both visual and code modes work together

Weaknesses:
✗ Steep learning curve for Pine Script
✗ Limited to TradingView ecosystem
✗ Code-first design (UI is secondary)
```

#### Betfair Exchange Strategies (Betting Industry)
**Current State**: Mostly manual rule-based systems

```
User Flow:
Plain Text Rules → Manual JSON/Config → API Execution

Limitations:
- No strategy builder UI
- Requires technical knowledge
- Manual validation of strategy feasibility
```

#### Tradestation / ESignal
**Type**: Chart-based with EasyLanguage

```
Features:
- Drag-drop indicators on charts
- Code generation from visual building
- Limited to pre-defined blocks
- Strong backtesting

Typical User Journey:
Chart Analysis → Visual Indicators → Auto-generated Code → Backtest → Deploy
```

#### Cryptohopper (Crypto Trading)
**Type**: Low-code Node-based Builder

```
UI Design:
┌────────────────────────────────────────────┐
│  Strategy Builder Canvas (Node-based)      │
├────────────────────────────────────────────┤
│ [BUY Signal] ──→ [ADD TO POSITION] ──→ [SELL]
│    (from                 (with             (at
│     RSI+                 DCA)              target)
│     MACD)                                  │
└────────────────────────────────────────────┘

Key Feature: Template-driven nodes with parameter forms
Typical Use: Crypto bots running continuously
```

#### Coinrule (Crypto Automation)
**Type**: Template + Conditional Logic

```
UI Pattern:
IF [Trigger] THEN [Action] ON [Exchange]
┌─────────┐  ┌────────┐  ┌──────────┐
│ RSI < 30│ → │Buy XYZ │ → │ On Binance│
└─────────┘  └────────┘  └──────────┘

Strengths:
✓ Simple if-then language
✓ Clear parameter UI
✓ Pre-built conditions library
✓ Non-technical users can use it
```

---

### 2.2 Traditional Trading Industry

#### TradeStation Strategy Wizard
**Type**: Multi-step wizard with code generation

```
Workflow:
Step 1: Define Entry Conditions (visual)
Step 2: Define Exit Conditions (visual)
Step 3: Set Risk Management (form)
Step 4: Review Generated Code
Step 5: Backtest
Step 6: Deploy

Result: Auto-generated EasyLanguage code
Advantage: Generates readable, editable code
```

#### MetaTrader 5 (MT5) - MQL Strategy Builder
**Type**: Code-based with IDE support

```
User must write MQL5 code, but:
- Integrated IDE
- Code wizard for common patterns
- Built-in template library
- Backtesting framework

Target: Professional traders who code
```

#### IG Markets Lightbulb (CFD Trading)
**Type**: Drag-drop strategy builder

```
Features:
┌─────────────────────────────────┐
│ Visual Strategy Builder          │
├─────────────────────────────────┤
│ [Condition]  [Logic]  [Action]  │
│   ↓            ↓         ↓       │
│ Price > 100   AND    Buy 10 Lots│
│ AND                             │
│ RSI < 30                        │
└─────────────────────────────────┘

Simplicity: Trading rules without code
Limitation: Limited to platform features
```

#### Interactive Brokers (TWS Algos)
**Type**: Behavioral + Rule-based

```
Approach:
- Pre-built algorithmic execution templates
- Parameter tuning (NOT strategy building)
- Risk management rules
- No true strategy builder (execution only)
```

#### Keltner (Quantitative Strategy Platform)
**Type**: Python-first with UI Parameter Tuning

```
Paradigm:
Users write Python strategies (full control)
→ Platform auto-generates parameter UI
→ Backtesting & optimization
→ Live trading

Philosophy: "Python for power, UI for convenience"
```

---

### 2.3 Emerging Platforms - AI-Driven Approaches

#### Catalyst by Sapient AI
**Type**: AI-driven English language strategy building

```
User Input: "Buy when RSI dips below 30 and volume increases"
          ↓
AI Analysis: Understands market concepts, parameters, validity
          ↓
Code Generation: Python or Solidity code automatically generated
          ↓
Validation: Check against available indicators/data
          ↓
Execution: Deploy on blockchain networks

Key Insight: Natural language understanding is POWERFUL
but requires:
- Strong NLP/LLM
- Domain-specific training
- Clear feedback loops
```

#### Superalgos (Community-driven)
**Type**: Multi-paradigm (Visual + Code + Community)

```
Design Philosophy: "Multiple entry points"
├─ Visual designer (drag-drop nodes)
├─ JavaScript Code
├─ Backtesting engine
└─ Community strategy library

Allows users to progress:
Beginner → Visual blocks
Intermediate → Code editing
Advanced → Full customization
```

#### Koinly (No-code Automation)
**Type**: Simple conditional rules engine

```
Rule Creation:
┌──────────────────────────────────┐
│ When [Signal] [Comparison] [Value]│
│ Then [Execute Action]             │
└──────────────────────────────────┘

Examples:
"When Price > $50,000 Then Sell All BTC"
"When RSI > 70 Then Take Profit"

Simplicity ✓✓✓
Expressiveness ✗ (limited to predefined conditions)
```

---

## Part 3: Comparative Analysis

### Paradigm Comparison Matrix

| Paradigm | Learning Curve | Power | Speed | Scalability | Community | Best For |
|----------|---|---|---|---|---|---|
| **Pure Natural Language (AI)** | Minimal | Very High | Slow (LLM calls) | High (LLMs evolve) | Growing | Non-technical, complex logic |
| **Visual/Node-based** | Low-Medium | Medium-High | Fast | Medium | Strong (TradingView) | Traders, visual thinkers |
| **Code-based (Python/C#)** | High | Very High | Fast | Very High | Very Large | Developers, quants |
| **Conditional Rules (If-Then)** | Very Low | Low-Medium | Instant | Low | Medium | Simplicity seekers |
| **Hybrid (Code + UI)** | Medium | High | Fast | High | Strong | Professional users |

### Current Bfexplorer Approach Evaluation

**Plain English + AI Agent Method:**

```
Strengths:
✓ Zero learning curve - describe in English
✓ Very high composability - can combine any strategies
✓ Natural for domain experts (traders, analysts)
✓ Handles complex logic without UI overhead
✓ Can explain reasoning to users

Weaknesses:
✗ Dependency on LLM availability & cost
✗ Slower execution (AI inference time)
✗ Black-box decisions (why was strategy deemed infeasible?)
✗ Hallucination risks (AI might suggest impossible combinations)
✗ Not suitable for time-critical trading (latency)
✗ Requires API keys management

Current Implementation Status:
Python FastAgent: ✅ FULLY WORKING
C# Agent: ✅ FULLY WORKING (see Post_AiAgentCSharpIntegration.md for details)
```

---

## Part 4: Hybrid Recommendation for Bfexplorer

### Proposed Hybrid Architecture

```
┌─────────────────────────────────────────────────────────────┐
│                  STRATEGY BUILDER INTERFACE                 │
├─────────────────────────────────────────────────────────────┤
│                                                              │
│  Input Layer (3 options for users):                         │
│  ├─ Natural Language Prompt Entry                           │
│  │  "Back at 1.5 odds with stop loss"                      │
│  │  → [Suggest Strategy] button → AI parses                │
│  │                                                          │
│  ├─ Visual Node Builder                                     │
│  │  [START] → [Entry Condition] → [Sizing] → [Exit]       │
│  │            ↓                                             │
│  │         [Risk Controls]                                  │
│  │                                                          │
│  └─ Template Library with Form UI                          │
│     Select: "Betting Ladder"                               │
│     Fill Form: [Entry Odds] [Stake] [Levels] [Exit Rule]   │
│                                                              │
├─────────────────────────────────────────────────────────────┤
│                  VALIDATION & PREVIEW LAYER                 │
├─────────────────────────────────────────────────────────────┤
│                                                              │
│  JSON Configuration:                                        │
│  {                                                          │
│    "strategies": [                                          │
│      {"name": "Back Bet 1.5", "parameters": {...}},       │
│      {"name": "Trailing Stop Loss", "parameters": {...}}   │
│    ],                                                       │
│    "controlFlow": "Sequence Execution"                      │
│  }                                                          │
│                                                              │
│  Preview: ASCII/Text representation of strategy flow       │
│  Validation: Check all parameters, data availability       │
│  Feasibility: AI judges if combination is valid            │
│                                                              │
├─────────────────────────────────────────────────────────────┤
│                    EXECUTION & MONITORING                    │
├─────────────────────────────────────────────────────────────┤
│  Save → Deploy → Monitor → Adjust                          │
└─────────────────────────────────────────────────────────────┘
```

### Detailed Recommendations

#### 1. **For Non-Technical Users** (Traders)
- **Primary UI**: Template Library + Parameter Forms
- **Secondary**: Visual Node Builder (drag-drop)
- **Tertiary**: Natural Language (optional, for exploration)

**Example Flow:**
```
1. Browse "Horse Racing Strategies" template category
2. Select "EV-based Back Betting"
3. Fill Form:
   - Entry Filter: "EV > 10%"
   - Odds Range: "1.5 - 3.0"
   - Stake: "10 EUR"
   - Exit on: "Profit target or loss limit"
4. Preview strategy composition
5. Deploy
```

#### 2. **For Intermediate Users** (Traders with coding basics)
- **Primary UI**: Visual Node Builder
- **Secondary**: Template Library
- **Tertiary**: JSON/Code editor

**Example Flow:**
```
Drag nodes:
[Market Filter] → [Entry Signal] → [Position Sizing] 
               ↓
            [Risk Stop]
               ↓
            [Profit Target]
               ↓
            [Close Position]
```

#### 3. **For Advanced Users** (Developers, Quants)
- **Primary**: JSON Configuration + Code Editor
- **Secondary**: Visual validation of compiled strategy
- **Support**: Python/F# direct API access to Bfexplorer

**Example Flow:**
```python
# Direct MCP access
strategy = SequenceExecution(
    strategies=[
        PlaceBet(bet_type="Back", odds_range=(1.5, 3.0), stake=10),
        TrailingStopLoss(loss_ticks=3, hedge=True)
    ]
)
agent.execute(strategy, market_id, selection_id)
```

---

## Part 5: Technology Stack Recommendations

### Frontend (UI Layer)

**Option A: Web-based (Recommended for accessibility)**
```
Framework: React + TypeScript
Visualization: React Flow (node-based builder)
Form Building: React Hook Form + Zod validation
Charts: Recharts for strategy visualization
State: TanStack Query + Zustand

Advantages:
- Browser-based (no installation)
- Works on Windows/Mac/Linux
- Easy to deploy
- Responsive design
```

**Option B: Desktop (Bfexplorer Integration)**
```
Framework: WPF (C#) or Avalonia (cross-platform)
Advantage: Native integration with Bfexplorer
Disadvantage: More development work
```

### Backend (Strategy Validation)

**Option A: FastAgent (Python) - Recommended**
```
Already working in your setup
- Python FastAgent framework
- MCP client for Bfexplorer integration
- LLM integration (DeepSeek, OpenAI)
- JSON schema validation
- Async execution

Benefits: Familiar, proven, minimal changes
```

**Option B: Hybrid (Python + C#)**
```
Python: AI reasoning, validation
C#: Direct Bfexplorer integration via MCP
Communication: gRPC or REST API

Benefits: Leverage both ecosystems
```

### Data Model (JSON Schema)

```json
{
  "$schema": "http://json-schema.org/draft-07/schema#",
  "title": "BfexplorerStrategy",
  "type": "object",
  "properties": {
    "id": { "type": "string", "format": "uuid" },
    "name": { "type": "string" },
    "category": { "enum": ["Betting", "Trading", "HorseRacing", "Football", "Tennis"] },
    "description": { "type": "string" },
    "controlFlow": {
      "type": "string",
      "enum": ["Sequence Execution", "Concurrent Execution", "If Then Else", "Repeat Until"]
    },
    "strategies": {
      "type": "array",
      "items": {
        "type": "object",
        "properties": {
          "template": { "type": "string", "description": "Strategy template name" },
          "name": { "type": "string", "description": "Instance name" },
          "parameters": { "type": "object" },
          "conditions": { "type": "string", "description": "Evaluation criteria if conditional" }
        }
      }
    },
    "dataContext": {
      "type": "array",
      "items": { "type": "string" },
      "description": "Required data providers"
    },
    "riskManagement": {
      "type": "object",
      "properties": {
        "dailyLossLimit": { "type": "number" },
        "positionSize": { "type": "number" },
        "maxConcurrentBets": { "type": "integer" }
      }
    },
    "backtestResults": {
      "type": "object",
      "properties": {
        "winRate": { "type": "number" },
        "profitFactor": { "type": "number" },
        "maxDrawdown": { "type": "number" }
      }
    }
  }
}
```

---

## Part 6: Implementation Roadmap

### Phase 1: Foundation (Weeks 1-2)
- [ ] Backend API for strategy validation (Python FastAgent)
- [ ] JSON schema definition and validation
- [ ] Template introspection and documentation
- [ ] Parameter validation rules

### Phase 2: Core UI (Weeks 3-4)
- [ ] Template library browser
- [ ] Basic parameter form generation
- [ ] Strategy preview (JSON view)
- [ ] Save/Load functionality

### Phase 3: Natural Language Layer (Weeks 5-6)
- [ ] Integrate existing AI agent
- [ ] Strategy parsing from English
- [ ] Feasibility validation
- [ ] Suggestion engine (recommend templates)

### Phase 4: Visual Builder (Weeks 7-8)
- [ ] React Flow integration
- [ ] Node components for templates
- [ ] Connection validation
- [ ] Preview/generation

### Phase 5: Testing & Deployment (Weeks 9-10)
- [ ] End-to-end testing
- [ ] Backtesting integration
- [ ] Demo strategies
- [ ] Deployment

---

## Part 7: Competitor Benchmarking

### Direct Competitors Analysis

#### TradingView (Leader)
**Strengths Bfexplorer should match:**
1. Huge community library (1000s of strategies)
2. Backtesting with real market data
3. Paper trading before live
4. Pine Script IDE with syntax highlighting
5. Visual indicators on charts
6. Parameter optimization
7. Permission-based strategy sharing
8. Strategy performance rankings

**Differentiators for Bfexplorer:**
- Betting-specific (not stock-focused)
- AI-driven English language
- Real Betfair market integration
- Horse racing focus
- Control flow strategies

#### Superalgos
**Strengths to match:**
1. Multiple UI layers for different skills
2. Community strategy exchange
3. Open-source foundation
4. Comprehensive documentation
5. Multi-exchange support (Bfexplorer = Betfair)

**Bfexplorer advantage:**
- Specialized for betting
- Simpler than Superalgos
- Native MCP integration

---

## Conclusion

### Recommended Direction for Bfexplorer

**Strategy: Hybrid Approach with Progressive Disclosure**

```
┌──────────────────────────────────────────────────┐
│  START WITH:                                     │
│  ✓ Template Library + Form UI (Easiest)          │
│  ✓ Natural Language Chatbot (Differentiation)    │
│  ✓ JSON Editor (Power users)                     │
├──────────────────────────────────────────────────┤
│  SECOND PHASE:                                   │
│  ✓ Visual Node Builder (if demand exists)        │
│  ✓ Backtesting engine                            │
│  ✓ Community strategy sharing                    │
├──────────────────────────────────────────────────┤
│  INFRASTRUCTURE REQUIRED:                        │
│  ✓ Web UI (React + TypeScript)                   │
│  ✓ Validation API (Python FastAgent)             │
│  ✓ Strategy registry database                    │
│  ✓ Backtesting engine                            │
└──────────────────────────────────────────────────┘
```

### Why This Works for Bfexplorer

1. **Simpler than competitors**: Focus on betting, not all trading
2. **Leverages existing assets**: AI agents already working, 113 templates ready
3. **Multiple entry points**: Accommodates trader skillsets
4. **Differentiation**: AI-driven English language is unique in betting
5. **Lower barrier to entry**: Template library gets non-technical users started
6. **Scalable**: Foundation for community strategies, marketplace

### Critical Success Factors

1. **Clear strategy composition rules** - Document which templates can be chained
2. **Robust parameter validation** - Prevent invalid combinations
3. **Excellent error messages** - Help users understand why strategy can't be built
4. **Performance** - Strategy building should be snappy (< 2 sec)
5. **Community** - Share strategies, learn from others, build marketplace
6. **Documentation** - Each template needs clear examples
7. **Backtesting** - Validate before deploying real money

---

## References & Further Research

### Industry Resources
- TradingView Pine Script Documentation: https://www.tradingview.com/pine-script-docs/
- Superalgos Open-Source Project: https://github.com/Superalgos/Superalgos
- Betfair API Documentation: https://docs.developer.betfair.com/

### Academic Research
- "Domain-Specific Languages for Financial Computing" - ACM COMPUTING SURVEYS
- "No-Code/Low-Code Software Development" - IEEE Standards

### Your Internal Resources
- `/docs/Automation/AIAgentIntegrationToBfexplorerApp.md` - Current AI integration
- `/docs/Automation/BfexplorerDataProviders.md` - Available data sources
- Strategy templates: 113 available templates with 20+ parameters each

---

**Document Version**: 1.0  
**Last Updated**: April 28, 2026  
**Research Scope**: Betting industry, trading platforms, emerging AI-driven approaches
