# Horse Racing Optimal Betting Strategy Using BetfairSpData

## Step 1: Retrieve Active Market Data
Call `GetActiveMarket` (no parameters) to get the `marketId` and selections (horses) with their current prices.

## Step 2: Retrieve BetfairSpData
Call `GetDataContextForMarket` with:
- `dataContextNames`: "BetfairSpData"
- `marketId`: (from Step 1)

This returns, for each selection:
- `eVforPriceOrBetfairSP`: Expected value calculated by BetfairSpData model
- `industryStartingPrice`: Traditional bookmaker forecast price
- `price`: Current market price (from BetfairSpData context)
- `betfairSP`: Betfair Starting Price (predicted or actual)

## Step 3: Analyze BetfairSpData
For each horse, apply the following logic:
- **Value Betting:** Bet only when `eVforPriceOrBetfairSP > 0.05` (positive expected value with threshold).
- **Price Comparison:** Prefer horses where `industryStartingPrice < price` (market underestimates compared to forecast).
- **BSP Analysis:** Consider `betfairSP` vs `price` for additional value assessment.
- **Stake Sizing:** Use Kelly Criterion for stake calculation, capped at 5% of bankroll.
- **Diversification:** Avoid backing multiple horses unless `eVforPriceOrBetfairSP > 0.15` for more than one.

## Step 4: Calculate Your Own EV
For each horse, calculate your own EV using the following steps:
1. **Industry Forecast Probability** = 1 / industryStartingPrice
2. **BSP Probability** = 1 / betfairSP
3. **Market Probability** = 1 / price
4. **User Calculated EV (Industry)** = (Industry Forecast Probability × price) - 1
	- Example: If industryStartingPrice = 8, price = 5.6, then Industry Forecast Probability = 0.125, User Calculated EV = (0.125 × 5.6) - 1 = 0.7 - 1 = -0.3
5. **User Calculated EV (BSP)** = (BSP Probability × price) - 1
	- Example: If betfairSP = 6.5, price = 5.6, then BSP Probability = 0.154, User Calculated EV = (0.154 × 5.6) - 1 = 0.862 - 1 = -0.138
6. **Combined EV** = (User Calculated EV (BSP) + eVforPriceOrBetfairSP) / 2
	- Example: If User Calculated EV (BSP) = -0.138, eVforPriceOrBetfairSP = 0.084, then Combined EV = (-0.138 + 0.084) / 2 = -0.027

## Step 5: Present Results
For each race, present a table with the following columns:
- Horse Name
- Current Price
- Industry SP (industryStartingPrice)
- Betfair SP (betfairSP)
- Model EV (eVforPriceOrBetfairSP)
- User Calculated EV (Industry)
- User Calculated EV (BSP)
- Combined EV (average of User Calculated EV (BSP) and Model EV)

**Instructions:**
- Before presenting the table, sort all horses by Combined EV, highest to lowest. The best entry (highest Combined EV) must be at the top of the table.
- Display the table in this sorted order.
- Highlight horses with both positive Combined EV and positive Model EV as best bet candidates.
- Horses with high Combined EV but negative Model EV should be flagged for caution.
- Always use the formulas and calculation steps above for all future market analyses to ensure accuracy.

**Example Table Format:**
| Horse Name         | Current Price | Industry SP | Betfair SP | Model EV | Calc EV (Industry) | Calc EV (BSP) | Combined EV |
|--------------------|--------------|-------------|------------|----------|-------------------|---------------|-------------|
| [Horse 1]          | [Price]      | [Ind SP]    | [BSP]      | [EV]     | [Calc EV Ind]     | [Calc EV BSP] | [Comb EV]   |
| [Horse 2]          | [Price]      | [Ind SP]    | [BSP]      | [EV]     | [Calc EV Ind]     | [Calc EV BSP] | [Comb EV]   |
| ...                | ...          | ...         | ...        | ...      | ...               | ...           | ...         |

## Step 6: Execute Bet
If a horse has both positive Combined EV and positive Model EV (eVforPriceOrBetfairSP), execute a bet using the appropriate MCP tool (e.g., `ExecuteBfexplorerStrategySettings`) with:
- `marketId`: from Step 1
- `selectionId`: of the horse
- `strategyName`: e.g., "Bet 10 Euro"

If no horse meets criteria, do not execute a bet.

---
*This prompt is optimized for Bfexplorer MCP tools and uses real data structure for automated or manual betting strategies.*
