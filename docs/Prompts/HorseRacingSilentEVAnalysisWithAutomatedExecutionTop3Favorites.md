# Horse Racing Silent EV Analysis with Automated Execution - Top 3 Favorites Only

## Silent Multi-Context EV Analysis with Strategy Execution on Top 3 Favorites Only

```
Task: Perform completely silent comprehensive Expected Value (EV) analysis for horse racing betting opportunities using both market trading data and racing post performance data, select the best horse to back FROM THE TOP 3 FAVORITES BY PRICE ONLY, and execute a betting strategy automatically. This analysis combines trading pattern interpretation, semantic performance analysis, and mathematical EV calculations to identify the optimal betting opportunity among the market favorites and automatically place the bet. ABSOLUTELY NO reports, tables, explanations, or intermediate outputs are generated - ONLY the final strategy execution confirmation message.

**CRITICAL RESTRICTION: Strategy execution is ONLY allowed on horses that are among the 3 favorites by current price. Horses ranked 4th or lower by price are excluded from strategy execution regardless of their EV or analysis results.**

Instructions:

1. **Market Data Retrieval**
   - Retrieve the active betfair market in BfexplorerApp using tool: GetActiveBetfairMarket
   - Save marketId for subsequent data retrieval
   - **Identify top 3 favorites by current price** (lowest prices = favorites)
   - No reports during data collection

2. **Multi-Context Data Collection**   
   - Retrieve the data context with the name 'MarketSelectionsTradedPricesData' for the betfair market using tool: GetDataContextForBetfairMarket
   - Retrieve the data context with the name 'RacingpostDataForHorses' for the betfair market using tool: GetDataContextForBetfairMarket
   - Focus on 'tradedPricesData' field from trading context and 'horsesData' field from racing post context
   - SILENT data collection - no outputs during this phase

3. **Complete Analysis of ALL Horses**
   - **Analyze ALL horses in the race** - perform comprehensive EV analysis on every runner
   - **No filtering during analysis phase** - evaluate every horse regardless of price ranking
   - **Calculate EV for all horses** and rank by Expected Value from highest to lowest
   - **Identify top 3 favorites by current price** (3 horses with lowest current prices) for later filtering
   - **Analysis phase treats all horses equally** - favorites filter applied only at execution decision

4. **Silent Analysis Framework**
     **CRITICAL: Price-Probability Relationship**
   - **Probability = 1 / Price** (e.g., price 4.0 = 25% probability, price 2.0 = 50% probability)
   - **Price Shortening** = Price DECREASING = Probability INCREASING = Positive market signal
   - **Price Drifting/Lengthening** = Price INCREASING = Probability DECREASING = Negative market signal
   - **ALWAYS use probability change for analysis, NOT price change**
   - **Example:** Price 3.5 → 3.9 = Probability 28.57% → 25.64% = **-2.93 percentage points** (NEGATIVE signal)
   - **Example:** Price 4.0 → 3.5 = Probability 25.0% → 28.57% = **+3.57 percentage points** (POSITIVE signal)
   - Always calculate probability changes correctly: Lower price = Higher probability

    **A) Trading Pattern Analysis (Silent):**

   - Analyze each horse's trading data focusing on price movements and market confidence
   - **ANALYZE ALL HORSES IN THE RACE** - not just top 3 favorites
   - Perform deep analysis of trading patterns focusing on:
     
     i) **Price Movement Patterns:**
        - Direction and magnitude of price changes from start to current
        - Price stability vs volatility (min/max price range)
        - Trend consistency (steady shortening/drifting vs erratic movement)
        - Current position relative to trading range
    ii) **Market Confidence Indicators:**
        - Traded volume as indicator of market interest and confidence
        - Price shortening (decreasing price = increasing probability) with high volume (strong money backing)
        - Price drifting (increasing price = decreasing probability) with low volume (lack of support)
        - Sudden price movements indicating insider information or market shifts
     
    iii) **Value Identification Signals:**
        - Horses whose current price is near or at maximum traded price (potential overlays if performance suggests better)
        - Horses with consistent price shortening (decreasing price) and strong volume (market confidence increasing)
        - Horses with minimal price movement but reasonable volume (stable market assessment)
        - Horses showing price lengthening (increasing price) despite trading activity (potential value if performance contradicts market drift)   

    **B) Semantic Performance Analysis (Silent):**
    
   - Analyze each horse's data in the 'lastRacesDescription' field ONLY
   - **ANALYZE ALL HORSES IN THE RACE** - not just top 3 favorites
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

5. **Silent Combined Win Probability Assessment**
   - Synthesize insights from both trading data and performance analysis **FOR ALL HORSES**
   - Weight trading signals (60%) and performance analysis (40%) for probability assignment
   - Consider convergence and divergence between market confidence and performance indicators
   - Assign final win probabilities ensuring they sum to approximately 100%
   - Document reasoning for each probability assignment showing both data sources **FOR EVERY RUNNER**

6. **Silent Expected Value Calculations**
   - Calculate EV for backing each horse using formula:
     EV = (Combined Win Probability × (Current Decimal Price - 1)) - (1 - Combined Win Probability)
   - Use CURRENT market prices (not start prices) for EV calculations **FOR ALL HORSES**
   - Market implied probability = 1 / Current Price for comparison with your assessed probability
   - Identify horses with positive EV (where your assessed probability > market implied probability)
   - Identify horses with significantly negative EV (potential lay opportunities)
   - Rank all horses by EV from highest to lowest **ACROSS ALL RUNNERS**

7. **Execution Decision with Favorites Filter**
   - **After complete analysis of ALL horses:** Identify the horse with highest Expected Value across entire field
   - **Apply favorites constraint at decision point:** Check if the best EV horse is among top 3 favorites by current price
   - **If best EV horse is among top 3 favorites:** Proceed with strategy execution on that horse
   - **If best EV horse is NOT among top 3 favorites:** Find the best EV horse that IS among top 3 favorites
   - **If no horse among top 3 favorites meets execution criteria:** Output "No suitable favorite found for execution"
   - **Strategy selection based on best qualifying horse:**
     1. Positive Expected Value (EV > 0)
     2. Convergence between trading confidence and performance indicators
     3. Strong signals from both data sources
     4. Reasonable win probability for betting (typically >15%) or trading opportunity (8-15%)
     5. Manageable risk profile
     6. Clear market mispricing signals based on combined analysis

8. **Silent Strategy Selection (Best EV Horse Among Top 3 Favorites)**
   - **Strategy execution only on the qualifying horse with best metrics:**
     * **Use "Back trailing stop loss trading"** when qualifying selection meets betting criteria:
       - Positive Expected Value (EV > 0.1)
       - Win probability ≥ 15%
       - Strong convergence between trading confidence and performance indicators
       - Current price represents clear value (price lengthening creating overlay OR strong shortening with volume confirming performance)
       - Strategy locks in any profit when odds turn against the opening back bet (trailing stop)
     * **Use "Trade 20% profit" strategy** when qualifying selection meets trading criteria:
       - Moderate Expected Value (EV > 0 but < 0.1)
       - Win probability between 8-15%
       - Volatile price movements with trading opportunities
       - Mixed signals between trading and performance data
       - Strategy takes exactly 20% profit from the opening back bet when available
   - **If best EV horse is not among top 3 favorites AND no top 3 favorite meets execution criteria:** Output "No suitable favorite found for execution"

9. **JSON Data Storage (Silent)**
   - Generate comprehensive JSON containing **ALL HORSES** with detailed analysis data
   - **Clearly mark favorites ranking** (1, 2, 3, 4, 5, etc. by price) and execution eligibility
   - Include all calculated metrics, trading signals, performance indicators **FOR EVERY RUNNER**
   - Structure for easy data processing and further analysis
   - Store JSON data to BfexplorerApp using tool: SetAIAgentDataContextForBetfairMarket
   - Use dataContextName: "HorseRacingEVAnalysisResults"
   - Include metadata about race, market conditions, and analysis parameters
   - Structure should include:
     * Race metadata (market ID, event, race name, start time)
     * Analysis configuration (weights, criteria, strategy selection logic, **favorites filter applied**)
     * **Individual horse data with full metric set for ALL HORSES** and favorites ranking (numbers only)
     * Market summary statistics
     * Selection decision rationale **within top 3 favorites constraint**
     * Strategy execution details or explanation of no execution

10. **Automated Strategy Execution (Best EV Horse if Among Top 3 Favorites)**
    - Store comprehensive analysis JSON data **FOR ALL HORSES** using tool: SetAIAgentDataContextForBetfairMarket with dataContextName "HorseRacingEVAnalysisResults"
    - **Decision Logic:**
      1. **Identify horse with highest EV across ALL horses in race**
      2. **Check if this horse is among top 3 favorites by current price**
      3. **If YES:** Execute strategy on this horse
      4. **If NO:** Find horse with highest EV that IS among top 3 favorites
      5. **If no top 3 favorite meets execution criteria:** Output "No suitable favorite found for execution"
    - **Strategy Execution (only if qualifying horse found):**
      - Activate the selected horse's market and selection using tool: ActivateBetfairMarketSelection
      - Execute the appropriate strategy using tool: ExecuteBfexplorerStrategySettings
      - Confirm strategy execution with minimal output
    - **Note: Analysis includes ALL horses, but execution requires best horse to be among top 3 favorites**

11. **Final Confirmation ONLY**
    - If strategy executed: "Strategy executed: [Strategy Name] on [Horse Name] at [Price] odds"
    - If no execution: "No suitable favorite found for execution"
    - **ABSOLUTELY NO detailed analysis reports, tables, explanations, or intermediate outputs**
    - **ONLY the single line execution confirmation or no-execution message**
    - **Note: Complete analysis of ALL horses preserved SILENTLY in JSON data storage**

Format: Complete analysis of ALL horses in COMPLETE SILENCE, then execute strategy automatically on top 3 favorites only. ABSOLUTELY NO intermediate outputs, reports, tables, or explanations. ONLY output the final execution confirmation or no-execution message. All horses analyzed and included in JSON data storage SILENTLY.

**Strategy Selection Criteria (Top 3 Favorites Only):**
- **Trading Strategy 1 ("Back trailing stop loss trading"):** High confidence selections with EV > 0.1, win probability ≥ 15%, strong convergence between trading and performance signals. Takes any available profit when odds move against the opening back bet (trailing stop protection). **ONLY EXECUTED ON TOP 3 FAVORITES.**
- **Trading Strategy 2 ("Trade 20% profit"):** Moderate confidence selections with trading volatility and mixed signal scenarios. Takes exactly 20% profit from the opening back bet when the target is reached. **ONLY EXECUTED ON TOP 3 FAVORITES.**

**Favorites Filter Logic:**
1. **Analyze ALL horses comprehensively** - no filtering during analysis phase
2. **Rank all horses by Expected Value** (highest to lowest across entire field)
3. **Separately rank all horses by current price** (ascending) - lowest price = 1st favorite, second lowest = 2nd favorite, third lowest = 3rd favorite
4. **Execution Decision:** 
   - **First choice:** Horse with highest EV if it's among top 3 favorites
   - **Second choice:** Horse with highest EV that IS among top 3 favorites
   - **No execution:** If no top 3 favorite meets execution criteria
5. **ALL HORSES are analyzed and included in JSON data** - comprehensive analysis for entire field with no filtering

**JSON Data Storage Structure (Modified for Favorites Filter):**
```json
{
  "analysis_metadata": {
    "timestamp": "ISO 8601 format",
    "market_id": "string",
    "race_info": {
      "event_name": "string",
      "race_name": "string", 
      "start_time": "ISO 8601 format",
      "total_runners": "number"
    },
    "strategy_criteria": {
      "trading_weight": "number",
      "performance_weight": "number",
      "min_ev_betting": "number",
      "min_probability_betting": "number",
      "favorites_filter_applied": true,
      "max_favorite_rank_for_execution": 3
    },
    "horses": [
    {
      "name": "string",
      "selection_id": "string",      
      "current_price": "number",
      "start_price": "number",
      "price_rank": "number",
      "favorite_status": "number",
      "ev_rank": "number",
      "execution_eligible": "boolean",
      "price_change": "number",
      "price_change_percentage": "number",
      "probability_change": "number",
      "probability_change_percentage": "number",
      "trading_data": {
        "volume_traded": "number",
        "min_price": "number",
        "max_price": "number",
        "price_volatility": "number",
        "trading_signal": "string",
        "trading_confidence": "string"
      },
      "performance_data": {
        "semantic_analysis": "string",
        "performance_signal": "string",
        "recent_form_summary": "string",
        "performance_confidence": "string"
      },
      "analysis_results": {
        "market_probability": "number",
        "adjusted_probability": "number",
        "trading_contribution": "number",
        "performance_contribution": "number",
        "expected_value": "number",
        "ev_rating": "string",
        "ranking": "number"
      }
    }
  ],
  "selection": {
    "selected_horse": "string",
    "selection_reasoning": "string",
    "strategy_chosen": "string",
    "strategy_reasoning": "string",    
    "execution_status": "string",
    "risk_assessment": "string",
    "favorites_constraint_applied": "boolean",
    "favorite_rank_of_selection": "number",
    "ev_rank_of_selection": "number",
    "best_ev_horse_was_favorite": "boolean"
  },
  "market_analysis": {
    "efficiency_assessment": "string",
    "value_opportunities": "array",
    "market_biases": "array",
    "liquidity_assessment": "string",
    "top_3_favorites": "array"
  }
}
```

## Automated Execution Sequence (ALL Horses Analyzed, Best EV Horse Executed if Among Top 3 Favorites)

1. Complete comprehensive dual-source EV analysis **FOR ALL HORSES** (silent phase)
2. **Rank ALL horses by Expected Value** (highest to lowest across entire field)
3. **Identify top 3 favorites by current price** (separate ranking by price)
4. **Apply execution logic:**
   - If horse with highest EV is among top 3 favorites: Select this horse for execution
   - If not: Select horse with highest EV that IS among top 3 favorites
   - If no top 3 favorite meets execution criteria: No execution
5. Store comprehensive JSON analysis data **FOR ALL HORSES** using SetAIAgentDataContextForBetfairMarket tool
6. **Execute strategy only if qualifying horse identified:**
   - Activate selected horse's market and selection
   - Execute appropriate strategy automatically
   - Confirm successful strategy execution with minimal output only
7. **If no qualifying horse:** Output "No suitable favorite found for execution"

## Benefits of Completely Silent Operation with Favorites Filter

**Risk Management:**
- **Favorite bias protection** - Only backs horses with significant market support
- **Liquidity assurance** - Top 3 favorites typically have best liquidity
- **Reduced variance** - Favorites have higher win rates and more predictable outcomes
- **Market efficiency** - Favorites are typically more efficiently priced, reducing extreme value traps

**Efficiency:**
- **ABSOLUTELY NO intermediate outputs** - Direct action without any delays or reports
- **Automated decision making** - No human intervention or information display required
- **Rapid execution** - From analysis to betting in minimal time with zero output
- **Focus on results** - Only final outcome matters, no process documentation or explanations

**Data Preservation:**
- **Complete analysis stored** - All data preserved in JSON format for future reference **FOR ALL HORSES**
- **Favorites ranking preserved** - Clear indication of market position and execution eligibility **FOR EVERY RUNNER**
- **Persistent storage** - Data maintained in BfexplorerApp for historical analysis
- **API accessible** - Other systems can retrieve analysis data via MCP tools
- **Structured format** - Consistent data structure for reliable processing **ACROSS ENTIRE FIELD**

This completely silent prompt ensures rapid, automated analysis and execution while preserving all analytical data for future reference through comprehensive JSON storage in BfexplorerApp. The process operates with ABSOLUTELY NO intermediate outputs, reports, tables, or explanations, focusing solely on identifying and executing the optimal betting opportunity. **ALL HORSES IN THE RACE ARE ANALYZED COMPREHENSIVELY** with the best EV horse selected for execution **ONLY IF IT IS AMONG THE TOP 3 FAVORITES BY CURRENT PRICE**. If the best EV horse is not a favorite, the system selects the best EV horse that IS among the top 3 favorites. **COMPLETE FIELD ANALYSIS IS PRESERVED IN JSON DATA STORAGE**. **ONLY FINAL OUTPUT: Single line execution confirmation or no-execution message.**

```

## Strategy Details

### Back Trailing Stop Loss Trading (Top 3 Favorites Only)
- **Mechanism**: Places an opening back bet, then monitors for any profit opportunity
- **Exit Trigger**: Takes profit immediately when odds move against the opening back bet
- **Risk Management**: Trailing stop protection ensures no profit is left on the table
- **Best For**: High-confidence selections among top 3 favorites with strong convergence between trading and performance signals
- **Profit Potential**: Variable - captures any available profit when odds shift unfavorably
- **Restriction**: **ONLY executed on horses ranked 1st, 2nd, or 3rd by current price**

### Trade 20% Profit (Top 3 Favorites Only)
- **Mechanism**: Places an opening back bet with a fixed profit target
- **Exit Trigger**: Takes exactly 20% profit from the opening back bet when target is reached
- **Risk Management**: Fixed profit target with predetermined exit point
- **Best For**: Moderate confidence selections among top 3 favorites with mixed signals or expected price volatility
- **Profit Potential**: Fixed at 20% of the opening back bet stake
- **Restriction**: **ONLY executed on horses ranked 1st, 2nd, or 3rd by current price**

## Usage Instructions

1. **Input Requirements:**
   - Active Betfair market with horse racing data
   - Access to MarketSelectionsTradedPricesData context
   - Access to RacingpostDataForHorses context
   - Current market prices for all runners
   - Access to Bfexplorer betting functionality

2. **Expected Output:**
   - Completely silent comprehensive dual-source EV analysis **for ALL horses in race**
   - **Favorites ranking applied** - only top 3 favorites by price eligible for execution
   - **JSON data storage** with comprehensive analysis data **for ALL HORSES** in BfexplorerApp (SILENT)
   - Automated execution of either:
     * "Back trailing stop loss trading" (locks in profit when odds turn against opening back bet) **ON TOP 3 FAVORITES ONLY**
     * "Trade 20% profit" (takes exactly 20% profit from opening back bet) **ON TOP 3 FAVORITES ONLY**
   - **ONLY final output:** "Strategy executed: [Strategy Name] on [Horse Name] at [Price] odds" **OR** "No suitable favorite found for execution"

3. **Automated Process:**   - SILENT data collection and analysis phase **FOR ALL HORSES**
   - **Favorites filter application** - identify top 3 by current price  
   - **ABSOLUTELY NO intermediate reports, tables, or explanations**
   - Automatic market activation and strategy execution (betting or trading) **ONLY ON TOP 3 FAVORITES**
   - **ONLY final output** - execution confirmation or no-execution message
   - **Complete horse analysis preserved in JSON** - no user intervention required during process

## Selection Framework (Top 3 Favorites Only)

The analysis will automatically select the horse that best combines **AMONG TOP 3 FAVORITES ONLY:**

**Primary Criteria:**
- **Must be ranked 1st, 2nd, or 3rd by current price** - MANDATORY REQUIREMENT
- **Positive Expected Value** - Mathematical edge in the bet/trade
- **Signal Convergence** - Trading and performance indicators align
- **Strong Dual Signals** - Positive indicators from both data sources
- **Appropriate Probability Range** - Betting (>15%) or Trading (8-15%) suitability

**Secondary Criteria:**
- **Market Confidence** - Strong volume supporting price movements
- **Performance Consistency** - Solid recent racing form and behavior
- **Price Positioning** - Favorable position within trading range
- **Strategy Suitability** - Clear betting value vs trading opportunity signals

## Silent Analysis Framework (Top 3 Favorites Constraint)

**Trading Data Signals (Analyzed Silently):**
- **Price Shortening + Volume** = Market confidence increasing (lower price = higher probability = POSITIVE probability change)
- **Price Drifting + Low Volume** = Market confidence decreasing (higher price = lower probability = NEGATIVE probability change)
- **Stable Price + High Volume** = Market consensus established at current probability level (minimal probability change)
- **Volatile Pricing + Mixed Volume** = Uncertainty or information flow affecting market assessment

**Performance Data Signals (Analyzed Silently):**
- **Recent Wins** = High ability confirmation
- **Strong Finishes** = Competitive consistency
- **Improvement Trends** = Positive trajectory
- **Behavioral Issues** = Risk factors

**Signal Convergence Analysis (Silent - Favorites Only):**
- **Perfect Alignment:** Trading confidence (positive probability change + volume) + strong performance = High confidence bet **IF IN TOP 3 FAVORITES**
- **Partial Alignment:** One strong signal + one moderate = Moderate confidence **IF IN TOP 3 FAVORITES**
- **Divergence Analysis:** Strong trading (probability movement) vs weak performance (or vice versa) = Careful evaluation needed **IF IN TOP 3 FAVORITES**
  - **Probability decreasing despite strong performance** = Potential value opportunity (market wrong) **IF IN TOP 3 FAVORITES**
  - **Probability increasing despite weak performance** = Market may be overconfident **IF IN TOP 3 FAVORITES**
- **Negative Alignment:** Poor trading (negative probability change + low volume) + poor performance = Avoid **REGARDLESS OF FAVORITE STATUS**

## Final Output Format

**If strategy executed on top 3 favorite:**
"Strategy executed: [Strategy Name] on [Horse Name] at [Price] odds"

**If no suitable favorite found:**
"No suitable favorite found for execution"

**Example Outputs:**
- "Strategy executed: Back trailing stop loss trading on Enthused at 2.86 odds"
- "No suitable favorite found for execution"

This enhanced completely silent prompt ensures the most comprehensive analysis possible by combining market intelligence (what the crowd thinks via trading patterns) with performance intelligence (what the horse has actually done) while operating in complete silence to execute the optimal betting strategy **ONLY ON HORSES RANKED AMONG THE TOP 3 FAVORITES BY CURRENT PRICE** with no intermediate outputs - just the final execution confirmation or no-execution message. **COMPLETE ANALYSIS OF ALL HORSES IS PERFORMED SILENTLY** and preserved in JSON format for comprehensive market assessment and future reference. **ABSOLUTELY NO REPORTS, TABLES, OR EXPLANATIONS ARE GENERATED.**
