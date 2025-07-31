using Skender.Stock.Indicators;
using BetfairMarketAnalyzer.Models;

namespace BetfairMarketAnalyzer.Services;

public class TechnicalAnalysisService
{
    public AnalysisResult AnalyzeSelection(MarketSelectionData marketData)
    {
        if (marketData.Selections.Count == 0)
            throw new ArgumentException("No selection data provided");

        var selection = marketData.Selections[0];
        var priceData = selection.TradedPricesAndVolume.OrderBy(x => x.Time).ToList();

        if (priceData.Count == 0)
            throw new ArgumentException("No price data available");

        // Convert to OHLCV format for technical analysis
        var quotes = ConvertToQuotes(priceData);
        
        // Calculate technical indicators
        var indicators = CalculateTechnicalIndicators(quotes);
        
        // Analyze support/resistance levels
        var supportResistance = AnalyzeSupportResistance(priceData);
        
        // Analyze volume patterns
        var volumeAnalysis = AnalyzeVolume(priceData);
        
        // Detect market flow
        var marketFlow = AnalyzeMarketFlow(priceData);
        
        // Generate trading recommendations
        var recommendations = GenerateTradingRecommendations(priceData, indicators, supportResistance, marketFlow);

        var currentPrice = priceData.Last().Price;
        var openingPrice = priceData.First().Price;
        var oddsMovement = currentPrice - openingPrice;
        var oddsMovementPercentage = (oddsMovement / openingPrice) * 100;

        return new AnalysisResult
        {
            SelectionName = selection.Selection.Name,
            CurrentOdds = currentPrice,
            OpeningOdds = openingPrice,
            ImpliedProbability = Math.Round(1 / currentPrice * 100, 2),
            OddsMovement = Math.Round(oddsMovement, 2),
            OddsMovementPercentage = Math.Round(oddsMovementPercentage, 2),
            TrendDirection = DetermineTrendDirection(quotes),
            TotalVolume = priceData.Sum(x => x.Volume),
            SupportLevels = supportResistance.SupportLevels,
            ResistanceLevels = supportResistance.ResistanceLevels,
            VolumeSpikes = volumeAnalysis,
            Indicators = indicators,
            TradingRecommendations = recommendations,
            MarketFlow = marketFlow,
            AnalysisTime = DateTime.Now,
            TimeToRaceStart = marketData.Market.StartTime - DateTime.Now
        };
    }

    private List<BetfairQuote> ConvertToQuotes(List<TradedPriceVolume> priceData)
    {
        var quotes = new List<BetfairQuote>();
        
        // Group by time periods (e.g., 1-minute intervals) to create OHLCV data
        var groupedData = priceData
            .GroupBy(x => new DateTime(x.Time.Year, x.Time.Month, x.Time.Day, x.Time.Hour, x.Time.Minute, 0))
            .OrderBy(g => g.Key)
            .ToList();

        foreach (var group in groupedData)
        {
            var trades = group.OrderBy(x => x.Time).ToList();
            if (trades.Count == 0) continue;

            quotes.Add(new BetfairQuote
            {
                Date = group.Key,
                Open = trades.First().Price,
                High = trades.Max(x => x.Price),
                Low = trades.Min(x => x.Price),
                Close = trades.Last().Price,
                Volume = trades.Sum(x => x.Volume)
            });
        }

        return quotes;
    }

    private TechnicalIndicators CalculateTechnicalIndicators(IEnumerable<BetfairQuote> quotes)
    {
        var quotesList = quotes.ToList();
        if (quotesList.Count < 14) return new TechnicalIndicators();

        try
        {
            var rsi = quotesList.GetRsi(14).LastOrDefault();
            var sma5 = quotesList.GetSma(5).LastOrDefault();
            var sma10 = quotesList.GetSma(10).LastOrDefault();
            var ema5 = quotesList.GetEma(5).LastOrDefault();
            var ema10 = quotesList.GetEma(10).LastOrDefault();
            var macd = quotesList.GetMacd().LastOrDefault();
            var bollinger = quotesList.GetBollingerBands().LastOrDefault();
            
            var volumeAvg = quotesList.TakeLast(10).Average(q => q.Volume);

            return new TechnicalIndicators
            {
                RSI = (decimal?)rsi?.Rsi,
                SMA5 = (decimal?)sma5?.Sma,
                SMA10 = (decimal?)sma10?.Sma,
                EMA5 = (decimal?)ema5?.Ema,
                EMA10 = (decimal?)ema10?.Ema,
                MACD = (decimal?)macd?.Macd,
                MACDSignal = (decimal?)macd?.Signal,
                BollingerUpper = (decimal?)bollinger?.UpperBand,
                BollingerLower = (decimal?)bollinger?.LowerBand,
                BollingerMiddle = (decimal?)bollinger?.Sma,
                MomentumDirection = DetermineMomentum(quotesList),
                VolumeMovingAverage = (decimal)volumeAvg
            };
        }
        catch
        {
            return new TechnicalIndicators();
        }
    }

    private string DetermineTrendDirection(List<BetfairQuote> quotes)
    {
        if (quotes.Count < 2) return "Insufficient Data";

        var recentQuotes = quotes.TakeLast(10).ToList();
        var priceChange = recentQuotes.Last().Close - recentQuotes.First().Close;

        if (priceChange < -0.05m) return "Shortening (Backing Pressure)";
        if (priceChange > 0.05m) return "Lengthening (Laying Pressure)";
        return "Sideways";
    }

    private string DetermineMomentum(List<BetfairQuote> quotes)
    {
        if (quotes.Count < 5) return "Neutral";

        var recent = quotes.TakeLast(5).ToList();
        var momentum = recent.Last().Close - recent.First().Close;

        if (momentum < -0.02m) return "Strong Shortening";
        if (momentum > 0.02m) return "Strong Lengthening";
        return "Neutral";
    }

    private (List<SupportResistanceLevel> SupportLevels, List<SupportResistanceLevel> ResistanceLevels) 
        AnalyzeSupportResistance(List<TradedPriceVolume> priceData)
    {
        var supportLevels = new List<SupportResistanceLevel>();
        var resistanceLevels = new List<SupportResistanceLevel>();

        // Group prices by levels (rounded to 2 decimal places)
        var priceGroups = priceData
            .GroupBy(x => Math.Round(x.Price, 2))
            .Where(g => g.Count() >= 3) // At least 3 touches
            .OrderByDescending(g => g.Sum(x => x.Volume))
            .Take(10)
            .ToList();

        var currentPrice = priceData.Last().Price;

        foreach (var group in priceGroups)
        {
            var level = new SupportResistanceLevel
            {
                Level = group.Key,
                TouchCount = group.Count(),
                TotalVolume = group.Sum(x => x.Volume),
                LastTouch = group.Max(x => x.Time),
                Strength = group.Sum(x => x.Volume) > 100 ? "Strong" : "Moderate"
            };

            if (group.Key < currentPrice)
                supportLevels.Add(level);
            else
                resistanceLevels.Add(level);
        }

        return (supportLevels.OrderByDescending(x => x.Level).Take(5).ToList(),
                resistanceLevels.OrderBy(x => x.Level).Take(5).ToList());
    }

    private List<VolumeAnalysis> AnalyzeVolume(List<TradedPriceVolume> priceData)
    {
        var volumeAnalysis = new List<VolumeAnalysis>();
        var avgVolume = priceData.Average(x => x.Volume);
        var volumeThreshold = avgVolume * 2; // Spike threshold

        for (int i = 1; i < priceData.Count; i++)
        {
            var current = priceData[i];
            var previous = priceData[i - 1];

            if (current.Volume > volumeThreshold)
            {
                var direction = current.Price < previous.Price ? "Backing" : "Laying";
                var type = current.Volume > avgVolume * 5 ? "SteamMove" : "Spike";

                volumeAnalysis.Add(new VolumeAnalysis
                {
                    Time = current.Time,
                    Price = current.Price,
                    Volume = current.Volume,
                    Type = type,
                    Direction = direction
                });
            }
        }

        return volumeAnalysis.OrderByDescending(x => x.Volume).Take(10).ToList();
    }

    private MarketFlow AnalyzeMarketFlow(List<TradedPriceVolume> priceData)
    {
        var recentData = priceData.TakeLast(50).ToList();
        if (recentData.Count < 2) return new MarketFlow();

        var backingVolume = 0m;
        var layingVolume = 0m;

        for (int i = 1; i < recentData.Count; i++)
        {
            var current = recentData[i];
            var previous = recentData[i - 1];

            if (current.Price < previous.Price) // Odds shortening = backing
                backingVolume += current.Volume;
            else if (current.Price > previous.Price) // Odds lengthening = laying
                layingVolume += current.Volume;
        }

        var totalVolume = backingVolume + layingVolume;
        var backingPressure = totalVolume > 0 ? (backingVolume / totalVolume) * 100 : 50;
        var layingPressure = 100 - backingPressure;

        var steamMove = recentData.Any(x => x.Volume > recentData.Average(y => y.Volume) * 5);
        var lastSteamMove = steamMove ? recentData.Where(x => x.Volume > recentData.Average(y => y.Volume) * 5)
            .OrderByDescending(x => x.Time).FirstOrDefault()?.Time : null;

        string dominantDirection;
        if (backingPressure > 60) dominantDirection = "Backing";
        else if (layingPressure > 60) dominantDirection = "Laying";
        else dominantDirection = "Neutral";

        return new MarketFlow
        {
            DominantDirection = dominantDirection,
            BackingPressure = Math.Round(backingPressure, 1),
            LayingPressure = Math.Round(layingPressure, 1),
            RecentVolumeRatio = Math.Round(backingVolume / Math.Max(layingVolume, 1), 2),
            SteamMoveDetected = steamMove,
            LastSteamMove = lastSteamMove,
            MarketSentiment = backingPressure > 70 ? "Strong Backing" : 
                           layingPressure > 70 ? "Strong Laying" : "Balanced"
        };
    }

    private List<TradingRecommendation> GenerateTradingRecommendations(
        List<TradedPriceVolume> priceData, 
        TechnicalIndicators indicators,
        (List<SupportResistanceLevel> SupportLevels, List<SupportResistanceLevel> ResistanceLevels) levels,
        MarketFlow marketFlow)
    {
        var recommendations = new List<TradingRecommendation>();
        var currentPrice = priceData.Last().Price;
        var recentTrend = priceData.TakeLast(10).ToList();
        var priceDirection = recentTrend.Last().Price - recentTrend.First().Price;

        // Back-to-Lay Strategy (Primary)
        if (marketFlow.DominantDirection == "Backing" && priceDirection < 0)
        {
            var nearestSupport = levels.SupportLevels.FirstOrDefault();
            var targetPrice = nearestSupport?.Level ?? currentPrice * 0.95m;

            recommendations.Add(new TradingRecommendation
            {
                Strategy = "Back-to-Lay Trading",
                Action = "Back-to-Lay",
                EntryPrice = currentPrice,
                TargetPrice = targetPrice,
                StopLoss = currentPrice * 1.05m,
                ExpectedProfit = CalculateBackToLayProfit(currentPrice, targetPrice),
                RiskReward = 2.5m,
                TimeFrame = "5-15 minutes",
                RiskLevel = "Moderate",
                ConfidenceLevel = 8,
                Logic = "Strong backing pressure with volume confirmation supporting further shortening",
                PositionSizing = 5m
            });
        }

        // Pure Backing Strategy
        if (marketFlow.BackingPressure > 65 && indicators.RSI < 70)
        {
            recommendations.Add(new TradingRecommendation
            {
                Strategy = "Pure Backing Position",
                Action = "Back",
                EntryPrice = currentPrice,
                TargetPrice = 0, // Hold to win
                StopLoss = currentPrice * 1.15m,
                ExpectedProfit = (1 / currentPrice - 1) * 100, // Win percentage
                RiskReward = currentPrice,
                TimeFrame = "Hold to race",
                RiskLevel = "Aggressive",
                ConfidenceLevel = 7,
                Logic = "Heavy backing pressure indicates market confidence",
                PositionSizing = 2m
            });
        }

        // Lay-to-Back Strategy (Counter-trend)
        if (marketFlow.DominantDirection == "Laying" && priceDirection > 0)
        {
            var nearestResistance = levels.ResistanceLevels.FirstOrDefault();
            var targetPrice = nearestResistance?.Level ?? currentPrice * 1.05m;

            recommendations.Add(new TradingRecommendation
            {
                Strategy = "Lay-to-Back Trading",
                Action = "Lay-to-Back",
                EntryPrice = currentPrice,
                TargetPrice = targetPrice,
                StopLoss = currentPrice * 0.95m,
                ExpectedProfit = CalculateLayToBackProfit(currentPrice, targetPrice),
                RiskReward = 1.8m,
                TimeFrame = "10-20 minutes",
                RiskLevel = "Conservative",
                ConfidenceLevel = 6,
                Logic = "Laying pressure may push odds higher providing lay-to-back opportunity",
                PositionSizing = 3m
            });
        }

        // Scalping Strategy
        if (recentTrend.Count > 5 && indicators.VolumeMovingAverage > 10)
        {
            recommendations.Add(new TradingRecommendation
            {
                Strategy = "Scalping",
                Action = "Quick Trade",
                EntryPrice = currentPrice,
                TargetPrice = currentPrice + (priceDirection > 0 ? 0.02m : -0.02m),
                StopLoss = currentPrice + (priceDirection > 0 ? -0.03m : 0.03m),
                ExpectedProfit = 2m, // 2 ticks
                RiskReward = 0.67m,
                TimeFrame = "1-3 minutes",
                RiskLevel = "Moderate",
                ConfidenceLevel = 5,
                Logic = "High volume and volatility suitable for quick scalping",
                PositionSizing = 8m
            });
        }

        return recommendations.OrderByDescending(x => x.ConfidenceLevel).ToList();
    }

    private decimal CalculateBackToLayProfit(decimal backPrice, decimal layPrice)
    {
        if (layPrice >= backPrice) return 0;
        return Math.Round(((backPrice - layPrice) / backPrice) * 100, 2);
    }

    private decimal CalculateLayToBackProfit(decimal layPrice, decimal backPrice)
    {
        if (backPrice <= layPrice) return 0;
        return Math.Round(((backPrice - layPrice) / layPrice) * 100, 2);
    }
}
