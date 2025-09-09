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
#r "BeloSoft.Bfexplorer.TennisScoreProvider.dll"

#r "DevExpress.Data.v25.1.dll"
#r "DevExpress.Office.v25.1.Core.dll"
#r "DevExpress.Spreadsheet.v25.1.Core.dll"
#r "DevExpress.Printing.v25.1.Core.dll"

open System
open System.Collections.Generic

open DevExpress.Spreadsheet
open DevExpress.XtraSpreadsheet

open BeloSoft.Data
open BeloSoft.Bfexplorer.Domain
open BeloSoft.Bfexplorer.Trading
open BeloSoft.Bfexplorer.TennisScoreProvider.Models

let marketRowIndexes = Dictionary<int, int> ()
let mutable lastRowIndex = 1

let resetRowIndex () =
    lock marketRowIndexes (fun () ->
        marketRowIndexes.Clear ()
        lastRowIndex <- 1
    )

let getMarketRowIndex (market : Market) =
    lock marketRowIndexes (fun () ->
        let marketId = market.MarketInfo.BetEvent.Id

        if marketRowIndexes.ContainsKey marketId
        then
            marketRowIndexes.[marketId]
        else
            let index = lastRowIndex
        
            marketRowIndexes.Add (marketId, index)
            lastRowIndex <- lastRowIndex + 2

            index
    )

/// <summary>
/// TriggerStatus
/// </summary>
type TriggerStatus =
    | Initialize
    | UpdateData

/// <summary>
/// TennisDataToSpreadsheet
/// </summary>
type TennisDataToSpreadsheet (market : Market, selection : Selection, botName : string, botTriggerParameters : BotTriggerParameters, myBfexplorer : IMyBfexplorer) =
    inherit BotTriggerBase (market, selection, botName, botTriggerParameters, myBfexplorer)

    let updateInterval = TimeSpan.FromSeconds (defaultArg (botTriggerParameters.GetParameter<float> "UpdateInterval") 2.0)

    let mutable tennisMatch = nil<TennisMatch>

    let mutable bfexplorerSpreadsheet = nil<ISpreadsheetControl>
    let mutable worksheet = nil<Worksheet>
    let mutable rowIndex = 1

    let mutable triggerStatus = TriggerStatus.Initialize
    let mutable timeToUpdate = DateTime.MinValue

    let isMyTennisMarket () =
        market.MarketInfo.BetEventType.Id = 2 && market.MarketDescription.MarketType = "MATCH_ODDS"

    let write value (row, column) =
        worksheet.[row, column].SetValue value

    let initialize () =
        tennisMatch <- toTennisMatch market

        worksheet <- bfexplorerSpreadsheet.Document.Worksheets.[0]

        if lastRowIndex = 1
        then
            [ 
                "Player Name"; "Score"; "Set 1"; "Set 2"; "Set 3"; "Set 4"; "Set 5"; "Back"; "Lay"
            ]
            |> List.iteri (fun column columnName -> (0, column) |> write columnName)

        if defaultArg (botTriggerParameters.GetParameter<bool> "Reset") false
        then
            resetRowIndex ()

        rowIndex <- getMarketRowIndex market

        market.Selections 
        |> Seq.iteri (fun index mySelection -> (rowIndex + index, 0) |> write mySelection.Name)

    let setTimeToUpdate () =
        timeToUpdate <- DateTime.Now.Add updateInterval

    let setSelectionPrice (selection : Selection, betType : BetType, index : int, column : int) =
        let priceValue =
            match selection.GetPrice betType with
            | Some price -> sprintf "%.2f" price
            | None -> String.Empty

        (rowIndex + index, column) |> write priceValue

    let updateMarketPrices () =
        market.Selections
        |> Seq.iteri (fun index mySelection ->
            setSelectionPrice (mySelection, BetType.Back, index, 7)
            setSelectionPrice (mySelection, BetType.Lay, index, 8)
        )

    let update () =
        try
            bfexplorerSpreadsheet.BeginUpdate ()

            if tennisMatch.IsUpdated
            then
                let homePlayer = tennisMatch.FirstPlayer
                let awayPlayer = tennisMatch.SecondPlayer

                (rowIndex, 1) |> write homePlayer.PointsWon
                (rowIndex + 1, 1) |> write awayPlayer.PointsWon

                (rowIndex, 2) |> write homePlayer.Set1Score
                (rowIndex + 1, 2) |> write awayPlayer.Set1Score

                (rowIndex, 3) |> write homePlayer.Set2Score
                (rowIndex + 1, 3) |> write awayPlayer.Set2Score

                (rowIndex, 4) |> write homePlayer.Set3Score
                (rowIndex + 1, 4) |> write awayPlayer.Set3Score

                (rowIndex, 5) |> write homePlayer.Set4Score
                (rowIndex + 1, 5) |> write awayPlayer.Set4Score

                (rowIndex, 6) |> write homePlayer.Set5Score
                (rowIndex + 1, 6) |> write awayPlayer.Set5Score

            updateMarketPrices ()
        finally
            bfexplorerSpreadsheet.EndUpdate ()

    interface IBotTrigger with

        /// <summary>
        /// Execute
        /// </summary>
        member _this.Execute () =
            match triggerStatus with
            | TriggerStatus.Initialize ->

                bfexplorerSpreadsheet <- myBfexplorer.BfexplorerService.Bfexplorer.BfexplorerSpreadsheet

                if isNullObj bfexplorerSpreadsheet
                then
                    TriggerResult.EndExecutionWithMessage "Open bfexplorer spreadsheet application!"
                else
                    if isMyTennisMarket ()
                    then
                        initialize ()

                        triggerStatus <- TriggerStatus.UpdateData

                        TriggerResult.WaitingForOperation
                    else
                        TriggerResult.EndExecutionWithMessage "You can execute this bot only on a tennis match odds market."
                                
            | TriggerStatus.UpdateData ->

                if DateTime.Now >= timeToUpdate
                then
                    setTimeToUpdate ()
                    update ()

                    TriggerResult.UpdateTennisMatchScore (tennisMatch, ignore)
                else
                    TriggerResult.WaitingForOperation

        /// <summary>
        /// EndExecution
        /// </summary>
        member _this.EndExecution () =
            ()