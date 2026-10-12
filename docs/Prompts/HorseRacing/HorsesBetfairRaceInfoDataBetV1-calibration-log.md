---
title: "HorsesBetfairRaceInfoData Bet V1 — Calibration Log"
aliases: ["HorsesBetfairRaceInfoData Bet V1 Calibration Log", "HorsesBetfairRaceInfoDataBetV1-calibration-log"]
type: research
tags: [analysis, bfexplorer, calibration, ev-analysis, horse-racing, research]
data_contexts: [HorsesNigelsRaceTissue, HorsesNigelsRaceRadar, HorsesBetfairRaceInfoData]
---

# Bet V1 — Calibration Log

**Purpose.** This file is the **evidence record** behind the rules in `HorsesBetfairRaceInfoDataBetV1.md`. The prompt is a system prompt for a live agent and contains only generalised rules; everything below is the race-by-race observation that produced them. Keep this file for learning and audit — do **not** load it into a production agent, because it contains one-off outcomes that are not rules and may bias a fresh model.

**Session 1 — 2026-09-30.** Thirteen markets were read; twelve carried full model coverage and are ranked below. `Sligo` `2m5f Hcap Chs` (`1.263043179`, 15 runners, Class 5, `Good`), `Bellewstown` `1m5f Hcap` (`1.263043279`, 12, Class 5, `Soft`), `Catterick` `1m4f Hcap` (`1.263045664`, 7, Class 4, `Good`), `Kempton` `1m Mdn Stks` (`1.263045578`, 11, Class 3, `A/W` · `Standard`, 8 of 11 unraced), `Bellewstown` `1m5f Mdn` (`1.263043286`, 8, Class 5, `Soft`), `Sligo` `3m2f Hcap Chs` (`1.263043192`, 12, Class 5, `Good`), `Catterick` `5f Hcap` (`1.263045671`, 7, Class 6, `Standard`), `Kempton` `1m Mdn Stks` (`1.263045584`, 10), `Kempton` `1m Hcap` (`1.263045590`, 11, Class 6), `Kempton` `1m Hcap` (`1.263045597`, 13, Class 4), `Kempton` `6f Cond Stks` (`1.263045603`, 6, Class 2), `Kempton` `1m3f Hcap` (`1.263045610`, 10, Class 4), `Kempton` `6f Hcap` (`1.263045617`, 11, Class 6).

## Timing — `Open` does not mean pre-off

Every market in the session reported `status: "Open"` when read. **Four were already past their off time** (offs 16:00, 16:25, 16:30 and 16:35Z, all read afterwards), so their prices were in-play prices compared against a pre-off model probability, and one of them had a bet placed on it. Two later markets were read with only **7 seconds** and **2.15 minutes** of margin. Only the runs with a stated clock margin were genuinely pre-off.

## Tissue book — the sum varies in both directions

| Race | Runners with a tissue | Σ `tissue_prob` | Effect of de-vigging |
| :--- | ---: | ---: | :--- |
| Sligo 2m5f | 14 of 15 | 122.88% | reduces every probability |
| Bellewstown 1m5f Hcap | 11 of 12 | 91.69% | increases every probability |
| Catterick 1m4f | 7 of 7 | 94.34% | increases every probability |
| Kempton (maiden) | 11 of 11 | 99.24% | increases every probability |
| Bellewstown 1m5f Mdn | 5 of 8 | 66.98% | increases — but depressed by three missing tissues |
| Sligo 3m2f | 12 of 12 | 125.61% | reduces every probability |

De-vigging moved Sligo 2m5f's model favourite `15.59%` → `12.69%` and pulled Sligo 3m2f's `18.43%` → `14.67%`; at the other four it moved the model's favourites **up**. The Bellewstown maiden's low sum is **not comparable** to the others: three of its eight runners had no tissue at all, so the missing data depresses the sum. Compare sums only across fields with full coverage.

## Coverage and data quality

- **Coverage is not guaranteed.** One runner returned `horseDetail` only (no tissue, no radar) at Sligo, one at Bellewstown (carrying only 3 past races against 6 for the rest), three of eight at Bellewstown 1m5f Mdn, and one at Kempton 6f Cond Stks.
- **`draw` is race-type dependent.** Inert (`50` / `"none"`) for all 15 at Sligo (jumps); live at `high` confidence for 11 of 12 at Bellewstown (34.3–69.3), all 7 at Catterick (55.1–66.5), all 11 at Kempton (44.2–58.7) and all 11 at Kempton 6f Hcap (42.4–61.1).
- **`going` can be inert on the all-weather.** Observed `50` / `"none"` on individual runners at Kempton 1m Mdn (Silent City), Kempton 6f Cond Stks (Wise Approach) and three runners at Kempton 1m3f Hcap — while the rest of each field carried real scores.
- **Malformed in-play rows recur.** `inPlayMinPrice` `1001` against `inPlayMaxPrice` `1` appeared repeatedly: twice at Bellewstown 1m5f Hcap, twice at Bellewstown 1m5f Mdn, once at Sligo 3m2f, once at Sligo 2m5f, once at Catterick 5f, once at Kempton 6f Cond Stks, once at Kempton 6f Hcap, once at Kempton Mdn — **nine across seven races**.
- **The in-play pair encodes the result — provider-confirmed at session close.** On a past run, `inPlayMinPrice == 1.01` means the horse **won** that race (the price it traded at passing the post) and `inPlayMaxPrice == 1000.0` means it **did not win**; a winning run instead carries a real maximum — the highest price it traded at during the race — and `0.0` / `0.0` on both means **no in-play prices were recorded**. **This retires the earlier reading of `1000` as "traded at the maximum / unbackable in running"**: on a past run it is an ordinary result flag, present on most runs in most records, and it must not be discarded as a ceiling artefact. It also settles the nine rows above, which stay corrupt under the corrected encoding — a winning run's minimum is `1.01`, so an `inPlayMaxPrice` of `1` cannot occur. Session 1 was analysed before this was known, which is why its horse notes read the `1.01` values as contention rather than as wins.
- **A fully zeroed record is positions-only.** `Shimmy Jimmy` (Kempton 1m Hcap) returned six past races with `price: 0`, `inPlayMaxPrice: 0`, `inPlayMinPrice: 0` and `date` at `00:00:00` — price and in-play history void, positions intact (13/14, 3/7, 4/4, 4/8, 4/5, 5/7). The tissue still rated him 2nd best in the field (composite 58.8).
- **The market price can be the ceiling.** At Bellewstown 1m5f Mdn two runners were quoted at exactly `1000` with only `£70` and `£71` matched against `£80,000+` on the favourite; their de-vigged `0.10%` shares are artefacts, and against a model figure of a few percent they manufacture `EV` of `+19.00` and `+27.00`.
- **A `1000` market quote cannot be laid either** — the same card's ceiling runners are neither backable nor layable at a meaningful price.

## `bias_flag`

One or two runners per race carried it: `"agree"` (Bellewstown), `"going"` / `"draw"` (Catterick), `"going"` / `"draw"` (Kempton), `"draw"` (Kempton 1m), `"going"` (Sligo 3m2f), `"draw"` (Kempton 1m3f), `"going"` (Kempton 6f Hcap). In the first four observations the named component *was* that runner's field-best; the fifth broke it — an unraced Kempton runner carried `"going"` while its own `going` component was `50` / `"none"`. A three-way and a five-way tie on `going` `86.0` later showed the same thing. **The flag does not reliably restate the component scores.**

## Unraced-heavy cards

At the Kempton 2yo maiden **8 of 11 runners had `pastRaces: []`** — genuinely unraced. For those, `form`, `going`, `course_dist`, `fitness` and `dist_suit` all returned `50` / `"none"` and `career` a flat `55`, so `composite` reduced to **trainer + jockey + draw**. Only 3 of 11 had any form, the pace map was unavailable (9 of 11 `Unknown`), and the `composite`→probability map was the steepest seen: `63.0 → 27.09%` against `56.1 → 11.79%`.

## The market's favourite, and how the model priced it

| Card | Market's favourite | Market figure | Model's figure | Rank 1 EV | Outcome of Rank 1 |
| :--- | :--- | ---: | ---: | ---: | :--- |
| Sligo 2m5f | Wing Back | 36.08% | 4.40% | +1.34 | lost |
| Bellewstown 1m5f Hcap | Scorchers Son | 26.93% | 5.90% | +0.75 | lost |
| Catterick 1m4f | Wardlaw | 47.59% | 34.00% | −0.29 | lost |
| Kempton Mdn | Encounter | 26.82% | 24.00% | −0.11 | lost |
| Bellewstown Mdn | Solness | 54.86% | 42.00% | −0.25 | **won** |
| Sligo 3m2f | Western Model | 22.95% | 20.00% | −0.08 | lost |
| Catterick 5f | Albegone | 29.61% | 28.50% | −0.05 | lost |
| Kempton 1m (17:30) | Hove Ranger | 25.68% | 21.00% | −0.19 | **won** |
| Kempton 1m (18:00) | Venetian Lion | 52.86% | 45.00% | −0.16 | lost |
| Kempton 6f Cond Stks | Kind Of Blue | 42.30% | 35.00% | −0.17 | **won** |
| Kempton 1m3f | Cape Fear | 24.74% | 25.00% | 0.00 → +0.10 | lost |
| Kempton 6f Hcap | Big Bard | 33.02% | 9.00% | −0.73 | lost |

**In every card the model priced the market's favourite below the market** — the mechanical consequence of placing more probability in the tail. The head/tail flag fired twice at the end of the session (**1.78x**, then **1.62x**).

## The tail, and why the divergence cap exists

Every large tail over-rating lost; the one that paid was modest:

| Card | Runner | Note | Model vs market | Result |
| :--- | :--- | :--- | ---: | :--- |
| Sligo 2m5f | Something | market's longest, model liked it | 13.00% vs 1.82% (**7.1x**) | lost |
| Kempton 1m3f | My Dad Tom | market's longest | 4.00% vs 0.99% (**4.0x**) | lost |
| Kempton 6f Hcap | Southbank | mid-tail | 10.50% vs 3.97% (2.6x) | lost |
| Bellewstown 1m5f Hcap | **Arch Enemy** | mid-tail, model liked it | 15.20% vs 8.30% (**1.8x**) | **won at 11.50** |

That contrast — 4x–7x losing, 1.8x paying — is the basis of the per-runner divergence cap in the prompt. (The counter-case, a large *under*-rating on a short price, is the Jojo Rabbit lay in the gate records below: `0.44x`, and the horse won.)

## Gate records

- **The back gate traded on three cards:** Bellewstown 1m5f (Arch Enemy `+0.75` at `11.50`, **won**), Kempton 1m3f (Cape Fear `+0.10` at `4.40`, lost), Kempton 6f (Rovinia `+0.29` / `+0.56` at `7.60` / `9.20`, lost). Sligo 2m5f qualified at `+1.34` but its market had closed.
- **Backing every Rank 1 at the quoted price would have returned about `+95%`** — but remove the single `11.50` winner and the remainder loses about `11%`.
- **Rank 1 won 4 of 12** (Arch Enemy, Solness, Hove Ranger, Kind Of Blue) and was inside the top 3 in 7 of 12. Two late winners were ignored entirely: Kempton 1m3f went to **Time Thief** (model Rank 3, tissue 18.04% against a market 8.18%) and Kempton 6f Hcap to **King Cherry** (model Rank **9 of 11**, `form` 24.4, `going` 20.0).
- **The lay candidates and their outcomes:** Jojo Rabbit `2.98` (**lost** — the horse won), Open To Offers `36.0` (**won**), Gilet `15.5` (**won**), Wise Approach `3.30` (**won**), Tiernan `4.40` (**won**). Four winners against one loser: `+€8.00` against `−€3.96`.
- **Session total ≈ `+€21` gross before commission on one bet per market, `€21` of it the single `11.50` back.** Strip that bet and the remaining seven trades net to about **zero**.
- Two runners with no model data or thin data were notable for opposite reasons: **Elan Dor** won Kempton 1m (18:00Z) at `32.0` with **no tissue and no radar at all**; and **King Cherry** won having never been placed in six runs.

## Field notes

- **The position legend is not closed.** `B/15` appeared at Sligo 3m2f (Pourquoi Poi) alongside `F`, `P`, `U`, `co`; codes are case-sensitive as returned and the meanings beyond the core set are inferred.
- **`pastRaces[]` carries no racecourse.** A past run gives a date, distance, in-play extremes, position and starting price — never the track, so "won over 5680 yards" cannot be attributed to today's course. The only course-form evidence is the blended `Course/Dist` component.
- **`roll_fin_rel` ranges:** Sligo `0.245`–`0.754`, Catterick `0.216`–`0.806`. In both, the best `roll_fin_won` and lowest `roll_fin_std` sat at the low end and the worst-form runner at the high extreme — consistent with "lower = closer to the front", still undocumented. **Confirmed a third time at Bellewstown 1m Hcap: the component `form` score and `roll_fin_rel` ranked in perfect inverse order, 6 of 6** (Cromane 46.8 / 0.438 down to Dropping Dimes 9.0 / 0.872).

## Session 2 — 2026-10-01, Bellewstown `1m Hcap` (`1.263085336`)

**Gate.** Read at `09:33:30Z`, off `12:00:00Z` — margin **+146.5 min**; refreshed at `09:36:50Z`, margin **+143.2 min**. `status: "Open"`, all 6 selections `Active`. *(Recorded with the prompt as it stood that morning: V1 still carried the clock gate then and has since removed every time check — see the design change at the foot of this file.)*

**Coverage: complete — the first market in the project with 100% context coverage.** All six runners returned `nigelTissuePricer` + `nigelRaceRadar` + `horseDetail`. Nothing was priced at the ceiling, so both probability columns shared one denominator; no corrupt in-play pair, so no run's prices had to be discarded.

**The market book changed sign inside three minutes.** Snapshot 1 (09:33) summed to **98.91%** — an *under*-round back book, so the de-vig *raised* every probability. Snapshot 2 (09:36) summed to **100.11%** — over-round, so the de-vig *lowered* every probability. The flip came from a single runner: Sister Nancy `6.4 → 5.8` (−9.4%). No runner had moved ~20%, so the model column stood and only the market column and the `EV`/`Edge` figures were recomputed on the fresh read. **The direction of the de-vig is a property of the moment, not of the race.**

**Every runner was `Hold-up` (6 of 6).** A uniform field has no pace contrast, so no race-shape read was used — the same conclusion as the "most of the field `Unknown`" case, reached from the opposite direction.

**The field.** Six 2yo fillies, Class 5, `Soft`, weights `142 → 119` lb, `totalMatched` £7,860. Race data gave `7f 195y` (1735y) against the market name `1m` (1760y) — a 25-yard disagreement with no impact on any trip-tolerance call.

**Data-shape findings (all folded into the prompt).** 7 of the field's 20 past runs had `inPlayMinPrice == inPlayMaxPrice` (`30`/`30`, `190`/`190`, `350`/`350`, `1000`/`1000` ×3) — no in-play movement recorded, and not a price the horse traded through. **Beaten runs are not reliably `1000`:** near-ceiling `980`/`940` appeared, as did values identical to the minimum. `pastRaces[].price` hit the ceiling (`1000`) twice, which is "no market that day", not a 1000/1 SP. Exactly one winning run in the field, and its `inPlayMinPrice` was exactly `1.01` — the strict result test held.

**Vector vs market (snapshot 2).**

| Horse | Model % | Price | Market % | EV | Divergence |
| :--- | ---: | ---: | ---: | ---: | ---: |
| Cromane | 37.00 | 2.76 | 36.19 | +0.021 | 1.02x |
| Violet Be Humble | 15.50 | 7.60 | 13.14 | +0.178 | 1.18x |
| Ippy | 15.00 | 9.60 | 10.41 | +0.440 | 1.44x |
| Sister Nancy | 14.50 | 5.80 | 17.22 | −0.159 | 0.84x |
| Green Sparkle | 13.50 | 5.40 | 18.50 | **−0.271** | 0.73x |
| Dropping Dimes | 4.50 | 22.00 | 4.54 | −0.010 | 0.99x |

- **Head/tail flag fired at 1.30x** — tail (`≥ 8.0`) mine 19.50% against the market's 14.95%. But the **head was level (1.02x)**, so the usual inference ("your favourite figure is mechanically depressed") did **not** apply here: with both columns totalling 100%, the extra tail weight came out of the **middle** — Sister Nancy `0.84x` and Green Sparkle `0.73x`. *A fat tail does not always mean a thin head; check which it is before writing the caveat.*
- **Multiple-edge count: 4 of 6 in snapshot 1, 3 of 6 in snapshot 2** — above the one-third threshold either way, so the flag fired in both reads.
- Rank 1 break-even `2.84` against a price of `2.76` — **no Back**.

**Executed:** `Lay 2 Euro` on **Green Sparkle** (`100759599`) at `5.40`; liability `2 × 4.40` = **€8.80**. Result: `{"status":true}`.

**Uncertainty carried on that trade — recorded, not used as a veto.** Green Sparkle was the market's **2nd favourite** on the 2nd-largest matched volume (£1,084), and its price had collapsed from past SPs of `100`, `87.51` and `29` to `5.40` — a move the retrievable form (`10/12`, `10/14`, `9/9`; `form` component 11.4) cannot explain. Both the tissue (`14.29%`) and my vector (`13.50%`) sat below the market's `18.50%`. Its only positive was the field-best `trainer` score (`39.7`). This is the reconciliation rules' "highest-uncertainty case" in its purest form: the lay cleared the mechanical gate, and the counter-case is a blind spot I cannot audit.

**Outcome: pending** — off at `12:00:00Z`.

## Design change — the pre-off gate is removed (2026-10-01)

**Decision (user).** V1 now performs **no time check of any kind.** The LLM that will run this prompt **has no access to system time**, so the pre-off gate, the "record the clock source and value" duty, the pre-execution off-time re-test and the entire pre-execution price-refresh bullet have been removed. The instruction was to remove "any other task to check time"; asked specifically whether the price-refresh half of that bullet should survive — it is a price task, not a time task — the answer was **remove the whole bullet, refresh included**.

**What the prompt says instead.** One explicit prohibition: there is no clock, the model must not obtain, estimate, infer or state one, and **`status` is the only market-state test available** — anything other than `"Open"` means stop. Fabricating a time, a clock source or an off-time margin is now a **guardrail failure**, listed as such rather than rewarded as diligence (the old text invited the opposite by telling a clock-less model to "use the OS clock").

**The risk this carries, on the record.** The gate was not decoration. In session 1, **four of the thirteen markets were read while `status: "Open"` and already past their off time, and one of them had a bet placed on it**; two later markets were read with only 7 seconds and 2.15 minutes of margin. `Open` genuinely does not mean pre-off, because Bfexplorer holds a market `Open` through its in-play period. **With no clock, nothing in the prompt can detect that case.** The responsibility therefore moves to the **host application**, which must either hand the agent only markets that are still pre-off or supply the time itself as part of the payload. This is written down so the loss of coverage is a known, accepted trade-off rather than an oversight — and so a future session can revisit it if a bet is ever found to have landed in-running.

**Also fixed while in the file — dangling cross-references** left by the restructure that removed the old "Confirm Data Availability" step and the "Optional contexts" section: `Step 3` → `Step 2`, `Step 6` → `Step 5`, `section 6` → `section 5`.

## Session 3 — 2026-10-01, Newcastle `1m Mdn Stks` (`1.263085179`)

**First run under the post-clock-change prompt, and the new contract held.** No time was obtained, estimated or stated, and no off-time margin was computed. The market-state test was `status: "Open"` with all 5 selections `Active` and none settled. (Off `2026-10-01T16:30:00Z` is noted here only because this log is not the prompt — nothing in the run depended on it, and the prompt now forbids deriving one.)

**Coverage: complete again** — all five runners returned tissue + `horseDetail` + radar; nothing was priced at the ceiling, so both probability columns shared one denominator; no malformed in-play pair, so no run's prices were discarded. **No runner in the field has ever won a race**: none of the seven past runs carries `inPlayMinPrice == 1.01`, which cross-checks cleanly against every position string being a defeat.

**Race shape does not exist.** Four of five runners are `style_label: "Unknown"` with **no `roll_fin_*` keys at all** (fewer than 2 runs); only King Kenny, with 2 runs, has radar statistics. An *unclassified* field has no pace map for the same reason an all-`Hold-up` field has none — session 2's conclusion and this one are the same finding reached from opposite directions.

**The field.** Newcastle `1m Mdn Stks` (A/W · `Standard` · Class 3), 5 runners, **all 2yo colts at 133 lb** (level weights), `totalMatched` **£72,116** — the largest market the project has run. Race data `1m 5y` (1765y) against the market name `1m` (1760y): a 5-yard disagreement, no trip-tolerance impact.

**Data-shape finding: `form` returns `50` / `"none"` below 2 runs.** Four of the five runners have ≤1 past run and **all four** return `form 50, confidence "none"`; the only runner with 2 runs returns `form 17.5, confidence "high"`. This matches the radar's documented "minimum 2" rule and means **the tissue carried no form read on 80% of this field** — its probabilities rest on trainer / jockey / draw plus neutral defaults, which is why its spread is so flat (23.99 – 28.44 across the top three).

**The tissue is compressed again, and the unraced runner is the extreme case.** Σ `tissue_prob` = **112.87%** (over-round → the de-vig lowers everything). Post-de-vig its order is NoG `28.44` > SoS `23.99` > **Ring Of Music `23.84` (unraced)** > STH `16.52` > KK `7.21`. The unraced colt places **third** on a composite built almost entirely from trainer + jockey + draw defaults — **2.99x the market's price for it**, over the 2.5x per-runner cap, so it was correctly *not* followed.

**Vector vs market.**

| Horse | Model % | Price | Market % | Tissue % | EV | Divergence |
| :--- | ---: | ---: | ---: | ---: | ---: | ---: |
| Saint Of Seville | 44.00 | 1.99 | 50.04 | 23.99 | −0.124 | 0.88x |
| Night Of Gold | 38.00 | 3.05 | 32.65 | 28.44 | +0.159 | 1.16x |
| Ring Of Music | 8.00 | 12.50 | 7.97 | 23.84 | 0.000 | 1.00x |
| Sir Terry Hadley | 6.00 | 16.00 | 6.22 | 16.52 | −0.040 | 0.96x |
| King Kenny | 4.00 | 32.00 | 3.11 | 7.21 | +0.280 | 1.29x |

- **The head/tail flag did NOT fire — the first time in the project.** Tail (`≥ 8.0`) `18.00` against the market's `17.30` = **1.04x**, and the favourite at **0.88x**. That matters: with the flag clear, a **negative Rank 1 `EV` is not mechanical** and the usual "your favourite figure is depressed by your own tail weighting" caveat must *not* be written. The flag's absence is itself a finding.
- **Multiple-edge count 2 of 5 (40%)** — above the one-third threshold, so that flag did fire. Both positives sit outside the credible mid-priced cluster: Night Of Gold at `3.05` (below the vendor's `8`–`50` zone) and King Kenny at `32.00` (inside it).
- Rank 1 break-even **`2.3864`** against a price of **`1.99`** — the market was ~20% short of the gate.

**Finding: the spread-sanity anchors stopped at 6 runners.** This is the project's first sub-6-runner field and the table had no row for it, so a band had to be applied (`38–55%`; the market's `50.04%` and my `44.00%` both sit inside). **Resolved the same day: a `≤5 runners → 35–60%` row was added to the prompt**, on the reasoning that in a small field the separation between the top two matters more than the headcount — so the band is deliberately wide enough to hold both a dominant-favourite and a four-contender five-runner race.

**Executed:** `Lay 2 Euro` on **Saint Of Seville** (`102817100`) at `1.99`; liability `2 × 0.99` = **€1.98**. Result: `{"status":true}`.

**Uncertainty carried — recorded, not used as a veto.** This is the **market's favourite**, carrying the **largest matched volume in the race** (£44,541 = 62% of the market), on a runner whose retrievable form *contradicts* it: a **5th-of-8 defeat as the 2.69 favourite over the same 1m trip**, with a `dist_suit` score of `41.5` (`low` — the trip is not its ideal) and a `jockey` score of `29.1` (4th of 5). Supporting the lay: the tissue independently rates it **`23.99%`** — `0.48x` the market. Against it: its price had *shortened* from SP `2.69` to `1.99`, and the one documented lay loss of this project (Jojo Rabbit, `0.44x`, market favourite) was this same shape — though at `0.88x` this is a considerably milder divergence.

**Decision-relevant diligence — the candidate turned on one call.** Saint Of Seville is the lay candidate only because **Sir Terry Hadley was held at the market** (`6.00%` against the market's `6.22%`) rather than pushed lower. The market has shortened STH from SP `60` to `16.00` — a 3.75x move the retrievable data cannot explain — so the "pull the figure toward the market where you cannot name a reason" rule applied. At `5.00%` its `EV` would have been `−0.20` and **it** would have been the candidate instead. Recorded because the choice of runner hinged entirely on that estimate.

**Outcome: pending.**

## Session 4 — 2026-10-01, Newcastle `5f Nursery` (`1.263085186`)

**Market-state test.** `status: "Open"`, all 6 selections `Active`, none settled. No clock check — the prompt forbids one — so `status` was the whole test.

**Coverage: one runner had neither model context.** **Blakes Monarch** returned **`horseDetail` only — no `nigelTissuePricer` and no `nigelRaceRadar`**, the first runner in the project to be missing *both*. It was scored from its form lines alone, its uncertainty widened, and — per the appendix's "one such runner won" rule — the gap was **not** treated as a discount. That matters here because this runner is the **last-out winner over today's exact trip** (1100y, `1/8`, SP 2.93, traded `1.01`–`2.54`, 16 days ago) and the market prices it at `6.80`.

**The corrupt in-play row reappeared.** Kentucky Rain's 2nd past race returned the documented `inPlayMaxPrice: 1` / `inPlayMinPrice: 1001` pair — the first occurrence since session 1's nine rows. Its prices were discarded, the finishing position `4/8` kept, and the discard reported.

**A near-ceiling minimum is a read, not missing data.** Not My Type's 2nd past race returned `min 950` against `max 1000`. Not equal, so the `min == max` rule does not apply: the pair means the horse **never traded below 950 in running at all** — it was never backed in running. That is a genuine negative on a runner whose last run was at today's trip, and it was read as one.

**Field.** Newcastle `5f Nursery` (A/W · `Standard` · Class 4), 6 runners, all 2yo, weights `136 → 130` lb (a 6 lb spread — a tight nursery), `totalMatched` £32,922. Race data `5f` (1100y) against the market name `5f Nursery` ✓ no disagreement. **Five of the six have won a race**, and the `1.01` encoding matched `roll_fin_won` field-wide: Not My Type 2 wins → `0.5`; the other five 1 win each → `0.25`/`0.2` ✓. That is the first field-wide cross-check of the encoding against the radar, and it confirms the radar's last-5-runs window (Bill The Bull has 6 runs and one win inside the last 5 = `0.2`).

**`roll_fin_rel` held a fourth time — and this time exactly.** With 5 of 6 runners carrying radar statistics, the `form` component and `roll_fin_rel` ranked in **perfect inverse order, 5 of 5** (Not My Type 66.3 / 0.267 → Bill The Bull 34.1 / 0.599), and `roll_fin_won` agreed at both ends. (Session 3 could not verify the direction at all — only one runner carried the keys.)

**`going` came back `50` / `"none"` for 4 of the 5 tissue holders** on an all-weather card, with the fifth at `35.0` / `low` — the documented inert-going pattern, now seen four times.

**The market's favourite sits below the anchor band.** Six runners means the `6–8 → 30–50%` band, yet the market's favourite came back at **27.92%** and my vector's at **21.00%** — both below it. This is the "closely matched head" case the prompt already permits: four runners sit between `6.00` and `6.80`. Stated, not bent.

**Vector vs market.**

| Horse | Model % | Price | Market % | Tissue % | EV | Divergence |
| :--- | ---: | ---: | ---: | ---: | ---: | ---: |
| Not My Type | 21.00 | 6.20 | 15.99 | 40.44 | **+0.302** | 1.31x |
| Havana Lightning | 20.00 | 3.55 | 27.92 | 25.02 | −0.290 | 0.72x |
| Kentucky Rain | 18.50 | 6.00 | 16.52 | 16.63 | +0.110 | 1.12x |
| Blakes Monarch | 17.00 | 6.80 | 14.58 | **n/a** | +0.156 | 1.17x |
| Peaceful Warrior | 13.00 | 6.20 | 15.99 | 10.38 | −0.194 | 0.81x |
| Bill The Bull | 10.50 | 11.00 | 9.01 | 7.53 | +0.155 | 1.17x |

- The tissue column is **conditional on 5 of 6 runners** (Blakes Monarch has none) and its raw sum is **92.58%**, so the de-vigged tissue figures are against a covered set, not the field.
- **Head/tail clear again at 1.17x.** The flag has now failed to fire twice running, so the negative `EV` on the market's favourite is a direct view rather than a tail artefact.
- **Multiple-edge count 4 of 6 (67%)** — well above a third, and **three of the four positives sit below the vendor's `8`–`50` zone** (`6.20`, `6.00`, `6.80`), which the vendor lists as a common backing mistake. Stated as the signature of an incomplete information set rather than tightened away. In this race the signature is apt: light 2yo form and one runner with no model data at all.
- Rank 1 (Not My Type) `EV` **+0.3020** against a break-even of `5.00` and a price of `6.20`.

**Executed:** `Back 2 Euro` on **Not My Type** (`72820056`) at `6.20`. Result: `{"status":true}`.

**Basis for that Back, and its counter-case.** For: the provider's own model — documented as calculated independently of exchange odds — rates it **`40.44%`** against the market's `15.99%` (2.53x, right at the divergence cap); it holds the field's best `composite` (53.5), `form` (66.3), `trainer` (50.5), `jockey` (39.8), `course_dist` (85.0), `dist_suit` (67.6) and `roll_fin_won` (0.5); it is the only `Leader` in a field of three `Hold-up` and one `Prominent`; it has **two wins, both at 5f**; and it is the freshest runner in the race at 7 days. Against: its last two runs were **5/11** (SP 21.71) and **5/7** (SP 8), and in the latter it never traded below `950` — a horse going backwards since the handicapper raised it for those two wins, which is precisely the story the market's price tells.

**Outcome: pending.**

**Prompt finding — an internal contradiction this run exposed, raised rather than silently reconciled.** The guardrail *"Back a short-priced runner outside the edge zone"* reads as an absolute prohibition, but its own stated reason concerns a short favourite with a **negative** `Edge`, while §3 ties the vendor's caution to the same case and the Execution Rule mandates a Back on a positive-`EV` Rank 1. This run's Rank 1 sat at `6.20` — below the `8`–`50` zone — so the executed trade is one the guardrail literally forbids and the Execution Rule mandates. **Resolved the same day: the user chose to leave both texts as they are**, so this is now a *known, accepted ambiguity* rather than an oversight — the Execution Rule continues to mandate the Back, and the guardrail continues to read as a blanket caution. Re-raising it is unnecessary; cite this entry if the two ever need reconciling.

## Derivation — `HorsesBetfairRaceInfoDataTradeV1.md` (2026-10-01)

**A second prompt was created from `HorsesBetfairRaceInfoDataBetV1.md`**, keeping every data-shape and calibration rule verbatim and replacing the action machinery. It trades **one runner per market — the market's favourite** (the shortest-priced `Active` selection, not Rank 1) — using the `Trade x profit` strategy via `execute_strategy_settings_with_parameters`, setting exactly two parameters:

| Favourite's `EV` | `OpenBetPosition.BetType` | `CloseBetPosition.Profit` |
| :--- | :--- | :--- |
| `> 0` | `Back` | `abs(EV) × 100`, one decimal |
| `≤ 0` | `Lay` | `abs(EV) × 100`, one decimal |

**There is no gate and no stand-aside** — the ±0.05 thresholds, the two-path precedence, the lay-candidate search and the "no bet" outcome are all gone. Every market with a priced favourite produces a trade.

**Verified against the live installation before writing it.** `Trade x profit` exists (id `5`, category `Trading`, template `Place Bet and Close Selection Bet Position`); `CloseBetPosition.ProfitLossType` is already `Percentage` (so `Profit` is a percent, as specified); and the stake with its `StakeType`, the stop and a close scheduled five seconds before the off are inherited from the saved setting, so the model must not pass them.

**Open discrepancy — `OpenBetPosition.StakeType`.** `get_strategy_setting("Trade x profit")` returned `Stake = 100` with `StakeType = "Liability"` on **two reads minutes apart**, from the same saved setting the execution call uses. The user reports **`Percentage`** in the Bfexplorer UI. Unresolved from the agent side. The prompt therefore **does not assert the value at all** — it states only that the stake and its type are inherited and must not be passed, which holds under either. If the UI is authoritative, no further change is needed; if the API is, the prompt is also already correct.

**Why this variant needs its own warning.** BetV1's head/tail check existed to stop a negative Rank 1 `EV` being written up as a finding. Here the **favourite is the traded runner**, and the appendix records the model pricing the market's favourite below the market in **every card seen** — so a negative favourite `EV`, and therefore a `Lay`, is this method's *default* direction rather than a discovery. That is now stated in the prompt (3c, the guardrails and the appendix), because otherwise every Trade V1 run would report a market-finding that is really a property of the method. The same reasoning removed the guardrail that forbade backing short prices outside the vendor's `8`–`50` zone: in this design that row would forbid *every possible trade*, so it now stands as information rather than a veto. *(Note this differs from the BetV1 decision immediately above, where the user chose to leave the equivalent tension in place — there it was one trade among several; here it is the entire design.)*

**No session has yet run under Trade V1**, so its trade machinery is unvalidated while the analysis rules it inherited remain calibrated. The next question for the project is where Trade V1's own outcomes should be recorded — this log is Bet V1's evidence record, and its appendix still points at it for the shared rules.

## Trade V1 — session 5 (first run under the new design), Newcastle `5f Nursery` (`1.263085186`)

Recorded here because no separate Trade V1 log exists yet; if outcomes move elsewhere, this section is the one to migrate.

**A deliberate test case.** This was the **same market, same card** as session 4 two hours earlier — the data context came back **byte-identical** (same tissues, same components, same radar values, same past races), while the market had repriced hard and matched volume had more than doubled, from **£32,922 to £82,772**. So the handicapping vector was *unchanged* and only the market moved: a clean test of the trade machinery with the analysis held constant.

**How the book moved:**

| Horse | Session 4 | Session 5 | Direction |
| :--- | ---: | ---: | :--- |
| Havana Lightning | 3.55 | **3.95** | out |
| Peaceful Warrior | 6.20 | 5.30 | **in** |
| Not My Type | 6.20 | **5.10** | **in** |
| Kentucky Rain | 6.00 | 6.20 | out |
| Blakes Monarch | 6.80 | 7.80 | out |
| Bill The Bull | 11.00 | 12.50 | out |

Not My Type — the runner session 4 backed at `6.20` — shortened to `5.10`, so **the market moved toward that view**. The favourite changed from Havana Lightning `3.55` to Havana Lightning `3.95` (still the favourite, but drifting).

**The favourite and the trade.** Favourite = **Havana Lightning** (`97400822`) at `3.95`; my figure `20.00%` against a de-vigged market `25.13%` (**0.80x**); `EV = −0.2100` → **not positive** → **`Lay`**, profit target **`21.0`**. Break-even `5.00` against a price of `3.95`.

**Executed:** `execute_strategy_settings_with_parameters(strategyName="Trade x profit", marketId="1.263085186", selectionId="97400822", parameters="{\"OpenBetPosition.BetType\":\"Lay\",\"CloseBetPosition.Profit\":21.0}")` → `{"status":true}`.

**The Rank-1-is-not-the-favourite rule earned its keep immediately.** My vector's Rank 1 was **Not My Type at `5.10`** — the market's *third* favourite — so under the old BetV1 rule this market would have produced a `Back 2 Euro` on a runner the new design never touches. The prompt's Step 1 warning and the `0.72x`-style divergences that follow are not theoretical.

**Both calibration flags fired, and neither blocks anything.**

- **Head/tail 1.32x** (tail `≥ 8.0`: mine `10.50%` vs the market's `7.94%`) — so per 3c the `Lay` direction **and** the `21.0` target inherit a mechanically depressed favourite figure. The appendix's favourite-bias row predicts exactly this, and it is now reported as the method's default rather than as evidence.
- **Multiple-edge count 4 of 6 (67%)** — positives were Not My Type, Kentucky Rain, Blakes Monarch and Bill The Bull.
- Per-runner divergences ranged `0.69x`–`1.34x`, all inside the `0.4x`–`2.5x` cap, so no figure needed pulling toward the market.

**No readback available.** The surface has no open-position tool — only `get_all_bet_results`, which reports **closed** markets — so `{"status":true}` is the whole of what can be confirmed about this trade until the market settles. Worth knowing before trusting a "placed" claim: **the prompt can confirm the call was accepted, never that the position exists as intended.**

**Outcome: pending** — and it may be observable through `get_all_bet_results` once the market closes, which is also how the three earlier pending outcomes (sessions 2–4) can finally be read.

## Trade V1 — the `30.0` profit-target cap and the calibration behind it (2026-10-01)

**Question raised.** Whether `|EV|` on the favourite can be judged "too big", after session 5 traded a `Lay` on a favourite with `EV = −0.21` and a `21.0` target.

**The uncomfortable part: the favourite's `EV` sign carries no information at all.** This method prices the market's favourite *below* the market by construction, so a negative favourite `EV` is guaranteed and its sign is not evidence. Only the magnitude can inform, and only against a baseline.

**The baseline, consolidated in one place** — the model's figure for the market's de-vigged favourite ÷ the market's figure:

| Session | Card | Model % | Market % | Divergence |
| :--- | :--- | ---: | ---: | ---: |
| 1 — **pre-calibration** | 12 cards | — | — | **mean `0.694x`, range `0.12x`–`1.01x`** |
| 2 | Bellewstown 1m Hcap | 37.00 | 36.90 | 1.00x |
| 3 | Newcastle 1m Mdn Stks | 44.00 | 50.05 | 0.88x |
| 4 | Newcastle 5f Nursery | 20.00 | 27.92 | 0.72x |
| 5 | Newcastle 5f Nursery | 20.00 | 25.13 | 0.80x |

Post-calibration (sessions 2–5, **n=4**): **mean `0.849x`, range `0.72x`–`1.00x`.** Session 5's `0.80x` sits **inside** that band, so on the record `−0.21` is *normal*, not extreme. Session 1 shows what an uncalibrated version looked like — `0.12x` to `1.01x` — and those extremes are exactly what the head/tail check and the divergence cap were built to prevent. **A "too big" threshold cannot be estimated from four observations**, which is why the prompt takes a cap *derived from that band* rather than a threshold.

**The change.** `CloseBetPosition.Profit = min(abs(EV) × 100, 30.0)`. **The cap is not about direction** — that still comes from the sign of `EV` alone. It is about the **target**, because the target is coupled to the `EV` while measuring something different: `abs(EV) × 100` is how *wrong* the price is claimed to be, whereas the target is how far the price must **travel** before the off to be closed in profit. The strategy closes the position seconds before the off, so a target the price cannot reach does not pay — the position exits at the stop or at the off, and **the spread is lost**. `30` is derived, not picked: on a near-100% book a divergence of `0.72x` gives `|EV| ≈ 0.28`, so `30` is the largest target the calibrated band can produce. Anything beyond it is the divergence cap's `0.4x` floor talking — which would permit `|EV| ≈ 0.60`, a target of `60`. The prompt now requires the raw figure to be reported whenever the cap binds.

**Worked on session 5:** a `21.0` target means the favourite must drift from `3.95` to about `4.57` (if the 21% is of liability) or `4.78` (if of the backer's stake — the basis is still unverified). **This card moved `3%`–`18%` between reads over roughly two hours, median `15%`**, so that target sat at the top of what the card itself demonstrated. Note the cap would *not* have changed session 5 (`21.0 < 30.0`) — which is the point: it bounds the failure mode, not the typical case.

**The identity that makes the cap meaningful.** `EV = price / fairPrice − 1`, so `|EV|` **is** the price error as a fraction of the fair price. It is not a probability advantage, and it is not the same figure as "the price is X% short" — that reciprocal framing gives `26.6%` for the same gap at `3.95 / 5.00`. Both describe one disagreement.

**Offered but not adopted.** A three-way head check adding the tissue's independent view, and shrinking the favourite's probability toward the market before computing `EV`. The user chose the cap only. The three-way pattern was reported at the time and is worth keeping on the record: in **sessions 4 and 5 the tissue and the market agreed with each other** (`25.02` vs `27.92`, then `25.02` vs `25.13`) and **the model was the outlier by ~5 points**, whereas in sessions 2 and 3 the tissue was itself the outlier (`25.09` vs `36.90`; `23.99` vs `50.05`). It is a cheap cross-check the prompt still does not make — and per the `form 50/"none"` rule it only counts as a witness when the tissue holds real form data on the field, which session 3's did not and sessions 4–5's did.

## Trade V1 — session 6, Newcastle `1m Hcap` (`1.263085204`)

**Coverage: two runners missing everything, for the first time.** **Sailors Return** (`35472040`) and **Fickle Mcselfish** (`91496940`) each returned **`horseDetail` only — no `nigelTissuePricer`, no `nigelRaceRadar`**. Both were scored from form lines alone with uncertainty widened; Sailors Return matters because it is one of the two **last-out winners over today's trip** in the field. The tissue column is therefore conditional on **8 of 10** runners, and its raw sum was **`72.39%`** — under-round, so the de-vig raised every figure.

**The corrupt row appeared again** — Fickle Mcselfish's oldest past race returned `inPlayMaxPrice: 1` / `inPlayMinPrice: 1001`. Prices discarded, the `7/8` position kept, and reported.

**Field.** Newcastle `1m Hcap` (A/W · `Standard` · **Class 6**), 10 runners, 3yo, weights `120 → 135` lb, `totalMatched` **£57,882**. Race data `1m 5y` (1765y); the market name agrees. Every runner had a run within 29 days except **Fickle Mcselfish at 168 days** — over the 120-day threshold, flagged against `fitness` and one reason its form-only assessment stayed low.

**A field-wide tissue signal worth naming.** Four runners carry **`going` 20.0** — a *bad* ground score — on an all-weather card: the favourite, Baldosa, Maynora and Truly Special. Against them, **Dalamara carries the field's best `going` (75.0), `course_dist` (80.4) and `dist_suit` (75.7)**, which is why it is rated second despite being only the market's third favourite. The favourite's own negatives are both of the same kind: `going` 20.0 and **`course_dist` 5.0 (the field's worst)** despite the best `composite` (48.9) and by far the best `jockey` (55.8) — and it has **0 wins from 6**, with the 2nd-worst `career`.

**The trade.** Favourite = **Alkumatic Jo Jo** (`87034912`) at `3.55`; mine `24.00%` against the market's de-vigged `28.04%` (**0.856x**); `EV = −0.1480` → **`Lay`**; raw target `14.8`, **cap not binding** (`14.8 < 30.0`).

**Executed:** `execute_strategy_settings_with_parameters(strategyName="Trade x profit", marketId="1.263085204", selectionId="87034912", parameters="{\"OpenBetPosition.BetType\":\"Lay\",\"CloseBetPosition.Profit\":14.8}")` → `{"status":true}`.

**Three notable firsts on this card.**

1. **Rank 1 was the favourite**, for the first time under Trade V1 — `not` the usual case, and a reminder that the `24.00 / 18.50` split between my top two here did not change which runner gets traded.
2. **The head/tail flag did not fire (1.10x)**, so the `Lay` is a **direct view** rather than the method's compression — the `going` 20.0 / `course_dist` 5.0 / 0-from-6 case, not a tail artefact. That is the first Trade V1 run where the direction does not inherit the distortion, and it is reported as such.
3. **The three-way check now goes the other way.** Mine `24.00`, tissue `24.48`, market `28.04` — **the tissue agrees with the model and the market is the outlier**. In sessions 4 and 5 the tissue sided with the market against the model; here it sides with the model against the market. The cross-check is not a rubber stamp; it is a vote, and this is the first card where it votes for the trade. (The tissue is a valid witness here: `form` was `high` confidence for all eight runners that carry one.)

**The multiple-edge flag fired — 4 of 10 (40%) — and the positives are all longshots:** Suntino Lady `+1.40` at `80.0`, Truly Special `+1.38` at `34.0`, Maynora `+1.00` at `25.0`, Dalamara `+0.15` at `6.2`. Three of the four sit at `25.0` or longer, i.e. **not** in the vendor's `8`–`50` mid-price zone but *beyond* it, which is exactly the "maths inflates a small gap" case the appendix warns about. Per-runner divergences stayed inside the cap (`0.74x`–`2.41x`), so the three big numbers are large **prices** rather than out-of-band probabilities.

**`roll_fin_rel` could not be verified this time.** Across the eight runners carrying radar statistics, the `form` component and `roll_fin_rel` did **not** rank in inverse order (Truly Special has the best `form` at `50.8` but a mid `0.446`; Maynora has the lowest `roll_fin_rel` at `0.317` with the 3rd-best `form`). Per the prompt it was therefore treated as a **tie-breaker only** and not used as an input — the first card where the reading was ambiguous rather than confirmed.

**Favourite-divergence series (extends the table above):** `0.856x`, bringing the post-calibration set to **`1.00, 0.88, 0.72, 0.80, 0.856`** — mean **`0.851x`**, range still **`0.72x`–`1.00x`** (n=5). The band is holding, and the `30.0` cap remains unbound in both Trade V1 runs.

**Outcome: pending.**

## Trade V1 — session 7, Newcastle `6f Hcap` (`1.263085221`)

**Coverage: complete for the first time in three runs** — all nine runners returned tissue, radar and `horseDetail`. No corrupt in-play row, no ceiling price, no missing component block. So this card is a clean read of the method with no data caveats attached.

**A new data-shape observation: the selections array is not price-ordered.** `get_active_market()` returned `10.5` *before* `9.6`, so the favourite cannot be assumed to be the first row — it must be found by comparing every `price`. Here the first row happened to be the shortest (`4.10`), so nothing turned on it, but the rule in Step 1 is doing real work rather than stating the obvious.

**The field.** Newcastle `6f Hcap` (A/W · `Standard` · Class 4), 9 runners, 3–5yo, weights `124 → 139` lb, `totalMatched` **£64,925**. Race data `6f` (1320y) against the market name `6f Hcap` — agreement. Every runner has run within 26 days except two: **Angel Love at 62 days** and **D Flawless at 84 days**, both past the 60-day threshold and both flagged against `fitness`.

**The traded runner, and why the 2nd favourite is the one I rate highest.** Vanilla Skies is the market's favourite at `4.10` and my **Rank 2**. **Lady Kodiac** is my Rank 1 at `4.50`, and the case is not marginal: Kodiac holds the field's **best `rating` by 28 points (81.6 against 53.1 next-best)**, the best `going` (86.0), the best `course_dist` (85.0) and the best `dist_suit` (83.6) — and **won last time out over today's exact 6f trip, 11 days ago at SP 3.25**, with two further 6f seconds. Vanilla Skies has the better `composite` (56.5), `form` (64.4), `trainer` (60.7) and `career` (65) — a **record-based** profile — but a far lower ability `rating` (51.0), `going` `50`/`"none"`, and **every one of its four runs at ~7f (1533–1540y)**, so today is a trip *drop* to a distance it has never run over, against a `dist_suit` score of only `low` confidence. That is the whole of the disagreement, and it is a real one.

**The trade.** Favourite = **Vanilla Skies** (`99962991`) at `4.10`; mine `22.00%` against the market's de-vigged `24.15%` (**0.91x**); `EV = −0.0980` → **`Lay`**; raw target `9.8`, **cap not binding**.

**Executed:** `execute_strategy_settings_with_parameters(strategyName="Trade x profit", marketId="1.263085221", selectionId="99962991", parameters="{\"OpenBetPosition.BetType\":\"Lay\",\"CloseBetPosition.Profit\":9.8}")` → `{"status":true}`.

**The design's structural blind spot, now seen three times.** My Rank 1 — Lady Kodiac — carries an `EV` of **+0.0800**, i.e. it *would* qualify under the BetV1 back rule, and the Trade V1 design cannot touch it because it is not the favourite. The prompt trades one runner by construction, so this is the rule working as specified rather than a fault; but the loop has now passed up its own best value on sessions 4, 6 and 7, and that is worth having on the record when the design is next reviewed.

**Both calibration flags, and they disagree about this trade.**

- **Head/tail: clear at 1.09x** (tail `≥ 8.0`, 6 runners: mine `42.00%` vs the market's `38.37%`). So the `Lay` is a **direct view**, not the method's compression — the second Trade V1 run where the direction does not inherit a distortion.
- **Multiple-edge: 4 of 9 (44%)** — Siouxpersonic `+1.20` at `55.0`, Society Kiss `+0.65` at `30.0`, D Flawless `+0.49` at `13.5`, Lady Kodiac `+0.08` at `4.5`. Two sit inside the vendor's `8`–`50` zone and one above it, which is a better distribution than session 6's all-longshots set.
- Per-runner divergences `0.69x`–`2.22x`, all inside the cap.

**The three-way check splits three ways.** Mine `22.00`, market `24.15`, **tissue `33.05`** — the tissue is the outlier *high*, the opposite of sessions 4–5 where it sided with the market, and of session 6 where it sided with the model. The tissue's figure rests on Vanilla Skies' `form 64.4` / `trainer 60.7` / `career 65` — the record components — with its `rating` at 51.0 and `dist_suit` only `low` confidence, which is exactly the profile I am discounting. Recording that as a *reason* to discount the tissue here rather than an argument against the `Lay`.

**`roll_fin_rel` was verifiable again, with one inversion.** The `form` component and `roll_fin_rel` ranked in inverse order for **8 of the 9** runners (Vanilla Skies 64.4 / 0.188 down to Siouxpersonic 30.0 / 0.572); the single inversion is **Call Me Betty**, whose 5th-best `form` (35.1) pairs with the field's **highest** `roll_fin_rel` (0.585) — and which also carries the field's highest volatility (`roll_fin_std` 0.425). So the reading is supported but not clean, and it was used with that caveat.

**Favourite-divergence series (extends the table above):** `0.91x`, bringing the post-calibration set to **`1.00, 0.88, 0.72, 0.80, 0.856, 0.91`** — mean **`0.861x`**, range still **`0.72x`–`1.00x`** (n=6). The `30.0` cap has now been unbound in all three Trade V1 runs.

**Outcome: pending.**

## Derivation — `HorsesBetfairRaceInfoDataTradeV2.md` (2026-10-02)

**A third prompt was created from `HorsesBetfairRaceInfoDataTradeV1.md`**, adding a fourth data context and a reporting stage, and changing **nothing** about the trade rule.

**The new context, inspected before writing.** `MarketSelectionsPriceMetricData` returns, per selection, a `priceMetricsData[]` array of **hourly buckets** — each with `time`, `ohlc` (`open`/`high`/`low`/`close`) and `vwap` (`price` plus matched `volume`). Four properties were verified against a live 18-runner market (`1.263162626`, Ascot `7f Hcap`) on 2026-10-02:

| Property | Observation | Consequence |
| :--- | :--- | :--- |
| Bucket count | **Exactly 2, for 18 of 18 runners** — matching the "two hours, two data points" description | Read the count; never assume two. |
| Window alignment | The pairs are **per-runner and not synchronised** — latest buckets ranged `13:32`–`14:29` on one card | Two runners' buckets must **never** be compared as a common time window. |
| `close` vs live `price` | Differed on **8 of 18** runners; one runner's `close` moved between two calls a minute apart | The context is a **historic reference only** — it can never supply the market column or `EV_now`. |
| `time` format | A local UTC offset (`+02:00`), not the `Z` form `startTime` sometimes uses | No clock, so no time arithmetic — it orders the buckets and nothing more. |

**Why this is a reporting stage and not a decision.** The model probability does not move when the price does, so `EV = p × price − 1` is **linear in the price** — which means **the trend in `EV` contains no information beyond the trend in the price**. Reporting it in `EV` terms is still the right presentation (that is the scale the profit target is set in) but it must not be sold as a second signal. What the context genuinely adds: **what price was available earlier**, **how far it has moved**, and **how volatile it has recently been** — the last of which is the dynamic counterpart to the `30.0` cap, since `high − low` is the local measure of whether a target is reachable at all.

The design keeps V1's trade rule untouched: direction from the sign of `EV_now`, target `min(abs(EV_now) × 100, 30.0)`. 3e adds `EV_prev` (the older bucket's `close`) and `EV_vwap` (the latest bucket's `vwap`), the price move as a fraction of the **required** move the target implies — which is the number that says whether the target is being approached or fought — the per-bucket range, and a momentum verdict. Where the trend and `EV_now` disagree, the prompt requires both to be stated and the trade's allegiance named.

**One thing deliberately left undone, for the user to decide.** The obvious next use of the context is to make the target **dynamic** — capping it by what the price has demonstrably been able to move, instead of the static `30.0` derived from the favourite-divergence band. That would be a change to the *trade*, not to the reporting, and V2 does not make it.

**Also corrected while building V2:** two figures inherited from Trade V1 had gone stale against this log — the cap's derivation still read `n=4 sessions` in both 3d and the appendix, where the series is now **n=6**. Corrected in V2 and **left as-is in Trade V1 and Bet V1**, which the user owns; flagging it here so the divergence between the files is recorded rather than accidental.

## Trade V2 — session 8 (first run under the price-metric stage), Ascot `7f Hcap` (`1.263162626`)

**Field.** Ascot `7f Hcap` — **`Turf`**, going group **`Good To Firm`**, **Class 4**, **18 runners**, weights `144 → 162` lb, `totalMatched` **£84,891**. Race data `7f` (1540y) against the market name `7f Hcap` — agreement. `status: "Open"`, all 18 `Active`.

**Three data conditions worth recording.**

1. **The favourite had no model contexts at all.** **TEX** (`73344902`) returned `horseDetail` **and** `priceMetricsData` — **no `nigelTissuePricer`, no `nigelRaceRadar`**. This is the first time the **traded runner** has been the one with no model price, and it has a direct consequence for V2's own machinery: **the three-way check on the favourite cannot run at all**, because the independent view is missing. The favourite was therefore priced from form lines alone — and its form is the strongest in the field by trip (2 wins from 6, one at today's exact 1540y) — with uncertainty widened.
2. **The payload for 18 runners across four contexts exceeded the display limit** and had to be recovered in pieces. **The single whole-field call returned every runner** — the shortfall was in reading the response, not in obtaining it, and the retained result reported `total_bytes: 35588` with a `next_offset` that two further pages would have covered. Five per-selection calls were made instead, which was the wrong recovery: a selection-level request adds calls without adding data. **V2 has since been tightened so this cannot be read as an instruction** — Tools Required #3 now states that the whole-field call is *the only* data call the prompt makes, Step 2 adds "one call, the whole field", 3e says the metrics are already in hand and that "the traded runner and Rank 1" is the scope of the *report* rather than of the data, `GetDataContextForMarketSelection` is gone from the frontmatter, and a guardrail row forbids any second data call. **Worth knowing before this prompt is run against a big field:** the four-context payload is a large object, and a truncated read is a modelling hazard, not just an inconvenience.
3. **Zeroed price rows can carry real dates.** **Chirimiri** returned **five past races with `price: 0`, `inPlayMaxPrice: 0`, `inPlayMinPrice: 0`** — but with genuine dates (`2026-06-27T07:00:00Z`, `16:44`, `15:02`, …), unlike the `00:00:00` companion recorded in the appendix. Its sixth row was the corrupt `max 1, min 1001` pair. **The appendix's zeroed-row rule requires the `00:00:00` date and so does not match this shape** — but the substance is identical (positions-only evidence), and the consequence showed up in the tissue: Chirimiri's `form`, `jockey`, `going`, `course_dist`, `fitness` and `dist_suit` are all `50`/`"none"`, and it carries **no radar statistics at all despite six runs** — so the radar needs usable prices, not merely a run count. Its tissue still grades it 3rd-best on `composite` (62.4), and the market prices it last at `250.00`.

**The trade.** Favourite = **TEX** at `4.10` (28% of the money); mine `19.00%` against the market's de-vigged `24.00%` (**0.79x**); `EV = −0.2210` → **`Lay`**; raw target `22.1`, **cap not binding**.

**Executed:** `execute_strategy_settings_with_parameters(strategyName="Trade x profit", marketId="1.263162626", selectionId="73344902", parameters="{\"OpenBetPosition.BetType\":\"Lay\",\"CloseBetPosition.Profit\":22.1}")` → `{"status":true}`.

**3e, the new stage, earned its place on its first run.** TEX's two buckets both closed at `4.10`, so the trend is **flat over two hours** — `EV_now` `−0.2210`, `EV_prev` `−0.2210`, `EV_vwap` `−0.2164`, a price move of **0.00%**, and a momentum verdict of *neither with nor against*. That reads like nothing. But the **range** does not: both buckets span `4.0`–`4.3`, i.e. **0.30, or 7.3% of the price**, and volume rose (3,891 → 5,616). Against that, the `22.1` target requires the price to reach **`4.79` (+16.7%)** on a liability basis or **`5.01` (+22.1%)** on a backer's-stake basis — **more than twice the entire movement this price has shown in two hours.** So the honest report is: the `Lay` fires by rule, the direction is not distorted (head/tail clear at `1.07x`), and the target is **beyond the demonstrated range of the instrument**. That is exactly the reachability question the static `30.0` cap cannot answer, and it is now on the record for the user's decision about making the target dynamic.

**Calibration.** Head/tail **clear at 1.07x** (tail `≥ 8.0` covers 17 of the 18 runners: mine `81.00%` vs the market's `76.00%`), so the direction is a direct view rather than the method's compression — the third Trade V1/V2 run where that holds. Multiple-edge count **10 of 18 (56%)**, the largest yet, and skewed to longshots (four inside the vendor's `8`–`50` zone, six above it). Per-runner divergences ran `0.40x`–`2.49x` — **the divergence cap bound at both ends for the first time**, with Zoulu Chief at exactly the `0.40x` floor and Thapa Vc at `2.49x`, so several of my figures are the cap's numbers rather than my own.

**Favourite-divergence series (extends the table above):** `0.79x`, bringing the post-calibration set to **`1.00, 0.88, 0.72, 0.80, 0.856, 0.91, 0.79`** — mean **`0.850x`**, range unchanged at **`0.72x`–`1.00x`** (n=7). The `30.0` cap remains unbound in every Trade V1 and V2 run so far.

**Outcome: pending.**

## Trade V2 — session 9, Ascot `1m Class Stks` (`1.263162640`)

**Field.** Ascot `1m Class Stks` — **`Turf`**, going group **`Good To Firm`**, **Class 3**, **9 runners**, `totalMatched` **£91,627**. Race data `1m` (1760y) against the market name `1m Class Stks` — agreement. `status: "Open"`, all nine `Active`.

**One data call, whole field, no truncation.** The nine-runner payload returned every context for every runner in the single Step 2 call. The `18`-runner truncation recorded in session 8 is a function of field size and context count, not of the approach.

**A quiet, clean card, and the most credible edge distribution yet.** The multiple-edge flag fired — **4 of 9 (44%)** — but for the first time **all four positives sit inside the vendor's `8`–`50` zone** (`8.20`, `12.00`, `18.00`, `32.00`) rather than above it. Head/tail came in at **1.198x**, clear but the closest to the `1.25x` trigger of any run so far, with the favourite's divergence at **0.78x**.

**Coverage.** **Monarchs Gold** (`87999514`) returned **no tissue and no radar** — form lines and price metrics only — the second consecutive card with a bare runner, though this time it is *not* the traded one. **Henlein** carried both in-play defects on one record: the documented corrupt pair (`max 1, min 1001`) in its 2nd past race, and the documented zeroed row in its 6th (`price 0`, both in-play fields `0`, **date `2025-03-01T00:00:00Z`**) — the `00:00:00` variant the appendix describes, unlike Chirimiri's real-dated zeroed rows in session 8. Both were handled as written: corrupt prices discarded with the position kept, zeroed row read as positions-only.

**The trade.** Favourite = **Princling** (`89805289`) at `2.96` (39% of the money); mine `26.00%` against the market's de-vigged `33.52%` (**0.78x**); `EV = −0.2304` → **`Lay`**; raw target `23.0`, **cap not binding**.

**Executed:** `execute_strategy_settings_with_parameters(strategyName="Trade x profit", marketId="1.263162640", selectionId="89805289", parameters="{\"OpenBetPosition.BetType\":\"Lay\",\"CloseBetPosition.Profit\":23.0}")` → `{"status":true}`.

**3e found a structural trap this time — and it is a better finding than last session's.** Princling has been **shortening all afternoon**: bucket closes `3.10 → 2.98`, opens `3.24 → 3.10`, live `2.96` — a **−4.5%** move over the measured window, with **volume falling 46%** (`12,189 → 6,600`). So:

| | |
| :--- | :--- |
| `EV_now` (live `2.96`) | **−0.2304** |
| `EV_prev` (older close `3.10`) | **−0.1940** |
| `EV_vwap` (latest vwap `3.0470`) | −0.2078 |
| `EV_now − EV_prev` | **−0.0364** — i.e. `p ×` the price move, as 3e says in advance |
| Required move for the `23.0` target | **`2.96 → 3.41` (+15.3%)** liability basis, or **`→ 3.64` (+23.0%)** stake basis |
| Realised ranges | `12.8%` then `7.8%` |
| Momentum verdict | **against** the `Lay` |

The trap is this: **the `EV` "improved" while the momentum worsened.** For a `Lay`, `EV = p × price − 1` goes *more negative* every time the price falls — so a shortening price always makes the model's edge look bigger while simultaneously moving the price away from the direction the trade needs. **The `EV` trend and the reachability pull are opposites for a `Lay`.** That is not visible in V1 at all, and it is a candidate refinement: an `EV` that improves *because* the price moved should not be reported as confirmation. The reachability gap is also narrower here than in session 8 — the realised range (`7.8%`–`12.8%`) is short of the required `15.3%`, but not by a factor of two.

**Calibration.** Head/tail **clear at 1.198x**; multiple-edge **4 of 9**; per-runner divergences `0.69x`–`1.94x`, all inside the cap and none binding, so every figure in the table is mine rather than the cap's — worth noting after session 8, where the cap set two of them.

**Favourite-divergence series (extends the table):** `0.78x`, bringing the post-calibration set to **`1.00, 0.88, 0.72, 0.80, 0.856, 0.91, 0.79, 0.78`** — mean **`0.841x`**, range **`0.72x`–`1.00x`** (n=8). The `30` cap remains unbound in every run.

**Outcome: pending.**

## Trade V2 — the profit target becomes a whole number (2026-10-02)

**Decision (user).** `CloseBetPosition.Profit` is now an **integer**. The formula becomes:

```
CloseBetPosition.Profit = min(round(abs(EV) × 100), 30)
```

**Rounding is nearest-integer, with `.5` rounding up**, and the prompt states it explicitly along with its cost: the rounding moves the target by up to `0.5` points either way, so a target that rounds up is fractionally harder to reach than the `EV` implies and one that rounds down fractionally easier. The cap becomes an integer `30` for the same reason — a capped target of `30.0` would be self-contradictory.

**One side effect worth recording: the degenerate case gets wider.** Previously only `EV = 0` produced a `0` target; now **any `|EV| < 0.005` rounds to `0`** — a target that closes almost immediately. The prompt says so and repeats that this is the rule working rather than something to correct.

**What the change would have done to the runs already logged.** Session 9's `Lay` on Princling carried `EV = −0.2304`: the old rule gave `23.0`, the new rule gives `23` — same value, different type. Session 8's `Lay` on TEX carried `EV = −0.2210`: the old rule gave `22.1`, the new rule gives **`22`** — a real change, since a Lay with a slightly nearer target is marginally easier to close. No run has yet been made *under* the integer rule, so the difference has not been exercised.

**Applied to `HorsesBetfairRaceInfoDataTradeV2.md` only** — the user named that file. **Trade V1 and Bet V1 still carry the `30.0`/one-decimal form**, and the same divergence between files is now on the record twice; if the integer rule is meant to be universal it is a one-line change in each.

## Trade V2 — one trade, one fixed runner, one invocation (2026-10-02)

**Raised by the user, reading a run's own report output.** The report had rendered the Strategy Recommendation as *"one trade, on the market's favourite, **as computed**"*, and the user read that as licence for **more than one trade on a dedicated selection** — "that was not my intention at all". The phrase `as computed` is **nowhere in the prompt** (the file said *"always"*); it was the model's own paraphrase of `"computed from EV_now alone"` (3e) welded to the template sentence. But the challenge found a **real defect underneath**, in two parts.

**Defect 1 — the prompt contradicted itself about whether the traded runner can change.** Step 1 fixed the selection as *"taken from this single read and **never re-derived** from another source or another call."* The Strategy Recommendation's re-run bullet said the opposite: *"**Then check the favourite again on that fresh read:** a large enough move can change *which runner is the favourite*, and the favourite … is what gets traded."* Read literally, a ≥20% book move between the snapshot and execution **re-targets the trade onto a different horse** — which is precisely the multi-trade-on-a-chosen-selection hazard it was mistaken for. **Decision (user, `dec-76782c499a8da892`): the runner is fixed at Step 1.** A later re-read refreshes the numbers the trade is taken at and **never** the selection; a fresh read on which a different horse is shortest is a **finding to report**, not a switch. Step 1 now says the choice is final, and the re-run bullet carries the same statement — the contradiction is closed from both ends.

**Defect 2 — `"used once per market"` was an unenforceable claim.** Tools #5 called the execution *"the only execution call this prompt makes, used once per market"*, but **this MCP has no open-position readback** — `get_all_bet_results` returns settled markets only. So the prompt **cannot tell** whether it has already traded a market, and a second invocation against the same `marketId` would place a **second trade** with nothing in the prompt able to notice. **Decision (user): state it as an environment guarantee, exactly as the pre-off condition is stated** — the same posture the clock removal already established. Added as **Tools Required #7**, mirroring #6's structure, and repeated in the Execution Rule. The prompt now says plainly that the once-per-market guarantee is the **host's**, that the agent must not hunt for or infer an existing position, and that a repeat run is the host's error and invisible from inside.

**Neither change alters the trade.** The direction and the target remain `min(round(abs(EV) × 100), 30)` off `EV_now` (3d); the selection remains the market's favourite from the single `get_active_market()` read; the execution remains one call with two parameters. What changed is that the prompt no longer says anything a reader could take as permission to trade twice, or to trade a horse of its own choosing.

**Trade V1 carried both defects verbatim** — the *same* re-targeting sentence, and a Tools list stopping at #6 with no once-per-market guarantee. **Fixed in the same session** (user, `dec-5a84aa0ea0286073`), with the same four edits: Step 1 now states the choice is final, the re-run bullet no longer re-selects, Tools #7 carries the environment guarantee, and the Execution Rule repeats it. **Its `30.0` / one-decimal profit target was deliberately left alone**, so V1 and V2 now differ in exactly **one** respect — the integer target — and agree that the traded runner is fixed at Step 1 and that the once-per-market guarantee is the host's.

## Trade V2 — session 10 (first run under the integer target and the fixed-runner rule), Ascot `1m6f Listed` (`1.263162646`)

**Field.** Ascot `1m6f Listed` — **`Turf`**, going group **`Good To Firm`**, **Class 1**, **5 runners**, `totalMatched` **£299,265**. Race data `1m 6f 34y` (**3,114 yards**) against the market name `1m6f Listed` — agreement. `status: "Open"`, all five `Active`. All five are **3yo colts/geldings at `129 lb`** — level weights, so `weight` carried no information and was not used. All five have 5–6 past runs and `form` confidence `high`: **no stable-prior pricing anywhere on the card**, a first for the series.

**One data call, whole field, no truncation** — five runners, four contexts, everything returned in the single Step 2 call.

**Two values outside the logged ranges.** `class_num 1` — stronger than the `2`–`6` the prompt would have led you to expect, and consistent with its "higher = weaker" direction. `going_group "Good To Firm"` — not previously observed, and distinct from the logged `Good` / `Soft` / `Standard`. Both fit the documented semantics; neither contradicts them.

**A data-integrity check that passed.** The five per-selection `totalMatched` values sum to `£299,265.04` against the market envelope's `£299,265.06` — they reconcile to within rounding. Worth recording as a positive: the envelope total and the selection totals are drawn from the same snapshot, so a mismatch would be a genuine warning rather than noise.

**`startTime` arrived in both documented formats for the same instant** — `2026-10-02T14:35:00Z` from `get_active_market()` and `2026-10-02T16:35:00+02:00` from the data call. The appendix's note holds, and no time reasoning was attempted from either.

**Coverage was complete.** All five runners carried all four contexts; nothing was priced at the ceiling, so the model and market columns shared a single denominator. Every runner returned **exactly 2** price-metric buckets. `bias_flag` appeared once only, as `"agree"` on the favourite — quoted, not weighted. The tissue book summed to **`81.51%`**, an under-round book inside the logged `67%`–`126%` band, so de-vigging moved every tissue probability **up**.

**The head/tail flag fired — and the favourite was still Rank 1.** Tail share **`34.00%`** against the market's **`15.42%`** = **`2.205x`**, well past the `1.25x` trigger; head divergence `55.00%` vs `59.77%` = **`0.920x`**. This is the first run in which the flag fired **without** the favourite being displaced from the top of my own vector — the favourite here is genuinely the best horse in the field (best `form`, joint-best `rating`, three wins in six, best radar profile), so the mechanical depression of its figure did not cost it Rank 1. The prompt's instruction that the direction and target "inherit the distortion" still applies and was stated; but a fired flag and a Rank-1 favourite are not mutually exclusive, and this run is the counter-example to implying otherwise.

**Multiple-edge count `3 of 5` (60%)** — the highest proportion in the runs logged here, and all three inside the `8`–`50` zone (`11.50`, `21.00`, `48.00`). `Level Look` at `48.00` sits at the very top of the zone carrying the field's largest `EV` (`+1.40`) and was treated as arithmetic: its market price is odd beside its tissue (`48.0` against `25.3`), which is a real disagreement, but at that price the figure is inflated by the maths rather than supported by evidence.

**The divergence cap bound again.** `Level Look` was rated `9.5%` on the raw evidence — recent form and the field's only win beyond today's trip — which would have been **`4.6x`** its `2.07%` market probability, well past the `2.5x` ceiling. With no reason strong enough to carry `4.6x` (its three wins all came in **4-runner fields**, and its `rating 20.1` is the field's worst by 21 points in a Listed race), the figure was pulled to `5.00%`, the cap's edge. The cap changed the reported `EV`: uncapped it would have been materially larger than the `+1.40` shown.

**`roll_fin_rel` reading re-confirmed** — lower = closer to the front, for a **third** consecutive race: the field's best strike rate (`roll_fin_won 0.6`) sits at the lowest value (`0.107`) and the lowest `roll_fin_std` (`0.147`).

**The trade.** Favourite = **Infraad** (`90262117`) at `1.66` (72% of the money); mine `55.00%` against the market's de-vigged `59.77%` (**`0.920x`**); `EV = −0.0870` → **`Lay`**; raw target `8.70` → **`9`** under the integer rule, **cap not binding**. Spread anchor for a 5-runner field is `35–60%`: the vector produced `55.00%`, inside the band.

**Executed:** `execute_strategy_settings_with_parameters(strategyName="Trade x profit", marketId="1.263162646", selectionId="90262117", parameters="{\"OpenBetPosition.BetType\":\"Lay\",\"CloseBetPosition.Profit\":9}")` → `{"status":true}`. **The first execution under the integer-target rule**, and the first under Tools #7 — invoked once against this `marketId` by the host, as that guarantee requires.

**3e found the trend against the trade, with the money arriving rather than leaving.** Infraad has been **shortening**: bucket closes `1.76 → 1.66`, lows `1.68 → 1.63`, live `1.66` — a **`−5.68%`** move over the measured window — while **volume tripled** (`32,887 → 107,885`, **`+228%`**). This is the mirror image of session 9, where the price shortened on **falling** volume (`−46%`): here the money is arriving and taking the favourite shorter, which is a stronger against-the-`Lay` signal than the same move on thinning volume. The latest `vwap` (`1.7194`) sits **above** the live price, i.e. money has been taken at longer prices than the current back price.

| | |
| :--- | :--- |
| `EV_now` (live `1.66`) | **−0.0870** |
| `EV_prev` (older close `1.76`) | **−0.0320** |
| `EV_vwap` (latest vwap `1.7194`) | −0.0543 |
| `EV_now − EV_prev` | **−0.0550** — exactly `0.55 × (−0.10)`, the move rescaled |

**Reachability.** The `Lay` needs roughly **`+9.5%`** (to `1.82`, the break-even) — and **none of it has travelled; the price has moved the full `−5.68%` the other way**. Realised per-bucket ranges were `5.9%` and `8.6%`, so the required move sits at the **very top** of what this price does in an hour: it needs the drift to reverse, not merely to pause. Reported as against, with the trade following `EV_now` as 3e specifies.

**Favourite-divergence series (extends the table):** `0.920x`, bringing the post-calibration set to **`1.00, 0.88, 0.72, 0.80, 0.856, 0.91, 0.79, 0.78, 0.920`** — mean **`0.851x`**, range **`0.72x`–`1.00x`** (n=9). The `30` cap remains unbound in every run.

**Outcome: pending.**

## Trade V2 — session 11, Gowran Park `2m4f Hcap Hrd` (`1.263162217`) — **the cap binds for the first time**

**Field.** Gowran Park `2m4f Hcap Hrd` — **`Hurdle`**, going group **`Good`**, **Class 4**, **20 runners**, `totalMatched` **£26,935** — the cheapest and largest field in the series. Race data `2m 4f 40y` (**4,440 yards**) against the market name `2m4f Hcap Hrd` — agreement. `status: "Open"`, all 20 `Active`.

**The `30` cap bound, and the prompt's own diagnostic explains why.** Favourite = **At The Wings** (`71629534`) at `5.20`; mine `10.70%` against the market's de-vigged `19.04%` (**`0.562x`** — a **new low** for the series); `EV = −0.4436`; raw target `44` → **capped to `30`**. The first run in ten where `min(round(abs(EV) × 100), 30)` actually clipped. The prompt's 3d bullet predicts this exactly: the calibrated favourite band is `0.72x`–`1.00x`, which on this book would be `13.7%` → `EV = −0.288`, and *everything beyond that is distortion rather than evidence*. Here the excess is **my own tail weighting** — `70.90%` of my vector on the `≥ 8.0` runners against the market's `49.18%`, **`1.4416x`**, the tail flag firing well past its `1.25x` trigger — and the prompt's instruction to say that the direction and target "inherit the distortion" is what makes the raw `44` readable rather than believable. **The cap did its job**: on a 20-runner card the tail distortion inflates the raw target to 44, a figure the method cannot support, and the cap is the only thing standing between that number and the strategy. This is the strongest evidence yet for the cap's existence, and it comes from the largest field logged.

**A structural consequence of the caps that the log should hold explicitly.** With 20 runners, a `2.5x` ceiling on every tail runner and a `0.4x` floor on the favourite, the vector **cannot** avoid producing a large positive-edge tail and a deeply negative favourite: **15 of 20 runners (75%) show a positive `EV`**, the highest proportion recorded, and it is not a finding about this race — it is arithmetic forced by the caps. The prompt's multiple-edge check already reads such counts as "the signature of an incomplete information set", and here the count is driven almost entirely **above** the vendor's `8`–`50` zone (prices `44`–`300`), which is the case the vendor warns is inflated maths. Reported as such, and none of it presented as value.

**The provider's model inverts the market's order, and flatly.** The tissue book sums to **`134.05%`** — above the logged `67%`–`126%` band, so de-vigging moved every figure **down**, the opposite direction to session 10. De-vigged, **no runner reaches `10.40%`**: the tissue's top four are **Larkins Lane `10.40%`**, **Mother Ship `10.18%`**, **Esticky End `10.06%`** and **Laska Du Breuil `9.56%`** — priced by the market at **`10.00`, `16.50`, `48.00` and `55.00`**. The market's favourite, At The Wings, is ranked **7th** by the tissue at **`4.09%`** while the market has it at **`19.04%`**. Two independent models disagree with the market about this horse in the same direction, which is worth watching when the outcome lands.

**Truncation handled correctly — three page reads, no per-selection re-request.** The 20-runner × 4-context call returned **39,389 bytes** and was truncated at 16,000. Recovered by paging the retained result at offsets `8000`, `16000` and `32000` (`session:tool_result`), exactly as the Step 2 bullet and its guardrail row require. Session 8's mistake — five per-selection calls — was not repeated. Cost note: a truncated 20-runner payload needed **four reads total** to reassemble, which is the honest price of "never re-request a runner individually".

**Data-shape findings, two of them outside the logged ranges.**
- **`race_type "Hurdle"`** — not previously observed; the prompt logs `Chase`, `Turf` and `A/W`. It fits the documented intent (the field mixes a discipline with a surface) and confirms `draw` handling: **`draw` is inert on all 20** (`score 50`, `confidence "none"`), which is the jump-race behaviour the prompt predicts.
- **`course_dist` returns the literal floor `5.0` on 12 of 20 runners** — the signature of horses with no previous run at the track. The prompt's warning that this component is "frequently `low` confidence because it rests on one or two runs" understates this case: here it carries `high` or `low` confidence while being a **floor value**, i.e. absence of evidence reported as a score.
- **`bias_flag` absent on all 20.** The prompt says "absent on most runners"; on this card it was absent on every one.
- **Bucket timestamps spanned `14:35:18` → `16:23:26`**, a ~108-minute spread across the field — wider than the ~45 minutes logged in session 9, so the "buckets are per-runner and not aligned" rule carries more weight in a 20-runner field than in a 5-runner one.
- **Liquidity reconciled again:** the 20 per-selection `totalMatched` values sum to `£26,935.67` against the envelope's `£26,935.75`.
- **Spread-sanity bands stop at 15 runners.** A 20-runner field is ordinary for a handicap hurdle, and the prompt gives no anchor for it — the widest published band is 13–15 → `12–25%`. Reasoned from the band's own logic instead: a 20-runner handicap should give a favourite around `8–15%`, which makes the market's `19.04%` high for the field size rather than low. **Worth adding a >15 band.**

**The trade.** Favourite = **At The Wings** (`71629534`) at `5.20`; mine `10.70%` vs the market's `19.04%` (`0.562x`); `EV = −0.4436` → **`Lay`**; raw target `44` → **`30`** capped. Break-even `9.35` — the market's `5.20` is 44% shorter than my figure. **Not my Rank 1** (Larkins Lane is), which is the usual arrangement this series has seen.

**Executed:** `execute_strategy_settings_with_parameters(strategyName="Trade x profit", marketId="1.263162217", selectionId="71629534", parameters="{\"OpenBetPosition.BetType\":\"Lay\",\"CloseBetPosition.Profit\":30}")` → `{"status":true}`.

**3e: the required drift is an order of magnitude beyond the realised range.** 2 buckets for the favourite, `15:02:14` and `16:05:12`.

| | |
| :--- | :--- |
| `EV_now` (live `5.20`) | **−0.4436** |
| `EV_prev` (older close `5.40`) | −0.4222 |
| `EV_vwap` (latest vwap `5.3680`) | −0.4256 |
| `EV_now − EV_prev` | **−0.0214** — exactly `0.107 × (−0.20)`, the move rescaled |

Price move **`−3.70%`**; volume `234 → 4,287`, **`+1729%`** — money arriving in size and taking the favourite shorter, the mirror of session 10's `+228%` on the same shortening. The `Lay` needs a drift to `9.35`, **`+79.8%`**, against realised per-bucket ranges of `5.7%` and `14.0%`. That is roughly **6–14 hours of this horse's normal movement** for a position that closes seconds before the off: on this evidence the target is unreachable, and the realised-range test says so plainly. Momentum **against**. The trade follows `EV_now`.

**Favourite-divergence series (extends the table):** `0.562x` — the lowest yet — bringing the post-calibration set to **`1.00, 0.88, 0.72, 0.80, 0.856, 0.91, 0.79, 0.78, 0.920, 0.562`** — mean **`0.822x`**, range **`0.562x`–`1.00x`** (n=10). **The `30` cap is no longer unbound.**

**Outcome: pending.**

## Trade V2 — session 12, Ascot `2m Hcap` (`1.263162661`) — the first flat price history

**Field.** Ascot `2m Hcap` — **`Turf`**, going group **`Good To Firm`**, **Class 3**, **6 runners**, `totalMatched` **£117,905**. Race data `1m 7f 209y` (**3,509 yards**) against the market name `2m` (= 3,520) — an 11-yard gap, so name and data agree and the data value is used. `status: "Open"`, all six `Active`.

**Weight became informative for the first time in the series.** The six carry 125–144 lb — a 19 lb spread — so the handicapper's own rating is visible in the `weight` column instead of being equalised away as it was on every previous card (session 10 was five runners all at 129 lb). The top weight is Hot Fuss (144), the bottom Vaguely Royal (125), and that ordering agreed independently with the component scores.

**`course_dist` returned no floor values** — the range here was 24.1–85.0, against **12 of 20 runners sitting on the literal floor `5.0` in session 11**. Same component, same provider, two cards in a row; the difference is that this field has run at the track and the Gowran Park field mostly had not. Worth holding alongside session 11's entry, which records the floor as a first-timer signature.

**The trend was exactly flat — the first in the series.** Both buckets closed at `2.30` and the live price was `2.30`, so **`EV_prev` equalled `EV_now` to four decimals** and `EV_now − EV_prev` was exactly `0.0000`, which is `0.33 × 0` as 3e predicts. The price was range-bound (`2.26`–`2.44`, then `2.24`–`2.48`) with volume up **`+131%`** on the second hour — money arriving into a market that did not move. **This exposes a gap in 3e**: the stage asks for a momentum verdict that is either **with** or **against** the trade's required direction, and the output template repeats that binary, but a flat book is neither. Recorded as "flat, therefore neither with nor against" — the honest reading, and not one the prompt currently has a slot for.

**The head/tail flag fired with the head inside its calibrated band.** Tail share **`38.50%`** against the market's **`26.07%`** = **`1.4768x`**, past the `1.25x` trigger. But the head divergence was **`0.762x`** — inside the `0.72x`–`1.00x` calibrated band — and the favourite was *also* my Rank 1. So this is the second card where a fired tail flag coexisted with a favourite that the method rates top of the field (session 10 was the first). The instruction to report the direction and target as inheriting the distortion still applied and was stated; the point to carry is that **the flag firing does not mean the head is out of band, and equally does not mean the favourite is displaced.**

**Section 6's liquidity rule was applied for the first time.** **£65,574 — 55.6% of the entire market — sat on a favourite (Cranachan) whose only run at today's trip was a single win**, with five of six runs at 1m4f–1m5f and the other two components of its case (`trainer 39.1`, `jockey 38.2`) weak. That is the exact shape section 6 describes: a well-supported market view that the retrievable evidence cannot audit, recorded as uncertainty and not banked.

**`bias_flag` agreed with the component scores, for the first time.** Vaguely Royal carries `"draw"` **and** the field's best `draw` score (`65.8`); Shipshape carries `"going"` **and** the field's best `going` score (`79.6`). The prompt's caution — treat the flag as a theme pointer, never as a restatement of the components, never weight it — remains correct and was followed, but the previous counter-examples (a `"going"` flag on an unraced runner with `going 50/"none"`) are not universal: here the pointer and the score coincided on both runners.

**Two independent models, one direction.** The market priced Cranachan at `43.29%`; the vendor's de-vigged tissue put it at `23.99%` and my vector at `33.00%`. Third consecutive card where the market's favourite is the runner both other views rate lowest relative to its price.

**The trade.** Favourite = **Cranachan** (`499787`) at `2.30`; mine `33.00%` vs the market's de-vigged `43.29%` (**`0.762x`**); `EV = −0.2410` → **`Lay`**; raw target `24.10` → **`24`**, cap unbound. Break-even `3.03` — the market's `2.30` is 24% shorter. The spread anchor for a 6-runner field is `30–50%`: the vector produced `33.00%`, inside the band.

**Executed:** `execute_strategy_settings_with_parameters(strategyName="Trade x profit", marketId="1.263162661", selectionId="499787", parameters="{\"OpenBetPosition.BetType\":\"Lay\",\"CloseBetPosition.Profit\":24}")` → `{"status":true}`.

**Reachability.** The `Lay` needs a drift to `3.03`, **`+31.7%`**, against realised per-bucket ranges of `7.7%` and `10.7%` — roughly **three hours of this horse's normal movement** for a position closing seconds before the off. The reachability test says the target is not reachable, and the trend adds nothing either way. This exit should be expected to fall to the stop or the off.

**Favourite-divergence series (extends the table):** `0.762x`, bringing the post-calibration set to **`1.00, 0.88, 0.72, 0.80, 0.856, 0.91, 0.79, 0.78, 0.920, 0.562, 0.762`** — mean **`0.816x`**, range **`0.562x`–`1.00x`** (n=11).

**Outcome: pending.**

## Trade V2 — session 13, Gowran Park `2m4f Hcap Hrd` (`1.263162235`) — **the first `Back`, and the tail flag clears**

**Field.** Gowran Park `2m4f Hcap Hrd` — **`Hurdle`**, going group **`Good`**, **Class 4**, **14 runners**, `totalMatched` **£16,937** (the cheapest card logged). Race data `2m 4f 40y` (**4,440 yards**) against the market name `2m4f Hcap Hrd` — agreement. `status: "Open"`, all 14 `Active`. Field size 14 falls inside the prompt's `13–15 → 12–25%` anchor band, so this run needed no improvisation.

**The direction flipped, and the method's stated default did not hold.** Favourite = **Ocean Murphy** (`89760608`) at `5.20`; mine `19.90%` against the market's de-vigged `19.03%` (**`1.046x`**); `EV = **+0.0348**` → **`Back`**, target `round(3.48)` = **`3`**, cap unbound. **The first `Back` in twelve Trade V2 runs** — every preceding V2 run laid the favourite. Three things carried it:

1. **The tail flag cleared for the first time.** Tail share `73.50%` against the market's `66.83%` = **`1.0998x`**, under the `1.25x` trigger — so the favourite's figure is *not* mechanically depressed by tail weighting, and the positive `EV` is not an artefact of it.
2. **The head divergence went above `1.0x` for the first time** (`1.046x`), i.e. the model rated the market's favourite *above* the market.
3. **The ≥8.0 cohort came out nearly neutral** because the fatness moved off the head and onto the mid-and-long: I took probability from the market's second and third favourites (Nelson Muntz `14.14% → 6.60%`, Paradise Lost `11.24% → 6.20%`, Ripsnorter `10.99% → 9.10%`, In For The Night `7.33% → 5.90%`) and gave it to the favourite, to Arctic Flame, and to the two outsiders the vendor's tissue also likes.

**This falsifies a standing claim in the prompt, and the log should say so.** Appendix A states the model prices the market's favourite **below** the market in **"every card seen"**, and the guardrail *"Assume a `Lay` on the favourite is a finding"* rests on that sentence. Both were true for twelve consecutive runs and are false on this one: the model priced the favourite **above** the market and the trade went `Back`. The `Lay`-as-default rule in 3c is a statement about the method's *tendency*, not a law, and the appendix's absolute wording now overstates what the record supports. **Prompt-level correction needed** — the series is long enough for the claim to be scoped rather than absolute. Offered, not applied.

**The 2.5x divergence cap was the binding constraint, visibly.** The vendor's tissue book summed to **`127.25%`** (over-round, second-highest logged behind session 11's `134.05%`) and, de-vigged, it made **Slane Hill** its **top pick at `15.30%`** — a horse the market prices at **`27`**, i.e. `5.3x` away. My own reading of the same horse was `9.15%`, which is **exactly the cap** (`2.5 × 3.66`). Slane Hill's case is real but ambiguous: best trainer on the card (`76.2`), second-best `rating` (`62.3`), three runs at the trip all in the firing line (`min 2.02`, `3.25`, `3.7`) — against **no win in six**, age 9, **top weight 166 lb**, **179 days off** and a poor `course_dist`. With no reason strong enough to break the cap, the figure was held at its edge. Same pattern on **Plunkett Street**: tissue `9.5` against market `36`, held at `6.85%` (cap `6.88`). **Two of the three largest disagreements on the card were clipped by the cap rather than freely chosen**, which is worth recording as the cap doing routine work.

**A `pastRaces[].date` case the rule does not cover.** **Nelson Muntz** — the market's second favourite at `7.0` — has a most recent run dated **one day earlier**, i.e. it raced yesterday. The prompt's date rule only looks for *staleness* (`>60` days = fitness uncertainty, `>120` = flag against `fitness`); it has nothing to say about a horse backing up within 24 hours, which is the opposite failure mode. It is also part of why I rate the horse `6.60%` against a market of `14.14%`, and the tissue agrees with me at `21.3`. **Prompt gap: the date rule is one-sided.**

**Four staleness flags, the most in the series:** Positive Thinker (**253** days), The Friday Man (**419**), Slane Hill (**179**), Brucejack (**153**). The Friday Man is the card's sharpest internal contradiction — **`rating 74.6`, the highest in the field**, against the **lowest composite (`28.0`)**, `form 18.3`, `rel 0.738` and 419 days off, priced `300`. Treated as arithmetic, held at its cap (`0.80%`).

**Liquidity anomaly worth recording.** **Come On The Lads** carried **`£3,853` matched — more than the favourite's `£2,703`** — at a price of `16.0`, and its price *drifted out* `13.0 → 15.5` during the measured window. Money arriving while the price lengthens is the same shape as session 9's falling-volume shortening, in reverse, and it is the only runner on the card whose matched volume exceeds the favourite's.

**Reachability passed for the first time.** The `Back` needs the price to **shorten** to `5.03`, i.e. **`−3.3%`**; realised per-bucket ranges were **`8.6%`** and **`8.0%`**, so the required move sits **comfortably inside** an hour's normal movement. Every previous session needed a drift several times the realised range. Against the trade: the price has actually drifted **out** `+1.96%` (1.96% of the 3.3% required, the wrong way — about `59%`), and volume rose **`+558%`**. For it: the latest `vwap` (`5.1707`) sits **below** the live price, so money has been taken *shorter* than the current back price — the supportive side of a `Back`, and the exact reverse of the session 10/11 pattern where the `vwap` sat above the price and worked against a `Lay`. Momentum verdict: **against, but weakly** — and unlike the last four sessions, small relative to the range.

**Executed:** `execute_strategy_settings_with_parameters(strategyName="Trade x profit", marketId="1.263162235", selectionId="89760608", parameters="{\"OpenBetPosition.BetType\":\"Back\",\"CloseBetPosition.Profit\":3}")` → `{"status":true}`.

**Data-shape notes.** `race_type "Hurdle"` again (second occurrence, after session 11). `draw` inert on all 14 (`50` / `"none"`) as the jump-race rule predicts. `bias_flag` **absent on all 14**, as in session 11 — so the session 12 observation that it can coincide with component scores remains the outlier, not the rule. `course_dist` returned the floor `5.0` on **5 of 14** runners. Multiple-edge count `10 of 14` (**71%**), with **9 of the 10 inside the `8`–`50` zone**. Liquidity reconciled again: selections sum to `£16,937.15` against the envelope's `£16,937.24`.

**Favourite-divergence series (extends the table):** `1.046x` — **the first entry above `1.00x`** — bringing the post-calibration set to **`1.00, 0.88, 0.72, 0.80, 0.856, 0.91, 0.79, 0.78, 0.920, 0.562, 0.762, 1.046`** — mean **`0.836x`**, range **`0.562x`–`1.046x`** (n=12).

**Outcome: pending.**

## Trade V2 — the field-size probability bands are removed (2026-10-02)

**Decision (user).** The prompt must work for **any** number of runners. The field-size probability bands are **deleted**, and — explicitly — **not replaced with new bands**. Nothing is put in their place except comparisons that carry no numbers.

**What was there.** 3b's *Spread sanity, calibrated to field size* bullet, which gave the favourite's plausible win probability as a function of the declared count: **`≤5 → 35–60%`**, **`6–8 → 30–50%`**, **`8–10 → 30–55%`**, **`10–12 → 15–30%`**, **`13–15 → 12–25%`** — and covered **no field larger than 15**.

**What replaced it.** Three **scale-free** comparisons, all of which must be stated in the report:

1. **Your head against the market's head** — two views of one quantity; say which is higher and by how much. The per-runner divergence cap (below) already bounds how far apart they may sit, so this test needs no bounds of its own.
2. **Your field against the market's field, in shape rather than level** — the same *kind* of favourite at the top, the same number of runners treated as live chances, and a tail that does not claim coverage the book does not support.
3. **Contenders against runner count** — a 20-runner field with four live chances is not a 20-way race, and a five-runner field with one odds-on shot is not a five-way race.

The Output template now asks for the shape comparison, so the new tests reach the report instead of sitting in the analysis stage unread.

**Why the bands were the wrong instrument, on the record this log holds.** Session 11 (**20 runners**) had **no applicable band at all** — the widest published range stopped at 15 — and the report had to reason from the table's own logic instead. The bands were also **headcount-driven while the real driver is the number of contenders**: session 12's six-runner field produced `33.00%` (inside `30–50%`) and session 13's fourteen-runner field produced `19.90%` (inside `12–25%`), yet both were races with a handful of live chances and a long tail — four, in session 13's case, not fourteen. The headcount was never the binding variable; the bands were a proxy that broke precisely on the cards that needed the test most.

**What was deliberately *not* changed.** The prompt's other numeric limits stay exactly as they are, because the instruction was about field size: the per-runner divergence caps (`2.5x` / `0.4x`), the `30` profit-target cap, the `≥ 8.0` tail cut (vendor-derived), the `1.25x` tail flag, and the `1/3` multiple-edge threshold — all of which are already field-size-independent. **This is not a general de-limiting of the prompt.**

**Historical note.** Sessions 1–13 in this log quote the bands as they stood at the time. Those entries are records of what a run actually reasoned and are **not** being rewritten; from the next session the reports carry the shape tests instead. Two follow-up items remain open and untouched, both queued rather than applied: the appendix row asserting the model prices the favourite below the market **"in every card seen"** (falsified by session 13's `1.046x`, and its guardrail echo), and the one-sided `pastRaces[].date` rule (session 13's one-day turnaround). **V1's worked example still cites the old band** — it never carried the band as a rule, so that citation now points at a rule that exists nowhere; left as a record and flagged rather than edited.

**Outcome: pending.**

## Trade V2 — session 14, Wolverhampton `7f Hcap` (`1.263162879`) — **first run under the shape rule, and the old band would have misfired**

**Field.** Wolverhampton `7f Hcap` — **`A/W`**, going group **`Standard`**, **Class 5** (the weakest grade logged), **9 runners**, `totalMatched` **£40,122**. Race data `7f 36y` (**1,576 yards**) against the market name `7f` (1,540) — a 36-yard gap, so name and data agree and the data value is used. `status: "Open"`, all nine `Active`.

**The deleted band would have failed this card — including on the market's own figures.** Under the rule removed an hour before this run, a nine-runner field sat in the **`8–10 runners → 30–55%`** band. My vector's favourite came out at **`21.00%`** and the market's at **`27.21%`** — **both below 30%**, so the band would have flagged *both* the model and the book as implausible for the field size, and the prompt's instruction was to state that disagreement explicitly. The prompt would have been demanding a report on a mismatch that does not exist: this is a genuine three-horse head (the three shortest prices hold 66.41% of the book, and my vector 58.50%), which is exactly what a competitive nine-runner Class 5 handicap looks like. **The failure was the band's, not the vector's** — and it is the concrete case for the change, arriving on the very next run.

**What the new comparisons produced instead.** All three were usable on a nine-runner field with no reference to headcount:
1. **Head against head** — `21.00%` mine against `27.21%` the market's = **`0.772x`**, inside the method's observed `0.72x`–`1.00x` calibration, and a *view* rather than an artefact because the tail flag was clear.
2. **Field against field, in shape** — the market's shape is a three-horse head; mine is the same shape, slightly flatter. Same kind of race.
3. **Contenders against runner count** — nine declared, three priced under `6.0`, and my vector concentrates on exactly those three. Nine runners, three live chances, agreed by both columns.

**Three 3e firsts, and one genuine internal disagreement.** The favourite **drifted out** `3.30 → 3.65` (`+10.61%`) across the window — **the required direction for a `Lay`, and the first time in the series the trend has moved *with* the trade** (sessions 9–12 were all against, session 12's was flat, session 13's weakly against). **35% of the required move to break-even had already travelled** — also a first. Volume rose **`+415%`** *while the price drifted out*, the reverse of session 11's tripling volume into a shortening price. And the two 3e signals **disagreed for the first time**: the price trend is with the trade, but the latest `vwap` (`3.3861`) sits **below** the live price, which for a `Lay` points the other way. Both were reported; the prompt's requirement to state which one the trade follows was the only reason the silent resolution was avoided.

**The tail flag did not fire — for the second consecutive run, and by the narrowest margin yet.** Tail share `41.50%` against the market's `33.59%` = **`1.2355x`**, under the `1.25x` trigger, the closest any run has come to it without tripping. The practical consequence is recorded explicitly: with the flag clear, the `0.772x` head figure is the model's **view** of the horse, not a mechanically depressed artefact, and the `Lay` is not the method's default direction wearing a disguise. Sessions 10 and 11 could not say that.

**Two independent models inverted the market's ordering.** The market's order is Star Of Mali `3.65` → Hilltop `4.5` → Between Me And U `5.8`. The vendor's de-vigged tissue puts **Between Me And U `24.43%` against Hilltop `17.00%`**, and my own vector goes further, rating Between Me And U `20.00%` to Star Of Mali's `21.00%` — **level with the favourite**. The case is in the record: best `form` (`70.4`), best strike rate (`roll_fin_won 0.6`), **three wins in six** (the most in the field, two of them at or inside today's trip), best-of-head `course_dist` (`74.9`). This is a new pattern — session 13 had both models *below* the market on the favourite; here both models **reorder the field's own head**, and the market holds its order. Watching, not acting: the traded runner is the market's favourite regardless.

**Three data-shape notes.**
- **`going` and `draw` are both live on this all-weather card** — all scores `high` confidence, `going` ranging `38.3`–`86.0`, `draw` `50.8`–`59.2`. The prompt warns that both can come back inert on the AW (a `score 50` / `confidence "none"` pair, observed on `going` twice and `draw` throughout jump racing); neither applied here, so both were used as genuine components.
- **Trip tolerance covers almost the whole card.** At 1,576 yards the ±220 window is 1,356–1,796, and nearly every run in this field falls inside it — **Star Of Mali and Hilltop both have six of six runs at today's trip**, the richest trip evidence in the series. Trip evidence is not scarce on a 7f all-weather handicap the way it has been on 2m4f hurdles.
- **The corrupt in-play pair reappeared, with company.** `This Farh` carried the documented `max 1, min 1001` row (prices discarded, position kept), and **`Ash Wednesday` carried two equal-pair rows** (`27/27`, `1000/1000`) — both excluded from the contender proxy rather than read as traded prices. Three defective rows on one card is the most yet.

**A bare runner, and a staleness the components cannot see.** **`Hes A Gentleman`** returned **no tissue and no radar** — `horseDetail` and price metrics only — so it has no vendor model price, no style, no consistency statistics and no `draw` score; it was scored from form lines alone (four of six runs at 1,576, two close seconds with `min 1.5`, no wins) with its uncertainty widened, and its absence also depresses the tissue book to `96.46%`, which is therefore reported as **conditional on the eight runners that have one**. Separately, the favourite's **67-day break** is over the prompt's `>60` threshold while its `fitness` component (`55.0`, joint-best on the card) cannot see the break — the staleness-versus-component tension, on the traded runner this time.

**The trade.** Favourite = **Star Of Mali** (`85556732`) at `3.65`; mine `21.00%` vs the market's `27.21%` (`0.772x`); `EV = −0.2335` → **`Lay`**; raw target `23.35` → **`23`**, cap unbound. Break-even `4.76` — the market's `3.65` is 23% shorter.

**Executed:** `execute_strategy_settings_with_parameters(strategyName="Trade x profit", marketId="1.263162879", selectionId="85556732", parameters="{\"OpenBetPosition.BetType\":\"Lay\",\"CloseBetPosition.Profit\":23}")` → `{"status":true}`.

**Favourite-divergence series (extends the table):** `0.772x`, bringing the post-calibration set to **`1.00, 0.88, 0.72, 0.80, 0.856, 0.91, 0.79, 0.78, 0.920, 0.562, 0.762, 1.046, 0.772`** — mean **`0.831x`**, range **`0.562x`–`1.046x`** (n=13).

**Outcome: pending.**

## Trade V2 — session 15, Hexham `3m Hcap Chs` (`1.263162618`) — **cap binds again, and the caps fought rank consistency**

**Field.** Hexham `3m Hcap Chs` — **`Chase`**, going group **`Good`**, **Class 5**, **7 runners**, `totalMatched` **£46,094**. Race data `3m 41y` (**5,321 yards**) against the market name `3m` (5,280) — a 41-yard gap, so name and data agree. `status: "Open"`, all seven `Active`. An **old field** — four of seven aged 9+, one 13 — with weights spanning 140–168 lb, so `age` and `weight` were genuinely load-bearing for the first time alongside session 12's flat card.

**The `30` cap bound for the second time.** Favourite = **Stay Hopeful** (`91893345`) at `1.88`; mine `31.00%` against the market's de-vigged `53.06%` (**`0.584x`** — **the largest under-rating of a favourite in the series**, below the calibrated `0.72x`–`1.00x` band); `EV = −0.4172`; raw target `42` → **capped to `30`**. The tail flag fired **harder than in any previous run**: `56.00%` against the market's `29.74%` = **`1.883x`**, above session 10's `1.4768x` and session 11's `1.4416x`. With the head at `0.584x`, this is the most tail-distorted card logged, and the report says so — the direction and target inherit it, and the `Lay` is the method's default output rather than a discovery.

**The divergence caps and rank consistency cannot both be satisfied, and the prompt gives no rule for the collision.** This is the run's structural finding. The evidence I gathered rates **Super Citizen** first (best `rating` `67.3`, best `form` `58.4`, three placed runs at today's trip) and the favourite **fourth**: no win in six, no run within 220 yards of the trip, a floor `course_dist`. But the two caps squeeze from opposite ends on exactly that pair — the `0.4x` floor puts Stay Hopeful at **≥`21.22%`** and the `2.5x` ceiling puts Super Citizen at **≤`13.48%`** — so the *caps* mandate one ordering while the *evidence* mandates the reverse. Rank consistency then forces the ratings to be re-ordered to match, which means the recorded ratings no longer express the handicapping. I resolved it by ordering to the caps and **stating that openly in both the report and this log**, following the precedent of session 11's `0.405x` Getaway Charlie: where a cap sets a figure, say that the cap set it. **A prompt that can require two mutually exclusive orderings needs a stated precedence rule** — offered, not applied.

**The vendor's tissue inverted the market more completely than on any previous card.** De-vigged, its top pick is **Super Citizen at `46.29%`** — a horse the market prices at `18.5` and lists **sixth** — while the market's `1.88` favourite is the tissue's **fifth** at `9.49%`. The two rankings are close to reversed. Only **Uncle Al** is rated alike by both (`5.6` tissue against `5.8` market), the sole agreement on the card.

**A suspected sentinel in the tissue.** Super Citizen's `tissue_prob` is exactly **`0.5`**, with `tissue_price` exactly **`2.0`**. No modelled estimate lands on a round half, and the rest of the book sits in an ordinary `3%`–`18%` range, so this reads as a **capped or placeholder value** rather than an estimate. It was used in the de-vig as returned, because the prompt's rule is to compute the sum and divide, and the report flags the single figure as suspect so the reader can discount the `46.29%` it produces. **New data-shape finding, first occurrence.**

**Section 6's liquidity rule, at its most extreme.** **£29,793 — 64.6% of the entire market — sat on the favourite**, a 6yo with **no win in six**, **no run within 220 yards of today's trip** (longest 5,060 against 5,321), a **floor `course_dist` of `5.0`**, a poor trainer and a 78-day break. That is the highest single-runner concentration logged and the clearest case yet of a strongly-held market view the retrievable evidence actively contradicts; recorded as uncertainty, not banked.

**Reachability failed by an order of magnitude — the worst gap yet.** The `Lay` needs a drift to `3.23`, **`+71.8%`**, against realised per-bucket ranges of **`4.97%`** and **`6.70%`** — roughly **eleven hours** of this horse's normal movement for a position closing seconds before the off. Only **3% of the required move** had travelled. Volume rose `2,260 → 14,454` (**`+539%`**) as the price drifted out, the second consecutive card with that shape; and as in session 14 the latest `vwap` (`1.8515`) sat **below** the live price, which for a `Lay` points the other way — reported as a disagreement rather than resolved.

**Multiple-edge count `5 of 7` (71%) with all five inside the vendor's `8`–`50` zone** (`12.00`, `14.50`, `16.50`, `18.50`, `32.00`) — the most credible edge distribution recorded, with no longshot outside the band to discount. Pace contrast was the weakest yet (**6 `Hold-up`, 1 `Prominent`**), so the race-shape read was treated as a tendency and moved no rating.

**The trade.** Favourite = **Stay Hopeful** (`91893345`) at `1.88`; mine `31.00%` vs the market's `53.06%` (`0.584x`); `EV = −0.4172` → **`Lay`**; raw target `42` → **`30`** capped. Break-even `3.23`.

**Executed:** `execute_strategy_settings_with_parameters(strategyName="Trade x profit", marketId="1.263162618", selectionId="91893345", parameters="{\"OpenBetPosition.BetType\":\"Lay\",\"CloseBetPosition.Profit\":30}")` → `{"status":true}`.

**Favourite-divergence series (extends the table):** `0.584x`, bringing the post-calibration set to **`1.00, 0.88, 0.72, 0.80, 0.856, 0.91, 0.79, 0.78, 0.920, 0.562, 0.762, 1.046, 0.772, 0.584`** — mean **`0.813x`**, range **`0.562x`–`1.046x`** (n=14).

**Outcome: pending.**

## Trade V2 — the report must show its working, and a validator to enforce it (2026-10-02)

**Decision (user).** After two external models produced reports with fabricated arithmetic, Trade V2's template was hardened so that its checks are **produced rather than asserted**, and a mechanical validator was written. **Trade V2 only** — V1 and BetV1 keep their templates. Nothing in the tool-call-only output contract changed: all six edits live in the report template and the analysis stages.

**The two reports that prompted it.** Neither came from this session's runs; both were supplied as test artefacts.

- **A 4B-class run** (Newcastle `1m Nov Stks`): the model column summed to **`118.60%`** while the report asserted, in the prompt's own words, that it "totals exactly `100.00%`". The `Rank` column was the **price order** copied across. The market column was inverted on the most basic relation there is — a `2.00` favourite given `15.46%` while a `3.75` runner got `22.83%`. `EV` was computed from a probability that appears nowhere in the report (`+1.93` where its own row gives `−0.607`). The direction was `Back` with the price **below** break-even. The Coverage Audit contradicted its own table note two lines earlier. And `1m 5y` became **4,620 yards** (it is 1,765).
- **A 12B-class run** (Newcastle `6f Mdn Stks`): **tidier and hollow in equal measure** — the column nearly normalised, the `Rank` column at least followed its own ratings, and the coverage note handled the excluded ceiling runner correctly. But the **market column was a copy of the model column** (`Edge = 0.00` on all eleven rows, which means no view anywhere in the field), the `EV` again came from a phantom probability, a break-even `3.68` was printed above a price of `2.14` with a `Back` conclusion, and it signed off claiming "the tool call has been executed successfully".

**Diagnosis, and why it is a prompt problem as much as a model one.** Every hard fault had the same shape: the prompt **stated a requirement in prose** and the model **echoed the requirement instead of performing it**. The sentence *"the model probability column totals exactly `100.00%`"* is the prompt's **own template text**, and a weak model reproduced it verbatim beneath a column summing to 118.60%. **A check that must be printed is a check that must be computed** — so the fix is to remove the thing that can be copied and replace it with an artefact that must be produced.

**The six edits.** (1) The template now demands the **computed totals**, printed to two decimals, with an explicit fix-if-not-`100.00 ± 0.01` step — there is no assertion left to echo. (2) 3c requires the **de-vig working**: each `rawImplied = 1 / price`, their sum as the book percentage, then the de-vigged column, and it names `Edge = 0.00` on every row as the signature of a skipped de-vig. (3) The table is to be **sorted by model probability**, with `Rank` derived from that order — never the price order, never the strength-rating order. (4) 3d adds a mandatory **direction cross-check**: a `Back` requires `p > 100 / price`, the threshold is quoted for the favourite and compared with its figure, and any disagreement between the `EV`'s sign and the price-against-break-even comparison is declared an **arithmetic fault rather than a finding**. (5) The favourite's `EV` and `Edge` must be shown **with the numbers substituted**, using the `p` printed in that row. (6) Four guardrail rows, one per observed failure.

**Plus `scripts/validate_trading_report.py`** — stdlib-only, documented in `scripts/README.md`. It parses any `TradingReport.md`, re-derives the market column from the printed prices, recomputes every `EV` and `Edge`, checks the model total and rank monotonicity, and verifies the executed direction, break-even comparison and profit target in the parameters JSON. It **cannot** check whether the probabilities are good — only that the report is self-consistent — and says so in its own output.

**Tested against both failures.** The 12B report: **9 of 11 checks fail**. The 4B figures, recreated in a temp file because that report has since been overwritten: **8 of 11 fail**. Both runs correctly *pass* what they got right — the 12B's `Edge` reproduces from its own (wrong) market column, and both reports' profit targets coincidentally cap to `30` — so the tool fails what is wrong rather than failing everything.

**It also caught this session's own arithmetic.** Hand-summing the 12B's model column gave `100.28%`; the true figure is **`101.67%`**, because Aughavannon's `1.39%` belongs in the model column even though that runner is excluded from the market column. The error was the analyst's, not the report's — which is the point of having the check.

**Still open, and deliberately untouched:** the caps-versus-rank-consistency precedence collision (session 15), the appendix's falsified "in every card seen", the one-sided `pastRaces[].date` rule, 3e's missing flat verdict, and `race_type` not listing `Hurdle`.

**Outcome: pending.**

## Trade V2 — `get_available_data_context_providers` removed (2026-10-02)

**Decision (user).** The prompt no longer calls `get_available_data_context_providers()`, and it is gone from the frontmatter's `mcp_tools`. The Tools Required list is **renumbered to six items**, with every internal cross-reference moved with it: the execution call is now `#4`, the clock rule `#5`, the no-position item `#6` — including the two references carried on the no-position line itself, which the first scan missed because the line was truncated in review. The appendix row that named the tool was reworded rather than deleted (below).

**Why removing it is safe.** The call was a pre-flight check that the four contexts this installation can serve, and this prompt has not made it in the last four sessions: every run's Step 2 returned all four contexts for the whole field in one call, and the availability question is settled once per installation rather than once per market. **An unavailable context fails loudly at Step 2 anyway** — it returns empty, which the prompt already handles as a coverage finding rather than a reason to re-request anything (its own guardrail row). Removing the item aligns the instructions with the practice those runs followed.

**The one place it was load-bearing.** Appendix A's price-metric row used the tool name to warn that `MarketSelectionsPriceMetricData` was absent when the Bet variant was written and present on a later call. That warning survives intact without the tool: availability is now described as a **host-provided dependency** the prompt cannot verify for itself, and an empty response is routed to the existing Step 2 coverage rule.

**Verified after the edit:** no surviving mention of the tool or of a "provider list"; the Tools list is `1`–`6` with the data call at `2`; and all five `#N` references resolve to the right items. 408 → 407 lines, all four tables intact.

**Not regenerated:** `docs_context.txt` is produced by `scripts/ingest_docs.py` and still reflects the pre-removal text. Run that script when convenient.

**Outcome: pending.**

## New prompt — `HorsesBetfairRaceInfoDataVDWTrade.md`: VDW's analysis, a fixed selection (2026-10-02)

**Decision (user, `dec-9617241560634700`).** A new sibling of Trade V2 built on **Van der Wheil's method** (`docs/Research/HorseRacing/VanDerWheilBasePrinciples.md`), using the same four data contexts and the same trade strategy, with the handicapping engine replaced. Three answers settled its spine:

1. **The traded runner is the market's favourite, always** — VDW changes only how the probability is derived. This is a **deliberate divergence from VDW**, whose method shortlists from the forecast and judges within it, so his own pick is frequently not the favourite.
2. **A no-bet is admissible** when no runner passes VDW's qualification test. This is a **deliberate divergence from V2**, whose spine is "there is no gate and no no-bet outcome".
3. **The four contexts stay** — so VDW's class layer is proxied rather than supplied (below).

**How VDW's five layers map onto the data.** Consistency is fully available (`pastRaces[].position`, last three **completed** runs, non-completions excluded and field sizes carried as part of the reading). **Form is unusually well served** — the in-play pair is a direct instrument for VDW's true-versus-false distinction: a beaten run with a minimum of `1.02`–`2.0` was *traded as the likely winner*, which is false form pointing at the horse rather than away from it, and a near-ceiling minimum is a run with nothing in its favour. Capability is available for distance (±220 yards from the form lines) but only **today** for going and weight. The market layer is the price, with VDW's "most winners come from the first few in the betting" operationalised as a **shortlist** fixed in Step 1.

**The class layer is a stated adaptation, and the prompt says so every time it is used.** VDW judges class and ability mainly by **race type and prize money** — neither exists anywhere in these contexts. The substitute is today's `class_num`, the **`pastRaces[].price` record as a proxy** for the company kept, and the tissue's `rating`/`career` components. The appendix states what the proxy cannot see: prize money separates a 3/1 shot in a seller from a 50/1 shot in a Grade 1, and a starting price cannot — so a horse with short prices in weak company proxies as classy, and one competing respectably at a higher grade without ever being fancied proxies as the opposite. **Both errors are live and neither is detectable from these four contexts.**

**The no-bet condition, and why it is reachable.** Four operational tests — at least two of the last three completed runs top-four; at least one completed run within ±220 yards of today's trip; a real in-play record on the most recent completed run; and at least one past start inside today's shortlist band. The prompt labels them as **its own operationalisation** of VDW's judgement, since his texts give no formulae. They fire on an **all-unraced maiden card** — the log has observed 8 of 11 runners unraced — where nothing has consistency, form or capability evidence. **Where nothing in the field qualifies there is no trade; the favourite's own pass/fail is reported regardless, and the trade goes even when the favourite fails.**

**What it inherits from Trade V2.** All six arithmetic-hardening edits: the computed column totals, the printed de-vig working, the probability-sorted table with `Rank` derived from it, the direction-versus-break-even cross-check, the `EV` substitution line, and the ban on a market column that restates the model column. Plus the four contexts, the de-vig and cap rules, the price-metric trend with a **flat** verdict now explicitly allowed, and the once-per-market guarantee. `mcp_tools` carries the four tools — no `GetAvailableDataContextProviders`, consistent with the removal above.

**Unvalidated as a prompt; its report shape is tested.** This prompt has **never been run** against a live market. Its *report shape* has been: `scripts/validate_trading_report.py` was run against a synthetic VDW-shaped table — nine columns including `Qualifies`, a "VDW Report" title — and **passed all ten checks**, so the extra column and the renamed rating column parse cleanly without touching the script. That run also supplied the validator's first **passing** case after three failing ones, and confirmed it handles an **under-round** book (92.50%) as readily as an over-round one. What remains unknown is the thing no checker can see: whether VDW's layers produce better probabilities than Trade V2's handicapping on this data. That question needs live runs and settled outcomes, and every outcome in this log is still pending.

**Outcome: pending.**

## VDW Trade — session 1, Kelso `2m1f Hcap Chs` (`1.263239327`) — first live run of the new prompt

**Field.** Kelso `2m1f Hcap Chs` — **`Chase`**, going group **`Good To Firm`**, **Class 2** (the strongest grade logged in this family), **5 runners**, `totalMatched` **£61,288**. Race data `2m 1f 14y` (**3,754 yards**; tolerance 3,534–3,974) against the market name `2m1f Hcap Chs` — agreement. `status: "Open"`, all five `Active`.

**The tightest head in the series, and the only market with no tail at all.** The first three prices were `3.95`, `4.00` and `4.20` — a 6% spread across them — and **every runner was priced under `8.0`**. The shortlist fixed at Step 1 therefore contained all five runners, so VDW's forecast layer filtered nothing here; the report says so rather than claiming a filter it did not apply. On a market with no tail the "contenders against runner count" comparison is close to vacuous, which was also stated.

**All five runners qualified, so the no-bet condition did not fire.** As expected: a Class 2 handicap chase where every horse has completed runs at roughly this trip has no evidence-free runners. The qualification test's firing case — an all-unraced maiden card — did not present itself, so **the gate remains untested in the field**.

**The prompt's central divergence was demonstrated on its first run — and it is the design working, not a fault.** VDW's layers ranked **Breaking Cover** top (`rating 68.9` against the favourite's `44.0`, the field's best `course_dist` at `85.0`, **four of six runs at today's trip**, two wins and a **real** tissue value of `4.9`), while **`Ice Jet` was the market's favourite** at `3.95`. **The trade went on Ice Jet regardless**, exactly as the prompt requires: VDW supplies the probability, the market supplies the selection. In the Trade V2 series a Rank 1 that differed from the favourite was a *reporting* fact; here it is a **stated rule**, and this run is the first to exercise it.

**The sentinel appeared live, exactly where the appendix predicted.** `Ice Jet`'s `tissue_prob` is **`0.5`** with `tissue_price` **`2.0`** — the value I documented from the gemma4-12b report as looking like a cap rather than an estimate. De-vigged it produces **`56.93%`**, an artefact rather than a probability. It was carried in the de-vig as the rule requires and every layer judgement rested on the **starting-price record and the components** instead. Its practical effect here is concrete: the favourite's *class* layer lost the cross-check that would have supported it, which is part of why VDW's layers put Breaking Cover ahead.

**A bare runner for the third time in this family.** **`Tommys Oscar`** returned **no tissue and no radar** — `horseDetail` and price metrics only — so it had no vendor price, no components, no consistency statistics, and **no going component at all**. Its capability layer is therefore recorded with an explicitly **missing** test rather than a passed one, and the tissue book's `87.83%` sum is reported as conditional on the four runners that have one.

**The best head agreement this family has produced.** My figure for the favourite was `24.00%` against the market's `25.18%` — **`0.953x`**, a 4.7% gap, the closest to parity of any run. The tail flag stayed clear at **`1.206x`**, so that head figure is a **view rather than an artefact**. `Breaking Cover` at `27.00%` was my only positive-EV pick of the two — **multiple-edge count 2 of 5 (40%)**, both inside the vendor's `8`–`50` zone.

**Reachability passed, and the price had already overshot it.** The `Lay` needed a drift of **`+5.6%`** to the `4.17` break-even; the realised two-hour move was **`+17.91%`**, i.e. **3.2× the requirement**, with volume up `+474%` arriving *as the price drifted out* — the supportive side of a `Lay`. This is the first run in which the move has **exceeded** the required distance rather than merely being contained within the realised range. The `vwap`/price disagreement recurred for the third consecutive run (`vwap 3.6892` below the live `3.95`, pointing the other way), and was reported as a disagreement rather than resolved.

**The trade.** Favourite = **Ice Jet** (`73230283`) at `3.95`; mine `24.00%`; `EV = (24.00 / 100) × 3.95 − 1 = −0.0520` → **`Lay`**; the direction cross-check agreed (`p > 100 / 3.95 = 25.32%` needed for a `Back`, and `24.00%` does not clear it); break-even `4.17`, price below it. Target `min(round(5.20), 30)` = **`5`**, cap unbound.

**Executed:** `execute_strategy_settings_with_parameters(strategyName="Trade x profit", marketId="1.263239327", selectionId="73230283", parameters="{\"OpenBetPosition.BetType\":\"Lay\",\"CloseBetPosition.Profit\":5}")` → `{"status":true}`.

**Checked by the validator, not by hand — the first time in this project.** The report's table and parameters were written to a temporary file and run through `scripts/validate_trading_report.py`, which re-derived the market column from the prices (book `100.51%`), recomputed all ten `EV`/`Edge` values from their own rows, verified the rank ordering, the `Lay`, the break-even comparison and the target — and **passed all ten checks**, worst row gap `0.01pp`. The two gemma4 reports were audited by hand and the fifteen Trade V2 runs likewise; this is the first whose arithmetic was verified mechanically before being reported. The temporary file was deleted.

**VDW series (new, not pooled with Trade V2's).** The favourite-divergence figure for this run is **`0.953x`** (n=1). It is deliberately **not** added to the Trade V2 series below — a different analysis engine produces a different series, and pooling them would hide which engine is being measured. **The Trade V2 series remains at n=14**, mean `0.813x`.

**Outcome: pending.**

## VDW prompt — the class layer loses its prize-money framing (2026-10-02)

**Decision (user).** Prize money exists nowhere in this dataset, so the prompt must not mention it. **Six occurrences removed**: the Goal's adaptation note, the `pastRaces[].price` row, the 3a class-layer row, a guardrail row, and two appendix passages — including the appendix's opening claim that VDW judges class "mainly by race type and prize money".

**What was kept, deliberately.** The honesty is unchanged. The class layer is still labelled a **stated adaptation**, the prompt still requires the report to say so every time, and the appendix still spells out what the proxy cannot see — now phrased as **what the price record does not contain** rather than as a field the dataset lacks: *a starting price records what the market thought of a horse in one race; it does not record the level of that race*, so short prices in modest company read exactly like short prices in good company.

**Why this is an improvement rather than a cosmetic edit.** Naming a field the contexts do not carry gives a model a fact to reach for. The two gemma4 reports showed how readily an invented or unavailable specific gets written up as a finding — a `100.000%` column, a market column copied from the model column, an `EV` built on a probability appearing nowhere in the report. Stating the limitation in terms of **what the record does not say** is checkable against the payload; stating it in terms of prize money is not, because no reader has ever seen prize money in these four contexts.

**Verified:** no occurrence of "prize" in any form, no "seller" / "Grade 1" / "race value" framing left, `393` lines unchanged, all four tables intact. **Trade V2 was checked and never mentioned it.**

**Not rewritten:** the Kelso session 1 report above records the class layer as *"proxied from the `pastRaces[].price` record, not prize money"* — that is what that run actually said, and records in this log are not edited after the fact.

**Outcome: pending.**

## VDW prompt — re-price on the entry, not on the analysis snapshot (2026-10-02)

**Source.** `docs/Analysis/HorseRacingAIStrategyTradeResults_03-04October2026.md` — a review of **33 strategy runs** over 3–4 October (25 Trade V2, 8 VDW), reconciled against `get_all_bet_results()`. **That review answers the pending outcomes recorded throughout this log**; the per-session lines above are not being rewritten one at a time. Its headline: **+£15.50** as executed over 29 markets, **−£868.60** if the same selections had been held as bets, and **+£1,226.00** backing the model's own `Rank 1` (t = +1.27) — of which +£1,025 was simply "back the market favourite" and only £115 came from the model's nine disagreements with the market. **The AI's handicapping supplied no usable directional edge in either direction**: 32 of 33 signals were `Lay`, because the model priced the favourite below the market in **32 of 33** races — the same property this log's own divergence series has recorded since session 2 — and the favourites then won **48.3%** of the time.

**Decision (user), implementing the review's §6.B.5.** *"Re-price on the entry, not on the analysis snapshot. Compute direction and target from the price the trade is actually taken at."* The measured snapshot-to-entry gap was **4.1% on average, median 1.7%, worst 33.0%** (Cool Azul 9.40 → 12.50; Alfalfa 4.60 → 5.50) — **larger than most of the profit targets being set**, so direction and target were being computed on a price the trade never got. The prompt now makes the re-read **Step 4, mandatory and unconditional**:

- **Re-read by the same `marketId`** with `get_market`, refreshing **every** runner's price — one price source for the whole field, never a mix of snapshot and entry prices.
- **Re-derive the market column from the entry prices** and recompute the favourite's `EV`, `Edge`, direction and target from them. The report's table and trade block now carry the **entry prices**, with the snapshot and the gap stated.
- **The model probabilities are frozen.** They are a property of the horses, settled in 3a–3b from the form lines, the layers and the shortlist; re-deriving them from a new price would make the model a function of the market — the one thing 3b's independence rule forbids — and a guardrail row now says so.
- `EV_now` in the trend block is the entry price, and the new Step 4 states the principle plainly: *a direction that was correct at the snapshot price and is the opposite at the entry price is the entry price's direction.*

**The companion change the re-read forced, from the review's §6.A.3 (its highest-value section).** If the re-read is mandatory, it must answer what happens when the fixed runner is gone. The review documents exactly that: **Ice Jet was withdrawn** (`RemovedVacant`) after the recommendation at Kelso `1.263239327`, the trade had already opened (−£67.74), and the same market carries an **undocumented +£127.75 on Breaking Cover** — the largest single P&L item in the sample, produced by what appears to have been a second prompt run against the same market. The prompt therefore now has **two** admissible no-bet causes rather than one: nothing qualifies in the field, **or the fixed runner is no longer `Active`** — which **cancels** the trade. It does not reassign it, and re-running the prompt to "fix" a withdrawal is forbidden in a guardrail row, because an undocumented re-run is the one way this design places a trade nobody can see.

**What did not change.** The fixed selection rule, the model probabilities' independence, the five layers, the qualification test, the divergence caps, the `30` cap on the target, and the `min(round(|EV| × 100), 30)` formula. The old "re-run the whole table if the book moved ≥20%" bullet is retuned rather than removed: a material move is now a **finding to report**, since the re-read happens unconditionally anyway. Frontmatter tools are unchanged — `GetMarket` was already listed.

**Open, flagged, not actioned.** The review makes three recommendations this change does **not** implement, each needing the user's decision:

1. **§6.B.4 — decouple the target from `|EV| × 100`.** The target was reached in **4 of 29** settled trades; the realised exit is the scheduled close, where the median capture is **+5.95%** of risk against a mean target of **14.5**. The review offers a fixed 5–8% target or dropping the early-exit concept entirely. **This is a change to the trade, not to its pricing, so I have not made it.**
2. **§6.C.7 — decide whether the strategy is "lay the favourite" or a model pick.** As observed it is the former, with the handicapping supplying only a sign. The review's own `Rank 1` back-test is a promising sign on 30 bets, not an edge.
3. **§6.A.1–2 — the portfolio half of the control.** `scripts/validate_trading_report.py` checks one report's internal arithmetic; nothing yet checks **whether the recommended trade happened and settled as assumed**. The review found **4 recommended trades with no settled bet despite `{"status":true}`**, one double-traded market, one failed hedge and one 4% fill — **−£172.13 of tails against +£187.63 of designed profit.** It also restates that the once-per-market guarantee must be enforced **in the host**, which the prompt can only promise.

**Bookkeeping.** The review counts **8 VDW runs**; this log records **1** (Kelso). The other seven were produced outside this session and are not entered above.

**Outcome: pending.**
