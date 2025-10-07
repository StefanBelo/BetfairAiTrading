
## Horse Racing EV Analysis Prompt

**Objective:** Analyze horse racing markets using Timeform data to calculate Expected Value (EV) and identify value bets. This prompt adapts to any active race and focuses on data-driven decision making.


### 1. Data Retrieval
- **Market Data:** Use `GetActiveBetfairMarket` to retrieve the current `marketId`, event details, and all selections with their decimal odds.
- **Horse Data:** For the active market, use `GetAllDataContextForBetfairMarket` with `['TimeformDataForHorsesInfo']` to obtain detailed horse information.

---


### 2. Data Structure Evaluation
- **TimeformDataForHorsesInfo Fields:** Each horse's data typically includes:
  - `ratingStars` (1–5): Core rating indicator.
  - Form indicators: `horseWinnerLastTimeOut`, `horseInForm`, `horseBeatenFavouriteLTO`.
  - Suitability: `suitedByGoing`, `suitedByCourse`, `suitedByDistance`.
  - Connections: `trainerInForm`, `trainerCourseRecord`, `jockeyInForm`, `jockeyWonOnHorse`.
  - Special: `timeformTopRated`, `timeformImprover`.
- **Validation:** Ensure data completeness (≥65% of horses with full Timeform data) before proceeding. Skip incomplete markets.


### 3. Scoring System
- **Base Score (35% weight):** Assign points based on `ratingStars` (e.g., 5★ = 35 pts, 4★ = 28 pts, 3★ = 21 pts, 2★ = 14 pts, 1★ = 7 pts).
- **Form Analysis (40% weight):**
  - `horseWinnerLastTimeOut`: +10 pts
  - `horseInForm`: +7 pts
  - `horseBeatenFavouriteLTO`: +3 pts
- **Suitability (15% weight):** +5 pts each for `suitedByGoing`, `suitedByCourse`, `suitedByDistance`.
- **Connections (10% weight):**
  - `trainerInForm`: +4 pts
  - `trainerCourseRecord`: +3 pts
  - `jockeyInForm`: +3 pts
  - `jockeyWonOnHorse`: +2 pts
- **Special Bonuses:** +5 pts for `timeformTopRated` or `timeformImprover`.
- **Total Score:** Sum all components for each horse.

### 4. EV Calculation
1. **Normalize Scores:** Calculate each horse's market share as True Probability = Horse Score / Sum of All Horse Scores.
2. **Implied Probability:** Implied Probability = 1 / Decimal Odds.
3. **EV Formula:** EV = ((True Probability - Implied Probability) / max(True Probability, Implied Probability)) × 100 (bounded -100% to +100%).
4. **Variations:**
   - Weight factors dynamically based on historical feedback (e.g., increase form weight if correlated with winners).
   - Apply multipliers for field strength or odds range.
   - Ensure positive EV for value bets.

### 5. Decision Logic
- **Identify Best Horse:** Select the horse with the highest positive EV.
- **Execution:** If EV > 0%, execute a betting strategy (e.g., "Bet 10 Euro" on the best horse). Otherwise, log as NO ACTION.
- **Logging:** Record scores, EV, and decisions for feedback analysis.

### 6. Optimization & Feedback
- **Refinement:** Adjust scoring weights based on past race outcomes and feedback data.
- **Experimentation:** Test different bonus structures or EV thresholds to improve accuracy.
- **Continuous Learning:** Use feedback to evolve the model over multiple races.

**Summary:** Follow the steps above to retrieve data, score horses, calculate EV, and make decisions. This framework is race-agnostic and optimizes through iterative feedback for better betting outcomes.
