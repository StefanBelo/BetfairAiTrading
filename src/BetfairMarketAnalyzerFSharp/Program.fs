open System
open System.IO
open BetfairMarketAnalyzerFSharp.Models
open BetfairMarketAnalyzerFSharp.Services

let generateRealisticPriceHistory () =
    let random = Random()
    let baseTime = DateTime.Now.AddHours(-2.0)
    
    let pricePoints = [|
        4.88m; 4.78m; 4.69m; 4.59m; 4.50m; 4.41m; 3.56m; 3.52m; 3.75m; 2.95m;
        3.19m; 3.05m; 2.91m; 2.86m; 2.74m; 2.72m; 2.63m; 2.59m; 2.81m; 2.86m;
        3.24m; 3.28m; 3.05m; 3.00m; 2.95m; 2.91m; 2.86m; 2.66m; 2.81m; 2.79m;
        3.25m; 3.45m; 3.50m; 3.60m; 3.70m; 3.75m; 3.85m; 3.95m; 3.70m; 3.50m;
        3.95m; 3.45m; 3.90m; 3.45m; 3.35m; 3.30m; 3.20m; 3.45m
    |]

    pricePoints
    |> Array.mapi (fun i price ->
        let volume = decimal (random.Next(1, 100))
        let adjustedVolume = 
            if i > 30 && (i > 0 && Math.Abs(price - pricePoints.[i-1]) > 0.1m) then
                decimal (random.Next(50, 500))
            else
                volume

        {
            Time = baseTime.AddMinutes(float (i * 2))
            Price = price
            Volume = adjustedVolume
        })
    |> Array.toList

let generateSampleData () =
    let marketData = {
        MarketId = "1.246135530"
        MarketName = "1m4f Hcap"
        EventName = "Galway Horse Racing"
        EventType = "Horse Racing"
        Status = "Open"
        StartTime = DateTime.Now.AddMinutes(5.0)
    }

    let selection = {
        SelectionId = "81530996_0.00"
        Name = "Glenroyal"
        Price = 3.45m
        Status = "Active"
    }

    let priceHistory = generateRealisticPriceHistory ()

    let selectionWithHistory = {
        Selection = selection
        TradedPricesAndVolume = priceHistory
    }

    {
        Market = marketData
        Selections = [selectionWithHistory]
    }

let displayKeyMetrics (result: AnalysisResult) =
    printfn ""
    printfn "=== KEY METRICS SUMMARY ==="
    printfn "Selection: %s" result.SelectionName
    printfn "Current Odds: %.2f" result.CurrentOdds
    printfn "Implied Probability: %.1f%%" result.ImpliedProbability
    printfn "Trend: %s" result.TrendDirection
    printfn "Total Volume: %.0f" result.TotalVolume
    printfn "Market Flow: %s" result.MarketFlow.DominantDirection
    
    let rsiText = result.Indicators.RSI |> Option.map (sprintf "%.1f") |> Option.defaultValue "N/A"
    printfn "RSI: %s" rsiText
    
    match result.TradingRecommendations with
    | topRec :: _ ->
        printfn "Top Strategy: %s" topRec.Strategy
        printfn "Confidence: %d/10" topRec.ConfidenceLevel
    | [] -> ()
    
    printfn "============================"

[<EntryPoint>]
let main args =
    printfn "Betfair Market Analyzer using Skender.Stock.Indicators (F# Version)"
    printfn "====================================================================="
    printfn ""

    try
        let sampleData = generateSampleData ()
        
        printfn "Performing technical analysis..."
        let analysisResult = TechnicalAnalysisService.analyzeSelection sampleData
        
        printfn "Generating comprehensive report..."
        let report = ReportGeneratorService.generateComprehensiveReport analysisResult sampleData.Market
        
        printfn ""
        printfn "%s" report
        
        let timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss")
        let filename = sprintf "BetfairAnalysis_%s.md" timestamp
        File.WriteAllText(filename, report)
        printfn "\nReport saved to: %s" filename
        
        displayKeyMetrics analysisResult
        
        0
    with
    | ex ->
        printfn "Error: %s" ex.Message
        printfn "Stack trace: %s" ex.StackTrace
        1
