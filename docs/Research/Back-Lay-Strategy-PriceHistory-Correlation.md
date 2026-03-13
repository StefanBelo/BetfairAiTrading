# Back/Lay Trading Strategy (Price History Correlation)

## Objective

Identify the best trade (back or lay) for the first and second favourite selections in any Betfair market, using price history and volume analysis. Analyze correlation in price movements between the two favourites to improve prediction of price movement for the first favourite. Apply stock market price action and correlation techniques if applicable.

---

## Analysis Prompt


Use this prompt on any active Betfair market:

```
On active market, for the two favourite selections, use GetDataContextForMarketSelection with data context "MarketSelectionsPriceHistoryData" for all two. Analyze price and volume history for each, and evaluate:
- Price trend (upward/downward, momentum)
- Volume spikes (liquidity, conviction)
- Support/resistance levels (price clustering)
- Volatility (price range, sudden moves)
- Correlation in price movements between the two favourites (e.g., do odds move together or inversely?)
- Price "true value" detection (volume + stability rule): require the analysis engine to wait for a small stabilization signal before treating early opening trades as the market price. Use both a cumulative volume threshold and short-term price stability checks (VWAP / spread / depth) to confirm the price reflects genuine liquidity rather than noise.
 
  Example defaults (tunable):
  - `AbsoluteMin` = 150 currency units matched on the runner since market open.
  - `RelPct` = 0.002 (0.2%) of market `TotalMatched`. Require cumulative matched >= `max(AbsoluteMin, RelPct * TotalMatched)`.
  - Stability window: last `W` trades or last `T` minutes (defaults: `W=50` trades or `T=3` minutes).
  - Stability thresholds: VWAP change over the window < `X%` (default `X=0.3%`), best back/lay spread <= 2 ticks, and depth at best price >= `DepthMin` (default `DepthMin=30` units).
  - Optional confirmation: require the stability condition hold for `N` consecutive checks (default `N=2`).
 
  Behavioural rule for the analysis engine:
  - Do not flag a runner as suitable for immediate execution unless the "true value" rule is satisfied. If not satisfied, report `WAIT` in the `Suggestion` column for that runner and include the reason (e.g., "insufficient matched volume", "VWAP unstable", or "thin depth").
  - When the rule is satisfied, include the cumulative matched, VWAP, spread and depth metrics used to reach the decision in the `Note` field.

Use correlation analysis to better predict price movement for the first favourite. Recommend the best trade direction (back or lay), entry odds, and target exit odds (3 ticks away) for the first favourite.

All analysis and execution must be performed automatically for the first favourite selection, without asking for confirmation or summary. Automatically execute the recommended strategy using the appropriate MCP call, without requiring operator confirmation or manual intervention.
```

---

## Required MCP Tool Calls

- `GetActiveMarket` — retrieve `marketId`, `TotalMatched`, `StartTime`, and selections.
- `GetDataContextForMarketSelection` with `dataContextNames: ["MarketSelectionsPriceHistoryData"]` — retrieve price history and volume for both favourite selections.

---

## Output Requirements — Markdown Result Table (MANDATORY)

Your analysis MUST produce a markdown table (renderable) as the primary result. The table must exactly include these columns and produce a single best suggestion row flagged `BEST`:

| Selection | Odds | Volume | Direction | Entry | Exit (3 ticks) | Suggestion | Note | Correlation |
|---|---:|---:|---|---:|---:|---|---|---|

- `Selection` — runner name
- `Odds` — current odds (numeric)
- `Volume` — recent total volume (numeric)
- `Direction` — `Back` or `Lay`
- `Entry` — recommended entry odds
- `Exit (3 ticks)` — recommended exit odds (3 ticks away)
- `Suggestion` — `BEST` for the chosen trade, otherwise `CONSIDER` or `IGNORE`
- `Note` — short justification (trend, liquidity, price action, correlation)
- `Correlation` — brief summary of price movement relationship between the two favourites (e.g., positive, negative, none)

---

## Execute Strategy (automation instructions)



When executing the trade, the strategy must be executed automatically using `ExecuteStrategySettingsWithParameters`. The JSON parameter block should set `OpenBetPosition.BetType` according to `Direction` and may include any other relevant parameters from the "Place Bet and Close Selection Bet Position" strategy template (such as odds, stake, stake type, entry/exit timing, or close position criteria) if required for correct execution.

**Important:**
- The strategy should set `OpenBetPosition.Odds` only when the analysis identifies a more optimal entry odds than the current market offer (e.g., based on support/resistance, volatility, or price action signals). If no better entry is found, omit this parameter and the strategy will use the current offered odds by default.
- When `OpenBetPosition.Odds` is set, you must also set `OpenBetPosition.PlaceBetInAllowedOddsRange` to `false` so the strategy waits for the specified odds.

- For `Back` -> `OpenBetPosition.BetType` = `Back`
- For `Lay` -> `OpenBetPosition.BetType` = `Lay`


If analysis determines additional parameters are needed (e.g., odds, stake, close criteria), set them in the JSON block referencing the template. The entry odds should be chosen wisely based on the analysis; do not always default to the current odds if a better entry is statistically supported by the data.

Provide a JSON parameter block in the output showing the exact parameter set to run.

Example MCP call (pseudocode):

```
ExecuteStrategySettingsWithParameters({
  "strategyName": "Trading Strategy",
  "marketId": "<marketId>",
  "selectionId": "<selectionId>",
  "parameters": {
    "OpenBetPosition.BetType": "Back",
    // Optionally include other parameters as needed, e.g.:
    // "OpenBetPosition.Odds": <entryOdds>,
    // "OpenBetPosition.Stake": <stake>,
    // "CloseBetPosition.ProfitLossType": "Ticks",
    // "CloseBetPosition.Profit": 3
    // **Important:** When using "OpenBetPosition.Odds", you must also set "OpenBetPosition.PlaceBetInAllowedOddsRange" to false.
  }
})
```

The execution must be performed automatically for the first favourite selection and direction, without requiring operator confirmation or manual intervention.

---

## Selection Criteria

| Criterion        | Back                                 | Lay                                  |
|------------------|--------------------------------------|--------------------------------------|
| Price trend      | Downward (odds shortening)           | Upward (odds drifting)               |
| Volume           | High (confirms market conviction)    | High (confirms market conviction)    |
| Support/resistance | Price clustering at lower odds     | Price clustering at higher odds      |
| Volatility       | Tight range for safer entry          | Tight range for safer entry          |
| Correlation      | Positive correlation with second fav (if both odds shorten, back); negative (if one shortens, one drifts, lay) |

---

## Result Table Columns (required output structure)

| Selection | Odds | Volume | Direction | Entry | Exit (3 ticks) | Suggestion | Note | Correlation |
| :--- | ---: | ---: | --- | ---: | ---: | --- | --- | --- |

- The table must contain exactly these columns and include one row flagged `BEST` in the `Suggestion` column.
- Do not include example data in the prompt; the analysis engine must populate the table from MCP inputs.

---
