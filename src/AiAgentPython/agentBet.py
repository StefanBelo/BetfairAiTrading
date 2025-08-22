import asyncio
from mcp_agent.core.fastagent import FastAgent

# Create the application
fast = FastAgent("Bfexplorer Assistant")

# Define the agent
@fast.agent(name="BfexplorerApp",
    instruction="You are a helpful AI Agent executing betting/trading strategies on bfexplorer.",
    model="deepseek-chat",
    #model="generic.openai/gpt-4.1",
    #model="generic.openai/gpt-5.0-mini",
    servers=["BfexplorerApp"]
)
async def main():
    async with fast.run() as agent:
        await agent(
"""
This prompt guides the analysis of horse racing data using both Timeform's professional racing analysis system and Racing Post's comprehensive racing data to calculate the Expected Value (EV) for each horse in an active Betfair market, with a specific focus on evaluating the market favourite's performance relative to the competitive strength of the entire field for enhanced betting decisions. **SILENT MODE: Executes strategies without generating reports or status outputs. All analysis must be performed internally without any visible output until strategy execution.**

## Step 1: Retrieve Active Market Data

First, identify the active horse racing market using the `GetActiveBetfairMarket` tool. This will provide the `marketId` and a list of selections (horses) with their current odds.

## Step 2: Retrieve Detailed Horse Information

With the `marketId` from the previous step, use the `GetAllDataContextForBetfairMarket` tool with `dataContextNames` set to `['TimeformDataForHorsesInfo', 'RacingpostDataForHorsesInfo']`. This will return comprehensive analysis for each horse combining:

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

**Note:** Fields such as topspeedRating, recentForm, courseWins, distanceWins, goingWins, jockeyStats, trainerStats, draw, weight, age, and breeding are NOT present in RacingpostDataForHorsesInfo. Only the above fields are reliably available. Adjust all analysis and scoring logic to use only these fields.

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
Not available in RacingpostDataForHorsesInfo. Do not use these factors.

### Connections Score (15% weight):
**Timeform Connections (8% weight):**
- `trainerInForm`: +4 points
- `trainerCourseRecord`: +3 points
- `jockeyInForm`: +3 points
- `jockeyWonOnHorse`: +2 points

**Racing Post Statistics (7% weight):**
Not available in RacingpostDataForHorsesInfo. Do not use these factors.

### Special Designations & Quality Factors (5% weight):
- `timeformTopRated`: +5 points
- `timeformImprover`: +3 points
- `timeformHorseInFocus`: +2 points

**Note:** Only Timeform special designations are available. Do not use `topspeedRating` or `age` from RacingpostDataForHorsesInfo.

**Total Combined Score**: Sum all applicable points (maximum possible: 255 points with semantic analysis)
**Important:** The maximum possible score may be lower in practice due to missing Racing Post fields. Adjust scoring and interpretation accordingly.

## Step 4: Field Competitive Strength Analysis

**Enhanced Field Assessment**

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
  - <=1 horse within 80% of favourite's score
  - <=2 horses with 3+ stars or RP Rating > 90
  
- **MODERATE FIELD** (Balanced Competition):
  - Average field rating 60-75% of favourite's score
  - 2-3 horses within 80% of favourite's score
  - 3-5 horses with quality ratings
  
- **STRONG FIELD** (Tough Competition):
  - Average field rating > 75% of favourite's score
  - >=4 horses within 80% of favourite's score
  - >=6 horses with quality ratings

### Opposition Threat Assessment:
For each non-favourite horse, calculate a "Threat Level" to the favourite:

**High Threat** (Dangerous Rivals):
- Combined score >=90% of favourite's score
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

### Field Strength Adjustments for Favourite:
- **WEAK FIELD**: Multiply favourite's EV by 1.15 (easier win opportunity)
- **MODERATE FIELD**: No adjustment (1.00)
- **STRONG FIELD**: Multiply favourite's EV by 0.85 (tougher competition)

### Opposition Threat Adjustments for Favourite:
- **3+ High Threat opponents**: Additional x0.90 multiplier
- **5+ Medium Threat opponents**: Additional x0.95 multiplier
- **Multiple in-form recent winners**: Additional x0.90 multiplier
- **Favourite has significant advantages**: Additional x1.10 multiplier

### Risk Adjustments:
- If none of the suitability factors are true: Multiply EV by 0.85
- If `ratingStars` <=2 AND Racing Post Rating < 70: Multiply EV by 0.80
- If semantic analysis shows consistent poor performance language: Multiply EV by 0.80
- If long layoff (> 200 days in recent runs): Multiply EV by 0.90
- If poor recent form (no win/place in last 5 runs) AND negative semantic indicators: Multiply EV by 0.75

## Step 7: Internal Analysis (No Output)

**CRITICAL: Perform all calculations internally without generating visible output:**
- Calculate combined scores for all horses
- Determine field strength category
- Calculate threat levels for opposition horses
- Calculate Expected Values with all adjustments
- Identify market favourite (lowest decimal odds)
- Apply all EV range filters and decision logic
- **DO NOT OUTPUT any analysis, scores, calculations, or reasoning**

## Step 8: EV Range Strategy Framework

### EV Execution Thresholds:

**BACK Strategy Execution Range:**
- **Minimum EV for BACK**: +8% (Conservative backing threshold)
- **Maximum EV for BACK**: +50% (Upper limit to avoid outlier scenarios)

**LAY Strategy Execution Range:**
- **Minimum EV for LAY**: -8% (Conservative laying threshold - horse slightly overvalued)
- **Maximum EV for LAY**: -100% (No upper limit - more negative = better LAY opportunity)

**NO ACTION ZONE:**
- **EV between -8% and +8%**: No strategy executed (insufficient edge)
- **EV beyond +50%**: No strategy executed (potentially unreliable calculations for BACK)

### EV Range Decision Logic:

**1. Calculate Final Adjusted EV** (after all field and quality adjustments)

**2. Apply EV Range Filter:**
```
If Adjusted EV >= +8% AND Adjusted EV <= +50%:
    Consider BACK strategy (subject to additional criteria)
Else If Adjusted EV <= -8%:
    Consider LAY strategy (subject to additional criteria - more negative = better)
Else:
    NO ACTION - EV outside actionable range
```

**3. If within actionable EV range, proceed to enhanced betting criteria**

## Step 9: Silent Favourite-Focused Strategy Execution

**SILENT EXECUTION ONLY: No analysis output, no explanations, no reasoning visible to user**

**Only proceed with strategy evaluation if EV is within actionable range (+/-8% to +/-50%)**

Identify the market favourite (horse with the lowest decimal odds) and apply the enhanced decision tree internally:

### Enhanced Favourite Analysis Criteria:
1. **EV Range Requirement**: Must be within +/-8% to +/-50% range
2. **Positive EV**: Greater than +8% for backing consideration
3. **Data Agreement**: Both Timeform and Racing Post semantic analysis should support the selection
4. **Minimum Quality**: At least 3 Timeform stars OR RP Rating > 75 (if available)
5. **Form Requirement**: Recent positive form from either source AND positive semantic indicators
6. **Field Advantage**: Favourite must have clear advantages over the field
7. **Confidence Level**: Must be Medium or High confidence

### Silent Favourite Betting Decision Logic:

**BACK THE FAVOURITE if ALL of the following are true:**
- **EV Range**: +8% <=EV <=+50%
- Favourite has positive EV > +8% (after field adjustments)
- Both Timeform and Racing Post semantic analysis support the selection
- Meets minimum quality criteria (3+ stars OR RP Rating > 75)
- Has recent positive form AND positive semantic indicators
- **WEAK or MODERATE field** (not STRONG field)
- **<=2 High Threat opponents**
- Confidence level is Medium or High
- No major contradictions between data sources

**BACK THE FAVOURITE - Special Case if ALL of the following are true:**
- **EV Range**: +5% <=EV <=+50% (lower threshold for exceptional favourites)
- Favourite has exceptional quality (5 stars AND RP Rating > 120)
- Dominant recent form (multiple recent wins with strong semantic analysis)
- **STRONG field** but favourite's combined score is >150% of average field score
- Positive EV > +5% (lower threshold for exceptional favourites)

**LAY THE FAVOURITE if ALL of the following are true:**
- **EV Range**: EV <=-8% (more negative = better LAY opportunity)
- Favourite has negative EV < -8%
- At least ONE of the following additional criteria:
  - **STRONG field** with >=3 High Threat opponents
  - Poor semantic analysis (consistent negative performance language)
  - Low confidence level due to conflicting data sources
  - Poor recent form despite being favourite
  - Odds too short (< 1.5) relative to true probability estimate
  - **Multiple in-form recent winners** in opposition with positive EVs

**NO ACTION if ANY of the following are true:**
- **EV is between -8% and +8%** (insufficient edge)
- **EV is beyond +50%** (potentially unreliable calculation for BACK)
- BACK criteria not fully met AND LAY criteria not fully met
- Data quality concerns (missing critical information)
- Conflicting signals from multiple sources

### Field-Specific EV Range Adjustments:

**WEAK FIELD Strategy:**
- Lower EV threshold for backing: +6% (easier win opportunity)
- Higher EV threshold for laying: -10% (less likely to lay in weak fields - need more overvaluation)

**MODERATE FIELD Strategy:**
- Standard EV thresholds: +8% for BACK, -8% for LAY
- Balanced approach

**STRONG FIELD Strategy:**
- Higher EV threshold for backing: +10% (tougher competition)
- Lower EV threshold for laying: -6% (more opportunities to lay - less overvaluation needed)

### Silent Strategy Execution:

**If BACK criteria are met AND EV in range:**
Execute using the `ExecuteBfexplorerStrategySettings` tool with:
- `marketId`: The ID of the active market
- `selectionId`: The ID of the favourite
- `strategyName`: "Bet 10 Euro"
**No output or confirmation message**

**If LAY criteria are met AND EV in range:**
Execute using the `ExecuteBfexplorerStrategySettings` tool with:
- `marketId`: The ID of the active market
- `selectionId`: The ID of the favourite
- `strategyName`: "Lay 10 Euro"
**No output or confirmation message**

**If NO ACTION:**
Do not execute any strategy. No output or message.

## Step 10: Silent Risk Management

### EV Range Risk Assessment (Internal):
- **Conservative Range (+8%/-8%)**: Reduces false positives and overconfident betting
- **Maximum Range (+50%)**: Prevents BACK action on potentially erroneous calculations
- **No Upper Limit for LAY**: More negative EV = better LAY opportunity
- **Neutral Zone (-8% to +8%)**: Recognizes markets where edge is insufficient

### Enhanced Confidence Levels (Internal):
- **High Confidence**: Both sources strongly agree, clear field advantages, consistent semantic analysis, EV >15% or <-15%
- **Medium Confidence**: Sources generally agree, some field advantages, mostly consistent patterns, EV 8-15% or -8% to -15%
- **Low Confidence**: Sources disagree, unclear field position, or conflicting analysis, requires higher EV thresholds

### Advanced Risk Factors (Internal Assessment):
- **False favourites in strong fields**: Market leader without clear superiority
- **Class advantage masking poor form**: High-rated horse in declining form
- **Course/distance specialists**: Opposition horses with specific advantages
- **Trainer/Jockey form cycles**: Connections hitting peak or trough periods
- **EV calculation reliability**: Ensure sufficient data quality for accurate EV estimation

**SILENT MODE**: This strategy performs all analysis and executes appropriate betting actions without generating any visible reports, status messages, or analytical output. The system operates entirely in the background, making informed betting decisions based on the comprehensive EV analysis framework while maintaining complete operational silence.

**EXECUTION INSTRUCTIONS FOR AI:**
1. Retrieve market data using the specified tools
2. Perform ALL calculations internally without any output
3. Apply decision logic silently
4. Execute strategy if criteria are met OR take no action
5. **NEVER explain reasoning, show calculations, or provide analysis output**
6. **ONLY execute the strategy tool call if betting criteria are satisfied**
"""
        )

if __name__ == "__main__":
    asyncio.run(main())
