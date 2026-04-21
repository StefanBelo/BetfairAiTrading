//#I @"C:\Program Files\BeloSoft\Bfexplorer\"
#I @"E:\Projects\Bfexplorer\Development\Applications\BeloSoft.Bfexplorer.App\bin\Debug\net10.0-windows"

#r "BeloSoft.Data.dll"
#r "BeloSoft.Betfair.API.dll"
#r "BeloSoft.Bfexplorer.API.dll"
#r "BeloSoft.Bfexplorer.Host.dll"
#r "BeloSoft.Bfexplorer.Domain.dll"
#r "BeloSoft.Bfexplorer.Service.Core.dll"
#r "BeloSoft.Bfexplorer.Service.dll"
#r "BeloSoft.Bfexplorer.Interoperability.dll"

open BeloSoft.Data
open BeloSoft.Bfexplorer
open BeloSoft.Bfexplorer.Service
open BeloSoft.Bfexplorer.Domain
open BeloSoft.Bfexplorer.Interoperability

(*
    Helper functions and methods.
*)

let doLogin (username, password) (bfexplorerService : BfexplorerService) = async {
    if bfexplorerService.ServiceStatus.IsAuthorized
    then
        return Result.Success
    else
        match! bfexplorerService.Login (username, password) with
        | DataSuccessFailure.Success _ -> return Result.Success
        | DataSuccessFailure.Failure (_, _, errorMessage) -> return Result.Failure errorMessage
}

let doGetActiveMarket () =
    let bfexplorerApplication = Services.CreateBfexplorerApplicationClient ()

    task {
        try
            let! marketResult = bfexplorerApplication.GetActiveMarket ()

            return DataResult.Success marketResult.MarketId
        with
        | ex -> return DataResult.Failure ex.Message
    }
    |> Async.AwaitTask

let showMarket (market : Market) =
    printfn "%s\n" market.MarketFullName

    getActiveSelections market
    |> Seq.map (fun selection -> sprintf "%s ~ %.2f | %.2f" selection.Name selection.LastPriceTraded selection.TotalMatched)
    |> String.concat "\n"
    |> printfn "%s"

(*
    Start the BfexplorerService test.
*)

match GetMyBetfairCredentials () with
| Some (username, password) ->

    let bfexplorerService = BfexplorerService (UiApplication = BfexplorerHost (), initializeBotManager = false)

    (* Two ways of using asyncFlow: 1. asyncFlow returns data.
    asyncFlow {
        do! doLogin (username, password) bfexplorerService

        use _ = CreateAsyncDisposable (fun () -> bfexplorerService.Logout ())
                
        return! bfexplorerService.GetAccountFunds ()
    }
    |> Async.RunSynchronously
    |> function
        | DataResult.Success accountFunds -> printfn "My account balance %.2f" accountFunds.AvailableToBetBalance
        | DataResult.Failure errorMessage -> printfn "Error: %s" errorMessage
    *)

    (* 2. unit - no value or data is returned from the asyncFlow. *)
    asyncFlow {
        do! doLogin (username, password) bfexplorerService

        use _ = CreateAsyncDisposable (fun () -> bfexplorerService.Logout ())
                
        let! accountFunds : AccountFunds = bfexplorerService.GetAccountFunds ()

        printfn "My account balance %.2f" accountFunds.AvailableToBetBalance

        let! marketId = doGetActiveMarket ()

        printfn "Get market data for: %s" marketId

        let! market = bfexplorerService.GetMarket marketId

        showMarket market

        printfn "\nUpdating the market data in 5 seconds.\n"

        do! Async.Sleep 5000 

        do! bfexplorerService.UpdateMarket market
        
        showMarket market                
    }
    |> Async.RunSynchronously
    |> DataResult.ToErrorMessage
    |> Option.iter (fun errorMessage -> printfn "Error: %s" errorMessage)

| None -> printfn "Please set your credentials to the enviroment variables: BETFAIR_USERNAME, BETFAIR_PASSWORD."