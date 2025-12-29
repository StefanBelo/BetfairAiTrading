// Bfexplorer cannot be held responsible for any losses or damages incurred during the use of this betfair bot.
// It is up to you to determine the level of risk you wish to trade under. 
// Do not gamble with money you cannot afford to lose.

module BfexplorerScript

//#I @"C:\Program Files\BeloSoft\Bfexplorer\"
#I @"E:\Projects\Bfexplorer\Development\Applications\BeloSoft.Bfexplorer.App\bin\Debug\net10.0-windows\"

(*
//#r "nuget: Plotly.NET, 6.0.0-preview.1"
#r @"C:\Users\Stefan\.nuget\packages\dynamicobj\7.0.1\lib\netstandard2.0\DynamicObj.dll"
#r @"C:\Users\Stefan\.nuget\packages\giraffe.viewengine.strongname\2.0.0-alpha1\lib\netstandard2.0\Giraffe.ViewEngine.StrongName.dll"
#r @"C:\Users\Stefan\.nuget\packages\fable.core\4.3.0\lib\netstandard2.0\Fable.Core.dll"
#r @"C:\Users\Stefan\.nuget\packages\plotly.net\6.0.0-preview.1\lib\netstandard2.0\Plotly.NET.dll"
*)

#r "DynamicObj.dll"
#r "Giraffe.ViewEngine.StrongName.dll"
#r "Plotly.NET.dll"

#r "DevExpress.Spreadsheet.v25.2.Core.dll"

#r "BeloSoft.Data.dll"
#r "BeloSoft.Bfexplorer.Domain.dll"
#r "BeloSoft.Bfexplorer.Service.Core.dll"

#r "BeloSoft.Betfair.WebAPI.Selenium.dll"

open System
open System.Diagnostics
open System.IO

open Plotly.NET

open BeloSoft.Data
open BeloSoft.Bfexplorer.Domain
open BeloSoft.Bfexplorer.Service

open BeloSoft.Bfexplorer.Domain.Data
open BeloSoft.Betfair.WebAPI.Selenium.Models

let mapToTimePriceVolumes (selectionData : SelectionDataContext) =
    let status, data = selectionData.Data.TryGetValue "timePriceVolumes"

    if status
    then
        try
            Some (data :?> TimePriceVolume array)
        with
        | _ -> None
    else
        None

let createChart (market : Market, selection : Selection) (timePriceVolumes : TimePriceVolume []) = 
    task {
        // Extract data
        let times = timePriceVolumes |> Array.map (fun tpv -> tpv.Time)
        let prices = timePriceVolumes |> Array.map (fun tpv -> tpv.Price)
        //let volumes = timePriceVolumes |> Array.map (fun tpv -> tpv.Volume)

        // Create Plotly chart
        let chart =
            Chart.Line(
                x = times,
                y = prices
            )
            |> Chart.withXAxisStyle(
                TitleText = "Time",
                ShowGrid = true
            )
            |> Chart.withYAxisStyle(
                TitleText = "Price (Decimal Odds)",
                ShowGrid = true
            )
            |> Chart.withTitle($"{selection.Name} - {market.MarketFullName}")
            |> Chart.withSize(1200, 600)

        // Save chart to temp folder and open in browser
        let filePath = Path.Combine (Path.GetTempPath (), sprintf "favourite_chart_%s.html" (DateTime.Now.ToString "yyyyMMdd_HHmmss"))

        // Generate HTML and save
        let htmlContent = chart |> GenericChart.toChartHTML

        do! File.WriteAllTextAsync (filePath, htmlContent)

        // Open in default browser
        ProcessStartInfo (FileName = filePath, UseShellExecute = true)
        |> Process.Start 
        |> ignore

        return filePath, times, prices
    }
    |> Async.AwaitTask

/// <summary>
/// Execute
/// </summary>
/// <param name="bfexplorerConsole"></param>
let Execute (bfexplorerConsole : IBfexplorerConsole) =
    let bfexplorer = bfexplorerConsole.Bfexplorer

    let market = bfexplorerConsole.ActiveMarket
    let selection = getFavouriteSelections market |> List.head

    let report message =
        bfexplorer.OutputMessage message

    async {        
        match! bfexplorer.GetDataContextForMarketSelection ([| "MarketSelectionsPriceHistoryData" |], market, selection) with
        | DataResult.Success marketDataContext -> 

            let selectionData = marketDataContext.SelectionsData.Head

            match mapToTimePriceVolumes selectionData with
            | Some timePriceVolumes when timePriceVolumes.Length > 0 -> 
        
                do! report $"Chart data for {market.MarketFullName} | {selection.Name}"

                let! filePath, times, prices = createChart (market, selection) timePriceVolumes
                    
                // Report statistics
                do! report (
                    [
                        $"Chart saved: {filePath}"
                        $"Data points: {timePriceVolumes.Length}"
                        $"Price range: {Array.min prices:F2} - {Array.max prices:F2}"
                        $"Time range: {times |> Array.head} to {times |> Array.last}"
                    ]
                    |> String.concat "\n"
                )
                
            | _ -> 
            
                do! report $"Failed to map 'timePriceVolumes' data for {selection.Name}!"
            
        | DataResult.Failure errorMessage -> do! report $"Error: {errorMessage}!"
    }
    |> Async.RunSynchronously