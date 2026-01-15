# Pedigree-Driven Market Report: Kempton 1m Hcap (2026-01-14)

## Market Metadata
- **Event:** Kempton
- **Market:** 1m Hcap
- **Start Time:** 2026-01-14T18:00:00Z

---

## Method & Parameters
- **Distance target:** Mile (1m) → DI_target = 1.0, CD_target = 0.0
- **Scoring:** w1=1.0, w2=1.5, k=1.2
- **d_DI =** |log2(DI) - log2(DI_target)|; **d_CD =** |CD - CD_target|
- **PedigreeBalanceScore =** 100 * exp(-k * (w1*d_DI + w2*d_CD)) (rounded)
- **DosageInfoScore mapping:** ≤6 → 0.3; 7–12 → 0.6; 13–20 → 0.8; >20 → 1.0
- **PedigreeConfidence =** DosageInfoScore (0 if missing/parse error)
- **PedigreeProbabilityShare:** weight = (PedigreeBalanceScore/100)*PedigreeConfidence; normalized across field

---

## Runner Table

| Name             | Price | Family | DosageProfile   | DI   | CD   | PedigreeBalanceScore | SpeedStaminaBucket | PedigreeConfidence | PedigreeProbabilityShare | SuggestedAction | Rationale |
|------------------|-------:|--------|-----------------|------:|------:|---------------------:|--------------------|--------------------:|------------------------:|-----------------|-----------|
| Homme De Fer     | 3.35  | 5-i    | 2-5-12-1-0 (20) | 1.86 | 0.40 | 17                   | Speed-lean         | 0.8                | 0.074                   | Ignore          | DI 1.86, CD 0.40 → speed bias, score low and price too short |
| Angelica K       | 4.3   | 8-h    | 4-3-11-2-0 (20) | 1.67 | 0.45 | 18                   | Speed-lean         | 0.8                | 0.078                   | Ignore          | DI 1.67, CD 0.45 → speed bias, short price, moderate confidence |
| Whiskey Sunrise  | 7     | 3-c    | 2-4-6-4-0 (16)  | 1.29 | 0.25 | 41                   | Speed-lean         | 0.8                | 0.177                   | Back            | DI/CD reasonably close to mile target, good points (16) → decent prior |
| Classic Speed    | 8.2   | 19-b   | 3-5-10-0-0 (18) | 2.60 | 0.61 | 6                    | Speed-lean         | 0.8                | 0.026                   | Ignore          | DI 2.6, CD 0.61 → far from mile target; very low score |
| First Encounter  | 9.4   | 9-c    | 0-2-2-0-0 (4)   | 3.00 | 0.50 | 6                    | Speed-lean         | 0.3                | 0.010                   | Exclude         | Very low dosage points (4) → confidence too low for pedigree-driven trade |
| Marchetti        | 27    | 9-b    | 3-1-14-10-0 (28)| 0.65 | -0.11| 39                   | Stamina-lean       | 1.0                | 0.211                   | Back            | DI 0.65, CD -0.11 → close to mile balance (lean stamina) with high points |
| Adace            | 34    | 14-b   | 0-1-1-0-0 (2)   | 3.00 | 0.50 | 6                    | Speed-lean         | 0.3                | 0.010                   | Exclude         | Dosage points 2 → pedigree info too weak |
| Pave The Way     | 48    | 13-e   | 1-3-4-0-0 (8)   | 3.00 | 0.63 | 5                    | Speed-lean         | 0.6                | 0.016                   | Ignore          | DI/CD far from target; modest points but low score |
| Cherry Hill      | 100   | 9      | 3-0-14-10-1 (28)| 0.56 | -0.21| 25                   | Stamina-lean       | 1.0                | 0.135                   | Ignore          | Strong points (28) and leaning stamina; price reflects outsider status |
| Weston Court     | 140   | 19-b   | 1-3-7-3-0 (14)  | 1.15 | 0.14 | 61                   | Balanced           | 0.8                | 0.264                   | Back            | DI 1.15, CD 0.14 → closest to mile target, high balance score and good confidence |
| Mister Knockout   | 380   | 10-b   | 0-0-0-0-0 (0)   | Inf  | Inf  | —                    | ParseError         | 0.0                | 0.000                   | Exclude         | Malformed pedigree (DI/CD infinite or missing) → exclude from pedigree use |

> Notes: Scores rounded; `PedigreeProbabilityShare` normalized across runners with usable pedigree. "Exclude" means do not use pedigree signals for decision-making.

---

## Short Rules (If → Then) ✅
1. **If** PedigreeBalanceScore ≥ 60 and PedigreeConfidence ≥ 0.8 and price ≥ 6.0 **then** consider a small Back-to-Lay trade.  
2. **If** PedigreeConfidence ≤ 0.3 or Dosage points ≤ 6 **then** **exclude** runner from pedigree-driven trades.  
3. **Never** back favorites (price < 4.0) on pedigree alone.  
4. **Risk:** max 1 open pedigree-based position; fixed small liability (e.g., 0.5% bank).  
5. **Exit:** exit if price drifts by 4+ ticks, or no later than 5 min pre-off; do not enter with thin liquidity.

---

## Suggested Action (0–3 micro-edges) 💡
- Primary idea: **Back Weston Court (140)** small stake (back-to-lay if lay liquidity exists), strict exit rules. Rationale: highest PedigreeBalanceScore (61) and good confidence (0.8); price is long.  
- Secondary: **Back Whiskey Sunrise (7)** small stake (price not short, score 41, confidence 0.8).  
- Tertiary (smaller): **Back Marchetti (27)** as a cautious candidate — high confidence (1.0) and moderate score (39).  
- If liquidity or spread is insufficient, **do not** enter.

---

## Exclusions (do not use pedigree to trade)
- **Mister Knockout** — parse error / malformed pedigree (PedigreeConfidence = 0).  
- **First Encounter**, **Adace** — Dosage points ≤ 4 → PedigreeConfidence too low.

---

## Validation Plan 🔬
- **Required data:** Historical markets with PedigreeDataForHorses, pre-off market prices, traded volume/liquidity, and race outcomes (win/place) plus BSP.  
- **Labels:** Win (1), Place (optional), and price move / best starting price (pre-off move).  
- **Test:** Time-based train/test split (no leakage), evaluate whether backing runners meeting rules (e.g., PBS ≥ 60 & confidence ≥ 0.8) produces economically significant returns vs. random/market benchmark. Include liquidity and transaction costs.  
- **Falsifiers:** If the strategy does not produce profit after costs, or if pedigree-flagged runners do not outperform similar-priced peers on price moves or outcomes, the pedigree edge is not supported.

---

## Closing Notes
- Pedigree is a weak-to-moderate prior; do not let it override form/trainer/pace signals.  
- All claims above reference specific DI, CD, and dosage points; decisions are conservative and limited to small-stake micro-edges.

Generated by automation following `TheExpertHorseRacingPedigreeAnalyst` workflow.
