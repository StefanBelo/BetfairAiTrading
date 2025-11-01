# Horse Racing Expected Value Analysis (Favourite R5) - Competitive Field Assessment Edition

This prompt guides the analysis of horse racing data using both Timeform's professional racing analysis system and Racing Post's comprehensive racing data to calculate the Expected Value (EV) for each horse in an active Betfair market, with a specific focus on evaluating the market favourite's performance relative to the competitive strength of the entire field for enhanced betting decisions.

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

## Step 4: Field Competitive Strength Analysis

**NEW: Enhanced Field Assessment**

Before calculating individual EVs, perform a comprehensive field strength analysis:

### Field Quality Metrics:
1. **Average Field Rating**: Calculate the mean combined score of all non-favourite runners
2. **Top Opposition Count**: Count horses with combined scores within 80% of the favourite's score
3. **Quality Depth**: Count horses with 3+ Timeform stars OR RP Rating > 90
4. **In-Form Opposition**: Count non-favourite horses with recent wins (last 3 runs)
5. **Course Specialists**: Count non-favourite horses suited by course conditions

### Field Competitiveness Categories:
- **WEAK FIELD** (Favourite Advantage): 
  - Average field rating < 60% of favourite's score
  - ≤1 horse within 80% of favourite's score
  - ≤2 horses with 3+ stars or RP Rating > 90
  
- **MODERATE FIELD** (Balanced Competition):
  - Average field rating 60-75% of favourite's score
  - 2-3 horses within 80% of favourite's score
  - 3-5 horses with quality ratings
  
- **STRONG FIELD** (Tough Competition):
  - Average field rating > 75% of favourite's score
  - ≥4 horses within 80% of favourite's score
  - ≥6 horses with quality ratings

### Opposition Threat Assessment:
For each non-favourite horse, calculate a "Threat Level" to the favourite:

**High Threat** (Dangerous Rivals):
- Combined score ≥ 90% of favourite's score
- Recent winner with positive form trend
- Course/distance advantages over favourite

**Medium Threat** (Competitive Rivals):
- Combined score 70-89% of favourite's score
- Recent good form or course experience
- Some advantages in conditions

**Low Threat** (Outsiders):
- Combined score < 70% of favourite's score
- Poor recent form
- Unsuited by conditions

## Step 5: Calculate Implied Probability and Expected Value (EV)

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

## Step 6: Apply Field-Adjusted Combined Data Adjustments

Apply additional adjustments based on both data sources AND field strength:

### Quality Multipliers:
- If `timeformTopRated` AND Racing Post Rating > 90: Multiply EV by 1.20
- If `timeformTopRated` OR Racing Post Rating > 100: Multiply EV by 1.15
- If `timeformImprover` AND recent improvement in semantic analysis: Multiply EV by 1.12
- If both `horseWinnerLastTimeOut` and `horseInForm` are true: Multiply EV by 1.08
- If strong semantic indicators in recent `raceDescription` ("made all", "went clear", "easily"): Multiply EV by 1.05

### Course/Distance/Going Synergy:
- If all suitability factors from Timeform are positive: Multiply EV by 1.10
- If semantic analysis shows consistent strong finishes: Multiply EV by 1.08

### **NEW: Field Strength Adjustments for Favourite:**
- **WEAK FIELD**: Multiply favourite's EV by 1.15 (easier win opportunity)
- **MODERATE FIELD**: No adjustment (1.00)
- **STRONG FIELD**: Multiply favourite's EV by 0.85 (tougher competition)

### **NEW: Opposition Threat Adjustments for Favourite:**
- **3+ High Threat opponents**: Additional ×0.90 multiplier
- **5+ Medium Threat opponents**: Additional ×0.95 multiplier
- **Multiple in-form recent winners**: Additional ×0.90 multiplier
- **Favourite has significant advantages**: Additional ×1.10 multiplier

### Risk Adjustments:
- If none of the suitability factors are true: Multiply EV by 0.85
- If `ratingStars` ≤ 2 AND Racing Post Rating < 70: Multiply EV by 0.80
- If semantic analysis shows consistent poor performance language: Multiply EV by 0.80
- If long layoff (> 200 days in recent runs): Multiply EV by 0.90
- If poor recent form (no win/place in last 5 runs) AND negative semantic indicators: Multiply EV by 0.75

## Step 7: Present the Enhanced Results

Present the analysis in an enhanced table format:

### Field Strength Summary:
- **Field Category**: [WEAK/MODERATE/STRONG]
- **Average Opposition Rating**: [X points]
- **High Threat Count**: [X horses]
- **Quality Depth**: [X horses with 3+ stars or RP Rating > 90]

### Detailed Results Table:
| Horse Name | Price | Combined Score | Threat Level | Calculated EV | Key Indicators |
|------------|-------|----------------|--------------|---------------|----------------|
| **FAVOURITE** | **X.X** | **XXX** | **N/A** | **±XX%** | **Summary** |
| Rival 1 | X.X | XXX | High/Med/Low | ±XX% | Summary |

**Important:** Identify the market favourite and highlight field strength analysis for focused betting decision.

## Step 8: Enhanced Favourite-Focused Betting Strategy

Identify the market favourite (horse with the lowest decimal odds) and apply the enhanced decision tree:

### Enhanced Favourite Analysis Criteria:
1. **Positive EV**: Greater than +10% (adjusted for field strength)
2. **Data Agreement**: Both Timeform and Racing Post semantic analysis should support the selection
3. **Minimum Quality**: At least 3 Timeform stars OR RP Rating > 75 (if available)
4. **Form Requirement**: Recent positive form from either source AND positive semantic indicators
5. **Field Advantage**: Favourite must have clear advantages over the field
6. **Confidence Level**: Must be Medium or High confidence

### Enhanced Favourite Betting Decision Logic:

**BACK THE FAVOURITE ("Bet 10 Euro") if ALL of the following are true:**
- Favourite has positive EV > +10% (after field adjustments)
- Both Timeform and Racing Post semantic analysis support the selection
- Meets minimum quality criteria (3+ stars OR RP Rating > 75)
- Has recent positive form AND positive semantic indicators
- **WEAK or MODERATE field** (not STRONG field)
- **≤2 High Threat opponents**
- Confidence level is Medium or High
- No major contradictions between data sources

**BACK THE FAVOURITE ("Bet 10 Euro") - Special Case if ALL of the following are true:**
- Favourite has exceptional quality (5 stars AND RP Rating > 120)
- Dominant recent form (multiple recent wins with strong semantic analysis)
- **STRONG field** but favourite's combined score is >150% of average field score
- Positive EV > +5% (lower threshold for exceptional favourites)

**LAY THE FAVOURITE ("Lay 10 Euro") if ANY of the following are true:**
- Favourite has negative EV < -5%
- **STRONG field** with ≥3 High Threat opponents
- Poor semantic analysis (consistent negative performance language)
- Low confidence level due to conflicting data sources
- Poor recent form despite being favourite
- Odds too short (< 1.5) relative to true probability estimate
- **Multiple in-form recent winners** in opposition with positive EVs
- All other cases not meeting the BACK criteria

### **NEW: Field-Specific Strategy Enhancements:**

**WEAK FIELD Strategy:**
- More aggressive backing of quality favourites
- Lower EV threshold (+8%) for backing
- Focus on favourites with class advantages

**MODERATE FIELD Strategy:**
- Standard approach with careful threat assessment
- Standard EV threshold (+10%) for backing
- Balance quality vs field strength

**STRONG FIELD Strategy:**
- Conservative approach, higher laying tendency
- Higher EV threshold (+15%) for backing
- Focus on exceptional favourites only
- Consider laying shorter-priced favourites facing strong opposition

### Enhanced Favourite Quality Checks:
- Compare favourite's combined score against field average and top opposition
- Assess if the favourite's odds accurately reflect both individual merit and field strength
- Look for value discrepancies created by field strength misassessment
- Consider if high-threat opposition horses offer better value than the favourite

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

## Step 9: Enhanced Risk Management

### Field-Aware Risk Assessment:
- **Competitive Balance**: In STRONG fields, even quality favourites can be vulnerable
- **Class Differentials**: In WEAK fields, class often prevails despite poor recent form
- **Market Efficiency**: STRONG fields often produce more accurate market pricing

### Enhanced Confidence Levels:
- **High Confidence**: Both sources strongly agree, clear field advantages, consistent semantic analysis
- **Medium Confidence**: Sources generally agree, some field advantages, mostly consistent patterns
- **Low Confidence**: Sources disagree, unclear field position, or conflicting analysis

### **NEW: Field-Specific Risk Factors:**
- **WEAK FIELD Risks**: Overconfidence in favourite, ignoring improving outsiders
- **MODERATE FIELD Risks**: Misjudging competitive balance, overthinking close decisions
- **STRONG FIELD Risks**: Underestimating favourite's class, overvaluing opposition

### Advanced Favourite-Specific Risk Factors:
- **False favourites in strong fields**: Market leader without clear superiority
- **Class advantage masking poor form**: High-rated horse in declining form
- **Course/distance specialists**: Opposition horses with specific advantages
- **Trainer/Jockey form cycles**: Connections hitting peak or trough periods

This enhanced favourite-focused approach provides a sophisticated method for evaluating the market leader within the context of competitive field strength, significantly improving betting decision accuracy by considering both individual merit and relative field position.
