# TASK: Elite Back-To-Lay Execution (Single Trade)

## Objective:
Identify the single most efficient runner for a **BACK-TO-LAY** trade. This selection must prioritize in-play authority (low IPL_Ratio) and tactical positioning. Use in conjunction with `TheAdvancedFormAndInPlayAnalystV2.md`.

## Selection Hierarchy (Enhanced):
1. **Primary Filter (Efficiency):** Identify runners with `IPL_Ratio < 0.45` (strong traveler).
2. **Secondary Filter (Elite Form):** Require `FormScore ≥ 75` (elite form).
3. **Tactical Trend:** Only consider runners with `DistTrend` as "Improving" or "Stable" (never "Regressing").
4. **Value Edge:** Require `AdjustedWinProb > MarketImpliedProb × 1.10` (clear value edge).
5. **Risk Exclusion:**
	- Exclude runners with regressing trends or latest beaten distance > 10.
	- Exclude runners with `FormScore < 60` or `IPL_Ratio > 0.7`.
6. **Contextual Boost:**
	- Double weight for GoingMatch on soft/heavy ground.
	- Favor runners with recent wins or proven course/distance suitability.
7. **Dynamic Filtering:**
	- If multiple runners qualify, select the one with the lowest IPL_Ratio and highest FormScore.
	- If no runner meets all criteria, avoid trading.
8. **Post-Selection Review:**
	- Track outcomes and refine thresholds (e.g., adjust FormScore/IPL_Ratio cutoffs) based on win rates.
	- Focus on high-probability, value-driven selections and minimize exposure to regressing or exposed runners.

## Execution Instruction:
- **Selected Runner:** [Name]
- **Selection Price:** [Current Price]
- **Strategy Name:** "Trade 2 Euro Profit"
- **Confidence Logic:** Explain why this runner's traveling efficiency (IPL_Ratio) makes it the premier candidate for a short-term in-play price collapse (e.g., front-runner, easy traveler).
- **Execution Call:** Use `execute_strategy_settings` for the selected runner.
