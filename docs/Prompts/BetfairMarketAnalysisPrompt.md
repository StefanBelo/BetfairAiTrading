# Betfair Market Analysis Prompt

## Objective
Analyze the currently active Betfair market by retrieving price history data, identifying trading patterns, and providing comprehensive analysis focused on the market favorite.

## Betfair Trading Fundamentals
**Core Profit Mechanism**: On Betfair, profit is made by backing at higher odds and laying at lower odds. This means:
- **Back then Lay**: Back a selection at higher odds (e.g., 3.0), then lay the same selection at lower odds (e.g., 2.5) for guaranteed profit
- **Lay then Back**: Lay a selection at lower odds, then back at higher odds to close the position profitably
- **Odds vs Probability**: In betting data, "price" = "odds". Probability is calculated as P = 1/Odds (or P = 1/Price)
- **Price Direction**: Odds shortening (decreasing numbers) = increasing probability, odds lengthening (increasing numbers) = decreasing probability
- **Trading Logic**: Look for selections likely to move from higher odds to lower odds (shortening) for back-to-lay opportunities, or from lower odds to higher odds (drifting) for lay-to-back opportunities

## Analysis Steps

### Step 1: Retrieve Active Market
First, get the currently active Betfair market being monitored in Bfexplorer:

```
Use GetActiveBetfairMarket to retrieve the active market details including:
- Market ID
- Market name
- Event name
- Market type
- Number of selections
- Market status
```

### Step 2: Identify Market Favorite
From the active market data retrieved in Step 1, identify the favorite selection (shortest odds/highest probability).

### Step 3: Get Favorite Selection Odds History Data
For the favorite selection identified in Step 2, get the MarketSelectionsPriceHistoryData:

```
Use GetDataContextForBetfairMarketSelection with:
- dataContextName: "MarketSelectionsPriceHistoryData"
- marketId: {from Step 1}
- selectionId: {favorite selection ID from Step 2}

This will provide detailed historical odds data (referred to as "price" in the data) specifically for the favorite selection.
Note: In the retrieved data, "price" field contains the betting odds, not probability.
```

### Step 4: Analyze Favorite Selection Trading Data and Patterns
Analyze the retrieved odds history data for the favorite selection to identify:
**Important**: In the data, "price" = betting odds. Calculate probability as P = 1/Price (or P = 1/Odds).

#### Odds Movement Analysis
- **Trend Direction**: Identify if odds are trending up (decreasing probability), down (increasing probability), or sideways
- **Volatility**: Measure odds fluctuations and stability
- **Support/Resistance Levels**: Key odds levels where selections bounce or break through
- **Odds Momentum**: Rate of odds change and acceleration/deceleration
- **Probability Shifts**: Track implied probability changes over time using P = 1/Odds

#### Volume and Liquidity Analysis
- **Trading Volume**: Amount of money matched at different odds levels
- **Liquidity Distribution**: Available backing and laying amounts at various odds
- **Market Depth**: Number of odds levels with significant liquidity
- **Spread Analysis**: Gap between best back and lay odds
- **Money Flow**: Direction of volume (backing vs laying activity)
- **Market Microstructure Analysis**: Determine if odds movements are driven by backing or laying activity

#### Betfair Exchange Flow Analysis
- **Backing Pressure Indicators**: Odds shortening (odds decreasing) indicates backers taking lay offers
- **Laying Pressure Indicators**: Odds lengthening (odds increasing) indicates layers taking back offers
- **Volume at Odds Movements**: High volume with odds changes reveals strength of backing/laying interest
- **Steam Move Detection**: Rapid odds movements with volume indicating informed money flow
- **Market Sentiment Direction**: Whether the favorite is being backed or laid by the market

#### Betting Market Efficiency Analysis
- **Market Overround**: Calculate total implied probability to assess market efficiency
- **Arbitrage Opportunities**: Identify potential sure-bet situations
- **Value Betting**: Compare implied probabilities with true probabilities
- **Market Bias**: Detect favorite-longshot bias or other market inefficiencies

#### Pattern Recognition
- **Odds Patterns**: Identify common trading patterns (breakouts, reversals, consolidations)
- **Time-based Patterns**: How odds behave at different times (pre-race, in-play, etc.)
- **Correlation Analysis**: How selections move relative to each other
- **Steam Moves**: Detect sudden, significant odds movements indicating informed money
- **Market Drift**: Identify gradual odds movements suggesting market sentiment shifts

#### Financial Trading Patterns Applied to Betting
- **Technical Analysis Patterns**: Apply chart patterns from financial markets to odds movements
- **Support and Resistance**: Key odds levels where odds bounce or break through (same as stock trading)
- **Trend Lines**: Draw trend lines on odds movements to identify direction and potential breakouts
- **Moving Averages**: Calculate moving averages of odds to identify trend direction and momentum
- **Volume Analysis**: Analyze volume patterns similar to stock trading (high volume breakouts, volume confirmation)
- **Candlestick Patterns**: Apply candlestick analysis to odds data for reversal and continuation signals
- **Momentum Indicators**: Use RSI, MACD-style indicators on odds data to identify overbought/oversold conditions
- **Fibonacci Retracements**: Apply Fibonacci levels to odds movements for potential reversal points
- **Gap Analysis**: Identify gaps in odds movements and their implications
- **Breakout Trading**: Identify when odds break through key levels with volume confirmation

### Step 5: Create Detailed Analysis Report
Generate a comprehensive analysis report for the favorite selection including:

#### Selection Overview
- **Selection Name**: Name of the favorite selection
- **Current Odds**: Latest available back/lay odds
- **Implied Probability**: Current market probability
- **Opening Odds**: Starting odds when market opened
- **Odds Movement**: Total change from opening to current
- **Position in Market**: Confirmation as market favorite

#### Detailed Odds Analysis
- **Odds Trajectory**: Complete odds movement history with key timestamps
- **Significant Odds Levels**: Support and resistance levels in the odds
- **Odds Volatility Periods**: Times of high/low volatility with causes
- **Trading Range**: Highest and lowest odds reached
- **Odds Momentum**: Current trend strength and direction
- **Volume at Odds Levels**: Matched amounts at different odds levels
- **Backing vs Laying Flow**: Identify periods of heavy backing (odds shortening) vs laying (odds lengthening)
- **Market Pressure Analysis**: Determine dominant market sentiment from odds/volume relationship
- **Technical Pattern Analysis**: Apply financial trading patterns to odds movements
- **Trend Analysis**: Identify uptrends (odds lengthening), downtrends (odds shortening), and sideways markets
- **Key Level Breaks**: Identify when odds break through significant support/resistance with volume
- **Momentum Shifts**: Detect changes in momentum using volume and odds action analysis

### Step 6: Trading Strategy and Recommendations
Provide specific trading recommendations for the favorite selection:

#### Favorite Selection Assessment
- Current market strength and confidence
- Liquidity analysis for the favorite
- Risk assessment for backing/laying the favorite

#### Trading Opportunities
- Value assessment of current odds
- Entry/exit points based on odds patterns
- Risk/reward calculations

#### Market Sentiment
- How the favorite's odds reflect market confidence
- External factors affecting the favorite's odds
- Comparison with historical performance patterns

#### Specific Trading Strategies
- Optimal backing strategies with stake recommendations
- Laying opportunities and liability calculations
- Hedging strategies if position needs protection
- Stop-loss levels and profit targets

### Step 7: Generate Detailed Trading Recommendations Report
Create a comprehensive trading strategy report with specific entry/exit points:

#### Primary Trading Strategy Assessment
- **Best Strategy**: Identify the optimal approach (backing, laying, or trading)
- **Risk Level**: Conservative, moderate, or aggressive approach
- **Expected Returns**: Realistic profit projections
- **Time Horizon**: Short-term scalping vs. hold-to-race strategy

#### Specific Trade Setups
For each viable strategy, provide:
- **Entry Points**: Exact odds levels for position entry
- **Exit Points**: Target odds for profit-taking
- **Stop Loss Levels**: Risk management exit points
- **Position Sizing**: Recommended stake as percentage of bankroll
- **Trade Logic**: Reasoning behind each recommendation
- **Risk/Reward Ratio**: Expected profit vs. potential loss

#### Market Timing Analysis
- **Optimal Entry Windows**: Best times to enter positions
- **Market Activity Patterns**: When to expect odds movements
- **Pre-Race Behavior**: How odds typically behave before race start
- **Volatility Periods**: Times to avoid or exploit for trading

## Expected Output Format

### Market Information
```
Market: {Market Name}
Event: {Event Name}
Market ID: {Market ID}
Status: {Market Status}
Analysis Time: {Current Timestamp}
```

### Favorite Selection Analysis Report
[Include the comprehensive analysis report for the favorite selection]

### Key Findings
- **Favorite Selection**: {Name and current odds}
- **Odds Movement**: {Total change from opening odds}
- **Current Trend**: {Direction and strength of recent odds movement}
- **Liquidity Status**: {Available backing/laying amounts}
- **Trading Pattern**: {Identified pattern in odds behavior}
- **Market Flow**: {Whether selection is being heavily backed or laid}
- **Dominant Activity**: {Backing pressure vs laying pressure analysis}

### Trading Strategy Recommendations
1. **Backing Strategy**: {Specific recommendations for backing the favorite}
2. **Laying Strategy**: {Specific recommendations for laying the favorite}
3. **Risk Management**: {Liability calculations and position sizing}
4. **Entry/Exit Points**: {Optimal odds levels for entry and exit}

### Price Pattern Alerts
- **Support Levels**: Key odds levels where odds have bounced
- **Resistance Levels**: Odds levels where odds have struggled to break through
- **Breakout Signals**: Patterns indicating potential significant moves
- **Volume Signals**: Unusual backing or laying activity
- **Momentum Indicators**: Strength of current odds trend

### Detailed Trading Recommendations Report
[Include comprehensive trading strategy analysis with specific recommendations]

#### Strategy 1: Back-to-Lay Trading (Primary Recommendation)
- **Entry Points**: {Specific odds levels for backing entry - these should be HIGHER odds}
- **Exit Points**: {Target odds levels for laying exit - these should be LOWER odds}
- **Trade Logic**: Back at higher odds (e.g., 3.5), lay at lower odds (e.g., 2.8) for guaranteed profit
- **Profit Calculation**: (Back odds - Lay odds) × Stake ÷ Back odds = Profit per unit
- **Risk Management**: {Stop loss levels if odds move against position}
- **Expected Return**: {Realistic profit projections based on odds differential}
- **Time Frame**: {Expected duration of trade}

#### Strategy 2: Pure Backing Position (Alternative)
- **Entry Points**: {Optimal backing odds levels}
- **Hold Strategy**: {Duration and exit criteria}
- **Value Assessment**: {Why current odds represent value}
- **Risk Factors**: {Potential downsides and mitigation}

#### Strategy 3: Lay-to-Back Trading (Counter-Trend Strategy)
- **Entry Points**: {Specific odds levels for laying entry - these should be LOWER odds}
- **Exit Points**: {Target odds levels for backing exit - these should be HIGHER odds}  
- **Trade Logic**: Lay at lower odds (e.g., 2.2), back at higher odds (e.g., 2.8) for profit
- **Profit Calculation**: (Back odds - Lay odds) × Stake ÷ Lay odds = Profit per unit
- **Liability Management**: Laying requires (Lay odds - 1) × Stake as liability
- **Risk Factors**: Unlimited liability if selection wins while position is open

#### Strategy 4: Advanced Trading Strategies (If Applicable)
- **Scalping Setups**: {Short-term profit opportunities from small odds movements}
- **Arbitrage Possibilities**: {Risk-free profit opportunities across different markets}
- **Financial Trading Patterns**: {Apply stock trading patterns to odds movements}
- **Breakout Trading**: {Trade odds breakouts through key levels with volume confirmation}
- **Trend Following**: {Follow established trends in odds movements - back before shortening, lay before drifting}
- **Reversal Trading**: {Trade against trends at key support/resistance levels}
- **Range Trading**: {Trade within established odds ranges - back at top of range, lay at bottom}
- **Volume-Based Strategies**: {Use volume analysis for optimal entry/exit timing}

#### Position Sizing and Risk Management
- **Conservative Approach**: {Low-risk position sizing}
- **Moderate Approach**: {Balanced risk/reward positioning}
- **Aggressive Approach**: {Higher risk, higher reward sizing}
- **Stop Loss Protocols**: {Exact exit criteria for each strategy}
- **Liability Calculations**: {For laying strategies}

#### Market Timing Recommendations
- **Immediate Actions**: {What to do right now}
- **Next 30 Minutes**: {Short-term opportunities}
- **Next 1-2 Hours**: {Medium-term positioning}
- **Pre-Race Final Hour**: {End-game strategy}

#### Top Recommendation Summary
- **Primary Strategy**: {Best recommended approach}
- **Entry Price**: {Specific odds level}
- **Target Price**: {Profit-taking level}
- **Risk Level**: {Conservative/Moderate/Aggressive}
- **Confidence Level**: {1-10 rating with justification}
- **Expected Outcome**: {Realistic profit expectation}

## Implementation Notes
- **Remember the Betfair profit rule**: Always back at higher odds and lay at lower odds for profit
- **Data terminology**: In retrieved data, "price" field = betting odds. Calculate probability as P = 1/Price
- Ensure data is current and specifically for the favorite selection
- Handle missing or incomplete odds history data gracefully
- Provide confidence levels for all trading recommendations
- Include timestamp for analysis validity
- Consider market type-specific factors affecting the favorite
- Calculate implied probabilities using P = 1/Odds and value assessment
- **Laying liability**: Laying requires (odds-1 × stake) as liability - factor this into risk management
- Account for Betfair commission (typically 2-5%) in profit calculations
- Consider liquidity when recommending position sizes for the favorite
- Focus on the favorite's odds behavior and trading patterns
- **Provide specific odds levels**: All entry/exit recommendations must include exact odds
- Include realistic timeframes for each trading strategy
- Consider current time vs. race start time for timing recommendations
- Factor in market volatility patterns when suggesting optimal entry windows
- Always provide alternative strategies for different risk tolerances
- **Trading direction clarity**: Specify whether to back first then lay, or lay first then back
- **Analyze odds direction to determine market flow**: Odds shortening = backing pressure, odds lengthening = laying pressure
- **Volume analysis**: High volume with odds movement indicates strength of backing/laying sentiment
- **Identify steam moves**: Rapid odds changes with heavy volume indicate informed money flow
- **Market sentiment assessment**: Determine if the favorite is being backed or opposed by the market
- **Apply financial trading concepts**: Use support/resistance, trend lines, volume analysis, and chart patterns
- **Technical indicators**: Consider momentum indicators and moving averages adapted for odds data
- **Pattern recognition**: Look for classic chart patterns like triangles, flags, head and shoulders in odds movements
- **Risk management**: Apply position sizing and stop-loss concepts from financial trading

## Success Criteria
- Complete analysis of the favorite selection's odds history
- Actionable trading recommendations specific to the favorite
- Clear risk assessment for backing/laying the favorite
- Professional presentation format focused on single selection
- Data-driven insights with supporting evidence from odds patterns
- Detailed understanding of the favorite's trading dynamics
- Specific entry/exit points with exact odds levels
- Multiple trading strategies for different risk profiles
- Realistic profit expectations and risk management protocols
- Time-sensitive recommendations based on race start timing
- Comprehensive trading report suitable for immediate execution
- **Correct terminology**: Use "odds" when referring to betting prices, calculate probability as P = 1/Odds