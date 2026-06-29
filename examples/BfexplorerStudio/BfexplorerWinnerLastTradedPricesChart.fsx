#r "nuget: Plotly.NET"

#I @"E:\Projects\Bfexplorer\Development\Applications\BeloSoft.Bfexplorer.App\bin\Debug\net10.0-windows"

#r "BeloSoft.Data.dll"
#r "BeloSoft.Bfexplorer.Domain.dll"
#r "BeloSoft.Bfexplorer.Service.Core.dll"

open Plotly.NET

open BeloSoft.Bfexplorer.Domain
open BeloSoft.Bfexplorer.Service

let toChart (market : Market) = 
    match market.Selections |> Seq.tryFind (fun selection -> selection.Status = SelectionStatus.Winner) with
    | Some selection ->
        
        let x, y = 
            let startTime = market.MarketInfo.StartTime
            
            selection.PriceTradedHistory.TradedPrices
            |> Seq.map (fun data -> (data.Time - startTime).TotalMinutes, data.Price)
            |> Seq.toList
            |> List.unzip

        Some (Chart.Line (x, y, Name = selection.Name))

    | None -> None

let execute (iBfexplorerConsole : IBfexplorerConsole) =
    let closedMarkets = iBfexplorerConsole.OpenMarkets |> List.filter isClosedMarket

    if closedMarkets.IsEmpty
    then
        printfn "No markets are closed."
    else
        closedMarkets 
        |> List.choose toChart
        |> Chart.combine
        |> Chart.withLayout(Layout.init(Width = 1200, Height = 600))
        |> Chart.show