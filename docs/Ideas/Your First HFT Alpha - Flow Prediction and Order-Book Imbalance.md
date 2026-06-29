---
title: "Your First HFT Alpha: Flow Prediction and Order-Book Imbalance"
date: 2026-06-23
source: "QFEX Research"
source_url: "https://research.qfex.com/p/your-first-hft-alpha-from-flow-prediction?utm_source=multiple-personal-recommendations-email&utm_medium=email&triedRedirect=true"
type: research
tags: [bfexplorer, strategy, market-microstructure, order-book, flow, imbalance, betfair]
---

# Your First HFT Alpha: Flow Prediction and Order-Book Imbalance

**Original article:** [Your First HFT Alpha: From Flow Prediction to Order-Book Imbalance](https://research.qfex.com/p/your-first-hft-alpha-from-flow-prediction?utm_source=multiple-personal-recommendations-email&utm_medium=email&triedRedirect=true)

## Overview

This article presents a simple but powerful HFT alpha framework: start with public order book and trade flow, then turn that flow into a predictive signal. The key insight is that market makers do not want to hold long-term inventory, so imbalances in flow and book liquidity often foreshadow short-term price moves.

## Core concepts

- **Flow as the base signal:** define net traded quantity as bought minus sold over a horizon. The article emphasizes that the important choice is not the raw formula, but the definition of the horizon and the flow source.
- **Exponential decay:** recent flow should matter more than old flow. A decayed sum of trade imbalances is a better signal than a fixed window, with a decay factor controlled by a half-life parameter.
- **Order-book imbalance (OBI):** the simplest top-of-book alpha is

  OBI = (bestBidQty - bestAskQty) / (bestBidQty + bestAskQty)

  This measures whether passive liquidity is leaning buyer or seller.
- **Horizon defines the strategy:** the meaning of "future price" is critical. Very short horizons behave like classic market making, while longer horizons require deeper book structure and a different imbalance definition.
- **Feature selection is alpha:** the secret edge is often knowing which trades and book events to include. One real example in the article is restricting to IOC-like aggressive flow to reduce noise from retail market orders.
- **Prediction vs reaction:** simple reactive flow measures are a good starting point, but competitive alphas try to predict future flow rather than only react to observed flow.

## Betfair use case

- **Order book leverage:** Betfair sports markets provide a public matched/unmatched order book, which is directly analogous to the limit order book used by HFT market makers.
- **Flow prediction:** net matched back vs lay traded volume can serve as a sports-market flow signal. Applying decay to recent matched aggression can reveal pressure shifts before the price adjusts.
- **Imbalance signal:** best back stake vs best lay stake at the top of the book can be treated as an OBI-like feature. In practice, this can be extended to multiple price levels for longer horizons.
- **Horizon tailoring:** for football/tennis, very short horizons might track micro price moves in-play, while longer horizons can use deeper book imbalance and event-specific dynamics (injuries, goals, momentum).
- **Noise filtering:** on Betfair, differentiating informative order types is equivalent to identifying aggressive and institutionsized matched volume versus noisy retail flow.

## BFExplorer agentic platform fit

- **Agentic signal extraction:** build an agent that continuously monitors Betfair market book state, computes flow and imbalance metrics, and proposes directional hypotheses.
- **Adaptive horizon selection:** use the BFExplorer agent to adapt the half-life and book depth weights by market type, event time, and liquidity regime.
- **Context-aware action:** the agent can reason about whether a detected imbalance is a transient liquidity skew or a genuine shift in participant sentiment, then choose whether to trade, hedge, or wait.
- **Explainability:** capture the alpha definition in BFExplorer as a structured thesis: "Current best-book imbalance is buyer-leaning and recent matched back flow exhibits exponential decay with half-life X, implying short-term upward pressure." This supports automated decision-making with human review.

## Strategy implications

- The article reinforces that simple order-book and flow features can be effective if they are defined precisely and calibrated for the target horizon.
- For Betfair sports, this suggests a hybrid approach: use order-book imbalance for signal orientation, then validate with traded flow and timing filters.
- The strongest edge may come from identifying the right subset of flow (e.g. aggressive, price-taking volume) rather than from a more complex formula.

## Next ideas

- Test a Betfair OBI-style signal using best-back and best-lay aggregated stakes.
- Compare fixed-window trade imbalance vs exponentially decayed flow on live in-play markets.
- Add an agentic BFExplorer module that scores markets by imbalance strength and recommends short-term market making or scalping actions.
- Explore whether Betfair order IDs or market metadata provide a systematic way to distinguish retail-like noise from informed flow.
