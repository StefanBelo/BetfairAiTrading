---
title: "Trade 3 Ticks Market Analysis & Execution"
aliases: ["Trade 3 Ticks Market Analysis & Execution"]
type: strategy
tags: [automation, bfexplorer, horse-racing]
mcp_tools: [GetActiveMarket, GetDataContextForMarketSelection, ExecuteStrategySettingsWithParameters]
data_contexts: [MarketSelectionsPriceHistoryData]
---

# Prompt: Trade 3 Ticks Market Analysis & Execution

## Objective
Analyze the active Betfair market to identify the best selection among the top 3 favorites for a "Trade 3 Ticks" strategy. Determine the optimal opening bet direction (Back or Lay) based on price momentum and liquidity, then execute the strategy.

## Workflow

### 1. Market & Data Retrieval
- Use `get_active_market` to identify the current market.
- Identify the top 3 selections by price (lowest odds).
- Use `get_data_context_for_market_selection` to retrieve `MarketSelectionsPriceHistoryData` for these 3 selections.

### 2. Analysis Criteria
Analyze the retrieved `timePriceVolumes` data for each selection, focusing on:
- **Price Momentum:** Identify the recent trend (last 5–10 mins). Is the price "steaming" or "drifting"?
- **Trade Velocity:** Check the frequency of matched trades. High velocity indicates a strong, active move.
- **Matched Volume Nodes:** Identify prices with the highest traded volume. These are support/resistance levels for "bouncing" 3-tick trades.
- **Anchor Reversion:** Compare the current price to the **Opening Price**. Is the price reverting to its early morning value or breaking out into a new range?
- **Correlation:** Compare the timing of moves across the top 3 favorites to find lead indicators or delayed reactions (gaps).

### 3. Selection Strategy
- **Primary Selection:** Choose the most liquid selection (usually the favorite) that shows a clear, steady trend or high frequency of small fluctuations.
- **Directional Logic:**
    - If the price is **steaming** (decreasing) or showing strong support: Open with a **BACK** bet.
    - If the price is **drifting** (increasing) or showing resistance: Open with a **LAY** bet.

### 4. Execution
Execute the strategy using `execute_strategy_settings_with_parameters`:
- **Strategy Name:** "Trade 3 ticks"
- **Selection:** [Identified Best Selection ID]
- **Parameters:** Set `{"OpenBetPosition.BetType":"Back"}` or `{"OpenBetPosition.BetType":"Lay"}` based on your analysis.

## Required Output
1. A summary table of the 3 analyzed favorites.
2. The rationale for the chosen selection and direction.
3. Confirmation of the strategy execution call.
