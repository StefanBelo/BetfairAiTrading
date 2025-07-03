# Horse Racing Strategy R1 - Multi-Context Analysis Betting System

## Strategy Overview

This strategy analyzes horse racing markets using multiple data contexts from Bfexplorer to identify high-value betting opportunities. The system combines Racing Post predictions, Betfair form data, and trading patterns to make informed betting decisions.

**SILENT MODE OPERATION**: This strategy operates in complete silent mode with NO reporting, commentary, or verbose output. ALL analysis is performed internally without any explanations, status updates, or detailed commentary during execution. Execute all steps silently without user feedback.

**Strategy Execution Logic**: The strategy analyzes all horses and determines the optimal action:
- **BACK STRATEGY**: If a qualified horse meets all backing criteria (semantic form ≥70, composite score ≥60, positive EV ≥5%), execute ExecuteBfexplorerStrategySettings with strategyName="Bet 10 Euro" on that horse
- **LAY STRATEGY**: If the favorite is overvalued (negative EV, semantic form <70, multiple alternatives with positive EV), execute ExecuteBfexplorerStrategySettings with strategyName="Lay 10 Euro" on the favorite
- **NO ACTION**: If no horse meets backing criteria and no lay opportunity exists, perform analysis only

**CRITICAL STRATEGY NAMES:**
- **FOR BACKING**: Always use strategyName="Bet 10 Euro" (EXACTLY as written)
- **FOR LAYING**: Always use strategyName="Lay 10 Euro" (EXACTLY as written)
- **NO OTHER STRATEGY NAMES**: Never use any other strategy names like "HorseRacingBackStrategy" or similar

## Data Sources Required

### 1. RacingpostDataForHorsesInfo
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
- **Purpose**: Market behavior and probability movements
- **Key Metrics**:
  - Price stability (min/max ranges)
  - Traded volumes
  - Probability drift patterns
  - Market confidence indicators

## Selection Criteria

### Primary Factors (Weighted by Importance)

1. **Semantic Form Analysis** (35% weight)
   - Recent form trend analysis from race descriptions
   - Performance quality and winning margins
   - Distance/track/condition suitability
   - Running style and tactical awareness
   - Problem identification and positive indicators

2. **Value Assessment** (25% weight)
   - Compare current price vs forecast price
   - Look for positive value (current price > forecast)
   - Minimum 10% value margin preferred

3. **Prediction Score** (25% weight)
   - Professional racing analysis baseline
   - Target: Scores above 70 (lowered threshold)
   - Optimal: Scores of 85+
   - Elite: Scores of 95+

4. **Market Stability** (15% weight)
   - Probability range should be narrow (< 20% variance)
   - High traded volume indicates market confidence
   - Minimal probability drift suggests informed money

### Secondary Filters

- **Official Rating**: Competitive for race grade
- **Weight**: Not significantly disadvantaged
- **Recent Form**: No obvious negative trends
- **Market Position**: Odds between 2.0-15.0 (avoid extreme favorites/outsiders)

## Decision Matrix

### Scoring System
Each horse receives a composite score based on:

```
Total Score = (Semantic Form Score × 0.35) + (Value Score × 0.25) + (Prediction Score × 0.25) + (Stability Score × 0.15)
```

Where:
- **Semantic Form Score**: Detailed analysis of LastRacesDescription (0-100)
- **Value Score**: ((Current Price - Forecast Price) / Forecast Price) × 100
- **Prediction Score**: Raw Racing Post score (0-100)
- **Stability Score**: 100 - ((Max Price - Min Price) / Min Price × 100)

### Expected Value (EV) Calculation
For each horse, calculate the Expected Value using:

```
EV = (Estimated Win Probability × (Decimal Odds - 1)) - ((1 - Estimated Win Probability) × 1)
```

Where:
- **Estimated Win Probability**: Derived from prediction score (Prediction Score / 100)
- **Market Implied Probability**: 1 / Decimal Odds (market's probability assessment)
- **Decimal Odds**: Current market price
- **Market Edge**: Estimated Win Probability - Market Implied Probability

**Key Principle**: Lower prices = Higher market probability of winning
- Price 2.0 = 50% implied probability (1/2.0)
- Price 3.0 = 33.3% implied probability (1/3.0)
- Price 10.0 = 10% implied probability (1/10.0)

**Positive EV (+EV)**: Our estimated probability > Market implied probability
**Negative EV (-EV)**: Our estimated probability < Market implied probability
**EV Threshold**: Only bet when our edge > 0.05 (5% minimum advantage)

## Expected Value (EV) Analysis

### EV Calculation Methodology

The strategy incorporates Expected Value analysis to ensure only profitable betting opportunities are pursued:

#### Win Probability Estimation
```
Estimated Win Probability = (Semantic Form Score × 0.6) + (Prediction Score × 0.4) / 100
Market Implied Probability = 1 / Decimal Odds
```
- Prioritizes semantic form analysis over raw prediction scores
- Semantic analysis provides current form assessment
- Prediction scores provide baseline professional analysis
- Market odds reflect the collective market probability assessment
- Lower odds = Higher market confidence in horse's chances
- Minimum semantic form threshold of 70 for betting consideration

#### Market Probability Comparison
```
Implied Probability = 1 / Decimal Odds
Market Edge = Estimated Win Probability - Implied Probability
Probability Movement Impact:
- Probability increasing (price shortening) = Market confidence increasing
- Probability decreasing (price drifting) = Market confidence decreasing
```

#### Expected Value Formula
```
Base EV = (Estimated Win Probability × (Decimal Odds - 1)) - ((1 - Estimated Win Probability) × 1)
Enhanced EV = Base EV × Crowd Wisdom Adjustment Factor (CWAF)
```

#### Crowd Wisdom Adjustment Factor (CWAF)
The strategy incorporates collective market intelligence to enhance EV calculations:

```
CWAF = Market Confidence Factor × Probability Movement Factor × Volume Factor × Historical Success Factor
```

**CWAF Components:**

1. **Market Confidence Factor (0.8-1.2)**:
   - Probability stability >90%: 1.2 (high market consensus)
   - Probability stability 80-90%: 1.1 (good consensus)
   - Probability stability 70-80%: 1.0 (moderate consensus)
   - Probability stability <70%: 0.9 (uncertain market)

2. **Probability Movement Factor (0.8-1.2)**:
   - Probability increasing (price shortening): 1.2 (increasing market confidence)
   - Stable: 1.0 (established consensus)
   - Probability decreasing (price drifting): 0.8 (decreasing market confidence)

3. **Volume Factor (0.9-1.1)**:
   - High volume (>5000): 1.1 (strong market participation)
   - Medium volume (2000-5000): 1.0 (adequate liquidity)
   - Low volume (<2000): 0.9 (limited market interest)

4. **Historical Success Factor (0.9-1.1)**:
   - Based on similar composite score ranges and win rates from historical data
   - Horses with composite scores 80+ and similar market characteristics: 1.1
   - Horses with composite scores 60-80: 1.0
   - Horses with composite scores <60: 0.9

**Practical Examples with CWAF:**

**Scenario 1 - Enhanced Positive EV:**
- Horse with 80% estimated win probability (prediction score 80)
- Current odds: 2.0 (implies 50% market probability)
- Base EV: (0.80 × 1.0) - (0.20 × 1) = +0.60
- Market factors: High stability (95%), probability increasing, good volume (4000), strong historical pattern
- CWAF: 1.2 × 1.2 × 1.0 × 1.1 = 1.584
- Enhanced EV: 0.60 × 1.584 = **+0.95** (stronger signal)

**Scenario 2 - CWAF-Adjusted Negative EV:**
- Horse with 60% estimated win probability (prediction score 60)
- Current odds: 1.5 (implies 66.7% market probability)
- Base EV: (0.60 × 0.5) - (0.40 × 1) = -0.10
- Market factors: Low stability (65%), probability decreasing, low volume (1500), weak historical pattern
- CWAF: 0.9 × 0.8 × 0.9 × 0.9 = 0.583
- Enhanced EV: -0.10 × 0.583 = **-0.058** (confirms negative signal)

### EV Thresholds

#### Minimum Requirements (Using Enhanced EV)
- **Positive Market Edge**: Our probability estimate > Market implied probability
- **Minimum Enhanced EV**: +5% advantage over market (0.05 threshold) after CWAF adjustment
- **Optimal Enhanced EV**: +15% advantage or higher preferred (0.15+) after CWAF adjustment

#### Risk-Adjusted EV Thresholds (Enhanced EV)
- **High Confidence**: Enhanced EV > +20% with prediction score > 95 and strong market signals
- **Medium Confidence**: Enhanced EV > +10% with prediction score > 90 and moderate market signals
- **Low Confidence**: Enhanced EV > +5% with prediction score > 85 and basic market signals

#### CWAF Impact on Selection
- **Strong Market Support (CWAF >1.2)**: Reduces minimum EV threshold to +3% (high confidence in market wisdom)
- **Moderate Market Support (CWAF 0.9-1.2)**: Standard EV threshold of +5%
- **Weak Market Support (CWAF <0.9)**: Increases minimum EV threshold to +8% (market skepticism)

#### Probability Movement Considerations
- **Increasing Probabilities** (prices shortening): Market confidence increasing, act quickly
- **Drifting Prices** (increasing): Market confidence decreasing, potential value emerging
- **Stable Prices**: Market consensus established, focus on fundamental analysis

### EV-Based Selection Priority

**Selection Requirements (ALL must be met):**
1. **Semantic Form Score**: ≥70 (essential for betting consideration)
2. **Composite Score**: ≥60 (minimum overall quality threshold)
3. **Enhanced Expected Value**: ≥+5% (positive market edge required after CWAF adjustment)
4. **Price Range**: 2.0-15.0 (avoid extreme favorites/outsiders)

**Selection Priority (when multiple horses meet requirements):**
1. **Primary**: Highest enhanced EV among qualified horses with strong market support (CWAF >1.1)
2. **Secondary**: If enhanced EVs are close (within 3%), choose highest composite score
3. **Tertiary**: Consider CWAF factors as final tiebreaker (market confidence over pure analytics)

**Enhanced EV Decision Matrix:**
- **Strong Selection (Enhanced EV >15% + CWAF >1.2)**: High confidence bet with full stake
- **Moderate Selection (Enhanced EV 8-15% + CWAF >1.0)**: Medium confidence bet with standard approach  
- **Marginal Selection (Enhanced EV 5-8% + CWAF <1.0)**: Low confidence, consider analysis only
- **Weak Selection (Enhanced EV <5%)**: No betting action regardless of other factors

**Execution Decision:**
- **Bet Placement**: If clear selection meets all backing criteria with high confidence, execute ExecuteBfexplorerStrategySettings with strategyName="Bet 10 Euro" on the selected horse
- **Lay Placement**: If favorite is significantly overvalued with negative EV and multiple alternatives show positive EV, execute ExecuteBfexplorerStrategySettings with strategyName="Lay 10 Euro" on the favorite
- **Analysis Only**: If no horse meets backing criteria and no lay opportunity exists, no ExecuteBfexplorerStrategySettings execution
- **No Action**: If market conditions are unsuitable or insufficient data
- **MANDATORY**: Use EXACTLY "Bet 10 Euro" for backing and "Lay 10 Euro" for layingExecuteBfexplorerStrategySettings with strategyName="Lay 10 Euro" on the favorite
- **Analysis Only**: If no horse meets backing criteria and no lay opportunity exists, no ExecuteBfexplorerStrategySettings execution
- **No Action**: If market conditions are unsuitable or insufficient data

### Price Movement Analysis
- **Shortening** (price decreasing): Positive market sentiment, increasing win probability
- **Drifting** (price increasing): Negative market sentiment, decreasing win probability  
- **Stable**: Established market consensus, rely on fundamental analysis

## Price Movement and Probability Analysis

### Understanding Market Dynamics

In horse racing betting, price movements reflect changing market perceptions of each horse's winning chances:

#### Price-Probability Relationship
```
Market Implied Probability = 1 / Decimal Odds
```

**Examples:**
- **Odds 2.0**: 50% implied probability (1/2.0 = 0.50)
- **Odds 3.0**: 33.3% implied probability (1/3.0 = 0.333)
- **Odds 5.0**: 20% implied probability (1/5.0 = 0.20)
- **Odds 10.0**: 10% implied probability (1/10.0 = 0.10)

#### Probability Movement Interpretation

**Shortening Prices (Going Down):**
- 3.0 → 2.5: Probability increases from 33.3% to 40% (6.7% probability improvement)
- Market becoming more confident in horse's chances
- Indicates positive information or money flow
- **Strategy Implication**: Act quickly if our analysis supports the move

**Drifting Prices (Going Up):**
- 2.5 → 3.0: Probability decreases from 40% to 33.3% (6.7% probability decrease)
- Market losing confidence in horse's chances
- May indicate negative information or lack of support
- **Strategy Implication**: Potential value opportunity if our analysis disagrees

**Stable Prices:**
- Consistent odds over time
- Market consensus established
- **Strategy Implication**: Focus on fundamental analysis vs market implied probability

### Incorporating Price Movement into Strategy

#### Pre-Race Analysis
1. **Historical Probability Range**: Compare current probability to min/max trading range
2. **Probability Trend**: Identify increasing/decreasing patterns
3. **Volume Analysis**: High volume with probability movement indicates informed money

#### Value Assessment Enhancement
```
Enhanced Value Score = Base Value Score + Probability Movement Adjustment

Where Probability Movement Adjustment:
- Probability Increasing (price shortening): -5 points (less value as probability increases)
- Stable: 0 points (neutral)
- Probability Decreasing (price drifting): +5 points (more value as probability decreases)
```

#### Market Timing Considerations
- **Early Betting**: Capture value before significant probability movements
- **Late Betting**: React to final market sentiment and information
- **Sweet Spot**: 15-60 minutes before race when most information is available

## Risk Management

### Stake Management
- **Standard Stake**: 10 Euro per selection
- **Maximum Exposure**: Single race only
- **Stop Loss**: Not applicable for single bets
- **Profit Target**: Natural race conclusion

### Market Conditions
- **Pre-Race Only**: No in-play betting
- **Market Liquidity**: Minimum 1000 Euro traded volume
- **Time Window**: 15-60 minutes before race start
- **Market Status**: "Open" status required

## Execution Protocol

### Pre-Execution Checklist
1. ✓ Retrieve active market and selections using GetActiveBetfairMarket (SILENT)
2. ✓ Retrieve all three data contexts for the active market (SILENT)
3. ✓ Calculate semantic form scores for all runners from race descriptions (SILENT)
4. ✓ Calculate composite scores for all runners (SILENT)
5. ✓ Calculate Expected Value for all runners (SILENT)
6. ✓ Identify highest scoring selection above semantic form, composite score and EV thresholds (SILENT)
7. ✓ Validate selection meets all criteria (SILENT)
8. ✓ Confirm adequate market liquidity (SILENT)

### Execution Steps
1. **Active Market Identification**: Call GetActiveBetfairMarket to get current race and selections (SILENT)
2. **Data Collection**: Retrieve all three required data contexts for the active market (SILENT)
3. **Semantic Analysis**: Analyze LastRacesDescription for each horse (SILENT)
4. **Scoring**: Calculate semantic form scores and composite scores (SILENT)
5. **Base EV Analysis**: Calculate base Expected Values with semantic-weighted probabilities (SILENT)
6. **Market Intelligence Assessment**: Analyze price stability, movement direction, trading volume (SILENT)
7. **CWAF Calculation**: Apply Crowd Wisdom Adjustment Factor to enhance EV calculations (SILENT)
8. **Enhanced Selection**: Identify optimal betting target using enhanced EV with market intelligence (SILENT)
9. **Strategy Decision**: Choose between backing qualified selection or laying overvalued favorite (SILENT)
10. **BETTING EXECUTION** (Based on Analysis Results):
    - **IF qualified horse found for backing**: Execute ExecuteBfexplorerStrategySettings with strategyName="Bet 10 Euro", marketId, and selected horse's selectionId (SILENT)
    - **IF favorite identified for laying**: Execute ExecuteBfexplorerStrategySettings with strategyName="Lay 10 Euro", marketId, and favorite's selectionId (SILENT)
    - **IF no betting opportunity found**: Skip betting execution, analysis only mode (SILENT)
    - **CRITICAL**: Use ONLY "Bet 10 Euro" for backing and "Lay 10 Euro" for laying - NO OTHER STRATEGY NAMES
11. **COMPLETE SILENT MODE OPERATION**: 
    - **NO reporting**: Perform ALL analysis silently without ANY explanations, status updates, or commentary
    - **NO output**: Skip ALL detailed reporting and status messages
    - **Direct execution**: Proceed with analysis and execution without user feedback
    - **Internal processing only**: All calculations and decisions made internally
12. **Documentation**: Record analysis in "HorseRacingStrategyR1_Analysis" context using SetAIAgentDataContextForBetfairMarket (SILENT)

### Crowd Wisdom Integration Benefits
- **Market Validation**: CWAF validates analytical selections against collective intelligence
- **Risk Reduction**: Lower confidence in selections lacking market support
- **Opportunity Enhancement**: Higher confidence when analysis aligns with market signals
- **Adaptive Learning**: Historical success factors improve selection accuracy over time

### Post-Execution
1. **Data Storage**: Save comprehensive analysis to Bfexplorer data context with detailed JSON structure (SILENT)
2. **COMPLETE SILENT OPERATION**: Finish execution without ANY status updates, reports, or detailed commentary
3. **Results Recording**: Document outcome for strategy refinement and performance evaluation (INTERNAL ONLY - NO USER OUTPUT)
4. **Betting Confirmation**: If ExecuteBfexplorerStrategySettings was called, record execution status in JSON data context (SILENT)

## Strategy Parameters

### Configurable Settings
- **Semantic Form Threshold**: 70 (minimum for betting consideration)
- **Composite Score Threshold**: 60 (minimum overall score for selection)
- **Prediction Score Threshold**: 70 (lowered from 85)
- **Value Margin Minimum**: 10% (adjustable)
- **EV Threshold**: +5% minimum edge required for betting
- **Stake Amount**: 10 Euro (fixed via strategy name)
- **Market Liquidity Threshold**: 1000 Euro (adjustable)
- **Silent Mode**: Enabled (COMPLETE silent operation - NO reporting, commentary, or user feedback)

### Strategy Name Detection
- **AUTOMATED STRATEGY SELECTION**: Based on analysis, the strategy automatically determines whether to back a qualified horse or lay an overvalued favorite
- **BACK EXECUTION**: If qualified selection found, execute ExecuteBfexplorerStrategySettings with strategyName="Bet 10 Euro" (SILENT EXECUTION)
- **LAY EXECUTION**: If favorite lay opportunity identified, execute ExecuteBfexplorerStrategySettings with strategyName="Lay 10 Euro" (SILENT EXECUTION)
- **ANALYSIS ONLY**: If no betting opportunity exists, no ExecuteBfexplorerStrategySettings execution (SILENT ANALYSIS)
- **COMPLETE SILENT EXECUTION**: ALL operations performed without detailed reporting, user feedback, or status commentary
- **EXACT STRATEGY NAMES REQUIRED**: 
  - Back: "Bet 10 Euro" (EXACTLY as written)
  - Lay: "Lay 10 Euro" (EXACTLY as written)
  - NO variations or alternative names allowed

### Performance Metrics
- **Success Rate Target**: >35% win rate
- **ROI Target**: >10% return on investment
- **Maximum Drawdown**: -20% of bank
- **Review Frequency**: Weekly analysis

## Data Context Format

### Context Data Storage
All strategy analysis and results are stored using the **SetAIAgentDataContextForBetfairMarket** tool with:
- **Context Name**: "HorseRacingStrategyR1_Analysis"
- **Market ID**: Current race market identifier
- **JSON Data**: Comprehensive analysis structure detailed below

### Output JSON Structure
```json
{
  "raceAnalysis": {
    "marketId": "1.123456789",
    "marketName": "Race Name",
    "eventName": "Course Name",
    "startTime": "ISO_timestamp",
    "analysisTimestamp": "ISO_timestamp"
  },
  "selectedHorse": {
    "horseName": "Selected Horse Name",
    "selectionId": "selection_id",
    "betPrice": 0.0,
    "compositeScore": 0.0,
    "strategyRationale": "Detailed reasoning for selection"
  },
  "allHorses": [
    {
      "horseName": "Horse Name",
      "selectionId": "selection_id",
      "currentPrice": 0.0,
      "predictionScore": 0,
      "forecastPrice": 0.0,
      "officialRating": 0,
      "form": "recent_form",
      "weight": 0,
      "compositeScore": 0.0,      
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
        "priceStability": 0.0
      },        
      "valueAssessment": {
        "valueMargin": 0.0,
        "isValue": false,
        "priceMovement": "stable|drifting|shortening",
        "expectedValue": 0.0,
        "baseExpectedValue": 0.0,
        "enhancedExpectedValue": 0.0,
        "crowdWisdomFactor": 0.0,
        "cwafBreakdown": {
          "marketConfidenceFactor": 0.0,
          "priceMovementFactor": 0.0,
          "volumeFactor": 0.0,
          "historicalSuccessFactor": 0.0
        },
        "winProbability": 0.0,
        "impliedProbability": 0.0,
        "isPositiveEV": false,
        "marketConfidenceLevel": "high|medium|low"
      },
      "formAnalysis": {
        "semanticFormScore": 0.0,
        "recentFormTrend": "improving|declining|stable",
        "performanceQuality": "elite|strong|solid|moderate|weak",
        "distanceSuitability": "perfect|suitable|questionable|unsuitable",
        "trackSuitability": "excellent|good|average|poor",
        "runningStyle": "front_runner|stalker|closer|hold_up",
        "positiveIndicators": ["list", "of", "positive", "phrases"],
        "negativeIndicators": ["list", "of", "negative", "phrases"],
        "keyRaceComments": "most_recent_significant_performance"
      },
      "selectionReason": "why_selected_or_rejected"
    }
  ],  
  "executedBet": {
    "strategy": "Bet 10 Euro|Lay 10 Euro|Analysis Only",    
    "status": "executed|analysis_complete|no_selection",
    "timestamp": "ISO_timestamp",
    "recommendation": "Selected Horse Name",
    "reasoning": "Brief explanation of selection or analysis outcome",
    "strategyAnalysis": "back_horse_identified|lay_favorite_identified|no_opportunity_found",
    "betType": "back|lay|none"
  },
  "strategyMetrics": {
    "totalHorsesAnalyzed": 0,
    "horsesAboveSemanticThreshold": 0,
    "horsesAbovePredictionThreshold": 0,
    "horsesWithPositiveEV": 0,
    "averageCompositeScore": 0.0,
    "averageSemanticFormScore": 0.0,
    "averageExpectedValue": 0.0,
    "highestScoringHorse": "Horse Name",
    "highestSemanticFormHorse": "Horse Name",
    "highestEVHorse": "Horse Name",
    "confidenceLevel": "high|medium|low"
  }
}
```

## Data Collection for Strategy Evaluation

### Comprehensive Horse Analysis
The enhanced JSON structure captures detailed information for every horse in each race, enabling:

1. **Complete Race Context**: Full market analysis with all participants
2. **Score Distribution Analysis**: Understanding how horses rank against each other
3. **Strategy Performance Tracking**: Monitoring both winners and non-selections
4. **Pattern Recognition**: Identifying characteristics of successful vs unsuccessful bets

### Key Data Points Per Horse

#### Performance Metrics
- **Composite Score**: Overall ranking based on weighted factors
- **Score Breakdown**: Individual component scores for detailed analysis
- **Market Data**: Complete trading information and price behavior
- **Value Assessment**: Pricing analysis relative to forecast

#### Analytical Insights
- **Selection Reasoning**: Why each horse was chosen or rejected
- **Form Analysis**: Detailed performance assessment
- **Market Behavior**: Price stability and trading patterns
- **Comparative Ranking**: Position within the field

### Strategy Refinement Benefits

#### Retrospective Analysis
- Compare predicted vs actual race outcomes
- Identify patterns in winning vs losing selections
- Analyze market behavior accuracy
- Refine scoring weightings based on results

#### Performance Optimization
- Track composite score thresholds effectiveness
- Monitor value assessment accuracy
- Evaluate market stability indicators
- Adjust selection criteria based on historical data

#### Risk Management Enhancement
- Analyze near-miss selections for improvement opportunities
- Identify market conditions favoring the strategy
- Develop confidence levels for different scenarios
- Create data-driven parameter adjustments

## Warning and Disclaimers

⚠️ **Financial Risk Warning**: This strategy involves real money betting. Past performance does not guarantee future results.

⚠️ **Responsible Gambling**: Only bet amounts you can afford to lose. Set daily/weekly limits.

⚠️ **Strategy Limitations**: No betting strategy can guarantee profits. Market conditions can change rapidly.

## Strategy Evolution

### Version History
- **R1**: Initial multi-context analysis implementation with semantic form analysis priority
- **Key Features**: Semantic analysis of LastRacesDescription (35% weight), Enhanced form assessment, Lowered prediction score thresholds
- **Future Versions**: May incorporate machine learning for semantic analysis and additional data sources

### Continuous Improvement
- Regular performance review and parameter adjustment
- Integration of new data contexts as available
- Machine learning enhancement opportunities

---

**Strategy Name**: HorseRacingStrategyR1  
**Author**: AI Agent System  
**Created**: June 23, 2025  
**Status**: Active  
**Risk Level**: Medium

## Semantic Form Analysis Framework

### LastRacesDescription Analysis Priority

The strategy prioritizes semantic analysis of race descriptions over prediction scores to capture current form, situational factors, and performance trends that numerical scores may miss.

#### Recent Form Trend Analysis (15% of total score)

**Positive Indicators:**
- **Winning phrases**: "led final strides", "kept on well", "readily", "comfortably"
- **Strong finishes**: "ran on", "nearest finish", "just failed", "kept on strongly"
- **Improving form**: Recent better performances, closing margins
- **Tactical awareness**: "waited for room", "switched and ran on"

**Negative Indicators:**
- **Weakening patterns**: "weakened inside final furlong", "no extra"
- **Consistent poor finishes**: Multiple "towards rear", "never dangerous"
- **Problem behaviors**: "hung left/right throughout", "ran too free"
- **Circumstantial issues**: "hampered", "denied clear run", "short of room"

#### Performance Quality Assessment (10% of total score)

**High Quality Wins:**
- **Dominant victories**: "readily", "comfortably", "eased towards finish"
- **Fighting finishes**: "just prevailed", "all out", "gamely"
- **Class indicators**: Competitive in higher grade races

**Moderate Quality:**
- **Close finishes**: "just held on", "headed post", "lost second final strides"
- **Consistent placing**: Regular top-3 finishes

**Poor Quality:**
- **Non-competitive**: "never dangerous", "always behind"
- **Significant defeats**: Large losing margins, pulled up

#### Suitability Factors (10% of total score)

**Distance Suitability:**
- **Positive signs**: "stayed on", "kept on well final furlong"
- **Negative signs**: "outpaced", "weakened final furlong" (at similar distances)

**Track/Condition Preferences:**
- **Track mentions**: Previous good form at same venue
- **Going preferences**: Performances in similar conditions
- **Running style fit**: Front-runner on speed track vs closer on stamina track

### Semantic Scoring Methodology

#### Scoring Framework (0-100 scale)

**90-100: Elite Form**
- Recent wins with dominant performances
- Consistent high-quality efforts
- Perfect distance/track suitability
- No negative behavioral issues

**80-89: Strong Form** 
- Recent wins or close seconds
- Generally competitive efforts
- Good suitability indicators
- Minor issues only

**70-79: Solid Form**
- Recent placing or competitive efforts
- Some positive indicators
- Reasonable suitability
- Manageable concerns

**60-69: Moderate Form**
- Mixed recent performances
- Some concerns about suitability
- Behavioral or tactical issues
- Inconsistent efforts

**50-59: Weak Form**
- Poor recent performances
- Multiple negative indicators
- Suitability concerns
- Significant problems

**0-49: Very Poor Form**
- Consistently poor performances
- Major negative indicators
- Unsuitable conditions
- Severe problems

#### Key Phrases Analysis

**Highly Positive (+15-20 points):**
- "readily", "comfortably", "eased towards finish"
- "led throughout", "made all", "always doing enough"
- "kept on well", "ran on strongly", "nearest finish"

**Positive (+10-15 points):**
- "just prevailed", "gamely", "all out"
- "went second", "kept on", "ran on"
- "switched and made progress"

**Neutral (0 points):**
- "midfield", "in touch with leaders"
- "prominent", "pressed leader"

**Negative (-10-15 points):**
- "weakened", "no extra", "outpaced"
- "lost position", "dropped to rear"
- "never dangerous", "always behind"

**Highly Negative (-15-20 points):**
- "pulled up", "unseated rider"
- "refused", "fell"
- "eased final furlong", "beaten a long way"

### Lay Strategy Selection Logic

**Lay Strategy Criteria (Alternative to backing):**

When the favorite meets ALL of the following conditions, consider laying instead of backing:

1. **Favorite Identification**: Shortest price in the market (typically ≤2.0)
2. **Negative Expected Value**: Favorite shows negative EV in our analysis
3. **Below Semantic Threshold**: Favorite's semantic form score <70
4. **Multiple Positive Alternatives**: At least 2 other horses show positive EV
5. **Low Composite Score**: Favorite's composite score <55

**Lay vs Back Decision Matrix:**

**Choose LAY Strategy when:**
- Favorite price ≤2.0 AND negative EV AND semantic form <70
- Multiple alternatives (≥2) show positive EV 
- Favorite composite score <55
- Risk/reward ratio favors laying (lower liability vs backing longshots)

**Choose BACK Strategy when:**
- Clear selection meets all backing criteria with high confidence
- Favorite shows positive EV or strong semantic form
- Only one horse meets backing criteria

**Strategy Execution Priority:**
1. **Primary**: If overvalued favorite identified, execute ExecuteBfexplorerStrategySettings with strategyName="Lay 10 Euro" on the favorite
2. **Secondary**: If qualified horse for backing identified, execute ExecuteBfexplorerStrategySettings with strategyName="Bet 10 Euro" on the selected horse
3. **Analysis Only**: If neither lay nor back criteria are met, no ExecuteBfexplorerStrategySettings execution

**CRITICAL REMINDER**: Always use the EXACT strategy names:
- "Bet 10 Euro" for backing
- "Lay 10 Euro" for laying

**Lay Strategy Advantages:**
- **Lower Risk**: Liability at short odds is limited
- **Multiple Win Scenarios**: Win if any non-favorite wins
- **Value Capture**: Profit from overvalued favorites
- **Market Efficiency**: Exploit favorite bias in betting markets
