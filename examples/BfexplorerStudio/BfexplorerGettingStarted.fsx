#I @"E:\Projects\Bfexplorer\Development\Applications\BeloSoft.Bfexplorer.App\bin\Debug\net10.0-windows"

#r "BeloSoft.Data.dll"
#r "BeloSoft.Bfexplorer.Domain.dll"
#r "BeloSoft.Bfexplorer.Service.Core.dll"

open BeloSoft.Data
open BeloSoft.Bfexplorer.Domain
open BeloSoft.Bfexplorer.Service

let showMarket (market : Market) =
    printfn "%s\n" market.MarketFullName

    getActiveSelections market
    |> Seq.map (fun selection -> sprintf "%s ~ %.2f | %.2f" selection.Name selection.LastPriceTraded selection.TotalMatched)
    |> String.concat "\n"
    |> printfn "%s\n"

let showBets (market : Market) =   
    let bets =
        market.Bets
        |> Seq.toList
        |> List.map (fun bet -> sprintf "%s ~ %A" bet.Selection.Name bet)

    if bets.IsEmpty
    then
        printfn "No bets were placed.\n"
    else
        printfn "All placed bets:\n%s\n" (bets |> String.concat "\n")

let showRunningBots (market : Market) =   
    let runningBots =
        market.RunningBots
        |> Seq.toList
        |> List.map (fun bot -> sprintf "%s ~ %A" bot.Name bot.Status)

    if runningBots.IsEmpty
    then
        printfn "No bots are running.\n"
    else
        printfn "All running bots:\n%s\n" (runningBots |> String.concat "\n")

let showOpenMarkets (iBfexplorerConsole : IBfexplorerConsole) =
    let closedMarkets, activeMarkets = 
        iBfexplorerConsole.OpenMarkets |> List.partition (fun market -> market.MarketStatus = MarketStatus.Closed)

    if not activeMarkets.IsEmpty
    then
        printfn "Active markets:\n"        
        activeMarkets |> List.iter showMarket

    if not closedMarkets.IsEmpty
    then
        printfn "Closed markets:\n"        
        closedMarkets 
        |> List.iter (fun market ->
                match toOption market.SettledProfit with
                | Some settledProfit -> printf "%s ~ %.2f" market.MarketFullName settledProfit
                | None -> printf "%s" market.MarketFullName 
            )

let execute (iBfexplorerConsole : IBfexplorerConsole) =
    let market = iBfexplorerConsole.ActiveMarket        

    showMarket market
    showBets market
    showRunningBots market
    showOpenMarkets iBfexplorerConsole