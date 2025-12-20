# LBBW Horse Racing Market Report Prompt

Concise specification for producing and persisting a single JSON report describing runner scores, value, and recommendations for an active Betfair horse racing market using the LBBW (Last–Best–Base–Weight) framework plus jockey factor.

---
## 1. Objective
Generate ONE JSON object (or `{"error":"Insufficient data"}`) containing: per-runner LBBW component scores (0–5), composite score, implied odds, value assessment, and recommendation set; then persist it.

## 2. Required Data Retrieval (Run Exactly Once Per Evaluation)
1. Call `GetActiveMarket` → capture `marketId`, `marketName`, selections (id, name).
2. Call `GetAllDataContextForMarket` with dataContextNames:
   `["DbJockeysResults","DbHorsesResults","RacingpostDataForHorses","TimeformDataForHorses"]`
3. Build in‑memory maps keyed by `selectionId` (and jockey identifiers). DO NOT call again per runner (guardrail). Only repeat if deliberate timed refresh.

## 3. Data Processing & Metrics Foundation

**A. Form & Recency Analysis (Exponential Smoothing)**

For each horse, analyze recent race history with emphasis on last 3-5 races:

1. **Base Performance Value (Pi):**
   - Primary rating: Use `racingpostRating` if > 0, else fall back to `officialRating`
   - Beaten distance penalty: `min(beatenDistance, 10) * 2`
   - Position bonus: Win = +6, Place (2nd/3rd) = +3, else 0
   - Formula: `Pi = rating - penalty + bonus`

2. **Exponential Smoothing (α=0.55):**
   - Apply to last 5 performances to weight recent form heavily
   - `S0 = P0; Si = α*Pi + (1-α)*S(i-1)`
   - Latest smoothed value = `S_last` (recency indicator)

3. **Peak Performance Metrics:**
   - `peakRPR` = highest valid racingpostRating from last 8 races
   - `peakTS` = highest topSpeed from last 8 races
   - `officialTrend` = linear regression slope of officialRating (last 6 valid ratings)
   - Look for improvement trajectories (positive trend = rising form)

**B. Qualitative Analysis (Sentiment & Context)**

1. **Racing Post Comment Sentiment:**
   - Analyze race descriptions from `racingpostHorseData.lastRaces[].raceDescription`
   - Positive indicators (+2 each, cap +6 total):
     * "travelled strongly", "travelled well", "travelled smoothly"
     * "ran on", "ran on well", "kept on well", "stayed on"
     * "gamely", "fought back", "battled"
   - Negative indicators (-2 each, cap -6 total):
     * "weakened", "weakening", "no extra"
     * "hung left", "hung right", "hung badly"
     * "pulled hard", "pulled too hard"
     * "lost action", "lost position"
   - `sentimentScore = positiveTotal - negativeTotal` (range: -6 to +6)

2. **Timeform Strategic Flags:**
   - Extract from `timeformHorseData` and assign weighted bonuses:
     * `timeformTopRated`: +4 (highest rated in race)
     * `horseInForm` OR `horseWinnerLastTimeOut`: +3
     * `suitedByGoing`, `suitedByCourse`, `suitedByDistance`: +1 each (cap +3 for all three)
     * `timeformImprover`: +2
     * `timeformHorseInFocus`: +1
   - Sum applicable flags to create `timeformBonusTotal`

**C. Jockey Performance Factor**

1. **Jockey Form Analysis:**
   - Process `jockeyResults` with same exponential smoothing (α=0.55)
   - Ride score: `(position==1 ? 5 : position in [2,3] ? 3 : 1) - (beatenDistance>10 ? 1 : 0)`
   - Apply smoothing across last 10-20 rides
   - Normalize: `jockeyScore = SmoothedRideScore / 5` (range: 0-1)

2. **Jockey-Horse Compatibility:**
   - Check `isRiddenByJockey` flag in results
   - Previous wins/places with this horse add confidence

## 4. LBBW Component Scoring (0-5 Scale)

Use the processed metrics to calculate each component with analytical judgment:

**1. Last (Recency & Current Form) - 0-5:**
- Base calculation: `lastScore = clamp((S_last - 40)/15, 0, 5)`
- **Analytical adjustments:**
  * Recent win (last 3 races): consider +0.5
  * Positive trend (`officialTrend > 0`): consider +0.3
  * Multiple consecutive places: consider +0.2
  * Long absence (>60 days) without trial: consider -0.5
- Captures: momentum, fitness, current trajectory

**2. Best (Peak Performance) - 0-5:**
- Base calculation: `bestScore = clamp((peakRPR - 70)/10, 0, 5)`
- **Analytical adjustments:**
  * Peak achieved recently (<5 races back): more reliable
  * High topSpeed alongside high RPR: confirms quality
  * Peak in similar conditions (class/distance): more relevant
  * Downward trend since peak: reduce confidence
- Captures: proven ceiling, class level

**3. Base (Consistency & Reliability) - 0-5:**
- Base calculation: `baseScore = max(0, 5 - clamp(sd(Pi last 5)/5, 0, 5))`
- **Analytical adjustments:**
  * Multiple top-3 finishes: boost score
  * Wildly inconsistent (big swings): reduce score
  * Consistent in similar conditions: boost confidence
  * Age factor: young improvers vs. established performers
- Captures: predictability, reliability as foundation

**4. Weight (Handicap Impact) - 0-5:**
- Determine weight tier relative to field (top 25% = high)
- **Scoring rules:**
  * High weight + recent place (1-3 in last 3): 3 → normalize to 5.0 (proven at weights)
  * Neutral weight or mixed results: 2 → normalize to 3.33
  * Low weight or struggling: 1 → normalize to 1.67
- **Analytical considerations:**
  * Weight drop from last run: positive factor
  * Carrying penalty weight: requires strong form
  * Class drop with weight advantage: significant edge
- Captures: handicap advantage/disadvantage

**5. Bonus (Context & Conditions) - 0-5:**
- Base calculation: `bonusContribution = timeformBonusTotal + sentimentScore/2`
- Range: -4 to +8, then normalize: `bonusScore = clamp(((bonusContribution + 4)/12)*5, 0, 5)`
- **Key factors weighted:**
  * `timeformTopRated` (+4): Highest rated is significant
  * Recent winner or in-form (+3): Strong momentum
  * Suited by conditions (+1-3): Course/distance/going fit
  * Positive race comments: Traveled well, stayed on
  * Negative patterns: Weakened, hung, pulled hard
- Captures: race-specific advantages, running style fit

**Missing Data Handling:**
- If component data is insufficient, impute with mean of available components for that runner
- Flag incomplete data in notes
- Reduce confidence for runners with limited data

## 5. Synthesis & Market Context

**A. Composite LBBW Score:**
- Base formula: `LBBW_Raw = (lastScore + bestScore + baseScore + weightScore + bonusScore) / 5`
- Jockey-adjusted: `CompositeLBBW = round((LBBW_Raw * 0.8) + (jockeyScore * 5 * 0.2), 2)`
- Score interpretation:
  * **≥ 4.5:** Elite contender, strong favorite material
  * **4.0-4.49:** High-quality runner, major threat
  * **3.5-3.99:** Solid chance, live contender
  * **3.0-3.49:** Moderate chance, each-way possibility
  * **2.5-2.99:** Outsider with some merit
  * **< 2.5:** Limited chance based on form

**B. Win Probability & Ranking:**

**CRITICAL: Probabilities must sum to 100% across all horses in the race.**

1. **Normalize CompositeLBBW scores to probabilities:**
   - Calculate sum: `totalLBBW = Σ(CompositeLBBW for all horses in race)`
   - For each horse: `winProbability_i = CompositeLBBW_i / totalLBBW`
   - Verify: `Σ(winProbability_i) = 1.0` (100%)

2. **Convert probability to implied odds:**
   - `impliedOdds_i = 1 / winProbability_i`
   - This represents the fair odds based purely on form analysis

3. **Ranking categories:**
   - Based on `winProbability`:
     * ≥ 30%: **Strong Favorite** (dominant chance)
     * 20-29%: **Favorite** (clear contender)
     * 12-19%: **Contender** (live chance)
     * 6-11%: **Outsider** (possible surprise)
     * < 6%: **Long Shot** (limited chance)

**Example (3-horse race):**
- Horse A: CompositeLBBW = 4.0
- Horse B: CompositeLBBW = 3.0
- Horse C: CompositeLBBW = 2.0
- Total = 9.0
- Horse A: 4.0/9.0 = 44.4% → impliedOdds = 2.25 → **Strong Favorite**
- Horse B: 3.0/9.0 = 33.3% → impliedOdds = 3.0 → **Favorite**
- Horse C: 2.0/9.0 = 22.2% → impliedOdds = 4.5 → **Contender**
- Verification: 44.4% + 33.3% + 22.2% = 100% ✓

## 6. Selection Strategy & Recommendations

**A. Selection Criteria (Pure Form-Based):**
- **Top Selection:** Highest `CompositeLBBW` with `winProbability ≥ 20%`
- **Strong Contenders:** `CompositeLBBW ≥ 3.5` with `winProbability ≥ 12%`
- **Each-way Prospects:** `CompositeLBBW ≥ 3.0` with consistent base scores

**B. Confidence Scaling:**
- Per-selection confidence: `(CompositeLBBW / 5) * 10` (range 0-10)
- Overall confidence: Based on separation between top selections
- High confidence: Top horse's probability exceeds second by ≥10%
- Moderate confidence: Top 2-3 horses within 5% probability range

**C. Ranking Approach:**
- Present horses ordered by `winProbability` (highest first)
- Highlight key differentiators in notes
- Flag horses with improving trends or favorable conditions

## 7. Output JSON Schema

Generate ONE complete JSON report containing all analysis. Structure exactly as follows:

```json
{
  "contenders": [
    {
      "selectionId": string,
      "horseName": string,
      "lastScore": number,         // 0-5, with analytical adjustments
      "bestScore": number,         // 0-5
      "baseScore": number,         // 0-5
      "weightScore": number,       // 0-5
      "bonusScore": number,        // 0-5
      "finalLBBWScore": number,    // CompositeLBBW with jockey factor
      "winProbability": number,    // Normalized probability (0-1)
      "impliedOdds": number,       // 1 / winProbability
      "ranking": string,           // "Strong Favorite" | "Favorite" | "Contender" | "Outsider" | "Long Shot"
      "sentimentScore": number,    // -6 to +6 from race comments
      "jockeyScore": number,       // 0-1 normalized jockey form
      "notes": string              // ≤180 chars expert rationale
    }
  ],
  "recommendations": {
    "topSelections": [
      {
        "horseName": string,
        "selectionType": string,   // "TOP_PICK" | "STRONG_CONTENDER" | "EACH_WAY"
        "confidence": number,      // 0-10 scaled from CompositeLBBW
        "winProbability": number,  // Percentage (0-100)
        "impliedOdds": number,     // Fair odds from form
        "rationale": string        // Short expert reasoning
      }
    ],
    "confidenceLevel": number,     // Overall confidence in top pick
    "strategyNotes": string        // Summary of form analysis, key insights, race dynamics
  }
}
```

**Validation Requirements:**
1. All numeric fields must be actual numbers (not strings)
2. All component scores and finalLBBWScore within [0, 5]
3. **Sum of all winProbability values across all horses must equal 1.0 (100% ±0.1% rounding tolerance)**
4. winProbability values between 0 and 1, impliedOdds ≥ 1.0
5. timeformFlags array contains only boolean-true flag names
6. Notes field maximum 180 characters
7. ranking field matches probability bands defined in Section 5.B
8. If insufficient data for analysis: `{"error": "Insufficient data"}` with explanation
9. Empty recommendations allowed: `topSelections: []` with explanatory strategyNotes

## 8. Persistence & Output

**CRITICAL: DO NOT display JSON data in output. Perform all analysis silently and persist only.**

**A. Silent Processing:**
1. Generate complete JSON report internally as specified in Section 7
2. Validate against all requirements (Section 7)
3. DO NOT emit, display, or show the JSON to user
4. Proceed directly to persistence

**B. Persistence (Required):**
1. Call `SetAIAgentDataContextForMarket` with:
   - `dataContextName = "LBBWData"`
   - `marketId = <from GetActiveMarket>`
   - `jsonData = <exact JSON string generated>`
2. Verify successful persistence (status: true)

**C. User Communication (After Persistence):**
- Provide brief summary only:
  * Market name and number of runners
  * Top selection name with win probability %
  * Confidence level
  * Key insight (one sentence about field quality or standout factor)
- Example: "Analysis complete for 7f Nursery (9 runners). Top pick: Crown The Future (15.6% probability). Confidence: 5.2/10. Weak competitive field with no dominant contender."

**D. Error Handling:**
- If mandatory data missing: generate `{"error": "Insufficient data", "reason": "<explanation>"}`
- Persist error JSON
- Inform user: "Insufficient data for analysis: <reason>"

## 9. Execution Workflow Summary

**Sequential Steps:**
1. **Data Retrieval** (Section 2): Single bulk fetch of all contexts (no market prices)
2. **Data Processing** (Section 3): Apply exponential smoothing, sentiment analysis, extract metrics
3. **Component Scoring** (Section 4): Calculate LBBW scores with analytical adjustments for each horse
4. **Synthesis** (Section 5): 
   - Compute composite scores for all horses
   - **Normalize across entire field** to create probability distribution (must sum to 100%)
   - Derive implied odds from normalized probabilities
   - Assign ranking categories based on win probability
5. **Recommendations** (Section 6): Generate form-based selection strategy
6. **JSON Generation** (Section 7): Assemble complete JSON report internally (ordered by winProbability descending)
7. **Persistence** (Section 8): Save to Bfexplorer via `SetAIAgentDataContextForMarket` - DO NOT display JSON
8. **User Summary** (Section 8.C): Provide brief 2-3 sentence summary only

## 10. Key Principles & Guardrails

**Analytical Integrity:**
- Balance mechanical calculations with expert judgment
- Use formulas as foundation, apply context-aware adjustments
- Document reasoning in notes fields (≤180 chars)
- Confidence scales with data quality and convergence

**Determinism & Reproducibility:**
- Same input data → same base calculations
- Analytical adjustments follow consistent logic
- Document any judgment calls in notes
- Track data quality issues

**Risk Management:**
- Never force recommendations from weak data
- Acknowledge market wisdom (crowd intelligence)
- Scale confidence with conviction level
- Prefer precision over volume in recommendations

END OF SPECIFICATION