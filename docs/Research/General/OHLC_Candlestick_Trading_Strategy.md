---
title: "OHLC Candlestick Trading Strategy: 15-Minute Microstructure Analysis"
type: research
status: active
tags: [trading-strategy, ohlc, horse-racing, market-efficiency, fsharp]
---

# OHLC Candlestick Trading Strategy

This document outlines a lightweight algorithmic approach to Betfair trading using the last three 5-minute candlesticks (15 minutes of history). It focuses on identifying short-term momentum (x-tick scalping) and cross-runner dependencies using OHLC (Open, High, Low, Close) data.

---

## 1. Data Model: The Quote Structure

The strategy consumes data in a standard candlestick format. For each selection, we maintain a list of `Quote` records.

```fsharp
type Quote = {
    Date: DateTime
    Open: float
    High: float
    Low: float
    Close: float
    Volume: float
}
```

---

## 2. Core Trading Strategies

### A. Trend Following (The "Steamers" & "Drifters")
*   **Logic:** Detects directional momentum across the 15-minute window.
*   **Signal:** If `Close` of Candle 3 is significantly lower than `Open` of Candle 1 (Steam) or higher (Drift).
*   **Action:** 
    *   **Steam:** BACK at current price → Offset LAY $x$ ticks lower.
    *   **Drift:** LAY at current price → Offset BACK $x$ ticks higher.

### B. Mean Reversion (The "Anchor Pull")
*   **Logic:** Identifies overextensions where the price has spiked but failed to hold.
*   **Signal:** Candle 3 shows a long "wick" (High - Close is large for a drift, or Close - Low is large for a steam) that retreats toward the average of Candles 1 and 2.
*   **Action:** Trade toward the statistical mean of the previous 10 minutes.

### C. Support & Resistance (The Floor/Ceiling)
*   **Logic:** Identifies price levels where the market has "bounced" multiple times.
*   **Signal:** The `Low` (or `High`) of all three candles is within a tight tolerance (e.g., < 0.5% difference).
*   **Action:** Trigger a trade on a breakout below the "floor" or rejection at the "ceiling."

---

## 3. The "Safest" Candidate Selection (Liquidity Filters)

To accommodate a trade of $x$ ticks without slippage, the algorithm must pass three safety gates:

1.  **Traded Volume Ratio:** Selection Volume must be $>25\%$ of Total Market Volume.
2.  **Minimum Depth:** Candle 3 must have a minimum matched volume (e.g., > £5,000).
3.  **Traded Volume per Tick (TVPT):**
    $$\text{TVPT} = \frac{\text{Volume of Candle 3}}{\text{High of Candle 3} - \text{Low of Candle 3}}$$
    *   **High TVPT:** Indicates massive depth and "Safe Havens" for large stakes.

---

## 4. Cross-Sectional Interdependency (The Field Trend)

Horse racing is a **Closed Loop** market. The sum of implied probabilities ($\sum \frac{1}{\text{Price}}$) must remain close to 100% (plus overround).

### The "Lead Horse" vs. "Reactors"
*   **The Anchor:** Usually the favorite. If the Anchor steams (price drops), it sucks probability out of the market.
*   **The Reactors:** Other horses (2nd/3rd favorites) must drift to compensate.
*   **The Opportunity (The Lagging Play):** If the Anchor has already moved 5% in probability space but the Reactor's OHLC hasn't shifted yet, the Reactor is the most profitable candidate for a corrective trade.

---

## 5. Algorithmic Guardrails

| Rule Type | Logic | Purpose |
| :--- | :--- | :--- |
| **Volume Filter** | `Vol(C3) > Vol(C1) * 1.5` | Ensures move is backed by real money velocity. |
| **Volatility Guard** | `(High - Low) / Price > 0.15` | Disqualify "erratic" horses with dangerous spreads. |
| **Spread Gate** | `Gap <= 1 Tick` | Essential for scalping/x-tick safety. |

---

## 6. F# Implementation Logic: The Field-Level Scanner

## 6. F# Implementation Logic: The Field-Level Scanner

```fsharp
module OHLCStrategy =

    type SignalType = 
        | MomentumBreakout of direction: string * conviction: float
        | LaggingReactor of targetId: string * divergence: float
        | Neutral

    type TradingSignal = {
        Type: SignalType
        Timestamp: DateTime
        SkepticFilterPassed: bool
        TVPT: float // Traded Volume per Tick (Liquidity Density)
        TrendIntegrity: float // Consistency of the move (0.0 to 1.0)
        SignalPower: float // Ranking factor: Divergence * TVPT
    }

    type SelectionAnalysis = {
        Id: string
        CurrentPrice: float
        ProbShift: float
        TVPT: float
        VVI: float 
        TrendIntegrity: float 
        IsDoji: bool
        LastTimestamp: DateTime
    }

    /// Calculates the Path of Least Resistance (Traded Volume per Tick)
    let calculateTVPT (q: Quote) =
        let range = q.High - q.Low
        if range = 0.0 then q.Volume else q.Volume / range

    /// Calculates VVI: High wicks vs Small body = High Indecision
    let calculateVVI (q: Quote) =
        let body = abs(q.Close - q.Open)
        let wicks = (q.High - q.Low) - body
        if body = 0.0 then 10.0 else wicks / body

    /// Calculates monotonicity across X candles (0.0 to 1.0)
    let calculateTrendIntegrity (quotes: Quote list) =
        if quotes.Length < 2 then 1.0
        else
            let probs = quotes |> List.map (fun q -> 1.0 / q.Close)
            let first, last = List.head probs, List.last probs
            let isSteaming = last > first
            
            let directionalSteps = 
                probs 
                |> List.pairwise 
                |> List.filter (fun (p1, p2) -> if isSteaming then p2 >= p1 else p2 <= p1)
                |> List.length
            
            float directionalSteps / float (quotes.Length - 1)

    /// Scans the field and returns a map of actionable TradingSignals
    let scanField (windowSize: int) (fieldData: Map<string, Quote list>) : Map<string, TradingSignal> =
        let analyses = 
            fieldData 
            |> Map.toList
            |> List.choose (fun (id, quotes) ->
                // Extract the last X candles
                let window = 
                    quotes 
                    |> List.sortByDescending (fun q -> q.Date) 
                    |> List.truncate windowSize 
                    |> List.rev
                
                if window.Length >= windowSize then
                    let cFirst, cLast = List.head window, List.last window
                    let probShift = (1.0 / cLast.Close) - (1.0 / cFirst.Open)
                    let integrity = calculateTrendIntegrity window
                    Some { 
                        Id = id
                        CurrentPrice = cLast.Close
                        ProbShift = probShift
                        TVPT = calculateTVPT cLast
                        VVI = calculateVVI cLast
                        TrendIntegrity = integrity
                        IsDoji = abs(cLast.Open - cLast.Close) < ((cLast.High - cLast.Low) * 0.1)
                        LastTimestamp = cLast.Date
                    }
                | _ -> None)

        // 1. Identify the Market Anchor (The Primary Driver)
        let anchor = analyses |> List.sortByDescending (fun a -> abs a.ProbShift) |> List.tryHead
        
        match anchor with
        | Some a when abs a.ProbShift > 0.01 -> // Anchor threshold: 1% prob shift
            let expectedFieldReaction = -a.ProbShift
            
            analyses 
            |> List.map (fun c ->
                let signalType = 
                    if c.Id = a.Id then 
                        let dir = if c.ProbShift > 0.0 then "BACK" else "LAY"
                        MomentumBreakout(dir, c.TrendIntegrity)
                    elif not c.IsDoji && c.VVI < 2.0 then
                        let divergence = abs(expectedFieldReaction - c.ProbShift)
                        LaggingReactor(a.Id, divergence)
                    else 
                        Neutral

                let power = 
                    match signalType with
                    | MomentumBreakout(_, conv) -> c.TVPT * conv
                    | LaggingReactor(_, div) -> div * c.TVPT
                    | _ -> 0.0

                let signal = {
                    Type = signalType
                    Timestamp = c.LastTimestamp
                    SkepticFilterPassed = c.TrendIntegrity > 0.5
                    TVPT = c.TVPT
                    TrendIntegrity = c.TrendIntegrity
                    SignalPower = power
                }
                (c.Id, signal))
            |> Map.ofList
        | _ -> 
            // Entire field is Neutral/Consolidating
            analyses |> List.map (fun c -> 
                (c.Id, { Type = Neutral; Timestamp = c.LastTimestamp; SkepticFilterPassed = false; TVPT = c.TVPT; TrendIntegrity = c.TrendIntegrity; SignalPower = 0.0 }))
            |> Map.ofList
```

---

## 7. Summary: Execution Strategy

For **x-tick trading**, the fastest moving horse is rarely the best choice. The algorithm prioritizes the **Heaviest** horse (highest TVPT) because it allows the bot to "hide" its exit in the massive flow of matched orders, ensuring the $x$ ticks are captured with minimal risk of a "gap-down."

> [!TIP]
> Always monitor the **Total Book Percentage**. If the sum of 1/Price for the field jumps significantly over 3 candles, the market is becoming illiquid/unbalanced—abort all active trades.
