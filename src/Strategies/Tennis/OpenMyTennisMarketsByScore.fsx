// Bfexplorer cannot be held responsible for any losses or damages incurred during the use of this betfair bot.
// It is up to you to determine the level of risk you wish to trade under. 
// Do not gamble with money you cannot afford to lose.

module BfexplorerScript

#I @"C:\Program Files\BeloSoft\Bfexplorer\"

#r "DevExpress.Spreadsheet.v25.2.Core.dll"

#r "BeloSoft.Data.dll"
#r "BeloSoft.Bfexplorer.Domain.dll"
#r "BeloSoft.Bfexplorer.Service.Core.dll"
#r "BeloSoft.Bfexplorer.TennisScoreProvider.dll"

open BeloSoft.Data
open BeloSoft.Bfexplorer.Service
open BeloSoft.Bfexplorer.TennisScoreProvider
open BeloSoft.Bfexplorer.TennisScoreProvider.Models

/// <summary>
/// Execute
/// </summary>
/// <param name="bfexplorerConsole"></param>
let Execute (bfexplorerConsole : IBfexplorerConsole) =
    let report message =
        bfexplorerConsole.Bfexplorer.OutputMessage message

    let isMyTennisMatch (tennisMatch : TennisMatch) =
        // Check if the match is in the second set and the score is 1:0 or 0:1
        (tennisMatch.FirstPlayer.SetsWon + tennisMatch.SecondPlayer.SetsWon) = 1uy

    let tennisScoreProvider = TennisScoreProvider (bfexplorerConsole.BfexplorerService)

    async {        
        match! tennisScoreProvider.GetActiveMatches () with
        | DataResult.Success tennisMatches ->
            if tennisMatches.IsEmpty 
            then
                do! report "No tennis matches playing!"
            else
                match! tennisScoreProvider.UpdateMatches tennisMatches with
                | Result.Success ->

                    let formattedMatches = 
                        tennisMatches
                        |> List.map (fun tennisMatch -> tennisMatch.ToString ())
                        |> String.concat "\n"
                
                    do! report formattedMatches

                    let marketsToOpen =
                        tennisMatches
                        |> List.filter isMyTennisMatch
                        |> List.map (fun tennisMatch -> tennisMatch.Market)

                    if not marketsToOpen.IsEmpty 
                    then
                        bfexplorerConsole.OpenMyMarkets marketsToOpen    

                | Result.Failure errorMessage -> do! report errorMessage

        | DataResult.Failure errorMessage -> do! report errorMessage
    }
    |> Async.RunSynchronously