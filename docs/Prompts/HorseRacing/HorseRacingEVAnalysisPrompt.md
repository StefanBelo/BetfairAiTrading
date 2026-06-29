---
title: "Horse Racing EV Analysis Prompt"
aliases: ["Horse Racing EV Analysis Prompt"]
type: prompt
tags: [ev-analysis, horse-racing, market-sentiment, prompt]
data_contexts: [RacingpostDataForHorses, TimeformDataForHorses]
---

# Horse Racing EV Analysis Prompt

## 🎯 Objective
Produce an Expected Value (EV) view for every runner in a Betfair horse race market using market odds, Timeform indicators, and Racing Post form notes.

## 🔌 Data Retrieval
```
1) Tool: get_active_market
   → Returns marketId, market metadata, runner odds

2) Tool: get_all_data_context_for_market
   Parameters:
     • marketId (from step 1)
     • dataContextNames: ["TimeformDataForHorses", "RacingpostDataForHorses"]
   → Returns Timeform booleans/ratings + Racing Post race descriptions per runner
```

## 📦 Input Snapshot
- Market: id, name, start time, runner odds
- Horse Data: Timeform `ratingStars` + boolean flags, Racing Post detailed race history:
  - `officialRating`: Current official handicap rating
  - `rpRating`: Racing Post's own rating
  - `lastRaces[]`: Array of recent races (latest first) containing:
    - `position`: Finishing position
    - `beatenDistance`: Lengths beaten by winner
    - `lastRunInDays`: Days since this race
    - `raceDescription`: Detailed running commentary
    - `topspeed`: Speed figure achieved
    - `weightCarried`: Weight carried (lbs)
    - `distance`: Race distance (metres)

## 🧮 Scoring Stack
### 1) Form Score (0–100)
- Base = (TimeformStars×20 + OfficialRating + RPRating) ÷ 3  
- Recency add-ons (based on `lastRunInDays` of most recent run):
  | 0–7 | 8–14 | 15–21 | 22–35 | 36+ |
  | --- | ---- | ----- | ----- | --- |
  | +5  | +2   | 0     | -3    | -5  |
- Speed adjustments (based on recent `topspeed` figures):
  - Consistent 50+ topspeed in last 3 runs: +5
  - Improving topspeed trend: +3
  - Declining topspeed trend: -3

### 2) Indicator Boosts
| Signal | Δ |
| --- | --- |
| horseInForm | +12 |
| suitedByGoing | +10 |
| timeformTopRated | +15 |
| trainerInForm | +8 |
| jockeyInForm | +6 |
| suitedByCourse / Distance | +8 each |
| jockeyWonOnHorse | +5 |
| horseBeatenFavouriteLTO | −8 |
| Not suited conditions (each) | −5 |

### 3) Text Sentiment (per race comment)
| Theme | Positive | Negative | Weight |
| --- | --- | --- | --- |
| Finish | "kept on", "ran on", "strong to line" | "weakened", "no extra", "faded" | ±8 |
| Progress | "headway", "made ground" | "never involved", "always behind" | +6 / −6 |
| Position | "led", "prominent", "disputed" | "towards rear" | +5 / −5 |
| Excuses | "hampered", "short of room", "switched" | "off feed", "lost many lengths start" | +3 / −8 |
| Equipment | "first-time headgear" (if improved) | "ran green" (flag future improvement) | +2 / context |

*Prioritise the most recent comment; older notes decay in influence.*

### 4) Trend & Class Context
- Recent run `position` improving (e.g., 8th→5th→3rd) → +10  
- `beatenDistance` margins shrinking in recent runs → +8
- Declining position trend → −10  
- Consistent top-3 finishes in last 3 runs → +5  
- Weight changes: dropping 5+ lbs from recent `weightCarried` → +5
- Distance suitability: previous wins/places at similar `distance` → +8

## 📈 Probability & EV
1. **Total Score** = Form Score + Indicator Boosts + Text Sentiment + Trend Adjustments  
   - Re-scale to 0–100 if needed using min–max across field.
2. **True Probability** = TotalScore ÷ Σ(TotalScore for all runners)
3. **Market Probability** = 1 ÷ DecimalOdds
4. **EV** = TrueProbability × (Odds − 1) − (1 − TrueProbability); present as percentage: **EV %** = EV × 100
5. **Confidence**
   - High: aligned positive signals + consistent trend  
   - Medium: mixed signals or sparse data  
   - Low: strong negatives or high uncertainty

## 🧭 Workflow Checklist
1. Fetch market + odds (tool 1)  
2. Fetch horse contexts (tool 2)  
3. Compute scores and probabilities per runner  
4. Compare with market odds to flag positive EV  
5. Summarise insights and confidence

## 📝 Output Template
Summarise the full field in a Markdown table so the EV picture is easy to scan. Include one row per runner sorted by descending EV; if EV ties, sort by descending true probability.

```
| Horse | Odds | Form Score | Positives | Negatives | True Prob | Market Prob | EV % | Recommendation | Confidence |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| Example Runner | 4.6 | 58 | "kept on well", trainer in form | off feed LTO | 18% | 21% | -25% | Avoid | Medium |
```

After the table, add a short narrative paragraph that highlights 2–3 key value angles (overlays/underlays, trends, confidence drivers).

## 🧠 Semantic Guidance
- Extract intent from `raceDescription` even when vocabulary is new: classify outcome (strong/weak finish), trip issues, positioning, energy usage, excuses, or red flags.
- Give more weight to comments that explain *why* a result happened (pace, traffic, ground, equipment).
- Treat veterinary or health negatives as strong red flags unless subsequent runs refute them.
- Highlight potential improvement angles (ran green, first run, equipment tweaks).
- Cross-reference `raceDescription` with `position`, `beatenDistance`, and `topspeed` for context validation.
- Weight recent runs more heavily - `lastRunInDays` under 30 should have stronger influence than older form.

## ✅ Final Instruction
Apply the framework consistently across all runners, emphasising discrepancies between modelled probability and market price to surface actionable value bets.