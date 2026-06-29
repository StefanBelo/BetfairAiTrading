open System
open System.IO
open System.Text.Json
open System.Text.Json.Serialization

type Candle = {
    [<JsonPropertyName("date")>]
    Date: string
    [<JsonPropertyName("high")>]
    High: decimal
    [<JsonPropertyName("open")>]
    Open: decimal
    [<JsonPropertyName("close")>]
    Close: decimal
    [<JsonPropertyName("low")>]
    Low: decimal
    [<JsonPropertyName("volume")>]
    Volume: decimal
}

type SelectionData = {
    [<JsonPropertyName("candleStickData")>]
    CandleStickData: Candle list
    [<JsonPropertyName("backLayRatio")>]
    BackLayRatio: decimal
}

type Selection = {
    [<JsonPropertyName("selectionId")>]
    SelectionId: string
    [<JsonPropertyName("name")>]
    Name: string
    [<JsonPropertyName("data")>]
    Data: SelectionData
}

type Market = {
    [<JsonPropertyName("marketId")>]
    MarketId: string
    [<JsonPropertyName("startTime")>]
    StartTime: string
    [<JsonPropertyName("eventType")>]
    EventType: string
    [<JsonPropertyName("eventName")>]
    EventName: string
    [<JsonPropertyName("marketName")>]
    MarketName: string
    [<JsonPropertyName("status")>]
    Status: string
}

type Root = {
    [<JsonPropertyName("market")>]
    Market: Market
    [<JsonPropertyName("selectionsData")>]
    SelectionsData: Selection list
}

let inputFile = Path.Combine(__SOURCE_DIRECTORY__, "OHLC_Generalized_Candle_Span_Strategy_Sample.json")
let json = File.ReadAllText(inputFile)
let options = JsonSerializerOptions(PropertyNameCaseInsensitive = true)
let root = JsonSerializer.Deserialize<Root>(json, options)

let impliedProbability odds = 1m / odds
let probabilityRange candle = abs ((1m / candle.High) - (1m / candle.Low))
let probabilityDelta candle = abs ((1m / candle.Close) - (1m / candle.Open))

let cumulativeProbabilityDelta candles =
    candles |> List.map probabilityDelta |> List.sum

let trendDirection candles =
    let first = List.head candles
    let last = List.last candles
    if last.Close < first.Open then "BACK" else "LAY"

let momentumConsistency candles =
    let directions = candles |> List.map (fun c -> c.Close < c.Open)
    let count = directions |> List.filter id |> List.length
    decimal count / decimal directions.Length

let volumeAcceleration candles =
    match List.rev candles with
    | current :: previous :: _ when previous.Volume > 0m ->
        (current.Volume - previous.Volume) / previous.Volume
    | _ -> 0m

let volumeDepth candles =
    let total = candles |> List.sumBy (fun c -> c.Volume)
    let current = List.last candles
    current.Volume / max 1m total

let calculateTVPT candle =
    let probRange = probabilityRange candle
    if probRange = 0m then candle.Volume else candle.Volume / probRange

let calculateVVI candle =
    let body = abs (candle.Close - candle.Open)
    let wicks = (candle.High - candle.Low) - body
    if body = 0m then 10m else wicks / body

let calculateTrendIntegrity candles =
    let probs = candles |> List.map (fun c -> 1m / c.Close)
    let ordered = probs |> List.windowed 3
    let consistent =
        ordered
        |> List.sumBy (fun window ->
            match window with
            | [p1; p2; p3] when (p1 < p2 && p2 < p3) || (p1 > p2 && p2 > p3) -> 1
            | _ -> 0)
    if List.length ordered = 0 then 0m else decimal consistent / decimal (List.length ordered)

let totalProbabilityShift candles =
    let first = List.head candles
    let last = List.last candles
    abs ((1m / last.Close) - (1m / first.Open))

let wickRatio candle =
    let openProb = impliedProbability candle.Open
    let closeProb = impliedProbability candle.Close
    let highProb = impliedProbability candle.High
    let lowProb = impliedProbability candle.Low
    let bodyHigh = max openProb closeProb
    let bodyLow = min openProb closeProb
    let wickTop = max 0m (bodyHigh - highProb)
    let wickBottom = max 0m (lowProb - bodyLow)
    let body = abs (closeProb - openProb)
    if body = 0m then 10m else max wickTop wickBottom / body

let calculateCandleMetrics candle =
    let openProb = impliedProbability candle.Open
    let closeProb = impliedProbability candle.Close
    let highProb = impliedProbability candle.High
    let lowProb = impliedProbability candle.Low
    let probRange = probabilityRange candle
    let probDelta = probabilityDelta candle
    let tvpt = calculateTVPT candle
    let vvi = calculateVVI candle
    let wick = wickRatio candle
    openProb, closeProb, highProb, lowProb, probRange, probDelta, tvpt, vvi, wick

let printRange label values =
    let values = values |> Seq.toList
    if List.isEmpty values then printfn "  %s: none" label
    else printfn "  %s: min=%.6f max=%.6f avg=%.6f" label (List.min values) (List.max values) (List.average values)

printfn "Market: %s (%s)" root.Market.MarketName root.Market.MarketId
printfn ""
let allProbabilityRanges = ResizeArray<decimal>()
let allProbabilityDeltas = ResizeArray<decimal>()
let allTVPTs = ResizeArray<decimal>()
let allVVIs = ResizeArray<decimal>()
let allWickRatios = ResizeArray<decimal>()
let allVolumeDepths = ResizeArray<decimal>()
let allVolumeAccelerations = ResizeArray<decimal>()
let allTrendIntegrities = ResizeArray<decimal>()
let allMomentumConsistencies = ResizeArray<decimal>()
let allTotalProbabilityShifts = ResizeArray<decimal>()
let allCandleStrengths = ResizeArray<decimal>()

for sel in root.SelectionsData do
    let candles = sel.Data.CandleStickData
    let candleMetrics = candles |> List.map calculateCandleMetrics
    candleMetrics |> List.iter (fun (_, _, _, _, pr, pd, tvpt, vvi, wick) ->
        allProbabilityRanges.Add(pr)
        allProbabilityDeltas.Add(pd)
        allTVPTs.Add(tvpt)
        allVVIs.Add(vvi)
        allWickRatios.Add(wick))

    let cumulativeDelta = cumulativeProbabilityDelta candles
    let trend = trendDirection candles
    let momentumCons = momentumConsistency candles
    let volAccel = volumeAcceleration candles
    let volDepth = volumeDepth candles
    let trendIntegrity = calculateTrendIntegrity candles
    let totProbShift = totalProbabilityShift candles
    let current = List.last candles
    let candleStrength =
        let body = probabilityDelta current
        let range = probabilityRange current
        if range > 0m then body / range else 0m
    let tvpt = calculateTVPT current
    let vvi = calculateVVI current
    let avgProbRange = candles |> List.averageBy probabilityRange
    let momentumQualified =
        totProbShift > 0.0008m &&
        momentumCons >= 0.66m &&
        volDepth > 0.2m &&
        volAccel > 0.2m &&
        candleStrength > 0.5m &&
        avgProbRange > 0m
    let reversionQualified =
        match candles |> List.rev |> List.take 2 |> List.rev with
        | [prev; current] when List.length candles >= 3 ->
            let prevprev = candles |> List.rev |> List.item 2
            (wickRatio prev > 1.8m || wickRatio current > 2m) &&
            abs (current.Close - prev.Close) < abs (prev.Close - prevprev.Close) &&
            current.Volume > prev.Volume * 1.2m
        | _ -> false

    allVolumeDepths.Add(volDepth)
    allVolumeAccelerations.Add(volAccel)
    allTrendIntegrities.Add(trendIntegrity)
    allMomentumConsistencies.Add(momentumCons)
    allTotalProbabilityShifts.Add(totProbShift)
    allCandleStrengths.Add(candleStrength)

    let minPrice = candles |> List.minBy (fun c -> c.Close) |> fun c -> c.Close
    let maxPrice = candles |> List.maxBy (fun c -> c.Close) |> fun c -> c.Close
    let avgPrice = candles |> List.averageBy (fun c -> c.Close)

    printfn "Selection: %s" sel.Name
    printfn "  odds close range: %.2f - %.2f, avg=%.2f" minPrice maxPrice avgPrice
    printfn "  totalProbabilityShift=%.8f" totProbShift
    printfn "  cumulativeProbabilityDelta=%.8f" cumulativeDelta
    printfn "  trendDirection=%s" trend
    printfn "  momentumConsistency=%.6f" momentumCons
    printfn "  volumeAcceleration=%.6f" volAccel
    printfn "  volumeDepth=%.6f" volDepth
    printfn "  trendIntegrity=%.6f" trendIntegrity
    printfn "  averageProbRange=%.8f" avgProbRange
    printfn "  current candleStrength=%.6f" candleStrength
    printfn "  current TVPT=%.6f" tvpt
    printfn "  current VVI=%.6f" vvi
    printfn "  momentumQualified=%b" momentumQualified
    printfn "  reversionQualified=%b" reversionQualified
    printfn "  candles:"
    for c, (openProb, closeProb, highProb, lowProb, pr, pd, tvpt, vvi, wick) in List.zip candles candleMetrics do
        printfn "    %s o=%.2f h=%.2f l=%.2f c=%.2f v=%.2f | pOpen=%.6f pClose=%.6f pHigh=%.6f pLow=%.6f pr=%.8f pd=%.8f TVPT=%.6f VVI=%.6f wick=%.6f" c.Date c.Open c.High c.Low c.Close c.Volume openProb closeProb highProb lowProb pr pd tvpt vvi wick
    printfn ""

printfn "GLOBAL DATA RANGES"
printRange "probabilityRange" allProbabilityRanges
printRange "probabilityDelta" allProbabilityDeltas
printRange "TVPT" allTVPTs
printRange "VVI" allVVIs
printRange "wickRatio" allWickRatios
printRange "volumeDepth" allVolumeDepths
printRange "volumeAcceleration" allVolumeAccelerations
printRange "trendIntegrity" allTrendIntegrities
printRange "momentumConsistency" allMomentumConsistencies
printRange "totalProbabilityShift" allTotalProbabilityShifts
printRange "candleStrength" allCandleStrengths
