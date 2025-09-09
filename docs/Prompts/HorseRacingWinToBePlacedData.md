# Horse Racing Win Market Strategy Using Place Data Analysis

## Overview
This prompt analyzes horse racing win and place market data to identify tradin## Prompt Execution Command

```
For active betfair market, retrieve "HorseRacingWinToBePlacedData" and execute win market analysis focusing on:
1. Place market momentum as leading indicator
2. Win market value identification  
3. Risk-adjusted position recommendations
4. Clear entry/exit criteria for win bets only

After analysis, execute strategies on the best selection based on combined divergence strength:
- For BACK_WIN signals: Execute "Bet 10 Euro" on the selection with the most negative divergence (place shortening faster than win)
- For LAY_WIN signals: Execute "Lay 10 Euro" on the selection with the most positive divergence (place drifting faster than win, if current_win_odds < 3.0)
- Prioritize LAY_WIN if present and meets criteria, otherwise BACK_WIN
- Only execute if time_to_race > 3 minutes
- Report execution results and final strategy status
```in the **win market only**. The place market data serves as a key indicator of market sentiment and informed money flow, helping to identify value and momentum in the win market.

## Strategy Focus
- **Primary Market**: Win bets only
- **Supporting Data**: Place market movements for analysis
- **Objective**: Identify win market opportunities based on place market signals

## Data Analysis Framework

### 1. Market Divergence Signals
Look for these key patterns between win and place markets:

#### Strong Win Signal Indicators:
- **Place market shortening faster than win market**: Indicates informed money backing horse to finish in top positions
- **Stable place odds with drifting win odds**: Suggests market overreaction, potential win value
- **Heavy place volume with modest win volume**: Smart money may be building position before win market move

#### Weak Win Signal Indicators:
- **Win odds shortening while place odds stable/drift**: May indicate uninformed backing
- **Both markets drifting**: General lack of confidence
- **Place odds lengthening faster than win**: Market losing confidence in horse's ability to finish well

### 2. Execution Strategy

#### Win Market Entry Criteria:
```
IF place_odds_momentum < -0.1 (shortening)
AND win_odds_momentum >= -0.05 (stable or slight drift)
AND time_to_race > 3_minutes
THEN consider_backing_win_market
```

#### Win Market Lay Criteria:
```
IF place_odds_momentum > 0.1 (drifting)
AND win_odds_momentum < -0.1 (shortening rapidly)
AND current_win_odds < 3.0
THEN consider_laying_win_market
```

## Practical Implementation

### Analysis Prompt Template:
```
For the active horse racing market with "HorseRacingWinToBePlacedData":

1. **Calculate Momentum Indicators**:
   - Win odds momentum = (current_win_odds - initial_win_odds) / initial_win_odds * 100
   - Place odds momentum = (current_place_odds - initial_place_odds) / initial_place_odds * 100

2. **Calculate Combined Divergence Score**:
   - Divergence = place_momentum - win_momentum
   - Positive divergence: Place drifting faster than win (potential LAY opportunity)
   - Negative divergence: Place shortening faster than win (potential BACK opportunity)

3. **Identify Market Divergence**:
   - Compare win vs place momentum ratios
   - Look for horses where place market shows more confidence than win market
   - Flag significant divergences (>10% difference in momentum)

4. **Generate Win Market Strategy**:
   - **BACK** horses with: Strong place support (-5% or better) + stable/value win odds (±2%) + negative divergence (< -5%)
   - **LAY** horses with: Weak place support (+5% or worse) + artificially short win odds (-10% or more) + positive divergence (>5%)
   - **AVOID** horses with: Consistent negative momentum in both markets (+10% both)
   - **WATCH** horses with: Mixed signals or developing patterns

5. **Risk Management**:
   - Maximum 5% bankroll per race
   - Exit 90 seconds before race start
   - Stop loss at 20% of position
   - Profit target at 15% ROI

6. **Create Analysis Tables**:
   - Complete market overview table with all runners
   - Market analysis summary with key insights
   - Detailed movement analysis for primary targets
   - Strategy execution framework with specific parameters
```

### Generic Analysis Output Format:

```json
{
  "race_analysis": {
    "market_id": "[Market ID]",
    "race_name": "[Race Name]",
    "time_to_race": "[X_minutes]",
    "total_runners": "[Number]",
    "analysis_timestamp": "[Current time]",
    "recommendations": [
      {
        "horse": "[Horse Name]",
        "selection_id": "[Selection ID]",
        "action": "BACK_WIN|LAY_WIN|AVOID|WATCH",
        "reasoning": "[Place vs win momentum analysis]",
        "entry_odds": "[Target odds range]",
        "stake": "[X%_bankroll]",
        "confidence": "HIGH|MEDIUM|LOW",
        "win_momentum": "[X.X%]",
        "place_momentum": "[X.X%]"
      }
    ],
    "market_summary": {
      "favorite": "[Horse name and odds]",
      "biggest_mover": "[Horse and % change]",
      "strongest_place_signal": "[Horse and analysis]",
      "best_divergence_play": "[Horse and reasoning]"
    }
  }
}
```

## Key Performance Indicators

### Monitor These Metrics:
1. **Correlation Coefficient**: Between place market movement and win market success
2. **Hit Rate**: Percentage of winning trades based on place market signals
3. **ROI**: Return on investment for win market trades
4. **Sharpe Ratio**: Risk-adjusted returns

### Success Criteria:
- Win rate > 35% on backed horses
- Average win odds > 2.5
- Monthly ROI > 10%
- Maximum drawdown < 15%

## Risk Management Rules

### Position Sizing:
- **High Confidence**: 3-5% of bankroll
- **Medium Confidence**: 1-3% of bankroll  
- **Low Confidence**: 0.5-1% of bankroll

### Exit Strategies:
1. **Time-based**: Close all positions 90 seconds before race start
2. **Profit-based**: Take profit at 15% ROI
3. **Loss-based**: Stop loss at 20% of position value
4. **Market-based**: Exit if both win and place markets move against position

## Implementation Notes

### Required Data Points:
- Historical win/place odds (minimum 5 data points)
- Volume data where available
- Time stamps for momentum calculation
- Current market status and time to race

### Strategy Variations:
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

After analysis, execute strategies on the best selection based on signal strength:
- For BACK_WIN signals: Execute "Bet 10 Euro" on the selection with the strongest place market momentum (most negative)
- For LAY_WIN signals: Execute "Lay 10 Euro" on the selection with the strongest win market momentum (most negative, if current_win_odds < 3.0)
- Prioritize LAY_WIN if present and meets criteria, otherwise BACK_WIN
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

*This strategy leverages place market intelligence to make informed win market decisions, treating place data as a sentiment indicator rather than a trading target.*
