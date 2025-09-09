# Horse Racing Win Market Strategy - Favourite Focus

**Objective**: Analyze horse racing win/place data to trade win market only, focusing on favourite. Use place data as sentiment indicator.

**Strategy**:
- Back favourite if it meets BACK criteria and has strongest negative divergence
- Otherwise lay favourite if it meets LAY criteria
- Execute only if time_to_race > 3 minutes

**Analysis Steps**:
1. Calculate momentum: win_momentum = (current_win - initial_win) / initial_win * 100
2. Calculate place_momentum similarly
3. Divergence = place_momentum - win_momentum
4. Identify favourite (lowest win odds)
5. Evaluate signals:
   - BACK: place_momentum < -0.1 AND win_momentum >= -0.05 AND divergence < -5%
   - LAY: place_momentum > 0.1 AND win_momentum < -0.1 AND current_win < 3.0 AND divergence > 5%

**Tool Usage** (use appropriate prefix):
1. `get_active_betfair_market` → get market ID
2. `get_data_context_for_betfair_market` (dataContextName: "HorseRacingWinToBePlacedData", marketId: from 1) → get data
3. `execute_bfexplorer_strategy_settings` (strategyName: "Bet 10 Euro" for back or "Lay 10 Euro" for lay, marketId, selectionId)

**Risk Management**:
- Stake: 10 Euro fixed
- Exit: 90s before race
- Stop loss: 20% of position
- Max 5% bankroll per race
1. **Momentum Strategy**: Focus on recent rapid movements
2. **Value Strategy**: Focus on divergence opportunities  
3. **Scalping Strategy**: Quick in/out based on short-term signals

## Prompt Execution Command

```
For active betfair market, retrieve "HorseRacingWinToBePlacedData" and execute win market analysis focusing on:
1. Place market momentum as leading indicator
2. Win market value identification  
3. Risk-adjusted position recommendations
4. Clear entry/exit criteria for win bets only

After analysis, evaluate the favourite:
- If favourite is the best selection (meets BACK criteria): Execute "Bet 10 Euro" strategy on favourite
- If favourite is not the best selection: Execute "Lay 10 Euro" strategy on favourite
- Only execute if time_to_race > 3 minutes
- Report execution results and final strategy status
```

## Data Analysis Template

### Market Data Analysis Table

| Horse | Selection ID | Current Win Odds | Win Movement | Current Place Odds | Place Movement | Win Momentum | Place Momentum | Divergence | Signal |
|-------|-------------|------------------|--------------|-------------------|----------------|--------------|----------------|------------|---------|
| [Horse Name] | [Selection ID] | [Current] | [Initial→Current] | [Current] | [Initial→Current] | [Calculate %] | [Calculate %] | [Place - Win %] | [🟢🔴⚫🟡] |

**Signal Legend:**
- 🟢 **BACK WIN**: Strong opportunity to back in win market
- 🔴 **LAY WIN**: Opportunity to lay in win market
- ⚫ **AVOID**: No clear opportunity or high risk
- 🟡 **WATCH**: Monitor for developing opportunity

### Market Analysis Summary Template

| Metric | Value | Interpretation |
|--------|--------|----------------|
| **Favorite** | [Horse Name (Odds)] | [Analysis of market leader] |
| **Biggest Mover** | [Horse Name (% change)] | [Reason for movement] |
| **Place Signal** | [Horse Name (% change)] | [Place market sentiment] |
| **Divergence Play** | [Horse Name (Divergence %)] | [Win vs place momentum difference] |
| **Time to Race** | [Minutes] | [Trading phase assessment] |

### Detailed Movement Analysis Template

#### [Primary Target Horse] - Analysis
| Time | Win Odds | Place Odds | Win Volume | Place Volume | Analysis |
|------|----------|------------|------------|--------------|----------|
| [T-5] | [Odds] | [Odds] | [Volume] | [Volume] | [Initial position] |
| [T-4] | [Odds] | [Odds] | [Volume] | [Volume] | [Market development] |
| [T-3] | [Odds] | [Odds] | [Volume] | [Volume] | [Trend confirmation] |
| [T-2] | [Odds] | [Odds] | [Volume] | [Volume] | [Signal strengthening] |
| [T-1] | [Odds] | [Odds] | [Volume] | [Volume] | **[Final assessment]** |

### Strategy Execution Framework

| Strategy Type | Entry Condition | Selection Criteria | Odds Target | Stake % | Expected ROI |
|---------------|----------------|-------------------|-------------|---------|--------------|
| **Momentum Back** | Place shortening >5% + Win stable ±2% | Favorites (odds <4.0) | Current +0.10 | 3-5% | 15-25% |
| **Value Back** | Win drifting >10% + Place stable ±5% | Mid-range (odds 4.0-15.0) | Current +0.20 | 2-3% | 20-30% |
| **Contrarian Lay** | Both markets shortening >15% artificially | Strong favorites (odds <2.5) | Current -0.10 | 1-2% | 10-20% |
| **Outsider Saver** | Place shortening >15% dramatically | Outsiders (odds >20.0) | Current +0.50 | 0.5-1% | 50-100% |

---

*This strategy leverages place market intelligence to make informed win market decisions, treating place data as a sentiment indicator rather than a trading target. Focus is on the favourite: back if best, otherwise lay.*
