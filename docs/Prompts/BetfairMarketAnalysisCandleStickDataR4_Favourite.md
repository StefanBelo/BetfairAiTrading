# Betfair Market Analysis – Candlestick Data R4 (Optimized)

Goal: Probability-first market evaluation + pressure (BLR) to decide IF and WHICH strategy to execute on the favorite ONLY when weighted opposition confirms.

NEW (Auto Execution with Confirmation): When a single selection (the favorite) fully meets all execution criteria (sections 5–8 & common preconditions) produce a confirmation block. Only after explicit confirmation token is supplied (see Section 14) should the strategy execution API be called. No execution attempts if: opposition <50%, odds range invalid, signal ambiguity (two signals within 10% ProbΔ), low data (<10 candles), or pressure divergent unless Value signal.

## 1. Data Fetch
1. GetActiveMarket
2. GetDataContextForMarket("MarketSelectionsCandleStickData") → includes: candleStickData[], backLayRatio = backVolume/(backVolume+layVolume)

## 2. Core Formulas
Probability = (1/Price)*100
Weighted_Price = (High+Low+Close)/3
Current_Prob = (1/Weighted_Price)*100
Open_Prob = (1/Open_Price)*100
Prob_Change (pp) = Current_Prob - Open_Prob
Prob_Velocity = Prob_Change / Minutes
Prob_Volatility = (High_Prob - Low_Prob)/Current_Prob * 100
BLR bands: StrongB ≥0.60 | ModB 0.55–0.60 | Neutral 0.45–0.55 | ModL 0.40–0.45 | StrongL <0.40
BPI = ((BLR - 0.50)/0.50)*100
Align: (Prob_Change>0 & BLR>0.52) or (Prob_Change<0 & BLR<0.48); Divergent: opposite; else Neutral.

Signals (base): STEAM Prob_Change>0, DRIFT Prob_Change<0.

## 3. Metrics Per Selection
Current_Prob | Prob_Change | Prob_Velocity | Prob_Volatility | Direction (Steam/Drift/Stable) | BLR | BPI | Pressure (Aligned/Divergent/Neutral) | Volume trend | Support/Resistance position.

## 4. Signal Classification
Strong Steam: max +Prob_Change & BLR≥0.58
Strong Drift: min Prob_Change & BLR≤0.42
Moderate Steam: +Prob_Change & 0.55–<0.58 OR missed strong BLR
Moderate Drift: −Prob_Change & 0.42–<0.45 OR missed strong BLR
Breakout Steam: Prob breaks resistance & VolTrend>+8% & Tech confirm & BLR≥0.60
False Favorite: Favorite with largest negative Prob_Change & BLR≤0.45
Value: Large |Prob_Change| with Neutral BLR (0.45–0.55) OR Divergence (mean reversion angle)
Divergence Flag: Steam & BLR<0.48 OR Drift & BLR>0.52

## 5. Favorite & Opposition Logic
Favorite = lowest odds.
Weight = Current_Prob / Sum(others Current_Prob).
Pressure boost: BLR≥0.60 or ≤0.40 ⇒ weight*1.25.
Divergence penalty: Divergent ⇒ weight*0.8.
Opposition passes if >50% weighted probability mass opposes favorite’s direction.
Execute ONLY if opposition passes + strategy criteria.

## 6. Strategy Matrix
Quoted strategy names (use exactly the quoted human‑readable form). Manual strategies never produce parameter JSON; Auto ones must.

```markdown
| Signal Class      | Odds Range | Strategy Name                | Mode   | Auto Params |
|-------------------|-----------|------------------------------|--------|-------------|
| Steam             | 1.5–15    | "Steam momentum trading"     | Manual | No          |
| Drift             | 1.8–50    | "Drift momentum trading"     | Manual | No          |
| Breakout Steam    | 1.2–8     | "Back scalping strategy"     | Auto   | Yes         |
| Value             | 2–20      | "Back hedge strategy"        | Auto   | Yes         |
| False Favorite    | 1.5–6     | "Lay fade strategy"          | Auto   | Yes         |
| Moderate Steam    | 2–12      | "Conservative back strategy" | Auto   | Yes         |
| Moderate Drift    | 2.5–25    | "Conservative lay strategy"  | Auto   | Yes         |
| Technical Steam   | 1.8–10    | "Technical back strategy"    | Auto   | Yes         |
```

Notes:
- Use ONLY the quoted names above when referencing or logging strategy selection.
- If multiple qualify: prefer an Auto strategy unless a Manual signal has >25% stronger absolute ProbΔ.
- "Auto Params" = must output parameter JSON per section 7 (stake, profit/loss, pressure info).
- Future additions must follow this quoted descriptive style and be added here.

Manual vs Auto Rule:
- (Manual) strategies NEVER send parameter objects. No dynamic stake, no JSON params. Execution is discretionary or via basic strategy trigger only.
- (Auto) strategies MUST return parameter JSON (see section 7) including stake (>=2.0), profit/loss targets, pressure info.
- If multiple signals qualify, prefer Auto over Manual unless Manual signal strength superior by >25% ProbΔ differential.

Common Preconditions: all selections scanned, opposition confirmed, odds in range, time >3m back or >2m lay, liquidity ok, no adverse volume anomaly, pressure not strongly divergent unless Value.

## 7. Parameters (Dynamic)
stakePercent = 1.0 * riskMultiplier(volatility) * timeMultiplier(minutes) * pressureMultiplier.
MINIMUM STAKE FLOOR: 2.00 (currency units). When calculating stake: baseStake * stakePercent → stake; if stake < 2.0 then stake = 2.0.
riskMultiplier: vol>20→0.7 ; vol<10→1.3 ; else 1.0.
timeMultiplier: >5m 1.0 ; >3m 0.8 ; else 0.6.
pressureMultiplier: strong aligned 1.15 ; divergent 0.75 ; else 1.0.
Return JSON + CloseBetPosition.Profit (odds<3→4 else <8→3 else 2), CloseBetPosition.Loss (vol>15→25 else 30), PressureInfo (BLR,BPI), OpenBetPosition.Stake (>=2.0).
Example: baseStake=10, vol=22 (risk=0.7), minutes=4 (time=0.8), aligned strong (pressure=1.15) → stake = 10*0.7*0.8*1.15 = 6.44 (>=2 so unchanged). If computed 1.6 → use 2.0.

Applicability:
- Only produce this parameter JSON for strategies labeled (Auto) in section 6.
- If selected strategy is (Manual): output a note: "Manual strategy – no parameters generated" and omit the JSON block entirely.

## 8. Risk & Exits (Condensed)
Steam Back: init stop = entry*1.2, trail 15%, exit: trail hit | volume -50% peak | 2 interval velocity reversal | <2m.
Drift Lay: init stop = entry*0.8, trail 20%, exit: trail | volume +30% (steam emerging) | velocity reversal to increase | strong steam signal.
Vol>20 widen (25–30%); Vol<10 tighten (10–15%). Tighten stops: -25% at ≤10m, -50% at <5m; flat all <2m.

## 9. Output Modes
Summary: top 3, key insights, opposition verdict, pressure snapshot.
Table: comparative view + detailed columns.
Detailed: table + per-selection notes.
Deep Dive: one selection narrative.
Flags: Divergence, LowData (<10 candles), HighVol (>20%), WeakOpposition (<50%).
 Confirmation: Adds an explicit actionable block requesting confirmation before executing any (Auto) strategy.

### Table Template

```markdown
| Selection | Price | Prob% | Open% | ProbΔ (pp) | ProbVel (pp/min) | Vol% | VolTrend | Support | Resist | Direction | Signal | BLR | BPI% | Pressure |
|-----------|-------|-------|-------|-----------:|-----------------:|-----:|----------|---------|--------|-----------|--------|-----|------|----------|
| Example A | 2.50  | 40.0% | 35.0% | +5.0       | +1.7             | 8.5  | ↑ +25%   | 35.7%  | 45.5%  | STEAMING  | Strong Steam | 0.59 | +18.0 | Aligned |
| Example B | 3.50  | 28.6% | 33.3% | -4.7       | -1.6             | 12.1 | ↓ -20%   | 25.0%  | 35.0%  | DRIFTING  | Moderate Drift | 0.41 | -18.0 | Aligned |
```
Notes:
- Prob% from weighted price (High+Low+Close)/3
- ProbΔ = Current_Prob - Open_Prob (pp)
- BLR 0–1 scale; BPI% = ((BLR-0.50)/0.50)*100

### Execution Report Template

```markdown
## Strategy Execution Analysis

**SELECTED**: <Selection> @ <Price> | **STRATEGY**: <Name> | **ProbΔ**: <pp> | **Opposition**: <Yes/No>
**PRESSURE**: BLR=<0.53> (BPI=<+6.0%>) Alignment=<Aligned/Divergent/Neutral>
**ODDS RANGE**: <Valid/Invalid> (<current> in <min–max>)
**EXECUTION**: ✅ Activated / ❌ Skipped

**CONFIRMATION STATUS**: PENDING (not yet executed) OR EXECUTED.

If PENDING, append the standardized confirmation request block:

```text
CONFIRM_EXECUTION_REQUEST
Selection=<SelectionName>; Strategy="<Exact Strategy Name>"; MarketId=<id>; SelectionId=<id>; Auto=<Yes/No>; Stake=<if auto computed or N/A>; ProbDelta=<pp>; BLR=<value>; OppositionPassed=<Yes/No>
ACTION: Respond with: CONFIRM_EXECUTE yes  (to run)  |  CONFIRM_EXECUTE no  (to cancel)
``` 

### Parameters (if automated)
```json
{
	"OpenBetPosition.Stake": <value>,
	"CloseBetPosition.Profit": <value>,
	"CloseBetPosition.Loss": <value>
}
```
If Manual strategy selected: (omit JSON)
```
Manual strategy – operator decision, no automated parameters.
```

### Alternatives
| Selection | Signal | ProbΔ (pp) | Odds | BLR | BPI% | Pressure | Reason |
|-----------|--------|------------|------|-----|------|----------|--------|
| Example B | Drift  | -4.7       | 8.2  |0.44 | -12.0| Aligned  | Opposition not confirmed |
```

## 10. Process
Analyze → Favorite → Opposition → Validate → Filter → Execute → Log → Pressure Review → QA tag.

Auto Execution Insertion Point: Insert a confirmation request between Filter and Execute. Only proceed to Execute step after receiving explicit CONFIRM_EXECUTE yes.

## 11. Quality Checks
10+ candles; opposition mass >50%; signal strength ranked; pressure coherence (log divergence); record outcome vs ProbΔ & BLR band.

## 12. API Calls (Pseudo)
ExecuteBfexplorerStrategySettings(name, marketId, selectionId)
ExecuteBfexplorerStrategySettingsWithParameters(name, marketId, selectionId, params)

Confirmation Flow Pseudo:
1. Produce analysis & execution report (status PENDING) + CONFIRM_EXECUTION_REQUEST block (no API call yet).
2. Wait for user/agent reply:
	 - If reply matches regex: ^CONFIRM_EXECUTE\s+yes\b → call appropriate Execute* API.
	 - If reply matches regex: ^CONFIRM_EXECUTE\s+no\b  → log cancellation, do NOT call API.
3. After successful execution append EXECUTION_LOG entry:
```json
{
	"event": "EXECUTED",
	"strategy": "<Name>",
	"marketId": "<id>",
	"selectionId": "<id>",
	"params": { ...only if Auto... }
}
```
4. If cancelled:
```json
{
	"event": "CANCELLED",
	"reason": "User declined confirmation",
	"strategy": "<Name>",
	"marketId": "<id>",
	"selectionId": "<id>"
}
```

## 13. Usage
Run any qualifying market. Only act when opposition + criteria met. Probability + pressure drive decision; price only for derivations.

## 14. Confirmation & Auto Execution Rules
Trigger Condition (to request confirmation):
- Exactly one qualifying strategy (post filtering & precedence rules) for the favorite.
- All common preconditions satisfied.
- No blocking flags (LowData, WeakOpposition, Disallowed Divergence unless Value).

Do NOT request confirmation if conditions unmet → explicitly state: "No qualifying execution – criteria not met".

Mandatory Confirmation Block: Always use the CONFIRM_EXECUTION_REQUEST format (Section 9) before any execution. Never call execution APIs without prior explicit CONFIRM_EXECUTE yes.

Cancellation: If confirmation declined or timeout (no response) treat as Skipped; include rationale in log.

Safety Checks Before Execution:
1. Recompute odds range validity at confirmation moment.
2. Ensure stake ≥ 2.00 if Auto; if not, bump to 2.00 and note adjustment.
3. Verify opposition mass still ≥50%; if dropped below, abort and output: "ABORT: Opposition lost (<value>%)".
4. Recalculate BLR; if moved into Divergent (unless Value), abort with note.

Idempotency: Prevent double execution by tracking last (marketId, selectionId, strategyName) tuple within current session. If duplicate confirmation attempt, respond with: "ALREADY_EXECUTED" and skip API call.

Audit Log: After execution or cancellation always produce a JSON log object (see Section 12) plus a short human-readable summary line.

Example Confirmation Response (user/agent):
```
CONFIRM_EXECUTE yes
```
Example Cancellation:
```
CONFIRM_EXECUTE no
```