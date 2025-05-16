// Bfexplorer cannot be held responsible for any losses or damages incurred during the use of this betfair bot.
// It is up to you to determine the level of risk you wish to trade under. 
// Do not gamble with money you cannot afford to lose.

module BfexplorerScript

#I @"C:\Program Files\BeloSoft\Bfexplorer\"

#r "DevExpress.Spreadsheet.v24.2.Core.dll"

#r "BeloSoft.Data.dll"
#r "BeloSoft.Bfexplorer.Domain.dll"
#r "BeloSoft.Bfexplorer.Service.Core.dll"
#r "BeloSoft.Bfexplorer.FootballScoreProvider.dll"

open BeloSoft.Data
open BeloSoft.Bfexplorer.Service
open BeloSoft.Bfexplorer.FootballScoreProvider
open BeloSoft.Bfexplorer.FootballScoreProvider.Models

/// <summary>
/// Execute
/// </summary>
/// <param name="bfexplorerConsole"></param>
let Execute (bfexplorerConsole : IBfexplorerConsole) =
    let report message =
        bfexplorerConsole.Bfexplorer.OutputMessage message

    let isMyFootballMatch (footballMatch : FootballMatch) =
        footballMatch.ScoreDifference > 0y

    let isAwayTeamDominatingEarly (footballMatch : FootballMatch) =
        footballMatch.MatchTime <= 60 && footballMatch.AwayScore - footballMatch.HomeScore >= 2uy

    let footballScoreProvider = FootballScoreProvider (bfexplorerConsole.BfexplorerService)

    async {        
        match! footballScoreProvider.GetActiveMatches () with
        | DataResult.Success footballMatches ->

            if footballMatches.IsEmpty
            then
                do! report "No football matches playing!"
            else
                match! footballScoreProvider.UpdateMatches footballMatches with
                | Result.Success ->

                    do! report (
                            footballMatches
                            |> List.map (fun footballMatch -> footballMatch.ToString ())
                            |> String.concat "\n"
                        )

                    let marketsToOpen =
                        footballMatches
                        |> List.filter isMyFootballMatch
                        //|> List.filter isAwayTeamDominatingEarly
                        |> List.map (fun footballMatch -> footballMatch.Market)

                    if not marketsToOpen.IsEmpty
                    then
                        bfexplorerConsole.OpenMyMarkets marketsToOpen    

                | Result.Failure errorMessage -> do! report errorMessage

        | DataResult.Failure errorMessage -> do! report errorMessage
    }
    |> Async.RunSynchronously