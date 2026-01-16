# F# DSLs: what they are, why they matter, and how they improve BotTrigger strategies

A domain‑specific language (DSL) is a small, focused language that expresses the concepts of a particular problem area directly. In F#, DSLs are often embedded inside the host language using computation expressions, records, and functions. This approach gives you the clarity of a custom language while keeping full access to the F# type system, tooling, and libraries.

## Why use an F# DSL?

- **Express intent, not plumbing.** A DSL lets you write code that reads like the strategy you want to execute, not the mechanics of how to execute it.
- **Better composability.** Steps can be chained, reused, and combined safely, which helps avoid duplicated logic.
- **Stronger correctness.** Types guide you toward valid combinations and make invalid states harder to represent.
- **Improved testability.** Strategy steps can be evaluated separately, with deterministic inputs and outputs.

## How this helps with Betfair BotTrigger strategies

In BotTrigger workflows, `Execute()` is called on every market update. That means you want the per‑tick work to be minimal and the strategy definition to be clear and reusable.

By introducing a computation expression DSL, the strategy becomes a pipeline of *what* to do rather than *how* to do it each time. The example below defines:

- A `TriggerContext` that packages market and selection data once.
- A `Trigger<'a>` type that describes a step in the strategy.
- A computation expression builder (`trigger`) that sequences steps and short‑circuits when a step fails.

This leads to a clean separation between **strategy definition** and **execution**, and it allows you to reuse the same strategy pipeline across updates without rebuilding it each time.

## Building the DSL

Let's start by defining our core types and the computation expression builder:

```fsharp
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
```

The key insight here is that `Trigger<'a>` is just a function from `TriggerContext` to `option<'a>`. This simple definition gives us:

- **Composability**: Steps can be chained using `let!` in the computation expression.
- **Short‑circuiting**: If any step returns `None`, the pipeline stops immediately.
- **Pure functions**: Each step is deterministic and testable in isolation.

## Helper functions for common operations

Now we add helper functions that work within our DSL:

```fsharp
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
```

These helpers demonstrate how to:
- Extract parameters safely (`param`)
- Filter selections based on criteria (`favouriteSelectionInRange`)
- Convert our DSL result to Betfair's `TriggerResult` type (`toTriggerResult`)

## Putting it all together

Here's the complete BotTrigger implementation using our DSL:

```fsharp
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
```

Notice how the strategy reads like a high‑level description:
1. Get the `fromPrice` parameter (default 2.5)
2. Get the `toPrice` parameter (default 3.0)
3. Find a favourite selection in that price range
4. Return that selection

The **key performance benefit** is that `ctx`, `strategy`, and `runStrategy` are all constructed once when the trigger instance is created. Each time `Execute()` is called (on every market update), we simply run the pre‑built pipeline. No allocations, no rebuilding—just execution.

## Summary

An F# DSL lets you model Betfair strategies as small, composable building blocks. In the BotTrigger scenario, it improves readability, reduces repeated work per market update, and keeps the intent of the strategy front‑and‑center. The result is a more maintainable and performant strategy layer that scales as your trading logic grows.
