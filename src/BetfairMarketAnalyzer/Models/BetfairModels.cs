using System.Text.Json.Serialization;

namespace BetfairMarketAnalyzer.Models;

public class MarketData
{
    [JsonPropertyName("marketId")]
    public string MarketId { get; set; } = string.Empty;

    [JsonPropertyName("startTime")]
    public DateTime StartTime { get; set; }

    [JsonPropertyName("eventType")]
    public string EventType { get; set; } = string.Empty;

    [JsonPropertyName("eventName")]
    public string EventName { get; set; } = string.Empty;

    [JsonPropertyName("marketName")]
    public string MarketName { get; set; } = string.Empty;

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;
}

public class Selection
{
    [JsonPropertyName("selectionId")]
    public string SelectionId { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;
}

public class TradedPriceVolume
{
    [JsonPropertyName("time")]
    public DateTime Time { get; set; }

    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    [JsonPropertyName("volume")]
    public decimal Volume { get; set; }
}

public class SelectionWithHistory
{
    [JsonPropertyName("selection")]
    public Selection Selection { get; set; } = new();

    [JsonPropertyName("tradedPricesAndVolume")]
    public List<TradedPriceVolume> TradedPricesAndVolume { get; set; } = new();
}

public class MarketSelectionData
{
    [JsonPropertyName("market")]
    public MarketData Market { get; set; } = new();

    [JsonPropertyName("selections")]
    public List<SelectionWithHistory> Selections { get; set; } = new();
}

public class BetfairActiveMarketResponse
{
    [JsonPropertyName("activeBetfairMarket")]
    public ActiveMarketData ActiveBetfairMarket { get; set; } = new();
}

public class ActiveMarketData
{
    [JsonPropertyName("marketId")]
    public string MarketId { get; set; } = string.Empty;

    [JsonPropertyName("startTime")]
    public DateTime StartTime { get; set; }

    [JsonPropertyName("eventType")]
    public string EventType { get; set; } = string.Empty;

    [JsonPropertyName("eventName")]
    public string EventName { get; set; } = string.Empty;

    [JsonPropertyName("marketName")]
    public string MarketName { get; set; } = string.Empty;

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("selections")]
    public List<Selection> Selections { get; set; } = new();
}
