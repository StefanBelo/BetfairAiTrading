# Horse Racing Expected Value Analysis (Favourite R4) - Timeform & Racing Post Combined Edition

This prompt guides the analysis of horse racing data using both Timeform's professional racing analysis system and Racing Post's comprehensive racing data to calculate the Expected Value (EV) for each horse in an active Betfair market, with a specific focus on the market favourite for betting decisions.

## Step 1: Retrieve Active Market Data

First, identify the active horse racing market using the `GetActiveMarket` tool. This will provide the `marketId` and a list of selections (horses) with their current odds.

## Step 2: Retrieve Detailed Horse Information

With the `marketId` from the previous step, use the `GetDataContextForMarket` tool with `dataContextNames` set to `['TimeformDataForHorses', 'RacingpostDataForHorses']`. This will return comprehensive analysis for each horse combining:

### Timeform Analysis (timeformHorseData field):
- `ratingStars`: Timeform's star rating system (1-5 stars, where 5 is highest quality)
- `horseWinnerLastTimeOut`: Boolean indicating if the horse won its last race
- `horseInForm`: Boolean indicating if the horse is currently in good form
- `horseBeatenFavouriteLTO`: Boolean indicating if the horse beat the favorite last time out
- `suitedByGoing`: Boolean indicating if current track conditions suit the horse
- `suitedByCourse`: Boolean indicating if the course suits the horse
- `suitedByDistance`: Boolean indicating if the race distance suits the horse
- `trainerInForm`: Boolean indicating if the trainer is currently in good form
- `trainerCourseRecord`: Boolean indicating if the trainer has a good record at this course
- `jockeyInForm`: Boolean indicating if the jockey is currently in good form
- `jockeyWonOnHorse`: Boolean indicating if the jockey has won on this horse before
- `timeformTopRated`: Boolean indicating if this is Timeform's top-rated horse in the race
- `timeformImprover`: Boolean indicating if Timeform expects improvement from this horse
- `timeformHorseInFocus`: Boolean indicating if this horse is featured in Timeform's focus

### Racing Post Analysis (racingpostHorseData field):
- `lastRacesDescriptions`: Array of recent race results, each with:
    - `beatenDistance`: Margin behind winner (numerical value, 0 for wins)
    - `lastRunInDays`: Days since last run (numerical value)
    - `position`: Finishing position (numerical value, 1 for wins, 0 for non-finishers)
    - `raceDescription`: Detailed text summary of race performance requiring semantic analysis
- `officialRating`: The horse's official handicap rating (if available)
- `rpRating`: Racing Post rating (if available)

**Important:** The `raceDescription` field contains rich textual data that requires semantic analysis to extract performance insights. Look for key phrases indicating:
- **Positive indicators**: "made all", "led", "won", "kept on well", "ran on", "always doing enough", "comfortably", "easily", "went clear", "good headway"
- **Negative indicators**: "weakened", "no extra", "always behind", "never dangerous", "eased", "pulled up", "tailed off", "detached"
- **Running style**: "prominent", "held up", "towards rear", "made all", "held up in last"
- **Trouble in running**: "hampered", "short of room", "denied clear run", "hung left/right", "stumbled", "slowly away"
- **Jockey comments**: Often in parentheses providing additional context

**Note:** Fields such as topspeedRating, recentForm, courseWins, distanceWins, goingWins, jockeyStats, trainerStats, draw, weight, age, and breeding are NOT present in RacingpostDataForHorses. Only the above fields are reliably available. Adjust all analysis and scoring logic to use only these fields.

## Step 3: Combined Data Analysis

Perform a comprehensive analysis using both data sources with the following enhanced scoring system:

### Base Rating Score (35% weight):
**Timeform Stars (15% weight):**
- **5 Stars**: 30 points
- **4 Stars**: 24 points
- **3 Stars**: 18 points
- **2 Stars**: 12 points
- **1 Star**: 6 points

**Racing Post Rating (20% weight):**
If `rpRating` is available:
    - RP Rating > 100: 40 points
    - RP Rating 90-100: 32 points
    - RP Rating 80-89: 24 points
    - RP Rating 70-79: 16 points
    - RP Rating < 70: 8 points
If not available, use 0 points.

### Form Analysis Score (30% weight):
**Timeform Form (15% weight):**
- `horseWinnerLastTimeOut`: +15 points
- `horseInForm`: +10 points
- `horseBeatenFavouriteLTO`: +5 points

**Racing Post Form (10% weight):**
Recent win (position == 1 in any of last 3 `lastRacesDescriptions`): +10 points
Recent place (position == 2 or 3 in any of last 3): +5 points
Recent run (`lastRunInDays` < 30 in any of last 3): +5 points
Long layoff (`lastRunInDays` > 100 in all last 3): -5 points

**Racing Post Semantic Analysis (additional 5% weight):**
Analyze `raceDescription` text for each of the last 3 runs:
- Strong performance language ("made all", "won easily", "kept on well", "went clear"): +3 points per race
- Positive finishing ("ran on", "stayed on", "kept on", "nearest finish"): +2 points per race
- Trouble in running that may excuse poor performance ("hampered", "denied clear run", "short of room"): +1 point per race
- Poor performance language ("weakened", "no extra", "never dangerous", "always behind"): -2 points per race
- Non-completion or serious issues ("pulled up", "unseated", "fell", "refused"): -3 points per race

### Course & Distance Suitability Score (20% weight):
**Timeform Suitability (10% weight):**
- `suitedByGoing`: +5 points
- `suitedByCourse`: +5 points
- `suitedByDistance`: +5 points

**Racing Post Statistics (10% weight):**
Not available in RacingpostDataForHorses. Do not use these factors.

### Connections Score (15% weight):
**Timeform Connections (8% weight):**
- `trainerInForm`: +4 points
- `trainerCourseRecord`: +3 points
- `jockeyInForm`: +3 points
- `jockeyWonOnHorse`: +2 points

**Racing Post Statistics (7% weight):**
Not available in RacingpostDataForHorses. Do not use these factors.

### Special Designations & Quality Factors (5% weight):
- `timeformTopRated`: +5 points
- `timeformImprover`: +3 points
- `timeformHorseInFocus`: +2 points

**Note:** Only Timeform special designations are available. Do not use `topspeedRating` or `age` from RacingpostDataForHorses.

**Total Combined Score**: Sum all applicable points (maximum possible: 255 points with semantic analysis)
**Important:** The maximum possible score may be lower in practice due to missing Racing Post fields. Adjust scoring and interpretation accordingly.

## Step 4: Calculate Implied Probability and Expected Value (EV)

1. **Estimate 'True' Probability:** Convert the Combined Score to a probability:
   - Normalize the Combined Score: `Normalized Score = (Combined Score / 255) * 100`
   - Calculate market share: For each horse, divide its Normalized Score by the sum of all horses' Normalized Scores
   - This gives you the estimated true probability for each horse

2. **Calculate Implied Probability:** For each horse, calculate the implied probability from their Betfair odds using the formula:
   `Implied Probability = 1 / Decimal Odds`

3. **Calculate Expected Value (EV):** For each horse, calculate the EV using the normalized formula:
   
   ```
   If True Probability >= Implied Probability:
       EV = ((True Probability - Implied Probability) / Implied Probability) * 100
   Else:
       EV = ((True Probability - Implied Probability) / True Probability) * 100
   
   Then apply bounds: EV = max(-100, min(100, EV))
   ```

## Step 5: Apply Combined Data Adjustments

Apply additional adjustments based on both data sources:

### Quality Multipliers:
- If `timeformTopRated` AND Racing Post Rating > 90: Multiply EV by 1.20
- If `timeformTopRated` OR Racing Post Rating > 100: Multiply EV by 1.15
- If `timeformImprover` AND recent improvement in semantic analysis: Multiply EV by 1.12
- If both `horseWinnerLastTimeOut` and `horseInForm` are true: Multiply EV by 1.08
- If strong semantic indicators in recent `raceDescription` ("made all", "went clear", "easily"): Multiply EV by 1.05

### Course/Distance/Going Synergy:
- If all suitability factors from Timeform are positive: Multiply EV by 1.10
- If semantic analysis shows consistent strong finishes: Multiply EV by 1.08

### Risk Adjustments:
- If none of the suitability factors are true: Multiply EV by 0.85
- If `ratingStars` ≤ 2 AND Racing Post Rating < 70: Multiply EV by 0.80
- If semantic analysis shows consistent poor performance language: Multiply EV by 0.80
- If long layoff (> 200 days in recent runs): Multiply EV by 0.90
- If poor recent form (no win/place in last 5 runs) AND negative semantic indicators: Multiply EV by 0.75

## Step 6: Present the Results

Present the analysis in a simplified table format with the following columns for each horse:
- Horse Name
- Price (Current Odds)
- Calculated EV (after adjustments)
- Key Indicators Summary (Short description of semantic analysis from recent race descriptions)

**Important:** Identify the market favourite (horse with the lowest odds) and highlight this selection for focused betting decision analysis.

## Step 7: Favourite-Focused Betting Strategy

Identify the market favourite (horse with the lowest decimal odds) and apply the following decision tree:

### Favourite Analysis Criteria:
1. **Positive EV**: Greater than +10% (lower threshold for favourites due to their market position)
2. **Data Agreement**: Both Timeform and Racing Post semantic analysis should support the selection
3. **Minimum Quality**: At least 3 Timeform stars OR RP Rating > 75 (if available)
4. **Form Requirement**: Recent positive form from either source (recent win/place) AND positive semantic indicators
5. **Confidence Level**: Must be Medium or High confidence

### Favourite Betting Decision Logic:

**BACK THE FAVOURITE ("Bet 10 Euro") if ALL of the following are true:**
- Favourite has positive EV > +10%
- Both Timeform and Racing Post semantic analysis support the selection
- Meets minimum quality criteria (3+ stars OR RP Rating > 75)
- Has recent positive form AND positive semantic indicators
- Confidence level is Medium or High
- No major contradictions between data sources

**LAY THE FAVOURITE ("Lay 10 Euro") if ANY of the following are true:**
- Favourite has negative EV < -5%
- Poor semantic analysis (consistent negative performance language)
- Low confidence level due to conflicting data sources
- Poor recent form despite being favourite
- Odds too short (< 1.5) relative to true probability estimate
- All other cases not meeting the BACK criteria

### Enhanced Favourite Quality Checks:
- Compare favourite's combined score against the field average
- Assess if the favourite's odds accurately reflect the calculated true probability
- Look for value discrepancies that suggest the market has over-bet or under-bet the favourite
- Consider if other horses in the race have significantly higher EV that might threaten the favourite

### Execution:

**If BACK criteria are met:**
Execute using the `ExecuteBfexplorerStrategySettings` tool with:
- `marketId`: The ID of the active market
- `selectionId`: The ID of the favourite
- `strategyName`: "Bet 10 Euro"

**If LAY criteria are met (or BACK criteria not met):**
Execute using the `ExecuteBfexplorerStrategySettings` tool with:
- `marketId`: The ID of the active market
- `selectionId`: The ID of the favourite
- `strategyName`: "Lay 10 Euro"

**Always execute one of these strategies** - there is no "no bet" option in this favourite-focused approach.

## Step 8: Risk Management

### Enhanced Safeguards for Favourite Strategy:
- Consider the strength of the opposition when assessing favourite value
- Be cautious of false favourites (horses with short odds but poor underlying fundamentals)
- Factor in market efficiency - favourites in competitive handicaps may be harder to find value in

### Confidence Levels:
- **High Confidence**: Both sources strongly agree, multiple positive indicators, consistent semantic analysis
- **Medium Confidence**: Sources generally agree, some positive indicators, mostly consistent semantic patterns
- **Low Confidence**: Sources disagree, limited positive indicators, or conflicting semantic analysis

### Favourite-Specific Risk Factors:
- **Over-bet favourites**: Market sentiment may have driven odds too low
- **Class drops**: Favourite dropping in class may be over-valued
- **Trainer/Jockey changes**: New connections may affect performance
- **Track bias**: Course conditions may not suit the favourite's running style

This favourite-focused approach provides a systematic method for evaluating the market leader while maintaining the comprehensive analysis framework of the combined Timeform and Racing Post data sources.
