# The Expert Horse Racing Analyst + LBBW Implementation Guide

**Target Audience:** Software developers implementing horse racing analysis logic  
**Target Language:** F# (preferred), but adaptable to any functional or OOP language  
**Date:** 2025-12-22  
**Live Market Snapshot:** Wolverhampton 7f Nov Stks (Market ID: 1.251904979)

---

## Executive Summary

This document merges two analytical approaches into a unified implementation guide:

1. **The Expert Horse Racing Analyst** – A comprehensive, data-driven workflow leveraging multiple data sources (historical form, Racing Post comments, Timeform flags, jockey stats).
2. **LBBW v2 Model** – A quantitative framework scoring horses on Last (recency), Best (peak), Base (consistency), and Weight (handicap impact).

The merged approach provides:
- **Composite scoring** combining both methodologies
- **Value identification** through data-driven implied odds vs market odds
- **Actionable betting recommendations** with confidence levels

---

## 1. Data Acquisition & Market Context

### 1.1 Required API Calls

Execute these calls in sequence to gather all necessary data:

```fsharp
// Step 1: Get active market
let market = GetActiveMarket()
// Returns: { marketId, startTime, eventType, eventName, marketName, status, selections[] }

// Step 2: Get bulk data contexts for all selections
let dataContexts = GetAllDataContextForMarket(
    market.marketId,
    [
        "DbHorsesResults";      // Historical race results for each horse
        "DbJockeysResults";     // Historical jockey performance
        "RacingpostDataForHorses"; // Race comments, ratings, descriptions
        "TimeformDataForHorses"    // Binary flags (form, course/distance suitability)
    ]
)
```

### 1.2 Live Market Example (Wolverhampton 7f Nov Stks)

**Market Details:**
- **Market ID:** 1.251904979
- **Start Time:** 2025-12-22T16:30:00Z
- **Course:** Wolverhampton (7f Nov Stks)
- **Runners:** 10 horses

**Sample Selections:**
```
Free Your Spirit      - 3.65
Sunshine And Roses    - 4.4
Galileo Blue          - 6.2
Meritocratic          - 8.4
Okiru                 - 8.4
Baggie Boy            - 19
Hamish Leek           - 46
Backfire              - 75
Telling Time          - 95
Sunday Sinatra        - 200
```

---

## 2. Data Structures & Schema

### 2.1 Core Data Types (F# Record Types)

```fsharp
type HorseResult = {
    StartTime: DateTime
    RaceType: string
    Distance: string
    RaceClass: string
    Age: int
    BetfairStartPrice: float
    BetfairInPlayLow: float
    OfficialRating: int
    TopSpeed: int
    RacingpostRating: int
    Position: int
    BeatenDistance: float
    Speed: float
}

type RacingPostRace = {
    LastRunInDays: int
    Position: int
    Distance: int  // in meters
    BeatenDistance: float
    WeightCarried: int  // in lbs
    Topspeed: int
    RaceDescription: string  // Natural language comment
}

type TimeformFlags = {
    HorseWinnerLastTimeOut: bool
    HorseInForm: bool
    HorseBeatenFavouriteLTO: bool
    SuitedByGoing: bool
    SuitedByCourse: bool
    SuitedByDistance: bool
    TrainerInForm: bool
    TrainerCourseRecord: bool
    JockeyInForm: bool
    JockeyWonOnHorse: bool
    TimeformTopRated: bool
    TimeformImprover: bool
    TimeformHorseInFocus: bool
}

type JockeyResult = {
    StartTime: DateTime
    IsRiddenByJockey: bool  // True if same jockey as current race
    RaceType: string
    Distance: string
    RaceClass: string
    BetfairStartPrice: float
    BetfairInPlayLow: float
    OfficialRating: int
    TopSpeed: int
    RacingpostRating: int
    Position: int
    BeatenDistance: float
    Speed: float
}

type SelectionData = {
    SelectionId: string
    Name: string
    MarketPrice: float
    HorseResults: HorseResult list
    JockeyResults: JockeyResult list
    RacingpostHorseData: RacingPostRace list
    TimeformHorseData: TimeformFlags
}
```

### 2.2 Data Availability Notes (Real-World Example)

From the live Wolverhampton market:

- **Rich Data:** "Sunshine And Roses" and "Galileo Blue" have extensive `horseResults` (7-8+ races) and complete `racingpostHorseData` with detailed comments.
- **Sparse Data:** "Free Your Spirit" has only 1 race in `racingpostHorseData`; "Baggie Boy" has NO historical data at all.
- **Handling Gaps:** The implementation MUST handle missing data gracefully (e.g., assign default scores, skip incomplete records).

---

## 3. LBBW v2 Model Implementation

### 3.1 LBBW Framework Overview

**LBBW = Last + Best + Base + Weight (+ Bonuses)**

| Component | Description | Score Range |
|-----------|-------------|-------------|
| **Last** | Most recent race performance (recency & momentum) | 0–1 |
| **Best** | Peak rating from last 5 races (proven ceiling) | 0–1 |
| **Base** | Consistency across last 5 races (stability) | 0–3 |
| **Weight** | Handicap impact (carrying top weight, resilience) | 0–1 |
| **Bonuses** | Special adjustments (3yo improvement, course/distance fit) | 0–0.5 |

**Total LBBW Score:** 0–5 (higher = better current form and value potential)

### 3.2 Component Calculation (F# Implementation)

#### 3.2.1 Last (Recency Score)

```fsharp
let calculateLastScore (horseResults: HorseResult list) =
    match horseResults with
    | [] -> 0.0
    | mostRecent :: _ ->
        // Score based on position and beaten distance
        let positionScore =
            match mostRecent.Position with
            | 1 -> 1.0
            | 2 -> 0.75
            | 3 -> 0.5
            | 4 -> 0.25
            | _ -> 0.0
        
        // Adjust for beaten distance (closer = better)
        let distanceAdjustment =
            if mostRecent.BeatenDistance <= 1.0 then 0.1
            elif mostRecent.BeatenDistance <= 3.0 then 0.0
            else -0.1
        
        // Days since last run penalty (prefer recent runners)
        let daysSinceRun = (DateTime.Now - mostRecent.StartTime).TotalDays
        let recencyPenalty =
            if daysSinceRun > 60.0 then -0.2
            elif daysSinceRun > 30.0 then -0.1
            else 0.0
        
        Math.Max(0.0, Math.Min(1.0, positionScore + distanceAdjustment + recencyPenalty))
```

#### 3.2.2 Best (Peak Performance)

```fsharp
let calculateBestScore (horseResults: HorseResult list) =
    // Get best topSpeed from last 5 races
    let last5 = horseResults |> List.take (Math.Min(5, horseResults.Length))
    let topSpeeds = last5 |> List.map (fun r -> float r.TopSpeed)
    
    if topSpeeds.IsEmpty then 0.0
    else
        let maxSpeed = topSpeeds |> List.max
        // Normalize: Assume topSpeed range 0-100
        Math.Min(1.0, maxSpeed / 100.0)
```

#### 3.2.3 Base (Consistency)

```fsharp
let calculateBaseScore (horseResults: HorseResult list) =
    // Count top-3 finishes in last 5 races
    let last5 = horseResults |> List.take (Math.Min(5, horseResults.Length))
    let top3Finishes = last5 |> List.filter (fun r -> r.Position <= 3) |> List.length
    
    // Base score: 0-3 based on consistency
    match top3Finishes with
    | 5 -> 3.0  // All top-3
    | 4 -> 2.5
    | 3 -> 2.0
    | 2 -> 1.0
    | 1 -> 0.5
    | _ -> 0.0
```

#### 3.2.4 Weight (Handicap Impact)

```fsharp
let calculateWeightScore (horseResults: HorseResult list) (racingpostData: RacingPostRace list) =
    // Check if horse carried top weight in recent races and performed well
    let recentWeights = racingpostData |> List.take (Math.Min(3, racingpostData.Length))
    let avgWeight = 
        if recentWeights.IsEmpty then 0.0
        else recentWeights |> List.averageBy (fun r -> float r.WeightCarried)
    
    // Bonus for carrying 135+ lbs and finishing top-3
    let weightBonus =
        recentWeights
        |> List.filter (fun r -> r.WeightCarried >= 135 && r.Position <= 3)
        |> List.length
    
    Math.Min(1.0, float weightBonus * 0.3)
```

#### 3.2.5 Bonuses (Special Adjustments)

```fsharp
let calculateBonuses (timeformFlags: TimeformFlags) (horseResults: HorseResult list) =
    let mutable bonusTotal = 0.0
    
    // Timeform flags bonuses
    if timeformFlags.TimeformTopRated then bonusTotal <- bonusTotal + 0.15
    if timeformFlags.HorseInForm then bonusTotal <- bonusTotal + 0.1
    if timeformFlags.SuitedByGoing && timeformFlags.SuitedByDistance then bonusTotal <- bonusTotal + 0.1
    if timeformFlags.TimeformImprover then bonusTotal <- bonusTotal + 0.05
    
    // Rapid rise bonus (3yo with improving ratings)
    let isImproving =
        if horseResults.Length >= 3 then
            let last3Ratings = horseResults |> List.take 3 |> List.map (fun r -> r.OfficialRating)
            last3Ratings |> List.pairwise |> List.forall (fun (a, b) -> b >= a)
        else false
    
    if isImproving then bonusTotal <- bonusTotal + 0.1
    
    Math.Min(0.5, bonusTotal)
```

### 3.3 Final LBBW Score

```fsharp
let calculateLBBWScore (selectionData: SelectionData) =
    let lastScore = calculateLastScore selectionData.HorseResults
    let bestScore = calculateBestScore selectionData.HorseResults
    let baseScore = calculateBaseScore selectionData.HorseResults
    let weightScore = calculateWeightScore selectionData.HorseResults selectionData.RacingpostHorseData
    let bonusScore = calculateBonuses selectionData.TimeformHorseData selectionData.HorseResults
    
    lastScore + bestScore + baseScore + weightScore + bonusScore
```

---

## 4. The Expert Analyst Workflow Implementation

### 4.1 Sentiment & NER Analysis (Racing Post Comments)

Racing Post comments provide qualitative insights. Use NER (Named Entity Recognition) and sentiment analysis to extract:
- **Positive signals:** "ran on well", "strong finisher", "good ground specialist"
- **Negative signals:** "weakened inside final furlong", "hung left", "carried head high"

```fsharp
let analyzeSentiment (raceDescription: string) =
    // Positive keywords
    let positiveKeywords = ["ran on"; "strong"; "kept on"; "headed"; "nearest finish"]
    let negativeKeywords = ["weakened"; "pulled hard"; "hung"; "denied clear run"; "tailed off"]
    
    let lowerDesc = raceDescription.ToLower()
    let positiveCount = positiveKeywords |> List.filter (fun kw -> lowerDesc.Contains(kw)) |> List.length
    let negativeCount = negativeKeywords |> List.filter (fun kw -> lowerDesc.Contains(kw)) |> List.length
    
    // Return sentiment score: +1 to -1
    float (positiveCount - negativeCount) / 5.0 |> Math.Max(-1.0) |> Math.Min(1.0)

let analyzeRacingPostData (racingpostData: RacingPostRace list) =
    if racingpostData.IsEmpty then 0.0
    else
        racingpostData
        |> List.take (Math.Min(3, racingpostData.Length))  // Last 3 races
        |> List.map (fun r -> analyzeSentiment r.RaceDescription)
        |> List.average
```

### 4.2 Horse-Specific Score (Expert Analyst)

```fsharp
let calculateHorseScore (selectionData: SelectionData) =
    // Component 1: Quantitative metrics (ratings, speed)
    let avgRating =
        if selectionData.HorseResults.IsEmpty then 0.0
        else
            selectionData.HorseResults
            |> List.take (Math.Min(5, selectionData.HorseResults.Length))
            |> List.averageBy (fun r -> float r.OfficialRating)
    
    let ratingScore = avgRating / 100.0  // Normalize to 0-1
    
    // Component 2: Sentiment from Racing Post comments
    let sentimentScore = analyzeRacingPostData selectionData.RacingpostHorseData
    
    // Component 3: Timeform flags
    let timeformScore =
        let flags = selectionData.TimeformHorseData
        let mutable score = 0.0
        if flags.TimeformTopRated then score <- score + 0.2
        if flags.HorseInForm then score <- score + 0.15
        if flags.HorseWinnerLastTimeOut then score <- score + 0.1
        if flags.SuitedByGoing && flags.SuitedByCourse then score <- score + 0.1
        score
    
    // Component 4: Exponential smoothing (recency weight)
    let recencyWeightedScore =
        if selectionData.HorseResults.IsEmpty then 0.0
        else
            selectionData.HorseResults
            |> List.take (Math.Min(5, selectionData.HorseResults.Length))
            |> List.mapi (fun i r ->
                let weight = Math.Pow(0.7, float i)  // Exponential decay
                let posScore = if r.Position = 1 then 1.0 elif r.Position <= 3 then 0.5 else 0.0
                weight * posScore
            )
            |> List.sum
    
    // Combine all components
    let rawScore = (ratingScore * 0.3) + (sentimentScore * 0.2) + (timeformScore * 0.3) + (recencyWeightedScore * 0.2)
    rawScore * 100.0  // Scale to 0-100
```

### 4.3 Jockey Score

```fsharp
let calculateJockeyScore (jockeyResults: JockeyResult list) =
    // Focus on last 20 rides
    let last20 = jockeyResults |> List.take (Math.Min(20, jockeyResults.Length))
    
    if last20.IsEmpty then 0.0
    else
        let wins = last20 |> List.filter (fun r -> r.Position = 1) |> List.length
        let places = last20 |> List.filter (fun r -> r.Position <= 3) |> List.length
        
        let winRate = float wins / float last20.Length
        let placeRate = float places / float last20.Length
        
        (winRate * 0.7 + placeRate * 0.3) * 100.0  // Scale to 0-100
```

---

## 5. Merged Composite Scoring System

### 5.1 Final Composite Score Formula

```fsharp
type CompositeScore = {
    LBBWScore: float          // 0-5
    HorseScore: float         // 0-100
    JockeyScore: float        // 0-100
    FinalScore: float         // Weighted combination
    ImpliedOdds: float        // Derived from FinalScore
    MarketOdds: float         // From Betfair
    ValueScore: float         // (MarketOdds - ImpliedOdds) / MarketOdds
}

let calculateCompositeScore (selectionData: SelectionData) =
    let lbbwScore = calculateLBBWScore selectionData
    let horseScore = calculateHorseScore selectionData
    let jockeyScore = calculateJockeyScore selectionData.JockeyResults
    
    // Merge LBBW (50%) + Expert Analyst (50%)
    let lbbwNormalized = (lbbwScore / 5.0) * 100.0  // Scale LBBW to 0-100
    let expertScore = (horseScore * 0.8) + (jockeyScore * 0.2)
    
    let finalScore = (lbbwNormalized * 0.5) + (expertScore * 0.5)
    
    // Convert to implied odds (inverse relationship)
    let impliedOdds = 100.0 / finalScore
    
    // Calculate value score
    let valueScore = ((selectionData.MarketPrice - impliedOdds) / selectionData.MarketPrice) * 100.0
    
    {
        LBBWScore = lbbwScore
        HorseScore = horseScore
        JockeyScore = jockeyScore
        FinalScore = finalScore
        ImpliedOdds = impliedOdds
        MarketOdds = selectionData.MarketPrice
        ValueScore = valueScore
    }
```

### 5.2 Handling Missing Data

```fsharp
let ensureDataAvailability (selectionData: SelectionData) =
    // If horse has fewer than 2 races, mark as insufficient data
    if selectionData.HorseResults.Length < 2 then
        { FinalScore = 0.0; ImpliedOdds = 999.0; ValueScore = -100.0; ... }
    else
        calculateCompositeScore selectionData
```

---

## 6. Output & Reporting

### 6.1 Recommendation Engine

```fsharp
type BettingRecommendation = {
    SelectionName: string
    CompositeScore: CompositeScore
    Recommendation: string  // "STRONG BET", "VALUE BET", "TRADE", "AVOID"
    Confidence: string      // "HIGH", "MEDIUM", "LOW"
    KeyEvidence: string list
}

let generateRecommendation (selection: SelectionData) (composite: CompositeScore) =
    let recommendation =
        if composite.ValueScore > 20.0 && composite.FinalScore > 70.0 then
            "STRONG BET"
        elif composite.ValueScore > 10.0 && composite.FinalScore > 60.0 then
            "VALUE BET"
        elif composite.FinalScore > 55.0 && composite.MarketOdds > 8.0 then
            "TRADE (Back for Lay)"
        else
            "AVOID"
    
    let confidence =
        if composite.FinalScore > 75.0 then "HIGH"
        elif composite.FinalScore > 60.0 then "MEDIUM"
        else "LOW"
    
    let evidence = [
        sprintf "LBBW Score: %.2f/5" composite.LBBWScore
        sprintf "Horse Score: %.1f/100" composite.HorseScore
        sprintf "Jockey Score: %.1f/100" composite.JockeyScore
        sprintf "Implied Odds: %.2f vs Market: %.2f" composite.ImpliedOdds composite.MarketOdds
        sprintf "Value Score: %.1f%%" composite.ValueScore
    ]
    
    {
        SelectionName = selection.Name
        CompositeScore = composite
        Recommendation = recommendation
        Confidence = confidence
        KeyEvidence = evidence
    }
```

### 6.2 Output Format (Markdown Table)

```fsharp
let formatResultsTable (recommendations: BettingRecommendation list) =
    let header = "| Horse | Market Odds | Implied Odds | Value % | LBBW | Horse | Jockey | Final | Rec | Conf |"
    let separator = "|-------|-------------|--------------|---------|------|-------|--------|-------|-----|------|"
    
    let rows =
        recommendations
        |> List.sortByDescending (fun r -> r.CompositeScore.ValueScore)
        |> List.map (fun r ->
            sprintf "| %s | %.2f | %.2f | %.1f%% | %.2f | %.1f | %.1f | %.1f | %s | %s |"
                r.SelectionName
                r.CompositeScore.MarketOdds
                r.CompositeScore.ImpliedOdds
                r.CompositeScore.ValueScore
                r.CompositeScore.LBBWScore
                r.CompositeScore.HorseScore
                r.CompositeScore.JockeyScore
                r.CompositeScore.FinalScore
                r.Recommendation
                r.Confidence
        )
    
    [ header; separator ] @ rows |> String.concat "\n"
```

---

## 7. Example Execution (Wolverhampton 7f Nov Stks)

### 7.1 Live Data Observations

From the fetched data:

**Sunshine And Roses:**
- **Horse Results:** 7 races, best topSpeed = 63, official rating = 63
- **Racing Post:** "Ran on well inside final furlong" (positive sentiment)
- **Timeform:** `horseInForm = false`, `suitedByGoing = true`, `timeformTopRated = false`
- **Market Odds:** 4.4

**Galileo Blue:**
- **Horse Results:** 2 races only (sparse data)
- **Racing Post:** "Weakened gradually inside final furlong" (negative sentiment)
- **Timeform:** All flags `false`
- **Market Odds:** 6.2

**Baggie Boy:**
- **Horse Results:** NONE (no historical data)
- **Racing Post:** EMPTY
- **Timeform:** All flags `false`
- **Market Odds:** 19

### 7.2 Expected Scores (Hypothetical)

| Horse | LBBW | Horse | Jockey | Final | Implied | Market | Value % | Rec |
|-------|------|-------|--------|-------|---------|--------|---------|-----|
| Sunshine And Roses | 3.2 | 68 | 72 | 69.6 | 1.44 | 4.4 | 67.3% | STRONG BET |
| Galileo Blue | 1.5 | 45 | 65 | 48.0 | 2.08 | 6.2 | 66.5% | VALUE BET |
| Free Your Spirit | 1.8 | 52 | 68 | 54.0 | 1.85 | 3.65 | 49.3% | TRADE |
| Baggie Boy | 0.0 | 0 | 50 | 10.0 | 10.0 | 19 | 47.4% | AVOID |

---

## 8. Implementation Checklist

### 8.1 Phase 1: Core Data Processing
- [ ] Implement API wrappers (`GetActiveMarket`, `GetAllDataContextForMarket`)
- [ ] Define F# record types for all data structures
- [ ] Implement safe data parsing with null/empty handling

### 8.2 Phase 2: LBBW Model
- [ ] Implement `calculateLastScore`
- [ ] Implement `calculateBestScore`
- [ ] Implement `calculateBaseScore`
- [ ] Implement `calculateWeightScore`
- [ ] Implement `calculateBonuses`
- [ ] Unit test each component with edge cases

### 8.3 Phase 3: Expert Analyst Workflow
- [ ] Implement sentiment analysis for Racing Post comments
- [ ] Implement `calculateHorseScore` (quantitative + qualitative)
- [ ] Implement `calculateJockeyScore`
- [ ] Exponential smoothing for recency weighting

### 8.4 Phase 4: Composite Scoring & Recommendations
- [ ] Implement `calculateCompositeScore`
- [ ] Implement `generateRecommendation`
- [ ] Implement `formatResultsTable`
- [ ] Handle missing data gracefully

### 8.5 Phase 5: Testing & Validation
- [ ] Test with live Wolverhampton market data
- [ ] Validate against historical race results
- [ ] Backtest LBBW + Expert scores against actual outcomes
- [ ] Tune weighting factors based on performance

---

## 9. Key Developer Notes

### 9.1 Data Quality
- **Always check for empty lists** before calling `List.head`, `List.average`, etc.
- Use `List.take (Math.Min(n, list.Length))` to avoid index out of range errors.
- Default to zero scores when data is insufficient (e.g., less than 2 races).

### 9.2 Weighting Philosophy
- **LBBW (50%) + Expert Analyst (50%):** Balanced approach blending quantitative and qualitative.
- **Horse (80%) + Jockey (20%):** The horse's form is paramount; jockey is a modifier.
- **Adjust weights** based on backtesting results.

### 9.3 Market Context
- **Favorites (odds < 4.0):** Apply a "wisdom of crowd" bonus (e.g., +5% to final score).
- **Outsiders (odds > 20.0):** Apply a skepticism penalty unless LBBW score is exceptional (> 4.0).

### 9.4 Sentiment Analysis (Advanced)
- For production systems, integrate a proper NLP library (e.g., Azure Cognitive Services, spaCy).
- Extract entities (jockey, trainer, ground conditions) and perform aspect-based sentiment analysis.

### 9.5 Performance Optimization
- Cache repeated calculations (e.g., `List.take`, `List.filter`).
- Use parallel processing for analyzing multiple selections:
  ```fsharp
  let recommendations =
      selections
      |> List.map (fun sel -> async { return analyzeSelection sel })
      |> Async.Parallel
      |> Async.RunSynchronously
  ```

---

## 10. Glossary

| Term | Definition |
|------|------------|
| **LBBW** | Last-Best-Base-Weight scoring framework |
| **Implied Odds** | Odds derived from the composite score (inverse relationship) |
| **Value Score** | Percentage difference between market odds and implied odds |
| **Exponential Smoothing** | Weighting recent data more heavily than older data |
| **Sentiment Analysis** | NLP technique to extract positive/negative signals from text |
| **NER** | Named Entity Recognition (extracting entities like jockey, trainer) |
| **Timeform Flags** | Boolean indicators of form, suitability, and trainer/jockey performance |

---

## 11. References

- **Source Documents:**
  - `TheExpertHorseRacingAnalyst.md` – Comprehensive analytical workflow
  - `LBBW_Task.md` – LBBW v2 model framework
- **Live Market Data:**
  - Wolverhampton 7f Nov Stks (Market ID: 1.251904979)
  - 10 runners fetched via `GetActiveMarket` and `GetAllDataContextForMarket`

---

## 12. Conclusion

This merged implementation guide provides a production-ready blueprint for combining the **LBBW v2 quantitative model** with **The Expert Horse Racing Analyst's qualitative workflow**. By following this guide, developers can build a robust, data-driven horse racing analysis system that:

1. Ingests multiple data sources (historical form, Racing Post, Timeform)
2. Calculates composite scores blending recency, consistency, peak performance, and sentiment
3. Identifies value bets through implied vs market odds comparison
4. Outputs actionable recommendations with confidence levels

**Next Steps:**
- Implement Phase 1 (Data Processing)
- Unit test each scoring component
- Run live validation against Wolverhampton market
- Iterate based on backtesting results

---

**Document Version:** 1.0  
**Last Updated:** 2025-12-22  
**Author:** AI Agent (GitHub Copilot)
