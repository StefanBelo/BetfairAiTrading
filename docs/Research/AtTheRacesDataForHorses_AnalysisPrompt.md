# AtTheRacesDataForHorses – Complete Analysis Prompt

## Purpose
Retrieve `AtTheRacesDataForHorses` for the active market and produce a richly scored, ranked analysis table that infers each horse's chance of winning.

---

## Step 1 – Data Retrieval

```
1. Call GetActiveMarket → capture marketId, each selection's SelectionId, Name, Price.
2. Call GetAllDataContextForMarket
      dataContextNames: ["AtTheRacesDataForHorses"]
      marketId: <from step 1>
```

---

## Step 2 – Data Field Dictionary

Each selection exposes the following fields under `atTheRacesHorseData`:

| Field | Type | Meaning |
|---|---|---|
| `Form` | string | Recent finishing positions (newest right; `Won` = 1st; `-` = seasonal break) |
| `Rating` | int (0–140) | AtTheRaces numerical ability rating; higher = stronger horse |
| `StarRating` | int (1–5) | Editorial star rating; 5 = highest confidence pick |
| `ExpertView` | string | Free-text qualitative assessment by in-house analysts |
| `RecentForm[].FinishingPosition` | string | `N/M` = finished N out of M; `Won/M` = won out of M |
| `RecentForm[].SpeedRating` | float | Speed figure for that run; higher = faster/better performance |
| `RecentForm[].EarlySectional` | float (secs) | Time over first portion of race; **lower = faster early pace** |
| `RecentForm[].MidSectional` | float (secs) | Time over middle portion |
| `RecentForm[].LateSectional` | float (secs) | Time over finish portion; **lower = stronger late kick** |
| `RecentForm[].RunningStyle` | string | Pace profile: `Fast`=went out hard early; `Even`=settled; `Ev-Fs`=even then accelerated (best); `Ev-Sl`=even then faded (worst) |
| `RecentForm[].Racecourse` | string | Venue of that run |
| `RecentForm[].Distance` | string | Distance run (compare to today's race distance) |

---

## Step 3 – Scoring Framework

Compute the five sub-scores below, then a weighted **TotalScore (0–100)**.

### A. Rating Score (0–25 pts)
```
RatingScore = clip(Rating / 140, 0, 1) × 25
```

### B. Star Score (0–20 pts)
```
StarScore = (StarRating / 5) × 20
```

### C. Expert Sentiment Score (0–25 pts)
Parse the `ExpertView` text for sentiment direction:

**Positive keywords** (each scores +1 to +3):
- "solid claims", "in form", "trainer in form", "drew wide [positive context]", "chance", "progressive", "suited", "step up in trip [positive]", "drop in grade may help", "effective at", "won", "return to this level a plus"

**Negative keywords** (each scores –1 to –3):
- "below form", "poor", "regressive", "very poor", "bit to find", "others look stronger", "beaten", "didn't stay", "never in it", "slow start", "well beaten", "held", "finishing down the field"

**Neutral / conditional** (+0):
- "if bouncing back", "may suit", "might help", "in and out of form"

**Intensity labels and sentiment mapping:**
| Category | Sentiment Score raw | ExpertScore pts |
|---|---|---|
| Very Positive | ≥ +4 net | 25 |
| Positive | +2 to +3 net | 20 |
| Mixed-Positive | +1 net | 15 |
| Neutral | 0 net | 10 |
| Negative | –1 to –2 net | 5 |
| Very Negative | ≤ –3 net | 0 |

### D. Speed Figure Score (0–20 pts)
Using last 3 completed runs (SpeedRating > 0):
```
AvgSpeed    = mean(last3 SpeedRatings)
FieldMaxAvg = max(AvgSpeed) across all runners
SpeedScore  = (AvgSpeed / FieldMaxAvg) × 20
```

### E. Sectional / Running Style Score (0–10 pts)
Evaluate the last 3 runs with valid sectionals:

- Start at 5 pts  
- +2 if RunningStyle contains `"Ev-Fs"` (strong finisher) in ≥ 2 of 3 runs  
- –2 if RunningStyle contains `"Ev-Sl"` (fader) in ≥ 2 of 3 runs  
- +2 if LateSectional is **below** the horse's own average LateSectional (improving finisher)  
- –2 if LateSectional is **above** its own average by > 1.5 sec (tiring)  
- +1 if horse has run the **exact same distance and track** as today within last 6 months  

Clamp to 0–10.

### F. TotalScore
```
TotalScore = RatingScore + StarScore + ExpertScore + SpeedScore + SectionalScore
```
Range: 0–100.

### G. Inferred Win Probability (normalized)
```
RawProb_i     = TotalScore_i  (unnormalized)
WinProb_i     = RawProb_i / sum(RawProb_j for all j)   × 100%
ImpliedOdds_i = 100 / WinProb_i
```

---

## Step 4 – Additional Qualitative Flags

Apply these as annotations in the final table (they do NOT adjust the score but flag known X-factors):

| Flag | Trigger |
|---|---|
| 🔵 **Track specialist** | ≥ 2 wins at today's racecourse in RecentForm |
| 🟢 **Trainer in form** | "trainer in form" mentioned in ExpertView |
| 🟡 **New headgear** | "blinkers first time", "cheekpieces first time", or similar in ExpertView |
| 🔴 **Regressive** | "regressive" or "very poor for some time" in ExpertView |
| ⚫ **Return from break** | Form string contains `-` or last run > 90 days ago |
| 🟠 **Class drop** | "drop in grade", "return to this level" in ExpertView |
| ⚪ **Distance concern** | "bit to find", "didn't stay", "down in trip" with no positive resolution |

---

## Step 5 – Output Table

Present results sorted by **TotalScore descending**.

```
| Horse | Odds | Rating | ★ | ExpertSentiment | AvgSpeedRating | RunStyle | TotalScore | WinProb% | ImpliedOdds | Flags | Key Inference |
```

Column definitions:
- **ExpertSentiment**: one of Very Positive / Positive / Mixed-Positive / Neutral / Negative / Very Negative
- **AvgSpeedRating**: mean of last 3 valid speed figures (rounded to 1 dp)
- **RunStyle**: dominant RunningStyle tag from last 3 runs
- **TotalScore**: 0–100 composite
- **WinProb%**: normalized % from TotalScore
- **ImpliedOdds**: 100/WinProb%, round to 1 dp
- **Flags**: emoji flags from Step 4
- **Key Inference**: 1-sentence rationale distilled from ExpertView + sectionals

---

## Step 6 – Decision Signal

After the table, add a brief signal block:

```
## Signal Summary
Best TotalScore: <Horse> (<score>)
Market Odds: <odds>  |  Model Implied Odds: <implied>
Edge direction: [BACK candidate if Market Odds > ImpliedOdds + 10%]
                [LAY candidate  if Market Odds < ImpliedOdds – 10%]
                [No edge otherwise]
```

---

## Notes on Data Limitations
- `SpeedRating = 0` means the figure was not recorded; exclude from averages.
- Sectionals of `0,0,0` indicate historical runs without timing data; skip for RunningStyle scoring.
- `Rating` and `StarRating` are AtTheRaces proprietary; they reflect class/form as of today and may not fully account for today's conditions (ground, draw, headgear).
- The `ExpertView` is written by a single analyst and carries inherent subjectivity; treat it as one signal, not gospel.
