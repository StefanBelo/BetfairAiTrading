# TheAdvancedFormAndInPlayAnalyst_Trade

## PURPOSE
This prompt is for automatic execution of a BACK-TO-LAY trade using elite form and in-play analysis. It does not generate any report or output except for executing the strategy when conditions are met.

## DATA RETRIEVAL
1. Use `get_active_market` to retrieve the current marketId, event, and all selections (selectionId, name, price).
2. Use `get_all_data_context_for_market` with `dataContextNames: ["TimeformFullDataForHorses"]` to retrieve full form and in-play data for all runners.
3. Use `set_ai_agent_data_context_for_market` to store analysis results in JSON format with `dataContextName: "AdvancedAnalyst_V2_Results"` (must include all runners).

## DATA VALIDATION
- Ensure Timeform data is present for at least 80% of runners. If missing for >20%, flag caution but proceed.

## ANALYSIS & PROCESSING
For each runner:
- Calculate all metrics using exponential decay weighting (weights: 1.0, 0.8, 0.64, 0.51, 0.41; normalization: 3.36).
- Compute:
  - IPL_Ratio: `SUM(Individual_Ratio_n * Weight_n) / 3.36` (classify: <0.45 strong traveler, >0.85 struggler)
  - DistTrend: Compare Run 1 beaten distance vs weighted historical average (normalized by 2.36). Classify: Improving (< avg - 3.0), Regressing (> avg + 3.0)
  - WinScore: Weighted frequency of wins (>=1.0 proven winner, <=0.4 maiden/exposed)
  - FormScore: Start at 50, adjust for trend, traveler, going, distance, mentality, class drop, claimer. Cap 0-100.
  - MarketImpliedProbRaw: 1/price; MarketImpliedProb: MarketImpliedProbRaw / sum(all_MarketImpliedProbRaw)
  - AdjustedWinProb: (0.70 * MarketImpliedProb) + (0.30 * (FormScore / sum(all_FormScores)))
  - SuggestedAction: BACK-TO-LAY if IPL_Ratio < 0.45 & GoingMatch true; VALUE BACK if AdjustedWinProb > MarketImpliedProb * 1.05 & not regressing; LAY/AVOID if regressing & latestBeatenDistance > 15

## FILTERING & EXECUTION LOGIC
1. Identify the runner with the lowest IPL_Ratio.
2. Runner must have SuggestedAction as "BACK-TO-LAY" or "VALUE BACK".
3. FormScore must be >= 70.
4. DistTrend must not be "Regressing".
5. If all criteria are met, immediately call:
   execute_strategy_settings {
       strategyName: "Trade 2 Ticks Profit",
       marketId: "<marketId>",
       selectionId: "<selected runner>"
   }
6. No other output or report is permitted.

## HARD CONSTRAINTS & SAFETY
- Double weight of GoingMatch on Heavy/Soft ground.
- Cap FormScore at 40 if latest beatenDistance > 15.
- Never emit any output except the execution call.

---
This is the operational prompt for agents or workflows requiring silent, structured, and automatic BACK-TO-LAY execution using elite form and in-play analysis.