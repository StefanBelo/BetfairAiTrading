# Horse Racing Value Intelligence & Automated Strategy Execution

**Output to console only. Do not write to files or modify repository content.**

---

## Objective

Perform comprehensive value analysis on all runners in the active Betfair horse racing market, identify the single best selection based on composite scoring, and automatically execute the "Bet 10 Euro" strategy on that selection.

---

## 1) Retrieve Active Market and Context Data

### Step 1 — Get the Active Market
- **Call:** `get_active_market`
- **Expected Output:** 
  - `marketId` (required for subsequent calls)
  - Market metadata (event name, start time, market type)
  - List of selections with `selectionId`, horse names, and current odds

### Step 2 — Fetch Market Data Contexts (Results Include All Runners)
- **Call:** `get_all_data_context_for_market`
- **Parameters:**
  - `marketId`: from Step 1
  - `dataContextNames`: `["AtTheRacesDataForHorses", "RacingpostDataForHorses", "TimeformDataForHorses"]`
- **Expected Output:** Enriched data for each `selectionId` across the requested sources

### Error Handling: Data Context Availability

- The function call `get_all_data_context_for_market` expects dataContextNames that precisely match what Bfexplorer recognizes.
- Names like `"AtTheRacesDataForHorses"`, `"RacingpostDataForHorses"`, and `"TimeformDataForHorses"` must be exactly as registered in the tool's backend.
- If there’s a typo, mismatch (eg: `"RacingPostForHorses"` vs `"RacingpostDataForHorses"`), or a missing context, the API will either:
  - Return an error,
  - Deliver an incomplete data context dictionary (omitting the unknowns).

### Example:
- Send `["AtTheRacesDataForHorses", "RacingpostDataForHorses", "TimeformDataForHorses"]`
- If `"TimeformDataForHorses"` is not found, only the other two contexts are returned, and the AI is expected to note the gap (per your workflow).

**Handling Missing/Partial Data:**
- **If no active market:** Report status and stop
- **If contexts partially unavailable:**
  - Proceed with available sources
  - Adjust scoring weights proportionally (e.g., 50/50 if only 2 of 3 sources available)
  - Note missing sources in output table and rationale
- **If all contexts fail:** Report "Data unavailable, cannot proceed" and stop

---


## 2) Value-Driven Assessment

For each runner (by `selectionId`), calculate:

### Composite Score (0–100)
Normalize and aggregate ratings and recent form across all available sources:

**Data Sources:**
- **At The Races (ATR):** Star rating, expert view, rating score
- **Racing Post (RP):** Official rating, RP rating, recent form, topspeed figures
- **Timeform:** Rating stars, in-form flags, suitability indicators (going, course, distance)

**Scoring Adjustments:**
- **Upweight for:** Valid excuses (unseated, unsuitable ground, poor trip, interference), positive trainer/jockey changes, class upgrades
- **Downweight for:** Inconsistency, negative form trends, unsuitable conditions

### Contextual Value Flags
Identify and flag:
- **Hidden Class/Rebound Potential:** Runners with underlying ratings superior to recent performance
- **Suitability Indicators:** 
  - Going preference (soft, good, firm)
  - Distance suitability (optimal trip vs current race distance)
  - Course form and track bias
  - Pace setup advantages
- **Market Intelligence:** Notable market support or drift, backed vs laid volume patterns
- **Trainer/Jockey Factors:** In-form connections, course/distance specialists

### Market Comparison
Compare each runner's composite score and contextual flags to current Betfair odds:
- **Value Back:** Score significantly exceeds implied probability from odds
- **Lay Candidate:** Score well below implied probability
- **Each-Way/Place:** Strong place probability with favorable each-way terms

---

## 3) Strategy Table (Decision Grid)

Generate a comprehensive table for **ALL runners:**

| Horse | Score | Flags | Odds | Strategy | Stake Advice | Rationale/Counterpoint | Execute "Bet 10 Euro" |
|-------|-------|-------|------|----------|--------------|------------------------|-----------------------|
| ...   | ...   | ...   | ...  | ...      | ...          | ...                    | ...                   |

**Table Requirements:**
- **Score:** Composite score (0–100) based on available data sources
- **Flags:** Concise indicators (e.g., "Class↗, Form+, Going✔, Rebound?")
- **Odds:** Current Betfair back price
- **Strategy:** Recommended action (Back Win, Back Place, Lay, No Bet)
- **Stake Advice:** Suggested stake as % of bankroll (1.0–2.0% for high confidence)
- **Rationale/Counterpoint:** Brief justification plus vulnerability assessment
- **Execute:** Mark top selection with ✅

**Selection Criteria for Execution:**
1. Highest composite score among all runners
2. Tiebreaker: Better value vs odds (higher edge percentage)
3. Tiebreaker: Stronger contextual flags and lower vulnerability profile

---

## 4) Automated Execution (Single Best Pick)

After the decision table is produced:

1. **Identify Top Selection:**
   - Runner with highest composite score
   - Apply tiebreakers if scores are equal (within 1 point)

2. **Execute Strategy:**
   - **Call:** `execute_bfexplorer_strategy_settings`
   - **Parameters:**
     - `marketId`: from Step 1
     - `selectionId`: of top-scoring runner
     - `strategyName`: `"Bet 10 Euro"`

3. **Confirmation Output:**
   - Print: `"Executed 'Bet 10 Euro' on [HorseName] (selectionId: [ID], marketId: [ID])"`
   - If execution fails, report error details

**Error Handling:**
- **If execution fails:** Report error message, do not retry automatically, do not place alternative bets
- **If no qualifying selection:** Report "No selection meets execution criteria"

**Example Flow:**
```plaintext
get_active_market()
→ marketId: 1.249203494

get_all_data_context_for_market({
  marketId: "1.249203494",
  dataContextNames: ["AtTheRacesDataForHorses", "RacingpostDataForHorses", "TimeformDataForHorses"]
})
→ Data returned for all runners

[Generate decision table]
→ Top selection: Fill The Tank (Score: 86)

execute_bfexplorer_strategy_settings({
  marketId: "1.249203494",
  selectionId: "33363150_0.00",
  strategyName: "Bet 10 Euro"
})
→ Status: Success
```

---

## 5) Risk Controls & Live Monitoring

### Exposure Guidelines
- **Per-race cap:** Maximum 3% of bankroll exposure
- **Default stake:** 1.0–1.5% per selection for standard confidence
- **High confidence:** Up to 2.0% for exceptional value (Score >85, EV >15%)

### Trading Opportunities
- **Back and Trade Out:** For likely leaders/pace angles; target in-play price contraction >30% for green-up
- **Place Betting:** When place probability significantly exceeds place odds imply

### Vulnerability Assessment
Always provide for top selections:
- **Why they might underperform:** Specific concerns (fitness, trip suitability, pace scenario, market overreaction)
- **Alternative outcomes:** Scenarios where other runners could prevail
- **Late market signals:** Watch for sustained support/drift that would change the decision

### Adaptive Refinement
- Monitor market movements in final minutes before race
- Adjust execution if significant new information emerges (e.g., market favorite drifts 20%+)
- Note any late jockey changes, non-runners, or ground condition updates

---

## Example Output

### Market Overview
**Race:** Limerick 3m Hcap Hrd  
**Date:** 2025-10-19  
**Runners:** 11  
**Data Sources:** ATR ✓, Racing Post ✓, Timeform ✓

### Decision Table

| Horse              | Score | Flags                    | Odds | Strategy         | Stake | Rationale/Counterpoint                         | Execute |
|--------------------|-------|--------------------------|------|------------------|-------|-----------------------------------------------|---------|
| Fill The Tank      | 86    | Class↗, G/C/D✔, OR fair  | 7.4  | Back Win/Place   | 1.5%  | Strong suitability; pace setup ideal; risk: needs honest gallop | ✅ |
| Ballyglass Beauty  | 82    | Trip✔, RP 106 peak       | 8.0  | EW/Place         | 1.0%  | Stays well; recent mixed; needs pace to settle |         |
| Zolpharine         | 83    | ATR 105, Going✔          | 6.6  | Back Win         | 1.2%  | Returned to form LTO; minor distance doubt    |         |
| Kir                | 80    | TF⭐=4, Trainer+          | 4.1  | Small Back       | 0.8%  | Progressive but tight price; going query      |         |

### Execution Confirmation
```
✓ Executed 'Bet 10 Euro' on Fill The Tank
  - selectionId: 33363150_0.00
  - marketId: 1.249203494
  - Composite Score: 86/100
  - Current Odds: 7.4
  - Edge: ~15% (estimated true odds: 6.3)
```

### Risk Summary
- **Max exposure:** 1.5% of bankroll (€10 stake)
- **Vulnerability:** Needs honest pace and clear run; track bias unknown
- **Monitor:** Market support/drift in final 10 minutes
- **Alternative scenario:** Zolpharine may outperform if early tempo is strong

---

## Example with Partial Data (Missing Timeform)

### Market Overview
**Race:** Example Race  
**Data Sources:** ATR ✓, Racing Post ✓, Timeform ❌ (unavailable)

### Decision Table

| Horse     | Score | Flags            | Odds | Strategy | Stake | Rationale/Counterpoint                                  | Execute |
|-----------|-------|------------------|------|----------|-------|---------------------------------------------------------|---------|
| Fast Ace  | 84    | ATR+ RP+ (TF N/A)| 5.5  | Back Win | 1.2%  | Missing Timeform reduces confidence; value still evident | ✅      |

**Note:** Composite scoring adjusted to 50/50 weighting (ATR: 50%, RP: 50%) due to missing Timeform data.

---

**This prompt enables automated quantitative analysis and tool-triggered execution in Bfexplorer with comprehensive risk management and error handling.**
  - If only ATR and RP sources are supplied, the scoring is based on those only.
  - Flags/rationale note the absence of Timeform, for instance.
- The **strategy execution** step is still performed for the top runner, based on available data.
- If all sources fail (i.e., dataContextNames do not match anything valid), the function returns nothing useful—the agent should then report: **data context unavailable, cannot proceed.**

---

## Summary

- **Always use the correct, case-sensitive dataContextNames** recognized by your API/tool.
- You can discover valid names by:
  - Referring to your backend’s documentation,
  - Calling a “list available data contexts” tool if available in your API,
  - Inspect tool config or ask your tech support.

---

### Example Table Output With Missing Context

| Horse     | Score | Flags       | Odds | Strategy | Stake | Rationale/Counterpoint                    | Execute   |
|-----------|-------|-------------|------|----------|-------|-------------------------------------------|-----------|
| Fast Ace  | 84    | ATR+ RP+    | 5.5  | Back Win | 1.4%  | Missing Timeform: value potentially lower | ✅        |

---