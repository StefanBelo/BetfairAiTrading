---
title: "The Art of Player Strength Models"
aliases: ["The Art of Player Strength Models - SharpsResearch"]
type: idea
tags: [automation, bfexplorer, basketball, ml, strategy]
date: 2026-05-07
---

# The Art of Player Strength Models

Source: https://thequantativegambler.substack.com/p/the-art-of-player-strength-models

Published: May 7, 2026

## Summary

This post argues that player strength models such as DARKO, LEBRON, RAPM, SharpsRAPM, and EPM are largely solving the same problem: compressing noisy basketball data into a useful player-strength signal. The key difference between them is not just architecture, but the design choices and storytelling decisions made by the creators.

The author emphasizes that building these metrics is an art, not a purely mathematical exercise. Many important model decisions are subjective and depend on the goal:

- Regularization choices (ridge, lasso, hierarchical priors) shape how much signal is preserved versus noise suppressed.
- Eligibility rules and small-sample handling determine which players are included and how uncertainty is expressed.
- Window length, decay, and normalization trade off between stability and responsiveness.
- Metric design differs by intent: some models target fan-facing player value, some target scouting, and others target machine learning features.
- Communication is critical: a metric intended for another ML model can use arbitrary scaling, while a public metric must be interpretable.

The post makes a strong point that these metrics should not be taken as gospel. Different designs can rank players similarly but produce very different absolute values, meaning the output is a product of the modeling decisions. The same metric may be “right” for one use case and “wrong” for another.

## Key takeaways

- Player strength models are conceptually similar, but the art of design matters.
- The model’s objective drives choices around sample thresholds, regularization, and data windows.
- Outputs must be matched to the intended consumer: bettors, analysts, front offices, or other ML systems.
- Absolute values are not directly comparable across different metrics; relative ranking is more stable.
- For ML features, stability, stationarity, normalization, and drift control are more important than human-meaningful scaling.
- Common metric biases:
  - RAPM can inflate glue players.
  - LEBRON is heavily influenced by box-score stats.
  - DARKO smooths and may lag player breakouts.
  - SharpsRAPM is designed as z-scores for feature comparability.

## Possible ideas for Betfair trading with the AI agentic BFExplorer framework

1. Use player-strength-style features in market prediction models.
   - Create strength/quality signals from game-by-game player impact data.
   - Feed these features into match outcome or prop prediction models.

2. Treat the feature design as an agentic optimization problem.
   - Let BFExplorer experiment with alpha/regularization, decay windows, and sample weighting.
   - Select the best variant based on predictive performance for Betfair markets.

3. Use normalized, stationary player-strength outputs for model stability.
   - Convert raw strength metrics into rolling z-scores or normalized features.
   - This fits the post’s idea of designing metrics for ML, not public consumption.

4. Build use-case-specific variations.
   - A responsive version for in-play and short-term markets.
   - A stable version for season-long or pre-match value assessment.

5. Incorporate communication/bias awareness into strategy selection.
   - Tag features with known biases (box-score heavy, lagging, glue-player inflation).
   - Use the BFExplorer framework to choose features that best match the betting objective.

6. Apply the same “art of model design” thinking to other sports.
   - Create analogous player/horse strength models for football, tennis, horse racing, etc.
   - Use the framework to compare design choices across sports.

7. Use metric intent as a filter when combining external data.
   - Prefer metrics built for ML feature use over public-facing rating scales.
   - Train the agent to detect when a metric is intended for comparability vs. absolute interpretation.

## Notes

This article is especially relevant for building AI-driven feature engineering pipelines in BFExplorer. It reinforces the idea that the value of a signal lies in how it is designed and tuned for the downstream betting task, rather than its face-value meaning.