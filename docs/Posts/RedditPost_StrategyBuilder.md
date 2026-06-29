---
title: "Redditpost Strategybuilder"
aliases: ["Redditpost Strategybuilder"]
type: strategy
tags: [automation, bfexplorer, python, reddit, staking, strategy, trading]
---

## My Experience with the Bfexplorer Strategy Builder (vs. Other Approaches)

I wanted to share my recent experience using the Bfexplorer strategy builder, discuss how it works, and compare our approach to what I often see from others in the Betfair / trading community.

### 🎯 What is Bfexplorer’s Strategy Builder?

Bfexplorer offers a powerful strategy builder where you create, combine, and manage automated betting and trading strategies for Betfair markets. Think of it like “bot blocks” – you assemble building blocks that define each step (e.g. place bet, set stop loss, sequence orders, run data analysis) and then run them live or in simulation.

You don’t need to code, but it’s more structured and modular than just fiddling with staking plans or copying tips.

---

### 🛠️ How We Built a Strategy (Example)

The following strategy was created directly from this prompt:

> Create strategy: Place back bet 10 EUR and then trailing stop loss 2 ticks


During our session, we worked through strategy creation using the following format:

```json
[
  {
    "name": "Back Bet 10 EUR",
    "template": "Place Bet",
    "parameters": { "BetType": "Back", "Stake": 10.0 }
  },
  {
    "name": "Trailing Stop Loss 2 Ticks",
    "template": "Trailing Stop Loss",
    "parameters": { "Loss": 2, "HedgingEnabled": true }
  },
  {
    "name": "Back and Trail",
    "template": "Sequence Execution",
    "parameters": { "StrategyNames": "Back Bet 10 EUR;Trailing Stop Loss 2 Ticks" }
  }
]
```
You basically describe each module (name/template/parameters), and combine them. In this example (matching the prompt above):
- Place a back bet for 10 EUR
- Activate a 2-tick trailing stop loss with hedging
- Chain them together for sequential execution

Bfexplorer interprets this as a step-by-step, reusable bot.

---

### 🔄 Our Approach vs. What Others Use

#### Our Method:
- Structured, modular, reusable
- No direct scripting required (parameters + templates only)
- Easy to chain logic & integrate ML/AI components
- Transparent and easy to document/share (see above text format)

#### Community/Reddit Trends:
- Lots of spreadsheet-based betting (not automated)
- Copying staking plans or “tipster” bets
- Some Python/bot users, but code is often closed-source or hard to share
- Tendency to focus on “systems” (e.g. lay the draw, DOB, etc.)
- Little attention to trailing stops or order sequencing

---

### 🤔 Key Takeaways

- The Bfexplorer strategy builder makes it easy to rapidly prototype, tweak, and automate _multi-step_ strategies.
- Key advantage: You don’t need to write code, but you get power far beyond “just a bot” or spreadsheet trigger.
- Great for tinkerers, those testing multiple logged strategies, or anyone wanting to blend ML models, risk management, and conditional logic—all in a readable format.

---

**Curious to hear:**
- Has anyone else built modular strategies like this?
- What’s your preferred way to test or evolve Betfair automation setups?

---