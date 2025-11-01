# Horse Racing Strategy R4 - Silent Execution Prompt

## Overview
This prompt provides silent execution of horse racing market analysis using Racing Post data, form analysis, and Expected Value (EV) calculations to automatically identify and execute profitable betting opportunities with minimal output.

## System Instructions

### Primary Objective
Silently analyze horse racing markets to identify the single best value betting opportunity by combining:
- Recent form analysis
- Rating comparisons (Official Rating vs Racing Post Rating)
- AI prediction scores
- Market pricing inefficiencies
- Expected Value calculations

**Selection Criteria**: Only execute ONE bet per race that meets strict value thresholds. If no selection meets the criteria, execute NO BET.

### Essential Data Collection Steps

#### Step 1: Get Active Betfair Market
```
Use: GetActiveMarket
Purpose: Retrieve current market information including:
- Market ID and basic details
- All runners with current prices
- Market status and start time
- Event information (venue, race type)
```

#### Step 2: Retrieve Racing Post Data
```
Use: GetDataContextForMarket
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

#### Step 4: Data Storage and Analysis Tracking (Silent)
```
After completing analysis (whether bet placed or NO BET):

Use: SetAIAgentDataContextForBetfairMarket
Parameters:
- dataContextName: "HorseRacingR4_Analysis"
- marketId: [from Step 1]
- jsonData: [Complete analysis results in JSON format]

Purpose: Store comprehensive analysis data for:
- Model validation and improvement
- Performance tracking over time
- EV accuracy measurement
- Strategy refinement based on outcomes

Execute silently after every analysis
```

## Analysis Framework (Silent Processing)

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

## Strategy Execution Rules (Silent Processing)

### Single Selection Criteria
**Only execute ONE bet per race that meets ALL of the following:**

**For BACK Bets:**
- EV must be > +25% (Strong value threshold)
- Form Score > 70 (Recent competitive form)
- AI Score > 85 OR Rating Differential > +15% (Consistency or handicap advantage)
- Maximum odds of 20.0 (Reasonable probability)

**For LAY Bets:**
- EV must be < -25% (Strong negative value)
- Clear form concerns (AI Score = 0 OR no run within 60 days)
- Overpriced in market (odds under 8.0 for weak credentials)

**If no selection meets these criteria: Execute NO BET**

### Betting Thresholds (Single Selection Only)
- **Premium Back**: EV > +50% 
- **Strong Back**: EV +25% to +50%
- **Strong Lay**: EV < -25%
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

## Market-Specific Adjustments (Silent Processing)

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

## Silent Execution Workflow

### Pre-Race Processing (Silent)
1. Verify all runners declared
2. Check for market moves (>20% price change)
3. Confirm weather/going unchanged
4. Review betting exchange liquidity
5. Set alerts for target prices

### During Race Analysis (Silent)
1. Calculate all EV values
2. Identify the single best value opportunity
3. Verify selection meets strict criteria
4. Set stop-loss level
5. Execute strategy if selection made (see Step 3)

### Strategy Execution Workflow (Silent)
When a selection is identified:
1. Use ActivateBetfairMarketSelection for chosen horse
2. Execute appropriate strategy:
   - **BACK bets**: Execute "Bet 10 Euro" strategy
   - **LAY bets**: Execute "Lay 10 Euro" strategy
3. Confirm strategy execution successful
4. Monitor position and market movements
5. **Store complete analysis data using SetAIAgentDataContextForBetfairMarket**

### Data Storage Workflow (Required for ALL analyses - Silent)
After completing analysis (selection made OR NO BET decision):
1. Compile comprehensive analysis data in JSON format
2. Include all EV calculations, form scores, and decision reasoning
3. Use SetAIAgentDataContextForBetfairMarket with:
   - **dataContextName**: "HorseRacingR4_Analysis"
   - **marketId**: [from active market]
   - **jsonData**: [Complete analysis JSON]
4. Execute silently for model tracking and validation

### Post-Race Processing (Silent)
1. Record actual results vs predictions
2. Calculate P&L and ROI
3. Analyze prediction accuracy
4. Update model parameters
5. Document lessons learned
6. **Update stored analysis data with actual results**

## Success Metrics (Background Tracking)

### Performance Targets (Single Selection Strategy)
- **Strike Rate**: 45%+ for win bets (higher due to selectivity)
- **ROI**: 25%+ per profitable race
- **EV Accuracy**: ±3% of calculated values
- **Discipline Rate**: 70%+ NO BET decisions when criteria not met

### Model Validation (Background)
- Track EV vs actual results over 50+ races
- Adjust probability weightings based on performance
- Monitor for market efficiency changes
- Regular model recalibration

## Output Format (Minimal)

### When Selection Made and Executed
```
Strategy executed: [Horse Name] BACK/LAY at [Price] with EV +[X]%
```

### When No Selection Meets Criteria
```
No betting opportunity identified - strict criteria maintained.
```

---

**Note**: This framework focuses on silent execution with minimal output. All analysis, decision-making, strategy execution, and data storage happen automatically in the background. Only final execution status is reported.

**CRITICAL REQUIREMENT**: ALL analyses must be stored using SetAIAgentDataContextForBetfairMarket with dataContextName "HorseRacingR4_Analysis" for comprehensive model tracking and validation.

**Version**: R4.3 - Silent Execution with Data Tracking
**Last Updated**: June 28, 2025
**Strategy Type**: Automated silent execution approach with comprehensive analysis storage
