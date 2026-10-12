Message
I'll begin by identifying the active market.Market identified. Now gathering all four data contexts for the whole field in one call.# Hexham — 2m7f Hcap Hrd Handicapping Report

**Market:** `1.263162606` · Hexham · 2m7f Hcap Hrd (Hurdle, Class 5, Good) · status **Open** · market `totalMatched` £51,618
**Step 1 did the only state test available:** `status == "Open"` and all five runners `Active`. No clock is read, and none is claimed.

---

**Overall Assessment**

A five-runner Class 5 staying handicap hurdle on Good ground at Hexham. Race data gives the trip as **`2m 7f 63y` = 5123 yards** (2×1760 + 7×220 + 63), while the market name says `2m7f` (5060 yards) — a 63-yard disagreement; I use the race data as authoritative and note the gap. Race type is returned as **`Hurdle`** (a discipline value, distinct from the `Chase`/`Turf`/`A/W` shapes listed in the context notes). `going_group` = `Good`, `class_num` = 5 (a weak grade, so form lines are worth less).

**Race shape:** the radar classifies only four runners — `Hold-up` (Frisky Whiskey, Killybegs Jet Lady) and `Leader` (Stylish Recruit, Scottish Dancer), with **Robert Dores unclassified** (no radar at all). Two committed front-runners against two hold-up horses is a genuine pace contrast, but NH styles persist only ~49% of the time, so I treat it as a tendency and let it move no rating more than slightly.

**Head/tail calibration:** the market's de-vigged favourite is **Frisky Whiskey at 33.13%**; my figure for the same horse is **24.00% — 0.72× the market's**, at the very floor of the method's observed `0.72x`–`1.00x` band. My share of runners priced **≥ 8.0** is **23.50%** against the market's **19.61%** — a **1.20×** tail, *below* the ~1.25× flag threshold, so the flag does **not** fire, but the tail is fat enough that it mechanically depresses the favourite's figure. Field-size band: for ≤5 runners the anchor is **35–60%**. My vector's top is **26.50%** and the market's favourite is **33.13%** — both **below** the anchor, because this is a genuinely closely-matched head (three runners under 4.6, four under 11.0). I state the disagreement rather than bending the numbers: this reads as a "four-contenders" five-runner race, not a dominant-favourite one.

**The largest model-vs-market disagreement in the race is the favourite itself** — Frisky Whiskey, market 33.13% vs my 24.00% (0.72×). **Multiple-edge count: 4 of 5 runners show positive `EV` (80%, far above one-third).** That count is a property of under-rating the favourite (probability taken off the head lands on the tail, since both columns sum to 100%), not four independent discoveries; it is the signature of my information being a strict subset of the market's (no trainer/jockey names, headgear, stable news or in-running comments). The edges cluster at **9.4 / 11.0** (inside the vendor's `8`–`50` edge zone) and **4.5** — none above `50`, so none is a longshot arithmetic artefact.

**What this prompt trades on — the favourite alone:** Frisky Whiskey's **`EV` = −0.28**, which implies **`Lay`**, with profit target `min(round(0.28 × 100), 30)` = **28** (integer). A `Lay` on the favourite is this method's *default* output, not a discovery — the model priced the market's favourite below the market in every card in the log.

---

**Field Probabilities & Expected Value**

★ = market favourite (the traded runner). ★★ = Rank 1.

| Horse | Strength Rating/10 | Model Probability (%) | Best Back Price | Market Probability (%) | EV | Edge | Rank |
| :--- | ---: | ---: | ---: | ---: | ---: | ---: | ---: |
| ★ Frisky Whiskey | 6.8 | 24.00 | 3.00 | 33.13 | **−0.28** | −0.28 | 3 |
| ★★ Stylish Recruit | 7.4 | 26.50 | 3.95 | 25.17 | +0.05 | +0.05 | 1 |
| Scottish Dancer | 7.3 | 26.00 | 4.50 | 22.09 | +0.17 | +0.18 | 2 |
| Killybegs Jet Lady | 4.9 | 12.00 | 11.00 | 9.04 | +0.32 | +0.33 | 4 |
| Robert Dores | 4.6 | 11.50 | 9.40 | 10.57 | +0.08 | +0.09 | 5 |

*Model column totals **100.00%** across all five active runners. Market column totals **100.00%** across the same five runners — **no runner is excluded**: every runner carries a valid, non-ceiling price, so the two columns share one denominator. **The favourite's break-even price is `1 / 0.24` = 4.17** — above its 3.00 back price, which is why its `EV` is negative and the direction is `Lay`.*

---

**Horse Notes** (rank order)

**1. Stylish Recruit — 7.4, 26.50%.** The strongest conditions profile in the field: won over **exactly 5123 yards** (1/11, June, `inPlayMinPrice 1.01` = a confirmed win) and was **2nd at 5123** (2/6) — the only runner with two in-tolerance runs at this exact trip. `course_dist` **79.8 / high**, `going` **72.0 / high**, `form` **69.1 / high** all corroborate. Against it: **top weight 167 lb**, an **8yo**, and **103 days since its last run** (>60 = fitness uncertainty; `fitness` component a soft 47.5). Its two wins came at 5123 and at 4428; its poor runs (5/12, 6/15, 10/11) were also at shorter trips. I price it mid 20s — **above** its de-vigged market 25.17% only marginally, because the weight and layoff genuinely temper the course record. Not the trade.

**2. Scottish Dancer — 7.3, 26.00%.** Freshest horse in the race (**24 days** since a **win**, 1/8 at 5092 — in tolerance, `inPlayMinPrice 1.01`). Highest `rating` in the field (**57.7**), `form` 64.5, `career` 56.9, `fitness` 55, three wins in six. The blemish is a **13/16 at 4620** (Oct 2025) and top weight **168 lb**; `course_dist` is only **70.2 / low**. Non-win in-play lows: 2.04 (2/6), 4.4 (3/9), 16.5 — a horse that trades short and finishes there. Mid 20s, fractionally **below** Stylish on the weight and course record. Not the trade.

**3. ★ Frisky Whiskey — 6.8, 24.00%.** Market favourite, but the weakest win credentials of the three: **0 wins in 5 starts**, placed 2/12, 3/6, 4/10, 6/8, 6/10. Her best run is the **2nd at 5583** (Aug 29) — outside the ±220-yard tolerance, though she stayed it, and her `inPlayMinPrice 3` there is the *lowest non-winning in-play low in the field*, so that run was a real throw at the prize. Against a market price of 3.0: `course_dist` **5.0 / low** (essentially no course record), `dist_suit` 45.4 / low, `form` only 40.7, `trainer` 29.2 / `jockey` 34.0 (weak connections), and `roll_fin_won = 0`. She is a lightly-raced improving 5yo mare and that is the whole bull case — the *tissue*, which prices independently of the market, has her at **7.7** (16.74% de-vigged), far below the exchange's 3.0. I respect the market more than the tissue here and price her **24.00%** — but still a clear under-rating. **Lay.**

**4. Killybegs Jet Lady — 4.9, 12.00%.** Real staying form — won at **5156** (1/5, `inPlayMinPrice 1.01`) and was **2nd at 5321** (2/13), with `dist_suit` **75.5 / high** — but **three non-completions in six** (`P/7`, `B/10`, `P/9`) plus an 11/12, and the **highest dispersion in the field** (`roll_fin_std` **0.511**). A 9yo mare who either lands or doesn't. `course_dist` only 13.3. Priced **above** its 9.04% market — that is the fat-tail effect in action, and it is also the field's one edge sitting inside the vendor's `8`–`50` zone. Not the trade.

**5. Robert Dores — 4.6, 11.50%.** **No tissue and no radar** — priced from `horseDetail` alone. Bottom weight **153 lb** (conceding 14–15 lb to the top weights) and a win over **5123** (Oct 2025, 1/8, `1.01`), which is the entire positive case. Recent form is poor — 6/6, 7/15, 6/9, 6/10 — it is a **9yo**, and it is **118 days** off. Its best non-win in-play low (2.02, 4/10) shows it can travel, but not recently. Widened uncertainty for missing contexts; priced close to, and a touch above, its 10.57% market.

**Rank-1 note / `roll_fin_rel` reading:** I used the **"lower = closer to the front"** reading (verified field-internally: the best strike rates — Stylish Recruit 0.4 at 0.184, Scottish Dancer 0.4 at 0.250 — sit at the **low** end, while the worst-form runner and the highest dispersion sit at 0.419 / 0.598). It was a tie-breaker only, never a primary input.

---

**Coverage Audit**

- **Robert Dores (`52783637`) is missing `nigelTissuePricer` AND `nigelRaceRadar`** — it carries only `horseDetail` and `priceMetricsData`. Per the rules it gets **no model price and no style/consistency measure**; I scored it from `horseDetail` alone and widened its uncertainty. Its absence also depresses the tissue book sum to **77.96%** (four of five runners), so those tissue figures are conditional on the covered runners and are not comparable to a fully-covered field. The gap is **not** treated as a discount.
- **No runner is priced at the ceiling (`1000`)**; all five carry genuine offers, so **no runner is excluded** from the market column and both columns share one denominator.
- **All five runners have `horseDetail`** and **all five have two `priceMetricsData` buckets** — full price-metric coverage. The buckets are **per-runner and not aligned** (Frisky's older bucket at 15:01, Stylish's at 14:58, Robert's latest at 16:39), so no bucket is cross-compared between runners.
- Non-completions counted, not averaged: Killybegs Jet Lady `P`/`B`/`P`; no malformed in-play pairs (`min > max`, values > 1000) encountered; two `min == max` runs (Robert Dores 9/9) were excluded from the contender proxy.

---

**The trade**

**Market favourite (fixed in Step 1): Frisky Whiskey — `selectionId 10583652`**, best back price **3.00**, my model probability **24.00%**, **`EV` = −0.28**, `Edge` = −0.28. She is **not** my Rank 1 (that is Stylish Recruit, 26.50%) — the favourite is my Rank **3**, exactly the pattern the prompt expects.

- **Direction:** `EV ≤ 0` → **`Lay`**. A negative `EV` means my model rates her *below* the market; laying at 3.00 returns `EV_lay = +0.28`. This is the method's **default direction**, not a claim that the market is wrong.
- **Profit target:** `min(round(0.28 × 100), 30)` = `min(28, 30)` = **28** (whole number; the cap does not bind, raw figure 28).
- **Her weak components:** `course_dist` **5.0 / low**, `form` 40.7, `trainer` 29.2, `jockey` 34.0, `dist_suit` 45.4 / low, `roll_fin_won` **0**. **Strong components:** `rating` 55.2 / high, `going` 44.8 / high, `fitness` 52.5. I would have rated her around **4.2–4.5** (≈23–24%) on the evidence; I cannot see *why* the market has her at 3.0 (no trainer/jockey names, headgear or stable news), so that gap is a reporting duty and **not** a reason to withhold the trade.
- **Exact `parameters` JSON string:** `{"OpenBetPosition.BetType":"Lay","CloseBetPosition.Profit":28}`

---

**Price-metric trend (last two hours)**

**Favourite — Frisky Whiskey: 2 buckets**, older `2026-10-02T15:01:02+02:00` (close 3.15), latest `2026-10-02T16:01:23+02:00` (close 2.98, vwap 2.9594, volume 14,430 — volume expanding sharply from 623).

- `EV_now` (live 3.00) = **−0.28**
- `EV_prev` (older close 3.15) = **−0.244**
- `EV_vwap` (latest vwap 2.9594) = **−0.290**

**Trend:** price moved **3.15 → 3.00, −0.15 (−4.76%)** — i.e. it **shortened**. `EV_now − EV_prev = −0.036`, which is exactly `p × (price_now − price_prev)` = 0.24 × (−0.15) — **the price move rescaled, not new information**.

**Fraction of the required move travelled:** the target is 28%, and a `Lay` needs the price to **drift out** (≈3.00 → ≈3.84, ≈ +28%). The price has instead gone the *other* way by 4.76% — roughly **−18% of the required move**, i.e. it has moved **against** the trade's required direction.

**Reachability:** latest bucket `high − low` = **3.30 − 2.74 = 0.56** (older bucket only 0.10). The price *can* move 0.56 in an hour on this liquidity, but reaching +0.84 for the full target is more than a typical hour's realised range — the target is plausible but not free.

**Momentum verdict:** the last two hours are moving **against** the `Lay`'s required out-drift — yet, because `EV` is a linear function of price, the same move **deepened** the lay's edge (`EV` −0.244 → −0.28). **The two readings disagree, and the trade follows `EV_now` — the `Lay` at target 28.** The trend is reported, never traded on.

**Rank 1 — Stylish Recruit (reported, not traded): 2 buckets**, older `14:58:17+02:00` (close 4.2), latest `16:01:24+02:00` (close 4.0, vwap 4.0488, volume 5,119). `EV_now` (3.95) = **+0.05**; `EV_prev` (4.2) = **+0.113**; `EV_vwap` (4.0488) = **+0.073**. Price moved **4.2 → 3.95 (−0.25, −5.95%)**; bucket ranges 0.25 then 0.45. For the `Back` this price would imply, the shortening is *with* the required direction — moot here, since Stylish Recruit is **not** traded.

---

**Strategy Recommendation**

One trade, on the market's favourite **as fixed in Step 1** — Frisky Whiskey (`10583652`), the shortest-priced `Active` runner, never Rank 1 and never re-derived from a later read. `EV_now` = **−0.28** → **`Lay`**, target **28** (integer; raw 28, cap not binding). The trend (against the lay's required out-drift) is reported and does not change the direction or the target, both of which come from `EV_now` alone. No gate, no stand-aside, no second runner. Executing once for this market.Trade executed — `{"status":true}`.

**Summary of the single action taken:** `Trade x profit` opened on **Frisky Whiskey** (`selectionId 10583652`), the market's favourite at 3.00, in the `Lay` direction with profit target **28**, parameters `{"OpenBetPosition.BetType":"Lay","CloseBetPosition.Profit":28}` — and nothing else set (stake, stop-loss and scheduled close inherited from the saved setting).