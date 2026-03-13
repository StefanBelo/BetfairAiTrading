# 3-Tick Back/Lay Trading Strategy (Traded Prices Data)

## Objective

Identify the best selection in any Betfair market for a 3-tick back-to-lay or lay-to-back trade, using traded prices and liquidity analysis.

---

## Analysis Prompt

Use this prompt on any active Betfair market:

```
On active market get all data context "MarketSelectionsTradedPricesData". Analyze data and identify
the best selection for 3-tick back/lay or lay/back trading. For each selection evaluate:
- Price trend (compare StartPrice, EndPrice, MaxPrice, MinPrice)
- TradedVolume (prefer high-volume selections)
- BackRatio (>0.6 back bias, <0.5 lay bias, ~0.5 balanced)
- Price range tightness (MaxPrice - MinPrice, lower spread = lower risk)

Recommend the best selection, the trading direction (back-to-lay or lay-to-back), entry odds,
and target exit odds 3 ticks away.

Automatically execute the recommended strategy for the best selection and direction using the appropriate MCP call, without requiring operator confirmation or manual intervention.
```

---

## Required MCP Tool Calls

- `GetActiveMarket` — retrieve `marketId`, `TotalMatched`, `StartTime`, and selections.
- `GetAllDataContextForMarket` with `dataContextNames: ["MarketSelectionsTradedPricesData"]` — retrieve traded prices and per-selection metrics.

---

## Output Requirements — Markdown Result Table (MANDATORY)

Your analysis MUST produce a markdown table (renderable) as the primary result. The table must exactly include these columns and produce a single best suggestion row flagged `BEST`:

| Selection | Odds | TradedVolume | BackRatio | Direction | Entry | Exit (3 ticks) | Suggestion | Note |
|---|---:|---:|---:|---|---:|---:|---|---|

- `Selection` — runner name
- `Odds` — current EndPrice (numeric)
- `TradedVolume` — total traded volume (numeric)
- `BackRatio` — provided from context or computed
- `Direction` — `Back-to-Lay` or `Lay-to-Back`
- `Entry` — recommended entry odds
- `Exit (3 ticks)` — recommended exit odds (3 ticks away)
- `Suggestion` — `BEST` for the chosen trade, otherwise `CONSIDER` or `IGNORE`
- `Note` — short justification (liquidity, trend, bias)

---

## Execute Strategy (automation instructions)

When executing the trade, the strategy must be executed automatically using `ExecuteStrategySettingsWithParameters` and set `OpenBetPosition.BetType` according to `Direction`.

- For `Back-to-Lay` -> `OpenBetPosition.BetType` = `Back`
- For `Lay-to-Back` -> `OpenBetPosition.BetType` = `Lay`

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

The execution must be performed automatically for the best selection and direction, without requiring operator confirmation or manual intervention.

---

## Selection Criteria

| Criterion        | Back-to-Lay                         | Lay-to-Back                          |
|------------------|-------------------------------------|--------------------------------------|
| Price trend      | Downward (EndPrice < StartPrice)    | Upward (EndPrice > StartPrice)       |
| BackRatio        | > 0.6 (back pressure dominant)      | < 0.5 (lay pressure dominant)        |
| TradedVolume     | High (confirms market conviction)   | High (confirms market conviction)    |
| Price range      | Tight range for safer entry         | Tight range for safer entry          |

---

## Result Table Columns (required output structure)

| Selection | Odds | TradedVolume | BackRatio | Direction | Entry | Exit (3 ticks) | Suggestion | Note |
| :--- | ---: | ---: | ---: | --- | ---: | ---: | --- | --- |

- The table must contain exactly these columns and include one row flagged `BEST` in the `Suggestion` column.
- Do not include example data in the prompt; the analysis engine must populate the table from MCP inputs.

---
