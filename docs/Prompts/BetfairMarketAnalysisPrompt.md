# Betfair Market Analysis and Trading Opportunities Prompt

## Objective
Analyze Betfair market data to identify professional betting patterns, assess market confidence, and provide specific trading recommendations including optimal entry/exit strategies with tick-level profit/loss targets.

## Analysis Framework

### 1. Required Tool Calls for Data Retrieval

Before beginning any analysis, execute the following tool calls in sequence:

#### Step 1: Get Active Market Information
```
GetActiveBetfairMarket()
```
- Retrieves current active market details including marketId, selections, and basic market information
- Use this to identify the target market and all available selections

#### Step 2: Get Full Market Data (for comprehensive analysis)
```
GetDataContextForBetfairMarket(
    dataContextNames: ["MarketSelectionsPriceHistoryData"],
    marketId: "[from step 1]"
)
```
- Retrieves price history for ALL selections in the market
- Enables comparative analysis and market-wide pattern recognition
- Required for creating comprehensive trading recommendations

### 2. Data Analysis Sequence

After retrieving data, follow this analysis sequence:
1. **Parse market context** (event details, timing, market status)
2. **Analyze individual selections** (price movements, volume patterns)
3. **Identify professional activity** (large trades, backing/laying patterns)
4. **Generate trading recommendations** (back/lay/trade opportunities)
5. **Create risk assessments** (confidence levels, profit/loss targets)
6. **Compile summary table** (all selections with clear recommendations)

### 3. Market Context Assessment
- **Market Type**: [Horse Racing/Football/Tennis etc.]
- **Event Details**: [Race/Match details, time, venue]
- **Market Status**: [Open/Suspended/Closed]
- **Time to Event**: [Minutes/hours until start]

### 4. Selection-Level Analysis
For each selection, analyze:

#### A. Price Movement Patterns
- **Opening Price vs Current Price**: Identify backing (shortening) or laying (drifting) trends
- **Price Volatility**: Measure price stability and sudden movements
- **Historical Price Zones**: Identify key support/resistance levels
- **Volume-Weighted Analysis**: Correlate price movements with trading volume

#### B. Professional Activity Indicators
Look for signs of informed money:
- **Large Volume Trades**: Transactions >£50+ at specific price points
- **Price Compression**: Sustained backing despite heavy volume
- **Market Timing**: Unusual activity patterns (early morning, pre-race rush)
- **Cross-Market Consistency**: Similar patterns across related markets

#### C. Volume Analysis
- **Total Volume Traded**: Assess market liquidity and interest
- **Volume Distribution**: Where most money has been matched
- **Recent Volume Spikes**: Identify sudden interest changes
- **Price-Volume Correlation**: Heavy volume + price shortening = confidence

### 5. Trading Pattern Recognition

#### Strong Backing Indicators:
- Price shortening from opening
- High volume at low prices
- Consistent buying pressure
- Multiple large trades in quick succession

#### Laying/Drift Indicators:
- Price lengthening from opening
- High volume at higher prices
- Selling pressure evident
- Market confidence declining

#### Volatile/Uncertain Patterns:
- Rapid price swings in both directions
- High volume with no clear direction
- Multiple reversals
- Conflicting signals

### 6. Recommendation Categories

#### A. BACK Opportunities
**Criteria:**
- Strong professional backing evident
- Price compression with volume
- Market confidence indicators positive
- Value assessment favorable

**Risk Management:**
- Define maximum stake
- Set stop-loss levels
- Consider cash-out scenarios

#### B. LAY Opportunities  
**Criteria:**
- Clear market drift pattern
- Weakening confidence indicators
- Overpriced relative to form/market
- Low probability assessment

**Risk Management:**
- Calculate maximum liability
- Set profit targets
- Monitor for market reversals

#### C. TRADING Opportunities (Back/Lay or Lay/Back)
**Criteria:**
- High volatility patterns
- Clear price ranges established
- Quick profit potential
- Manageable risk exposure

**Strategy:**
- Entry price targets
- Exit profit levels (2-5 ticks typically)
- Stop-loss thresholds (5-10 ticks maximum)
- Time-based exits

### 7. Risk Assessment Framework

#### Low Risk:
- Strong market consensus
- High liquidity
- Clear directional bias
- Professional money aligned

#### Medium Risk:
- Mixed signals present
- Moderate volatility
- Some uncertainty indicators
- Reasonable liquidity

#### High Risk:
- Extreme volatility
- Low liquidity
- Conflicting patterns
- Unpredictable behavior

### 8. Tick-Level Profit/Loss Guidelines

#### Conservative Approach:
- **Profit Target**: 2-3 ticks
- **Loss Limit**: 5-6 ticks
- **Success Rate Required**: 65%+

#### Moderate Approach:
- **Profit Target**: 3-4 ticks  
- **Loss Limit**: 6-8 ticks
- **Success Rate Required**: 60%+

#### Aggressive Approach:
- **Profit Target**: 4-5 ticks
- **Loss Limit**: 8-10 ticks
- **Success Rate Required**: 55%+

### 9. Output Requirements

#### For Each Selection Provide:
1. **Current Assessment**: Back/Lay/Trade/Avoid
2. **Confidence Level**: High/Medium/Low
3. **Primary Reasoning**: Key factors driving recommendation
4. **Entry Strategy**: Specific price targets or conditions
5. **Exit Strategy**: Profit targets and stop-loss levels
6. **Risk Rating**: Low/Medium/High
7. **Time Sensitivity**: Immediate/Pre-race/In-play optimal

#### Market Summary:
- **Strongest Bet**: Best single selection opportunity
- **Best Trading Play**: Highest probability trade setup
- **Market Bias**: Overall directional preference
- **Key Risks**: Main factors that could invalidate analysis

## Example Analysis Structure

### Selection: [Horse Name] (Current Price: X.XX)
**Price Movement**: Started at X.XX, now at X.XX ([+/-]X% change)
**Volume Analysis**: XXX units traded, heavy concentration at X.XX-X.XX range
**Professional Activity**: [Large trades/backing patterns/laying evidence]
**Recommendation**: **[BACK/LAY/TRADE/AVOID]**
**Strategy**: [Specific entry/exit plan]
**Risk**: [Low/Medium/High] - [Brief reasoning]
**Profit Target**: X ticks | **Stop Loss**: X ticks

## Final Deliverable Format

Provide analysis in narrative form followed by a comprehensive summary table listing all selections with clear recommendations, reasoning, and risk parameters.

### Required Summary Table Format

Create a markdown table with the following structure:

```markdown
| Selection | Current Price | Recommendation | Confidence | Reasoning | Entry Strategy | Profit Target | Stop Loss | Risk Level |
|-----------|---------------|----------------|------------|-----------|----------------|---------------|-----------|------------|
| Horse Name 1 | X.XX | BACK/LAY/TRADE/AVOID | High/Med/Low | Brief key reasoning | Specific entry conditions | X ticks | X ticks | Low/Med/High |
| Horse Name 2 | X.XX | BACK/LAY/TRADE/AVOID | High/Med/Low | Brief key reasoning | Specific entry conditions | X ticks | X ticks | Low/Med/High |
| ... | ... | ... | ... | ... | ... | ... | ... | ... |
```

**Table Column Descriptions:**
- **Selection**: Horse name or selection identifier
- **Current Price**: Latest available odds
- **Recommendation**: Primary action (BACK/LAY/TRADE/AVOID)
- **Confidence**: Assessment confidence (High/Medium/Low)
- **Reasoning**: 1-2 sentence summary of key factors
- **Entry Strategy**: Specific price targets or conditions
- **Profit Target**: Recommended profit in ticks
- **Stop Loss**: Maximum loss in ticks
- **Risk Level**: Overall risk assessment (Low/Medium/High)
