# Horse Racing Combined EV Analysis with Automated Betting

## Comprehensive Multi-Context EV Analysis and Betting Execution Prompt

```
Task: Perform comprehensive Expected Value (EV) analysis for horse racing betting opportunities using both market trading data and racing post performance data, select the best horse to back, and execute a betting strategy. This analysis combines trading pattern interpretation, semantic performance analysis, and mathematical EV calculations to identify the optimal betting opportunity and automatically place the bet.

Instructions:

1. **Market Data Retrieval**
   - Retrieve the active betfair market in BfexplorerApp using tool: GetActiveBetfairMarket
   - Save marketId for subsequent data retrieval
   - Make no preliminary reports during data collection

2. **Multi-Context Data Collection**
   - Retrieve the data context with the name 'MarketSelectionsTradedPricesData' for the betfair market using tool: GetDataContextForBetfairMarket
   - Retrieve the data context with the name 'RacingpostDataForHorsesInfo' for the betfair market using tool: GetDataContextForBetfairMarket
   - Focus on 'tradedPricesData' field from trading context and 'horsesData' field from racing post context
   - Do not make any reports during data collection phase

3. **Dual Analysis Framework**   **CRITICAL: Price-Probability Relationship and Movement Analysis**
   - **Probability = 1 / Price** (e.g., price 4.0 = 25% probability, price 2.0 = 50% probability)
   - **Price Shortening** = Price DECREASING = Probability INCREASING = Positive market signal
   - **Price Drifting/Lengthening** = Price INCREASING = Probability DECREASING = Negative market signal
   - **IMPORTANT**: Calculate probability changes, not price changes, for market confidence assessment
   - **Example**: Price 7.4→6.4 = Probability 13.5%→15.6% = +15.6% probability improvement (POSITIVE signal)
   
   **A) Trading Pattern Analysis:**
   - Analyze each horse's trading data focusing on price movements and market confidence
   - Perform deep analysis of trading patterns focusing on:
       i) **Probability Movement Patterns:**
        - Direction and magnitude of probability changes from start to current (calculate as probability percentages, not price percentages)
        - Probability stability vs volatility (derived from min/max price range converted to probabilities)
        - Trend consistency (steady probability increases/decreases vs erratic movement)
        - Current probability position relative to trading range
     
     ii) **Market Confidence Indicators:**
        - Traded volume as indicator of market interest and confidence
        - Probability increasing (price shortening) with high volume (strong money backing)
        - Probability decreasing (price drifting) with low volume (lack of support)
        - Sudden probability movements indicating insider information or market shifts
     
     iii) **Value Identification Signals:**
        - Horses whose current probability is near minimum traded probability (potential overlays)
        - Horses with consistent probability increases and strong volume (market confidence)
        - Horses with minimal probability movement but reasonable volume (stable assessment)
        - Horses showing probability decreases despite trading activity (potential value emerging)

   **B) Semantic Performance Analysis:**
   - Analyze each horse's data in the 'lastRacesDescription' field ONLY
   - Completely ignore the 'predictionScore' field
   - Perform deep semantic analysis of recent race descriptions focusing on:
     
     i) **Performance Patterns:**
        - Consistency of competitive positions
        - Frequency of strong finishes ("led", "kept on well", "ran on")
        - Recovery ability from setbacks
        - Late race surge capability
     
     ii) **Negative Performance Indicators:**
        - Frequent mentions of "weakened", "outpaced", "struggling"
        - Jumping errors or hesitation
        - Physical issues (lameness, stumbling, breathing problems)
        - Behavioral problems (hanging, running green)
     
     iii) **Positive Performance Indicators:**
        - Winning performances and margins
        - Strong finishing kicks and sustained runs
        - Competitive throughout race duration
        - Professional racing behavior
     
     iv) **Contextual Performance Factors:**
        - Ground condition preferences and performance
        - Equipment changes and their impact (blinkers, cheek-pieces)
        - Trainer/jockey comments indicating improvement or issues
        - Class level performance and progression

4. **Combined Win Probability Assessment**
   - Synthesize insights from both trading data and performance analysis
   - Weight trading signals (60%) and performance analysis (40%) for probability assignment
   - Consider convergence and divergence between market confidence and performance indicators
   - Assign final win probabilities ensuring they sum to approximately 100%
   - Document reasoning for each probability assignment showing both data sources

5. **Expected Value Calculations**
   - Calculate EV for backing each horse using formula:
     EV = (Combined Win Probability × (Decimal Odds - 1)) - (1 - Combined Win Probability)
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
   - Key reasons for selection (combination of EV, trading analysis, and performance analysis)
   - Expected value and win probability of selected horse
   - Risk assessment for the recommended bet

   **B) Individual Horse Analysis:**
   - Horse name and current market price
   - Trading pattern analysis summary (2-3 sentences)
   - Performance semantic analysis summary (2-3 sentences)
   - Combined probability reasoning showing both data source contributions
   - Calculated Expected Value
   - Final ranking position   **C) Complete Horse Analysis JSON:**
   - Generate comprehensive JSON containing all horses with detailed analysis data
   - Include all calculated metrics, trading signals, performance indicators
   - Structure for easy data processing and further analysis
   - Format for API consumption or database storage

   **D) EV Rankings Table:**
   - All horses ranked by Expected Value (highest to lowest)
   - Include: Horse Name, Current Price, Start Price, Probability Change %, Traded Volume, Trading Signal, Performance Signal, Combined Win Probability, EV, EV Rating
   - Highlight the selected horse for betting

   **E) Selection Justification:**
   - Detailed explanation of why the chosen horse represents the best betting opportunity
   - Convergence analysis: How trading signals align with performance indicators
   - Divergence analysis: Where market and performance disagree and why
   - Risk-reward assessment based on multi-source data
   - Confidence level in the selection

   **E) Market Analysis Summary:**
   - Overall market efficiency assessment based on trading patterns
   - Performance vs market pricing discrepancies
   - Key value opportunities identified through dual analysis
   - Market biases or mispricing indicated by combined data sources
   - Volume distribution and liquidity assessment

   **F) Strategy Selection Decision:**
   - Clear explanation of whether betting or trading strategy was chosen
   - Criteria met for the selected strategy approach
   - Risk assessment for the chosen strategy type
   - Expected outcome based on strategy selection

8. **Automated Strategy Execution**
   - After completing the analysis and identifying the best horse
   - Activate the selected horse's market and selection using tool: ActivateBetfairMarketSelection
   - Apply conditional strategy selection:
     * **Use "Back trailing stop loss trading"** when selection meets betting criteria:
       - Positive Expected Value (EV > 0.1)
       - Win probability ≥ 15%
       - Strong convergence between trading confidence and performance indicators
       - Current price represents clear value (near maximum range or strong shortening with volume)
       - Strategy locks in any profit when odds turn against the opening back bet (trailing stop)
     * **Use "Trade 20% profit" strategy** when selection meets trading criteria:
       - Moderate Expected Value (EV > 0 but < 0.1)
       - Win probability between 8-15%
       - Volatile price movements with trading opportunities
       - Mixed signals between trading and performance data
       - Strategy takes exactly 20% profit from the opening back bet when available
   - Execute the appropriate strategy using tool: ExecuteBfexplorerStrategySettings
   - Confirm strategy execution and document reasoning for strategy choice

Format: Present the final analysis in clear, actionable format with emphasis on the single best opportunity. After analysis completion, automatically execute either the betting or trading strategy on the selected horse based on suitability criteria.

Selection Criteria Priority:
1. Positive Expected Value (EV > 0)
2. Convergence between trading confidence and performance indicators
3. Strong signals from both data sources
4. Reasonable win probability for betting (typically >15%) or trading opportunity (8-15%)
5. Manageable risk profile
6. Clear market mispricing signals based on combined analysis

**Strategy Selection Criteria:**
- **Trading Strategy 1 ("Back trailing stop loss trading"):** High confidence selections with EV > 0.1, win probability ≥ 15%, strong convergence between trading and performance signals. Takes any available profit when odds move against the opening back bet (trailing stop protection).
- **Trading Strategy 2 ("Trade 20% profit"):** Moderate confidence selections with trading volatility and mixed signal scenarios. Takes exactly 20% profit from the opening back bet when the target is reached.

**Combined Analysis Framework:**

**Trading Data Interpretation Guidelines:**
- **Strong Positive Signals:** Probability increasing (price shortening) with high traded volume
- **Moderate Positive Signals:** Minimal probability movement with steady volume
- **Negative Signals:** Probability decreasing (price drifting) with declining volume

**Performance Data Interpretation Guidelines:**
- **Strong Positive Signals:** Recent wins, consistent strong finishes, competitive performances
- **Moderate Positive Signals:** Improvement trends, solid recent form
- **Negative Signals:** Frequent poor performances, physical/behavioral issues

**Convergence Analysis:**
- **High Confidence:** Both trading and performance indicators align positively
- **Moderate Confidence:** Mixed signals requiring careful evaluation
- **Low Confidence:** Conflicting signals between market and performance

Note: This analysis combines market intelligence (trading data) with performance intelligence (racing narratives) to provide the most comprehensive assessment possible. The process culminates in automated strategy execution on the optimal selection identified through dual-source analysis.
```

## Strategy Details

### Back Trailing Stop Loss Trading
- **Mechanism**: Places an opening back bet, then monitors for any profit opportunity
- **Exit Trigger**: Takes profit immediately when odds move against the opening back bet
- **Risk Management**: Trailing stop protection ensures no profit is left on the table
- **Best For**: High-confidence selections with strong convergence between trading and performance signals
- **Profit Potential**: Variable - captures any available profit when odds shift unfavorably

### Trade 20% Profit  
- **Mechanism**: Places an opening back bet with a fixed profit target
- **Exit Trigger**: Takes exactly 20% profit from the opening back bet when target is reached
- **Risk Management**: Fixed profit target with predetermined exit point
- **Best For**: Moderate confidence selections with mixed signals or expected price volatility
- **Profit Potential**: Fixed at 20% of the opening back bet stake

## Usage Instructions

1. **Input Requirements:**
   - Active Betfair market with horse racing data
   - Access to MarketSelectionsTradedPricesData context
   - Access to RacingpostDataForHorsesInfo context
   - Current market prices for all runners
   - Access to Bfexplorer betting functionality

2. **Expected Output:**
   - Complete dual-source EV analysis for all horses in race
   - Clear identification of best horse with appropriate strategy selection
   - Automated execution of either:
     * "Back trailing stop loss trading" (locks in profit when odds turn against opening back bet)
     * "Trade 20% profit" (takes exactly 20% profit from opening back bet)
   - Confirmation of strategy execution with reasoning

3. **Automated Process:**
   - Silent dual data collection and analysis phase
   - Comprehensive final report with best selection and strategy choice
   - Automatic market activation and strategy execution (betting or trading)
   - No user intervention required during process

## Selection Framework

The analysis will automatically select the horse that best combines:

**Primary Criteria:**
- **Positive Expected Value** - Mathematical edge in the bet/trade
- **Signal Convergence** - Trading and performance indicators align
- **Strong Dual Signals** - Positive indicators from both data sources
- **Appropriate Probability Range** - Betting (>15%) or Trading (8-15%) suitability

**Secondary Criteria:**
- **Market Confidence** - Strong volume supporting probability movements
- **Performance Consistency** - Solid recent racing form and behavior
- **Probability Positioning** - Favorable position within trading range (converted from prices)
- **Strategy Suitability** - Clear betting value vs trading opportunity signals

## Combined Data Analysis Framework

**Trading Data Signals:**
- **Shortening + Volume** = Market confidence increasing
- **Drifting + Low Volume** = Market confidence decreasing  
- **Stable + High Volume** = Market consensus established
- **Volatile + Mixed Volume** = Uncertainty or information flow

**Performance Data Signals:**
- **Recent Wins** = High ability confirmation
- **Strong Finishes** = Competitive consistency
- **Improvement Trends** = Positive trajectory
- **Behavioral Issues** = Risk factors

**Signal Convergence Analysis:**
- **Perfect Alignment:** Trading confidence + strong performance = High confidence bet
- **Partial Alignment:** One strong signal + one moderate = Moderate confidence
- **Divergence:** Strong trading vs weak performance (or vice versa) = Requires careful evaluation
- **Negative Alignment:** Poor trading + poor performance = Avoid

## Automated Execution Sequence

1. Complete comprehensive dual-source EV analysis (silent phase)
2. Generate final report with best horse selection and strategy determination
3. Activate selected horse's market and selection
4. Execute appropriate strategy automatically:
   - **"Back trailing stop loss trading"** for high-confidence convergent opportunities
   - **"Trade 20% profit"** for moderate confidence or mixed signal opportunities
5. Confirm successful strategy execution with reasoning

This prompt ensures the most comprehensive analysis possible by combining market intelligence (what the crowd thinks via trading patterns) with performance intelligence (what the horse has actually done) while maintaining focus on identifying and executing the optimal strategy on the single best opportunity available.
