# Horse Racing EV Analysis R1 (Silent)

## Overview
This prompt outlines a silent process for analyzing horse racing data, calculating Expected Value (EV), and executing bets on the highest EV horse if specific criteria are met. The output is limited to the execution result only.

## Steps

### 1. Retrieve Active Market
- Use `GetActiveMarket` to obtain the currently active horse racing market.

### 2. Fetch Horse Data
- Call `GetDataContextForMarket` with `dataContextNames: ['RacingpostDataForHorses']` to retrieve detailed horse performance data.

### 3. Analyze Horse Performance
For each horse, evaluate the `lastRaces` array based on the following factors (prioritize recent races):
- **Position**: 1st place is optimal; unplaced is worst.
- **Beaten Distance**: Shorter distances indicate better performance.
- **Race Distance**: Consider suitability for the current race distance.
- **Topspeed Rating**: Higher ratings are preferable.
- **Weight Carried**: Lighter weights can be advantageous.
- **Recency**: Lower `lastRunInDays` values (more recent) are better.
- **Race Description Sentiment**: Look for positive keywords (e.g., "led", "won", "comfortably") and negative ones (e.g., "weakened", "distressed").
- **Official/RP Ratings**: Higher ratings suggest stronger form.

### 4. Estimate True Probability
- Based on the analysis in Step 3, assign a "true" probability of winning for each horse, adjusting for market odds if they appear mispriced.

### 5. Calculate Expected Value (EV)
- Use the formula: `EV = (True Probability * (Decimal Odds - 1)) - (1 - True Probability)`

### 6. Evaluate Betting Criteria
- Identify the horse with the highest positive EV.
- Ensure it meets: Positive EV and True Probability > 10%.

### 7. Execute Bet
- If criteria are met, use `ExecuteBfexplorerStrategySettings` with:
  - `marketId`: From Step 1
  - `selectionId`: The highest EV horse
  - `strategyName`: "Bet 10 Euro"

## Output Format
- If bet executed: "Strategy executed on [Horse Name] with EV [value] and probability [value]"
- If no bet: "No bet executed - criteria not met"