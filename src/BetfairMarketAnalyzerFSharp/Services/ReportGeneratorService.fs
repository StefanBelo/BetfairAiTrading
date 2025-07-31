namespace BetfairMarketAnalyzerFSharp.Services

open System
open System.Text
open BetfairMarketAnalyzerFSharp.Models

module ReportGeneratorService =

    let generateComprehensiveReport (analysis: AnalysisResult) (marketData: MarketData) =
        let report = StringBuilder()
        
        report.AppendLine("# Betfair Market Analysis Report (F# Version)") |> ignore
        report.AppendLine("## Generated using Skender.Stock.Indicators Technical Analysis") |> ignore
        report.AppendLine() |> ignore
        report.AppendLine(sprintf "**Analysis Time**: %s" (analysis.AnalysisTime.ToString("yyyy-MM-dd HH:mm:ss"))) |> ignore
        report.AppendLine(sprintf "**Time to Race Start**: %O" analysis.TimeToRaceStart) |> ignore
        report.AppendLine() |> ignore
        
        report.AppendLine("## Market Information") |> ignore
        report.AppendLine(sprintf "- **Market**: %s" marketData.MarketName) |> ignore
        report.AppendLine(sprintf "- **Event**: %s" marketData.EventName) |> ignore
        report.AppendLine(sprintf "- **Market ID**: %s" marketData.MarketId) |> ignore
        report.AppendLine(sprintf "- **Status**: %s" marketData.Status) |> ignore
        report.AppendLine() |> ignore
        
        report.AppendLine("## Selection Analysis") |> ignore
        report.AppendLine(sprintf "- **Selection Name**: %s" analysis.SelectionName) |> ignore
        report.AppendLine(sprintf "- **Current Odds**: %.2f" analysis.CurrentOdds) |> ignore
        report.AppendLine(sprintf "- **Implied Probability**: %.1f%%" analysis.ImpliedProbability) |> ignore
        report.AppendLine(sprintf "- **Opening Odds**: %.2f" analysis.OpeningOdds) |> ignore
        report.AppendLine(sprintf "- **Odds Movement**: %.2f points" analysis.OddsMovement) |> ignore
        report.AppendLine(sprintf "- **Trend Direction**: %s" analysis.TrendDirection) |> ignore
        report.AppendLine(sprintf "- **Total Volume**: %.1f units" analysis.TotalVolume) |> ignore
        report.AppendLine() |> ignore
        
        report.AppendLine("## Technical Indicators") |> ignore
        let rsiText = analysis.Indicators.RSI |> Option.map (sprintf "%.1f") |> Option.defaultValue "N/A"
        report.AppendLine(sprintf "- **RSI**: %s" rsiText) |> ignore
        report.AppendLine(sprintf "- **Momentum Direction**: %s" analysis.Indicators.MomentumDirection) |> ignore
        report.AppendLine() |> ignore
        
        report.AppendLine("## Market Flow") |> ignore
        report.AppendLine(sprintf "- **Dominant Direction**: %s" analysis.MarketFlow.DominantDirection) |> ignore
        report.AppendLine(sprintf "- **Backing Pressure**: %.1f%%" analysis.MarketFlow.BackingPressure) |> ignore
        report.AppendLine(sprintf "- **Laying Pressure**: %.1f%%" analysis.MarketFlow.LayingPressure) |> ignore
        report.AppendLine(sprintf "- **Market Sentiment**: %s" analysis.MarketFlow.MarketSentiment) |> ignore
        report.AppendLine() |> ignore
        
        if not analysis.TradingRecommendations.IsEmpty then
            report.AppendLine("## Trading Recommendations") |> ignore
            analysis.TradingRecommendations
            |> List.iteri (fun i recommendation ->
                report.AppendLine(sprintf "### Strategy %d: %s" (i + 1) recommendation.Strategy) |> ignore
                report.AppendLine(sprintf "- **Action**: %s" recommendation.Action) |> ignore
                report.AppendLine(sprintf "- **Entry Price**: %.2f" recommendation.EntryPrice) |> ignore
                report.AppendLine(sprintf "- **Confidence Level**: %d/10" recommendation.ConfidenceLevel) |> ignore
                report.AppendLine(sprintf "- **Logic**: %s" recommendation.Logic) |> ignore
                report.AppendLine() |> ignore)
        
        report.AppendLine("---") |> ignore
        report.AppendLine("*Analysis generated using F# functional programming and Skender.Stock.Indicators*") |> ignore
        report.AppendLine(sprintf "*Report generated at: %s*" (DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))) |> ignore

        report.ToString()
