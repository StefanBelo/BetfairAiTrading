# Automated Traded Prices EV Analysis Prompt

## 🤖 AUTOMATED EXECUTION INSTRUCTIONS

**THIS PROMPT MUST BE EXECUTED AUTOMATICALLY BY AI WITHOUT ASKING FOR CONFIRMATION**

### STEP-BY-STEP AUTOMATED EXECUTION:

1. **AUTOMATICALLY** get the active Betfair market using: `GetActiveMarket`
2. **AUTOMATICALLY** retrieve trading data using: `GetDataContextForMarket` with dataContextNames ["MarketSelectionsTradedPricesData", "RacingpostDataForHorses"]
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
    - Parameters: marketId from Step 1, dataContextNames ["MarketSelectionsTradedPricesData", "RacingpostDataForHorses"]
    - Parse the trading data for all selections

      **VERIFIED Data structure (updated from live data):**
      - Response root contains:
          - `market` (object):
             - `marketId` (string)
             - `startTime` (string, ISO 8601 with timezone, e.g. "2025-08-11T21:10:00+02:00")
             - `eventType` (string, e.g. "Horse Racing")
             - `eventName` (string, e.g. "Windsor")
             - `marketName` (string, e.g. "5f Hcap")
             - `status` (string, e.g. "Open")
          - `selectionsData` (array): items have
             - `selectionId` (string, e.g. "28751397_0.00")
             - `name` (string, e.g. "Four Adaay")
             - `data.tradedPricesData` (object):
                - `startPrice` (number) — opening price for this analysis window
                - `endPrice` (number) — latest/current price (use as Current Odds)
                - `minPrice` (number) — lowest price during period
                - `maxPrice` (number) — highest price during period
                - `tradedVolume` (number) — total matched volume for the selection
                - `backRatio` (number) — proportion of volume that was back bets (0.0-1.0)
                - `layRatio` (number) — proportion of volume that was lay bets (0.0-1.0)
             - `data.racingpostHorseData` (object, if available for Horse Racing):
                - `officialRating` (number) — official handicap rating
                - `rpRating` (number) — Racing Post rating
                - `lastRacesDescriptions` (array) — detailed race history with distances beaten, days since, positions, etc.

   **Critical parsing notes:**
   - Current Odds = `data.tradedPricesData.endPrice`
   - Opening Price = `data.tradedPricesData.startPrice`
   - BackRatio = `data.tradedPricesData.backRatio` (already calculated as decimal 0.0-1.0)
   - LayRatio = `data.tradedPricesData.layRatio` (already calculated as decimal 0.0-1.0)
   - Total Volume = `data.tradedPricesData.tradedVolume`
   - Back Volume = BackRatio × TotalVolume
   - Lay Volume = LayRatio × TotalVolume
   - Price Range = maxPrice - minPrice
   - Price Movement = (endPrice - startPrice) / startPrice * 100 (percentage change)

3. **STEP 3 - CALCULATE EV METRICS (EXECUTE IMMEDIATELY):**
   For each selection, calculate:
   - Current implied probability = 1 / decimal_odds
   - BackRatio = data.tradedPricesData.backRatio (direct field, 0.0-1.0)
   - LayRatio = data.tradedPricesData.layRatio (direct field, 0.0-1.0)
   - Back volume = BackRatio × tradedVolume
   - Lay volume = LayRatio × tradedVolume
   - Market sentiment score = BackRatio - LayRatio (range: -1.0 to +1.0)
   - Price momentum = (endPrice - startPrice) / startPrice * 100 (percentage change)
   - Price volatility = (maxPrice - minPrice) / endPrice * 100 (percentage volatility)
   - Volume-weighted sentiment = sentiment_score × log(1 + tradedVolume/1000) (adjust for volume significance)
   - Liquidity factor = min(1.0, tradedVolume / 5000) (normalize high volume impact)
   - Expected Value = sentiment_score × liquidity_factor - market_overround_adjustment
   - Risk-adjusted EV = EV × (1 - price_volatility/100) × confidence_score

   **Enhanced EV Calculation Formula:**
   ```
   Base_EV = (BackRatio - 0.5) × 2  // Convert to -1 to +1 scale
   Volume_Weight = log(1 + TradedVolume / 1000) / 5  // Logarithmic volume scaling
   Momentum_Factor = tanh(Price_Momentum / 10)  // Bounded momentum impact
   Volatility_Penalty = 1 - (Price_Volatility / 100)  // Reduce EV for high volatility
   
   Final_EV = Base_EV × Volume_Weight × Momentum_Factor × Volatility_Penalty
   ```

4. **STEP 4 - ANALYZE MARKET EFFICIENCY (EXECUTE IMMEDIATELY):**
   - Calculate total market overround
   - Identify price discrepancies
   - Assess liquidity depth per selection
   - Determine market confidence levels

5. **STEP 5 - GENERATE RESULTS TABLE (EXECUTE IMMEDIATELY):**
   Display results in this EXACT format:

   | Selection | Current Odds | Impl. Prob % | EV % | Form Analysis |
   |-----------|--------------|--------------|------|---------------|
   | [Auto-populate from data] | [endPrice] | [1/endPrice*100] | [calculated EV] | [Semantic form summary] |

   **Enhanced Analysis Columns:**
   - **Current Odds**: data.tradedPricesData.endPrice
   - **Impl. Prob %**: (1/endPrice) × 100, rounded to 1 decimal
   - **EV %**: Final calculated EV as percentage, 2 decimal places
   - **Form Analysis**: Semantic analysis of recent form from lastRacesDescriptions (last 3-5 races)
     - Extract key performance patterns from raceDescription text
     - Identify strengths: "strong finisher", "travels well", "good jumper"
     - Identify weaknesses: "weakened", "mistake prone", "hangs left/right"
     - Note recent wins/places and consistency patterns
     - Summarize in 2-3 concise phrases maximum

   **Additional Summary Tables:**

   **📊 MARKET OVERVIEW:**
   | Metric | Value |
   |--------|-------|
   | Total Market Volume | [sum of all volumes] |
   | Market Overround | [sum of implied probabilities - 100%] |
   | Most Backed | [highest backRatio selection] |
   | Most Laid | [highest layRatio selection] |
   | Price Movers | [biggest price changes] |

6. **STEP 6 - PROVIDE STRATEGIC RECOMMENDATIONS (EXECUTE IMMEDIATELY):**
   
   **For Horse Racing Markets, include Racing Post Analysis:**
   - Extract `data.racingpostHorseData.officialRating` and `rpRating`
   - Analyze recent form from `lastRacesDescriptions` (last 3-5 races)
   - **Semantic Form Analysis Process:**
     * Parse `raceDescription` text for performance indicators
     * Identify positive patterns: "led", "kept on", "ran on", "strong challenge", "going well"
     * Identify negative patterns: "weakened", "mistake", "unseated", "pulled up", "tailed off"
     * Note running style: "prominent", "held up", "towards rear", "made all"
     * Extract recent placings and beaten distances
     * Summarize strengths/weaknesses in concise phrases
   - Calculate form metrics: average beaten distance, consistency, class trends
   - Cross-reference ratings with current odds for value identification

   **Strategic Analysis:**
   - Identify TOP 3 positive EV opportunities with supporting evidence
   - List selections to AVOID (negative EV) with reasoning
   - Recommend optimal bet sizing using Kelly criterion: f* = (bp-q)/b where b=odds-1, p=implied_prob, q=1-p
   - Suggest entry/exit timing based on price momentum and volume trends
   - Provide risk management advice including stop-loss levels
   - Include form-based insights for horse racing (if applicable)

   **Risk Assessment Matrix:**
   | Selection | EV Score | Risk Level | Recommended Stake | Form Summary |
   |-----------|----------|------------|-------------------|--------------|
   | [Top picks] | [EV%] | [Low/Med/High] | [Kelly %] | [Key form points] |

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
PARAMETERS: marketId=[from Command 1], dataContextNames=["MarketSelectionsTradedPricesData", "RacingpostDataForHorses"]
STORE RESULT: Parse trading data and form data for comprehensive analysis
```

### Command 3: Process and Display Results
```
EXECUTE NOW: Calculate EV metrics and display results table immediately
NO CONFIRMATION NEEDED: Generate strategic recommendations automatically
```

**⚠️ CRITICAL: DO NOT WAIT FOR USER INPUT - EXECUTE ALL COMMANDS SEQUENTIALLY**


## 📋 EXPECTED AUTOMATED OUTPUT FORMAT

**THE AI WILL AUTOMATICALLY GENERATE THIS TABLE:**

| Selection         | Current Odds | Impl. Prob % | EV % | Form Analysis |
|-------------------|--------------|--------------|------|---------------|
| [AUTO-POPULATED]  | [AUTO-CALC]  | [AUTO-CALC]  | [AUTO-CALC] | [SEMANTIC FORM SUMMARY] |

**AUTOMATIC CALCULATIONS INCLUDE:**
- **Enhanced EV Formula**: Incorporates volume weighting, momentum factors, and volatility penalties
- **Risk-Adjusted Metrics**: Confidence scores based on data quality and market stability
- **Semantic Form Analysis**: AI-powered analysis of race descriptions to identify patterns
- **Market Efficiency**: Overround calculations and liquidity assessments

**AUTOMATED STRATEGIC SUMMARY:**
✅ **TOP VALUE OPPORTUNITIES** (Auto-identified highest positive EV with supporting analysis)
❌ **AVOID THESE SELECTIONS** (Auto-identified negative EV with risk factors)
📊 **MARKET INSIGHTS** (Auto-generated efficiency metrics and volume patterns)
💰 **RECOMMENDED ACTIONS** (Auto-calculated optimal strategies with Kelly criterion)
🏇 **FORM INSIGHTS** (AI-analyzed recent performance patterns from race descriptions)
⚠️ **RISK MANAGEMENT** (Auto-generated stop-loss and position sizing recommendations)


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
- Statistical confidence intervals for EV estimates based on volume and volatility
- Optimal stake sizing using Kelly criterion with risk-adjusted probabilities
- Risk-adjusted return projections with downside protection
- Market inefficiency exploitation strategies
- Semantic form analysis from race description narratives (horse racing)
- AI-powered pattern recognition from race commentary text
- Real-time sentiment analysis from back/lay ratios
- Liquidity depth assessment and volume trend analysis
- Price momentum indicators with volatility adjustments
- Cross-market efficiency comparisons
- Form-based value identification using natural language processing
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
