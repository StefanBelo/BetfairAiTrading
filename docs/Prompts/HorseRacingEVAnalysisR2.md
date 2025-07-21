# Horse Racing Expected Value Analysis (R2) - Timeform Edition

This prompt guides the analysis of horse racing data using Timeform's professional racing analysis system to calculate the Expected Value (EV) for each horse in an active Betfair market.

## Step 1: Retrieve Active Market Data

First, identify the active horse racing market using the `GetActiveBetfairMarket` tool. This will provide the `marketId` and a list of selections (horses) with their current odds.

## Step 2: Retrieve Detailed Horse Information

With the `marketId` from the previous step, use the `GetAllDataContextForBetfairMarket` tool with `dataContextNames` set to `['TimeformDataForHorsesInfo']`. This will return detailed Timeform analysis for each horse in the `timeformHorseData` field, including:

### Timeform Rating System:
- `ratingStars`: Timeform's star rating system (1-5 stars, where 5 is highest quality)

### Performance Indicators:
- `horseWinnerLastTimeOut`: Boolean indicating if the horse won its last race
- `horseInForm`: Boolean indicating if the horse is currently in good form
- `horseBeatenFavouriteLTO`: Boolean indicating if the horse beat the favorite last time out

### Suitability Factors:
- `suitedByGoing`: Boolean indicating if current track conditions suit the horse
- `suitedByCourse`: Boolean indicating if the course suits the horse
- `suitedByDistance`: Boolean indicating if the race distance suits the horse

### Connections Analysis:
- `trainerInForm`: Boolean indicating if the trainer is currently in good form
- `trainerCourseRecord`: Boolean indicating if the trainer has a good record at this course
- `jockeyInForm`: Boolean indicating if the jockey is currently in good form
- `jockeyWonOnHorse`: Boolean indicating if the jockey has won on this horse before

### Timeform Special Designations:
- `timeformTopRated`: Boolean indicating if this is Timeform's top-rated horse in the race
- `timeformImprover`: Boolean indicating if Timeform expects improvement from this horse
- `timeformHorseInFocus`: Boolean indicating if this horse is featured in Timeform's focus

## Step 3: Analyze Timeform Data

For each horse, perform a comprehensive analysis of the Timeform boolean indicators and star rating. Create a scoring system based on the following factors:

### Base Rating Score (40% weight):
- **5 Stars**: 100 points
- **4 Stars**: 80 points  
- **3 Stars**: 60 points
- **2 Stars**: 40 points
- **1 Star**: 20 points

### Form Analysis Score (25% weight):
- `horseWinnerLastTimeOut`: +15 points
- `horseInForm`: +10 points
- `horseBeatenFavouriteLTO`: +8 points

### Suitability Score (20% weight):
- `suitedByGoing`: +7 points
- `suitedByCourse`: +7 points
- `suitedByDistance`: +6 points

### Connections Score (10% weight):
- `trainerInForm`: +4 points
- `trainerCourseRecord`: +3 points
- `jockeyInForm`: +2 points
- `jockeyWonOnHorse`: +1 point

### Timeform Special Designations (5% weight):
- `timeformTopRated`: +3 points
- `timeformImprover`: +1 point
- `timeformHorseInFocus`: +1 point

**Total Timeform Score**: Sum all applicable points (maximum possible: 200 points)

## Step 4: Calculate Implied Probability and Expected Value (EV)

1. **Estimate 'True' Probability:** Convert the Timeform Score to a probability:
   - Normalize the Timeform Score: `Normalized Score = (Timeform Score / 200) * 100`
   - Calculate market share: For each horse, divide its Normalized Score by the sum of all horses' Normalized Scores
   - This gives you the estimated true probability for each horse

2. **Calculate Implied Probability:** For each horse, calculate the implied probability from their Betfair odds using the formula:
   `Implied Probability = 1 / Decimal Odds`

3. **Calculate Expected Value (EV):** For each horse, calculate the EV using the normalized formula that ensures results stay within -100% to +100%:
   
   ```
   If True Probability >= Implied Probability:
       EV = ((True Probability - Implied Probability) / Implied Probability) * 100
   Else:
       EV = ((True Probability - Implied Probability) / True Probability) * 100
   
   Then apply bounds: EV = max(-100, min(100, EV))
   ```
   
   This normalized approach ensures:
   - When True Probability > Implied Probability: Positive EV (value bet), capped at +100%
   - When True Probability < Implied Probability: Negative EV (avoid bet), floored at -100%
   - Results stay within practical betting decision ranges

## Step 5: Apply Timeform-Specific Adjustments

Apply additional adjustments based on Timeform's special indicators:

### Quality Multipliers:
- If `timeformTopRated` is true: Multiply EV by 1.15
- If `timeformImprover` is true: Multiply EV by 1.10
- If both `horseWinnerLastTimeOut` and `horseInForm` are true: Multiply EV by 1.08

### Risk Adjustments:
- If none of the suitability factors (`suitedByGoing`, `suitedByCourse`, `suitedByDistance`) are true: Multiply EV by 0.90
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

## Step 7: Execute Bet

Identify the horse with the highest positive EV. If a horse with a positive EV exists **and** meets the following Timeform quality criteria, execute a bet on that horse:

### Betting Criteria:
- **Positive EV**: Greater than +10% (using normalized EV calculation)
- **Minimum Quality**: At least 3 Timeform stars OR `timeformTopRated` is true
- **Form Requirement**: At least one of: `horseInForm`, `horseWinnerLastTimeOut`, or `timeformImprover` is true
- **Odds Range**: Between 2.0 and 15.0

### Execution:
If a horse meets these criteria, execute a bet using the `ExecuteBfexplorerStrategySettings` tool with the following parameters:
- `marketId`: The ID of the active market
- `selectionId`: The ID of the qualifying horse with the highest positive EV  
- `strategyName`: "Bet 10 Euro"

If no horse meets these criteria, do not execute a bet and report "No qualifying Timeform opportunity found."

## Step 8: Risk Management

### Additional Safeguards:
- Avoid betting on horses with odds greater than 15.0
- Do not bet if more than 3 horses have positive EV (market may be too competitive)
- Consider the race distance and going conditions in your final assessment

This Timeform-based approach leverages professional racing analysis and decades of expertise to identify value betting opportunities with systematic risk management.
