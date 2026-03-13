# Research: Fine Form Master Formula

This document summarises ideas and concepts drawn from the UK Betting Forum thread
"The Fine Form Master Formula" (April 2016) and interprets them in the
context of quantitative betting strategy development.

## Background

The Fine Form Master Formula (FFMF) is presented as a rigid system for race
selection that assigns a rating up to 20 (+ half/plus for ties) based on a
combination of factors:

* **Top 3 in the betting forecast** – derived from popular tip sheets or market
  lists.
* **Last two outings in the current season** – emphasis on recent form.
* **Course and distance winners** – extra credit for proven ability at the
  specific track or trip.
* **Handicap rating** – top three runners by official rating.

A rating is produced for every runner; stronger horses score higher.  The system
does not publicise all the exact rules but this set of criteria forms the
core, with the author later adding several filters to reduce bet volume.


## Key Strategy Elements

### 1. Systematic vs. Gut-driven Betting

The thread opens with a defence of systems: a *rational punter* follows
inflexible rules, backing selections regardless of emotions.  The FFMF serves
as the framework, producing candidates from which final stakes might be drawn.
System discipline is seen as especially valuable when results are poor, because
it prevents the gambler from chasing losses or abandoning the method.

### 2. Filters to Improve Strike-rate

Jackform introduces several pragmatic filters to cut losing bets:

* avoid races run on extreme going
* only class 4 or above
* race must contain at least one winner in the last two outings
* at least 80 % of the field must have a handicap rating
* maximum field size of 12 (smaller fields improve value)

These rules narrow the selection universe and target more standardised,
handicap‑type events where ratings and form carry weight.

### 3. Betting Value and Expected Price

A recurring theme is the need to bet **only when the market price is longer than
the true chance**.  The author stresses that the average punter must avoid
short-priced favourites; instead, they should seek overlays where implied odds
exceed estimated probability.

Practical points include:

* smaller fields often yield lower bookmaker overrounds; value is more likely
  to occur there
* derive rough “probability percentages” for runners based on field size and
  assigned numbers (e.g. favourite ≈25 % in a 12‑runner handicap)
* monitor early market prices and note those that appear to offer value

### 4. Expected Value and Probability

Jackform reiterates the mathematical expected value (EV) formula:

```
EV = P(win) × (odds) − P(loss) × 1
```

Overlay bets (positive EV) are the only long‑term winners.  He reminds readers
that probabilities must be rooted in statistical fact, not hunches, and that
failure to estimate true chances accurately undermines staking plans.

### 5. Staking Discussion

A significant portion of the thread debates staking plans:

* **Level stakes** is considered the baseline; any system that loses on level
  stakes is to be discarded.
* **Optimised staking** (Kelly‑like) is described with caution.  The Racedata
  Modelling Ltd plan allocates stakes as a percentage of the bank according to
  the bettor’s edge.  However, because true edge is unknowable in racing, this
  can lead to ruin if over‑estimated.
* Jackform recommends estimating one’s overall margin (5–10 % realistic) and
  staking to return a fixed percentage of the bank (e.g. stake to win 6.5 % of
  bank if margin is 5 %).
* A simplified rule based on recent strike‑rate: e.g., bet 2 % of bank for 20‑25 %
  win rate, up to 7 % for 56‑65 %.
* Retrieval and progressive staking schemes are dismissed as “financial
  suicide”.

The thread also includes example calculations, Kelly criterion explanation, and
personal staking heuristics (e.g. incrementing stakes on losers and adjusting
after wins based on odds).

### 6. Market Observations

Later forum contributors (e.g. Boyo, Chesham) add remarks about market structure:

* overrounds are printed in Racing Post results; watch for over‑broke races
* price disparity between ladbrokes and implied market odds may not signal
  true value; layers sometimes price against the public

## Potential Strategy Concepts for Implementation

1. **Automated ratings** – build software replicating the FFMF rating rules and
   compute a score for each runner based on forecast position, season form,
   course/distance, and handicap rating.
2. **Filter pipeline** – implement the discussed filters and apply them before
   generating bets to reduce noise.
3. **Value detection** – calculate implied market probabilities from field size
   and compare with model probabilities to flag overlays.
4. **Simple staking module** – create configurable staking algorithms including
   level stakes, fixed‑return percentages, and basic Kelly approximations with
   conservative margin estimates.
5. **Market overround tracker** – scrape Racing Post or Betfair data to monitor
   overrounds and highlight low‑overround races for potential value.
6. **Expected value logging** – record EV calculations for each bet and analyse
   long‑term margin performance to refine edge estimates.


## Next Steps

- Extract historical handicap and forecast data from existing datasets under
  `data/`.
- Prototype FFMF rating computation and back‑test with the workspace’s
  bet results JSON files.
- Compare filter‑filtered selections against baseline to quantify strike‑rate
  improvement.
- Implement EV calculator and back‑test staking rules using historical prices.
- Consider incorporating overround and market‑disparity features into the models.

---

This file provides a neatly packaged summary of the Fine Form Master Formula
conversation and suggests experiments aligned with the principles discussed.
Feel free to expand or refine as further insights are uncovered.