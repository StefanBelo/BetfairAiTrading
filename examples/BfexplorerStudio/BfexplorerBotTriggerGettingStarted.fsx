module BfexplorerBot

//(*
#I @"E:\Projects\Bfexplorer\Development\Applications\BeloSoft.Bfexplorer.App\bin\Debug\net10.0-windows\"
//#I @"C:\Program Files\BeloSoft\Bfexplorer\"

#r "DevExpress.Spreadsheet.v25.2.Core.dll"

#r "BeloSoft.Data.dll"
#r "BeloSoft.Betfair.API.dll"
#r "BeloSoft.Bfexplorer.Domain.dll"
#r "BeloSoft.Bfexplorer.Trading.dll"
#r "BeloSoft.Bfexplorer.Service.Core.dll"
//*)

open System

open BeloSoft.Bfexplorer.Domain
open BeloSoft.Bfexplorer.Trading
open BeloSoft.Bfexplorer.Service
    
/// <summary>
/// ShowTurnoverPerMinute
/// </summary>
type ShowTurnoverPerMinute (market : Market, selection : Selection, botName : string, botTriggerParameters : BotTriggerParameters, myBfexplorer : IMyBfexplorer) as this =
    inherit BotTriggerBase (market, selection, botName, botTriggerParameters, myBfexplorer)
         
    let timeSpanToCheck = defaultArg (botTriggerParameters.GetParameter<float> "TimeSpanToCheck") 1.0
    
    let mutable timeToCheck = DateTime.MinValue

    let canReport time =
        let status = time >= timeToCheck

        if status
        then
            timeToCheck <- time.AddMinutes timeSpanToCheck

        status

    let reportTurnover (time : DateTime) =
        let fromTime = time.AddMinutes -timeSpanToCheck

        getActiveSelections market
        |> List.map (fun selection ->
                let totalTradedVolumePerMinute =
                    selection.PriceTradedHistory.TradedPrices
                    |> Seq.filter (fun timePriceTraded -> timePriceTraded.Time >= fromTime)
                    |> Seq.sumBy (fun timePriceTraded -> timePriceTraded.Volume)

                sprintf "%s: %.2f" selection.Name totalTradedVolumePerMinute
            )
        |> String.concat "\n"
        |> this.Report

    interface IBotTrigger with

        /// <summary>
        /// Execute
        /// </summary>
        member _this.Execute () =
            let time = DateTime.Now

            if canReport time
            then
                reportTurnover time
            
            TriggerResult.WaitingForOperation
            
        /// <summary>
        /// EndExecution
        /// </summary>
        member _this.EndExecution () =
            ()

/// <summary>
/// MyTestStrategyBotTrigger
/// </summary>
type MyTestStrategyBotTrigger (market : Market, selection : Selection, botName : string, botTriggerParameters : BotTriggerParameters, myBfexplorer : IMyBfexplorer) as this =
    inherit BotTriggerBase (market, selection, botName, botTriggerParameters, myBfexplorer)
         
    let timeSpanToExecute = defaultArg (botTriggerParameters.GetParameter<float> "TimeSpanToExecute") 1.0
    
    let mutable aTimeToExecute : DateTime option = None

    let canExecute time =        
        match aTimeToExecute with
        | Some timeToExecute ->
            let status = timeToExecute >= time

            if status
            then
                aTimeToExecute <- None

            status

        | None -> aTimeToExecute <- Some (time.AddMinutes timeSpanToExecute); false

    let getMySelection (time : DateTime) =
        let fromTime = time.AddMinutes -timeSpanToExecute

        let mySelection, totalTradedVolume =
            getActiveSelections market
            |> List.map (fun selection ->
                    let totalTradedVolume =
                        selection.PriceTradedHistory.TradedPrices
                        |> Seq.filter (fun timePriceTraded -> timePriceTraded.Time >= fromTime)
                        |> Seq.sumBy (fun timePriceTraded -> timePriceTraded.Volume)

                    selection, totalTradedVolume
                )
            |> List.sortByDescending (fun (_selection, totalTradedVolume) -> totalTradedVolume)
            |> List.head

        if totalTradedVolume > 0.0
        then
            Some mySelection
        else
            None
        
    interface IBotTrigger with

        /// <summary>
        /// Execute
        /// </summary>
        member _this.Execute () =
            let time = DateTime.Now

            if canExecute time
            then
                match getMySelection time with
                | Some mySelection -> 

                    TriggerResult.ExecuteMyActionBotOnMarketSelectionAndContinueToExecute (
                            "Bet 10 Euro", 
                            market, mySelection, 
                            List.empty, 
                            false
                        )

                | None -> TriggerResult.EndExecutionWithMessage "Nothing has been traded!"
            else            
                TriggerResult.WaitingForOperation
            
        /// <summary>
        /// EndExecution
        /// </summary>
        member _this.EndExecution () =
            ()

(*
    Helpers
*)

let createIMyBfexplorer (market, iBfexplorerConsole : IBfexplorerConsole) =
    let iBfexplorerService = iBfexplorerConsole.BfexplorerService

    { 
        new IMyBfexplorer with
            
            member _.OpenBetEvent
                with get () = iBfexplorerService.Bfexplorer.GetOpenBetEvent market

            member _.BfexplorerService
                with get () = iBfexplorerService
    }

let setupMyBotTrigger (iBfexplorerConsole : IBfexplorerConsole) =
    let market = iBfexplorerConsole.ActiveMarket
    let selection = iBfexplorerConsole.ActiveSelection
    let botTriggerParameters = BotTriggerParameters (String.Empty, String.Empty)    
    let myBfexplorer = createIMyBfexplorer (market, iBfexplorerConsole)

    ShowTurnoverPerMinute (market, selection, "Show the turnover per minute", botTriggerParameters, myBfexplorer)

let setupMyTestStrategyBotTrigger (iBfexplorerConsole : IBfexplorerConsole) =
    let market = iBfexplorerConsole.ActiveMarket
    let selection = iBfexplorerConsole.ActiveSelection
    let botTriggerParameters = BotTriggerParameters (String.Empty, String.Empty)    
    let myBfexplorer = createIMyBfexplorer (market, iBfexplorerConsole)

    MyTestStrategyBotTrigger (market, selection, "My test strategy bot trigger", botTriggerParameters, myBfexplorer)    

let executeBotTrigger (botTrigger : IBotTrigger, iBfexplorerConsole : IBfexplorerConsole) =    
    let triggerResult = 
        let uiApplication = iBfexplorerConsole.BfexplorerService.UiApplication

        uiApplication.ExecuteAndReturn<TriggerResult> (fun () -> botTrigger.Execute ())
        |> Async.RunSynchronously

    printfn "%A" triggerResult