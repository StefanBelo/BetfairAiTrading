# Horse Racing Expected Value Analysis and Betting Recommendations

## Comprehensive EV Analysis Prompt

```
Task: Perform comprehensive Expected Value (EV) analysis for horse racing betting opportunities. This analysis combines semantic interpretation of racing performance with mathematical EV calculations to identify optimal betting strategies.

Instructions:

1. **Market Data Retrieval**
   - Retrieve the active betfair market in BfexplorerApp using tool: GetActiveBetfairMarket
   - Save marketId for subsequent data retrieval
   - Make no preliminary reports during data collection

2. **Performance Data Collection**
   - Retrieve the data context with the name 'RacingpostDataForHorses' for the betfair market using tool: GetDataContextForBetfairMarket
   - Focus exclusively on the 'horsesData' field
   - Do not make any reports during data collection phase

3. **Semantic Performance Analysis**
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
   - Identify horses with positive EV (value bets)
   - Identify horses with significantly negative EV (lay opportunities)
   - Rank all horses by EV from highest to lowest

6. **Comprehensive Output Report**

   Provide structured report containing:

   **A) Individual Horse Analysis:**
   - Horse name and current market price
   - Semantic analysis summary (2-3 sentences)
   - Assigned win probability with reasoning
   - Calculated Expected Value
   - Betting recommendation (BACK/LAY/TRADE/AVOID)

   **B) EV Rankings Table:**
   - All horses ranked by Expected Value (highest to lowest)
   - Include: Horse Name, Current Price, Win Probability, EV, EV Rating
   - Use clear visual indicators (stars/symbols) for EV quality

   **C) Strategic Betting Recommendations:**
   - Top 3-5 horses with positive EV for backing
   - Horses suitable for laying (significant negative EV)
   - Portfolio approach with suggested stake distribution
   - Risk assessment for each recommendation

   **D) Market Analysis Summary:**
   - Overall market efficiency assessment
   - Key value opportunities identified
   - Potential market biases or mispricing
   - Confidence level in analysis (High/Medium/Low)

   **E) Methodology Transparency:**
   - Brief explanation of semantic analysis approach
   - Key factors that influenced probability assignments
   - Any limitations or uncertainties in the analysis

Format: Present all findings in clear, actionable format suitable for immediate betting decisions. Use tables, bullet points, and visual indicators to enhance readability and decision-making speed.

Note: This analysis relies purely on qualitative interpretation of racing narratives rather than statistical modeling, providing insights into performance trends and behavioral patterns that may not be captured in traditional quantitative approaches.
```

## Usage Instructions

1. **Input Requirements:**
   - Active Betfair market with horse racing data
   - Access to RacingpostDataForHorses context
   - Current market prices for all runners

2. **Expected Output:**
   - Complete EV analysis for all horses in race
   - Clear betting recommendations with rationale
   - Risk-assessed portfolio suggestions
   - Market efficiency insights

3. **Decision Support:**
   - Prioritize horses with positive EV for backing
   - Consider laying horses with highly negative EV
   - Use confidence levels to adjust stake sizing
   - Monitor for any last-minute market movements

## Example Analysis Framework

For each horse, the analysis should follow this structure:

**Horse Name (Current Price: X.X)**
- **Semantic Analysis:** [2-3 sentence summary of recent performance patterns]
- **Win Probability:** X% 
- **Expected Value:** +/- X.XX
- **Recommendation:** BACK/LAY/TRADE/AVOID
- **Reasoning:** [Brief explanation based on semantic analysis]

This prompt ensures comprehensive analysis while maintaining focus on practical betting applications and Expected Value optimization.
