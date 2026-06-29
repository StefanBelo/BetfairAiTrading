---
title: "The Shape of Fear: Deriving Market Regimes from Skew"
date: 2026-06-23
source: "Tale of Two Tails / Antoine Guillon"
source_url: "https://taleoftwotails.substack.com/p/the-shape-of-fear-deriving-market?utm_source=multiple-personal-recommendations-email&utm_medium=email&triedRedirect=true"
type: research
tags: [bfexplorer, strategy, regime, skew, market-structure, betfair]
---

# The Shape of Fear: Deriving Market Regimes from Skew

**Original article:** [The Shape of Fear: Deriving Market Regimes from Skew](https://taleoftwotails.substack.com/p/the-shape-of-fear-deriving-market?utm_source=multiple-personal-recommendations-email&utm_medium=email&triedRedirect=true)

## Overview

This article builds a real-time regime classifier from the geometry of the SPX volatility surface alone, focusing on skew features rather than spot returns or realized volatility. The key claim is that the market's "shape of fear" is visible in the surface, and a walk-forward hidden Markov model can identify persistent calm and stressed states without looking at price.

## Core concepts

- **Skew as a regime signal:** a steep downside skew measures asymmetric demand for downside protection versus upside participation. Skew is treated as a distinct signal, not just a byproduct of higher volatility.
- **Geometry features:** the model uses six surface quantities from the nearest 30-day expiry, including ATM vol, 25-delta skew, smile convexity, put-wing richness, call-wing richness, and term-structure slope.
- **Walk-forward discipline:** the model is estimated on a rolling historical window and labels each day using only data available up to that day. This avoids look-ahead bias from smoothing or full-sample fitting.
- **Two persistent regimes:** a two-state model outperformed more complex state counts on predictive out-of-sample likelihood. The regimes separate into calm and stressed states.
- **Skew is not just volatility:** the full skew-based model differs meaningfully from a volatility-only regime model. 25-delta skew was the most discriminating feature between calm and stress, showing skew adds genuine information beyond ATM vol.
- **Regime meaning:** calm regimes have lower ATM volatility, flatter skew, richer calls, and an upward sloping term structure. Stressed regimes show higher vol, steeper downside skew, more put-rich wings, and a flatter near-term term structure.

## Betfair use case

- **Regime labeling for sports markets:** apply the same principle to Betfair by building regime indicators from the shape of market-implied prices and liquidity, rather than only from price momentum.
- **Skew analogue:** on Betfair, the asymmetry between back-leaning and lay-leaning book pressure can play a similar role to option skew. A strong bias in the book shape may indicate a stressed or fear-driven regime.
- **Feature design:** use top-of-book/back-lay spread metrics, depth-weighted imbalance, and term-structure analogues such as price movement sensitivity at different time slices before event start.
- **Real-time, no-lookahead:** compute regime labels live using only current and past market state. Avoid using future market moves or look-ahead smoothing in the training of the regime model.
- **Context layer:** use regime labels to condition trading decisions—e.g., reduce aggressive scalping in stressed regimes, or favor mean-reversion trades in calm regimes where the market appears structurally balanced.

## BFExplorer agentic platform fit

- **Agentic regime monitor:** build a BFExplorer agent that watches market-structure geometry and produces a regime state stream. The agent can surface a "calm vs stressed" view for each market.
- **Surface-based state model:** implement a lightweight state classifier using derived features from Betfair order book shape and recent matched flow, with periodic re-calibration on trailing windows.
- **Explainable regime output:** the agent can explain regime shifts using feature contributions, e.g. "Book shape shows elevated downside imbalance and term-structure flattening, consistent with a stress-like regime."
- **Strategy conditioning:** use the regime label to gate trade aggressiveness, adjust sizing, or select between long-horizon and short-horizon execution policies.

## Strategy implications

- The article supports using market-shape features as a contextual layer rather than a direct forecast. A regime label can be valuable even if it is only descriptive of the present.
- For Betfair, the most promising application is likely not a single skew signal but a regime-aware framework that combines book geometry, flow asymmetry, and event timing.
- The disciplined, walk-forward estimation approach is especially important for agentic trade systems: regime models should be retrained and standardized on rolling windows to avoid hidden future leaks.

## Next ideas

- Prototype a Betfair regime classifier using book imbalance, depth distribution, and short-term price sensitivity.
- Compare regime-normalized signals versus raw signals in in-play markets.
- Add a BFExplorer agent that tags markets as "calm" or "stressed" and chooses strategy templates accordingly.
- Investigate whether Betfair liquidity shape contains a second axis of structure beyond simple price level, analogous to the residual skew dimension in the article.
