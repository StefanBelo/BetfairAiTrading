# Horse Racing Expected Value Analysis with Conservative Betting Strategy

## Conservative EV Analysis and Betting Execution Prompt

```
Task: Perform comprehensive Expected Value (EV) analysis for horse racing betting opportunities with a conservative betting approach that only backs the market favorite when it demonstrates fair or better expected value AND has a win probability advantage of at least 8% over the second-best horse. This analysis combines semantic interpretation of racing performance with mathematical EV calculations to identify dominant favorites with value.

Instructions:

1. **Market Data Retrieval**
   - Retrieve the active betfair market in BfexplorerApp using tool: GetActiveMarket
   - Save marketId for subsequent data retrieval
   - Make no preliminary reports during data collection

2. **Performance Data Collection**
   - Retrieve the data context with the name 'RacingpostDataForHorses' for the betfair market using tool: GetDataContextForMarket
   - Focus exclusively on the 'horsesData' field
   - Do not make any reports during data collection phase

3. **Semantic Performance Analysis**

   **CRITICAL: Price-Probability Relationship**
   - **Probability = 1 / Price** (e.g., price 4.0 = 25% probability, price 2.0 = 50% probability)
   - **Price Shortening** = Price DECREASING = Probability INCREASING = Positive market signal
   - **Price Drifting/Lengthening** = Price INCREASING = Probability DECREASING = Negative market signal

   - Analyze each horse's data in the 'lastRacesDescription' field ONLY
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
   - Assign EV ratings based on calculated values:
     - **Excellent**: EV > +0.20
     - **Good**: EV +0.05 to +0.20
     - **Fair**: EV -0.05 to +0.05
     - **Poor**: EV -0.20 to -0.05
     - **Very Poor**: EV < -0.20
   - Identify horses with positive EV (value bets)
   - Identify horses with significantly negative EV (lay opportunities)
   - Rank all horses by EV from highest to lowest

6. **Market Favorite Identification**
   - Identify the market favorite (horse with lowest price/highest implied probability)
   - Analyze the favorite's recent form comprehensively
   - Calculate the favorite's Expected Value and assign EV rating
   - Determine if favorite meets conservative betting criteria

7. **Conservative Betting Decision**
   - **BETTING CRITERIA**: Only place bet if:
     a) The horse is the market favorite (lowest price)
     b) The favorite's EV rating is "Fair" or better (EV ≥ -0.05)
     c) The favorite shows no major recent form concerns
     d) The favorite's win probability exceeds the second-best horse by at least 8%
   - If criteria not met, analyze all horses but do NOT place any bet
   - Document reasoning for betting decision or no-bet decision

8. **Silent Analysis Phase**
   - Complete all analysis without generating interim reports
   - Focus on evaluating the market favorite's betting worthiness
   - Prepare comprehensive final analysis only

9. **Final Analysis Report and Conservative Selection**

   Provide structured report containing:   **A) Executive Summary:**
   - Market favorite identification and current price
   - Favorite's EV rating and calculated expected value
   - Favorite's win probability vs second-best horse (probability advantage)
   - **BETTING DECISION**: Whether favorite meets conservative criteria
   - Clear justification for bet/no-bet decision
   - Risk assessment for conservative approach

   **B) Market Favorite Deep Analysis:**
   - Favorite's name and current market price
   - Comprehensive semantic analysis of recent form (4-5 sentences)
   - Assigned win probability with detailed reasoning
   - Calculated Expected Value and EV rating
   - Assessment of recent performance trends
   - Identification of any concerning form indicators
   - Probability advantage over second-best horse

   **C) Complete Field EV Rankings Table:**
   - All horses ranked by Expected Value (highest to lowest)
   - Include: Horse Name, Current Price, Win Probability, EV, EV Rating
   - Highlight the market favorite and second-best horse by probability
   - Show which horses (if any) have superior EV to favorite

   **D) Conservative Selection Justification:**
   - Detailed explanation of conservative betting philosophy
   - Why favorite does/doesn't meet betting criteria (including probability advantage)
   - Probability dominance assessment (8% minimum advantage requirement)
   - Comparison of favorite's value versus field alternatives
   - Risk-reward assessment of conservative approach
   - Market efficiency evaluation

   **E) Market Analysis Summary:**
   - Overall market efficiency assessment
   - Favorite's relative value in market context
   - Alternative value opportunities (for information only)
   - Market biases or mispricing patterns

10. **Conditional Automated Betting Execution**
    - **IF** favorite meets conservative criteria:
      - Activate the favorite's market and selection using tool: ActivateBetfairMarketSelection
      - Execute the "Bet 10 Euro" strategy on the favorite using tool: ExecuteBfexplorerStrategySettings
      - Confirm strategy execution
    - **IF** favorite does NOT meet criteria:
      - Activate the favorite's market and selection using tool: ActivateBetfairMarketSelection
      - Execute the "Lay 10 Euro" strategy on the favorite using tool: ExecuteBfexplorerStrategySettings
      - Confirm strategy execution

Format: Present the final analysis in clear, actionable format with emphasis on conservative risk management. Execute betting strategy ONLY when favorite demonstrates acceptable value.

Conservative Selection Criteria (ALL must be met):
1. **Market Favorite Status** - Lowest price in market
2. **Minimum EV Standard** - EV Rating of "Fair" or better (EV ≥ -0.05)
3. **Form Reliability** - No major recent performance concerns
4. **Competitive Viability** - Recent form shows consistent competitiveness
5. **Probability Dominance** - Favorite's win probability exceeds second-best horse by at least 8%

Note: This conservative approach prioritizes capital preservation and consistent value identification over aggressive profit maximization. The strategy acknowledges that favorites often represent the most reliable betting opportunities when they offer fair value AND demonstrate clear probability dominance over the field, while avoiding the higher variance associated with backing outsiders or closely contested races.
```

## Usage Instructions

1. **Input Requirements:**
   - Active Betfair market with horse racing data
   - Access to RacingpostDataForHorses context
   - Current market prices for all runners
   - Access to Bfexplorer betting functionality

2. **Expected Output:**
   - Complete EV analysis for all horses in race
   - Focused evaluation of market favorite's betting worthiness
   - Conservative betting decision (bet favorite or lay favorite)
   - Conditional execution of "Bet 10 Euro" or "Lay 10 Euro" strategy

3. **Conservative Process:**
   - Silent data collection and analysis phase
   - Comprehensive evaluation of market favorite
   - Strict application of conservative betting criteria
   - Automatic bet execution if criteria met, lay bet if not
   - Clear documentation of betting decisions

## Conservative Selection Framework

The analysis will automatically evaluate the favorite against these criteria:

**Primary Requirements (ALL must be satisfied):**
- **Market Favorite** - Lowest price/highest implied probability
- **Fair Value Minimum** - EV Rating ≥ "Fair" (EV ≥ -0.05)
- **Form Stability** - No major recent performance deterioration
- **Competitive Evidence** - Recent races show consistent involvement
- **Probability Advantage** - Win probability exceeds second-best horse by at least 8%

**Risk Management Benefits:**
- **Capital Preservation** - Only bet when value is demonstrable
- **Reduced Variance** - Focus on most reliable market selections
- **Consistent Approach** - Systematic evaluation removes emotional decisions
- **Long-term Sustainability** - Conservative criteria protect bankroll

## Conservative Execution Sequence

1. Complete comprehensive EV analysis (silent phase)
2. Identify and deeply analyze market favorite
3. Apply conservative betting criteria strictly
4. Generate final report with betting decision
5. **IF criteria met**: Activate favorite and execute "Bet 10 Euro" strategy
6. **IF criteria NOT met**: Activate favorite and execute "Lay 10 Euro" strategy
7. Confirm execution status (bet placed or lay bet placed)

This conservative prompt ensures systematic evaluation while executing bets when the market favorite demonstrates acceptable expected value AND clear probability dominance (8%+ advantage), or laying the favorite when it doesn't meet these stricter criteria, prioritizing consistent value identification and active betting decisions in races with clear standout performers.
