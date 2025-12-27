// Bfexplorer cannot be held responsible for any losses or damages incurred during the use of this betfair bot.
// It is up to you to determine the level of risk you wish to trade under. 
// Do not gamble with money you cannot afford to lose.

module HorseRacingBotTrigger

#I @"C:\Program Files\BeloSoft\Bfexplorer\"
//#I @"E:\Projects\Bfexplorer\Development\Applications\BeloSoft.Bfexplorer.App\bin\Debug\net9.0-windows\"

#r "DevExpress.Spreadsheet.v25.1.Core.dll"

#r "BeloSoft.Data.dll"
#r "BeloSoft.Betfair.API.dll"

#r "BeloSoft.Bfexplorer.Domain.dll"
#r "BeloSoft.Bfexplorer.Service.Core.dll"
#r "BeloSoft.Bfexplorer.Trading.dll"

#r "BeloSoft.Bfexplorer.TimeformProviderPro.dll"
#r "BeloSoft.Bfexplorer.HorseRacingDatabase.dll"
#r "BeloSoft.Bfexplorer.HorseRacingDataProviders.dll"

open System
open System.Collections.Generic

open BeloSoft.Data
open BeloSoft.Bfexplorer
open BeloSoft.Bfexplorer.Domain
open BeloSoft.Bfexplorer.Domain.Data
open BeloSoft.Bfexplorer.Trading

open BeloSoft.Data.HorseRacing.Models
open BeloSoft.Bfexplorer.HorseRacingDataProviders

/// <summary>
/// MySelectionData
/// </summary>
type MySelectionData =
    {
        Selection : Selection
        RacingpostHorseData : RacingpostDto.HorseDataDto option
        TimeformHorseData : TimeformProviderPro.Models.TimeformData option
        HorseResults : HorseRaceResultData array option
        JockeyResults : JockeyRaceResultForHorseData array option
    }

/// <summary>
/// EvaluationResult
/// </summary>
type EvaluationResult = 
    {
        SelectionResults : SelectionResult list        
        TopSelectionResults : SelectionResult list
    }

and SelectionResult =
    {
        MySelectionData : MySelectionData
        LastScore : float
        BestScore : float
        BaseScore : float
        WeightScore : float
        BonusScore : float
        LBBWScore : float
        HorseFormScore : float
        JockeyFormScore : float
        CompositeScore : float
        ImpliedOdds : float
        ValueScore : float
    }

[<AutoOpen>]
module Helpers =

    let getData<'T> (dataField : string) (data : Dictionary<string, obj>) = 
        try
            let status, myData = data.TryGetValue dataField

            if status
            then        
                Some (myData :?> 'T)
            else
                None
        with
        | _ -> None

    let mapToMySelectionData (selectionData : SelectionDataContext) =
        let data = selectionData.Data

        {
            MySelectionData.Selection = selectionData.Selection
            MySelectionData.RacingpostHorseData = getData<RacingpostDto.HorseDataDto> "racingpostHorseData" data
            MySelectionData.TimeformHorseData = getData<TimeformProviderPro.Models.TimeformData> "timeformHorseData" data
            MySelectionData.HorseResults = getData<HorseRaceResultData array> "horseResults" data
            MySelectionData.JockeyResults = getData<JockeyRaceResultForHorseData array> "jockeyResults" data
        }

    // LBBW Component: Last - Recent performance score
    let calculateLastScore (myData : MySelectionData) =
        match myData.HorseResults with
        | Some results when results.Length > 0 ->
            let lastRace = results.[0]
            // Weight recent performance: Win=1.0, Place(2-3)=0.6, 4th=0.3, else=0.0
            if lastRace.Position = 1uy then 1.0
            elif lastRace.Position <= 3uy then 0.6
            elif lastRace.Position = 4uy then 0.3
            else 0.0
        | _ -> 0.0

    // LBBW Component: Best - Peak recent performance rating
    let calculateBestScore (myData : MySelectionData) =
        match myData.HorseResults with
        | Some results when results.Length > 0 ->
            let recentRaces = results |> Array.take (min 5 results.Length)
            let bestRating = 
                recentRaces 
                |> Array.map (fun r -> max r.OfficialRating r.RacingpostRating)
                |> Array.max
            // Normalize to 0-1.0 scale (assume ratings typically 40-120)
            min 1.0 ((float bestRating - 40.0) / 80.0)
        | _ -> 0.0

    // LBBW Component: Base - Consistency score (0-3 scale)
    let calculateBaseScore (myData : MySelectionData) =
        match myData.HorseResults with
        | Some results when results.Length > 0 ->
            let recentRaces = results |> Array.take (min 5 results.Length)
            let top3Count = recentRaces |> Array.filter (fun r -> r.Position <= 3uy) |> Array.length
            let consistencyRatio = float top3Count / float recentRaces.Length
            consistencyRatio * 3.0  // Scale to 0-3
        | _ -> 0.0

    // LBBW Component: Weight - Suitability factors
    let calculateWeightScore (myData : MySelectionData) =
        match myData.TimeformHorseData with
        | Some timeform ->
            let mutable score = 0.0
            if timeform.SuitedByGoing then score <- score + 0.33
            if timeform.SuitedByCourse then score <- score + 0.33
            if timeform.SuitedByDistance then score <- score + 0.34
            score
        | _ -> 0.0

    // LBBW Component: Bonus - Special factors and improvements
    let calculateBonusScore (myData : MySelectionData) =
        match myData.TimeformHorseData with
        | Some timeform ->
            let mutable bonus = 0.0
            if timeform.TimeformImprover then bonus <- bonus + 0.5
            if timeform.HorseInForm then bonus <- bonus + 0.3
            if timeform.HorseWinnerLastTimeOut then bonus <- bonus + 0.2
            bonus
        | _ -> 0.0

    // Horse Form Score - Exponentially weighted recent performance
    let calculateHorseFormScore (myData : MySelectionData) =
        match myData.HorseResults with
        | Some results when results.Length > 0 ->
            let recentRaces = results |> Array.take (min 5 results.Length)
            let weightedScore = 
                recentRaces 
                |> Array.mapi (fun i race ->
                    let weight = Math.Pow(0.8, float i)  // Exponential decay
                    let positionScore = 
                        match race.Position with
                        | 1uy -> 100.0
                        | 2uy -> 70.0
                        | 3uy -> 50.0
                        | 4uy -> 30.0
                        | _ -> 10.0
                    let ratingBonus = float (max race.OfficialRating race.RacingpostRating) / 2.0
                    weight * (positionScore + ratingBonus)
                )
                |> Array.sum
            
            let timeformBonus = 
                match myData.TimeformHorseData with
                | Some tf ->
                    let mutable b = 0.0
                    if tf.TimeformTopRated then b <- b + 20.0
                    if tf.TimeformHorseInFocus then b <- b + 15.0
                    b
                | _ -> 0.0
            
            weightedScore + timeformBonus
        | _ -> 0.0

    // Jockey Form Score - Recent jockey performance
    let calculateJockeyFormScore (myData : MySelectionData) =
        match myData.JockeyResults with
        | Some results when results.Length > 0 ->
            let recentRides = results |> Array.take (min 20 results.Length)
            let wins = recentRides |> Array.filter (fun r -> r.Position = 1uy) |> Array.length
            let places = recentRides |> Array.filter (fun r -> r.Position <= 3uy) |> Array.length
            
            let winRate = float wins / float recentRides.Length
            let placeRate = float places / float recentRides.Length
            
            (winRate * 100.0) + (placeRate * 50.0)
        | _ -> 50.0  // Default average score

    // Composite Score - Weighted combination of Horse (80%) and Jockey (20%)
    let calculateCompositeScore (horseScore : float) (jockeyScore : float) =
        (horseScore * 0.8) + (jockeyScore * 0.2)

    // Implied Odds from Composite Score
    let calculateImpliedOdds (compositeScore : float) =
        if compositeScore > 0.0 then
            100.0 / compositeScore
        else
            999.0

    // Value Score - Market odds vs Implied odds
    let calculateValueScore (marketOdds : float) (impliedOdds : float) =
        if marketOdds > 0.0 then
            ((marketOdds - impliedOdds) / marketOdds) * 100.0
        else
            0.0

    let evaluate (market : Market) (mySelectionsData : MySelectionData list) =
        let selectionResults = 
            mySelectionsData 
            |> List.map (fun myData ->
                let lastScore = calculateLastScore myData
                let bestScore = calculateBestScore myData
                let baseScore = calculateBaseScore myData
                let weightScore = calculateWeightScore myData
                let bonusScore = calculateBonusScore myData
                let lbbwScore = lastScore + bestScore + baseScore + weightScore + bonusScore
                
                let horseFormScore = calculateHorseFormScore myData
                let jockeyFormScore = calculateJockeyFormScore myData
                let compositeScore = calculateCompositeScore horseFormScore jockeyFormScore
                let impliedOdds = calculateImpliedOdds compositeScore
                
                let marketOdds = 
                    try
                        match myData.Selection.PriceGridData.BestToBack with
                        | Some priceData -> priceData.Price
                        | None -> 0.0
                    with
                    | _ -> 0.0
                
                let valueScore = calculateValueScore marketOdds impliedOdds
                
                {
                    SelectionResult.MySelectionData = myData
                    LastScore = lastScore
                    BestScore = bestScore
                    BaseScore = baseScore
                    WeightScore = weightScore
                    BonusScore = bonusScore
                    LBBWScore = lbbwScore
                    HorseFormScore = horseFormScore
                    JockeyFormScore = jockeyFormScore
                    CompositeScore = compositeScore
                    ImpliedOdds = impliedOdds
                    ValueScore = valueScore
                }
            )
        
        let topSelectionResults = 
            selectionResults 
            |> List.sortByDescending (fun sr -> sr.LBBWScore)
            |> List.take (min 3 selectionResults.Length)
            |> List.map (fun sr ->
                {
                    SelectionResult.MySelectionData = sr.MySelectionData
                    LastScore = sr.LastScore
                    BestScore = sr.BestScore
                    BaseScore = sr.BaseScore
                    WeightScore = sr.WeightScore
                    BonusScore = sr.BonusScore
                    LBBWScore = sr.LBBWScore
                    HorseFormScore = sr.HorseFormScore
                    JockeyFormScore = sr.JockeyFormScore
                    CompositeScore = sr.CompositeScore
                    ImpliedOdds = sr.ImpliedOdds
                    ValueScore = sr.ValueScore
                }
            )
        
        {
            EvaluationResult.SelectionResults = selectionResults
            EvaluationResult.TopSelectionResults = topSelectionResults
        }

/// <summary>
/// TriggerStatus
/// </summary>
type TriggerStatus =
    | Initialize
    | WaitForDataContext
    | ReportData of EvaluationResult
    | ReportError of string

/// <summary>
/// HorseRacingRaceLBBWBotTrigger
/// </summary>
type HorseRacingRaceLBBWBotTrigger (market : Market, selection : Selection, botName : string, botTriggerParameters : BotTriggerParameters, myBfexplorer : IMyBfexplorer) as this =
    inherit BotTriggerBase (market, selection, botName, botTriggerParameters, myBfexplorer)

    let mutable status = TriggerStatus.Initialize

    let isMyHorseRacingMarket () =
        market.MarketInfo.BetEventType.Id = 7 && market.MarketDescription.MarketType = "WIN"

    let retrieveDataContext () =
        async {
            let bfexplorer = myBfexplorer.BfexplorerService.Bfexplorer

            match! bfexplorer.GetDataContextForMarket ([| "DbJockeysResults"; "DbHorsesResults"; "RacingpostDataForHorses"; "TimeformDataForHorses" |], market) with
            | DataResult.Success marketDataContext -> 

                let result = evaluate market (marketDataContext.SelectionsData |> List.map mapToMySelectionData)

                status <- TriggerStatus.ReportData result

            | DataResult.Failure errorMessage -> 
            
                status <- TriggerStatus.ReportError errorMessage

        }
        |> Async.Start

    let doReportData (result : EvaluationResult) =
        let sb = System.Text.StringBuilder()
        
        sb.AppendLine("=".PadRight(120, '=')) |> ignore
        sb.AppendLine("LBBW HORSE RACING ANALYSIS REPORT") |> ignore
        sb.AppendLine("=".PadRight(120, '=')) |> ignore
        sb.AppendLine() |> ignore
        
        sb.AppendLine(sprintf "%-30s %8s %8s %8s %8s %8s %10s" "Horse" "Last" "Best" "Base" "Weight" "Bonus" "LBBW Score") |> ignore
        sb.AppendLine("-".PadRight(120, '-')) |> ignore
        
        for sr in result.SelectionResults do
            sb.AppendLine(sprintf "%-30s %8.2f %8.2f %8.2f %8.2f %8.2f %10.2f" 
                sr.MySelectionData.Selection.Name 
                sr.LastScore 
                sr.BestScore 
                sr.BaseScore 
                sr.WeightScore 
                sr.BonusScore 
                sr.LBBWScore) |> ignore
        
        sb.AppendLine() |> ignore
        sb.AppendLine("=".PadRight(120, '=')) |> ignore
        sb.AppendLine("TOP 3 SELECTIONS (LBBW ≥ 3.5 indicates strong value)") |> ignore
        sb.AppendLine("=".PadRight(120, '=')) |> ignore
        
        for i, tsr in result.TopSelectionResults |> List.mapi (fun i tsr -> (i + 1, tsr)) do
            sb.AppendLine(sprintf "%d. %-30s LBBW: %.2f" i tsr.MySelectionData.Selection.Name tsr.LBBWScore) |> ignore
            sb.AppendLine(sprintf "   Last: %.2f | Best: %.2f | Base: %.2f | Weight: %.2f | Bonus: %.2f" 
                tsr.LastScore tsr.BestScore tsr.BaseScore tsr.WeightScore tsr.BonusScore) |> ignore
        
        sb.AppendLine("=".PadRight(120, '=')) |> ignore
        
        this.Report (sb.ToString())

    interface IBotTrigger with

        /// <summary>
        /// Execute
        /// </summary>
        member _this.Execute () =
            match status with
            | TriggerStatus.Initialize ->

                if isMyHorseRacingMarket ()
                then
                    retrieveDataContext ()

                    status <- TriggerStatus.WaitForDataContext

                    TriggerResult.WaitingForOperation
                else
                    TriggerResult.EndExecutionWithMessage "You can run this bot on a horse racing market only!"

            | TriggerStatus.WaitForDataContext -> TriggerResult.WaitingForOperation
            | TriggerStatus.ReportData result ->

                doReportData result

                TriggerResult.EndExecution

            | TriggerStatus.ReportError errorMessage -> TriggerResult.EndExecutionWithMessage errorMessage

        /// <summary>
        /// EndExecution
        /// </summary>
        member _this.EndExecution () =
            ()
