namespace BeloSoft.AnalyzeResults.Domain

/// <summary>
/// Feature
/// </summary>
type Feature =
    | SpPrice
    | GoingRank
    | DistanceRank
    | GoingDistRank
    | CourseRank
    | TotalWinsRank
    | PercentageTotalRank
    | ORRank
    | TSRank
    | RPRRank
    | JockeyPercentageRank
    | TrainerPercentageRank
    | Rank

/// <summary>
/// Rule
/// </summary>
type Rule =
    | ByteAtLeast of Feature * byte
    | ByteAtMost of Feature * byte
    | FloatAtLeast of Feature * float
    | FloatAtMost of Feature * float
    | And of Rule list
    | Or of Rule list

/// <summary>
/// SearchConfig
/// </summary>
type SearchConfig =
    {
        MaxRules: int
        MaxCandidates: int
        MinWinCoverage: float
    }

/// <summary>
/// BettingStrategy
/// </summary>
type BettingStrategy =
    {
        Weights: Map<Feature, float>
        Threshold: float
    }

/// <summary>
/// StrategyResult
/// </summary>
type StrategyResult =
    {
        Strategy: BettingStrategy
        Profit: float
        BetsPlaced: int
        WinningBets: int
        ROI: float
    }

/// <summary>
/// FeatureStats
/// </summary>
type FeatureStats =
    {
        Mean: float
        Std: float
    }

