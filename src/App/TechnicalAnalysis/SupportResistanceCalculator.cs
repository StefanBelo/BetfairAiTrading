using System;
using System.Collections.Generic;
using System.Linq;

namespace BetfairAiTrading.TechnicalAnalysis
{
    public class PricePoint
    {
        public DateTime Time { get; set; }
        public decimal Price { get; set; }
        public decimal Volume { get; set; }
    }

    public class SupportResistanceLevel
    {
        public decimal Price { get; set; }
        public int TouchCount { get; set; }
        public decimal TotalVolume { get; set; }
        public DateTime FirstTouch { get; set; }
        public DateTime LastTouch { get; set; }
        public string Type { get; set; } // "Support" or "Resistance"
        public decimal Strength { get; set; } // 0-100 strength score
    }

    public class SupportResistanceCalculator
    {
        private readonly decimal _tolerancePercentage;
        private readonly int _minTouches;
        private readonly decimal _minVolumeThreshold;

        public SupportResistanceCalculator(decimal tolerancePercentage = 0.02m, int minTouches = 2, decimal minVolumeThreshold = 10m)
        {
            _tolerancePercentage = tolerancePercentage;
            _minTouches = minTouches;
            _minVolumeThreshold = minVolumeThreshold;
        }

        public List<SupportResistanceLevel> CalculateSupportResistance(List<PricePoint> priceData)
        {
            var levels = new List<SupportResistanceLevel>();
            var sortedData = priceData.OrderBy(x => x.Time).ToList();

            // Find local highs and lows
            var pivotPoints = FindPivotPoints(sortedData);

            // Group similar price levels
            var priceGroups = GroupSimilarPrices(pivotPoints);

            foreach (var group in priceGroups)
            {
                if (group.Count >= _minTouches)
                {
                    var level = new SupportResistanceLevel
                    {
                        Price = group.Average(x => x.Price),
                        TouchCount = group.Count,
                        TotalVolume = group.Sum(x => x.Volume),
                        FirstTouch = group.Min(x => x.Time),
                        LastTouch = group.Max(x => x.Time),
                        Type = DetermineType(group, sortedData),
                        Strength = CalculateStrength(group, sortedData)
                    };

                    if (level.TotalVolume >= _minVolumeThreshold)
                    {
                        levels.Add(level);
                    }
                }
            }

            return levels.OrderByDescending(x => x.Strength).ToList();
        }

        private List<PricePoint> FindPivotPoints(List<PricePoint> data, int window = 3)
        {
            var pivots = new List<PricePoint>();

            for (int i = window; i < data.Count - window; i++)
            {
                var current = data[i];
                var leftPrices = data.Skip(i - window).Take(window).Select(x => x.Price);
                var rightPrices = data.Skip(i + 1).Take(window).Select(x => x.Price);

                // Check for local high
                if (current.Price >= leftPrices.Max() && current.Price >= rightPrices.Max())
                {
                    pivots.Add(current);
                }
                // Check for local low
                else if (current.Price <= leftPrices.Min() && current.Price <= rightPrices.Min())
                {
                    pivots.Add(current);
                }
            }

            return pivots;
        }

        private List<List<PricePoint>> GroupSimilarPrices(List<PricePoint> pivotPoints)
        {
            var groups = new List<List<PricePoint>>();
            var used = new HashSet<PricePoint>();

            foreach (var pivot in pivotPoints)
            {
                if (used.Contains(pivot)) continue;

                var group = new List<PricePoint> { pivot };
                used.Add(pivot);

                var tolerance = pivot.Price * _tolerancePercentage;

                foreach (var other in pivotPoints)
                {
                    if (used.Contains(other)) continue;

                    if (Math.Abs(pivot.Price - other.Price) <= tolerance)
                    {
                        group.Add(other);
                        used.Add(other);
                    }
                }

                groups.Add(group);
            }

            return groups;
        }

        private string DetermineType(List<PricePoint> group, List<PricePoint> allData)
        {
            var avgPrice = group.Average(x => x.Price);
            var overallAvg = allData.Average(x => x.Price);

            // Simple heuristic: if level is below average, likely support; if above, likely resistance
            return avgPrice < overallAvg ? "Support" : "Resistance";
        }

        private decimal CalculateStrength(List<PricePoint> group, List<PricePoint> allData)
        {
            // Strength based on:
            // 1. Number of touches (40%)
            // 2. Total volume at level (30%)
            // 3. Time span (20%)
            // 4. Price significance (10%)

            var touchScore = Math.Min(group.Count * 20m, 40m); // Max 40 points for touches
            
            var volumeScore = Math.Min((group.Sum(x => x.Volume) / allData.Sum(x => x.Volume)) * 300m, 30m); // Max 30 points
            
            var timeSpan = (group.Max(x => x.Time) - group.Min(x => x.Time)).TotalHours;
            var timeScore = Math.Min(timeSpan * 2m, 20m); // Max 20 points
            
            var avgPrice = group.Average(x => x.Price);
            var priceRange = allData.Max(x => x.Price) - allData.Min(x => x.Price);
            var priceSignificance = (decimal)(avgPrice / (decimal)priceRange) * 10m; // Max 10 points

            return touchScore + volumeScore + timeScore + priceSignificance;
        }

        public List<SupportResistanceLevel> FindCurrentLevels(List<PricePoint> priceData, decimal currentPrice, decimal priceRange = 0.1m)
        {
            var allLevels = CalculateSupportResistance(priceData);
            
            return allLevels.Where(level => 
                Math.Abs(level.Price - currentPrice) <= (currentPrice * priceRange))
                .OrderBy(level => Math.Abs(level.Price - currentPrice))
                .ToList();
        }

        public SupportResistanceLevel FindNearestSupport(List<PricePoint> priceData, decimal currentPrice)
        {
            var levels = CalculateSupportResistance(priceData);
            return levels
                .Where(x => x.Type == "Support" && x.Price < currentPrice)
                .OrderByDescending(x => x.Price)
                .FirstOrDefault();
        }

        public SupportResistanceLevel FindNearestResistance(List<PricePoint> priceData, decimal currentPrice)
        {
            var levels = CalculateSupportResistance(priceData);
            return levels
                .Where(x => x.Type == "Resistance" && x.Price > currentPrice)
                .OrderBy(x => x.Price)
                .FirstOrDefault();
        }
    }

    // Extension methods for additional analysis
    public static class TechnicalAnalysisExtensions
    {
        public static decimal CalculateRSI(this List<PricePoint> data, int period = 14)
        {
            if (data.Count < period + 1) return 50m; // Default neutral RSI

            var gains = new List<decimal>();
            var losses = new List<decimal>();

            for (int i = 1; i < data.Count; i++)
            {
                var change = data[i].Price - data[i - 1].Price;
                gains.Add(change > 0 ? change : 0);
                losses.Add(change < 0 ? Math.Abs(change) : 0);
            }

            var avgGain = gains.TakeLast(period).Average();
            var avgLoss = losses.TakeLast(period).Average();

            if (avgLoss == 0) return 100m;

            var rs = avgGain / avgLoss;
            return 100m - (100m / (1m + rs));
        }

        public static List<decimal> CalculateMovingAverage(this List<PricePoint> data, int period)
        {
            var result = new List<decimal>();
            
            for (int i = period - 1; i < data.Count; i++)
            {
                var avg = data.Skip(i - period + 1).Take(period).Average(x => x.Price);
                result.Add(avg);
            }

            return result;
        }

        public static (decimal upper, decimal middle, decimal lower) CalculateBollingerBands(
            this List<PricePoint> data, int period = 20, decimal standardDeviations = 2m)
        {
            if (data.Count < period) return (0, 0, 0);

            var recentData = data.TakeLast(period).ToList();
            var sma = recentData.Average(x => x.Price);
            
            var variance = recentData.Sum(x => (decimal)Math.Pow((double)(x.Price - sma), 2)) / period;
            var stdDev = (decimal)Math.Sqrt((double)variance);

            return (
                upper: sma + (standardDeviations * stdDev),
                middle: sma,
                lower: sma - (standardDeviations * stdDev)
            );
        }
    }
}
