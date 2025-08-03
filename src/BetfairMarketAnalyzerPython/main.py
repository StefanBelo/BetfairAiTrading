#!/usr/bin/env python3
"""
Betfair Market Analyzer - Python Version

A Python application that uses TA-Lib to perform comprehensive technical analysis 
on Betfair betting market data, generating detailed trading reports similar to 
professional financial market analysis.

This is the Python equivalent of the C# BetfairMarketAnalyzer project.
"""

import sys
import os
from datetime import datetime, timedelta
import random
from typing import List

# Add the current directory to the Python path
sys.path.append(os.path.dirname(os.path.abspath(__file__)))

from models.betfair_models import (
    MarketData, Selection, TradedPriceVolume, 
    SelectionWithHistory, MarketSelectionData
)
from services.report_generator_service import ReportGeneratorService

# Try to import TA-Lib version first, fall back to basic version
try:
    from services.technical_analysis_service import TechnicalAnalysisService
    print("Using TA-Lib for technical analysis")
except ImportError:
    from services.technical_analysis_service_basic import TechnicalAnalysisServiceBasic as TechnicalAnalysisService
    print("TA-Lib not available, using basic technical analysis")


def generate_sample_data() -> MarketSelectionData:
    """
    Generate sample market data for testing.
    In production, this would be replaced with actual API calls to Bfexplorer.
    """
    # Create market data
    market_data = MarketData(
        market_id="1.246135530",
        market_name="1m4f Hcap",
        event_name="Galway Horse Racing",
        event_type="Horse Racing",
        status="Open",
        start_time=datetime.now() + timedelta(minutes=5)  # Race starts in 5 minutes
    )
    
    # Create selection
    selection = Selection(
        selection_id="81530996_0.00",
        name="Glenroyal",
        price=3.45,
        status="Active"
    )
    
    # Generate realistic price history data
    price_history = generate_realistic_price_history()
    
    # Create selection with history
    selection_with_history = SelectionWithHistory(
        selection=selection,
        traded_prices_and_volume=price_history
    )
    
    return MarketSelectionData(
        market=market_data,
        selections=[selection_with_history]
    )


def generate_realistic_price_history() -> List[TradedPriceVolume]:
    """
    Generate realistic price movement data based on actual patterns
    """
    history = []
    base_time = datetime.now() - timedelta(hours=2)
    
    # Simulate realistic price movements based on the actual data pattern
    price_points = [
        4.88, 4.78, 4.69, 4.59, 4.50, 4.41, 3.56, 3.52, 3.75, 2.95,
        3.19, 3.05, 2.91, 2.86, 2.74, 2.72, 2.63, 2.59, 2.81, 2.86,
        3.24, 3.28, 3.05, 3.00, 2.95, 2.91, 2.86, 2.66, 2.81, 2.79,
        3.25, 3.45, 3.50, 3.60, 3.70, 3.75, 3.85, 3.95, 3.70, 3.50,
        3.95, 3.45, 3.90, 3.45, 3.35, 3.30, 3.20, 3.45
    ]
    
    for i, price in enumerate(price_points):
        volume = random.randint(1, 100)
        
        # Higher volume on significant moves
        if i > 30 and i > 0 and abs(price - price_points[i-1]) > 0.1:
            volume = random.randint(50, 500)
        
        history.append(TradedPriceVolume(
            time=base_time + timedelta(minutes=i * 2),
            price=price,
            volume=volume
        ))
    
    return history


def display_key_metrics(result):
    """
    Display key metrics summary in the console
    """
    print()
    print("=== KEY METRICS SUMMARY ===")
    print(f"Selection: {result.selection_name}")
    print(f"Current Odds: {result.current_odds:.2f}")
    print(f"Implied Probability: {result.implied_probability:.1f}%")
    print(f"Odds Movement: {result.odds_movement:+.2f} ({result.odds_movement_percentage:+.1f}%)")
    print(f"Trend: {result.trend_direction}")
    print(f"Total Volume: {result.total_volume:.0f}")
    print(f"Market Flow: {result.market_flow.dominant_direction}")
    
    if result.indicators.rsi is not None:
        print(f"RSI: {result.indicators.rsi:.1f}")
    
    if result.trading_recommendations:
        top_rec = result.trading_recommendations[0]
        print(f"Top Strategy: {top_rec.strategy}")
        print(f"Confidence: {top_rec.confidence_level}/10")
    
    print("============================")


def main():
    """
    Main entry point of the application
    """
    print("Betfair Market Analyzer using TA-Lib (Python Version)")
    print("=====================================================")
    print()
    
    try:
        # Sample data - in real implementation, this would come from Bfexplorer API
        print("Generating sample data...")
        sample_data = generate_sample_data()
        
        # Initialize services
        print("Initializing analysis services...")
        analysis_service = TechnicalAnalysisService()
        report_service = ReportGeneratorService()
        
        # Perform technical analysis
        print("Performing technical analysis...")
        analysis_result = analysis_service.analyze_selection(sample_data)
        
        # Generate comprehensive report
        print("Generating comprehensive report...")
        report = report_service.generate_comprehensive_report(analysis_result, sample_data.market)
        
        # Display report
        print()
        print(report)
        
        # Save report to file
        timestamp = datetime.now().strftime("%Y%m%d_%H%M%S")
        filename = f"BetfairAnalysis_{timestamp}.md"
        
        with open(filename, 'w', encoding='utf-8') as f:
            f.write(report)
        
        print(f"\nReport saved to: {filename}")
        
        # Display key metrics in console
        display_key_metrics(analysis_result)
        
    except ImportError as e:
        print(f"Import Error: {e}")
        print("\nPlease install required dependencies:")
        print("pip install -r requirements.txt")
        print("\nNote: TA-Lib requires additional setup. Please refer to README.md for installation instructions.")
    except Exception as ex:
        print(f"Error: {ex}")
        import traceback
        print(f"Stack trace: {traceback.format_exc()}")
    
    print("\nPress Enter to exit...")
    input()


if __name__ == "__main__":
    main()
