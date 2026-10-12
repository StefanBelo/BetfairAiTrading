// Bfexplorer cannot be held responsible for any losses or damages incurred during the use of this betfair bot.
// It is up to you to determine the level of risk you wish to trade under. 
// Do not gamble with money you cannot afford to lose.

module BfexplorerScript

//#I @"C:\Program Files\BeloSoft\Bfexplorer\"
#I @"E:\Projects\Bfexplorer\Development\Applications\BeloSoft.Bfexplorer.App\bin\Release\net10.0-windows\"

#r "DevExpress.Spreadsheet.v25.2.Core.dll"

#r "BeloSoft.Data.dll"
#r "BeloSoft.Bfexplorer.Domain.dll"
#r "BeloSoft.Bfexplorer.Service.Core.dll"

open BeloSoft.Data
open BeloSoft.Bfexplorer.Domain
open BeloSoft.Bfexplorer.Service

let isTradeClosedAtInPlay (market : Market) =
    let startTime = market.MarketInfo.StartTime

    if market.Bets |> Seq.exists (fun bet -> bet.OrderStatus = BetOrderStatus.Unmatched)
    then
        true
    else
        market.Bets |> Seq.exists (fun bet -> bet.Time > startTime)

let totalProfit (markets : Market list) =
    markets |> List.sumBy (fun market -> defaultNullableArg market.SettledProfit 0.0)

let getProfitLostAtAround5Seconds (market : Market) =
    let timeToCloseBetPosition = market.MarketInfo.StartTime.AddSeconds -5.0

    match market.ProfitHistory.Values |> Seq.tryFind (fun timeValue -> timeValue.Time >= timeToCloseBetPosition) with
    | Some timeValue -> timeValue.Value
    | None -> 0.0

let reportRiskTrade (market : Market) =
    let maximalLoss = market.ProfitHistory.LowValue

    sprintf "\t%s: %.2f | %.2f" market.Id maximalLoss (getProfitLostAtAround5Seconds market)

/// <summary>
/// Execute
/// </summary>
/// <param name="bfexplorerConsole"></param>
let Execute (bfexplorerConsole : IBfexplorerConsole) =
    let report message =
        bfexplorerConsole.Bfexplorer.OutputMessage message

    let closedMarkets = 
        bfexplorerConsole.OpenMarkets
        |> Seq.filter isClosedMarket
        |> Seq.toList

    if closedMarkets.IsEmpty
    then
        printf "No closed market!"
    else
        let inPlayTradedMarkets, preOffTradedMarkets = 
            closedMarkets 
            |> List.partition isTradeClosedAtInPlay

        let preOffProfit = totalProfit preOffTradedMarkets
        
        printfn "In-play traded: %d | Profit: %.2f, Pre-off traded: %d | Profit: %.2f" 
            (inPlayTradedMarkets |> List.length) (totalProfit inPlayTradedMarkets)
            (preOffTradedMarkets |> List.length) preOffProfit

        if not inPlayTradedMarkets.IsEmpty
        then
            printfn "In-play trades maximal loss:\n%s" (
                    inPlayTradedMarkets 
                    |> List.map reportRiskTrade
                    |> String.concat "\n" 
                )

            let fiveSecondsProfit = 
                inPlayTradedMarkets 
                |> List.sumBy (fun market -> getProfitLostAtAround5Seconds market)

            printfn "In-play profit (-5s): %.2f" fiveSecondsProfit
                