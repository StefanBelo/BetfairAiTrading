---
title: Horse Race Winner Prediction - Quantitative Analysis
description: Predict horse race winners using Bfexplorer tools with data-driven composite scoring
purpose: Determine the most likely race winner based on multiple performance metrics
---

# Horse Race Winner Prediction Model

## Objective
Predict the winner of a horse race on Betfair by analyzing historical performance, jockey records, market sentiment, and expert ratings using quantitative composite scoring.

## Methodology

### Step 1: Retrieve Active Market Data
**Tool:** `get_active_market`
**Action:** Call with no arguments to fetch the current active race.

**Extract from result:**
- `marketId` - Race identifier (string)
- `selections` - Array of horses with:
  - `selectionId` - Unique horse ID
  - `name` - Horse name
  - `price` - Decimal odds (current market price)
- `metadata` - Race context (course, surface, distance if available)

---

### Step 2: Collect Comprehensive Horse & Jockey Data
**Tool:** `get_all_data_context_for_market`
**Arguments:**
- `marketId` - From Step 1
- `dataContextNames` - Array: `["DbJockeysResults", "DbHorsesResults", "MarketSelectionsTradedPricesData", "RacingpostDataForHorses", "TimeformFullDataForHorses"]`

**Parse & Calculate Metrics for Each Horse:**

#### A. Horse Historical Performance
- **Data Source:** `DbHorsesResults`
- **Calculation:** `Horse Win Rate % = (wins / total races) * 100`
- **Normalization:** Divide by 100 for composite formula

#### B. Jockey Performance
- **Data Source:** `DbJockeysResults`
- **Calculation:** `Jockey Win Rate % = (wins / total rides) * 100`
- **Default:** Use 0% if no jockey data available
- **Normalization:** Divide by 100 for composite formula

#### C. Market Sentiment
- **Data Source:** `MarketSelectionsTradedPricesData`
- **Calculation:** `Implied Probability = 1 / price`
- **Use:** Most recent price if multiple values available; average if needed
- **Range:** 0-1 (direct probability)

#### D. Expert Consensus Rating
- **Data Sources:** `RacingpostDataForHorses` + `TimeformFullDataForHorses`
- **Process:**
  1. Extract numeric rating from RacingpostDataForHorses
  2. Extract numeric rating from TimeformFullDataForHorses
  3. Normalize both to 0-10 scale (divide by 10 if max is 100)
  4. `Average Expert Rating = (Racingpost + Timeform) / 2`
- **Normalization:** Divide by 10 for composite formula

---

### Step 3: Calculate Composite Winning Score

**Formula:**
$$\text{Composite Score} = (H \times 0.25) + (J \times 0.25) + (M \times 0.3) + (E \times 0.2)$$

Where:
- **H** = Horse Win Rate % / 100
- **J** = Jockey Win Rate % / 100
- **M** = Implied Probability from Price (0-1)
- **E** = Average Expert Rating / 10

**Weighting Rationale:**
- **Past Performance (50%):** Horse (25%) + Jockey (25%) = Combined historical track record
- **Market Consensus (30%):** Implied probability reflects aggregate betting market intelligence
- **Expert Opinion (20%):** Professional ratings provide qualitative validation

---

### Step 4: Rank & Present Results

**Output Format:** Table with columns:
| Horse | Price | Horse Win % | Jockey Win % | Implied Prob | Expert Rating | Composite Score | Rank |
|-------|-------|-------------|--------------|--------------|---------------|-----------------|------|

**Ranking:** Sort by Composite Score (descending)

---

## Key Insights

- **Score > 0.15:** Strong favorite with multiple positive indicators
- **Score 0.10-0.15:** Competitive contender with solid form
- **Score 0.05-0.10:** Moderate prospect; consider odds/value
- **Score < 0.05:** Long shot; higher risk

---

## Output Requirements

1. Display complete results table sorted by composite score
2. Highlight top 3 candidates with key differentiators
3. Note any horses with incomplete data
4. Provide brief recommendation based on quantitative analysis