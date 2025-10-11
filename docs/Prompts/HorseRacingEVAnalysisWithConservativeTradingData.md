# Horse Racing Expected Value Analysis with Conservative Trading Data Strategy

## Conservative EV Analysis and Betting Execution Prompt (Trading Data Focus)

```
Task: Perform comprehensive Expected Value (EV) analysis for horse racing betting opportunities using market trading data with a conservative approach that prioritizes the market favorite when it demonstrates fair or better expected value. This analysis combines trading pattern interpretation with conservative risk management to identify value in market leaders while maintaining capital preservation focus.

Instructions:

1. **Market Data Retrieval**
   - Retrieve the active betfair market in BfexplorerApp using tool: GetActiveBetfairMarket
   - Save marketId for subsequent data retrieval
   - Make no preliminary reports during data collection

2. **Dual Data Source Collection**
   - Retrieve the data context with the name 'MarketSelectionsTradedPricesData' for the betfair market using tool: GetDataContextForBetfairMarket
   - Retrieve the data context with the name 'RacingpostDataForHorses' for the betfair market using tool: GetDataContextForBetfairMarket
   - Focus on 'tradedPricesData' field for trading analysis AND 'horsesData' field for performance analysis
   - Do not make any reports during data collection phase

3. **Combined Trading Pattern and Performance Analysis**   
   **CRITICAL: Price-Probability Relationship and Movement Analysis**
   - **Probability = 1 / Price** (e.g., price 4.0 = 25% probability, price 2.0 = 50% probability)
   - **Price Shortening** = Price DECREASING = Probability INCREASING = Positive market signal
   - **Price Drifting/Lengthening** = Price INCREASING = Probability DECREASING = Negative market signal
   - **IMPORTANT**: Calculate probability changes, not price changes, for market confidence assessment
   - **Example**: Price 7.4→6.4 = Probability 13.5%→15.6% = +15.6% probability improvement (POSITIVE signal)

   - Analyze each horse using BOTH data sources for comprehensive assessment:
   
   **A) Trading Data Analysis** - Perform deep analysis of trading patterns focusing on:
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

   **B) Performance Data Analysis** - Analyze each horse's 'lastRacesDescription' field focusing on:
     
     a) **Performance Patterns:**
        - Consistency of competitive positions across recent races
        - Frequency of strong finishes ("led", "kept on well", "ran on", "finished strongly")
        - Recovery ability from setbacks and late race surge capability
        - Professional racing behavior and tactical racing
     
     b) **Positive Performance Indicators:**
        - Winning performances and competitive margins
        - Strong finishing kicks and sustained runs throughout races
        - Consistent involvement in race finish and competitive throughout duration
        - Evidence of improvement or progression in class/performance level
     
     c) **Negative Performance Indicators:**
        - Frequent mentions of "weakened", "outpaced", "struggling", "faded"
        - Jumping errors, hesitation, or technical issues
        - Physical problems (lameness, stumbling, breathing difficulties)
        - Behavioral problems (hanging, running green, refusing)
     
     d) **Contextual Performance Factors:**
        - Ground condition preferences and performance correlation
        - Equipment changes impact (blinkers, cheek-pieces, tongue ties)
        - Distance suitability and course preferences
        - Class level performance and recent form trends

   **C) Combined Assessment Integration:**
        - Cross-reference trading confidence with performance reliability
        - Identify horses where strong performance indicators align with positive trading signals
        - Flag discrepancies where trading data contradicts performance analysis
        - Weight trading signals more heavily for market efficiency, performance data for horse capability assessment

4. **Win Probability Assessment Based on Combined Data Analysis**
   - Assign win probabilities based on integrated trading pattern and performance analysis
   - Consider both market confidence indicators and actual racing capability
   - Weight factors for conservative approach:
     * **Trading Signals (60% weight)**: Strong volume with price shortening = higher probability
     * **Performance Quality (40% weight)**: Consistent competitive form and positive indicators
     * Combined positive signals = significantly higher confidence
     * Conflicting signals = moderate probability with risk assessment
     * Negative signals in both = lower probability assignment
   - Ensure probabilities are realistic and sum to approximately 100%
   - Document reasoning for each probability assignment with reference to both data sources

5. **Expected Value Calculations**
   - Calculate EV for backing each horse using formula:
     EV = (Win Probability × (Decimal Odds - 1)) - (1 - Win Probability)
   - Assign EV ratings based on calculated values:
     - **Excellent**: EV > +0.20
     - **Good**: EV +0.05 to +0.20
     - **Fair**: EV -0.05 to +0.05
     - **Poor**: EV -0.20 to -0.05
     - **Very Poor**: EV < -0.20
   - Identify horses with positive EV (value bets)
   - Identify horses with significantly negative EV (lay opportunities)
   - Rank all horses by EV from highest to lowest

6. **Market Favorite Identification and Combined Analysis**
   - Identify the market favorite (horse with lowest price/highest implied probability)
   - Analyze the favorite comprehensively using both data sources:
     * **Trading Analysis**: Volume support, price movement direction and magnitude, market confidence indicators, position within trading range
     * **Performance Analysis**: Recent form quality, consistency patterns, positive/negative indicators from race descriptions
     * **Combined Assessment**: How well trading confidence aligns with performance capability
   - Calculate the favorite's Expected Value and assign EV rating
   - Assess overall confidence level based on data source agreement/disagreement

7. **Conservative Selection Decision with Combined Data Analysis**
   - **PRIMARY CRITERIA**: Evaluate market favorite first using both data sources:
     a) The horse is the market favorite (lowest price)
     b) The favorite's EV rating is "Fair" or better (EV ≥ -0.05)
     c) The favorite shows positive or neutral trading signals (stable/shortening price with volume support)
     d) The favorite shows positive or neutral performance indicators (no major form concerns)
     e) Both trading volume and performance quality provide confidence in assessment
   
   - **ALTERNATIVE CRITERIA**: If favorite doesn't meet criteria, evaluate best alternative using combined assessment:
     a) Horse with highest positive EV that meets both trading and performance confidence criteria
     b) EV > +0.10 (minimum threshold for non-favorite selection)
     c) Win probability ≥ 15% based on combined trading and performance analysis
     d) Strong positive signals in BOTH trading data (shortening with volume) AND performance data (competitive form)
     e) No major negative indicators in either data source
   
   - **NO-BET CRITERIA**: If neither favorite nor alternatives meet conservative standards:
     a) Favorite has "Poor" or "Very Poor" EV rating (EV < -0.05)
     b) Favorite shows negative indicators in either trading or performance data
     c) No alternative selections meet minimum EV and combined confidence thresholds
     d) Market shows high inefficiency or contradictory signals between data sources

8. **Silent Analysis Phase**
   - Complete all analysis without generating interim reports
   - Focus on evaluating favorite first, then best alternatives
   - Prepare comprehensive final analysis only

9. **Final Analysis Report and Conservative Selection**

   Provide structured report containing:

   **A) Executive Summary:**
   - **SELECTION DECISION**: Chosen horse (favorite or alternative) or NO-BET decision
   - Selected horse's current price and EV rating
   - Key reasons for selection based on trading data and conservative criteria
   - Expected value and win probability of selected horse (if applicable)
   - Risk assessment for conservative approach   **B) Market Favorite Deep Analysis:**
   - Favorite's name and current market price
   - **Trading Pattern Analysis**: Comprehensive probability movements, volume analysis, range position
   - **Performance Analysis**: Detailed recent form assessment from race descriptions, consistency patterns, positive/negative indicators
   - **Combined Assessment**: How trading confidence aligns with performance capability
   - Assigned win probability with detailed reasoning based on both data sources
   - Calculated Expected Value and EV rating
   - Overall confidence level and data source agreement evaluation
   - Conclusion on favorite's suitability for conservative betting

   **C) Alternative Selection Analysis (if applicable):**
   - Alternative horse's name and current market price
   - **Combined Analysis**: Trading patterns AND performance indicators supporting selection
   - **Data Source Alignment**: How well trading and performance data agree
   - Justification for selection over favorite using both data sources
   - Risk-reward comparison with favorite   **D) Complete Field EV Rankings Table:**
   - All horses ranked by Expected Value (highest to lowest)
   - Include: Horse Name, Current Price, Start Price, Price Change, Traded Volume, Performance Rating, Win Probability, EV, EV Rating
   - Highlight the market favorite and selected horse (if different)
   - Show trading confidence indicators and performance assessment for top selections
   - Note data source agreement/disagreement for key contenders   **E) Conservative Selection Justification:**
   - Detailed explanation of conservative betting philosophy using combined data analysis
   - Why chosen horse meets conservative criteria or why no bet was selected
   - **Data Source Integration**: How trading and performance data support the decision
   - **Confidence Assessment**: Quality and agreement between both data sources
   - Comparison of selected horse's value versus field alternatives
   - Risk-reward assessment of conservative approach with dual data validation
   - Market efficiency evaluation based on trading patterns and performance correlation

   **F) Market Analysis Summary:**
   - Overall market efficiency assessment based on trading patterns
   - Favorite's relative value in market context using trading data
   - Key value opportunities identified through price/volume analysis
   - Market biases or mispricing indicated by trading data
   - Volume distribution and liquidity assessment

10. **Conditional Automated Strategy Execution**
    - **IF** selection meets conservative criteria (favorite or alternative):
      - Activate the selected horse's market and selection using tool: ActivateBetfairMarketSelection
      - Apply strategy selection based on confidence level:
        * **Use "Back trailing stop loss trading"** for high confidence selections:
          - Favorite with "Good" or "Excellent" EV rating
          - Alternative with EV > +0.15 and strong trading signals
          - Strategy locks in profit when odds move against opening back bet
        * **Use "Trade 20% profit"** for moderate confidence selections:
          - Favorite with "Fair" EV rating but positive trading signals
          - Alternative with moderate EV (+0.10 to +0.15) and decent trading support
          - Strategy takes exactly 20% profit from opening back bet
      - Execute appropriate strategy using tool: ExecuteBfexplorerStrategySettings
      - Confirm strategy execution
    
    - **IF** no selection meets conservative criteria:
      - Activate the favorite's market and selection using tool: ActivateBetfairMarketSelection
      - Execute the "Lay 10 Euro" strategy on the favorite using tool: ExecuteBfexplorerStrategySettings
      - Confirm lay strategy execution (conservative approach when no value found)

Format: Present the final analysis in clear, actionable format with emphasis on conservative risk management using trading data insights. Execute betting strategy ONLY when selection demonstrates acceptable value with strong trading confidence indicators.

Conservative Selection Criteria Priority:
1. **Market Favorite Evaluation** - Primary focus on lowest-risk market leader
2. **Trading Data Confidence** - Volume and price movement validation
3. **Minimum EV Standards** - Fair value threshold for favorite, higher for alternatives
4. **Risk Management** - Capital preservation over aggressive profit seeking
5. **Market Efficiency Assessment** - Clear value signals from trading patterns

**Strategy Selection Criteria:**
- **"Back trailing stop loss trading":** High confidence selections (favorite with Good+ EV or alternative with excellent signals)
- **"Trade 20% profit":** Moderate confidence selections (favorite with Fair EV or alternative with moderate signals)  
- **"Lay 10 Euro":** No acceptable selections meet conservative criteria

Trading Data Interpretation Guidelines for Conservative Approach:

**High Confidence Signals (Suitable for Conservative Betting):**
- Probability increasing (price shortening) with high traded volume
- Stable probability near maximum with consistent high volume
- Favorite showing neutral to positive trading signals with strong volume support

**Moderate Confidence Signals (Conditional Conservative Betting):**
- Minimal probability movement with steady moderate volume
- Slight probability increases with reasonable volume backing
- Market consensus indicators supporting current assessment

**Low Confidence Signals (Avoid or Lay):**
- Probability decreasing (price drifting) with declining volume
- Extreme volatility without volume justification
- Favorite showing negative trading signals despite market position

**Market Efficiency Indicators for Conservative Approach:**
- High volume favorites generally represent efficient pricing (good for conservative betting)
- Low volume markets may lack reliable price discovery (higher risk)
- Dramatic price movements require volume confirmation before betting

Note: This conservative approach prioritizes capital preservation and reliable value identification while leveraging trading data insights. The strategy acknowledges that favorites often represent the most reliable betting opportunities when supported by positive trading indicators and fair value, while providing systematic alternatives when the favorite fails to meet conservative standards.
```

## Conservative Strategy Details with Trading Data

### Back Trailing Stop Loss Trading (High Confidence)
- **Mechanism**: Places an opening back bet, then monitors for any profit opportunity
- **Exit Trigger**: Takes profit immediately when odds move against the opening back bet
- **Risk Management**: Trailing stop protection ensures no profit is left on the table
- **Conservative Use**: For favorites with Good+ EV ratings or alternatives with exceptional trading signals
- **Profit Potential**: Variable - captures any available profit when odds shift unfavorably

### Trade 20% Profit (Moderate Confidence)
- **Mechanism**: Places an opening back bet with a fixed profit target  
- **Exit Trigger**: Takes exactly 20% profit from the opening back bet when target is reached
- **Risk Management**: Fixed profit target with predetermined exit point
- **Conservative Use**: For favorites with Fair EV ratings showing positive trading signals
- **Profit Potential**: Fixed at 20% of the opening back bet stake

### Lay 10 Euro (No Confidence)
- **Mechanism**: Places a lay bet against the market favorite
- **Risk Management**: Fixed liability with potential for consistent small profits
- **Conservative Use**: When no selections meet conservative criteria
- **Profit Potential**: Small consistent returns when favorites fail to meet value thresholds

## Usage Instructions

1. **Input Requirements:**
   - Active Betfair market with horse racing data
   - Access to MarketSelectionsTradedPricesData context (for trading pattern analysis)
   - Access to RacingpostDataForHorses context (for performance analysis)
   - Current market prices for all runners
   - Access to Bfexplorer betting functionality

2. **Expected Output:**
   - Complete EV analysis for all horses based on combined trading data and performance analysis
   - Conservative evaluation prioritizing market favorite using dual data validation
   - Conditional selection of favorite, alternative, or no-bet decision
   - Automated execution of appropriate strategy based on confidence level from both data sources

3. **Conservative Process:**
   - Silent dual data collection phase (trading data AND performance analysis)
   - Systematic evaluation of favorite using conservative criteria with both data sources
   - Alternative assessment if favorite fails to meet standards using combined analysis
   - Automatic strategy execution or lay bet if no value found based on dual data validation

## Conservative Selection Framework with Combined Data Analysis

The analysis will automatically evaluate selections using this hierarchy:

**Primary Evaluation (Market Favorite):**
- **Market Position** - Lowest price/highest implied probability
- **EV Threshold** - "Fair" rating minimum (EV ≥ -0.05)
- **Trading Confidence** - Positive/neutral signals with volume support
- **Performance Quality** - No major form concerns from race descriptions
- **Risk Assessment** - Lowest variance option with acceptable value from both data sources

**Secondary Evaluation (Best Alternative):**
- **EV Threshold** - Higher standard (EV > +0.10) for non-favorites
- **Win Probability** - Minimum 15% based on combined trading and performance analysis
- **Trading Signals** - Strong positive indicators required
- **Performance Indicators** - Competitive form and positive race descriptions
- **Volume Support** - Adequate market conviction evidence

**Tertiary Option (No Suitable Selection):**
- **Lay Strategy** - Against overvalued favorite when no value found
- **Risk Management** - Small consistent profits from poor value favorites
- **Capital Preservation** - Active position while protecting bankroll

## Conservative Execution Sequence with Combined Data Analysis

1. Complete comprehensive dual data analysis (trading patterns AND performance analysis - silent phase)
2. Evaluate market favorite against conservative criteria using both trading indicators and performance quality
3. If favorite unsuitable, assess best alternative with higher standards using combined analysis
4. Generate final report with selection decision and confidence assessment from both data sources
5. **IF suitable selection found**: Activate and execute appropriate backing strategy
6. **IF no suitable selection**: Activate favorite and execute lay strategy
7. Confirm execution status and document reasoning

This combined approach ensures systematic evaluation of both trading patterns and horse performance while maintaining conservative risk management principles, providing both aggressive value-seeking opportunities and defensive capital preservation strategies based on comprehensive market conditions and dual data validation.
