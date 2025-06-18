# Horse Racing Expected Value Analysis with Numerical Data

## Comprehensive EV Analysis and Dutch Betting Execution Prompt (Numerical Data Version)

```
Task: Perform comprehensive Expected Value (EV) analysis for horse racing betting opportunities using numerical horse performance data, select the three most qualified horses to back, and execute a Dutch betting strategy. This analysis uses mathematical evaluation of numerical performance indicators combined with EV calculations to identify the optimal Dutch betting opportunity and automatically place bets on multiple selections.

Instructions:

1. **Market Data Retrieval**
   - Retrieve the active betfair market in BfexplorerApp using tool: GetActiveBetfairMarket
   - Save marketId for subsequent data retrieval
   - Make no preliminary reports during data collection

2. **Performance Data Collection**
   - Retrieve the data context with the name 'TestRaceData' for the betfair market using tool: GetDataContextForBetfairMarket
   - Focus exclusively on the 'horseData' field
   - Extract all boolean and float fields for each horse
   - Do not make any reports during data collection phase

3. **Numerical Performance Analysis**
   - Analyze each horse's numerical data fields only
   - Process all boolean and float values in the horseData field
   - **Available Data Fields:**
     - `horseInPlayRating` (0-1.0): Primary horse performance rating
     - `horseInPlayRatingTrend` (float): Horse rating trend indicator
     - `isPreviousWinner` (boolean): Previous winning status
     - `isBeatenFavourite` (boolean): Beaten favourite indicator
     - `jockeyInPlayRating` (0-1.0): Jockey performance rating
     - `jockeyInPlayRatingTrend` (float): Jockey rating trend (positive/negative)
     - `jockeyWinnerPercentageInLastX` (0-1.0): Jockey recent win percentage
     - `beatenDistanceTrend` (float): Distance performance trend
   
   - Perform comprehensive analysis of performance indicators focusing on:
     
     a) **Horse Performance Score Calculation:**
        - Primary weight: `horseInPlayRating` (×4.0 multiplier)
        - Bonus: `isPreviousWinner` (+2.0 if true)
        - Trend factor: `horseInPlayRatingTrend` (×1.0 multiplier)
        - Risk penalty: `isBeatenFavourite` (-1.0 if true)
        - Calculate composite horse performance score (0-10 scale)
     
     b) **Jockey Performance Analysis:**
        - Jockey rating: `jockeyInPlayRating` (×2.0 multiplier)
        - Win percentage: `jockeyWinnerPercentageInLastX` (×3.0 multiplier)
        - Trend momentum: `jockeyInPlayRatingTrend` (×1.5 multiplier)
        - Calculate composite jockey performance score (0-10 scale)
     
     c) **Combined Performance Metrics:**
        - Weighted combination: Horse Score (70%) + Jockey Score (30%)
        - Trend analysis: Sum all trend indicators for momentum assessment
        - Risk assessment: Evaluate low ratings and negative trends
        - Normalize final scores across all horses in the race
     
     d) **Performance Ranking Factors:**
        - Primary: Combined performance score
        - Secondary: Individual horse and jockey ratings
        - Tertiary: Positive trend momentum
        - Risk filter: Penalize consistently low performers

4. **Win Probability Assessment**
   - Calculate realistic win probabilities using market-adjusted methodology
   - **Calculation Methodology:**
     - Horse Performance Score: `(horseInPlayRating × 4.0) + (isPreviousWinner ? 2.0 : 0) + (horseInPlayRatingTrend × 1.0) - (isBeatenFavourite ? 1.0 : 0)`
     - Jockey Performance Score: `(jockeyInPlayRating × 2.0) + (jockeyWinnerPercentageInLastX × 3.0) + (jockeyInPlayRatingTrend × 1.5)`
     - Combined Score: `(Horse Score × 0.7) + (Jockey Score × 0.3)`
     - **Market-Implied Probabilities:** Calculate baseline using `1/decimal_odds` for each horse
     - **Performance Adjustment:** Modify market probabilities based on performance scores:
       - High performers (score > 2.0): Multiply market probability by 1.5-2.5
       - Average performers (score 0.8-2.0): Multiply market probability by 0.9-1.1  
       - Poor performers (score < 0.8): Multiply market probability by 0.6-0.9
     - **Final Probabilities:** Normalize adjusted probabilities to ensure total = 100%
   - Apply conservative adjustment factors to maintain market efficiency principles
   - Document both market-implied and performance-adjusted probabilities

5. **Expected Value Calculations**
   - Calculate EV for backing each horse using formula:
     EV = (Adjusted Win Probability × (Decimal Odds - 1)) - (1 - Adjusted Win Probability)
   - Express EV as both absolute value (profit per unit bet) and percentage
   - **Realistic EV Expectations:**
     - Typical positive EV range: +5% to +50% (exceptional circumstances may show higher)
     - Values above +100% should be rare and carefully validated
     - Negative EV indicates overvalued horses in the market
   - Identify horses with positive EV (value bets) using conservative probability adjustments
   - Rank all horses by EV from highest to lowest
   - **EV Validation:** Compare calculated probabilities with market-implied probabilities to ensure realistic edges

6. **Dutch Betting Qualification Analysis**
   - Identify the top 3 horses based on combined criteria:
     - Positive Expected Value OR highest EV if none positive
     - Strong combined performance scores (horse + jockey)
     - Reasonable win probability (typically >8%)
     - Positive or neutral trend indicators
   - **Specific Selection Criteria:**
     - Primary: `horseInPlayRating` ≥ 0.5 OR `isPreviousWinner` = true
     - Secondary: `jockeyWinnerPercentageInLastX` ≥ 0.1 (10%)
     - Tertiary: Combined performance score in top 50% of field
     - Risk filter: Avoid negative trend combinations
   - Calculate combined win probability of the three selections
   - Assess Dutch betting viability and profit potential
   - **CRITICAL**: Dutch betting execution will ONLY proceed if combined win probability >= 70%

7. **Silent Analysis Phase**
   - Complete all analysis without generating interim reports
   - Focus on identifying the three best horses for Dutch betting
   - Prepare comprehensive final analysis only

8. **Final Analysis Report and Dutch Selection**

   Provide structured report containing:

   **A) Executive Summary:**
   - Three selected horses for Dutch betting with clear justification
   - Key reasons for each selection (combination of EV and numerical indicators)
   - Expected value and win probability of each selected horse
   - Combined probability and Dutch betting advantage
   - Risk assessment for the recommended Dutch bet   
     **B) Individual Horse Analysis:**
   - Horse name and current market price
   - **Horse Performance Metrics:**
     - `horseInPlayRating` score and interpretation
     - `isPreviousWinner` status and impact
     - `horseInPlayRatingTrend` momentum analysis
   - **Jockey Performance Metrics:**
     - `jockeyInPlayRating` current form
     - `jockeyWinnerPercentageInLastX` recent success rate
     - `jockeyInPlayRatingTrend` momentum direction
   - Combined performance score calculation
   - Assigned win probability with calculation basis
   - Calculated Expected Value
   - Final ranking position   **C) EV Rankings Table:**
   - All horses ranked by Expected Value (highest to lowest)
   - Include: Horse Name, Current Price, Market Prob, Adjusted Prob, Win Probability, EV %, Performance Score
   - Show both market-implied and performance-adjusted probabilities for transparency
   - Highlight horses with EV > +10% as potential value bets
   - Highlight the three selected horses for Dutch betting

   **D) Dutch Selections Summary Table:**
   Present the three selected horses in a clear table format:
   
   | Selection | Horse Name | Price | Win Prob | EV % | Performance Score | Key Indicator |
   |-----------|------------|-------|----------|------|-------------------|---------------|
   | 1st       | [Name]     | X.X   | XX%      | +XX% | X.XX/10          | [Top metric] |
   | 2nd       | [Name]     | X.X   | XX%      | +XX% | X.XX/10          | [Top metric] |
   | 3rd       | [Name]     | X.X   | XX%      | +XX% | X.XX/10          | [Top metric] |
     
   **Combined Strategy:**
   - Total Win Probability: XX%
   - Dutch Bet Status: ✅ Executed / ❌ Not Executed
   - Target Profit: €10

   **E) Dutch Selection Justification:**
   - Detailed explanation of why the chosen three horses represent the best Dutch betting opportunity
   - Combination of positive EV and strong numerical indicators for each horse
   - Combined probability analysis and coverage assessment
   - Risk-reward assessment for Dutch betting strategy
   - Confidence level in the selection based on data quality   **F) Numerical Data Analysis Summary:**
   - **Available Data Quality Assessment:**
     - Horse ratings range and distribution
     - Jockey performance metrics reliability
     - Trend indicator significance
     - Previous winner status impact
   - **Statistical Analysis:**
     - Performance score correlations with market pricing
     - Jockey win percentage vs market expectations
     - Trend momentum predictive value
     - Risk factor identification (low ratings, negative trends)
   - **Market Efficiency Analysis:**
     - Comparison of market-implied vs performance-adjusted probabilities
     - Identification of significant probability gaps (>5% difference)
     - Assessment of market biases and potential mispricing
     - Validation of EV calculations against realistic expectations
   - Comparative analysis of selected horses vs field
   - Data-driven confidence levels for selections

   **G) Dutch Betting Strategy Analysis:**
   - Combined win probability of the three selections
   - Expected profit margin from Dutch betting
   - Risk diversification benefits through numerical analysis
   - Market coverage and competitive advantage assessment

   **H) Market Analysis Summary:**
   - Overall market efficiency assessment based on numerical data
   - Key value opportunities identified through data analysis
   - Potential market biases or mispricing patterns
   - Dutch betting market position and advantages

9. **Automated Dutch Betting Execution**
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

Format: Present the final analysis in clear, actionable format with emphasis on the three best horses for Dutch betting. Only execute the Dutch betting strategy automatically if the combined win probability >= 70%.

Selection Criteria Priority:
1. Combined positive Expected Value or highest EV horses
2. Strong numerical performance indicators and scores
3. Combined win probability must be >= 70% for execution
4. High consistency and reliability metrics
5. Clear competitive advantages based on numerical data
6. Good market coverage without over-extending

Note: This analysis relies purely on quantitative evaluation of numerical performance data, providing objective insights into horse capabilities and form through statistical modeling. The process culminates in automated Dutch bet placement on the three optimal selections, providing risk diversification while maintaining value betting principles based on data-driven analysis.
```

## Usage Instructions

1. **Input Requirements:**
   - Active Betfair market with horse racing data
   - Access to TestRaceData context with numerical horse performance data
   - Current market prices for all runners
   - Access to Bfexplorer Dutch betting functionality

2. **Expected Output:**
   - Complete EV analysis for all horses in race based on numerical data
   - Clear identification of best three horses for Dutch betting
   - Conditional execution of "Dutch to profit 10 Euro" strategy (only if combined probability >= 70%)
   - Confirmation of Dutch bet placement or explanation of why bets were not placed

3. **Automated Process:**
   - Silent data collection and numerical analysis phase
   - Comprehensive final report with three best selections
   - Conditional Dutch bet execution (only if combined probability >= 70%)
   - No user intervention required during process

## Numerical Analysis Framework

The analysis will automatically select three horses that best combine:

**Primary Criteria:**
- **Realistic Positive Expected Value** - Conservative mathematical edge based on market-adjusted probability analysis (typically +10% to +50%)
- **Strong Performance Scores** - High `horseInPlayRating` and `jockeyInPlayRating` combinations
- **High Combined Probability** - Combined win probability must be >= 70% for execution

**Secondary Criteria:**
- **Previous Winner Status** - `isPreviousWinner` = true provides significant advantage
- **Jockey Win Percentage** - `jockeyWinnerPercentageInLastX` ≥ 10% preferred
- **Positive Trends** - `jockeyInPlayRatingTrend` > 0 indicates improving form
- **Risk Assessment** - Avoid horses with consistently low ratings

**Numerical Data Advantages:**
- **Objective Analysis** - No subjective interpretation required
- **Market-Aware Methodology** - Balances performance data with market efficiency
- **Conservative EV Calculations** - Realistic expectation management
- **Statistical Reliability** - Data-driven probability calculations with market validation
- **Comprehensive Coverage** - All available performance metrics utilized

## Automated Execution Sequence

1. Complete comprehensive numerical data analysis (silent phase)
2. Generate final report with best three horses selection based on data
3. Check if combined win probability >= 70%
4. If threshold met: Activate selected market and execute "Dutch to profit 10 Euro" strategy
5. If threshold not met: Report analysis without placing bets and explain why
6. Confirm execution status (successful bet placement or threshold not met)

## Dutch Betting Strategy Benefits

**Risk Management:**
- Diversification across three data-validated selections
- Reduced impact of single horse failure
- More consistent long-term returns through numerical analysis

**Value Optimization:**
- Capture value from multiple numerically superior horses
- Better market efficiency exploitation through data analysis
- Enhanced profit potential through systematic coverage

**Strategic Advantages:**
- Objective, data-driven betting approach
- Reduced emotional decision-making
- Mathematical edge through multiple statistically validated bets

This prompt ensures thorough numerical analysis while maintaining focus on identifying the three best opportunities available in the current market. Dutch betting execution occurs automatically only when the combined win probability meets or exceeds the 70% threshold, ensuring high-confidence betting decisions based on quantitative data analysis.
