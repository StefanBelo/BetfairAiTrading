# Horse Racing Strategy: Favourite-Only Betting (BSP) - Silent Mode

**Objective**: Execute betting strategy on favourite horse using BSP-based value criteria. Output only execution results.

**Logic**:
- Calculate BSP metrics: bspEdge = (1/betfairSP) - (1/price), bspEvNet = (1/betfairSP) × price - 1
- Calculate priceRatio = price / betfairSP  
- Find favourite (lowest price)
- BACK if: eVforPriceOrBetfairSP > 0 AND bspEvNet > -0.02
- LAY if: candidate criteria NOT met

**Tools**: `get_active_betfair_market` → `get_data_context_for_betfair_market` (dataContextName: "BetfairSpData") → `execute_bfexplorer_strategy_settings`

## Execution Command

```
1. Get active betfair market to obtain market ID
2. Use market ID to retrieve "BetfairSpData" context for that specific market
3. Calculate BSP metrics for favourite
4. Execute appropriate strategy (Bet 10 Euro/Lay 10 Euro) on the favourite
5. Report only execution result
```

## Output Format

**Strategy Execution Result:**
- **Market**: [Race Name/Time]
- **Favourite**: [Horse Name] (Price: [odds], BSP: [bsp])
- **Strategy Executed**: [Bet 10 Euro/Lay 10 Euro]
- **Selection ID**: [selectionId]
- **Reason**: [BACK criteria met/LAY criteria met/Missing data]
- **BSP Metrics**: Price Ratio [X.XX], BSP Edge [X.XXXX], BSP EV Net [X.XXXX]
- **Status**: [SUCCESS/FAILED/SKIPPED]
