---
title: "OHLC Micro-Momentum Strategy Implementation"
aliases: ["OHLC Trading Strategy", "F# Horse Racing OHLC Strategy"]
type: research
tags: [trading-strategy, fsharp, ohlc, market-microstructure, automation]
---

# OHLC Micro-Momentum Strategy Implementation

This document defines a lightweight, market-structure-driven horse racing trading strategy that uses the last three 5-minute OHLC candles for each selection. The core goal is to identify the best candidate for a quick x-tick trade based on momentum, volume depth, and cross-runner market balance.

> [!NOTE]
> This strategy is designed for fast pre-race trading using price action and liquidity signals. It is not a standalone winner-prediction model.

## 1. Strategy Overview

We use a 15-minute window of OHLC data per horse, combined with race-level implied probability and volume ratios, to score each runner. The strategy works in two stages:

1. Runner-level signal extraction from the last three 5-minute candles.
2. Field-level ranking to find the highest-conviction trade candidate.

The strategy is intended for F# implementation and assumes access to:

- Selection-level OHLC data for the three most recent 5-minute periods.
- Matched volume and available liquidity measures.
- Market-wide implied probability and total matched volume.

## 2. Signal Architecture

We derive two primary signal families:

- **Momentum Breakouts**: strong directional steam or drift confirmed by volume and candle body strength.
- **Mean Reversion / Pressure Relief**: high-probability corrective moves after a sharp temporary spike.

### Signal Types

```fsharp
type SignalType =
    | MomentumCandidate of direction: string * confidence: decimal
    | ReversionCandidate of target: decimal * divergence: decimal
    | Neutral

type SelectionSignal = {
    SelectionId: string
    SignalType: SignalType
    Score: decimal
    VolumeDepth: decimal
    LiquidityShare: decimal
    TrendBias: string
    Candle3Strength: decimal
    TimeStamp: DateTime
}
```

### OHLC Inputs per Runner

Each horse produces three candle records:

- Candle1: Open1, High1, Low1, Close1, Volume1
- Candle2: Open2, High2, Low2, Close2, Volume2
- Candle3: Open3, High3, Low3, Close3, Volume3

The candidate scoring is built from these metrics across all three candles:

- `TrendDelta = Close3 - Open1`
- `CandleBody3 = abs(Close3 - Open3)`
- `CandleRange3 = High3 - Low3`
- `MomentumConsistency = count of candles with the same directional close / 3`
- `VolumeAcceleration = Volume3 / max(1m, min Volume1 Volume2)`
- `VolumeTrend = (Volume3 - Volume2) / max(1m, Volume2)`
- `AverageCandleRange = (Range1 + Range2 + Range3) / 3`
- `ImpliedProbabilityShift = 1m/Open3 - 1m/Open1`

## 3. Runner-Level Rules

### A. Momentum Candidate Rule

A runner becomes a momentum candidate when:

- Candle 3 is trending strongly in one direction.
- `VolumeAcceleration > 1.5`.
- The candle body is at least 50% of the full range.
- The spread is tight and there is enough available matched volume.

Every selection is evaluated and assigned one of the three `SignalType` states. This means the entire field is categorized, not just the traded candidate.

Example logic:

```fsharp
let isMomentumCandidate candle1 candle2 candle3 =
    let body3 = abs (candle3.Close - candle3.Open)
    let range3 = candle3.High - candle3.Low
    let bodyRatio3 = if range3 > 0m then body3 / range3 else 0m
    let direction1 = candle1.Close < candle1.Open
    let direction2 = candle2.Close < candle2.Open
    let direction3 = candle3.Close < candle3.Open
    let directionCount = [ direction1; direction2; direction3 ] |> List.filter id |> List.length
    let direction = if directionCount >= 2 then "BACK" else "LAY"
    let consistency = decimal directionCount / 3m
    let move = candle3.Close - candle1.Open
    let strongTrend = abs move > 0.05m * candle1.Open
    let volumeAcceleration = candle3.Volume / max 1m (min candle1.Volume candle2.Volume)
    let volumeTrend = (candle3.Volume - candle2.Volume) / max 1m candle2.Volume
    let range1 = candle1.High - candle1.Low
    let range2 = candle2.High - candle2.Low
    let averageRange = (range1 + range2 + range3) / 3m

    direction, strongTrend, consistency >= 0.66m, volumeAcceleration > 1.5m, volumeTrend > 0.2m, bodyRatio3 > 0.5m, averageRange > 0m
```

- If the move is downward and confirmed, the candidate is a `MomentumCandidate("BACK", confidence)`.
- If the move is upward and confirmed, the candidate is a `MomentumCandidate("LAY", confidence)`.

If the runner does not pass momentum qualification, it can still be assigned `ReversionCandidate` or `Neutral`, so the strategy knows the state of the whole field.

### B. Reversion Candidate Rule

A runner becomes a reversion candidate when:

- Candle 3 shows a large wick relative to the body.
- A sharp move has already been reversed within the candle.
- The runner is not already the race favorite.

Example logic:

```fsharp
let isReversionCandidate candle1 candle2 candle3 =
    let wickRatio candle =
        let wickTop = candle.High - max candle.Open candle.Close
        let wickBottom = min candle.Open candle.Close - candle.Low
        let body = abs (candle.Close - candle.Open)
        max wickTop wickBottom / max 1m body

    let wickRatio2 = wickRatio candle2
    let wickRatio3 = wickRatio candle3
    let body2 = abs (candle2.Close - candle2.Open)
    let body3 = abs (candle3.Close - candle3.Open)
    let reversalMove = abs (candle3.Close - candle2.Close) < abs (candle2.Close - candle1.Close)
    let volumeSurge = candle3.Volume > candle2.Volume * 1.2m && candle2.Volume > candle1.Volume * 1.1m

    (wickRatio2 > 1.8m || wickRatio3 > 2m) && reversalMove && volumeSurge
```

If true, calculate a revert target using the average close of Candle 1/2 or the mid-price of Candle 2.

## 4. Field-Level Ranking

Because horse racing is a closed loop, we score candidates against the whole field.

### A. Implied Probability Balance

Convert each candidate's latest close odds into implied probability:

```fsharp
let impliedProbability odds = 1m / odds
```

Then compare candidate movement against the field:

```fsharp
let fieldProbabilitySum selections =
    selections |> Seq.sumBy (fun s -> impliedProbability s.Close3)
```

A strong candidate should either:

- Take probability share from the rest of the field when it is steam-driven.
- Offer an inevitable correction if the favorite has already consumed too much probability.

### B. Liquidity Score

A safe candidate must have volume, not just momentum.

```fsharp
let liquidityScore totalVolume selectionVolume =
    min 1m (selectionVolume / max 1m totalVolume)
```

Prefer candidates with high matched volume and a stable 1-tick or 2-tick spread.

### C. Conviction Score

Combine multiple dimensions:

- Trend strength
- Volume acceleration
- Liquidity share
- Cross-field divergence
- Candle 3 body/range ratio

Example scoring:

```fsharp
let calculateConvictionScore signal trendScore volumeScore liquidityScore imbalanceScore =
    (trendScore * 0.35m) +
    (volumeScore * 0.25m) +
    (liquidityScore * 0.20m) +
    (imbalanceScore * 0.20m)
```

Rank selections by this score and only trade the top candidate.

## 5. Execution Filter

Trade only when all filters pass:

- Narrow spread (1 or 2 ticks)
- Candle 3 volume is accelerating versus Candle 2 and Candle 1
- `TrendBias` is consistent across at least two of the three candles
- Market is in the final 10 minutes before off
- If available, Weight of Money or order book depth supports the directional thesis

### Example execution condition

```fsharp
let canExecute selectionSignal =
    selectionSignal.Score > 0.65m &&
    selectionSignal.LiquidityShare > 0.15m &&
    (selectionSignal.SignalType <> Neutral)
```

If the signal is `MomentumCandidate("BACK", _)`, open a back and offset with a lay x ticks lower. If it is `MomentumCandidate("LAY", _)`, open a lay and offset with a back x ticks higher.

## 6. F# Implementation Pattern

This strategy is designed to fit into an F# market scanner and signal engine.

### Data Types

```fsharp
type Candle5m = {
    Open: decimal
    High: decimal
    Low: decimal
    Close: decimal
    Volume: decimal
}

type OHLCHistory = {
    SelectionId: string
    SelectionName: string
    Candles: Candle5m list
    TotalVolume: decimal
    SpreadTicks: int
}
```

### Runner Scoring Module

```fsharp
module RunnerScoring =

    let calculateTrendStrength candles =
        let c1, c2, c3 = candles.[0], candles.[1], candles.[2]
        let trendMagnitude = abs (c3.Close - c1.Open) / c1.Open
        let direction = if c3.Close < c1.Open then "BACK" else "LAY"
        let candleDirections =
            [ c1.Close < c1.Open; c2.Close < c2.Open; c3.Close < c3.Open ]
        let consistency = decimal (candleDirections |> List.filter id |> List.length) / 3m
        trendMagnitude, direction, consistency

    let calculateVolumeDepth candles =
        let total = candles |> List.sumBy (fun c -> c.Volume)
        let volume3 = candles.[2].Volume
        let volume2 = candles.[1].Volume
        let acceleration = if volume2 > 0m then (volume3 - volume2) / volume2 else 0m
        volume3 / max 1m total, acceleration

    let calculateCandleStrength candles =
        let c3 = candles.[2]
        let body = abs (c3.Close - c3.Open)
        let range = c3.High - c3.Low
        if range > 0m then body / range else 0m

    let deriveSelectionSignal history =
        let c1, c2, c3 = history.Candles.[0], history.Candles.[1], history.Candles.[2]
        let trendMagnitude, direction, consistency = calculateTrendStrength history.Candles
        let volumeDepth, volumeAcceleration = calculateVolumeDepth history.Candles
        let bodyRatio = calculateCandleStrength history.Candles
        let averageRange =
            let r1 = c1.High - c1.Low
            let r2 = c2.High - c2.Low
            let r3 = c3.High - c3.Low
            (r1 + r2 + r3) / 3m
        let momentumQualified = trendMagnitude > 0.04m && consistency >= 0.66m && volumeDepth > 0.2m && volumeAcceleration > 0.2m && bodyRatio > 0.5m
        let reversionQualified =
            let wickRatio candle =
                let wickTop = candle.High - max candle.Open candle.Close
                let wickBottom = min candle.Open candle.Close - candle.Low
                let body = abs (candle.Close - candle.Open)
                max wickTop wickBottom / max 1m body
            (wickRatio c2 > 1.8m || wickRatio c3 > 2m)
            && abs (c3.Close - c2.Close) < abs (c2.Close - c1.Close)
            && c3.Volume > c2.Volume * 1.2m && c2.Volume > c1.Volume * 1.1m

        if momentumQualified then
            Some {
                SelectionId = history.SelectionId
                SignalType = MomentumCandidate(direction, min 1m (trendMagnitude * 3m))
                Score = 0m
                VolumeDepth = volumeDepth
                LiquidityShare = history.TotalVolume / max 1m history.TotalVolume
                TrendBias = direction
                Candle3Strength = bodyRatio
                TimeStamp = DateTime.UtcNow
            }
        elif reversionQualified then
            Some {
                SelectionId = history.SelectionId
                SignalType = ReversionCandidate((c1.Close + c2.Close) / 2m, abs ((1m / c3.Close) - (1m / c1.Close)))
                Score = 0m
                VolumeDepth = volumeDepth
                LiquidityShare = history.TotalVolume / max 1m history.TotalVolume
                TrendBias = direction
                Candle3Strength = bodyRatio
                TimeStamp = DateTime.UtcNow
            }
        else
            Some {
                SelectionId = history.SelectionId
                SignalType = Neutral
                Score = 0m
                VolumeDepth = volumeDepth
                LiquidityShare = history.TotalVolume / max 1m history.TotalVolume
                TrendBias = direction
                Candle3Strength = bodyRatio
                TimeStamp = DateTime.UtcNow
            }
```

## 7. Risk Management and Guardrails

This is a short-term x-tick trading strategy, so risk controls are essential:

- Limit exposure to one runner per market.
- Always require a minimum matched volume threshold in Candle 3.
- Avoid trades when Candle 3 has a long wick on both sides.
- Do not trade if the field’s implied probability sum is far from a stable range.
- Use a hard stop if the offset price is missed by more than 1 tick.

## 8. Practical Notes

- The strategy is best used in the last 10 minutes before the race.
- Three candles are useful for momentum detection, but not sufficient alone for winner prediction.
- The highest-conviction candidate is the one with both strong direction and volume depth, not just the one with the biggest price move.
- Cross-runner balance matters: if one horse is rapidly steam-driving, the best trade may be a lagging secondary horse that still has room to reprice.

## 9. Summary

This OHLC Micro-Momentum Strategy uses three 5-minute candles per horse plus race-level liquidity measures to identify the safest, highest-probability candidate for a quick trade. The key edges are:

- using the latest candle as the execution trigger,
- filtering by volume acceleration and body strength,
- ranking candidates by cross-field probability balance,
- and trading only the top-ranked runner with sufficient depth.

This design is intentionally lightweight for F#, while still leveraging meaningful OHLC structure and market microstructure signals.
