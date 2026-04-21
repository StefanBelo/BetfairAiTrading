//#I @"C:\Program Files\BeloSoft\Bfexplorer\"
#I @"E:\Projects\Bfexplorer\Development\Applications\BeloSoft.Bfexplorer.App\bin\Debug\net10.0-windows"

#r "Newtonsoft.Json.dll"

#r "BeloSoft.Data.dll"
#r "BeloSoft.Betfair.API.dll"
#r "BeloSoft.Bfexplorer.Host.dll"

open BeloSoft.Data
open BeloSoft.Bfexplorer

open BeloSoft.Betfair.API
open BeloSoft.Betfair.API.Operations
open BeloSoft.Betfair.API.Models

let showTotalNumberOfOfferedMarkets (browsingOperations : IBrowsingOperations) = async {
    let filter = MarketFilter ()

    match! browsingOperations.GetEventTypes filter with
    | DataResult.Success betEventTypeResults ->

        printfn "Total number of betfair markets: %d\n" (betEventTypeResults |> Seq.sumBy (fun betEventTypeResult -> betEventTypeResult.MarketCount))

        betEventTypeResults
        |> Seq.iter (fun betEventTypeResult -> 
                printfn "%s: %d" betEventTypeResult.EventType.Name betEventTypeResult.MarketCount
            )

    | DataResult.Failure errorMessage -> printfn "Failed to GetEventTypes: %s" errorMessage
}

let showMarketCatalogue (marketCatalogue : MarketCatalogue) =
    let betEvent = marketCatalogue.Event

    printfn "%A: %s, eventId: %d, marketId: %s, %.2f" betEvent.OpenDate betEvent.Name betEvent.Id marketCatalogue.MarketId marketCatalogue.TotalMatched

let getMarketBooks (marketCatalogue : MarketCatalogue) (browsingOperations : IBrowsingOperations) = async {
    match! browsingOperations.GetMarketBooks ([| marketCatalogue.MarketId |], priceProjection = PriceProjection.DefaultActiveMarket ()) with
    | DataResult.Success marketBooks ->

        let marketBook = marketBooks.[0]
        let betEvent = marketCatalogue.Event

        printfn "\nMarket: %A, %s\nSelections:\n" betEvent.OpenDate betEvent.Name
            
        Seq.iter2 (fun (runner : RunnerCatalog) (runnerData : Runner) -> printfn "\t%s: %.2f, %.2f" runner.RunnerName runnerData.LastPriceTraded runnerData.TotalMatched)
            marketCatalogue.Runners marketBook.Runners

    | DataResult.Failure errorMessage -> printfn "Failed to GetMarketBooks: %s" errorMessage
}

let showMarketsData (browsingOperations : IBrowsingOperations) = async {
    let filter = 
        MarketFilter (
                MarketStartTime = TimeRange.Today (),
                // MarketCountries = [| "GB" |],
                EventTypeIds = [| 1 |],
                MarketTypeCodes = [| "MATCH_ODDS" |]
            )

    let marketProjection = [| MarketProjection.EVENT; MarketProjection.MARKET_START_TIME; MarketProjection.COMPETITION; MarketProjection.RUNNER_DESCRIPTION; MarketProjection.MARKET_DESCRIPTION |]

    // Get only the 5 most traded markets.
    match! browsingOperations.GetMarketCatalogues (filter, 5, marketProjection, MarketSort.MAXIMUM_TRADED) with
    | DataResult.Success marketCatalogues ->

        printfn "\nMarket catalogues:\n"

        marketCatalogues |> Seq.iter showMarketCatalogue

        for marketCatalogue in marketCatalogues do
            do! getMarketBooks marketCatalogue browsingOperations

    | DataResult.Failure errorMessage -> printfn "Failed to GetMarketCatalogues: %s" errorMessage
}

match GetMyBetfairCredentials () with
| Some (username, password) ->

    let betfairServiceProvider = BetfairServiceProvider ()
    
    let accountOperations = betfairServiceProvider.AccountOperations
    let browsingOperations = betfairServiceProvider.BrowsingOperations

    let doLogin () =
        if accountOperations.IsAuthorized
        then
            async { return Result.Success }
        else
            accountOperations.Login (username, password)

    asyncFlow {                   
        do! doLogin ()

        use _ = CreateAsyncDisposable (fun () -> accountOperations.Logout ())

        do! showTotalNumberOfOfferedMarkets browsingOperations
        do! showMarketsData browsingOperations
    }
    |> Async.RunSynchronously
    |> DataResult.ToErrorMessage
    |> Option.iter (fun errorMessage -> printfn "Error: %s" errorMessage)

| None -> printfn "Please set your credentials to the enviroment variables: BETFAIR_USERNAME, BETFAIR_PASSWORD."