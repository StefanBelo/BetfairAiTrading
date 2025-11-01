# Horse Racing Favourite Value Analysis - Back or Lay Strategy

**SILENT MODE:** Executes strategies without generating reports or outputs. All analysis performed internally with performance tracking.

## 1. Data Retrieval & Validation Framework

1. **Get Market Data**: Use `GetActiveMarket` for `marketId` and selections with odds
2. **Get Horse Data**: Use `GetDataContextForMarket` with `dataContextName: "AtTheRacesBookmakersOdds"`
3. **Data Validation**: Ensure data completeness across all horses before proceeding

### Data Sources Summary:
**AtTheRaces:** `bookmakersOdds` (array of decimal odds from multiple bookmakers)

## 2. Favourite Identification & Field Analysis Framework

### Favourite Selection:
**Identify the market favourite as the horse with the lowest Betfair odds (highest implied probability):**

**FAVOURITE CRITERIA:**
- Must have the lowest odds in the market
- Must have bookmaker odds data available
- Must be within reasonable odds range (2.0 - 10.0) to ensure liquidity

### Field Probability Estimation:
**Calculate the total implied probability of the entire field using bookmaker consensus:**

**PROBABILITY CALCULATION:**
- For each horse: bookmaker_avg_probability = 1 / bookmaker_average_odds
- Total field probability = Sum of all bookmaker_avg_probabilities
- Favourite bookmaker probability = 1 / favourite_bookmaker_average

**OVERROUND ASSESSMENT:**
- If total field probability > 1.0: Bookmakers have built-in margin (typical 5-10%)
- Favourite's fair odds = 1 / (favourite_bookmaker_probability / total_field_probability)

### Favourite Value Assessment:
**Compare the favourite's Betfair odds against estimated fair odds:**

**VALUE METRICS:**
- Favourite differential = (Betfair odds - fair odds) / fair odds
- Positive differential (>5%): Favourite is undervalued → BACK
- Negative differential (<-5%): Favourite is overvalued → LAY
- Neutral differential (±5%): No clear edge → NO ACTION

**CONFIDENCE FACTORS:**
- Bookmaker consensus strength: Lower dispersion = higher confidence
- Field size impact: Larger fields = more reliable probability estimates
- Favourite odds range: More reliable assessment at 2.0-5.0 range

**VALUE SCORING ALGORITHM:**
- Base score = favourite_differential
- Confidence adjustment = (1 - dispersion_percentage/100) * 0.5
- Final value score = base_score * confidence_adjustment
- Score > 0.1: BACK favourite
- Score < -0.1: LAY favourite
- Score between -0.1 and 0.1: NO ACTION

## 3. Scoring Framework (Internal Calculation Only)

### Favourite Value Score:
**Value Score = Net favourite assessment (ranges from -1 to 1)**
- Positive: Favourite is good value to back
- Negative: Favourite is overvalued, good to lay

## 4. Decision Logic & Strategy Execution

**FAVOURITE VALUE DECISION:**
- Sort by odds ascending to identify favourite
- Calculate value score using bookmaker consensus
- Execute based on score thresholds

### Decision Criteria:
**STRATEGY Requirements (ALL must be true):**
1. Favourite identified with odds between 2.0-10.0
2. Data completeness ≥80%
3. At least 5 bookmaker odds available per horse
4. |Value Score| ≥ 0.1 for action

### Execution Flow:
1. **BACK Strategy**: If Value Score ≥ 0.1, execute `ExecuteBfexplorerStrategySettings(marketId, favouriteSelectionId, "Bet 10 Euro")`
2. **LAY Strategy**: If Value Score ≤ -0.1, execute `ExecuteBfexplorerStrategySettings(marketId, favouriteSelectionId, "Lay 10 Euro")`
3. **NO ACTION**: If Value Score between -0.1 and 0.1
4. **Silent Performance Tracking**: Log internally for optimization

## 5. Favourite Analysis Table

Based on the active market data and bookmaker odds, the following table shows favourite value analysis:

| Favourite | Betfair Odds | Bookmaker Avg | Fair Odds | Differential | Confidence | Value Score | Decision |
|-----------|--------------|---------------|-----------|--------------|------------|-------------|----------|
{favourite_analysis}

### Table Generation Rules:
- **Favourite**: Horse name with lowest Betfair odds
- **Betfair Odds**: Favourite's decimal odds from market data
- **Bookmaker Avg**: Average of favourite's bookmaker odds
- **Fair Odds**: Estimated fair odds based on field probability distribution
- **Differential**: Percentage difference between Betfair and fair odds
- **Confidence**: Confidence level based on bookmaker consensus (0-100%)
- **Value Score**: Calculated value assessment (-1 to 1)
- **Decision**: BACK / LAY / NO ACTION (highlight decision in **bold**)

### Field Statistics:
- **Total Horses:** {total_horses}
- **Favourite:** {favourite_name}
- **Favourite Odds:** {favourite_odds}
- **Field Probability Sum:** {field_probability_sum}
- **Data Completeness:** {completeness_percentage}%

### Favourite Value Analysis Summary:
- **Favourite Value Score:** {favourite_value_score}
- **Confidence Level:** {confidence_percentage}%

### Strategy Logic Applied:
- **Favourite Selection:** {favourite_name} (lowest odds)
- **Value Thresholds:** ≥0.1 for BACK, ≤-0.1 for LAY
- **Result:** Score {favourite_value_score} → {decision} execution