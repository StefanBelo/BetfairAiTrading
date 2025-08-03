#!/usr/bin/env python3
"""
Test script for Betfair Market Analyzer Python

This script runs a quick test to ensure all components are working correctly.
"""

import sys
import os
from datetime import datetime

# Add the current directory to the Python path
sys.path.append(os.path.dirname(os.path.abspath(__file__)))

def test_basic_functionality():
    """Test basic functionality of the analyzer"""
    print("Testing Betfair Market Analyzer Python...")
    
    try:
        # Test imports
        print("1. Testing imports...")
        from models.betfair_models import MarketData, Selection, TradedPriceVolume
        from models.analysis_models import AnalysisResult, TechnicalIndicators
        from services.technical_analysis_service_basic import TechnicalAnalysisServiceBasic
        from services.report_generator_service import ReportGeneratorService
        print("   ✓ All imports successful")
        
        # Test model creation
        print("2. Testing model creation...")
        market = MarketData(
            market_id="test.123",
            market_name="Test Market",
            event_name="Test Event"
        )
        selection = Selection(
            selection_id="test_selection",
            name="Test Horse",
            price=2.5
        )
        print("   ✓ Models created successfully")
        
        # Test technical indicators
        print("3. Testing technical indicators...")
        indicators = TechnicalIndicators(
            rsi=45.6,
            sma5=2.45,
            sma10=2.55
        )
        print("   ✓ Technical indicators created successfully")
        
        # Test services
        print("4. Testing services...")
        analysis_service = TechnicalAnalysisServiceBasic()
        report_service = ReportGeneratorService()
        print("   ✓ Services initialized successfully")
        
        print("\n✓ All tests passed! The Betfair Market Analyzer Python is ready to use.")
        return True
        
    except Exception as e:
        print(f"\n✗ Test failed: {e}")
        import traceback
        print(f"Stack trace: {traceback.format_exc()}")
        return False

def main():
    """Main test function"""
    print("Betfair Market Analyzer Python - Test Suite")
    print("=" * 50)
    
    success = test_basic_functionality()
    
    if success:
        print("\nTo run the full application, execute:")
        print("python main.py")
    else:
        print("\nPlease check the error messages above and fix any issues.")
    
    return 0 if success else 1

if __name__ == "__main__":
    sys.exit(main())
