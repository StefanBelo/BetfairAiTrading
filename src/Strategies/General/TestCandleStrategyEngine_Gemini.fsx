open System
open System.IO
open System.Text.Json
open System.Text.Json.Serialization

type Candle = 
    {
        Time : DateTime
        High : float
        Open : float
        Close : float
        Low : float
        Volume : float
    }

type SelectionData = 
    {
        CandleStickData : Candle list
        BackLayRatio : float
    }

type Selection = 
    {
        SelectionId : string
        Name : string
        Data : SelectionData option
    }

type Market = 
    {
        MarketId : string
        StartTime : string
        EventType : string
        EventName : string
        MarketName : string
        Status : string
    }

type MarketData = 
    {
        Market : Market
        SelectionsData : Selection list
    }

let getTestData () =
    let jsonSerializerOptions = JsonSerializerOptions (
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,            
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        )

    jsonSerializerOptions.Converters.Add (JsonStringEnumConverter ())

    let path = @"E:\Projects\BetfairAiTrading\data\TestData\"
    let fileNames = [
        "MarketSelectionsCandleStickData.json"
        "MarketSelectionsCandleStickData2.json"
        "MarketSelectionsCandleStickData3.json"
    ]
        
    [
        for fileName in fileNames do
            let json = File.ReadAllText (Path.Combine (path, fileName))

            JsonSerializer.Deserialize<MarketData> (json, jsonSerializerOptions)
    ]

let allData = getTestData ()

type TradeAction =
    | Back
    | Lay
    | DoNothing

type SignalBand =
    | StrongBack
    | WeakBack
    | Neutral
    | WeakLay
    | StrongLay

type SelectionAnalysis =
    {
        Selection : Selection
        Action : TradeAction
        Band : SignalBand
        IsTradable : bool
        GateReason : string
        Score : float
        FairProbability : float
        FairBackPrice : float
        FairLayPrice : float
        ReversalRisk : float
        EntryAllowed : bool
        EntryReason : string
        Momentum : float
        VolumeSpike : float
        TrendConsistency : float
        MomentumAcceleration : float
        Volatility : float
        AverageVolume : float
        CurrentVolume : float
        ReversalBias : float
        AgreementScore : int
        BodyRatio : float
        BackLayRatio : float
        Vwap : float // ADDED
    }

// ADDED: CandleMetrics record
type CandleMetrics =
    {
        Momentum : float
        VolumeSpike : float
        BodyRatio : float
        TrendConsistency : float
        MomentumAcceleration : float
        Volatility : float
        AverageVolume : float
        CurrentVolume : float
        ReversalBias : float
        BaseScore : float
        Vwap : float
    }

let toProbability (price : float) = 1.0 / price

let clamp minValue maxValue value =
    max minValue (min maxValue value)

let scoreToFairProbability score =
    let z = clamp -3.0 3.0 (score * 2.5)
    clamp 0.02 0.98 (1.0 / (1.0 + Math.Exp(-z)))

let reversalRisk momentum trendConsistency momentumAcceleration reversalBias =
    let weakTrend = if trendConsistency < 0.55 then 1.0 else 0.0
    let oppositeAccel = if momentum * momentumAcceleration < 0.0 then 1.0 else 0.0
    let biasRisk = clamp 0.0 1.0 (abs reversalBias * 1.5)
    clamp 0.0 1.0 ((weakTrend * 0.35) + (float oppositeAccel * 0.25) + (biasRisk * 0.40))

// Refactored gates
let evaluateGates momentum trendConsistency volumeSpike averageVolume volatility backLayRatio =
    // Market conditions check (Tradable)
    if abs (backLayRatio - 0.5) < 0.03 then
        false, "Order book is balanced (backLay near 0.50)."
    elif volumeSpike < 1.15 || averageVolume < 25.0 then
        false, "Participation quality is too low (volume gate failed)."
    elif abs momentum < 0.02 && volatility > 0.10 then
        false, "High-noise regime: volatility high while momentum is weak."
    else
        true, "Tradable"

let evaluateEntryGate adjustedScore trendConsistency reversalRisk currentOdds fairBackPrice fairLayPrice vwap =
    // Signal check (Entry Allowed)
    if abs adjustedScore < 0.18 then
        false, "Score is too weak for entry."
    elif trendConsistency < 0.45 then
        false, "Trend consistency is too weak."
    elif reversalRisk > 0.55 then
        false, "Reversal risk is elevated."
    // Fair Price / VWAP Value check
    elif adjustedScore > 0.0 && currentOdds < fairBackPrice then
        false, "Market odds are worse than Fair Back Price."
    elif adjustedScore < 0.0 && currentOdds > fairLayPrice then
        false, "Market odds are worse than Fair Lay Price."
    elif adjustedScore > 0.0 && currentOdds < vwap then // In Betfair, low odds = high prob. Price < VWAP means odds worse than VWAP
        false, "Market odds are below VWAP (worse value for Back)."
    elif adjustedScore < 0.0 && currentOdds > vwap then
        false, "Market odds are above VWAP (worse value for Lay)."
    else
        true, "Entry conditions are acceptable."

let analyzeCandles (candles : Candle list) : CandleMetrics =
    let first = List.head candles
    let last = List.last candles
    let firstProb = toProbability first.Open
    let lastProb = toProbability last.Close
    let momentum =
        if firstProb = 0.0 then 0.0
        else (lastProb - firstProb) / firstProb

    let candleMoves =
        candles
        |> List.map (fun c ->
            let openProb = toProbability c.Open
            let closeProb = toProbability c.Close
            closeProb - openProb)

    let trendConsistency =
        let sign = if momentum >= 0.0 then 1.0 else -1.0
        let alignedCount =
            candleMoves
            |> List.filter (fun move -> move * sign > 0.0)
            |> List.length
        float alignedCount / float candleMoves.Length

    let halfCount = max 1 (candles.Length / 2)
    let earlier = candles |> List.take halfCount
    let later = candles |> List.skip (candles.Length - halfCount)

    let segmentMomentum segment =
        let s = List.head segment
        let e = List.last segment
        let sProb = toProbability s.Open
        let eProb = toProbability e.Close
        if sProb = 0.0 then 0.0 else (eProb - sProb) / sProb

    let earlierMomentum = segmentMomentum earlier
    let laterMomentum = segmentMomentum later
    let momentumAcceleration = laterMomentum - earlierMomentum

    let averageVolume = candles |> List.averageBy (fun c -> c.Volume)
    let currentVolume = last.Volume
    let volumeSpike = if averageVolume = 0.0 then 0.0 else currentVolume / averageVolume

    let bodyRatio =
        candles
        |> List.averageBy (fun c ->
            let openProb = toProbability c.Open
            let closeProb = toProbability c.Close
            let highProb = toProbability c.High
            let lowProb = toProbability c.Low
            let extremeHighProb = max highProb lowProb
            let extremeLowProb = min highProb lowProb
            let range = extremeHighProb - extremeLowProb
            if range = 0.0 then 0.0 else abs (closeProb - openProb) / range)

    let volatility =
        candles
        |> List.averageBy (fun c ->
            let highProb = toProbability c.High
            let lowProb = toProbability c.Low
            abs (highProb - lowProb))

    let reversalBias =
        candles
        |> List.rev
        |> List.truncate 5
        |> List.averageBy (fun c ->
            let openProb = toProbability c.Open
            let closeProb = toProbability c.Close
            let highProb = toProbability c.High
            let lowProb = toProbability c.Low
            let extremeHighProb = max highProb lowProb
            let extremeLowProb = min highProb lowProb
            let bodyHighProb = max openProb closeProb
            let bodyLowProb = min openProb closeProb
            let upperWick = max 0.0 (extremeHighProb - bodyHighProb)
            let lowerWick = max 0.0 (bodyLowProb - extremeLowProb)
            let range = extremeHighProb - extremeLowProb
            if range = 0.0 then 0.0 else (upperWick - lowerWick) / range)

    // Calculate VWAP
    let totalVolume = candles |> List.sumBy (fun c -> c.Volume)
    let vwap =
        if totalVolume = 0.0 then 0.0
        else
            let volumeWeightedPrice = 
                candles |> List.sumBy (fun c -> 
                    let typicalPrice = (c.High + c.Low + c.Close) / 3.0
                    typicalPrice * c.Volume)
            volumeWeightedPrice / totalVolume

    let scoreFrom x scale = clamp -1.0 1.0 (x / scale)
    let momentumVote = scoreFrom momentum 0.10
    let accelerationVote = scoreFrom momentumAcceleration 0.08
    let bodyVote = scoreFrom (bodyRatio - 0.50) 0.30
    let trendVote = scoreFrom (trendConsistency - 0.50) 0.20
    let participationVote = scoreFrom (volumeSpike - 1.0) 1.5
    let reversalVote = -scoreFrom reversalBias 0.60

    let baseScore =
        (momentumVote * 0.34) +
        (accelerationVote * 0.18) +
        (bodyVote * 0.14) +
        (trendVote * 0.14) +
        (participationVote * 0.12) +
        (reversalVote * 0.08)

    {
        Momentum = momentum
        VolumeSpike = volumeSpike
        BodyRatio = bodyRatio
        TrendConsistency = trendConsistency
        MomentumAcceleration = momentumAcceleration
        Volatility = volatility
        AverageVolume = averageVolume
        CurrentVolume = currentVolume
        ReversalBias = reversalBias
        BaseScore = baseScore
        Vwap = vwap
    }

let buildBand score =
    if score >= 0.22 then StrongBack
    elif score >= 0.08 then WeakBack
    elif score <= -0.22 then StrongLay
    elif score <= -0.08 then WeakLay
    else Neutral

let bandToAction band =
    match band with
    | StrongBack
    | WeakBack -> Back
    | StrongLay
    | WeakLay -> Lay
    | Neutral -> DoNothing

let agreementScore momentum acceleration trendConsistency bodyRatio backLayRatio =
    let momentumVote =
        if momentum > 0.01 then 1
        elif momentum < -0.01 then -1
        else 0

    let accelerationVote =
        if acceleration > 0.01 then 1
        elif acceleration < -0.01 then -1
        else 0

    let trendVote =
        if trendConsistency > 0.60 then
            if momentum >= 0.0 then 1 else -1
        else 0

    let bodyVote =
        if bodyRatio > 0.55 then
            if momentum >= 0.0 then 1 else -1
        else 0

    let bookVote =
        if backLayRatio > 0.55 then 1
        elif backLayRatio < 0.45 then -1
        else 0

    momentumVote + accelerationVote + trendVote + bodyVote + bookVote

let emptyAnalysis selection reason =
    {
        Selection = selection
        Action = DoNothing
        Band = Neutral
        IsTradable = false
        GateReason = reason
        Score = 0.0
        FairProbability = 0.5
        FairBackPrice = 2.0
        FairLayPrice = 2.0
        ReversalRisk = 0.0
        EntryAllowed = false
        EntryReason = reason
        Momentum = 0.0
        VolumeSpike = 0.0
        TrendConsistency = 0.0
        MomentumAcceleration = 0.0
        Volatility = 0.0
        AverageVolume = 0.0
        CurrentVolume = 0.0
        ReversalBias = 0.0
        AgreementScore = 0
        BodyRatio = 0.0
        BackLayRatio = 0.0
        Vwap = 0.0
    }

let buildSelectionAnalysis (selection : Selection) =
    match selection.Data with
    | None
    | Some { CandleStickData = [] } ->
        emptyAnalysis selection "Missing candle data."
        
    | Some data when data.CandleStickData.Length < 5 ->
        emptyAnalysis selection "Insufficient candle data (< 5 candles)."

    | Some data ->
        let candles = data.CandleStickData
        let metrics = analyzeCandles candles

        let weightedScore = metrics.BaseScore + ((data.BackLayRatio - 0.5) * 0.16)
        let agreement = agreementScore metrics.Momentum metrics.MomentumAcceleration metrics.TrendConsistency metrics.BodyRatio data.BackLayRatio
        let adjustedScore = weightedScore + (float agreement * 0.03)
        let fairProbability = scoreToFairProbability adjustedScore
        let fairBackPrice = 1.0 / fairProbability
        let fairLayPrice = 1.0 / (1.0 - fairProbability)
        let isTradable, gateReason =
            evaluateGates metrics.Momentum metrics.TrendConsistency metrics.VolumeSpike metrics.AverageVolume metrics.Volatility data.BackLayRatio

        let currentOdds = (List.last candles).Close
        let revRisk = reversalRisk metrics.Momentum metrics.TrendConsistency metrics.MomentumAcceleration metrics.ReversalBias

        let entryAllowed, entryReason =
            if isTradable then
                evaluateEntryGate adjustedScore metrics.TrendConsistency revRisk currentOdds fairBackPrice fairLayPrice metrics.Vwap
            else
                false, gateReason

        let band =
            if entryAllowed then buildBand adjustedScore
            else Neutral

        {
            Selection = selection
            Action = bandToAction band
            Band = band
            IsTradable = isTradable
            GateReason = gateReason
            Score = adjustedScore
            FairProbability = fairProbability
            FairBackPrice = fairBackPrice
            FairLayPrice = fairLayPrice
            ReversalRisk = revRisk
            EntryAllowed = entryAllowed
            EntryReason = entryReason
            Momentum = metrics.Momentum
            VolumeSpike = metrics.VolumeSpike
            TrendConsistency = metrics.TrendConsistency
            MomentumAcceleration = metrics.MomentumAcceleration
            Volatility = metrics.Volatility
            AverageVolume = metrics.AverageVolume
            CurrentVolume = metrics.CurrentVolume
            ReversalBias = metrics.ReversalBias
            AgreementScore = agreement
            BodyRatio = metrics.BodyRatio
            BackLayRatio = data.BackLayRatio
            Vwap = metrics.Vwap
        }

let executeTest (data : MarketData) =
    printfn "\n\nLoaded %d selections from market '%s'" data.SelectionsData.Length data.Market.MarketName

    let strategyResults =
        data.SelectionsData
        |> List.map buildSelectionAnalysis

    let actionCategory action =
        match action with
        | Back -> "Back"
        | Lay -> "Lay"
        | DoNothing -> "DoNothing"

    let summary =
        strategyResults
        |> List.groupBy (fun r -> actionCategory r.Action)
        |> List.map (fun (category, group) -> category, group.Length)

    printfn "\nStrategy results for market '%s' (%d selections):" data.Market.MarketName strategyResults.Length
    summary |> List.iter (fun (action, count) -> printfn "  %A: %d" action count)
    printfn "\n"

    strategyResults
    |> List.iteri (fun idx result ->
            printfn "%02d %s  action=%A  band=%A  score=%+.4f  agree=%+d  momentum=%+.4f  accel=%+.4f  trend=%0.2f  volSpike=%0.2f  vol=%0.3f  body=%0.2f  rev=%+.2f  backLay=%0.2f  vwap=%0.2f  tradable=%b  gate=%s"
                (idx + 1)
                result.Selection.Name
                result.Action
                result.Band
                result.Score
                result.AgreementScore
                result.Momentum
                result.MomentumAcceleration
                result.TrendConsistency
                result.VolumeSpike
                result.Volatility
                result.BodyRatio
                result.ReversalBias
                result.BackLayRatio
                result.Vwap
                result.IsTradable
                result.GateReason
        )

allData |> List.iter executeTest
