# Horse Racing EV Analysis - R10 Silent

## Overview
This strategy analyzes horse racing markets to identify the best Expected Value (EV) opportunity for BACK or LAY bets. It calculates EV for all selections, focuses on the top 3 favourites by odds, sorts them by EV, and executes a trade on the most attractive option. All operations are silent, with reports generated only upon execution.

**Key Steps**:
1. Retrieve market and horse data.
2. Score horses using a combined framework.
3. Calculate EV for all selections.
4. Select from top 3 favourites by odds, sorted by EV.
5. Execute trade if thresholds are met.

## Data Retrieval & Validation
- **Market Data**: Use `GetActiveBetfairMarket` to fetch active market and selections.
- **Horse Data**: Use `GetAllDataContextForBetfairMarket` with `['TimeformDataForHorses', 'RacingpostDataForHorses']`.
- **Validation**: Proceed only if data completeness ≥80% for top 3 favourites. Skip if incomplete.

**Configuration**:
- `generateEVReport`: `true` (default).
- `evReportFormat`: `"table"` or `"json"`.

## Scoring Framework
Analyze race history (up to 6 races) for performance evolution, comparing recent (last 3) vs. historical (previous 3) races. Score evolution from +6 (Strong Improvement) to -5 (Strong Decline). Consider race class, distance, ground, and field strength.

**Combined Horse Score (100 points)**:
- **Base Rating (35%)**: Average of Timeform Stars (out of 5) and normalized RP Rating.
- **Enhanced Form & Evolution (40%)**: Recent wins, form flags (`horseWinnerLastTimeOut`, `horseInForm`), and evolution score.
- **Suitability (15%)**: `suitedByGoing/Course/Distance`.
- **Connections (10%)**: `trainerInForm`, `jockeyInForm`.

## EV Calculation
- **True Probability**: Normalized from horse scores.
- **Implied Probability**: `1 / Decimal Odds`.
- **EV Formula**: `((TrueProb - ImpProb) / ImpProb) * 100`.
- **Adjustments**: Apply multipliers for quality, field strength, opposition, and evolution.

**Field Metrics**:
- **Selection Quality**: Score vs. top 3 average.
- **Quality Threats**: Favourites with score ≥70.
- **Category**: DOMINANT (≥20 above avg) to WEAK (<5 above avg).

## Decision Logic
- **Selection Process**: Calculate EV for all selections, identify top 3 favourites by odds, sort by EV (highest positive for BACK, most negative for LAY), execute on the best EV among them.
- **EV Thresholds** (by category):
  - DOMINANT: BACK ≥+3%, LAY ≤-8%
  - STRONG: BACK ≥+5%, LAY ≤-6%
  - MODERATE: BACK ≥+7%, LAY ≤-4%
  - VULNERABLE: BACK ≥+10%, LAY ≤-2%
  - WEAK: BACK ≥+12%, LAY ≤-1%
- **Criteria**:
  - **BACK**: Positive EV ≥ threshold, strong position, positive evolution.
  - **LAY**: Negative EV ≤ threshold, weak position, declining evolution.
  - **NO ACTION**: No qualifying selection.

## Execution & Reporting
- **Commands**:
  - BACK: `ExecuteBfexplorerStrategySettings(marketId, bestSelectionId, "Bet 10 Euro")`
  - LAY: `ExecuteBfexplorerStrategySettings(marketId, bestSelectionId, "Lay 10 Euro")`
- **Reports**:
  - **Execution Summary**: `STRATEGY_EXECUTED: [BACK/LAY] [Horse_Name] at [Odds] - EV: [XX.X]% - Field: [Category]`
  - **EV Report**: Markdown table of top 3 favourites, ranked by EV (if `generateEVReport` enabled).

## Error Handling
- If data <80% complete or calculation errors occur, default to NO ACTION and log internally.