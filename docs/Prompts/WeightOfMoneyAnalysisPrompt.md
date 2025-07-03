# Weight of Money Analysis Prompt

## Objective
Analyze Betfair market weight of money data to predict price movements for all selections in a betting market.

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

## Output Format

Provide analysis in the following table format:

| Selection | Current Price | Avg Back | Avg Lay | Volume Ratio (B:L) | Prediction | Confidence | Expected Range |
|-----------|---------------|----------|---------|-------------------|------------|------------|----------------|
| Horse Name | 5.2 | 5.34 | 5.48 | 1.3:1 | Drift ⬆️ | 78% 🟡 | 5.5-5.8 |

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

## Analysis Steps

### Step 1: Get Active Market
1. **Retrieve active market**: Use GetActiveBetfairMarket to get the currently monitored market
2. **Extract market details**: Get marketId, market name, and event information

### Step 2: Get Weight of Money Data
1. **Retrieve WeightOfMoneyData**: Use GetDataContextForBetfairMarket with:
   - dataContextName: "WeightOfMoneyData"
   - marketId: [from Step 1]
2. **Parse data structure**: Extract average traded prices, volumes, and offered prices for all selections

### Step 3: Data Analysis Process
1. Calculate volume ratios (backing:laying) from historical traded data
2. Compare current price to historical averages for each selection
3. Assess price deviation percentage for each selection
4. Analyze offered prices ladder for each selection:
   - Sum BetType 2 volumes = Available backing liquidity (indicates laying pressure)
   - Sum BetType 1 volumes = Available laying liquidity (indicates backing pressure)
5. Calculate base confidence score using the numerical system above
6. **Calculate total traded volume** (averageBackTraded.volume + averageLayTraded.volume)
7. **Apply volume liquidity adjustment** to reduce confidence for low-volume selections

### Step 4: Market Analysis and Predictions
8. Determine movement direction and strength for each selection based on:
   - Historical volume imbalances
   - Current price vs averages
   - Offered liquidity imbalances
9. Assign final confidence percentage and category for each selection
10. Provide expected price range for next significant move for each selection

### Step 5: Generate Analysis Output
11. Create comprehensive predictions table with all selections
12. Identify and highlight key trading opportunities
13. Provide market dynamics summary
14. Include risk assessment for identified opportunities

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
