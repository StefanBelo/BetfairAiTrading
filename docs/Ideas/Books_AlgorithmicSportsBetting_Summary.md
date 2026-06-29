---
title: "The Books I Actually Recommend — Maurice Berk"
aliases: ["The Books I Actually Recommend — Maurice Berk"]
type: note
tags: [books, reading-list, substack, research, ai-agent, bfexplorer]
source: "Algorithmic Sports Betting"
source_url: "https://algorithmicsportsbetting.substack.com/p/the-books-i-actually-recommend-as"
date: 2026-05-02
---

# Summary — "The Books I Actually Recommend" (Maurice Berk)

Source: https://algorithmicsportsbetting.substack.com/p/the-books-i-actually-recommend-as

This document lists every book referenced in Maurice Berk's article, followed by concise, actionable notes on how each book's ideas can be applied in a BFExplorer AI agentic architecture. At the end I add a few additional book recommendations relevant to building agentic betting systems.

---

## Books Mentioned (with agentic use-cases)

- **Lucky Devils — Kit Chellel**
  - Quick note: a narrative/history of professional gamblers and how home computing changed the game.
  - How to use with BFExplorer agents:
    - Create a "research/narrative" agent that converts historical case studies into checklist heuristics and scenario-driven test cases (e.g., market shifts when technology changes).
    - Use as inspiration for exploration policies and anomaly detection triggers (detect structural changes that warrant new models).

- **The Logic of Sports Betting — Ed Miller & Matthew Davidow**
  - Quick note: primer on odds, market structure and practical betting concepts.
  - How to use with BFExplorer agents:
    - Implement an `odds-interpreter` module (devigging, implied probability conversion) as a canonical preprocessing step.
    - Use the book's breakdown as unit tests for pricing agents and sanity checks for any new model's probability outputs.

- **Statistical Sports Models in Excel — Andrew Mack**
  - Quick note: practical modelling approaches, good for prototyping.
  - How to use with BFExplorer agents:
    - Build a `prototype-to-code` agent that ingests spreadsheet logic (features, weights) and converts it into reproducible model code or templates.
    - Use these Excel patterns for quick baseline models and feature generators inside an agent research loop.

- **A Man For All Markets — Ed Thorp**
  - Quick note: autobiography and thinking-about-markets lessons from a serial quant.
  - How to use with BFExplorer agents:
    - Encode Thorp-style experimental cycles into a `research-agent` (formulate hypothesis → test → iterate) and log decisions for reproducibility.
    - Use as a blueprint for meta-strategies: diversify across market types and preserve tools for transfer learning between markets.

- **On The Edge — Nate Silver**
  - Quick note: profiles of risk takers and forecasters; also references Silver’s forecasting work.
  - How to use with BFExplorer agents:
    - Implement calibration and forecast-ensemble agents (Brier score/Bayesian model averaging) to combine model outputs.
    - Use behavioral insights to add features that capture mass-market biases (public sentiment, attention spikes).

- **The Signal and the Noise — Nate Silver**
  - Quick note: fundamentals of good forecasting and model evaluation.
  - How to use with BFExplorer agents:
    - Create an `evaluation-agent` that routinely checks calibration, overfitting, and predictive value (not just accuracy).
    - Operationalise lessons into model-selection criteria and monitoring alerts.

- **Beyond The Odds — Elihu D. Feustel**
  - Quick note: practical market-beating ideas and the "Answer Key" concept.
  - How to use with BFExplorer agents:
    - Implement an "Answer Key" agent that treats market prices as priors and estimates adjusted priors (Empirical Bayes) to generate alternative lines.
    - Use the book's worked examples as templates for automated backtests comparing alt-lines to exchange prices.

- **Precision — C X Wong**
  - Quick note: mathematically heavy horse-racing modelling (often translated from Chinese editions).
  - How to use with BFExplorer agents:
    - Build specialized `racing-modelling` agent modules that implement Wong's feature transforms and multidimensional integration techniques.
    - Use this for candidate generation in racing markets where domain-specific math yields alpha.

- **But How Much Did You Lose? — Dan Abrams**
  - Quick note: practical treatments of Kelly and staking decisions.
  - How to use with BFExplorer agents:
    - Add a `staking-agent` implementing Kelly/Kelly-fraction with constraints (drawdown caps, capacity aware sizing) and simulate before deployment.
    - Integrate dynamic sizing into execution agents so stake scales to both edge and market liquidity.

- **The Laws Of Trading — Agustin Lebron**
  - Quick note: short, sharp tenets for trading practice.
  - How to use with BFExplorer agents:
    - Convert the laws into operational rules and automated checks: counterparty awareness, latency/tech requirements, robustness tests.
    - Use as a safety/operationalization checklist agent (pre-deploy checklist for any new strategy).

- **Expected Returns — Antti Ilmanen**
  - Quick note: systematic view on risk premia across asset classes.
  - How to use with BFExplorer agents:
    - Search for analogues of financial risk premia in betting markets (e.g., favourite-longshot bias, volatility premia in props) using a `risk-premia` discovery agent.
    - Use portfolio-construction agents to combine uncorrelated bet types to smooth P&L.

- **A Philosophy Of Software Design — John Ousterhout**
  - Quick note: software design principles for maintainable systems.
  - How to use with BFExplorer agents:
    - Apply principles to the agent architecture (clear module boundaries, naming conventions, tests) so agents are auditable and safe.
    - Build a `code-quality` agent that enforces design patterns across strategy modules.

- **Designing Data-Intensive Applications — Martin Kleppmann**
  - Quick note: system design for robust data platforms.
  - How to use with BFExplorer agents:
    - Use recommended architectures (event sourcing, logs, streams) to power realtime agents that react to market microstructure.
    - Add a `data-observability` agent to validate inputs, detect pipeline breaks, and version datasets used for model training.

- **Chasing Points: A Season on The Pro Tennis Circuit — Gregory Howe**
  - Quick note: sports narrative useful for contextual domain understanding.
  - How to use with BFExplorer agents:
    - Use to generate features about player incentives, scheduling and travel fatigue that are otherwise absent from box-score data.
    - Build a `contextual-features` agent that augments quantitative inputs with qualitative signals drawn from curated reading.

- **Retail Options Trading — Euan Sinclair**
  - Quick note: touches on the essence of edge and options-thinking.
  - How to use with BFExplorer agents:
    - Adapt the "essence of edge" concept to produce edge-quantification modules and stress-test agents that measure fragility to distributional shifts.

Other referenced/regarded works (useful pointers): *The Kelly Capital Growth Investment Criterion: Theory and Practice*; *Fortune's Formula* (both staking foundations).

---

## My Additional Recommendations (for an agentic BFExplorer stack)

- **Reinforcement Learning: An Introduction — Sutton & Barto**
  - Why: foundational RL concepts for agentic decision-making and sequential bet execution.
  - Use-case: train simulation-based RL agents for execution, market-making, and exploration/exploitation trade-offs in a controlled environment.

- **Probabilistic Machine Learning: An Introduction — Kevin P. Murphy**
  - Why: modern probabilistic modelling and Bayesian methods.
  - Use-case: build Bayesian agents for uncertainty quantification, Empirical Bayes answer-key implementations, and principled model ensembles.

- **Hands-On Machine Learning with Scikit-Learn, Keras, and TensorFlow — Aurélien Géron**
  - Why: practical, production-oriented ML workflows.
  - Use-case: implement reproducible training pipelines, model serialization, and CI/CD for model deployment inside BFExplorer agent services.

- **Thinking, Fast and Slow — Daniel Kahneman**
  - Why: behavioral biases and human decision-making.
  - Use-case: codify common bettor biases as features and design agents that detect and exploit predictable mispricings caused by those biases.

---

## Suggested immediate actions (how I would implement this)

1. Implement the basic `odds-interpreter` and `staking-agent` (from *The Logic of Sports Betting* and *But How Much Did You Lose?*) as priority modules.
2. Build a `data-observability` agent (Kleppmann) and a `research-agent` that runs hypothesis tests (Thorp patterns, Feustel answer key).
3. Prototype an RL execution agent in simulation (Sutton & Barto) to understand dynamics of bet placement and market impact.

---