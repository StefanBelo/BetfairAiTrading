#r "nuget: Plotly.NET"

#I @"E:\Projects\Bfexplorer\Development\Applications\BeloSoft.Bfexplorer.App\bin\Debug\net10.0-windows"

#r "BeloSoft.Data.dll"
#r "BeloSoft.Bfexplorer.Domain.dll"
#r "BeloSoft.Bfexplorer.Service.Core.dll"
#r "BeloSoft.Bfexplorer.AiTradingBots.Bot.dll"

open Plotly.NET

open BeloSoft.Data
open BeloSoft.Bfexplorer.Domain
open BeloSoft.Bfexplorer.Service
open BeloSoft.Bfexplorer.AiTradingBots

let toBackSelection (market : Market) = maybe {
    let! selectionsData = market.GetData<SelectionCandleData list> "SelectionsData"

    let selectionsDataByScore = selectionsData |> List.sortByDescending (fun selectionData -> selectionData.Score)

    let selectionsDataToBack =
        selectionsDataByScore
        |> List.filter (fun selectionData -> 
                match selectionData.BetType with
                | Some betType -> betType = BetType.Back
                | None -> false
            )

    let mySelection =
        if selectionsDataToBack.IsEmpty
        then
            selectionsDataByScore.Head
        else
            selectionsDataToBack.Head

    if mySelection.IsWinner.HasValue
    then
        return mySelection
}

let showChart (mySelectionsData : (SelectionCandleData * float) list) = 
    let xs = [ 1 .. mySelectionsData.Length ]
    let ys = 
        mySelectionsData 
        |> List.map snd
        |> List.scan (+) 0.0
        |> List.tail        

    let pointLabels = 
        mySelectionsData 
        |> List.map (fun (selectionData, _profit) -> sprintf "%s ~ %.2f" selectionData.Selection.Name selectionData.Score)

    let chart =
        Chart.Line(
            xs,
            ys,
            Name = "Profit",
            MultiText = pointLabels,           // Labels per point
            ShowMarkers = true,
            TextPosition = StyleParam.TextPosition.TopCenter
        )
        |> Chart.withTitle("Backing the best-scored selection.")

    Chart.show chart

let execute (iBfexplorerConsole : IBfexplorerConsole) =
    let closedMarkets = iBfexplorerConsole.OpenMarkets |> List.filter isClosedMarket

    if closedMarkets.IsEmpty
    then
        printfn "No markets are closed."
    else
        let mySelectionsData =
            closedMarkets 
            |> List.choose toBackSelection
            |> List.map (fun selectionData ->
                    let profit =
                        if defaultNullableArg selectionData.IsWinner false
                        then
                            (selectionData.Price - 1.0) * 10.0
                        else
                            -10.0

                    selectionData, profit
                )

        showChart mySelectionsData
