---
title: "Price History Analysis (F#)"
aliases: ["Price History Analysis in F#"]
type: research
status: active
tags: [fsharp, price-history, data-processing, automation]
---

# Price History Analysis using F#

This note summarizes how to detect a fair-price zone and syndicate-like moves from `PricePoint` history data, plus an F# example you can adapt for Betfair or Polymarket-style markets.

## Idea

A practical approach is to combine rolling VWAP, volatility, and volume surge logic. That gives you a fair-price proxy and a way to flag unusual momentum without pretending you can prove syndicate activity with certainty. On Betfair, traded volume and price history are especially useful for this kind of microstructure analysis; on Polymarket, midpoint and order-book dynamics matter more.

## Data shape

```fsharp
open System

type PricePoint = {
    Time: DateTime
    Price: float
    Volume: float
}

type Signal = {
    Time: DateTime
    FairPrice: float
    FairDistance: float
    VolumeZ: float
    MoveScore: float
    IsFairZone: bool
    IsSyndicateLikeMove: bool
}
```

## Rolling VWAP fair price

A simple fair price proxy is the rolling VWAP over a window. If the current price stays near VWAP and the market is quiet, that often looks like fair value.

```fsharp
let rollingVwap (windowSize:int) (points: PricePoint list) =
    points
    |> List.sortBy _.Time
    |> List.mapi (fun i p ->
        let start = max 0 (i - windowSize + 1)
        let window = points |> List.skip start |> List.take (i - start + 1)
        let totalVol = window |> List.sumBy _.Volume
        let vwap =
            if totalVol = 0.0 then p.Price
            else window |> List.sumBy (fun x -> x.Price * x.Volume) / totalVol
        p, vwap)
```

## Rolling volatility and volume z-score

This helps separate quiet fair periods from aggressive moves. A syndicate-like move often shows both price acceleration and abnormal volume.

```fsharp
let mean xs = xs |> List.average
let stdDev xs =
    let m = mean xs
    sqrt (xs |> List.averageBy (fun x -> (x - m) ** 2.0))

let rollingStats windowSize (points: PricePoint list) =
    points
    |> List.sortBy _.Time
    |> List.mapi (fun i p ->
        let start = max 0 (i - windowSize + 1)
        let window = points |> List.skip start |> List.take (i - start + 1)
        let prices = window |> List.map _.Price
        let vols = window |> List.map _.Volume
        let pMean = mean prices
        let pStd = if prices.Length > 1 then stdDev prices else 0.0
        let vMean = mean vols
        let vStd = if vols.Length > 1 then stdDev vols else 0.0
        let volumeZ =
            if vStd = 0.0 then 0.0 else (p.Volume - vMean) / vStd
        p, pMean, pStd, volumeZ)
```

## Fair-zone and syndicate flags

This example flags a fair zone when price is close to VWAP and volatility is low, and flags a syndicate-like move when distance, volume, and momentum are all high.

```fsharp
let detectSignals (windowSize:int) (points: PricePoint list) =
    let pts = points |> List.sortBy _.Time

    pts
    |> List.mapi (fun i p ->
        let start = max 0 (i - windowSize + 1)
        let window = pts |> List.skip start |> List.take (i - start + 1)

        let totalVol = window |> List.sumBy _.Volume
        let fairPrice =
            if totalVol = 0.0 then p.Price
            else window |> List.sumBy (fun x -> x.Price * x.Volume) / totalVol

        let prices = window |> List.map _.Price
        let vols = window |> List.map _.Volume
        let priceStd = if prices.Length > 1 then stdDev prices else 0.0
        let volMean = mean vols
        let volStd = if vols.Length > 1 then stdDev vols else 0.0

        let fairDistance =
            if fairPrice = 0.0 then 0.0 else abs (p.Price - fairPrice) / fairPrice

        let volumeZ =
            if volStd = 0.0 then 0.0 else (p.Volume - volMean) / volStd

        let momentum =
            if i = 0 then 0.0 else p.Price - pts.[i-1].Price

        let moveScore =
            0.6 * fairDistance +
            0.25 * max 0.0 volumeZ +
            0.15 * abs momentum

        let isFairZone =
            fairDistance < 0.005 && priceStd < (fairPrice * 0.002)

        let isSyndicateLikeMove =
            fairDistance > 0.01 && volumeZ > 1.5 && abs momentum > (fairPrice * 0.002)

        {
            Time = p.Time
            FairPrice = fairPrice
            FairDistance = fairDistance
            VolumeZ = volumeZ
            MoveScore = moveScore
            IsFairZone = isFairZone
            IsSyndicateLikeMove = isSyndicateLikeMove
        })
```

## Example usage

```fsharp
let sample =
    [
        { Time = DateTime(2026,5,5,12,0,0); Price = 2.00; Volume = 100.0 }
        { Time = DateTime(2026,5,5,12,1,0); Price = 2.01; Volume = 110.0 }
        { Time = DateTime(2026,5,5,12,2,0); Price = 2.00; Volume = 95.0 }
        { Time = DateTime(2026,5,5,12,3,0); Price = 1.96; Volume = 400.0 }
        { Time = DateTime(2026,5,5,12,4,0); Price = 1.94; Volume = 500.0 }
    ]

let signals = detectSignals 3 sample
```

## Notes

For Betfair, this works well because traded volume and price history are strong signals. For Polymarket, you’d usually add midpoint/spread logic and order-book depth because the visible price is tied more directly to the book.

A stronger version would add time decay, price-compression detection, clustering around levels, and backtesting labels so you can measure whether a move tends to continue or revert.
