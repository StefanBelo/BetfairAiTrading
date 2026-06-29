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

type SignalType =
    | MomentumCandidate of direction:string * confidence:decimal
    | ReversionCandidate of targetPrice:decimal * divergence:decimal * confidence:decimal
    | Neutral

let impliedProbability odds = 1m / odds

let volumeAcceleration candles =
    match List.rev candles with
    | current :: previous :: _ when previous.Volume > 0m ->
        (current.Volume - previous.Volume) / previous.Volume
    | _ -> 0m

let volumeDepth candles =
    let total = candles |> List.sumBy (fun c -> c.Volume)
    let current = candles |> List.last
    current.Volume / max 1m total

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

let calculateReversionConfidence probShift wickScore volumeAccel volumeDepth trendIntegrity =
    let baseScore = min 1m (probShift / 0.001m)
    let volumeScore = min 1m (volumeDepth + volumeAccel * 0.3m)
    let wickScoreNorm = min 1m (wickScore / 10m)
    let trendScore = trendIntegrity
    min 1m ((baseScore * 0.5m) + (volumeScore * 0.25m) + (trendScore * 0.15m) + (wickScoreNorm * 0.10m))

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

let reversionQualified candles =
    match candles |> List.rev |> List.take 2 |> List.rev with
    | [prev; current] when List.length candles >= 3 ->
        let prevprev = candles |> List.rev |> List.item 2
        let prevCloseProb = impliedProbability prev.Close
        let currentCloseProb = impliedProbability current.Close
        let prevprevCloseProb = impliedProbability prevprev.Close
        (wickRatio prev > 1.8m || wickRatio current > 2m) &&
        abs (currentCloseProb - prevCloseProb) < abs (prevCloseProb - prevprevCloseProb) &&
        current.Volume > prev.Volume * 1.2m
    | _ -> false

let oddsRange candles =
    let prices = candles |> List.map (fun c -> c.Close)
    (List.min prices, List.max prices, List.averageBy id prices)

printfn "Market: %s" root.Market.MarketName
for sel in root.SelectionsData do
    let candles = sel.Data.CandleStickData
    let qualified = reversionQualified candles
    let prev = List.item (List.length candles - 2) candles
    let current = List.last candles
    let minPrice, maxPrice, avgPrice = oddsRange candles
    let volumeDepthValue = volumeDepth candles
    let volumeAccelValue = (current.Volume - prev.Volume) / max 1m prev.Volume
    let trendIntegrity = calculateTrendIntegrity candles
    let reversionConfidence =
        let wickScore = max (wickRatio prev) (wickRatio current)
        calculateReversionConfidence
            (abs ((1m / current.Close) - (1m / prev.Close)))
            wickScore
            volumeAccelValue
            volumeDepthValue
            trendIntegrity
    printfn "Selection: %s" sel.Name
    printfn "  last odds: %.2f -> %.2f" prev.Close current.Close
    printfn "  wick(prev)=%.2f wick(cur)=%.2f" (wickRatio prev) (wickRatio current)
    printfn "  volume prev=%.2f current=%.2f" prev.Volume current.Volume
    printfn "  reversionQualified = %b" qualified
    printfn "  reversionConfidence = %.6f" reversionConfidence
    printfn "  odds range = %.2f - %.2f, avg = %.2f" minPrice maxPrice avgPrice
    printfn ""
