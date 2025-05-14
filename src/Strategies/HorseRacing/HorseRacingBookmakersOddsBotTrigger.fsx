// Bfexplorer cannot be held responsible for any losses or damages incurred during the use of this betfair bot.
// It is up to you to determine the level of risk you wish to trade under. 
// Do not gamble with money you cannot afford to lose.

module BfexplorerBot

#I @"C:\Program Files\BeloSoft\Bfexplorer\"

#r "BeloSoft.Data.dll"
#r "BeloSoft.Betfair.API.dll"
#r "BeloSoft.Bfexplorer.Domain.dll"
#r "BeloSoft.Bfexplorer.Trading.dll"
#r "BeloSoft.Bfexplorer.Service.Core.dll"
#r "BeloSoft.Bfexplorer.AtTheRacesProvider.dll"

open System
open System.ComponentModel.DataAnnotations

open BeloSoft.Data
open BeloSoft.Bfexplorer.Domain
open BeloSoft.Bfexplorer.Trading
open BeloSoft.Bfexplorer.AtTheRacesProvider
open BeloSoft.Bfexplorer.AtTheRacesProvider.Models

/// <summary>
/// MySelectionData
/// </summary>
type MySelectionData =
    {
        Selection : Selection
        [<DisplayFormat (DataFormatString = "{0:n2}")>]
        MinimalPrice : float
        [<DisplayFormat (DataFormatString = "{0:n2}")>]
        AveragePrice : float
        [<DisplayFormat (DataFormatString = "{0:n2}")>]
        MaximalPrice : float
    }

    static member DataKey = "MySelectionDataBPD" 

    [<DisplayFormat (DataFormatString = "{0:n2}")>]
    member this.PriceDifference 
        with get () = (toPriceProbability this.AveragePrice) - (toPriceProbability this.Selection.LastPriceTraded)

    override this.ToString () = 
        sprintf "%s: %.2f ~ %.2f | %.2f | %.2f ~ %.2f" this.Selection.Name this.Selection.LastPriceTraded 
            this.MinimalPrice this.AveragePrice this.MaximalPrice 
            this.PriceDifference

    interface ISelection with

        member this.GetSelection () =
            this.Selection

    static member Create (selection : Selection, bookmakersOdds : float seq) =
        {
            Selection = selection
            MinimalPrice = bookmakersOdds |> Seq.min
            AveragePrice = bookmakersOdds |> Seq.average
            MaximalPrice = bookmakersOdds |> Seq.max
        }

    static member CreateMySelectionDatas (market : Market, horseRunnersBookmakersOdds : HorseRunnerBookmakersOdds list) =
        let getSelectionBookmakersPrices (selection : Selection) (horseRunnersBookmakersOdds : HorseRunnerBookmakersOdds list) = maybe {
            let! horseRunner = 
                let horseName = selection.Name
    
                horseRunnersBookmakersOdds |> List.tryFind (fun runner -> String.Compare (runner.Name, horseName, true) = 0)
            
            let bookmakersOdds = horseRunner.BookmakersOdds

            if bookmakersOdds.Count > 0
            then
                return bookmakersOdds
        }

        getActiveSelections market
        |> List.choose (fun selection -> 
                getSelectionBookmakersPrices selection horseRunnersBookmakersOdds
                |> Option.map (fun bookmakersOdds -> MySelectionData.Create (selection, bookmakersOdds))
            )
        |> List.sortByDescending (fun mySelectionData -> mySelectionData.PriceDifference)

/// <summary>
/// TriggerStatus
/// </summary>
type TriggerStatus =
    | Initialize
    | WaitToLoadBookmakersPrices
    | ExecuteActionBot
    | ReportError of string

/// <summary>
/// HorseRacingBookmakersOddsBotTrigger
/// </summary>
type HorseRacingBookmakersOddsBotTrigger (market : Market, selection : Selection, botName : string, botTriggerParameters : BotTriggerParameters, myBfexplorer : IMyBfexplorer) =
    inherit BotTriggerBase (market, selection, botName, botTriggerParameters, myBfexplorer)

    let mutable triggerStatus = TriggerStatus.Initialize
    let mutable mySelectionsData = nil<MySelectionData list>

    let isMyHorseRacingMarket () =
        market.MarketInfo.BetEventType.Id = 7 && market.MarketDescription.MarketType = "WIN"

    let setErrorStatus errorMessage =
        triggerStatus <- TriggerStatus.ReportError errorMessage

    let initialize () =        
        triggerStatus <- TriggerStatus.WaitToLoadBookmakersPrices

        let atTheRacesProvider = AtTheRacesProvider ()

        Async.StartWithContinuations (
            computation = atTheRacesProvider.GetRaceBookmakersOdds market.MarketInfo,
            continuation = (fun result ->                 
                match result with
                | DataResult.Success horseRunnersBookmakersOdds ->

                    mySelectionsData <- MySelectionData.CreateMySelectionDatas (market, horseRunnersBookmakersOdds)
                    triggerStatus <- TriggerStatus.ExecuteActionBot

                | DataResult.Failure errorMessage -> setErrorStatus errorMessage
            ),
            exceptionContinuation = (fun ex -> setErrorStatus ex.Message),
            cancellationContinuation = (fun ex -> setErrorStatus ex.Message)
        )
                        
    interface IBotTrigger with

        /// <summary>
        /// Execute
        /// </summary>
        member this.Execute () =
            match triggerStatus with
            | TriggerStatus.Initialize ->

                if isMyHorseRacingMarket ()
                then                   
                    initialize ()

                    TriggerResult.WaitingForOperation
                else
                    TriggerResult.EndExecutionWithMessage "You can execute this bot only on a horse racing market!"

            | TriggerStatus.WaitToLoadBookmakersPrices -> TriggerResult.WaitingForOperation

            | TriggerStatus.ExecuteActionBot ->
            
                market.SetData (MySelectionData.DataKey, mySelectionsData) 

                this.Report (
                    mySelectionsData
                    |> List.map (fun mySelectionData -> mySelectionData.ToString ())
                    |> String.concat "\n"
                )

                let allMySelectionData = this.GetMySelectionsBySelectionCriteria mySelectionsData

                if allMySelectionData.IsEmpty
                then
                    TriggerResult.EndExecution
                else
                    TriggerResult.ExecuteActionBotOnSelection allMySelectionData.Head.Selection

            | TriggerStatus.ReportError errorMessage -> TriggerResult.EndExecutionWithMessage errorMessage

        /// <summary>
        /// EndExecution
        /// </summary>
        member _this.EndExecution () =
            ()