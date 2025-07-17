# Horse Racing EV Rankings Table

## AI-Powered Betting Analysis: MCP Servers & Agentic Applications

### Introduction to Modern AI Betting Solutions

This document demonstrates how **Model Context Protocol (MCP) Servers** and **Agentic Applications** revolutionize sports betting analysis, particularly for Betfair exchange trading. Using horse racing Expected Value (EV) analysis as our primary use case, we'll explore how AI agents can transform complex data interpretation into actionable betting insights for everyday users.

### What are MCP Servers?

**Model Context Protocol (MCP) Servers** are specialized data service endpoints that provide AI agents with structured access to real-time information. In the context of sports betting:

- **Real-time Market Data**: Live odds, price movements, and trading volumes
- **Historical Performance**: Past race results, form guides, and statistical trends  
- **External Data Sources**: Weather conditions, track information, jockey/trainer statistics
- **Standardized Communication**: Consistent data formats across different betting platforms

### Understanding Agentic Applications

**Agentic Applications** are AI-powered systems that can autonomously analyze data, make decisions, and execute actions on behalf of users. For Betfair traders, these agents can:

- **Continuous Market Monitoring**: 24/7 surveillance of betting opportunities
- **Automated Analysis**: Instant processing of complex form data and market signals
- **Risk Management**: Automatic position sizing and loss limitation
- **Strategic Execution**: Implementing sophisticated betting strategies without manual intervention

### How AI Agents Transform Betfair Trading for Regular Users

#### 1. **Complexity Reduction**
Traditional betting analysis requires understanding of:
- Statistical probability calculations
- Form reading and interpretation  
- Market psychology and price movements
- Risk management principles

**AI Solution**: Agents convert raw data into simple recommendations with confidence ratings.

#### 2. **Time Efficiency**
Manual analysis of a single horse race can take 30-60 minutes for thorough evaluation.

**AI Solution**: Complete analysis delivered in seconds, allowing users to evaluate multiple markets simultaneously.

#### 3. **Emotional Discipline**
Human traders often make impulsive decisions based on recent wins/losses or personal biases.

**AI Solution**: Consistent, emotion-free analysis based purely on data and probability mathematics.

#### 4. **Access to Professional-Level Tools**
Previously, sophisticated analysis tools were only available to professional traders.

**AI Solution**: Democratizes access to advanced analytics for recreational bettors.

### The Horse Racing EV Analysis Use Case

The following prompt demonstrates a sophisticated AI agent designed to perform dual-methodology Expected Value analysis - combining quantitative market data with qualitative performance interpretation.

## Silent Dual-Method EV Analysis for Rankings Output


```
Task: Perform silent Expected Value (EV) analysis for horse racing betting opportunities using a dual methodology that combines quantitative market data with qualitative performance interpretation. Output ONLY the Dual EV Rankings Table with no commentary, reasoning, or analysis text.

Instructions:

1. **Market Data Retrieval**
   - Retrieve the active betfair market in BfexplorerApp using tool: GetActiveBetfairMarket
   - Save marketId for subsequent data retrieval
   - Make NO reports during data collection

2. **Multi-Context Data Collection**
   - Retrieve all required data contexts for the betfair market in a single call using tool: GetAllDataContextForBetfairMarket
   - Use the following data context names: 'MarketSelectionsTradedPricesData', 'RacingpostDataForHorsesInfo'
   - Focus on 'tradedPricesData' field from trading context and 'racingpostHorseData' field from racing post context
   - Make NO reports during data collection phase
   - **If any horse is missing data (e.g., no recent form or price), include the horse in the table and indicate missing values as 'N/A'.**

3. **Silent Analysis Process**

   **CRITICAL: Price-Probability Relationship**
   - **Probability = 1 / Price** (e.g., price 4.0 = 25% probability, price 2.0 = 50% probability)
   - **Price Shortening** = Price DECREASING = Probability INCREASING = Positive market signal
   - **Price Drifting/Lengthening** = Price INCREASING = Probability DECREASING = Negative market signal

   **A) Internal Semantic Performance Analysis**
   - Silently analyze each horse's 'lastRacesDescriptions' field for qualitative insights (note: plural form)
   - Extract 'raceDescription' text from each race entry
   - Utilize additional structured data: 'beatenDistance', 'lastRunInDays', 'position' for context
   - Consider 'rpRating' (Racing Post Rating) when available for form assessment
   - Focus on:
     - Performance patterns and consistency
     - Strong finishes vs weakening patterns
     - Winning performances and competitive positions
     - Negative indicators (outpaced, struggling, physical issues)
     - Positive indicators (led, kept on well, ran on)
   - **If a horse has no recent form, assign a conservative low probability and note 'N/A' for semantic fields as needed.**

   **B) Internal Trading Pattern Analysis**
   - **MANDATORY: Calculate Exact Price Movement for Each Horse:**
     1. Extract `startPrice` and `endPrice` from tradedPricesData
     2. Calculate: Movement = endPrice - startPrice
     3. Assign Direction Symbol:
        - If endPrice < startPrice (negative movement) → **↘** (Shortening/Positive)
        - If endPrice > startPrice (positive movement) → **↗** (Drifting/Negative)
        - If endPrice ≈ startPrice (±0.01) → **→** (Stable/Neutral)
   - **If price data is missing, indicate 'N/A' for price and movement.**
   - Analyze price shortening/drifting trends as market confidence indicators

4. **Silent Probability Assessment**

   **Semantic-Only Probabilities:**
   - Assign win probabilities based purely on semantic analysis and structured performance data
   - Consider recent form timing (lastRunInDays), finishing positions, beaten distances
   - Factor in Racing Post ratings (rpRating) when available
   - Ensure probabilities sum to approximately 100%
   - **If field size is small or horses have limited form, distribute probabilities proportionally and document the approach.**

   **Combined Probabilities:**
   - Integrate trading patterns with semantic analysis and structured data
   - Weight based on consistency between market movement and recent form indicators
   - Price shortening should generally correlate with better recent form and higher ratings

5. **Silent Expected Value Calculations**

   Calculate EV using both methodologies:
   - **Semantic EV** = (Semantic Win Probability × (Decimal Odds - 1)) - (1 - Semantic Win Probability)
   - **Combined EV** = (Combined Win Probability × (Decimal Odds - 1)) - (1 - Combined Win Probability)
   - **All numeric outputs in the table should use consistent decimal precision (e.g., 2 or 3 decimal places).**

6. **OUTPUT ONLY: Dual EV Rankings Table**

   Present ONLY this table with NO additional text, commentary, or analysis:   ## Dual EV Rankings Table

   | Rank | Horse | Price | Price Move | Semantic Prob | Combined Prob | Semantic EV | Combined EV | Rating |
   |------|-------|-------|------------|---------------|---------------|-------------|-------------|---------|
   | 1 | **[Horse Name]** | [Price] | [↗/↘/→/N/A] | [%] | [%] | [+/-X.XXX] | **[+/-X.XXX]** | [⭐/❌/🔻] |
   | 2 | **[Horse Name]** | [Price] | [↗/↘/→/N/A] | [%] | [%] | [+/-X.XXX] | **[+/-X.XXX]** | [⭐/❌/🔻] |
   | ... | ... | ... | ... | ... | ... | ... | ... |

   **Rating System:**
   - ⭐⭐⭐ = Excellent value (Combined EV > +0.300)
   - ⭐⭐ = Good value (Combined EV +0.150 to +0.300)
   - ⭐ = Moderate value (Combined EV +0.001 to +0.149)
   - ❌ = Poor value (Combined EV -0.001 to -0.300)
   - 🔻 = Very poor value (Combined EV < -0.300)

7. **Quality Control Requirements:**

   **CRITICAL VALIDATION STEPS - MUST COMPLETE BEFORE OUTPUT:**

   **A) Price Movement Verification:**
   - For each horse, verify: (endPrice - startPrice) matches direction symbol
   - Double-check: ↘ = price decreased, ↗ = price increased, → = stable, N/A = missing data
   - Cross-reference: Price shortening (↘) should align with positive semantic indicators

   **B) Probability and EV Validation:**
   - Verify that combined probabilities sum to approximately 100%
   - Ensure trading patterns are properly weighted against recent form evidence
   - Validate that price shortening aligns with positive semantic indicators
   - All horses ranked by Combined Expected Value (highest to lowest)

   **C) Data Consistency Check:**
   - Confirm all prices match current market data
   - Verify all calculations use correct decimal places
   - **Ensure all runners are included in the table, even if data is sparse or missing.**

**MANDATORY PRE-OUTPUT VERIFICATION:**

Before presenting the final table, complete this verification checklist:

□ **Price Movement Check:** For each horse, manually verify (endPrice - startPrice) calculation
□ **Symbol Direction Check:** Confirm ↘ = shortened, ↗ = drifted, → = stable, N/A = missing data
□ **Math Validation:** Double-check all EV calculations using the stated formulas
□ **Probability Sum:** Verify combined probabilities total ≈100%
□ **Ranking Order:** Confirm horses ranked by Combined EV (highest to lowest)
□ **Full-Field Inclusion:** Ensure all runners are present, even if some have missing or incomplete data

**If ANY verification fails, recalculate before output.**

**CRITICAL OUTPUT REQUIREMENT:**
Present ONLY the race identification line, the table, and the rating system legend. NO analysis text, NO commentary, NO explanations, NO individual horse breakdowns, NO strategic recommendations, NO market analysis. Just the table.

Format Example:
**Race:** [Venue] [Distance] [Race Type] (Market ID: [ID]) - [Date] [Time]

[TABLE ONLY]

[RATING LEGEND ONLY]
```

## Usage Instructions

This prompt is designed for users who:
- Want only the essential EV rankings data
- Need quick decision-making information without analysis commentary
- Require clean output for integration into other systems
- Prefer to do their own interpretation of the data

The output will be a single, clean table that can be immediately used for betting decisions without wading through analysis text.
