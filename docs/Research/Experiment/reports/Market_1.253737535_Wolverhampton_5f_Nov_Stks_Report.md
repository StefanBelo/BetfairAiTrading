# Pedigree Report — Market 1.253737535 (Wolverhampton — 5f Nov Stks)

Date: 2026-02-09

## Market context
- Distance: 5f (Sprint)
- Sprint target: DI_target = 3.0, CD_target = 0.6
- Weights used: w1=1.0, w2=1.5, k=1.2

## Raw market & pedigree data
| Name | Price | DI | CD | DosagePoints |
|---|---:|---:|---:|---:|
| Blue Deveron | 2.38 | 2.08 | 0.40 | 20 |
| Timescape | 2.98 | 0.60 | -0.25 | 4 |
| Alvin | 7 | 1.40 | 0.17 | 6 |
| Peregrine Falcon | 12 | 1.67 | 0.17 | 12 |
| Seven Of Hearts | 50 | 3.00 | 0.50 | 10 |

## Derived features (computed)
- DosageInfoScore mapping: ≤6 → 0.3; 7–12 → 0.6; 13–20 → 0.8; >20 → 1.0
- Speed/Stamina buckets (DI >= 2 → Speed-lean; 1–2 → Balanced; <1 → Stamina-lean)

| Name | PedigreeBalanceScore (0–100) | SpeedStaminaBucket | DosageInfoScore | PedigreeConfidence | PedigreeProbabilityShare | SuggestedAction | Rationale |
|---|---:|---|---:|---:|---:|---|---|
| Blue Deveron | 37 | Speed-lean | 0.8 | 0.8 | 0.315 | Ignore* | Close to DI target but CD short of ideal; market price too short for pedigree edge. |
| Timescape | 1 | Stamina-lean | 0.3 | 0.3 | 0.004 | Ignore | DI/CD far from sprint target; low dosage points → low confidence. |
| Alvin | 12 | Balanced | 0.3 | 0.3 | 0.039 | Ignore | Moderate DI but low dosage points → low confidence; no clear value. |
| Peregrine Falcon | 17 | Balanced | 0.6 | 0.6 | 0.107 | Ignore | Some suitability vs target; moderate confidence but market price not offering favourable entry. |
| Seven Of Hearts | 84 | Speed-lean | 0.6 | 0.6 | 0.533 | **Back (small)** ✅ | Best pedigree fit for sprint (DI=3.0); pedigree share (53%) >> market implied (~2%) — flag for small-stake back if other checks (form/fitness) allow. |

Notes:
- PedigreeProbabilityShare was computed as weight = (PedigreeBalanceScore/100) * PedigreeConfidence; weights normalized to sum to 1.0.
- *"Ignore" for Blue Deveron means: pedigree does not suggest value vs market price; not a recommended entry.

## Rules / Actionable Strategy (short)
1. If PedigreeBalanceScore ≥ 70 and PedigreeConfidence ≥ 0.6 and market price > 10, consider a small back (micro-stake) with strict risk limits. ✅
2. Exclude runners with DosageInfoScore ≤ 0.3 from pedigree-driven trades (low confidence). ⚠️
3. Max open pedigree-driven positions: 1; stake small (e.g., fixed liability €10 or flat stake €5) and cap exposure per day. 💸
4. Exit rules: cash out / hedge if price shortens by 50% (i.e., odds halve) or if price moves 5+ ticks against; otherwise hold until N minutes pre-off. 🔧

## Recommended trade idea (0–1 picks)
- Seven Of Hearts — Suggested: Back small (example: €5–€10 stake) **only** if manual checks (form, jockey, course, non-pedigree signals) show no clear disqualifying factor. Rationale: strong pedigree fit (DI=3.0, high balance score) and moderate confidence; pedigree-implied share (53%) greatly exceeds market-implied (≈2%), indicating possible value.

## Exclusions
- Timescape, Alvin: exclude due to low dosage points (≤6) and low confidence.
- Blue Deveron, Peregrine Falcon: not recommended despite some pedigree suitability because market prices are short or do not offer value.

## Validation / Backtest blueprint
1. Collect races classified as "Sprint (<7f)" and compute PedigreeBalanceScore + PedigreeConfidence per runner historically.
2. Track outcomes for top N pedigree-ranked runners and measure ROI when backing at market prices available pre-off.
3. Evaluate whether selecting runners with (PedigreeBalanceScore ≥ 70 and PedigreeConfidence ≥ 0.6) yields positive edge after transaction costs and staking.

---

*Report generated automatically by The Expert Horse Racing Pedigree Analyst workflow.*
