from datetime import datetime, timedelta
from typing import List

from models.betfair_models import MarketData
from models.analysis_models import AnalysisResult


class ReportGeneratorService:
    
    def generate_comprehensive_report(self, analysis: AnalysisResult, market_data: MarketData) -> str:
        """
        Generate a comprehensive markdown report from analysis results
        """
        report_lines = []
        
        # Header
        self._generate_header(report_lines, analysis, market_data)
        
        # Market Information
        self._generate_market_information(report_lines, market_data, analysis)
        
        # Favorite Selection Analysis
        self._generate_favorite_analysis(report_lines, analysis)
        
        # Technical Analysis Section
        self._generate_technical_analysis(report_lines, analysis)
        
        # Volume and Flow Analysis
        self._generate_volume_analysis(report_lines, analysis)
        
        # Support/Resistance Analysis
        self._generate_support_resistance_analysis(report_lines, analysis)
        
        # Trading Recommendations
        self._generate_trading_recommendations(report_lines, analysis)
        
        # Risk Assessment
        self._generate_risk_assessment(report_lines, analysis)
        
        # Summary and Key Findings
        self._generate_summary(report_lines, analysis)
        
        return '\n'.join(report_lines)
    
    def _generate_header(self, report_lines: List[str], analysis: AnalysisResult, market_data: MarketData):
        """Generate report header"""
        report_lines.extend([
            "# Betfair Market Analysis Report",
            "## Generated using TA-Lib Technical Analysis (Python)",
            "",
            f"**Analysis Time**: {analysis.analysis_time.strftime('%Y-%m-%d %H:%M:%S')}",
            f"**Time to Race Start**: {analysis.time_to_race_start}",
            ""
        ])
    
    def _generate_market_information(self, report_lines: List[str], market_data: MarketData, analysis: AnalysisResult):
        """Generate market information section"""
        report_lines.extend([
            "## Market Information",
            "```",
            f"Market: {market_data.market_name}",
            f"Event: {market_data.event_name}",
            f"Market ID: {market_data.market_id}",
            f"Status: {market_data.status}",
            f"Event Type: {market_data.event_type}",
            f"Start Time: {market_data.start_time.strftime('%Y-%m-%d %H:%M:%S')}",
            f"Analysis Time: {analysis.analysis_time.strftime('%Y-%m-%d %H:%M:%S')}",
            "```",
            ""
        ])
    
    def _generate_favorite_analysis(self, report_lines: List[str], analysis: AnalysisResult):
        """Generate favorite selection analysis"""
        report_lines.extend([
            "## Favorite Selection Analysis Report",
            "",
            "### Selection Overview",
            f"- **Selection Name**: {analysis.selection_name}",
            f"- **Current Odds**: {analysis.current_odds:.2f}",
            f"- **Implied Probability**: {analysis.implied_probability:.1f}% (P = 1/{analysis.current_odds:.2f})",
            f"- **Opening Odds**: {analysis.opening_odds:.2f}",
            f"- **Odds Movement**: {analysis.odds_movement:+.2f} points ({analysis.odds_movement_percentage:+.1f}% change)",
            f"- **Position in Market**: Market favorite with lowest odds",
            ""
        ])
        
        movement_description = (
            "significant shortening (backing pressure)" if analysis.odds_movement < 0 else
            "lengthening (laying pressure)" if analysis.odds_movement > 0 else
            "stable"
        )
        report_lines.extend([
            f"- **Movement Analysis**: {movement_description}",
            ""
        ])
    
    def _generate_technical_analysis(self, report_lines: List[str], analysis: AnalysisResult):
        """Generate technical analysis section"""
        indicators = analysis.indicators
        
        report_lines.extend([
            "### Technical Analysis (TA-Lib)",
            "",
            "#### Technical Indicators",
            f"- **RSI (14-period)**: {f'{indicators.rsi:.1f}' if indicators.rsi is not None else 'N/A'}",
            f"- **SMA (5-period)**: {f'{indicators.sma5:.2f}' if indicators.sma5 is not None else 'N/A'}",
            f"- **SMA (10-period)**: {f'{indicators.sma10:.2f}' if indicators.sma10 is not None else 'N/A'}",
            f"- **EMA (5-period)**: {f'{indicators.ema5:.2f}' if indicators.ema5 is not None else 'N/A'}",
            f"- **EMA (10-period)**: {f'{indicators.ema10:.2f}' if indicators.ema10 is not None else 'N/A'}"
        ])
        
        if indicators.macd is not None:
            report_lines.extend([
                f"- **MACD**: {indicators.macd:.3f}",
                f"- **MACD Signal**: {f'{indicators.macd_signal:.3f}' if indicators.macd_signal is not None else 'N/A'}"
            ])
        
        if indicators.bollinger_upper is not None:
            report_lines.extend([
                f"- **Bollinger Upper**: {indicators.bollinger_upper:.2f}",
                f"- **Bollinger Middle**: {f'{indicators.bollinger_middle:.2f}' if indicators.bollinger_middle is not None else 'N/A'}",
                f"- **Bollinger Lower**: {f'{indicators.bollinger_lower:.2f}' if indicators.bollinger_lower is not None else 'N/A'}"
            ])
        
        report_lines.extend([
            f"- **Momentum Direction**: {indicators.momentum_direction}",
            f"- **Volume Moving Average**: {indicators.volume_moving_average:.1f}",
            "",
            "#### Trend Analysis",
            f"- **Current Trend**: {analysis.trend_direction}",
            f"- **Total Trading Volume**: {analysis.total_volume:.1f} units"
        ])
        
        # RSI interpretation
        if indicators.rsi is not None:
            rsi_interpretation = (
                "Oversold (potential bounce)" if indicators.rsi < 30 else
                "Overbought (potential reversal)" if indicators.rsi > 70 else
                "Neutral"
            )
            report_lines.append(f"- **RSI Interpretation**: {rsi_interpretation}")
        
        report_lines.append("")
    
    def _generate_volume_analysis(self, report_lines: List[str], analysis: AnalysisResult):
        """Generate volume and market flow analysis"""
        market_flow = analysis.market_flow
        
        report_lines.extend([
            "### Volume and Market Flow Analysis",
            "",
            "#### Market Flow Indicators",
            f"- **Dominant Direction**: {market_flow.dominant_direction}",
            f"- **Backing Pressure**: {market_flow.backing_pressure:.1f}%",
            f"- **Laying Pressure**: {market_flow.laying_pressure:.1f}%",
            f"- **Market Sentiment**: {market_flow.market_sentiment}",
            f"- **Steam Move Detected**: {'Yes' if market_flow.steam_move_detected else 'No'}"
        ])
        
        if market_flow.last_steam_move:
            report_lines.append(f"- **Last Steam Move**: {market_flow.last_steam_move.strftime('%H:%M:%S')}")
        
        report_lines.extend([
            f"- **Recent Volume Ratio**: {market_flow.recent_volume_ratio:.2f}",
            ""
        ])
        
        if analysis.volume_spikes:
            report_lines.extend([
                "#### Significant Volume Activity"
            ])
            for spike in analysis.volume_spikes[:5]:
                report_lines.append(
                    f"- **{spike.time.strftime('%H:%M:%S')}** - {spike.type} at {spike.price:.2f} "
                    f"(Volume: {spike.volume:.1f}, Direction: {spike.direction})"
                )
            report_lines.append("")
    
    def _generate_support_resistance_analysis(self, report_lines: List[str], analysis: AnalysisResult):
        """Generate support and resistance analysis"""
        report_lines.extend([
            "### Support and Resistance Analysis",
            ""
        ])
        
        if analysis.support_levels:
            report_lines.append("#### Key Support Levels")
            for support in analysis.support_levels[:3]:
                report_lines.append(
                    f"- **{support.level:.2f}**: {support.strength} support "
                    f"({support.touch_count} touches, Volume: {support.total_volume:.1f})"
                )
            report_lines.append("")
        
        if analysis.resistance_levels:
            report_lines.append("#### Key Resistance Levels")
            for resistance in analysis.resistance_levels[:3]:
                report_lines.append(
                    f"- **{resistance.level:.2f}**: {resistance.strength} resistance "
                    f"({resistance.touch_count} touches, Volume: {resistance.total_volume:.1f})"
                )
            report_lines.append("")
    
    def _generate_trading_recommendations(self, report_lines: List[str], analysis: AnalysisResult):
        """Generate trading recommendations section"""
        report_lines.extend([
            "## Trading Strategy Recommendations",
            ""
        ])
        
        if not analysis.trading_recommendations:
            report_lines.append("No specific trading recommendations generated based on current market conditions.")
            return
        
        for i, rec in enumerate(analysis.trading_recommendations, 1):
            report_lines.extend([
                f"### Strategy {i}: {rec.strategy}",
                "",
                f"- **Action**: {rec.action}",
                f"- **Entry Price**: {rec.entry_price:.2f}"
            ])
            
            if rec.target_price > 0:
                report_lines.append(f"- **Target Price**: {rec.target_price:.2f}")
            else:
                report_lines.append("- **Target**: Hold to win")
            
            report_lines.extend([
                f"- **Stop Loss**: {rec.stop_loss:.2f}",
                f"- **Expected Profit**: {rec.expected_profit:.1f}%",
                f"- **Risk/Reward Ratio**: {rec.risk_reward:.1f}",
                f"- **Time Frame**: {rec.time_frame}",
                f"- **Risk Level**: {rec.risk_level}",
                f"- **Confidence Level**: {rec.confidence_level}/10",
                f"- **Position Sizing**: {rec.position_sizing}% of bankroll",
                f"- **Logic**: {rec.logic}",
                ""
            ])
    
    def _generate_risk_assessment(self, report_lines: List[str], analysis: AnalysisResult):
        """Generate risk assessment section"""
        time_to_race = analysis.time_to_race_start.total_seconds() / 60  # Convert to minutes
        risk_level = "High" if time_to_race < 5 else "Moderate" if time_to_race < 15 else "Low"
        
        report_lines.extend([
            "### Risk Assessment and Position Sizing",
            "",
            f"- **Time Risk**: {risk_level} (Race starts in {time_to_race:.0f} minutes)",
            f"- **Volatility Risk**: {'High' if len(analysis.volume_spikes) > 5 else 'Moderate'} "
            f"({len(analysis.volume_spikes)} recent volume spikes)"
        ])
        
        liquidity_risk = (
            "Low" if analysis.total_volume > 1000 else
            "Moderate" if analysis.total_volume > 500 else
            "High"
        )
        report_lines.extend([
            f"- **Liquidity Risk**: {liquidity_risk} (Total volume: {analysis.total_volume:.0f})",
            "",
            "#### Recommended Position Sizing",
            "- **Conservative**: 1-2% of bankroll",
            "- **Moderate**: 3-5% of bankroll",
            "- **Aggressive**: 5-10% of bankroll",
            ""
        ])
    
    def _generate_summary(self, report_lines: List[str], analysis: AnalysisResult):
        """Generate summary section"""
        report_lines.extend([
            "## Key Findings Summary",
            "",
            f"- **Favorite Selection**: {analysis.selection_name} at {analysis.current_odds:.2f} odds "
            f"({analysis.implied_probability:.1f}% implied probability)",
            f"- **Odds Movement**: {analysis.odds_movement:+.2f} points from opening "
            f"({analysis.odds_movement_percentage:+.1f}% change)",
            f"- **Current Trend**: {analysis.trend_direction}",
            f"- **Liquidity Status**: {'Excellent' if analysis.total_volume > 1000 else 'Good'} "
            f"- {analysis.total_volume:.0f} units traded",
            f"- **Market Flow**: {analysis.market_flow.dominant_direction} pressure dominant "
            f"({analysis.market_flow.backing_pressure:.0f}% backing vs {analysis.market_flow.laying_pressure:.0f}% laying)"
        ])
        
        if analysis.market_flow.steam_move_detected:
            report_lines.append("- **Steam Move Alert**: Detected - significant informed money activity")
        
        report_lines.append("")
        
        if analysis.trading_recommendations:
            top_rec = analysis.trading_recommendations[0]
            report_lines.extend([
                "### Top Recommendation",
                f"- **Strategy**: {top_rec.strategy}",
                f"- **Entry Price**: {top_rec.entry_price:.2f}",
                f"- **Target**: {top_rec.target_price:.2f if top_rec.target_price > 0 else 'Hold to win'}",
                f"- **Risk Level**: {top_rec.risk_level}",
                f"- **Confidence**: {top_rec.confidence_level}/10",
                f"- **Expected Return**: {top_rec.expected_profit:.1f}%"
            ])
        
        report_lines.extend([
            "",
            "---",
            "*Analysis generated using TA-Lib library for technical analysis*",
            f"*Report generated at: {datetime.now().strftime('%Y-%m-%d %H:%M:%S')}*"
        ])
