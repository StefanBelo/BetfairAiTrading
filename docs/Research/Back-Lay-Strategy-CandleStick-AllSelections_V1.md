# Back/Lay Selection Strategy (Candlestick Data, All Selections) — V1 with Internal Probability

## Objective

Identify the best back or lay bet among all selections in any Betfair market, using candlestick price history, volume analysis, and a calculated probability to win derived from internal score values (not odds/price). Analyze correlation in candlestick price movements between all selections to improve prediction. The strategy executed is a simple back or lay bet of 10 Euro on the best candidate, indicating if the horse wins or loses, and providing a probability estimate based on internal model scores.

---

## Analysis Prompt

Use this prompt on any active Betfair market:

```
On active market, use GetAllDataContextForMarket with data context "MarketSelectionsCandleStickData" to retrieve candlestick price and volume history for all selections. Analyze candlestick price and volume history for each, and evaluate:
- Price trend (upward/downward, momentum) from Open/Close/High/Low
- Volume spikes (liquidity, conviction)
- Support/resistance levels (price clustering in High/Low/Close)
- Volatility (price range, sudden moves)
- Back/Lay ratio (market sentiment)
- Correlation in candlestick price movements between all selections (e.g., do odds move together or inversely?)
- Price "true value" detection (volume + stability rule): require the analysis engine to wait for a small stabilization signal before treating early opening trades as the market price. Use both a cumulative volume threshold and short-term price stability checks (VWAP / spread / depth) to confirm the price reflects genuine liquidity rather than noise.
- **Probability to Win Calculation (Internal Score):** For each selection, calculate an internal score based on the evaluated data features (trend, volume, volatility, support/resistance, correlation, etc). Convert these internal scores into a probability to win using a normalization method (e.g., softmax or min-max scaling across all selections). Do NOT use odds/price for this probability. The probability should reflect the model's confidence in each selection's chance to win based on the internal analysis only.

  Example defaults (tunable):
  - `AbsoluteMin` = 150 currency units matched on the runner since market open.
  - `RelPct` = 0.002 (0.2%) of market `TotalMatched`. Require cumulative matched >= `max(AbsoluteMin, RelPct * TotalMatched)`.
  - Stability window: last `W` candlesticks or last `T` minutes (defaults: `W=6` candlesticks or `T=30` minutes).
  - Stability thresholds: VWAP change over the window < `X%` (default `X=0.3%`), best back/lay spread <= 2 ticks, and depth at best price >= `DepthMin` (default `DepthMin=30` units).
  - Optional confirmation: require the stability condition hold for `N` consecutive checks (default `N=2`).

  Behavioural rule for the analysis engine:
  - Do not flag a runner as suitable for immediate execution unless the "true value" rule is satisfied. If not satisfied, report `WAIT` in the `Suggestion` column for that runner and include the reason (e.g., "insufficient matched volume", "VWAP unstable", or "thin depth").
  - When the rule is satisfied, include the cumulative matched, VWAP, spread and depth metrics used to reach the decision in the `Note` field.

Use correlation analysis to better predict price movement for the best selection. Recommend the best trade direction (back or lay), entry odds, indicate if the horse is expected to win or lose, and provide the calculated probability to win (from internal score).

All analysis and execution must be performed automatically for the best selection, without asking for confirmation or summary. Automatically execute the recommended strategy using the appropriate MCP call, without requiring operator confirmation or manual intervention.
```

---

## Required MCP Tool Calls

- `GetActiveMarket` — retrieve `marketId`, `TotalMatched`, `StartTime`, and selections.
- `GetAllDataContextForMarket` with `dataContextNames: ["MarketSelectionsCandleStickData"]` — retrieve candlestick price history and volume for all selections.

---

## Output Requirements — Markdown Result Table (MANDATORY)

Your analysis MUST produce a markdown table (renderable) as the primary result. The table must exactly include these columns and produce a single best suggestion row flagged `BEST`:

| Selection | Odds | Volume | Direction | Entry | Suggestion | Note | Correlation | Probability | Win/Lose |
|---|---:|---:|---|---:|---|---|---|---:|---|

- `Selection` — runner name
- `Odds` — current odds (numeric)
- `Volume` — recent total volume (numeric)
- `Direction` — `Back` or `Lay`
- `Entry` — recommended entry odds
- `Suggestion` — `BEST` for the chosen trade, otherwise `CONSIDER` or `IGNORE`
- `Note` — short justification (trend, liquidity, price action, correlation)
- `Correlation` — brief summary of candlestick price movement relationship between the selection and others (e.g., positive, negative, none)
- `Probability` — calculated probability to win (as a percentage, e.g., 23.5%) based on internal score, not odds/price
- `Win/Lose` — indicate if the horse is expected to win or lose based on the analysis

---

## Execute Strategy (automation instructions)

When executing the trade, the strategy must be executed automatically using `ExecuteStrategySettings`. The strategy name must be:
- For `Back` -> `Bet 10 Euro`
- For `Lay` -> `Lay 10 Euro`

The execution must be performed automatically for the best selection and direction, without requiring operator confirmation or manual intervention.

---

## Selection Criteria

| Criterion        | Back                                 | Lay                                  |
|------------------|--------------------------------------|--------------------------------------|
| Price trend      | Downward (odds shortening)           | Upward (odds drifting)               |
| Volume           | High (confirms market conviction)    | High (confirms market conviction)    |
| Support/resistance | Price clustering at lower odds     | Price clustering at higher odds      |
| Volatility       | Tight range for safer entry          | Tight range for safer entry          |
| Correlation      | Positive correlation with others (if all odds shorten, back); negative (if one shortens, others drift, lay) |

---

## Result Table Columns (required output structure)

| Selection | Odds | Volume | Direction | Entry | Suggestion | Note | Correlation | Probability | Win/Lose |
| :--- | ---: | ---: | --- | ---: | --- | --- | --- | ---: | --- |

- The table must contain exactly these columns and include one row flagged `BEST` in the `Suggestion` column.
- Do not include example data in the prompt; the analysis engine must populate the table from MCP inputs.

---
