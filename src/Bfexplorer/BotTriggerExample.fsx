// Bfexplorer cannot be held responsible for any losses or damages incurred during the use of this betfair bot.
// It is up to you to determine the level of risk you wish to trade under. 
// Do not gamble with money you cannot afford to lose.

module BotTriggerExample

#I @"C:\Program Files\BeloSoft\Bfexplorer\"
//#I @"E:\Projects\Bfexplorer\Development\Applications\BeloSoft.Bfexplorer.App\bin\Debug\net9.0-windows\"

#r "BeloSoft.Data.dll"
#r "BeloSoft.Bfexplorer.Domain.dll"
#r "BeloSoft.Bfexplorer.Service.Core.dll"
#r "BeloSoft.Bfexplorer.Trading.dll"

open BeloSoft.Bfexplorer.Domain
open BeloSoft.Bfexplorer.Trading

/// <summary>
/// BotTriggerExample
/// </summary>
type BotTriggerExample (market : Market, selection : Selection, botName : string, botTriggerParameters : BotTriggerParameters, myBfexplorer : IMyBfexplorer) =
    inherit BotTriggerBase (market, selection, botName, botTriggerParameters, myBfexplorer)

    interface IBotTrigger with

        /// <summary>
        /// Execute
        /// </summary>
        member this.Execute () =
            TriggerResult.EndExecution

        /// <summary>
        /// EndExecution
        /// </summary>
        member _this.EndExecution () =
            ()
