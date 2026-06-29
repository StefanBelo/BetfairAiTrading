---
title: "Detecting Fair Price and Syndicate Moves"
aliases: ["Fair Price Detection", "Syndicate Move Analysis"]
type: research
tags: [trading-analysis, market-efficiency, fsharp, price-history]
---

# Detecting Fair Price and Syndicate Moves

This research explores how to utilize `MarketSelectionsPriceHistoryData` to identify the "Fair Price" (market equilibrium) and detect "Syndicate Moves" (aggressive institutional capital) using F# for data processing.

## 1. Defining the Fair Price (Equilibrium)

The Fair Price is the price point where the collective "Wisdom of the Crowd" has found the most balance between Backers and Layers.

### Key Metrics:
*   **Volume Weighted Average Price (VWAP):** The true statistical center of gravity for a selection's history.
*   **Most Matched Price (Volume Node):** The specific price level where the highest amount of capital has been exchanged.

### F# Implementation:
```fsharp
type PricePoint = { Time: DateTime; Price: float; Volume: float }

module MarketEfficiency =
    
    /// Calculates the Volume Weighted Average Price (VWAP)
    let calculateVwap (points: seq<PricePoint>) =
        let totalVolume = points |> Seq.sumBy (fun p -> p.Volume)
        if totalVolume = 0.0 then 0.0
        else (points |> Seq.sumBy (fun p -> p.Price * p.Volume)) / totalVolume

    /// Identifies the Most Matched Price (Price Node)
    let getPriceNode (points: seq<PricePoint>) =
        points 
        |> Seq.groupBy (fun p -> p.Price)
        |> Seq.map (fun (price, group) -> price, group |> Seq.sumBy (fun p -> p.Volume))
        |> Seq.sortByDescending snd
        |> Seq.tryHead
        |> Option.map fst

    /// Checks if the current price is within the "Fair Zone" (e.g., 0.5% of VWAP)
    let isPriceInFairZone (tolerance: float) (points: seq<PricePoint>) =
        if Seq.isEmpty points then false
        else
            let current = (points |> Seq.last).Price
            let vwap = calculateVwap points
            abs(current - vwap) <= (vwap * tolerance)
```

## 2. Detecting Syndicate Moves (Aggressive Capital)

Syndicates and large-scale traders often move the market using "Sweeps" or "Bursts". These events represent high conviction and are indisputable because they involve matched volume, not just pending offers.

### Indicators:
*   **Price Sweeps:** Multiple price levels matched at the exact same timestamp. This indicates an order large enough to clear all liquidity at one price and hit the next.
*   **Volume Bursts:** A massive spike in volume at a single timestamp compared to the selection's average velocity.

### F# Implementation:
```fsharp
type TradeInitiator = BACKER | LAYER | UNKNOWN

type EventData = {
    Time: DateTime
    Prices: float[]
    Volume: float
}

type AggressiveData = {
    Base: EventData
    Multiplier: float
}

type SyndicateEvent = 
    | Sweep of EventData
    | Burst of AggressiveData
    | PowerfulMove of AggressiveData
    
    member this.Data = 
        match this with 
        | Sweep d -> d
        | Burst d | PowerfulMove d -> d.Base

    member this.Time = this.Data.Time
    member this.Prices = this.Data.Prices
    member this.Volume = this.Data.Volume
    member this.AvgPrice = this.Prices |> Array.average
    
    member this.Initiator =
        let p = this.Prices
        if p.Length < 2 then UNKNOWN
        else
            let first = p.[0]
            let minP = Array.min p
            let maxP = Array.max p
            // Compare which extreme price was furthest from the start
            if abs(first - minP) > abs(first - maxP) then BACKER // Majority pressure was DOWN (Steam)
            else LAYER // Majority pressure was UP (Drift)

module SyndicateDetection =

    /// Detects "Sweeps" where at least THREE prices are matched at the same time
    let detectSweeps (points: seq<PricePoint>) =
        points
        |> Seq.toArray // Eagerly convert for low-latency processing
        |> Array.groupBy (fun p -> p.Time)
        |> Array.filter (fun (_, group) -> (group |> Array.map (fun p -> p.Price) |> Array.distinct |> Array.length) > 2)
        |> Array.map (fun (time, group) -> 
            Sweep { 
                Base = {
                    Time = time
                    Prices = group |> Array.map (fun p -> p.Price)
                    Volume = group |> Array.sumBy (fun p -> p.Volume) 
                }
            })

    /// Detects "Bursts" relative to the ROLLING average volume
    let detectRollingBursts (windowSize: int) (thresholdMultiplier: float) (points: seq<PricePoint>) =
        let timeGroups = 
            points 
            |> Seq.groupBy (fun p -> p.Time) 
            |> Seq.map (fun (t, g) -> 
                t, 
                g |> Seq.map (fun p -> p.Price) |> Seq.toArray, 
                g |> Seq.sumBy (fun p -> p.Volume))
            |> Seq.toArray
            
        if timeGroups.Length < windowSize then Seq.empty
        else
            timeGroups
            |> Array.windowed windowSize
            |> Array.choose (fun window ->
                let (time, prices, volume) = window.[window.Length - 1]
                let avgHistory = window.[0 .. window.Length - 2] |> Array.averageBy (fun (_, _, v) -> v)
                
                if avgHistory > 0.0 && volume > (avgHistory * thresholdMultiplier) then
                    Some (Burst {
                        Base = { Time = time; Prices = prices; Volume = volume }
                        Multiplier = volume / avgHistory
                    })
                else None)

    /// Combines and merges concurrent sweeps and bursts into PowerfulMoves
    let detectAllEvents (points: seq<PricePoint>) =
        let sweeps = detectSweeps points
        let bursts = detectRollingBursts 10 3.0 points
        
        Seq.append sweeps bursts
        |> Seq.groupBy (fun e -> e.Time)
        |> Seq.map (fun (time, events) ->
            let sweep = events |> Seq.tryPick (function Sweep d -> Some d | _ -> None)
            let burst = events |> Seq.tryPick (function Burst d -> Some d | _ -> None)
            
            match sweep, burst with
            | Some s, Some b -> PowerfulMove { Base = s; Multiplier = b.Multiplier }
            | Some s, None   -> Sweep s
            | None, Some b   -> Burst b
            | _ -> failwith "Unexpected state")
        |> Seq.sortBy (fun e -> e.Time)
```

## 3. Practical Strategy Application

### Signal: The "Price Convergence"
When the **Current Price** is equal to the **Price Node** and **VWAP**, the market is at peak efficiency. Trading here requires a high-conviction "Value" edge.

### Signal: The "Syndicate Breakout"
If a **Sweep** occurs that moves the price away from the **Price Node**, it is a strong signal that a new trend is forming. 
*   **Steam Strategy:** Follow a **LAYER** sweep that clears **lower** prices (Aggressive Layering).
*   **Drift Strategy:** Follow a **BACKER** sweep that clears **higher** prices (Aggressive Backing).

## 4. Matching Engine Considerations

When analyzing 1-second aggregated history data, it is important to understand the underlying Betfair Matching Engine:

*   **Atomic Sequencing:** Betfair matches orders in discrete cycles (50-100ms) using a **FIFO (First-In, First-Out)** algorithm.
*   **Deterministic Sweeps:** A single large order that exceeds the volume at the best price will "sweep" through the book levels in strict logical sequence within a single cycle.
*   **Aggregation Proxy:** Even though our data is aggregated by second, a `Sweep` of 3+ prices in a single timestamp is a high-probability proxy for a single institutional move, as it is mathematically unlikely for independent retail participants to clear multiple levels of liquidity within the same 50ms matching window.

## 5. Trade Initiator Identification

By analyzing the price displacement within a `Sweep`, we can infer which side of the market was the aggressor:

*   **Aggressive Backing (BACKER):** The price reaches a **Min** further from the start than the **Max** (e.g., 1.84 -> 1.80 -> 1.86). The primary work was clearing Back offers (Steam).
*   **Aggressive Laying (LAYER):** The price reaches a **Max** further from the start than the **Min**. The primary work was clearing Lay offers (Drift).

This distinction is the key to differentiating between **Momentum (Follow the Initiator)** and **Mean Reversion (Initiator pushing price too far from Fair Value).**

## 6. Performance & Memory Management

For low-latency trading, F# performance is critical. Choosing the right collection type is the difference between catching a price move and missing it.

### Collection Comparison Matrix

| Feature | `Array` (`[\| ... \|]`) | `F# List` (`[ ... ]`) | `Sequence` (`seq`) |
| :--- | :--- | :--- | :--- |
| **Storage** | Contiguous memory | Linked nodes | Lazy Iterator |
| **Access** | $O(1)$ (Instant) | $O(N)$ (Walk nodes) | $O(N)$ (Re-calculate) |
| **GC Impact** | Low (Single block) | High (Per node) | Moderate (Per step) |
| **Iteration** | Fastest (Cache friendly) | Fast (Recursion) | Slow (Interface overhead) |
| **Mutation** | Mutable | Immutable | Immutable |

### Key Guidelines for Trading:
*   **Use Arrays for Signal Detection:** The `Array` module is your primary tool for windowed calculations (Moving Averages, VWAP, Sweeps).
*   **Use Lists for Strategy State:** Lists are ideal for managing immutable collections of active orders or strategy flags where you use head/tail pattern matching.
*   **Use `seq` for Inputs only:** Keep your function signatures flexible with `seq`, but convert to `Array` (via `Seq.toArray`) at the very first line of your logic.
## 7. Mathematical Foundations: Probability vs. Ticks

When implementing these algorithms, there is a fundamental choice between using **Price Ticks** or **Implied Probability**.

### The "Lumpy" Tick Problem
Betfair tick sizes jump at specific boundaries (e.g., from 0.05 to 0.10 at price 4.0). Using "Tick Difference" for logic creates "Blind Spots" where your strategy behaves differently just because a price crossed an arbitrary boundary.

### The "Smooth" Probability Solution
We use **Implied Probability ($1.0 / Price$)** for all decision logic.
*   **The Brain (Probability):** A 1.0% probability shift is equally significant at odds of 2.0 as it is at odds of 50.0. This ensures a consistent "Edge."
*   **The Execution (Ticks):** Ticks are used only at the final step to find the nearest valid Betfair price for order placement.

> [!TIP]
> Always perform your `abs(current - vwap)` checks in probability space to ensure your bot doesn't over-trade at long odds where ticks are "cheap."
