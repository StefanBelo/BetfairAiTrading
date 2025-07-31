using Skender.Stock.Indicators;

namespace BetfairMarketAnalyzer.Models;

public class BetfairQuote : IQuote
{
    public DateTime Date { get; set; }
    public decimal Open { get; set; }
    public decimal High { get; set; }
    public decimal Low { get; set; }
    public decimal Close { get; set; }
    public decimal Volume { get; set; }
}

public class AnalysisResult
{
    public string SelectionName { get; set; } = string.Empty;
    public decimal CurrentOdds { get; set; }
    public decimal OpeningOdds { get; set; }
    public decimal ImpliedProbability { get; set; }
    public decimal OddsMovement { get; set; }
    public decimal OddsMovementPercentage { get; set; }
    public string TrendDirection { get; set; } = string.Empty;
    public decimal TotalVolume { get; set; }
    public List<SupportResistanceLevel> SupportLevels { get; set; } = new();
    public List<SupportResistanceLevel> ResistanceLevels { get; set; } = new();
    public List<VolumeAnalysis> VolumeSpikes { get; set; } = new();
    public TechnicalIndicators Indicators { get; set; } = new();
    public List<TradingRecommendation> TradingRecommendations { get; set; } = new();
    public MarketFlow MarketFlow { get; set; } = new();
    public DateTime AnalysisTime { get; set; }
    public TimeSpan TimeToRaceStart { get; set; }
}

public class SupportResistanceLevel
{
    public decimal Level { get; set; }
    public int TouchCount { get; set; }
    public decimal TotalVolume { get; set; }
    public string Strength { get; set; } = string.Empty;
    public DateTime LastTouch { get; set; }
}

public class VolumeAnalysis
{
    public DateTime Time { get; set; }
    public decimal Price { get; set; }
    public decimal Volume { get; set; }
    public string Type { get; set; } = string.Empty; // "Spike", "SteamMove", "Normal"
    public string Direction { get; set; } = string.Empty; // "Backing", "Laying"
}

public class TechnicalIndicators
{
    public decimal? RSI { get; set; }
    public decimal? SMA5 { get; set; }
    public decimal? SMA10 { get; set; }
    public decimal? EMA5 { get; set; }
    public decimal? EMA10 { get; set; }
    public decimal? MACD { get; set; }
    public decimal? MACDSignal { get; set; }
    public decimal? BollingerUpper { get; set; }
    public decimal? BollingerLower { get; set; }
    public decimal? BollingerMiddle { get; set; }
    public string MomentumDirection { get; set; } = string.Empty;
    public decimal VolumeMovingAverage { get; set; }
}

public class TradingRecommendation
{
    public string Strategy { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty; // "Back", "Lay", "Back-to-Lay", "Lay-to-Back"
    public decimal EntryPrice { get; set; }
    public decimal TargetPrice { get; set; }
    public decimal StopLoss { get; set; }
    public decimal ExpectedProfit { get; set; }
    public decimal RiskReward { get; set; }
    public string TimeFrame { get; set; } = string.Empty;
    public string RiskLevel { get; set; } = string.Empty;
    public int ConfidenceLevel { get; set; }
    public string Logic { get; set; } = string.Empty;
    public decimal PositionSizing { get; set; }
}

public class MarketFlow
{
    public string DominantDirection { get; set; } = string.Empty; // "Backing", "Laying", "Neutral"
    public decimal BackingPressure { get; set; }
    public decimal LayingPressure { get; set; }
    public decimal RecentVolumeRatio { get; set; }
    public bool SteamMoveDetected { get; set; }
    public DateTime? LastSteamMove { get; set; }
    public string MarketSentiment { get; set; } = string.Empty;
}

public class PatternDetection
{
    public string PatternType { get; set; } = string.Empty;
    public decimal Confidence { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal TargetPrice { get; set; }
    public DateTime DetectedAt { get; set; }
}
