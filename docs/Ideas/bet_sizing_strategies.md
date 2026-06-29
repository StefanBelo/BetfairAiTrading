---
title: Bet Sizing Strategies from Andrew Courtney Article
date: 2026-06-02
source: Whirligig Bear / On Bet Sizing, part 1
tags: [betfair, bet-sizing, risk-management, kelly-criterion, ai-trading]
---

# AI Trading Strategy Ideas from Bet Sizing Principles (Andrew Courtney Article)

**Date:** June 2, 2026
**Source Research:** [On Bet Sizing, part 1 - by Andrew Courtney](https://whirligigbear.substack.com/p/on-bet-sizing-part-1?utm_source=multiple-personal-recommendations-email&utm_medium=email&triedRedirect=true)
**Target Platform:** Betfair AI Trading System

---

## 💡 Executive Summary for Strategy Development

The article emphasizes that successful trading is less about finding a single "perfect bet" and more about **rigorous risk management, defining the true edge, and understanding the limitations of one's own confidence.** Our AI platform must evolve from being purely predictive to being highly *risk-aware* and *utility-conscious*.

---

## 🎯 Core Strategy Pillars for Implementation

### 1. Utility-Driven Bankroll Management (The "True Bankroll")
**Concept:** The bankroll is not just liquid cash; it's tied to the overall utility of the user/firm, which includes job stability and retirement savings. Losing money can cause non-betting negative consequences that compound losses faster than market volatility.
**AI Actionable Item:**
*   Develop a **Risk Profile Module**. This module must allow users to define their "non-negotiable loss point" (the level at which utility sharply decreases).
*   The system should calculate an *Effective Risk Capital* based on this profile, rather than just the current cash balance.

### 2. Dynamic Kelly Criterion Sizing with Heuristics
**Concept:** The Kelly criterion maximizes growth but is too volatile for most real-world applications due to noisy inputs and uncertainty in true probability ($p$).
**AI Actionable Item:**
*   **Primary Bet Sizer:** Use the full Kelly formula as a theoretical maximum.
*   **Practical Bet Sizer (Recommended):** Implement a configurable dampening factor, defaulting to **25% - 33% of the calculated Kelly size**. This acknowledges input noise while still being mathematically grounded.

### 3. Multi-Layered Edge Validation System (The "Why")
**Concept:** Finding an edge requires two components: 1) A model fair value that differs from the market, AND 2) A concrete reason why the market is wrong (i.e., understanding the systemic inefficiency).
**AI Actionable Item:**
*   **Layer 1: Quantitative Edge:** Calculate $\text{Edge} = \text{Model Fair Value} - \text{Market Price}$.
*   **Layer 2: Qualitative Justification:** Integrate NLP/Sentiment analysis to generate a **"Mispricing Thesis."** This thesis must explain *why* the market is wrong (e.g., "The market has not yet priced in the impact of recent regulatory changes," or "Opposing bettors are over-indexing on X factor").
*   **Confidence Score:** The final confidence score for a trade should be a weighted average of the quantitative edge magnitude and the qualitative strength/novelty of the Mispricing Thesis.

### 4. Conditional Probability Stress Testing
**Concept:** A bet's perceived edge must be re-evaluated based on how large the proposed stake is relative to the bankroll, simulating real-world decision constraints.
**AI Actionable Item:**
*   Before executing a trade size calculation, run a **Stake Sensitivity Check**. This function should output: "If we increase the bet size by X%, our calculated edge confidence drops from Y% to Z%." This forces the AI to be conservative when stakes get large.

---

## 🛠️ Next Steps for Development
1.  **Refine Risk Module:** Build out the user interface and backend logic for defining the "Utility Function" constraints.
2.  **Integrate Thesis Generator:** Focus development on improving the NLP component that generates actionable, market-specific reasons for mispricing.
3.  **Testing:** Test all sizing mechanisms (Kelly $\rightarrow$ Dampened Kelly) against historical Betfair data to validate risk mitigation effectiveness.