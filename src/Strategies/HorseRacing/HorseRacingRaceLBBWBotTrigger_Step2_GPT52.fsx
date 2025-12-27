// Bfexplorer cannot be held responsible for any losses or damages incurred during the use of this betfair bot.
// It is up to you to determine the level of risk you wish to trade under. 
// Do not gamble with money you cannot afford to lose.

module HorseRacingBotTrigger

//#I @"C:\Program Files\BeloSoft\Bfexplorer\"
#I @"E:\Projects\Bfexplorer\Development\Applications\BeloSoft.Bfexplorer.App\bin\Debug\net9.0-windows\"

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
        MarketName : string
        GeneratedAtUtc : DateTime
        SelectionResults : SelectionResult list        
        TopSelectionResults : TopSelectionResult list
    }

and LBBWScore =
    {
        Last : float
        Best : float
        Base : float
        Weight : float
        Bonuses : float
        Total : float
    }

and SelectionResult =
    {
        MySelectionData : MySelectionData
        MarketOdds : float option
        LBBW : LBBWScore
        HorseFormScore : float
        JockeyFormScore : float
        CompositeScore : float
        ImpliedOdds : float option
        ValueScorePercent : float option
        Notes : string
    }

and TopSelectionResult =
    {
        Rank : int
        Selection : SelectionResult
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

    let clamp (minValue : float) (maxValue : float) (value : float) =
        if Double.IsNaN value || Double.IsInfinity value then minValue
        elif value < minValue then minValue
        elif value > maxValue then maxValue
        else value

    let tryGetBestToBackPrice (selection : Selection) =
        try
            match selection.PriceGridData.BestToBack with
            | Some priceData when priceData.Price > 0.0 -> Some priceData.Price
            | _ -> None
        with
        | _ -> None

    let parseWeight (weightStr : string) =
        try
            if String.IsNullOrWhiteSpace weightStr then 0
            else
                let parts = weightStr.Split('-')
                if parts.Length = 2 then
                    (int parts.[0] * 14) + int parts.[1]
                else
                    int weightStr
        with
        | _ -> 0

    let getMetaData (key : string) (selection : Selection) =
        match selection.Data.TryFind key with
        | Some v -> string v
        | None -> ""

    let tryGetAge (selection : Selection) =
        let raw = getMetaData "Age" selection
        try if String.IsNullOrWhiteSpace raw then None else Some (int raw)
        with | _ -> None

    let tryGetWeightLbs (selection : Selection) =
        let raw = getMetaData "Weight" selection
        if String.IsNullOrWhiteSpace raw then None
        else
            try Some (parseWeight raw) with | _ -> None

    let mapToMySelectionData (selectionData : SelectionDataContext) =
        let data = selectionData.Data

        {
            MySelectionData.Selection = selectionData.Selection
            MySelectionData.RacingpostHorseData = getData<RacingpostDto.HorseDataDto> "racingpostHorseData" data
            MySelectionData.TimeformHorseData = getData<TimeformProviderPro.Models.TimeformData> "timeformHorseData" data
            MySelectionData.HorseResults = getData<HorseRaceResultData array> "horseResults" data
            MySelectionData.JockeyResults = getData<JockeyRaceResultForHorseData array> "horseResults" data
        }

    // ---- LBBW v2 (0..5) scoring ----

    let private positionToLastScore (position : byte) =
        // Max 0.50
        match position with
        | 1uy -> 0.50
        | p when p <= 3uy -> 0.30
        | 4uy -> 0.15
        | _ -> 0.0

    let private normalizeRatingToHalfPoint (rating : int) =
        // Normalize typical 40..120 rating to 0..0.50
        let r = clamp 40.0 120.0 (float rating)
        ((r - 40.0) / 80.0) * 0.50

    let private calculateLBBW (data : MySelectionData) (maxWeightInFieldLbs : int option) =
        let last =
            match data.HorseResults with
            | Some results when results.Length > 0 -> positionToLastScore results.[0].Position
            | _ -> 0.0

        let best =
            match data.HorseResults with
            | Some results when results.Length > 0 ->
                let recent = results |> Array.take (min 5 results.Length)
                let bestRating =
                    recent
                    |> Array.map (fun r -> max (int r.OfficialRating) (int r.RacingpostRating))
                    |> Array.max
                normalizeRatingToHalfPoint bestRating
            | _ -> 0.0

        let baseScore =
            // 0..3
            match data.HorseResults with
            | Some results when results.Length > 0 ->
                let recent = results |> Array.take (min 5 results.Length)
                let top3Count = recent |> Array.filter (fun r -> r.Position > 0uy && r.Position <= 3uy) |> Array.length
                (float top3Count / float recent.Length) * 3.0
            | _ -> 0.0

        let weightScore =
            // 0..0.5, blend "top weight" + Timeform suitability
            let mutable score = 0.0

            match maxWeightInFieldLbs, tryGetWeightLbs data.Selection with
            | Some maxLbs, Some lbs when maxLbs > 0 && lbs >= maxLbs ->
                score <- score + 0.20
            | _ -> ()

            match data.TimeformHorseData with
            | Some tf ->
                if tf.SuitedByGoing then score <- score + 0.10
                if tf.SuitedByCourse then score <- score + 0.10
                if tf.SuitedByDistance then score <- score + 0.10
            | _ -> ()

            clamp 0.0 0.50 score

        let bonuses =
            // 0..0.5
            let mutable bonus = 0.0

            match tryGetAge data.Selection with
            | Some 3 -> bonus <- bonus + 0.15
            | _ -> ()

            match data.TimeformHorseData with
            | Some tf ->
                if tf.TimeformImprover then bonus <- bonus + 0.20
                if tf.HorseInForm then bonus <- bonus + 0.10
                if tf.HorseWinnerLastTimeOut then bonus <- bonus + 0.05
                if tf.TimeformTopRated then bonus <- bonus + 0.05
            | _ -> ()

            // Consistency bonus: multiple recent top-3
            match data.HorseResults with
            | Some results when results.Length > 0 ->
                let recent = results |> Array.take (min 5 results.Length)
                let top3Count = recent |> Array.filter (fun r -> r.Position > 0uy && r.Position <= 3uy) |> Array.length
                if top3Count >= 3 then bonus <- bonus + 0.05
            | _ -> ()

            clamp 0.0 0.50 bonus

        let total = clamp 0.0 5.0 (last + best + baseScore + weightScore + bonuses)

        {
            LBBWScore.Last = Math.Round(last, 2)
            Best = Math.Round(best, 2)
            Base = Math.Round(baseScore, 2)
            Weight = Math.Round(weightScore, 2)
            Bonuses = Math.Round(bonuses, 2)
            Total = Math.Round(total, 2)
        }

    // ---- ExpertHorseRacingAnalyst composite/value scoring ----

    let private positionTo100Score (position : byte) =
        match position with
        | 1uy -> 100.0
        | 2uy -> 70.0
        | 3uy -> 50.0
        | 4uy -> 30.0
        | _ -> 10.0

    let private normalizeRatingTo30 (rating : int) =
        // 40..120 -> 0..30
        let r = clamp 40.0 120.0 (float rating)
        ((r - 40.0) / 80.0) * 30.0

    let private ema (alpha : float) (values : float array) =
        if values.Length = 0 then 0.0
        else
            let a = clamp 0.01 1.0 alpha
            let mutable s = values.[0]
            for i = 1 to values.Length - 1 do
                s <- (a * values.[i]) + ((1.0 - a) * s)
            s

    let private calculateHorseFormScore (data : MySelectionData) =
        let baseScore =
            match data.HorseResults with
            | Some results when results.Length > 0 ->
                let recent = results |> Array.take (min 5 results.Length)
                let perRace =
                    recent
                    |> Array.map (fun r ->
                        let pos = positionTo100Score r.Position
                        let rating = normalizeRatingTo30 (max (int r.OfficialRating) (int r.RacingpostRating))
                        clamp 0.0 100.0 (pos * 0.7 + rating * 1.0)
                    )
                ema 0.45 perRace
            | _ -> 50.0

        let tfBonus =
            match data.TimeformHorseData with
            | Some tf ->
                let mutable b = 0.0
                if tf.TimeformTopRated then b <- b + 8.0
                if tf.HorseInForm then b <- b + 6.0
                if tf.TimeformHorseInFocus then b <- b + 4.0
                if tf.TimeformImprover then b <- b + 4.0
                b
            | _ -> 0.0

        clamp 0.0 100.0 (baseScore + tfBonus)

    let private calculateJockeyFormScore (data : MySelectionData) =
        match data.JockeyResults with
        | Some results when results.Length > 0 ->
            let recent = results |> Array.take (min 20 results.Length)
            let perRide = recent |> Array.map (fun r -> positionTo100Score r.Position)
            clamp 0.0 100.0 (ema 0.35 perRide)
        | _ -> 50.0

    let private calculateCompositeScore (marketOdds : float) (horseScore : float) (jockeyScore : float) (data : MySelectionData) =
        let compositeBase = (horseScore * 0.8) + (jockeyScore * 0.2)

        // "Wisdom of the crowd" adjustment: if strong favourite, slightly trust positives.
        let boosted =
            if marketOdds > 0.0 && marketOdds <= 4.0 then
                match data.TimeformHorseData with
                | Some tf when tf.TimeformTopRated || tf.HorseInForm -> compositeBase * 1.05
                | _ -> compositeBase
            else compositeBase

        clamp 0.0 100.0 boosted

    let private impliedOddsFromComposite (compositeScore : float) =
        if compositeScore > 0.0 then Some (100.0 / compositeScore) else None

    let private valueScorePercent (marketOdds : float) (impliedOdds : float) =
        if marketOdds > 0.0 then Some (((marketOdds - impliedOdds) / marketOdds) * 100.0) else None

    let private notesFor (data : MySelectionData) (lbbw : LBBWScore) (valuePct : float) =
        let parts = ResizeArray<string>()

        if lbbw.Total >= 3.5 then parts.Add("LBBW≥3.5")
        if valuePct >= 5.0 then parts.Add("Value")

        match data.TimeformHorseData with
        | Some tf ->
            if tf.TimeformTopRated then parts.Add("TF Top")
            if tf.TimeformImprover then parts.Add("TF Improver")
            if tf.HorseInForm then parts.Add("InForm")
            if tf.HorseWinnerLastTimeOut then parts.Add("LTO Win")
        | _ -> ()

        match tryGetAge data.Selection with
        | Some 3 -> parts.Add("3yo")
        | _ -> ()

        String.Join(", ", parts)

    let evaluate (market : Market) (mySelectionsData : MySelectionData list) =
        let maxWeightInFieldLbs =
            mySelectionsData
            |> List.choose (fun s -> tryGetWeightLbs s.Selection)
            |> (fun ws -> if ws.Length > 0 then Some (List.max ws) else None)

        let selectionResults =
            mySelectionsData
            |> List.map (fun data ->
                let marketOddsOpt = tryGetBestToBackPrice data.Selection
                let marketOddsForAdjust = marketOddsOpt |> Option.defaultValue 0.0

                let lbbw = calculateLBBW data maxWeightInFieldLbs
                let horseScore = calculateHorseFormScore data
                let jockeyScore = calculateJockeyFormScore data
                let composite = calculateCompositeScore marketOddsForAdjust horseScore jockeyScore data
                let impliedOddsOpt = impliedOddsFromComposite composite
                let valuePctOpt =
                    match marketOddsOpt, impliedOddsOpt with
                    | Some marketOdds, Some impliedOdds -> valueScorePercent marketOdds impliedOdds
                    | _ -> None

                let notes =
                    let v = valuePctOpt |> Option.defaultValue 0.0
                    notesFor data lbbw v

                {
                    SelectionResult.MySelectionData = data
                    MarketOdds = marketOddsOpt
                    LBBW = lbbw
                    HorseFormScore = Math.Round(horseScore, 1)
                    JockeyFormScore = Math.Round(jockeyScore, 1)
                    CompositeScore = Math.Round(composite, 1)
                    ImpliedOdds = impliedOddsOpt |> Option.map (fun v -> Math.Round(v, 2))
                    ValueScorePercent = valuePctOpt |> Option.map (fun v -> Math.Round(v, 1))
                    Notes = notes
                }
            )

        let topSelectionResults =
            let candidates =
                selectionResults
                |> List.sortByDescending (fun s ->
                    let value = s.ValueScorePercent |> Option.defaultValue -1e9
                    (s.LBBW.Total, value, s.CompositeScore)
                )

            candidates
            |> List.take (min 5 candidates.Length)
            |> List.mapi (fun i s -> { TopSelectionResult.Rank = i + 1; Selection = s })

        {
            EvaluationResult.MarketName =
                try market.MarketInfo.MarketName with | _ -> ""
            GeneratedAtUtc = DateTime.UtcNow
            SelectionResults = selectionResults
            TopSelectionResults = topSelectionResults
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
        let fmt2 (v : float) = sprintf "%.2f" v
        let fmt1 (v : float) = sprintf "%.1f" v
        let fmtPct (v : float) = sprintf "%.1f%%" v
        let fmtOdds (v : float) = sprintf "%.2f" v

        let fmtOpt (fmt : 'a -> string) (value : 'a option) =
            match value with
            | Some v -> fmt v
            | None -> "-"

        let notesOrEmpty (notes : string) =
            if String.IsNullOrWhiteSpace notes then "" else notes

        let topHeader =
            [
                "TOP SELECTIONS"
                "| Rank | Horse | Odds | LBBW | Last | Best | Base | Wt | Bonus | Comp | Impl | Value | Notes |"
                "|---:|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---|"
            ]

        let fullHeader =
            [
                "FULL FIELD"
                "| Horse | Odds | LBBW | Last | Best | Base | Wt | Bonus | Horse | Jockey | Comp | Impl | Value | Notes |"
                "|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---|"
            ]

        let renderTopRow (item : TopSelectionResult) =
            let s = item.Selection
            sprintf "| %d | %s | %s | %s | %s | %s | %s | %s | %s | %s | %s | %s | %s |"
                item.Rank
                s.MySelectionData.Selection.Name
                (fmtOpt fmtOdds s.MarketOdds)
                (fmt2 s.LBBW.Total)
                (fmt2 s.LBBW.Last)
                (fmt2 s.LBBW.Best)
                (fmt2 s.LBBW.Base)
                (fmt2 s.LBBW.Weight)
                (fmt2 s.LBBW.Bonuses)
                (fmt1 s.CompositeScore)
                (fmtOpt fmtOdds s.ImpliedOdds)
                (fmtOpt fmtPct s.ValueScorePercent)
                (notesOrEmpty s.Notes)

        let renderFullRow (s : SelectionResult) =
            sprintf "| %s | %s | %s | %s | %s | %s | %s | %s | %s | %s | %s | %s | %s | %s |"
                s.MySelectionData.Selection.Name
                (fmtOpt fmtOdds s.MarketOdds)
                (fmt2 s.LBBW.Total)
                (fmt2 s.LBBW.Last)
                (fmt2 s.LBBW.Best)
                (fmt2 s.LBBW.Base)
                (fmt2 s.LBBW.Weight)
                (fmt2 s.LBBW.Bonuses)
                (fmt1 s.HorseFormScore)
                (fmt1 s.JockeyFormScore)
                (fmt1 s.CompositeScore)
                (fmtOpt fmtOdds s.ImpliedOdds)
                (fmtOpt fmtPct s.ValueScorePercent)
                (notesOrEmpty s.Notes)

        let reportHeader =
            [
                "LBBW HORSE RACING ANALYSIS (v2)"
                (if String.IsNullOrWhiteSpace result.MarketName then "" else sprintf "Market: %s" result.MarketName)
                (sprintf "Generated (UTC): %O" result.GeneratedAtUtc)
                ""
            ]
            |> List.filter (fun s -> not (String.IsNullOrWhiteSpace s) || s = "")

        let topLines =
            topHeader @ (result.TopSelectionResults |> List.map renderTopRow)

        let ordered =
            result.SelectionResults
            |> List.sortByDescending (fun s ->
                let value = s.ValueScorePercent |> Option.defaultValue -1e9
                (value, s.LBBW.Total, s.CompositeScore)
            )

        let fullLines =
            fullHeader @ (ordered |> List.map renderFullRow)

        let reportText =
            [ reportHeader; topLines; [ "" ]; fullLines ]
            |> List.concat
            |> String.concat Environment.NewLine

        this.Report reportText

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
