#I @"C:\Program Files\BeloSoft\Bfexplorer\"

#r "BeloSoft.Data.dll"
#r "BeloSoft.Bfexplorer.Domain.dll"
#r "BeloSoft.Bfexplorer.Service.Core.dll"
#r "BeloSoft.Bfexplorer.Trading.dll"

open BeloSoft.Bfexplorer.Domain
open BeloSoft.Bfexplorer.Trading

[<AutoOpen>]
module BotTriggerDSL =

    type TriggerContext =
        {
            Market : Market
            Selection : Selection
            BotName : string
            Parameters : BotTriggerParameters
            MyBfexplorer : IMyBfexplorer
        }

    type Trigger<'a> = TriggerContext -> option<'a>

    type TriggerBuilder () =
        member _.Return (value : 'a) : Trigger<'a> = fun _ -> Some value
        member _.ReturnFrom (step : Trigger<'a>) : Trigger<'a> = step
        member _.Bind (step : Trigger<'a>, next : 'a -> Trigger<'b>) : Trigger<'b> =
            fun ctx ->
                match step ctx with
                | Some value -> next value ctx
                | None -> None

        member _.Zero () : Trigger<unit> = fun _ -> Some ()
        member _.Combine (first : Trigger<unit>, second : Trigger<'a>) : Trigger<'a> =
            fun ctx ->
                match first ctx with
                | Some () -> second ctx
                | None -> None

        member _.Delay (f : unit -> Trigger<'a>) : Trigger<'a> =
            fun ctx -> f () ctx

    let trigger = TriggerBuilder ()

    let param (name : string) (fallback : 'a) : Trigger<'a> =
        fun ctx ->
            ctx.Parameters.GetParameter<'a>(name)
            |> defaultArg <| fallback
            |> Some

    let favouriteSelectionInRange (fromPrice : float) (toPrice : float) : Trigger<Selection> =
        fun ctx ->
            getFavouriteSelections ctx.Market
            |> List.tryFind (fun mySelection ->
                let price = mySelection.LastPriceTraded
                price >= fromPrice && price <= toPrice
            )

    let toTriggerResult (step : Trigger<'a>) (onSome : 'a -> TriggerResult) (onNone : TriggerResult) : TriggerContext -> TriggerResult =
        fun ctx ->
            match step ctx with
            | Some value -> onSome value
            | None -> onNone

module BotTrigger =

    type MyStrategyBotTrigger (market : Market, selection : Selection, botName : string, botTriggerParameters : BotTriggerParameters, myBfexplorer : IMyBfexplorer) =
        inherit BotTriggerBase (market, selection, botName, botTriggerParameters, myBfexplorer)

        let ctx =
            {
                TriggerContext.Market = market
                TriggerContext.Selection = selection
                TriggerContext.BotName = botName
                TriggerContext.Parameters = botTriggerParameters
                TriggerContext.MyBfexplorer = myBfexplorer
            }

        let strategy =
            trigger {
                let! fromPrice = param "FromPrice" 2.5
                let! toPrice = param "ToPrice" 3.0
                let! mySelection = favouriteSelectionInRange fromPrice toPrice

                return mySelection
            }

        let runStrategy =
            toTriggerResult                                
                strategy
                TriggerResult.ExecuteActionBotOnSelection
                (TriggerResult.EndExecutionWithMessage "No selection fulfills my criteria.")                
                
        interface IBotTrigger with

            member _this.Execute () =
                runStrategy ctx

            member _this.EndExecution () =
                ()
