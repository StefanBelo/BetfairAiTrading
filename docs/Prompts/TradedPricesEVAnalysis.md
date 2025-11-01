# Automated Traded Prices EV Analysis Prompt

## 🤖 AUTOMATED EXECUTION INSTRUCTIONS

**THIS PROMPT MUST BE EXECUTED AUTOMATICALLY BY AI WITHOUT ASKING FOR CONFIRMATION**

### STEP-BY-STEP AUTOMATED EXECUTION:

1. **AUTOMATICALLY** get the active Betfair market using: `GetActiveMarket`
2. **AUTOMATICALLY** retrieve trading data using: `GetDataContextForMarket` with dataContextName "MarketSelectionsTradedPricesData"
3. **AUTOMATICALLY** perform EV analysis on the retrieved data
4. **AUTOMATICALLY** format and display results in the specified table format
5. **AUTOMATICALLY** provide strategic recommendations

**DO NOT ASK FOR USER INPUT - EXECUTE ALL STEPS IMMEDIATELY**

---

## 📊 Complete EV Analysis Execution Prompt

```
EXECUTE THIS AUTOMATED ANALYSIS NOW:

1. **STEP 1 - GET ACTIVE MARKET (EXECUTE IMMEDIATELY):**
   - Use GetActiveMarket tool
   - Extract marketId from response

2. **STEP 2 - GET TRADING DATA (EXECUTE IMMEDIATELY):**
    - Use GetDataContextForMarket tool
    - Parameters: marketId from Step 1, dataContextName "MarketSelectionsTradedPricesData"
    - Parse the trading data for all selections

      **ACTUAL Data structure (verified from live data):**
      - Response root contains:
          - `market` (object):
             - `marketId` (string)
             - `startTime` (string, ISO 8601 with timezone, e.g. 2025-08-11T18:50:00+02:00)
             - `eventType` (string, e.g. "Horse Racing")
             - `eventName` (string, e.g. venue name)
             - `marketName` (string, e.g. race name)
             - `status` (string, e.g. "Open")
          - `selectionsData` (array): items have
             - `selectionId` (string)
             - `name` (string)
             - `data.tradedPricesData` (object):
                - `startPrice` (number) — opening price for this analysis window
                - `endPrice` (number) — latest/current price
                - `minPrice` (number)
                - `maxPrice` (number)
                - `tradedVolume` (number) — total matched volume for the selection
                - `backRatio` (number) — proportion of volume that was back bets (0.0-1.0)
                - `layRatio` (number) — proportion of volume that was lay bets (0.0-1.0)

   **Parsing notes:**
   - Current Odds = `data.tradedPricesData.endPrice`
   - Opening Price = `data.tradedPricesData.startPrice`
   - BackRatio = `data.tradedPricesData.backRatio` (already calculated, 0.0-1.0)
   - LayRatio = `data.tradedPricesData.layRatio` (already calculated, 0.0-1.0)
   - Total Volume = `data.tradedPricesData.tradedVolume`
   - Back Volume = BackRatio × TotalVolume
   - Lay Volume = LayRatio × TotalVolume

3. **STEP 3 - CALCULATE EV METRICS (EXECUTE IMMEDIATELY):**
   For each selection, calculate:
   - Current implied probability = 1 / decimal_odds
   - BackRatio = data.tradedPricesData.backRatio (direct field, 0.0-1.0)
   - LayRatio = data.tradedPricesData.layRatio (direct field, 0.0-1.0)
   - Back volume = BackRatio × tradedVolume
   - Lay volume = LayRatio × tradedVolume
   - Market sentiment score = (back_volume - lay_volume) / tradedVolume = BackRatio - LayRatio
   - Price momentum = (endPrice - startPrice) / startPrice
   - Expected Value = (BackRatio × (1/current_odds)) - market_efficiency_factor
   - Risk-adjusted EV = EV × liquidity_factor × confidence_score

4. **STEP 4 - ANALYZE MARKET EFFICIENCY (EXECUTE IMMEDIATELY):**
   - Calculate total market overround
   - Identify price discrepancies
   - Assess liquidity depth per selection
   - Determine market confidence levels

5. **STEP 5 - GENERATE RESULTS TABLE (EXECUTE IMMEDIATELY):**
   Display results in this EXACT format:

   | Selection | Current Odds | Impl. Prob % | Back/Lay Ratio | EV % | Value Rating | Price Trend | Liquidity Score |
   |-----------|--------------|--------------|----------------|------|--------------|-------------|-----------------|
   | [Auto-populate from data] | [Auto-calculate] | [Auto-calculate] | [Auto-calculate] | [Auto-calculate] | [Auto-determine] | [Auto-analyze] | [Auto-assess] |

6. **STEP 6 - PROVIDE STRATEGIC RECOMMENDATIONS (EXECUTE IMMEDIATELY):**
   - Identify TOP 3 positive EV opportunities
   - List selections to AVOID (negative EV)
   - Recommend optimal bet sizing using Kelly criterion
   - Suggest entry/exit timing based on price momentum
   - Provide risk management advice

**EXECUTE ALL STEPS ABOVE AUTOMATICALLY - NO USER INTERACTION REQUIRED**
```


## ⚡ AUTOMATED EXECUTION SEQUENCE

**AI AGENT: EXECUTE THESE COMMANDS AUTOMATICALLY IN ORDER**

### Command 1: Get Market Data
```
EXECUTE NOW: Use BfexplorerApp MCP tool: GetActiveMarket
STORE RESULT: Extract marketId for next step
```

### Command 2: Get Trading Context
```
EXECUTE NOW: Use BfexplorerApp MCP tool: GetDataContextForMarket 
PARAMETERS: marketId=[from Command 1], dataContextName="MarketSelectionsTradedPricesData"
STORE RESULT: Parse trading data for analysis
```

### Command 3: Process and Display Results
```
EXECUTE NOW: Calculate EV metrics and display results table immediately
NO CONFIRMATION NEEDED: Generate strategic recommendations automatically
```

**⚠️ CRITICAL: DO NOT WAIT FOR USER INPUT - EXECUTE ALL COMMANDS SEQUENTIALLY**


## 📋 EXPECTED AUTOMATED OUTPUT FORMAT

**THE AI WILL AUTOMATICALLY GENERATE THIS TABLE:**

| Selection         | Current Odds | Impl. Prob % | Back/Lay Ratio | EV % | Value Rating | Price Trend | Liquidity |
|-------------------|--------------|--------------|----------------|------|--------------|-------------|-----------|
| [AUTO-POPULATED]  | [AUTO-CALC]  | [AUTO-CALC]  | [AUTO-CALC]    | [AUTO-CALC] | [AUTO-DETERMINED] | [AUTO-ANALYZED] | [AUTO-ASSESSED] |

**AUTOMATIC CALCULATIONS INCLUDE:**

**AUTOMATED STRATEGIC SUMMARY:**
✅ **TOP VALUE OPPORTUNITIES** (Auto-identified highest positive EV)
❌ **AVOID THESE SELECTIONS** (Auto-identified negative EV)  
📊 **MARKET INSIGHTS** (Auto-generated efficiency metrics)
💰 **RECOMMENDED ACTIONS** (Auto-calculated optimal strategies)


## 🔧 AUTOMATED CUSTOMIZATION OPTIONS

**THE AI CAN AUTOMATICALLY ADJUST ANALYSIS FOR:**
- **Price Range Focus**: Auto-identifies favorites vs outsiders patterns
---

## 💡 AUTOMATED PRO FEATURES

**THE AI AUTOMATICALLY:**
1. **Gets fresh data** - Always uses latest market state
2. **Considers market timing** - Adjusts EV calculations based on time to start
3. **Factors market type** - Applies sport-specific analysis patterns  
4. **Monitors volume trends** - Identifies increasing volume as value indicator
5. **Tracks price movements** - Detects sustained firming/drifting significance

---

## 🚀 ADVANCED AUTOMATED ANALYSIS

**FOR COMPREHENSIVE ANALYSIS, THE AI AUTOMATICALLY INCLUDES:**

```
AUTOMATICALLY CALCULATED:
- Statistical confidence intervals for EV estimates
- Optimal stake sizing using Kelly criterion  
- Risk-adjusted return projections
- Market inefficiency exploitation strategies
- Historical pattern recognition
- Real-time sentiment analysis
- Liquidity depth assessment
- Price momentum indicators
```

**⚡ EXECUTION COMMAND FOR AI:**
```
EXECUTE ALL ANALYSIS STEPS NOW - NO USER CONFIRMATION REQUIRED
```

---

## 🎯 FINAL AUTOMATION REMINDER

**THIS PROMPT IS DESIGNED FOR COMPLETE AUTOMATION:**
- ✅ No user interaction needed
- ✅ All data retrieval automated  
- ✅ All calculations performed automatically
- ✅ Results formatted and displayed automatically
- ✅ Strategic recommendations generated automatically

**AI AGENT: EXECUTE IMMEDIATELY UPON RECEIVING THIS PROMPT**
