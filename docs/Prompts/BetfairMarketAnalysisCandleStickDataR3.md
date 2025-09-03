# Betfair Market Analysis - Candlestick Data R3

## Objective
Analyze Betfair market candlestick data using probability-based metrics to identify the strongest trading opportunities and execute optimal automated strategies on the best selection.

## ⚠️ CRITICAL: Probability-Based Analysis Framework
**ALL analysis must be probability-focused, NOT price-focused:**

- **STEAM** = Probability INCREASING (price decreasing) = More money backing = Momentum rising
- **DRIFT** = Probability DECREASING (price increasing) = Money leaving = Momentum falling
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

### Strong Signals (Largest Probability Changes)
```javascript
// Steam Signal - Strong backing with probability increase (price decrease)
Criteria: Largest positive Probability_Change
// Note: Probability increase means price decreasing (more money backing selection)

// Drift Signal - Market confidence decline (probability decrease)
Criteria: Largest negative Probability_Change
// Note: Probability decrease means price increasing (money leaving selection)

// Value Opportunity - Significant probability divergence from expected  
Criteria: Largest absolute Probability_Change

// Steam Breakout - Probability surge through resistance with volume
Criteria: Probability_Breaks_Above_Resistance AND Volume_Trend > +8% AND Technical_Confirmation
// Note: Probability breakout = price breaking DOWN through support level

// False Favorite - Market leader showing probability weakness
Criteria: Market_Leader AND Largest negative Probability_Change
```

### Moderate Signals (Secondary Strength)
```javascript
// Moderate Steam - Developing backing momentum
Criteria: Second largest positive Probability_Change

// Moderate Drift - Developing negative momentum
Criteria: Second largest negative Probability_Change
```

## Strategy Execution Framework

### Strategy Selection Matrix
| Signal Type | Odds Range | Strategy | Execution Type | Probability Direction |
|------------|------------|----------|----------------|---------------------|
| Steam | 1.5-15.0 | Steam momentum trading | Manual Setup | INCREASING (+pp) |
| Drift | 1.8-50.0 | Drift momentum trading | Manual Setup | DECREASING (-pp) |
| Steam Breakout | 1.2-8.0 | Back scalping strategy | Automated | INCREASING (+pp) |
| Value | 2.0-20.0 | Back hedge strategy | Automated | Variable |
| False Favorite | 1.5-6.0 | Lay fade strategy | Automated | DECREASING (-pp) |
| Moderate Steam | 2.0-12.0 | Conservative back strategy | Automated | INCREASING (+pp) |
| Moderate Drift | 2.5-25.0 | Conservative lay strategy | Automated | DECREASING (-pp) |
| Technical Steam | 1.8-10.0 | Technical back strategy | Automated | INCREASING (+pp) |

### Execution Criteria Validation
**All strategies require:**
- ✅ **Strongest Signal** (largest absolute Probability_Change)
- ✅ **Odds Range** within strategy parameters  
- ✅ **Time Remaining** >3min (back) or >2min (lay)
- ✅ **Liquidity Check** sufficient for position size
- ✅ **Volume Confirmation** (no major divergences)

### Automated Strategy Parameters
```javascript
// Dynamic parameter calculation
function calculateParameters(signal, odds, timeToStart, volatility) {
    const baseStake = 1.0; // Base stake without confidence
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

| Selection | Price | Prob% | Open% | ProbΔ | ProbVel | Vol% | Vol Trend | Support | Resistance | Direction | Signal |
|-----------|-------|-------|-------|-------|---------|------|-----------|---------|------------|-----------|--------|
| Horse 1   | 2.50  | 40.0% | 35.0% | +5.0pp| +1.7pp/min| 8.5% | ↑ +25%   | 35.7%   | 45.5%     | STEAMING  | Steam  |
| Horse 2   | 3.50  | 28.6% | 33.3% | -4.7pp| -1.6pp/min| 12.1%| ↓ -20%   | 25.0%   | 35.0%     | DRIFTING  | Drift  |
```
*Note: ProbΔ = Probability Change from Open (+ = steaming, - = drifting)*

### Execution Output Template
```markdown
## Strategy Execution Analysis

### Execution Decision
**SELECTED**: [Selection] @ [Price] | **STRATEGY**: [Strategy Name] | **SIGNAL STRENGTH**: [XX pp]
**EXECUTION**: ✅ Activated / ❌ Criteria Not Met | **ODDS VALIDATION**: ✅ Valid ([X.X] within [Y.Y-Z.Z])

### Strategy Function Call
ExecuteBfexplorerStrategySettings("[Strategy Name]", "[marketId]", "[selectionId]")
// OR for automated strategies:
ExecuteBfexplorerStrategySettingsWithParameters("[Strategy Name]", "[marketId]", "[selectionId]", "[parameters]")

### Risk Profile
**Position Size**: [X]% bankroll | **Stop Loss**: [X]% trailing | **Time Risk**: [Assessment]
**Expected Behavior**: [Strategy description and parameter details]

### Alternative Opportunities (Not Executed)
| Selection | Signal | ProbΔ | Odds | Range | Why Not Executed |
|-----------|--------|-------|------|-------|------------------|
| Horse 2   | Drift  | -4.7pp| 8.2  | 1.8-50.0 | Not strongest signal |
```

### Summary Format (ENABLE_SUMMARY_ONLY = true)
- **Top 3 Opportunities** with signal strength and execution readiness
- **Market Insights** (2-3 key points)
- **Immediate Actions** with time sensitivity warnings

## Implementation Logic

### Execution Process
1. **ANALYZE** → Scan all selections, calculate probability metrics and signals
2. **RANK** → Sort by absolute probability change (largest first)  
3. **VALIDATE** → Apply execution criteria to top candidate
4. **FILTER** → Check odds ranges for the signal type
5. **EXECUTE** → Run optimal strategy on the best qualifying opportunity
6. **LOG** → Record decision rationale and alternative opportunities

### Quality Assurance
- **Minimum Data**: 10+ candlestick intervals required
- **Signal Strength**: Prioritize largest probability changes
- **Error Handling**: Flag incomplete data, low liquidity, unusual patterns
- **Performance Tracking**: Monitor success rates vs signal strength

---

**Usage**: Apply to any Betfair market with candlestick data. Execute on the selection with the strongest probability signal within valid time windows. Always execute the best qualifying automated strategy while respecting risk management parameters.
