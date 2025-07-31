using System.Text.Json;
using BetfairMarketAnalyzer.Models;
using BetfairMarketAnalyzer.Services;

namespace BetfairMarketAnalyzer;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Betfair Market Analyzer using Skender.Stock.Indicators");
        Console.WriteLine("======================================================");
        Console.WriteLine();

        try
        {
            // Sample data - in real implementation, this would come from Bfexplorer API
            var sampleData = GenerateSampleData();
            
            // Initialize services
            var analysisService = new TechnicalAnalysisService();
            var reportService = new ReportGeneratorService();
            
            // Perform technical analysis
            Console.WriteLine("Performing technical analysis...");
            var analysisResult = analysisService.AnalyzeSelection(sampleData);
            
            // Generate comprehensive report
            Console.WriteLine("Generating comprehensive report...");
            var report = reportService.GenerateComprehensiveReport(analysisResult, sampleData.Market);
            
            // Display report
            Console.WriteLine();
            Console.WriteLine(report);
            
            // Save report to file
            var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            var filename = $"BetfairAnalysis_{timestamp}.md";
            await File.WriteAllTextAsync(filename, report);
            Console.WriteLine($"\nReport saved to: {filename}");
            
            // Display key metrics in console
            DisplayKeyMetrics(analysisResult);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
        }
        
        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }

    private static MarketSelectionData GenerateSampleData()
    {
        // This simulates the real data structure from Bfexplorer
        // In production, this would be replaced with actual API calls
        
        var marketData = new MarketData
        {
            MarketId = "1.246135530",
            MarketName = "1m4f Hcap",
            EventName = "Galway Horse Racing",
            EventType = "Horse Racing",
            Status = "Open",
            StartTime = DateTime.Now.AddMinutes(5) // Race starts in 5 minutes
        };

        var selection = new Selection
        {
            SelectionId = "81530996_0.00",
            Name = "Glenroyal",
            Price = 3.45m,
            Status = "Active"
        };

        // Generate realistic price history data
        var priceHistory = GenerateRealisticPriceHistory();

        var selectionWithHistory = new SelectionWithHistory
        {
            Selection = selection,
            TradedPricesAndVolume = priceHistory
        };

        return new MarketSelectionData
        {
            Market = marketData,
            Selections = new List<SelectionWithHistory> { selectionWithHistory }
        };
    }

    private static List<TradedPriceVolume> GenerateRealisticPriceHistory()
    {
        var history = new List<TradedPriceVolume>();
        var random = new Random();
        var baseTime = DateTime.Now.AddHours(-2);
        var currentPrice = 4.88m;
        
        // Simulate realistic price movements based on the actual data pattern
        var pricePoints = new[]
        {
            4.88m, 4.78m, 4.69m, 4.59m, 4.50m, 4.41m, 3.56m, 3.52m, 3.75m, 2.95m,
            3.19m, 3.05m, 2.91m, 2.86m, 2.74m, 2.72m, 2.63m, 2.59m, 2.81m, 2.86m,
            3.24m, 3.28m, 3.05m, 3.00m, 2.95m, 2.91m, 2.86m, 2.66m, 2.81m, 2.79m,
            3.25m, 3.45m, 3.50m, 3.60m, 3.70m, 3.75m, 3.85m, 3.95m, 3.70m, 3.50m,
            3.95m, 3.45m, 3.90m, 3.45m, 3.35m, 3.30m, 3.20m, 3.45m
        };

        for (int i = 0; i < pricePoints.Length; i++)
        {
            var volume = random.Next(1, 100);
            if (i > 30 && Math.Abs(pricePoints[i] - (i > 0 ? pricePoints[i-1] : pricePoints[i])) > 0.1m)
            {
                volume = random.Next(50, 500); // Higher volume on significant moves
            }

            history.Add(new TradedPriceVolume
            {
                Time = baseTime.AddMinutes(i * 2),
                Price = pricePoints[i],
                Volume = volume
            });
        }

        return history;
    }

    private static void DisplayKeyMetrics(AnalysisResult result)
    {
        Console.WriteLine();
        Console.WriteLine("=== KEY METRICS SUMMARY ===");
        Console.WriteLine($"Selection: {result.SelectionName}");
        Console.WriteLine($"Current Odds: {result.CurrentOdds:F2}");
        Console.WriteLine($"Implied Probability: {result.ImpliedProbability:F1}%");
        Console.WriteLine($"Odds Movement: {result.OddsMovement:+0.00;-0.00} ({result.OddsMovementPercentage:+0.0;-0.0}%)");
        Console.WriteLine($"Trend: {result.TrendDirection}");
        Console.WriteLine($"Total Volume: {result.TotalVolume:F0}");
        Console.WriteLine($"Market Flow: {result.MarketFlow.DominantDirection}");
        Console.WriteLine($"RSI: {result.Indicators.RSI?.ToString("F1") ?? "N/A"}");
        
        if (result.TradingRecommendations.Any())
        {
            var topRec = result.TradingRecommendations.First();
            Console.WriteLine($"Top Strategy: {topRec.Strategy}");
            Console.WriteLine($"Confidence: {topRec.ConfidenceLevel}/10");
        }
        
        Console.WriteLine("============================");
    }
}
