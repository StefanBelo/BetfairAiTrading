# Horse Racing Expected Value Analysis with Dutch Betting

## Comprehensive EV Analysis and Dutch Betting Execution Prompt

```
Task: Perform comprehensive Expected Value (EV) analysis for horse racing betting opportunities, select the three most qualified horses to back, and execute a Dutch betting strategy. This analysis combines semantic interpretation of racing performance with mathematical EV calculations to identify the optimal Dutch betting opportunity and automatically place bets on multiple selections.

Instructions:

1. **Market Data Retrieval**
   - Retrieve the active betfair market in BfexplorerApp using tool: GetActiveMarket
   - Save marketId for subsequent data retrieval
   - Make no preliminary reports during data collection

2. **Performance Data Collection**
   - Retrieve the data context with the name 'RacingpostDataForHorses' for the betfair market using tool: GetDataContextForMarket
   - Focus exclusively on the 'selectionsData' field
   - Do not make any reports during data collection phase

3. **Semantic Performance Analysis**
   - Analyze each horse's data in the 'racingpostHorseData.lastRacesDescriptions' field ONLY
   - Access race descriptions from the array of race objects within this field
   - Completely ignore the 'predictionScore' field and focus only on the 'raceDescription' text within each race entry
   - Perform deep semantic analysis of recent race descriptions focusing on:
     
     a) **Performance Patterns:**
        - Consistency of competitive positions
        - Frequency of strong finishes ("led", "kept on well", "ran on")
        - Recovery ability from setbacks
        - Late race surge capability
     
     b) **Negative Performance Indicators:**
        - Frequent mentions of "weakened", "outpaced", "struggling"
        - Jumping errors or hesitation
        - Physical issues (lameness, stumbling, breathing problems)
        - Behavioral problems (hanging, running green)
     
     c) **Positive Performance Indicators:**
        - Winning performances and margins
        - Strong finishing kicks and sustained runs
        - Competitive throughout race duration
        - Professional racing behavior
     
     d) **Contextual Performance Factors:**
        - Ground condition preferences and performance
        - Equipment changes and their impact (blinkers, cheek-pieces)
        - Trainer/jockey comments indicating improvement or issues
        - Class level performance and progression

4. **Win Probability Assessment**
   - Based solely on semantic analysis, assign win probabilities to each horse
   - Consider recent form trends, consistency, and competitive ability
   - Ensure probabilities are realistic and sum to approximately 100%
   - Document reasoning for each probability assignment

5. **Expected Value Calculations**
   - Calculate EV for backing each horse using formula:
     EV = (Win Probability × (Decimal Odds - 1)) - (1 - Win Probability)
   - Identify horses with positive EV (value bets)
   - Identify horses with significantly negative EV (lay opportunities)
   - Rank all horses by EV from highest to lowest

6. **Dutch Betting Qualification Analysis**
   - Identify the top 3 horses based on combined criteria:
     - Positive Expected Value OR highest EV if none positive
     - Strong semantic form indicators
     - Reasonable win probability (typically >10%)
     - Consistent competitive performances
   - Calculate combined win probability of the three selections
   - Assess Dutch betting viability and profit potential
   - **CRITICAL**: Dutch betting execution will ONLY proceed if combined win probability >= 60%

7. **Silent Analysis Phase**
   - Complete all analysis without generating interim reports
   - Focus on identifying the three best horses for Dutch betting
   - Prepare comprehensive final analysis only

8. **Final Analysis Report and Dutch Selection**

   Provide structured report containing:

   **A) Executive Summary:**
   - three selected horses for Dutch betting with clear justification
   - Key reasons for each selection (combination of EV and form analysis)
   - Expected value and win probability of each selected horse
   - Combined probability and Dutch betting advantage
   - Risk assessment for the recommended Dutch bet   
   
   **B) Individual Horse Analysis:**
   - Horse name and current market price
   - Semantic analysis summary (2-3 sentences)
   - Assigned win probability with reasoning
   - Calculated Expected Value
   - Final ranking position   
   **C) EV Rankings Table:**
   - All horses ranked by Expected Value (highest to lowest)
   - Include: Horse Name, Current Price, Win Probability, EV %, EV Rating
   - Highlight the three selected horses for Dutch betting

   **D) Dutch Selections Summary Table:**
   Present the three selected horses in a clear table format:
   
   | Selection | Horse Name | Price | Win Prob | EV % | Key Form Indicator |
   |-----------|------------|-------|----------|------|-------------------|
   | 1st       | [Name]     | X.X   | XX%      | +XX% | [Brief form summary] |
   | 2nd       | [Name]     | X.X   | XX%      | +XX% | [Brief form summary] |
   | 3rd       | [Name]     | X.X   | XX%      | +XX% | [Brief form summary] |
     
   **Combined Strategy:**
   - Total Win Probability: XX%
   - Dutch Bet Status: ✅ Executed
   - Target Profit: €10

   **E) Dutch Selection Justification:**
   - Detailed explanation of why the chosen three horses represent the best Dutch betting opportunity
   - Combination of positive EV and strong form indicators for each horse
   - Combined probability analysis and coverage assessment   - Risk-reward assessment for Dutch betting strategy
   - Confidence level in the selection

   **F) Dutch Betting Strategy Analysis:**
   - Combined win probability of the three selections
   - Expected profit margin from Dutch betting
   - Risk diversification benefits
   - Market coverage and competitive advantage

   **G) Market Analysis Summary:**
   - Overall market efficiency assessment
   - Key value opportunities identified
   - Potential market biases or mispricing
   - Dutch betting market position

9. **Automated Dutch Betting Execution**
   - After completing the analysis and identifying the best three horses
   - **CONDITIONAL EXECUTION**: Only proceed with betting if combined win probability >= 60%
   - If threshold met:
     - Activate the selected market using the first horse's selection ID
     - Execute the "Dutch to profit 10 Euro" strategy on selected horses using tool: ExecuteBfexplorerStrategySettingsOnSelections
     - Provide selection IDs in the selectionIds array
     - Confirm strategy execution
   - If threshold not met:
     - Report analysis results without executing bets
     - Clearly state why Dutch betting was not executed (combined probability < 60%)

Format: Present the final analysis in clear, actionable format with emphasis on the three best horses for Dutch betting. Only execute the Dutch betting strategy automatically if the combined win probability >= 60%.

Selection Criteria Priority:
1. Combined positive Expected Value or highest EV horses
2. Strong semantic form indicators for selections
3. Combined win probability must be >= 60% for execution
4. Manageable risk profile with diversification benefits
5. Clear competitive advantages based on recent performances
6. Good market coverage without over-extending

Note: This analysis relies purely on qualitative interpretation of racing narratives rather than statistical modeling, providing insights into performance trends and behavioral patterns that may not be captured in traditional quantitative approaches. The process culminates in automated Dutch bet placement on the three optimal selections, providing risk diversification while maintaining value betting principles.
```

## Usage Instructions

1. **Input Requirements:**
   - Active Betfair market with horse racing data
   - Access to RacingpostDataForHorses context
   - Current market prices for all runners
   - Access to Bfexplorer Dutch betting functionality

2. **Expected Output:**
   - Complete EV analysis for all horses in race
   - Clear identification of best three horses for Dutch betting
   - Conditional execution of "Dutch to profit 10 Euro" strategy (only if combined probability >= 60%)
   - Confirmation of Dutch bet placement or explanation of why bets were not placed

3. **Automated Process:**
   - Silent data collection and analysis phase
   - Comprehensive final report with three best selections
   - Conditional Dutch bet execution (only if combined probability >= 60%)
   - No user intervention required during process

## Dutch Selection Framework

The analysis will automatically select three horses that best combine:

**Primary Criteria:**
- **Positive Expected Value** - Mathematical edge in the bets
- **Strong Form Indicators** - Consistent competitive performances
- **High Combined Probability** - Combined win probability must be >= 60% for execution

**Secondary Criteria:**
- **Recent Performance Trends** - Improving or consistent form
- **Competitive Positioning** - Regular involvement in race finish
- **Behavioral Reliability** - Professional racing without major issues
- **Risk Diversification** - Different running styles or race positions

**Dutch Betting Advantages:**
- **Risk Spreading** - Multiple chances to win
- **Market Coverage** - Better race coverage than single selection
- **Value Maximization** - Capture value from multiple horses
- **Reduced Variance** - More consistent returns over time

## Automated Execution Sequence

1. Complete comprehensive EV analysis (silent phase)
2. Generate final report with best three horses selection
3. Check if combined win probability >= 60%
4. If threshold met: Activate selected market and execute "Dutch to profit 10 Euro" strategy
5. If threshold not met: Report analysis without placing bets and explain why
6. Confirm execution status (successful bet placement or threshold not met)

## Dutch Betting Strategy Benefits

**Risk Management:**
- Diversification across three quality selections
- Reduced impact of single horse failure
- More consistent long-term returns

**Value Optimization:**
- Capture value from multiple mispriced horses
- Better market efficiency exploitation
- Enhanced profit potential through coverage

**Strategic Advantages:**
- Professional betting approach
- Reduced emotional attachment to single selection
- Mathematical edge through multiple value bets

This prompt ensures thorough analysis while maintaining focus on identifying the three best opportunities available in the current market. Dutch betting execution occurs automatically only when the combined win probability meets or exceeds the 60% threshold, ensuring high-confidence betting decisions.
