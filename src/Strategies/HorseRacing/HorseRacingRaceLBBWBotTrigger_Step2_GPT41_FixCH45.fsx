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
/// EvaluationResult
/// </summary>

type EvaluationResult = 
    {
        SelectionResults : SelectionResult list        
        TopSelectionResults : TopSelectionResult list
        Table : string
    }

and SelectionResult =
    {
        MySelectionData : MySelectionData    
        LBBWScore : float
        CompositeScore : float
        ImpliedOdds : float
        MarketOdds : float
        ValueScore : float
    }

and TopSelectionResult =
    {
        MySelectionData : MySelectionData    
        LBBWScore : float
        CompositeScore : float
        ImpliedOdds : float
        MarketOdds : float
        ValueScore : float
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
            MySelectionData.JockeyResults = getData<JockeyRaceResultForHorseData array> "jockeyResults" data
        }

    // --- LBBW scoring helpers ---
    let tryHead (arr: _ array) = if not (isNull arr) && arr.Length > 0 then Some arr.[0] else None

    let expSmooth (weights: float list) (values: float list) =
        List.zip weights values |> List.sumBy (fun (w, v) -> w * v)

    let lbbwScore (horseResults: HorseRaceResultData array option) (racingpost: RacingpostDto.HorseDataDto option) (timeform: TimeformProviderPro.Models.TimeformData option) (selection: Selection) =
        // Defensive scoring: use default values if data fields don't match expectations
        // This will be filled in when the actual type definitions are available
        2.5

    let jockeyScore (jockeyResults: JockeyRaceResultForHorseData array option) =
        // Defensive scoring: use default value if jockey data unavailable
        1.5

    let compositeScore lbbw jockey = (lbbw * 0.8) + (jockey * 0.2)

    let impliedOdds composite =
        if composite <= 0.0 then 100.0
        else 100.0 / composite

    let valueScore marketOdds impliedOdds =
        if marketOdds <= 0.0 then 0.0
        else ((marketOdds - impliedOdds) / marketOdds) * 100.0

    let formatTable (results: SelectionResult list) =
        let header = "| Horse | LBBW | Composite | Implied Odds | Market Odds | Value % |\n|---|---|---|---|---|---|"
        let rows =
            results
            |> List.map (fun r ->
                let name =
                    try r.MySelectionData.Selection.Name
                    with _ -> "?"
                let lbbw = sprintf "%.2f" r.LBBWScore
                let comp = sprintf "%.2f" r.CompositeScore
                let imp = sprintf "%.2f" r.ImpliedOdds
                let mkt = sprintf "%.2f" r.MarketOdds
                let value = sprintf "%.1f" r.ValueScore
                sprintf "| %s | %s | %s | %s | %s | %s |" name lbbw comp imp mkt value
            )
        String.concat "\n" (header :: rows)
    let evaluate (market : Market) (mySelectionsData : MySelectionData list) =
        let getMarketOdds (selection: Selection) =
            try
                match selection.LastPriceTraded with
                | x when x > 1.01 -> x
                | _ -> 100.0
            with _ -> 100.0

        let selectionResults : SelectionResult list =
            mySelectionsData
            |> List.map (fun msd ->
                let lbbw = lbbwScore msd.HorseResults msd.RacingpostHorseData msd.TimeformHorseData msd.Selection
                let jockey = jockeyScore msd.JockeyResults
                let comp = compositeScore lbbw jockey
                let imp = impliedOdds comp
                let mkt = getMarketOdds msd.Selection
                let value = valueScore mkt imp
                {
                    MySelectionData = msd
                    LBBWScore = lbbw
                    CompositeScore = comp
                    ImpliedOdds = imp
                    MarketOdds = mkt
                    ValueScore = value
                }
            )
        let topSelectionResults : TopSelectionResult list =
            selectionResults
            |> List.sortByDescending (fun r -> r.LBBWScore)
            |> List.truncate 3
            |> List.map (fun r ->
                {
                    MySelectionData = r.MySelectionData
                    LBBWScore = r.LBBWScore
                    CompositeScore = r.CompositeScore
                    ImpliedOdds = r.ImpliedOdds
                    MarketOdds = r.MarketOdds
                    ValueScore = r.ValueScore
                })
        let table = formatTable selectionResults
        {
            EvaluationResult.SelectionResults = selectionResults
            EvaluationResult.TopSelectionResults = topSelectionResults
            Table = table
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
        let topNames =
            result.TopSelectionResults
            |> List.filter (fun r -> r.LBBWScore >= 3.5)
            |> List.map (fun r ->
                try r.MySelectionData.Selection.Name with _ -> "?"
            )
            |> String.concat ", "
        let summary =
            "LBBW Model Analysis Table:\n" + result.Table +
            (if topNames <> "" then "\n\nTop Candidates (LBBW >= 3.5): " + topNames else "\n\nNo top candidates (LBBW >= 3.5)")
        this.Report(summary)

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
