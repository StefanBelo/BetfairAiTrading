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

open BeloSoft.Bfexplorer.Domain
open BeloSoft.Bfexplorer.Service

/// <summary>
/// Execute
/// </summary>
/// <param name="bfexplorerConsole"></param>
let Execute (bfexplorerConsole : IBfexplorerConsole) =
    let mutable bookValue = 0.0

    let selectionsOutput =        
        getActiveSelections bfexplorerConsole.ActiveMarket
        |> List.map (fun selection -> 
                //let probability = toPriceProbability (selection.GetBestPrice BetType.Back)
                let probability = toPriceProbability selection.LastPriceTraded

                bookValue <- bookValue + probability

                sprintf "%s | %.2f | %.2f" selection.Name selection.LastPriceTraded probability
            )
        |> String.concat "\n"

    printf "%s\nMarket Book Value: %.2f" selectionsOutput bookValue
