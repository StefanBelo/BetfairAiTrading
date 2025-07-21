# Horse Racing Expected Value Analysis (Favourite R3)

This prompt guides the analysis of horse racing data using Timeform's professional racing analysis system to calculate the Expected Value (EV) for each horse in an active Betfair market, but only the favourite (lowest odds) will be considered for betting.

## Step 1: Retrieve Active Market Data

First, identify the active horse racing market using the `GetActiveBetfairMarket` tool. This will provide the `marketId` and a list of selections (horses) with their current odds.

## Step 2: Retrieve Detailed Horse Information

With the `marketId` from the previous step, use the `GetAllDataContextForBetfairMarket` tool with `dataContextNames` set to `['TimeformDataForHorsesInfo']`. This will return detailed Timeform analysis for each horse in the `timeformHorseData` field, including:

- `ratingStars`: Timeform's star rating system (1-5 stars)
- `horseWinnerLastTimeOut`, `horseInForm`, `horseBeatenFavouriteLTO`
- `suitedByGoing`, `suitedByCourse`, `suitedByDistance`
- `trainerInForm`, `trainerCourseRecord`, `jockeyInForm`, `jockeyWonOnHorse`
- `timeformTopRated`, `timeformImprover`, `timeformHorseInFocus`

## Step 3: Analyze Timeform Data

For each horse, perform a comprehensive analysis of the Timeform boolean indicators and star rating. Create a scoring system based on the following factors:

- **Base Rating Score (40% weight):**
  - 5 Stars: 100 points
  - 4 Stars: 80 points
  - 3 Stars: 60 points
  - 2 Stars: 40 points
  - 1 Star: 20 points
- **Form Analysis Score (25% weight):**
  - `horseWinnerLastTimeOut`: +15 points
  - `horseInForm`: +10 points
  - `horseBeatenFavouriteLTO`: +8 points
- **Suitability Score (20% weight):**
  - `suitedByGoing`: +7 points
  - `suitedByCourse`: +7 points
  - `suitedByDistance`: +6 points
- **Connections Score (10% weight):**
  - `trainerInForm`: +4 points
  - `trainerCourseRecord`: +3 points
  - `jockeyInForm`: +2 points
  - `jockeyWonOnHorse`: +1 point
- **Timeform Special Designations (5% weight):**
  - `timeformTopRated`: +3 points
  - `timeformImprover`: +1 point
  - `timeformHorseInFocus`: +1 point

**Total Timeform Score**: Sum all applicable points (maximum possible: 200 points)

## Step 4: Calculate Implied Probability and Expected Value (EV)

1. **Estimate 'True' Probability:**
   - Normalize the Timeform Score: `Normalized Score = (Timeform Score / 200) * 100`
   - Calculate market share: For each horse, divide its Normalized Score by the sum of all horses' Normalized Scores
   - This gives you the estimated true probability for each horse
2. **Calculate Implied Probability:** For each horse, calculate the implied probability from their Betfair odds using the formula:
   `Implied Probability = 1 / Decimal Odds`
3. **Calculate Expected Value (EV):** For each horse, calculate the EV using the normalized formula:
   ```
   If True Probability >= Implied Probability:
       EV = ((True Probability - Implied Probability) / Implied Probability) * 100
   Else:
       EV = ((True Probability - Implied Probability) / True Probability) * 100
   Then apply bounds: EV = max(-100, min(100, EV))
   ```

## Step 5: Apply Timeform-Specific Adjustments

- If `timeformTopRated` is true: Multiply EV by 1.15
- If `timeformImprover` is true: Multiply EV by 1.10
- If both `horseWinnerLastTimeOut` and `horseInForm` are true: Multiply EV by 1.08
- If none of the suitability factors are true: Multiply EV by 0.90
- If `ratingStars` is 1 or 2: Multiply EV by 0.85

## Step 6: Present the Results

Present the analysis in a clear table format with the following columns for each horse:
- Horse Name
- Timeform Stars
- Current Odds
- Timeform Score (out of 200)
- Implied Probability
- Estimated "True" Probability
- Calculated EV (after adjustments)
- Key Timeform Indicators Summary

The final output should be a ranked list of horses by their adjusted EV, from highest to lowest.

## Step 7: Bet Only on the Favourite

Identify the favourite (the horse with the lowest current odds). Only this horse will be considered for betting.

### Betting Criteria for Favourite:
- **Positive EV**: Greater than +10% (using normalized EV calculation)
- **Minimum Quality**: At least 3 Timeform stars OR `timeformTopRated` is true
- **Form Requirement**: At least one of: `horseInForm`, `horseWinnerLastTimeOut`, or `timeformImprover` is true
- **Odds Range**: Between 2.0 and 15.0


### Execution:
- If the favourite meets all criteria, execute a bet using the `ExecuteBfexplorerStrategySettings` tool with:
  - `marketId`: The ID of the active market
  - `selectionId`: The ID of the favourite
  - `strategyName`: "Bet 10 Euro"
- If the favourite does **not** meet all criteria, execute a lay bet using the `ExecuteBfexplorerStrategySettings` tool with:
  - `marketId`: The ID of the active market
  - `selectionId`: The ID of the favourite
  - `strategyName`: "Lay 10 Euro"

After executing the strategy, always include a report table showing the EV analysis for all horses in the market, as described in Step 6. This table should be presented in markdown format and ranked by adjusted EV, so the user can review the value assessment for every runner.

## Step 8: Risk Management

- Avoid betting on horses with odds greater than 15.0
- Consider the race distance and going conditions in your final assessment

This approach leverages professional racing analysis and systematic risk management, but always bets (back or lay) only on the favourite based on value and quality criteria.
