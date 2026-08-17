// Bfexplorer cannot be held responsible for any losses or damages incurred during the use of this betfair bot.
// It is up to you to determine the level of risk you wish to trade under. 
// Do not gamble with money you cannot afford to lose.

module BfexplorerScript

#I @"C:\Program Files\BeloSoft\Bfexplorer\"
//#I @"E:\Projects\Bfexplorer\Development\Applications\BeloSoft.Bfexplorer.App\bin\Debug\net10.0-windows\"

#r "DevExpress.Spreadsheet.v25.2.Core.dll"

#r "BeloSoft.Data.dll"
#r "BeloSoft.Bfexplorer.Domain.dll"
#r "BeloSoft.Bfexplorer.Service.Core.dll"

open BeloSoft.Data
open BeloSoft.Bfexplorer.Service

/// <summary>
/// Execute
/// </summary>
/// <param name="bfexplorerConsole"></param>
let Execute (bfexplorerConsole : IBfexplorerConsole) =
    let report message =
        bfexplorerConsole.Bfexplorer.OutputMessage message

    async {        
        match! bfexplorerConsole.Bfexplorer.OpenMarket "1.261142591" with
        | DataResult.Success market -> do! report market.MarketFullName
        | DataResult.Failure errorMessage -> do! report errorMessage
    }
    |> Async.RunSynchronously