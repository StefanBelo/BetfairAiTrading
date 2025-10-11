# Horse Racing Expected Value Analysis with Automated Betting

## Comprehensive EV Analysis and Betting Execution Prompt

```
Task: Perform comprehensive Expected Value (EV) analysis for horse racing betting opportunities, select the best horse to back, and execute a betting strategy. This analysis combines semantic interpretation of racing performance with mathematical EV calculations to identify the optimal betting opportunity and automatically place the bet.

Instructions:

1. **Market Data Retrieval**
   - Retrieve the active betfair market in BfexplorerApp using tool: GetActiveBetfairMarket
   - Save marketId for subsequent data retrieval
   - Make no preliminary reports during data collection

2. **Performance Data Collection**
   - Retrieve the data context with the name 'RacingpostDataForHorses' for the betfair market using tool: GetDataContextForBetfairMarket
   - For each selection, focus on the 'racingpostHorseData' field
   - Do not make any reports during data collection phase

3. **Semantic Performance Analysis**

   **CRITICAL: Price-Probability Relationship**
   - **Probability = 1 / Price** (e.g., price 4.0 = 25% probability, price 2.0 = 50% probability)
   - **Price Shortening** = Price DECREASING = Probability INCREASING = Positive market signal
   - **Price Drifting/Lengthening** = Price INCREASING = Probability DECREASING = Negative market signal

   - Analyze each horse's data in the 'lastRacesDescriptions' field ONLY (note: plural form)
   - Completely ignore the 'predictionScore' field
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

6. **Silent Analysis Phase**
   - Complete all analysis without generating interim reports
   - Focus on identifying the single best betting opportunity
   - Prepare comprehensive final analysis only

7. **Final Analysis Report and Best Selection**

   Provide structured report containing:

   **A) Executive Summary:**
   - Selected horse for backing with clear justification
   - Key reasons for selection (combination of EV and form analysis)
   - Expected value and win probability of selected horse
   - Risk assessment for the recommended bet

   **B) Individual Horse Analysis:**
   - Horse name and current market price
   - Semantic analysis summary (2-3 sentences)
   - Assigned win probability with reasoning
   - Calculated Expected Value
   - Final ranking position

   **C) EV Rankings Table:**
   - All horses ranked by Expected Value (highest to lowest)
   - Include: Horse Name, Current Price, Win Probability, EV, EV Rating
   - Highlight the selected horse for betting

   **D) Selection Justification:**
   - Detailed explanation of why the chosen horse represents the best betting opportunity
   - Combination of positive EV and strong form indicators
   - Risk-reward assessment
   - Confidence level in the selection

   **E) Market Analysis Summary:**
   - Overall market efficiency assessment
   - Key value opportunities identified
   - Potential market biases or mispricing

8. **Automated Betting Execution**
   - After completing the analysis and identifying the best horse
   - Activate the selected horse's market and selection using tool: ActivateBetfairMarketSelection
   - Execute the "Bet 10 Euro" strategy on the selected horse using tool: ExecuteBfexplorerStrategySettings
   - Confirm strategy execution

Format: Present the final analysis in clear, actionable format with emphasis on the single best betting opportunity. After analysis completion, automatically execute the betting strategy on the selected horse.

Selection Criteria Priority:
1. Positive Expected Value (EV > 0)
2. Strong semantic form indicators
3. Reasonable win probability (typically >15%)
4. Manageable risk profile
5. Clear competitive advantages based on recent performances

Note: This analysis relies purely on qualitative interpretation of racing narratives rather than statistical modeling, providing insights into performance trends and behavioral patterns that may not be captured in traditional quantitative approaches. The process culminates in automated bet placement on the optimal selection.
```

## Usage Instructions

1. **Input Requirements:**
   - Active Betfair market with horse racing data
   - Access to RacingpostDataForHorses context
   - Current market prices for all runners
   - Access to Bfexplorer betting functionality

2. **Expected Output:**
   - Complete EV analysis for all horses in race
   - Clear identification of best horse to back
   - Automated execution of "Bet 10 Euro" strategy
   - Confirmation of bet placement

3. **Automated Process:**
   - Silent data collection and analysis phase
   - Comprehensive final report with best selection
   - Automatic market activation and bet execution
   - No user intervention required during process

## Selection Framework

The analysis will automatically select the horse that best combines:

**Primary Criteria:**
- **Positive Expected Value** - Mathematical edge in the bet
- **Strong Form Indicators** - Consistent competitive performances
- **Reasonable Probability** - Realistic chance of winning (>15%)

**Secondary Criteria:**
- **Recent Performance Trends** - Improving or consistent form
- **Competitive Positioning** - Regular involvement in race finish
- **Behavioral Reliability** - Professional racing without major issues

## Automated Execution Sequence

1. Complete comprehensive EV analysis (silent phase)
2. Generate final report with best horse selection
3. Activate selected horse's market and selection
4. Execute "Bet 10 Euro" strategy automatically
5. Confirm successful bet placement

This prompt ensures thorough analysis while maintaining focus on identifying and betting the single best opportunity available in the current market.
