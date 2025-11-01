# Horse Racing EV Analysis - Minimal Execution

## Minimal EV Analysis and Strategy Execution Prompt

```
Task: Perform silent Expected Value (EV) analysis for horse racing with conservative betting criteria. Execute "Bet 10 Euro" strategy on favorite if criteria met, otherwise execute "Lay 10 Euro" strategy. Report only final strategy execution.

Instructions:

1. **Silent Data Collection**
   - Retrieve active betfair market using tool: GetActiveMarket
   - Retrieve racing data using tool: GetDataContextForMarket with 'RacingpostDataForHorses'
   - No interim reports during data collection

2. **Silent Analysis**
   - Analyze each horse's 'lastRacesDescription' field semantically
   - Ignore 'predictionScore' field completely
   - Assign win probabilities based on recent form patterns
   - Calculate EV for each horse: EV = (Win Probability × (Decimal Odds - 1)) - (1 - Win Probability)
   - Identify market favorite (lowest price)

3. **Conservative Criteria Check (Silent)**
   - Market Favorite Status: Lowest price in market
   - Minimum EV Standard: EV ≥ -0.05 ("Fair" rating or better)
   - Form Reliability: No major recent performance concerns
   - Probability Dominance: Favorite's win probability exceeds second-best horse by at least 8%

4. **Strategy Execution**
   - **IF** favorite meets ALL criteria:
     - Activate favorite using tool: ActivateBetfairMarketSelection
     - Execute "Bet 10 Euro" strategy using tool: ExecuteBfexplorerStrategySettings
   - **IF** favorite does NOT meet criteria:
     - Activate favorite using tool: ActivateBetfairMarketSelection
     - Execute "Lay 10 Euro" strategy using tool: ExecuteBfexplorerStrategySettings

5. **Final Report**
   - Report only: Horse name, strategy executed (Bet/Lay), and execution status
```

## Expected Output Format:
```
Strategy executed: [BET/LAY] 10 Euro on [Horse Name] - Status: [Success/Failed]
```

This minimal prompt performs comprehensive analysis silently and reports only the final strategy execution result.
