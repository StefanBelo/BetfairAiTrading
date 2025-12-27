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
open BeloSoft.Bfexplorer.HorseRacingDataProviders

/// <summary>
/// MySelectionData
/// </summary>
type MySelectionData =
    {
        Selection : Selection
        RacingpostHorseData : RacingpostDto.HorseDataDto option
        TimeformHorseData : TimeformProviderPro.Models.TimeformData option
        HorseResults : HorseRacing.Models.HorseRaceResultData array option
        JockeyResults : HorseRacing.Models.JockeyRaceResultForHorseData array option
    }

/// <summary>
/// EvaluationResult
/// </summary>
type EvaluationResult = 
    {
        SelectionResults : SelectionResult list        
        TopSelectionResults : SelectionResult list
    }

    member this.ToString() =
        let sb = System.Text.StringBuilder()
        sb.AppendLine("Selection Results:") |> ignore
        for sr in this.SelectionResults do
            sb.AppendLine(sprintf "%s: Last %.2f, Best %.2f, Base %.2f, Weight %.2f, Bonus %.2f, LBBW %.2f" sr.MySelectionData.Selection.Name sr.LastScore sr.BestScore sr.BaseScore sr.WeightScore sr.BonusScore sr.LBBWScore) |> ignore
        sb.AppendLine("Top Selections:") |> ignore
        for tsr in this.TopSelectionResults do
            sb.AppendLine(sprintf "%s: LBBW %.2f" tsr.MySelectionData.Selection.Name tsr.LBBWScore) |> ignore
        sb.ToString()

and SelectionResult =
    {
        MySelectionData : MySelectionData    
        LastScore : float
        BestScore : float
        BaseScore : float
        WeightScore : float
        BonusScore : float
        LBBWScore : float
    }

[<AutoOpen>]
module internal Helpers =

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
            MySelectionData.HorseResults = getData<HorseRacing.Models.HorseRaceResultData array> "horseResults" data
            MySelectionData.JockeyResults = getData<HorseRacing.Models.JockeyRaceResultForHorseData array> "horseResults" data
        }

    let calculateLastScore (myData : MySelectionData) =
        match myData.HorseResults with
        | Some results when results.Length > 0 ->
            let lastRace = results.[0]
            if lastRace.Position = 1uy then 1.0
            elif lastRace.Position <= 3uy then 0.5
            else 0.0
        | _ -> 0.0

    let calculateBestScore (myData : MySelectionData) =
        match myData.HorseResults with
        | Some results when results.Length > 0 ->
            let lastRaces = results |> Array.take (min 5 results.Length)
            let maxRating = lastRaces |> Array.map (fun r -> r.OfficialRating) |> Array.max
            float maxRating / 100.0
        | _ -> 0.0

    let calculateBaseScore (myData : MySelectionData) =
        match myData.HorseResults with
        | Some results when results.Length > 0 ->
            let lastRaces = results |> Array.take (min 5 results.Length)
            let top3Count = lastRaces |> Array.filter (fun r -> r.Position <= 3uy) |> Array.length
            float top3Count / float lastRaces.Length * 3.0
        | _ -> 0.0

    let calculateWeightScore (myData : MySelectionData) =
        match myData.TimeformHorseData with
        | Some timeform ->
            if timeform.SuitedByGoing && timeform.SuitedByCourse && timeform.SuitedByDistance then 1.0 else 0.0
        | _ -> 0.0

    let calculateBonusScore (myData : MySelectionData) =
        match myData.TimeformHorseData with
        | Some timeform ->
            if timeform.TimeformImprover then 1.0 else 0.0
        | _ -> 0.0

    let evaluate (market : Market) (mySelectionsData : MySelectionData list) =
        let selectionResults = 
            mySelectionsData |> List.map (fun myData ->
                let last = calculateLastScore myData
                let best = calculateBestScore myData
                let baseScore = calculateBaseScore myData
                let weight = calculateWeightScore myData
                let bonus = calculateBonusScore myData
                let lbbw = last + best + baseScore + weight + bonus
                {
                    SelectionResult.MySelectionData = myData
                    LastScore = last
                    BestScore = best
                    BaseScore = baseScore
                    WeightScore = weight
                    BonusScore = bonus
                    LBBWScore = lbbw
                }
            )
        let topSelectionResults = 
            selectionResults 
            |> List.sortByDescending (fun sr -> sr.LBBWScore)
            |> List.take 3
            |> List.map (fun sr ->
                {
                    SelectionResult.MySelectionData = sr.MySelectionData
                    LastScore = sr.LastScore
                    BestScore = sr.BestScore
                    BaseScore = sr.BaseScore
                    WeightScore = sr.WeightScore
                    BonusScore = sr.BonusScore
                    LBBWScore = sr.LBBWScore
                }
            )
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
        this.Report (result.ToString ())

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
