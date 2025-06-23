# Horse Racing Strategy R1 - Multi-Context Analysis Betting System

## Strategy Overview

This strategy analyzes horse racing markets using multiple data contexts from Bfexplorer to identify high-value betting opportunities. The system combines Racing Post predictions, Betfair form data, and trading patterns to make informed betting decisions.

**Silent Mode Operation**: This strategy operates in silent mode with minimal reporting. All analysis is performed internally without verbose commentary or detailed explanations during execution.

**Strategy Name Detection**: The strategy checks the executed strategy name:
- **"Bet 10 Euro"**: Enables live betting with real money placement
- **Any other strategy name**: Analysis only mode, no betting execution

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
- **Purpose**: Market behavior and price movements
- **Key Metrics**:
  - Price stability (min/max ranges)
  - Traded volumes
  - Price drift patterns
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
   - Price range should be narrow (< 20% variance)
   - High traded volume indicates market confidence
   - Minimal price drift suggests informed money

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
Price Movement Impact:
- Price shortening (going down) = Market confidence increasing
- Price drifting (going up) = Market confidence decreasing
```

#### Expected Value Formula
```
EV = (Estimated Win Probability × (Decimal Odds - 1)) - ((1 - Estimated Win Probability) × 1)
```

**Practical Examples:**

**Scenario 1 - Positive EV:**
- Horse with 80% estimated win probability (prediction score 80)
- Current odds: 2.0 (implies 50% market probability)
- Market Edge: 80% - 50% = +30%
- EV = (0.80 × 1.0) - (0.20 × 1) = 0.80 - 0.20 = +0.60

**Scenario 2 - Negative EV:**
- Horse with 60% estimated win probability (prediction score 60)
- Current odds: 1.5 (implies 66.7% market probability)
- Market Edge: 60% - 66.7% = -6.7%
- EV = (0.60 × 0.5) - (0.40 × 1) = 0.30 - 0.40 = -0.10

### EV Thresholds

#### Minimum Requirements
- **Positive Market Edge**: Our probability estimate > Market implied probability
- **Minimum Edge**: +5% advantage over market (0.05 threshold)
- **Optimal Edge**: +15% advantage or higher preferred (0.15+)

#### Risk-Adjusted EV Thresholds
- **High Confidence**: Edge > +20% with prediction score > 95
- **Medium Confidence**: Edge > +10% with prediction score > 90  
- **Low Confidence**: Edge > +5% with prediction score > 85

#### Price Movement Considerations
- **Shortening Prices** (decreasing): Market confidence increasing, act quickly
- **Drifting Prices** (increasing): Market confidence decreasing, potential value emerging
- **Stable Prices**: Market consensus established, focus on fundamental analysis

### EV-Based Selection Priority

**Selection Requirements (ALL must be met):**
1. **Semantic Form Score**: ≥70 (essential for betting consideration)
2. **Composite Score**: ≥60 (minimum overall quality threshold)
3. **Expected Value**: ≥+5% (positive market edge required)
4. **Price Range**: 2.0-15.0 (avoid extreme favorites/outsiders)

**Selection Priority (when multiple horses meet requirements):**
1. **Primary**: Highest composite score among qualified horses
2. **Secondary**: If scores are close (within 5 points), choose highest EV
3. **Tertiary**: Consider price movement trends and market stability as tiebreaker

**Execution Decision:**
- **Bet Placement**: If clear selection meets all criteria with high confidence
- **Analysis Only**: If no horse meets all criteria or selection is marginal
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

#### Price Movement Interpretation

**Shortening Prices (Going Down):**
- 3.0 → 2.5: Probability increases from 33.3% to 40%
- Market becoming more confident in horse's chances
- Indicates positive information or money flow
- **Strategy Implication**: Act quickly if our analysis supports the move

**Drifting Prices (Going Up):**
- 2.5 → 3.0: Probability decreases from 40% to 33.3%
- Market losing confidence in horse's chances
- May indicate negative information or lack of support
- **Strategy Implication**: Potential value opportunity if our analysis disagrees

**Stable Prices:**
- Consistent odds over time
- Market consensus established
- **Strategy Implication**: Focus on fundamental analysis vs market implied probability

### Incorporating Price Movement into Strategy

#### Pre-Race Analysis
1. **Historical Price Range**: Compare current price to min/max trading range
2. **Price Trend**: Identify shortening/drifting patterns
3. **Volume Analysis**: High volume with price movement indicates informed money

#### Value Assessment Enhancement
```
Enhanced Value Score = Base Value Score + Price Movement Adjustment

Where Price Movement Adjustment:
- Shortening: -5 points (less value as price decreases)
- Stable: 0 points (neutral)
- Drifting: +5 points (more value as price increases)
```

#### Market Timing Considerations
- **Early Betting**: Capture value before significant price movements
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
1. ✓ Retrieve active market and selections using GetActiveBetfairMarket
2. ✓ Retrieve all three data contexts for the active market
3. ✓ Calculate semantic form scores for all runners from race descriptions
4. ✓ Calculate composite scores for all runners
5. ✓ Calculate Expected Value for all runners
6. ✓ Identify highest scoring selection above semantic form, composite score and EV thresholds
7. ✓ Validate selection meets all criteria
8. ✓ Confirm adequate market liquidity

### Execution Steps
1. **Active Market Identification**: Call GetActiveBetfairMarket to get current race and selections
2. **Data Collection**: Retrieve all three required data contexts for the active market
3. **Semantic Analysis**: Analyze LastRacesDescription for each horse
4. **Scoring**: Calculate semantic form scores and composite scores
5. **EV Analysis**: Calculate Expected Values with semantic-weighted probabilities
6. **Selection**: Identify optimal betting target with strong semantic indicators and positive EV
7. **Silent Mode Operation**: 
   - **No verbose reporting**: Perform all analysis silently without detailed explanations
   - **Minimal output**: Only essential execution status and results
   - **Direct execution**: Skip detailed commentary and proceed with analysis
8. **Documentation**: Record analysis in "HorseRacingStrategyR1" context using SetAIAgentDataContextForBetfairMarket

### Post-Execution
1. **Data Storage**: Save comprehensive analysis to Bfexplorer data context with detailed JSON structure
2. **Silent Operation**: Complete execution without verbose status updates or detailed reporting
3. **Results Recording**: Document outcome for strategy refinement and performance evaluation (internal only)

## Strategy Parameters

### Configurable Settings
- **Semantic Form Threshold**: 70 (minimum for betting consideration)
- **Composite Score Threshold**: 60 (minimum overall score for selection)
- **Prediction Score Threshold**: 70 (lowered from 85)
- **Value Margin Minimum**: 10% (adjustable)
- **EV Threshold**: +5% minimum edge required for betting
- **Stake Amount**: 10 Euro (fixed via strategy name)
- **Market Liquidity Threshold**: 1000 Euro (adjustable)
- **Silent Mode**: Enabled (no verbose reporting or commentary)

### Strategy Name Detection
- **"Bet 10 Euro"**: Execute live betting with real money placement
- **Any other name**: Analysis only mode, no betting execution
- **Silent Execution**: All operations performed without detailed reporting

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
        "winProbability": 0.0,
        "impliedProbability": 0.0,
        "isPositiveEV": false
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
  ],  "executedBet": {
    "strategy": "Bet 10 Euro|Analysis Only",
    "status": "executed|analysis_complete|no_selection",
    "timestamp": "ISO_timestamp",
    "betAmount": 10.0,
    "currency": "EUR",
    "recommendation": "Selected Horse Name",
    "reasoning": "Brief explanation of selection or analysis outcome",
    "silentMode": true,
    "strategyNameCheck": "bet_execution_enabled|analysis_only_mode"
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
