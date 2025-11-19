namespace BeloSoft.Bfexplorer.HorseRacingStatto.API.Models

open System

/// <summary>
/// RacingStattoData
/// </summary>
[<NoEquality; NoComparison>]
type RacingStattoData =
    {
        GoingRank : byte
        DistanceRank : byte
        GoingDistRank : byte

        CourseRank : byte

        TotalWinsRank : byte
        PercentageTotalRank : byte

        ORRank : byte
        TSRank : byte
        RPRRank : byte

        JockeyPercentageRank : byte
        TrainerPercentageRank : byte
    
        TimeRank : byte
        FastestTimeRank : byte
        Rank : byte
        AverageRank : float
    }

/// <summary>
/// RaceWinnerData
/// </summary>
[<NoEquality; NoComparison>]
type RaceData =
    {
        RaceTime : DateTime
        Racecourse : string
        Distance : string
        RaceClass : string
        NumberOfRunners : byte
    }

/// <summary>
/// RaceRunnerData
/// </summary>
[<NoEquality; NoComparison>]
type RaceRunnerData =
    {
        Name : string
        SpPrice : float
        Position : byte
        RacingStattoData : RacingStattoData
    }

    member this.IsWinner
        with get () = this.Position = 1uy

/// <summary>
/// RaceWinnerData
/// </summary>
[<NoEquality; NoComparison>]
type RaceWinnerData =
    {
        RaceData : RaceData
        Winner : RaceRunnerData
    }

/// <summary>
/// RaceResultData
/// </summary>
[<NoEquality; NoComparison>]
type RaceResultData =
    {
        RaceData : RaceData
        Runners : RaceRunnerData []
    }
