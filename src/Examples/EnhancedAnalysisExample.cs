using System;
using System.Collections.Generic;
using BetfairAiTrading.TechnicalAnalysis;

namespace BetfairAiTrading.Examples
{
    public class EnhancedTechnicalAnalysisExample
    {
        public void RunComprehensiveAnalysis()
        {
            // Sample Betfair price data (based on Saliko example)
            var priceData = new List<PricePoint>
            {
                new() { Time = DateTime.Parse("2025-07-31T08:00:00"), Price = 3.88m, Volume = 2.4m },
                new() { Time = DateTime.Parse("2025-07-31T08:30:00"), Price = 3.79m, Volume = 5.62m },
                new() { Time = DateTime.Parse("2025-07-31T09:00:00"), Price = 3.70m, Volume = 8.2m },
                new() { Time = DateTime.Parse("2025-07-31T09:30:00"), Price = 3.62m, Volume = 12.4m },
                new() { Time = DateTime.Parse("2025-07-31T10:00:00"), Price = 3.53m, Volume = 15.1m },
                new() { Time = DateTime.Parse("2025-07-31T10:30:00"), Price = 3.44m, Volume = 18.9m },
                new() { Time = DateTime.Parse("2025-07-31T11:00:00"), Price = 3.40m, Volume = 22.3m },
                new() { Time = DateTime.Parse("2025-07-31T11:30:00"), Price = 3.36m, Volume = 19.8m },
                new() { Time = DateTime.Parse("2025-07-31T12:00:00"), Price = 3.32m, Volume = 25.6m },
                new() { Time = DateTime.Parse("2025-07-31T12:30:00"), Price = 3.23m, Volume = 31.2m },
                new() { Time = DateTime.Parse("2025-07-31T13:00:00"), Price = 3.14m, Volume = 28.7m },
                new() { Time = DateTime.Parse("2025-07-31T13:30:00"), Price = 3.09m, Volume = 35.4m },
                new() { Time = DateTime.Parse("2025-07-31T14:00:00"), Price = 2.84m, Volume = 45.2m },
                new() { Time = DateTime.Parse("2025-07-31T14:30:00"), Price = 2.80m, Volume = 52.1m },
                new() { Time = DateTime.Parse("2025-07-31T15:00:00"), Price = 2.93m, Volume = 38.9m },
                new() { Time = DateTime.Parse("2025-07-31T15:30:00"), Price = 2.90m, Volume = 41.7m },
                new() { Time = DateTime.Parse("2025-07-31T16:00:00"), Price = 3.10m, Volume = 33.2m },
                new() { Time = DateTime.Parse("2025-07-31T16:30:00"), Price = 3.20m, Volume = 42.8m },
                new() { Time = DateTime.Parse("2025-07-31T17:00:00"), Price = 3.35m, Volume = 55.3m },
                new() { Time = DateTime.Parse("2025-07-31T17:30:00"), Price = 3.50m, Volume = 67.1m },
                new() { Time = DateTime.Parse("2025-07-31T18:00:00"), Price = 3.65m, Volume = 78.9m },
                new() { Time = DateTime.Parse("2025-07-31T18:10:00"), Price = 3.75m, Volume = 82.4m }
            };

            var analyzer = new EnhancedTechnicalAnalyzer();
            var analysis = analyzer.AnalyzeBetfairMarket(priceData);
            var currentPrice = priceData[^1].Price;

            // Print comprehensive analysis
            analyzer.PrintAnalysisReport(analysis, currentPrice);

            // Generate specific trading recommendations
            GenerateTradingRecommendations(analysis, currentPrice);
        }

        private void GenerateTradingRecommendations(
            EnhancedTechnicalAnalyzer.ComprehensiveTechnicalAnalysis analysis, 
            decimal currentPrice)
        {
            Console.WriteLine("\n=== TRADING RECOMMENDATIONS ===");

            var recommendations = new List<string>();

            // Primary strategy based on overall sentiment
            switch (analysis.OverallSentiment)
            {
                case "BULLISH" when analysis.ConfidenceScore > 70:
                    recommendations.Add("🟢 PRIMARY: BACKING STRATEGY");
                    recommendations.Add($"   Entry: Back at {currentPrice:F2} or below");
                    
                    if (analysis.NearestResistance != null)
                    {
                        recommendations.Add($"   Target: Lay at {analysis.NearestResistance.Price:F2} (resistance)");
                        var profitPotential = (analysis.NearestResistance.Price - currentPrice) / currentPrice * 100;
                        recommendations.Add($"   Profit Potential: {profitPotential:F1}%");
                    }
                    break;

                case "BEARISH" when analysis.ConfidenceScore > 70:
                    recommendations.Add("🔴 PRIMARY: LAYING STRATEGY");
                    recommendations.Add($"   Entry: Lay at {currentPrice:F2} or above");
                    
                    if (analysis.NearestSupport != null)
                    {
                        recommendations.Add($"   Target: Back at {analysis.NearestSupport.Price:F2} (support)");
                        var profitPotential = (currentPrice - analysis.NearestSupport.Price) / currentPrice * 100;
                        recommendations.Add($"   Profit Potential: {profitPotential:F1}%");
                    }
                    break;

                default:
                    recommendations.Add("🟡 PRIMARY: RANGE TRADING");
                    if (analysis.NearestSupport != null && analysis.NearestResistance != null)
                    {
                        recommendations.Add($"   Back near support: {analysis.NearestSupport.Price:F2}");
                        recommendations.Add($"   Lay near resistance: {analysis.NearestResistance.Price:F2}");
                    }
                    break;
            }

            // RSI-based recommendations
            if (analysis.RSI.HasValue)
            {
                if (analysis.RSI.Value > 70)
                {
                    recommendations.Add("📉 RSI STRATEGY: Lay position (overbought)");
                    recommendations.Add($"   Wait for RSI to drop below 65 for confirmation");
                }
                else if (analysis.RSI.Value < 30)
                {
                    recommendations.Add("📈 RSI STRATEGY: Back position (oversold)");
                    recommendations.Add($"   Wait for RSI to rise above 35 for confirmation");
                }
            }

            // Bollinger Bands strategy
            if (analysis.BollingerUpper.HasValue && analysis.BollingerLower.HasValue)
            {
                if (currentPrice > analysis.BollingerUpper.Value)
                {
                    recommendations.Add("💫 BOLLINGER STRATEGY: Mean reversion lay");
                    recommendations.Add($"   Target: {analysis.BollingerMiddle:F2} (middle band)");
                }
                else if (currentPrice < analysis.BollingerLower.Value)
                {
                    recommendations.Add("💫 BOLLINGER STRATEGY: Mean reversion back");
                    recommendations.Add($"   Target: {analysis.BollingerMiddle:F2} (middle band)");
                }
            }

            // Risk management
            recommendations.Add("\n⚠️  RISK MANAGEMENT:");
            recommendations.Add($"   Position Size: 2-3% of bankroll (Confidence: {analysis.ConfidenceScore:F0}%)");
            
            if (analysis.ATR.HasValue)
            {
                var stopLoss = currentPrice * 0.02m; // 2% stop loss
                recommendations.Add($"   Stop Loss: ±{stopLoss:F2} from entry ({analysis.ATR:F4} ATR)");
            }

            // Time-based considerations
            recommendations.Add($"   Time to Race: Consider market volatility increase");
            recommendations.Add($"   Exit all positions 30-60 seconds before race start");

            foreach (var recommendation in recommendations)
            {
                Console.WriteLine(recommendation);
            }

            // Summary score
            Console.WriteLine($"\n📊 CONFIDENCE SCORE: {analysis.ConfidenceScore:F0}%");
            Console.WriteLine($"💡 SIGNAL COUNT: {analysis.TradingSignals.Count} active signals");
        }

        // Example of specific indicator access
        public void ShowSpecificIndicatorUsage(List<PricePoint> priceData)
        {
            var analyzer = new EnhancedTechnicalAnalyzer();
            var analysis = analyzer.AnalyzeBetfairMarket(priceData);

            Console.WriteLine("=== SPECIFIC INDICATOR VALUES ===");

            // Access specific values for custom logic
            if (analysis.RSI.HasValue)
            {
                var rsiSignal = analysis.RSI.Value switch
                {
                    > 80 => "Extremely Overbought - Strong Lay Signal",
                    > 70 => "Overbought - Moderate Lay Signal", 
                    > 60 => "Bullish Momentum",
                    < 20 => "Extremely Oversold - Strong Back Signal",
                    < 30 => "Oversold - Moderate Back Signal",
                    < 40 => "Bearish Momentum",
                    _ => "Neutral"
                };
                Console.WriteLine($"RSI Analysis: {analysis.RSI.Value:F1} - {rsiSignal}");
            }

            // MACD divergence analysis
            if (analysis.CurrentMACD.HasValue && analysis.MACDSignal.HasValue && analysis.MACDHistogram.HasValue)
            {
                var macdTrend = analysis.MACDHistogram.Value > 0 ? "Bullish" : "Bearish";
                var momentum = Math.Abs(analysis.MACDHistogram.Value) > 0.01m ? "Strong" : "Weak";
                Console.WriteLine($"MACD Analysis: {macdTrend} {momentum} ({analysis.MACDHistogram.Value:F4})");
            }

            // Bollinger Squeeze detection
            if (analysis.BollingerUpper.HasValue && analysis.BollingerLower.HasValue && analysis.ATR.HasValue)
            {
                var bandWidth = analysis.BollingerUpper.Value - analysis.BollingerLower.Value;
                var squeeze = bandWidth < (analysis.ATR.Value * 2);
                Console.WriteLine($"Bollinger Squeeze: {(squeeze ? "Yes - Breakout Expected" : "No - Normal Volatility")}");
            }
        }
    }
}
