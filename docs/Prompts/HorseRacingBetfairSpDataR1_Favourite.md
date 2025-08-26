# Horse Racing Strategy: Favourite-Only Betting (BetfairSpData)

**CONFIGURATION**:
- TABLE_REPORT = FALSE  *(set to TRUE for detailed analysis table)*
- EVALUATION_METHOD = "SIMPLE"  *(SIMPLE/STRICT/RELAXED/ANY_POSITIVE)*

**OBJECTIVE**: Analyze all selections but bet ONLY on the favourite horse using BSP-based value criteria.

**RESEARCH FOUNDATION**: 
- Betfair Starting Price (BSP) provides superior accuracy and value compared to forecast prices
- BSP incorporates late market information and shows higher efficiency than traditional bookmaker odds
- BSP consistently averages 10-20% higher prices for mid-to-long odds horses
- Sweet spots exist in mid-range prices (12-18) where BSP shows minimal losses
- Research indicates BSP outperforms Industry Starting Price (ISP) in bettor returns

**EXECUTION RULE**: 
- Favourite qualifies as candidate → Back 10 Euro
- Favourite does NOT qualify → Lay 10 Euro  
- No other selections are bet regardless of their value

**CRITICAL**: Bet direction is determined EXCLUSIVELY by the Candidate flag computed below.

## EXECUTION STEPS

### Step 1: Get Market Data
- Call `GetActiveBetfairMarket()`
- Extract: marketId, selections[{selectionId, name, price}]
- Skip selections missing price or marked inactive

### Step 2: Get Model Data  
- Call `GetDataContextForBetfairMarket(dataContextName="BetfairSpData", marketId)`
- Extract for each selection: eVforPriceOrBetfairSP, industryStartingPrice, price, betfairSP
- Skip if industryStartingPrice ≤ 0 OR price ≤ 1
- **Note**: BetfairSP is the primary accuracy benchmark per research findings

### Step 3: Calculate Enhanced BSP Metrics
For each selection compute:
- Pf = 1/industryStartingPrice (forecast probability - less reliable per research)
- Pb = 1/betfairSP (BSP probability - more accurate benchmark)
- Pm = 1/price (current market probability)  
- bspEdge = Pb - Pm (BSP-based edge - preferred metric)
- forecastEdge = Pf - Pm (traditional edge - secondary metric)
- userEvNet = Pf × price - 1 (traditional EV)
- bspEvNet = Pb × price - 1 (BSP-based EV - more reliable)
- combinedEv = (bspEvNet + eVforPriceOrBetfairSP) / 2 (weighted toward BSP accuracy)
- kellyFraction = min(kellyRaw, 0.05, bspEdge × 2) where kellyRaw = max(0, (Pb × price - 1) / (price - 1))
- priceRatio = price / betfairSP (research shows 1.01-1.24 ratios yield best returns)

### Step 4: Determine Candidates (BSP-Enhanced Approach)
**PRIMARY APPROACH** (BSP-focused per research): Selection qualifies as candidate if:
- **eVforPriceOrBetfairSP > 0** AND **bspEvNet > -0.02** (BSP-based value with small tolerance)

**ENHANCED APPROACH** (Multi-factor BSP validation):
1. eVforPriceOrBetfairSP > 0 (model confirms BSP value)
2. bspEdge > 0.005 (positive BSP-based edge)
3. priceRatio between 1.01-1.24 (research-proven sweet spot)
4. For favorites (price < 3.0): additional bspEvNet > 0.01 (higher threshold for short odds)

*Alternative research-based approaches:*
```
// OPTION A: BSP Sweet Spot Focus (12-18 price range shows minimal losses)
1. If betfairSP between 12-18: relaxed thresholds (eVforPriceOrBetfairSP > -0.01)
2. Otherwise: standard thresholds (eVforPriceOrBetfairSP > 0)

// OPTION B: Price Band Optimization (based on research findings)
1. Evens-6/4 (1.0-2.5): bspEvNet > 0.02 (favorites often underbet)
2. 7/4-4/1 (2.75-5.0): bspEvNet > 0 
3. 9/2-8/1 (5.5-9.0): bspEvNet > -0.01 (slight tolerance)
4. 9/1+ (10.0+): eVforPriceOrBetfairSP > 0 (longshot bias consideration)

// OPTION C: Conservative BSP Approach
1. bspEvNet > 0 AND eVforPriceOrBetfairSP > 0 AND bspEdge > 0.01
```

### Step 5: Execute Strategy
1. **Identify favourite** = lowest currentPrice
2. **Check candidate status** from analysis above
3. **Execute bet**:
   - If favourite Candidate = Yes → `ExecuteBfexplorerStrategySettings(strategyName="Bet 10 Euro")`
   - If favourite Candidate = No → `ExecuteBfexplorerStrategySettings(strategyName="Lay 10 Euro")`

### Step 6: Output Results (Optional)
**TABLE_REPORT = TRUE** (set to control table generation)

**If TABLE_REPORT = TRUE:** Sort by combinedEv DESC, then output table with ALL selections:

| Rank | Horse Name | Current Price | Industry SP | BSP | Pm | Pf | Pb | BSP Edge | Forecast Edge | Price Ratio | Model EV | BSP EV Net | User EV Net | Combined EV | Kelly Fraction | Candidate |
|------|------------|---------------|-------------|-----|----|----|----| ---------|---------------|-------------|----------|------------|-------------|-------------|----------------|-----------|

**Format Rules:**
- Mark favourite with " (Favourite)" suffix
- Prices: 2 decimals | Probabilities/EVs: 4 decimals | Price Ratio: 3 decimals
- BSP Edge = Pb - Pm | Forecast Edge = Pf - Pm | Price Ratio = Current Price / BSP
- Candidate: "Yes" or "No" with reasoning if BSP-enhanced approach used
- Include ALL horses even if not candidates
- Highlight BSP-derived metrics as primary indicators per research

**If TABLE_REPORT = FALSE:** Only report:
- Market identification
- Favourite horse name, current price, and BSP
- BSP-based metrics (BSP Edge, Price Ratio, BSP EV Net)
- Candidate status (Yes/No) with primary reason
- Bet action taken

**Final Report Format:**
- If TABLE_REPORT = TRUE: Show full table + "Executed: [Back/Lay] 10 Euro on favourite [HorseName] | BSP Context: Price Ratio [X.XX], BSP Edge [X.XXXX]"
- If TABLE_REPORT = FALSE: "Market: [EventName - MarketName] | Favourite: [HorseName] ([Price] vs BSP [BSP_Price]) | Price Ratio: [X.XX] | BSP Edge: [X.XXXX] | Candidate: [Yes/No] | Action: [Back/Lay] 10 Euro"

## QUALITY ASSURANCE

**Validation Checklist:**
- [ ] Favourite correctly identified (lowest price)
- [ ] BSP-based candidate logic applied consistently (primary method per research)
- [ ] Price ratio calculated and considered (1.01-1.24 sweet spot)
- [ ] BSP edge evaluated as primary metric over forecast edge
- [ ] Bet direction matches candidate status
- [ ] Strategy name matches action (Back/Lay)

**Error Handling:**
- Missing BSP data → use forecast data with warning about reduced accuracy
- Missing data → mark selection as non-candidate
- API failures → abort execution with error message
- Price inconsistencies → use latest market price, note BSP comparison

**Research Notes:**
- BSP shows 10-20% higher prices for mid-to-long odds vs ISP
- Sweet spot at 12-18 odds shows minimal losses (-0.4% ROI)
- Price ratios 1.01-1.24 yield best returns historically
- BSP incorporates late market information, more accurate than early forecasts
- Commission impact: 2% vs 5% can turn marginal losses into gains

**Strategy Names:**
- Back: "Bet 10 Euro" 
- Lay: "Lay 10 Euro"

---
*Optimized for consistent execution with Bfexplorer MCP integration*
