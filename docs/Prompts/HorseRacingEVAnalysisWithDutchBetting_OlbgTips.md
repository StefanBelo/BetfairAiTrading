# Horse Racing Expected Value Analysis with Dutch Betting (OLBG Tips Data)

## Comprehensive EV Analysis and Dutch Betting Execution Prompt Using OLBG Race Tips

```
Task: Perform comprehensive Expected Value (EV) analysis for horse racing betting opportunities using OLBG race tips data, select the three most qualified horses to back, and execute a Dutch betting strategy. This analysis combines semantic interpretation of expert racing tips with mathematical EV calculations to identify the optimal Dutch betting opportunity and automatically place bets on multiple selections.

Instructions:

1. **Market Data Retrieval**
   - Retrieve the active betfair market in BfexplorerApp using tool: GetActiveBetfairMarket
   - Save marketId for subsequent data retrieval
   - Make no preliminary reports during data collection

2. **OLBG Tips Data Collection**
   - Retrieve the data context with the name 'OlbgRaceTipsData' for the betfair market using tool: GetDataContextForBetfairMarket
   - Focus exclusively on the 'horseTipData' field
   - Collect all available tip data fields for each horse
   - Do not make any reports during data collection phase

3. **Comprehensive Horse Tip Data Analysis**
   - Analyze each horse's complete tip data from all available 'horseTipData' fields
   - Perform semantic analysis specifically on 'preraceComment' and 'comments' fields
   - Consider all other horseTipData fields for supporting analysis including:
     - Tip confidence levels and ratings
     - Expert tipster assessments
     - Form indicators and recommendations
     - Value assessments and market positioning
     - Any numerical scores or ratings provided

4. **Semantic Analysis of Tips Commentary**
   - Deep semantic analysis of 'preraceComment' and 'comments' fields focusing on:
     
     a) **Expert Confidence Indicators:**
        - Strong endorsements ("confident selection", "best bet", "each-way value")
        - Certainty levels in tipster language
        - Emphasis on winning chances
        - Value betting recommendations
     
     b) **Negative Tip Indicators:**
        - Uncertainty expressions ("risky", "questions to answer", "needs to prove")
        - Form concerns mentioned by tipsters
        - Market overvaluation warnings
        - Consistency or reliability issues noted
     
     c) **Positive Tip Indicators:**
        - Winning form praise and recent improvements
        - Class advantage assessments
        - Favorable race conditions mentioned
        - Trainer/jockey confidence expressed
        - Value opportunities highlighted
     
     d) **Contextual Tip Factors:**
        - Ground/weather condition advantages
        - Distance and trip suitability
        - Class level appropriateness
        - Market position and value assessment
        - Comparative analysis with other runners

5. **Multi-Field Tip Assessment**
   - Integrate semantic analysis with all other horseTipData fields
   - Weight expert recommendations and confidence scores
   - Consider tip frequency and consensus among multiple tipsters
   - Evaluate numerical ratings and assessment scores
   - Cross-reference tip quality indicators with commentary

6. **Win Probability Assessment**
   - Based on comprehensive tip data analysis, assign win probabilities to each horse
   - Consider expert consensus, confidence levels, and detailed commentary
   - Weight multiple tipster opinions and rating systems
   - Ensure probabilities are realistic and sum to approximately 100%
   - Document reasoning combining all tip data sources

7. **Expected Value Calculations**
   - Calculate EV for backing each horse using formula:
     EV = (Win Probability × (Decimal Odds - 1)) - (1 - Win Probability)
   - Identify horses with positive EV (value bets)
   - Identify horses with significantly negative EV (lay opportunities)
   - Rank all horses by EV from highest to lowest

8. **Dutch Betting Qualification Analysis**
   - Identify the top 3 horses based on combined criteria:
     - Positive Expected Value OR highest EV if none positive
     - Strong expert tip endorsements and confidence
     - High consensus among multiple tipsters
     - Reasonable win probability (typically >10%)
     - Positive semantic indicators in commentary
   - Calculate combined win probability of the three selections
   - Assess Dutch betting viability and profit potential
   - **CRITICAL**: Dutch betting execution will ONLY proceed if combined win probability >= 70%

9. **Silent Analysis Phase**
   - Complete all analysis without generating interim reports
   - Focus on identifying the three best horses for Dutch betting
   - Prepare comprehensive final analysis only

10. **Final Analysis Report and Dutch Selection**

    Provide structured report containing:

    **A) Executive Summary:**
    - Three selected horses for Dutch betting with clear justification
    - Key reasons for each selection (combination of EV and expert tip analysis)
    - Expected value and win probability of each selected horse
    - Combined probability and Dutch betting advantage
    - Risk assessment for the recommended Dutch bet
    
    **B) Individual Horse Tip Analysis:**
    - Horse name and current market price
    - Expert tip consensus summary (2-3 sentences)
    - Key tipster recommendations and confidence levels
    - Semantic analysis of preraceComment and comments
    - Supporting data from other horseTipData fields
    - Assigned win probability with reasoning
    - Calculated Expected Value
    - Final ranking position

    **C) EV Rankings Table:**
    - All horses ranked by Expected Value (highest to lowest)
    - Include: Horse Name, Current Price, Win Probability, EV %, Expert Rating
    - Highlight the three selected horses for Dutch betting

    **D) Dutch Selections Summary Table:**
    Present the three selected horses in a clear table format:
    
    | Selection | Horse Name | Price | Win Prob | EV % | Expert Consensus | Key Tip Indicator |
    |-----------|------------|-------|----------|------|------------------|-------------------|
    | 1st       | [Name]     | X.X   | XX%      | +XX% | [Rating/Confidence] | [Brief tip summary] |
    | 2nd       | [Name]     | X.X   | XX%      | +XX% | [Rating/Confidence] | [Brief tip summary] |
    | 3rd       | [Name]     | X.X   | XX%      | +XX% | [Rating/Confidence] | [Brief tip summary] |
      
    **Combined Strategy:**
    - Total Win Probability: XX%
    - Dutch Bet Status: ✅ Executed / ❌ Not Executed (< 70% threshold)
    - Target Profit: €10

    **E) Dutch Selection Justification:**
    - Detailed explanation of why the chosen three horses represent the best Dutch betting opportunity
    - Combination of positive EV and strong expert endorsements for each horse
    - Combined probability analysis and coverage assessment
    - Risk-reward assessment for Dutch betting strategy
    - Confidence level in the selection based on expert consensus

    **F) Expert Tip Analysis Summary:**
    - Overall expert consensus quality and reliability
    - Key themes from tipster commentary
    - Confidence levels across selected horses
    - Value opportunities identified by experts
    - Market efficiency assessment based on tips

    **G) Dutch Betting Strategy Analysis:**
    - Combined win probability of the three selections
    - Expected profit margin from Dutch betting
    - Risk diversification benefits through expert-backed selections
    - Market coverage and competitive advantage
    - Expert validation of strategy approach

    **H) OLBG Tips Market Analysis:**
    - Overall tip quality and expert engagement for this race
    - Consensus levels and agreement among tipsters
    - Value opportunities highlighted by expert community
    - Market biases or mispricing identified through tips
    - Dutch betting position relative to expert opinion

11. **Automated Dutch Betting Execution**
    - After completing the analysis and identifying the best three horses
    - **CONDITIONAL EXECUTION**: Only proceed with betting if combined win probability >= 70%
    - If threshold met:
      - Activate the selected market using the first horse's selection ID
      - Execute the "Dutch to profit 10 Euro" strategy on selected horses using tool: ExecuteBfexplorerStrategySettingsOnSelections
      - Provide selection IDs in the selectionIds array
      - Confirm strategy execution
    - If threshold not met:
      - Report analysis results without executing bets
      - Clearly state why Dutch betting was not executed (combined probability < 70%)

Format: Present the final analysis in clear, actionable format with emphasis on the three best horses for Dutch betting based on expert tip analysis. Only execute the Dutch betting strategy automatically if the combined win probability >= 70%.

Selection Criteria Priority:
1. Combined positive Expected Value or highest EV horses
2. Strong expert tip endorsements and high confidence ratings
3. Positive semantic indicators in preraceComment and comments
4. Combined win probability must be >= 70% for execution
5. High consensus among multiple tipsters when available
6. Manageable risk profile with expert validation
7. Clear competitive advantages identified by racing experts
8. Good market coverage without over-extending

Note: This analysis relies on expert racing tip analysis and semantic interpretation of professional commentary rather than historical race descriptions. The process combines quantitative tip ratings with qualitative expert insights to identify optimal Dutch betting opportunities, culminating in automated Dutch bet placement on the three optimal selections when confidence thresholds are met.
```

## Usage Instructions

1. **Input Requirements:**
   - Active Betfair market with horse racing data
   - Access to OlbgRaceTipsData context
   - Current market prices for all runners
   - Access to Bfexplorer Dutch betting functionality

2. **Expected Output:**
   - Complete EV analysis for all horses based on expert tips
   - Clear identification of best three horses for Dutch betting
   - Conditional execution of "Dutch to profit 10 Euro" strategy (only if combined probability >= 70%)
   - Confirmation of Dutch bet placement or explanation of why bets were not placed

3. **Automated Process:**
   - Silent data collection and expert tip analysis phase
   - Comprehensive final report with three best expert-backed selections
   - Conditional Dutch bet execution (only if combined probability >= 70%)
   - No user intervention required during process

## Dutch Selection Framework Using OLBG Tips

The analysis will automatically select three horses that best combine:

**Primary Criteria:**
- **Positive Expected Value** - Mathematical edge in the bets
- **Strong Expert Endorsements** - High confidence from professional tipsters
- **Positive Semantic Analysis** - Favorable commentary in preraceComment and comments
- **High Combined Probability** - Combined win probability must be >= 70% for execution

**Secondary Criteria:**
- **Expert Consensus** - Agreement among multiple tipsters when available
- **Tip Quality Indicators** - High-rated or confident expert selections
- **Value Identification** - Experts highlighting market opportunities
- **Risk Assessment** - Professional evaluation of chances and conditions

**OLBG Tips Advantages:**
- **Expert Validation** - Professional racing knowledge and analysis
- **Market Insight** - Expert identification of value opportunities
- **Risk Assessment** - Professional evaluation of betting opportunities
- **Quality Control** - Curated expert opinion rather than raw data

## Automated Execution Sequence

1. Complete comprehensive expert tip analysis (silent phase)
2. Generate final report with best three expert-backed horses
3. Check if combined win probability >= 70%
4. If threshold met: Activate selected market and execute "Dutch to profit 10 Euro" strategy
5. If threshold not met: Report analysis without placing bets and explain why
6. Confirm execution status (successful bet placement or threshold not met)

## Dutch Betting Strategy Benefits with Expert Tips

**Expert-Backed Risk Management:**
- Diversification across three professionally endorsed selections
- Reduced impact of single selection failure
- More consistent long-term returns through expert validation

**Value Optimization Through Expert Analysis:**
- Capture value from multiple expert-identified opportunities
- Better market efficiency exploitation through professional insight
- Enhanced profit potential through expert-validated coverage

**Strategic Advantages:**
- Professional racing expertise integration
- Reduced emotional attachment through expert validation
- Mathematical edge through multiple expert-backed value bets
- Quality control through professional tip analysis

This prompt ensures thorough analysis of expert racing tips while maintaining focus on identifying the three best opportunities available in the current market. Dutch betting execution occurs automatically only when the combined win probability meets or exceeds the 70% threshold, ensuring high-confidence betting decisions backed by professional racing expertise.
