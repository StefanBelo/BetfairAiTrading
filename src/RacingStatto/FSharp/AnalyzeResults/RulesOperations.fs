namespace BeloSoft.AnalyzeResults

module internal RulesOperations =

    open BeloSoft.AnalyzeResults.Domain
    open BeloSoft.Bfexplorer.HorseRacingStatto.API.Models

    let private tryGetByteValue (feature: Feature) (runner: RaceRunnerData) : byte option =
        let s = runner.RacingStattoData
        match feature with
        | GoingRank -> Some s.GoingRank
        | DistanceRank -> Some s.DistanceRank
        | GoingDistRank -> Some s.GoingDistRank
        | CourseRank -> Some s.CourseRank
        | TotalWinsRank -> Some s.TotalWinsRank
        | PercentageTotalRank -> Some s.PercentageTotalRank
        | ORRank -> Some s.ORRank
        | TSRank -> Some s.TSRank
        | RPRRank -> Some s.RPRRank
        | JockeyPercentageRank -> Some s.JockeyPercentageRank
        | TrainerPercentageRank -> Some s.TrainerPercentageRank
        | Rank -> Some s.Rank
        | SpPrice -> None

    let private tryGetFloatValue (feature: Feature) (runner: RaceRunnerData) : float option =
        match feature with
        | SpPrice -> Some runner.SpPrice
        | _ -> None

    let rec ruleMatches (runner: RaceRunnerData) (rule: Rule) : bool =
        match rule with
        | ByteAtLeast (feature, value) ->
            tryGetByteValue feature runner
            |> Option.exists (fun v -> v >= value)
        | ByteAtMost (feature, value) ->
            tryGetByteValue feature runner
            |> Option.exists (fun v -> v <= value)
        | FloatAtLeast (feature, value) ->
            tryGetFloatValue feature runner
            |> Option.exists (fun v -> v >= value)
        | FloatAtMost (feature, value) ->
            tryGetFloatValue feature runner
            |> Option.exists (fun v -> v <= value)
        | And rules -> rules |> List.forall (ruleMatches runner)
        | Or rules -> rules |> List.exists (ruleMatches runner)

    let runnerMatchesAll (rules: Rule list) (runner: RaceRunnerData) : bool =
        rules |> List.forall (ruleMatches runner)

    let tryPickRunnerByRules (rules: Rule list) (raceResult: RaceResultData) : RaceRunnerData option =
        raceResult.Runners
        |> Array.filter (runnerMatchesAll rules)
        |> Array.sortBy (fun r -> r.RacingStattoData.Rank)
        |> Array.tryHead

    let evaluateRules (rules: Rule list) (racesResults: RaceResultData[]) : StrategyResult =
        let folder (profit, bets, wins) raceResult =
            match tryPickRunnerByRules rules raceResult with
            | None -> (profit, bets, wins)
            | Some runner ->
                let newBets = bets + 1
                let newWins = wins + (if runner.IsWinner then 1 else 0)
                let newProfit = profit + runner.Profit
                (newProfit, newBets, newWins)

        let profit, bets, wins = Array.fold folder (0.0, 0, 0) racesResults
        let roi = if bets = 0 then 0.0 else profit / float bets

        {
            Strategy = { Weights = Map.empty; Threshold = 0.0 }
            Profit = profit
            BetsPlaced = bets
            WinningBets = wins
            ROI = roi
        }

    let doAnalyzeWithRules (rules: Rule list) (racesResults: RaceResultData[]) =
        if racesResults.Length = 0 then
            printfn "No races to analyze."
        elif rules.IsEmpty then
            printfn "No manual rules configured."
        else
            let result = evaluateRules rules racesResults

            printfn "\n=== MANUAL RULES RESULT ==="
            printfn "Bets Placed: %d / %d races" result.BetsPlaced racesResults.Length

            if result.BetsPlaced > 0 then
                printfn "Winning Bets: %d (%.1f%%)" result.WinningBets (100.0 * float result.WinningBets / float result.BetsPlaced)

            printfn "Profit: %.4f" result.Profit
            printfn "ROI per bet: %.4f" result.ROI

    // Put your manual rules here.
    let manualRules : Rule list =
        [
            ByteAtMost (Rank, 2uy)
        ]
