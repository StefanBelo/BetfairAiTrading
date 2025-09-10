# Horse Racing Favourite Strategy - Silent Mode

**Objective**: Execute win market strategy on favourite based on place/win momentum analysis. Output only execution results.

**Logic**:
- Calculate win_momentum = (current_win - initial_win) / initial_win * 100
- Calculate place_momentum = (current_place - initial_place) / initial_place * 100  
- Divergence = place_momentum - win_momentum
- Find favourite (lowest win odds)
- BACK if: place_momentum < -0.1 AND win_momentum >= -0.05 AND divergence < -5%
- LAY if: place_momentum > 0.1 AND win_momentum < -0.1 AND current_win < 3.0 AND divergence > 5%

**Tools**: `get_active_betfair_market` → `get_data_context_for_betfair_market` (dataContextName: "HorseRacingWinToBePlacedData") → `execute_bfexplorer_strategy_settings`

## Execution Command

```
1. Get active betfair market to obtain market ID
2. Use market ID to retrieve "HorseRacingWinToBePlacedData" context for that specific market
3. Calculate momentum/divergence for favourite
4. Execute appropriate strategy (Bet 10 Euro/Lay 10 Euro) on the favourite
5. Report only execution result
```

## Output Format

**Strategy Execution Result:**
- **Market**: [Race Name/Time]
- **Favourite**: [Horse Name] (Win: [odds], Place: [odds])
- **Strategy Executed**: [Bet 10 Euro/Lay 10 Euro/No Action]
- **Selection ID**: [selectionId]
- **Reason**: [BACK criteria met/LAY criteria met/Time <3min/No signal]
- **Momentum**: Win [%], Place [%], Divergence [%]
- **Status**: [SUCCESS/FAILED/SKIPPED]
