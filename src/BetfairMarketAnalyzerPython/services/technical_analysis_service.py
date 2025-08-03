import pandas as pd
import numpy as np
from datetime import datetime, timedelta
from typing import List, Tuple
import talib
from collections import defaultdict

from models.betfair_models import MarketSelectionData, TradedPriceVolume
from models.analysis_models import (
    AnalysisResult, TechnicalIndicators, SupportResistanceLevel,
    VolumeAnalysis, MarketFlow, TradingRecommendation
)


class TechnicalAnalysisService:
    
    def analyze_selection(self, market_data: MarketSelectionData) -> AnalysisResult:
        """
        Perform comprehensive technical analysis on a selection's price data
        """
        if not market_data.selections:
            raise ValueError("No selection data provided")
        
        selection = market_data.selections[0]
        price_data = sorted(selection.traded_prices_and_volume, key=lambda x: x.time)
        
        if not price_data:
            raise ValueError("No price data available")
        
        # Convert to pandas DataFrame for analysis
        df = self._convert_to_dataframe(price_data)
        
        # Calculate technical indicators
        indicators = self._calculate_technical_indicators(df)
        
        # Analyze support/resistance levels
        support_levels, resistance_levels = self._analyze_support_resistance(price_data)
        
        # Analyze volume patterns
        volume_analysis = self._analyze_volume(price_data)
        
        # Detect market flow
        market_flow = self._analyze_market_flow(price_data)
        
        # Generate trading recommendations
        recommendations = self._generate_trading_recommendations(
            price_data, indicators, support_levels, resistance_levels, market_flow
        )
        
        current_price = price_data[-1].price
        opening_price = price_data[0].price
        odds_movement = current_price - opening_price
        odds_movement_percentage = (odds_movement / opening_price) * 100
        
        return AnalysisResult(
            selection_name=selection.selection.name,
            current_odds=current_price,
            opening_odds=opening_price,
            implied_probability=round(1 / current_price * 100, 2),
            odds_movement=round(odds_movement, 2),
            odds_movement_percentage=round(odds_movement_percentage, 2),
            trend_direction=self._determine_trend_direction(df),
            total_volume=sum(trade.volume for trade in price_data),
            support_levels=support_levels,
            resistance_levels=resistance_levels,
            volume_spikes=volume_analysis,
            indicators=indicators,
            trading_recommendations=recommendations,
            market_flow=market_flow,
            analysis_time=datetime.now(),
            time_to_race_start=market_data.market.start_time - datetime.now()
        )
    
    def _convert_to_dataframe(self, price_data: List[TradedPriceVolume]) -> pd.DataFrame:
        """
        Convert price data to pandas DataFrame with OHLCV format
        """
        # Create DataFrame from price data
        data = []
        for trade in price_data:
            data.append({
                'timestamp': trade.time,
                'price': trade.price,
                'volume': trade.volume
            })
        
        df = pd.DataFrame(data)
        df.set_index('timestamp', inplace=True)
        
        # Resample to 1-minute intervals to create OHLCV data
        ohlcv = df.groupby(pd.Grouper(freq='1min')).agg({
            'price': ['first', 'max', 'min', 'last'],
            'volume': 'sum'
        }).dropna()
        
        # Flatten column names
        ohlcv.columns = ['open', 'high', 'low', 'close', 'volume']
        
        # Forward fill missing values
        ohlcv = ohlcv.ffill()
        
        return ohlcv
    
    def _calculate_technical_indicators(self, df: pd.DataFrame) -> TechnicalIndicators:
        """
        Calculate technical indicators using TA-Lib
        """
        if len(df) < 14:
            return TechnicalIndicators()
        
        try:
            close_prices = df['close'].values.astype(float)
            high_prices = df['high'].values.astype(float)
            low_prices = df['low'].values.astype(float)
            volumes = df['volume'].values.astype(float)
            
            # RSI
            rsi = talib.RSI(close_prices, timeperiod=14)
            
            # Moving averages
            sma5 = talib.SMA(close_prices, timeperiod=5)
            sma10 = talib.SMA(close_prices, timeperiod=10)
            ema5 = talib.EMA(close_prices, timeperiod=5)
            ema10 = talib.EMA(close_prices, timeperiod=10)
            
            # MACD
            macd, macd_signal, _ = talib.MACD(close_prices)
            
            # Bollinger Bands
            bb_upper, bb_middle, bb_lower = talib.BBANDS(close_prices)
            
            # Volume moving average
            volume_avg = np.mean(volumes[-10:]) if len(volumes) >= 10 else np.mean(volumes)
            
            return TechnicalIndicators(
                rsi=float(rsi[-1]) if not np.isnan(rsi[-1]) else None,
                sma5=float(sma5[-1]) if not np.isnan(sma5[-1]) else None,
                sma10=float(sma10[-1]) if not np.isnan(sma10[-1]) else None,
                ema5=float(ema5[-1]) if not np.isnan(ema5[-1]) else None,
                ema10=float(ema10[-1]) if not np.isnan(ema10[-1]) else None,
                macd=float(macd[-1]) if not np.isnan(macd[-1]) else None,
                macd_signal=float(macd_signal[-1]) if not np.isnan(macd_signal[-1]) else None,
                bollinger_upper=float(bb_upper[-1]) if not np.isnan(bb_upper[-1]) else None,
                bollinger_lower=float(bb_lower[-1]) if not np.isnan(bb_lower[-1]) else None,
                bollinger_middle=float(bb_middle[-1]) if not np.isnan(bb_middle[-1]) else None,
                momentum_direction=self._determine_momentum(df),
                volume_moving_average=float(volume_avg)
            )
        except Exception:
            return TechnicalIndicators()
    
    def _determine_trend_direction(self, df: pd.DataFrame) -> str:
        """
        Determine the current trend direction
        """
        if len(df) < 2:
            return "Insufficient Data"
        
        recent_data = df.tail(10)
        price_change = recent_data['close'].iloc[-1] - recent_data['close'].iloc[0]
        
        if price_change < -0.05:
            return "Shortening (Backing Pressure)"
        elif price_change > 0.05:
            return "Lengthening (Laying Pressure)"
        else:
            return "Sideways"
    
    def _determine_momentum(self, df: pd.DataFrame) -> str:
        """
        Determine momentum direction
        """
        if len(df) < 5:
            return "Neutral"
        
        recent = df.tail(5)
        momentum = recent['close'].iloc[-1] - recent['close'].iloc[0]
        
        if momentum < -0.02:
            return "Strong Shortening"
        elif momentum > 0.02:
            return "Strong Lengthening"
        else:
            return "Neutral"
    
    def _analyze_support_resistance(self, price_data: List[TradedPriceVolume]) -> Tuple[List[SupportResistanceLevel], List[SupportResistanceLevel]]:
        """
        Analyze support and resistance levels
        """
        support_levels = []
        resistance_levels = []
        
        # Group prices by levels (rounded to 2 decimal places)
        price_groups = defaultdict(list)
        for trade in price_data:
            rounded_price = round(trade.price, 2)
            price_groups[rounded_price].append(trade)
        
        # Filter groups with at least 3 touches
        significant_levels = {
            price: trades for price, trades in price_groups.items()
            if len(trades) >= 3
        }
        
        # Sort by total volume
        sorted_levels = sorted(
            significant_levels.items(),
            key=lambda x: sum(t.volume for t in x[1]),
            reverse=True
        )[:10]
        
        current_price = price_data[-1].price
        
        for price, trades in sorted_levels:
            level = SupportResistanceLevel(
                level=price,
                touch_count=len(trades),
                total_volume=sum(t.volume for t in trades),
                last_touch=max(t.time for t in trades),
                strength="Strong" if sum(t.volume for t in trades) > 100 else "Moderate"
            )
            
            if price < current_price:
                support_levels.append(level)
            else:
                resistance_levels.append(level)
        
        # Sort and limit results
        support_levels.sort(key=lambda x: x.level, reverse=True)
        resistance_levels.sort(key=lambda x: x.level)
        
        return support_levels[:5], resistance_levels[:5]
    
    def _analyze_volume(self, price_data: List[TradedPriceVolume]) -> List[VolumeAnalysis]:
        """
        Analyze volume patterns and detect spikes
        """
        volume_analysis = []
        avg_volume = np.mean([t.volume for t in price_data])
        volume_threshold = avg_volume * 2  # Spike threshold
        
        for i in range(1, len(price_data)):
            current = price_data[i]
            previous = price_data[i - 1]
            
            if current.volume > volume_threshold:
                direction = "Backing" if current.price < previous.price else "Laying"
                volume_type = "SteamMove" if current.volume > avg_volume * 5 else "Spike"
                
                volume_analysis.append(VolumeAnalysis(
                    time=current.time,
                    price=current.price,
                    volume=current.volume,
                    type=volume_type,
                    direction=direction
                ))
        
        # Sort by volume and return top 10
        volume_analysis.sort(key=lambda x: x.volume, reverse=True)
        return volume_analysis[:10]
    
    def _analyze_market_flow(self, price_data: List[TradedPriceVolume]) -> MarketFlow:
        """
        Analyze market flow and pressure
        """
        recent_data = price_data[-50:] if len(price_data) >= 50 else price_data
        if len(recent_data) < 2:
            return MarketFlow()
        
        backing_volume = 0.0
        laying_volume = 0.0
        
        for i in range(1, len(recent_data)):
            current = recent_data[i]
            previous = recent_data[i - 1]
            
            if current.price < previous.price:  # Odds shortening = backing
                backing_volume += current.volume
            elif current.price > previous.price:  # Odds lengthening = laying
                laying_volume += current.volume
        
        total_volume = backing_volume + laying_volume
        backing_pressure = (backing_volume / total_volume) * 100 if total_volume > 0 else 50
        laying_pressure = 100 - backing_pressure
        
        # Detect steam moves
        avg_volume = np.mean([t.volume for t in recent_data])
        steam_moves = [t for t in recent_data if t.volume > avg_volume * 5]
        steam_move_detected = len(steam_moves) > 0
        last_steam_move = max(s.time for s in steam_moves) if steam_moves else None
        
        # Determine dominant direction
        if backing_pressure > 60:
            dominant_direction = "Backing"
        elif laying_pressure > 60:
            dominant_direction = "Laying"
        else:
            dominant_direction = "Neutral"
        
        # Determine market sentiment
        if backing_pressure > 70:
            market_sentiment = "Strong Backing"
        elif laying_pressure > 70:
            market_sentiment = "Strong Laying"
        else:
            market_sentiment = "Balanced"
        
        return MarketFlow(
            dominant_direction=dominant_direction,
            backing_pressure=round(backing_pressure, 1),
            laying_pressure=round(laying_pressure, 1),
            recent_volume_ratio=round(backing_volume / max(laying_volume, 1), 2),
            steam_move_detected=steam_move_detected,
            last_steam_move=last_steam_move,
            market_sentiment=market_sentiment
        )
    
    def _generate_trading_recommendations(
        self,
        price_data: List[TradedPriceVolume],
        indicators: TechnicalIndicators,
        support_levels: List[SupportResistanceLevel],
        resistance_levels: List[SupportResistanceLevel],
        market_flow: MarketFlow
    ) -> List[TradingRecommendation]:
        """
        Generate trading recommendations based on analysis
        """
        recommendations = []
        current_price = price_data[-1].price
        recent_trend = price_data[-10:] if len(price_data) >= 10 else price_data
        price_direction = recent_trend[-1].price - recent_trend[0].price
        
        # Back-to-Lay Strategy (Primary)
        if market_flow.dominant_direction == "Backing" and price_direction < 0:
            nearest_support = support_levels[0] if support_levels else None
            target_price = nearest_support.level if nearest_support else current_price * 0.95
            
            recommendations.append(TradingRecommendation(
                strategy="Back-to-Lay Trading",
                action="Back-to-Lay",
                entry_price=current_price,
                target_price=target_price,
                stop_loss=current_price * 1.05,
                expected_profit=self._calculate_back_to_lay_profit(current_price, target_price),
                risk_reward=2.5,
                time_frame="5-15 minutes",
                risk_level="Moderate",
                confidence_level=8,
                logic="Strong backing pressure with volume confirmation supporting further shortening",
                position_sizing=5.0
            ))
        
        # Pure Backing Strategy
        if market_flow.backing_pressure > 65 and (indicators.rsi is None or indicators.rsi < 70):
            recommendations.append(TradingRecommendation(
                strategy="Pure Backing Position",
                action="Back",
                entry_price=current_price,
                target_price=0,  # Hold to win
                stop_loss=current_price * 1.15,
                expected_profit=(1 / current_price - 1) * 100,  # Win percentage
                risk_reward=current_price,
                time_frame="Hold to race",
                risk_level="Aggressive",
                confidence_level=7,
                logic="Heavy backing pressure indicates market confidence",
                position_sizing=2.0
            ))
        
        # Lay-to-Back Strategy (Counter-trend)
        if market_flow.dominant_direction == "Laying" and price_direction > 0:
            nearest_resistance = resistance_levels[0] if resistance_levels else None
            target_price = nearest_resistance.level if nearest_resistance else current_price * 1.05
            
            recommendations.append(TradingRecommendation(
                strategy="Lay-to-Back Trading",
                action="Lay-to-Back",
                entry_price=current_price,
                target_price=target_price,
                stop_loss=current_price * 0.95,
                expected_profit=self._calculate_lay_to_back_profit(current_price, target_price),
                risk_reward=1.8,
                time_frame="10-20 minutes",
                risk_level="Conservative",
                confidence_level=6,
                logic="Laying pressure may push odds higher providing lay-to-back opportunity",
                position_sizing=3.0
            ))
        
        # Scalping Strategy
        if len(recent_trend) > 5 and indicators.volume_moving_average > 10:
            recommendations.append(TradingRecommendation(
                strategy="Scalping",
                action="Quick Trade",
                entry_price=current_price,
                target_price=current_price + (0.02 if price_direction > 0 else -0.02),
                stop_loss=current_price + (-0.03 if price_direction > 0 else 0.03),
                expected_profit=2.0,  # 2 ticks
                risk_reward=0.67,
                time_frame="1-3 minutes",
                risk_level="Moderate",
                confidence_level=5,
                logic="High volume and volatility suitable for quick scalping",
                position_sizing=8.0
            ))
        
        # Sort by confidence level
        recommendations.sort(key=lambda x: x.confidence_level, reverse=True)
        return recommendations
    
    def _calculate_back_to_lay_profit(self, back_price: float, lay_price: float) -> float:
        """
        Calculate back-to-lay profit percentage
        """
        if lay_price >= back_price:
            return 0.0
        return round(((back_price - lay_price) / back_price) * 100, 2)
    
    def _calculate_lay_to_back_profit(self, lay_price: float, back_price: float) -> float:
        """
        Calculate lay-to-back profit percentage
        """
        if back_price <= lay_price:
            return 0.0
        return round(((back_price - lay_price) / lay_price) * 100, 2)
