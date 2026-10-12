---
title: "Horse Racing AI Strategy (Trade x profit) — Executed Results Review, 5 October 2026"
aliases: ["Horse Racing AI Strategy Executed Results 5 October 2026", "HorseRacingAIStrategyTradeResults_05October2026"]
type: research
tags: [analysis, automation, bfexplorer, ev-analysis, horse-racing, results, strategy, trading, vdw]
mcp_tools: [GetAllBetResults, GetMarket]
data_contexts: [HorsesNigelsRaceTissue, HorsesNigelsRaceRadar, HorsesBetfairRaceInfoData, MarketSelectionsPriceMetricData]
---

# Horse Racing AI Strategy (Trade x profit) — Executed Results Review, 5 October 2026

**Three questions answered.** The executed strategy traded **one runner per market — the market's favourite** — opened and (normally) closed before the off.

- **How many trades closed before the race start time?** **16 of the 23 settled trades (70%) closed before the off; 7 closed after it**, from **+57 seconds** to **+147 seconds (2m27s)** past the start. The median close was **123 seconds before** the off. The seven late closes produced **+£85.98 — 49% of the day's entire profit** — so roughly half of this day's result came from positions that were still open when the race was running, not from the designed pre-off hedge.
- **Versus betting only:** the same selections, same direction, £100 risk, held to the race result: **−£1,288.00** over 23 markets, against **+£176.87** actually traded. The open-and-hedge mechanic is worth **+£1,464.87** on a like-for-like 23-market comparison, and it is the only reason the strategy is not a large loser.
- **Versus backing the favourite only:** `Back` the market favourite at the price each output stated, £100 stake, held to the result: **+£1,020.00** over 29 markets (**+35.2% ROI**). That is **5.8× the executed book**. Twelve favourites won (41.4%).
- **Versus backing our `Rank 1` only:** `Back` the model's own top pick, £100 stake, held to the result: **−£742.00** over 29 markets (**−25.6% ROI**). It won only 6 times (20.7%) — and **5 of those 6 were races where `Rank 1` was simply the market favourite**; the single genuine disagreement that landed was Caraway at 9.8.

**The common thread — again, the AI's handicapping supplied no usable directional edge.** 20 of the 23 settled trades were `Lay` and only 2 were `Back`, because the model prices the favourite below the market; where the model actively disagreed with the market (12 races), **the market's favourite returned +£1,542 and the model's own top pick returned −£220**.

**A warning about yesterday's headline number.** The previous review's most attractive finding — "backing the model's `Rank 1` returns +40.9% ROI" — **reversed sign on this day** (−25.6%). Two days of a small sample cannot establish an edge in either direction, and this day is evidence *against* the `Rank 1` idea rather than for it.

---

## Executive Summary

| | Selection and stake | Markets | Stakes / risk | Net P&L | Return |
| :--- | :--- | ---: | ---: | ---: | ---: |
| **A. As executed** | market favourite, opened and hedged, £100 stake or liability | 23 | £2,300 | **+£176.87** | **+7.69%** |
| A₂. As executed, only the 16 trades that closed **before** the off | as above | 16 | £1,600 | +£90.89 | +5.68% |
| A₃. As executed, the 7 trades that closed **after** the off (57–147s late) | as above | 7 | £700 | **+£85.98** | **+12.28%** |
| **B. Bet-only counterfactual** | same selection, same direction, £100 risk, settled at the result | 23 | £2,300 | **−£1,288.00** | **−56.0%** |
| **C. Back-the-favourite counterfactual** | market favourite, `Back` only, £100 stake, settled at the result | 29 | £2,900 | **+£1,020.00** | **+35.2%** |
| **D. Rank-1 Back counterfactual** | the model's own top pick, `Back` only, £100 stake, settled at the result | 29 | £2,900 | **−£742.00** | **−25.6%** |

- **29 strategy runs**, all from `HorsesBetfairRaceInfoDataVDWTrade.md` — one per market, across **7 meetings** (Stratford, Yarmouth, Wolverhampton, Killarney, Fairyhouse, Kempton, Pontefract) and **286 priced runners** in total. No `TradeV2` run was made on this day.
- **23 of the 29 runs produced a settled bet. Six produced none** — and only **one** of those six says so (`1.263285614`, the only `Strategy Recommendation — NO BET` in the set). The other five recommend a trade and are silent about execution (§6.1).
- **Every one of the 23 settled trades was profitable** — the smallest +£0.69, the largest +£23.72, median **+£4.88**. But the profits are small and uniform relative to the £100 risked, which is the signature of the hedge mechanic rather than of a handicapping edge.
- **The profit target (`min(round(|EV| × 100), 30)`) was reached in 4 of the 23 settled trades** (`…607` 10→£11.11, `…455` 4→£14.03, `…662` 12→£18.47, `…668` 15→£15.38). As in the previous review, the realised P&L is the pre-off price drift captured at the close, not the target.
- **Every executed opening leg matched the direction the output stated** (22 of 23; one output, `1.263285449`, contains no executable parameter block although the trade was placed). Direction was not the source of any loss.

---

## Scope and Method

| Source | Tool | What it gave |
| :--- | :--- | :--- |
| Bfexplorer bet results | `get_all_bet_results()` | 23 closed markets, every matched leg with time, `betType`, price, size, and the settled `profit` |
| Bfexplorer market states | `get_market(marketId)` | the settled `Winner`/`Loser` status of runners in the 13 markets where the selection needed was **not** the traded one, or was not traded at all |
| Strategy outputs | `data/AIStrategyOutputs/*.md` (29 files, excluding `Old/`) | the field table (model %, stated price, de-vigged market %, EV, Edge, rank), the marked favourite, `Rank 1`, the direction and the target |

- **Where the outcome of the traded runner was needed**, it was taken from `get_all_bet_results()` — the settled `Winner`/`Loser` of the selection that was actually bet. `get_market` was called **only** for selections the bet record cannot see: the 6 favourites that were never traded, and the `Rank 1` in markets where `Rank 1` was a different horse from the traded favourite. That is **15 markets called, not 29**.
- **Prices in the counterfactuals are the prices each output itself stated** (`Best Back Price` / `Snapshot P`), the same basis as the previous review. They are the analysis-time prices, not the executed prices, so they flatter nothing: the executed book had already paid the snapshot-to-entry move.
- **"Favourite"** = the traded runner, which the prompt defines as the market's favourite (shortest `price`). Verified against the outputs' own tables: in **22 of the 23** traded markets the traded runner is the table's shortest-priced row outright. The 23rd, `1.263285449`, prints **two prices per row** — `Best Back` and the entry price in brackets: Dreams Galore **4.5 (4.4)**, Sultan Darius **4.4 (4.5)**. At the two prices the trade could actually take (Dreams Galore 4.4, Sultan Darius 4.5) the traded runner was the shorter, so the rule held; the apparent tie is an artefact of which of the two printed prices is read.
- **"Rank 1"** = top of the model's own probability vector (`Rank` column), as in the previous review. Where the model's pick *was* the favourite, the two counterfactuals share the same bet.
- Settled Bfexplorer markets report every beaten runner as `price: 1000` and the winner as `1.01`; the outcome is read from `status`, never from `price`, in closed markets.

---

## 1. The timing question — did the trades close before the off?

**16 of 23 before the off. 7 after.**

| | Count | Share | P&L | Return on £100/market |
| :--- | ---: | ---: | ---: | ---: |
| Closed **before** the off | **16** | 69.6% | +£90.89 | +5.68% |
| Closed **after** the off | **7** | 30.4% | **+£85.98** | **+12.28%** |

- Close-vs-offset in seconds: **min −582s** (9m42s early), **median −123s**, **max +147s** (2m27s late).
- **The late closes were the profitable ones** — +£12.28 each against +£5.68 for the punctual ones — because for those trades the position was still open when the race was decided. That is **race-outcome exposure**, not the pre-off price drift the design intends to capture. It flatters the day and it is a live risk.

The seven late closes:

| Market | Meeting / race | Off | Close leg | Closed |
| :--- | :--- | :--- | :--- | ---: |
| `1.263285648` | Kempton 6f Hcap | 19:45 | `Back` 4.8 | **+147s** |
| `1.263285662` | Kempton 7f Hcap | 20:45 | `Back` 1.74 | **+140s** |
| `1.263285627` | Kempton 1m4f Nov Stks | 18:15 | `Back` 1.75 | **+131s** |
| `1.263285318` | Wolverhampton 1m1f Nursery | 17:26 | `Back` 2.24 | **+118s** |
| `1.263285449` | Wolverhampton 5f Nursery | 18:00 | `Lay` 3.65 | **+93s** |
| `1.263285469` | Wolverhampton 6f Hcap | 19:30 | `Back` 2.86 | **+69s** |
| `1.263285332` | Yarmouth 1m Hcap | 17:40 | `Back` 5.9 | **+57s** |

- **All seven were Kempton, Wolverhampton or Yarmouth evening/afternoon markets; every one closed with a `Back`** (the hedge leg) **except `1.263285449`**, where the opening leg was itself the `Back` and the late leg was the hedging `Lay`.
- The late closes cluster in the last two hours of the card and in the two all-weather tracks, which points at an **execution-scheduling** problem rather than at the prompt: the outputs themselves contain no clock at all (the prompt is explicitly forbidden from reasoning about time), so the close schedule is entirely the host's.

---

## 2. The executed book

- **23 markets, £100 of stake-or-liability risk each** (a `Back` opens with a £100 stake; a `Lay` opens with £100 of liability, e.g. `…708` `Lay` 20.83 @ 5.8 = £100). Total risk **£2,300**.
- **Net +£176.87 (+7.69%)**, and **every trade won**: min +£0.69, median +£4.88, max +£23.72.
- **21 `Lay`, 2 `Back`** among the 23 settled (the two `Back` signals, `…607` Powder Monkey and `…668` Currumbin, returned +£11.11 and +£15.38 and both closed before the off — the family's first `Back` signals that actually traded).
- The uniformity of the wins — twenty-three small positives, no losers — is the fingerprint of the mechanic: opening at one price and closing at a drifted one inside the same pre-off window. It is not a distribution of handicapping results.

---

## 3. Counterfactual — back the market favourite only

`Back` £100 at the favourite's stated price, held to the result, all 29 log markets:

- **+£1,020.00 on 29 bets (+35.2% ROI).** Twelve favourites won (41.4%).
- Restricted to the 23 markets that actually traded: **+£880.00**. The 6 markets the strategy did **not** trade added **+£140** net to the favourite-only plan — and that net hides the point: of those 6 untraded favourites, **two won** (`…655` Portman Blue +£330, `…714` Noble Grace +£210) and four lost (−£100 each). **The two winners the system sat out were worth +£540 of foregone favourite-Back profit.**
- **The favourite-only plan beat the executed strategy by £843.13** (+£1,020.00 against +£176.87) while risking £100 on every market rather than only on the 23 the AI chose.

---

## 4. Counterfactual — back our `Rank 1` only

`Back` £100 at `Rank 1`'s stated price, held to the result, all 29 log markets:

- **−£742.00 on 29 bets (−25.6% ROI).** Six won (20.7%).
- **Five of the six wins are not evidence for the model.** In 17 of the 29 markets `Rank 1` *is* the market favourite, so those bets are the favourite-backing plan in disguise. `Rank 1` beat the market on the winner only once: **Caraway at 9.8** (`…601`), worth +£880.
- **This reverses the previous review's headline.** Two days ago the same bet returned +£1,226.00 (+40.9%); here it returns −£742.00 (−25.6%). The previous review already flagged that result as statistically unestablished (t = +1.27) and mostly attributable to the favourite. This day confirms the caution.

### When the favourite is also our `Rank 1`

The prompt's own ranking, used as a **filter** on the favourite — back it only when the model also ranks that same horse 1:

| | Markets | Favourite won | Net (`Back` £100) | ROI |
| :--- | ---: | ---: | ---: | ---: |
| `Rank 1` **is** the favourite (model agrees) | 17 | 5 (29.4%) | **−£522.00** | −30.7% |
| `Rank 1` is **not** the favourite (model disagrees) | 12 | 7 (58.3%) | **+£1,542.00** | +128.5% |
| All favourites, unfiltered | 29 | 12 (41.4%) | +£1,020.00 | +35.2% |

- **The filter destroys the result.** Requiring the model's agreement turns +£1,020 into **−£522**, and it does so by removing exactly the races that went well: the favourites the model *agreed* with won **29.4%** of the time, the ones it disputed won **58.3%**. The agreement subset is not a refined version of the favourite bet, it is its inverse.
- Seventeen and twelve bets are not a sample, so this is not established either. But its direction is the same one both days point at: where the model has an opinion that differs from the market, the market has been right more often, and using the model as a *filter* has been worse than ignoring it.

### The 12 races where the model disagreed with the market

| | Market favourite (`Back` £100) | Model `Rank 1` (`Back` £100) |
| :--- | ---: | ---: |
| Wins | 7 of 12 | 1 of 12 |
| Net | **+£1,542.00** | **−£220.00** |

- In the only races where the model's opinion had any content — where it rated a horse above the market's favourite — **the market was right seven times and the model once**, and the model's picks lost money outright.
- Note that the *executed* strategy did not back its `Rank 1` in these races: it laid the favourite and hedged, and it made a small profit in all 12. The loss shown here is what the prompt's own ranking would have produced if it were bet directly.

---

## 5. What the AI's handicapping did contribute

- **Direction:** effectively nothing usable, as before. 20 of 23 settled trades were `Lay`, because the model prices the favourite below the de-vigged market in race after race. The 3 that were not `Lay` were 2 `Back` signals and 1 unstated.
- **Target:** reached in 4 of 23. The realised P&L is drift captured at the close.
- **Selection:** the traded runner is the favourite by construction, so selection added no independent choice at all — the strategy is, in practice, *"lay the favourite and hedge it before the off, occasionally back one"*.
- **The one thing that paid was not analysis:** the open-then-hedge mechanic, worth +£1,464.87 against betting the same selections to the result (−£1,288.00 vs +£176.87).

---

## 6. Defects and open questions

### 6.1 Six runs produced no settled bet; five of them say nothing about it

| Market | Favourite | Favourite result | Output declares | Would-be favourite-Back |
| :--- | :--- | :--- | :--- | ---: |
| `1.263285053` | Expert Analysis 1.87 | Loser | nothing (no parameter block) | −£100 |
| `1.263285065` | Mon Missel 2.44 | Loser | direction + target, no execution | −£100 |
| `1.263285462` | Lady Branksome 2.8 | Loser | nothing (no parameter block) | −£100 |
| `1.263285614` | Berkshire Regal 3.4 | Loser | **`Strategy Recommendation — NO BET`** | −£100 |
| `1.263285655` | Portman Blue 4.3 | **Winner** | direction + target, no execution | +£330 |
| `1.263285714` | Noble Grace 3.1 | **Winner** | direction + target, no execution | +£210 |

- **Only `1.263285614` is honest about it** (and it is the only one of the six whose no-bet is *intended* — every runner failed the VDW qualification test, so the prompt's gate fired correctly). The other five recommend a trade and never state whether it was placed.
- **A change from the previous review:** two days ago the defect was four outputs claiming `{"status": true}` with no settled bet. **This day, none of the six claims a successful execution** — they are silent instead. A silent failure is harder to detect than a false success, and it is not detectable from the output at all: the prompt has no position readback.
- **The cost is asymmetric:** four of the six favourites lost (sitting them out was right), but the two that won were the biggest-priced winners of the set (`…655` at 4.3, `…714` at 3.1). Net, sitting out all six cost **+£140** relative to backing every favourite.

### 6.2 Seven closes after the off (see §1)

The single largest execution-layer defect of the day. Half the day's profit came from it, which means half the day's profit is **not reproducible by the strategy as designed**.

### 6.3 One output with no executable parameter block

`1.263285449` was traded (`Back` 100 @ 4.4, closed `Lay` 3.65) but contains no `{"OpenBetPosition.BetType": …}` block, so its direction and target exist only in prose. The executed direction was `Back`, and its `Rank 1` was the favourite, so nothing was lost — but a report whose machine-readable contract can be omitted by the model is a report that cannot be validated automatically.

### 6.4 Not established, and worth saying so

- Whether the 7 late closes are a scheduling bug or a fill problem cannot be told from these logs: the outputs carry no clock and the host does not record why a close happened when it did.
- `Rank 1`'s −25.6% here and +40.9% two days ago are two draws from one small sample. Neither is an edge. What *is* consistent across both days is that the model's disagreements with the market performed worse than the market.

---

## 7. Recommendations

1. **Do not promote `Rank 1` to a bet.** Across the two days it is +£484.00 on 59 bets (+8.2% ROI) — entirely carried by two days' worth of favourite-driven wins, with the model's own disagreements losing money on both days. If it is to be tested further, it must be tested *only* on the disagreement subset, and that subset is 12–20 bets per day: years away from significance.
2. **Fix the close schedule in the host, and record the close time.** 7 of 23 closes ran past the off, and those 7 supplied 49% of the profit. The host already owns the timing guarantee (the prompt is forbidden from reasoning about time); it should also **log the close time against the off time** so this is measurable every day rather than reconstructed from bet records afterwards.
3. **Record the execution outcome in the host, and make the output report it.** Five of six non-executions are invisible. Either the host writes the strategy's own execution result next to the output, or the prompt gains a readback step — the prompt alone cannot see whether its call landed.
4. **Compare against the favourite every day, automatically.** On both days the trivial control ("back every favourite in the markets the AI looked at") beat the executed strategy — +£1,025.00 vs +£15.50, and +£1,020.00 vs +£176.87. This control costs nothing to compute and is the correct null hypothesis for every future day's number.
5. **Report the bet-only counterfactual as the headline, not the net P&L.** Both days, the executed figure is a hedge mechanic with a large bet-only loss underneath it (−£868.60, −£1,288.00). Publishing the net alone invites the reading that the handicapping worked.

---

## Appendix A — Per-market detail

| Market | Meeting / race | Favourite | Fav P | Fav result | Fav `Back` | `Rank 1` | R1 P | R1 result | R1 `Back` | Dir | Tgt | Traded | Closed < off | Executed |
| :--- | :--- | :--- | ---: | :--- | ---: | :--- | ---: | :--- | ---: | :--- | ---: | :--- | :--- | ---: |
| `1.263281704` | Fairyhouse 3m Hcap Hrd | Dinoland | 5.8 | Loser | −£100 | Dinoland | 5.8 | Loser | −£100 | Lay | 5 | yes | yes | +£0.69 |
| `1.263281728` | Fairyhouse 3m Hcap Hrd | Gypsy Poet | 5.8 | Loser | −£100 | Las Brisas Boy | 7.0 | Loser | −£100 | Lay | 10 | yes | yes | +£1.59 |
| `1.263281806` | Killarney 1m3f Hcap | Shays Dream | 5.2 | **Winner** | **+£420** | Wie Wunderbar | 5.8 | Loser | −£100 | Lay | 27 | yes | yes | +£4.59 |
| `1.263281813` | Killarney 1m3f Mdn | Shemiyka | 2.18 | Loser | −£100 | Shemiyka | 2.18 | Loser | −£100 | Lay | 11 | yes | yes | +£7.80 |
| `1.263285053` | Stratford 2m3f Hcap Chs | Expert Analysis | 1.87 | Loser | −£100 | Expert Analysis | 2.88 | Loser | −£100 | — | — | **NO** | — | — |
| `1.263285065` | Stratford 2m NHF | Mon Missel | 2.44 | Loser | −£100 | Turbogeist | 3.25 | Loser | −£100 | Lay | 22 | **NO** | — | — |
| `1.263285318` | Wolverhampton 1m1f Nursery | Solar Swing | 2.0 | **Winner** | +£100 | Solar Swing | 2.0 | **Winner** | +£100 | Lay | 12 | yes | **no (+118s)** | +£10.72 |
| `1.263285325` | Yarmouth 1m2f Hcap | Warning Symbol | 5.6 | Loser | −£100 | Lucky Hero | 5.9 | Loser | −£100 | Lay | 30 | yes | yes | +£4.07 |
| `1.263285332` | Yarmouth 1m Hcap | Media Legend | 4.7 | **Winner** | **+£370** | Rogue Allegiance | 8.6 | Loser | −£100 | Lay | 23 | yes | **no (+57s)** | +£4.89 |
| `1.263285449` | Wolverhampton 5f Nursery | Dreams Galore | 4.5 | **Winner** | **+£350** | Dreams Galore | 4.5 | **Winner** | **+£350** | — | — | yes | **no (+93s)** | +£20.54 |
| `1.263285455` | Wolverhampton 6f Nov Stks | Iron Dome | 1.32 | **Winner** | +£32 | Iron Dome | 1.32 | **Winner** | +£32 | Lay | 4 | yes | yes | +£14.03 |
| `1.263285462` | Wolverhampton 6f Mdn Stks | Lady Branksome | 2.8 | Loser | −£100 | Lady Branksome | 2.8 | Loser | −£100 | — | — | **NO** | — | — |
| `1.263285469` | Wolverhampton 6f Hcap | Apex Star | 2.66 | Loser | −£100 | Apex Star | 2.66 | Loser | −£100 | Lay | 7 | yes | **no (+69s)** | +£3.75 |
| `1.263285476` | Wolverhampton 1m1f Hcap | Faeberon | 4.1 | Loser | −£100 | Faeberon | 4.1 | Loser | −£100 | Lay | 10 | yes | yes | +£2.72 |
| `1.263285594` | Wolverhampton 1m1f Hcap | Hauteluce | 2.42 | **Winner** | +£142 | Hauteluce | 2.42 | **Winner** | +£142 | Lay | 7 | yes | yes | +£4.88 |
| `1.263285601` | Wolverhampton 1m4f Hcap | Mmmbop | 5.5 | Loser | −£100 | Caraway | 9.8 | **Winner** | **+£880** | Lay | 15 | yes | yes | +£2.51 |
| `1.263285607` | Wolverhampton 7f Hcap | Powder Monkey | 3.55 | Loser | −£100 | Powder Monkey | 3.55 | Loser | −£100 | **Back** | 10 | yes | yes | +£11.11 |
| `1.263285614` | Kempton 1m Nov Stks | Berkshire Regal | 3.4 | Loser | −£100 | Berkshire Regal | 3.4 | Loser | −£100 | Lay | 12 | **NO** *(declared)* | — | — |
| `1.263285621` | Kempton 1m Hcap | Bofa Beach | 3.0 | Loser | −£100 | Footstepinthewoods | 7.6 | Loser | −£100 | Lay | 30 | yes | yes | +£11.11 |
| `1.263285627` | Kempton 1m4f Nov Stks | Noble Journey | 1.52 | **Winner** | +£52 | Pursuit Of Love | 3.5 | Loser | −£100 | Lay | 30 | yes | **no (+131s)** | +£23.72 |
| `1.263285634` | Kempton 1m3f Hcap | Extraterrestrial | 4.4 | **Winner** | **+£340** | Sax Avoidance | 7.0 | Loser | −£100 | Lay | 15 | yes | yes | +£3.37 |
| `1.263285641` | Kempton 6f Hcap | Sparksmith | 3.55 | Loser | −£100 | Sparksmith | 3.55 | Loser | −£100 | Lay | 4 | yes | yes | +£1.04 |
| `1.263285648` | Kempton 6f Hcap | Mesaafi | 4.2 | **Winner** | **+£320** | French Affair | 4.5 | Loser | −£100 | Lay | 15 | yes | **no (+147s)** | +£3.89 |
| `1.263285655` | Kempton 7f Hcap | Portman Blue | 4.3 | **Winner** | **+£330** | Twitch | 11.0 | Loser | −£100 | Lay | 15 | **NO** | — | — |
| `1.263285662` | Kempton 7f Hcap | Ridger | 1.54 | **Winner** | +£54 | Ridger | 1.54 | **Winner** | +£54 | Lay | 12 | yes | **no (+140s)** | +£18.47 |
| `1.263285668` | Kempton 1m4f Hcap | Currumbin | 3.35 | Loser | −£100 | Currumbin | 3.35 | Loser | −£100 | **Back** | 15 | yes | yes | +£15.38 |
| `1.263285708` | Pontefract 1m Hcap | Always Blue | 4.8 | Loser | −£100 | Always Blue | 4.8 | Loser | −£100 | Lay | 6 | yes | yes | +£1.05 |
| `1.263285714` | Pontefract 2m2f Hcap | Noble Grace | 3.1 | **Winner** | **+£210** | Free World | 3.15 | Loser | −£100 | Lay | 12 | **NO** | — | — |
| `1.263285721` | Pontefract 5f Hcap | Jojo Rabbit | 3.6 | Loser | −£100 | Jojo Rabbit | 3.6 | Loser | −£100 | Lay | 17 | yes | yes | +£4.94 |
| | | | | | **+£1,020** | | | | **−£742** | | | 23/29 | 16/23 | **+£176.87** |

Favourite prices are the values the outputs stated (`Best Back Price`, or `Snapshot P` where the output printed both, and the single printed column where only one was given). `Rank 1` prices likewise. Counterfactual columns are £100 stake, `Back` only, settled at the race result, and exclude commission.

## Appendix B — Reproducing this review

| Script | Does |
| :--- | :--- |
| `scripts/parse_strategy_logs.py` | reads every `data/AIStrategyOutputs/*.md` (excluding `Old/`), extracts the field table, the marked favourite, `Rank 1`, the direction and the target, and prints one tab-separated record per log plus a list of logs it could not read |
| `scripts/analyse_trade_results.py` | joins those records to `get_all_bet_results()`, decides which leg is the opening leg, compares the closing leg's time with `startTime`, and computes the four P&L frames and Appendix A |

The market outcomes that `get_all_bet_results()` does not cover — the 6 untraded favourites and the `Rank 1` selections that were a different horse — were read from `get_market(marketId)` and are supplied to the analysis script as a name→`Winner`/`Loser` map. All 15 such lookups are listed in §Scope and Method.
