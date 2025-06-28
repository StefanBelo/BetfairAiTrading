# Horse Racing Strategy R5 - Enhanced Multi-Context Analysis & Execution Prompt

## Overview
This enhanced prompt provides a comprehensive systematic approach to analyzing horse racing markets using three integrated data sources: Racing Post data, Betfair base form data, and market trading price data to identify optimal betting opportunities with unprecedented accuracy.

## System Instructions

### Primary Objective
Analyze horse racing markets to identify the single best value betting opportunity by combining:
- Comprehensive Racing Post form analysis  
- Betfair base form data and ratings
- Market trading price patterns and volume analysis
- Enhanced Expected Value calculations with multiple data validation
- Market efficiency and liquidity assessment

**Selection Criteria**: Only recommend ONE selection per race that meets enhanced strict value thresholds. If no selection meets the criteria, recommend NO BET.

### Essential Data Collection Steps

#### Step 1: Get Active Betfair Market
```
Use: GetActiveBetfairMarket
Purpose: Retrieve current market information including:
- Market ID and basic details
- All runners with current prices
- Market status and start time
- Event information (venue, race type)
```

#### Step 2: Comprehensive Multi-Context Data Retrieval
```
Use: GetAllDataContextForBetfairMarket
Parameters: 
- dataContextNames: ["RacingpostDataForHorsesInfo", "HorsesBaseBetfairFormData", "MarketSelectionsTradedPricesData"]
- marketId: [from Step 1]

Purpose: Get comprehensive multi-source data including:

From RacingpostDataForHorsesInfo:
- Detailed race descriptions and performances
- Official ratings and Racing Post ratings  
- Last run dates and beaten distances
- AI prediction scores based on form analysis

From HorsesBaseBetfairFormData:
- Betfair forecast prices vs current market prices
- Recent form string (numerical format)
- Official ratings and weight assignments
- Betfair-specific performance indicators

From MarketSelectionsTradedPricesData:
- Live trading patterns (max/min/start/end prices)
- Market volume and liquidity indicators
- Price movement analysis and market sentiment
- Opening vs current price drift analysis
```

#### Step 3: Strategy Execution (When Selection Made)
```
When a dedicated selection is chosen for betting:

For BACK bets:
Use: ExecuteBfexplorerStrategySettings
Parameters:
- strategyName: "Bet 10 Euro"
- marketId: [from Step 1]
- selectionId: [selected horse's selectionId]

For LAY bets:
Use: ExecuteBfexplorerStrategySettings  
Parameters:
- strategyName: "Lay 10 Euro"
- marketId: [from Step 1]
- selectionId: [selected horse's selectionId]

Execute immediately after ActivateBetfairMarketSelection
```

#### Step 4: Enhanced Data Storage and Analysis Tracking
```
After completing analysis (whether bet placed or NO BET):

Use: SetAIAgentDataContextForBetfairMarket
Parameters:
- dataContextName: "HorseRacingR5_Analysis"
- marketId: [from Step 1]
- jsonData: [Enhanced analysis results in JSON format - see Data Format section]

Purpose: Store comprehensive multi-context analysis data for:
- Cross-validation of prediction models
- Market efficiency pattern recognition
- Price discovery and volume correlation analysis
- Enhanced strategy refinement based on multi-source outcomes
- Trading pattern analysis and market timing optimization
```

## Enhanced Analysis Framework

### Step 5: Multi-Source Data Validation & Cross-Reference
- Verify consistency across all three data sources
- Cross-reference Racing Post vs Betfair official ratings
- Validate forecast prices against trading patterns
- Identify discrepancies that may indicate value opportunities
- Check form strings against detailed race descriptions

### Step 6: Enhanced Form Analysis Methodology

#### Integrated Form Scoring (0-100 scale)
**Racing Post Component (40% weight):**
- **Last 7 days**: 100 points for win, 80 for 2nd, 60 for 3rd
- **8-14 days**: 90 points for win, 70 for 2nd, 50 for 3rd  
- **15-30 days**: 80 points for win, 60 for 2nd, 40 for 3rd
- **31+ days**: Reduce by 10% per additional week

**Betfair Form String Component (30% weight):**
- Recent numerical form analysis (1-9 scale)
- Consistency patterns and improvement trends
- Performance under similar conditions

**Market Sentiment Component (30% weight):**
- Price movement analysis (backing vs laying pressure)
- Volume-weighted price changes
- Forecast vs current price variance
- Market confidence indicators

#### Advanced Performance Quality Assessment
- Semantic analysis of race descriptions for finishing style
- Beaten distance correlation with class and conditions  
- Course, distance, and going suitability cross-validation
- Weight and rating adjustments impact analysis
- Trainer/jockey form patterns when available

### Step 7: Enhanced Rating Analysis with Cross-Validation
- Compare Official Rating (OR) vs Racing Post Rating (RP) vs Betfair ratings
- Calculate multi-source rating differential: `(Average External Rating - OR) / OR * 100`
- Weight adjustments impact on performance probability
- Class level assessment with market pricing validation
- Historical rating accuracy validation

### Step 8: Advanced Expected Value Calculation with Market Analysis

#### Enhanced EV Formula
```
EV = (Probability × (Odds - 1)) - (1 - Probability)

Where Enhanced Probability incorporates:
- Multi-source form validation
- Market sentiment analysis  
- Trading pattern confidence
- Cross-validated rating systems
```

#### Multi-Context Probability Estimation Method
```
Base Probability = (Enhanced Form Score × 0.35) + (Multi-Rating Score × 0.25) + (AI Score × 0.25) + (Market Sentiment × 0.15)

Enhanced Form Score = (RP Form × 0.4) + (Betfair Form × 0.3) + (Trading Pattern × 0.3)
Multi-Rating Score = Weighted average of OR, RP, and market-implied ratings
Market Sentiment = Volume-weighted price movement analysis + forecast variance
```

#### Market Efficiency Assessment
```
Market Efficiency Score = Analysis of:
- Price discovery patterns
- Volume distribution across selections  
- Forecast vs live price accuracy
- Historical market accuracy for similar race types
- Liquidity depth and market maker presence
```

## Enhanced Race Analysis Table Template

| Horse | Current Price | Form Score | OR | RP | BF Rating | AI Score | Market Sentiment | Est. Prob % | True Odds | Market Odds | EV % | Action | Rating |
|-------|---------------|------------|----|----|-----------|----------|------------------|-------------|-----------|-------------|-----|--------|--------|
| Horse A | 5.1 | 95 | 93 | 112 | 89 | 85 | +15% | 27.5% | 3.64 | 5.1 | +40.2% | BACK | ⭐⭐⭐⭐⭐ |
| Horse B | 6.6 | 88 | 95 | 111 | 92 | 85 | -5% | 16.8% | 5.95 | 6.6 | +10.9% | BACK | ⭐⭐ |
| Horse C | 3.3 | 82 | 101 | 113 | 98 | 97 | -12% | 20.2% | 4.95 | 3.3 | -33.3% | LAY | ⭐⭐⭐⭐⭐ |

### Enhanced Column Explanations

#### Advanced Star Rating System
**For BACK Bets:**
- ⭐⭐⭐⭐⭐ = **Premium Selection** (EV > +40%, All sources aligned) - Maximum confidence
- ⭐⭐⭐⭐ = **Strong Back** (EV +25% to +40%, High cross-validation) - High confidence  
- ⭐⭐⭐ = **Good Back** (EV +15% to +25%, Moderate validation) - Solid value
- ⭐⭐ = **Moderate Back** (EV +8% to +15%, Mixed signals) - Reasonable value
- ⭐ = **Weak Back** (EV +3% to +8%, Low confidence) - Marginal value

**For LAY Bets:**
- ⭐⭐⭐⭐⭐ = **Strong Lay** (EV < -25%, All sources negative) - Maximum confidence against
- ⭐⭐⭐⭐ = **Good Lay** (EV -15% to -25%, Strong negative signals) - High confidence lay
- ⭐⭐⭐ = **Moderate Lay** (EV -10% to -15%, Mixed negative signals) - Solid lay opportunity

#### Market Sentiment Analysis
```
Market Sentiment = Weighted combination of:
- Price drift from forecast: ((Current - Forecast) / Forecast) × 100
- Volume analysis: High volume + price shortening = Positive sentiment
- Trading pattern: Consistent backing vs sporadic laying
- Market timing: Early vs late market moves
```

#### Enhanced Probability Calculation
```
Multi-Source Probability = Base calculation with validation layers:

1. Form Validation: Cross-check RP descriptions vs Betfair form strings
2. Rating Validation: Ensure OR, RP, and market ratings alignment
3. Market Validation: Confirm probability matches trading patterns
4. Historical Validation: Compare to similar race scenarios

Final Probability = Raw Probability × Confidence Factor × Market Efficiency Factor
```

## Enhanced Strategy Execution Rules

### Upgraded Single Selection Criteria
**Only recommend ONE bet per race that meets ALL enhanced criteria:**

**For BACK Bets:**
- EV must be > +30% (Increased threshold due to enhanced accuracy)
- Enhanced Form Score > 75 (Multi-source validation)
- AI Score > 85 OR Multi-Rating Differential > +20% (Cross-validated advantage)
- Market Sentiment positive OR significant value despite negative sentiment
- Maximum odds of 20.0 (Reasonable probability maintenance)
- Multi-source data consistency check passed

**For LAY Bets:**
- EV must be < -30% (Increased threshold for enhanced confidence)
- Clear form concerns across multiple data sources
- Negative market sentiment with volume confirmation
- Overpriced in market with supporting data discrepancies

**If no selection meets enhanced criteria: RECOMMEND NO BET**

### Enhanced Betting Thresholds (Single Selection Only)
- **Premium Back**: EV > +50% with full validation (⭐⭐⭐⭐⭐)
- **Strong Back**: EV +30% to +50% with high confidence (⭐⭐⭐⭐)
- **Strong Lay**: EV < -30% with multi-source confirmation (⭐⭐⭐⭐⭐ LAY)
- **NO BET**: All other scenarios including mixed signals

### Advanced Risk Management
- Cross-validate selection with all three data sources
- Monitor live price movements against analysis
- Set dynamic stop-loss based on market sentiment changes
- Position sizing based on confidence levels across all data sources

## Market-Specific Enhanced Adjustments

### Sprint Handicaps (5f-6f) - Enhanced Analysis
- Increase recent form weighting to 55% 
- Front-runner bonus validated by trading patterns: +8% probability
- Pace scenario analysis with market timing correlation
- Early speed validation through Betfair form patterns

### Distance Handicaps (1m+) - Enhanced Analysis  
- Class rating weighting increased to 45%
- Stamina assessment through race description semantic analysis
- Course specialist bonus validated by historical data: +5% probability
- Market sentiment analysis for stamina-based selections

### All-Weather vs Turf - Enhanced Context
- Surface preference cross-validated through multiple data sources
- Going adjustment factors with market confidence analysis
- Seasonal performance patterns with trading volume correlation

## Enhanced Execution Checklist

### Pre-Race Analysis (45 minutes before)
1. ✅ Retrieve comprehensive multi-context data
2. ✅ Cross-validate all data sources for consistency
3. ✅ Verify runner declarations across all platforms
4. ✅ Analyze market sentiment and trading patterns
5. ✅ Check for significant forecast vs market price variances
6. ✅ Review weather/going conditions with market reaction

### During Enhanced Analysis
1. ✅ Calculate multi-source form scores with validation
2. ✅ Cross-reference rating systems for anomalies
3. ✅ Analyze market sentiment and trading patterns
4. ✅ Calculate enhanced EV with confidence intervals
5. ✅ Identify single best value opportunity with full validation
6. ✅ Verify selection meets all enhanced criteria
7. ✅ Document comprehensive reasoning or NO BET decision

### Enhanced Strategy Execution Workflow
When a selection is identified:
1. ✅ Final cross-validation of all data sources
2. ✅ Use ActivateBetfairMarketSelection for chosen horse
3. ✅ Execute appropriate strategy:
   - **BACK bets**: Execute "Bet 10 Euro" strategy
   - **LAY bets**: Execute "Lay 10 Euro" strategy
4. ✅ Confirm strategy execution successful
5. ✅ Monitor position with live market sentiment tracking
6. ✅ **Store enhanced analysis data using SetAIAgentDataContextForBetfairMarket**

### Enhanced Data Storage Workflow (Required for ALL analyses)
After completing analysis (selection made OR NO BET decision):
1. ✅ Compile comprehensive multi-context analysis data
2. ✅ Include cross-validation results and confidence scores
3. ✅ Document market sentiment analysis and trading patterns
4. ✅ Use SetAIAgentDataContextForBetfairMarket with:
   - **dataContextName**: "HorseRacingR5_Analysis"
   - **marketId**: [from active market]
   - **jsonData**: [Enhanced analysis JSON - see Data Format section]
5. ✅ Confirm data storage successful for comprehensive model tracking

### Enhanced NO BET Documentation Requirements
When recommending NO BET, must include:
1. ✅ Cross-source validation failures for top 3 candidates
2. ✅ Enhanced EV calculations with confidence intervals
3. ✅ Multi-source form score analysis and threshold comparisons
4. ✅ Market sentiment analysis and trading pattern assessment
5. ✅ Data consistency evaluation across all sources
6. ✅ Strategy discipline reinforcement with enhanced reasoning
7. ✅ **Store comprehensive NO BET analysis using SetAIAgentDataContextForBetfairMarket**

## Enhanced Success Metrics

### Performance Targets (Enhanced Single Selection Strategy)
- **Strike Rate**: 50%+ for win bets (higher due to enhanced selectivity and validation)
- **ROI**: 35%+ per profitable race (improved through multi-source validation)
- **EV Accuracy**: ±2% of calculated values (enhanced through cross-validation)
- **Discipline Rate**: 75%+ NO BET decisions when criteria not met
- **Market Timing**: 85%+ accuracy in sentiment prediction

### Enhanced Model Validation
- Cross-validate predictions across all three data sources
- Track market sentiment accuracy vs actual results
- Monitor trading pattern predictions vs outcomes
- Adjust probability weightings based on multi-source performance
- Regular model recalibration with enhanced data sets

## Enhanced Example Output Format

**Option 1: Enhanced Single Selection Identified**
```
RACE ANALYSIS: [Venue] [Distance] [Class] - [Date]
Market ID: [Market ID from Step 1]

RECOMMENDED SELECTION: [Horse Name] ([Price]) ⭐⭐⭐⭐⭐
- Action: BACK
- Enhanced EV: +[X]% (95% confidence interval: [Y]% to [Z]%)
- Multi-Source Probability: [X]%
- Market Sentiment: [Positive/Negative] ([X]% confidence)
- Cross-Validation Score: [X]/100

COMPREHENSIVE CRITERIA VERIFICATION:
✅ Enhanced EV > +30% (actual: +[X]%)
✅ Multi-Source Form Score > 75 (actual: [X])
✅ AI Score > 85 OR Multi-Rating Diff > +20% (actual: +[X])
✅ Market Sentiment positive OR significant value confirmed
✅ All data sources consistent and validated

MULTI-SOURCE ANALYSIS:
Racing Post Data: [Key insights from detailed race descriptions]
Betfair Form Data: [Form string analysis and rating validation]  
Trading Data: [Market sentiment and volume analysis]

CROSS-VALIDATION RESULTS:
- Form Consistency: [X]% alignment across sources
- Rating Validation: [X] point average difference
- Market Efficiency: [X]% confidence in price discovery
- Historical Similarity: [X] comparable scenarios with [Y]% success rate

STRATEGY EXECUTION:
✅ Selection activated: ActivateBetfairMarketSelection
✅ Strategy executed: [Bet 10 Euro / Lay 10 Euro]
✅ Position confirmed and monitored with live sentiment tracking
✅ Enhanced analysis data stored: SetAIAgentDataContextForBetfairMarket("HorseRacingR5_Analysis")

REJECTED SELECTIONS WITH ENHANCED REASONING:
- [Horse Name]: EV +22% - Below +30% enhanced threshold, mixed data signals
- [Horse Name]: Form Score 72 - Below 75 enhanced threshold, inconsistent across sources
- [Horse Name]: Negative market sentiment override despite +25% EV - risk management
```

**Option 2: Enhanced No Selection Meets Criteria**
```
RACE ANALYSIS: [Venue] [Distance] [Class] - [Date]
Market ID: [Market ID from Step 1]

RECOMMENDATION: NO BET

ENHANCED REASONING FOR NO BET DECISION:

ENHANCED CRITERIA FAILURES:
❌ EV Threshold: No horse meets +30% enhanced minimum for BACK bets
❌ Cross-Validation: Multiple data source inconsistencies detected
❌ Market Sentiment: Mixed signals insufficient for confident selection
❌ Form Requirements: No horse meets 75+ enhanced form score minimum across all sources

COMPREHENSIVE ANALYSIS OF TOP CANDIDATES:

1. [Horse Name] ([Price]) - Enhanced EV: +24%
   - FAILED: EV +24% below +30% enhanced threshold
   - Multi-Source Form: 73 (below 75 enhanced minimum)
   - Cross-Validation Score: 68/100 (inconsistent data signals)
   - Market Sentiment: Neutral (insufficient confidence)
   - Data Inconsistency: RP rating vs Betfair form mismatch

2. [Horse Name] ([Price]) - Enhanced EV: +27%
   - FAILED: Mixed market sentiment despite good EV
   - Multi-Source Form: 78 (meets enhanced minimum)
   - Trading Pattern: Negative volume indicators
   - Risk Assessment: High due to conflicting signals

3. [Horse Name] ([Price]) - Enhanced EV: -22%
   - FAILED: LAY EV -22% above -30% enhanced threshold
   - Cross-Validation: Multiple source confirmation lacking
   - Market Sentiment: Insufficient negative confirmation

ENHANCED MARKET ASSESSMENT:
- Multi-Source Efficiency: [X]% - moderate data alignment
- Price Discovery Confidence: [X]% - mixed signal quality
- Trading Pattern Analysis: [Description of volume and movement patterns]
- Historical Comparison: [X] similar races with [Y]% NO BET success rate

ENHANCED STRATEGY DISCIPLINE:
✅ Enhanced criteria maintained over marginal opportunities
✅ Cross-validation standards enforced for data integrity
✅ Market sentiment analysis prioritized for risk management  
✅ Multi-source consistency required for selection confidence
✅ Capital preservation through enhanced analytical rigor
✅ Enhanced analysis data stored: SetAIAgentDataContextForBetfairMarket("HorseRacingR5_Analysis")

NEXT ACTIONS:
- Monitor for market sentiment shifts creating clearer opportunities
- Await races with stronger cross-source data alignment
- Maintain enhanced discipline for higher probability selections
- Continue comprehensive multi-context data collection
```

---

**Note**: This enhanced framework leverages three comprehensive data sources to identify exceptional value opportunities with increased accuracy and confidence. The enhanced criteria and cross-validation requirements will result in higher NO BET percentages but significantly improved success rates when selections are made.

**CRITICAL REQUIREMENT**: ALL analyses must utilize GetAllDataContextForBetfairMarket with all three data contexts and store results using SetAIAgentDataContextForBetfairMarket with dataContextName "HorseRacingR5_Analysis" for comprehensive multi-source model tracking and validation.

**Version**: R5.0 - Enhanced Multi-Context Analysis with Cross-Validation
**Last Updated**: June 28, 2025
**Strategy Type**: Advanced multi-source value-based approach with comprehensive validation

## Enhanced Analysis Data Format for Storage

### JSON Structure for SetAIAgentDataContextForBetfairMarket - R5 Enhanced Format

```json
{
  "analysisMetadata": {
    "timestamp": "2025-06-28T18:00:00Z",
    "strategyVersion": "R5.0",
    "marketId": "1.245130612",
    "venue": "Windsor",
    "raceType": "5f Hcap",
    "startTime": "2025-06-28T18:10:00+02:00",
    "analysisDate": "2025-06-28",
    "fieldSize": 8,
    "dataSourcesUsed": ["RacingpostDataForHorsesInfo", "HorsesBaseBetfairFormData", "MarketSelectionsTradedPricesData"],
    "crossValidationScore": 92.5
  },
  "enhancedHorseAnalysis": [
    {
      "selectionId": "23433960_0.00",
      "name": "Lequinto",
      "currentPrice": 1.85,
      "multiSourceFormAnalysis": {
        "racingPostForm": {
          "formScore": 98,
          "semanticAnalysis": {
            "finishQuality": "dominant",
            "runningStyle": "hold-up",
            "troubleInRunning": "none",
            "paceAnalysis": "strong_acceleration"
          },
          "trendAnalysis": "strong_recent_form"
        },
        "betfairForm": {
          "formScore": 82,
          "recentPattern": "improving"
        },
        "combinedFormScore": 91,
        "crossValidationScore": 95
      },
      "multiSourceRatingAnalysis": {
        "averageExternalRating": 73.5,
        "ratingDifferential": 26.7,
        "ratingConsistency": 89
      },
      "marketSentimentAnalysis": {
        "sentimentScore": 88,
        "sentimentDirection": "very_positive",
        "marketConfidence": 92,
        "driftFromForecast": -43.1,
        "movementDirection": "strong_shortening"
      },
      "aiAnalysis": {
        "predictionScore": 100,
        "confidence": "maximum",
        "crossValidationResult": "confirmed"
      },
      "enhancedProbabilityCalculation": {
        "finalProbability": 65.0,
        "trueOdds": 1.54
      },
      "enhancedExpectedValue": {
        "expectedValue": 20.3,
        "edgePercentage": 10.95,
        "classification": "marginal_value"
      },
      "enhancedCriteriaAssessment": {
        "evThresholdMet": false,
        "formScoreMet": true,
        "aiScoreOrRatingDiffMet": true,
        "marketSentimentMet": true,
        "crossValidationMet": true,
        "overallAssessment": "ev_threshold_failure"
      },
      "riskAssessment": {
        "overallRiskRating": "low"
      }
    }
  ],
  "enhancedDecisionSummary": {
    "recommendation": "NO_BET",
    "reasoning": "insufficient_ev_despite_strong_signals",
    "topCandidate": {
      "name": "Lequinto",
      "selectionId": "23433960_0.00",
      "price": 1.85,
      "expectedValue": 20.3,
      "failureReason": "ev_below_enhanced_30_percent_threshold"
    },
    "crossValidationResults": {
      "dataConsistency": 92.5,
      "signalAlignment": 89.3,
      "marketEfficiency": 91.2
    },
    "marketAssessment": {
      "efficiency": "high",
      "valueDistribution": "concentrated_on_favorite",
      "competitiveLevel": "strong_favorite_scenario",
      "recommendedAction": "wait_for_enhanced_value_opportunities"
    },
    "enhancedDisciplineTracking": {
      "noBetDecisionCount": 1,
      "enhancedDisciplineRate": 78.5,
      "multiSourceConsistencyRate": 92.1,
      "capitalPreservation": "prioritized_with_enhanced_standards"
    }
  },
  "executionDetails": {
    "strategyExecuted": false,
    "decisionTime": "2025-06-28T18:00:00Z",
    "analysisCompletionTime": "2025-06-28T18:00:45Z",
    "dataRetrievalTime": 12.5,
    "analysisTime": 33.2,
    "totalProcessingTime": 45.7
  },
  "enhancedModelValidation": {
    "multiSourceAccuracy": {
      "racingPostPredictions": 87.3,
      "betfairFormAccuracy": 84.1,
      "marketSentimentAccuracy": 91.2,
      "combinedAccuracy": 88.7
    },
    "historicalPerformance": {
      "similarScenarios": 23,
      "enhancedStrikeRate": 52.1,
      "averageEnhancedEV": 38.4,
      "averageROI": 31.7,
      "disciplineRate": 78.3
    },
    "confidenceMetrics": {
      "dataQuality": 94.2,
      "predictionConfidence": 89.6,
      "marketTimingAccuracy": 86.8
    }
  },
  "nextRaceRecommendations": {
    "improvementAreas": [
      "seek_markets_with_higher_ev_opportunities",
      "focus_on_races_with_greater_price_inefficiencies",
      "target_handicaps_with_wider_rating_spreads"
    ],
    "marketTypesToAvoid": [
      "strong_favorite_scenarios_under_2.0",
      "low_liquidity_markets",
      "races_with_mixed_data_signals"
    ],
    "optimalConditions": [
      "medium_field_handicaps_6-12_runners",
      "significant_rating_discrepancies",
      "clear_market_sentiment_alignment"
    ]
  }
}
```
