# 3-Tick Back/Lay Trading Strategy

## Objective

Identify the best selection in any Betfair market for a 3-tick back-to-lay or lay-to-back trade, using candlestick price action and liquidity analysis.

---

## Analysis Prompt

Use this prompt on any active Betfair market:

```
On active market get all data context "MarketSelectionsCandleStickData". Analyze data and identify
the best selection for 3-tick back/lay or lay/back trading. For each selection evaluate:
- Price trend across candles (direction and momentum)
- Volume and liquidity (prefer high-volume selections)
- BackLayRatio (>0.6 back bias, <0.5 lay bias, ~0.5 balanced)
- Price range tightness (low candle body spread = lower risk)

Recommend the best selection, the trading direction (back-to-lay or lay-to-back), entry odds,
and target exit odds 3 ticks away.

Automatically execute the recommended strategy for the best selection and direction using the appropriate MCP call, without requiring operator confirmation or manual intervention.
```

---

## Required MCP Tool Calls

This prompt must declare and use the following MCP calls at the start (these are mandatory inputs):

- `GetActiveMarket` — retrieve `marketId`, `TotalMatched`, `StartTime`, and selections.
- `GetAllDataContextForMarket` with `dataContextNames: ["MarketSelectionsCandleStickData"]` — retrieve selection OHLC/volume candles and any per-selection metrics (e.g., `BackLayRatio`).

Make these calls before performing analysis. Example (pseudocode):

```
GetActiveMarket()
GetAllDataContextForMarket(marketId, ["MarketSelectionsCandleStickData"]) 
```

---

## Output Requirements — Markdown Result Table (MANDATORY)

Your analysis MUST produce a markdown table (renderable) as the primary result. The table must exactly include these columns and produce a single best suggestion row flagged `BEST`:

| Selection | Odds | Volume | BackLayRatio | Direction | Entry | Exit (3 ticks) | Suggestion | Note |
|---|---:|---:|---:|---|---:|---:|---|---|

- `Selection` — runner name
- `Odds` — current odds (numeric)
- `Volume` — recent total volume (numeric)
- `BackLayRatio` — provided from context or computed
- `Direction` — `Back-to-Lay` or `Lay-to-Back`
- `Entry` — recommended entry odds
- `Exit (3 ticks)` — recommended exit odds (3 ticks away)
- `Suggestion` — `BEST` for the chosen trade, otherwise `CONSIDER` or `IGNORE`
- `Note` — short justification (liquidity, trend, bias)

Example required single-row marker: set `Suggestion` = `BEST` for the selected runner.

---

## Execute Strategy (automation instructions)

When executing the trade, the strategy must be executed automatically using `ExecuteStrategySettingsWithParameters` and set `OpenBetPosition.BetType` according to `Direction`.

- For `Back-to-Lay` -> `OpenBetPosition.BetType` = `Back`
- For `Lay-to-Back` -> `OpenBetPosition.BetType` = `Lay`

Provide a JSON parameter block in the output showing the exact parameter set to run, for example:

```json
{
  "StrategyName": "Trade 3 Ticks Profit",
  "Parameters": { "OpenBetPosition.BetType": "Back" }
}
```

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
---

## Selection Criteria

| Criterion        | Back-to-Lay                         | Lay-to-Back                          |
|------------------|-------------------------------------|--------------------------------------|
| Price trend      | Downward (odds shortening)          | Upward (odds drifting)               |
| BackLayRatio     | > 0.6 (back pressure dominant)      | < 0.5 (lay pressure dominant)        |
| Volume           | High (confirms market conviction)   | High (confirms market conviction)    |
| Tick range       | Tight range for safer entry         | Tight range for safer entry          |

---

## Execute Strategy

Once the best selection and trading direction are identified, execute the strategy using BFExplorer's
**"Trade 2 Ticks Profit"** strategy with the `OpenBetPosition.BetType` parameter
set according to the trading direction:

### Back-to-Lay (price expected to shorten)

```json
{
  "OpenBetPosition.BetType": "Back"
}
```

Execute on the selected market and selection:
```
Execute strategy "Trade 2 Ticks Profit" on active market, best selection,
with parameters: { "OpenBetPosition.BetType": "Back" }
```

### Lay-to-Back (price expected to drift)

```json
{
  "OpenBetPosition.BetType": "Lay"
}
```

Execute on the selected market and selection:
```
Execute strategy "Trade 3 Ticks Profit" on active market, best selection,
with parameters: { "OpenBetPosition.BetType": "Lay" }
```

---

## Result Table Columns (required output structure)

Produce a single markdown table with these columns (no example rows):

| Selection | Odds | Volume | BackLayRatio | Direction | Entry | Exit (3 ticks) | Suggestion | Note |
| :--- | ---: | ---: | ---: | --- | ---: | ---: | --- | --- |

- The table must contain exactly these columns and include one row flagged `BEST` in the `Suggestion` column.
- Do not include example data in the prompt; the analysis engine must populate the table from MCP inputs.

## Notes

- Apply this prompt and strategy to any Betfair market before race start.
- Tick sizes vary by odds range — always verify 3 ticks at the relevant price point.
- Confirm liquidity (volume) and price direction before executing.
- The strategy stops automatically at 2-tick profit or 10-tick loss.
