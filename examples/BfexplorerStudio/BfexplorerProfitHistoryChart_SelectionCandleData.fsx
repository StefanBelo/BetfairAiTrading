#r "nuget: Plotly.NET"

#I @"E:\Projects\Bfexplorer\Development\Applications\BeloSoft.Bfexplorer.App\bin\Debug\net10.0-windows"

#r "BeloSoft.Data.dll"
#r "BeloSoft.Bfexplorer.Domain.dll"
#r "BeloSoft.Bfexplorer.Service.Core.dll"
#r "BeloSoft.Bfexplorer.AiTradingBots.Bot.dll"

open Plotly.NET
open System.Text.RegularExpressions

open BeloSoft.Bfexplorer.Domain
open BeloSoft.Bfexplorer.Service
open BeloSoft.Bfexplorer.AiTradingBots

let getSelectionNameID (market : Market) =
    match market.GetData<SelectionCandleData list> "SelectionsData" with
    | Some selectionsData ->
    
        selectionsData 
        |> List.find (fun selectionData -> selectionData.Selection.HaveMatchedBets)
        |> fun selectionData -> selectionData.ToString ()

    | None -> market.MarketFullName
    
let toChart (market : Market) = 
    let x, y = 
        let startTime = market.MarketInfo.StartTime
        
        market.ProfitHistory.Values 
        |> Seq.map (fun data -> (data.Time - startTime).TotalMinutes, data.Value)
        |> Seq.toList
        |> List.unzip

    Chart.Line (x, y, Name = getSelectionNameID market)

let execute (iBfexplorerConsole : IBfexplorerConsole) =
    let closedMarkets = iBfexplorerConsole.OpenMarkets |> List.filter (fun market -> isClosedMarket market && market.SettledProfit.HasValue)

    if not closedMarkets.IsEmpty
    then
        let combinedChartHtml =
            closedMarkets 
            |> List.map toChart
            |> Chart.combine
            |> Chart.withLayout(Layout.init(Width = 500, Height = 500))
            |> GenericChart.toEmbeddedHTML

        let readyHtml =                                                                                                                                                                 
            Regex.Replace(                                           
                combinedChartHtml,
                @"(renderPlotly_\w+)\(\);",
                """var render=$1;if(window.Plotly){render();}else{document.querySelector('script[src*="plotly"]').addEventListener('load',render);}""")  

        //displayAs "text/html" readyHtml
        ()
