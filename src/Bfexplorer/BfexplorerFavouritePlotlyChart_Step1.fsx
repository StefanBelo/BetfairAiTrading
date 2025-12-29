// Bfexplorer cannot be held responsible for any losses or damages incurred during the use of this betfair bot.
// It is up to you to determine the level of risk you wish to trade under. 
// Do not gamble with money you cannot afford to lose.

module BfexplorerScript

#I @"C:\Program Files\BeloSoft\Bfexplorer\"
//#I @"E:\Projects\Bfexplorer\Development\Applications\BeloSoft.Bfexplorer.App\bin\Debug\net9.0-windows\"

#r "DevExpress.Spreadsheet.v25.2.Core.dll"

#r "BeloSoft.Data.dll"
#r "BeloSoft.Bfexplorer.Domain.dll"
#r "BeloSoft.Bfexplorer.Service.Core.dll"

#r "BeloSoft.Betfair.WebAPI.Selenium.dll"

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
            | Some timePriceVolumes -> 
        
                do! report $"Chart data for {market.MarketFullName} | {selection.Name}"

                // Add the code to show chart data with Plotly

            | None -> do! report $"Failed to map 'timePriceVolumes' data for {selection.Name}!"
            
        | DataResult.Failure errorMessage -> do! report $"Error: {errorMessage}!"
    }
    |> Async.RunSynchronously