# Weight of Money Analysis Prompt

## Objective
Analyze Betfair market weight of money data to predict price movements for all selections in a betting market.

## Analysis Framework

### 1. Data Points to Examine
- **Average Back Traded Price & Volume**: Shows where money has been backing the selection
- **Average Lay Traded Price & Volume**: Shows where money has been laying against the selection
- **Current Market Price**: The present best back price available
- **Traded Prices History**: Complete trading history showing volume at each price level
  - **NEW**: Provides detailed view of where all money has traded
  - **Price Distribution**: Shows concentration of trading activity across price ranges
  - **Volume Patterns**: Identifies price levels with highest trading interest
  - **Volume-Weighted Average Price (VWAP)**: More accurate than simple averages
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

## Output Format

Provide analysis in the following table format:

| Selection | Current | Avg Back | Avg Lay | VWAP | Vol Ratio (B:L) | Vol Conc | Prediction | Confidence | Expected Range |
|-----------|---------|----------|---------|------|------------------|----------|------------|------------|----------------|
| Horse Name | 5.2 | 5.34 | 5.48 | 5.41 | 1.3:1 | 68% | Drift ⬆️ | 78% 🟡 | 5.5-5.8 |

**Column Explanations:**
- **VWAP**: Volume-Weighted Average Price from complete trading history
- **Vol Conc**: Volume Concentration - % of total volume in top 3 traded prices
- **Vol Ratio (B:L)**: Historical backing:laying volume ratio

## Prediction Symbols
- ⬆️ Drift (price increase)
- ⬇️ Shorten (price decrease)  
- ➡️ Stable (minimal change)
- 🔴 Strong movement expected
- 🟡 Moderate movement expected
- 🟢 Minor movement expected

## Confidence Scoring System (Numerical)

### Confidence Calculation (0-100%):
Calculate confidence score by summing points from each factor:

#### Historical Volume Signal Strength (0-25 points):
- **Extreme imbalance** (>2:1 ratio): 25 points
- **Strong imbalance** (1.5-2:1 ratio): 18 points  
- **Moderate imbalance** (1.2-1.5:1 ratio): 10 points
- **Balanced** (0.8-1.2:1 ratio): 0 points

#### Price Deviation Signal (0-20 points):
- **>20% deviation** from historical average: 20 points
- **10-20% deviation**: 15 points
- **5-10% deviation**: 8 points
- **<5% deviation**: 0 points

#### Traded Prices Distribution Analysis (0-20 points):
- **Strong concentration** (>60% volume in top 3 prices): 20 points
- **Moderate concentration** (40-60% volume in top 3 prices): 15 points
- **Balanced distribution** (20-40% volume in top 3 prices): 10 points
- **Wide distribution** (<20% volume in top 3 prices): 5 points

#### Offered Prices Signal Strength (0-20 points):
- **Extreme offered imbalance** (>3:1 ratio): 20 points
- **Strong offered imbalance** (2-3:1 ratio): 15 points
- **Moderate offered imbalance** (1.5-2:1 ratio): 10 points
- **Balanced offered prices** (0.8-1.5:1 ratio): 0 points

#### Signal Alignment (0-15 points):
- **All signals align** (historical + current + offered + traded distribution): 15 points
- **Three signals align**: 12 points
- **Two signals align**: 8 points
- **One signal only**: 4 points
- **Conflicting signals**: -5 points

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
- **85-100%**: Very High Confidence 🔴
- **70-84%**: High Confidence 🟡  
- **55-69%**: Medium Confidence 🟠
- **40-54%**: Low Confidence 🟢
- **<40%**: Very Low Confidence ⚪

## Analysis Steps

### Step 1: Get Active Market
1. **Retrieve active market**: Use GetActiveBetfairMarket to get the currently monitored market
2. **Extract market details**: Get marketId, market name, and event information

### Step 2: Get Weight of Money Data
1. **Retrieve WeightOfMoneyData**: Use GetDataContextForBetfairMarket with:
   - dataContextName: "WeightOfMoneyData"
   - marketId: [from Step 1]
2. **Parse data structure**: Extract for all selections:
   - Average traded prices and volumes (averageBackTraded, averageLayTraded)
   - Complete trading history (tradedPrices array with price and volume pairs)
   - Current offered prices ladder (offeredPrices with betType, price, volume)

### Step 3: Data Analysis Process
1. Calculate volume ratios (backing:laying) from historical traded data
2. **Calculate Volume-Weighted Average Price (VWAP)** for each selection from tradedPrices
3. **Analyze trading volume distribution**:
   - Identify price levels with highest volumes
   - Calculate volume concentration in top 3 traded prices
   - Determine if trading is concentrated or distributed
4. Compare current price to historical averages and VWAP for each selection
5. Assess price deviation percentage for each selection
6. Analyze offered prices ladder for each selection:
   - Sum BetType 2 volumes = Available backing liquidity (indicates laying pressure)
   - Sum BetType 1 volumes = Available laying liquidity (indicates backing pressure)
7. Calculate base confidence score using the updated numerical system
8. **Calculate total traded volume** from complete tradedPrices array
9. **Apply volume liquidity adjustment** to reduce confidence for low-volume selections

### Step 4: Market Analysis and Predictions
10. Determine movement direction and strength for each selection based on:
    - Historical volume imbalances
    - Current price vs averages and VWAP
    - Trading volume distribution patterns
    - Offered liquidity imbalances
11. Assign final confidence percentage and category for each selection
12. Provide expected price range for next significant move for each selection

### Step 5: Generate Analysis Output
13. Create comprehensive predictions table with all selections including VWAP and volume concentration
14. Identify and highlight key trading opportunities
15. Provide market dynamics summary including volume distribution insights
16. Include risk assessment for identified opportunities

## Usage Instructions

### Analysis Execution Sequence:
1. **Get Active Market**: Use GetActiveBetfairMarket to retrieve current market information
2. **Get Weight of Money Data**: Use GetDataContextForBetfairMarket with dataContextName "WeightOfMoneyData"
3. **Apply Analysis Framework**: Process each selection using the confidence scoring system
4. **Generate Comprehensive Report**: Create predictions table and trading opportunities analysis
5. **Provide Strategic Insights**: Highlight key patterns and market inefficiencies

### Expected Output Format:
- **Market Overview**: Basic market information and status
- **Predictions Table**: All selections with confidence scores and price predictions
- **Key Trading Opportunities**: High confidence trades by category
- **Risk Assessment**: Evaluation of identified opportunities
- **Market Dynamics**: Volume analysis and price efficiency insights

### Error Handling:
- **No active market**: Report "No active market found"
- **WeightOfMoneyData unavailable**: Report "Weight of Money data not available for this market"
- **Insufficient data**: Note any selections with limited trading history
- **Data quality issues**: Flag any anomalies in the weight of money data
