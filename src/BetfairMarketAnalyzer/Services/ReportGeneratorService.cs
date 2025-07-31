using System.Text;
using BetfairMarketAnalyzer.Models;

namespace BetfairMarketAnalyzer.Services;

public class ReportGeneratorService
{
    public string GenerateComprehensiveReport(AnalysisResult analysis, MarketData marketData)
    {
        var report = new StringBuilder();

        // Header
        GenerateHeader(report, analysis, marketData);
        
        // Market Information
        GenerateMarketInformation(report, marketData, analysis);
        
        // Favorite Selection Analysis
        GenerateFavoriteAnalysis(report, analysis);
        
        // Technical Analysis Section
        GenerateTechnicalAnalysis(report, analysis);
        
        // Volume and Flow Analysis
        GenerateVolumeAnalysis(report, analysis);
        
        // Support/Resistance Analysis
        GenerateSupportResistanceAnalysis(report, analysis);
        
        // Trading Recommendations
        GenerateTradingRecommendations(report, analysis);
        
        // Risk Assessment
        GenerateRiskAssessment(report, analysis);
        
        // Summary and Key Findings
        GenerateSummary(report, analysis);

        return report.ToString();
    }

    private void GenerateHeader(StringBuilder report, AnalysisResult analysis, MarketData marketData)
    {
        report.AppendLine("# Betfair Market Analysis Report");
        report.AppendLine("## Generated using Skender.Stock.Indicators Technical Analysis");
        report.AppendLine();
        report.AppendLine($"**Analysis Time**: {analysis.AnalysisTime:yyyy-MM-dd HH:mm:ss}");
        report.AppendLine($"**Time to Race Start**: {analysis.TimeToRaceStart}");
        report.AppendLine();
    }

    private void GenerateMarketInformation(StringBuilder report, MarketData marketData, AnalysisResult analysis)
    {
        report.AppendLine("## Market Information");
        report.AppendLine("```");
        report.AppendLine($"Market: {marketData.MarketName}");
        report.AppendLine($"Event: {marketData.EventName}");
        report.AppendLine($"Market ID: {marketData.MarketId}");
        report.AppendLine($"Status: {marketData.Status}");
        report.AppendLine($"Event Type: {marketData.EventType}");
        report.AppendLine($"Start Time: {marketData.StartTime:yyyy-MM-dd HH:mm:ss}");
        report.AppendLine($"Analysis Time: {analysis.AnalysisTime:yyyy-MM-dd HH:mm:ss}");
        report.AppendLine("```");
        report.AppendLine();
    }

    private void GenerateFavoriteAnalysis(StringBuilder report, AnalysisResult analysis)
    {
        report.AppendLine("## Favorite Selection Analysis Report");
        report.AppendLine();
        
        report.AppendLine("### Selection Overview");
        report.AppendLine($"- **Selection Name**: {analysis.SelectionName}");
        report.AppendLine($"- **Current Odds**: {analysis.CurrentOdds:F2}");
        report.AppendLine($"- **Implied Probability**: {analysis.ImpliedProbability:F1}% (P = 1/{analysis.CurrentOdds:F2})");
        report.AppendLine($"- **Opening Odds**: {analysis.OpeningOdds:F2}");
        report.AppendLine($"- **Odds Movement**: {analysis.OddsMovement:+0.00;-0.00} points ({analysis.OddsMovementPercentage:+0.0;-0.0}% change)");
        report.AppendLine($"- **Position in Market**: Market favorite with lowest odds");
        report.AppendLine();

        var movementDescription = analysis.OddsMovement < 0 ? "significant shortening (backing pressure)" : 
                                 analysis.OddsMovement > 0 ? "lengthening (laying pressure)" : "stable";
        report.AppendLine($"- **Movement Analysis**: {movementDescription}");
        report.AppendLine();
    }

    private void GenerateTechnicalAnalysis(StringBuilder report, AnalysisResult analysis)
    {
        report.AppendLine("### Technical Analysis (Skender.Stock.Indicators)");
        report.AppendLine();
        
        report.AppendLine("#### Technical Indicators");
        report.AppendLine($"- **RSI (14-period)**: {analysis.Indicators.RSI?.ToString("F1") ?? "N/A"}");
        report.AppendLine($"- **SMA (5-period)**: {analysis.Indicators.SMA5?.ToString("F2") ?? "N/A"}");
        report.AppendLine($"- **SMA (10-period)**: {analysis.Indicators.SMA10?.ToString("F2") ?? "N/A"}");
        report.AppendLine($"- **EMA (5-period)**: {analysis.Indicators.EMA5?.ToString("F2") ?? "N/A"}");
        report.AppendLine($"- **EMA (10-period)**: {analysis.Indicators.EMA10?.ToString("F2") ?? "N/A"}");
        
        if (analysis.Indicators.MACD.HasValue)
        {
            report.AppendLine($"- **MACD**: {analysis.Indicators.MACD:F3}");
            report.AppendLine($"- **MACD Signal**: {analysis.Indicators.MACDSignal?.ToString("F3") ?? "N/A"}");
        }
        
        if (analysis.Indicators.BollingerUpper.HasValue)
        {
            report.AppendLine($"- **Bollinger Upper**: {analysis.Indicators.BollingerUpper:F2}");
            report.AppendLine($"- **Bollinger Middle**: {analysis.Indicators.BollingerMiddle?.ToString("F2") ?? "N/A"}");
            report.AppendLine($"- **Bollinger Lower**: {analysis.Indicators.BollingerLower?.ToString("F2") ?? "N/A"}");
        }
        
        report.AppendLine($"- **Momentum Direction**: {analysis.Indicators.MomentumDirection}");
        report.AppendLine($"- **Volume Moving Average**: {analysis.Indicators.VolumeMovingAverage:F1}");
        report.AppendLine();

        report.AppendLine("#### Trend Analysis");
        report.AppendLine($"- **Current Trend**: {analysis.TrendDirection}");
        report.AppendLine($"- **Total Trading Volume**: {analysis.TotalVolume:F1} units");
        
        // RSI interpretation
        if (analysis.Indicators.RSI.HasValue)
        {
            var rsiInterpretation = analysis.Indicators.RSI < 30 ? "Oversold (potential bounce)" :
                                   analysis.Indicators.RSI > 70 ? "Overbought (potential reversal)" :
                                   "Neutral";
            report.AppendLine($"- **RSI Interpretation**: {rsiInterpretation}");
        }
        
        report.AppendLine();
    }

    private void GenerateVolumeAnalysis(StringBuilder report, AnalysisResult analysis)
    {
        report.AppendLine("### Volume and Market Flow Analysis");
        report.AppendLine();
        
        report.AppendLine("#### Market Flow Indicators");
        report.AppendLine($"- **Dominant Direction**: {analysis.MarketFlow.DominantDirection}");
        report.AppendLine($"- **Backing Pressure**: {analysis.MarketFlow.BackingPressure:F1}%");
        report.AppendLine($"- **Laying Pressure**: {analysis.MarketFlow.LayingPressure:F1}%");
        report.AppendLine($"- **Market Sentiment**: {analysis.MarketFlow.MarketSentiment}");
        report.AppendLine($"- **Steam Move Detected**: {(analysis.MarketFlow.SteamMoveDetected ? "Yes" : "No")}");
        
        if (analysis.MarketFlow.LastSteamMove.HasValue)
        {
            report.AppendLine($"- **Last Steam Move**: {analysis.MarketFlow.LastSteamMove:HH:mm:ss}");
        }
        
        report.AppendLine($"- **Recent Volume Ratio**: {analysis.MarketFlow.RecentVolumeRatio:F2}");
        report.AppendLine();

        if (analysis.VolumeSpikes.Any())
        {
            report.AppendLine("#### Significant Volume Activity");
            foreach (var spike in analysis.VolumeSpikes.Take(5))
            {
                report.AppendLine($"- **{spike.Time:HH:mm:ss}** - {spike.Type} at {spike.Price:F2} " +
                                $"(Volume: {spike.Volume:F1}, Direction: {spike.Direction})");
            }
            report.AppendLine();
        }
    }

    private void GenerateSupportResistanceAnalysis(StringBuilder report, AnalysisResult analysis)
    {
        report.AppendLine("### Support and Resistance Analysis");
        report.AppendLine();
        
        if (analysis.SupportLevels.Any())
        {
            report.AppendLine("#### Key Support Levels");
            foreach (var support in analysis.SupportLevels.Take(3))
            {
                report.AppendLine($"- **{support.Level:F2}**: {support.Strength} support " +
                                $"({support.TouchCount} touches, Volume: {support.TotalVolume:F1})");
            }
            report.AppendLine();
        }

        if (analysis.ResistanceLevels.Any())
        {
            report.AppendLine("#### Key Resistance Levels");
            foreach (var resistance in analysis.ResistanceLevels.Take(3))
            {
                report.AppendLine($"- **{resistance.Level:F2}**: {resistance.Strength} resistance " +
                                $"({resistance.TouchCount} touches, Volume: {resistance.TotalVolume:F1})");
            }
            report.AppendLine();
        }
    }

    private void GenerateTradingRecommendations(StringBuilder report, AnalysisResult analysis)
    {
        report.AppendLine("## Trading Strategy Recommendations");
        report.AppendLine();

        if (!analysis.TradingRecommendations.Any())
        {
            report.AppendLine("No specific trading recommendations generated based on current market conditions.");
            return;
        }

        for (int i = 0; i < analysis.TradingRecommendations.Count; i++)
        {
            var rec = analysis.TradingRecommendations[i];
            
            report.AppendLine($"### Strategy {i + 1}: {rec.Strategy}");
            report.AppendLine();
            
            report.AppendLine($"- **Action**: {rec.Action}");
            report.AppendLine($"- **Entry Price**: {rec.EntryPrice:F2}");
            
            if (rec.TargetPrice > 0)
                report.AppendLine($"- **Target Price**: {rec.TargetPrice:F2}");
            else
                report.AppendLine($"- **Target**: Hold to win");
                
            report.AppendLine($"- **Stop Loss**: {rec.StopLoss:F2}");
            report.AppendLine($"- **Expected Profit**: {rec.ExpectedProfit:F1}%");
            report.AppendLine($"- **Risk/Reward Ratio**: {rec.RiskReward:F1}");
            report.AppendLine($"- **Time Frame**: {rec.TimeFrame}");
            report.AppendLine($"- **Risk Level**: {rec.RiskLevel}");
            report.AppendLine($"- **Confidence Level**: {rec.ConfidenceLevel}/10");
            report.AppendLine($"- **Position Sizing**: {rec.PositionSizing}% of bankroll");
            report.AppendLine($"- **Logic**: {rec.Logic}");
            report.AppendLine();
        }
    }

    private void GenerateRiskAssessment(StringBuilder report, AnalysisResult analysis)
    {
        report.AppendLine("### Risk Assessment and Position Sizing");
        report.AppendLine();
        
        var timeToRace = analysis.TimeToRaceStart.TotalMinutes;
        var riskLevel = timeToRace < 5 ? "High" : timeToRace < 15 ? "Moderate" : "Low";
        
        report.AppendLine($"- **Time Risk**: {riskLevel} (Race starts in {timeToRace:F0} minutes)");
        report.AppendLine($"- **Volatility Risk**: {(analysis.VolumeSpikes.Count > 5 ? "High" : "Moderate")} " +
                         $"({analysis.VolumeSpikes.Count} recent volume spikes)");
        
        var liquidityRisk = analysis.TotalVolume > 1000 ? "Low" : analysis.TotalVolume > 500 ? "Moderate" : "High";
        report.AppendLine($"- **Liquidity Risk**: {liquidityRisk} (Total volume: {analysis.TotalVolume:F0})");
        
        report.AppendLine();
        
        report.AppendLine("#### Recommended Position Sizing");
        report.AppendLine("- **Conservative**: 1-2% of bankroll");
        report.AppendLine("- **Moderate**: 3-5% of bankroll");
        report.AppendLine("- **Aggressive**: 5-10% of bankroll");
        report.AppendLine();
    }

    private void GenerateSummary(StringBuilder report, AnalysisResult analysis)
    {
        report.AppendLine("## Key Findings Summary");
        report.AppendLine();
        
        report.AppendLine($"- **Favorite Selection**: {analysis.SelectionName} at {analysis.CurrentOdds:F2} odds " +
                         $"({analysis.ImpliedProbability:F1}% implied probability)");
        report.AppendLine($"- **Odds Movement**: {analysis.OddsMovement:+0.00;-0.00} points from opening " +
                         $"({analysis.OddsMovementPercentage:+0.0;-0.0}% change)");
        report.AppendLine($"- **Current Trend**: {analysis.TrendDirection}");
        report.AppendLine($"- **Liquidity Status**: {(analysis.TotalVolume > 1000 ? "Excellent" : "Good")} " +
                         $"- {analysis.TotalVolume:F0} units traded");
        report.AppendLine($"- **Market Flow**: {analysis.MarketFlow.DominantDirection} pressure dominant " +
                         $"({analysis.MarketFlow.BackingPressure:F0}% backing vs {analysis.MarketFlow.LayingPressure:F0}% laying)");
        
        if (analysis.MarketFlow.SteamMoveDetected)
        {
            report.AppendLine($"- **Steam Move Alert**: Detected - significant informed money activity");
        }
        
        report.AppendLine();
        
        if (analysis.TradingRecommendations.Any())
        {
            var topRec = analysis.TradingRecommendations.First();
            report.AppendLine("### Top Recommendation");
            report.AppendLine($"- **Strategy**: {topRec.Strategy}");
            report.AppendLine($"- **Entry Price**: {topRec.EntryPrice:F2}");
            report.AppendLine($"- **Target**: {(topRec.TargetPrice > 0 ? topRec.TargetPrice.ToString("F2") : "Hold to win")}");
            report.AppendLine($"- **Risk Level**: {topRec.RiskLevel}");
            report.AppendLine($"- **Confidence**: {topRec.ConfidenceLevel}/10");
            report.AppendLine($"- **Expected Return**: {topRec.ExpectedProfit:F1}%");
        }
        
        report.AppendLine();
        report.AppendLine("---");
        report.AppendLine("*Analysis generated using Skender.Stock.Indicators library for technical analysis*");
        report.AppendLine($"*Report generated at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}*");
    }
}
