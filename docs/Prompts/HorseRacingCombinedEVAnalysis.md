# Horse Racing Combined Expected Value Analysis and Betting Recommendations

## Comprehensive Dual-Method EV Analysis Prompt

```
Task: Perform comprehensive Expected Value (EV) analysis for horse racing betting opportunities using a dual methodology that combines quantitative prediction scores with semantic interpretation of racing performance to identify optimal betting strategies.

Instructions:

1. **Market Data Retrieval**
   - Retrieve the active betfair market in BfexplorerApp using tool: GetActiveBetfairMarket
   - Save marketId for subsequent data retrieval
   - Make no preliminary reports during data collection

2. **Performance Data Collection**
   - Retrieve the data context with the name 'RacingpostDataForHorsesInfo' for the betfair market using tool: GetDataContextForBetfairMarket
   - Extract both 'predictionScore' and 'lastRacesDescription' fields from 'horsesData'
   - Do not make any reports during data collection phase

3. **Dual Analysis Methodology**

   **A) Semantic Performance Analysis**
   - Analyze each horse's 'lastRacesDescription' field for qualitative insights
   - Perform deep semantic analysis of recent race descriptions focusing on:
     
     i) **Performance Patterns:**
        - Consistency of competitive positions
        - Frequency of strong finishes ("led", "kept on well", "ran on")
        - Recovery ability from setbacks
        - Late race surge capability
     
     ii) **Negative Performance Indicators:**
        - Frequent mentions of "weakened", "outpaced", "struggling"
        - Jumping errors or hesitation (for NH racing)
        - Physical issues (lameness, stumbling, breathing problems)
        - Behavioral problems (hanging, running green, refused to enter stalls)
     
     iii) **Positive Performance Indicators:**
        - Winning performances and margins
        - Strong finishing kicks and sustained runs
        - Competitive throughout race duration
        - Professional racing behavior
        - Tactical improvements
     
     iv) **Contextual Performance Factors:**
        - Ground condition preferences and performance
        - Equipment changes and their impact (blinkers, cheek-pieces)
        - Trainer/jockey comments indicating improvement or issues
        - Class level performance and progression
        - Distance suitability evidence

   **B) Prediction Score Analysis**
   - Evaluate each horse's 'predictionScore' (0-100 scale)
   - Consider prediction scores as quantitative performance indicators
   - Note that scores may reflect:
     - Historical form analysis
     - Statistical modeling outputs
     - Composite performance metrics
     - Recent form trends

4. **Dual Win Probability Assessment**
   
   **Semantic-Only Probabilities:**
   - Assign win probabilities based purely on semantic analysis
   - Weight recent form narratives and performance patterns
   - Consider race type, distance, and conditions mentioned
   - Ensure probabilities sum to approximately 100%

   **Combined Probabilities:**
   - Integrate prediction scores with semantic analysis
   - Use prediction scores to validate or adjust semantic assessments
   - Weight methodology based on:
     - Consistency between prediction score and recent form
     - Quality and recency of race description data
     - Race type and distance factors
   - Higher prediction scores should generally correlate with better recent form
   - Significant discrepancies require investigation and explanation

5. **Dual Expected Value Calculations**
   
   Calculate EV using both methodologies:
   - **Semantic EV** = (Semantic Win Probability × (Decimal Odds - 1)) - (1 - Semantic Win Probability)
   - **Combined EV** = (Combined Win Probability × (Decimal Odds - 1)) - (1 - Combined Win Probability)
   
   Identify value opportunities in both calculations:
   - Horses with positive EV in either method
   - Horses with consistent positive EV in both methods (highest confidence)
   - Significant discrepancies between methods requiring explanation

6. **Comprehensive Dual-Method Output Report**

   Provide structured report containing:

   **A) Individual Horse Analysis:**
   - Horse name and current market price
   - Prediction score (0-100)
   - Semantic analysis summary (2-3 sentences)
   - Semantic-only win probability with reasoning
   - Combined win probability with methodology explanation
   - Both Expected Value calculations
   - Final betting recommendation (BACK/LAY/TRADE/AVOID)
   - Confidence level based on methodology agreement

   **B) Dual EV Rankings Table:**
   - All horses ranked by Combined Expected Value (highest to lowest)
   - Include columns: Horse Name, Price, Prediction Score, Semantic Prob, Combined Prob, Semantic EV, Combined EV, EV Rating
   - Use clear visual indicators (stars/symbols) for EV quality
   - Highlight horses with consistent positive EV across both methods

   **C) Strategic Betting Recommendations:**
   - Primary selections: Horses with positive Combined EV and strong semantic support
   - Secondary selections: Horses with positive EV in one method with explanation
   - Laying opportunities: Horses with negative EV in both methods
   - Portfolio approach with suggested stake distribution based on confidence levels
   - Risk assessment categorized by methodology agreement

   **D) Market Analysis Summary:**
   - Overall market efficiency assessment using dual methodology
   - Key value opportunities identified through combined analysis
   - Prediction score vs. semantic analysis discrepancies and insights
   - Market biases revealed by methodology differences
   - Confidence level in analysis (High/Medium/Low) based on consistency

   **E) Methodology Transparency:**
   - Explanation of how prediction scores influenced probability adjustments
   - Key semantic factors that validated or contradicted prediction scores
   - Cases where methodologies disagreed and resolution approach
   - Limitations of each approach and combined methodology
   - Recommendations for when to favor one method over the other

7. **Quality Control Checks:**
   - Verify that combined probabilities sum to approximately 100%
   - Ensure prediction scores are properly weighted against recent form evidence
   - Check for logical consistency in probability adjustments
   - Validate that high prediction scores align with positive semantic indicators
   - Flag any horses where methodologies significantly diverge for manual review

Format: Present all findings in clear, actionable format suitable for immediate betting decisions. Use tables, bullet points, and visual indicators to enhance readability and decision-making speed. Prioritize recommendations where both methodologies agree for highest confidence plays.

Note: This dual approach provides both quantitative validation and qualitative insights, offering a more robust analysis than either method alone. The prediction scores serve as a statistical baseline while semantic analysis captures nuanced performance factors and recent form trends that may not be reflected in historical metrics.
```

## Usage Instructions

1. **Input Requirements:**
   - Active Betfair market with horse racing data
   - Access to RacingpostDataForHorsesInfo context with both predictionScore and lastRacesDescription
   - Current market prices for all runners

2. **Expected Output:**
   - Dual-method EV analysis for all horses in race
   - Both semantic-only and combined probability assessments
   - Clear betting recommendations with confidence ratings
   - Risk-assessed portfolio suggestions based on methodology agreement
   - Market efficiency insights from comparative analysis

3. **Decision Support:**
   - Highest confidence: Positive Combined EV with supporting semantic analysis
   - Medium confidence: Positive EV in one method with logical explanation
   - Laying opportunities: Negative EV in both methods with poor form evidence
   - Portfolio diversification based on confidence levels and risk tolerance

## Example Analysis Framework

For each horse, the analysis should follow this structure:

**Horse Name (Current Price: X.X)**
- **Prediction Score:** XX/100
- **Semantic Analysis:** [2-3 sentence summary of recent performance patterns]
- **Semantic-Only Win Probability:** X% (reasoning)
- **Combined Win Probability:** X% (adjustment explanation)
- **Semantic EV:** +/- X.XX | **Combined EV:** +/- X.XX
- **Recommendation:** BACK/LAY/TRADE/AVOID
- **Confidence:** HIGH/MEDIUM/LOW based on methodology agreement
- **Reasoning:** [Brief explanation of final assessment combining both approaches]

## Methodology Weighting Guidelines

- **High Prediction Score (80-100) + Strong Semantic Support:** Maximum confidence, may slightly boost combined probability
- **High Prediction Score (80-100) + Weak Semantic Support:** Investigate discrepancy, moderate confidence
- **Low Prediction Score (0-30) + Strong Semantic Support:** Recent improvement possible, moderate confidence  
- **Low Prediction Score (0-30) + Weak Semantic Support:** Low confidence, likely avoid
- **Medium Prediction Score (31-79):** Weight semantic analysis more heavily for final assessment

This prompt ensures comprehensive analysis while leveraging both quantitative predictions and qualitative insights for optimal betting decision-making.
