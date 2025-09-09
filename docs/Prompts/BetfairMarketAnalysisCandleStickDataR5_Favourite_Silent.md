# Favourite Bet Decision Prompt (Compact)

Objective: Select exactly one action on the current favourite (lowest price): Bet 10 Euro, Lay 10 Euro, or No Bet.

## 1. Inputs
1. Retrieve active market: GetActiveBetfairMarket
2. Candles & BLR, on the active market: GetDataContextForBetfairMarket(marketId, "MarketSelectionsCandleStickData")

## 2. Metrics (Favourite)
- Price = latest close
- OpenPrice = first candle open | CurrentPrice = last candle close
- Prob% = 100 / Price
- OpenProb% = 100 / OpenPrice; ProbΔ_pp = CurrentProb − OpenProb
- Direction: STEAM (>0), DRIFT (<0), STABLE (|Δ| < 0.2pp)
- Minutes = (lastTime − firstTime)/60
- Velocity_ppm = ProbΔ_pp / Minutes
- Volatility% = (HighProb − LowProb)/CurrentProb * 100 (High/Low from candle highs/lows → convert to prob)
- BLR = backVolume / (backVolume + layVolume) (provided)
- OppWeight = 1 − FavouriteProbShare (sum probs others / total)

## 3. Bet Filters (must pass for any Bet/Lay)
Price 1.5–12.0 AND Volatility ≤ 50%.
Never Lay if ProbΔ_pp > 0.

## 4. Decision Rules
BACK (Bet 10 Euro) if:
- ProbΔ_pp ≥ +0.4 AND BLR ≥ 0.55 AND OppWeight < 0.85

LAY (Lay 10 Euro) if:
- ProbΔ_pp ≤ -0.4 AND BLR ≤ 0.45 AND OppWeight ≥ 0.50

Else: NO BET.

Tie-break: If both sides appear (should not logically) → No Bet.

## 5. Execution
If BACK or LAY → immediately call:
ExecuteBfexplorerStrategySettings(marketId, selectionId, "Bet 10 Euro" | "Lay 10 Euro")
No confirmation. Optional idempotency: skip if identical (marketId, selectionId, decision) already executed in session.

End.