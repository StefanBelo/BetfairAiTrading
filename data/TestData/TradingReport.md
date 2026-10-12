
**Overall Assessment:**

**Race conditions.** Newcastle, `6f Hcap`, `race_type: A/W`, `class_num: 6` (the weakest grade), `going_group: Standard`, distance **6f = 1,320 yards** (the market name and the race data agree on the trip). Ten declared runners, **all `Active`**, market `status: Open`, `totalMatched` £53,519. This is a low-grade all-weather sprint handicap: form is compressed, the draw component is live (Flat) though its spread is narrow (53.3–57.8), and the `going` component is real here (no `none` confidences returned).

**Race shape.** `style_label` counts: **6 Hold-up** (Blazing Son, Dance Fantasy, Men Of Honour, Brazen Idol, Pockley, Coulsty Exchange, Cape Sovereign — 7 actually), **2 Prominent** (Get Up Everybody, Mayo County), **1 Leader** (Bellagio Man). One Leader and two Prominent types against seven hold-up runners implies a contested gallop is *plausible but not guaranteed*; with a single front-runner the shape is a tendency only. **Per the provenance caveat (NH styles persist ~49%), I use this as a tendency and let it move no rating materially.** No runner is `Unknown`.

**Coverage audit** — every one of the ten runners returned all four contexts (`nigelTissuePricer`, `nigelRaceRadar`, `horseDetail`, `priceMetricsData`). See the dedicated section below for the malformed-row and no-history findings.

**Most promising.** Get Up Everybody is the clear form pick (won at the trip in August, placed repeatedly at 6–6.5f, best `form`/`dist_suit` components, best radar profile). Mayo County is the consistent placer. Nothing else has a convincing recent 6f case.

**Largest disagreement with the market.** **Blazing Son** — the market's 2nd favourite at `5.3` (18.75% de-vigged) against my 13.00%. Both the tissue (raw 8.52%) and I dispute that price.

**Multiple-edge count.** **7 of 10 runners show positive `Edge`** — more than a third, which is the signature of an incomplete information set (I have no headgear, stable news, finer ground detail or non-completion reasons). **Where they sit matters:** three of the four material positive edges (Brazen Idol 16.0, Bellagio Man 17.0, Pockley 24.0, Coulsty Exchange 30.0) fall **inside the vendor's credible `8`–`50` edge zone**; the largest, **Cape Sovereign at `60.0`, is above `50` and is arithmetic, not value** — I do not present it as such.

**Head/tail calibration.**
- **Head:** my figure for the market's de-vigged favourite (Get Up Everybody) is **26.50% vs the market's 25.48%** — mine is **higher by 1.02 points**. My head is *not* mechanically depressed.
- **Tail (`price ≥ 8.0`):** my total for the seven such runners is **44.00% vs the market's 39.75%** — **1.11x**, below the `1.25x` flag. **The fat-tail flag did not fire.**

**Shape comparison.** My top horse is the **same horse** as the market's favourite, and the same *kind* of favourite — a short, clear ~25% favourite at `3.9`. The market treats four-five runners as live (top five = 78.84%); my vector likewise concentrates 73.50% on its top five. Contenders, not runners: a 10-runner field with one clear favourite and a compact middle — my shape matches the book's.

**The three things this prompt trades on:** the favourite's **`EV = +0.0335`**, implying **`Back`**, with profit target **`min(round(0.0335 × 100), 30) = 3`**. The cap does not bind.

---

**Field Probabilities & Expected Value:**

| Horse | Strength Rating/10 | Model Probability (%) | Best Back Price | Market Probability (%) | EV | Edge | Rank |
| :--- | ---: | ---: | ---: | ---: | ---: | ---: | ---: |
| **Get Up Everybody** *(market favourite — traded)* | 8.5 | 26.50 | 3.90 | 25.48 | +0.03 | +0.04 | 1 |
| Mayo County | 7.5 | 16.50 | 6.20 | 16.03 | +0.02 | +0.03 | 2 |
| Blazing Son | 6.5 | 13.00 | 5.30 | 18.75 | −0.31 | −0.31 | 3 |
| Dance Fantasy | 6.0 | 9.50 | 10.00 | 9.94 | −0.05 | −0.04 | 4 |
| Brazen Idol | 5.7 | 8.00 | 16.00 | 6.21 | +0.28 | +0.29 | 5 |
| Bellagio Man | 5.5 | 7.00 | 17.00 | 5.84 | +0.19 | +0.20 | 6 |
| Men Of Honour | 5.0 | 6.50 | 11.50 | 8.64 | −0.25 | −0.25 | 7 |
| Pockley | 4.8 | 6.00 | 24.00 | 4.14 | +0.44 | +0.45 | 8 |
| Coulsty Exchange | 4.0 | 4.00 | 30.00 | 3.31 | +0.20 | +0.21 | 9 |
| Cape Sovereign | 3.0 | 3.00 | 60.00 | 1.66 | +0.80 | +0.81 | 10 |

**Totals actually computed: Model column = `100.00%` across all ten active runners. Market column = `100.00%` across the ten priced runners** (no runner at the ceiling, so the two columns share a denominator). Both columns use largest-remainder rounding.

**De-vig working (reproducible from the printed prices).** `rawImplied = 1 / price`:
`0.25641 + 0.18868 + 0.16129 + 0.10000 + 0.08696 + 0.06250 + 0.05882 + 0.04167 + 0.03333 + 0.01667 = 1.00633` → **book = 100.63%** (a small over-round). Each market probability above is that runner's `rawImplied / 1.00633 × 100`.

**Favourite's break-even price = `1 / (26.50 / 100) = 3.774`.** At the offered `3.90` my estimate clears the break-even, so the sign of `EV` is positive — and the break-even comparison agrees with it.

---

**Horse Notes** *(rank order; `roll_fin_rel` reading used = **lower is closer to the front**, verified this race: the field's best strike rates sit at the low end — Get Up Everybody 0.255, Mayo County 0.284, Bellagio Man 0.461 — and the worst-form runner at the extreme high end, Cape Sovereign 0.879).*

**1. Get Up Everybody (75281339) — 8.5 — 26.50%.** 4yo gelding, 134lb. Won at **1,320y (6f)** on 20 Aug (`1/14`, SP 7.43, in-play min **1.01 = confirmed win**), and placed `3/11` (1 Aug, 1,546y), `3/11` (18 Jul, 1,320y) — five of six runs sit between 1,100y and 1,546y, i.e. inside ±1f of today's trip. Best `form` (58.1) and `dist_suit` (62.6) components in the field, `fitness` 50 `high`; radar Prominent with the **field's lowest `roll_fin_rel` (0.255)**, win rate `0.20` and low dispersion (0.191) — the most reliable profile here. **Negative:** the latest run (19 Sep, 1,320y) traded to a `2.2` in-play low and finished `4/9` — traded like a winner and did not get home. My 26.50% sits just **above** the market's 25.48% (1.04x): we agree this is the form pick; the 1.0-point premium is my trip record against the market's sight of that last beaten run.

**2. Mayo County (64561304) — 7.5 — 16.50%.** 5yo mare, 134lb. **Every one of six runs at ~1,540y (7f):** `3/15`, `10/14`, `4/11`, **WON `1/11`** (25 May, min 1.01), `3/8`, `3/14` — an exceptionally consistent placer stepping *down* to 6f. `form` 56.1 (2nd best), `fitness` 56.7; radar Prominent, `roll_fin_rel` 0.284, win rate 0.20. **Caveats:** `going` 33.8, and the 19 Jun run traded 3.95 but finished `10/14` — it does not always get home. The 6 Jun pair (`max 1, min 1001`) is **corrupt — prices discarded, position `4/11` kept**. Mare in an open handicap (small historical penalty, not over-weighted). My 16.50% ≈ the market's 16.03%.

**3. Blazing Son (36452424) — 6.5 — 13.00%.** 8yo gelding, 136lb. **The largest disagreement in the race.** The market's 2nd favourite at `5.3` (18.75%, £10,114 matched); I rate 13.00%. Form is uninspiring: `8/9`, `10/13`, `3/9`, `5/10`, `8/10`, `9/14` — **five of six at 1,554y (7f) and only one at 6f (`8/10`)** — with **no in-play low under 8.6 in any recent run** (it is never in contention in running). Tissue `form` 26.6 (weak) agrees. The positives are `going` **72.4 (best in field)**, `course_dist` 67.5, `dist_suit` 69.7 and a `bias_flag: "agree"` — the model sees a 7f→6f drop suiting an 8yo whose bare form does not support 18.75%. At 0.69x the market I am **inside the 0.4x cap and I name the reason** (one unplaced 6f run, no recent in-running contention). Recorded as **high uncertainty** — the money is a view I cannot audit.

**4. Dance Fantasy (92754398) — 6.0 — 9.50%.** 3yo filly, 135lb. **The field's best `trainer` component (62.0)**, jockey 50.8. Form is poor: `5/9`, `4/4`, `3/7`, `8/13`, `12/14`, with a ~6-month break between March and September. `going` 20, `course_dist` 5 (`low`), `dist_suit` 39.8 (`low`). My 9.50% ≈ the market's 9.94% — we are both essentially pricing the yard.

**5. Brazen Idol (40705717) — 5.7 — 8.00%.** 7yo gelding, **top weight 138lb**. **Highest `rating` component in the field (60.2)**, plus `going` 67.1, `dist_suit` 67.6, `fitness` 62.5, `career` 55.8. One genuine effort: `2/5` at 5f on 23 Jun (min `1.59` — traded as the likely winner). Against that: recent form `11/11`, `10/11` and top weight. My 8.00% vs market 6.21% = **1.29x** — an in-zone (16.0) edge I will hold.

**6. Bellagio Man (38983374) — 5.5 — 7.00%.** 8yo gelding, 136lb. Two wins at **1,320y** in Oct 2025 (both min `1.01`) give it the **field's best radar win rate (0.40)** — but with the **highest dispersion (std 0.439, "Win or Bust")** and style Leader. Recent form `5/8`, `10/12`, `12/13`; `going` 42.8, jockey 28.8 (weak); 37 days off. My 7.00% vs market 5.84% = 1.20x.

**7. Men Of Honour (72999467) — 5.0 — 6.50%.** 4yo gelding, 131lb. **Six runs, no placing:** `7/10`, `7/13`, `5/8`, `9/10`, `6/9`, `9/10`, and no in-play low better than `13` — never in contention. Tissue `form` 24.5, `course_dist` 5 (`low`). The 22 Aug pair (`max 1000, min 500`) shows it was **never backed in running at all** — a negative, not missing data. My 6.50% is **below** the market's 8.64% (0.75x): I cannot find the `11.5` price on this record.

**8. Pockley (35514509) — 4.8 — 6.00%.** 8yo gelding, 131lb. Good conditions scores (`going` 68.9, `course_dist` 61.4, `fitness` 62.5) and a 5f win on 21 Oct 2025 (min `1.01`), but its last run (17 Sep) was a **`20/20` at an SP of 350.77** after a nine-month break, and all its form is at 1,100y (5f). My 6.00% vs market 4.14% = **1.45x** — an in-zone (24.0) divergence resting on the conditions scores; flagged for uncertainty.

**9. Coulsty Exchange (94147984) — 4.0 — 4.00%.** 4yo filly, **bottom weight 124lb**. Only three runs, **all at 1,540y+ (7f–1m)**, last on 11 Feb 2026 — a ~7.5-month absence *and* a trip question. `rating` 32.1 (worst in field), `course_dist` 5 (`low`), `dist_suit` 45.3 (`low`). My 4.00% vs the market's 3.31% — close; bottom weight and an unexposed profile are the only positives.

**10. Cape Sovereign (71058775) — 3.0 — 3.00%.** 4yo gelding, 129lb. **Field's worst form:** six runs, best finish 7th (`8/10`, `10/12`, `11/11`, `13/13`, `9/11`, `7/9`), tissue `form` **8.5**, and it ran two days before this card (30 Sep, `8/10`). It traded to 5.7 in running once (`7/9`). My 3.00% vs the market's 1.66% = 1.81x — inside the 2.5x cap, but **a positive `EV` of +0.80 on a `60.0` shot is arithmetic, not value, and I do not present it as value.**

---

**Coverage Audit:**

- **Missing contexts:** none — all ten runners returned all four contexts.
- **Ceiling (`1000`) market prices:** none. No runner was excluded from the de-vig; **the model and market columns share the same denominator (all ten runners)**.
- **Corrupt in-play row:** **Mayo County, 6 Jun 2026** — `inPlayMaxPrice: 1`, `inPlayMinPrice: 1001` (min > max, and above the ceiling). **Prices discarded, finishing position `4/11` kept.**
- **`min == max` rows (uninformative, excluded from the contender proxy):** Blazing Son 5 Jul (`8.6/8.6`); Mayo County 9 Sep (`11/11`).
- **Zeroed rows:** none.
- **No two-hour history:** none — **every runner returned exactly two `priceMetricsData` buckets.** Bucket windows are **per-runner and not aligned** (latest buckets range 20:10 to 21:02 local), so I compare each runner only against its own history.
- **`roll_fin_rel` direction:** verified empirically as **lower = closer to the front** (see note header).

---

**The trade:**

**The market's favourite is Get Up Everybody (`selectionId 75281339`)** — shortest `Active` price at **`3.90`**, fixed from the Step 1 read and never re-derived. It is **also my Rank 1** (26.50%), which is unusual for this method but is what this field's evidence shows.

- **Best back price:** `3.90` · **My model probability:** `26.50%` · **Market (de-vigged):** `25.48%`
- **`Edge = 26.50 / 25.48 − 1 = +0.04`**

**Direction — `EV` substitution in full:**
`EV = (26.50 / 100) × 3.90 − 1 = 0.2650 × 3.90 − 1 = 1.0335 − 1 = +0.0335` → **positive → `Back`**.

**Break-even cross-check:** `100 / 3.90 = 25.64%`. My `p = 26.50% > 25.64%` → **the `Back` branch is confirmed by the break-even comparison**, so there is no arithmetic fault. (Break-even *price* = `1 / 0.2650 = 3.774`.)

**Profit target:** `min(round(|+0.0335| × 100), 30) = min(round(3.35), 30) = min(3, 30) = ` **`3`**. The cap does **not** bind, so there is no raw figure beyond `3` to report. Rounding moved the target by 0.35 points (a fractionally *easier* target than the exact 3.35). `EV = 0` is not in play here.

**The favourite's own components.** *Strong:* `dist_suit` 62.6, `form` 58.1 (both best in field), `going` 56.7, `draw` 54.2, and a Prominent/low-volatility radar profile. *Weak:* `trainer` 36.5, `jockey` 39.0, `career` 42.8. **The price I would have rated it at is `1 / 0.2650 = 3.77`**, against a market `3.90` — a narrow 3.4% disagreement in my favour. This is a **reporting duty, not a justification**: I cannot observe why the market prices it at 3.90 (no headgear, stable news or in-running comments are available to me), and my side of a 3.4% gap is as likely to be my blind spot as a market error. **The head/tail flag did not fire, so neither the direction nor the target inherits any tail-weighting distortion.**

**Exact `parameters` JSON string:**
`{"OpenBetPosition.BetType":"Back","CloseBetPosition.Profit":3}`

---

**Price-metric trend (last two hours):**

Get Up Everybody and Rank 1 are **the same runner**, so one history serves both: **two buckets received**, opening **19:42** and closing **20:43** (local `+02:00`) — roughly an hour apart. (The field's latest buckets are not aligned; this runner's own window is what is used.)

| Reference | Price | `EV = 0.2650 × price − 1` |
| :--- | ---: | ---: |
| **`EV_now`** (live price) | 3.90 | **+0.0335** |
| **`EV_prev`** (older bucket `close`) | 4.60 | **+0.2190** |
| **`EV_vwap`** (latest bucket `vwap`) | 4.1078 | **+0.0886** |

- **The price move:** `3.90 − 4.60 = −0.70`, i.e. **−15.2%** — the price has **shortened** over the window.
- **`EV_now − EV_prev` = `+0.0335 − 0.2190 = −0.1855`** — and `p × (price_now − price_prev) = 0.2650 × (−0.70) = −0.1855` exactly. **This is the price move rescaled, not new information.**
- **How much of the required move has already travelled.** The trade is a **`Back`, which needs the price to shorten**. A 3% target corresponds to closing near `3.90 / 1.03 ≈ 3.79` — a further **~0.11 (≈2.8%)** of shortening. From the older reference `4.60` to that target `3.79` is a journey of `0.81`; the move already made (`4.60 → 3.90` = `0.70`) is **≈86% of it**. The window's move is **with** the trade, and the target sits essentially within a tick or two of the current price.
- **Reachability — per-bucket `high − low`:** older bucket `4.6 − 4.3 = 0.30`; latest bucket `4.7 − 3.6 = **1.10**`. The most recent hour's realised range is **ten times** the ~0.11 still required — the target is well inside what this price has already demonstrated it moves.
- **Momentum verdict:** **with the trade's required direction.** The price shortened 15.2% across the window, the latest bucket closed `3.85` with a low of `3.6`, and volume rose from 1,258 to 8,333. *One caution:* the latest `vwap` (`4.11`) sits well **above** the live back price (`3.90`), so the money over the hour was taken at a higher average than the price now on offer — the current back price is short relative to where the volume traded.
- **The trend changes nothing.** Direction (`Back`) and target (`3`) come from **`EV_now` alone**. Here the two **agree** — the history moves with the `Back` — and the trade follows `EV_now`.

---

**Strategy Recommendation:** one trade, on **the market's favourite as fixed in Step 1** — Get Up Everybody (`75281339`), the shortest-priced `Active` runner, never a runner of my own choosing. Direction and target come from `EV_now` alone (3d); the price-metric trend (3e) is reported and never traded on. No re-read was required — no runner has moved ~20% or more from the Step 1 snapshot (the largest is Cape Sovereign's `42→46` older close, i.e. within the window, and the live prices still match Step 1). **There is no second path, no second execution and no stand-aside.**Execution confirmed (`status: true`).

**Trade placed:** `Trade x profit` on **Get Up Everybody** (`selectionId 75281339`) in market `1.263162722` — **`Back`** at the market's favourite, with a **`3`** percent profit target. The stake, stop-loss and pre-off close schedule were inherited from the saved setting; only `OpenBetPosition.BetType` and `CloseBetPosition.Profit` were set.