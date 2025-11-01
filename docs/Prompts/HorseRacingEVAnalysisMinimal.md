# Horse Racing EV Analysis - Minimal Execution Only

## Minimal EV Analysis and Betting Execution Prompt

```
Task: Perform silent Expected Value (EV) analysis for horse racing betting opportunities with conservative betting approach. Execute betting strategy without detailed reports - only confirm execution and target horse.

Instructions:

1. **Silent Market Data Retrieval**
   - Retrieve active betfair market using tool: GetActiveMarket
   - Save marketId for data retrieval
   - No output during collection

2. **Silent Performance Data Collection**
   - Retrieve data context 'RacingpostDataForHorses' using tool: GetDataContextForMarket
   - Focus on 'racingpostHorseData' field within selectionsData
   - No output during collection

3. **Silent Semantic Performance Analysis**
   - Analyze 'lastRacesDescriptions' field for each horse (note: plural form)
   - Extract 'raceDescription' text from each race entry
   - Utilize additional data: 'beatenDistance', 'lastRunInDays', 'position' for context
   - Consider 'rpRating' (Racing Post Rating) if available
   - Assess performance patterns, indicators, and contextual factors
   - No output during analysis

4. **Silent Win Probability Assessment**
   - Assign win probabilities based on semantic analysis and structured data
   - Consider recent form (lastRunInDays), finishing positions, and beaten distances
   - Factor in Racing Post ratings (rpRating) when available
   - Ensure probabilities sum to ~100%
   - No output during assessment

5. **Silent Expected Value Calculations**
   - Calculate EV using: EV = (Win Probability × (Decimal Odds - 1)) - (1 - Win Probability)
   - Assign EV ratings (Excellent/Good/Fair/Poor/Very Poor)
   - No output during calculations

6. **Silent Market Favorite Analysis**
   - Identify market favorite (lowest price)
   - Analyze favorite's form and calculate EV
   - No output during analysis

7. **Silent Conservative Betting Decision**
   - Apply criteria:
     a) Horse is market favorite
     b) EV rating "Fair" or better (EV ≥ -0.05)
     c) No major form concerns
     d) Win probability exceeds second-best horse by 8%+
   - No output during decision process

8. **Strategy Execution and Minimal Report**
   - **IF** favorite meets criteria:
     - Activate favorite using tool: ActivateBetfairMarketSelection
     - Execute "Bet 10 Euro" strategy using tool: ExecuteBfexplorerStrategySettings
     - **OUTPUT ONLY**: "STRATEGY EXECUTED: Bet 10 Euro on [Horse Name] (Market Favorite)"
   - **IF** favorite does NOT meet criteria:
     - Activate favorite using tool: ActivateBetfairMarketSelection
     - Execute "Lay 10 Euro" strategy using tool: ExecuteBfexplorerStrategySettings
     - **OUTPUT ONLY**: "STRATEGY EXECUTED: Lay 10 Euro on [Horse Name] (Market Favorite)"

Conservative Selection Criteria (ALL must be met for backing):
1. Market Favorite Status - Lowest price in market
2. Minimum EV Standard - EV ≥ -0.05
3. Form Reliability - No major recent concerns
4. Competitive Viability - Consistent competitiveness
5. Probability Dominance - 8%+ advantage over second-best horse

Note: This minimal approach performs full analysis silently and reports only the final execution action and target horse.
```

## Usage Instructions

**Input:** Active Betfair market with horse racing data
**Output:** Single line confirming strategy execution and target horse
**Process:** Complete silent analysis → Execute strategy → Report execution only

## Execution Examples

**Successful Backing:**
```
STRATEGY EXECUTED: Bet 10 Euro on Thunder Bay (Market Favorite)
```

**Lay Betting (Criteria Not Met):**
```
STRATEGY EXECUTED: Lay 10 Euro on Lightning Strike (Market Favorite)
```

This minimal prompt eliminates all detailed reporting while maintaining the complete analytical framework and conservative betting criteria, providing only essential execution confirmation.
