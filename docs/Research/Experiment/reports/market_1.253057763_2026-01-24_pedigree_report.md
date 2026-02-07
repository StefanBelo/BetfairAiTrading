# Pedigree Analyst Report — Market 1.253057763

Market: Newcastle — 5f Hcap — Start 2026-01-24T18:35:00+01:00

Method: Ran `TheExpertHorseRacingPedigreeAnalyst.md` for distance 5f (Sprint). Targets: DI_target=3.0, CD_target=0.8; w1=1.0, w2=1.5, k=1.2.

## Pedigree Table

| name | price | Family | DosageProfile (pts) | DI | CD | PedigreeBalanceScore | SpeedStaminaBucket | PedigreeConfidence | PedigreeProbabilityShare | SuggestedAction | Rationale |
|---|---:|---|---:|---:|---:|---:|---|---:|---:|---|---|
| Germanic | 3.65 | 4-m | 1-0-4-3-0 (8) | 0.60 | -0.13 | 1.2 | Stamina-lean | 0.60 | 2.3% | Ignore | DI 0.6 & CD -0.13 far below sprint targets; moderate points but poor fit. |
| Quinault | 3.60 | 1-o | 1-4-8-1-0 (14) | 1.80 | 0.36 | 18.7 | Balanced | 0.80 | 48.7% | Ignore | DI 1.8/CD 0.36 below sprint target (3.0/0.8); best relative fit but not strong enough for micro‑edge. |
| Carbine Harvester | 5.00 | 4-n | 0-3-7-2-0 (12) | 1.18 | 0.08 | 5.5 | Balanced | 0.60 | 10.6% | Ignore | DI 1.18 & CD 0.08 show weak sprint fit despite moderate points. |
| Paddys Day | 6.40 | 20-a | 2-1-2-1-0 (6) | 2.00 | 0.67 | 39.3 | Speed-lean | 0.30 | 38.3% | Ignore (low confidence) | DI 2.0 & CD 0.67 near sprint target but very low dosage points → confidence penalized. |
| Equality | 11.50 | 4-k | 0-1-1-0-4 (6) | 0.33 | -1.17 | 0.1 | Stamina-lean | 0.30 | 0.06% | Ignore | DI 0.33 & CD -1.17 indicate strong stamina bias; no pedigree support for sprint. |

Notes on computation:
- Distance = 5f → Sprint target DI_target = 3.0, CD_target = 0.8.
- PedigreeBalanceScore = 100 * exp(-k * d) with d = w1*d_DI + w2*d_CD, d_DI = |log2(DI)-log2(DI_target)|.
- DosageInfoScore mapping: ≤6→0.3, 7–12→0.6, 13–20→0.8, >20→1.0.
- PedigreeConfidence = DosageInfoScore (penalize very low points by lower confidence).
- PedigreeProbabilityShare = normalized weight where weight = (PedigreeBalanceScore/100) * PedigreeConfidence.

## Trade Ideas
- No micro‑edge trades recommended. None meet the conservative threshold (PedigreeBalanceScore ≥ 85 and PedigreeConfidence ≥ 0.6 and price ≥ 12).
- `Paddys Day` shows a relatively high pedigree share but PedigreeConfidence = 0.3 (points=6) → exclude from trading, prefer research/backtest.

## Exclusions
- Exclude any runner with PedigreeConfidence ≤ 0.3 from live trade decisions (here: `Paddys Day`, `Equality` flagged low confidence).

## If‑Then Rules (short)
- If PedigreeBalanceScore ≥ 85 AND PedigreeConfidence ≥ 0.6 AND price ≥ 12 → consider micro-back with strict stake cap and lay-to-lock profit.
- If PedigreeConfidence ≤ 0.3 → exclude pedigree signal from trade decisions.
- Do not enter if spread > 2 ticks or available liquidity insufficient; cap max open pedigree trades to 2 and use micro liabilities.

## Validation / Backtest Blueprint (brief)
- Compute PedigreeProbabilityShare per race and compare to realized outcomes by decile for 5f–6f races.
- Simulate micro-stake P&L with stop/exit rules; track ROI, hit rate, calibration, and liquidity effects.

*Report saved.*
