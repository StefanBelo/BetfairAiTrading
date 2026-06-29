---
title: "Syndicate Momentum Strategy: Theoretical Principles"
type: research
status: active
tags: [trading-theory, market-microstructure, institutional-bias, risk-management]
---

# Syndicate Momentum Strategy: Theoretical Principles

This document summarizes the core behavioral and mathematical principles developed for the Syndicate Momentum Engine. It focuses on distinguishing institutional "Smart Money" signatures from retail noise and high-frequency volatility.

---

## 1. Signal Taxonomy: The Decision Architecture
The engine categorizes market states into discrete signals. Each signal defines the **Intent** (Back/Lay) and the **Mechanism** (Breakout/Reversion).

### Core Signal Types (`SignalType`)
| Signal | Use Case | Execution Logic |
| :--- | :--- | :--- |
| **MomentumBreakout** | **Backing / Laying** | Follow aggressive "Smart Money" Sweeps. Use for Steams (Back) and Drifts (Lay). |
| **MeanReversion** | **Trading / Scalping** | Trade against retail noise toward the **VWAP** (Anchor). High-frequency exit at the mean. |
| **NodeRejection** | **Support & Resistance** | Trade the "Bounce" at high-volume price nodes. Institutional orders "absorbing" pressure. |
| **Neutral** | **Observational** | No clear institutional signature. Do not engage. |

### Signal Metadata (`TradingSignal`)
Each signal carries a payload of validation metrics:
*   **Impact Density (MID):** Quantifies the capital required to move the price.
*   **Sentiment Ratio:** Long-term (1-hour) institutional bias vs. short-term timing.
*   **Signal Power:** The ultimate ranking factor combining Density, Dominance, and Odds.
*   **Skeptic Filter:** A boolean gate validating volume velocity to prevent "Fake-Outs."

---

## 2. Displacement-Based Initiator Identification
We use **Extreme Price Displacement** within a 1-second window to identify the aggressor (The Initiator).
*   **Aggressive Backer (Steam):** Price hits a **Min** displacement further from the start than the Max. Signal to **BACK**.
*   **Aggressive Layer (Drift):** Price hits a **Max** displacement further from the start than the Min. Signal to **LAY**.
*   **Significance:** Prevents "V-shape" noise from being misidentified as counter-momentum.

---

## 3. Market Impact Density (MID)
The "Friction" of the market is as important as the direction. We measure how much volume is required to move the win probability by 1%.
*   **Formula:** Total Volume / Total Path-Integrated Probability Shift.
*   **Institutional Benchmarks:**
    *   **> 1000 (Extreme):** "Brick Wall" moves. Extreme institutional commitment. **Market Orders.**
    *   **500 - 1000:** Heavy Syndicate signatures.
    *   **100 - 500 (Solid):** Professional market activity.
    *   **< 100 (Thin):** "Thin Air" moves. Speculative or retail noise. **Discard these.**

---

## 4. Pattern-Based Sequence Analysis (The PatternEngine)
Single-event sweeps are often traps. We prioritize sequences of events over the last 60 seconds.
*   **Dominance Ratio:** The ratio of Backers vs. Layers in a sequence. We require > 0.5 (clear majority) for a trade.
*   **Volume Escalation:** If the final trade in a sequence has higher volume than the average of the previous trades, the signal conviction is boosted.

---

## 5. Sentiment Memory
To prevent being "faked out" by late-market volatility (e.g., a horse drifting in the final 10 seconds), we maintain a **1-Hour Sentiment Ratio**.
*   **Logic:** If a horse has been backed heavily for 60 minutes, a sudden drift in the final 10 seconds is likely a "False Drift."
*   **Decision:** We block "Contrarian" trades that fight the long-term institutional bias.

---

## 6. Signal Power Ranking (The Moon Chime Case)
To surface high-value longshots and institutional Lays, we use a logarithmic **Odds Multiplier** that accounts for **Liability Scaling**.
*   **The Problem:** Favorites move price naturally due to high liquidity. Outsiders (50/1+) require massive risk (Liability) to move.
*   **The Refined Formula:** 
    `SignalPower = ((Volume * (Price - 1)) / 1000) * Log10(Price) * Dominance`
*   **The Logic:** A 20€ lay at 50.0 (980€ risk) is a stronger "Signature" than a 500€ back at 2.0. The logarithmic factor dampens the effect at extreme odds (100+) to prevent noise-triggering.

---

## 7. The Triple-Gate Execution Filter
Before a trade is mapped to an action, it must pass three distinct gates:
1.  **Skeptic Gate:** Is Volume Velocity > 300% of the 5-minute rolling average?
2.  **Conviction Gate:** Is the Dominance Ratio > 0.5 (Clear side-majority)?
3.  **Density Gate:** Is the Impact Density > 500 (Real money vs. Thin air)?

---

## 8. Strategic Execution Modes
*   **The Scalper (Trading):** Uses `MeanReversion` or `NodeRejection` to extract 2-3 ticks of profit using **VWAP** as the exit target.
*   **The Value Hunter (Betting):** Uses `MomentumBreakout` to follow "Smart Money" into the race. Positions are held for larger probability shifts or until the "Off."

---
**Goal:** Transition from "Following Price" to "Following Intent" across all price brackets.
