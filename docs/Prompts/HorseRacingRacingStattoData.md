
# Horse Racing Data Evaluation Prompt: RacingStattoData

## Data Ingestion
1. Call `GetActiveBetfairMarket` for marketId, metadata, selections (selectionId, name, price).
2. Call `GetAllDataContextForBetfairMarket` with marketId and dataContextNames: ["RacingStattoData"].

## Data Preparation
Table:
| Horse Name | Price | TimeRank | FastestTimeRank | Rank | AverageRank |
|------------|-------|----------|-----------------|------|-------------|
| ...        | ...   | ...      | ...             | ...  | ...         |

## Calculations
1. Implied Probability = 1 / Price
2. True Probability: Compute composite score per horse = sum(1/timeRank, 1/fastestTimeRank, 1/rank). Normalize: True Probability = composite_score / sum(composite_scores)
3. EV = (True Probability × (Price - 1)) - (1 - True Probability)
4. Kelly Fraction = max(0, EV / (Price - 1))

## Reporting
Table:
| Horse Name | Price | Implied Probability | True Probability | EV | Kelly Fraction |
|------------|-------|--------------------|------------------|----|----------------|
| ...        | ...   | ...                | ...              | ...| ...            |

## Notes
- Highlight highest positive EV and Kelly stakes.