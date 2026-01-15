---
title: "Hello Betfair: The Same Strategy in F#, C#, VB.NET, and an AI Prompt (bfexplorer)"
published: false
description: One simple bfexplorer strategy implemented four ways: direct F# scripting, compiled C#/VB.NET DLLs, and an AI/MCP prompt.
tags: betfair, fsharp, csharp, dotnet, automation
cover_image:
---

# Hello Betfair: The Same Strategy in F#, C#, VB.NET, and an AI Prompt (bfexplorer)

bfexplorer lets you automate trading strategies in a few different “shapes”:

- **F# script (or DLL)**: run a BotTrigger directly from `.fsx` source (and yes, you can also compile F# to a DLL)
- **C# / VB.NET plugin**: compile a DLL and load it into bfexplorer
- **AI prompt**: describe the logic in natural language and let an LLM execute it via bfexplorer’s MCP integration

This post uses the same “Hello Betfair” strategy idea in all approaches with one main goal: **help you see the code differences** (syntax, interop, and packaging) while keeping the trading logic constant.

## The strategy logic

From the repo’s HelloBetfair overview, the core rule is:

- Place a back bet (or execute an action bot) if a selection price is within a given range.

In the code examples below the default range is **2.5 to 3.0** (inclusive). The trigger looks for a suitable selection (favourites in these examples) and either:

- executes an action on that selection, or
- ends with a message if nothing matches.

## Where the examples live

- Overview: ../../src/HelloBetfair/README.md
- F# script: ../../src/HelloBetfair/FSharp/MyStrategyBotTrigger.fsx
- C# trigger: ../../src/HelloBetfair/CSharp/MyStrategyBotTrigger/MyStrategyBotTrigger.cs
- VB.NET trigger: ../../src/HelloBetfair/VisualBasic/MyStrategyBotTrigger/MyStrategyBotTrigger.vb
- AI prompt: ../../src/HelloBetfair/AI/MyStrategy.md

## 1) F# — shortest code, runs from source

bfexplorer is built with F#, and it supports **F# scripting**. That means you can execute a BotTrigger directly from the `.fsx` file.

If you prefer a compiled workflow, you can also package an F# trigger as a DLL assembly (similar to the C#/VB.NET examples). The nice part is that you get to choose: script for iteration speed, DLL for deployment/versioning.

Here’s the HelloBetfair F# example:

```fsharp
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
        let fromPrice = defaultArg (botTriggerParameters.GetParameter<float> ("fromPrice")) 2.5
        let toPrice = defaultArg (botTriggerParameters.GetParameter<float> ("toPrice")) 3.0

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
```

What I like about this one:

- **No DLL build**: iterate fast
- the selection filtering reads cleanly (`tryFind` + pattern matching)
- parameter defaults are one-liners (`defaultArg ... 2.5`)

## 2) C# — same logic, packaged as a DLL

In C#, you implement the same `IBotTrigger` interface, but you compile the project to a DLL.

HelloBetfair C# trigger:

```csharp
using BeloSoft.Bfexplorer.Domain;
using BeloSoft.Bfexplorer.Trading;
using Microsoft.FSharp.Core;

namespace MyStrategyBotTrigger
{
    public class MyStrategyBotTrigger(Market market, Selection selection, string botName, BotTriggerParameters botTriggerParameters, IMyBfexplorer myBfexplorer) 
        : IBotTrigger
    {
        private (double FromPrice, double ToPrice) GetParameters()
        {
            var fromPriceOption = botTriggerParameters.GetParameter<double>("FromPrice");
            var toPriceOption = botTriggerParameters.GetParameter<double>("ToPrice");

            var fromPrice = FSharpOption<double>.get_IsSome(fromPriceOption) ? fromPriceOption.Value : 2.5;
            var toPrice = FSharpOption<double>.get_IsSome(toPriceOption) ? toPriceOption.Value : 3.0;

            return (fromPrice, toPrice);
        }

        private Selection GetMySelection()
        {
            var (fromPrice, toPrice) = GetParameters();

            return MarketExtensionsModule.getFavouriteSelections(market)
                .FirstOrDefault(s => s.LastPriceTraded >= fromPrice && s.LastPriceTraded <= toPrice);
        }

        public TriggerResult Execute()
        {
            var mySelection = GetMySelection();
            
            if (mySelection != null)
            {
                return TriggerResult.NewExecuteActionBotOnSelection(mySelection);
            }
            else
            {
                return TriggerResult.NewEndExecutionWithMessage("No selection fulfills my criteria.");
            }
        }

        public void EndExecution()
        {
        }
    }
}
```

Two very “real-world” details show up here:

- bfexplorer APIs expose F# `option`, so you’ll see `FSharpOption<T>` interop
- the parameter names are `FromPrice` / `ToPrice` (PascalCase)

## 3) Visual Basic — same DLL workflow, different syntax

VB.NET is functionally the same as the C# approach: you build a DLL and load it.

HelloBetfair VB.NET trigger:

```vb
Imports BeloSoft.Bfexplorer.Domain
Imports BeloSoft.Bfexplorer.Trading
Imports Microsoft.FSharp.Core

Public Class MyStrategyBotTrigger
    Implements IBotTrigger

    Private ReadOnly _market As Market
    Private ReadOnly _selection As Selection
    Private ReadOnly _botName As String
    Private ReadOnly _botTriggerParameters As BotTriggerParameters
    Private ReadOnly _myBfexplorer As IMyBfexplorer

    Public Sub New(market As Market, selection As Selection, botName As String, botTriggerParameters As BotTriggerParameters, myBfexplorer As IMyBfexplorer)
        _market = market
        _selection = selection
        _botName = botName
        _botTriggerParameters = botTriggerParameters
        _myBfexplorer = myBfexplorer
    End Sub

    Private Function GetParameters() As (FromPrice As Double, ToPrice As Double)
        Dim fromPriceOption = _botTriggerParameters.GetParameter(Of Double)("FromPrice")
        Dim toPriceOption = _botTriggerParameters.GetParameter(Of Double)("ToPrice")

        Dim fromPrice = If(OptionModule.IsSome(fromPriceOption), fromPriceOption.Value, 2.5)
        Dim toPrice = If(OptionModule.IsSome(toPriceOption), toPriceOption.Value, 3.0)

        Return (fromPrice, toPrice)
    End Function

    Private Function GetMySelection() As Selection
        Dim parameters = GetParameters()

        Dim fromPrice = parameters.FromPrice
        Dim toPrice = parameters.ToPrice

        Return MarketExtensionsModule.getFavouriteSelections(_market) _
            .FirstOrDefault(Function(s) s.LastPriceTraded >= fromPrice AndAlso s.LastPriceTraded <= toPrice)
    End Function

    Public Function Execute() As TriggerResult Implements IBotTrigger.Execute
        Dim mySelection = GetMySelection()

        If mySelection IsNot Nothing Then
            Return TriggerResult.NewExecuteActionBotOnSelection(mySelection)
        Else
            Return TriggerResult.NewEndExecutionWithMessage("No selection fulfills my criteria.")
        End If
    End Function

    Public Sub EndExecution() Implements IBotTrigger.EndExecution
    End Sub
End Class
```

## 4) AI prompt — describe the intent, let bfexplorer/MCP execute

The AI example in the repo is a prompt file. It’s not trying to be a full programming language replacement; it’s a fast way to express intent and run it on the active market.

HelloBetfair AI prompt:

```
On the currently active Betfair market, sort the market selections by price in ascending order. 

Immediately execute the 'Bet 10 Euro' strategy on the first selection whose price is between 2.5 and 3.0 (inclusive). 

If there is no selection in this price range, do not execute any bet. 

Do not ask for further user confirmation—proceed automatically according to these criteria.
```

Notice this one intentionally differs from the code examples:

- it sorts selections by price
- it uses the same range ($[2.5, 3.0]$)
- it executes a named strategy (“Bet 10 Euro”) on the first match

That’s actually a feature: prompts are great for quickly trying an idea, then “promoting” it into an F#/C#/VB trigger once it proves useful.

## Practical notes (things that bite people)

Here are the real “interop” details you’ll notice when you compare the three codebases:

- **Parameters are just string keys**: `botTriggerParameters.GetParameter<T>(name)` looks up a string in a dictionary. Pick names you like (for example `FromPrice`/`ToPrice`) and keep them consistent across languages.
- **F# discriminated unions (DU) show up as generated classes in C#/VB**: `TriggerResult` is an F# DU. In F# you return DU cases directly (e.g. `TriggerResult.ExecuteActionBotOnSelection mySelection`). In C#/VB you construct the DU using generated helpers (e.g. `TriggerResult.NewExecuteActionBotOnSelection(mySelection)` / `NewEndExecutionWithMessage(...)`).
- **F# `option` types require interop**: when you read optional values from F#, C#/VB will see `FSharpOption<T>` (or use `OptionModule.IsSome(...)` in VB). That’s why the C#/VB examples look a bit more “ceremonial” around parameters.
- **Trigger vs execution separation is intentional**: these examples return `ExecuteActionBotOnSelection` rather than placing orders inline. In bfexplorer, that separation often makes strategies easier to reuse and test (selection logic in the trigger, execution rules in the action bot).

## Wrap-up

The point of this “Hello Betfair” example is the side-by-side comparison.

When you scan the three implementations, focus on these differences:

- **Expression of the same rule**: F# uses `List.tryFind` + pattern matching; C#/VB use LINQ-style `FirstOrDefault` + `if`.
- **F# DU / option interop**: `TriggerResult` and `option` feel native in F#, but become generated classes/helpers in C#/VB.
- **Packaging and workflow**: `.fsx` is great for iteration; DLLs are great when you want a compiled artifact (and you can choose either approach in F#).
- **AI prompt as a contrast**: it’s not about syntax at all—it’s about describing intent and letting bfexplorer/MCP drive execution.

If you want, tell me how you build/load DLL triggers in your setup (or point me to the solution files), and I’ll add a short “Build & load” section with the exact steps.

---

*Trading involves risk. Test in a safe environment first. This post is for educational purposes only.*
