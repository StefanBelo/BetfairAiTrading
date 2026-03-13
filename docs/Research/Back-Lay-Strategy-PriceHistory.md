# Back/Lay Trading Strategy (Price History Data)

## Objective

Identify the best trade (back or lay) for the favourite selection in any Betfair market, using price history and volume analysis. Apply stock market price action techniques if applicable.

---

## Analysis Prompt

Use this prompt on any active Betfair market:

```
On active market, for favourite selection, use GetDataContextForMarketSelection with data context "MarketSelectionsPriceHistoryData". Analyze price and volume history to identify the best trade (back or lay). For the selection evaluate:
- Price trend (upward/downward, momentum)
- Volume spikes (liquidity, conviction)
- Support/resistance levels (price clustering)
- Volatility (price range, sudden moves)
- Recent price action (last 20 trades)

Recommend the best trade direction (back or lay), entry odds, and target exit odds (3 ticks away).

All analysis and execution must be performed automatically for the favourite selection, without asking for confirmation or summary. Automatically execute the recommended strategy using the appropriate MCP call, without requiring operator confirmation or manual intervention.
```

---

## Required MCP Tool Calls

- `GetActiveMarket` — retrieve `marketId`, `TotalMatched`, `StartTime`, and selections.
- `GetDataContextForMarketSelection` with `dataContextNames: ["MarketSelectionsPriceHistoryData"]` — retrieve price history and volume for favourite selection only.

---

## Output Requirements — Markdown Result Table (MANDATORY)

Your analysis MUST produce a markdown table (renderable) as the primary result. The table must exactly include these columns and produce a single best suggestion row flagged `BEST`:

| Selection | Odds | Volume | Direction | Entry | Exit (3 ticks) | Suggestion | Note |
|---|---:|---:|---|---:|---:|---|---|

- `Selection` — runner name
- `Odds` — current odds (numeric)
- `Volume` — recent total volume (numeric)
- `Direction` — `Back` or `Lay`
- `Entry` — recommended entry odds
- `Exit (3 ticks)` — recommended exit odds (3 ticks away)
- `Suggestion` — `BEST` for the chosen trade, otherwise `CONSIDER` or `IGNORE`
- `Note` — short justification (trend, liquidity, price action)

---

## Execute Strategy (automation instructions)

When executing the trade, the strategy must be executed automatically using `ExecuteStrategySettingsWithParameters` and set `OpenBetPosition.BetType` according to `Direction`.

- For `Back` -> `OpenBetPosition.BetType` = `Back`
- For `Lay` -> `OpenBetPosition.BetType` = `Lay`

Provide a JSON parameter block in the output showing the exact parameter set to run.

Example MCP call (pseudocode):

```
ExecuteStrategySettingsWithParameters({
  "strategyName": "Trade 3 Ticks Profit",
  "marketId": "<marketId>",
  "selectionId": "<selectionId>",
  "parameters": { "OpenBetPosition.BetType": "Back" }
})
```

The execution must be performed automatically for the favourite selection and direction, without requiring operator confirmation or manual intervention.

---

## Selection Criteria

| Criterion        | Back                                 | Lay                                  |
|------------------|--------------------------------------|--------------------------------------|
| Price trend      | Downward (odds shortening)           | Upward (odds drifting)               |
| Volume           | High (confirms market conviction)    | High (confirms market conviction)    |
| Support/resistance | Price clustering at lower odds     | Price clustering at higher odds      |
| Volatility       | Tight range for safer entry          | Tight range for safer entry          |

---

## Result Table Columns (required output structure)

| Selection | Odds | Volume | Direction | Entry | Exit (3 ticks) | Suggestion | Note |
| :--- | ---: | ---: | --- | ---: | ---: | --- | --- |

- The table must contain exactly these columns and include one row flagged `BEST` in the `Suggestion` column.
- Do not include example data in the prompt; the analysis engine must populate the table from MCP inputs.

---
