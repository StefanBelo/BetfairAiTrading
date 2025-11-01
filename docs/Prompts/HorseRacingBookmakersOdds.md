# Horse Racing Bookmakers Odds Analysis - Favourite Value Strategy

**SILENT MODE:** Executes strategies on the market favourite only, based on value analysis and field threat assessment. All analysis performed internally with performance tracking.

## 1. Data Retrieval & Validation Framework

1. **Get Market Data**: Use `GetActiveMarket` for `marketId` and selections with odds
2. **Get Horse Data**: Use `GetDataContextForMarket` with `dataContextName: "AtTheRacesBookmakersOdds"`
3. **Data Validation**: Ensure data completeness across all horses before proceeding

### Data Sources Summary:
**AtTheRaces:** `bookmakersOdds` (array of decimal odds from multiple bookmakers)

## 2. Bookmakers Odds Analysis & Value Detection Framework

### Value Analysis Methodology:
**Analyze the `bookmakersOdds` array to identify value opportunities by comparing bookmaker consensus against Betfair market odds:**

**VALUE DETECTION ALGORITHM:**
- Calculate bookmaker average: Sum of all bookmaker odds / number of bookmakers
- Calculate bookmaker minimum: Lowest odds in the array
- Calculate bookmaker maximum: Highest odds in the array
- Calculate odds dispersion: (max - min) / average * 100 (percentage)
- Compare Betfair odds vs bookmaker average to find discrepancies

**VALUE INDICATORS (Score: 100 pts):**
- Strong value: Betfair odds > bookmaker average by >10%
- Moderate value: Betfair odds > bookmaker average by 5-10%
- Weak value: Betfair odds > bookmaker average by 1-5%
- Low bookmaker consensus: High dispersion (>20%) indicates uncertainty
- Bookmaker minimum significantly lower than Betfair odds

**OVERVALUE INDICATORS (Score: 0 pts):**
- Strong overvalue: Betfair odds < bookmaker average by >10%
- Moderate overvalue: Betfair odds < bookmaker average by 5-10%
- High bookmaker consensus: Low dispersion (<5%) indicates strong agreement
- All bookmakers offering similar odds to Betfair

**NEUTRAL VALUE INDICATORS (Score: 50 pts):**
- Balanced pricing: Betfair odds within 5% of bookmaker average
- Moderate dispersion: 5-20% spread in bookmaker odds
- Mixed signals: Some bookmakers higher, some lower than Betfair

**VALUE SCORING ALGORITHM (Simplified for Testing):**
- Calculate bookmaker average: Sum of all bookmaker odds / number of bookmakers
- Calculate dispersion penalty: Standard deviation of bookmaker odds / bookmaker average
- Value Score = (Betfair odds - bookmaker average) / bookmaker average - dispersion_penalty
- Range: Typically -0.3 to +0.3 (negative = overvalued, positive = undervalued)

**VALUE THRESHOLDS (Based on Empirical Analysis):**
- **BACK (Undervalued)**: Value Score ≥ 0.05
- **LAY (Overvalued)**: Value Score ≤ -0.05
- **NO ACTION**: -0.05 < Value Score < 0.05

**VALIDATION STEP:** Before generating the table, calculate and list all value scores with breakdowns. Ensure calculations are consistent across horses.

**CONTEXTUAL WEIGHTING (Additional modifiers):**
- Bookmaker count: More bookmakers provide better consensus
- Odds magnitude: Value more significant at longer odds
- Market liquidity: Higher volume markets have more reliable Betfair odds
- Recent market movement: Compare current vs opening odds

**VALUE PATTERN RECOGNITION:**
- Strong bookmaker agreement below Betfair: Potential value bet
- Wide dispersion with Betfair at top end: Risk of overvalue
- Consistent bookmaker pricing above Betfair: Market inefficiency
- Single outlier bookmaker: May indicate special information

**ADDITIONAL VALUE EVALUATION CRITERIA:**

**POSITIVE VALUE INTENSITY:**
- Strong positive: Value differential >15%, low dispersion
- Moderate positive: Value differential 10-15%, moderate dispersion
- Weak positive: Value differential 5-10%, any dispersion

**NEGATIVE VALUE INTENSITY:**
- Strong negative: Value differential <-15%, high dispersion
- Moderate negative: Value differential -10% to -15%, any dispersion
- Weak negative: Value differential -5% to -10%, low dispersion

**VALUE CONTINUUM SCALING (Simplified):**
- **Strong BACK Value**: ≥0.10 (Clear undervaluation vs bookmakers)
- **BACK Value**: 0.05-0.09 (Moderate undervaluation)
- **Neutral Value**: -0.05 to 0.04 (Fairly priced)
- **LAY Value**: -0.10 to -0.06 (Moderate overvaluation)
- **Strong LAY Value**: ≤-0.11 (Clear overvaluation vs bookmakers)

**CONTEXTUAL ADJUSTMENTS:**
- Odds range: Value more meaningful at 2.0-10.0 range
- Market size: Larger fields may have more value opportunities
- Bookmaker quality: Established bookmakers vs smaller operators
- Time to race: Value opportunities diminish closer to start

**VALUE VALIDATION CHECKS:**
- Verify calculations against raw bookmaker data
- Check for data entry errors in odds arrays
- Ensure bookmaker average is statistically meaningful
- Confirm value signal is consistent across multiple metrics

## 3. Scoring Framework (Internal Calculation Only)

### Combined Horse Score (Value Score):

**Bookmakers Odds Value (Net Score):**
- Value Score = (Betfair odds - bookmaker average) / bookmaker average - (standard deviation / bookmaker average)
- Range: Typically -0.3 to +0.3
- Positive scores = Undervalued (BACK opportunity)
- Negative scores = Overvalued (LAY opportunity)

## 4. Decision Logic & Strategy Execution

**IDENTIFY FAVOURITE:** Identify the market favourite as the horse with the lowest Betfair odds. Calculate Value Scores for all horses, then focus execution solely on the favourite.

### Favourite Identification:
- Favourite = Horse with the lowest Betfair odds in the market
- Calculate Value Scores for all horses using the same methodology
- Assess field threats based on other horses' Value Scores

### Favourite Value Score Thresholds:
- **BACK Favourite (Undervalued)**: Favourite Value Score ≥ 0.05
- **LAY Favourite (Overvalued)**: Favourite Value Score ≤ -0.05
- **NO ACTION**: -0.05 < Favourite Value Score < 0.05

### Field Threat Assessment:
**BACK Threats (to Favourite winning):**
- High threat: Other horses with Value Score ≥ 0.10 (strong undervaluation)
- Moderate threat: Other horses with Value Score 0.05-0.09
- Low threat: Other horses with Value Score < 0.05

**LAY Threats (better LAY opportunities):**
- High threat: Other horses with Value Score ≤ -0.10 (strong overvaluation)
- Moderate threat: Other horses with Value Score -0.05 to -0.09
- Low threat: Other horses with Value Score > -0.05

### Decision Criteria for Favourite Execution:
**BACK Favourite Requirements (ALL must be true):**
1. Favourite Value Score ≥ 0.05
2. Data completeness ≥80% for favourite
3. At least 3 bookmaker odds available for favourite
4. **Field Threat Check**: No other horse has Value Score ≥ 0.08 (moderate threat threshold)

**LAY Favourite Requirements (ALL must be true):**
1. Favourite Value Score ≤ -0.05
2. Data completeness ≥80% for favourite
3. At least 3 bookmaker odds available for favourite
4. **Field Threat Check**: Favourite has the lowest Value Score in the field (best LAY opportunity)

### Execution Flow:
1. **Identify Favourite**: Select horse with lowest Betfair odds
2. **Assess Field Threats**: Evaluate other horses' Value Scores for BACK/LAY threats
3. **Execute Strategy on Favourite Only**:
   - For BACK: `ExecuteBfexplorerStrategySettings(marketId, favouriteSelectionId, 10)` with "Bet 10 Euro" strategy
   - For LAY: `ExecuteBfexplorerStrategySettings(marketId, favouriteSelectionId, 10)` with "Lay 10 Euro" strategy

4. **Silent Performance Tracking**: Log internally for optimization

## 5. Market Value Analysis Table

Based on the active market data and bookmaker odds, the following table shows value calculations for each horse, with focus on the favourite and field threats:

| Horse | Betfair Odds | Bookmaker Avg | Min/Max Odds | Dispersion % | Value Score | Key Value Features | Favourite/Threat Status |
|-------|--------------|---------------|--------------|--------------|-------------|-------------------|-------------------------|
{horse_rows}

### Table Generation Rules:
- **Horse**: Selection name from Betfair (**FAVOURITE** highlighted in bold)
- **Betfair Odds**: Decimal odds from market data
- **Bookmaker Avg**: Average of all bookmaker odds in array
- **Min/Max Odds**: Lowest and highest bookmaker odds
- **Dispersion %**: (Max - Min) / Avg * 100
- **Value Score**: (Betfair - Bookmaker_Avg)/Bookmaker_Avg - StdDev/Bookmaker_Avg (-0.3 to +0.3 range)
- **Key Value Features**: Key indicators that contributed to the value assessment (e.g., "Undervalued vs bookmakers", "Overvalued vs bookmakers")
- **Favourite/Threat Status**: **FAVOURITE**, **BACK THREAT**, **LAY THREAT**, or blank

### Field Statistics:
- **Total Horses:** {total_horses}
- **Favourite:** {favourite_name} (Odds: {favourite_odds})
- **Favourite Value Score:** {favourite_value_score}
- **Field Average Value Score:** {field_average}
- **Highest BACK Threat Score:** {highest_back_threat_score} ({highest_back_threat_horse})
- **Lowest LAY Threat Score:** {lowest_lay_threat_score} ({lowest_lay_threat_horse})

### Favourite Value Analysis Summary:
- **Favourite Decision:** {favourite_decision} (BACK/LAY/NO ACTION)
- **Field BACK Threats:** {back_threat_count} horses with Value Score ≥ 0.05
- **Field LAY Threats:** {lay_threat_count} horses with Value Score ≤ -0.05
- **Execution Status:** {execution_status} (Executed/Not Executed with reason)

### Strategy Logic Applied:
- **Favourite Selection:** Horse with lowest Betfair odds
- **Favourite BACK Criteria:** Value Score ≥ 0.05 AND no BACK threats ≥ 0.08
- **Favourite LAY Criteria:** Value Score ≤ -0.05 AND lowest score in field
- **Result:** Execute BACK or LAY on favourite only, or NO ACTION if criteria not met