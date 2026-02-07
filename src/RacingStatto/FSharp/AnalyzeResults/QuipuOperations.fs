namespace BeloSoft.AnalyzeResults

module internal QuipuAnalyze =

    open System

    open Quipu

    open BeloSoft.Bfexplorer.HorseRacingStatto.API.Models
    open BeloSoft.AnalyzeResults.Domain
    
    let allFeatures : Feature [] =
        [|
            SpPrice
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
        |]

    let featureName = function
        | SpPrice -> "SpPrice"
        | GoingRank -> "GoingRank"
        | DistanceRank -> "DistanceRank"
        | GoingDistRank -> "GoingDistRank"
        | CourseRank -> "CourseRank"
        | TotalWinsRank -> "TotalWinsRank"
        | PercentageTotalRank -> "PercentageTotalRank"
        | ORRank -> "ORRank"
        | TSRank -> "TSRank"
        | RPRRank -> "RPRRank"
        | JockeyPercentageRank -> "JockeyPercentageRank"
        | TrainerPercentageRank -> "TrainerPercentageRank"
        | Rank -> "Rank"

    let featureValue (feature: Feature) (runner: RaceRunnerData) : float =
        let s = runner.RacingStattoData

        match feature with
        | SpPrice -> runner.SpPrice
        | GoingRank -> float s.GoingRank
        | DistanceRank -> float s.DistanceRank
        | GoingDistRank -> float s.GoingDistRank
        | CourseRank -> float s.CourseRank
        | TotalWinsRank -> float s.TotalWinsRank
        | PercentageTotalRank -> float s.PercentageTotalRank
        | ORRank -> float s.ORRank
        | TSRank -> float s.TSRank
        | RPRRank -> float s.RPRRank
        | JockeyPercentageRank -> float s.JockeyPercentageRank
        | TrainerPercentageRank -> float s.TrainerPercentageRank
        | Rank -> float s.Rank
        
    let computeStats (racesResults: RaceResultData[]) : Map<Feature, FeatureStats> =
        let runners = racesResults |> Array.collect (fun rr -> rr.Runners)

        allFeatures
        |> Array.map (fun feature ->
            let values = runners |> Array.map (featureValue feature)

            if values.Length = 0 then
                feature, { Mean = 0.0; Std = 1.0 }
            else
                let mean = values |> Array.average
                let variance =
                    values
                    |> Array.averageBy (fun v ->
                        let d = v - mean
                        d * d)

                let std = sqrt variance
                feature, { Mean = mean; Std = if std > 0.0 then std else 1.0 })
        |> Map.ofArray

    let normalizedValue (stats: Map<Feature, FeatureStats>) (feature: Feature) (runner: RaceRunnerData) : float =
        let s = stats.[feature]
        (featureValue feature runner - s.Mean) / s.Std

    let score (stats: Map<Feature, FeatureStats>) (strategy: BettingStrategy) (runner: RaceRunnerData) : float =
        allFeatures
        |> Array.sumBy (fun feature ->
            let w = strategy.Weights |> Map.tryFind feature |> Option.defaultValue 0.0
            w * normalizedValue stats feature runner)

    let tryMaxBy (f: 'a -> 'b when 'b: comparison) (xs: 'a[]) : 'a option =
        if xs.Length = 0 then None else Some (xs |> Array.maxBy f)

    let tryPickRunner (stats: Map<Feature, FeatureStats>) (strategy: BettingStrategy) (raceResult: RaceResultData) : RaceRunnerData option =
        let candidates = raceResult.Runners |> Array.map (fun r -> r, score stats strategy r)

        if candidates.Length = 0 then
            None
        else
            let best = candidates |> Array.maxBy snd
            if snd best > strategy.Threshold then Some (fst best) else None

    let evaluateStrategy (stats: Map<Feature, FeatureStats>) (strategy: BettingStrategy) (racesResults: RaceResultData[]) : StrategyResult =
        let folder (profit, bets, wins) raceResult =
            match tryPickRunner stats strategy raceResult with
            | None -> (profit, bets, wins)
            | Some runner ->
                let newBets = bets + 1
                let newWins = wins + (if runner.IsWinner then 1 else 0)
                let newProfit = profit + runner.Profit
                (newProfit, newBets, newWins)

        let profit, bets, wins = Array.fold folder (0.0, 0, 0) racesResults
        let roi = if bets = 0 then 0.0 else profit / float bets

        {
            Strategy = strategy
            Profit = profit
            BetsPlaced = bets
            WinningBets = wins
            ROI = roi
        }

    let evaluateStrategyWithPicks (stats: Map<Feature, FeatureStats>) (strategy: BettingStrategy) (racesResults: RaceResultData[]) : StrategyResult * RaceRunnerData list =
        let folder (profit, bets, wins, picks) raceResult =
            match tryPickRunner stats strategy raceResult with
            | None -> (profit, bets, wins, picks)
            | Some runner ->
                let newBets = bets + 1
                let newWins = wins + (if runner.IsWinner then 1 else 0)
                let newProfit = profit + runner.Profit
                let newPicks = runner :: picks
                (newProfit, newBets, newWins, newPicks)

        let profit, bets, wins, picks = Array.fold folder (0.0, 0, 0, []) racesResults
        let roi = if bets = 0 then 0.0 else profit / float bets

        {
            Strategy = strategy
            Profit = profit
            BetsPlaced = bets
            WinningBets = wins
            ROI = roi
        }, picks

    let featureExpr (feature: Feature) : string option =
        match feature with
        | SpPrice -> Some "SpPrice"
        | GoingRank -> Some "RacingStattoData.GoingRank"
        | DistanceRank -> Some "RacingStattoData.DistanceRank"
        | GoingDistRank -> Some "RacingStattoData.GoingDistRank"
        | CourseRank -> Some "RacingStattoData.CourseRank"
        | TotalWinsRank -> Some "RacingStattoData.TotalWinsRank"
        | PercentageTotalRank -> Some "RacingStattoData.PercentageTotalRank"
        | ORRank -> Some "RacingStattoData.ORRank"
        | TSRank -> Some "RacingStattoData.TSRank"
        | RPRRank -> Some "RacingStattoData.RPRRank"
        | JockeyPercentageRank -> Some "RacingStattoData.JockeyPercentageRank"
        | TrainerPercentageRank -> Some "RacingStattoData.TrainerPercentageRank"
        | Rank -> Some "RacingStattoData.Rank"
        
    let featureByteValue (feature: Feature) (runner: RaceRunnerData) : byte option =
        let s = runner.RacingStattoData

        match feature with
        | SpPrice -> None
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
        
    let featureFloatValue (feature: Feature) (runner: RaceRunnerData) : float option =
        match feature with
        | SpPrice -> Some runner.SpPrice
        | _ -> None

    let deriveRuleExpression (strategy: BettingStrategy) (picks: RaceRunnerData list) : string =
        if picks.IsEmpty then
            "true"
        else
            let topFeatures =
                strategy.Weights
                |> Seq.sortByDescending (fun kvp -> abs kvp.Value)
                |> Seq.filter (fun kvp -> abs kvp.Value > 1e-9)

            let rules =
                topFeatures
                |> Seq.choose (fun kvp ->
                    let feature = kvp.Key
                    let w = kvp.Value

                    match featureExpr feature with
                    | None -> None
                    | Some expr ->

                        let values =
                            picks
                            |> List.choose (featureByteValue feature)

                        if values.IsEmpty then
                            None
                        else
                            let maxVal = List.max values
                            let minVal = List.min values
                            if w < 0.0 then
                                if maxVal < Byte.MaxValue then
                                    Some (sprintf "%s <= %duy" expr maxVal)
                                else
                                    None
                            else
                                if minVal > 0uy then
                                    Some (sprintf "%s >= %duy" expr minVal)
                                else
                                    None
                    )
                |> Seq.distinct
                |> Seq.toList

            match rules with
            | [] -> "true"
            | xs -> String.concat " && " xs

    let buildStrategy (rawParams: float[]) : BettingStrategy =
        let weights =
            allFeatures
            |> Array.mapi (fun i feature ->
                // keep weights bounded to reduce instability
                let w = Math.Tanh(rawParams.[i]) * 5.0
                feature, w)
            |> Map.ofArray

        let threshold = Math.Tanh(rawParams.[allFeatures.Length]) * 0.3 + 0.8
        {
            Weights = weights
            Threshold = threshold
        }

    let optimizeWithQuipu (racesResults: RaceResultData[]) : StrategyResult * SolverResult =
        let stats = computeStats racesResults
        let dim = allFeatures.Length + 1

        let objective (x: float[]) : float =
            let rawParams = x
            let strategy = buildStrategy rawParams

            let result = evaluateStrategy stats strategy racesResults

            // Regularize weights a bit more to encourage sparsity
            let l1 = strategy.Weights |> Seq.sumBy (fun kvp -> abs kvp.Value)

            result.Profit - 0.1 * l1

        let startVector = Array.zeroCreate<float> dim

        let problem =
            NelderMead.objective (dim, objective)
            |> NelderMead.startFrom (Start.around startVector)
            |> NelderMead.withMaximumIterations 20000
            |> NelderMead.withTolerance 0.001

        let solverResult = NelderMead.maximize problem

        let bestArgs =
            match solverResult with
            | Successful s -> s.Candidate.Arguments
            | Abnormal (a: AbnormalSimplex) ->
                // Abnormal results still return a simplex; pick the best vertex.
                a.Simplex |> Array.maxBy objective

        let bestStrategy = buildStrategy bestArgs

        evaluateStrategy stats bestStrategy racesResults, solverResult

    let doAnalyze (racesResults: RaceResultData[]) =
        if racesResults.Length = 0 then
            printfn "No races to analyze."
        else
            let best, solverResult = optimizeWithQuipu racesResults

            printfn "\n=== BEST STRATEGY (Quipu Nelder-Mead) ==="
            printfn "Bets Placed: %d / %d races" best.BetsPlaced racesResults.Length

            if best.BetsPlaced > 0 then
                printfn "Winning Bets: %d (%.1f%%)" best.WinningBets (100.0 * float best.WinningBets / float best.BetsPlaced)

            printfn "Profit: %.4f" best.Profit
            printfn "ROI per bet: %.4f" best.ROI

            printfn "\nTop weights (by |w|):"

            best.Strategy.Weights
            |> Seq.sortByDescending (fun kvp -> abs kvp.Value)
            |> Seq.iter (fun kvp ->
                printfn "  %s: %.4f" (featureName kvp.Key) kvp.Value)

            printfn "Threshold: %.4f" best.Strategy.Threshold

            // Produce deployable rules (byte-aware) from the runners the best strategy actually picked.
            let stats = computeStats racesResults
            let _, picks = evaluateStrategyWithPicks stats best.Strategy racesResults
            printfn "\n=== GENERATED RULES (deployable predicate) ==="
            printfn "%s" (deriveRuleExpression best.Strategy picks)

            match solverResult with
            | Successful s ->
                printfn "\nSolver status: %A (iters=%d, objective=%.4f)" s.Status s.Iterations s.Candidate.Value
            | Abnormal (a: AbnormalSimplex) ->
                printfn "\nSolver status: Abnormal (%s)" a.Message
                printfn "(Using best found strategy so far: profit=%.4f, bets=%d)" best.Profit best.BetsPlaced
