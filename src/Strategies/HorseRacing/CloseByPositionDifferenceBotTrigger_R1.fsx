// Bfexplorer cannot be held responsible for any losses or damages incurred during the use of this betfair bot.
// It is up to you to determine the level of risk you wish to trade under. 
// Do not gamble with money you cannot afford to lose.

module BfexplorerBot

#I @"C:\Program Files\BeloSoft\Bfexplorer\"

#r "BeloSoft.Data.dll"
#r "BeloSoft.Bfexplorer.Domain.dll"
#r "BeloSoft.Bfexplorer.Trading.dll"
#r "BeloSoft.Bfexplorer.Service.Core.dll"

open System.Collections.Generic

open BeloSoft.Data
open BeloSoft.Bfexplorer.Domain
open BeloSoft.Bfexplorer.Trading

/// <summary>
/// SelectionFavouriteData
/// </summary>
type SelectionFavouriteData =
    {
        Selection : Selection
        FavouriteIndex : int
        mutable CurrentFavouriteIndex : int
    }

    static member Create (selection, favouriteIndex) =
        {
            SelectionFavouriteData.Selection = selection
            SelectionFavouriteData.FavouriteIndex = favouriteIndex
            SelectionFavouriteData.CurrentFavouriteIndex = favouriteIndex
        }

    static member CreateSelectionFavouriteDatas (market : Market) =
        List (
            getFavouriteSelections market
            |> List.mapi (fun index selection -> selection, index + 1)
            |> List.filter (fun (selection, _favouriteIndex) -> selection.CanCloseBetPosition)
            |> Seq.map SelectionFavouriteData.Create
        )

/// <summary>
/// TriggerStatus
/// </summary>
type TriggerStatus =
    | Initialize
    | WaitToClosePosition

/// <summary>
/// CloseByPositionDifferenceBotTrigger
/// </summary>
type CloseByPositionDifferenceBotTrigger (market : Market, selection : Selection, botName : string, botTriggerParameters : BotTriggerParameters, myBfexplorer : IMyBfexplorer) as this =
    inherit BotTriggerBase (market, selection, botName, botTriggerParameters, myBfexplorer)

    let positionDifference = defaultArg (botTriggerParameters.GetParameter<int> "PositionDifference") 2
    let minimalFavouriteOdds = defaultArg (botTriggerParameters.GetParameter<float> "MinimalFavouriteOdds") 0.0
    let showPositionChanges = defaultArg (botTriggerParameters.GetParameter<bool> "ShowPositionChanges") false

    let mutable triggerStatus = TriggerStatus.Initialize
    let mutable mySelectionsDataToWatch = nil<List<SelectionFavouriteData>>

    let isMyHorseRacingMarket () =
        market.MarketInfo.BetEventType.Id = 7 && market.MarketDescription.MarketType = "WIN"

    let initialize () =        
        mySelectionsDataToWatch <- SelectionFavouriteData.CreateSelectionFavouriteDatas market

        mySelectionsDataToWatch.Count > 0

    let removeFromWatching mySelectionData =
        mySelectionsDataToWatch.Remove mySelectionData |> ignore

    let getSelectionsToClosePosition () =
        if mySelectionsDataToWatch.Count > 0
        then
            let favouriteSelections = getFavouriteSelections market
            let favouriteOdds = favouriteSelections.Head.LastPriceTraded

            let getFavouriteIndex (mySelectionData : SelectionFavouriteData) =
                let mySelectionIdentity = mySelectionData.Selection.Identity

                favouriteSelections 
                |> List.findIndex (fun mySelection -> mySelection.Identity = mySelectionIdentity)
                |> (+) 1

            let mutable mySelectionsDataToExecute = List.empty<SelectionFavouriteData>

            for mySelectionData in mySelectionsDataToWatch do
                let favouriteIndex = getFavouriteIndex mySelectionData
                let currentPositionDifference = favouriteIndex - mySelectionData.FavouriteIndex

                if showPositionChanges && mySelectionData.CurrentFavouriteIndex <> favouriteIndex
                then
                    mySelectionData.CurrentFavouriteIndex <- favouriteIndex

                    this.Report $"{favouriteIndex}. {mySelectionData.Selection} | {currentPositionDifference}"

                if favouriteOdds <= minimalFavouriteOdds || currentPositionDifference >= positionDifference
                then
                    mySelectionsDataToExecute <- mySelectionData :: mySelectionsDataToExecute

            mySelectionsDataToExecute |> List.iter removeFromWatching
            
            mySelectionsDataToExecute 
            |> List.map (fun mySelectionData -> mySelectionData.Selection)
            |> Some
        else
            None
                            
    interface IBotTrigger with

        /// <summary>
        /// Execute
        /// </summary>
        member _this.Execute () =
            match triggerStatus with
            | TriggerStatus.Initialize ->

                if isMyHorseRacingMarket ()
                then                   
                    if initialize ()
                    then
                        triggerStatus <- TriggerStatus.WaitToClosePosition

                        TriggerResult.WaitingForOperation
                    else
                        TriggerResult.EndExecution                        
                else
                    TriggerResult.EndExecutionWithMessage "You can execute this bot only on a horse racing market!"

            | TriggerStatus.WaitToClosePosition -> 

                match getSelectionsToClosePosition () with
                | Some executeOnSelections ->
                            
                    if executeOnSelections.IsEmpty
                    then
                        TriggerResult.WaitingForOperation
                    else
                        TriggerResult.ExecuteActionBotOnSelectionsAndContinueToExecute (executeOnSelections, true)

                | None -> TriggerResult.EndExecution

        /// <summary>
        /// EndExecution
        /// </summary>
        member _this.EndExecution () =
            ()