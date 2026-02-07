namespace BeloSoft.AnalyzeResults

module App =

    open BeloSoft.Data
    open BeloSoft.Net

    open BeloSoft.Bfexplorer.HorseRacingStatto.API.Models
    
    [<EntryPoint>]
    let main _args =
        printfn "Loading horse racing Statto data."

        let resultRacesResults =
            Http.GetApi<RaceResultData []> "http://localhost:10043/api/getAIAgentDataContextFeedback?dataContextName=RacesResultsForRacingStattoData&forLastResults=1000"
            |> Async.RunSynchronously

        match resultRacesResults with
        | DataResult.Success racesResults ->

            printfn "Starting to analyze %d races." racesResults.Length

            printfn "\nProfit for all selections bet placed: %.2f\n\n" 
                (
                    racesResults 
                    |> Array.map (fun racesResult -> racesResult.Runners) 
                    |> Array.concat 
                    |> Array.sumBy (fun runner -> runner.Profit)
                )          
            
            QuipuAnalyze.doAnalyze racesResults

            RulesOperations.doAnalyzeWithRules RulesOperations.manualRules racesResults

            WinnerRulesOperations.doAnalyzeWithWinnerRules racesResults

            printfn "All races data has been analyzed."

            0

        | DataResult.Failure errorMessage ->
            
            printfn "Failed to load API data: %s" errorMessage

            -2
