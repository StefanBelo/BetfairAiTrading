namespace BetfairMarketAnalyzerFSharp.Models

open System
open Skender.Stock.Indicators

type BetfairQuote = {
    Date: DateTime
    Open: decimal
    High: decimal
    Low: decimal
    Close: decimal
    Volume: decimal
} with
    interface IQuote with
        member this.Date with get() = this.Date
        member this.Open with get() = this.Open
        member this.High with get() = this.High
        member this.Low with get() = this.Low
        member this.Close with get() = this.Close
        member this.Volume with get() = this.Volume

[<CLIMutable>]
type SupportResistanceLevel = {
    Level: decimal
    TouchCount: int
    TotalVolume: decimal
    Strength: string
    LastTouch: DateTime
}

[<CLIMutable>]
type VolumeAnalysis = {
    Time: DateTime
    Price: decimal
    Volume: decimal
    Type: string // "Spike", "SteamMove", "Normal"
    Direction: string // "Backing", "Laying"
}

[<CLIMutable>]
type TechnicalIndicators = {
    RSI: decimal option
    SMA5: decimal option
    SMA10: decimal option
    EMA5: decimal option
    EMA10: decimal option
    MACD: decimal option
    MACDSignal: decimal option
    BollingerUpper: decimal option
    BollingerLower: decimal option
    BollingerMiddle: decimal option
    MomentumDirection: string
    VolumeMovingAverage: decimal
}

[<CLIMutable>]
type TradingRecommendation = {
    Strategy: string
    Action: string // "Back", "Lay", "Back-to-Lay", "Lay-to-Back"
    EntryPrice: decimal
    TargetPrice: decimal
    StopLoss: decimal
    ExpectedProfit: decimal
    RiskReward: decimal
    TimeFrame: string
    RiskLevel: string
    ConfidenceLevel: int
    Logic: string
    PositionSizing: decimal
}

[<CLIMutable>]
type MarketFlow = {
    DominantDirection: string // "Backing", "Laying", "Neutral"
    BackingPressure: decimal
    LayingPressure: decimal
    RecentVolumeRatio: decimal
    SteamMoveDetected: bool
    LastSteamMove: DateTime option
    MarketSentiment: string
}

[<CLIMutable>]
type PatternDetection = {
    PatternType: string
    Confidence: decimal
    Description: string
    TargetPrice: decimal
    DetectedAt: DateTime
}

[<CLIMutable>]
type AnalysisResult = {
    SelectionName: string
    CurrentOdds: decimal
    OpeningOdds: decimal
    ImpliedProbability: decimal
    OddsMovement: decimal
    OddsMovementPercentage: decimal
    TrendDirection: string
    TotalVolume: decimal
    SupportLevels: SupportResistanceLevel list
    ResistanceLevels: SupportResistanceLevel list
    VolumeSpikes: VolumeAnalysis list
    Indicators: TechnicalIndicators
    TradingRecommendations: TradingRecommendation list
    MarketFlow: MarketFlow
    AnalysisTime: DateTime
    TimeToRaceStart: TimeSpan
}
