---
title: Analysis of TTI Method for Betfair & bfexplorer
source: https://thetradinginitiative.substack.com/p/my-trading-system-17-years-and-counting?utm_source=multiple-personal-recommendations-email&utm_medium=email&triedRedirect=true
tags: [ideas, analysis, strategy]
---

# Analysis of TTI Method for Betfair & bfexplorer

## Overview
This document analyzes the "TTI Method" described by Hamilton in his article "My Trading System: 17-Years and Counting" and explores how these concepts can be integrated into our **bfexplorer** agentic platform for trading on the Betfair Sport Exchange.

## Key Concepts from the Article
The core of the TTI method is moving from a "strategy" (a single entry rule) to a "system" (a comprehensive process).

### 1. The Market Blueprint (Top-Down Analysis)
Hamilton's system filters markets through several layers:
*   **Market -> Sector -> Industry -> Stock/Event.**
*   **Relative Strength:** Identifying leaders that outperform their immediate environment.
*   **Capital Flow:** Using flow as evidence of trend maturity, but letting price be the ultimate authority.

### 2. The TTI Scanner (Filtering)
A rigorous filtering process to eliminate "shitty" trades:
*   Healthy long-term trend.
*   Relative strength across multiple timeframes.
*   Improving momentum.
*   Real participation.
*   Adequate liquidity.
*   Definable trade setup.

### 3. Entry & Execution (The BRB Setup)
*   **Breakout-Retest-Bounce:** Waiting for a level to be tested as support before entering.
*   **Risk Management:** Sizing based on the distance between entry and stop-loss. "Price will hurt you, but size will kill you."

### 4. Convexity (Options Overlay)
*   Using options to add non-linear returns once a core position is established.
*   "Selling doubles" at 100% profit to de-risk the original capital.

## Application to bfexplorer & Betfair

Based on these insights, we can enhance our platform in the following ways:

### A. Agentic "Market Blueprint" Workflow
We can implement a multi-agent workflow where different agents handle each layer of the TTI blueprint:
1.  **Macro/Trend Agent:** Analyzes broader market conditions (e.g., overall sports trends, betting volume shifts).
2.  **Relative Strength Agent:** Compares specific markets or selections against their "peers" (e.g., a horse's performance relative to the field, or a team's form relative to the league average).
3.  **Liquidity & Momentum Agent:** Analyzes order book depth and price velocity to ensure we aren't entering "thin" markets where slippage is high.

### B. Automated Filtering (The "Scanner")
We can build specialized filters within `bfexplorer` that act as the "TTI Scanner":
*   **Liquidity Filter:** Automatically flag markets with insufficient depth for our intended stake.
*   **Momentum Check:** Use technical indicators to ensure we aren't buying at the peak of a move.

### C. Execution Logic (BRB & Risk)
*   **Automated Retest Detection:** Instead of just "buying on breakout," the agent can wait for a retest of a key level before triggering an order.
*   **Dynamic Position Sizing:** Automatically calculate stake size based on the distance to the next logical "invalidating" level (the stop-loss).

### D. Convexity Module
*   Integrate logic to identify opportunities where an "options-like" approach (e.g., hedging or specific laddering) can provide convexity, though this is more applicable to financial markets, we can adapt the concept of "scaling out" at specific profit milestones to manage risk effectively on Betfair.

## Action Items
- [ ] Research how to implement a "Relative Strength" metric for sports markets (e.g., comparing a horse's odds movement against the average market move).
- [ ] Develop a "Liquidity Score" for the `bfexplorer` dashboard.
- [ ] Create a "Retest Alert" system for key price levels in high-volume markets.

---
*Original Article:* [My Trading System: 17-Years and Counting..](https://thetradinginitiative.substack.com/p/my-trading-system-17-years-and-counting?utm_source=multiple-personal-recommendations-email&utm_medium=email&triedRedirect=true)
