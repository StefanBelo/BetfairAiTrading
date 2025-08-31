# Betfair Market Analysis - Candlestick Data R2

## Objective
Analyze Betfair market candlestick data using probability-based metrics to identify high-confidence trading opportunities and execute optimal automated strategies.

## ⚠️ CRITICAL: Probability-Based Analysis Framework
**ALL analysis must be probability-focused, NOT price-focused:**

- **STEAM** = Probability INCREASING (price decreasing) = More money backing = Confidence rising
- **DRIFT** = Probability DECREASING (price increasing) = Money leaving = Confidence falling
- **Primary Metric**: Probability change in percentage points (pp)
- **Price movements are secondary** and only used to calculate probability changes

```javascript
// FUNDAMENTAL CALCULATIONS
Probability = (1 / Price) × 100
STEAM: Probability_Change > 0 (price going DOWN)
DRIFT: Probability_Change < 0 (price going UP)
```

## Data Source & Configuration
- **Data Context**: "MarketSelectionsCandleStickData" from active Betfair market
- **Output Mode**: Choose ONE configuration based on analysis needs:

```javascript
// Quick Decision - Essential insights only
ENABLE_SUMMARY_ONLY = true

// Comparative Analysis - Table view for selection comparison  
ENABLE_TABLE_REPORT = true, ENABLE_DETAILED_REPORT = false

// Comprehensive Analysis - Full detailed breakdown
ENABLE_TABLE_REPORT = true, ENABLE_DETAILED_REPORT = true

// Deep Dive - Individual selection focus
ENABLE_TABLE_REPORT = false, ENABLE_DETAILED_REPORT = true
```

## Core Analysis Framework

### 1. Market Context Analysis
- **Market Details**: Event, race name, start time, status, selections count
- **Time Context**: Minutes to start, market maturity phase
- **Market Structure**: Overround percentage, liquidity assessment

### 2. Probability-Based Selection Metrics

#### Probability-Based Market Dynamics
```javascript
// Core Calculations (ALL analysis based on probability changes)
Implied_Probability = (1 / Decimal_Price) × 100
Open_Probability = (1 / Open_Price) × 100
Current_Probability = (1 / Current_Price) × 100
Probability_Change = Current_Probability - Open_Probability  
Probability_Velocity = Probability_Change / Time_Interval_Minutes
Probability_Volatility = ((High_Prob - Low_Prob) / Current_Prob) × 100

// Signal Definitions (probability-based)
STEAM = Probability_Change > 0 (probability increasing, price decreasing)
DRIFT = Probability_Change < 0 (probability decreasing, price increasing)
```

**Key Metrics Per Selection:**
- **Current Probability**: Primary metric (derived from current price)
- **Probability Change**: Movement in percentage points from open (CORE SIGNAL INDICATOR)
- **Probability Velocity**: Rate of probability change per minute (momentum strength)
- **Probability Volatility**: Percentage volatility of probability range
- **Market Direction**: STEAMING (+prob) / DRIFTING (-prob) / STABLE with momentum strength

#### Volume Intelligence
- **Volume Profile**: Average, recent trend, liquidity depth
- **Volume-Probability Correlation**: Confirming/Diverging patterns
- **Volume Change**: Percentage change from baseline

#### Technical Levels (Probability-Based)
- **Probability Support/Resistance**: Key probability boundaries from candlestick range
- **Market Position**: Current probability relative to High/Mid/Low probability range
- **Breakout Potential**: Proximity to technical probability levels (not price levels)

## Signal Detection Framework

### High-Confidence Signals (>70% reliability)
```javascript
// Steam Signal - Strong backing with probability increase (price decrease)
Criteria: Probability_Change > +3pp AND Volume_Trend > +20% AND Consistency > 3_intervals
// Note: Probability increase means price decreasing (more money backing selection)

// Late Drift - Market confidence decline near start (probability decrease)
Criteria: Probability_Change < -5pp AND Volume_Trend < -15% AND Time < 5min
// Note: Probability decrease means price increasing (money leaving selection)

// Value Opportunity - Significant probability divergence from expected  
Criteria: ABS(Probability_Change) > 8pp AND Volume_Support > baseline

// Steam Breakout - Probability surge through resistance with volume
Criteria: Probability_Breaks_Above_Resistance AND Volume_Trend > +15% AND Technical_Confirmation
// Note: Probability breakout = price breaking DOWN through support level

// False Favorite - Market leader showing probability weakness
Criteria: Market_Leader AND Probability_Change < -4pp AND Volume_Trend < -10%
// Note: Probability decrease in favorite = price increasing (confidence loss)

// Resistance Test - Probability approaching resistance with stalling
Criteria: Approaching_Probability_Resistance AND Probability_Momentum_Stalling AND Volume_Divergence
```

### Signal Confidence Scoring
```javascript
function calculateConfidence(signal) {
    let confidence = 50; // Base confidence
    
    // Probability momentum strength (max +25)
    if (Math.abs(signal.probabilityChange) > 5) confidence += 25;
    else if (Math.abs(signal.probabilityChange) > 3) confidence += 15;
    else if (Math.abs(signal.probabilityChange) > 2) confidence += 10;
    
    // Volume confirmation (max +20)  
    if (signal.volumeTrend > 20) confidence += 20;
    else if (signal.volumeTrend > 10) confidence += 10;
    
    // Consistency factor - probability movement sustained (max +15)
    if (signal.consistentProbabilityDirection > 3) confidence += 15;
    else if (signal.consistentProbabilityDirection > 2) confidence += 10;
    
    // Time factor (max +10)
    if (signal.timeToStart > 5) confidence += 10;
    else if (signal.timeToStart > 3) confidence += 5;
    
    // Volume divergence penalty (max -20)
    if (signal.volumeDivergence) confidence -= 20;
    
    return Math.min(confidence, 100);
}
```

## Strategy Execution Framework

### Strategy Selection Matrix
| Signal Type | Confidence | Odds Range | Strategy | Execution Type | Probability Direction |
|------------|------------|------------|----------|----------------|---------------------|
| Steam | >75% | 1.5-15.0 | Steam momentum trading | Manual Setup | INCREASING (+pp) |
| Drift | >70% | 1.8-50.0 | Drift momentum trading | Manual Setup | DECREASING (-pp) |
| Steam Breakout | >70% | 1.2-8.0 | Back scalping strategy | Automated | INCREASING (+pp) |
| Value | >70% | 2.0-20.0 | Back hedge strategy | Automated | Variable |
| False Favorite | >75% | 1.5-6.0 | Lay fade strategy | Automated | DECREASING (-pp) |
| Resistance Test | >70% | 2.0-25.0 | Lay resistance trading | Automated | DECREASING (-pp) |

### Execution Criteria Validation
**All strategies require:**
- ✅ **Signal Confidence** meets threshold
- ✅ **Odds Range** within strategy parameters  
- ✅ **Time Remaining** >3min (back) or >2min (lay)
- ✅ **Liquidity Check** sufficient for position size
- ✅ **Volume Confirmation** (no major divergences for high-confidence signals)

### Automated Strategy Parameters
```javascript
// Dynamic parameter calculation
function calculateParameters(signal, odds, confidence, timeToStart, volatility) {
    const baseStake = confidence > 80 ? 2.0 : confidence > 70 ? 1.5 : 1.0;
    const riskMultiplier = volatility > 20 ? 0.7 : volatility < 10 ? 1.3 : 1.0;
    const timeMultiplier = timeToStart > 5 ? 1.0 : timeToStart > 3 ? 0.8 : 0.6;
    
    const stakePercent = baseStake * riskMultiplier * timeMultiplier;
    
    return {
        "OpenBetPosition.Stake": calculateStakeAmount(stakePercent),
        "CloseBetPosition.Profit": odds < 3 ? 4 : odds < 8 ? 3 : 2,
        "CloseBetPosition.Loss": volatility > 15 ? 25 : 30
    };
}
```

### Available Strategy Functions
```javascript
// Manual Setup Strategies (Momentum with preset trailing stops)
ExecuteBfexplorerStrategySettings("Steam momentum trading", marketId, selectionId)
ExecuteBfexplorerStrategySettings("Drift momentum trading", marketId, selectionId)

// Automated Strategies (with configurable parameters)
ExecuteBfexplorerStrategySettingsWithParameters("Back scalping strategy", marketId, selectionId, parameters)
ExecuteBfexplorerStrategySettingsWithParameters("Back hedge strategy", marketId, selectionId, parameters)
ExecuteBfexplorerStrategySettingsWithParameters("Lay fade strategy", marketId, selectionId, parameters)
ExecuteBfexplorerStrategySettingsWithParameters("Lay resistance trading", marketId, selectionId, parameters)
```

## Risk Management & Trailing Stops

### Risk Management & Trailing Stops

### Steam Momentum Exit Rules (Probability Increasing Trades)
```javascript
// Steam Momentum (Back Trades - betting on probability increase continuation)
TrailingStopLogic: {
    initialStop: entryPrice * 1.2,  // 20% stop loss
    trailPercent: 15,               // Trail 15% behind best price
    exitTriggers: [
        "price_hits_trailing_stop",
        "volume_drops_50%_from_peak", 
        "probability_velocity_reverses_2_intervals", // Key: probability momentum reversal
        "time_remaining < 2_minutes"
    ]
}

// Drift Momentum (Lay Trades - betting on probability decrease continuation)  
TrailingStopLogic: {
    initialStop: entryPrice * 0.8,  // Liability protection
    trailPercent: 20,               // Trail 20% behind best price
    exitTriggers: [
        "price_hits_trailing_stop",
        "volume_increases_30%_steam_developing", // Key: steam signal detected
        "probability_velocity_reverses_toward_selection", // Key: probability increasing
        "strong_steam_signal_detected" // Key: probability gaining momentum
    ]
}
```

### Dynamic Risk Adjustments
- **High Volatility (>20% prob range)**: Wider stops (25-30%), require stronger confirmation
- **Low Volatility (<10% prob range)**: Tighter stops (10-15%), hold longer for development
- **Time Decay**: Tighten stops by 25% at 5-10min, 50% at <5min, exit all at <2min

## Output Formats

### Table Report (ENABLE_TABLE_REPORT = true)
```markdown
## Market Analysis: [Event] - [Race] | Start: [Time] | Selections: [N]

| Selection | Price | Prob% | Open% | ProbΔ | ProbVel | Vol% | Vol Trend | Support | Resistance | Direction | Confidence | Signal |
|-----------|-------|-------|-------|-------|---------|------|-----------|---------|------------|-----------|------------|--------|
| Horse 1   | 2.50  | 40.0% | 35.0% | +5.0pp| +1.7pp/min| 8.5% | ↑ +25%   | 35.7%   | 45.5%     | STEAMING  | 85%        | Steam  |
| Horse 2   | 3.50  | 28.6% | 33.3% | -4.7pp| -1.6pp/min| 12.1%| ↓ -20%   | 25.0%   | 35.0%     | DRIFTING  | 78%        | Drift  |
```
*Note: ProbΔ = Probability Change from Open (+ = steaming, - = drifting)*

### Execution Output Template
```markdown
## Strategy Execution Analysis

### Execution Decision
**SELECTED**: [Selection] @ [Price] | **STRATEGY**: [Strategy Name] | **CONFIDENCE**: [XX%]
**EXECUTION**: ✅ Activated / ❌ Criteria Not Met | **ODDS VALIDATION**: ✅ Valid ([X.X] within [Y.Y-Z.Z])

### Strategy Function Call
ExecuteBfexplorerStrategySettings("[Strategy Name]", "[marketId]", "[selectionId]")
// OR for automated strategies:
ExecuteBfexplorerStrategySettingsWithParameters("[Strategy Name]", "[marketId]", "[selectionId]", "[parameters]")

### Risk Profile
**Position Size**: [X]% bankroll | **Stop Loss**: [X]% trailing | **Time Risk**: [Assessment]
**Expected Behavior**: [Strategy description and parameter details]

### Alternative Opportunities (Not Executed)
| Selection | Signal | Confidence | Odds | Range | Why Not Executed |
|-----------|--------|------------|------|-------|------------------|
| Horse 2   | Drift  | 68%        | 8.2  | 1.8-50.0 | Below 70% confidence |
```

### Summary Format (ENABLE_SUMMARY_ONLY = true)
- **Top 3 Opportunities** with confidence scores and execution readiness
- **Market Insights** (2-3 key points)
- **Immediate Actions** with time sensitivity warnings

## Implementation Logic

### Execution Process
1. **ANALYZE** → Scan all selections, calculate probability metrics and signals
2. **RANK** → Sort by confidence score (highest first)  
3. **VALIDATE** → Apply execution criteria to top candidates
4. **FILTER** → Check odds ranges for each strategy type
5. **EXECUTE** → Run optimal strategy on first qualifying opportunity
6. **LOG** → Record decision rationale and alternative opportunities

### Quality Assurance
- **Minimum Data**: 10+ candlestick intervals required
- **Confidence Thresholds**: >75% signals achieve 70%+ success rate target
- **Error Handling**: Flag incomplete data, low liquidity, unusual patterns
- **Performance Tracking**: Monitor success rates vs confidence predictions

---

**Usage**: Apply to any Betfair market with candlestick data. Prioritize highest confidence signals within valid time windows. Always execute the best qualifying automated strategy while respecting risk management parameters.
