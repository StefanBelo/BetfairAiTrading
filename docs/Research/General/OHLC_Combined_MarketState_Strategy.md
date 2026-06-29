---
title: "OHLC Combined Market-State Strategy for Horse Racing"
type: research
status: active
tags: [trading-strategy, ohlc, horse-racing, market-state, fsharp]
---

# OHLC Combined Market-State Strategy for Horse Racing

This document merges two Betfair horse-racing OHLC strategy designs into a unified approach:

- `OHLC_Micro_Momentum_Strategy_Implementation.md`
- `OHLC_Candlestick_Trading_Strategy.md`

The unified strategy preserves the practical F# implementation structure while adding richer cross-runner market-state logic and liquidity density metrics.

## 1. Strategy Overview

The strategy uses three 5-minute OHLC candles per runner plus market liquidity data to:

- classify every selection into a signal state,
- rank candidates by conviction,
- identify the primary mover and lagging reactors,
- execute the safest x-tick trade in the field.

It is designed for fast pre-race horse trading and is intentionally lightweight, but it retains enough field-aware structure to capture cross-runner dependencies.

## 2. Data Model

```fsharp
type Candle5m = {
    Open: decimal
    High: decimal
    Low: decimal
    Close: decimal
    Volume: decimal
}

type SignalType =
    | MomentumCandidate of direction: string * confidence: decimal
    | ReversionCandidate of targetPrice: decimal * divergence: decimal
    | LaggingReactor of anchorId: string * divergence: decimal
    | Neutral

type SelectionSignal = {
    SelectionId: string
    SelectionName: string
    SignalType: SignalType
    Score: decimal
    VolumeDepth: decimal
    LiquidityShare: decimal
    TrendBias: string
    Candle3Strength: decimal
    TVPT: decimal
    VVI: decimal
    TrendIntegrity: decimal
    TimeStamp: DateTime
}

type OHLCHistory = {
    SelectionId: string
    SelectionName: string
    Candles: Candle5m list
    TotalVolume: decimal
    SpreadTicks: int
}
```

## 3. Core Signals and Field State

The unified strategy classifies each runner into one of four states:

- `MomentumCandidate`: the runner appears to be the current steam/drift mover.
- `ReversionCandidate`: the runner shows a spike/fade structure and may mean-revert.
- `LaggingReactor`: the runner is a field follower expected to move after the anchor.
- `Neutral`: no clear signal.

This gives full field visibility and avoids trading without knowing the market state.

## 4. Runner-Level Analysis

### 4.1. Momentum Detection

Momentum is evaluated using all three candles:

- `TrendMagnitude`: `abs(Close3 - Open1) / Open1`
- `MomentumConsistency`: proportion of candles that move in the same direction.
- `VolumeAcceleration`: `Volume3 / max(1m, min Volume1 Volume2)`
- `VolumeTrend`: change from Candle 2 to Candle 3.
- `Candle3Strength`: body size relative to candle range.
- `TVPT`: traded volume per tick in Candle 3.
- `TrendIntegrity`: whether the three candles form a consistent directional path.

### 4.2. Reversion Detection

Reversion candidates are identified by:

- large wick ratios in candle 2 or candle 3,
- a failed follow-through move,
- a volume surge across the final two candles,
- and a weak current trend integrity.

### 4.3. Metrics Definitions

```fsharp
let impliedProbability odds = 1m / odds

let calculateTVPT candle =
    // Use implied probability range instead of raw odds range.
    // A 50-100 move is not comparable to a 2-3 move in pure odds, but
    // their probability impact can be similar.
    let probRange = abs ((1m / candle.High) - (1m / candle.Low))
    if probRange = 0m then candle.Volume else candle.Volume / probRange

let calculateProbabilityDelta candle =
    abs ((1m / candle.Close) - (1m / candle.Open))

let calculateVVI candle =
    let body = abs (candle.Close - candle.Open)
    let wicks = (candle.High - candle.Low) - body
    if body = 0m then 10m else wicks / body

let calculateTrendIntegrity c1 c2 c3 =
    let p1 = 1m / c1.Close
    let p2 = 1m / c2.Close
    let p3 = 1m / c3.Close
    if (p1 < p2 && p2 < p3) || (p1 > p2 && p2 > p3) then 1m
    elif (p1 < p3 && p2 > p1 && p2 < p3) || (p1 > p3 && p2 < p1 && p2 > p3) then 0.7m
    else 0m
```

## 5. Field-Level Market-State Logic

### 5.1. Anchor and Reactor

The field scanner first identifies the market anchor: the runner with the largest absolute implied probability shift.

```fsharp
let identifyAnchor signals =
    signals
    |> Seq.maxBy (fun s -> abs s.ProbabilityShift)
```

The anchor is usually the favorite or the most aggressively backed/laid runner. The rest of the field is then evaluated for lagging reaction.

### 5.2. Lagging Reactor Logic

A lagging reactor is a runner whose current movement lags the anchor and whose microstructure metrics indicate a cleaner follow-through path.

```fsharp
let isLaggingReactor anchor signal =
    signal.SignalType = Neutral &&
    signal.VVI < 2m &&
    signal.TrendIntegrity > 0.4m &&
    abs(signal.ProbabilityShift) < abs(anchor.ProbabilityShift) * 0.7m
```
```

Lagging reactors may be the best candidates when the anchor has already moved and the field has not yet fully rebalanced.

## 6. Unified Runner Scoring

### 6.1. Signal derivation

```fsharp
let deriveSelectionSignal history totalMarketVolume =
    let c1, c2, c3 = history.Candles.[0], history.Candles.[1], history.Candles.[2]
    let trendMagnitude = abs ((1m / c3.Close) - (1m / c1.Open))
    let direction = if c3.Close < c1.Open then "BACK" else "LAY"
    let candleDirections = [ c1.Close < c1.Open; c2.Close < c2.Open; c3.Close < c3.Open ]
    let consistency = decimal (candleDirections |> List.filter id |> List.length) / 3m
    let volumeDepth, volumeAcceleration =
        let total = history.Candles |> List.sumBy (fun c -> c.Volume)
        let v3 = c3.Volume
        let v2 = c2.Volume
        let acc = if v2 > 0m then (v3 - v2) / v2 else 0m
        v3 / max 1m total, acc
    let bodyRatio =
        let body = abs ((1m / c3.Close) - (1m / c3.Open))
        let probRange = abs ((1m / c3.High) - (1m / c3.Low))
        if probRange > 0m then body / probRange else 0m
    let tvpt = calculateTVPT c3
    let vvi = calculateVVI c3
    let trendIntegrity = calculateTrendIntegrity c1 c2 c3
    let averageProbRange =
        let r1 = abs ((1m / c1.High) - (1m / c1.Low))
        let r2 = abs ((1m / c2.High) - (1m / c2.Low))
        let r3 = abs ((1m / c3.High) - (1m / c3.Low))
        (r1 + r2 + r3) / 3m
    let momentumQualified =
        trendMagnitude > 0.0008m &&
        consistency >= 0.66m &&
        volumeDepth > 0.2m &&
        volumeAcceleration > 0.2m &&
        bodyRatio > 0.5m &&
        averageProbRange > 0m
    let reversionQualified =
        let wickRatio candle =
            let wickTop = candle.High - max candle.Open candle.Close
            let wickBottom = min candle.Open candle.Close - candle.Low
            let body = abs (candle.Close - candle.Open)
            max wickTop wickBottom / max 1m body
        (wickRatio c2 > 1.8m || wickRatio c3 > 2m) &&
        abs (c3.Close - c2.Close) < abs (c2.Close - c1.Close) &&
        c3.Volume > c2.Volume * 1.2m &&
        c2.Volume > c1.Volume * 1.1m
    let signalType =
        if momentumQualified then
            MomentumCandidate(direction, min 1m (trendMagnitude * 3m))
        elif reversionQualified then
            ReversionCandidate((c1.Close + c2.Close) / 2m, abs ((1m / c3.Close) - (1m / c1.Close)))
        else
            Neutral
    {
        SelectionId = history.SelectionId
        SelectionName = history.SelectionName
        SignalType = signalType
        Score = 0m
        VolumeDepth = volumeDepth
        LiquidityShare = history.TotalVolume / max 1m totalMarketVolume
        TrendBias = direction
        Candle3Strength = bodyRatio
        TVPT = tvpt
        VVI = vvi
        TrendIntegrity = trendIntegrity
        TimeStamp = DateTime.UtcNow
    }
```

### 6.2. Conviction score

The unified score combines momentum, liquidity, and field balance.

```fsharp
let calculateConvictionScore signal trendScore volumeScore liquidityScore imbalanceScore =
    (trendScore * 0.30m) +
    (volumeScore * 0.25m) +
    (liquidityScore * 0.20m) +
    (imbalanceScore * 0.25m)
```

Where:

- `trendScore` is derived from `TrendIntegrity` and `Candle3Strength`.
- `volumeScore` uses `TVPT` and `VolumeDepth`.
- `liquidityScore` uses `LiquidityShare` and `SpreadTicks`.
- `imbalanceScore` is based on cross-field probability shift and anchor/reactor positioning.

## 7. Execution framework

### 7.1. Field scanner

```fsharp
let scanField histories totalMarketVolume =
    let signals = histories |> List.map (fun h -> deriveSelectionSignal h totalMarketVolume)
    let anchor = signals |> List.maxBy (fun s -> abs ((1m / (List.last h.Candles).Close) - (1m / (List.head h.Candles).Open)))

    signals
    |> List.map (fun s ->
        match s.SignalType with
        | Neutral when isLaggingReactor anchor s ->
            { s with SignalType = LaggingReactor(anchor.SelectionId, abs ((1m / (List.last h.Candles).Close) - (1m / (List.head h.Candles).Open))) }
        | _ -> s)
```
```

### 7.2. Execution filter

Trade only when:

- `SpreadTicks <= 2`
- `TVPT` is high for the selection and the field is liquid.
- `TrendBias` is consistent across at least two candles.
- `SignalType` is not `Neutral`.
- If the signal is `LaggingReactor`, the anchor has already moved meaningfully.

### 7.3. Trade mapping

- `MomentumCandidate("BACK", _)`: back now, offset lay x ticks lower.
- `MomentumCandidate("LAY", _)`: lay now, offset back x ticks higher.
- `ReversionCandidate(target, _)`: place a limit order toward the target.
- `LaggingReactor(_)`: trade the reactor when the anchor move has confirmed.

## 8. Risk management

- Limit exposure to one runner per market.
- Do not trade if the total field implied probability is unstable.
- Avoid trades when `VVI > 3m` or the selection has a long two-sided wick in Candle 3.
- Abort if the market spread widens beyond 2 ticks after signal generation.

## 9. Practical implementation notes

- Use this strategy in the final 10 minutes before off.
- Maintain the field state by classifying every selection rather than only the active candidate.
- Prefer the heaviest runner with sufficient TVPT over the fastest runner.
- Use anchor/reactor logic to spot second-wave opportunities in the field.

## 10. Summary

This merged OHLC strategy combines the best of both designs:

- the practical F# runner scoring and signal assignment from the Micro-Momentum implementation,
- the market-state anchor/reactor scanning and liquidity density metrics from the Gemini candlestick strategy.

It is intended to produce a market-aware x-tick trade engine that can score the entire book, detect field dependencies, and select the safest, most probable candidate.
