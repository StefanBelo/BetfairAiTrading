# Weight of Money Strategy Execution Prompt

## Objective
Analyze Betfair market weight of money data, identify the highest confidence prediction, and automatically execute the appropriate trading strategy based on the prediction direction.

### Automatic Strategy Selection:
- **Shorten Prediction (⬇️)**: Execute "Back Trade" strategy on highest confidence selection
- **Drift Prediction (⬆️)**: Execute "Lay Trade" strategy on highest confidence selection
- **Minimum Confidence**: Only execute if confidence ≥ 70%

## Analysis Framework

### 1. Data Points to Examine
- **Average Back Traded Price & Volume**: Shows where money has been backing the selection
- **Average Lay Traded Price & Volume**: Shows where money has been laying against the selection
- **Current Market Price**: The present best back price available
- **Offered Prices Ladder**: Distribution of available liquidity at different price levels
  - **CRITICAL**: BetType 1 (Back) volume = Money available to LAY (lay offers)
  - **CRITICAL**: BetType 2 (Lay) volume = Money available to BACK (back offers)
- **Volume Imbalance**: Compare backing vs laying volumes to identify market sentiment

### 2. Key Indicators for Price Movement

#### Price Direction Signals:
- **Current vs Historical Averages**: If current price significantly differs from traded averages
- **Volume Imbalance**: Heavy backing suggests price shortening, heavy laying suggests drifting
- **Liquidity Distribution**: Where the weight of money is positioned on the price ladder
  - **Backing Pressure**: High volume at BetType 2 (Lay) prices = Strong backing interest
  - **Laying Pressure**: High volume at BetType 1 (Back) prices = Strong laying interest

#### Strength Indicators:
- **Volume Magnitude**: Higher volumes indicate stronger signals
- **Price Deviation**: Larger gaps between current and average prices suggest bigger moves
- **Market Efficiency**: Consistent patterns across multiple price levels

### 3. Movement Prediction Categories

#### Strong Movement Expected (>15% price change):
- Large volume imbalance (>50% difference)
- Current price >20% away from traded averages
- Clear directional bias in offered prices

#### Moderate Movement Expected (5-15% price change):
- Moderate volume imbalance (20-50% difference)
- Current price 10-20% away from traded averages
- Some directional bias visible

#### Stable/Minor Movement (<5% price change):
- Balanced volumes
- Current price within 10% of traded averages
- Well-distributed liquidity

### 4. Risk Factors to Consider
- **Market Volatility**: Horse racing markets can be highly volatile
- **Time to Event**: Closer to start time = more reliable signals
- **External Factors**: Weather, jockey changes, market rumors
- **Liquidity Depth**: Thin markets more susceptible to manipulation

## CRITICAL: Understanding BetType in Offered Prices

**BetType Interpretation (ESSENTIAL):**
- **BetType 1 (Back)**: Volume represents money available to LAY
  - These are lay offers - people willing to lay at these prices
  - High volume here = Strong laying interest/pressure
- **BetType 2 (Lay)**: Volume represents money available to BACK  
  - These are back offers - people willing to back at these prices
  - High volume here = Strong backing interest/pressure

**Market Pressure Analysis:**
- **Backing Pressure**: High BetType 2 volumes (back offers) indicate demand to back
- **Laying Pressure**: High BetType 1 volumes (lay offers) indicate willingness to lay
- **Price Movement**: 
  - Strong backing pressure (high BetType 2 volumes) → Odds likely to shorten
  - Strong laying pressure (high BetType 1 volumes) → Odds likely to drift

## Confidence Scoring System (Numerical)

### Confidence Calculation (0-100%):
Calculate confidence score by summing points from each factor:

#### Historical Volume Signal Strength (0-30 points):
- **Extreme imbalance** (>2:1 ratio): 30 points
- **Strong imbalance** (1.5-2:1 ratio): 20 points  
- **Moderate imbalance** (1.2-1.5:1 ratio): 10 points
- **Balanced** (0.8-1.2:1 ratio): 0 points

#### Price Deviation Signal (0-25 points):
- **>20% deviation** from historical average: 25 points
- **10-20% deviation**: 15 points
- **5-10% deviation**: 8 points
- **<5% deviation**: 0 points

#### Offered Prices Signal Strength (0-25 points):
- **Extreme offered imbalance** (>3:1 ratio): 25 points
- **Strong offered imbalance** (2-3:1 ratio): 18 points
- **Moderate offered imbalance** (1.5-2:1 ratio): 12 points
- **Balanced offered prices** (0.8-1.5:1 ratio): 0 points

#### Signal Alignment (0-20 points):
- **All signals align** (historical + current + offered): 20 points
- **Two signals align**: 12 points
- **One signal only**: 5 points
- **Conflicting signals**: -10 points

#### Volume Liquidity Adjustment (Multiplier):
**Total Traded Volume** = averageBackTraded.volume + averageLayTraded.volume

**Volume Adjustment Factor:**
- **High Volume** (>1000 total): 1.0 (no adjustment)
- **Medium Volume** (500-1000 total): 0.9 (10% reduction)
- **Low Volume** (100-500 total): 0.8 (20% reduction)
- **Very Low Volume** (50-100 total): 0.6 (40% reduction)
- **Minimal Volume** (<50 total): 0.4 (60% reduction)

**Final Confidence = Base Confidence × Volume Adjustment Factor**

### Confidence Categories:
- **85-100%**: Very High Confidence 🔴
- **70-84%**: High Confidence 🟡  
- **55-69%**: Medium Confidence 🟠
- **40-54%**: Low Confidence 🟢
- **<40%**: Very Low Confidence ⚪

## Trading Strategy Parameters

### Strategy 1: Back Trade (for Shorten Predictions ⬇️)

#### Entry Conditions:
- **Prediction**: Shorten (⬇️) with confidence ≥ 70%
- **Entry Price**: Current market price or better
- **Position Size**: Based on confidence level:
  - **85-100% confidence**: 3-5% of bankroll
  - **70-84% confidence**: 2-3% of bankroll

#### Strategy Parameters:
- **Strategy Name**: "Back Trade" (exact name)
- **Entry Type**: Back position on selection
- **Risk Management**: Built into strategy
- **Time Horizon**: Pre-race only

#### Exit Conditions:
- **Profit Target**: 15-25% price movement toward predicted direction
- **Stop Loss**: If price moves 10% against prediction
- **Time Exit**: Close position 2 minutes before race start

### Strategy 2: Lay Trade (for Drift Predictions ⬆️)

#### Entry Conditions:
- **Prediction**: Drift (⬆️) with confidence ≥ 70%
- **Entry Price**: Current market price or better
- **Position Size**: Based on confidence level:
  - **85-100% confidence**: 2-3% liability of bankroll
  - **70-84% confidence**: 1-2% liability of bankroll

#### Strategy Parameters:
- **Strategy Name**: "Lay Trade" (exact name)
- **Entry Type**: Lay position on selection
- **Risk Management**: Built into strategy
- **Time Horizon**: Pre-race only

#### Exit Conditions:
- **Profit Target**: 15-25% price movement toward predicted direction
- **Stop Loss**: If price moves 10% against prediction (cover lay position)
- **Time Exit**: Close position 2 minutes before race start

## Analysis and Execution Steps

### Step 1: Get Active Market
1. **Retrieve active market**: Use GetActiveBetfairMarket to get the currently monitored market
2. **Extract market details**: Get marketId and market information

### Step 2: Get Weight of Money Data
1. **Retrieve WeightOfMoneyData**: Use GetDataContextForBetfairMarket with:
   - dataContextName: "WeightOfMoneyData"
   - marketId: [from Step 1]
2. **Parse data structure**: Extract average traded prices, volumes, and offered prices for all selections

### Step 3: Market Analysis
1. Calculate volume ratios (backing:laying) from historical traded data
2. Compare current price to historical averages
3. Assess price deviation percentage
4. Analyze offered prices ladder for each selection
5. Calculate confidence scores for all selections using the numerical system
6. Apply volume liquidity adjustment to reduce confidence for low-volume selections

### Step 4: Selection Identification
1. **Identify highest confidence prediction**: Find selection with highest confidence ≥ 70%
2. **Determine prediction direction**: Shorten (⬇️) or Drift (⬆️) based on analysis
3. **Validate execution criteria**: Ensure confidence threshold is met

### Step 5: Strategy Selection and Execution
1. **If highest confidence prediction is Shorten (⬇️)**:
   - Execute "Back Trade" strategy
   - Use ExecuteBfexplorerStrategySettings with strategyName: "Back Trade"
2. **If highest confidence prediction is Drift (⬆️)**:
   - Execute "Lay Trade" strategy
   - Use ExecuteBfexplorerStrategySettings with strategyName: "Lay Trade"
3. **If no prediction meets 70% confidence threshold**:
   - Do not execute any strategy
   - Report "No high confidence signals found"

## Strategy Parameter Calculation

### Strategy Selection Logic:

#### For Shorten Predictions (⬇️):
- **Strategy Name**: "Back Trade"
- **Logic**: If odds are predicted to shorten, back the selection
- **Risk**: Built into strategy template

#### For Drift Predictions (⬆️):
- **Strategy Name**: "Lay Trade" 
- **Logic**: If odds are predicted to drift, lay the selection
- **Risk**: Built into strategy template

### Confidence Requirements:
- **Minimum Confidence**: 70% required for execution
- **Strategy Selection**: Based purely on prediction direction
- **Risk Management**: Handled by strategy templates

## Output Format

### Strategy Execution Summary:
```
Market: [Market Name]
Market ID: [Market ID]
Highest Confidence Selection: [Selection Name]
Selection ID: [Selection ID]
Confidence Score: [XX]%
Prediction: [Shorten/Drift]
Strategy Executed: ["Back Trade"/"Lay Trade"]
Execution Status: [Success/Failed]
```

## Usage Instructions

### Automated Execution Sequence:
1. **Get Active Market**: Use GetActiveBetfairMarket to retrieve current market
2. **Get Weight of Money Data**: Use GetDataContextForBetfairMarket with dataContextName "WeightOfMoneyData"
3. **Analysis Phase**: Run weight of money analysis on all selections
4. **Selection Phase**: Identify highest confidence prediction ≥ 70%
5. **Strategy Phase**: Select "Back Trade" or "Lay Trade" based on prediction direction
6. **Execution Phase**: Execute strategy using:
   ```
   ExecuteBfexplorerStrategySettings(
     strategyName: "Back Trade" or "Lay Trade",
     marketId: "[Market ID]", 
     selectionId: "[Selection ID of highest confidence prediction]"
   )
   ```
7. **Monitoring Phase**: Strategy will handle position management automatically

### Success Criteria:
- **Market retrieved**: Successfully get active market data
- **Weight of Money data obtained**: WeightOfMoneyData context retrieved
- **Confidence threshold met**: ≥ 70% for strategy execution
- **Appropriate strategy selected**: "Back Trade" for Shorten, "Lay Trade" for Drift
- **Strategy executed**: ExecuteBfexplorerStrategySettings called with correct parameters
- **Execution confirmed**: Strategy activation successful

### Error Handling:
- **No active market**: Report "No active market found" and wait
- **WeightOfMoneyData unavailable**: Report "Weight of Money data not available"
- **No high confidence signals**: Report "No predictions meet 70% confidence threshold"
- **Strategy execution failure**: Log error and report execution failure
- **Invalid market/selection IDs**: Validate IDs before execution attempt

## Risk Warnings
- **Maximum daily exposure**: Do not exceed 10% of bankroll across all strategies
- **Market volatility**: Horse racing markets can move rapidly
- **Liquidity risk**: Ensure sufficient market depth before execution
- **Time decay**: All strategies must be closed before race start
- **System failure**: Always have manual override capability
