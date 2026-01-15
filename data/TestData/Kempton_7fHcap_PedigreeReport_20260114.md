# Pedigree-Driven Market Report: Kempton 7f Hcap (2026-01-14)

## Market Metadata
- **Event:** Kempton
- **Market:** 7f Hcap
- **Start Time:** 2026-01-14T17:30:00Z

---

## Runner Table

| Name             | Price | Family | DosageProfile   | DI   | CD   | PedigreeBalanceScore | SpeedStaminaBucket | PedigreeConfidence | PedigreeProbabilityShare | SuggestedAction | Rationale |
|------------------|-------|--------|-----------------|------|------|---------------------|--------------------|--------------------|-------------------------|-----------------|-----------|
| Rising Force     | 2     | 19-b   | 4-0-5-1-0 (10)  | 1.86 | 0.7  | 67                  | Speed-lean         | 0.6                | 0.19                    | Ignore          | Good speed pedigree, but price is short and confidence moderate |
| Vizzavona Lady   | 5.5   | 12-b   | 0-0-4-0-0 (4)   | 1    | 0    | 44                  | Balanced           | 0.3                | 0.07                    | Ignore          | Low dosage points, low confidence |
| Up The Anti      | 10.5  | 3-o    | 3-6-13-2-0 (24) | 1.82 | 0.42 | 62                  | Speed-lean         | 1.0                | 0.18                    | Back            | Strong profile for 7f, high confidence, price not short |
| Hallowed Time    | 8.4   | 5-f    | 3-2-3-0-0 (8)   | 4.33 | 1    | 44                  | Speed-lean         | 0.6                | 0.13                    | Ignore          | DI/CD extreme, low points, confidence moderate |
| Knightmare       | 10.5  | 16-h   | 1-2-3-0-0 (6)   | 3    | 0.67 | 54                  | Speed-lean         | 0.3                | 0.08                    | Ignore          | Low dosage points, confidence low |
| The Cola Brasil  | 130   | 4-m    | 1-3-4-0-0 (8)   | 3    | 0.63 | 54                  | Speed-lean         | 0.6                | 0.13                    | Ignore          | Outsider, moderate confidence, but price reflects chance |

---

## Strategy Rules

- **If** PedigreeBalanceScore > 60 **and** PedigreeConfidence ≥ 0.8 **and** price > 6.0 **then** consider Back-to-Lay with small stake.
- **If** PedigreeConfidence < 0.5 **or** DosageProfile points ≤ 6 **then** Ignore.
- **Never** back favorites solely on pedigree.
- **Max open positions:** 1
- **Stake:** Small fixed liability (e.g., 0.5% of bank)
- **Exit:** If price drifts by 4+ ticks, or 5 min pre-off, whichever comes first.
- **Do not enter** if liquidity is poor or spread > 10%.

---

## Exclusions
- Vizzavona Lady (low dosage points)
- Knightmare (low dosage points)
- Hallowed Time (low confidence, extreme DI)
- The Cola Brasil (price reflects outsider status)
- Rising Force (price too short for edge)

---

## Actionable Trade Idea
- **Up The Anti** (10.5):
  - Back-to-Lay, small stake, exit if price drifts 4+ ticks or 5 min pre-off.
  - Rationale: High PedigreeBalanceScore, strong dosage profile, price not short, high confidence.

---

## Validation Plan
- **Data needed:** Historical markets with PedigreeDataForHorses, market prices, BSP, and results.
- **Labels:** Win, place, price move pre-off.
- **Method:** Time-based train/test split, avoid leakage. Test if high PedigreeBalanceScore + confidence yields better-than-market returns or price moves.
- **Falsification:** If no significant difference in returns or price moves for pedigree-flagged runners vs. market, edge is not real.

---

## Notes
- Pedigree signals are weak priors; do not override form, fitness, or trainer intent.
- All claims reference DI, CD, and dosage points with explicit confidence.
- Prefer “ignore” over weak signals.
