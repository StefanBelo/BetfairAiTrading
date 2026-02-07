# Pedigree Analyst Report — Market 1.253024156

Market: Southwell — 7f Hcap — Start 2026-01-23T19:15:00+01:00

Method: Ran `TheExpertHorseRacingPedigreeAnalyst.md` for distance 7f (Sprint/Mile). Targets: DI_target=1.5, CD_target=0.3; w1=1.0, w2=1.5, k=1.2.

## Runners

- Golden Pharaoh — Price 4.7 — Family 3-l — Dosage: 3-3-14-2-0 (22) — DI 1.44 — CD 0.32
  - PedigreeBalanceScore: 89.9 — SpeedStaminaBucket: Balanced — PedigreeConfidence: 1.00 — PedigreeProbabilityShare: 37.6% — SuggestedAction: Ignore
  - Rationale: Strong DI/CD fit and high dosage points, but already short-priced (4.7) — monitor for back-to-lay only.

- Tennessee Gold — Price 5.2 — Family 19-b — Dosage: 3-5-2-0-0 (10) — DI 9 — CD 1.10
  - PedigreeBalanceScore: 1.1 — SpeedStaminaBucket: Speed-lean — PedigreeConfidence: 0.60 — PedigreeProbabilityShare: 0.3% — SuggestedAction: Ignore
  - Rationale: Extremely high DI / CD mismatch for 7f; pedigree signal very weak for distance.

- Down To The Kid — Price 4.3 — Family 1-u — Dosage: 2-1-7-2-0 (12) — DI 1.18 — CD 0.25
  - PedigreeBalanceScore: 60.4 — SpeedStaminaBucket: Balanced — PedigreeConfidence: 0.60 — PedigreeProbabilityShare: 15.2% — SuggestedAction: Ignore
  - Rationale: Reasonable pedigree fit but already short-priced (4.3); not a clean pre-off micro-edge.

- Commander Of Life — Price 7.0 — Family 6-e — Dosage: 2-3-5-0-0 (10) — DI 3.00 — CD 0.70
  - PedigreeBalanceScore: 14.7 — SpeedStaminaBucket: Speed-lean — PedigreeConfidence: 0.60 — PedigreeProbabilityShare: 3.7% — SuggestedAction: Ignore
  - Rationale: DI/CD both lean faster than 7f target; low pedigree fit.

- Chola Empire — Price 16.5 — Family 12-c — Dosage: 1-2-5-0-0 (8) — DI 2.20 — CD 0.50
  - PedigreeBalanceScore: 36.0 — SpeedStaminaBucket: Balanced (slight speed) — PedigreeConfidence: 0.60 — PedigreeProbabilityShare: 9.0% — SuggestedAction: Back (micro)
  - Rationale: Moderate DI/CD fit and long price → cautious micro-back candidate (low confidence).

- Mumayaz — Price 17.5 — Family 23-b — Dosage: 4-10-8-0-0 (22) — DI 4.50 — CD 0.82
  - PedigreeBalanceScore: 5.9 — SpeedStaminaBucket: Speed-lean — PedigreeConfidence: 1.00 — PedigreeProbabilityShare: 2.5% — SuggestedAction: Ignore
  - Rationale: Good dosage points but DI/CD too speedy for 7f target; pedigree does not support value.

- Silver Samurai — Price 17.5 — Family 9-e — Dosage: 1-2-11-0-0 (14) — DI 1.55 — CD 0.29
  - PedigreeBalanceScore: 92.8 — SpeedStaminaBucket: Balanced — PedigreeConfidence: 0.80 — PedigreeProbabilityShare: 31.1% — SuggestedAction: Back (micro)
  - Rationale: Best DI/CD fit and strong normalized pedigree share vs long price — primary value candidate.

- Giorgio M — Price 20.0 — Family 23 — Dosage: 5-5-4-0-0 (14) — DI 6.00 — CD 1.07
  - PedigreeBalanceScore: 2.3 — SpeedStaminaBucket: Speed-lean — PedigreeConfidence: 0.80 — PedigreeProbabilityShare: 0.8% — SuggestedAction: Ignore
  - Rationale: DI/CD strongly speed-tilted and very poor distance fit.

## Trade Ideas (conservative)

1. Primary: Back Silver Samurai (small fixed liability). Entry: back pre-off at available price (~17.5) or better. Target/Exit: lay to lock ~20% net profit OR exit if price moves adversely by ≥30% or by 2 ticks. Stake: micro (example max liability £10). Max open positions: 2.

2. Secondary: Micro-back Chola Empire only if liquidity exists and not conflicting with primary. Entry: ~16.5. Target: lay ~13–14 for ~15–20% realized. Stake: very small (example max liability £5).

No-Trade: skip if spread > 2 ticks or insufficient matched liquidity.

## Exclusions
Ignore: Tennessee Gold, Giorgio M, Commander Of Life, Mumayaz, Down To The Kid, Golden Pharaoh (no immediate trade) — reasons: DI/CD mismatch, low pedigree fit, or already too short-priced.

## If‑Then Rules (short)
- If PedigreeBalanceScore ≥ 85 AND PedigreeConfidence ≥ 0.6 AND price ≥ 12 → consider micro-back with strict stake cap and lay-to-lock profit target.
- If pedigree share ≥ 2× market-implied probability AND confidence ≥ 0.6 AND liquidity OK → consider trade.
- If PedigreeConfidence ≤ 0.3 or parse error or missing → exclude.
- Stop on adverse move ≥ 30% or 2 ticks; exit no later than 10 minutes pre-off.

## Validation / Backtest Blueprint
- Compute PedigreeProbabilityShare per race; compare to realized win frequency by decile over 7f–8f historical races.
- Run P&L simulation with micro-stakes and above exit rules; measure ROI, hit rate, calibration, and liquidity impact.

*Notes*: No missing or malformed pedigree fields in this market. Pedigree is a secondary prior; suggestions are conservative micro-stakes only.
