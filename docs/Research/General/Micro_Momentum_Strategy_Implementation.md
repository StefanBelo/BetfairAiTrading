---
title: "Micro-Momentum Strategy Implementation"
aliases: ["Syndicate Trading Strategy", "F# Scalping Strategy"]
type: research
tags: [trading-strategy, fsharp, market-microstructure, automation]
---

# Micro-Momentum Strategy Implementation

This document outlines a high-conviction trading strategy that integrates **Market Efficiency** anchors with **Syndicate Move** triggers. The goal is to identify short-term breakouts (Steam/Drift) or mean reversions with a "Skeptic Filter" to avoid market noise.

> [!NOTE]
> This strategy relies on the core detection algorithms defined in [Detecting Fair Price and Syndicate Moves](/docs/Research/General/Detecting_Fair_Price_and_Syndicate_Moves.md).

## 1. Strategy Signal Architecture

We categorize the market state into four primary signals based on the relationship between current price, historical anchors (VWAP/Node), and recent aggressive capital entry (Sweeps/Bursts).

```fsharp
type SignalType = 
    | MomentumBreakout of direction: string * conviction: float
    | MeanReversion of target: float * currentDistance: float
    | NodeRejection of nodePrice: float * volumeAbsorption: float
    | Neutral

type TradingSignal = {
    Type: SignalType
    Timestamp: DateTime
    SkepticFilterPassed: bool
    ImpactDensity: float // Volume matched per 1% of probability shift
    SentimentRatio: float // Long-term bias (-1.0 to 1.0)
    SignalPower: float // Ultimate ranking factor (Density * Dominance * Odds)
}

type TradeSide = BACK | LAY

type TradeAction = {
    Side: TradeSide
    Price: float
    Stake: float
    Label: string
    ExecutionType: string // "MARKET" or "LIMIT"
}
```

## 2. The Decision Matrix

The following logic defines how we evaluate the price history data to generate a signal.

```fsharp
module PatternEngine =
    /// Analyzes a sequence of events to find institutional signatures
    /// allEvents: Full history (1 hour) | windowEvents: Recent activity (1 min)
    let evaluate (allEvents: SyndicateEvent[]) (windowEvents: SyndicateEvent[]) (currentPrice: float) =
        // 1. Long-Term Sentiment Memory (SentimentRatio)
        let totalBackVol = allEvents |> Array.filter (fun e -> e.Initiator = BACKER) |> Array.sumBy (fun e -> e.Volume)
        let totalLayVol = allEvents |> Array.filter (fun e -> e.Initiator = LAYER) |> Array.sumBy (fun e -> e.Volume)
        let sentiment = if (totalBackVol + totalLayVol) > 0.0 then (totalBackVol - totalLayVol) / (totalBackVol + totalLayVol) else 0.0

        if windowEvents.Length < 3 then None 
        else
            let initiators = windowEvents |> Array.map (fun e -> e.Initiator)
            let volumes = windowEvents |> Array.map (fun e -> e.Volume)
            let totalVol = Array.sum volumes
            
            // Calculate Total Path-Integrated Probability Shift (Market Friction)
            let totalFriction = 
                windowEvents |> Array.sumBy (fun e -> 
                    let sP = 1.0 / e.Prices.[0]
                    let eP = 1.0 / e.Prices.[e.Prices.Length - 1]
                    abs(sP - eP))
            
            let mid = if totalFriction > 0.001 then totalVol / (totalFriction * 100.0) else 0.0

            let backCount = initiators |> Array.filter ((=) BACKER) |> Array.length
            let layCount = initiators |> Array.filter ((=) LAYER) |> Array.length
            let total = windowEvents.Length
            
            let lastVol = Array.last volumes
            let avgPrevVol = volumes |> Array.take (volumes.Length - 1) |> Array.average
            let isEscalating = lastVol > avgPrevVol * 1.2
            let dominance = float (backCount - layCount) / float total

            // Calculate SignalPower: Dominance * (MID/1000) * OddsMultiplier
            let oddsMultiplier = Math.Log(currentPrice) / Math.Log(2.0)
            let signalPower = abs(dominance) * (mid / 1000.0) * oddsMultiplier

            if dominance > 0.5 then 
                let conviction = if isEscalating then 1.0 else dominance
                Some { Type = MomentumBreakout("BACK", conviction); Timestamp = (Array.last windowEvents).Time; SkepticFilterPassed = true; ImpactDensity = mid; SentimentRatio = sentiment; SignalPower = signalPower }
            elif dominance < -0.5 then 
                let conviction = if isEscalating then 1.0 else abs(dominance)
                Some { Type = MomentumBreakout("LAY", conviction); Timestamp = (Array.last windowEvents).Time; SkepticFilterPassed = true; ImpactDensity = mid; SentimentRatio = sentiment; SignalPower = signalPower }
            else None

module StrategyEngine =

    let evaluate (points: seq<PricePoint>) =
        let arr = points |> Seq.toArray
        if arr.Length < 20 then { Type = Neutral; Timestamp = DateTime.Now; SkepticFilterPassed = false }
        else
            let current = arr |> Array.last
            let vwap = MarketEfficiency.calculateVwap arr
            let node = MarketEfficiency.getPriceNode arr |> Option.defaultValue vwap
            
            let currentProb = 1.0 / current.Price
            let vwapProb = 1.0 / vwap
            let nodeProb = 1.0 / node
            
            // 1. Detect Syndicate Activity (Analyze the latest 60 seconds of data)
            let latestTime = current.Time
            let allEvents = SyndicateDetection.detectAllEvents arr |> Seq.toArray
            let windowEvents = allEvents |> Array.filter (fun e -> e.Time > latestTime.AddSeconds(-60.0))
            
            // 2. Pattern Analysis (High Conviction Sequence Check with Sentiment Memory)
            let patternResult = PatternEngine.evaluate allEvents windowEvents current.Price
            
            match patternResult with
            | Some signal -> signal 
            | None ->
                // Fallback to Single-Event Analysis
                let latestEvent = windowEvents |> Array.tryLast
                
                // Calculate Sentiment for Fallback signals
                let totalBackVol = allEvents |> Array.filter (fun e -> e.Initiator = BACKER) |> Array.sumBy (fun e -> e.Volume)
                let totalLayVol = allEvents |> Array.filter (fun e -> e.Initiator = LAYER) |> Array.sumBy (fun e -> e.Volume)
                let sentiment = if (totalBackVol + totalLayVol) > 0.0 then (totalBackVol - totalLayVol) / (totalBackVol + totalLayVol) else 0.0

                // Skeptic Filter: Volume Velocity (Recent vs Baseline)
                let sixtySecWindow = arr |> Array.filter (fun p -> p.Time > latestTime.AddSeconds(-60.0))
                let avgVolPerSec = (sixtySecWindow |> Array.sumBy (fun p -> p.Volume)) / 60.0
                let lastSecVol = sixtySecWindow |> Array.filter (fun p -> p.Time > latestTime.AddSeconds(-1.0)) |> Array.sumBy (fun p -> p.Volume)
                let isVolSpike = lastSecVol > (avgVolPerSec * 3.0)

                match latestEvent with
                | Some(event) ->
                    let initiator = event.Initiator
                    
                    match event with
                    | PowerfulMove d -> 
                        let conviction = (min 1.0 (float d.Base.Prices.Length / 5.0) + min 1.0 (d.Multiplier / 10.0)) / 2.0
                        let direction = if initiator = BACKER then "BACK" else "LAY"
                        { Type = MomentumBreakout(direction, conviction); Timestamp = event.Time; SkepticFilterPassed = true; ImpactDensity = 0.0; SentimentRatio = sentiment; SignalPower = 0.0 }
                        
                    | Sweep d -> 
                        let conviction = min 1.0 (float d.Prices.Length / 5.0)
                        let direction = if initiator = BACKER then "BACK" else "LAY"
                        { Type = MomentumBreakout(direction, conviction); Timestamp = event.Time; SkepticFilterPassed = isVolSpike; ImpactDensity = 0.0; SentimentRatio = sentiment; SignalPower = 0.0 }
                        
                    | Burst _ when abs((1.0/event.AvgPrice) - nodeProb) < 0.005 -> 
                        { Type = NodeRejection(node, event.Volume); Timestamp = event.Time; SkepticFilterPassed = true; ImpactDensity = 0.0; SentimentRatio = sentiment; SignalPower = 0.0 }
                        
                    | _ -> { Type = Neutral; Timestamp = DateTime.Now; SkepticFilterPassed = false; ImpactDensity = 0.0; SentimentRatio = sentiment; SignalPower = 0.0 }
                    
                | None when abs(currentProb - vwapProb) > 0.01 ->
                    { Type = MeanReversion(vwap, abs(currentProb - vwapProb)); Timestamp = DateTime.Now; SkepticFilterPassed = true; ImpactDensity = 0.0; SentimentRatio = sentiment; SignalPower = 0.0 }
                    
                | _ -> { Type = Neutral; Timestamp = DateTime.Now; SkepticFilterPassed = false; ImpactDensity = 0.0; SentimentRatio = sentiment; SignalPower = 0.0 }
                
            | _ -> { Type = Neutral; Timestamp = DateTime.Now; SkepticFilterPassed = false; ImpactDensity = 0.0; SentimentRatio = 0.0; SignalPower = 0.0 }
```

## 3. Strategy Descriptions

### A. Momentum Breakout (The Syndicate Slipstream)
*   **Condition:** A `PowerfulMove` or `Sweep` occurs.
*   **Steam (Price Down):** Driven by an aggressive **BACKER** who pushes the price to a new **Low** (Min Displacement). Signal to **BACK**.
*   **Drift (Price Up):** Driven by an aggressive **LAYER** who pushes the price to a new **High** (Max Displacement). Signal to **LAY**.
*   **Rationale:** Aggressive capital is moving the market. Follow the move for a quick 2-3 tick profit.
*   **Exit:** Exit if a counter-sweep occurs or if price reverts to VWAP.

### B. Mean Reversion (The Anchor Pull)
*   **Condition:** Price is $>X$ ticks from VWAP with **Zero** syndicate activity.
*   **Rationale:** The move is driven by retail "noise" or thin liquidity. Expect the "Magnet" effect of the VWAP to pull it back.
*   **Exit:** Exit exactly at the VWAP level.

### C. Node Rejection (The Absorption Wall)
*   **Condition:** Price hits a major Volume Node, volume spikes, but price does not break through.
*   **Rationale:** Large pending orders are "absorbing" the market pressure. Trade the bounce.

## 4. Skeptic Filter (Risk Management)
The `SkepticFilterPassed` flag is critical. It should only be `true` when:
1.  **Matched Volume** at the signal timestamp is $>300\%$ of the 5-minute rolling average.
2.  The **Spread** (if available) is tight (1-2 ticks).
3.  The signal occurs within the **High-Activity Window** (e.g., 10 minutes before the "off").

---

## F# Integration Example

---

## 5. Selection Ranking and Market Scanning

In a multi-runner market, we must decide not just *how* to trade, but *where* the highest conviction lies. We use a weighted scoring model to rank selections in real-time.

### Ranking Dimensions:
1.  **Aggression (50%):** Frequency and volume of recent `Sweeps` and `Bursts`.
2.  **Liquidity (30%):** The selection's share of total matched volume (higher is safer).
3.  **Efficiency Gap (20%):** The distance between the current price and the Fair Price anchors.

### F# MarketScanner Implementation:

```fsharp
type SelectionMetrics = {
    SelectionId: string
    AggressionScore: float
    LiquidityScore: float
    EfficiencyGap: float
}

module MarketScanner =
    
    /// Calculates a weighted score to rank the best selection to trade
    /// Uses normalization to ensure all metrics are on a comparable 0.0-1.0 scale
    let calculateConvictionScore (metrics: SelectionMetrics) =
        // Normalize Aggression (Assume 5+ events in 2m is peak intensity)
        let normAggression = min 1.0 (metrics.AggressionScore / 5.0)
        
        // Normalize Gap (Assume 10% probability gap is peak divergence)
        let normGap = min 1.0 (metrics.EfficiencyGap / 0.10)
        
        // Weighted Sum: Aggression (50%) | Liquidity (30%) | Gap (20%)
        (normAggression * 0.5) + 
        (metrics.LiquidityScore * 0.3) + 
        (normGap * 0.2)

    /// Converts raw history into comparable metrics for scanning
    let deriveMetrics (id: string) (points: seq<PricePoint>) (totalMarketMatched: float) =
        if Seq.length points < 20 then None
        else
            let current = points |> Seq.last
            let node = MarketEfficiency.getPriceNode points |> Option.defaultValue current.Price
            
            // Count all syndicate events in the last 2 minutes
            let latestTime = current.Time
            let aggression = 
                SyndicateDetection.detectAllEvents points 
                |> Seq.filter (fun e -> e.Time > latestTime.AddMinutes(-2.0))
                |> Seq.length |> float
                
            let liquidity = (points |> Seq.sumBy (fun p -> p.Volume)) / totalMarketMatched
            let gap = abs((1.0 / current.Price) - (1.0 / node))
            
            Some { SelectionId = id; AggressionScore = aggression; LiquidityScore = liquidity; EfficiencyGap = gap }

    /// Scans all active selections and returns the one with the highest trading potential
    let findPrimeTarget (selections: seq<SelectionMetrics>) =
        selections
        |> Seq.sortByDescending calculateConvictionScore
        |> Seq.tryHead
```

## Summary of the "Golden Rule" for Selection
Always prioritize the **Primary Mover**. If the Favorite is drifting slowly but the 3rd Favorite just had a 500 EUR `Sweep` breakout, the 3rd Favorite is your trading target. Aggression from syndicates overrides historical weight.

## 4. The Execution Pipeline

The trading bot operates as a **Funnel**, reducing thousands of data points into a single binary decision.

### Stage 1: The Broad Scan (Selection)
The `MarketScanner` runs on every runner in the market. It calculates a **Conviction Score** based on:
*   **Distance from Fair Price:** Is the probability mispriced?
*   **Aggression Density:** How many Syndicate Events in the last 2 minutes?
*   **Liquidity:** Is there enough volume to enter and exit without slippage?

**Gatekeeper Rule:** Only runners with a `ConvictionScore > 0.7` are passed to the next stage.

### Stage 2: The Deep Scan (Timing)
The `StrategyEngine` is called only for high-conviction runners. It performs the **Microstructure Analysis**:
1.  **Event Merging:** Identifying `PowerfulMove` vs `Sweep`.
2.  **Initiator Detection:** Determining if the aggressor is a BACKER or LAYER (Steam vs Drift).
3.  **The Skeptic Filter:** Validating volume velocity to ensure the move isn't a "fake-out".

### Stage 3: Execution (Bfexplorer Action)
The `SignalType` is mapped to a `TradeAction` which determines the side, price, and urgency:

*   **Momentum Signal** -> `BACK` or `LAY` in the direction of the sweep. Execution: **Market/Aggressive.**
*   **Mean Reversion** -> Trade toward the VWAP. Execution: **Passive Limit.**
*   **Node Rejection** -> Trade against the approach at the node. Execution: **Passive Limit.**

## 5. Trade Mapping Logic

```fsharp
module TradeMapper =
    /// Maps a high-level signal to a concrete exchange action
    let mapSignalToAction (signal: TradingSignal) currentPrice baseStake =
        let sentiment = signal.SentimentRatio
        let density = signal.ImpactDensity
        
        match signal.Type with
        | MomentumBreakout(dir, conviction) -> 
            // 1. Quality Filter: Is the move "Heavy" or "Thin"?
            if density < 100.0 then None // Discard "Thin Air" moves
            else
                // 2. Conflict Filter: Is the timing move fighting the 1-hour trend?
                let isContrarian = (dir = "BACK" && sentiment < -0.4) || (dir = "LAY" && sentiment > 0.4)
                
                if isContrarian then None 
                else
                    // 3. Stake Scaling: Reward agreement between Timing and Sentiment
                    let alignmentBonus = 
                        if (dir = "BACK" && sentiment > 0.5) || (dir = "LAY" && sentiment < -0.5) then 1.5 
                        else 1.0
                    
                    Some { 
                        Side = if dir = "BACK" then BACK else LAY
                        Price = currentPrice
                        Stake = baseStake * conviction * alignmentBonus
                        Label = sprintf "MOMENTUM_%s_MID_%f" dir density
                        // 4. Execution Urgency: Use MARKET for "Walls" (>1000), LIMIT for solid moves
                        ExecutionType = if density > 1000.0 then "MARKET" else "LIMIT" 
                    }

        | MeanReversion(target, _) ->
            Some { Side = if currentPrice < target then BACK else LAY
                   Price = target
                   Stake = baseStake
                   Label = "VWAP_REVERSION"
                   ExecutionType = "LIMIT" }

        | NodeRejection(node, _) ->
            Some { Side = if currentPrice < node then LAY else BACK
                   Price = node
                   Stake = baseStake
                   Label = "NODE_BOUNCE"
                   ExecutionType = "LIMIT" }
        
        | Neutral -> None
```

## 6. Summary of Conviction Logic

| Component | Responsibility | Output |
| :--- | :--- | :--- |
| **MarketScanner** | Selection (The "Who") | ConvictionScore |
| **StrategyEngine** | Timing (The "When") | SignalType |
| **SkepticFilter** | Risk (The "If") | Boolean |

### 6. Risk Management: The "Double-Gate" Trigger

In live execution, we recommend a **Dual-Gate** approach before any order is sent to the exchange:

| Filter | Logic | Objective |
| :--- | :--- | :--- |
| **1. Skeptic Filter** | `SkepticFilterPassed = true` | **Validation:** Volume velocity check. |
| **2. Conviction Gate** | `Conviction > 0.5` | **Intensity:** Directional majority check. |
| **3. Density Rank** | `ImpactDensity > 500` | **Quality:** Real intent vs. Thin air. |

#### Impact Density (MID) Benchmarks:
Based on real-world testing (e.g., Wolverhampton/Ripon), we use the following scale for ranking:
*   **MID > 1000 (Extreme):** Institutional "Brick Wall." Extremely high conviction.
*   **MID 500 - 1000 (Heavy):** Strong Syndicate signatures.
*   **MID 100 - 500 (Solid):** Normal professional market activity.
*   **MID < 100 (Thin):** High risk. Likely retail noise or liquidity gaps.

## 7. Strategic Modes: Trading vs. Betting

The signals generated by this strategy are "Agnostic"—they work for both high-frequency scalping and traditional value betting.

### Mode A: The Scalper (Trading)
*   **Objective:** Extract 2-3 ticks of profit and exit.
*   **Logic:** Uses the `VWAP` or `Price Node` as the **Exit Target**.
*   **Risk:** Low risk per trade, high turnover.

### Mode B: The Value Hunter (Betting)
*   **Objective:** Back a horse because the price is "Wrong."
*   **Logic:** Uses the `MomentumBreakout` or `MeanReversion` signal as a **Buy Trigger**.
*   **Exit:** Hold until the "Off" (start of race) or the finish line.
*   **Edge:** Following "Smart Money" (Syndicates) into the race.

> [!IMPORTANT]
> The `TradeMapper` should be configured with a `Mode` flag to determine whether to attach a "Take Profit" (Offset) order or simply let the position run for value.
