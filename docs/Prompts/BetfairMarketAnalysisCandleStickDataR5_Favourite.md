# Betfair Market Analysis – Candlestick Data R5 (Favourite Betting Edition)

Purpose: Evaluate the favourite using probability + price pressure (BLR) to choose one simple betting action:
- "Bet 10 Euro" (Back) when ## 12. Change Log (Compared to R4)
- Removed multi-strategy matrix → binary Bet/Lay/No Bet decision.
- Fixed stake 10 Euro; removed parameter JSON generation.
- **REMOVED confirmation step** → automated immediate execution.
- Clarified opposition role: supportive filter not always mandatory for Back, required for Lay.
- Added explicit decision matrix and automated execution flow.vourite shows strengthening win probability.
- "Lay 10 Euro" when the favourite shows credible weakening (drift) and losing pressure.
- Otherwise: "No Bet" (insufficient edge / conflicting signals).

This replaces the multi‑strategy trading matrix from R4 with a binary betting decision model.

---
## 1. Data Inputs
1. GetActiveBetfairMarket
2. GetDataContextForBetfairMarket("MarketSelectionsCandleStickData") → provides candleStickData[] per selection and backLayRatio (BLR = backVolume/(backVolume+layVolume)).

---
## 2. Core Calculations
For each selection (focus on favourite = lowest current price):
- Probability% = (1 / Price) * 100
- Open_Price = Open price from FIRST candle in series
- Current_Price = Close price from LAST candle in series  
- Open_Prob = (1 / Open_Price) * 100
- Current_Prob = (1 / Current_Price) * 100
- **Prob_Change_pp = Current_Prob − Open_Prob (percentage points)**
- **CRITICAL: Positive = Strengthening (price shortening), Negative = Weakening (price drifting)**
- Minutes = time span covered / 60 seconds
- Prob_Velocity = Prob_Change_pp / Minutes (pp/min)
- Prob_Volatility% = (High_Prob − Low_Prob)/Current_Prob * 100
- BLR Bands: StrongB ≥0.60 | ModB 0.55–0.60 | Neutral 0.45–0.55 | ModL 0.40–0.45 | StrongL <0.40
- Direction Tag: STEAM (Prob_Change_pp > 0 = strengthening), DRIFT (Prob_Change_pp < 0 = weakening), STABLE (abs(Prob_Change_pp) < 0.2pp)

---
## 3. Opposition Weighting
For favourite only:
- OppWeight = Sum(Current_Prob of all other runners) / (Total Current_Prob including favourite) = 1 − FavouriteProbShare.
- Opposition Confirmed if OppWeight ≥ 0.50 (≥50%).

Usage:
- Back Decision (Bet 10 Euro) prefers OppWeight < 85% (not overwhelmingly against favourite).
- Lay Decision (Lay 10 Euro) requires OppWeight ≥ 50% (market breadth supports weakness).

---
## 4. Betting Decision Rules
PRIMARY EDGE FILTERS (must all pass for any bet):
- Favourite price 1.5–12.0 (practical betting range) - OPTIMIZED range.
- Prob_Volatility% ≤ 50% (else No Bet → HighVol flag) unless strong alignment.
- No severe divergence unless Lay opportunity with strong drift (Prob_Change_pp < -1.0 & BLR < 0.48).

DECISION MATRIX:
1. **Bet 10 Euro (Back)**
   - Prob_Change_pp ≥ +0.4pp (strengthening - price shortening)
   - BLR ≥ 0.55 (moderate backing pressure - LOWERED from 0.6)
   - OppWeight < 85% (not overwhelming opposition)

2. **Lay 10 Euro**
   - Prob_Change_pp ≤ -0.4pp (weakening - price drifting OUT)
   - BLR < 0.5 (laying pressure)
   - OppWeight ≥ 50% (market breadth confirms)

3. **No Bet**
   - Any unmet criteria above
   - Conflicting signals (|Prob_Change_pp| < 0.4pp AND BLR 0.5-0.6)

**CRITICAL LOGIC:**
- **BACK** when favourite is strengthening (price shortening, prob increasing)
- **LAY** when favourite is weakening (price drifting, prob decreasing)
- **Never LAY a strengthening favourite** regardless of BLR
- **Probability change momentum takes precedence over BLR when both are positive**

Edge Amplifiers (log only):
- Strong Steam: Prob_Change_pp ≥ +0.8 & BLR ≥ 0.56 - LOWERED thresholds
- Strong Drift: Prob_Change_pp ≤ -0.8 & BLR ≤ 0.44 - ADJUSTED thresholds

---
## 5. Output Content
Modes (choose as required): Summary | Table | Execution | Deep Dive | Flags | **Diagnostic**.
Required always for decision: A concise Execution Block.

**DIAGNOSTIC MODE** (when no bet triggers):
```
### Diagnostic Report - Why No Bet Triggered

**PRIMARY FILTERS CHECK:**
- Price Range: [PASS/FAIL] - £X.XX (need 1.5-12.0)  
- Volatility: [PASS/FAIL] - X% (need ≤50%)

**BACK BET CRITERIA:**
- Prob Change: [PASS/FAIL] - +X.Xpp (need ≥+0.4pp STRENGTHENING)
- BLR Level: [PASS/FAIL] - X.XX (need ≥0.55 - RELAXED)
- Opposition: [PASS/FAIL] - X% (reject if ≥85%)

**LAY BET CRITERIA:**
- Prob Change: [PASS/FAIL] - -X.Xpp (need ≤-0.4pp WEAKENING)
- BLR Level: [PASS/FAIL] - X.XX (need <0.5)
- Opposition: [PASS/FAIL] - X% (need ≥50%)

**PROBABILITY DIRECTION CHECK:**
- Favourite Direction: [STRENGTHENING/WEAKENING/STABLE]
- Price Movement: [SHORTENING/DRIFTING/STABLE]
- Logic Check: Only LAY when WEAKENING, only BACK when STRENGTHENING

**CLOSEST TO TRIGGERING**: [Back/Lay/Neither] - [what needs to change]
```

Table Columns (favourite first, may include top 2 opponents for context):
| Selection | Price | Prob% | Open% | ProbΔ (pp) | Velocity | Vol% | BLR | BPI% | Direction | Alignment | OppWeight (fav only) |

Flags: HighVol (>50%), Divergent, WeakOpposition (<50% for Lay), OverOpposition (≥85% for Back), LowBLR (<0.55 for Back).

---
## 6. Execution Flow (Automated)
A. Produce analysis + decision recommendation (Bet 10 Euro / Lay 10 Euro / No Bet).
B. If decision = No Bet → state reason(s) list and stop (no execution).
C. If Bet or Lay → execute the action immediately using the specified strategy.
D. Log JSON outcome immediately after execution.
E. Record in log which checks passed/failed.

Idempotency: Do not execute twice for same (marketId, selectionId, Decision) within session; respond ALREADY_EXECUTED if repeated.

---
## 7. Stake & Risk Model (Simplified)
- Fixed Stake: 10 Euro (no scaling) for both Bet 10 Euro and Lay 10 Euro.
- No dynamic profit/loss parameter JSON required.
- Just log executed action and key metrics (ProbΔ, BLR, OppWeight, timestamp).

Execution Log JSON Example:
```json
{
  "event": "EXECUTED",
  "decision": "Lay 10 Euro",
  "marketId": "<id>",
  "selectionId": "<id>",
  "selection": "<Name>",
  "price": <price>,
  "probDelta_pp": <value>,
  "BLR": <value>,
  "OppWeight": <value>,
  "alignment": "Aligned"
}
```
Cancellation Example:
```json
{
  "event": "CANCELLED",
  "decision": "Bet 10 Euro",
  "reason": "Execution failed",
  "marketId": "<id>",
  "selectionId": "<id>"
}
```

---
## 8. Execution Report Template
````markdown
## Favourite Betting Decision

**FAVOURITE**: <Selection> @ <Price>
Prob% <Current_Prob>% | Open% <Open_Prob>% | ProbΔ <Prob_Change_pp> pp | Velocity <Prob_Velocity> pp/min
BLR <BLR> (BPI <BPI%>%) | Alignment <Aligned/Neutral/Divergent> | OppWeight <OppWeight*100>%
Volatility <Prob_Volatility%>% | Direction <STEAM/DRIFT/STABLE>

**DECISION**: <Bet 10 Euro | Lay 10 Euro | No Bet>
**RATIONALE**: <bullet list of key supporting factors or reasons for No Bet>

<If Bet or Lay - Execute Immediately>
**EXECUTION**: Strategy executed automatically using "Bet 10 Euro" or "Lay 10 Euro" strategy
**STATUS**: <Execution result from API call>

### Context Table
| Selection | Price | Prob% | Open% | ProbΔ | Vel | Vol% | BLR | BPI% | Dir | Align |
|-----------|-------|-------|-------|------:|----:|-----:|-----|-----:|-----|-------|
| Favourite | ...   | ...   | ...   | ...   | ... | ...  | ... | ...  | ... | ...   |
| Opp A     | ...   | ...   | ...   | ...   | ... | ...  | ... | ...  | ... | ...   |
| Opp B     | ...   | ...   | ...   | ...   | ... | ...  | ... | ...  | ... | ...   |

### Flags
- <List any triggered flags>
````

---
## 9. API (Automated)
- ExecuteBetDecision(marketId, selectionId, action) where action ∈ {"BACK_10", "LAY_10"}
- Execute immediately upon decision without confirmation
- Use "Bet 10 Euro" or "Lay 10 Euro" strategy names

Flow:
1. Analysis → Decision
2. If Bet/Lay → Execute immediately using ExecuteBetDecision
3. Log JSON result immediately
4. If execution fails → Log failure reason

---
## 10. Quality & Governance
Before issuing Bet/Lay:
- Price within practical range (1.5-12.0) ✓
- OppWeight threshold (per rule) ✓
- **Probability direction logic check (BACK=strengthening, LAY=weakening)** ✓
- BLR directional alignment ✓
- Volatility acceptable (≤50%) ✓
- **CRITICAL: Never LAY a strengthening favourite** ✓
Record in log which checks passed/failed.

---
## 11. Usage
Use in live pre‑race monitoring loop focused on favourite. Outputs a clear binary wagering recommendation with transparent criteria and immediate automated execution. If environment lacks live execution, still produce decision & explanation.

---
## 12. Parameter Adjustments for Increased Bet Triggering

**OPTIMIZED THRESHOLDS (September 2025):**
- **Price Range**: 1.5-12.0 (practical betting range) - focused on tradeable odds
- **Volatility Limit**: 50% - accepts dynamic markets while avoiding chaos
- **Probability Change**: ±0.4pp - triggers on meaningful movements
- **BLR Thresholds**: Back ≥0.55 (RELAXED), Lay <0.5 - momentum-focused approach
- **Opposition Weight**: Back reject ≥85%, Lay require ≥50% - balanced approach
- **Execution**: **Immediate automated** (no confirmation delays)

**MOMENTUM-FIRST PHILOSOPHY:**
- **Primary**: Strong probability change momentum (±0.4pp+)
- **Secondary**: BLR confirmation (relaxed to 0.55 for BACK)
- **Tertiary**: Opposition weight validation

**RATIONALE**: Optimized for practical trading with momentum-first approach. Analysis of Lingfield 5f Hcap showed that strong probability change momentum (Glamorous Breeze +13.46pp) can be more predictive than moderate BLR. Relaxed BLR threshold from 0.6 to 0.55 to capture more momentum-driven opportunities while maintaining risk control through price ranges and opposition weight limits.

---
## 13. Change Log (Compared to R4)
- Removed multi-strategy matrix → binary Bet/Lay/No Bet decision.
- Fixed stake 10 Euro; removed parameter JSON generation.
- **REMOVED confirmation step** → automated immediate execution.
- Clarified opposition role: supportive filter not always mandatory for Back, required for Lay.
- Added explicit decision matrix and automated execution flow.

End of R5 Favourite Betting Prompt.
