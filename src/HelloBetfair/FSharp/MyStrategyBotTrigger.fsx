module BotTrigger

#I @"C:\Program Files\BeloSoft\Bfexplorer\"

#r "BeloSoft.Data.dll"
#r "BeloSoft.Bfexplorer.Domain.dll"
#r "BeloSoft.Bfexplorer.Service.Core.dll"
#r "BeloSoft.Bfexplorer.Trading.dll"

open BeloSoft.Bfexplorer.Domain
open BeloSoft.Bfexplorer.Trading

type MyStrategyBotTrigger (market : Market, selection : Selection, botName : string, botTriggerParameters : BotTriggerParameters, myBfexplorer : IMyBfexplorer) =
    inherit BotTriggerBase (market, selection, botName, botTriggerParameters, myBfexplorer)

    let getMySelection () =
        let fromPrice = defaultArg (botTriggerParameters.GetParameter<float> ("FromPrice")) 2.5
        let toPrice = defaultArg (botTriggerParameters.GetParameter<float> ("ToPrice")) 3.0

        getFavouriteSelections market
        |> List.tryFind (fun mySelection -> 
                let price = mySelection.LastPriceTraded

                price >= fromPrice && price <= toPrice
            )

    interface IBotTrigger with

        member _this.Execute () =
            match getMySelection () with
            | Some mySelection -> TriggerResult.ExecuteActionBotOnSelection mySelection
            | None -> TriggerResult.EndExecutionWithMessage "No selection fulfills my criteria."

        member _this.EndExecution () =
            ()