# Horse Racing EV Analysis - Favourite R9 Silent

## 1. Core Strategy & Data

**Objective**: Analyze the entire field to execute trades ONLY on the favourite horse. BACK strong favourites, LAY weak ones, and take NO ACTION otherwise. All analysis is performed silently, with a report generated only upon execution.

**Data Retrieval & Validation**:
1.  **Get Market**: Use `GetActiveBetfairMarket` to get the active market and selections.
2.  **Get Horse Data**: Use `GetAllDataContextForBetfairMarket` with `['TimeformDataForHorses', 'RacingpostDataForHorses']`.
3.  **Validation**: Proceed only if data completeness is ≥80% for all horses.

**Configuration**:
-   `generateEVReport`: `true` (default) or `false`.
-   `evReportFormat`: `"table"` (default) or `"json"`.

## 2. Analysis & Scoring Framework

**Semantic Analysis**:
-   Analyze race history (up to 6 races) to identify performance evolution.
-   Compare recent (last 3) vs. historical (previous 3) races to score evolution from **Strong Improvement (+6)** to **Strong Decline (-5)**.
-   Contextual factors include race class, distance, ground, and field strength.

**Combined Horse Score (100 points total)**:
-   **Base Rating (35%)**: Timeform Stars and RP Rating.
-   **Enhanced Form & Evolution (40%)**: Recent wins, form flags (`horseWinnerLastTimeOut`, `horseInForm`), and the performance evolution score.
-   **Suitability (15%)**: `suitedByGoing/Course/Distance`.
-   **Connections (10%)**: Trainer and jockey form (`trainerInForm`, `jockeyInForm`).

## 3. Field Analysis & EV Calculation

**Field Position Metrics**:
-   **Favourite Dominance Score**: Favourite's score vs. the field average.
-   **Quality Threats**: Count of horses with a score ≥70.
-   **Field Strength Category**: Classify the favourite's position from **DOMINANT** (≥20 points above avg) to **WEAK** (<5 points above avg).

**EV Calculation**:
1.  **True Probability**: Derived from normalized horse scores.
2.  **Implied Probability**: `1 / Decimal Odds`.
3.  **EV Formula**: `((TrueProb - ImpProb) / ImpProb) * 100`.
4.  **Dynamic Adjustments**: Apply multipliers based on horse quality, field strength, opposition threat, and performance evolution.

## 4. Execution Logic & Reporting

**Decision Logic**:
-   **Identify Favourite**: The horse with the lowest decimal odds.
-   **Field-Relative EV Thresholds**: Use stricter EV thresholds for weaker favourites.
    -   **DOMINANT Favourite**: BACK ≥+3%, LAY ≤-10%
    -   **STRONG Favourite**: BACK ≥+5%, LAY ≤-8%
    -   **MODERATE Favourite**: BACK ≥+7%, LAY ≤-6%
    -   **VULNERABLE Favourite**: BACK ≥+10%, LAY ≤-4%
    -   **WEAK Favourite**: BACK ≥+12%, LAY ≤-2%
-   **Execution Criteria**:
    -   **BACK**: Positive EV, DOMINANT/STRONG field position, positive form/evolution.
    -   **LAY**: Negative EV, VULNERABLE/WEAK field position, declining/erratic evolution.
    -   **NO ACTION**: Unclear value, high data uncertainty, or conflicting signals.

**Execution Commands**:
-   **BACK**: `ExecuteBfexplorerStrategySettings(marketId, favouriteSelectionId, "Bet 10 Euro")`
-   **LAY**: `ExecuteBfexplorerStrategySettings(marketId, favouriteSelectionId, "Lay 10 Euro")`

**Reporting**:
1.  **Execution Report**: A single-line summary is generated upon execution.
    ```
    STRATEGY_EXECUTED: [BACK/LAY] [Horse_Name] at [Odds] - EV: [XX.X]% - Field: [DOMINANT/STRONG/etc]
    ```
2.  **EV Report (Conditional)**: If `generateEVReport` is `true`, a detailed report (markdown table or JSON) for all horses is generated after execution, providing a full breakdown of the analysis that informed the favourite-only decision.

**Error Handling**:
-   If data is incomplete (<80%) or a calculation error occurs, default to NO ACTION and log the issue internally.
