# Horse Racing Strategy R5.3 - Enhanced Multi-Context Analysis with Weight of Money Integration

## Overview
This enhanced prompt provides a comprehensive systematic approach to analyzing horse racing markets using four integrated data sources: Racing Post data, Betfair base form data, market trading price data, and Weight of Money analysis to identify optimal betting opportunities. Updated to work with current data structure including the new WeightOfMoneyData context for professional betting pattern analysis.

### Revised Performance Targets (Based on Results Analysis)
- **Strike Rate**: 35%+ for win bets (realistic target based on data)
- **ROI**: 25%+ per profitable race (achievable with revised thresholds)
- **EV Accuracy**: ±5% of calculated values (more realistic tolerance)
- **Discipline Rate**: 60%+ NO BET decisions when criteria not met (reduced from 75%)
- **Market Timing**: 75%+ accuracy in sentiment prediction (more realistic)
- **Opportunity Capture**: Minimum 30% of races should meet betting criteria (NEW TARGET)

## System Instructions

### Primary Objective
Analyze horse racing markets to identify the single best value betting opportunity by combining:
- Comprehensive Racing Post form analysis  
- Betfair base form data and ratings
- Market trading price patterns and volume analysis
- **NEW**: Weight of Money analysis for professional betting patterns
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
- dataContextNames: ["RacingpostDataForHorsesInfo", "HorsesBaseBetfairFormData", "MarketSelectionsTradedPricesData", "WeightOfMoneyData"]
- marketId: [from Step 1]

Purpose: Get comprehensive multi-source data including:

From RacingpostDataForHorsesInfo:
- Detailed race descriptions and performances from lastRacesDescriptions
- Official ratings and Racing Post ratings (officialRating, rpRating)
- Last run dates and beaten distances from race data
- Form analysis based on race descriptions and position trends

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

From WeightOfMoneyData:
- Professional betting patterns and volume distribution
- Smart money indicators and institutional backing
- Market confidence signals from large stake activity
- Contra-indicator signals from public money vs sharp money
- Weight of money flow analysis and timing patterns
```

#### Step 3: Strategy Execution (When Selection Made)
```
When a dedicated selection is chosen for betting:

**CRITICAL**: Only use the following two strategy names - no other strategies are available:

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

**WARNING**: Do NOT use any other strategy names such as "BackTrade100EuroStrategy" or similar. 
Only "Bet 10 Euro" and "Lay 10 Euro" are valid strategy names.

Execute immediately after ActivateBetfairMarketSelection
```

#### Step 4: Enhanced Data Storage and Analysis Tracking
```
After completing analysis (whether bet placed or NO BET):

Use: SetAIAgentDataContextForBetfairMarket
Parameters:
- dataContextName: "HorseRacingR5_1_Analysis"
- marketId: [from Step 1]
- jsonData: [Enhanced analysis results in JSON format - see Data Format section]

Purpose: Store comprehensive multi-context analysis data for:
- Cross-validation of prediction models
- Market efficiency pattern recognition
- Price discovery and volume correlation analysis
- Enhanced strategy refinement based on multi-source outcomes
- Trading pattern analysis and market timing optimization
```

## CRITICAL LESSONS FROM RESULTS ANALYSIS

### Key Findings from HorseRacingR5_Analysis Performance Data:
1. **100% NO BET Rate Across 5 Races** - Strategy was over-conservative
2. **Missed Both Actual Winners**: Meblesh (5.1) and Diomed Duke (11.5) - both outsiders
3. **Favorite Bias Problem**: Consistently analyzed short-priced horses that didn't win
4. **Probability Calculation Issues**: Over-estimated favorite chances (65% for 3.75 odds)
5. **Form Score Disconnect**: High form scores (90+) for non-winners, lower scores for actual winners

### Mandatory Adjustments Based on Results:

#### 1. **Systematic Full-Field Analysis**
- **MUST analyze ALL runners in every race**, not just top 2-3 favorites
- Create comprehensive analysis table for every horse
- Identify outsider value opportunities specifically

#### 2. **Probability Recalibration**
- Apply favorite bias correction (0.85 multiplier for odds <3.0)
- Apply outsider value boost (1.15 multiplier for improving horses 8.0+)
- Weight recent form more heavily (50% vs 35%)

#### 3. **Enhanced Outsider Detection Protocol**
```
For horses 8/1+ (8.0+), apply special analysis:
- Recent form improvement trend
- Significant rating discrepancies
- Market drift patterns
- Trainer/jockey form when available
- Lower EV threshold (15% vs 20%) for outsiders with strong improving metrics
```

#### 4. **Revised Analysis Priorities**
- **Primary Focus**: Value identification across entire field
- **Secondary Focus**: Market efficiency gaps
- **Tertiary Focus**: Favorite validation (reduced emphasis)

#### 5. **Practical Betting Frequency Target**
- **Minimum 25% of races** should produce a betting opportunity
- If falling below this rate, review and adjust criteria
- Balance discipline with opportunity capture

**Version Update**: R5.1 - Results-Calibrated Analysis
**Implementation Date**: June 28, 2025
**Key Change**: Reduced over-conservatism based on actual performance data

---

## Enhanced Analysis Framework

### Step 5: Multi-Source Data Validation & Cross-Reference
- Verify consistency across all three data sources
- Cross-reference Racing Post vs Betfair official ratings
- Validate forecast prices against trading patterns
- Identify discrepancies that may indicate value opportunities
- Check form strings against detailed race descriptions
- **CRITICAL ADDITION**: Systematically analyze ALL runners, not just top 2-3 favorites

**IMPORTANT NOTE**: The scores calculated in Step 6 (Enhanced Form Score, Multi-Rating Score, Market Sentiment, Weight of Money Score) are *not* Expected Value (EV) calculations. They are component scores that contribute to a horse's overall `Total Score`, which is then used to derive a `Normalized Probability` and finally, a single `Enhanced Expected Value` for each horse. Do not interpret these individual component scores as separate EV values.

### Step 6: Enhanced Form Analysis Methodology

#### Revised Form Scoring (0-100 scale) - Calibrated Based on Results with Weight of Money Integration
**Racing Post Component (45% weight - adjusted for Weight of Money integration):**
- Points based on recent top 3 finishes for the last 5 races:
    - 1st place: 5 points
    - 2nd place: 3 points
    - 3rd place: 1 point
    - Other: 0 points
- Maximum raw score: 25 points (5 wins). Scale to 0-100 by multiplying by 4.

**Betfair Form String Component (20% weight - reduced for Weight of Money integration):**
- Points based on recent top 3 finishes for the last 5 races:
    - 1st place: 5 points
    - 2nd place: 3 points
    - 3rd place: 1 point
    - Other: 0 points
- Maximum raw score: 25 points (5 wins). Scale to 0-100 by multiplying by 4.

**Market Sentiment Component (20% weight - reduced for Weight of Money integration):**
- Score based on price movement (-10 to +10). Scale to 0-100 using formula: `(raw_score + 10) * 5`
    - Strong shortening: +10
    - Slight shortening: +5
    - Stable: 0
    - Slight lengthening: -5
    - Strong lengthening: -10

**Weight of Money Component (15% weight - NEW ADDITION):**
- Score based on money flow (-10 to +10). Scale to 0-100 using formula: `(raw_score + 10) * 5`
    - Strong professional backing: +10
    - Moderate professional backing: +5
    - Mixed/Neutral: 0
    - Potential contra-indicator: -5
    - Strong contra-indicator: -10

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

#### Corrected EV Calculation Methodology
The previous method of calculating probability was flawed. Based on user feedback, the new methodology is as follows:
1.  For each horse, a `Total Score` is calculated by combining the various data points (Form, Ratings, WoM, etc.).
2.  These `Total Scores` for all horses in the race are then normalized to create a valid probability distribution that sums to 100%.
3.  This `Normalized Probability` is then used to calculate the final, correct EV.

#### Enhanced EV Formula
```
EV = (Normalized Probability × (Odds - 1)) - (1 - Normalized Probability)

Where 'Odds' are the Current Price, and 'Normalized Probability' is calculated as described below.
```

#### Step 1: Total Score Calculation
This step combines all analytical scores into a single `Total Score` for each horse. This is NOT a probability.
**IMPORTANT**: While individual scores are on a 0-100 scale, the combination should aim for a `Total Score` that, when normalized, yields realistic probabilities (0-100%). Unusually high `Total Scores` can lead to unrealistic `Normalized Probabilities` and inflated EV values. Consider the relative impact of each component to ensure a balanced `Total Score`.
```
Total Score = (RP Form Score * 0.2) + (BF Form Score * 0.15) + (Multi-Rating Score * 0.15) + (AI Score * 0.2) + (Market Sentiment * 0.15) + (Weight of Money * 0.15)

Enhanced Form Score = (RP Form × 0.45) + (Betfair Form × 0.20) + (Trading Pattern × 0.35)
Multi-Rating Score = Weighted average of OR, RP, and market-implied ratings
Market Sentiment = Volume-weighted price movement analysis + forecast variance
Weight of Money Score = Smart money confidence + institutional backing strength - public money contra-indicators
```

#### Step 2: Probability Normalization (CRITICAL)
To ensure the probabilities of all outcomes sum to 100%, the `Total Score` for each horse must be normalized.

1.  Calculate the `Total Score` for every horse in the race.
2.  Sum all the `Total Scores` together to get a `SumOfAllScores`.
3.  Calculate the `Normalized Probability` for each horse:
    `Normalized Probability = Total Score / SumOfAllScores`

**IMPORTANT**: The `Normalized Probability` MUST be a decimal value between 0 and 1 (e.g., 0.25 for 25%). The `Est. Prob %` column in the analysis table should then display this as a percentage.

#### Enhanced EV Formula (Post-Normalization Adjustment)
After calculating the initial `EV` using the `Normalized Probability`, apply the following adjustments:

-   **Favorite Bias Adjustment**: If Current Price < 3.0 (favorite): `Adjusted EV = EV × 0.9`
-   **Outsider Value Boost**: If Current Price >= 8.0 (outsider) and has an "improving trend": `Adjusted EV = EV × 1.1`
-   Otherwise: `Adjusted EV = EV`

**Definition of "Improving Trend"**: An "improving trend" for an outsider is defined as having at least two top-3 finishes (1st, 2nd, or 3rd) in their last five races, with at least one of those being a 1st or 2nd place. This should be derived from the `lastRacesDescriptions` and `form` data.

#### Advanced Weight of Money Analysis
```
Weight of Money Indicators:
- Smart Money Flow: Large stakes placed early vs late public money
- Professional Patterns: Consistent backing from known sharp accounts
- Market Maker Activity: Exchange specialist money vs general punter activity
- Institutional Confidence: Pension fund/syndicate style betting patterns
- Contra-Indicators: Heavy public money against professional sentiment

Weight of Money Score Calculation:
- Professional backing strength (0-40 points)
- Smart money timing advantage (0-30 points)  
- Institutional confidence level (0-20 points)
- Contra-indicator adjustments (-10 to +10 points)
- Final Weight of Money Score (0-100)

**IMPORTANT NOTE on OfferedPrices BetType**: In `OfferedPrices` data, `BetType: 1` (Back) indicates a price offered by a bettor who wants to place a LAY bet (i.e., they are offering to lay the selection). Conversely, `BetType: 2` (Lay) indicates a price offered by a bettor who wants to place a BACK bet (i.e., they are offering to back the selection). This is crucial for correctly interpreting market depth and sentiment.
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

## Enhanced Race Analysis Table Template with Weight of Money

| Horse | Current Price | Form Score | OR | RP | WoM Score | AI Score | Market Sentiment | Est. Prob % | True Odds | Market Odds | EV % | Action | Rating |
|-------|---------------|------------|----|----|-----------|----------|------------------|-------------|-----------|-------------|-----|--------|--------|
| Horse A | 5.1 | 95 | 93 | 112 | 85 | 85 | +15% | 27.5% | 3.64 | 5.1 | +40.2% | BACK | ⭐⭐⭐⭐⭐ |
| Horse B | 6.6 | 88 | 95 | 111 | 65 | 85 | -5% | 16.8% | 5.95 | 6.6 | +10.9% | BACK | ⭐⭐ |
| Horse C | 3.3 | 82 | 101 | 113 | 45 | 97 | -12% | 20.2% | 4.95 | 3.3 | -33.3% | LAY | ⭐⭐⭐⭐⭐ |

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
Market Sentiment = Score based on price movement (0-100): If Current Price < Forecast Price (price shortening), score is 100. If Current Price > Forecast Price (price lengthening), score is 0. If Current Price == Forecast Price, score is 50.
**IMPORTANT**: This score should reflect the strength of the sentiment, not just its direction. Consider volume and speed of price movement for a more nuanced score.
**IMPORTANT**: This score should reflect the strength of the sentiment, not just its direction. Consider volume and speed of price movement for a more nuanced score.
```

#### Weight of Money (WoM) Score Analysis
```
WoM Score Components (0-100 scale):
- Professional Money Flow (0-40): Early smart money backing patterns
- Institutional Confidence (0-30): Large stake patterns from known sharp accounts  
- Market Timing Advantage (0-20): Early positioning vs late public money
- Contra-Indicator Signals (0-10): Public money flowing against professional sentiment

WoM Score Interpretation:
- 80-100: Strong professional backing, high confidence indicator
- 60-79: Moderate smart money interest, positive signal
- 40-59: Mixed or neutral money flow, no clear signal
- 20-39: Potential contra-indicator, public money heavy
- 0-19: Strong contra-indicator, avoid or consider lay opportunity
```

#### Final Probability Normalization
To ensure the estimated probabilities of all outcomes sum to 100%, the `Total Score` for each horse must be normalized.

1.  Calculate the `Total Score` for every horse in the race.
2.  Sum all the `Total Scores` together to get a `SumOfAllScores`.
3.  Calculate the `Normalized Probability` for each horse:
    `Normalized Probability = Total Score / SumOfAllScores`

The `Est. Prob %` column in the analysis table MUST be this `Normalized Probability`.


## Enhanced Strategy Execution Rules

### Revised Pragmatic Selection Criteria
**Based on analysis of actual results, criteria have been adjusted for better balance between selectivity and opportunity capture:**

**Enhanced For BACK Bets with Weight of Money Integration:**
- EV must be > +15% (Reduced from 30% - analysis showed 30% threshold missed profitable opportunities)
- Enhanced Form Score > 65 (Reduced from 75 - high scorers didn't always win)
- AI Score > 80 OR Multi-Rating Differential > +15% (Slightly lowered thresholds)
- **NEW**: Weight of Money Score > 50 OR EV > +25% despite lower WoM score
- Market Sentiment positive OR significant value despite negative sentiment
- Maximum odds of 25.0 (Increased to capture more outsider value)
- Multi-source data consistency check passed (>70% alignment)

**ENHANCED ADDITION - Outsider Value Detection with Weight of Money:**
- Special consideration for horses 8/1+ (8.0+) with:
  - Improving recent form trend
  - AI Score > 75 OR Weight of Money Score > 60
  - Market drift suggesting hidden support
  - EV > +20% despite lower overall scores
  - **NEW**: Professional backing despite public dismissal (WoM score high vs market price)

**Enhanced For LAY Bets with Weight of Money:**
- EV must be < -20% (Reduced from 30% for more opportunities)
- Clear form concerns across multiple data sources
- Negative market sentiment with volume confirmation
- **NEW**: Weight of Money Score < 30 (strong contra-indicator)
- **NEW**: Heavy public money vs minimal professional backing
- Overpriced in market with supporting data discrepancies

**If no selection meets revised criteria: RECOMMEND NO BET**

### Enhanced Betting Thresholds with Weight of Money Integration
- **Premium Back**: EV > +35% with full validation + WoM Score > 70 (⭐⭐⭐⭐⭐)
- **Strong Back**: EV +20% to +35% with high confidence + WoM Score > 60 (⭐⭐⭐⭐)
- **Value Back**: EV +15% to +20% with reasonable confidence + WoM Score > 50 (⭐⭐⭐)
- **Professional Backed Outsider**: EV +20%+ for horses 8/1+ with WoM Score > 60 (⭐⭐⭐⭐)
- **Smart Money Special**: EV +25%+ with WoM Score > 80 regardless of other factors (⭐⭐⭐⭐⭐)
- **Strong Lay**: EV < -20% with multi-source confirmation + WoM Score < 30 (⭐⭐⭐⭐⭐ LAY)
- **Contra-Indicator Lay**: EV < -15% with WoM Score < 20 (heavy public money) (⭐⭐⭐⭐ LAY)
- **NO BET**: All other scenarios including mixed signals

### Advanced Risk Management with Weight of Money Integration
- Cross-validate selection with all four data sources including Weight of Money
- Monitor live price movements against analysis AND smart money flow changes
- Set dynamic stop-loss based on market sentiment changes AND Weight of Money shifts
- Position sizing based on confidence levels across all data sources AND WoM score strength
- **NEW**: Alert system for sudden Weight of Money reversals indicating insider information
- **NEW**: Professional money exit signals monitoring for early position closure

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

### Enhanced Analysis with Weight of Money Integration
1. ✅ Calculate multi-source form scores with Weight of Money validation
2. ✅ Cross-reference rating systems for anomalies
3. ✅ Analyze market sentiment, trading patterns, AND Weight of Money flows
4. ✅ Calculate enhanced EV with confidence intervals including WoM factors
5. ✅ Verify Weight of Money alignment with form analysis
6. ✅ Identify single best value opportunity with full four-source validation
7. ✅ Verify selection meets all enhanced criteria including WoM thresholds
8. ✅ Document comprehensive reasoning or NO BET decision with WoM analysis

### Enhanced Strategy Execution Workflow with Weight of Money Monitoring
When a selection is identified:
1. ✅ Final cross-validation of all four data sources including Weight of Money
2. ✅ Verify Weight of Money score supports betting decision
3. ✅ Use ActivateBetfairMarketSelection for chosen horse
4. ✅ Execute appropriate strategy (ONLY use these exact strategy names):
   - **BACK bets**: Execute "Bet 10 Euro" strategy
   - **LAY bets**: Execute "Lay 10 Euro" strategy
   - **CRITICAL**: No other strategy names are valid or available
5. ✅ Confirm strategy execution successful
6. ✅ Monitor position with live market sentiment tracking AND Weight of Money changes
7. ✅ **Store enhanced analysis data using SetAIAgentDataContextForBetfairMarket**

### Enhanced Data Storage Workflow with Weight of Money (Required for ALL analyses)
After completing analysis (selection made OR NO BET decision):
1. ✅ Compile comprehensive multi-context analysis data including Weight of Money
2. ✅ Include cross-validation results and confidence scores across all four sources
3. ✅ Document market sentiment analysis, trading patterns, AND Weight of Money flows
4. ✅ Use SetAIAgentDataContextForBetfairMarket with:
   - **dataContextName**: "HorseRacingR5_Analysis"
   - **marketId**: [from active market]
   - **jsonData**: [Enhanced analysis JSON with Weight of Money - see Data Format section]
5. ✅ Confirm data storage successful for comprehensive four-source model tracking

### Enhanced NO BET Documentation Requirements

When recommending NO BET, the following is required:
1. ✅ A full analysis table for all horses.
2. ✅ Clear reasoning for the NO BET decision based on the strategy criteria.
3. ✅ **CRITICAL**: The analysis data for the race MUST be stored. Use `SetAIAgentDataContextForBetfairMarket` with `dataContextName`: "HorseRacingR5_Analysis". This step is mandatory for every race, including those with no bet.

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
✅ Enhanced EV > +15% (actual: +[X]%)
✅ Enhanced Form Score > 65 (actual: [X])
✅ AI Score > 80 OR Multi-Rating Diff > +15% (actual: +[X])
✅ Market Sentiment positive OR significant value confirmed
✅ All data sources consistent and validated

FULL RACE ANALYSIS TABLE:
| Horse | Current Price | Form Score | OR | RP | WoM Score | AI Score | Market Sentiment | Est. Prob % | True Odds | Market Odds | EV % | Action | Rating |
|---|---|---|---|---|---|---|---|---|---|---|---|---|---|
| **[Horse Name]** | **[Price]** | **[Form]** | **[OR]** | **[RP]** | **[WoM]** | **[AI]** | **[Sentiment]** | **[Prob]** | **[T. Odds]** | **[M. Odds]** | **[EV]** | **BACK** | ⭐⭐⭐⭐⭐ |
| [Other Horse 1] | [Price] | [Form] | [OR] | [RP] | [WoM] | [AI] | [Sentiment] | [Prob] | [T. Odds] | [M. Odds] | [EV] | NO BET | ⭐⭐ |
| [Other Horse 2] | [Price] | [Form] | [OR] | [RP] | [WoM] | [AI] | [Sentiment] | [Prob] | [T. Odds] | [M. Odds] | [EV] | NO BET | ⭐ |

STRATEGY EXECUTION:
✅ Selection activated: ActivateBetfairMarketSelection
✅ Strategy executed: [Bet 10 Euro / Lay 10 Euro] - ONLY these strategy names valid
✅ Position confirmed and monitored with live sentiment tracking
✅ Enhanced analysis data stored: SetAIAgentDataContextForBetfairMarket("HorseRacingR5_Analysis")
```

**Option 2: Enhanced No Selection Meets Criteria**
```
RACE ANALYSIS: [Venue] [Distance] [Class] - [Date]
Market ID: [Market ID from Step 1]

RECOMMENDATION: NO BET

ENHANCED REASONING FOR NO BET DECISION:
No selection met the enhanced criteria for a bet. See full analysis below.

ENHANCED CRITERIA FAILURES:
❌ EV Threshold: No horse meets +15% enhanced minimum for BACK bets or < -20% for LAY bets.
❌ Cross-Validation: Multiple data source inconsistencies detected for top candidates.
❌ Market Sentiment: Mixed signals insufficient for confident selection.

FULL RACE ANALYSIS TABLE:
| Horse | Current Price | Form Score | OR | RP | WoM Score | AI Score | Market Sentiment | Est. Prob % | True Odds | Market Odds | EV % | Action | Rating |
|---|---|---|---|---|---|---|---|---|---|---|---|---|---|
| [Horse A] | [Price] | [Form] | [OR] | [RP] | [WoM] | [AI] | [Sentiment] | [Prob] | [T. Odds] | [M. Odds] | [EV] | NO BET | ⭐⭐ |
| [Horse B] | [Price] | [Form] | [OR] | [RP] | [WoM] | [AI] | [Sentiment] | [Prob] | [T. Odds] | [M. Odds] | [EV] | NO BET | ⭐ |
| [Horse C] | [Price] | [Form] | [OR] | [RP] | [WoM] | [AI] | [Sentiment] | [Prob] | [T. Odds] | [M. Odds] | [EV] | NO BET | ⭐ |

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

**Note**: This enhanced framework leverages four comprehensive data sources including Weight of Money analysis to identify exceptional value opportunities with increased accuracy and confidence. The enhanced criteria and cross-validation requirements will result in higher NO BET percentages but significantly improved success rates when selections are made.

**CRITICAL REQUIREMENT**: ALL analyses must utilize GetAllDataContextForBetfairMarket with all four data contexts including "WeightOfMoneyData" and store results using SetAIAgentDataContextForBetfairMarket with dataContextName "HorseRacingR5_1_Analysis" for comprehensive multi-source model tracking and validation.

**Version**: R5.3 - Weight of Money Integration
**Last Updated**: July 4, 2025
**Strategy Type**: Advanced four-source value-based approach with professional betting pattern analysis
**Key Changes**: Added Weight of Money data integration, enhanced EV calculation with smart money factors, professional backing detection

## Enhanced Analysis Data Format for Storage

### JSON Structure for SetAIAgentDataContextForBetfairMarket - R5 Enhanced Format
**Note**: The `enhancedHorseAnalysis` array in the JSON below must include an object for **every horse** that ran in the race, not just the recommended selection or top candidates.

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
    "dataSourcesUsed": ["RacingpostDataForHorsesInfo", "HorsesBaseBetfairFormData", "MarketSelectionsTradedPricesData", "WeightOfMoneyData"],
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
      "weightOfMoneyAnalysis": {
        "womScore": 75,
        "professionalBackingStrength": 32,
        "smartMoneyTiming": 25,
        "institutionalConfidence": 18,
        "contraIndicatorSignals": 0,
        "publicVsProfessionalRatio": "65_35_pro_favored",
        "moneyFlowDirection": "professional_backing",
        "confidenceLevel": "high"
      },
      "aiAnalysis": {
        "formScore": 100,
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
    },
    {
      "selectionId": "23433961_0.00",
      "name": "Another Horse",
      "currentPrice": 8.5,
      "multiSourceFormAnalysis": {
        "combinedFormScore": 80
      },
      "multiSourceRatingAnalysis": {
        "averageExternalRating": 70
      },
      "marketSentimentAnalysis": {
        "sentimentScore": 60
      },
      "weightOfMoneyAnalysis": {
        "womScore": 65
      },
      "aiAnalysis": {
        "formScore": 85
      },
      "enhancedProbabilityCalculation": {
        "finalProbability": 15.0,
        "trueOdds": 6.67
      },
      "enhancedExpectedValue": {
        "expectedValue": 10.5,
        "classification": "no_value"
      },
      "enhancedCriteriaAssessment": {
        "overallAssessment": "no_bet"
      },
      "riskAssessment": {
        "overallRiskRating": "medium"
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
      "racingPostFormAnalysis": 87.3,
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
      "formConfidence": 89.6,
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
