# Advanced Betfair Horse Racing Market Analysis Prompt

## OBJECTIVE
Analyze all horses in the active Betfair horse racing market to identify value bets, lay opportunities, and potential rebounders using comprehensive data analysis.

---

## EXECUTION STEPS

### STEP 1: Retrieve Market & Data
1. Call `get_active_market` to identify the current race
2. Call `get_all_data_context_for_market` with these data contexts:
   - `AtTheRacesDataForHorses`
   - `RacingpostDataForHorses`
   - `TimeformDataForHorses`

### STEP 2: Calculate Composite Score (0-100) for Each Horse

**Scoring Components:**
- **Recent Form (30 points):** Last 3-4 runs, positions, beaten distances
- **Performance Ratings (25 points):** Topspeed figures, class ratings
- **Conditions Suitability (20 points):** Distance/going/course fit from Timeform flags
- **Connections (15 points):** Trainer/jockey form, course records, winning partnerships
- **Momentum (10 points):** Progression, improvement trends, ratings trajectory

**Score Adjustments:**
- **Add 5-10 points** for valid excuses in recent runs:
  - Unsuitable ground, wrong distance, interference, unseated rider
  - Self-certificate/pulled-up with documented reason
  - First-time blinkers/cheekpieces or gear changes
- **Deduct 5-10 points** for negative patterns:
  - Consistent weakening/stopping
  - Temperament issues (hanging, running green)
  - Significant rating drops

### STEP 3: Identify Contextual Flags

For each horse, mark applicable flags:

**Positive Flags:**
- ✓ Winner Last Time Out (LTO)
- ✓ Potential Rebounder (class/ratings > recent form)
- ✓ Valid Excuse Last Run (specify: ground/trip/interference)
- ✓ Gear Change First Time (blinkers/cheekpieces/tongue-tie)
- ✓ Trainer In Form + Course Record
- ✓ Jockey In Form + Won On Horse
- ✓ Suited By Conditions (distance/going/course)
- ✓ Class Improver / Step Down In Grade
- ✓ Progressive Profile (improving ratings)

**Warning Flags:**
- ⚠️ Unsuitable Conditions (distance/going/course per Timeform)
- ⚠️ Trainer/Jockey NOT In Form
- ⚠️ Quick Turnaround (<14 days)
- ⚠️ Untested Conditions (new distance/surface)
- ⚠️ Temperament Concerns (hanging/running green)
- ⚠️ Step Up In Class/Grade
- ⚠️ Heavy Weight / Weight Increase

### STEP 4: Market Value Analysis

For each horse, compare:
1. **Composite Score vs Odds:** Identify mismatches
2. **Expected Odds Range:** Based on score (e.g., 85+ = 2.0-4.0, 70-84 = 4.0-8.0)
3. **Value Classification:**
   - **VALUE BACK:** Score significantly higher than odds suggest
   - **OVERBET LAY:** Score lower than odds suggest (especially favorites)
   - **EACH-WAY VALUE:** Consistent placer at 10.0+ odds
   - **AVOID:** Score matches odds (no edge)

### STEP 5: Generate Strategy Table

| Horse | Score | Key Flags | Odds | Strategy | Stake % | Rationale/Counter |
|-------|-------|-----------|------|----------|---------|-------------------|
| [Name] | [0-100] | [2-3 critical flags] | [Current odds] | [BACK WIN/LAY/E-W/TRADE] | [0.5-3.0%] | [Why/Why not] |

**Strategy Types:**
- **BACK WIN:** High confidence, clear value
- **BACK WIN/PLACE:** Value but some uncertainty
- **EACH-WAY:** Consistent placer, outsider odds
- **LAY:** Overbet, vulnerabilities exposed
- **TRADE:** In-play green-up opportunity
- **SAVER:** Small stake on potential surprise
- **AVOID:** No edge or unclear picture

**Stake Sizing Guidelines:**
- **3.0%:** Exceptional value (score 80+, odds 15.0+)
- **2.0-2.5%:** Strong value (score 75+, clear edge)
- **1.0-1.5%:** Moderate value or each-way plays
- **0.5%:** Speculative savers
- **Lay liability:** 1.0% max per horse

### STEP 6: Priority Betting Actions Section

**Structure:**
1. **PRIMARY VALUE PLAYS (3 max):**
   - Rank by confidence: star ratings ⭐⭐⭐
   - List score, odds, key reasons
   - State automation approach if applicable
   - Highlight main risk factor

2. **LAY OPPORTUNITIES (1-2 max):**
   - Identify overbet favorites
   - Explain vulnerabilities
   - Suggest liability limit

3. **EACH-WAY/SAVER PLAYS (2-3 max):**
   - Longshots with place potential
   - Consistent placers undervalued

### STEP 7: Risk Management Summary

**Required Elements:**
- **Total Race Exposure:** Sum all stake percentages (max 8-10%)
- **Exposure Breakdown:** By strategy type
- **Dynamic Pre-Race Refinements:**
  - Price movement triggers (e.g., "If X drifts to Y, increase stake to Z")
  - Condition changes (ground updates, non-runners)
- **In-Play Management:**
  - When to green-up trades
  - When to exit positions
  - Monitoring points

### STEP 8: Challenge Your Analysis

**REQUIRED:** For top-rated horse, answer:
1. **Why might this horse lose?** (specific vulnerabilities)
2. **Which lower-scored horse could upset?** (scenario analysis)
3. **What market assumption might be wrong?** (contrarian view)

**Hidden Value Check:**
- Identify "rebound" candidates (poor LTO with valid excuse)
- Flag horses with superior class/ratings masked by recent form
- Highlight first-time gear changes that could transform

---

## Example Output Structure

---

## OUTPUT FORMAT

### Race Header
```
Race: [Venue] [Distance] [Race Type] | Start: [Time]
```

### Main Analysis
For EACH horse (ordered by odds, favorite first):
1. **Header:** Horse Name (Odds: X.X)
2. **Composite Score:** XX/100
3. **Form Analysis:** 3-4 sentences covering recent runs, topspeed, progression
4. **Contextual Flags:** Bullet list with ✓ and ⚠️ symbols
5. **Market Position:** Value classification with brief explanation
6. **Vulnerabilities:** 2-3 specific concerns

### Strategy Table
[Full table as specified in Step 5]

### Priority Actions
[As specified in Step 6]

### Risk Management
[As specified in Step 7]

### Hidden Value & Challenges
[As specified in Step 8]

---

## CRITICAL RULES

1. **ALWAYS identify "rebound" candidates** with valid excuses
2. **ALWAYS challenge the top-scored horse** with counterarguments
3. **NEVER recommend total exposure >10%** of bankroll per race
4. **ALWAYS explain automation logic** for trading strategies
5. **PRIORITIZE hidden value over obvious favorites**
6. **USE data from ALL three sources** (ATR, Racingpost, Timeform)
7. **FLAG untested conditions** (new ground, distance, first-time gear)
8. **COMPARE scores to odds** to find mismatches
9. **INCLUDE specific stake percentages** (not ranges)
10. **PROVIDE actionable, executable recommendations**

---

## SUCCESS CRITERIA

✅ All horses analyzed with scores 0-100  
✅ Valid excuses identified and scored appropriately  
✅ Value/lay opportunities clearly flagged  
✅ Specific stake sizes assigned  
✅ Automation logic provided where relevant  
✅ Risk management within 8-10% total exposure  
✅ Top pick challenged with alternative scenarios  
✅ Hidden value candidates highlighted  

---