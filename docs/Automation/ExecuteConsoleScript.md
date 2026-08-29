---
title: Execute Console Script Usage Example
date: 2026-08-29
tags: [bfexplorer, agentic-app, data-analysis, betting-strategy]
aliases: Execute Console Script Documentation
---

# Executing Console Scripts in Bfexplorer Agentic App

I have integrated a new tool into the bfexplorer agentic application: **"Execute Console Script."** This grants AI agents full autonomy to execute data analysis scripts directly on live data from within the bfexplorer application. Here is documentation detailing its first usage scenario.

## Scenario Overview

**My Prompt:**
> "The market ID is 1.261618942, which is the specific race we are having problems with. Some scripts can run on all closed markets, while others are designed to run only on active markets. In the latest version of the bfexplorer mcp server, I added an option to execute a script directly in the bfexplorer console tool. What does this mean for you? You can perform these steps yourself; previously, I had to do them manually for you."

**AI Agent Response Summary:**
This capability is now fully self-service. The agent successfully activated the market and selection, executed both required scripts via `execute_console_script`, and pulled the stored contexts—including my newly captured data points: **`PriceScoreBetTrigger[2433]` + `BetTriggerEvents[8]`**. This confirms that the record-array serialization for `SetData` is working as expected.

The combined data reveals critical insights into the problematic race:

## Analysis: Why the Engine Triggered 8 Lays During a Price Collapse (`FineTuningResult` - Lethal Nymph)

### 1. Signal Predictive Power
Every signal used by the score demonstrated minimal predictive power for this specific runner:

| Signal | Correlation with Future Price (r) |
| :--- | :--- |
| `tickMom4kCorr` | −0.079 |
| `tickMom8kCorr` | **−0.010** |
| `epCorr` | +0.104 *(Strongest, but still weak)* |
| `womCorr` | −0.048 |
| `microDevCorr` | +0.009 |
| `probTrendCorr` / `probTrendVelCorr` | +0.045 / +0.055 |

### 2. Directional Inversion During Collapse
The score was directionally inverted throughout the entire collapse period. From `PriceScoreBetTrigger`: the score registered **−0.33 @ 7.2** (14:43:51), **−0.35 @ 6.4**, **−0.49/−0.50 @ 6.6** (14:44:59), and **−0.53 @ 4.8** (14:51:53). These readings were deeply negative (indicating a Strong Lay signal) while the price was falling from 7.2 to 4.6. The engine consistently signaled "Lay" exactly when the horse was being backed.

### 3. Over-Persistence is the Core Issue
The problem wasn't a lack of flipping signals (`pctShortFlips: 0`, `triggersAfterShortFlip: 0`), but rather **over-persistence**. The average band duration (`avgBandDurationTicks: 1216.5`) was excessive, allowing the Strong Lay signal to persist for approximately 5 minutes and trigger **8 bets** into a losing position.

### 4. Failure to Identify Winning Opportunities
The engine completely missed the positive trading opportunities. At 14:39:01–05, the score spiked to **+0.47 (≈ P97)** while the price was at 8.0—a textbook Strong Back moment (the price subsequently fell to 7.2). However, **zero Back triggers** were fired. The system ignored the winning direction and focused solely on the losing one.

## Conclusion (Evidence-Ranked)
The score for this runner was built from signals with a correlation coefficient $|r| \le 0.10$, rendering the Lay calls essentially coin flips. Furthermore, the persistence rules allowed a single poor call to repeat eight times. The primary suspects are:
1.  **Score Weighting / Signal Selection:** (e.g., tick momentum was useless here; EP remains the only mildly informative input).
2.  **Lack of Re-trigger Prevention:** There is no mechanism to stop re-triggering into a losing position once adverse movement begins (the band survived the entire 2-point adverse run).

**Suggested Next Step (Data-Only):** I recommend pulling the `FineTuningResult` for the 7 **winning** Back-scalp markets from today and comparing their signal correlations. If those signals show a meaningfully higher correlation than Lethal Nymph's, this provides clean, data-backed evidence to determine which signals deserve more weight in the model. Shall I run that comparison?

![ExecuteConsoleScript](/docs/Automation/images/ExecuteConsoleScript.png)