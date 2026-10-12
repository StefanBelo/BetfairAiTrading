# Bfexplorer — data-context and trade-selection know-how

**Purpose:** the agreed knowledge for this project, so a fresh session does not re-derive it and
re-make the errors it took a full afternoon to correct. Every statement carries its **provenance**.
Do not upgrade a claim beyond its provenance, and do not treat an `HYPOTHESIS` as a rule.

Provenance key:
- **[USER]** — stated by the user (bfexplorer's author). Authoritative. Do not re-raise or re-derive.
- **[VERIFIED]** — checked against returned data in this project; the check is stated.
- **[HYPOTHESIS]** — my (the agent's) inference. **Unvalidated.** May be wrong. Must not be baked
  into a prompt as though it were a fact.

Scope note: `bfexplorer` is the user's own application, and **he controls the shape of every data
context**. If a context needs to change, ask him — do not work around it.

---

## 1. Which context is which — and which may be used for what

| Context | Source | Character | May a prompt use it? |
| :--- | :--- | :--- | :--- |
| **`MarketSelectionsCandleStickData`** | **Betfair web API** — **[USER]** | **the market's own price/volume history**, with a `time` per bucket | **YES — it is the only legitimate price/volume history** |
| **`MarketSelectionsTradedPricesData`** | **only from when the bfexplorer app started monitoring that market** — **[USER]** | a **partial, app-observed** session summary | **NO — diagnostic only.** Never prompt input |
| **`Profit History`** | my own open position's P&L | the **outcome of the agent's own action** | **NO — diagnostic only.** Never prompt input |

**[USER] `Profit History` exists only so the developer can see how badly a selection was traded. It
cannot be used by the prompt.** Feeding it in would make each decision depend on the previous
trade's P&L — the rule would be adapting to its own outcome instead of to the market.

**[USER] `MarketSelectionsTradedPricesData` is app-observed, not market history.** Its `startPrice`
is the price when monitoring began, not the market's open; `tradedVolume` accumulates from that
moment. Consequences observed as it accumulated across three reads of one market on 2026-10-07 —
`76502681` Kiniro: `tradedVolume` **9,687 → 27,689 → 32,791**, `startPrice` **2.52 → 2.36 → 2.30**.

**[VERIFIED] The candle context DOES return on a settled/suspended market.** An earlier claim in this
project that "settled markets return nothing" was **wrong** — the empty results were a stale
`marketId`. On the settled Kempton 6f Hcap (`1.263382874`, `status: "Suspended"`) all 11 runners
returned full `candleStickData` series. **Settled markets are therefore testable after the fact.**

**[VERIFIED] The newest bucket in a live read is a window still in progress.** A bucket stamped `T`
covers `T` to `T + 5 min`, so a read taken at time `t` returns a **partial** bucket whenever `t`
falls inside a window. Kiniro's `20:17:36` bucket read `volume 5,547.94` when read live and
`7,400.98` when read after settlement — the **same window, five minutes further along**.
**This is arithmetic, not a defect, and it is no reason to distrust the context.** The real flaw is
in any test that *compares* buckets: **the newest bucket's `volume` covers only part of its five
minutes and must not be compared against complete ones.** A recency or rate test must either scale
it by elapsed time or use the last **complete** bucket. (Recorded originally as an "incomplete
bucket problem" — that framing was wrong: two reads five minutes apart of the same window are
supposed to differ.)

**[VERIFIED] In-play buckets are identifiable in the candle series.** The bucket containing the off
carries **`high` = 1000** (the ceiling marker for a horse that is not winning) and its **`low` is the
in-play peak price**. Examples from `1.263382874`: Blue Orbit `high 1000, low 4.2`; Goldwork
`high 1000, low 2`; Kiniro `low 1.01`. **This is how pre-off and in-play are separated.**

**[VERIFIED] `candleStickData` shape:** each bucket is one **5-minute window**, and **only windows
with trades are returned** — so bucket count and span vary per runner; compare buckets **by `time`,
never by index**. Per selection there are also whole-window aggregates `vwaPrice` and
`backLayRatio`.

---

## 2. Four measurement errors that were made and corrected in this project

Each of these was made by the agent, caught by the user, and must not be repeated.

| # | Wrong measure | Correct measure | Caught by |
| --: | :--- | :--- | :--- |
| 1 | **price distance** — `(high − low) / price` | **probability points** — `1/low − 1/high` | **[USER]** |
| 2 | **bucket index** — "the last third of the windows" | **the clock** — the buckets' own `time` stamps | **[USER]** |
| 3 | **a level** — live price vs `vwaPrice` | **a slope** — `1/close` across the recent buckets | **[USER]**, via evidence |
| 4 | **a field median** — "volume above the field's median" | **self-relative** — volume against *that selection's own book depth* | **[USER]** |

**On (1) — why price and probability differ, and why the sign flips.** A Betfair price is **odds**;
`1 / price` is the probability it implies. A price move is **not** a linear probability move, and the
**signs are opposite**: price up = probability down. `2.20 → 2.02` is `45.5% → 49.5%` = **+3.9
probability points**; `13.5 → 12.4` is `7.4% → 8.1%` = **+0.6 points** — the same ~8% *price* move,
six times the probability. `EV = p / marketProb − 1` is already a *relative probability* error and
needs no conversion; a `Profit: N` percentage target is a fixed relative-probability target at any
price.

**On (2) — why `lateShare` was wrong.** It took "the volume of the last third of the returned
buckets as a share of the total". But the buckets are 5-minute windows **with gaps**, so a busy
runner's last third spans ~20 minutes and a quiet one's ~90. **It is not a time measure at all.**

**On (3) — why the `vwaPrice` level test was wrong.** `vwaPrice` is a volume-weighted average over
the **whole returned window**. A runner whose price had collapsed from 28 to 14.5 read as "above its
average" — the test **blocked** a selection that subsequently traded to **1.51**. **A level cannot
see a trend.** Note: two prices may be compared in price space when only the **sign** matters.

**On (4) — why the field median was wrong.** Observed on `1.263382874`: the runner a field-median
volume test **passed** carried **1,135** in its most recent bucket; the one it **failed** carried
**7,401**. A field median cannot express liquidity, because **liquidity is only meaningful against
the size to be traded** — an order of a few hundred against €118–€235 at the touch *is* the price
move, and the agent was the print.

---

## 3. Off-time / timing

**[USER] The agent has no access to system time**, and a prompt must never state or invent a time.
It does not need one: **`startTime` (market envelope) minus the latest `candleStickData[].time`
across the field** gives the time remaining, from timestamps already in the data. Compare them in the
same zone — buckets carry a local UTC offset, `startTime` is `Z`.

**[VERIFIED] Run time determines whether there is any signal to select on.** Two runs on the same
market, `1.263382867` Kempton 7f Hcap (off 20:00 local):

| Run | Latest bucket in the payload | Time to the off | Selected | Whole-race probability move of the pick |
| :--- | :--- | --: | :--- | --: |
| 1 | 18:54 | ~66 min | Mojito | **+0.1 points** |
| 2 | 19:50 | ~10 min | Because We Can | traded to **1.51** |

**An hour out the informed money has not arrived, and a ranking produced then is noise.** A rule that
must rank something will rank something.

---

## 4. `backLayRatio` — what it is, and what it does not do

**[USER]** `backLayRatio` is the **back/lay volume ratio**: 0.5 neutral, **below 0.5 = more laid,
above 0.5 = more backed**. It is a **cumulative figure for the whole returned window** — one value per
selection, not per bucket.

**[VERIFIED] It does not predict direction.** On the settled Kempton 6f Hcap `1.263382874` (11
runners), **every** runner was back-sided above 0.5, **three drifted out**, and the **two most
back-sided went opposite ways** (Calling A Star 0.790 came in; Perfect Location 0.723 drifted out).
Earlier partial readings in the near-neutral band were wrong three times (0.73, 0.55, 0.56); the
only readings that ever agreed with direction were the extremes (0.32, 0.44). **Use it as
corroboration at most; never let it set the trade direction.**

---

## 5. The execution contract

**[USER / template-verified]** `Trade x profit`, with `get_strategy_settings` unless overridden —
exactly **two** keys passed:

```
execute_strategy_settings_with_parameters(
  strategyName: "Trade x profit",
  marketId:      <marketId>,
  selectionId:   <tradedSelectionId>,
  parameters:    '{"OpenBetPosition.BetType":"Back"|"Lay","CloseBetPosition.Profit":N}')
```

Stake and its type are **inherited from the setting** — never passed. The setting carries `Stake`,
`StakeType`, `ProfitLossType`, `Loss` and `ClosePositionTimeSpan`; the strategy closes before the
off.

**[USER] A strategy *setting* is not an executed *instance*.** Reading a setting says nothing about a
running strategy's behaviour, what filled, or what a trailing stop did. Do not reason from setting
parameters to instance behaviour, and never present such inference as evidence.

---

## 6. The test/calibration method this project now has

**[HYPOTHESIS — method, not result]** Because candles return on settled markets and the last live
bucket is incomplete:

1. At decision time, record the **snapshot actually used** (the candle series as returned then).
2. After settlement, fetch the **same market's candles** again.
3. Compare the snapshot against the **settled pre-off range**, per runner, in **probability points**.
4. Score the selection rule on: did it pick a runner whose **pre-off range was large enough to trade
   and deep enough to absorb the stake**; and how much of that range was **still ahead** at the
   moment of the decision.

This is the only test in this project that measures the *decision* rather than the *result*. It
requires many markets — **one is not evidence.**

---

## 7. Agent failure modes recorded for future sessions

- **Reading absence into a truncated payload** — claimed `nigelHorseDetail` was absent on a maiden
  (it returns, reduced), claimed settled markets return nothing (stale `marketId`), claimed the
  candle context has no `vwap` (it has `vwaPrice`).
- **Treating a diagnostic context as data** — proposed adding `Profit History` to the prompt and
  calling the prompt's "no position readback" claim false. **Both wrong.**
- **Inventing field semantics** — "the trend is against it", "the move has completed" — as
  description, when they were inference. Mark inference as inference.
- **Building a rule from one market** and revising it four times in an afternoon. §0 in
  `HorsesBetfairRaceInfoDataVDWTradeNewV1.md` is such a construction and is **not validated**.
- **Answering the easier question.** Asked which selections were tradeable, the agent repeatedly
  answered "which horse won" and had to be corrected to the decision-calibration question.
