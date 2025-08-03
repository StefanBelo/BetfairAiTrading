from dataclasses import dataclass
from datetime import datetime, timedelta
from typing import List, Optional


@dataclass
class SupportResistanceLevel:
    level: float = 0.0
    touch_count: int = 0
    total_volume: float = 0.0
    strength: str = ""
    last_touch: datetime = datetime.now()


@dataclass
class VolumeAnalysis:
    time: datetime = datetime.now()
    price: float = 0.0
    volume: float = 0.0
    type: str = ""  # "Spike", "SteamMove", "Normal"
    direction: str = ""  # "Backing", "Laying"


@dataclass
class TechnicalIndicators:
    rsi: Optional[float] = None
    sma5: Optional[float] = None
    sma10: Optional[float] = None
    ema5: Optional[float] = None
    ema10: Optional[float] = None
    macd: Optional[float] = None
    macd_signal: Optional[float] = None
    bollinger_upper: Optional[float] = None
    bollinger_lower: Optional[float] = None
    bollinger_middle: Optional[float] = None
    momentum_direction: str = ""
    volume_moving_average: float = 0.0


@dataclass
class TradingRecommendation:
    strategy: str = ""
    action: str = ""  # "Back", "Lay", "Back-to-Lay", "Lay-to-Back"
    entry_price: float = 0.0
    target_price: float = 0.0
    stop_loss: float = 0.0
    expected_profit: float = 0.0
    risk_reward: float = 0.0
    time_frame: str = ""
    risk_level: str = ""
    confidence_level: int = 0
    logic: str = ""
    position_sizing: float = 0.0


@dataclass
class MarketFlow:
    dominant_direction: str = ""  # "Backing", "Laying", "Neutral"
    backing_pressure: float = 0.0
    laying_pressure: float = 0.0
    recent_volume_ratio: float = 0.0
    steam_move_detected: bool = False
    last_steam_move: Optional[datetime] = None
    market_sentiment: str = ""


@dataclass
class PatternDetection:
    pattern_type: str = ""
    confidence: float = 0.0
    description: str = ""
    target_price: float = 0.0
    detected_at: datetime = datetime.now()


@dataclass
class AnalysisResult:
    selection_name: str = ""
    current_odds: float = 0.0
    opening_odds: float = 0.0
    implied_probability: float = 0.0
    odds_movement: float = 0.0
    odds_movement_percentage: float = 0.0
    trend_direction: str = ""
    total_volume: float = 0.0
    support_levels: List[SupportResistanceLevel] = None
    resistance_levels: List[SupportResistanceLevel] = None
    volume_spikes: List[VolumeAnalysis] = None
    indicators: TechnicalIndicators = None
    trading_recommendations: List[TradingRecommendation] = None
    market_flow: MarketFlow = None
    analysis_time: datetime = datetime.now()
    time_to_race_start: timedelta = timedelta()
    
    def __post_init__(self):
        if self.support_levels is None:
            self.support_levels = []
        if self.resistance_levels is None:
            self.resistance_levels = []
        if self.volume_spikes is None:
            self.volume_spikes = []
        if self.indicators is None:
            self.indicators = TechnicalIndicators()
        if self.trading_recommendations is None:
            self.trading_recommendations = []
        if self.market_flow is None:
            self.market_flow = MarketFlow()
