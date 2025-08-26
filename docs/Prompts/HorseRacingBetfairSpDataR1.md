# Horse Racing Optimal Betting Strategy (BetfairSpData) — AI Agent Optimized

Objective: Select and size value bets using Betfair market + BetfairSpData + model-derived probabilities with transparent metrics and deterministic logic.

## Output Options
Configure the desired output format by specifying one or both options:
- **generateTable**: true (default: true) - Generate human-readable table report
- **generateJSON**: false (default: true) - Generate machine-readable JSON output

If both are false, only provide the textual analysis and candidate recommendations.

## Research Context: BSP vs Forecast Price Value
This strategy leverages research showing that Betfair Starting Price (BSP) often provides better value than Industry Starting Price (ISP) and forecast prices:
- **BSP Advantage**: BSP typically averages 10-20% higher than ISP for mid-to-long odds horses, incorporating late market information and crowd-sourced probability assessments
- **Value Sweet Spots**: Mid-range prices (12.01-18.00) show optimal BSP performance with ROI approaching breakeven (-0.4%), while extreme favorites and longshots exhibit bias
- **Forecast Limitations**: Early forecast prices may overestimate favorites and underestimate outsiders, missing late market movements that BSP captures
- **Efficiency Edge**: BSP demonstrates superior forecasting accuracy over traditional bookmaker odds, with higher informational efficiency and reduced bias in implied probabilities

**Strategic Implication**: When forecast prices exceed eventual BSP, this indicates potential value opportunities. The strategy prioritizes selections where industryStartingPrice > expected BSP or current market price suggests favorable positioning.

## Step 1: Retrieve Active Market
Call `GetActiveBetfairMarket` (no parameters). Extract:
- marketId
- selections: [{ selectionId, name, price (current best back) }]
Ignore any selection missing price or marked inactive.

## Step 2: Enrich With BetfairSpData
Call `GetDataContextForBetfairMarket` with:
- dataContextName = "BetfairSpData"
- marketId (from Step 1)

For each selection obtain (if available):
- eVforPriceOrBetfairSP (originalEv): original expected value metric (source model)
- industryStartingPrice: traditional bookmaker forecast price
- price (may duplicate current market price; prefer latest from Step 1 if different)
- betfairSP: Betfair Starting Price (predicted or actual)

**BSP Analysis Enhancement**: If betfairSP is available:
- bspValueRatio = industryStartingPrice / betfairSP (>1.0 indicates forecast premium over BSP)
- bspEdge = (industryStartingPrice - betfairSP) / betfairSP (percentage advantage of forecast over BSP)
- Use these metrics to identify value opportunities based on BSP research

Skip any selection where industryStartingPrice <= 0 OR current price <= 1.

## Step 3: Derived Metrics (per selection)
Let:
- currentPrice = latest price
- industryStartingPrice = traditional bookmaker forecast price
- originalEv = eVforPriceOrBetfairSP
- Pf = forecastProbability = 1 / industryStartingPrice
- Pm = marketProbability = 1 / currentPrice
- edgeProbability = Pf - Pm                (positive => model more optimistic than market)
- valueMultiple = currentPrice / industryStartingPrice
- userEvNet = Pf * (currentPrice - 1) - (1 - Pf)   (expected net return per 1 unit stake)
	(Equivalent: userEvNet = Pf * currentPrice - 1)
- priceEdgePercent = (currentPrice - industryStartingPrice) / industryStartingPrice

**BSP-Enhanced Metrics** (if betfairSP available):
- bspValueRatio = industryStartingPrice / betfairSP
- bspEdge = (industryStartingPrice - betfairSP) / betfairSP
- bspEfficiencyScore = bspValueRatio * edgeProbability (combines BSP research with probability edge)
- priceRangeBonus = 1.0 (default), increased to 1.15 if currentPrice in optimal range 12.01-18.00 based on research

- kellyFractionRaw = ( (currentPrice - 1) * Pf - (1 - Pf) ) / (currentPrice - 1)  (only if numerator > 0 else 0)
- kellyFraction = min( kellyFractionRaw, maxKelly, capKellyByEdge )
	- maxKelly = 0.05 (5% bankroll hard cap)
	- capKellyByEdge = edgeProbability * 2 (additional prudence; if undefined use maxKelly)
- combinedEv (weighted) = (wUser * userEvNetNorm + wOriginal * originalEvNorm + wBsp * bspEfficiencyScore) / (wUser + wOriginal + wBsp)
	- Default weights: wUser = 1, wOriginal = 1, wBsp = 0.5 (BSP factor as supporting evidence)
	- If bspEfficiencyScore unavailable, use original formula with wUser = 1, wOriginal = 1
	- Normalisation: userEvNetNorm = userEvNet; originalEvNorm = originalEv; bspEfficiencyScore used as-is
	- Override weights only if specified externally; otherwise keep defaults.

## Step 4: Qualification Rules
Mark a selection as a candidate ONLY if:
1. userEvNet > 0
2. edgeProbability > minEdge (default 0.01)
3. combinedEv > 0
4. kellyFraction > 0

**BSP-Enhanced Qualification** (additional criteria if BSP data available):
5. bspValueRatio > 1.05 (forecast price at least 5% above BSP, indicating potential value)
6. For optimal price range 12.01-18.00: reduce minEdge to 0.005 (research shows better value in this range)

Reject if any of:
- originalEv < 0 AND userEvNet <= 0
- industryStartingPrice < 1 OR currentPrice <= 1
- Data missing required fields
- **BSP Red Flags**: bspValueRatio < 0.95 (forecast significantly below BSP suggests overvaluation)

## Step 5: Output Generation
Compute all metrics, then SORT by combinedEv DESC (tie-breakers: higher userEvNet, then higher edgeProbability, then lower marketProbability).

INCLUDE ALL SELECTIONS (horses) in any output, not only candidates. Non‑qualifiers keep their computed metrics where possible and show `Candidate = No`. (Rationale: retaining full field context aids later analysis, auditing, and model recalibration.)

### Table Output (if generateTable = true)
Generate human-readable table with the following columns (in order):
1. Rank
2. Horse Name
3. Current Price
4. Industry Starting Price
5. Betfair SP (if available)
6. Market Probability (Pm)
7. Forecast Probability (Pf)
8. Edge Probability (Pf - Pm)
9. Value Multiple (currentPrice / industryStartingPrice)
10. BSP Value Ratio (industryStartingPrice / betfairSP, if available)
11. BSP Edge % (if available)
12. Original EV
13. User EV Net
14. Combined EV
15. Kelly Fraction (proposed stake %)
16. Price Range Bonus (1.0 or 1.15 for optimal range)
17. Candidate (Yes/No)

Rounding:
- Prices: 2 decimals (retain higher precision internally)
- Probabilities & fractions: 4 decimals
- EV metrics: 4 decimals

### JSON Output (if generateJSON = true)
Produce machine-readable JSON after the table (if both enabled) with schema:
{
	"marketId": string,
	"selections": [
		{
			"selectionId": string,
			"name": string,
			"currentPrice": number,
			"industryStartingPrice": number,
			"betfairSP": number | null,
			"marketProbability": number,
			"forecastProbability": number,
			"edgeProbability": number,
			"valueMultiple": number,
			"bspValueRatio": number | null,
			"bspEdge": number | null,
			"bspEfficiencyScore": number | null,
			"originalEv": number,
			"userEvNet": number,
			"combinedEv": number,
			"kellyFraction": number,
			"priceRangeBonus": number,
			"candidate": boolean
		}
	]
}

## Step 6: Bet Execution Rule

**Strategy Execution Rule (Updated):**
Only execute the strategy on selections that meet ALL candidate qualification rules:
1. userEvNet > 0
2. edgeProbability > minEdge (default 0.01)
3. combinedEv > 0
4. kellyFraction > 0

If at least one candidate exists, choose the top-ranked candidate (or top N if a portfolio rule is externally specified). For each candidate:
- Double-check that the selection is marked as Candidate = Yes in the output table and JSON.
- Execute `ExecuteBfexplorerStrategySettings` with parameters:
	- marketId
	- selectionId
	- strategyName (e.g., "Bet 10 Euro" or one derived from stake sizing logic)

Stake sizing (if dynamic): stake = bankroll * kellyFraction (round to 2 decimals, enforce minStake if defined, and never exceed bankroll * maxKelly).

If no candidates qualify, output: "No value bets — no action taken." Do not execute any strategy call.

## Notes & Justification
- **BSP Research Integration**: Strategy now incorporates findings that BSP provides superior value to forecast prices, especially for mid-range odds (12.01-18.00)
- **Enhanced Value Detection**: BSP metrics help identify when forecast prices offer genuine value vs. market inefficiencies
- **Price Range Optimization**: Reduced minimum edge requirements for the 12.01-18.00 range where research shows optimal BSP performance
- Using both probabilities (model vs market) separates price edge from payout structure
- userEvNet formula Pf * currentPrice - 1 is algebraically simpler and identical to earlier form
- combinedEv now includes BSP efficiency score to leverage crowd-sourced probability assessments
- Kelly is throttled by both a hard cap and edge-based damping to reduce variance
- Sorting + deterministic tie-breakers ensures reproducible output for automation
- **Research Validation**: Strategy can be backtested against actual BSP outcomes to verify forecast price value assumptions

## Failure / Edge Handling
- If BetfairSpData missing for a selection, skip its advanced metrics; optionally list under "omitted".
- If all selections skipped, clearly state reason (e.g., "All industryStartingPrice invalid").
- Ensure no division by zero (guard industryStartingPrice > 0, currentPrice > 1).

## Example Output Formats

### Example Minimal Human Table (generateTable = true, columns trimmed)
| Rank | Horse | Curr | InduSP | BSP  | Pf   | Pm   | Edge | ValMult | BSPRatio | OrigEV | UserEV | CombEV | Kelly | Bonus | Cand |
|------|-------|------|--------|------|------|------|------|---------|----------|--------|--------|--------|-------|-------|------|
| 1    | X     | 7.20 | 5.8    | 5.2  | 0.172| 0.139| 0.033| 1.24    | 1.12     | 0.62   | 0.18   | 0.45   | 0.025 | 1.0   | Yes  |
| 2    | Y     | 15.0 | 12.5   | 13.8 | 0.080| 0.067| 0.013| 1.20    | 0.91     | 0.15   | 0.12   | 0.09   | 0.010 | 1.15  | No   |

*Note: BSP ratio <1.0 for Horse Y indicates forecast below BSP (potential overvaluation), contributing to rejection*

### Example Summary (when generateTable = false, generateJSON = false)
**Analysis Summary:**
- Market analyzed: [Market Name]
- Total selections: X
- Qualified candidates: Y
- Top candidate: [Horse Name] with Combined EV: Z
- Recommended action: [Execute strategy / No action]

---
*Optimized for Bfexplorer MCP tool orchestration (deterministic, machine-parsable, extensible).* 
