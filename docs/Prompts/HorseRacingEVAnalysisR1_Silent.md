# Horse Racing EV Analysis R1 (Silent)

## Objective
Silently analyze horse racing data, calculate Expected Value (EV), and execute bet on highest EV horse if criteria met. Report only execution result.

## Process
1. Get active market: `GetActiveBetfairMarket`
2. Get horse data: `GetAllDataContextForBetfairMarket` with `dataContextNames: ['RacingpostDataForHorsesInfo']`
3. For each horse, analyze `lastRacesDescriptions` considering:
   - Position (1st=best, unplaced=worst)
   - Beaten distance (smaller=better)
   - Recency (`lastRunInDays`, recent=better)
   - Race description sentiment (positive/negative keywords)
   - Official/RP ratings
4. Estimate "true" probability for each horse
5. Calculate: `EV = (True Probability * (Decimal Odds - 1)) - (1 - True Probability)`
6. Execute bet if highest EV horse has:
   - Positive EV
   - True Probability > 10%

## Execution
Use `ExecuteBfexplorerStrategySettings` with:
- `marketId`: from step 1
- `selectionId`: highest EV horse 
- `strategyName`: "Bet 10 Euro"

## Output
Only report: "Strategy executed on [Horse Name] with EV [value] and probability [value]" OR "No bet executed - criteria not met"
