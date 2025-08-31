# Betfair Market Analysis - Candlestick Data Prompt

## Objective
Analyze Betfair market candl## Trailing Stop Loss Framework for Momentum Strategies

### Steam Momentum Trading (Back Trades)
```
Entry: Price 5.0 (20% probability)
Initial Stop: 6.0 (16.7% probability) - 20% loss protection

As trade moves favorably:
Price 4.5 → Trail stop to 5.4 (15% trail)
Price 4.0 → Trail stop to 4.8 (20% trail) 
Price 3.5 → Trail stop to 4.2 (20% trail)

Exit Triggers:
- Price hits trailing stop level
- Volume drops >50% from peak
- Probability velocity reverses for 2 intervals
- <2 minutes to race start (time decay risk)
```

### Drift Momentum Trading (Lay Trades)
```
Entry: Lay at 3.0 (33.3% probability)
Initial Stop: 2.5 (40% probability) - Liability protection

As trade moves favorably:
Price 3.5 → Trail stop to 2.8 (20% trail)
Price 4.0 → Trail stop to 3.2 (20% trail)
Price 5.0 → Trail stop to 4.0 (20% trail)

Exit Triggers:
- Price hits trailing stop level
- Volume increases >30% (steam developing)
- Probability velocity reverses toward selection
- Strong steam signal detected on selection
```

### Breakout Trading (Back/Lay)
```
Entry: At breakout level confirmation
Initial Stop: At breakdown level (breakout failure)

Trail Management:
- No trailing until 10% profit achieved
- Then trail 10% behind best price
- Tighten to 5% trail if momentum accelerates
- Lock in profits at key resistance/support levels
```

### Dynamic Trailing Adjustments
```
High Volatility Markets (>20% probability range):
- Wider trailing stops (25-30%)
- Require stronger momentum confirmation
- Exit faster on volume decline

Low Volatility Markets (<10% probability range):  
- Tighter trailing stops (10-15%)
- Hold longer for momentum development
- More sensitive to small volume changes

Time-Based Adjustments:
>10 minutes to start: Standard trailing rules
5-10 minutes: Tighten trails by 25%
<5 minutes: Tighten trails by 50% or exit
<2 minutes: Close all momentum positions
```

### Automated Strategy Executionstick data to identify high-probability trading opportunities through systematic price movement, volume, and technical pattern analysis.

## Data Source
Retrieve data context: **"MarketSelectionsCandleStickData"** from active Betfair market

## Output Configuration
Choose ONE configuration based on analysis needs:

```
// Quick Decision Mode - Essential insights only
ENABLE_SUMMARY_ONLY = true

// Comparative Analysis Mode - Table view for selection comparison  
ENABLE_TABLE_REPORT = true, ENABLE_DETAILED_REPORT = false

// Comprehensive Analysis Mode - Full detailed breakdown
ENABLE_TABLE_REPORT = true, ENABLE_DETAILED_REPORT = true

// Deep Dive Mode - Individual selection focus
ENABLE_TABLE_REPORT = false, ENABLE_DETAILED_REPORT = true
```

## Core Analysis Framework

### 1. Market Context
- **Market Details**: Event, race name, start time, status
- **Market Structure**: Number of selections, overround percentage
- **Time Context**: Minutes to start, market maturity phase

### 2. Selection Metrics (for each runner)

#### Price & Probability Dynamics
- **Current Price** & **Implied Probability**: Price and probability (1/price × 100)
- **Probability Change**: Movement in probability percentage points from open
- **Price Change %**: Traditional price movement percentage (for reference)
- **Probability Velocity**: Rate of probability change per minute  
- **Price Velocity**: Rate of price change per minute (for comparison)
- **Probability Volatility**: (High_Prob-Low_Prob)/Current_Prob as percentage
- **Trend Direction**: Strengthening/Weakening/Stable with momentum strength

#### Volume Intelligence  
- **Volume Profile**: Average, recent trend (increasing/decreasing/stable)
- **Volume-Probability Correlation**: Confirming/Diverging patterns
- **Liquidity Assessment**: Market depth and spread analysis

#### Technical Levels
- **Support/Resistance**: Key probability boundaries from data range
- **Market Position**: Current probability relative to High/Mid/Low range  
- **Breakout Potential**: Proximity to key technical probability levels

### 3. Signal Detection Framework

#### High-Confidence Signals (70%+ reliability)
- **Steam Move**: Probability increase >3pp + Volume increase >20% + Consistent direction
- **Late Drift**: Probability decrease >5pp + Volume decline >15% + Time <5min to start  
- **Value Opportunity**: Probability divergence >8pp from technical levels + Volume confirmation
- **Breakout Signal**: Price breaking through support/resistance + Volume increase >15% + Technical confirmation
- **False Favorite**: Market leader showing weakness + Probability decrease >4pp + Volume decline >10%
- **Resistance Test**: Selection approaching technical resistance + Price stalling + Volume divergence

#### Medium-Confidence Signals (50-70% reliability)  
- **Probability momentum without volume confirmation**
- **Technical probability level approaches with mixed volume**
- **Cross-selection probability correlation patterns**

#### Low-Confidence Signals (<50% reliability)
- **High probability volatility with low volume**
- **Conflicting technical indicators**
- **Late market noise near start time**

## Output Formats

### Table Report Format (ENABLE_TABLE_REPORT = true)
```markdown
## Market Analysis: [Event] - [Race] | Start: [Time] | Selections: [N]

| Selection | Price | Prob% | ProbΔ | ProbVel | Vol% | Vol Trend | Prob Support | Prob Resistance | Position | Confidence | Signal |
|-----------|-------|-------|-------|---------|------|-----------|--------------|-----------------|----------|------------|--------|
| Horse 1   | 2.50  | 40.0% | +5.2pp| +1.7pp/min| 8.5% | ↑ +25%   | 35.7%        | 45.5%          | Mid      | 85%        | Steam  |
| Horse 2   | 4.20  | 23.8% | -2.1pp| -0.7pp/min| 12%  | → Stable | 21.7%        | 26.3%          | High     | 45%        | Drift  |
```

### Detailed Report Format (ENABLE_DETAILED_REPORT = true)
For each selection with >60% confidence:
- **Complete technical breakdown** with historical context
- **Risk assessment** and position sizing recommendations  
- **Entry/exit timing** with specific price levels
- **Strategy suitability** for different trading approaches

### Summary Format (ENABLE_SUMMARY_ONLY = true)
- **Top 3 opportunities** with confidence scores
- **Key market insights** in 2-3 bullet points
- **Immediate action items** with time sensitivity

## Automated Strategy Execution

### Strategy Selection Matrix
Execute the optimal strategy based on signal type and market conditions:

#### For BACK Opportunities (Steam Signals)
- **Primary**: `Steam momentum trading` - **MANUAL SETUP REQUIRED** - For strong steam moves with momentum
- **Alternative**: `Back scalping strategy` - **AUTOMATED** - For quick price corrections near support
- **Conservative**: `Back hedge strategy` - **AUTOMATED** - For medium confidence signals

#### For LAY Opportunities (Drift Signals)  
- **Primary**: `Drift momentum trading` - **MANUAL SETUP REQUIRED** - For sustained price lengthening with volume decline
- **Alternative**: `Lay fade strategy` - **AUTOMATED** - For false favorites showing weakness
- **Conservative**: `Lay resistance trading` - **AUTOMATED** - For selections approaching technical resistance

### Execution Criteria Framework

#### Back Strategy Execution (Steam Signals)
Execute when **ALL** criteria met:
- ✅ **Signal Confidence** >75%
- ✅ **Steam Pattern**: Probability increase >3pp + Volume increase >20%
- ✅ **Legacy Check**: Price shortening >5% (for validation)
- ✅ **Time Remaining** >3 minutes to start
- ✅ **Momentum Consistency** >3 consecutive intervals
- ✅ **Liquidity Check**: Sufficient depth for position size
- ✅ **Odds Range Filter**: Current price within acceptable range for strategy type

#### Lay Strategy Execution (Drift Signals)
Execute when **ALL** criteria met:
- ✅ **Signal Confidence** >70% 
- ✅ **Drift Pattern**: Probability decrease >5pp + Volume decline >15%
- ✅ **Legacy Check**: Price lengthening >8% (for validation)
- ✅ **Time Remaining** >2 minutes to start
- ✅ **Technical Resistance** approaching or breached (probability levels)
- ✅ **Market Position** in upper 25% of probability range
- ✅ **Odds Range Filter**: Current price within acceptable range for strategy type

### Execution Priority Logic
1. **Scan all selections** for qualifying signals
2. **Rank by confidence score** (highest first)
3. **Apply strategy-specific criteria** to top candidates
4. **Apply odds range filters** for each strategy type
5. **Execute FIRST qualifying opportunity** (back or lay)
6. **Log decision rationale** for all candidates

### Odds Range Filters by Strategy

#### Back Strategy Odds Ranges
```
// Steam momentum trading (Steam Signals)
Optimal Range: 1.5 - 15.0
- Below 1.5: Insufficient profit potential, high liability risk
- Above 15.0: Too volatile, poor liquidity typically

// Back scalping strategy (Breakout Signals)  
Optimal Range: 1.2 - 8.0
- Below 1.2: Minimal scalping opportunity
- Above 8.0: Breakouts less reliable, higher risk

// Back hedge strategy (Value Signals)
Optimal Range: 2.0 - 20.0  
- Below 2.0: Limited hedging value
- Above 20.0: Hedging becomes impractical
```

#### Lay Strategy Odds Ranges
```
// Drift momentum trading (Drift Signals)
Optimal Range: 1.8 - 50.0
- Below 1.8: High liability risk, limited drift potential
- Above 50.0: Liquidity concerns, extreme liability

// Lay fade strategy (False Favorites)
Optimal Range: 1.5 - 6.0
- Below 1.5: Extreme liability risk for false favorite
- Above 6.0: Not typically market leaders/favorites

// Lay resistance trading (Resistance Test)
Optimal Range: 2.0 - 25.0
- Below 2.0: High liability for resistance test
- Above 25.0: Resistance levels less meaningful
```

### Risk-Based Odds Filtering
```
// Conservative Mode (Lower Risk)
Back Strategies: 2.0 - 10.0
Lay Strategies: 3.0 - 20.0

// Aggressive Mode (Higher Risk/Reward)  
Back Strategies: 1.3 - 25.0
Lay Strategies: 1.5 - 100.0

// Balanced Mode (Standard)
Back Strategies: 1.5 - 15.0
Lay Strategies: 2.0 - 50.0
```

### Parameter Configuration Framework

#### Dynamic Parameter Calculation
```javascript
// Base calculations for automated strategies
function calculateParameters(signal, odds, confidence, timeToStart, volatility) {
    
    // Base stake calculation (percentage of bankroll)
    const baseStakePercent = confidence > 80 ? 2.0 : confidence > 70 ? 1.5 : 1.0;
    const riskMultiplier = volatility > 20 ? 0.7 : volatility < 10 ? 1.3 : 1.0;
    const timeMultiplier = timeToStart > 5 ? 1.0 : timeToStart > 3 ? 0.8 : 0.6;
    
    const finalStakePercent = baseStakePercent * riskMultiplier * timeMultiplier;
    const stakeAmount = calculateStakeFromPercent(finalStakePercent); // Convert to actual amount
    
    // Strategy-specific parameters
    if (signal.type === "breakout") {
        return {
            "OpenBetPosition.Stake": stakeAmount,
            "CloseBetPosition.Profit": odds < 3 ? 2 : odds < 8 ? 3 : 5,
            "CloseBetPosition.Loss": volatility > 15 ? 20 : 30
        };
    }
    
    if (signal.type === "value") {
        return {
            "OpenBetPosition.Stake": stakeAmount,
            "CloseBetPosition.Profit": confidence > 75 ? 4 : 3,
            "CloseBetPosition.Loss": odds < 5 ? 25 : 30
        };
    }
    
    if (signal.type === "false_favorite") {
        return {
            "OpenBetPosition.Stake": stakeAmount, // This is liability for lay bets
            "CloseBetPosition.Profit": odds < 3 ? 4 : 3,
            "CloseBetPosition.Loss": 30
        };
    }
    
    if (signal.type === "resistance") {
        return {
            "OpenBetPosition.Stake": stakeAmount, // This is liability for lay bets
            "CloseBetPosition.Profit": 3,
            "CloseBetPosition.Loss": volatility > 20 ? 25 : 30
        };
    }
}
```

#### Example Parameter Outputs
```json
// Back scalping strategy (Breakout at 4.5 odds, 72% confidence, 6min to start)
{
    "OpenBetPosition.Stake": 25,
    "CloseBetPosition.Profit": 3, 
    "CloseBetPosition.Loss": 30
}

// Lay fade strategy (False favorite at 2.8 odds, 78% confidence, 4min to start)  
{
    "OpenBetPosition.Stake": 35,
    "CloseBetPosition.Profit": 4,
    "CloseBetPosition.Loss": 30
}

// Back hedge strategy (Value signal at 8.0 odds, 71% confidence, 7min to start)
{
    "OpenBetPosition.Stake": 20,
    "CloseBetPosition.Profit": 3,
    "CloseBetPosition.Loss": 30
}
```

### Execution Process
```
1. ANALYZE → Identify highest confidence opportunity (back/lay)
2. SELECT → Choose optimal strategy based on signal type
3. VALIDATE → Confirm all execution criteria are met
4. CHECK PARAMETERS → Use GetBfexplorerStrategySetting to verify available parameters
5. CALCULATE → Generate parameters for automated strategies (if applicable)
6. EXECUTE → 
   - If momentum strategy: Call ExecuteBfexplorerStrategySettings("[Strategy Name]", marketId, selectionId)
   - If other strategy: Call ExecuteBfexplorerStrategySettingsWithParameters("[Strategy Name]", marketId, selectionId, parameters)
7. LOG → Record execution details and reasoning
```

### Enhanced Execution Output Template
```markdown
## Strategy Execution Analysis

### Market Scan Results
**Total Qualifying Signals**: [N back opportunities] + [N lay opportunities]
**Time to Start**: [X minutes] | **Market Liquidity**: [Good/Fair/Poor]

### Execution Decision
**SELECTED**: [Selection Name] @ [Current Price]
**STRATEGY**: [Strategy Name] | **SIGNAL TYPE**: [Steam/Drift/Breakout]
**CONFIDENCE**: [XX%] | **ODDS RANGE**: ✅ Valid (X.X within Y.Y-Z.Z) | **EXECUTION**: ✅ Activated | ❌ Criteria Not Met

### Execution Details
**Strategy Function**: 
- **If Momentum Strategy**: `ExecuteBfexplorerStrategySettings("[Strategy Name]", "[marketId]", "[selectionId]")`
- **If Automated Strategy**: `ExecuteBfexplorerStrategySettingsWithParameters("[Strategy Name]", "[marketId]", "[selectionId]", "[parameters]")`
**Trigger Reason**: [Specific criteria that qualified this selection]
**Odds Validation**: Current odds [X.X] within strategy range [Y.Y-Z.Z]
**Expected Behavior**: [Strategy description and default/calculated parameters]
**Risk Profile**: [Position size and stop loss expectations based on odds level]
**Parameter Configuration**: [JSON parameters for automated strategies only]
**Manual Setup Notes**: [For momentum strategies - they execute with preset sequence: bet placement + trailing stop loss]

### Alternative Opportunities (Not Executed)
| Selection | Signal | Confidence | Current Odds | Odds Range | Why Not Executed |
|-----------|--------|------------|--------------|------------|------------------|
| Horse 2   | Drift  | 68%        | 8.2          | 1.8-50.0   | Below 70% confidence threshold |
| Horse 3   | Steam  | 78%        | 95.0         | 1.5-15.0   | Outside odds range (>15.0) |
| Horse 4   | False Fav | 80%      | 1.2          | 1.5-6.0    | Outside odds range (<1.5) |
```

### Strategy Function Mapping
```
// MANUAL SETUP STRATEGIES (Momentum - No Parameters, Just Execute)
Steam Signal (>75% confidence + 1.5-15.0 odds) → ExecuteBfexplorerStrategySettings("Steam momentum trading", marketId, selectionId)
Drift Signal (>70% confidence + 1.8-50.0 odds) → ExecuteBfexplorerStrategySettings("Drift momentum trading", marketId, selectionId)

// AUTOMATED STRATEGIES (with configurable parameters)
Breakout Signal (>70% confidence + 1.2-8.0 odds) → ExecuteBfexplorerStrategySettingsWithParameters("Back scalping strategy", marketId, selectionId, parameters)
Value Signal (>70% confidence + 2.0-20.0 odds) → ExecuteBfexplorerStrategySettingsWithParameters("Back hedge strategy", marketId, selectionId, parameters)
False Favorite (>75% confidence + 1.5-6.0 odds) → ExecuteBfexplorerStrategySettingsWithParameters("Lay fade strategy", marketId, selectionId, parameters)
Resistance Test (>70% confidence + 2.0-25.0 odds) → ExecuteBfexplorerStrategySettingsWithParameters("Lay resistance trading", marketId, selectionId, parameters)

// Available Parameters for Automated Strategies:
Back scalping strategy: 
- OpenBetPosition.Stake (default: 50)
- CloseBetPosition.Profit (default: 3) 
- CloseBetPosition.Loss (default: 30)

Back hedge strategy:
- OpenBetPosition.Stake (default: 50)
- CloseBetPosition.Profit (default: 3)
- CloseBetPosition.Loss (default: 30)

Lay fade strategy:
- OpenBetPosition.Stake (default: 50, liability-based)
- CloseBetPosition.Profit (default: 3)
- CloseBetPosition.Loss (default: 30)

Lay resistance trading:
- OpenBetPosition.Stake (default: 50, liability-based)
- CloseBetPosition.Profit (default: 3)
- CloseBetPosition.Loss (default: 30)
```

## Strategic Recommendations Framework

### Immediate Actions (Next 5 minutes)
- **Priority 1**: Selections with >80% confidence + steam signals
- **Priority 2**: Technical breakout opportunities with volume
- **Priority 3**: Value positions against market drift

### Risk Management
- **Position Sizing**: Scale by confidence level (Max 3% bankroll for <70% confidence)
- **Time Risk**: Reduce exposure <2 minutes to start  
- **Correlation Risk**: Limit same-market exposure to 10% bankroll
- **Liquidity Risk**: Ensure 3+ price levels for exit strategy
- **Trailing Stop Loss**: Essential for momentum strategies - adjust stop as price moves favorably
- **Momentum Exit Rules**: Exit when volume drops >50% or probability velocity reverses

### Market Timing Guidelines
- **Entry Windows**: After volume confirmation, before momentum exhaustion
- **Exit Triggers**: Profit targets, trailing stop losses, or signal reversal  
- **Hold Conditions**: Strong momentum + increasing volume + time remaining >5min
- **Trailing Stop Rules**: 
  - **Steam Trades**: Trail stop 15% behind best price achieved
  - **Drift Trades**: Trail stop 20% above worst price achieved
  - **Breakout Trades**: Trail stop at breakout level once 10% profit achieved
- **Momentum Invalidation**: Exit immediately if volume drops >50% or velocity reverses

## Technical Calculations & Signal Detection

### Key Formulas
```
// Price to Probability Conversion
Implied_Probability = 1 / Decimal_Price × 100

// Probability-Based Change Calculations  
Probability_Change = Current_Probability - Open_Probability
Probability_Change_% = (Probability_Change / Open_Probability) × 100

// Traditional Price Calculations (for reference)
Price_Change_% = ((Current_Price - Open_Price) / Open_Price) × 100

// Velocity & Momentum (using probability)
Probability_Velocity = Probability_Change / Time_Interval_Minutes
Price_Velocity = Price_Change_% / Time_Interval_Minutes  

// Volatility Calculations
Probability_Volatility = ((High_Prob - Low_Prob) / Current_Prob) × 100
Price_Volatility_% = ((High - Low) / Current_Price) × 100

// Volume and Position
Volume_Trend = (Recent_Avg - Early_Avg) / Early_Avg × 100
Market_Position = (Current_Prob - Low_Prob) / (High_Prob - Low_Prob) × 100
```

### Signal Thresholds (Probability-Based)
```
// Steam Signal - Probability increasing (price shortening)
Steam Signal: Probability_Change > +3pp AND Volume_Trend > +20% AND Consistency > 3 intervals

// Drift Signal - Probability decreasing (price lengthening)  
Drift Signal: Probability_Change < -5pp AND Volume_Trend < -15% AND Time < 5min

// Value Signal - Significant probability shift
Value Signal: ABS(Probability_Change) > 8pp AND Volume_Support > baseline

// Breakout Signal - Technical level breach with volume
Breakout Signal: Price_Break_Through_Level AND Volume_Trend > +15% AND Technical_Confirmation

// False Favorite Signal - Market leader weakness
False Favorite: Market_Leader_Status AND Probability_Change < -4pp AND Volume_Trend < -10%

// Resistance Test Signal - Technical resistance approach
Resistance Test: Approaching_Resistance_Level AND Price_Stalling AND Volume_Divergence

// False Signal - High volatility without direction
False Signal: Probability_Volatility > 15% AND Volume < 50% of average
```

### Legacy Price-Based Thresholds (for comparison)
```
Steam Signal: Price_Change < -5% AND Volume_Trend > +20% AND Consistency > 3 intervals
Drift Signal: Price_Change > +10% AND Volume_Trend < -10% AND Time < 5min
Value Signal: Technical_Divergence > 15% AND Volume_Support > baseline
False Signal: Volatility > 25% AND Volume < 50% of average
```

### Probability vs Price Comparison Example
```
// Example: Horse moving from 10.0 to 8.0 vs Horse moving from 2.0 to 1.8

Traditional Price Analysis:
Horse A: 10.0 → 8.0 = -20% change
Horse B: 2.0 → 1.8 = -10% change
Conclusion: Horse A shows stronger backing

Probability-Based Analysis:
Horse A: 10% → 12.5% = +2.5pp change  
Horse B: 50% → 55.6% = +5.6pp change
Conclusion: Horse B shows stronger backing (more significant probability shift)

Why Probability is Better:
- Horse B's move represents a much larger shift in market confidence
- Probability changes are comparable across all price ranges
- More accurate representation of market sentiment and value
```

### Quality Indicators
- **Signal Strength**: Combination of probability momentum + volume confirmation + time consistency
- **Confidence Score**: Weighted average of all confirming factors (0-100%)
- **Risk Rating**: Inverse relationship to confidence + time-decay factor

## Implementation Notes

### Data Quality Requirements
- **Minimum**: 10+ candlestick intervals for trend analysis
- **Optimal**: 30+ intervals with consistent volume data
- **Time Range**: Focus on last 15-30 minutes for current signal relevance

### Performance Benchmarks  
- **High Performance**: >75% confidence signals should achieve 70%+ success rate
- **Acceptable**: >60% confidence signals should achieve 55%+ success rate  
- **Review Threshold**: <50% confidence signals require pattern review

### Error Handling
- **Missing Data**: Flag incomplete datasets and adjust confidence accordingly
- **Low Liquidity**: Warn when volume insufficient for reliable signals
- **Market Anomalies**: Detect and flag unusual patterns requiring manual review

---

**Usage**: Apply this framework to any Betfair market with candlestick data. Prioritize high-confidence signals and always execute the automated strategy on the best qualifying opportunity.
