---
title: "Generalized OHLC Candle Span Strategy for Horse Racing"
type: research
status: active
tags: [trading-strategy, ohlc, horse-racing, market-state, fsharp, generalization]
---

# Generalized OHLC Candle Span Strategy for Horse Racing

This document generalizes the merged OHLC strategy so it can operate on a full list of candlesticks, not only the last three.

The goal is to enable experiments with different candle counts, spans, and lookback windows while preserving market-state scoring, liquidity filtering, and field-aware selection.

## 1. Why generalize beyond 3 candles?

Using only 3 candles is convenient, but it fixes the strategy to a single 15-minute lookback.

A generalized approach lets you:

- test 6, 9 or 12 candles,
- compare 1-minute, 3-minute, 5-minute or 10-minute candle spans,
- tune the lookback window to different market regimes,
- use the same signal engine for both short-term and medium-term bias.

## 2. Data model

```fsharp
type Candle = {
    Date: DateTime
    Open: decimal
    High: decimal
    Low: decimal
    Close: decimal
    Volume: decimal
}

type SignalType =
    | MomentumCandidate of direction: string * confidence: decimal
    | ReversionCandidate of targetPrice: decimal * divergence: decimal * confidence: decimal
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
    CandleStrength: decimal
    TVPT: decimal
    VVI: decimal
    TrendIntegrity: decimal
    ProbabilityShift: decimal
    SpreadTicks: int
    TimeStamp: DateTime
}

type SelectionHistory = {
    SelectionId: string
    SelectionName: string
    Candles: Candle list
    TotalVolume: decimal
    SpreadTicks: int
}
```

## 3. Generalized metrics

### 3.1. Implied probability utilities

```fsharp
let impliedProbability odds = 1m / odds
let probabilityRange candle = abs ((1m / candle.High) - (1m / candle.Low))
let probabilityDelta candle = abs ((1m / candle.Close) - (1m / candle.Open))
```

### 3.2. Trend and strength metrics

```fsharp
let cumulativeProbabilityDelta candles =
    candles
    |> List.map probabilityDelta
    |> List.sum

let trendDirection candles =
    let first = List.head candles
    let last = List.last candles
    if last.Close < first.Open then "BACK" else "LAY"

let momentumConsistency candles =
    candles
    |> List.map (fun c -> c.Close < c.Open)
    |> fun directions ->
        let count = directions |> List.filter id |> List.length
        decimal count / decimal directions.Length
```

### 3.3. Volume metrics

```fsharp
let volumeAcceleration candles =
    match List.rev candles with
    | current :: previous :: _ when previous.Volume > 0m ->
        (current.Volume - previous.Volume) / previous.Volume
    | _ -> 0m

let volumeDepth candles =
    let total = candles |> List.sumBy (fun c -> c.Volume)
    let current = candles |> List.last
    current.Volume / max 1m total
```

### 3.4. Liquidity and microstructure

```fsharp
let calculateTVPT candle =
    let probRange = probabilityRange candle
    if probRange = 0m then candle.Volume else candle.Volume / probRange

let calculateVVI candle =
    let body = abs (candle.Close - candle.Open)
    let wicks = (candle.High - candle.Low) - body
    if body = 0m then 10m else wicks / body
```

### 3.5. Trend integrity over all candles

```fsharp
let calculateTrendIntegrity candles =
    let probs = candles |> List.map (fun c -> 1m / c.Close)
    let ordered = probs |> List.windowed 3
    let consistent =
        ordered
        |> List.filter (fun [p1; p2; p3] -> (p1 < p2 && p2 < p3) || (p1 > p2 && p2 > p3))
        |> List.length
    decimal consistent / decimal (List.length ordered)
```

## 4. Field-state and anchor/reactor logic

### 4.1. Field anchor

The field anchor is the selection with the largest absolute probability shift across the window.

```fsharp
let totalProbabilityShift candles =
    let first = List.head candles
    let last = List.last candles
    abs ((1m / last.Close) - (1m / first.Open))

let identifyAnchor signals =
    signals |> Seq.maxBy (fun s -> s.ProbabilityShift)
```

### 4.2. Lagging reactor detection

A lagging reactor is a runner that remains neutral while the anchor has already moved.

```fsharp
let isLaggingReactor anchor signal =
    signal.SignalType = Neutral &&
    signal.VVI < 2m &&
    signal.TrendIntegrity > 0.4m &&
    signal.ProbabilityShift < anchor.ProbabilityShift * 0.7m
```

## 5. Generalized signal derivation

```fsharp
let deriveSelectionSignal history totalMarketVolume =
    let candles = history.Candles
    let direction = trendDirection candles
    let consistency = momentumConsistency candles
    let volumeDepth = volumeDepth candles
    let volumeAccel = volumeAcceleration candles
    let current = List.last candles
    let candleStrength =
        let body = probabilityDelta current
        let range = probabilityRange current
        if range > 0m then body / range else 0m
    let tvpt = calculateTVPT current
    let vvi = calculateVVI current
    let trendIntegrity = calculateTrendIntegrity candles
    let probShift = totalProbabilityShift candles
    let averageProbRange = candles |> List.averageBy probabilityRange

    let momentumQualified =
        probShift > 0.0008m &&
        consistency >= 0.66m &&
        volumeDepth > 0.2m &&
        volumeAccel > 0.2m &&
        candleStrength > 0.5m &&
        averageProbRange > 0m

    let reversionQualified =
        let wickRatio candle =
            let pOpen = 1m / candle.Open
            let pClose = 1m / candle.Close
            let pHigh = 1m / candle.High
            let pLow = 1m / candle.Low
            let bodyHigh = max pOpen pClose
            let bodyLow = min pOpen pClose
            let wickTop = max 0m (bodyHigh - pHigh)
            let wickBottom = max 0m (pLow - bodyLow)
            let body = abs (pClose - pOpen)
            if body = 0m then 10m else max wickTop wickBottom / body
        let last2 = candles |> List.rev |> List.take 2 |> List.rev
        match last2 with
        | [prev; current] ->
            let prevCloseProb = 1m / prev.Close
            let currentCloseProb = 1m / current.Close
            let prevprevCloseProb = 1m / (candles |> List.rev |> List.item 2).Close
            (wickRatio prev > 1.8m || wickRatio current > 2m) &&
            abs (currentCloseProb - prevCloseProb) < abs (prevCloseProb - prevprevCloseProb) &&
            current.Volume > prev.Volume * 1.2m
        | _ -> false

    let reversionConfidence =
        let wickScore =
            let prev = List.item (List.length candles - 2) candles
            let current = List.last candles
            max (wickRatio prev) (wickRatio current)
        let base = min 1m (probShift / 0.001m)
        let volumeScore = min 1m (volumeDepth + volumeAccel * 0.3m)
        let trendScore = trendIntegrity
        min 1m (base * 0.5m + volumeScore * 0.3m + trendScore * 0.2m)

    let signalType =
        if momentumQualified then
            MomentumCandidate(direction, min 1m (probShift / 0.001m))
        elif reversionQualified then
            ReversionCandidate((List.last candles).Close, probShift, reversionConfidence)
        else
            Neutral

    let baseSignal = {
        SelectionId = history.SelectionId
        SelectionName = history.SelectionName
        SignalType = signalType
        Score = 0m
        VolumeDepth = volumeDepth
        LiquidityShare = history.TotalVolume / max 1m totalMarketVolume
        TrendBias = direction
        CandleStrength = candleStrength
        TVPT = tvpt
        VVI = vvi
        TrendIntegrity = trendIntegrity
        ProbabilityShift = probShift
        SpreadTicks = history.SpreadTicks
        TimeStamp = DateTime.UtcNow
    }

    let score = calculateConvictionScore baseSignal
    { baseSignal with Score = score }
```

## 6. Scanning a generalized candle history

```fsharp
let scanField histories totalMarketVolume =
    let signals = histories |> List.map (fun h -> deriveSelectionSignal h totalMarketVolume)
    let anchor = identifyAnchor signals

    signals
    |> List.map (fun s ->
        match s.SignalType with
        | Neutral when isLaggingReactor anchor s ->
            { s with SignalType = LaggingReactor(anchor.SelectionId, abs s.ProbabilityShift) }
        | _ -> s)
```

## 7. Conviction scoring

```fsharp
let calculateConvictionScore signal =
    let trendScore = signal.TrendIntegrity * signal.CandleStrength
    let volumeScore = min 1m (signal.TVPT * signal.VolumeDepth)
    let liquidityScore = signal.LiquidityShare * (if signal.SpreadTicks <= 2 then 1m else 0.5m)
    let imbalanceScore = min 1m (signal.ProbabilityShift / 0.001m)

    (trendScore * 0.30m) +
    (volumeScore * 0.25m) +
    (liquidityScore * 0.20m) +
    (imbalanceScore * 0.25m)
```

type TradeAction =
    | Back of conviction: decimal
    | Lay of conviction: decimal
    | DoNothing

let mapSignalToAction signal =
    let conviction = calculateConvictionScore signal
    let minConviction = 0.35m
    let minMomentumConfidence = 0.15m
    let minReversionConfidence = 0.15m
    let minReversionDivergence = 0.0005m

    match signal.SignalType with
    | MomentumCandidate(direction, confidence)
        when conviction >= minConviction && confidence >= minMomentumConfidence ->
            if direction = "BACK" then Back conviction else Lay conviction

    | ReversionCandidate(_, divergence, confidence)
        when conviction >= minConviction && confidence >= minReversionConfidence && abs divergence >= minReversionDivergence ->
            if signal.TrendBias = "BACK" then Lay conviction else Back conviction

    | _ -> DoNothing

## 8. Execution guidelines

Trade only when:

- the spread is tight (`SpreadTicks <= 2`),
- `TVPT` is high enough for the runner,
- `TrendBias` is consistent across the chosen candle window,
- the runner is not `Neutral`,
- the target market has sufficient field liquidity.

## 9. Why this generalization helps

By processing a full candle list, you can:

- test different candle spans such as 1-minute, 2-minute, 5-minute or 10-minute bars,
- use 3, 5, 7, 10, or more candles to compare short-term vs medium-term patterns,
- validate whether the best signal comes from the last 3 candles or a longer window,
- maintain the same anchor/reactor field-state model across multiple timeframes.

## 10. Practical notes

- Start with 3-5 candles to validate the model quickly.
- Then expand to 8-12 candles to test more stable trend bias.
- Keep the field-state assignment intact so every runner still has a signal state.
- Ensure the result is still driven by probability-space metrics, not raw odds distance.

## 11. Summary

This generalized OHLC strategy keeps the original market-state logic while allowing dynamic candle count and span.
It is suitable for F# experimentation and backtesting across multiple lookback windows and candle resolutions.
