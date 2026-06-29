---
title: "OHLC Strategy Comparison: Micro-Momentum vs Gemini Candlestick"
type: research
status: active
tags: [trading-strategy, ohlc, comparison, horse-racing, fsharp]
---

# OHLC Strategy Comparison: Micro-Momentum vs Gemini Candlestick

This comparison document contrasts two Betfair horse-racing strategy designs:

1. `OHLC_Micro_Momentum_Strategy_Implementation.md`
2. `OHLC_Candlestick_Trading_Strategy.md`

The goal is to identify where the two approaches overlap, where they differ, and how their strengths can be combined.

## 1. Shared foundation

Both strategies are built on the same core assumptions:

- Each runner is represented by three 5-minute OHLC candles.
- Market moves are driven by short-term momentum and liquidity flow.
- Horse racing is a closed-loop market where cross-runner interdependencies matter.
- Tight spreads and real volume are critical for safe x-tick trades.

Both documents also use the same trading domain language: momentum/steam, drift, reversion, and liquidity depth.

## 2. Strategy focus

### Micro-Momentum Implementation

- Focuses on a concrete F# implementation pattern.
- Defines explicit result types via `SignalType` and `SelectionSignal`.
- Treats every runner as a scored candidate with one of three states:
  - `MomentumCandidate`
  - `ReversionCandidate`
  - `Neutral`
- Emphasizes runner-level scoring and ranking.

### Gemini Candlestick Strategy

- Focuses on market-state scanning and candlestick behavior.
- Defines a field-level scanner with anchor/reactor logic.
- Uses broader signal types such as:
  - `MomentumBreakout`
  - `LaggingReactor`
  - `Neutral`
- More explicitly frames cross-runner dependency through the favorite/reactor relationship.

## 3. Signal classification

### Micro-Momentum

- Uses `SignalType` for each selection.
- Momentum is assigned when the last candle confirms a directional move plus volume acceleration and consistency.
- Reversion is assigned when a large wick and reversal structure appear in candle 2 or candle 3.
- Neutral is assigned when neither condition holds.

### Gemini

- Uses signals to express market roles:
  - `MomentumBreakout` for the primary mover.
  - `LaggingReactor` for selections that are expected to move after the anchor.
  - `Neutral` for consolidating runners.
- Explicitly evaluates the field to identify the market anchor and expected field reaction.

## 4. Liquidity and execution metrics

### Micro-Momentum

- Uses volume acceleration and candle body strength.
- Measures a selection's liquidity share and filters by tight spread.
- Uses `AverageCandleRange` and `TrendBias` consistency.

### Gemini

- Introduces specific microstructure metrics:
  - `TVPT` (Traded Volume Per Tick)
  - `VVI` (Volatility-to-Volume Index)
  - `TrendIntegrity`
- Uses these metrics to select the path of least resistance and avoid noisy runners.

## 5. Field-level logic

### Micro-Momentum

- Uses `fieldProbabilitySum` to assess implied probability balance.
- Scores and ranks candidates across the field.
- Treats the field state as a set of assigned runner signals.

### Gemini

- Uses anchor/reactor dynamics to model why certain runners should move second.
- Directly labels a `LaggingReactor` when the field should correct after an anchor move.
- Encourages a market-wide scan rather than a single-runner score.

## 6. Reversion handling

### Micro-Momentum

- Reversion is detected by candle structure in the final two candles.
- Requires wick ratio, reversal move, and volume surge.
- Targets the mean of previous candles or the mid-price of candle 2.

### Gemini

- Reversion is framed as a mean-reversion to the 10-minute average.
- Uses the large wick/spike retreat dynamic as a sign of exhaustion.

## 7. Best use cases

### Micro-Momentum

- Best when you want a clear F# implementation path.
- Useful for runner-by-runner state tracking and candidate ranking.
- Good for building a deterministic scanning engine with full field signal assignment.

### Gemini

- Best when you want a richer market-state model.
- Useful for understanding anchor/reactor dynamics in a live market.
- Good for adding microstructure metrics like TVPT and trend integrity.

## 8. Complementary merge opportunities

These two approaches can be combined effectively:

- Keep `SignalType` assignment for every runner from the Micro-Momentum design.
- Add `TVPT`, `VVI`, and `TrendIntegrity` from the Gemini design.
- Extend the Micro-Momentum runner scoring to include anchor/reactor context.
- Use the Gemini field scanner to detect the primary mover and generate lagging-reactor candidates.

## 9. Recommendation

For a unified strategy document and implementation:

1. Use the Micro-Momentum doc as the F# implementation backbone.
2. Add Gemini-style market scanning and field-state reasoning.
3. Preserve `SignalType` for runner state and add `LaggingReactor`/anchor logic.
4. Use TVPT as a liquidity density filter and trend integrity as a confirmation metric.

This hybrid will give both a practical F# design and a deeper market-state model suitable for pre-race horse trading.
