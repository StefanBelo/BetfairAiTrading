# TASK: Elite Selection Execution (Single Back)

## Objective: 
Identify the single most mathematically sound bet from the analyzed field to produce a "Maximum Conviction" selection. Use this task in conjunction with the analysis provided by `@TheAdvancedFormAndInPlayAnalystV2.md`.

## Selection Hierarchy:
1. **Primary Filter:** Identify all runners where `SuggestedAction == "VALUE BACK"`.
2. **Secondary Filter (Form Intensity):** From the Value list, select the runner with the highest `FormScore`.
3. **Tertiary Filter (Traveler Quality):** If there is a tie, prioritize the runner with the lowest `IPL_Ratio` (indicating the strongest traveler).
4. **Safety Check:** Ensure the runner does NOT have a `DistTrend > 3.0` (Regressing).

## Execution Instruction:
- **Selected Runner:** [Name]
- **Selection Price:** [Current Price]
- **Confidence Logic:** Explain why this specific runner outranks other "Value" suggestions using the IPL/Form/Trend triad.
- **Staking Plan:** Recommend a 1-unit flat stake.
- **Limit Order:** Suggest a price 2 ticks above current to ensure value is captured.
