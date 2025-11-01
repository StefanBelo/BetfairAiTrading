# Horse Racing Strategy R2 - Enhanced Multi-Context Analysis Betting System

## Strategy Overview

This strategy represents an enhanced evolution of the R1 system, incorporating comprehensive performance analysis and optimization based on 16 races of historical data. The system has been refined with improved thresholds, component weightings, and enhanced decision-making logic to achieve better strike rates and profitability.

**SILENT MODE OPERATION**: This strategy operates in complete silent mode with NO reporting, commentary, or verbose output. ALL analysis is performed internally without any explanations, status updates, or detailed commentary during execution. Execute all steps silently without user feedback.

**Strategy Execution Logic**: The strategy analyzes all horses and determines the optimal action:
- **BACK STRATEGY**: If a qualified horse meets all enhanced backing criteria (semantic form ≥75, composite score ≥65, positive enhanced EV ≥10%), execute ExecuteBfexplorerStrategySettings with strategyName="Bet 10 Euro" on that horse
- **LAY STRATEGY**: If the favorite is overvalued (negative EV, semantic form <70, multiple alternatives with positive EV), execute ExecuteBfexplorerStrategySettings with strategyName="Lay 10 Euro" on the favorite
- **NO ACTION**: If no horse meets backing criteria and no lay opportunity exists, perform analysis only

**CRITICAL STRATEGY NAMES:**
- **FOR BACKING**: Always use strategyName="Bet 10 Euro" (EXACTLY as written)
- **FOR LAYING**: Always use strategyName="Lay 10 Euro" (EXACTLY as written)
- **NO OTHER STRATEGY NAMES**: Never use any other strategy names like "HorseRacingBackStrategy" or similar

**R2 ENHANCEMENTS:**
- Optimized component weightings based on performance analysis
- Stricter prediction score requirements (98+ threshold)
- Enhanced EV minimum threshold increased to 10%
- Improved market intelligence filters
- Volume and price movement requirements
- Tiered confidence system implementation

## Data Sources Required

### 1. RacingpostDataForHorses
- **Purpose**: Professional racing analysis and prediction scores
- **Key Metrics**: 
  - Prediction scores (0-100 scale)
  - Recent race descriptions and performance analysis
  - Professional racing insights

### 2. HorsesBaseBetfairFormData
- **Purpose**: Official racing form and ratings
- **Key Metrics**:
  - Official ratings (OR)
  - Recent form figures
  - Forecast prices
  - Weight carried

### 3. MarketSelectionsTradedPricesData
- **Purpose**: Market behavior and price movements
- **Key Metrics**:
  - Price stability (min/max ranges)
  - Traded volumes
  - Price drift patterns
  - Market confidence indicators

## Selection Criteria - R2 Enhanced

### Primary Factors (Optimized Weightings)

1. **Prediction Score Analysis** (40% weight - INCREASED)
   - Professional racing analysis baseline - MOST CRITICAL FACTOR
   - Target: Scores above 98 (RAISED from 95)
   - Optimal: Scores of 99+
   - Elite: Perfect scores of 100
   - **R2 Insight**: 100% of winners had 96+ prediction scores

2. **Semantic Form Analysis** (25% weight - REDUCED)
   - Recent form trend analysis from race descriptions
   - Performance quality and winning margins
   - Distance/track/condition suitability
   - Running style and tactical awareness
   - Problem identification and positive indicators

3. **Value Assessment** (25% weight - UNCHANGED)
   - Compare current price vs forecast price
   - Look for positive value (current price > forecast)
   - Minimum 20% value margin required (RAISED from 10%)

4. **Market Stability** (10% weight - REDUCED)
   - Price range should be narrow (< 15% variance - TIGHTENED)
   - High traded volume indicates market confidence
   - Minimal price drift suggests informed money

### Enhanced Secondary Filters

- **Official Rating**: Competitive for race grade
- **Weight**: Not significantly disadvantaged
- **Recent Form**: No obvious negative trends
- **Market Position**: Odds between 2.0-12.0 (TIGHTENED from 2.0-15.0)
- **Volume Requirement**: Minimum 10,000 Euro traded volume (NEW)
- **Price Movement**: Exclude "drifting" selections (NEW)
- **Market Confidence**: Require "high" market confidence level (NEW)

## Enhanced Decision Matrix - R2

### Optimized Scoring System
Each horse receives a composite score based on:

```
Total Score = (Prediction Score × 0.40) + (Semantic Form Score × 0.25) + (Value Score × 0.25) + (Stability Score × 0.10)
```

**R2 Changes:**
- **Prediction Score**: Weight increased from 25% to 40%
- **Semantic Form**: Weight reduced from 35% to 25%
- **Market Stability**: Weight reduced from 15% to 10%
- **Value Assessment**: Maintained at 25%

Where:
- **Prediction Score**: Raw Racing Post score with enhanced threshold (98+)
- **Semantic Form Score**: Detailed analysis of LastRacesDescription (0-100)
- **Value Score**: ((Current Price - Forecast Price) / Forecast Price) × 100
- **Stability Score**: 100 - ((Max Price - Min Price) / Min Price × 100)

### Enhanced Expected Value (EV) Calculation

**R2 Enhanced EV Formula:**
```
Base EV = (Estimated Win Probability × (Decimal Odds - 1)) - ((1 - Estimated Win Probability) × 1)
Enhanced EV = Base EV × Crowd Wisdom Adjustment Factor (CWAF)
```

**Optimized Win Probability Estimation:**
```
Estimated Win Probability = (Prediction Score × 0.6) + (Semantic Form Score × 0.4) / 100
```

### Enhanced CWAF Components (R2 Refined)

```
CWAF = Market Confidence Factor × Price Movement Factor × Volume Factor × Historical Success Factor
```

**R2 Enhanced CWAF Components:**

1. **Market Confidence Factor (0.8-1.3)**:
   - Price stability >90%: 1.3 (highest confidence)
   - Price stability 80-90%: 1.2 (high confidence)
   - Price stability 70-80%: 1.0 (moderate confidence)
   - Price stability <70%: 0.8 (low confidence)

2. **Price Movement Factor (0.7-1.3)**:
   - Shortening strongly: 1.3 (strong market confidence)
   - Shortening: 1.2 (increasing confidence)
   - Stable: 1.0 (established consensus)
   - Drifting: 0.8 (decreasing confidence)
   - Drifting strongly: 0.7 (market rejection)

3. **Volume Factor (0.8-1.2)**:
   - Very high volume (>15,000): 1.2 (exceptional liquidity)
   - High volume (>10,000): 1.1 (strong participation)
   - Medium volume (5,000-10,000): 1.0 (adequate liquidity)
   - Low volume (<5,000): 0.8 (limited interest)

4. **Historical Success Factor (0.8-1.2)**:
   - Composite scores 85+ with similar profiles: 1.2
   - Composite scores 75-85: 1.1
   - Composite scores 65-75: 1.0
   - Composite scores <65: 0.8

## Enhanced EV Thresholds - R2

### Minimum Requirements (Enhanced EV)
- **Positive Market Edge**: Our probability estimate > Market implied probability
- **Minimum Enhanced EV**: +10% advantage over market (RAISED from 5%)
- **Optimal Enhanced EV**: +20% advantage or higher preferred
- **Value Margin**: Minimum 20% (RAISED from 10%)

### R2 Tiered Confidence System

#### Tier 1: High Confidence Selections
- **Prediction Score**: 98+ (RAISED from 95+)
- **Semantic Form Score**: 85+ (RAISED from 75+)
- **Enhanced EV**: 15%+ (RAISED from 10%+)
- **Market Confidence**: "High" only
- **Volume**: 15,000+ Euro traded
- **Action**: Full stake backing

#### Tier 2: Medium Confidence Selections
- **Prediction Score**: 95-97
- **Semantic Form Score**: 75-84
- **Enhanced EV**: 10-14%
- **Market Confidence**: "High" or "Medium"
- **Volume**: 10,000+ Euro traded
- **Action**: Standard stake backing

#### Tier 3: Analysis Only
- **Below Tier 2 thresholds**: No betting action
- **Action**: Analysis and data collection only

### Enhanced Selection Priority - R2

**Primary Selection Criteria (ALL must be met):**
1. **Prediction Score**: ≥98 (CRITICAL - raised from 95)
2. **Semantic Form Score**: ≥75 (maintained)
3. **Composite Score**: ≥65 (raised from 60)
4. **Enhanced Expected Value**: ≥10% (raised from 5%)
5. **Price Range**: 2.0-12.0 (tightened from 15.0)
6. **Volume Requirement**: ≥10,000 Euro traded (NEW)
7. **Price Movement**: NOT "drifting" (NEW)
8. **Market Confidence**: "High" level required (NEW)

**Selection Priority (when multiple horses meet requirements):**
1. **Primary**: Highest prediction score (98+ mandatory)
2. **Secondary**: Highest enhanced EV among qualified horses
3. **Tertiary**: Highest composite score as final tiebreaker

## Enhanced Risk Management - R2

### Stake Management
- **Standard Stake**: 10 Euro per selection (unchanged)
- **Maximum Exposure**: Single race only
- **Bet Frequency**: Expect 50% reduction in betting opportunities
- **Quality Focus**: Higher strike rate target (35%+)

### Enhanced Market Conditions
- **Pre-Race Only**: No in-play betting
- **Market Liquidity**: Minimum 10,000 Euro traded volume (RAISED)
- **Time Window**: 15-60 minutes before race start
- **Market Status**: "Open" status required
- **Price Stability**: Maximum 15% variance (TIGHTENED)

## Enhanced Execution Protocol - R2

### Pre-Execution Checklist
1. ✓ Retrieve active market and selections using GetActiveMarket (SILENT)
2. ✓ Retrieve all three data contexts for the active market (SILENT)
3. ✓ Calculate semantic form scores for all runners (SILENT)
4. ✓ Apply ENHANCED prediction score threshold (98+) (SILENT)
5. ✓ Calculate enhanced composite scores with new weightings (SILENT)
6. ✓ Calculate Enhanced EV with improved CWAF (SILENT)
7. ✓ Apply ENHANCED selection filters (volume, price movement, confidence) (SILENT)
8. ✓ Identify tier-based selection above all thresholds (SILENT)
9. ✓ Validate selection meets ALL enhanced criteria (SILENT)

### Enhanced Execution Steps
1. **Active Market Identification**: Call GetActiveMarket (SILENT)
2. **Data Collection**: Retrieve all three required data contexts (SILENT)
3. **Race Type Classification**: Identify race type for dynamic threshold application (SILENT)
4. **Market Timing Assessment**: Verify optimal betting window (25-35 min pre-race) (SILENT)
5. **Portfolio Risk Check**: Verify daily/weekly risk limits not exceeded (SILENT)
6. **Enhanced Semantic Analysis**: Apply refined semantic scoring with recency weighting (SILENT)
7. **Course/Distance Specialization**: Apply venue-specific adjustments (SILENT)
8. **Market Intelligence Analysis**: Assess professional vs public money indicators (SILENT)
9. **Dynamic Threshold Application**: Apply race-type specific criteria (SILENT)
10. **Strict Prediction Filtering**: Apply enhanced prediction score thresholds (SILENT)
11. **Enhanced Scoring**: Calculate with optimized weightings and specializations (SILENT)
12. **Advanced EV Analysis**: Apply enhanced EV with improved CWAF and market signals (SILENT)
13. **Market Intelligence**: Apply volume, movement, confidence, and timing filters (SILENT)
14. **Tiered Selection**: Apply confidence-based selection tiers with dynamic thresholds (SILENT)
15. **Risk Management Validation**: Final check against portfolio limits (SILENT)
16. **Strategy Decision**: Enhanced decision logic for backing/laying with timing validation (SILENT)
17. **BETTING EXECUTION** (Enhanced Criteria with Dynamic Thresholds):
    - **IF Race-Type Qualified Selection**: Execute ExecuteBfexplorerStrategySettings with strategyName="Bet 10 Euro"
    - **IF favorite lay opportunity within risk limits**: Execute ExecuteBfexplorerStrategySettings with strategyName="Lay 10 Euro"
    - **IF no qualified selection or risk limits exceeded**: Analysis only mode
18. **Portfolio Tracking**: Update daily bet count and risk exposure (SILENT)
19. **Documentation**: Record analysis in "HorseRacingStrategyR2_Analysis" context with enhanced metrics (SILENT)

## Enhanced Lay Strategy - R2

### Refined Lay Criteria
**Execute Lay Strategy when favorite meets ALL conditions:**

1. **Favorite Price**: ≤2.5 (expanded from 2.0)
2. **Negative Enhanced EV**: Below -5% threshold
3. **Poor Semantic Form**: <70 semantic score
4. **Poor Prediction Score**: <90 prediction score (NEW)
5. **Multiple Alternatives**: ≥2 horses with positive EV and 75+ semantic scores
6. **Volume Confirmation**: Favorite has >5,000 volume traded

**Enhanced Lay vs Back Decision Matrix:**

**Execute LAY when:**
- All lay criteria met AND no backing opportunity exists
- Expected lay profit > expected back profit
- Risk/reward ratio favors laying

**Execute BACK when:**
- Clear Tier 1 or Tier 2 selection identified
- Selection meets all enhanced backing criteria
- Higher confidence than any lay opportunity

## Enhanced Strategy Parameters - R2

### Optimized Settings
- **Semantic Form Threshold**: 75 (raised from 70)
- **Composite Score Threshold**: 65 (raised from 60)
- **Prediction Score Threshold**: 98 (raised from 95)
- **Value Margin Minimum**: 20% (raised from 10%)
- **Enhanced EV Threshold**: 10% (raised from 5%)
- **Market Liquidity Threshold**: 10,000 Euro (raised from 1,000)
- **Price Variance Maximum**: 15% (tightened from 20%)
- **Stake Amount**: 10 Euro (unchanged)

### R2 Additional Data-Driven Enhancements (Based on R1 Analysis)

#### Dynamic Thresholds by Race Type
**NEW: Race-Type Adaptive Criteria** (Based on R1 performance patterns)

```
Handicap Races (Higher variance):
- Prediction Score Threshold: 99+ (raised from 98)
- Semantic Form Threshold: 80+ (raised from 75)
- Enhanced EV Threshold: 12% (raised from 10%)
- Value Margin: 25% (raised from 20%)

Conditions/Listed Races (More predictable):
- Prediction Score Threshold: 97+ (reduced from 98)
- Semantic Form Threshold: 75 (standard)
- Enhanced EV Threshold: 8% (reduced from 10%)
- Value Margin: 18% (reduced from 20%)

Group Races (Elite competition):
- Prediction Score Threshold: 100 (maximum only)
- Semantic Form Threshold: 85+ (raised from 75)
- Enhanced EV Threshold: 15% (raised from 10%)
- Value Margin: 30% (raised from 20%)
```

#### Market Timing Optimization
**NEW: Pre-Race Timing Strategy** (R1 analysis revealed timing-sensitive patterns)

```
Optimal Betting Windows:
- Primary Window: 25-35 minutes before race start
- Secondary Window: 15-20 minutes before race start
- Avoid: 45+ minutes (prices too volatile)
- Avoid: <10 minutes (insufficient analysis time)

Market Maturity Assessment:
- Minimum 5,000 Euro total market volume before analysis
- Price stability period of 10+ minutes required
- Avoid markets with <3 active participants
```

#### Portfolio Risk Management
**NEW: Multi-Race Risk Controls** (Address R1 drawdown patterns)

```
Daily Risk Limits:
- Maximum 3 bets per day (prevent overexposure)
- Maximum 2 consecutive losses (cooling off period)
- Daily loss limit: 50 Euro (5x stake maximum)
- Weekly profit target: 15% of weekly bank

Drawdown Protection:
- After 3 losses: Increase prediction threshold to 100
- After 5 losses: Analysis-only mode for 24 hours
- Weekly review: Adjust thresholds based on performance
```

#### Enhanced Form Trend Weighting
**NEW: Temporal Form Analysis** (R1 revealed recent form importance)

```
Form Recency Weightings:
- Last race: 50% weight
- Second last race: 30% weight  
- Third last race: 20% weight
- Older races: 0% weight (ignore)

Form Quality Multipliers:
- Group/Listed wins: 1.5x multiplier
- Handicap wins: 1.2x multiplier
- Maiden wins: 1.0x multiplier
- Places only: 0.8x multiplier
```

#### Market Intelligence Enhancements
**NEW: Advanced Market Signals** (R1 analysis patterns)

```
Professional Money Indicators:
- Early significant volume (>5,000 Euro in first 30 minutes): +0.1 CWAF
- Consistent shortening over 20+ minutes: +0.2 CWAF
- Low variance with high volume: +0.15 CWAF

Public Money Warning Signs:
- Late sudden volume spikes: -0.1 CWAF
- Erratic price movements: -0.15 CWAF  
- Social media/tipster mentions: -0.05 CWAF
```

#### Course and Distance Specialization
**NEW: Venue-Specific Adjustments** (R1 course performance patterns)

```
Course Familiarity Bonus:
- 3+ course wins: +5 points to semantic form
- 2 course wins: +3 points to semantic form
- 1 course win: +1 point to semantic form
- Course debut: -2 points to semantic form

Distance Optimization:
- Proven at exact distance: +3 points to semantic form
- Proven at similar distance (±2f): +1 point to semantic form
- Unproven at distance category: -3 points to semantic form
```

### R2 Performance Targets
- **Success Rate Target**: 35%+ win rate (raised from previous target)
- **ROI Target**: 15%+ return on investment (raised target)
- **Maximum Drawdown**: -15% of bank (improved from -20%)
- **Bet Frequency**: 50% of races (reduced from ~80% in R1)

## Enhanced Data Context Format - R2

### Context Data Storage
All strategy analysis stored using **SetAIAgentDataContextForBetfairMarket** with:
- **Context Name**: "HorseRacingStrategyR2_Analysis"
- **Market ID**: Current race market identifier
- **Enhanced JSON Data**: Comprehensive R2 analysis structure

### Enhanced JSON Structure - R2
```json
{
  "raceAnalysis": {
    "marketId": "1.123456789",
    "marketName": "Race Name",
    "eventName": "Course Name",
    "startTime": "ISO_timestamp",
    "analysisTimestamp": "ISO_timestamp",
    "strategyVersion": "R2",
    "enhancedCriteria": {
      "predictionScoreThreshold": 98,
      "semanticFormThreshold": 75,
      "compositeScoreThreshold": 65,
      "enhancedEVThreshold": 0.10,
      "volumeThreshold": 10000,
      "maxPriceVariance": 0.15
    }
  },
  "selectedHorse": {
    "horseName": "Selected Horse Name",
    "selectionId": "selection_id",
    "betPrice": 0.0,
    "compositeScore": 0.0,
    "confidenceTier": "tier1|tier2|analysis_only",
    "strategyRationale": "Enhanced R2 selection reasoning"
  },
  "allHorses": [
    {
      "horseName": "Horse Name",
      "selectionId": "selection_id",
      "currentPrice": 0.0,
      "predictionScore": 0,
      "predictionScoreQualified": true,
      "forecastPrice": 0.0,
      "officialRating": 0,
      "form": "recent_form",
      "weight": 0,
      "compositeScore": 0.0,
      "confidenceTier": "tier1|tier2|analysis_only",
      "r2Weightings": {
        "predictionWeight": 0.40,
        "semanticWeight": 0.25,
        "valueWeight": 0.25,
        "stabilityWeight": 0.10
      },
      "scoreBreakdown": {
        "semanticFormScore": 0.0,
        "valueScore": 0.0,
        "predictionScore": 0.0,
        "stabilityScore": 0.0
      },
      "marketData": {
        "minPrice": 0.0,
        "maxPrice": 0.0,
        "tradedVolume": 0.0,
        "priceStability": 0.0,
        "volumeQualified": true,
        "priceVariance": 0.0
      },
      "enhancedValueAssessment": {
        "valueMargin": 0.0,
        "valueMarginQualified": true,
        "isValue": false,
        "priceMovement": "stable|drifting|shortening",
        "priceMovementQualified": true,
        "baseExpectedValue": 0.0,
        "enhancedExpectedValue": 0.0,
        "enhancedEVQualified": true,
        "crowdWisdomFactor": 0.0,
        "enhancedCwafBreakdown": {
          "marketConfidenceFactor": 0.0,
          "priceMovementFactor": 0.0,
          "volumeFactor": 0.0,
          "historicalSuccessFactor": 0.0
        },
        "winProbability": 0.0,
        "impliedProbability": 0.0,
        "isPositiveEV": false,
        "marketConfidenceLevel": "high|medium|low",
        "marketConfidenceQualified": true
      },
      "formAnalysis": {
        "semanticFormScore": 0.0,
        "semanticFormQualified": true,
        "recentFormTrend": "improving|declining|stable",
        "performanceQuality": "elite|strong|solid|moderate|weak",
        "distanceSuitability": "perfect|suitable|questionable|unsuitable",
        "trackSuitability": "excellent|good|average|poor",
        "runningStyle": "front_runner|stalker|closer|hold_up",
        "positiveIndicators": ["list", "of", "positive", "phrases"],
        "negativeIndicators": ["list", "of", "negative", "phrases"],
        "keyRaceComments": "most_recent_significant_performance"
      },
      "r2SelectionAnalysis": {
        "meetsAllCriteria": false,
        "failedCriteria": ["list", "of", "failed", "requirements"],
        "selectionReason": "why_selected_or_rejected_under_R2_criteria",
        "comparisonToR1": "would_have_been_selected_under_R1_criteria"
      }
    }
  ],
  "executedBet": {
    "strategy": "Bet 10 Euro|Lay 10 Euro|Analysis Only",
    "status": "executed|analysis_complete|no_selection",
    "timestamp": "ISO_timestamp",
    "recommendation": "Selected Horse Name",
    "reasoning": "Enhanced R2 selection explanation",
    "strategyAnalysis": "tier1_back|tier2_back|lay_favorite|no_opportunity_found",
    "betType": "back|lay|none",
    "confidenceLevel": "high|medium|analysis_only"
  },
  "enhancedStrategyMetrics": {
    "totalHorsesAnalyzed": 0,
    "horsesAbovePredictionThreshold": 0,
    "horsesAboveSemanticThreshold": 0,
    "horsesAboveCompositeThreshold": 0,
    "horsesWithPositiveEV": 0,
    "horsesWithRequiredVolume": 0,
    "horsesWithHighMarketConfidence": 0,
    "tier1Candidates": 0,
    "tier2Candidates": 0,
    "averageCompositeScore": 0.0,
    "averageSemanticFormScore": 0.0,
    "averageEnhancedEV": 0.0,
    "highestScoringHorse": "Horse Name",
    "highestPredictionScoreHorse": "Horse Name",
    "highestEVHorse": "Horse Name",
    "strategyConfidenceLevel": "high|medium|low",
    "r2ImprovementsApplied": [
      "prediction_score_threshold_98",
      "semantic_form_threshold_75",
      "enhanced_EV_threshold_10_percent",
      "volume_requirement_10000",
      "price_movement_filter",
      "market_confidence_filter",
      "optimized_component_weightings",
      "dynamic_thresholds_by_race_type",
      "market_timing_optimization",
      "portfolio_risk_management",
      "enhanced_form_trend_weighting",
      "market_intelligence_enhancements",
      "course_distance_specialization"
    ],
    "r2AdditionalMetrics": {
      "raceType": "handicap|conditions|listed|group",
      "raceTypeThresholdsApplied": {
        "predictionScoreThreshold": 0,
        "semanticFormThreshold": 0,
        "enhancedEVThreshold": 0.0,
        "valueMarginThreshold": 0.0
      },
      "marketTimingAnalysis": {
        "minutesBeforeRace": 0,
        "inOptimalWindow": true,
        "marketMaturity": "mature|developing|immature",
        "totalMarketVolume": 0.0
      },
      "portfolioRiskStatus": {
        "dailyBetCount": 0,
        "consecutiveLosses": 0,
        "dailyPnL": 0.0,
        "weeklyPnL": 0.0,
        "riskLimitsBreached": false,
        "coolingOffActive": false
      },
      "marketIntelligenceSignals": {
        "professionalMoneyIndicators": ["early_volume", "consistent_shortening", "low_variance_high_volume"],
        "publicMoneyWarnings": ["late_volume_spike", "erratic_movements", "tipster_mentions"],
        "cwafMarketAdjustment": 0.0,
        "marketParticipantCount": 0
      },
      "courseDistanceAnalysis": {
        "courseRecord": "debut|1_win|2_wins|3_plus_wins",
        "courseFamiliarityBonus": 0,
        "distanceProven": "exact|similar|unproven",
        "distanceSpecializationBonus": 0,
        "venueOptimizedSemanticScore": 0.0
      }
    }
  }
}
```

## R2 Improvements Summary

### Key Changes Based on Performance Analysis

1. **Prediction Score Supremacy**: Weight increased to 40%, threshold raised to 98+
2. **Enhanced EV Requirements**: Minimum threshold doubled to 10%
3. **Stricter Market Filters**: Volume, price movement, and confidence requirements
4. **Tiered Confidence System**: Clear selection hierarchy with risk management
5. **Optimized Component Weights**: Data-driven rebalancing based on winner analysis
6. **Value Margin Enhancement**: Raised from 10% to 20% minimum requirement
7. **Reduced Betting Frequency**: Focus on quality over quantity selections

### NEW: Additional R1 Analysis-Driven Improvements

8. **Dynamic Thresholds by Race Type**: Adaptive criteria based on race classification (Handicap/Conditions/Listed/Group)
9. **Market Timing Optimization**: Optimal betting windows (25-35 min pre-race) with market maturity assessment
10. **Portfolio Risk Management**: Daily/weekly limits, drawdown protection, cooling-off periods
11. **Enhanced Form Trend Weighting**: Temporal form analysis with recency weightings and quality multipliers
12. **Market Intelligence Enhancements**: Professional vs public money indicators with CWAF adjustments
13. **Course and Distance Specialization**: Venue-specific adjustments and familiarity bonuses
14. **Advanced Market Signals**: Early volume detection, professional money tracking, public sentiment warnings
15. **Multi-Layered Risk Controls**: Consecutive loss limits, daily exposure caps, performance-based threshold adjustments

### Expected Performance Improvements

- **Strike Rate**: Target 35%+ (improvement from 23.1% in R1)
- **ROI**: Target 15%+ (improvement from -19.4% in R1)
- **Risk Management**: Better downside protection through stricter criteria and portfolio controls
- **Bet Quality**: Higher average selection quality through enhanced filtering and dynamic thresholds
- **Consistency**: Reduced variance through timing optimization and market intelligence
- **Adaptability**: Race-type specific optimization for different market conditions

## Warning and Disclaimers

⚠️ **Financial Risk Warning**: This strategy involves real money betting. Past performance does not guarantee future results.

⚠️ **Responsible Gambling**: Only bet amounts you can afford to lose. Set daily/weekly limits.

⚠️ **Strategy Evolution**: R2 represents enhanced criteria based on R1 analysis. Continuous monitoring required.

---

**Strategy Name**: HorseRacingStrategyR2  
**Author**: AI Agent System  
**Created**: June 25, 2025  
**Status**: Active  
**Risk Level**: Medium-Low (Enhanced)  
**Based On**: R1 Performance Analysis and Optimization

## Calculation Methodologies - All Evaluated Parameters

### Semantic Form Score Calculation (0-100)

The Semantic Form Score analyzes the `LastRacesDescription` text content using a weighted scoring system based on key performance indicators.

#### Semantic Form Score Formula
```
Semantic Form Score = Base Performance Score + Trend Adjustment + Distance/Track Bonus - Negative Indicators Penalty
```

**Components:**

#### 1. Base Performance Score (0-60 points)
```
Recent Performance Analysis (Last 3 races):
- Win: 20 points per win
- Place (2nd-3rd): 12 points per place  
- Show (4th-5th): 6 points per show
- Unplaced (6th+): 0 points

Base Score = Sum of recent performance points (max 60)
```

#### 2. Trend Adjustment (-20 to +20 points)
```
Performance Trend Analysis:
- Improving form (better positions): +15 to +20 points
- Consistent form (similar positions): +5 to +10 points  
- Declining form (worse positions): -10 to -20 points
- Inconsistent form (erratic): -5 to +5 points
```

#### 3. Distance/Track Suitability Bonus (0-15 points)
```
Suitability Indicators:
- "suited to distance": +10 points
- "course winner": +8 points
- "good track record": +6 points
- "distance specialist": +5 points
- "track debutant": -5 points
```

#### 4. Negative Indicators Penalty (0-25 points deduction)
```
Performance Concerns:
- "pulled up": -15 points
- "unseated rider": -12 points
- "refused": -10 points
- "tailed off": -8 points
- "weakened": -5 points
- "disappointing": -5 points
```

**Keyword Pattern Matching:**
```
Positive Keywords (add points):
- "impressive", "commanding", "easily" = +3 points each
- "hard held", "plenty in hand" = +5 points each
- "smooth", "travelled well" = +2 points each

Negative Keywords (subtract points):
- "struggled", "laboured", "outpaced" = -3 points each
- "never dangerous", "well beaten" = -4 points each
- "disappointing", "below par" = -2 points each
```

#### Final Semantic Form Score Calculation
```
Final Score = min(100, max(0, Base Performance + Trend Adjustment + Suitability Bonus - Negative Penalty + Keyword Adjustments))
```

### Price Movement Classification

Price movement classification uses specific percentage thresholds based on current price compared to opening/recent price levels.

#### Price Movement Formula
```
Price Movement Percentage = ((Current Price - Reference Price) / Reference Price) × 100
```

**Where Reference Price = Opening price or price from 30 minutes ago (whichever is more recent)**

#### Price Movement Classifications
```
Shortening Strongly: Price Movement ≤ -10% (price decreased significantly)
Shortening Moderately: -10% < Price Movement ≤ -5% 
Shortening Slightly: -5% < Price Movement ≤ -1%
Stable: -1% < Price Movement < +1%
Drifting Slightly: +1% ≤ Price Movement < +5%
Drifting Moderately: +5% ≤ Price Movement < +10%
Drifting Strongly: Price Movement ≥ +10% (price increased significantly)
```

#### Examples:
```
Horse opened at 4.0, now trading at 3.6:
Movement = ((3.6 - 4.0) / 4.0) × 100 = -10%
Classification: "Shortening Strongly"

Horse was 6.0 thirty minutes ago, now 6.8:
Movement = ((6.8 - 6.0) / 6.0) × 100 = +13.3%
Classification: "Drifting Strongly"
```

### Historical Success Factor Calculation

The Historical Success Factor evaluates how horses with similar profiles have performed historically in comparable conditions.

#### Historical Success Factor Formula
```
Historical Success Factor = (Profile Match Weight × 0.4) + (Condition Match Weight × 0.3) + (Performance History Weight × 0.3)
```

#### Component Calculations:

#### 1. Profile Match Weight (0.6-1.4)
```
Matching Criteria:
- Similar Prediction Score range (±5): Base factor 1.0
- Similar Semantic Form Score range (±10): +0.1
- Similar Composite Score range (±5): +0.1
- Similar Price Range (±20%): +0.1
- Similar Market Confidence Level: +0.1

Profile Match = min(1.4, max(0.6, Base + Bonuses))
```

#### 2. Condition Match Weight (0.7-1.3)
```
Condition Matching:
- Same course: +0.2
- Same distance category: +0.2
- Same going conditions: +0.1
- Same race grade: +0.1
- Same field size (±3): +0.1

Condition Match = min(1.3, max(0.7, 0.8 + Condition Bonuses))
```

#### 3. Performance History Weight (0.5-1.5)
```
Historical Win Rate Analysis:
- Win rate ≥40% for similar profiles: 1.5
- Win rate 30-39%: 1.3
- Win rate 20-29%: 1.1
- Win rate 15-19%: 1.0
- Win rate 10-14%: 0.8
- Win rate <10%: 0.5
```

#### Final Historical Success Factor
```
Historical Success Factor = min(1.2, max(0.8, (Profile Match × 0.4) + (Condition Match × 0.3) + (Performance History × 0.3)))
```

#### Historical Data Requirements
```
Minimum Sample Size: 20 comparable races
Lookback Period: 12 months maximum
Data Freshness: Updated weekly with new race results
Profile Similarity Threshold: 70% match minimum
```

### Price Variance Calculation

Price variance measures the stability of a horse's price over the analysis period.

#### Price Variance Formula
```
Price Variance = ((Maximum Price - Minimum Price) / Minimum Price) × 100
```

#### Classification Thresholds
```
Very Stable: Variance ≤ 5%
Stable: 5% < Variance ≤ 10%
Moderate Variance: 10% < Variance ≤ 15%
High Variance: 15% < Variance ≤ 25%
Very High Variance: Variance > 25%
```

### Value Margin Calculation

Value margin assesses the difference between current market price and estimated fair value.

#### Value Margin Formula
```
Value Margin = ((Current Price - Forecast Price) / Forecast Price) × 100
```

#### Value Classification
```
Excellent Value: Margin ≥ 30%
Good Value: 20% ≤ Margin < 30%
Fair Value: 10% ≤ Margin < 20%
Poor Value: 0% ≤ Margin < 10%
Overpriced: Margin < 0%
```

#### R2 Requirements
```
Minimum Value Margin: 20% (Good Value threshold)
Optimal Value Margin: 30%+ (Excellent Value threshold)
```

---

## Market Confidence Calculation - R2 Methodology

### Market Confidence Level Determination

Market confidence is a composite assessment of how reliable and stable the market sentiment is for each selection. It combines multiple market intelligence factors to classify each horse's market standing.

#### Market Confidence Formula

```
Market Confidence Score = (Price Stability Weight × 0.4) + (Volume Weight × 0.3) + (Price Movement Weight × 0.2) + (Liquidity Depth Weight × 0.1)
```

**Where each component is scored 0-100:**

### Component Calculations

#### 1. Price Stability Weight (40% of confidence score)
```
Price Stability = 100 - ((Max Price - Min Price) / Min Price × 100)
Price Stability Weight = Price Stability Score (0-100)
```

**Examples:**
- Horse trading 2.0-2.1: Stability = 100 - ((2.1-2.0)/2.0 × 100) = 95 (high stability)
- Horse trading 5.0-6.5: Stability = 100 - ((6.5-5.0)/5.0 × 100) = 70 (moderate stability)
- Horse trading 8.0-12.0: Stability = 100 - ((12.0-8.0)/8.0 × 100) = 50 (low stability)

#### 2. Volume Weight (30% of confidence score)
```
Volume Score = min(100, (Traded Volume / 1000) × 10)
Volume Weight = Volume Score (capped at 100)
```

**Scoring Bands:**
- Very High Volume (>15,000 Euro): 100 points
- High Volume (10,000-15,000 Euro): 75-100 points
- Medium Volume (5,000-10,000 Euro): 50-75 points
- Low Volume (1,000-5,000 Euro): 10-50 points
- Very Low Volume (<1,000 Euro): 0-10 points

#### 3. Price Movement Weight (20% of confidence score)
```
Price Movement Direction Analysis:
- Shortening strongly (>10% decrease): 100 points
- Shortening moderately (5-10% decrease): 85 points
- Shortening slightly (1-5% decrease): 75 points
- Stable (±1%): 70 points
- Drifting slightly (1-5% increase): 50 points
- Drifting moderately (5-10% increase): 30 points
- Drifting strongly (>10% increase): 10 points
```

#### 4. Liquidity Depth Weight (10% of confidence score)
```
Liquidity Assessment based on price spreads and order book depth:
- Tight spreads (<2% between layers): 100 points
- Moderate spreads (2-5%): 75 points
- Wide spreads (5-10%): 50 points
- Very wide spreads (>10%): 25 points
```

### Market Confidence Classification

#### Final Market Confidence Level Assignment

```
Total Market Confidence Score = Sum of weighted components (0-100)

Classification:
- High Confidence: 80-100 points
- Medium Confidence: 60-79 points  
- Low Confidence: 40-59 points
- Very Low Confidence: 0-39 points
```

### Practical Examples

#### Example 1: High Market Confidence
```
Horse: "Strong Favorite"
- Price Range: 1.95-2.05 (Stability = 97.4)
- Traded Volume: 25,000 Euro (Volume = 100)
- Price Movement: Shortening from 2.1 to 2.0 (Movement = 85)
- Liquidity: Tight spreads (Liquidity = 100)

Market Confidence = (97.4 × 0.4) + (100 × 0.3) + (85 × 0.2) + (100 × 0.1)
                  = 38.96 + 30 + 17 + 10 = 95.96
Classification: HIGH CONFIDENCE
```

#### Example 2: Medium Market Confidence  
```
Horse: "Moderate Contender"
- Price Range: 5.0-5.8 (Stability = 84.0)
- Traded Volume: 8,000 Euro (Volume = 80)
- Price Movement: Stable (Movement = 70)
- Liquidity: Moderate spreads (Liquidity = 75)

Market Confidence = (84.0 × 0.4) + (80 × 0.3) + (70 × 0.2) + (75 × 0.1)
                  = 33.6 + 24 + 14 + 7.5 = 79.1
Classification: MEDIUM CONFIDENCE
```

#### Example 3: Low Market Confidence
```
Horse: "Uncertain Outsider"
- Price Range: 12.0-18.0 (Stability = 50.0)
- Traded Volume: 2,000 Euro (Volume = 20)
- Price Movement: Drifting moderately (Movement = 30)
- Liquidity: Wide spreads (Liquidity = 50)

Market Confidence = (50.0 × 0.4) + (20 × 0.3) + (30 × 0.2) + (50 × 0.1)
                  = 20 + 6 + 6 + 5 = 37.0
Classification: VERY LOW CONFIDENCE
```

### R2 Market Confidence Requirements

#### For Backing Selections
- **Tier 1**: HIGH confidence (80+) mandatory
- **Tier 2**: HIGH or MEDIUM confidence (60+) required
- **Tier 3**: Analysis only regardless of confidence

#### For Lay Opportunities
- **Favorite Laying**: Can proceed with MEDIUM+ confidence (60+)
- **Risk Assessment**: Lower confidence = higher lay risk

### Market Confidence Integration with CWAF

The Market Confidence Level directly feeds into the CWAF calculation:

```
Market Confidence Factor (part of CWAF):
- High Confidence (80-100): Factor = 1.2-1.3
- Medium Confidence (60-79): Factor = 1.0-1.2  
- Low Confidence (40-59): Factor = 0.8-1.0
- Very Low Confidence (0-39): Factor = 0.7-0.8
```

This ensures that selections with weak market support receive reduced expected value estimates, while those with strong market backing get enhanced confidence in their EV calculations.

### Market Confidence Monitoring

#### Real-Time Assessment
- Continuously monitor during pre-race analysis window
- Flag significant confidence changes (>20 point shifts)
- Abort selections if confidence drops below tier requirements

#### Historical Validation
- Track correlation between market confidence and actual race outcomes
- Adjust weighting factors based on historical performance
- Refine classification thresholds based on success rates
