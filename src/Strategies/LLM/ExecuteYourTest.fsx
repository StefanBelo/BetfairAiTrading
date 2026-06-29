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
#r "BeloSoft.Bfexplorer.RacingTvProvider.dll"

open BeloSoft.Data
open BeloSoft.Bfexplorer
open BeloSoft.Bfexplorer.Service
open BeloSoft.Bfexplorer.Domain
open BeloSoft.Bfexplorer.Interoperability

#load "RacingTvProvider.fsx"
open RacingTvProvider.Test
open BeloSoft.Bfexplorer.RacingTvProvider.Models

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
    Start the BfexplorerService. Login to betfair.
*)

let bfexplorerService = BfexplorerService (UiApplication = BfexplorerHost (), initializeBotManager = false)

match GetMyBetfairCredentials () with
| Some (username, password) ->

    doLogin (username, password) bfexplorerService
    |> Async.RunSynchronously
    |> function
        | Result.Success -> printfn "Successfully logged in to Betfair."
        | Result.Failure errorMessage -> printfn "Error: %s" errorMessage


| None -> printfn "Please set your credentials to the enviroment variables: BETFAIR_USERNAME, BETFAIR_PASSWORD."

(*
    Get the active market in bfexplorer.
*)

let result =    
    asyncFlow {        
        let! marketId : string = doGetActiveMarket () 
                
        return! bfexplorerService.GetMarket (marketId)
    }
    |> Async.RunSynchronously

(*
    Do your test.
*)

let aMarket =
    match result with
    | DataResult.Success market -> showMarket market; Some market
    | DataResult.Failure errorMessage -> printfn "Error: %s" errorMessage; None

match aMarket with
| Some market ->

    asyncFlow {
        do! bfexplorerService.UpdateMarket market
                
        showMarket market

        let! raceCard : RaceCard = doGetRaceCard market

        printfn "%A" raceCard
    }
    |> Async.RunSynchronously
    |> DataResult.ToErrorMessage
    |> Option.iter (fun errorMessage -> printfn "Error: %s" errorMessage)

| None -> printfn "Not valid market!"

(*
    End of the test.
*)

bfexplorerService.Logout () 
|> Async.Ignore 
|> Async.RunSynchronously