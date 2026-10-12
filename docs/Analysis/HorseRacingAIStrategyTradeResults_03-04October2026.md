---
title: "Horse Racing AI Strategy (Trade x profit) — Executed Results Review, 3-4 October 2026"
aliases: ["Horse Racing AI Strategy Executed Results 3-4 October 2026", "HorseRacingAIStrategyTradeResults_03-04October2026"]
type: research
tags: [analysis, automation, bfexplorer, ev-analysis, horse-racing, results, strategy, trading, vdw]
mcp_tools: [GetAllBetResults, GetMarket]
data_contexts: [HorsesNigelsRaceTissue, HorsesNigelsRaceRadar, HorsesBetfairRaceInfoData, MarketSelectionsPriceMetricData]
---

# Horse Racing AI Strategy (Trade x profit) — Executed Results Review, 3–4 October 2026

**Two questions answered.** The executed strategy traded **one runner per market — the market's favourite** — opened and closed before the off.

- **Bet-only on those same dedicated selections** (same direction, £100 risk, held to the race result): **−£868.60** over 31 markets, against **+£15.50** actually traded. The opening-and-hedging mechanic is worth **+£798.78** on a like-for-like 26-market comparison, and it is the only reason the strategy is not a large loser.
- **Back the model's own `Rank 1`** (£100 stake, `Back` only, held to the race result): **+£1,226.00** over 30 bets (+40.9% ROI) — but "always back the market favourite" alone returns **+£1,025.00** of that, the model's *disagreements* with the market contributed only **£115**, and the model's own predicted return for those exact 30 bets was **+£0.26**, i.e. zero. t = +1.27: a promising sign, not an established edge.

**The common thread — the AI's handicapping supplied no usable directional edge in either direction.** It produced `Lay` on 32 of 33 favourites because its model prices the favourite below the market in 32 of 33 races; and on this sample the favourites **won more often than either the market or the model expected** (48.3% against 37.6% and 32.6%). Both the `Lay` rule and the `Rank 1` Back result are therefore driven by a small sample in which the market's favourites over-performed — not by the model's opinion.

---

## Executive Summary

| | Selection and stake | Markets | Stakes / risk | Net P&L | Return |
| :--- | :--- | ---: | ---: | ---: | ---: |
| **A. As executed** | market favourite, opened and hedged, £100 risk | 29 | £2,900 | **+£15.50** | **+0.53%** |
| A₂. As executed, excluding the double-traded market `1.263239327` | as above | 28 | £2,800 | +£83.24 | +2.97% |
| A₃. As executed, only the 26 clean trades (opened and fully hedged) | as above | 26 | £2,600 | +£187.63 | +7.22% |
| A₄. As executed, the 3 defective trades (withdrawal / hedge miss / partial fill) | as above | 3 | £300 | **−£172.13** | −57.4% |
| **B. Bet-only counterfactual** | same favourite, same direction, £100 risk, settled at the result | 31 | £3,100 | **−£868.60** | **−28.0%** |
| **C. Rank-1 Back counterfactual** | the model's own top pick, `Back` only, £100 stake, settled at the result | 30 | £3,000 | **+£1,226.00** | **+40.9%** |
| C₂. Control — always `Back` the market favourite, no model input | market favourite, `Back` only, £100 stake | 30 | £3,000 | +£1,025.00 | +34.2% |

- **33 strategy runs** were produced over the two days — 25 from `HorsesBetfairRaceInfoDataTradeV2.md`, 8 from `HorsesBetfairRaceInfoDataVDWTrade.md` — one per market, across 5 meetings (Southwell, Kelso, Uttoxeter, Curragh, Killarney) and 299 priced runners in total.
- **Every run traded the market's favourite**, and **32 of the 33 signals were `Lay`** (only Kelso 2m1f Hcap Chs was a `Back`). The strategy is, in practice, *"lay the favourite and hedge it before the off"*, with the AI's handicapping supplying only the direction sign and the profit target.
- **29 of the 33 recommended trades have a settled bet. Four do not**, although each of those outputs states the execution returned `status: true` (see [§5.1](#51-four-recommended-trades-have-no-settled-bet)).
- **The whole two-day result rests on one market.** `1.263239327` produced the single largest loss (−£67.74, on a favourite that was withdrawn) *and* the single largest gain (+£127.75, on a second, undocumented selection in the same market). Including the undocumented leg, the two-day total is **+£143.26**; without it, **+£15.50**.
- **Backing the model's own `Rank 1` instead would have returned +£1,226.00 (+40.9% ROI) on 30 bets** — the best of the three plans tested — but it is not statistically established (t = +1.27), "always back the market favourite" alone returns +£1,025.00 of it, and the model's own predicted return for those exact bets was **+£0.26**. See [§3](#3-alternative-counterfactual--back-the-models-rank-1-pick-100-stake-back-only).
- **The profit target set by the prompts (`min(round(|EV| × 100), 30)`) was reached in only 4 of the 29 settled trades.** The realised P&L is the pre-off price drift captured at the scheduled close, not the target.

---

## Scope and Method

| Source | Tool | What it gave |
| :--- | :--- | :--- |
| Bfexplorer bet results | `get_all_bet_results()` | 29 closed markets, every matched leg with time, `betType`, price, size, and the settled `profit` |
| Bfexplorer market states | `get_market(marketId)` | the settled `Winner`/`Loser` status of every runner in 31 of the 33 markets, used to test whether each **dedicated selection actually won** |
| Strategy outputs | `data/AIStrategyOutputs/*.md` | 33 reports; per-market field table (model %, best-back price, de-vigged market %, EV, Edge, rank), the traded favourite, the direction and the target |

**Stake basis.** The saved `Trade x profit` setting risks **£100 per selection** — a Lay opens with £100 of *liability* (e.g. `51.55 × (2.94 − 1) = £100.00`), a Back with a £100 stake. 27 of the 28 opening Lay legs reproduce that to the penny (the exception is Pirate Anne Bonny, see §5.3). The counterfactual uses the same £100 risk so the two columns are directly comparable.

**How the executed trade works (this matters for reading the results).** The strategy opens the position, then closes it with a fully-hedging opposite bet — the close size is `open size × open price ÷ close price`, which locks a profit that is **identical whether the horse wins or loses**:

```
locked profit = S × (close price − open price) ÷ close price      (S = backer's stake on a Lay)
```

Worked example — Tuppence (Kelso 2m Nov Hrd): Lay `149.25 @ 1.67`, closed Back `136.20 @ 1.83` →
`149.25 × (1.83 − 1.67) ÷ 1.83 = £13.05`, which is exactly the reported `13.0485`. The figure is **gross of commission** (2% market commission on the winning markets would take roughly £4 off the aggregate).

So the executed P&L is **not** a betting result at all: it is the P&L of a **directional pre-off trade**, which pays only if the price **travels**. A `Lay` needs the price to drift **out**; a `Back` needs it to **shorten**.

---

## 1. As-Executed Results

29 markets, £100 risk each, one selection per market — the market's favourite, always.

| Metric | As executed |
| :--- | ---: |
| Markets traded | 29 |
| Profitable / losing | **26 / 3** |
| Net P&L | **+£15.50** |
| Mean per market | +£0.53 |
| Median per market | +£3.36 |
| Best | +£17.57 (Governors Rock, Uttoxeter 2m Mdn Hrd) |
| Worst | −£99.99 (Kilbrin Rocco, Killarney 2m1f Hcap Chs) |
| Favourite actually won | 14 of 29 (48.3%) |

**The three losses are all operational, not analytical:**

| Market | Selection | What went wrong | P&L |
| :--- | :--- | :--- | ---: |
| `1.263239327` | Ice Jet | the AI's Step-1 favourite was **withdrawn** (`RemovedVacant`) after the recommendation; the trade had already been opened | −£67.74 |
| `1.263245220` | Kilbrin Rocco | the closing Back (`26.59 @ 4.90`) was placed **3.3 seconds** after the opening Lay — and the reported settlement equals the **full lay liability** (−£99.99 = `30.3 × 3.30`), which is only consistent with the hedge **not** having been matched. The horse won | −£99.99 |
| `1.263244959` | Pirate Anne Bonny | **one leg only** — a Lay of `£10 @ 1.44` where the £100-liability setting implies `£227.27`; the trade was ~4% of intended size and never closed | −£4.40 |

Strip those three out and the strategy behaves exactly as designed: **26 clean trades, +£187.63, mean +£7.22, median +£5.95, worst +£0.64.** Every single one was profitable. The design is a small, high-frequency drift capture with an occasional catastrophic tail — and on this sample **the three tails cost £172.13, which is everything the 26 clean trades earned and more.**

---

## 2. The Counterfactual — Betting the Same Dedicated Selections

**Same 33 selections. Same direction (the AI's EV sign). £100 risk each. No close — held to the race result.** Two of the 33 have no retrievable winner and are excluded; the withdrawn favourite (Ice Jet) is treated as a void £0.

| | Value |
| :--- | ---: |
| Markets settled as a bet | 31 |
| Gross risk | £3,100 |
| Selections that won | 15 |
| Selections that lost | 15 |
| Selections void (withdrawn favourite) | 1 |
| **Net P&L** | **−£868.60** |
| Return on risk | **−28.0%** |
| Mean per market | −£28.02 |
| Best / worst | +£116.28 (Time Is A Thief @ 1.86) / −£100.00 (each of 15 winners) |

**On the 26 markets where the executed trade was clean, the like-for-like comparison is:**

| | Net P&L |
| :--- | ---: |
| Held to the race result (bet only) | **−£611.15** |
| Opened and hedged (as executed) | **+£187.63** |
| **Value of the hedging / drift-capture mechanic** | **+£798.78** |

**The mechanism is simple and it is arithmetic, not skill.** Laying a short-priced favourite with £100 of liability risks the whole £100 to win `100 ÷ (price − 1)` — and here the favourites won **48.3%** of the time, so the 15 winners cost £1,500 while the 15 losers returned only £631.40. There is no price at which this is a good bet *unless the position is closed before the result*: the strategy's entire margin comes from the fact that the pre-off book **drifts out** on the favourite a little more often than it drifts in, and the hedged close monetises that drift regardless of the race.

**How much of the loss is bad luck?** Under the market's own de-vigged probabilities the 30 Layouts had an expected P&L of **+£9.88** — i.e. the bet-only plan was a fair coin flip before the race, not a losing system. The observed −£868.60 is **−1.90 standard deviations** from that expectation. Under the AI's model probabilities the expectation was **+£270.52** — the model's *claimed* edge. The truth sat with neither: **15 favourites won against 10.9 implied by the market and 9.5 implied by the model.**

---

## 3. Alternative Counterfactual — Back the Model's Rank-1 Pick (£100 stake, Back only)

The variant asked for next: **ignore the direction rule entirely and simply Back the model's own top pick — its `Rank 1` — with a £100 stake, held to the race result, in every market.**

`Rank 1` is the top of the AI's own probability vector. It **is** the market favourite in 21 of the 30 settled markets here and a different horse in 9.

| | Value |
| :--- | ---: |
| Markets settled | 30 |
| Stakes | £3,000 (£100 flat) |
| Won / lost | 15 / 15 |
| **Net P&L** | **+£1,226.00** |
| **ROI on stakes** | **+40.87%** |
| Mean per bet | +£40.87 |
| Median per bet | **−£34.00** |
| Best / worst | +£520.00 (Toptime @ 6.20) / −£100.00 |
| Mean price / mean model probability | 3.95 / 32.94% |
| Favourite held to the result (control, same £100 Back, no model input) | +£1,025.00 |
| **Model's contribution over the control** | **+£201.00** |
| Expected under the market's de-vigged probabilities | −£17.51 |
| Expected under the model's *own* probabilities | **+£0.26** |

**Three findings, and the second and third matter more than the first.**

1. **Backing `Rank 1` was the best of the three plans tested here** (+£1,226, +40.9% ROI) against the executed strategy (+£15.50) and the same-selections bet-only (−£868.60).
2. **It is not statistically established.** t = **+1.27**; the median bet still *loses* £100; and the profit is concentrated in **six winners** (Toptime +£520, Carla Ridge +£370, Kilbrin Rocco +£340, Divilabother +£295, James The Brave +£270, New Image +£215) that account for £2,010 of the £2,726 gross return. Remove the top two and the plan is roughly break-even.
3. **The model did not claim this edge.** The mean EV of the `Rank 1` picks is **+0.000** — by its own numbers the model priced its top runner at fair value, and the model's predicted P&L for these exact 30 bets is **+£0.26** — i.e. *zero*. So the +£1,226 is variance that happened to land well, not the model's opinion being vindicated. Almost all of it is attributable to backing the favourite, which "always back the market favourite" achieved on its own (+£1,025).

**Where the money came from — and where it did not:**

| Group | n | Net P&L | Rank-1 implied win % | Actual win % |
| :--- | ---: | ---: | ---: | ---: |
| `Rank 1` **was** the market favourite | 21 | **+£1,111.00** | 42.5% | **61.9%** (13/21) |
| `Rank 1` **was not** the market favourite | 9 | +£115.00 | 16.6% | 22.2% (2/9) |
| `Rank 1` priced below 4.0 | 19 | +£796.00 | — | 63.2% (12/19) |
| `Rank 1` priced 4.0–9.9 | 11 | +£430.00 | — | 27.3% (3/11) |

The **only** place the model's selection beat the price was where it *agreed* with the market: `Rank 1` = favourite won 13 of 21 against 42.5% implied (+1.8σ). Where the model **disagreed** and promoted a different horse, those picks won 2 of 9 against 16.6% implied — no edge, and £115 of £1,226. The model's *disagreements* with the market — the entire point of the EV machinery — contributed almost nothing.

**And the sharpest comparison in this whole review:** on the 19 markets where `Rank 1` was the market favourite *and* the trade also settled, the executed strategy — laying those same horses and hedging — made **+£35.16**, while **backing** them would have made **+£1,173.00**. On this sample the live strategy was on the wrong side of its own best picks.

**Verdict:** worth testing properly, not yet worth changing anything for. Backing `Rank 1` is a hypothesis with a promising sign and a 30-bet sample, on a plan whose own model claimed exactly zero edge on it.

---

## 4. Why the AI's Handicapping Added No Direction

This is the most important structural finding, and it is visible in the outputs themselves.

| Measure | Value |
| :--- | ---: |
| Signals that were `Lay` | **32 of 33** |
| Mean model probability for the favourite | 32.59% |
| Mean de-vigged market probability for the same horse | **37.55%** |
| Mean gap | **−4.96 pp** |
| Markets where the model sat *below* the market on the favourite | **32 of 33** |
| Favourites that actually won | **48.3%** (15 of 31) |

The prompts require a field-normalised probability vector that prices every runner's chance, then compare it to the de-vigged price. In practice the vector lands **below the market on the favourite in 32 of the 33 races**, which forces `EV < 0` and therefore `Lay` — a direction that is a **property of the method**, not a per-race finding. Several outputs say so explicitly ("*the log records the model pricing the market's favourite below the market in every card seen*").

And on this sample the method's premise is not supported by outcomes: the favourites won **48.3%** of the time while the model gave them **32.6%** and the market **37.6%**. Calibrated against the results, the model's favourite probabilities are **too low in every band**:

| Model probability band | n | Mean model | Actual win rate |
| :--- | ---: | ---: | ---: |
| 0–20% | 12 | 16.3% | 25.0% |
| 20–40% | 11 | 29.9% | 45.5% |
| 40–60% | 7 | 49.7% | 57.1% |
| 60–80% | 3 | 67.8% | 100.0% |

So the `Lay` had **negative informational value** and the strategy's gross profit came entirely from the closing mechanic. (n = 33 is small — this is a direction of travel, not proof, but it is consistent across every band.)

**Two further consequences of the same effect:**

- The **target is a fiction in practice.** `CloseBetPosition.Profit = min(round(|EV| × 100), 30)` asks the price to travel `|EV| × 100` percent against you before the off. A Lay target of 26 on New Image required 3.15 → 4.26 (+35%); the price reached 3.70. **The target was met in 4 of 29 trades**; the realised P&L is whatever the price did by the scheduled close (median **+5.95%** of risk, versus a mean target of **14.5**).
- The **entry price is not the analysis price.** The AI decides on the best-back price in its snapshot; the strategy opens minutes later, when the book has moved. Mean absolute gap **4.1%**, median 1.7%, worst **33.0%** (Cool Azul 9.40 → 12.50; Alfalfa 4.60 → 5.50). The direction and target are therefore computed on a price the trade never gets.

---

## 5. Data-Integrity and Execution Defects

### 5.1 Four recommended trades have no settled bet

`1.263199859` (Southwell 7f Nov Stks), `1.263239318` (Kelso 2m5f Nov Hrd), `1.263239477` (Uttoxeter 2m Beg Chs) and `1.263245114` (Curragh 1m Hcap) each produced a full report **and a stated successful execution**, yet `get_all_bet_results()` returns **no bet at all** for them.

- This is not a truncation artefact. `1.263239318` (Kelso 2m5f Nov Hrd) and `1.263239477` (Uttoxeter 2m Beg Chs) sit **squarely inside** the span of the 29 markets that *were* returned, so a trimmed or recency-limited result list cannot explain them. The other two sit at the two ends of the two-day window (`1.263199859` ran just before the first returned market, `1.263245114` just after the last), where they are consistent with the same gap.
- For two of them Bfexplorer still holds the fully settled market, so the market is retrievable and simply carries no bets:
  - `1.263239477` — **Country Mile, the favourite, WON**. A £100-liability Lay would have lost £100.
  - `1.263245114` — **Highbury See See, the favourite, LOST**. A £100-liability Lay would have won £42.55.
- The remaining two markets (`1.263199859`, `1.263239318`) are no longer retrievable from Bfexplorer at all, so their outcome cannot be established.

**Bottom line:** the AI's self-reported `{"status":true}` is **not evidence that a bet exists**. Missing *wins* (Highbury) and missing *losses* (Country Mile) are both present, so this is a random execution/reporting gap, not a filter.

### 5.2 One market was traded twice

`1.263239327` (Kelso 2m1f Hcap Chs) carries bets on **two** selections: **Ice Jet** (the documented favourite, −£67.74) and **Breaking Cover** (+£127.75). Only Ice Jet is mentioned anywhere in the strategy output. The prompt's own Tools Required #4/#6 warn about exactly this — a second invocation against the same `marketId` places a second trade, and the agent cannot see it. Whatever produced it, **the largest single P&L item in the whole sample is a trade the output does not document**, and the host is the only place it can be caught.

A plausible sequence (not provable from the data): the favourite Ice Jet was **withdrawn**, the market reformed with Breaking Cover as the new favourite, and a second run traded it. That would mean the withdrawal was handled by *re-running the whole prompt* rather than by cancelling.

### 5.3 A closing hedge can silently fail, and a stake can silently under-fill

- **Kilbrin Rocco** (`1.263245220`): opening Lay `30.3 @ 4.30` and closing Back `26.59 @ 4.90` are **3.3 seconds apart**, and the settled −£99.99 is the *entire* lay liability — inconsistent with both legs having been matched. A silent hedge failure on a winning favourite converts a designed +£3.71 into −£99.99.
- **Pirate Anne Bonny** (`1.263244959`): matched size `£10` against an intended £227.27 backer's stake. The trade was 4% of the intended size.

### 5.4 Post-trade verification does not exist

Nothing in the tool set used by the prompts can read back a matched bet or an open position, and the reports say as much. That is fine as a *design constraint*, but it means the only way the four missing executions and the hedge failures above are ever discovered is an offline reconciliation like this one, run days later.

---

## 6. Suggestions and Improvements

Ordered by value. Items 1–3 are worth more than everything else combined; items 4–6 change the economics; items 7–8 are for the next sample.

### A. Fix the execution layer (highest value)

> `scripts/validate_trading_report.py` already validates a **single** report's internal arithmetic (column sums, de-vig reproducibility, EV/Edge from that row's own `p`, direction against break-even, target from `|EV|`). What is missing is the **portfolio-level** half: did the recommended trade actually happen, and did it settle the way the design assumed? That is the control below.

1. **Reconcile every trade against `get_all_bet_results()` at market close, and alert on any run with no settled bet, one leg, or an unmatched hedge.** This single control would have caught every defect above (4 missing executions, 1 double trade, 1 hedge miss, 1 undersized fill). Given **−£172.13 of tail losses against +£187.63 of designed profit**, this is the difference between the strategy and a losing one.
2. **Enforce the once-per-market guarantee in the host, not in the prompt.** The prompt can only *promise* not to trade twice; the host must *prevent* it. Key the guard on `marketId` (and log any second invocation), and reconcile the count of executions to the count of `AIStrategyOutputs` files.
3. **Handle a withdrawn favourite as a first-class case.** Re-check `status == "Active"` for the fixed selection immediately before executing. If it is no longer `Active`, cancel and report — do **not** re-run the prompt, which is what appears to have happened and what produced the undocumented +£127.75. If you would rather the trade follow the market's *re-formed* favourite, say so explicitly as a documented fallback, with its own reconciliation.

### B. Fix the economics

4. **Stop deriving `CloseBetPosition.Profit` from `|EV| × 100`.** It is the AI's own model error restated, and it was reached in **4 of 29** trades; the realised exit is the scheduled close at the off, where the median capture is **+5.95%** of risk. Two better options:
   - set the target from the **realised drift distribution** (e.g. a fixed 5–8% of risk, which the sample shows is roughly what a pre-off book actually delivers), or
   - drop the early-exit concept and treat the strategy as a pure **close-at-the-off drift trade**, which is what it already is.
   Either way, remove the coupling between the profit target and a probability the outcomes contradict.
5. **Re-price on the entry, not on the analysis snapshot.** Compute direction and target from the price the trade is actually taken at. The snapshot gap is 4.1% on average and 33% at worst, which is larger than most of the targets being set.
6. **Model commission in the EV and in the target.** The reported figures are gross; 2% market commission removes roughly £4 from this sample and would take a meaningfully larger bite out of a properly-sized book.

### C. Fix the analysis layer

7. **Decide deliberately whether you want "always lay the favourite" or a model pick — because that is what you currently have.** The AI's `Edge`/`EV` machinery is not selecting anything: it produces `Lay` on 32 of 33 favourites, the direction is sign-determined by a systematic −4.96 pp bias against favourites, and the favourites went on to win **48.3%** of the time. Two honest paths:
   - **Keep the drift trade, drop the handicap.** If the edge is pre-off price drift on the favourite, it needs no 300-runner handicapping report — it needs a drift model and correct execution. That is cheaper, faster, and far easier to validate.
   - **Keep the handicap, change the selection rule.** The prompt fixes the traded runner at Step 1 as the market favourite and forbids re-deriving it — so the AI's own top-rated runner (its `Rank 1`) never gets traded. `Rank 1` differed from the favourite in **9 of 33** races (27%), which is where any genuine handicap edge would have to live. **That back-test is now in [§3](#3-alternative-counterfactual--back-the-models-rank-1-pick-100-stake-back-only): `Rank 1` backed at £100 returned +£1,226 (+40.9%) — but +£1,025 of that came from the plain "back the favourite" it mostly agreed with, the 9 genuine disagreements returned only +£115, and the model's own predicted return for those bets was +£0.26.** The `Rank 1` route is worth a proper trial; the model's *disagreements* are not yet showing anything.
8. **Re-run the calibration on at least 200–300 markets before changing anything material.** 31 resolved favourites is not enough to distinguish a −28% bet-only result from fair value (it is only −1.9σ), and the whole *executed* result is dominated by a handful of outlier trades. The useful metric to track is not the headline P&L but: **trades with ≥2 matched legs**, **hedge-fill rate**, **mean realised capture as a % of risk**, and **model-vs-market gap on the favourite**. The first of those should be 100%; it is 96.6% here and that 3.4% cost more than the strategy earned.

### D. Prompt-level observations (no change requested)

- The prompts' own guardrails repeatedly warn that a favourite `Lay` is the method's **default output**, not a discovery, and the observed −4.96 pp bias confirms the warning was well-founded. The prompts correctly preclude acting on it as evidence — but the *strategy layer above them* does act on it, unconditionally. That disconnect is worth a deliberate decision rather than an inherited default.
- The VDW variant produced 7 settled trades (+£18.13 as executed, versus −£2.62 for the 22 TradeV2 trades). With n = 7 that is noise, but the two variants are worth instrumenting separately so the split is visible when the sample grows.

---

## 7. Caveats

- **Small sample, one dominant market.** 29 executed markets, and the two-day headline swings between +£15.50 and +£143.26 depending on whether one undocumented trade is counted.
- **Neither counterfactual is statistically established.** The `Rank 1` Back plan's +£1,226 is t = **+1.27** on 30 bets and its median bet *loses* £100 — six winners carry £2,010 of the £2,726 gross return. The bet-only plan's −£868.60 is t = **−1.90** on 31 bets. Both are single two-day samples over the same 33 races, so they are **not independent** of each other.
- **The `Rank 1` counterfactual is a subset test, not a selection test.** In 21 of the 30 settled markets the model's top pick *was* the market favourite, so the plan is largely "back the favourite" with 9 substitutions — and those 9 substitutions contributed only £115.
- **Figures are gross of commission.**
- **Two markets could not be resolved** (`1.263199859`, `1.263239318`) — both counterfactuals exclude them; the `Rank 1` test also voids `1.263239327` (withdrawn runner).
- **Both counterfactuals are modelled settlements**, not observed ones: each assumes a single Back or Lay at the AI's stated snapshot price, £100 of stake or risk, held to the result. Real execution would have filled at different prices.
- **Prices in the table are the AI's analysis-time best-back prices**, not the prices the trades were taken at (see §4).

---

## Appendix A — Market-by-Market Detail

Direction and target are the AI's; *as executed* is the bfexplorer settled profit; *bet only* is the modelled £100-risk settlement of the same selection in the same direction.

| # | Date / Meeting — Race | Dedicated selection (market favourite) | AI price | Model % | Market % | EV | Dir | Target | Favourite | As executed £ | Bet-only £ |
| ---: | :--- | :--- | ---: | ---: | ---: | ---: | :--- | ---: | :--- | ---: | ---: |
| 1 | 10-03 Southwell 7f Nov Stks ⚠ | Dontworryaboutme | 2.16 | 44.00 | 46.29 | −0.05 | Lay | 5 | ? | **n/a** | n/a |
| 2 | 10-03 Southwell 6f Hcap | New Image | 3.15 | 23.50 | 31.46 | −0.26 | Lay | 26 | WON | 10.59 | −100.00 |
| 3 | 10-03 Southwell 5f Hcap | Hi Lord | 6.00 | 13.00 | 16.41 | −0.22 | Lay | 22 | lost | 3.12 | 20.00 |
| 4 | 10-03 Southwell 1m6f Hcap | Blue Tulip | 2.30 | 38.00 | 43.38 | −0.13 | Lay | 13 | WON | 8.54 | −100.00 |
| 5 | 10-04 Kelso 2m5f Nov Hrd ⚠ | Jentobello | 1.37 | 52.00 | 72.62 | −0.29 | Lay | 29 | ? | **n/a** | n/a |
| 6 | 10-04 Kelso 2m5f Hcap Hrd | Heart Above | 4.40 | 19.50 | 22.65 | −0.14 | Lay | 14 | lost | 3.36 | 29.41 |
| 7 | 10-04 Kelso 2m1f Hcap Chs ⚠* | Ice Jet | 3.65 | 31.00 | 27.22 | +0.13 | Back | 13 | **withdrawn** | −67.74 | 0.00 |
| 8 | 10-04 Kelso 2m Nov Hrd | Tuppence | 1.67 | 54.00 | 59.64 | −0.10 | Lay | 10 | WON | 13.05 | −100.00 |
| 9 | 10-04 Kelso 2m5f Hcap Hrd | Luminaries | 5.50 | 16.60 | 18.01 | −0.09 | Lay | 9 | WON | 1.45 | −100.00 |
| 10 | 10-04 Kelso 3m2f Hcap Chs | Upfordebate | 1.96 | 43.00 | 50.80 | −0.16 | Lay | 16 | WON | 15.15 | −100.00 |
| 11 | 10-04 Uttoxeter 2m Beg Chs ⚠ | Country Mile | 1.38 | 68.50 | 72.23 | −0.05 | Lay | 5 | WON | **n/a** | −100.00 |
| 12 | 10-04 Uttoxeter 2m7f Nov Hrd | Gentle Ocean | 1.60 | 55.00 | 62.67 | −0.12 | Lay | 12 | WON | 17.31 | −100.00 |
| 13 | 10-04 Uttoxeter 2m Mdn Hrd | Governors Rock | 1.77 | 48.00 | 56.19 | −0.15 | Lay | 15 | WON | 17.57 | −100.00 |
| 14 | 10-04 Uttoxeter 2m Mdn Hrd | Larzac | 2.64 | 30.50 | 37.68 | −0.19 | Lay | 19 | WON | 11.02 | −100.00 |
| 15 | 10-04 Uttoxeter 2m7f Hcap Hrd | Roger Pol | 3.75 | 19.00 | 26.31 | −0.29 | Lay | 29 | lost | 7.59 | 36.36 |
| 16 | 10-04 Uttoxeter 2m Hcap Hrd | Uncle Remus | 3.80 | 20.00 | 26.16 | −0.24 | Lay | 24 | lost | 6.35 | 35.71 |
| 17 | 10-04 Uttoxeter 2m Hcap Chs | Ask A Sainte | 4.00 | 19.50 | 24.59 | −0.22 | Lay | 22 | lost | 5.55 | 33.33 |
| 18 | 10-04 Uttoxeter 3m Hcap Chs | Jo Arengeot Bey Aa | 3.40 | 28.50 | 29.34 | −0.03 | Lay | 3 | lost | 1.19 | 41.67 |
| 19 | 10-04 Curragh 7f Listed | Loughrea | 2.34 | 32.00 | 42.33 | −0.25 | Lay | 25 | lost | 14.82 | 74.63 |
| 20 | 10-04 Curragh 7f Mdn | Pirate Anne Bonny | 1.50 | 62.00 | 66.44 | −0.07 | Lay | 7 | WON | −4.40 | −100.00 |
| 21 | 10-04 Curragh 7f Mdn | Hmas Perth | 1.32 | 73.00 | 75.51 | −0.04 | Lay | 4 | WON | 11.40 | −100.00 |
| 22 | 10-04 Curragh 1m1f Hcap | Johnny Soda | 4.90 | 17.50 | 20.24 | −0.14 | Lay | 14 | lost | 2.82 | 25.64 |
| 23 | 10-04 Curragh 6f Listed | Carla Ridge | 4.70 | 19.50 | 21.23 | −0.08 | Lay | 8 | WON | 1.63 | −100.00 |
| 24 | 10-04 Curragh 6f Hcap | Cool Azul | 9.40 | 9.80 | 10.57 | −0.08 | Lay | 8 | lost | 0.64 | 11.90 |
| 25 | 10-04 Curragh 1m4f Hcap | Alfalfa | 4.60 | 18.00 | 21.61 | −0.17 | Lay | 17 | lost | 3.12 | 27.78 |
| 26 | 10-04 Curragh 1m Hcap ⚠ | Highbury See See | 3.35 | 28.50 | 29.46 | −0.05 | Lay | 5 | lost | **n/a** | 42.55 |
| 27 | 10-04 Killarney 2m1f Hcap Hrd | Colins Friend | 3.00 | 15.50 | 33.01 | −0.54 | Lay | 30 | lost | 12.35 | 50.00 |
| 28 | 10-04 Killarney 2m1f Mdn Hrd | Astelia | 2.66 | 36.00 | 37.57 | −0.04 | Lay | 4 | WON | 2.10 | −100.00 |
| 29 | 10-04 Killarney 2m1f Nov Hrd | Lips Freedom | 2.40 | 35.00 | 41.51 | −0.16 | Lay | 16 | lost | 9.98 | 71.43 |
| 30 | 10-04 Killarney 2m7f Hcap Hrd | James The Brave | 3.70 | 26.00 | 27.03 | −0.04 | Lay | 4 | WON | 0.76 | −100.00 |
| 31 | 10-04 Killarney 2m1f Hcap Chs | Kilbrin Rocco | 4.40 | 19.00 | 22.63 | −0.16 | Lay | 16 | WON | −99.99 | −100.00 |
| 32 | 10-04 Killarney 2m5f Beg Chs | Time Is A Thief | 1.86 | 52.00 | 53.57 | −0.03 | Lay | 3 | lost | 2.94 | 116.28 |
| 33 | 10-04 Killarney 2m7f Hcap Chs | True Harmony | 7.80 | 8.20 | 12.78 | −0.36 | Lay | 30 | lost | 3.24 | 14.71 |
| | | | | | | | | | **TOTAL** | **+15.50** | **−868.60** |

⚠ = recommended trade with **no settled bet** in Bfexplorer. ⚠* = market traded on two selections (the second, Breaking Cover, is undocumented and produced +£127.75).

Notes on the table:

- `Model %`, `Market %`, `EV`, `Dir` and `Target` are taken verbatim from each market's `data/AIStrategyOutputs/<marketId>.md` field table. `Market %` is the de-vigged column as printed by the report.
- The market's favourite was also the AI's own `Rank 1` in 24 of the 33 races; in the 9 where they differed (`1.263199884`, `1.263239338`, `1.263239504`, `1.263239512`, `1.263239531`, `1.263245098`, `1.263245107`, `1.263245154`, `1.263245247`) the traded runner was still the favourite, per the prompts' fixed selection rule.
- The AI's stated best-back price differs from the strategy's actual opening price in most markets (mean 4.1% absolute); the table shows the analysis price.

## Appendix B — Reconciliation Workflow Used

```
1. get_all_bet_results()                    -> 29 settled markets, every matched leg + settled profit
2. parse data/AIStrategyOutputs/*.md        -> 33 field tables + traded selection + direction + target
3. get_market(marketId) for each market      -> settled Winner/Loser field  (31 of 33 retrievable)
4. join on marketId:
       selection traded == the AI's Step-1 favourite?   yes in 29 / 29
       favourite's model-vs-market gap, EV, direction, target
       favourite win/loss vs the dedicated selection
5. counterfactual: same selection, same direction, £100 risk, settled at the result
6. expectations: E[P&L] under the market's de-vigged probabilities vs under the model's
```

The per-market scrape, the bet-leg reconstruction and the counterfactual arithmetic are reproducible from `data/AIStrategyOutputs/`, the `get_all_bet_results()` payload and `get_market()` alone.

## Appendix C — Rank-1 Back Test, Market by Market

`£100 Back` = the modelled settlement of a flat £100 Back on the model's own top pick, held to the race result. `Model's claim` = the same bet's expected P&L under the AI's *own* probability for that runner, `100 × (p × price − 1)`. The two columns disagreeing is the point: the model did not claim most of what happened.

| # | Date / Meeting — Race | `Rank 1` | Price | Model % | EV | = favourite? | Result | £100 Back | Model's claim |
| ---: | :--- | :--- | ---: | ---: | ---: | :--- | :--- | ---: | ---: |
| 1 | 10-03 Southwell 7f Nov Stks ⚠ | Dontworryaboutme | 2.16 | 44.00 | −0.05 | yes | **?** | **n/a** | −4.96 |
| 2 | 10-03 Southwell 6f Hcap | New Image | 3.15 | 23.50 | −0.26 | yes | WON | +215.00 | −25.98 |
| 3 | 10-03 Southwell 5f Hcap | Toptime | 6.20 | 20.50 | +0.27 | **no** | WON | +520.00 | +27.10 |
| 4 | 10-03 Southwell 1m6f Hcap | Blue Tulip | 2.30 | 38.00 | −0.13 | yes | WON | +130.00 | −12.60 |
| 5 | 10-04 Kelso 2m5f Nov Hrd ⚠ | Jentobello | 1.37 | 52.00 | −0.29 | yes | **?** | **n/a** | −28.76 |
| 6 | 10-04 Kelso 2m5f Hcap Hrd | Heart Above | 4.40 | 19.50 | −0.14 | yes | lost | −100.00 | −14.20 |
| 7 | 10-04 Kelso 2m1f Hcap Chs ⚠* | Ice Jet | 3.65 | 31.00 | +0.13 | yes | withdrawn (void) | 0.00 | +13.15 |
| 8 | 10-04 Kelso 2m Nov Hrd | Tuppence | 1.67 | 54.00 | −0.10 | yes | WON | +67.00 | −9.82 |
| 9 | 10-04 Kelso 2m5f Hcap Hrd | Helsgetaway | 5.80 | 19.60 | +0.14 | **no** | lost | −100.00 | +13.68 |
| 10 | 10-04 Kelso 3m2f Hcap Chs | Upfordebate | 1.96 | 43.00 | −0.16 | yes | WON | +96.00 | −15.72 |
| 11 | 10-04 Uttoxeter 2m Beg Chs ⚠ | Country Mile | 1.38 | 68.50 | −0.05 | yes | WON | +38.00 | −5.47 |
| 12 | 10-04 Uttoxeter 2m7f Nov Hrd | Gentle Ocean | 1.60 | 55.00 | −0.12 | yes | WON | +60.00 | −12.00 |
| 13 | 10-04 Uttoxeter 2m Mdn Hrd | Governors Rock | 1.77 | 48.00 | −0.15 | yes | WON | +77.00 | −15.04 |
| 14 | 10-04 Uttoxeter 2m Mdn Hrd | Sytadel | 3.65 | 31.50 | +0.15 | **no** | lost | −100.00 | +14.98 |
| 15 | 10-04 Uttoxeter 2m7f Hcap Hrd | Divilabother | 3.95 | 34.00 | +0.34 | **no** | WON | +295.00 | +34.30 |
| 16 | 10-04 Uttoxeter 2m Hcap Hrd | Uncle Remus | 3.80 | 20.00 | −0.24 | yes | lost | −100.00 | −24.00 |
| 17 | 10-04 Uttoxeter 2m Hcap Chs | Holy Legend | 7.00 | 20.50 | +0.44 | **no** | lost | −100.00 | +43.50 |
| 18 | 10-04 Uttoxeter 3m Hcap Chs | Jo Arengeot Bey Aa | 3.40 | 28.50 | −0.03 | yes | lost | −100.00 | −3.10 |
| 19 | 10-04 Curragh 7f Listed | Loughrea | 2.34 | 32.00 | −0.25 | yes | lost | −100.00 | −25.12 |
| 20 | 10-04 Curragh 7f Mdn | Pirate Anne Bonny | 1.50 | 62.00 | −0.07 | yes | WON | +50.00 | −7.00 |
| 21 | 10-04 Curragh 7f Mdn | Hmas Perth | 1.32 | 73.00 | −0.04 | yes | WON | +32.00 | −3.64 |
| 22 | 10-04 Curragh 1m1f Hcap | Johnny Soda | 4.90 | 17.50 | −0.14 | yes | lost | −100.00 | −14.25 |
| 23 | 10-04 Curragh 6f Listed | Carla Ridge | 4.70 | 19.50 | −0.08 | yes | WON | +370.00 | −8.35 |
| 24 | 10-04 Curragh 6f Hcap | Apache Outlaw | 9.80 | 10.80 | +0.06 | **no** | lost | −100.00 | +5.84 |
| 25 | 10-04 Curragh 1m4f Hcap | Sinbad My Dad | 5.60 | 19.50 | +0.09 | **no** | lost | −100.00 | +9.20 |
| 26 | 10-04 Curragh 1m Hcap ⚠ | Highbury See See | 3.35 | 28.50 | −0.05 | yes | lost | −100.00 | −4.53 |
| 27 | 10-04 Killarney 2m1f Hcap Hrd | Granlahan | 8.80 | 17.50 | +0.54 | **no** | lost | −100.00 | +54.00 |
| 28 | 10-04 Killarney 2m1f Mdn Hrd | Astelia | 2.66 | 36.00 | −0.04 | yes | WON | +166.00 | −4.24 |
| 29 | 10-04 Killarney 2m1f Nov Hrd | Lips Freedom | 2.40 | 35.00 | −0.16 | yes | lost | −100.00 | −16.00 |
| 30 | 10-04 Killarney 2m7f Hcap Hrd | James The Brave | 3.70 | 26.00 | −0.04 | yes | WON | +270.00 | −3.80 |
| 31 | 10-04 Killarney 2m1f Hcap Chs | Kilbrin Rocco | 4.40 | 19.00 | −0.16 | yes | WON | +340.00 | −16.40 |
| 32 | 10-04 Killarney 2m5f Beg Chs | Time Is A Thief | 1.86 | 52.00 | −0.03 | yes | lost | −100.00 | −3.28 |
| 33 | 10-04 Killarney 2m7f Hcap Chs | Cullenwaine | 9.00 | 15.80 | +0.42 | **no** | lost | −100.00 | +42.20 |
| | | | | | | | **TOTAL (30 settled)** | **+1,226.00** | **+0.26** |

Reading the two right-hand columns together:

- The **`Model's claim` column barely moves** because the model's mean EV for its own top pick is **+0.000** — it thought these bets were fairly priced. Where its claim was largest, it was wrong: Holy Legend (+£43.50 claimed, −£100 actual), Granlahan (+£54.00 claimed, −£100 actual), Cullenwaine (+£42.20 claimed, −£100 actual).
- The **`£100 Back` column is carried by the short-priced winners the model also called top**: six of them produce £2,010 of the £2,726 gross return.
- The 9 rows marked **no** (where the model's pick was *not* the market favourite) net **+£115.00** between them, against **+£1,111.00** from the 21 rows marked yes.
