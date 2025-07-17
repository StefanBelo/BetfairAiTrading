# Horse Racing Strategy R6 - Optimized

## 1. Overview
- **Goal**: Analyze horse racing markets to find high-value back or lay bets.
- **Operation**: **SILENT MODE ONLY.** No reporting, commentary, or output. All analysis and execution are internal and silent.
- **Logic**:
  - **Back**: Bet on a horse if it meets all backing criteria.
  - **Lay**: Bet against an overvalued favorite if it meets all laying criteria.
  - **No Action**: If no opportunity is found, perform analysis only.
- **CRITICAL**: Use exact strategy names: `strategyName="Bet 10 Euro"` for backing, `strategyName="Lay 10 Euro"` for laying. No other names are permitted.

## 2. Required Data Sources
1.  **RacingpostDataForHorsesInfo**: Contains `lastRacesDescriptions[]`, `officialRating`, `rpRating`.
2.  **HorsesBaseBetfairFormData**: Contains `forecastPrice`, `form`, `officialRating`, `weight`.
3.  **MarketSelectionsTradedPricesData**: Contains `endPrice`, `maxPrice`, `minPrice`, `startPrice`, `tradedVolume`.

## 3. Decision Logic & Execution

### Step 1: Calculate Scores for Each Horse
- **Composite Score**: `(Semantic Form * 0.35) + (Value * 0.25) + (Prediction * 0.25) + (Stability * 0.15)`
  - **Semantic Form Score**: Analyze `lastRacesDescriptions` array:
    - If no descriptions: 0
    - If recent wins (position 1): 80-100 based on race quality
    - If recent places (position 2-3): 60-80 based on performance
    - If poor recent form (position >4 or pulled up): 20-40
    - Factor in `lastRunInDays` (recent runs <60 days get bonus)
  - **Value Score**: `((endPrice - forecastPrice) / forecastPrice) * 100`.
  - **Prediction Score**: Use `rpRating` from RacingpostDataForHorsesInfo (0-130 scale, normalize to 0-100).
  - **Stability Score**: `100 - ((maxPrice - minPrice) / minPrice * 100)`.

### Step 2: Calculate Enhanced Expected Value (EV)
- **Base EV**: `(Est. Win Probability * (endPrice - 1)) - ((1 - Est. Win Probability) * 1)`
  - **Est. Win Probability**: `((Semantic Form Score * 0.6) + (Prediction Score * 0.4)) / 100`
- **Enhanced EV**: `Base EV * CWAF`
  - **CWAF**: `Market Confidence * Probability Movement * Volume * Historical Success`
    - **Market Confidence**: >90% stability=1.2; 80-90%=1.1; 70-80%=1.0; <70%=0.9.
    - **Probability Movement**: If endPrice < startPrice (shortening)=1.2; Stable=1.0; Drifting=0.8.
    - **Volume**: tradedVolume >5k=1.1; 2k-5k=1.0; <2k=0.9.
    - **Historical Success**: Strong recent form=1.1; Average=1.0; Weak=0.9.

### Step 3: Determine Action based on Betting Criteria

#### Lay Bet First (Check Favorite)
- **IF** the favorite meets **ALL** these criteria:
  - Price (endPrice): ≤ 2.0
  - Enhanced EV: Is Negative
  - Semantic Form Score: < 70
  - Composite Score: < 55
  - Alternatives: ≥ 2 other horses have positive Enhanced EV
- **THEN**: Execute `ExecuteBfexplorerStrategySettings` with `strategyName="Lay 10 Euro"`.

#### Back Bet Second (Check All Horses)
- **ELSE IF** any horse meets **ALL** these criteria:
  - Semantic Form Score: ≥ 70
  - Composite Score: ≥ 60
  - Enhanced EV: ≥ +5%
  - Price Range (endPrice): 2.0 - 15.0
- **THEN**: Select the horse with the highest Enhanced EV (if close, highest composite score) and execute `ExecuteBfexplorerStrategySettings` with `strategyName="Bet 10 Euro"`.

#### No Bet
- **ELSE**: Perform analysis only. Do not execute any strategy.

## 4. Execution Protocol (Silent)
1.  Call `GetActiveBetfairMarket`.
2.  Retrieve the three required data contexts using `GetAllDataContextForBetfairMarket`.
3.  Perform all calculations from Step 1 & 2 for all horses using actual field names.
4.  Follow the logic in Step 3 to determine the action.
5.  Ensure market has > 1000 Euro `tradedVolume` total before betting.
6.  Execute the appropriate tool call if a bet is warranted using `ExecuteBfexplorerStrategySettings`.
7.  All steps are performed silently.