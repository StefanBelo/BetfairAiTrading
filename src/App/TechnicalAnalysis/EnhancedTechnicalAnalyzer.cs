using System;
using System.Collections.Generic;
using System.Linq;
using Skender.Stock.Indicators;

namespace BetfairAiTrading.TechnicalAnalysis
{
    public class EnhancedTechnicalAnalyzer
    {
        private readonly SupportResistanceCalculator _supportResistanceCalculator;

        public EnhancedTechnicalAnalyzer()
        {
            _supportResistanceCalculator = new SupportResistanceCalculator();
        }

        public class BetfairQuote : IQuote
        {
            public DateTime Date { get; set; }
            public decimal Open { get; set; }
            public decimal High { get; set; }
            public decimal Low { get; set; }
            public decimal Close { get; set; }
            public decimal Volume { get; set; }
        }

        public class ComprehensiveTechnicalAnalysis
        {
            // Custom Support/Resistance
            public List<SupportResistanceLevel> SupportResistanceLevels { get; set; } = new();
            public SupportResistanceLevel NearestSupport { get; set; }
            public SupportResistanceLevel NearestResistance { get; set; }

            // Trend Indicators
            public decimal? CurrentSMA20 { get; set; }
            public decimal? CurrentEMA20 { get; set; }
            public decimal? CurrentMACD { get; set; }
            public decimal? MACDSignal { get; set; }
            public decimal? MACDHistogram { get; set; }
            public decimal? ADX { get; set; }
            public string TrendDirection { get; set; }
            public decimal? SuperTrend { get; set; }

            // Momentum Indicators
            public decimal? RSI { get; set; }
            public decimal? StochasticK { get; set; }
            public decimal? StochasticD { get; set; }
            public decimal? WilliamsR { get; set; }
            public decimal? CCI { get; set; }
            public decimal? ROC { get; set; }

            // Volatility Indicators
            public decimal? BollingerUpper { get; set; }
            public decimal? BollingerMiddle { get; set; }
            public decimal? BollingerLower { get; set; }
            public decimal? ATR { get; set; }
            public decimal? KeltnerUpper { get; set; }
            public decimal? KeltnerLower { get; set; }

            // Volume Indicators
            public decimal? OBV { get; set; }
            public decimal? VWAP { get; set; }
            public decimal? AccumulationDistribution { get; set; }

            // Pivot Points
            public decimal? PivotPoint { get; set; }
            public decimal? Resistance1 { get; set; }
            public decimal? Resistance2 { get; set; }
            public decimal? Resistance3 { get; set; }
            public decimal? Support1 { get; set; }
            public decimal? Support2 { get; set; }
            public decimal? Support3 { get; set; }

            // Market Signals
            public List<string> TradingSignals { get; set; } = new();
            public string OverallSentiment { get; set; }
            public decimal ConfidenceScore { get; set; }
        }

        public ComprehensiveTechnicalAnalysis AnalyzeBetfairMarket(List<PricePoint> priceData)
        {
            var analysis = new ComprehensiveTechnicalAnalysis();
            
            if (!priceData.Any()) return analysis;

            // Convert to Skender format
            var quotes = ConvertToQuotes(priceData);
            var currentPrice = priceData.Last().Price;

            // Custom Support/Resistance Analysis
            analysis.SupportResistanceLevels = _supportResistanceCalculator.CalculateSupportResistance(priceData);
            analysis.NearestSupport = _supportResistanceCalculator.FindNearestSupport(priceData, currentPrice);
            analysis.NearestResistance = _supportResistanceCalculator.FindNearestResistance(priceData, currentPrice);

            // Trend Indicators
            CalculateTrendIndicators(quotes, analysis);

            // Momentum Indicators
            CalculateMomentumIndicators(quotes, analysis);

            // Volatility Indicators
            CalculateVolatilityIndicators(quotes, analysis);

            // Volume Indicators
            CalculateVolumeIndicators(quotes, analysis);

            // Pivot Points
            CalculatePivotPoints(quotes, analysis);

            // Generate Trading Signals
            GenerateComprehensiveSignals(analysis, currentPrice);

            return analysis;
        }

        private List<BetfairQuote> ConvertToQuotes(List<PricePoint> priceData)
        {
            return priceData.Select(p => new BetfairQuote
            {
                Date = p.Time,
                Open = p.Price,
                High = p.Price,
                Low = p.Price,
                Close = p.Price,
                Volume = p.Volume
            }).ToList();
        }

        private void CalculateTrendIndicators(List<BetfairQuote> quotes, ComprehensiveTechnicalAnalysis analysis)
        {
            try
            {
                // Simple Moving Average
                var sma20 = quotes.GetSma(20).LastOrDefault();
                analysis.CurrentSMA20 = sma20?.Sma;

                // Exponential Moving Average
                var ema20 = quotes.GetEma(20).LastOrDefault();
                analysis.CurrentEMA20 = ema20?.Ema;

                // MACD
                var macd = quotes.GetMacd(12, 26, 9).LastOrDefault();
                analysis.CurrentMACD = macd?.Macd;
                analysis.MACDSignal = macd?.Signal;
                analysis.MACDHistogram = macd?.Histogram;

                // ADX (Average Directional Index)
                var adx = quotes.GetAdx(14).LastOrDefault();
                analysis.ADX = adx?.Adx;

                // SuperTrend
                var superTrend = quotes.GetSuperTrend(10, 3).LastOrDefault();
                analysis.SuperTrend = superTrend?.SuperTrend;

                // Determine trend direction
                var currentPrice = quotes.Last().Close;
                if (analysis.CurrentSMA20.HasValue)
                {
                    analysis.TrendDirection = currentPrice > analysis.CurrentSMA20.Value ? "Bullish" : "Bearish";
                }
            }
            catch (Exception ex)
            {
                // Handle insufficient data gracefully
                Console.WriteLine($"Trend calculation error: {ex.Message}");
            }
        }

        private void CalculateMomentumIndicators(List<BetfairQuote> quotes, ComprehensiveTechnicalAnalysis analysis)
        {
            try
            {
                // RSI
                var rsi = quotes.GetRsi(14).LastOrDefault();
                analysis.RSI = rsi?.Rsi;

                // Stochastic
                var stoch = quotes.GetStoch(14, 3, 3).LastOrDefault();
                analysis.StochasticK = stoch?.K;
                analysis.StochasticD = stoch?.D;

                // Williams %R
                var williamsR = quotes.GetWilliamsR(14).LastOrDefault();
                analysis.WilliamsR = williamsR?.WilliamsR;

                // CCI (Commodity Channel Index)
                var cci = quotes.GetCci(20).LastOrDefault();
                analysis.CCI = cci?.Cci;

                // Rate of Change
                var roc = quotes.GetRoc(10).LastOrDefault();
                analysis.ROC = roc?.Roc;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Momentum calculation error: {ex.Message}");
            }
        }

        private void CalculateVolatilityIndicators(List<BetfairQuote> quotes, ComprehensiveTechnicalAnalysis analysis)
        {
            try
            {
                // Bollinger Bands
                var bollinger = quotes.GetBollingerBands(20, 2).LastOrDefault();
                analysis.BollingerUpper = bollinger?.UpperBand;
                analysis.BollingerMiddle = bollinger?.Sma;
                analysis.BollingerLower = bollinger?.LowerBand;

                // Average True Range
                var atr = quotes.GetAtr(14).LastOrDefault();
                analysis.ATR = atr?.Atr;

                // Keltner Channels
                var keltner = quotes.GetKeltner(20, 2, 10).LastOrDefault();
                analysis.KeltnerUpper = keltner?.UpperBand;
                analysis.KeltnerLower = keltner?.LowerBand;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Volatility calculation error: {ex.Message}");
            }
        }

        private void CalculateVolumeIndicators(List<BetfairQuote> quotes, ComprehensiveTechnicalAnalysis analysis)
        {
            try
            {
                // On-Balance Volume
                var obv = quotes.GetObv().LastOrDefault();
                analysis.OBV = obv?.Obv;

                // VWAP
                var vwap = quotes.GetVwap().LastOrDefault();
                analysis.VWAP = vwap?.Vwap;

                // Accumulation/Distribution Line
                var adl = quotes.GetAdl().LastOrDefault();
                analysis.AccumulationDistribution = adl?.Adl;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Volume calculation error: {ex.Message}");
            }
        }

        private void CalculatePivotPoints(List<BetfairQuote> quotes, ComprehensiveTechnicalAnalysis analysis)
        {
            try
            {
                // Standard Pivot Points
                var pivots = quotes.GetPivotPoints(PivotPointType.Standard).LastOrDefault();
                if (pivots != null)
                {
                    analysis.PivotPoint = pivots.PP;
                    analysis.Resistance1 = pivots.R1;
                    analysis.Resistance2 = pivots.R2;
                    analysis.Resistance3 = pivots.R3;
                    analysis.Support1 = pivots.S1;
                    analysis.Support2 = pivots.S2;
                    analysis.Support3 = pivots.S3;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Pivot calculation error: {ex.Message}");
            }
        }

        private void GenerateComprehensiveSignals(ComprehensiveTechnicalAnalysis analysis, decimal currentPrice)
        {
            var signals = new List<string>();
            var bullishSignals = 0;
            var bearishSignals = 0;

            // RSI Signals
            if (analysis.RSI.HasValue)
            {
                if (analysis.RSI.Value > 70)
                {
                    signals.Add("RSI OVERBOUGHT (>70): Strong sell/lay signal");
                    bearishSignals++;
                }
                else if (analysis.RSI.Value < 30)
                {
                    signals.Add("RSI OVERSOLD (<30): Strong buy/back signal");
                    bullishSignals++;
                }
                else if (analysis.RSI.Value > 60)
                {
                    signals.Add("RSI BULLISH MOMENTUM (>60): Moderate buy signal");
                    bullishSignals++;
                }
                else if (analysis.RSI.Value < 40)
                {
                    signals.Add("RSI BEARISH MOMENTUM (<40): Moderate sell signal");
                    bearishSignals++;
                }
            }

            // MACD Signals
            if (analysis.CurrentMACD.HasValue && analysis.MACDSignal.HasValue)
            {
                if (analysis.CurrentMACD.Value > analysis.MACDSignal.Value)
                {
                    signals.Add("MACD BULLISH CROSSOVER: Buy/back signal");
                    bullishSignals++;
                }
                else
                {
                    signals.Add("MACD BEARISH CROSSOVER: Sell/lay signal");
                    bearishSignals++;
                }
            }

            // Bollinger Bands Signals
            if (analysis.BollingerUpper.HasValue && analysis.BollingerLower.HasValue)
            {
                if (currentPrice > analysis.BollingerUpper.Value)
                {
                    signals.Add("PRICE ABOVE UPPER BOLLINGER: Overbought - lay signal");
                    bearishSignals++;
                }
                else if (currentPrice < analysis.BollingerLower.Value)
                {
                    signals.Add("PRICE BELOW LOWER BOLLINGER: Oversold - back signal");
                    bullishSignals++;
                }
            }

            // Support/Resistance Signals
            if (analysis.NearestSupport != null)
            {
                var supportDistance = Math.Abs(currentPrice - analysis.NearestSupport.Price) / currentPrice;
                if (supportDistance < 0.01m) // Within 1%
                {
                    signals.Add($"NEAR STRONG SUPPORT at {analysis.NearestSupport.Price:F2}: Back signal");
                    bullishSignals++;
                }
            }

            if (analysis.NearestResistance != null)
            {
                var resistanceDistance = Math.Abs(analysis.NearestResistance.Price - currentPrice) / currentPrice;
                if (resistanceDistance < 0.01m) // Within 1%
                {
                    signals.Add($"NEAR STRONG RESISTANCE at {analysis.NearestResistance.Price:F2}: Lay signal");
                    bearishSignals++;
                }
            }

            // ADX Trend Strength
            if (analysis.ADX.HasValue)
            {
                if (analysis.ADX.Value > 25)
                {
                    signals.Add($"STRONG TREND (ADX: {analysis.ADX.Value:F1}): Follow trend direction");
                }
                else if (analysis.ADX.Value < 20)
                {
                    signals.Add($"WEAK TREND (ADX: {analysis.ADX.Value:F1}): Range-bound market");
                }
            }

            // Stochastic Signals
            if (analysis.StochasticK.HasValue && analysis.StochasticD.HasValue)
            {
                if (analysis.StochasticK.Value > 80)
                {
                    signals.Add("STOCHASTIC OVERBOUGHT: Lay signal");
                    bearishSignals++;
                }
                else if (analysis.StochasticK.Value < 20)
                {
                    signals.Add("STOCHASTIC OVERSOLD: Back signal");
                    bullishSignals++;
                }
            }

            // Overall Sentiment
            if (bullishSignals > bearishSignals)
            {
                analysis.OverallSentiment = "BULLISH";
                analysis.ConfidenceScore = (decimal)bullishSignals / (bullishSignals + bearishSignals) * 100;
            }
            else if (bearishSignals > bullishSignals)
            {
                analysis.OverallSentiment = "BEARISH";
                analysis.ConfidenceScore = (decimal)bearishSignals / (bullishSignals + bearishSignals) * 100;
            }
            else
            {
                analysis.OverallSentiment = "NEUTRAL";
                analysis.ConfidenceScore = 50m;
            }

            analysis.TradingSignals = signals;
        }

        public void PrintAnalysisReport(ComprehensiveTechnicalAnalysis analysis, decimal currentPrice)
        {
            Console.WriteLine("=== COMPREHENSIVE TECHNICAL ANALYSIS ===");
            Console.WriteLine($"Current Price: {currentPrice:F2}");
            Console.WriteLine($"Overall Sentiment: {analysis.OverallSentiment} (Confidence: {analysis.ConfidenceScore:F1}%)");
            Console.WriteLine();

            Console.WriteLine("=== TREND INDICATORS ===");
            Console.WriteLine($"SMA(20): {analysis.CurrentSMA20:F2}");
            Console.WriteLine($"EMA(20): {analysis.CurrentEMA20:F2}");
            Console.WriteLine($"MACD: {analysis.CurrentMACD:F4} | Signal: {analysis.MACDSignal:F4}");
            Console.WriteLine($"ADX: {analysis.ADX:F1} | Trend: {analysis.TrendDirection}");
            Console.WriteLine();

            Console.WriteLine("=== MOMENTUM INDICATORS ===");
            Console.WriteLine($"RSI(14): {analysis.RSI:F1}");
            Console.WriteLine($"Stochastic K: {analysis.StochasticK:F1} | D: {analysis.StochasticD:F1}");
            Console.WriteLine($"Williams %R: {analysis.WilliamsR:F1}");
            Console.WriteLine($"CCI: {analysis.CCI:F1}");
            Console.WriteLine();

            Console.WriteLine("=== VOLATILITY INDICATORS ===");
            Console.WriteLine($"Bollinger Bands: {analysis.BollingerLower:F2} | {analysis.BollingerMiddle:F2} | {analysis.BollingerUpper:F2}");
            Console.WriteLine($"ATR: {analysis.ATR:F4}");
            Console.WriteLine();

            Console.WriteLine("=== SUPPORT/RESISTANCE ===");
            if (analysis.NearestSupport != null)
                Console.WriteLine($"Nearest Support: {analysis.NearestSupport.Price:F2} (Strength: {analysis.NearestSupport.Strength:F1})");
            if (analysis.NearestResistance != null)
                Console.WriteLine($"Nearest Resistance: {analysis.NearestResistance.Price:F2} (Strength: {analysis.NearestResistance.Strength:F1})");
            Console.WriteLine();

            Console.WriteLine("=== PIVOT POINTS ===");
            Console.WriteLine($"R3: {analysis.Resistance3:F2} | R2: {analysis.Resistance2:F2} | R1: {analysis.Resistance1:F2}");
            Console.WriteLine($"PP: {analysis.PivotPoint:F2}");
            Console.WriteLine($"S1: {analysis.Support1:F2} | S2: {analysis.Support2:F2} | S3: {analysis.Support3:F2}");
            Console.WriteLine();

            Console.WriteLine("=== TRADING SIGNALS ===");
            foreach (var signal in analysis.TradingSignals)
            {
                Console.WriteLine($"• {signal}");
            }
        }
    }
}
