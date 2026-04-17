#I @"E:\Projects\Bfexplorer\Development\Applications\BeloSoft.Bfexplorer.App\bin\Debug\net10.0-windows"

#r "BeloSoft.Data.dll"
#r "BeloSoft.Betfair.API.dll"
#r "BeloSoft.Bfexplorer.API.dll"
#r "BeloSoft.Bfexplorer.Host.dll"
#r "BeloSoft.Bfexplorer.Domain.dll"
#r "BeloSoft.Bfexplorer.Service.Core.dll"
#r "BeloSoft.Bfexplorer.Service.dll"
#r "BeloSoft.Bfexplorer.Interoperability.dll"

open System

open BeloSoft.Data
open BeloSoft.Bfexplorer
open BeloSoft.Bfexplorer.Service
open BeloSoft.Bfexplorer.Domain
open BeloSoft.Bfexplorer.Interoperability

let bfexplorerService = BfexplorerService (UiApplication = BfexplorerHost (), initializeBotManager = false)

match GetMyBetfairCredentials () with
| Some (username, password) ->

    async {
        match! bfexplorerService.Login (username, password) with
        | DataSuccessFailure.Success _ ->

            let! resultAccountFunds = bfexplorerService.GetAccountFunds ()

            printfn "%s\n" <|
                match resultAccountFunds with
                | DataResult.Success accountFunds -> sprintf "My account balance %.2f" accountFunds.AvailableToBetBalance
                | DataResult.Failure errorMessage -> errorMessage
            
        | DataSuccessFailure.Failure (_, _, errorMessage) -> printfn "Failed to login: %s" errorMessage
    }
    |> Async.RunSynchronously

| None -> printfn "Please set your credentials to the enviroment variables: BETFAIR_USERNAME, BETFAIR_PASSWORD."

let mutable marketId = String.Empty

async {    
    let doGetActiveMarket () =
        let bfexplorerApplication = Services.CreateBfexplorerApplicationClient ()

        task {
            try
                let! marketResult = bfexplorerApplication.GetActiveMarket ()

                return DataResult.Success marketResult
            with
            | ex -> return DataResult.Failure ex.Message
        }
        |> Async.AwaitTask

    match! doGetActiveMarket () with
    | DataResult.Success marketResult -> 

        marketId <- marketResult.MarketId

        //Variables.Set("marketId", marketId) 
        
        printfn "The active market in bfexplorer: %s" marketId

    | DataResult.Failure errorMessage -> printfn "Error: %s" errorMessage
}
|> Async.RunSynchronously

let showMarket (market : Market) =
    printfn "%s\n" market.MarketFullName

    getActiveSelections market
    |> Seq.map (fun selection -> sprintf "%s ~ %.2f | %.2f" selection.Name selection.LastPriceTraded selection.TotalMatched)
    |> String.concat "\n"
    |> printfn "%s"

printfn "Get market data for: %s" marketId

let mutable market = nil<Market> 

async {
    match! bfexplorerService.GetMarket marketId with
    | DataResult.Success myMarket -> 
    
        market <- myMarket

        //Variables.Set("myMarket", myMarket) 
        showMarket myMarket

    | DataResult.Failure errorMessage -> printfn "Failed to get market: %s" errorMessage
}
|> Async.RunSynchronously

async {
    match! bfexplorerService.UpdateMarket market with
    | Result.Success -> showMarket market
    | Result.Failure errorMessage -> printfn "Failed to update market: %s" errorMessage
}
|> Async.RunSynchronously