# Horse Racing Expected Value Analysis with Trading Data & Automated Betting

## Comprehensive EV Analysis and Betting Execution Prompt (Trading Data Focus)

```
Task: Perform comprehensive Expected Value (EV) analysis for horse racing betting opportunities using market trading data, select the best horse to back, and execute a betting strategy. This analysis combines trading pattern interpretation and price movement analysis with mathematical EV calculations to identify the optimal betting opportunity and automatically place the bet.

Instructions:

1. **Market Data Retrieval**
   - Retrieve the active betfair market in BfexplorerApp using tool: GetActiveBetfairMarket
   - Save marketId for subsequent data retrieval
   - Make no preliminary reports during data collection

2. **Trading Data Collection**
   - Retrieve the data context with the name 'MarketSelectionsTradedPricesData' for the betfair market using tool: GetDataContextForBetfairMarket
   - Focus exclusively on the 'tradedPricesData' field for each selection
   - Do not make any reports during data collection phase

3. **Trading Pattern Analysis**   **CRITICAL: Price-Probability Relationship and Movement Analysis**
   - **Probability = 1 / Price** (e.g., price 4.0 = 25% probability, price 2.0 = 50% probability)
   - **Price Shortening** = Price DECREASING = Probability INCREASING = Positive market signal
   - **Price Drifting/Lengthening** = Price INCREASING = Probability DECREASING = Negative market signal
   - **IMPORTANT**: Calculate probability changes, not price changes, for market confidence assessment
   - **Example**: Price 7.4→6.4 = Probability 13.5%→15.6% = +15.6% probability improvement (POSITIVE signal)

   - Analyze each horse's trading data focusing on price movements and market confidence
   - Perform deep analysis of trading patterns focusing on:
       a) **Probability Movement Patterns:**
        - Direction and magnitude of probability changes from start to current (calculate as probability percentages, not price percentages)
        - Probability stability vs volatility (derived from min/max price range converted to probabilities)
        - Trend consistency (steady probability increases/decreases vs erratic movement)
        - Current probability position relative to trading range
     
     b) **Market Confidence Indicators:**
        - Traded volume as indicator of market interest and confidence
        - Probability increasing (price shortening) with high volume (strong money backing)
        - Probability decreasing (price drifting) with low volume (lack of support)
        - Sudden probability movements indicating insider information or market shifts
     
     c) **Value Identification Signals:**
        - Horses whose current probability is near minimum traded probability (potential overlays)
        - Horses with consistent probability increases and strong volume (market confidence)
        - Horses with minimal probability movement but reasonable volume (stable assessment)
        - Horses showing probability decreases despite trading activity (potential value emerging)
     
     d) **Market Efficiency Factors:**
        - Comparison of start price vs current price movements
        - Volume distribution across different price levels
        - Market timing indicators (early vs late money)
        - Liquidity patterns and trading depth

4. **Win Probability Assessment Based on Trading Data**
   - Assign win probabilities based on trading pattern analysis
   - Consider market confidence indicators and price movement significance
   - Weight factors:
     * Strong volume with price shortening = higher probability
     * Stable prices with good volume = solid probability assessment
     * Price drifting with low volume = lower probability
     * Extreme price movements = reassess based on volume context
   - Ensure probabilities are realistic and sum to approximately 100%
   - Document reasoning for each probability assignment

5. **Expected Value Calculations**
   - Calculate EV for backing each horse using formula:
     EV = (Win Probability × (Decimal Odds - 1)) - (1 - Win Probability)
   - Identify horses with positive EV (value bets)
   - Identify horses with significantly negative EV (lay opportunities)
   - Rank all horses by EV from highest to lowest

6. **Silent Analysis Phase**
   - Complete all analysis without generating interim reports
   - Focus on identifying the single best betting opportunity
   - Prepare comprehensive final analysis only

7. **Final Analysis Report and Best Selection**

   Provide structured report containing:

   **A) Executive Summary:**
   - Selected horse for backing with clear justification
   - Key reasons for selection (combination of EV and trading analysis)
   - Expected value and win probability of selected horse
   - Risk assessment for the recommended bet

   **B) Individual Horse Analysis:**
   - Horse name and current market price
   - Trading pattern analysis summary (2-3 sentences)
   - Assigned win probability with reasoning
   - Calculated Expected Value
   - Final ranking position

   **C) EV Rankings Table:**
   - All horses ranked by Expected Value (highest to lowest)
   - Include: Horse Name, Current Price, Start Price, Price Change, Traded Volume, Win Probability, EV, EV Rating
   - Highlight the selected horse for betting

   **D) Selection Justification:**
   - Detailed explanation of why the chosen horse represents the best betting opportunity
   - Combination of positive EV and strong trading indicators
   - Risk-reward assessment based on market data
   - Confidence level in the selection   
   
   **E) Market Analysis Summary:**
   - Overall market efficiency assessment based on trading patterns
   - Key value opportunities identified through price/volume analysis
   - Market biases or mispricing indicated by trading data
   - Volume distribution and liquidity assessment

   **F) Strategy Selection Decision:**
   - Clear explanation of whether betting or trading strategy was chosen
   - Criteria met for the selected strategy approach
   - Risk assessment for the chosen strategy type
   - Expected outcome based on strategy selection

8. **Automated Strategy Execution**
   - After completing the analysis and identifying the best horse
   - Activate the selected horse's market and selection using tool: ActivateBetfairMarketSelection   - Apply conditional strategy selection:
     * **Use "Back trailing stop loss trading"** when selection meets betting criteria:
       - Positive Expected Value (EV > 0.1)
       - Win probability ≥ 15%
       - Strong trading confidence indicators (high volume + favorable price movement)
       - Current price represents clear value (near maximum range or strong shortening with volume)
       - Strategy locks in any profit when odds turn against the opening back bet (trailing stop)
     * **Use "Trade 20% profit" strategy** when selection meets trading criteria:
       - Moderate Expected Value (EV > 0 but < 0.1)
       - Win probability between 8-15%
       - Volatile price movements with trading opportunities
       - Current price shows potential for profitable trading rather than outright backing
       - Strategy takes exactly 20% profit from the opening back bet when available
   - Execute the appropriate strategy using tool: ExecuteBfexplorerStrategySettings
   - Confirm strategy execution and document reasoning for strategy choice

Format: Present the final analysis in clear, actionable format with emphasis on the single best opportunity. After analysis completion, automatically execute either the betting or trading strategy on the selected horse based on suitability criteria.

Selection Criteria Priority:
1. Positive Expected Value (EV > 0)
2. Strong trading confidence indicators (volume + price movement)
3. Reasonable win probability for betting (typically >15%) or trading opportunity (8-15%)
4. Manageable risk profile
5. Clear market mispricing signals based on trading data

**Strategy Selection Criteria:**
- **Trading Strategy 1 ("Back trailing stop loss trading"):** High confidence selections with EV > 0.1, win probability ≥ 15%, and strong value signals. Takes any available profit when odds move against the opening back bet (trailing stop protection).
- **Trading Strategy 2 ("Trade 20% profit"):** Moderate confidence selections with trading volatility and profit potential. Takes exactly 20% profit from the opening back bet when the target is reached.

Trading Data Interpretation Guidelines:

**Strong Positive Signals:**
- Probability increasing (price shortening) with high traded volume
- Stable probability near maximum with consistent volume
- Current probability at or near minimum range (potential overlay)

**Moderate Positive Signals:**
- Minimal probability movement with steady volume
- Probability increasing (price shortening) with moderate volume
- Trading range positioning favoring current assessment

**Negative Signals:**
- Probability decreasing (price drifting) with declining volume
- Extreme volatility without volume justification
- Current price at minimum with low volume (no support)

**Market Efficiency Indicators:**
- High volume horses generally more efficiently priced
- Low volume horses may contain more value opportunities
- Dramatic price movements require volume confirmation

Note: This analysis relies on market trading data interpretation rather than performance narratives, focusing on what the collective market intelligence reveals through price movements and trading volumes. The process culminates in automated strategy execution (betting or trading) on the optimal selection identified through trading pattern analysis.
```

## Strategy Details

### Back Trailing Stop Loss Trading
- **Mechanism**: Places an opening back bet, then monitors for any profit opportunity
- **Exit Trigger**: Takes profit immediately when odds move against the opening back bet
- **Risk Management**: Trailing stop protection ensures no profit is left on the table
- **Best For**: High-confidence selections with strong value where market movement is expected
- **Profit Potential**: Variable - captures any available profit when odds shift unfavorably

### Trade 20% Profit  
- **Mechanism**: Places an opening back bet with a fixed profit target
- **Exit Trigger**: Takes exactly 20% profit from the opening back bet when target is reached
- **Risk Management**: Fixed profit target with predetermined exit point
- **Best For**: Moderate confidence selections with expected price volatility
- **Profit Potential**: Fixed at 20% of the opening back bet stake

## Usage Instructions

1. **Input Requirements:**
   - Active Betfair market with horse racing data
   - Access to MarketSelectionsTradedPricesData context
   - Current market prices for all runners
   - Access to Bfexplorer betting functionality

2. **Expected Output:**
   - Complete EV analysis for all horses in race based on trading data
   - Clear identification of best horse with appropriate strategy selection
   - Automated execution of either:
     * "Back trailing stop loss trading" (locks in profit when odds turn against opening back bet)
     * "Trade 20% profit" (takes exactly 20% profit from opening back bet)
   - Confirmation of strategy execution with reasoning

3. **Automated Process:**
   - Silent data collection and analysis phase
   - Comprehensive final report with best selection and strategy choice
   - Automatic market activation and strategy execution (betting or trading)
   - No user intervention required during process

## Selection Framework

The analysis will automatically select the horse that best combines:

**Primary Criteria:**
- **Positive Expected Value** - Mathematical edge in the bet/trade
- **Strong Trading Signals** - Positive price/volume indicators
- **Appropriate Probability Range** - Betting (>15%) or Trading (8-15%) suitability

**Secondary Criteria:**
- **Market Confidence** - Strong volume supporting price movements
- **Price Positioning** - Favorable position within trading range
- **Strategy Suitability** - Clear betting value vs trading opportunity signals

## Trading Data Analysis Framework

**Price Movement Analysis:**
- **Shortening** = Market confidence increasing
- **Drifting** = Market confidence decreasing  
- **Stable** = Market consensus established
- **Volatile** = Uncertainty or information flow

**Volume Analysis:**
- **High Volume** = Strong market interest and confidence
- **Moderate Volume** = Standard market assessment
- **Low Volume** = Limited market conviction
- **Volume + Price Direction** = Confirmation of market sentiment

**Value Identification:**
- **Current Price vs Range** = Position relative to min/max
- **Volume Support** = Does trading activity justify current price
- **Movement Direction** = Is market moving toward or away from horse
- **Strategy Assessment** = Is current situation better for betting (clear value) or trading (volatility opportunity)

## Automated Execution Sequence

1. Complete comprehensive EV analysis using trading data (silent phase)
2. Generate final report with best horse selection and strategy determination
3. Activate selected horse's market and selection
4. Execute appropriate strategy automatically:
   - **"Back trailing stop loss trading"** for high-confidence betting opportunities (locks in profit when odds turn against opening back bet)
   - **"Trade 20% profit"** for trading opportunities with volatility (takes exactly 20% profit from opening back bet)
5. Confirm successful strategy execution with reasoning

This prompt ensures thorough analysis of market trading patterns while maintaining focus on identifying and executing the optimal strategy (betting or trading) on the single best opportunity available based on collective market intelligence and mathematical edge.
