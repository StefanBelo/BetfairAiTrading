namespace BetfairMarketAnalyzerFSharp.Models

open System
open System.Text.Json.Serialization

[<CLIMutable>]
type MarketData = {
    [<JsonPropertyName("marketId")>]
    MarketId: string
    [<JsonPropertyName("startTime")>]
    StartTime: DateTime
    [<JsonPropertyName("eventType")>]
    EventType: string
    [<JsonPropertyName("eventName")>]
    EventName: string
    [<JsonPropertyName("marketName")>]
    MarketName: string
    [<JsonPropertyName("status")>]
    Status: string
}

[<CLIMutable>]
type Selection = {
    [<JsonPropertyName("selectionId")>]
    SelectionId: string
    [<JsonPropertyName("name")>]
    Name: string
    [<JsonPropertyName("price")>]
    Price: decimal
    [<JsonPropertyName("status")>]
    Status: string
}

[<CLIMutable>]
type TradedPriceVolume = {
    [<JsonPropertyName("time")>]
    Time: DateTime
    [<JsonPropertyName("price")>]
    Price: decimal
    [<JsonPropertyName("volume")>]
    Volume: decimal
}

[<CLIMutable>]
type SelectionWithHistory = {
    [<JsonPropertyName("selection")>]
    Selection: Selection
    [<JsonPropertyName("tradedPricesAndVolume")>]
    TradedPricesAndVolume: TradedPriceVolume list
}

[<CLIMutable>]
type MarketSelectionData = {
    [<JsonPropertyName("market")>]
    Market: MarketData
    [<JsonPropertyName("selections")>]
    Selections: SelectionWithHistory list
}

[<CLIMutable>]
type BetfairActiveMarketResponse = {
    [<JsonPropertyName("activeBetfairMarket")>]
    ActiveBetfairMarket: ActiveMarketData
}

and [<CLIMutable>] ActiveMarketData = {
    [<JsonPropertyName("marketId")>]
    MarketId: string
    [<JsonPropertyName("startTime")>]
    StartTime: DateTime
    [<JsonPropertyName("eventType")>]
    EventType: string
    [<JsonPropertyName("eventName")>]
    EventName: string
    [<JsonPropertyName("marketName")>]
    MarketName: string
    [<JsonPropertyName("status")>]
    Status: string
    [<JsonPropertyName("selections")>]
    Selections: Selection list
}
