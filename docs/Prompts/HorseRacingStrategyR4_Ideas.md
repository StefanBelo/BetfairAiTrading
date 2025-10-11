# Horse Racing Strategy R4 - Comprehensive Race Analysis & Execution Prompt

## Overview
This prompt provides a systematic approach to analyzing horse racing markets using Racing Post data, form analysis, and Expected Value (EV) calculations to identify profitable betting opportunities.

## System Instructions

### Primary Objective
Analyze horse racing markets to identify the single best value betting opportunity by combining:
- Recent form analysis
- Rating comparisons (Official Rating vs Racing Post Rating)
- AI prediction scores
- Market pricing inefficiencies
- Expected Value calculations

**Selection Criteria**: Only recommend ONE selection per race that meets strict value thresholds. If no selection meets the criteria, recommend NO BET.

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

#### Step 2: Retrieve Racing Post Data
```
Use: GetDataContextForBetfairMarket
Parameters: 
- dataContextName: "RacingpostDataForHorses" 
- marketId: [from Step 1]
Purpose: Get comprehensive form data for all horses including:
- Recent race descriptions and performances
- Official ratings and Racing Post ratings
- Last run dates and beaten distances
- AI prediction scores
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

#### Step 4: Data Storage and Analysis Tracking
```
After completing analysis (whether bet placed or NO BET):

Use: SetAIAgentDataContextForBetfairMarket
Parameters:
- dataContextName: "HorseRacingR4_Analysis"
- marketId: [from Step 1]
- jsonData: [Complete analysis results in JSON format - see Data Format section]

Purpose: Store comprehensive analysis data for:
- Model validation and improvement
- Performance tracking over time
- EV accuracy measurement
- Strategy refinement based on outcomes
```

## Analysis Framework

### Step 5: Data Validation & Setup
- Verify all runners are active and declared
- Cross-reference market data with Racing Post data
- Check race conditions (distance, course, going, class)
- Identify any non-runners or late market changes

### Step 6: Form Analysis Methodology

#### Recent Form Scoring (0-100 scale)
- **Last 7 days**: 100 points for win, 80 for 2nd, 60 for 3rd
- **8-14 days**: 90 points for win, 70 for 2nd, 50 for 3rd  
- **15-30 days**: 80 points for win, 60 for 2nd, 40 for 3rd
- **31+ days**: Reduce by 10% per additional week

#### Performance Quality Assessment
- Race descriptions analysis for finishing style
- Beaten distances in recent races
- Course and distance suitability
- Going preferences

### Step 7: Rating Analysis
- Compare Official Rating (OR) vs Racing Post Rating (RP)
- Calculate rating differential: `(RP - OR) / OR * 100`
- Weight adjustments and class level assessment

### Step 8: Expected Value Calculation

#### EV Formula
```
EV = (Probability × (Odds - 1)) - (1 - Probability)
```

Where:
- **Probability** = Estimated true chance of winning
- **Odds** = Current market decimal odds
- **Positive EV** = Value bet opportunity
- **Negative EV** = Avoid or lay opportunity

#### Probability Estimation Method
```
Base Probability = (Form Score × 0.4) + (Rating Score × 0.3) + (AI Score × 0.3)
Adjusted for field size and race conditions
```

## Race Analysis Table Template

| Horse | Current Price | Form Score | OR | RP | AI Score | Est. Prob % | True Odds | Market Odds | EV % | Action | Rating |
|-------|---------------|------------|----|----|----------|-------------|-----------|-------------|-----|--------|--------|
| Horse A | 5.1 | 95 | 93 | 112 | 85 | 25.0% | 4.0 | 5.1 | +27.5% | BACK | ⭐⭐⭐ |
| Horse B | 6.6 | 88 | 95 | 111 | 85 | 18.0% | 5.56 | 6.6 | +18.8% | BACK | ⭐⭐ |
| Horse C | 3.3 | 82 | 101 | 113 | 97 | 22.0% | 4.55 | 3.3 | -27.4% | LAY | ⭐⭐ |
| Horse D | 7.0 | 75 | 88 | 112 | 98 | 15.0% | 6.67 | 7.0 | +5.0% | E/W | ⭐ |
| Others | Various | - | - | - | - | 20.0% | - | - | - | AVOID | - |

### Column Explanations

#### Star Rating System
The Rating column uses a star system to indicate bet confidence and preference:

**For BACK Bets:**
- ⭐⭐⭐⭐⭐ = **Premium Selection** (EV > +50%) - Maximum confidence, primary selection
- ⭐⭐⭐⭐ = **Strong Back** (EV +30% to +50%) - High confidence, secondary selection
- ⭐⭐⭐ = **Good Back** (EV +20% to +30%) - Solid value bet
- ⭐⭐ = **Moderate Back** (EV +10% to +20%) - Reasonable value
- ⭐ = **Weak Back** (EV +5% to +10%) - Marginal value, each-way consideration

**For LAY Bets:**
- ⭐⭐⭐⭐⭐ = **Strong Lay** (EV < -30%) - Maximum confidence against
- ⭐⭐⭐⭐ = **Good Lay** (EV -20% to -30%) - High confidence lay
- ⭐⭐⭐ = **Moderate Lay** (EV -15% to -20%) - Solid lay opportunity
- ⭐⭐ = **Weak Lay** (EV -10% to -15%) - Marginal lay value
- ⭐ = **Slight Lay** (EV -5% to -10%) - Minor lay consideration

**Special Indicators:**
- **AVOID** = No rating (EV -5% to +5%) - No betting value
- **E/W** = ⭐ rating - Each-way value only

#### Form Score Calculation
```
Form Score = (Recent Win Points × Recency Weight) + (Consistency Bonus) + (Class Adjustment)

Example: Sample Horse
- Won 6 days ago: 100 × 1.0 = 100 points
- Consistency (3 wins in 6 runs): +10 points  
- Class level: No adjustment
- Final Score: 95/100
```

#### Rating Differential
```
Rating Diff = ((RP Rating - OR Rating) / OR Rating) × 100

Example: Sample Horse
- RP: 112, OR: 93
- Diff: ((112-93)/93) × 100 = +20.4%
- Indicates underrated by handicapper
```

#### AI Score Integration
```
AI Score Weight = Min(AI Prediction Score, 100)
- High scoring horses typically show consistency
- Scores above 95 indicate strong recent form
- Use as confidence indicator for selections
```

#### Probability Estimation
```
Base Probability = (Form × 0.4) + (Rating × 0.3) + (AI × 0.3)

Example: Sample Horse
- Form: 95 × 0.4 = 38
- Rating: 80 × 0.3 = 24 (adjusted for rating differential)
- AI: 85 × 0.3 = 25.5
- Total: 87.5/100 = Raw probability
- Field adjustment: 87.5 × (1/field_strength) = Final probability %
```

#### Expected Value Calculation
```
EV = (True Probability × (Market Odds - 1)) - (1 - True Probability)

Example: Sample Horse
- True Probability: 25% (0.25)
- Market Odds: 5.1
- EV = (0.25 × (5.1 - 1)) - (1 - 0.25)
- EV = (0.25 × 4.1) - 0.75
- EV = 1.025 - 0.75 = +0.275 (+27.5%)
```

## Strategy Execution Rules

### Single Selection Criteria
**Only recommend ONE bet per race that meets ALL of the following:**

**For BACK Bets:**
- EV must be > +25% (Strong value threshold)
- Form Score > 70 (Recent competitive form)
- AI Score > 85 OR Rating Differential > +15% (Consistency or handicap advantage)
- Maximum odds of 20.0 (Reasonable probability)

**For LAY Bets:**
- EV must be < -25% (Strong negative value)
- Clear form concerns (AI Score = 0 OR no run within 60 days)
- Overpriced in market (odds under 8.0 for weak credentials)

**If no selection meets these criteria: RECOMMEND NO BET**

**NO BET Decision Requirements:**
- Must document specific criteria failures for each potential selection
- Explain why best EV candidate falls short of thresholds
- Assess market efficiency and value distribution
- Reinforce strategy discipline and capital preservation
- Provide guidance for future opportunity identification

### Betting Thresholds (Single Selection Only)
- **Premium Back**: EV > +50% (⭐⭐⭐⭐⭐)
- **Strong Back**: EV +25% to +50% (⭐⭐⭐⭐)
- **Strong Lay**: EV < -25% (⭐⭐⭐⭐⭐ LAY)
- **NO BET**: All other scenarios

### Stake Sizing (Single Selection)
```
Stake % = (EV × Probability) / (Odds - 1)
Minimum stake: 2% of bank
Maximum stake: 8% of bank per bet
```

### Risk Management (Single Selection Focus)
- Only one bet per race maximum
- Stop loss: If selection drifts 50% from analysis price
- Profit target: 20%+ return on successful bets
- Strict discipline: NO BET if criteria not met

## Market-Specific Adjustments

### Sprint Handicaps (5f-6f)
- Increase recent form weighting to 50%
- Front-runner bonus: +5% probability
- Pace scenario analysis required

### Distance Handicaps (1m+)
- Class rating weighting increased to 40%
- Stamina assessment from race descriptions
- Course specialist bonus: +3% probability

### All-Weather vs Turf
- Surface preference analysis
- Going adjustment factors
- Seasonal performance patterns

## Execution Checklist

### Pre-Race (30 minutes before)
1. ✅ Verify all runners declared
2. ✅ Check for market moves (>20% price change)
3. ✅ Confirm weather/going unchanged
4. ✅ Review betting exchange liquidity
5. ✅ Set alerts for target prices

### During Race Analysis
1. ✅ Calculate all EV values
2. ✅ Identify the single best value opportunity
3. ✅ Verify selection meets strict criteria
4. ✅ Set stop-loss level
5. ✅ Document reasoning or NO BET decision
6. ✅ Execute strategy if selection made (see Step 3)

### Strategy Execution Workflow
When a selection is identified:
1. ✅ Use ActivateBetfairMarketSelection for chosen horse
2. ✅ Execute appropriate strategy:
   - **BACK bets**: Execute "Bet 10 Euro" strategy
   - **LAY bets**: Execute "Lay 10 Euro" strategy
3. ✅ Confirm strategy execution successful
4. ✅ Monitor position and market movements
5. ✅ **Store complete analysis data using SetAIAgentDataContextForBetfairMarket**

### Data Storage Workflow (Required for ALL analyses)
After completing analysis (selection made OR NO BET decision):
1. ✅ Compile comprehensive analysis data in JSON format
2. ✅ Include all EV calculations, form scores, and decision reasoning
3. ✅ Use SetAIAgentDataContextForBetfairMarket with:
   - **dataContextName**: "HorseRacingR4_Analysis"
   - **marketId**: [from active market]
   - **jsonData**: [Complete analysis JSON - see Data Format section]
4. ✅ Confirm data storage successful for model tracking

### NO BET Documentation Requirements
When recommending NO BET, must include:
1. ✅ Specific criteria failures for top 3 candidates
2. ✅ EV calculations and threshold comparisons
3. ✅ Form score analysis and minimum requirements
4. ✅ AI score and rating differential assessments
5. ✅ Market efficiency evaluation
6. ✅ Strategy discipline reinforcement messaging
7. ✅ Guidance for future opportunity identification
8. ✅ **Store NO BET analysis data using SetAIAgentDataContextForBetfairMarket**

### Post-Race Review
1. ✅ Record actual results vs predictions
2. ✅ Calculate P&L and ROI
3. ✅ Analyze prediction accuracy
4. ✅ Update model parameters
5. ✅ Document lessons learned

## Success Metrics

### Performance Targets (Single Selection Strategy)
- **Strike Rate**: 45%+ for win bets (higher due to selectivity)
- **ROI**: 25%+ per profitable race
- **EV Accuracy**: ±3% of calculated values
- **Discipline Rate**: 70%+ NO BET decisions when criteria not met

### Model Validation
- Track EV vs actual results over 50+ races
- Adjust probability weightings based on performance
- Monitor for market efficiency changes
- Regular model recalibration

## Example Output Format

**Option 1: Single Selection Identified**
```
RACE ANALYSIS: [Venue] [Distance] [Class] - [Date]
Market ID: [Market ID from Step 1]

RECOMMENDED SELECTION: [Horse Name] ([Price]) ⭐⭐⭐⭐⭐
- Action: BACK (or LAY)
- EV: +[X]% 
- Probability: [X]%
- Stake: [X]% of bank
- Reasoning: [Key factors from Racing Post data]
- Confidence: Maximum

CRITERIA VERIFICATION:
✅ EV > +25% (or < -25% for lay)
✅ Form Score > 70
✅ AI Score > 85 OR Rating Diff > +15%
✅ Odds under 20.0 (for backs)

STRATEGY EXECUTION:
✅ Selection activated: ActivateBetfairMarketSelection
✅ Strategy executed: [Bet 10 Euro / Lay 10 Euro]
✅ Position confirmed and monitored
✅ Analysis data stored: SetAIAgentDataContextForBetfairMarket("HorseRacingR4_Analysis")

REJECTED SELECTIONS:
- [Horse Name]: EV +18% - Below +25% threshold
- [Horse Name]: Form Score 65 - Below 70 threshold
- [Horse Name]: AI Score 82, Rating Diff +12% - Below criteria
```

**Option 2: No Selection Meets Criteria**
```
RACE ANALYSIS: [Venue] [Distance] [Class] - [Date]
Market ID: [Market ID from Step 1]

RECOMMENDATION: NO BET

DETAILED REASONING FOR NO BET DECISION:

PRIMARY CRITERIA FAILURES:
❌ EV Threshold: No horse meets +25% minimum for BACK bets
❌ Form Requirements: No horse meets 70+ form score minimum  
❌ AI/Rating Requirements: No horse meets AI >85 OR Rating Diff >+15%
❌ LAY Criteria: No horse meets <-25% EV with supporting form concerns

BEST OPPORTUNITIES ANALYZED:
1. [Horse Name] ([Price]) - EV: +22%
   - FAILED: EV +22% below +25% threshold
   - Form Score: 68 (below 70 minimum)
   - AI Score: 82, Rating Diff: +12% (both below thresholds)
   - Reasoning: Marginal value insufficient for selection criteria

2. [Horse Name] ([Price]) - EV: +18%  
   - FAILED: EV +18% below +25% threshold
   - Form Score: 75 (meets minimum)
   - AI Score: 88 (meets minimum)
   - Reasoning: Despite good form/AI, EV insufficient

3. [Horse Name] ([Price]) - EV: -20%
   - FAILED: LAY EV -20% above -25% threshold
   - Form Concerns: AI Score 0, last run 45 days ago
   - Reasoning: Negative value present but below LAY threshold

MARKET ASSESSMENT:
- Field Strength: [Strong/Moderate/Weak] - affects probability calculations
- Price Efficiency: Market appears [efficient/inefficient] 
- Value Distribution: [Concentrated/Scattered] across runners
- Betting Recommendation: Wait for clearer value opportunities

STRATEGY DISCIPLINE REINFORCEMENT:
✅ Strict criteria maintained over marginal opportunities
✅ Capital preservation prioritized  
✅ Quality over quantity approach enforced
✅ Model integrity protected from emotional betting
✅ Analysis data stored: SetAIAgentDataContextForBetfairMarket("HorseRacingR4_Analysis")

NEXT ACTIONS:
- Monitor for late market moves creating value
- Await races with clearer form/rating advantages
- Maintain discipline for better opportunities
```

---

**Note**: This framework focuses on finding ONE exceptional value opportunity per race. The majority of races should result in NO BET recommendations due to the strict selection criteria. Quality over quantity is paramount for long-term profitability.

**CRITICAL REQUIREMENT**: ALL analyses must be stored using SetAIAgentDataContextForBetfairMarket with dataContextName "HorseRacingR4_Analysis" for comprehensive model tracking and validation.

**Version**: R4.3 - Single Selection Focus with Data Tracking
**Last Updated**: June 28, 2025
**Strategy Type**: Selective value-based approach with comprehensive analysis storage

## Analysis Data Format for Storage

### JSON Structure for SetAIAgentDataContextForBetfairMarket

```json
{
  "analysisMetadata": {
    "timestamp": "2025-06-28T16:45:00Z",
    "strategyVersion": "R4.2",
    "marketId": "1.245129918",
    "venue": "Newcastle",
    "raceType": "7f Hcap",
    "startTime": "2025-06-28T16:45:00+02:00",
    "analysisDate": "2025-06-28",
    "fieldSize": 13
  },
  "marketData": {
    "selections": [
      {
        "selectionId": "69366446_0.00",
        "name": "Rare Change",
        "marketPrice": 5.9,
        "status": "Active"
      }
    ]
  },
  "horseAnalysis": [
    {
      "selectionId": "69366446_0.00",
      "name": "Rare Change",
      "marketPrice": 5.9,
      "formAnalysis": {
        "formScore": 95,
        "recentRuns": [
          {
            "daysAgo": 5,
            "position": 1,
            "beatenDistance": 0,
            "points": 100,
            "decayFactor": 1.0,
            "qualityMultiplier": 1.1,
            "raceDescription": "towards rear, headway and ridden briefly over 1f out, led inside final furlong, kept on well pushed out, comfortably",
            "semanticAnalysis": {
              "finishQuality": "strong",
              "runningStyle": "hold-up",
              "troubleInRunning": "none",
              "paceAnalysis": "strong_finish",
              "qualityIndicators": ["led inside final furlong", "kept on well", "comfortably"]
            }
          }
        ],
        "consistencyScore": 85,
        "trendAnalysis": "improving",
        "courseAdvantage": true,
        "courseBonus": 5
      },
      "ratingAnalysis": {
        "officialRating": 89,
        "rpRating": 117,
        "ratingDifferential": 31.5,
        "ratingScore": 90,
        "classLevel": "handicap",
        "weightAdjustment": 0
      },
      "aiAnalysis": {
        "predictionScore": 90.83,
        "confidence": "high",
        "consistencyIndicator": true
      },
      "probabilityCalculation": {
        "formComponent": 38.0,
        "ratingComponent": 27.0,
        "aiComponent": 27.25,
        "rawProbability": 92.25,
        "fieldAdjustment": 0.29,
        "finalProbability": 26.8,
        "trueOdds": 3.73
      },
      "expectedValue": {
        "calculation": "(0.268 × (5.9 - 1)) - (1 - 0.268)",
        "marketOdds": 5.9,
        "impliedProbability": 16.95,
        "trueProbability": 26.8,
        "edgePercentage": 9.85,
        "expectedValue": 58.2,
        "classification": "premium_selection"
      },
      "criteriaAssessment": {
        "evThreshold": {"required": 25, "actual": 58.2, "meets": true},
        "formScore": {"required": 70, "actual": 95, "meets": true},
        "aiScore": {"required": 85, "actual": 90.83, "meets": true},
        "ratingDiff": {"required": 15, "actual": 31.5, "meets": true},
        "oddsLimit": {"required": 20.0, "actual": 5.9, "meets": true},
        "overallAssessment": "all_criteria_met"
      },
      "stakingRecommendation": {
        "stakePercentage": 8.0,
        "stakeJustification": "maximum_due_to_exceptional_value",
        "kellyFraction": 0.076,
        "riskAssessment": "low_to_moderate"
      }
    }
  ],
  "decisionSummary": {
    "recommendation": "BACK",
    "selectedHorse": {
      "name": "Rare Change",
      "selectionId": "69366446_0.00",
      "price": 5.9,
      "expectedValue": 58.2,
      "confidence": "maximum"
    },
    "reasoning": [
      "Recent course winner (5 days ago)",
      "Exceptional rating differential (+31.5%)",
      "Strong AI prediction score (90.83)",
      "Premium expected value (+58.2%)",
      "All strict criteria met"
    ],
    "alternativesConsidered": [
      {
        "name": "Eldrickjones",
        "price": 5.7,
        "expectedValue": 45.4,
        "rejectionReason": "lower_ev_than_primary_selection"
      }
    ],
    "marketAssessment": {
      "efficiency": "inefficient",
      "valueDistribution": "concentrated_on_outsiders",
      "competitiveLevel": "moderate_to_strong"
    }
  },
  "executionDetails": {
    "strategyExecuted": true,
    "executionTime": "2025-06-28T16:42:15Z",
    "strategyName": "Bet 10 Euro",
    "selectionActivated": true,
    "executionStatus": "successful",
    "priceAtExecution": 5.9,
    "slippage": 0.0
  },
  "riskManagement": {
    "stopLossLevel": 8.85,
    "profitTarget": 1.18,
    "maxDrawdown": 50,
    "positionSize": "8%_of_bank",
    "riskRating": "moderate"
  },
  "modelValidation": {
    "backtestingData": {
      "similarScenarios": 15,
      "historicalStrikeRate": 47.3,
      "averageEV": 42.1,
      "averageROI": 28.4
    },
    "confidenceInterval": {
      "lower": 22.1,
      "upper": 31.5,
      "confidence": 95
    }
  },
  "postRaceTracking": {
    "actualResult": null,
    "actualPosition": null,
    "pnlResult": null,
    "evAccuracy": null,
    "lessonsLearned": null,
    "modelAdjustments": null
  }
}
```

### Alternative JSON Format for NO BET Scenarios

```json
{
  "analysisMetadata": {
    "timestamp": "2025-06-28T16:45:00Z",
    "strategyVersion": "R4.2",
    "marketId": "1.245129918",
    "venue": "Newcastle",
    "raceType": "7f Hcap",
    "decision": "NO_BET"
  },
  "decisionSummary": {
    "recommendation": "NO_BET",
    "reasoning": "no_horse_meets_strict_criteria",
    "criteriaFailures": {
      "evThreshold": 2,
      "formScore": 1,
      "aiScore": 3,
      "oddsLimit": 0
    }
  },
  "topCandidatesAnalyzed": [
    {
      "name": "Horse A",
      "price": 4.5,
      "expectedValue": 22.1,
      "failureReason": "ev_below_25_percent_threshold",
      "formScore": 68,
      "aiScore": 82
    }
  ],
  "marketAssessment": {
    "efficiency": "moderate",
    "valueDistribution": "marginal_across_field",
    "recommendedAction": "wait_for_better_opportunities"
  },
  "disciplineTracking": {
    "noBetDecisionCount": 1,
    "disciplineRate": 73.2,
    "capitalPreservation": "prioritized"
  }
}
```
