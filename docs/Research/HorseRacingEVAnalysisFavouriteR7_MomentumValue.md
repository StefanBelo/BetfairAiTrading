---
title: "Horse Racing Expected Value Analysis (Favourite R7) - Momentum & Value Edition"
aliases: ["Horse Racing Expected Value Analysis (Favourite R7) - Momentum & Value Edition"]
type: strategy
tags: [automation, bfexplorer, ev-analysis, horse-racing, strategy, momentum, value-betting]
mcp_tools: [GetActiveMarket, GetAllDataContextForMarket]
data_contexts: [RacingpostDataForHorses, TimeformDataForHorses]
---

# Horse Racing Expected Value Analysis (Favourite R7) - Momentum & Value Edition

This prompt guides the analysis of horse racing data using both Timeform's professional racing analysis system and Racing Post's comprehensive racing data. It introduces an advanced **Momentum & Value** framework that prioritizes horses with "closing" finishing margins and identifies "Hidden Value" where market odds do not reflect the underlying Racing Post ratings.

## Step 1: Retrieve Active Market Data

First, identify the active horse racing market using the `GetActiveMarket` tool. This will provide the `marketId` and a list of selections (horses) with their current odds.

## Step 2: Retrieve Detailed Horse Information

With the `marketId` from the previous step, use the `GetAllDataContextForMarket` tool with `dataContextNames` set to `['TimeformDataForHorses', 'RacingpostDataForHorses']`. This will return:

### Timeform Analysis (timeformHorseData field):
- `ratingStars`: Timeform's star rating system (1-5 stars).
- `horseWinnerLastTimeOut`: Boolean.
- `horseInForm`: Boolean.
- `horseBeatenFavouriteLTO`: Boolean.
- `suitedByGoing`, `suitedByCourse`, `suitedByDistance`: Booleans.
- `trainerInForm`, `trainerCourseRecord`: Booleans.
- `jockeyInForm`, `jockeyWonOnHorse`: Booleans.
- `timeformTopRated`, `timeformImprover`, `timeformHorseInFocus`: Booleans.

### Racing Post Analysis (racingpostHorseData field):
- `lastRacesDescriptions`: Array of recent results, each with:
    - `beatenDistance`: Margin behind winner (0 for wins).
    - `lastRunInDays`: Days since last run.
    - `position`: Finishing position.
    - `raceDescription`: Text summary for semantic analysis.
    - `topspeed`: Racing Post topspeed rating.
    - `weightCarried`: Weight in pounds.
    - `distance`: Race distance in meters.
- `officialRating`: Official handicap rating.
- `rpRating`: Racing Post rating.

## Step 3: Combined Data Analysis (Momentum & Value Edition)

Perform a comprehensive analysis with the following scoring system:

### Base Rating Score (35% weight):
**Timeform Stars (15% weight):**
- 5 Stars: 30 pts | 4 Stars: 24 pts | 3 Stars: 18 pts | 2 Stars: 12 pts | 1 Star: 6 pts.

**Racing Post Rating (20% weight):**
- RP Rating > 100: 40 pts | 90-100: 32 pts | 80-89: 24 pts | 70-79: 16 pts | < 70: 8 pts.
- **NEW: Hidden Value Detection (Bonus)**: If horse's `rpRating` is within 3 points of the Favorite's `rpRating` but its Odds are > 2x the Favorite's Odds: **+15 points**.

### Form Analysis Score (30% weight):
**Timeform Form (10% weight):**
- `horseWinnerLastTimeOut`: +15 pts | `horseInForm`: +10 pts | `horseBeatenFavouriteLTO`: +5 pts.

**Racing Post Momentum (15% weight):**
- **Momentum Bonus**: If `beatenDistance[n]` < `beatenDistance[n-1]` AND `beatenDistance[n-1]` < `beatenDistance[n-2]`: **+12 points** (The "Sleeper" signal).
- **Distance Normalization**: If (`beatenDistance` / (`distance` / 200)) < 0.5: **+5 points** (Strong performance relative to race length).
- Recent win (pos 1): +10 pts | Recent place (pos 2-3): +5 pts.

**Racing Post Semantic Analysis (5% weight):**
- Positive ("made all", "won easily", "kept on well"): +3 pts per race.
- Negative ("weakened", "no extra", "always behind"): -2 pts per race.

### Suitability & Connections (20% weight):
- Timeform Suitability (Going/Course/Distance): +5 pts each.
- Timeform Connections (Trainer/Jockey in form): +3-4 pts each.

## Step 4: Field Competitive Strength Analysis

Perform a field assessment as per the R6 framework:
1. **Average Field Rating**: Mean score of non-favourites.
2. **Top Opposition Count**: Runners within 80% of favourite's score.
3. **Field Category**: WEAK (Advantage Fav), MODERATE (Balanced), or STRONG (Tough).

## Step 5: Calculate EV with Range Strategy

1. **True Probability**: Normalize Combined Score / Total Field Score.
2. **Implied Probability**: 1 / Decimal Odds.
3. **Expected Value (EV)**: `((True Prob - Implied Prob) / Implied Prob) * 100`.

## Step 6: Apply "Momentum & Skeptic" Adjustments

**Quality Multipliers:**
- If `timeformTopRated` AND `rpRating` > 90: Multiply EV by 1.20.
- **NEW: The Skeptic Filter (Conflict Penalty)**: If `timeformStars` >= 4 BUT `rpRating` has dropped by >5 points in each of the last 2 runs: **Multiply EV by 0.75** (Potential "Trap Favorite").
- **NEW: Under-the-Radar Bonus**: If `timeformImprover` is FALSE but **Momentum Bonus** is TRUE: **Multiply EV by 1.10**.

**Field Adjustments:**
- WEAK FIELD: Fav EV x 1.15 | STRONG FIELD: Fav EV x 0.85.

## Step 7: EV Range Execution Framework

- **BACK Strategy Range**: +8% ≤ EV ≤ +50%.
- **LAY Strategy Range**: EV ≤ -8%.
- **NO ACTION ZONE**: -8% < EV < +8% (Insufficient edge).

## Step 8: Execution Decision

**BACK THE FAVOURITE if:**
- EV is in range (+8% to +50%).
- **Momentum Bonus** is TRUE OR `timeformStars` >= 4.
- Field is WEAK or MODERATE.
- ≤2 High Threat opponents.

**LAY THE FAVOURITE if:**
- EV is ≤ -8%.
- **Skeptic Filter** triggered OR STRONG field with ≥3 High Threat opponents.
- Poor semantic analysis trends.

## Step 9: Results Presentation

Report the following for each runner:
1. Horse Name & Price.
2. Combined Score (highlighting Momentum/Value bonuses).
3. Field Category & Threat Level.
4. Final Adjusted EV.
5. **Momentum Status**: [IMPROVING/STABLE/DECLINING] based on `beatenDistance` trend.

---
**Execution:** Use `ExecuteBfexplorerStrategySettings` with "Bet 10 Euro" or "Lay 10 Euro" based on the above logic.