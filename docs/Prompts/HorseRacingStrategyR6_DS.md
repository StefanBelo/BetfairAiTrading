# Horse Racing Strategy R6 - Direct Signal

## 1. Overview
- **Goal**: Analyze horse racing markets to find high-value back or lay bets.
- **Operation**: **SILENT MODE ONLY.** No reporting except final execution status.
- **Output Format**: Single line indicating if executed and on which horse
  - "Executed [Back/Lay] on [HorseName]"
  - "No action taken"
- **Logic**:
  - **Back**: Bet on a horse if it meets all backing criteria.
  - **Lay**: Bet against an overvalued favorite if it meets all laying criteria.
  - **No Action**: If no opportunity is found, output "No action taken".
- **CRITICAL**: Use exact strategy names: `strategyName="Bet 10 Euro"` for backing, `strategyName="Lay 10 Euro"` for laying. No other names are permitted.

## 2. Required Data Sources
1.  **RacingpostDataForHorses**: For prediction scores and professional analysis.
2.  **HorsesBaseBetfairFormData**: For official ratings, form, and forecast prices.
3.  **MarketSelectionsTradedPricesData**: For market behavior, price, and volume.

## 3. Decision Logic & Execution

### Step 1: Calculate Scores for Each Horse
- **Composite Score**: `(Semantic Form * 0.35) + (Value * 0.25) + (Prediction * 0.25) + (Stability * 0.15)`
  - **Semantic Form Score**: From `LastRacesDescription` (0-100).
  - **Value Score**: `((Current Price - Forecast Price) / Forecast Price) * 100`.
  - **Prediction Score**: Raw Racing Post score (0-100).
  - **Stability Score**: `100 - ((Max Price - Min Price) / Min Price * 100)`.

### Step 2: Calculate Enhanced Expected Value (EV)
- **Base EV**: `(Est. Win Probability * (Decimal Odds - 1)) - ((1 - Est. Win Probability) * 1)`
  - **Est. Win Probability**: `((Semantic Form Score * 0.6) + (Prediction Score * 0.4)) / 100`
- **Enhanced EV**: `Base EV * CWAF`
  - **CWAF**: `Market Confidence * Probability Movement * Volume * Historical Success`
    - **Market Confidence**: >90% stability=1.2; 80-90%=1.1; 70-80%=1.0; <70%=0.9.
    - **Probability Movement**: Shortening=1.2; Stable=1.0; Drifting=0.8.
    - **Volume**: >5k=1.1; 2k-5k=1.0; <2k=0.9.
    - **Historical Success**: Strong profile=1.1; Avg profile=1.0; Weak profile=0.9.

### Step 3: Determine Action based on Betting Criteria

#### Lay Bet First (Check Favorite)
- **IF** the favorite meets **ALL** these criteria:
  - Price: ≤ 2.0
  - Enhanced EV: Is Negative
  - Semantic Form Score: < 70
  - Composite Score: < 55
  - Alternatives: ≥ 2 other horses have positive Enhanced EV
- **THEN**: Execute `ExecuteBfexplorerStrategySettings` with `strategyName="Lay 10 Euro"` and output "Executed Lay on [HorseName]".

#### Back Bet Second (Check All Horses)
- **ELSE IF** any horse meets **ALL** these criteria:
  - Semantic Form Score: ≥ 70
  - Composite Score: ≥ 60
  - Enhanced EV: ≥ +5%
  - Price Range: 2.0 - 15.0
- **THEN**: Select the horse with the highest Enhanced EV (if close, highest composite score) and execute `ExecuteBfexplorerStrategySettings` with `strategyName="Bet 10 Euro"` and output "Executed Back on [HorseName]".

#### No Bet
- **ELSE**: Output "No action taken".

## 4. Execution Protocol (Silent)
1.  Call `GetActiveBetfairMarket`.
2.  Retrieve the three required data contexts `GetAllDataContextForBetfairMarket`.
3.  Perform all calculations from Step 1 & 2 for all horses.
4.  Follow the logic in Step 3 to determine the action.
5.  Ensure market has > 1000 Euro traded volume before betting.
6.  Execute the appropriate tool call if a bet is warranted `ExecuteBfexplorerStrategySettings`.
7.  Output only the final execution status as specified above.
