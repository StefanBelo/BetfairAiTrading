#r "nuget: Plotly.NET"

#I @"E:\Projects\Bfexplorer\Development\Applications\BeloSoft.Bfexplorer.App\bin\Debug\net10.0-windows"

#r "BeloSoft.Data.dll"
#r "BeloSoft.Bfexplorer.Domain.dll"
#r "BeloSoft.Bfexplorer.Service.Core.dll"
#r "BeloSoft.Bfexplorer.AiTradingBots.Bot.dll"

open Plotly.NET
open System.Text.RegularExpressions

open BeloSoft.Data
open BeloSoft.Bfexplorer.Domain
open BeloSoft.Bfexplorer.Service

open BeloSoft.Bfexplorer.AiTradingBots
open BeloSoft.Bfexplorer.DataAnalysis.AiModels

let getFavouriteSelectionData (market : Market) = maybe {
    let! selectionsData = market.GetData<SelectionAiData list> "SelectionsData"

    return
        selectionsData 
        |> List.sortBy (fun selectionData -> selectionData.Price)
        |> List.head
}

let getSelectionDataByName name (market : Market) = maybe {
    let! selectionsData = market.GetData<SelectionAiData list> "SelectionsData"

    return!
        selectionsData 
        |> List.tryFind (fun selectionData -> selectionData.Selection.Name = name)
}
    
let showChart (market : Market, selectionData : SelectionAiData) = 
    let x, yProbabilities, yScores, yWomImbalances, yExecutionPressures = 
        let startTime = market.MarketInfo.StartTime    
        let dataSelectionAnalysis = selectionData.DataSelectionAnalysis

        let x =
            dataSelectionAnalysis 
            |> Seq.map (fun data -> (startTime - data.Time).TotalMinutes)
            |> Seq.toList

        let yProbabilities, yScores = 
            dataSelectionAnalysis 
            |> Seq.map (fun data -> 
                    100.0 / data.Price, data.Score 
                )
            |> Seq.toList        
            |> List.unzip

        let yWomImbalances, yExecutionPressures = 
            dataSelectionAnalysis 
            |> Seq.map (fun data -> 
                    let metrics = data.SelectionAnalysis.Metrics

                    metrics.WomImbalance, metrics.ExecutionPressure 
                )
            |> Seq.toList        
            |> List.unzip

        x, yProbabilities, yScores, yWomImbalances, yExecutionPressures

    //let combinedChartHtml =
    let chart =
        [
            Chart.Line (x, yProbabilities, Name = "Probability")
            [
                Chart.Line (x, yScores, Name = "Score")
                Chart.Line (x, [for _ in x -> 0.08], Name = "+0.08 Threshold") |> Chart.withLineStyle(Dash = StyleParam.DrawingStyle.Dash, Color = Color.fromKeyword ColorKeyword.Gray)
                Chart.Line (x, [for _ in x -> -0.08], Name = "-0.08 Threshold") |> Chart.withLineStyle(Dash = StyleParam.DrawingStyle.Dash, Color = Color.fromKeyword ColorKeyword.Gray)
            ] |> Chart.combine
            Chart.Line (x, yWomImbalances, Name = "WoM Imbalance")
            Chart.Line (x, yExecutionPressures, Name = "Execution Pressure")
        ]
        |> Chart.SingleStack()
        |> Chart.withLayout(Layout.init(Width = 1400, Height = 800, HoverMode = StyleParam.HoverMode.XUnified))
        |> Chart.withShape(
                LayoutObjects.Shape.init(
                    ShapeType = StyleParam.ShapeType.Rectangle,
                    Xref = "paper", X0 = 0.0, X1 = 1.0,
                    Yref = "y2", Y0 = -0.08, Y1 = 0.08,
                    FillColor = Color.fromKeyword ColorKeyword.LightGreen,
                    Opacity = 0.2,
                    Line = Line.init(Width = 0.0)
                )
            )
        |> Chart.withTitle(selectionData.Selection.Name)
        |> Chart.withXAxisStyle("Time (minutes)", ShowSpikes = true)
        |> Chart.withYAxisStyle("Probability", Id = StyleParam.SubPlotId.YAxis 1)
        |> Chart.withYAxisStyle("Score", Id = StyleParam.SubPlotId.YAxis 2)
        |> Chart.withYAxisStyle("WoM Imbalance", Id = StyleParam.SubPlotId.YAxis 3)
        |> Chart.withYAxisStyle("Execution Pressure", Id = StyleParam.SubPlotId.YAxis 4)
        //|> GenericChart.toEmbeddedHTML

    (*
    let readyHtml =                                                                                                                                                                 
        Regex.Replace(                                           
            combinedChartHtml,
            @"(renderPlotly_\w+)\(\);",
            """var render=$1;if(window.Plotly){render();}else{document.querySelector('script[src*="plotly"]').addEventListener('load',render);}""")  

    displayAs "text/html" readyHtml
    *)

    Chart.show chart

let execute (iBfexplorerConsole : IBfexplorerConsole) =
    let market = iBfexplorerConsole.ActiveMarket
    let selecttion = iBfexplorerConsole.ActiveSelection
    
    //getFavouriteSelectionData market
    getSelectionDataByName selecttion.Name market
    |> Option.iter (fun selectionData -> showChart (market, selectionData))
