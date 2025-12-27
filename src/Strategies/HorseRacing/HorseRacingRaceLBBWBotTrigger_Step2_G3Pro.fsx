// Bfexplorer cannot be held responsible for any losses or damages incurred during the use of this betfair bot.
// It is up to you to determine the level of risk you wish to trade under. 
// Do not gamble with money you cannot afford to lose.

module HorseRacingBotTrigger

#I @"C:\Program Files\BeloSoft\Bfexplorer\"
//#I @"E:\Projects\Bfexplorer\Development\Applications\BeloSoft.Bfexplorer.App\bin\Debug\net9.0-windows\"

#r "DevExpress.Spreadsheet.v25.1.Core.dll"

#r "BeloSoft.Data.dll"
#r "BeloSoft.Betfair.API.dll"

#r "BeloSoft.Bfexplorer.Domain.dll"
#r "BeloSoft.Bfexplorer.Service.Core.dll"
#r "BeloSoft.Bfexplorer.Trading.dll"

#r "BeloSoft.Bfexplorer.TimeformProviderPro.dll"
#r "BeloSoft.Bfexplorer.HorseRacingDatabase.dll"
#r "BeloSoft.Bfexplorer.HorseRacingDataProviders.dll"

open System
open System.Collections.Generic

open BeloSoft.Data
open BeloSoft.Bfexplorer
open BeloSoft.Bfexplorer.Domain
open BeloSoft.Bfexplorer.Domain.Data
open BeloSoft.Bfexplorer.Trading

open BeloSoft.Data.HorseRacing.Models
open BeloSoft.Bfexplorer.HorseRacingDataProviders

/// <summary>
/// MySelectionData
/// </summary>
type MySelectionData =
    {
        Selection : Selection
        RacingpostHorseData : RacingpostDto.HorseDataDto option
        TimeformHorseData : TimeformProviderPro.Models.TimeformData option
        HorseResults : HorseRaceResultData array option
        JockeyResults : JockeyRaceResultForHorseData array option
    }

/// <summary>
/// LBBWScore
/// </summary>
type LBBWScore = 
    {
        Last : float
        Best : float
        Base : float
        Weight : float
        Bonuses : float
        Total : float
    }

/// <summary>
/// EvaluationResult
/// </summary>
type EvaluationResult = 
    {
        SelectionResults : SelectionResult list        
        TopSelectionResults : TopSelectionResult list
    }

and SelectionResult =
    {
        MySelectionData : MySelectionData
        LBBW : LBBWScore
    }

and TopSelectionResult =
    {
        MySelectionData : MySelectionData
        LBBW : LBBWScore
        Rank : int
    }

[<AutoOpen>]
module Helpers =

    let getData<'T> (dataField : string) (data : Dictionary<string, obj>) = 
        try
            let status, myData = data.TryGetValue dataField

            if status
            then        
                Some (myData :?> 'T)
            else
                None
        with
        | _ -> None

    let mapToMySelectionData (selectionData : SelectionDataContext) =
        let data = selectionData.Data

        {
            MySelectionData.Selection = selectionData.Selection
            MySelectionData.RacingpostHorseData = getData<RacingpostDto.HorseDataDto> "racingpostHorseData" data
            MySelectionData.TimeformHorseData = getData<TimeformProviderPro.Models.TimeformData> "timeformHorseData" data
            MySelectionData.HorseResults = getData<HorseRaceResultData array> "horseResults" data
            MySelectionData.JockeyResults = getData<JockeyRaceResultForHorseData array> "horseResults" data
        }

    let getPositionScore (position : byte) =
        match position with
        | 1uy -> 1.0
        | p when p <= 3uy -> 0.7
        | p when p <= 5uy -> 0.4
        | _ -> 0.0

    let parseWeight (weightStr : string) =
        try
            if String.IsNullOrWhiteSpace weightStr then 0
            else
                let parts = weightStr.Split('-')
                if parts.Length = 2 then
                    (int parts.[0] * 14) + int parts.[1]
                else
                    int weightStr
        with
        | _ -> 0

    let getMetaData (key : string) (selection : Selection) =
        match selection.Data.TryFind key with
        | Some v -> string v
        | None -> ""

    let calculateLBBW (data : MySelectionData) (maxBestRprInField : int) (maxWeightInField : int) =
        // Last: Recent form
        let lastScore = 
            match data.HorseResults with
            | Some results when results.Length > 0 -> getPositionScore results.[0].Position
            | _ -> 0.0

        // Best: Peak RPR normalized against field
        let getRpr (r : HorseRaceResultData) = int r.RacingpostRating

        let bestRpr = 
            match data.HorseResults with
            | Some results -> 
                if results.Length > 0 then results |> Array.map getRpr |> Array.max else 0
            | None -> 0
        
        let bestScore = 
            if maxBestRprInField > 0 then (float bestRpr / float maxBestRprInField) else 0.0

        // Base: Consistency (0-3)
        let baseScore =
            match data.HorseResults with
            | Some results ->
                let recent = results |> Array.truncate 5
                let top3Count = recent |> Array.filter (fun r -> r.Position <= 3uy && r.Position > 0uy) |> Array.length
                (float top3Count / 5.0) * 3.0
            | None -> 0.0

        // Weight: 
        let weightVal = 
            let w = getMetaData "Weight" data.Selection
            try parseWeight w with | _ -> 0
        
        let weightScore = 
            if maxWeightInField > 0 && weightVal >= maxWeightInField then 1.0 // Top weight bonus
            else 0.5 // Baseline

        // Bonuses
        let mutable bonusScore = 0.0
        // Age bonus (Rapid Rise for 3yo)
        let age = 
            let a = getMetaData "Age" data.Selection
            try int a with | _ -> 0

        if age = 3 then bonusScore <- bonusScore + 0.5
        
        // Timeform adaptability
        match data.TimeformHorseData with
        | Some tf ->
            if tf.SuitedByDistance then bonusScore <- bonusScore + 0.2
            // SuitedByGoing and SuitedByCourse not available in TimeformData
            if tf.TimeformTopRated then bonusScore <- bonusScore + 0.2
        | None -> ()

        {
            Last = Math.Round(lastScore, 2)
            Best = Math.Round(bestScore, 2)
            Base = Math.Round(baseScore, 2)
            Weight = Math.Round(weightScore, 2)
            Bonuses = Math.Round(bonusScore, 2)
            Total = Math.Round(lastScore + bestScore + baseScore + weightScore + bonusScore, 2)
        }

    let evaluate (market : Market) (mySelectionsData : MySelectionData list) =
       // Pre-calculate field maxs
       let allRprs = 
            mySelectionsData 
            |> List.collect (fun s -> 
                match s.HorseResults with 
                | Some res -> res |> Array.map (fun r -> int r.RacingpostRating) |> Array.toList
                | None -> [])
       let maxBestRprInField = if allRprs.Length > 0 then List.max allRprs else 0

       let allWeights = 
            mySelectionsData 
            |> List.map (fun s -> 
                let w = getMetaData "Weight" s.Selection
                try parseWeight w with | _ -> 0)
       let maxWeightInField = if allWeights.Length > 0 then List.max allWeights else 0

       let selectionResults = 
            mySelectionsData
            |> List.map (fun s -> 
                {
                    SelectionResult.MySelectionData = s
                    SelectionResult.LBBW = calculateLBBW s maxBestRprInField maxWeightInField
                })
       
       let topSelectionResults = 
            selectionResults
            |> List.sortByDescending (fun s -> s.LBBW.Total)
            |> List.mapi (fun i s -> 
                {
                    TopSelectionResult.MySelectionData = s.MySelectionData
                    TopSelectionResult.LBBW = s.LBBW
                    TopSelectionResult.Rank = i + 1
                })

       {
            EvaluationResult.SelectionResults = selectionResults
            EvaluationResult.TopSelectionResults = topSelectionResults
       }

/// <summary>
/// TriggerStatus
/// </summary>
type TriggerStatus =
    | Initialize
    | WaitForDataContext
    | ReportData of EvaluationResult
    | ReportError of string

/// <summary>
/// HorseRacingRaceLBBWBotTrigger
/// </summary>
type HorseRacingRaceLBBWBotTrigger (market : Market, selection : Selection, botName : string, botTriggerParameters : BotTriggerParameters, myBfexplorer : IMyBfexplorer) as this =
    inherit BotTriggerBase (market, selection, botName, botTriggerParameters, myBfexplorer)

    let mutable status = TriggerStatus.Initialize

    let isMyHorseRacingMarket () =
        market.MarketInfo.BetEventType.Id = 7 && market.MarketDescription.MarketType = "WIN"

    let retrieveDataContext () =
        async {
            let bfexplorer = myBfexplorer.BfexplorerService.Bfexplorer

            match! bfexplorer.GetDataContextForMarket ([| "DbJockeysResults"; "DbHorsesResults"; "RacingpostDataForHorses"; "TimeformDataForHorses" |], market) with
            | DataResult.Success marketDataContext -> 

                let result = evaluate market (marketDataContext.SelectionsData |> List.map mapToMySelectionData)

                status <- TriggerStatus.ReportData result

            | DataResult.Failure errorMessage -> 
            
                status <- TriggerStatus.ReportError errorMessage

        }
        |> Async.Start

    let doReportData (result : EvaluationResult) =
        let sb = System.Text.StringBuilder()
        sb.AppendLine("| Rank | Horse | Total | Last | Best | Base | Weight | Bonuses |") |> ignore
        sb.AppendLine("|---|---|---|---|---|---|---|---|") |> ignore
        
        for item in result.TopSelectionResults do
            let s = item.MySelectionData.Selection
            let l = item.LBBW
            sb.AppendFormat("| {0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} |", 
                item.Rank, s.Name, l.Total, l.Last, l.Best, l.Base, l.Weight, l.Bonuses) |> ignore
            sb.AppendLine() |> ignore
            
        this.Report (sb.ToString())

    interface IBotTrigger with

        /// <summary>
        /// Execute
        /// </summary>
        member _this.Execute () =
            match status with
            | TriggerStatus.Initialize ->

                if isMyHorseRacingMarket ()
                then
                    retrieveDataContext ()

                    status <- TriggerStatus.WaitForDataContext

                    TriggerResult.WaitingForOperation
                else
                    TriggerResult.EndExecutionWithMessage "You can run this bot on a horse racing market only!"

            | TriggerStatus.WaitForDataContext -> TriggerResult.WaitingForOperation
            | TriggerStatus.ReportData result ->

                doReportData result

                TriggerResult.EndExecution

            | TriggerStatus.ReportError errorMessage -> TriggerResult.EndExecutionWithMessage errorMessage

        /// <summary>
        /// EndExecution
        /// </summary>
        member _this.EndExecution () =
            ()
