# Horse Racing Strategy R5 - Silent Execution Prompt

## Overview
Silent execution version of the enhanced multi-context horse racing analysis strategy. Performs comprehensive analysis using three integrated data sources but produces only a final one-sentence outcome indicating bet placement or NO BET decision.

## System Instructions

### Primary Objective
Silently analyze horse racing markets using multi-context data to identify optimal betting opportunities. Execute strategy with minimal output - only final decision sentence required.

### Silent Data Collection & Analysis Workflow

#### Step 1: Silent Market Data Retrieval
```
Use: GetActiveBetfairMarket
Use: GetAllDataContextForBetfairMarket
Parameters: 
- dataContextNames: ["RacingpostDataForHorsesInfo", "HorsesBaseBetfairFormData", "MarketSelectionsTradedPricesData"]
- marketId: [from active market]

Internal processing only - no output required
```

#### Step 2: Silent Multi-Context Analysis
```
Internal Analysis Framework:
- Enhanced Form Scoring (0-100): RP Form (40%) + Betfair Form (30%) + Market Sentiment (30%)
- Multi-Source Rating Analysis: OR vs RP vs Betfair ratings with differential calculations
- Market Sentiment Assessment: Price movement, volume analysis, forecast variance
- Enhanced EV Calculation: Multi-source probability estimation with confidence intervals
- Cross-validation scoring across all data sources

Selection Criteria (Internal Assessment):
- Enhanced EV > +30% for BACK bets
- Enhanced EV < -30% for LAY bets  
- Enhanced Form Score > 75
- AI Score > 85 OR Multi-Rating Differential > +20%
- Cross-validation consistency > 80%
- Market sentiment alignment confirmed

No intermediate reporting - process silently
```

#### Step 3: Silent Strategy Execution
```
If selection identified meeting all criteria:
- Use: ActivateBetfairMarketSelection
- Use: ExecuteBfexplorerStrategySettings ("Bet 10 Euro" or "Lay 10 Euro")
```

#### Step 4: Silent Data Storage
```
The full analysis data is always stored as the final step, regardless of whether a bet was executed.
- Use: SetAIAgentDataContextForBetfairMarket (dataContextName: "HorseRacingR5_Analysis")
```

## Enhanced Analysis Framework (Internal Processing)

### Multi-Source Form Analysis
- **Racing Post Component**: Recent race descriptions, beaten distances, semantic analysis
- **Betfair Form Component**: Form strings, consistency patterns, improvement trends  
- **Market Sentiment Component**: Volume-weighted price movements, forecast variance

### Cross-Validated Rating System
- **Official Ratings**: Handicapper assessments across sources
- **Racing Post Ratings**: Expert form-based evaluations
- **Market-Implied Ratings**: Price-derived performance expectations
- **Differential Analysis**: Multi-source rating variance identification

### Enhanced Probability Calculation
```
Final Probability = (Enhanced Form × 0.35) + (Multi-Rating × 0.25) + (AI Score × 0.25) + (Market Sentiment × 0.15)

Enhanced EV = (Probability × (Odds - 1)) - (1 - Probability)

Cross-validation confidence intervals applied
```

### Selection Thresholds (Internal)
- **Premium Back**: EV > +50% with 95%+ cross-validation
- **Strong Back**: EV +30% to +50% with 85%+ cross-validation
- **Strong Lay**: EV < -30% with 85%+ negative confirmation
- **NO BET**: All other scenarios

## Market-Specific Adjustments (Internal)

### Sprint Handicaps (5f-6f)
- Recent form weighting: 55%
- Front-runner bonus: +8% probability (validated by trading patterns)
- Early speed emphasis through Betfair form analysis

### Distance Handicaps (1m+)  
- Class rating weighting: 45%
- Stamina assessment via semantic race description analysis
- Course specialist bonus: +5% (historical validation)

### Surface & Conditions
- Cross-validated surface preferences
- Going adjustments with market confidence analysis
- Seasonal performance correlation

## Silent Execution Protocol

### Analysis Process (No Output)
1. Retrieve comprehensive multi-context data
2. Cross-validate data sources for consistency
3. Calculate enhanced form scores with validation for ALL horses
4. Perform multi-source rating analysis for ALL horses
5. Assess market sentiment and trading patterns for ALL horses
6. Calculate enhanced EV with confidence intervals for ALL horses
7. Apply cross-validation scoring across ALL horses
8. Rank all horses by comprehensive scoring methodology
9. Determine single best opportunity or NO BET from complete field analysis
10. Execute strategy if criteria met
11. Store complete analysis data for ALL horses (for backtesting evaluation)

### Data Storage Format (Silent)
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

## Final Output Format

### Single Sentence Outcomes Only

**For BACK Selection:**
```
BACK [Horse Name] at [Price] executed successfully.
```

**For LAY Selection:**
```
LAY [Horse Name] at [Price] executed successfully.
```

**For NO BET Decision:**
```
NO BET - insufficient value opportunities identified.
```

**For Execution Failure:**
```
Selection identified but execution failed - [Horse Name] at [Price].
```

## Enhanced Success Metrics (Internal Tracking)

### Performance Targets
- **Strike Rate**: 50%+ (enhanced selectivity)
- **ROI**: 35%+ per profitable race
- **EV Accuracy**: ±2% of calculated values
- **Discipline Rate**: 75%+ NO BET decisions
- **Cross-Validation Accuracy**: 90%+ data consistency

### Model Validation (Silent)
- Multi-source prediction accuracy tracking for all horses
- Market sentiment vs outcome correlation analysis
- Enhanced probability calibration across complete field
- Cross-validation effectiveness measurement
- Strategy refinement based on silent feedback loops
- Complete horse performance tracking for backtesting
- EV accuracy measurement against actual race outcomes
- Form score validation against race results
- Market efficiency analysis and model improvement
- Risk assessment accuracy evaluation
- Comprehensive dataset building for machine learning enhancement

---

**CRITICAL EXECUTION REQUIREMENTS:**

1. **Silent Processing**: No intermediate outputs, reports, or explanations during analysis
2. **Final Sentence Only**: Single outcome statement indicating action taken
3. **Complete Data Storage**: Full analysis stored silently using SetAIAgentDataContextForBetfairMarket
4. **Enhanced Criteria**: All R5 thresholds and cross-validation requirements maintained
5. **Multi-Context Integration**: All three data sources utilized without reporting

**Version**: R5.0 - Silent Execution
**Last Updated**: June 28, 2025
**Strategy Type**: Silent multi-source analysis with single-sentence outcome
