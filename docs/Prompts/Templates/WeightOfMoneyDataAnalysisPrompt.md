# Betfair Market WeightOfMoneyData Analysis Prompt Template

**Prompt:**


For the currently active Betfair market, retrieve the data context `WeightOfMoneyData` using GetAllDataContextForMarket. Analyze all available data and report the results in a table. For each selection, include:

- Selection name
- Average back traded price
- Average back traded volume
- Average lay traded price
- Average lay traded volume
- Key findings from offeredPrices (price levels, liquidity, notable price gaps, support/resistance zones)
- Key findings from tradedPrices (price movement, traded volume at price points, momentum, volatility)
- Short description of the main findings (market support, price competitiveness, volume, notable outliers, and trading strategy implications)


**Instructions:**
- Summarize the key findings for the market, highlighting selections with strong support, competitive prices, or weak market interest.
- Analyze offeredPrices for each selection to identify price levels with significant liquidity, price gaps, and potential support/resistance zones relevant for trading decisions.
- Analyze tradedPrices for each selection to highlight price movement, traded volume at specific price points, momentum, and volatility that may inform trading strategies.
- Provide a concise summary of trading strategy implications for each selection based on the offered and traded price analysis (e.g., entry/exit points, price action signals, liquidity considerations).
- Ensure the table is clear and concise for quick decision-making.
- Use this prompt for any active market to get a structured overview of WeightOfMoneyData and actionable trading insights.

---

**Example Table Format:**

| Selection                | Avg Back Price | Avg Back Vol | Avg Lay Price | Avg Lay Vol | Short Description |
|--------------------------|---------------|--------------|---------------|-------------|------------------|
| [Selection Name]         | [value]       | [value]      | [value]       | [value]     | [summary]        |

---