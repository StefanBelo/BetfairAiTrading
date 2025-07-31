using System;
using System.Collections.Generic;
using System.Linq;
using BetfairAiTrading.TechnicalAnalysis;

namespace BetfairAiTrading.Examples
{
    public class BetfairTechnicalAnalysisExample
    {
        public class BetfairPriceData
        {
            public DateTime Time { get; set; }
            public decimal Price { get; set; }
            public decimal Volume { get; set; }
        }

        public void AnalyzeBetfairMarket(List<BetfairPriceData> betfairData)
        {
            // Convert Betfair data to our technical analysis format
            var pricePoints = betfairData.Select(x => new PricePoint
            {
                Time = x.Time,
                Price = x.Price,
                Volume = x.Volume
            }).ToList();

            // Initialize the calculator
            var calculator = new SupportResistanceCalculator(
                tolerancePercentage: 0.02m, // 2% tolerance for grouping similar prices
                minTouches: 2,              // Minimum 2 touches to be considered a level
                minVolumeThreshold: 10m     // Minimum volume threshold
            );

            // Calculate all support and resistance levels
            var levels = calculator.CalculateSupportResistance(pricePoints);

            Console.WriteLine("=== SUPPORT AND RESISTANCE ANALYSIS ===");
            foreach (var level in levels.Take(5)) // Top 5 strongest levels
            {
                Console.WriteLine($"{level.Type}: {level.Price:F2} " +
                                $"(Strength: {level.Strength:F1}, Touches: {level.TouchCount}, " +
                                $"Volume: {level.TotalVolume:F1})");
            }

            // Find current market context
            var currentPrice = pricePoints.Last().Price;
            var nearestSupport = calculator.FindNearestSupport(pricePoints, currentPrice);
            var nearestResistance = calculator.FindNearestResistance(pricePoints, currentPrice);

            Console.WriteLine($"\n=== CURRENT MARKET CONTEXT ===");
            Console.WriteLine($"Current Price: {currentPrice:F2}");
            
            if (nearestSupport != null)
            {
                Console.WriteLine($"Nearest Support: {nearestSupport.Price:F2} " +
                                $"(Distance: {((currentPrice - nearestSupport.Price) / currentPrice * 100):F1}%)");
            }

            if (nearestResistance != null)
            {
                Console.WriteLine($"Nearest Resistance: {nearestResistance.Price:F2} " +
                                $"(Distance: {((nearestResistance.Price - currentPrice) / currentPrice * 100):F1}%)");
            }

            // Calculate additional technical indicators
            var rsi = pricePoints.CalculateRSI(14);
            var bollingerBands = pricePoints.CalculateBollingerBands(20, 2m);
            var sma20 = pricePoints.CalculateMovingAverage(20);

            Console.WriteLine($"\n=== TECHNICAL INDICATORS ===");
            Console.WriteLine($"RSI (14): {rsi:F1}");
            Console.WriteLine($"Bollinger Bands: Upper={bollingerBands.upper:F2}, " +
                            $"Middle={bollingerBands.middle:F2}, Lower={bollingerBands.lower:F2}");
            
            if (sma20.Any())
            {
                Console.WriteLine($"SMA (20): {sma20.Last():F2}");
                var trendDirection = currentPrice > sma20.Last() ? "Bullish" : "Bearish";
                Console.WriteLine($"Trend vs SMA: {trendDirection}");
            }

            // Generate trading signals
            GenerateTradingSignals(pricePoints, nearestSupport, nearestResistance, currentPrice, rsi, bollingerBands);
        }

        private void GenerateTradingSignals(
            List<PricePoint> pricePoints, 
            SupportResistanceLevel nearestSupport, 
            SupportResistanceLevel nearestResistance,
            decimal currentPrice,
            decimal rsi,
            (decimal upper, decimal middle, decimal lower) bollingerBands)
        {
            Console.WriteLine($"\n=== TRADING SIGNALS ===");

            var signals = new List<string>();

            // Support/Resistance signals
            if (nearestSupport != null)
            {
                var supportDistance = (currentPrice - nearestSupport.Price) / currentPrice;
                if (supportDistance < 0.01m) // Within 1% of support
                {
                    signals.Add($"NEAR SUPPORT: Price near strong support at {nearestSupport.Price:F2} - Consider backing");
                }
            }

            if (nearestResistance != null)
            {
                var resistanceDistance = (nearestResistance.Price - currentPrice) / currentPrice;
                if (resistanceDistance < 0.01m) // Within 1% of resistance
                {
                    signals.Add($"NEAR RESISTANCE: Price near resistance at {nearestResistance.Price:F2} - Consider laying");
                }
            }

            // RSI signals
            if (rsi > 70)
            {
                signals.Add("RSI OVERBOUGHT: Consider laying opportunities");
            }
            else if (rsi < 30)
            {
                signals.Add("RSI OVERSOLD: Consider backing opportunities");
            }

            // Bollinger Band signals
            if (currentPrice > bollingerBands.upper)
            {
                signals.Add("ABOVE UPPER BOLLINGER: Potentially overbought - lay signal");
            }
            else if (currentPrice < bollingerBands.lower)
            {
                signals.Add("BELOW LOWER BOLLINGER: Potentially oversold - back signal");
            }

            // Trend signals
            var recentTrend = CalculateRecentTrend(pricePoints.TakeLast(10).ToList());
            if (recentTrend > 0.02m)
            {
                signals.Add("STRONG UPTREND: Consider lay-to-back strategy");
            }
            else if (recentTrend < -0.02m)
            {
                signals.Add("STRONG DOWNTREND: Consider back-to-lay strategy");
            }

            if (signals.Any())
            {
                foreach (var signal in signals)
                {
                    Console.WriteLine($"• {signal}");
                }
            }
            else
            {
                Console.WriteLine("• No clear signals - market in consolidation");
            }
        }

        private decimal CalculateRecentTrend(List<PricePoint> recentData)
        {
            if (recentData.Count < 2) return 0;

            var firstPrice = recentData.First().Price;
            var lastPrice = recentData.Last().Price;

            return (lastPrice - firstPrice) / firstPrice;
        }

        // Example of how to use with Saliko data from the analysis
        public void ExampleWithSalikoData()
        {
            // Sample data based on the Saliko analysis
            var salikoData = new List<BetfairPriceData>
            {
                new() { Time = DateTime.Parse("2025-07-31T08:42:42+02:00"), Price = 2.84m, Volume = 9.09m },
                new() { Time = DateTime.Parse("2025-07-31T09:43:28+02:00"), Price = 2.84m, Volume = 33m },
                new() { Time = DateTime.Parse("2025-07-31T10:16:37+02:00"), Price = 2.72m, Volume = 2.92m },
                new() { Time = DateTime.Parse("2025-07-31T13:16:14+02:00"), Price = 3.2m, Volume = 18.14m },
                new() { Time = DateTime.Parse("2025-07-31T16:24:05+02:00"), Price = 3.5m, Volume = 9.47m },
                new() { Time = DateTime.Parse("2025-07-31T18:10:07+02:00"), Price = 3.75m, Volume = 32.76m }
            };

            Console.WriteLine("SALIKO TECHNICAL ANALYSIS EXAMPLE");
            Console.WriteLine("=================================");
            
            AnalyzeBetfairMarket(salikoData);
        }
    }
}
