# Horse Racing Optimal Betting Strategy (BetfairSpData) — AI Agent Optimized

**Objective**: Select and size value bets using Betfair market data, BetfairSpData, and model probabilities with deterministic logic.

## Output Configuration
- **generateTable**: true (default) - Human-readable table
- **generateJSON**: true (default) - Machine-readable JSON

## Strategy Foundation
Leverages research showing BSP provides superior value to forecast prices, especially for mid-range odds (12.01-18.00) with ~10-20% premium over ISP. Strategy identifies value when forecast prices exceed expected BSP.

## Execution Steps

### Step 1: Get Active Market
Call `GetActiveMarket()` → Extract marketId and selections [selectionId, name, price]. Skip inactive selections.

### Step 2: Enrich with BetfairSpData  
Call `GetDataContextForMarket(dataContextName="BetfairSpData", marketId)` → Extract per selection:
- eVforPriceOrBetfairSP (originalEv)
- industryStartingPrice (forecast price)
- betfairSP (if available)

Skip selections where industryStartingPrice ≤ 0 OR currentPrice ≤ 1.

### Step 3: Calculate Metrics
For each valid selection:

**Core Metrics:**
- Pf = 1 / industryStartingPrice (forecast probability)
- Pm = 1 / currentPrice (market probability)  
- edgeProbability = Pf - Pm
- userEvNet = Pf × currentPrice - 1
- valueMultiple = currentPrice / industryStartingPrice

**BSP Enhanced (if available):**
- bspValueRatio = industryStartingPrice / betfairSP
- bspEdge = (industryStartingPrice - betfairSP) / betfairSP
- bspEfficiencyScore = bspValueRatio × edgeProbability
- priceRangeBonus = 1.15 if currentPrice ∈ [12.01, 18.00], else 1.0

**Kelly & Combined:**
- kellyFractionRaw = max(0, ((currentPrice-1) × Pf - (1-Pf)) / (currentPrice-1))
- kellyFraction = min(kellyFractionRaw, 0.05, edgeProbability × 2)
- combinedEv = (userEvNet + originalEv + 0.5 × bspEfficiencyScore) / 2.5

### Step 4: Qualification & Ranking
**Candidate Requirements (ALL must be true):**
1. userEvNet > 0
2. edgeProbability > 0.01 (reduced to 0.005 for prices 12.01-18.00)
3. combinedEv > 0  
4. kellyFraction > 0
5. bspValueRatio > 1.05 (if BSP available)

**Reject if:** originalEv < 0 AND userEvNet ≤ 0, OR bspValueRatio < 0.95

**Sort by:** combinedEv DESC, then userEvNet DESC, then edgeProbability DESC.

### Step 5: Output Generation
Include ALL selections in output. Mark non-qualifiers as `candidate: false`.

**JSON Schema:**
```json
{
  "marketId": "string",
  "selections": [{
    "selectionId": "string", "name": "string",
    "currentPrice": 0.00, "industryStartingPrice": 0.00, "betfairSP": 0.00,
    "marketProbability": 0.0000, "forecastProbability": 0.0000, "edgeProbability": 0.0000,
    "valueMultiple": 0.00, "bspValueRatio": 0.00, "bspEdge": 0.00,
    "originalEv": 0.0000, "userEvNet": 0.0000, "combinedEv": 0.0000,
    "kellyFraction": 0.0000, "priceRangeBonus": 0.00, "candidate": false
  }]
}
```

### Step 6: Strategy Execution
If qualified candidates exist, execute on top-ranked candidate:
`ExecuteBfexplorerStrategySettings(marketId, selectionId, strategyName)`

**Stake**: bankroll × kellyFraction (min 2 decimals, ≤ bankroll × 0.05)

If no candidates: "No value bets — no action taken."

## Error Handling
- Missing BetfairSpData: Skip BSP metrics, use core metrics only
- Invalid prices (≤0): Skip selection entirely  
- Missing required fields: Mark as non-candidate
- Zero division: Guard all calculations

## Key Features
- **BSP Integration**: Leverages crowd-sourced probability assessments
- **Price Range Optimization**: Enhanced criteria for 12.01-18.00 range
- **Risk Management**: Kelly criterion with conservative caps
- **Deterministic**: Reproducible results for automation
- **Comprehensive**: Includes all selections for analysis context

---
*Optimized for Bfexplorer MCP tool orchestration* 
