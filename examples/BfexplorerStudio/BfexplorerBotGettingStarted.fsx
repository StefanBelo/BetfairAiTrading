module BfexplorerBot

//(*
#I @"E:\Projects\Bfexplorer\Development\Applications\BeloSoft.Bfexplorer.App\bin\Debug\net10.0-windows\"
//#I @"C:\Program Files\BeloSoft\Bfexplorer\"

//#r "DevExpress.Spreadsheet.v25.2.Core.dll"

#r "BeloSoft.Data.dll"
#r "BeloSoft.Betfair.API.dll"
#r "BeloSoft.Bfexplorer.Domain.dll"
#r "BeloSoft.Bfexplorer.Trading.dll"
#r "BeloSoft.Bfexplorer.Service.Core.dll"
//*)

open System
open System.ComponentModel.DataAnnotations

open BeloSoft.Bfexplorer.Domain
open BeloSoft.Bfexplorer.Trading
open BeloSoft.Bfexplorer.Service

/// <summary>
/// MyTestStrategyBotParameters
/// </summary>
type MyTestStrategyBotParameters () =
    inherit BotParameters ()

    [<Required>]
    [<Display (GroupName = StrategyGroup, Description = StrategyNameDescription)>]
    member val StrategyName : string = String.Empty with get, set

    [<Display (GroupName = TimeGroup, Description = "Data retrieval and start time.")>]
    member val StartTimeSpan : TimeSpan = TimeSpan.FromMinutes 1.0 with get, set
    
/// <summary>
/// MyTestStrategyBot
/// </summary>
type MyTestStrategyBot (market : Market, parameters : MyTestStrategyBotParameters, bfexplorerService : IBfexplorerService) as this =
    inherit MarketBaseBot (market, parameters, bfexplorerService)

    do
        this.Status <- BotStatus.WaitingForEntryCriteria

    let getMySelection (time : DateTime) =
        let fromTime = time.Add -parameters.StartTimeSpan

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

    let startMyBot (selection : Selection) =
        let botName = parameters.StrategyName

        if not (String.IsNullOrEmpty botName)
        then
            let message =
                match bfexplorerService.CreateMyBot (botName, market, selection) with
                | Some (bot, _) ->

                    addBot bot market
                    sprintf "The strategy %s has been started." botName

                | None -> sprintf "Failed to create the strategy '%s'." botName

            this.OutputMessage message

    /// <summary>
    /// Execute
    /// </summary>
    override this.Execute () =
        match this.Status with
        | BotStatus.WaitingForEntryCriteria ->

            if this.EvaluateCriteria ()
            then
                this.Status <- BotStatus.WaitingForOperation

        | BotStatus.WaitingForOperation ->

            if this.CanExecuteTimeDelayOperation parameters.StartTimeSpan
            then
                getMySelection DateTime.Now |> Option.iter startMyBot

                this.Status <- BotStatus.ExecutionEnded

        | _ -> ()

let setupMyTestStrategyBot (iBfexplorerConsole : IBfexplorerConsole) =
    let market = iBfexplorerConsole.ActiveMarket    
    let myTestStrategyBotParameters = MyTestStrategyBotParameters (
            Name = "My test strategy bot",
            StrategyName = "Bet 10 Euro"
        )    
    let iBfexplorerService = iBfexplorerConsole.BfexplorerService
    
    let bot = MyTestStrategyBot (market, myTestStrategyBotParameters, iBfexplorerService)

    iBfexplorerService.UiApplication.Execute (fun () -> addBot bot market)