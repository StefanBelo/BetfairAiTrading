# Weight of Money Strategy Execution Prompt

## Objective
Analyze Betfair market weight of money data, identify the highest confidence prediction, and automatically execute the appropriate trading strategy based on the prediction direction.

### Automatic Strategy Selection:
- **Shorten Prediction (⬇️)**: Execute "Back Trade" strategy on highest confidence selection
- **Drift Prediction (⬆️)**: Execute "Lay Trade" strategy on highest confidence selection
- **Minimum Confidence**: Only execute if confidence ≥ 40%
- **Market Position Filter**: Only execute on selections among the 3 favorites (lowest odds)

## Analysis Framework

### 1. Data Points to Examine
- **Average Back Traded Price & Volume**: Shows where money has been backing the selection
- **Average Lay Traded Price & Volume**: Shows where money has been laying against the selection
- **Current Market Price**: The present best back price available
- **Traded Prices History**: Complete trading history showing volume at each price level
  - **NEW**: Provides detailed view of where all money has traded
  - **Price Distribution**: Shows concentration of trading activity across price ranges
  - **Volume Patterns**: Identifies price levels with highest trading interest
- **Offered Prices Ladder**: Distribution of available liquidity at different price levels
  - **CRITICAL**: BetType 1 (Back) volume = Money available to LAY (lay offers)
  - **CRITICAL**: BetType 2 (Lay) volume = Money available to BACK (back offers)
- **Volume Imbalance**: Compare backing vs laying volumes to identify market sentiment

### 2. Key Indicators for Price Movement

#### Price Direction Signals:
- **Current vs Historical Averages**: If current price significantly differs from traded averages
- **Traded Price Distribution**: Analysis of where the bulk of trading volume has occurred
  - **High Volume Zones**: Price levels with significant trading activity indicate strong support/resistance
  - **Price Migration**: Movement of trading volume from higher to lower prices (or vice versa)
  - **Volume Concentration**: Whether trading is concentrated at few price levels or distributed
- **Volume Imbalance**: Heavy backing suggests price shortening, heavy laying suggests drifting
- **Liquidity Distribution**: Where the weight of money is positioned on the price ladder
  - **Backing Pressure**: High volume at BetType 2 (Lay) prices = Strong backing interest
  - **Laying Pressure**: High volume at BetType 1 (Back) prices = Strong laying interest

#### Strength Indicators:
- **Volume Magnitude**: Higher volumes indicate stronger signals
- **Price Deviation**: Larger gaps between current and average prices suggest bigger moves
- **Traded Volume Distribution**: How trading volume is spread across price levels
  - **Concentrated Trading**: Most volume at few price levels suggests strong conviction
  - **Distributed Trading**: Volume spread across many prices suggests uncertainty
  - **Volume Weighted Average**: Price weighted by actual trading volumes
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

### 5. Favorites Filter Rationale
- **Liquidity Focus**: The 3 favorites typically have the highest liquidity and most reliable price movements
- **Market Efficiency**: Favorites receive the most attention and analysis, making weight of money signals more meaningful
- **Risk Management**: Outsiders can have erratic price movements unrelated to fundamental value
- **Strategy Effectiveness**: Trading strategies work best on selections with consistent market participation
- **Execution Quality**: Better fills and tighter spreads available on favorites

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

## NEW: Traded Prices Analysis Framework

### Understanding Traded Prices Data
The **tradedPrices** array provides complete trading history showing:
- **Price**: Each price level where trading occurred
- **Volume**: Total volume traded at that specific price

### Key Analysis Techniques:

#### 1. Volume-Weighted Average Price (VWAP)
```
VWAP = Σ(Price × Volume) / Σ(Volume)
```
- More accurate than simple averages
- Shows true center of trading activity
- Compare current price to VWAP for direction signals

#### 2. Volume Distribution Analysis
- **High Volume Zones**: Price levels with >10% of total volume
- **Support/Resistance**: Prices with significant trading create psychological levels
- **Volume Concentration**: Calculate what % of volume is in top 3 traded prices

#### 3. Price Migration Patterns
- **Upward Migration**: Recent trading at higher prices than historical
- **Downward Migration**: Recent trading at lower prices than historical
- **Stable Trading**: Consistent price levels over time

#### 4. Market Sentiment Indicators
- **Heavy Trading Above Current**: Suggests market expects higher prices
- **Heavy Trading Below Current**: Suggests market expects lower prices
- **Balanced Distribution**: Uncertain market sentiment

## Confidence Scoring System (Numerical)

### Confidence Calculation (0-100%):
Calculate confidence score by summing points from each factor:

#### Historical Volume Signal Strength (0-30 points):
- **Extreme imbalance** (>2:1 ratio): 30 points
- **Strong imbalance** (1.5-2:1 ratio): 25 points  
- **Moderate imbalance** (1.2-1.5:1 ratio): 15 points
- **Balanced** (0.8-1.2:1 ratio): 0 points

#### Price Deviation Signal (0-25 points):
- **>20% deviation** from historical average: 25 points
- **10-20% deviation**: 18 points
- **5-10% deviation**: 12 points
- **<5% deviation**: 5 points

#### Traded Prices Distribution Analysis (0-20 points):
- **Strong concentration** (>60% volume in top 3 prices): 20 points
- **Moderate concentration** (40-60% volume in top 3 prices): 15 points
- **Balanced distribution** (20-40% volume in top 3 prices): 10 points
- **Wide distribution** (<20% volume in top 3 prices): 5 points

#### Offered Prices Signal Strength (0-25 points):
- **Extreme offered imbalance** (>3:1 ratio): 25 points
- **Strong offered imbalance** (2-3:1 ratio): 20 points
- **Moderate offered imbalance** (1.5-2:1 ratio): 15 points
- **Balanced offered prices** (0.8-1.5:1 ratio): 5 points

#### Signal Alignment (0-25 points):
- **All signals align** (historical + current + offered + traded distribution): 25 points
- **Three signals align**: 20 points
- **Two signals align**: 15 points
- **One signal only**: 8 points
- **Conflicting signals**: 0 points

#### Volume Liquidity Adjustment (Multiplier):
**Total Traded Volume** = Sum of all volumes in tradedPrices array

**Volume Adjustment Factor:**
- **High Volume** (>1000 total): 1.0 (no adjustment)
- **Medium Volume** (500-1000 total): 0.9 (10% reduction)
- **Low Volume** (100-500 total): 0.8 (20% reduction)
- **Very Low Volume** (50-100 total): 0.6 (40% reduction)
- **Minimal Volume** (<50 total): 0.4 (60% reduction)

**Final Confidence = Base Confidence × Volume Adjustment Factor**

### Confidence Categories:
- **80-100%**: Very High Confidence 🔴
- **60-79%**: High Confidence 🟡  
- **40-59%**: Medium Confidence 🟠
- **25-39%**: Low Confidence 🟢
- **<25%**: Very Low Confidence ⚪

## Trading Strategy Parameters

### Strategy 1: Back Trade (for Shorten Predictions ⬇️)

#### Entry Conditions:
- **Prediction**: Shorten (⬇️) with confidence ≥ 40%
- **Entry Price**: Current market price or better
- **Position Size**: Based on confidence level:
  - **80-100% confidence**: 3-5% of bankroll
  - **60-79% confidence**: 2-3% of bankroll
  - **40-59% confidence**: 1-2% of bankroll

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
- **Prediction**: Drift (⬆️) with confidence ≥ 40%
- **Entry Price**: Current market price or better
- **Position Size**: Based on confidence level:
  - **80-100% confidence**: 2-3% liability of bankroll
  - **60-79% confidence**: 1-2% liability of bankroll
  - **40-59% confidence**: 0.5-1% liability of bankroll

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
1. **Retrieve active market**: Use GetActiveMarket to get the currently monitored market
2. **Extract market details**: Get marketId and market information

### Step 2: Get Weight of Money Data
1. **Retrieve WeightOfMoneyData**: Use GetDataContextForMarket with:
   - dataContextName: "WeightOfMoneyData"
   - marketId: [from Step 1]
2. **Parse data structure**: Extract for all selections:
   - Average traded prices and volumes (averageBackTraded, averageLayTraded)
   - Complete trading history (tradedPrices array with price and volume pairs)
   - Current offered prices ladder (offeredPrices with betType, price, volume)

### Step 3: Market Analysis
1. Calculate volume ratios (backing:laying) from historical traded data
2. Compare current price to historical averages
3. Assess price deviation percentage
4. **Analyze traded prices distribution**:
   - Calculate volume-weighted average price from tradedPrices
   - Identify price levels with highest trading volumes
   - Determine if trading is concentrated or distributed
   - Compare current price to volume-weighted historical prices
5. Analyze offered prices ladder for each selection
6. Calculate confidence scores for all selections using the numerical system
7. Apply volume liquidity adjustment using total traded volume from tradedPrices array

### Step 4: Selection Identification
1. **Filter to top 3 favorites**: Identify the 3 selections with the lowest current odds (favorites)
2. **Calculate confidence scores**: Only analyze selections among the 3 favorites using the numerical system
3. **Identify highest confidence prediction**: Find selection with highest confidence ≥ 40% among favorites only
4. **Determine prediction direction**: Shorten (⬇️) or Drift (⬆️) based on analysis
5. **Validate execution criteria**: Ensure confidence threshold is met and selection is a favorite

### Step 5: Strategy Selection and Execution
1. **If highest confidence prediction is Shorten (⬇️) and selection is among 3 favorites**:
   - Execute "Back Trade" strategy
   - Use ExecuteBfexplorerStrategySettings with strategyName: "Back Trade"
2. **If highest confidence prediction is Drift (⬆️) and selection is among 3 favorites**:
   - Execute "Lay Trade" strategy
   - Use ExecuteBfexplorerStrategySettings with strategyName: "Lay Trade"
3. **If no prediction meets 40% confidence threshold among favorites**:
   - Do not execute any strategy
   - Report "No high confidence signals found among the 3 favorites"
4. **If highest confidence is outside top 3 favorites**:
   - Do not execute any strategy
   - Report "Highest confidence signal not among the 3 favorites"

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
- **Minimum Confidence**: 40% required for execution
- **Market Position**: Selection must be among the 3 favorites (lowest odds)
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
1. **Get Active Market**: Use GetActiveMarket to retrieve current market
2. **Get Weight of Money Data**: Use GetDataContextForMarket with dataContextName "WeightOfMoneyData"
3. **Parse Complete Data**: Extract averageBackTraded, averageLayTraded, tradedPrices, and offeredPrices
4. **Filter Favorites**: Identify the 3 selections with lowest current odds (favorites)
5. **Analysis Phase**: Run comprehensive analysis including:
   - Historical volume ratios
   - Price deviation from averages and VWAP
   - Traded prices distribution analysis
   - Offered prices ladder analysis
   - Apply analysis only to the 3 favorites
6. **Selection Phase**: Identify highest confidence prediction ≥ 40% among favorites only
7. **Strategy Phase**: Select "Back Trade" or "Lay Trade" based on prediction direction
8. **Execution Phase**: Execute strategy using:
   ```
   ExecuteBfexplorerStrategySettings(
     strategyName: "Back Trade" or "Lay Trade",
     marketId: "[Market ID]", 
     selectionId: "[Selection ID of highest confidence prediction among favorites]"
   )
   ```
9. **Monitoring Phase**: Strategy will handle position management automatically

### Success Criteria:
- **Market retrieved**: Successfully get active market data
- **Weight of Money data obtained**: WeightOfMoneyData context retrieved with complete tradedPrices
- **Favorites identified**: Top 3 selections with lowest odds determined
- **Complete analysis performed**: Historical, current, and traded prices distribution analyzed
- **Confidence threshold met**: ≥ 40% for strategy execution among favorites
- **Appropriate strategy selected**: "Back Trade" for Shorten, "Lay Trade" for Drift
- **Strategy executed**: ExecuteBfexplorerStrategySettings called with correct parameters
- **Execution confirmed**: Strategy activation successful

### Error Handling:
- **No active market**: Report "No active market found" and wait
- **WeightOfMoneyData unavailable**: Report "Weight of Money data not available"
- **No high confidence signals among favorites**: Report "No predictions meet 40% confidence threshold among the 3 favorites"
- **Highest confidence outside favorites**: Report "Highest confidence signal not among the 3 favorites"
- **Strategy execution failure**: Log error and report execution failure
- **Invalid market/selection IDs**: Validate IDs before execution attempt

## Risk Warnings
- **Maximum daily exposure**: Do not exceed 10% of bankroll across all strategies
- **Market volatility**: Horse racing markets can move rapidly
- **Liquidity risk**: Ensure sufficient market depth before execution
- **Time decay**: All strategies must be closed before race start
- **System failure**: Always have manual override capability
