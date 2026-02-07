namespace BeloSoft.AnalyzeResults

module internal WinnerRulesOperations =

    open System
    
    open BeloSoft.AnalyzeResults.Domain
    open BeloSoft.Bfexplorer.HorseRacingStatto.API.Models

    let defaultConfig =
        {
            MaxRules = 3
            MaxCandidates = 24
            MinWinCoverage = 0.3
        }

    let byteFeatures =
        [
            GoingRank
            DistanceRank
            GoingDistRank
            CourseRank
            TotalWinsRank
            PercentageTotalRank
            ORRank
            TSRank
            RPRRank
            JockeyPercentageRank
            TrainerPercentageRank
            Rank
        ]

    let floatFeatures =
        [
            SpPrice
        ]

    let tryGetByteValue (feature: Feature) (runner: RaceRunnerData) : byte option =
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

    let tryGetFloatValue (feature: Feature) (runner: RaceRunnerData) : float option =
        match feature with
        | SpPrice -> Some runner.SpPrice
        | _ -> None

    let getWinners (racesResults: RaceResultData[]) : RaceRunnerData list =
        racesResults
        |> Array.collect (fun r -> r.Runners |> Array.filter (fun rr -> rr.IsWinner))
        |> Array.toList

    let sampleDistinct (maxCount: int) (values: 'a list) : 'a list when 'a: comparison =
        let distinctValues = values |> List.distinct |> List.sort |> List.toArray
        if distinctValues.Length <= maxCount then
            distinctValues |> Array.toList
        elif maxCount <= 1 then
            [ distinctValues.[0] ]
        else
            let step = float (distinctValues.Length - 1) / float (maxCount - 1)
            [ 0 .. maxCount - 1 ]
            |> List.map (fun i ->
                let idx = int (Math.Round(step * float i))
                distinctValues.[idx])
            |> List.distinct

    let buildCandidateRules (winners: RaceRunnerData list) : Rule list =
        let byteRules =
            byteFeatures
            |> List.collect (fun feature ->
                winners
                |> List.choose (tryGetByteValue feature)
                |> sampleDistinct 8
                |> List.collect (fun v -> [ ByteAtMost (feature, v); ByteAtLeast (feature, v) ]))

        let floatRules =
            floatFeatures
            |> List.collect (fun feature ->
                winners
                |> List.choose (tryGetFloatValue feature)
                |> List.map (fun v -> Math.Round(v, 2))
                |> sampleDistinct 8
                |> List.collect (fun v -> [ FloatAtMost (feature, v); FloatAtLeast (feature, v) ]))

        byteRules @ floatRules

    let ruleWinCoverage (rule: Rule) (winners: RaceRunnerData list) : float =
        if winners.IsEmpty then
            0.0
        else
            winners
            |> List.averageBy (fun w -> if RulesOperations.ruleMatches w rule then 1.0 else 0.0)

    let evaluateRuleList (rules: Rule list) (racesResults: RaceResultData[]) : StrategyResult =
        RulesOperations.evaluateRules rules racesResults

    let scoreKey (result: StrategyResult) =
        (result.ROI, result.Profit, result.BetsPlaced)

    let combinations (k: int) (items: 'a list) : 'a list list =
        let rec loop k items =
            match k, items with
            | 0, _ -> [ [] ]
            | _, [] -> []
            | k, x :: xs ->
                let withX = loop (k - 1) xs |> List.map (fun rest -> x :: rest)
                let withoutX = loop k xs
                withX @ withoutX
        loop k items

    let findBestRulesFromWinners (racesResults: RaceResultData[]) : Rule list * StrategyResult =
        if racesResults.Length = 0 then
            let emptyResult = evaluateRuleList [] racesResults
            [], emptyResult
        else
            let winners = getWinners racesResults
            let allCandidates = buildCandidateRules winners

            let filteredCandidates =
                allCandidates
                |> List.filter (fun r -> ruleWinCoverage r winners >= defaultConfig.MinWinCoverage)

            let rankedCandidates =
                filteredCandidates
                |> List.map (fun r -> r, evaluateRuleList [ r ] racesResults)
                |> List.sortByDescending (fun (_, res) -> scoreKey res)

            let candidates =
                rankedCandidates
                |> List.truncate defaultConfig.MaxCandidates
                |> List.map fst

            let mutable bestRules: Rule list = []
            let mutable bestResult: StrategyResult = evaluateRuleList [] racesResults

            for k in 1 .. defaultConfig.MaxRules do
                let combos = combinations k candidates
                for rules in combos do
                    let result = evaluateRuleList rules racesResults
                    if scoreKey result > scoreKey bestResult then
                        bestRules <- rules
                        bestResult <- result

            bestRules, bestResult

    let doAnalyzeWithWinnerRules (racesResults: RaceResultData[]) =
        if racesResults.Length = 0 then
            printfn "No races to analyze."
        else
            let rules, result = findBestRulesFromWinners racesResults

            printfn "\n=== WINNER-DERIVED RULES RESULT ==="
            printfn "Rules Selected: %d" rules.Length
            rules |> List.iter (fun r -> printfn "  %A" r)
            printfn "Bets Placed: %d / %d races" result.BetsPlaced racesResults.Length

            if result.BetsPlaced > 0 then
                printfn "Winning Bets: %d (%.1f%%)" result.WinningBets (100.0 * float result.WinningBets / float result.BetsPlaced)

            printfn "Profit: %.4f" result.Profit
            printfn "ROI per bet: %.4f" result.ROI
