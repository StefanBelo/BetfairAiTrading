
# Horse Racing Data Evaluation Prompt: RacingStattoData

## Data Ingestion
1. Call `GetActiveMarket` for marketId, metadata, selections (selectionId, name, price).
2. Call `GetAllDataContextForMarket` with marketId and dataContextNames: ["RacingStattoData"].

## Data Preparation

Data Fields (schema)
- `goingRank` (integer) — rank of going; 1 is best.
- `distanceRank` (integer)
- `goingDistRank` (integer)
- `courseRank` (integer)
- `totalWinsRank` (integer)
- `percentageTotalRank` (integer)
- `orRank` (integer)
- `tsRank` (integer)
- `rprRank` (integer)
- `jockeyPercentageRank` (integer)
- `trainerPercentageRank` (integer)
- `timeRank` (integer)
- `fastestTimeRank` (integer)
- `rank` (integer)

Example JSON row (for data ingestion & validation):
```
{
	"selectionId": "78584165_0.00",
	"name": "Giantsgrave",
	"price": 1.61,
	"racingStatto": {
		"goingRank": 9, "distanceRank": 9, "goingDistRank": 9, "courseRank": 9, "totalWinsRank": 9,
		"percentageTotalRank": 9, "orRank": 2, "tsRank": 3, "rprRank": 3, "jockeyPercentageRank": 1,
		"trainerPercentageRank": 2, "timeRank": 6, "fastestTimeRank": 9, "rank": 1
	}
}
```

Validation rules
- Treat undefined or 0 ranks as missing; either drop the field from composite scoring or replace with maxRank.
- If more than half of the ranking fields are missing, set `confidence: low` and flag the selection.
- Avoid division by zero by using `value = max(1, fieldValue)` in formulas.

Weighting and tuning
- Default: equal weights across fields. Optional override: supply `weights` map, e.g. `{"timeRank":2.0, "rprRank":1.25}`.
- You can tune weights if historical calibration shows some fields are more predictive.

Compact human-friendly view
- Keep the small reporting table only within `## Reporting` (see below) — remove large data tables from this section to avoid duplication.

## Calculations
1. Implied Probability = 1 / Price
2. True Probability (weighted & safe):
	- Preprocess: set fieldValue = max(1, fieldValue) to avoid division by zero.
	- Composite score per horse = sum( weight[field] / fieldValue for each present field ); by default weights = 1 for all fields.
	- Normalize: True Probability = composite_score / sum(composite_scores across all selections)
3. EV = (True Probability × (Price - 1)) - (1 - True Probability)
4. Kelly Fraction = max(0, EV / (Price - 1))
	- Note: Kelly as fraction of bankroll — consider using fractional Kelly (e.g., 0.25 × Kelly) for risk control.

## Reporting
Table:
| Horse Name | Price | Implied Probability | True Probability | EV | Kelly Fraction |
|------------|-------|--------------------|------------------|----|----------------|
| ...        | ...   | ...                | ...              | ...| ...            |

## Notes
- Highlight highest positive EV and Kelly stakes.