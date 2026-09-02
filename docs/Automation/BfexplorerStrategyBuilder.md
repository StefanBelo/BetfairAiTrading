---
title: Bfexplorer Strategy Builder
date: 2026-09-02
tags: [bfexplorer, agentic-app, betting-strategy]
aliases: Bfexplorer Strategy Builder Documentation
---

# Bfexplorer Strategy Builder

The **Bfexplorer** Betfair trading application offers a wide range of pre-built strategies. These can be categorized into three main groups:

1.  **General Strategies**: Basic actions for placing bets.
2.  **Trading Strategies**: Complex maneuvers that execute both an opening bet and a closing action (for profit or loss) when specific conditions are met.
3.  **Control Flow Strategies**: Tools to manage the execution flow, such as sequential or concurrent execution of multiple strategies, scheduled executions, and other logic-based controls.

More complex strategies can be constructed by combining any of these components. The Bfexplorer application provides a dedicated dialog form to configure these combinations easily.

To assist users in building custom strategies simply by describing their goals in plain English—for example: *"I want to execute a strategy only when the selection's traded volume is >= 5.0 and, after 2 minutes, if the volume falls to <= 4.5, place a €50 back bet"*—we have developed a specialized system prompt. Since Bfexplorer is an agent-based application with integrated AI capabilities, this prompt guides the AI through a 4-step process to construct the user's desired strategy.

![BfexplorerStrategyBuilder](/docs/Automation/images/BfexplorerStrategyBuilder.png)

Below is a response from the AI agent demonstrating how this process works:

**I want to execute strategy only when selection is traded >= 5.0 and after 2 minutes if the selection is traded <= 4.5, I want to place 50 Euro back bet.**

I'll help you create this trading strategy. Let me start by discovering the available strategy templates that can handle these conditions.

## STATE 1: Discovery & Initial Filtering

Now let me examine the relevant control flow strategies that can handle these conditions. I'll look at the "If Then Else", "Scheduled Execution", and "Place Bet" templates.

Let me also check the "Sequence Execution" template to see how we can combine these strategies:

Now I have all the information I need. Let me analyze and create the optimal solution.

## STATE 2: Validation & Simplification

Based on the templates analyzed, here's the strategy structure:

**Logic Flow:**
1. **Entry Condition**: Only start when `LastPriceTraded >= 5.0`
2. **Time Delay**: Wait 2 minutes
3. **Second Condition**: Check if `LastPriceTraded <= 4.5`
4. **Action**: Place 50 EUR Back bet

**Required Templates:**
| Template | Purpose |
|----------|---------|
| `If Then Else` | Main control flow - checks initial condition (>= 5.0) |
| `Scheduled Execution` | Provides 2-minute delay after initial trigger |
| `If Then Else` (nested) | Checks second condition (<= 4.5) |
| `Place Bet` | Places the 50 EUR back bet |

**Simplified Parameters:**

1. **Main Strategy - "If Then Else":**
   - `EntryCriteria`: `LastPriceTraded >= 5.0`
   - `EvaluateEntryCriteriaOnlyOnce`: `True`
   - `IfThenStrategyName`: The scheduled execution strategy name

2. **Scheduled Execution Strategy:**
   - `TimeSpanType`: `FromStrategyStart`
   - `StartTimeSpan`: `0:02:00` (2 minutes)
   - `StrategyName`: The nested If Then Else strategy name

3. **Nested If Then Else Strategy:**
   - `IfThenCriteria`: `LastPriceTraded <= 4.5`
   - `IfThenStrategyName`: "Place Bet" (the actual bet placement)

4. **Place Bet Strategy:**
   - `BetType`: `Back`
   - `Stake`: `50.0`

---

## STATE 3: Recommendation & Consent

| Strategy Component | Template | Key Parameters |
|--------------------|----------|----------------|
| **Main Strategy** | If Then Else | EntryCriteria: `LastPriceTraded >= 5.0`<br>EvaluateEntryCriteriaOnlyOnce: `True` |
| **Delay Strategy** | Scheduled Execution | TimeSpanType: `FromStrategyStart`<br>StartTimeSpan: `0:02:00` |
| **Condition Check** | If Then Else | IfThenCriteria: `LastPriceTraded <= 4.5` |
| **Bet Placement** | Place Bet | BetType: `Back`<br>Stake: `50.0` |

**Strategy Names (suggested):**
- Main: "Price Trigger Back Bet"
- Scheduled: "Wait 2 Minutes"
- Nested: "Check Price And Bet"
- Bet: "Place 50 Back"

**Does this configuration meet your requirements?** Please confirm to proceed with creating the strategy settings.

**Use Scheduled Execution and Place Bet, and create strategy settings for me.**

I'll create the strategy settings using only Scheduled Execution and Place Bet as requested.

## STATE 4: Execution

The strategy settings have been successfully created.

## Summary

| Strategy Name | Template | Parameters |
|---------------|----------|------------|
| **Wait And Place Back Bet** | Scheduled Execution | EntryCriteria: `LastPriceTraded >= 5.0`<br>EvaluateEntryCriteriaOnlyOnce: `True`<br>StartTimeSpan: `0:02:00` (2 minutes)<br>StrategyName: `Place 50 Back` |
| **Place 50 Back** | Place Bet | BetType: `Back`<br>EntryCriteria: `LastPriceTraded <= 4.5`<br>Stake: `50.0` |

**How it works:**
1. **Scheduled Execution** (`Wait And Place Back Bet`) monitors the selection and only starts the 2-minute timer when the last traded price is **>= 5.0**
2. After **2 minutes** elapse, it triggers the `Place 50 Back` strategy
3. **Place Bet** (`Place 50 Back`) checks if the price is **<= 4.5** and places a **50 EUR Back bet** if the condition is met

This configuration uses the simplest possible setup with just 2 templates as requested, minimizing dependencies while satisfying your exact requirements.

![WaitAndPlaceBackBet](/docs/Automation/images/WaitAndPlaceBackBet.png)