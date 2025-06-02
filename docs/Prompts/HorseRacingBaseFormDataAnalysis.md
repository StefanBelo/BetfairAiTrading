# Horse Racing Base Form Data Analysis

## Core Betting Metrics Analysis Prompt

```
Task: Perform comprehensive analysis of horse racing betting opportunities using four core betting metrics: Forecast Price, Form, Official Rating, and Weight. This analysis focuses on fundamental handicapping principles to identify value betting opportunities and assess each horse's winning chances.

Instructions:

1. **Market Data Retrieval**
   - Retrieve the active betfair market using tool: GetActiveBetfairMarket
   - Save marketId for subsequent data retrieval
   - Make no preliminary reports during data collection

2. **Form Data Collection**
   - Retrieve the data context with the name 'HorsesBaseBetfairFormData' for the betfair market using tool: GetDataContextForBetfairMarket
   - Focus on the core metrics: forecastPrice, form, officialRating, and weight for each horse
   - Do not make any reports during data collection phase

3. **Core Metrics Analysis**

   **CRITICAL: Understanding the Metrics**
   - **Current Price vs Forecast Price** = Market movement indicator (shortening = confidence, drifting = doubt)
   - **Form Reading** = Recent race positions (1-9, 0=10th+, P=Pulled up, F=Fell, U=Unseated, R=Refused)
   - **Official Rating** = Handicapper's assessment of ability (higher = better)
   - **Weight** = Handicap burden (heavier = harder task)

   **A) Price Movement Analysis**
   - Compare current betting price with forecast price for each horse
   - Calculate price movement percentage: ((Current Price - Forecast Price) / Forecast Price) × 100
   - Identify horses where:
     i) **Strong Market Support**: Current price significantly shorter than forecast (> 20% reduction)
     ii) **Market Drift**: Current price longer than forecast (> 15% increase)
     iii) **Stable Pricing**: Current price within ±10% of forecast
   
   **B) Form Analysis**
   - Parse recent form string from left (most recent) to right (oldest)
   - Identify form patterns:
     i) **Consistent Form**: Regularly finishing in top 3-4 positions
     ii) **Improving Form**: Progressive improvement in recent races
     iii) **Declining Form**: Deteriorating performance trend
     iv) **Last Time Out (LTO) Winner**: Most recent race was a win (form starts with '1')
     v) **Poor Form**: Multiple poor finishes, P/F/U indicators
   
   **C) Official Rating Assessment**
   - Analyze rating relative to field:
     i) **Top Rated**: Highest official rating in race
     ii) **Well Handicapped**: Rating suggests competitive chance at current weight
     iii) **Outclassed**: Significantly lower rating than main contenders
     iv) **Unrated**: No official rating (often inexperienced horses)
   
   **D) Weight Analysis**
   - Assess weight burden relative to ability:
     i) **Weight Advantage**: Carrying less weight than rating suggests
     ii) **Fair Weight**: Weight appropriate for rating
     iii) **Overweight**: Carrying more weight than seems fair for rating
     iv) **Weight-for-Age**: Consider if appropriate weight for horse's age/experience

4. **Integrated Analysis**
   
   **A) Value Identification**
   - Cross-reference all four metrics to identify:
     i) **Strong Value Bets**: Good form + fair rating + reasonable weight + price drift
     ii) **Market Confidence**: Strong form + high rating + market support (price shortening)
     iii) **Handicap Angles**: Lower rated horse with weight advantage + improving form
     iv) **Avoid Signals**: Poor form + overweight + market drift + low rating
   
   **B) Probability Assessment**
   - Assign win probabilities based on metric convergence:
     i) **High Confidence (20%+ win probability)**: 3-4 positive metrics align
     ii) **Moderate Confidence (10-20% win probability)**: 2 positive metrics, no major negatives
     iii) **Low Confidence (5-10% win probability)**: Mixed signals or single positive
     iv) **Avoid (<5% win probability)**: Multiple negative indicators

5. **Expected Value Calculation**
   - For each horse calculate: EV = (Probability × (Price - 1)) - (1 - Probability)
   - Identify positive EV opportunities (+0.10 or better)
   - Rank horses by EV value

6. **Risk Assessment**
   
   **A) Confidence Levels**
   - **High Confidence**: All metrics support assessment, clear value identified
   - **Medium Confidence**: 3 out of 4 metrics support assessment
   - **Low Confidence**: Mixed signals or limited supporting data
   
   **B) Key Risk Factors**
   - Form inconsistency or limited recent form
   - Significant weight changes from previous races
   - Large gaps in official ratings between contenders
   - Extreme price movements (>50% from forecast)

7. **Output Format**

   **Summary Table:**
   | Horse | Current Price | Forecast Price | Price Movement | Form Analysis | Official Rating | Weight Assessment | Win Probability | Expected Value | Confidence |
   |-------|---------------|----------------|----------------|---------------|-----------------|-------------------|-----------------|----------------|------------|

   **Detailed Analysis:**
   For each horse provide:
   - **Price Movement**: Percentage change and interpretation
   - **Form Summary**: Key patterns and recent performance
   - **Rating Position**: Relative to field and weight assessment
   - **Value Assessment**: Why this horse represents value or should be avoided
   - **Risk Factors**: Specific concerns or uncertainties

   **Betting Recommendations:**
   - **Primary Selection**: Highest EV horse with positive value
   - **Secondary Options**: Alternative horses with positive EV
   - **Avoid List**: Horses with negative EV or high risk factors
   - **Market Observations**: Notable price movements or market trends

8. **Quality Control**
   - Verify all calculations are mathematically correct
   - Ensure assessments are consistent across all metrics
   - Flag any horses where data appears incomplete or contradictory
   - Provide confidence ratings for all recommendations

9. **Automated Betting Execution**
   - After completing the analysis, select the optimal horse using enhanced selection logic
   - **Enhanced Selection Logic:**
     i) **Primary Priority**: Horses with High Confidence and EV > +0.10
     ii) **Secondary Priority**: Horses with Medium Confidence and EV > +0.15
     iii) **Selection Method**: Among qualifying horses, choose the one with highest EV
     iv) **Tie-breaker**: If EV difference is <0.05, prioritize higher confidence
   
   - If the selected horse meets execution criteria:
     i) **Positive Expected Value** (EV > +0.10 for High Confidence, EV > +0.15 for Medium Confidence)
     ii) **High or Medium Confidence** rating
     iii) **No critical risk factors** that would prevent betting
   - Then execute the 'Bet 10 Euro' strategy on this selection using tool: ExecuteBfexplorerStrategySettings
   - Parameters required:
     - strategyName: "Bet 10 Euro"
     - marketId: [current market ID from step 1]
     - selectionId: [selection ID of the optimal horse]
   
   **Execution Criteria:**
   - **High Confidence horses**: EV > +0.10 (10% edge minimum)
   - **Medium Confidence horses**: EV > +0.15 (15% edge minimum - higher threshold due to lower confidence)
   - Do not execute if analysis flags critical risk factors
   - Report the selection logic and execution result clearly in the final output

10. **Final Execution Report**
    - State whether automated betting was executed
    - If executed: Report strategy name, horse selected, EV value, confidence level, and selection reasoning
    - If not executed: Explain why execution criteria were not met (include both EV and confidence analysis)
    - Provide summary of analysis confidence and recommended manual actions
    - **Selection Logic Summary**: Report which horses qualified and why the final selection was made

IMPORTANT: Base analysis solely on the four core metrics provided (forecastPrice, form, officialRating, weight). Do not speculate on factors not included in the data context. Focus on mathematical relationships and proven handicapping principles.
```

## Usage Notes

This prompt is designed to work with the `HorsesBaseBetfairFormData` context in Bfexplorer, which provides the essential handicapping metrics used by professional bettors. The analysis focuses on fundamental relationships between price, form, ability ratings, and weight to identify betting value. **Now includes automated betting execution** using the 'Bet 10 Euro' strategy for high-confidence, positive EV selections.

### Key Strengths:
- Uses core handicapping principles
- Mathematical approach to value assessment
- Clear risk assessment framework
- Focuses on proven betting metrics
- **Automated execution** of high-confidence bets
- **Built-in safety criteria** to prevent poor bet execution

### Best Used For:
- Pre-race analysis of handicap races
- Identifying value bets based on fundamental metrics
- Quick assessment of betting opportunities
- Systematic approach to horse racing betting
- **Automated betting** on strong value opportunities

### Safety Features:
- Minimum EV threshold (+0.10) for execution
- Confidence level requirements (High/Medium only)
- Risk factor assessment before execution
- Clear execution reporting and audit trail

### Integration with Bfexplorer:
- Compatible with `HorsesBaseBetfairFormData` context
- Works with standard Betfair market data
- **Executes 'Bet 10 Euro' strategy** automatically on qualifying selections
- Can be automated for systematic analysis and betting
- Suitable for both manual review and automated betting strategies
