# LLM Capabilities in Horse Racing Strategy Creation

**Document**: LLM-Capabilities-in-Strategy-Creation_CH45.md  
**Date**: April 30, 2026  
**Context**: Analysis based on RacingTvDataForHorses.json (Lingfield 5f Handicap)

---

## Executive Summary

LLMs can significantly contribute to horse racing strategy development on Betfair, particularly in **data analysis, pattern recognition, quantitative evaluation, and rule structuring**. However, they work best as co-analysts alongside domain expertise and historical validation, not as standalone prediction engines.

---

## Part 1: LLM Capabilities in Strategy Creation

### What LLMs Do Well ✓

**1. Statistical & Financial Calculations**
- Expected Value (EV) computation across odds and win probabilities
- Return on Investment (ROI) analysis from historical results
- Stake optimization and Kelly Criterion calculations
- Profit/loss projections under different betting scenarios
- Variance and standard deviation analysis

**2. Data Pattern Recognition**
- Identifying correlations between horse attributes (age, weight, days off, ratings)
- Trend analysis in form sequences
- Performance clustering (e.g., horses performing better at specific distances/tracks)
- Anomaly detection (horses showing unusual form changes)

**3. Form Evaluation & Logic**
- Systematic assessment of form lines using quantifiable metrics
- Class-change analysis and handicap rating evaluation
- Going impact assessment across performance history
- Jockey/trainer/stable change correlation analysis
- Consistency scoring and reliability metrics

**4. Strategic Rule Structuring**
- Converting qualitative insights into betting rules
- Hedge strategy design and exit logic
- Lay betting mechanics and back-up calculations
- Multi-leg strategy combinations (sequential execution)
- Risk management framework building

**5. Data Processing & Aggregation**
- Deriving new metrics (win%, place%, impact factors)
- Normalizing ratings across different systems
- Timeline analysis (distance beaten trends, velocity metrics)
- Historical comparison and performance binning

### What LLMs Cannot Do ✓

- **Real-time data access**: Cannot query live Betfair odds or in-play prices directly
- **Complex ML models**: Cannot train neural networks or build sophisticated prediction algorithms
- **Direct execution**: Cannot place bets or interface with exchanges
- **Perfect accuracy**: Occasional arithmetic errors on very complex calculations
- **Genuine predictive power**: Cannot forecast race outcomes better than domain experts with proper analysis
- **Causality discovery**: Can suggest correlations, not definitively prove causation

---

## Part 2: Case Study Analysis - RacingTvDataForHorses.json

### Available Data Richness

This JSON contains **exceptional data depth** for strategy development:

| Data Category | Elements | Strategic Value |
|---|---|---|
| **Market Context** | Market ID, start time, event type, race distance, track type, class | Filter & categorization rules |
| **Horse Attributes** | Age, sex, weight, days since last run, apprentice claim, focus flags | Base selection criteria |
| **Ratings** | Timeform star, rating, analyst comments, tips | Authority-weighted scoring |
| **Form String** | Recent 6-race finishing positions (e.g., "63-1252") | Quick trend analysis |
| **Race Statistics** | Wins, seconds, thirds, total races by race type | Career consistency metrics |
| **Performance History** | 19+ detailed race entries per horse including: | Multi-dimensional analysis |
| | Date, track, distance, class, going, weight, handicap rating | Track/distance affinity |
| | Finish position, field size, distance beaten | Performance scoring |
| | Starting price, in-play dominance | Price efficiency, momentum |
| | Race commentary (detailed analysis) | Qualitative signals |
| **Performance Metrics** | Percentile score, distance beaten cumulative | Relative strength |
| **Prompts** | "Finished 1st for Finishing Speed in 3 of last 4" | Pattern summary flags |

---

## Part 3: Potential Strategies from This Data

### Strategy 1: **Form & Consistency Handicapping**
**Thesis**: Horses with improving form and high consistency at current distances deserve higher backing.

**LLM Analysis Would Include**:
- Form line parsing (e.g., Tuscan Point's "63-1252" = recent strong form)
- Win frequency at exact distance (5f): Tuscan Point 1/19 overall, recently 2nd at 5f
- Seconds-to-wins ratio indicating if horse is "due"
- Class consistency (6f vs 5f performance variance)
- Days since last run effect (Tuscan Point 8 days, optimal recovery window)

**Quantifiable Score**:
```
Consistency Score = (Wins + 0.5×Seconds) / Total Races
- Tuscan Point: (1 + 0.5×3) / 19 = 0.237
- Honour Your Dreams: (5 + 0.5×7) / 36 = 0.180
- Wedgewood: (6 + 0.5×8) / 31 = 0.258 ← Strongest indicator
- Desdemona: (3 + 0.5×4) / 14 = 0.286 ← Best ratio, but smaller sample
```

**LLM Output**: Rule: *Back horses with Consistency Score > 0.24 AND recent 6-race form ≥ 2 placings*

---

### Strategy 2: **Class-to-Handicap Regression Analysis**
**Thesis**: Horses with superior Timeform ratings relative to handicap rating are undervalued.

**LLM Analysis Would Include**:
- Timeform rating vs. current handicap rating differential
- Class change impact (Desdemona: "upped in grade...sixth of 12" = class ceiling)
- Performance at current rating vs. historical performance at lower ratings
- Analyst comment sentiment analysis ("well treated", "vulnerable", "good mark")

**Quantifiable Score**:
```
Rating Advantage = Timeform_Rating - Current_Handicap_Rating
- Tuscan Point: 83 - 65 = +18 ← Significant positive gap
- Desdemona: 78 - 65 = +13 ← Moderate, but recent class struggles
- Honour Your Dreams: 75 - 61 = +14 ← Well-handicapped
- Wedgewood: 77 - 60 = +17 ← Good value
- She Went Whoosh: N/A (no rating) ← Red flag
```

**LLM Output**: Rule: *Back horses with Rating Advantage > +15 AND Timeform stars ≥ 3*

---

### Strategy 3: **Stable Form & Yard Momentum Indicator**
**Thesis**: Leverage stable efficiency patterns to identify momentum runners.

**LLM Analysis Would Include**:
- Trainer change detection (Tuscan Point: new to Adam Kirby, "bolted up")
- Win rate post-trainer change vs. historical
- Analyst comment keywords: "yard is going well", "stable debut", "first time blinkers"
- Recent performance clustering to identify stable-wide upswings

**LLM Output**: Rule: *Bonus points for trainers mentioned positively in analyst comments AND recent stable success*
- Wedgewood: "Yard is going well at present" ← +1 confidence
- Tuscan Point: New to current yard with instant success ← Monitor for regression or consolidation

---

### Strategy 4: **Distance & Going Affinity Profile**
**Thesis**: Horses perform better at their optimal distance/going combination; exploit overround when preferences mismatched.

**LLM Analysis Would Include**:
- Win rate at exact distance (5f) vs. overall
- Going performance matrix (good, soft, standard, firm)
- Track-specific form (Lingfield 5f track record)
- Distance trend: are they improving/declining with trip changes?

**Quantifiable Metrics**:
```
Tuscan Point at 5f:
- Recent: 1×2nd (8 days ago at Catterick 5f) = Good proximity
- Historical: Limited 5f data, but "similar form on 2 of 3 starts since"
- Going: Prefers good to firm ground (last race: "good to firm")
- Lingfield: No recent record visible in data

Wedgewood at 5f:
- Historical: 5f appears optimal trip (multiple wins), struggles at 6f+
- Performance: Wins at 5f (Lingfield win in March)
- Consistency: 3×1st over last 4 starts noted in prompts
```

**LLM Output**: Rule: *Back horses with ≥60% win rate at exact race distance AND compatible going*

---

### Strategy 5: **In-Play Dominance & Price Efficiency**
**Thesis**: In-play dominance metric reveals market-beating trades; compare SP to performance.

**LLM Analysis Would Include**:
- In-play dominance correlation to finishing position (does dominance predict wins?)
- Starting price accuracy (are big prices efficient or wrong?)
- Directional bias: horses trading below SP (undervalued) or above SP (overvalued)
- Recent price/performance trends

**Observable Pattern**:
```
Honour Your Dreams (Feb race):
- SP: 7/2 = 3.5
- In-play dominance: 1 (maximum ← led throughout)
- Result: 2nd
- Assessment: Led but couldn't hold on. Market was right about competitive race.

Tuscan Point (8 days ago):
- SP: 5
- In-play dominance: 0.98 (near maximum)
- Result: 2nd (0.1L beaten)
- Assessment: Led and nearly held. Market slightly underestimated.
```

**LLM Output**: Rule: *Monitor in-play dominance ≥0.8; these horses are executing well-backed strategies*

---

### Strategy 6: **Composite Scoring & Ranking System**
**Thesis**: Combine all factors into weighted scoring to identify "best backed" horse.

**Example Composite Score** (weights to be validated against historical data):

```
Selection Strength Score = 
  (0.25 × Consistency Score) +
  (0.20 × Rating Advantage) +
  (0.15 × Stability Bonus) +
  (0.20 × Distance/Going Fit) +
  (0.15 × Analyst Sentiment) +
  (0.05 × In-play Momentum)

Maximum 100 points

Estimated Rankings (for this race):
1. Wedgewood: ~72 (best consistency, good rating advantage, proven 5f winner)
2. Tuscan Point: ~68 (strong rating advantage, new trainer bounce, good recent form)
3. Honour Your Dreams: ~58 (well-treated but inconsistent, 64 days off)
4. Desdemona: ~55 (good rating advantage but class struggles, 36 days off)
5. She Went Whoosh: ~38 (no rating, poor form, 10 days notice)
```

---

## Part 4: LLM Approach to Data Analysis

### What I Would Do with This Data

**Step 1: Automated Data Extraction**
- Parse each horse's form string and performance array
- Extract numerical metrics into standard format
- Identify missing data (She Went Whoosh has no Timeform rating)
- Flag anomalies (track changes, going variations, weight fluctuations)

**Step 2: Comparative Analysis**
- Create head-to-head comparison tables
- Calculate percentile rankings within field (e.g., "Tuscan Point ranks 1st/5 for recent form")
- Identify performance clusters (e.g., "3 horses form tight group")
- Detect selection overlaps (horses that beat each other multiple times)

**Step 3: Pattern Synthesis**
- Identify which factors most consistently predict good performances
- Create decision trees: IF (age < 5) AND (consistency > 0.25) AND (days off < 30) THEN "backed"
- Calculate confidence levels based on data sample size
- Highlight contradictions (e.g., Desdemona: "back on track" vs. "vulnerable off this mark")

**Step 4: Historical Simulation**
- If I had historical results: Apply proposed rules to 50+ past races
- Calculate hit rate, ROI, Sharpe ratio
- Identify rule failings and refine thresholds
- Stress-test: Does rule work on all track types? All seasons?

**Step 5: Strategic Recommendation**
- Rank selections by expected value (EV)
- Suggest betting angles: Who to back? Who to lay? Sizes?
- Identify hedges or lay-bets to offset risk
- Recommend in-play adjustments if momentum changes

---

## Part 5: Real Limitations & Honest Assessment

### Acknowledged Weaknesses

1. **No True Prediction Power**: Even with rich analysis, I cannot forecast *race outcomes* more accurately than domain experts. My role is to:
   - Systematize expert intuition
   - Catch data inconsistencies
   - Quantify beliefs
   - Test rules against history

2. **Data Context Missing**: I don't have:
   - **Jockey skill**: Affect of rider changes (e.g., Morris taking over Wedgewood)
   - **Track bias**: Is Lingfield faster on rail or center?
   - **Field quality**: Are 9-runner fields different from 11-runner?
   - **Betting liquidity**: Can you actually back Honour Your Dreams at 7/2 again?
   - **Bookmaker overround**: How much is Betfair's takeaway?

3. **Temporal Dynamics**: Form can change dramatically:
   - Horses improve from one run to next (Tuscan Point: from 0-15 record to instant win)
   - Trainer effects are real but sometimes transient
   - Age-related decline isn't always visible in 4-race windows

4. **Narrative vs. Data**: Race comments reveal things numbers miss:
   - "carried head awkwardly" ← Temperament issue
   - "very easy to back" ← Market knew something
   - "given no peace up front" ← Race tactics matter
   - LLMs can extract these narratives but cannot weight their importance

5. **Overfitting Risk**: The more rules I add, the more likely they fit *this race* specifically but fail on others.

---

## Part 6: Recommended LLM-Powered Workflow

### For Strategy Development (Not Betting)

**Phase 1: Discovery** (LLM Strength)
```
Input: Raw race data (JSON)
↓
LLM Process:
- Extract all metrics automatically
- Calculate correlations
- Suggest 5-10 plausible rule sets
- Highlight contradictions and gaps
↓
Output: Structured analysis, ranked hypotheses
```

**Phase 2: Validation** (LLM + Human)
```
Input: Hypotheses from Phase 1 + Historical race database
↓
Human Role: Run rules against 50-100 past races manually or via code
LLM Role: Analyze results, identify failure patterns, suggest refinements
↓
Output: Tested rules with empirical win rates and ROI metrics
```

**Phase 3: Deployment** (Human-Driven)
```
Input: Validated rules + Live race data
↓
LLM Process:
- Score each selection against rules
- Calculate implied probabilities vs. Betfair odds
- Identify value opportunities (EV > 0)
- Suggest stake sizes (Kelly Criterion, fractional Kelly)
↓
Output: Betting recommendations with confidence levels
```

**Phase 4: Post-Race Analysis** (LLM + Human)
```
Input: Race results + Predictions
↓
LLM Process:
- Compare predictions vs. actual outcomes
- Identify prediction failures and rule violations
- Suggest refinements
↓
Output: Updated rule set, improved for next iteration
```

---

## Part 7: Specific Recommendations for This Race

### Analyst Verdict vs. Data-Driven View

**Timeform Comment**: *"TUSCAN POINT has made a positive start for Adam Kirby on the whole and gets the nod from Wedgewood, who looks the most obvious danger."*

**My Data-Driven Assessment**:

| Horse | Key Strength | Key Weakness | Overall Grade |
|---|---|---|---|
| **Wedgewood** | Proven 5f winner, best consistency (0.258), +17 rating advantage | 41 days off, form dip when upped in grade | **A-** |
| **Tuscan Point** | +18 rating advantage, new stable bounce (1st run = win), excellent 0.98 in-play | Only 1 win in 19 races, limited race experience | **A-** |
| **Honour Your Dreams** | Well-treated mark, good rating advantage | Inconsistent (64 days off), form declining | **B+** |
| **Desdemona** | Decent rating advantage | Recent class ceiling (6f = 6th), form inconsistent | **B** |
| **She Went Whoosh** | N/A | No Timeform rating, poor form (2-4-7-8-8-4), 10-day turnaround | **C-** |

**Consensus**: Data supports Timeform's lean toward **Tuscan Point & Wedgewood** as clear standouts. The field is relatively weak (Class 6, "very weak" pace), favoring consistent runners.

---

## Part 8: Conclusion

### Can LLMs Create Horse Racing Strategies?

**Yes, with caveats:**

✓ **LLMs excel at**:
- Quantifying form systematically
- Identifying patterns in available data
- Structuring betting rules and logic
- Calculating odds/EV relationships
- Rapid exploratory analysis of complex datasets

✗ **LLMs cannot**:
- Predict outcomes reliably (betting requires edge, not just good data)
- Replace domain expertise (racing knowledge is crucial)
- Execute automatically (human judgment on context essential)
- Backtest rules without historical data (offline analysis needed)

### Best Practice Model

**LLM as Research Assistant + Analyst Collaboration**:

1. Human brings racing domain knowledge
2. LLM brings analytical rigor and pattern extraction
3. Human provides historical validation and context
4. LLM systematizes the refined rules
5. Repeat until edge is identified

**Investment**: 
- 5-10 races worth of careful analysis = ~5-10 hours per human with LLM acceleration
- Expected output: 1-2 validated rules with >52% win rate or positive ROI
- Applicability: Sustainable only if rules aren't sportsbook-known already

### For Your Betfair Trading Project

**Recommendation**: Use LLMs to:
1. Parse Bfexplorer data automatically into analysis-ready format
2. Score selections against your validated rule set
3. Alert you to value opportunities (when rule score > odds)
4. Track predictions vs. outcomes for continuous refinement

**Do NOT use LLMs to**:
1. Make final betting decisions without review
2. Replace intuition on unusual circumstances (equipment changes, jockey injuries, etc.)
3. Bet on untested rules (always backtest first)
4. Assume past patterns predict future races (market evolves)

---

## References & Data Source

- **Input File**: `RacingTvDataForHorses.json`
- **Race**: Lingfield 5f Handicap, April 30, 2026, 17:05 GMT+2
- **Selections Analyzed**: 5 horses (Tuscan Point, Desdemona, Honour Your Dreams, Wedgewood, She Went Whoosh)
- **Data Depth**: 19-36 historical race performances per horse
- **Analysis Approach**: Systematic quantification of available attributes with strategic implications

---

**Document Status**: Complete  
**Confidence Level**: Medium-High (data-rich example, but untested rule validity)  
**Next Steps**: Backtest proposed strategies against 50+ historical races for validation
