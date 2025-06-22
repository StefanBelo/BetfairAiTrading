# Horse Racing Combined EV Analysis with Table and JSON Output

## Comprehensive Multi-Context EV Analysis with Dual Output Format

```
Task: Perform comprehensive Expected Value (EV) analysis for horse racing betting opportunities using both market trading data and racing post performance data, select the best horse to back, and execute a betting strategy. This analysis combines trading pattern interpretation, semantic performance analysis, and mathematical EV calculations to identify the optimal betting opportunity and automatically place the bet. The output includes both structured table format for human readability and comprehensive JSON format for data processing.

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

3. **Dual Analysis Framework**
     **CRITICAL: Price-Probability Relationship**
   - **Probability = 1 / Price** (e.g., price 4.0 = 25% probability, price 2.0 = 50% probability)
   - **Price Shortening** = Price DECREASING = Probability INCREASING = Positive market signal
   - **Price Drifting/Lengthening** = Price INCREASING = Probability DECREASING = Negative market signal
   - **ALWAYS use probability change for analysis, NOT price change**
   - **Example:** Price 3.5 → 3.9 = Probability 28.57% → 25.64% = **-2.93 percentage points** (NEGATIVE signal)
   - **Example:** Price 4.0 → 3.5 = Probability 25.0% → 28.57% = **+3.57 percentage points** (POSITIVE signal)
   - Always calculate probability changes correctly: Lower price = Higher probability
   
   **A) Trading Pattern Analysis:**
   - Analyze each horse's trading data focusing on price movements and market confidence
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
     EV = (Combined Win Probability × (Current Decimal Price - 1)) - (1 - Combined Win Probability)
   - Use CURRENT market prices (not start prices) for EV calculations
   - Market implied probability = 1 / Current Price for comparison with your assessed probability
   - Identify horses with positive EV (where your assessed probability > market implied probability)
   - Identify horses with significantly negative EV (potential lay opportunities)
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
   - **CRITICAL: Always use probability change for analysis**
     * Start probability = 1 / Start Price
     * Current probability = 1 / Current Price  
     * **Probability change = Current Probability - Start Probability**
     * **POSITIVE probability change = Market confidence INCREASED (price shortened)**
     * **NEGATIVE probability change = Market confidence DECREASED (price drifted)**
     * Express probability changes as percentage points for clarity
   - Trading pattern analysis summary incorporating correct price movement interpretation
   - Performance semantic analysis summary (2-3 sentences)
   - Combined probability reasoning showing both data source contributions
   - Calculated Expected Value using current market price
   - Final ranking position   
   
   **C) EV Rankings Table:**
   - All horses ranked by Expected Value (highest to lowest)
   - Include: Horse Name, Current Price, Start Price, **Probability Change (%)**, Traded Volume, Trading Signal, Performance Signal, Combined Win Probability, EV, EV Rating
   - Highlight the selected horse for betting
   - Format as clear markdown table for easy reading   **D) Complete Horse Analysis JSON Storage:**
   - Generate comprehensive JSON containing all horses with detailed analysis data
   - Include all calculated metrics, trading signals, performance indicators
   - Structure for easy data processing and further analysis
   - Store JSON data to BfexplorerApp using tool: SetAIAgentDataContextForBetfairMarket
   - Use dataContextName: "HorseRacingEVAnalysisResults"
   - Include metadata about race, market conditions, and analysis parameters
   - Structure should include:
     * Race metadata (market ID, event, race name, start time)
     * Analysis configuration (weights, criteria, strategy selection logic)
     * Individual horse data with full metric set
     * Market summary statistics
     * Selection decision rationale
     * Strategy execution details

   **E) Selection Justification:**
   - Detailed explanation of why the chosen horse represents the best betting opportunity
   - Convergence analysis: How trading signals align with performance indicators
   - Divergence analysis: Where market and performance disagree and why
   - Risk-reward assessment based on multi-source data
   - Confidence level in the selection

   **F) Market Analysis Summary:**
   - Overall market efficiency assessment based on trading patterns
   - Performance vs market pricing discrepancies
   - Key value opportunities identified through dual analysis
   - Market biases or mispricing indicated by combined data sources
   - Volume distribution and liquidity assessment

   **G) Strategy Selection Decision:**
   - Clear explanation of whether betting or trading strategy was chosen
   - Criteria met for the selected strategy approach
   - Risk assessment for the chosen strategy type
   - Expected outcome based on strategy selection

8. **Automated Strategy Execution and Data Storage**
   - After completing the analysis and identifying the best horse
   - Store comprehensive analysis JSON data using tool: SetAIAgentDataContextForBetfairMarket with dataContextName "HorseRacingEVAnalysisResults"
   - Activate the selected horse's market and selection using tool: ActivateBetfairMarketSelection
   - Apply conditional strategy selection:* **Use "Back trailing stop loss trading"** when selection meets betting criteria:
       - Positive Expected Value (EV > 0.1)
       - Win probability ≥ 15%
       - Strong convergence between trading confidence and performance indicators
       - Current price represents clear value (price lengthening creating overlay OR strong shortening with volume confirming performance)
       - Strategy locks in any profit when odds turn against the opening back bet (trailing stop)
     * **Use "Trade 20% profit" strategy** when selection meets trading criteria:
       - Moderate Expected Value (EV > 0 but < 0.1)
       - Win probability between 8-15%
       - Volatile price movements with trading opportunities
       - Mixed signals between trading and performance data
       - Strategy takes exactly 20% profit from the opening back bet when available   - Execute the appropriate strategy using tool: ExecuteBfexplorerStrategySettings
   - Confirm strategy execution and document reasoning for strategy choice
   - Update stored JSON data with final execution results

Format: Present the final analysis in clear, actionable format with emphasis on the single best opportunity. Provide human-readable table format and store comprehensive machine-readable JSON format in BfexplorerApp. After analysis completion, automatically execute either the betting or trading strategy on the selected horse based on suitability criteria.

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

**Output Format Requirements:**

**Table Format:**
- Clear markdown table with proper alignment
- Color coding or highlighting for selected horse
- Easy-to-scan numerical data
- Sortable by EV ranking
- Human-friendly labels and descriptions

**JSON Data Storage:**
- Store comprehensive analysis data using SetAIAgentDataContextForBetfairMarket tool
- Use dataContextName: "HorseRacingEVAnalysisResults"
- Valid, well-structured JSON with consistent naming
- Nested objects for logical grouping
- Arrays for multiple horses data
- Standardized field names for API compatibility
- Complete metadata for context preservation
- Timestamps and version information
- Error handling and validation flags

**Combined Analysis Framework:**

**Trading Data Interpretation Guidelines:**
- **Strong Positive Signals:** Price shortening (decreasing price = increasing probability = POSITIVE probability change) with high traded volume
- **Moderate Positive Signals:** Minimal price movement with steady volume indicating stable market consensus (minimal probability change)
- **Negative Signals:** Price drifting (increasing price = decreasing probability = NEGATIVE probability change) with declining volume

**Performance Data Interpretation Guidelines:**
- **Strong Positive Signals:** Recent wins, consistent strong finishes, competitive performances
- **Moderate Positive Signals:** Improvement trends, solid recent form
- **Negative Signals:** Frequent poor performances, physical/behavioral issues

**Convergence Analysis:**
- **High Confidence:** Both trading and performance indicators align positively
- **Moderate Confidence:** Mixed signals requiring careful evaluation
- **Low Confidence:** Conflicting signals between market and performance

Note: This analysis combines market intelligence (trading data) with performance intelligence (racing narratives) to provide the most comprehensive assessment possible. The process culminates in automated strategy execution on the optimal selection identified through dual-source analysis. Both table and JSON outputs ensure the analysis is accessible for human decision-making and automated processing systems.
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
   - **Human-readable table format** with EV rankings and key metrics
   - **Machine-readable JSON format** with comprehensive analysis data
   - Clear identification of best horse with appropriate strategy selection
   - Automated execution of either:
     * "Back trailing stop loss trading" (locks in profit when odds turn against opening back bet)
     * "Trade 20% profit" (takes exactly 20% profit from opening back bet)
   - Confirmation of strategy execution with reasoning

3. **Automated Process:**
   - Silent dual data collection and analysis phase
   - Comprehensive final report with best selection and strategy choice
   - **Dual format output:** Both table and JSON for different use cases
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
- **Market Confidence** - Strong volume supporting price movements
- **Performance Consistency** - Solid recent racing form and behavior
- **Price Positioning** - Favorable position within trading range
- **Strategy Suitability** - Clear betting value vs trading opportunity signals

## Combined Data Analysis Framework

**Trading Data Signals:**
- **Price Shortening + Volume** = Market confidence increasing (lower price = higher probability = POSITIVE probability change)
- **Price Drifting + Low Volume** = Market confidence decreasing (higher price = lower probability = NEGATIVE probability change)
- **Stable Price + High Volume** = Market consensus established at current probability level (minimal probability change)
- **Volatile Pricing + Mixed Volume** = Uncertainty or information flow affecting market assessment

**Performance Data Signals:**
- **Recent Wins** = High ability confirmation
- **Strong Finishes** = Competitive consistency
- **Improvement Trends** = Positive trajectory
- **Behavioral Issues** = Risk factors

**Signal Convergence Analysis:**
- **Perfect Alignment:** Trading confidence (positive probability change + volume) + strong performance = High confidence bet
- **Partial Alignment:** One strong signal + one moderate = Moderate confidence
- **Divergence Analysis:** Strong trading (probability movement) vs weak performance (or vice versa) = Careful evaluation needed
  - **Probability decreasing despite strong performance** = Potential value opportunity (market wrong)
  - **Probability increasing despite weak performance** = Market may be overconfident
- **Negative Alignment:** Poor trading (negative probability change + low volume) + poor performance = Avoid

## Output Format Specifications

### Table Format Requirements
- **Markdown table structure** with proper column alignment
- **Header row** with clear field descriptions
- **Data rows** sorted by Expected Value (highest to lowest)
- **Highlighting** or special marking for selected horse
- **Numeric formatting** with appropriate decimal places
- **Signal indicators** using descriptive text (Strong/Moderate/Weak)

### JSON Format Requirements
The comprehensive analysis data should be stored using the SetAIAgentDataContextForBetfairMarket tool with the following structure:
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
      "min_probability_betting": "number"
    },
    "horses": [
    {
      "name": "string",
      "selection_id": "string",      
      "current_price": "number",
      "start_price": "number",
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
    "risk_assessment": "string"
  },
  "market_analysis": {
    "efficiency_assessment": "string",
    "value_opportunities": "array",
    "market_biases": "array",
    "liquidity_assessment": "string",
    "total_volume": "number"
  }
}
```

## Automated Execution Sequence

1. Complete comprehensive dual-source EV analysis (silent phase)
2. Generate final report with best horse selection and strategy determination
3. **Output human-readable table format** for immediate decision review
4. **Store comprehensive JSON analysis data** using SetAIAgentDataContextForBetfairMarket tool
5. Activate selected horse's market and selection
6. Execute appropriate strategy automatically:
   - **"Back trailing stop loss trading"** for high-confidence convergent opportunities
   - **"Trade 20% profit"** for moderate confidence or mixed signal opportunities
7. Confirm successful strategy execution with reasoning
8. **Update stored JSON data** with execution results and final status

## Benefits of Dual Output Format

**Table Format Benefits:**
- **Human-readable** - Easy to scan and understand at a glance
- **Quick decision making** - Immediate visual comparison of key metrics
- **Presentation ready** - Can be directly used in reports or discussions
- **Familiar format** - Standard racing analysis presentation style

**JSON Data Storage Benefits:**
- **Persistent storage** - Data maintained in BfexplorerApp for future reference
- **API accessible** - Other systems can retrieve analysis data via MCP tools
- **Automated processing** - Enables automated decision making and backtesting
- **Version control** - Historical analysis data preserved for learning
- **Integration ready** - Seamless connection with trading systems and databases
- **Structured format** - Consistent data structure for reliable processing

This enhanced prompt ensures the most comprehensive analysis possible by combining market intelligence (what the crowd thinks via trading patterns) with performance intelligence (what the horse has actually done) while providing both human-friendly table output and persistent machine-readable JSON data storage in BfexplorerApp for maximum utility across different use cases.
