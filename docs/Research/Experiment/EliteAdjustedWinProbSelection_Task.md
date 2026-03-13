# TASK: Elite Adjusted Probability Execution (Single Trade)

## Objective:
Identify the single most mathematically likely winner based on the **AdjustedWinProb**. This selection prioritizes overall statistical dominance in the field. Use in conjunction with `@TheAdvancedFormAndInPlayAnalystV2.md`.

## Selection Hierarchy:
1. **Primary Filter (Probability):** Identify the runner with the **highest `AdjustedWinProb`** in the analyzed field.
2. **Secondary Filter (Value Check):** Priority given to runners where `SuggestedAction` is **"VALUE BACK"** or **"NEUTRAL"** over those marked as "LAY/AVOID".
3. **Tertiary Filter (Form Intensity):** If probabilities are equal, select the runner with the highest `FormScore`.
4. **Safety Check:** Ensure the runner is NOT marked as "Regressing" in `DistTrend` unless the `AdjustedWinProb` exceeds the next best by >15%.

## Execution Instruction:
- **Selected Runner:** [Name]
- **Selection Price:** [Current Price]
- **Adjusted Win Prob:** [Value]%
- **Strategy Name:** "Trade 2 Euro Profit"
- **Confidence Logic:** Explain why this runner's overall probability profile (AdjustedWinProb) makes it the most robust candidate for a high-probability win or a significant price move.
- **Execution Call:** Use `execute_strategy_settings` for the selected runner.
