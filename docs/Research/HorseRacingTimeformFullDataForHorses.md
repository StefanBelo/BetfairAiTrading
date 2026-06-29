---
title: "Horse Racing Timeform Full Data Analysis - Expert & Form Edition"
aliases: ["Horse Racing Timeform Full Data Analysis - Expert & Form Edition"]
type: strategy
tags: [automation, bfexplorer, timeform, expert-analysis, form-momentum, horse-racing]
mcp_tools: [GetActiveMarket, GetAllDataContextForMarket]
data_contexts: [TimeformFullDataForHorses]
---

# Horse Racing Timeform Full Data Analysis - Expert & Form Edition

This prompt utilizes the comprehensive **TimeformFullDataForHorses** data context to perform an advanced analysis. It combines Timeform's professional semantic "Expert View," granular performance metrics like "In-Play Dominance" and "Percentile Score," and historical form trends to identify high-conviction value opportunities.

## Step 1: Retrieve Active Market Data

Identify the active horse racing market using the `GetActiveMarket` tool to obtain the `marketId` and the list of selections with their current prices.

## Step 2: Retrieve Timeform Full Data

Use the `GetAllDataContextForMarket` tool with `dataContextNames` set to `['TimeformFullDataForHorses']`. This retrieves:

### 1. Expert View (`expertView`):
- A detailed text summary from Timeform analysts providing context on recent runs, equipment changes (e.g., "visor replacing cheekpieces"), and expected performance.

### 2. Timeform Horse Data (`timeformHorseData`):
- **Flags**: `TimeformTopRated`, `TimeformImprover`, `TimeformHorseInFocus`.
- **Form Status**: `HorseWinnerLastTimeOut`, `HorseInForm`, `HorseBeatenFavouriteLTO`.
- **Suitability**: `SuitedByGoing`, `SuitedByCourse`, `SuitedByDistance`.
- **Connections**: `TrainerInForm`, `TrainerCourseRecord`, `JockeyInForm`, `JockeyWonOnHorse`.

### 3. Recent Form (`recentForm`):
- **Percentile Score**: A normalized measure of performance (0 to 1) relative to the field.
- **In-Play Dominance**: A metric (0 to 1) indicating how much the horse dominated the race (e.g., 1.0 = led/won easily).
- **Beaten Distance**: Margin behind the winner.
- **Official Rating (OR)**: Handicap rating at the time of the race.
- **Betfair Start Price (BSP)**: Historical market expectation.

## Step 3: Advanced Scoring System (Timeform Full Edition)

Calculate a **Total Score (0-100)** for each runner:

### A. Expert Sentiment Analysis (30% weight)
Extract sentiment from `expertView`:
- **Strong Positive** ("definite claims", "shortlist", "major player"): **30 pts**
- **Moderate Positive** ("better showing not ruled out", "not discounted"): **20 pts**
- **Neutral/Speculative** ("interesting if market confidence", "possible he'll strip fitter"): **12 pts**
- **Negative** ("hard to make a case", "never a threat", "reverted to slow-starting"): **0 pts**

### B. Timeform Performance Flags (20% weight)
- `TimeformTopRated`: **+10 pts**
- `TimeformImprover`: **+5 pts**
- `TimeformHorseInFocus`: **+5 pts**

### C. Form Momentum & Dominance (30% weight)
1. **Exponential Time-Decay Performance (20 pts)**:
   For each run in `recentForm` (up to 5):
   - Calculate `DaysOld` = Current Date - Race Date.
   - Calculate `Weight = 0.5 ^ (DaysOld / 45)` (45-day half-life).
   - **Weighted Score** = `(Sum of (PercentileScore * Weight)) / (Sum of Weights) * 20`.
   - *If 0-1 races*: Default to 10 pts if `TimeformImprover` is TRUE, otherwise 5 pts.
   
2. **Dominance & Trend Bonus (10 pts)**:
   - **Dominance**: If `InPlayDominance` > 0.8 in most recent run: **+5 pts**.
   - **Improving Margin**: If `BeatenDistance[n] < BeatenDistance[n-1]`: **+5 pts**.
   - **Layoff Penalty**: If most recent run is >150 days ago: **-8 pts** (Reduce to **-3 pts** if expert says "should benefit from reappearance" or "strip fitter").

### D. Suitability & Connections (20% weight)
- **Well-Treated Bonus**: If expert mentions "eye-catching mark", "well-treated", "handicap mark", or "back to winning mark": **+8 pts**
- `SuitedByGoing`: **+5 pts**
- `SuitedByDistance`: **+5 pts**
- `TrainerInForm`: **+4 pts**
- `JockeyInForm`: **+3 pts**
- `JockeyWonOnHorse`: **+3 pts**

## Step 4: Field Competitive Strength

1. **Top Contender Count**: Number of runners with a Total Score > 65.
2. **Field Density**: If >3 runners are within 5 points of each other at the top, categorize as **ULTRA-COMPETITIVE**.
3. **Field Category**: WEAK (1 top contender), MODERATE (2-3), or STRONG (4+).

## Step 5: EV Calculation (Value Discovery)

1. **Estimated Win Probability (EP)**: `(Horse Total Score) / (Sum of all Horse Scores in Field)`.
2. **Market Implied Probability (IP)**: `1 / Current Decimal Odds`.
3. **Expected Value (EV)**: `((EP - IP) / IP) * 100`.

## Step 6: The "Timeform Edge" Adjustments

- **The "Improver" Multiplier**: If `TimeformImprover` is TRUE and `expertView` mentions "handicap debut" or "step up in trip": **Multiply EV by 1.20**.
- **The "Dominance" Factor**: If `InPlayDominance` = 1.0 (Won Easily) LTO but Odds are > 5.0: **Multiply EV by 1.15**.
- **The "Trap" Penalty**: If `expertView` mentions "slow-starting" or "disappointed" AND `HorseInForm` is FALSE: **Multiply EV by 0.70**.

## Step 7: Strategy Execution Framework

- **HIGH CONVICTION BACK**: EV > +15% AND Total Score > 75 AND Field is WEAK/MODERATE.
- **VALUE BACK**: +8% < EV ≤ +15% AND Total Score > 60.
- **LAY CANDIDATE**: EV < -15% AND Total Score < 40 AND Expert View is Negative.
- **NO ACTION**: EV between -8% and +8%.

## Step 8: Final Presentation

For each runner, display:
1. **Name & Price**
2. **Estimated Win Probability (EP)**: (Calculated % based on Score)
3. **Expert Sentiment**: [Summary of Expert View]
4. **Form Profile**: [Raw: Avg % | Decayed: Weighted %] (Trend: Improving/Stable/Declining)
5. **Dominance Rating**: (L2 Max In-Play Dominance)
6. **Adjusted EV (%)**
7. **Final Recommendation**: [BACK/LAY/NO ACTION]

---
**Execution:** Use `ExecuteBfexplorerStrategySettings` with the appropriate stake based on the Final Recommendation.
