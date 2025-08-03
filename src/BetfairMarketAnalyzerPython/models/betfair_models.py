from dataclasses import dataclass
from datetime import datetime
from typing import List, Optional


@dataclass
class MarketData:
    market_id: str = ""
    start_time: datetime = datetime.now()
    event_type: str = ""
    event_name: str = ""
    market_name: str = ""
    status: str = ""


@dataclass
class Selection:
    selection_id: str = ""
    name: str = ""
    price: float = 0.0
    status: str = ""


@dataclass
class TradedPriceVolume:
    time: datetime = datetime.now()
    price: float = 0.0
    volume: float = 0.0


@dataclass
class SelectionWithHistory:
    selection: Selection = None
    traded_prices_and_volume: List[TradedPriceVolume] = None
    
    def __post_init__(self):
        if self.selection is None:
            self.selection = Selection()
        if self.traded_prices_and_volume is None:
            self.traded_prices_and_volume = []


@dataclass
class MarketSelectionData:
    market: MarketData = None
    selections: List[SelectionWithHistory] = None
    
    def __post_init__(self):
        if self.market is None:
            self.market = MarketData()
        if self.selections is None:
            self.selections = []


@dataclass
class BetfairActiveMarketResponse:
    active_betfair_market: 'ActiveMarketData' = None
    
    def __post_init__(self):
        if self.active_betfair_market is None:
            self.active_betfair_market = ActiveMarketData()


@dataclass
class ActiveMarketData:
    market_id: str = ""
    start_time: datetime = datetime.now()
    event_type: str = ""
    event_name: str = ""
    market_name: str = ""
    status: str = ""
    selections: List[Selection] = None
    
    def __post_init__(self):
        if self.selections is None:
            self.selections = []
