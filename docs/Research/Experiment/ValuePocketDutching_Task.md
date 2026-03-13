# TASK: Portfolio Execution (Dutching the Value Pocket)

## Objective: 
Create a dutched portfolio of all runners identified as having a "Probabilistic Edge" to neutralize individual runner variance. Use this task in conjunction with the analysis provided by `@TheAdvancedFormAndInPlayAnalystV2.md`.

## Selection Hierarchy:
1. **Inclusion Rule:** Include EVERY runner where `SuggestedAction == "VALUE BACK"`.
2. **Exclusion Rule:** Explicitly exclude any runner where `SuggestedAction` is "WATCH" or "LAY/AVOID", even if they are the favorite.
3. **Weighting Logic:** Calculate the stake distribution based on `AdjustedWinProb`.
4. **Validation:** Analyze if the market favorite is a "Struggler" (`IPL_Ratio > 0.80`). If yes, increase the conviction in the Dutching strategy.

## Execution Instruction:
- **Dutch Portfolio:** List all included runners and their current prices.
- **Combined Odds:** Calculate the "Real Odds" of the combined dutch.
- **Combined Win Probability:** Sum the `AdjustedWinProb` of the selections.
- **Strategic Note:** Contrast the combined Form Scores of your "Value Pocket" against the favorite to highlight the source of the edge.
