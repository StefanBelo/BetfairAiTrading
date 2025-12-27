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
        RacingpostData : RacingpostDto.HorseDataDto option
        TimeformData : TimeformProviderPro.Models.TimeformData option
        HorseResults : HorseRaceResultData array option
        JockeyResults : JockeyRaceResultForHorseData array option
    }

/// <summary>
/// SelectionResult
/// </summary>
type SelectionResult =
    {
        MySelectionData : MySelectionData    
        // To be added
    }

/// <summary>
/// EvaluationResult
/// </summary>
type EvaluationResult = 
    {
        SelectionResults : SelectionResult list        
        TopSelectionResults : SelectionResult list
        // To be added
    }

    override this.ToString () = 
        // To be added
        ""

[<AutoOpen>]
module private Helpers =

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
            MySelectionData.RacingpostData = getData<RacingpostDto.HorseDataDto> "racingpostHorseData" data
            MySelectionData.TimeformData = getData<TimeformProviderPro.Models.TimeformData> "timeformHorseData" data
            MySelectionData.HorseResults = getData<HorseRaceResultData array> "horseResults" data
            MySelectionData.JockeyResults = getData<JockeyRaceResultForHorseData array> "horseResults" data
        }

    let evaluate (market : Market) (mySelectionsData : MySelectionData list) =
       // To be added
       let selectionResults = List.empty<SelectionResult>
       let topSelectionResults = List.empty<SelectionResult>

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

            | DataResult.Failure errorMessage -> status <- TriggerStatus.ReportError errorMessage

        }
        |> Async.Start

    interface IBotTrigger with

        /// <summary>
        /// Execute
        /// </summary>
        member this.Execute () =
            match status with
            | TriggerStatus.Initialize ->

                if isMyHorseRacingMarket ()
                then
                    status <- TriggerStatus.WaitForDataContext

                    retrieveDataContext ()

                    TriggerResult.WaitingForOperation
                else
                    TriggerResult.EndExecutionWithMessage "You can run this bot on a horse racing market only!"

            | TriggerStatus.WaitForDataContext -> TriggerResult.WaitingForOperation
            | TriggerStatus.ReportData result ->

                this.Report (result.ToString ())

                TriggerResult.EndExecution

            | TriggerStatus.ReportError errorMessage -> TriggerResult.EndExecutionWithMessage errorMessage

        /// <summary>
        /// EndExecution
        /// </summary>
        member _this.EndExecution () =
            ()
