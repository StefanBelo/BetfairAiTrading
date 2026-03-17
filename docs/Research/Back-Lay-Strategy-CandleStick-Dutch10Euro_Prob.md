# Dutching Strategy (Candlestick Data, All Selections) — V1: Probability-Driven Dutch for 10 Euro

## Objective

Automatically identify all selections in any Betfair market with a calculated probability to win of at least 15% (from internal model scores, not odds/price), and execute a Dutching strategy for 10 Euro across those selections. The analysis uses candlestick price history, volume, and correlation features to derive internal probabilities. All qualifying selections are included in the Dutch bet, and the strategy is executed automatically.

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
- Correlation in candlestick price movements between all selections
- Price "true value" detection (volume + stability rule): require the analysis engine to wait for a small stabilization signal before treating early opening trades as the market price. Use both a cumulative volume threshold and short-term price stability checks (VWAP / spread / depth) to confirm the price reflects genuine liquidity rather than noise.
- **Probability to Win Calculation (Internal Score):** For each selection, calculate an internal score based on the evaluated data features (trend, volume, volatility, support/resistance, correlation, etc). Convert these internal scores into a probability to win using a normalization method (e.g., softmax or min-max scaling across all selections). Do NOT use odds/price for this probability. The probability should reflect the model's confidence in each selection's chance to win based on the internal analysis only.

  Example defaults (tunable):
  - `AbsoluteMin` = 150 currency units matched on the runner since market open.
  - `RelPct` = 0.002 (0.2%) of market `TotalMatched`. Require cumulative matched >= `max(AbsoluteMin, RelPct * TotalMatched)`.
  - Stability window: last `W` candlesticks or last `T` minutes (defaults: `W=6` candlesticks or `T=30` minutes).
  - Stability thresholds: VWAP change over the window < `X%` (default `X=0.3%`), best back/lay spread <= 2 ticks, and depth at best price >= `DepthMin` (default `DepthMin=30` units).
  - Optional confirmation: require the stability condition hold for `N` consecutive checks (default `N=2`).

  Behavioural rule for the analysis engine:
  - Do not flag a runner as suitable for execution unless the "true value" rule is satisfied. If not satisfied, report `WAIT` in the `Suggestion` column for that runner and include the reason (e.g., "insufficient matched volume", "VWAP unstable", or "thin depth").
  - When the rule is satisfied, include the cumulative matched, VWAP, spread and depth metrics used to reach the decision in the `Note` field.

Select all runners with Probability >= 10% (from internal score, not odds/price) and include them in the Dutch bet.

Automatically execute the Dutching strategy for 10 Euro using the appropriate MCP call, without requiring operator confirmation or manual intervention.
```

---

## Required MCP Tool Calls

- `GetActiveMarket` — retrieve `marketId`, `TotalMatched`, `StartTime`, and selections.
- `GetAllDataContextForMarket` with `dataContextNames: ["MarketSelectionsCandleStickData"]` — retrieve candlestick price history and volume for all selections.
- `ExecuteStrategySettingsOnSelections` with `strategyName: "Dutch for 10 Euro"` and all qualifying selection IDs.

---

## Output Requirements — Markdown Result Table (MANDATORY)

Your analysis MUST produce a markdown table (renderable) as the primary result. The table must exactly include these columns and flag all Dutched selections with `DUTCH` in the `Suggestion` column:

| Selection | Odds | Volume | Suggestion | Note | Correlation | Probability | Win/Lose |
|---|---:|---:|---|---|---|---:|---|

- `Selection` — runner name
- `Odds` — current odds (numeric)
- `Volume` — recent total volume (numeric)
- `Suggestion` — `DUTCH` for included selections, otherwise `IGNORE` or `WAIT`
- `Note` — short justification (trend, liquidity, price action, correlation)
- `Correlation` — brief summary of candlestick price movement relationship between the selection and others (e.g., positive, negative, none)
- `Probability` — calculated probability to win (as a percentage, e.g., 23.5%) based on internal score, not odds/price
- `Win/Lose` — indicate if the horse is expected to win or lose based on the analysis

---

## Execution Instructions

- For all selections with Probability >= 10%, execute the strategy `Dutch for 10 Euro` using `ExecuteStrategySettingsOnSelections`.
- The execution must be performed automatically for all qualifying selections, without requiring operator confirmation or manual intervention.

---

## Selection Criteria

- Only include selections with Probability >= 10% (from internal score, not odds/price) in the Dutch bet.
- All other selections should be flagged as `IGNORE` or `WAIT` with a reason.

---

## Result Table Columns (required output structure)

| Selection | Odds | Volume | Suggestion | Note | Correlation | Probability | Win/Lose |
| :--- | ---: | ---: | --- | --- | --- | ---: | --- |

- The table must contain exactly these columns and flag all Dutched selections with `DUTCH` in the `Suggestion` column.
- Do not include example data in the prompt; the analysis engine must populate the table from MCP inputs.

---
