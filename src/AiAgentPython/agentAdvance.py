import asyncio
from mcp_agent import RequestParams
from mcp_agent.core.fastagent import FastAgent

# Create the application
fast = FastAgent("Bfexplorer Assistant")

# Define the agent
@fast.agent(name="BfexplorerApp", 
    instruction="You are a helpful AI Agent executing betting/trading strategies on bfexplorer.", 
    model="deepseek-chat",
    #model="generic.openai/o3",
    request_params=RequestParams(
      maxTokens=8192,
      #maxTokens=8000,
      use_history=False,
      max_iterations=10
    ), 
    servers=["BfexplorerApp"]
)
async def main():
    async with fast.run() as agent:
        await agent(
"""
## Overview
Silent execution version of the results-calibrated multi-context horse racing analysis strategy. Performs comprehensive analysis using three integrated data sources with revised thresholds based on actual performance data. Produces only a final one-sentence outcome indicating bet placement or NO BET decision.

## System Instructions

### Primary Objective
Silently analyze horse racing markets using multi-context data to identify optimal betting opportunities. Execute strategy with minimal output - only final decision sentence required.

### Critical Results-Based Adjustments (Internal Processing)
Based on analysis of HorseRacingR5_Analysis performance data:
- **Issue Identified**: 100% NO BET rate across 5 races - over-conservative approach
- **Missed Winners**: Meblesh (5.1) and Diomed Duke (11.5) - both outsiders were overlooked
- **Favorite Bias**: Strategy focused on short-priced horses that didn't win
- **Solution**: Systematic full-field analysis with reduced thresholds and outsider detection

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

#### Step 2: Silent Multi-Context Analysis (Results-Calibrated)
```
Internal Analysis Framework:
- Enhanced Form Scoring (0-100): RP Form (50% - increased based on results) + Betfair Form (25%) + Market Sentiment (25%)
- Multi-Source Rating Analysis: OR vs RP vs Betfair ratings with differential calculations
- Market Sentiment Assessment: Price movement, volume analysis, forecast variance
- Enhanced EV Calculation: Multi-source probability estimation with bias adjustments
- Cross-validation scoring across all data sources
- **CRITICAL**: Systematic analysis of ALL runners, not just top favorites

Revised Selection Criteria (Based on Results Analysis):
- Enhanced EV > +15% for BACK bets (reduced from 30% - was too conservative)
- Enhanced EV < -20% for LAY bets (reduced from 30%)
- Enhanced Form Score > 65 (reduced from 75 - high scorers didn't always win)
- AI Score > 80 OR Multi-Rating Differential > +15%
- Cross-validation consistency > 70% (reduced from 80%)
- **SPECIAL**: Outsider consideration for horses 8/1+ with EV > +20% and improving metrics

Probability Calibration Adjustments:
- Apply 0.85 multiplier to horses <3.0 (favorite bias correction)
- Apply 1.15 multiplier to improving horses 8.0+
- Weight recent form more heavily (50% vs 35%)
- Reduce AI score influence for favorites

Target: Minimum 25% of races should produce betting opportunities

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
- Use: SetAIAgentDataContextForBetfairMarket (dataContextName: "HorseRacingR5_1_Analysis")
```

## Enhanced Analysis Framework (Internal Processing - Results-Calibrated)

### Multi-Source Form Analysis (Revised Weighting)
- **Racing Post Component (50% - increased)**: Recent race descriptions, beaten distances, semantic analysis, improvement trend bonuses
- **Betfair Form Component (25% - reduced)**: Form strings, consistency patterns, improvement trends
- **Market Sentiment Component (25% - reduced)**: Volume-weighted price movements, forecast variance

### Cross-Validated Rating System
- **Official Ratings**: Handicapper assessments across sources
- **Racing Post Ratings**: Expert form-based evaluations
- **Market-Implied Ratings**: Price-derived performance expectations
- **Differential Analysis**: Multi-source rating variance identification

### Enhanced Probability Calculation (Results-Calibrated)
```
Base Calculation = (Enhanced Form × 0.50) + (Multi-Rating × 0.25) + (AI Score × 0.15) + (Market Sentiment × 0.10)

Bias Adjustments:
- Favorite bias correction: 0.85 multiplier for horses <3.0
- Outsider value boost: 1.15 multiplier for improving horses 8.0+
- Recent form emphasis: 50% weighting

Final Probability = Base Calculation × Bias Adjustment × Confidence Factor

Enhanced EV = (Final Probability × (Odds - 1)) - (1 - Final Probability)

Cross-validation confidence intervals applied
```

### Selection Thresholds (Results-Calibrated)
- **Premium Back**: EV > +35% with 90%+ cross-validation
- **Strong Back**: EV +20% to +35% with 80%+ cross-validation
- **Value Back**: EV +15% to +20% with 70%+ cross-validation
- **Outsider Special**: EV +20%+ for horses 8/1+ with improving metrics
- **Strong Lay**: EV < -20% with 80%+ negative confirmation
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
    "strategyVersion": "R5.1",
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
    "enhancedStrikeRate": 35.0,
    "averageEnhancedEV": 25.0,
    "averageROI": 25.0,
    "disciplineRate": 60.0
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

### Performance Targets (Results-Calibrated)
- **Strike Rate**: 35%+ (realistic target based on data)
- **ROI**: 25%+ per profitable race (achievable target)
- **EV Accuracy**: ±5% of calculated values (realistic tolerance)
- **Discipline Rate**: 60%+ NO BET decisions (reduced from 75%)
- **Cross-Validation Accuracy**: 85%+ data consistency (more achievable)
- **Opportunity Capture**: Minimum 25% of races should meet betting criteria

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
4. **Results-Calibrated Criteria**: All R5.1 revised thresholds and cross-validation requirements maintained
5. **Multi-Context Integration**: All three data sources utilized without reporting
6. **Full-Field Analysis**: ALL runners must be analyzed systematically, not just favorites
7. **Outsider Detection**: Special consideration for horses 8/1+ with improving metrics
"""
        )

if __name__ == "__main__":
    asyncio.run(main())
