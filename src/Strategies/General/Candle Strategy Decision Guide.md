# Candle Strategy Decision Guide

## Overview

This strategy analyzes each selection using candle data converted into probability space.
All direction and comparison logic is based on probability values, not raw odds.
It also derives a model-based fair win probability, implied fair back/lay prices, and an explicit entry gate to avoid weak or reversal-risk setups.

Core flow:
1. Compute per-selection market features from candles.
2. Build a composite score from directional and quality signals.
3. Apply no-trade and entry gates to avoid noisy, low-quality, or reversal-prone setups.
4. Map final score to confidence bands.
5. Convert band to action: Back, Lay, or DoNothing.

## Probability Convention

Price is converted with:

- toProbability(price) = 1.0 / price

Interpretation:
- Rising probability implies improving chance (typically Back-friendly).
- Falling probability implies weakening chance (typically Lay-friendly).

## Fair probability and implied price

The final score is mapped through a logistic-style conversion to a model fair probability.
That probability is then converted into:

- FairBackPrice = 1 / FairProbability
- FairLayPrice = 1 / (1 - FairProbability)

This gives a fair entry price estimate for both Back and Lay views.

## VWAP as a supporting fair-value reference

VWAP is a useful liquidity-weighted benchmark for safer entries.
It is not the primary score, but it can confirm or weaken the signal when the current price has moved far from the volume-weighted consensus.

Usage:
- If the model signals Back and current price is above VWAP, the setup is stronger.
- If the model signals Lay and current price is below VWAP, the setup is stronger.
- If the signal disagrees with VWAP and the score is weak, skip the trade.
- If price has moved away from VWAP on low volume, treat the move as less trustworthy.

## Calculated Analysis Parameters

### 1) Score
Final strategy score after:
- weighted feature combination,
- BackLayRatio contribution,
- signal agreement adjustment.

Usage:
- Higher positive score -> stronger Back.
- Lower negative score -> stronger Lay.
- Near zero -> no edge.

### 2) Momentum
Relative change from first candle open probability to last candle close probability.

Usage:
- Positive momentum supports Back.
- Negative momentum supports Lay.

### 3) MomentumAcceleration
Difference between later-segment momentum and earlier-segment momentum.

Usage:
- Positive acceleration means trend strengthening.
- Negative acceleration means trend weakening.

### 4) TrendConsistency
Fraction of candles moving in the same direction as overall momentum.
Range: 0.0 to 1.0.

Usage:
- High consistency means cleaner trend.
- Low consistency means mixed/choppy movement.

### 5) VolumeSpike
Current volume divided by average volume.

Usage:
- > 1.0 means participation is increasing.
- Strategy gate uses this to reject weak participation.

### 6) AverageVolume and CurrentVolume
Absolute volume context used with VolumeSpike.

Usage:
- Prevents false signals where spike exists but total liquidity is very low.

### 7) Volatility
Average probability range of candles.

Usage:
- High volatility with weak momentum is treated as noise.
- Used in no-trade gate.

### 8) BodyRatio
Average candle body size relative to full range.

Usage:
- Higher value means more decisive directional candles.
- Lower value means indecision/wicks dominating.

### 9) ReversalBias
Recent wick imbalance (upper vs lower) normalized by range.

Usage:
- Positive value indicates upward-wick pressure (potential rejection risk).
- Included as a score penalty term.

### 10) BackLayRatio
Market microstructure preference signal.

Usage:
- Above 0.5 slightly favors Back.
- Below 0.5 slightly favors Lay.
- Near 0.5 triggers a no-trade gate.

### 11) AgreementScore
Discrete consensus score from multiple signals:
- momentum,
- acceleration,
- trend consistency,
- body quality,
- back/lay ratio.

Usage:
- Positive value strengthens Back confidence.
- Negative value strengthens Lay confidence.

### 12) FairProbability
A model-derived win probability from the final adjusted score.

Usage:
- Converts score into a probability that can be compared against market odds.
- Helps decide whether the current price offers enough edge.

### 13) FairBackPrice / FairLayPrice
Implied fair entry prices derived from FairProbability.

Usage:
- Use FairBackPrice when considering Back entries.
- Use FairLayPrice when considering Lay entries.
- If the market price is worse than the fair price, skip the trade.

### 14) ReversalRisk
A risk estimate for trend reversal based on:
- weak trend consistency,
- opposite momentum acceleration,
- recent reversal-bias wick structure.

Usage:
- High reversal risk should prevent entry even when the score is moderate.

### 15) EntryAllowed / EntryReason
Explicit entry gating results.

Usage:
- EntryAllowed = false means the strategy will force DoNothing.
- EntryReason explains why the signal was rejected.

## No-Trade Gates

The strategy returns DoNothing by forcing Neutral band when any gate fails.

Current gates:
- BackLayRatio too balanced: abs(backLayRatio - 0.5) < 0.03
- Weak trend consistency: trendConsistency < 0.45
- Participation too low: volumeSpike < 1.15 or averageVolume < 25.0
- High-noise regime: abs(momentum) < 0.02 and volatility > 0.10
- Entry score too weak: abs(score) < 0.18
- Order book too balanced for entry: abs(backLayRatio - 0.5) < 0.04
- Elevated reversal risk from weak trend/acceleration/bias

## Confidence Bands

Bands from adjusted score:
- score >= 0.22 -> StrongBack
- 0.08 <= score < 0.22 -> WeakBack
- -0.08 < score < 0.08 -> Neutral
- -0.22 < score <= -0.08 -> WeakLay
- score <= -0.22 -> StrongLay

Action mapping:
- StrongBack, WeakBack -> Back
- StrongLay, WeakLay -> Lay
- Neutral -> DoNothing

## How To Use

1. Run Test.fsx. It loads the available test JSON snapshots and evaluates each market.
2. Inspect each selection output line.
3. Prefer selections where:
- tradable=true,
- band is StrongBack or StrongLay,
- agreement score has clear sign and magnitude.
4. For multiple Back candidates, prioritize highest score and ensure the fair back price is attractive.
5. For multiple Lay candidates, prioritize lowest score and ensure the fair lay price is attractive.
6. Ignore candidates with gate failures or when EntryAllowed is false unless you intentionally relax thresholds.

## Suggested Tuning Workflow

1. Log historical outputs with realized outcomes.
2. Group by band and score bucket (for example, 0.08-0.15, 0.15-0.22, >0.22).
3. Measure win rate and drawdown by bucket.
4. Adjust one threshold at a time (not all at once).
5. Re-run and compare before/after statistics.

## Notes

- This is a decision-support strategy, not a guarantee of profit.
- Keep stake sizing separate from signal generation.
- Re-calibrate thresholds per sport, market type, and time-to-start regime.
