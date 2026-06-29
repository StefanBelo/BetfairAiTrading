---
title: "Trade 3 Ticks Market Analysis & Execution (Silent)"
aliases: ["Trade 3 Ticks Market Analysis & Execution (Silent)", "Trade_3_Ticks_Analysis_Silent"]
type: strategy
tags: [automation, bfexplorer, horse-racing]
mcp_tools: [GetActiveMarket, GetDataContextForMarketSelection, ExecuteStrategySettingsWithParameters]
data_contexts: [MarketSelectionsPriceHistoryData]
silent: true
---

# Prompt: Trade 3 Ticks Market Analysis & Execution (Silent)

## Objective
Analyze the active Betfair market to identify the best selection among the top 3 favorites for a "Trade 3 Ticks" strategy and execute the chosen trade immediately without any human confirmation or reporting.

## Workflow

### 1. Market & Data Retrieval
- Call `get_active_market` to identify the current market.
- Identify the top 3 selections by price (lowest odds).
- Call `get_data_context_for_market_selection` to retrieve `MarketSelectionsPriceHistoryData` for these selections.

### 2. Analysis Criteria (Internal Only)
Analyze `timePriceVolumes` data for each selection focusing on:
- **Price Momentum:** Determine recent trend (last 5–10 minutes): steaming vs drifting.
- **Trade Velocity:** Frequency of matched trades; higher velocity indicates stronger moves.
- **Matched Volume Nodes:** Identify prices with highest traded volume (support/resistance nodes).
- **Anchor Reversion:** Compare current price to Opening Price for reversion vs breakout.
- **Correlation:** Compare timing of moves across top-3 favorites to find leading indicators.

All analysis MUST remain internal to the agent. Do not produce tables, summaries, or human-readable analysis output.

### 3. Selection Strategy
- **Primary Selection:** Prefer the most liquid selection among the top 3 that shows a clear trend or high-frequency small fluctuations.
- **Directional Logic:**
  - If price is steaming (decreasing) or showing strong support → open with `Back`.
  - If price is drifting (increasing) or showing resistance → open with `Lay`.

### 4. Execution (Silent, Unconfirmed)
- Immediately execute the chosen action using `execute_strategy_settings_with_parameters`.
- Use the following fields:
  - **Strategy Name:** `Trade 3 ticks`
  - **Selection:** [Identified Best Selection ID]
  - **Parameters:** `{"OpenBetPosition.BetType":"Back"}` or `{"OpenBetPosition.BetType":"Lay"}` according to the directional decision.

Mandatory execution constraints:
- Do not prompt or wait for any human confirmation before executing.
- Do not produce any human-readable report, table, or summary. Avoid logging analysis details.
- Only perform the execution call(s); return only machine-readable success/failure codes if required by the surrounding automation platform.
- On execution failure, perform a single automated retry; if the retry fails, silently abort and set an appropriate machine-readable failure status.

## Notes
- This prompt is optimized for unattended automated operation. Use only the listed `mcp_tools` and `data_contexts`.
- Keep all analysis and decision logic internal; avoid any interactive or verbose outputs.
