# Horse Racing Expected Value Analysis (R1)

This prompt guides the analysis of horse racing data to calculate the Expected Value (EV) for each horse in an active Betfair market.

## Step 1: Retrieve Active Market Data

First, identify the active horse racing market using the `GetActiveBetfairMarket` tool. This will provide the `marketId` and a list of selections (horses) with their current odds.

## Step 2: Retrieve Detailed Horse Information

With the `marketId` from the previous step, use the `GetDataContextForBetfairMarket` tool with the `dataContextName` set to `'RacingpostDataForHorsesInfo'`. This will return detailed information for each horse, including their official rating, Racing Post rating, and descriptions of their last races.

## Step 3: Analyze Last Races Data

For each horse, perform a semantic analysis of the `lastRacesDescriptions` data. The goal is to quantify the horse's recent performance. Consider the following factors from the text descriptions:

*   **Finishing Position:** Winning (1st), placing (2nd, 3rd), or unplaced.
*   **Beaten Distance:** How far the horse was from the winner. A smaller distance is better.
*   **Semantic Race Analysis:** For each race, perform a semantic analysis of the `raceDescription`. Determine the overall sentiment (positive, negative, or neutral) and extract the key factors contributing to the horse's performance. Instead of relying on specific keywords, understand the narrative of the race. For example, a description like "finished strongly after a slow start" indicates a positive trajectory, while "faded in the final furlong" suggests a negative one.
*   **Jockey/Trainer Comments:** Note any specific comments like "jockey said gelding ran too free" or "denied a clear run". These provide crucial context.
*   **Recency:** Give more weight to more recent races (smaller `lastRunInDays`).

## Step 4: Calculate Implied Probability and Expected Value (EV)

1.  **Estimate 'True' Probability:** Based on your analysis from Step 3, assign a "true" probability of winning for each horse. This is a subjective assessment based on the qualitative data. You might create a scoring system (e.g., -10 to +10) for each race description and average them, then normalize to get a probability distribution across all horses.

2.  **Calculate Implied Probability:** For each horse, calculate the implied probability from their Betfair odds using the formula:
    `Implied Probability = 1 / Decimal Odds`

3.  **Calculate Expected Value (EV):** For each horse, calculate the EV using the formula:
    `EV = (True Probability * (Decimal Odds - 1)) - (1 - True Probability)`

## Step 5: Present the Results

Present the analysis in a clear table format with the following columns for each horse:
*   Horse Name
*   Current Odds
*   Implied Probability
*   Your Estimated "True" Probability
*   Calculated EV
*   Brief Reasoning Summary.

The final output should be a ranked list of horses by their EV, from highest to lowest.

## Step 6: Execute Bet

Identify the horse with the highest positive EV. If a horse with a positive EV exists **and** its Estimated "True" Probability is greater than 20%, execute a bet on that horse using the `ExecuteBfexplorerStrategySettings` tool. Use the following parameters:

*   `marketId`: The ID of the active market.
*   `selectionId`: The ID of the horse with the highest positive EV.
*   `strategyName`: "Bet 10 Euro"

If no horse meets these criteria, do not execute a bet.
