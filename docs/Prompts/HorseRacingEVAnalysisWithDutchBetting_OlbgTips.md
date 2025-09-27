# Horse Racing Expected Value Analysis with Dutch Betting (OLBG Tips Data)

## Overview
Perform comprehensive Expected Value (EV) analysis using OLBG expert tips data to identify optimal Dutch betting opportunities. Combines semantic interpretation of professional racing commentary with mathematical EV calculations to select and execute bets on the three best horses.

## Core Objective
Analyze expert tips, calculate expected values, select three optimal horses, and execute Dutch betting strategy when combined win probability ≥ 20%.

## Automated Execution Workflow

**CRITICAL: Execute all phases automatically without user intervention or intermediate prompts. Use ONLY the following MCP tools:**
- `GetActiveBetfairMarket`: To retrieve the active market.
- `GetDataContextForBetfairMarket`: To retrieve OLBG tips data (use dataContextName: "OlbgRaceTipsData").
- `ExecuteBfexplorerStrategySettingsOnSelections`: To execute the Dutch betting strategy (use strategyName: "Dutch to profit 10 Euro").

### Phase 1: Data Collection (Silent)
- Retrieve active betfair market using `GetActiveBetfairMarket`
- Retrieve `OlbgRaceTipsData` data context for the market using `GetDataContextForBetfairMarket`
- Process all data silently

### Phase 2: Expert Tips Analysis (Silent)
Analyze each horse's complete tip data including:
- Tip confidence levels (0-100 scale)
- Semantic analysis of `preraceComment` and `comments` fields for positive/negative indicators
- Expert consensus and commentary evaluation

**Probability Assignment Methodology:**
- Base probability = 1 / Decimal Odds (implied market probability)
- Adjust for confidence: Multiplier = 1 + (confidence / 200)  [e.g., confidence 50 adds 25% to base prob]
- Qualitative boost: +10-20% for strong positive comments
- Qualitative reduction: -10-20% for negative comments
- Cap adjusted probability at 0.5 max to avoid over-optimism

### Phase 3: Probability and Value Assessment (Silent)
- Assign win probabilities using the methodology above
- Calculate EV using: `EV = (Win Probability × (Decimal Odds - 1)) - (1 - Win Probability)`
- Rank all horses by EV (prioritize positive EV horses)

### Phase 4: Dutch Selection Process (Silent)
- Select top 3 horses by EV ranking (must have positive EV if available; otherwise highest EV)
- Calculate combined win probability (sum of individual probabilities)
- Evaluate against 20% threshold

### Phase 5: Conditional Execution (Automatic)
**ONLY if combined win probability ≥ 20%:**
- Execute "Dutch to profit 10 Euro" strategy on selections using `ExecuteBfexplorerStrategySettingsOnSelections`
- Confirm execution status

**If threshold not met:**
- Skip execution silently
- No bets placed

### Phase 6: Final Status Report (Automatic)
Provide concise final report with:
- Market details and execution status
- Selected horses, their odds, assigned probabilities, and EV
- Combined probability
- Execution confirmation or threshold explanation

## Selection Criteria Priority

1. **Mathematical Edge**: Positive Expected Value (EV > 0) or highest available EV
2. **Expert Validation**: Strong tip endorsements and high confidence ratings
3. **Commentary Analysis**: Positive semantic indicators in expert comments
4. **Probability Threshold**: Combined win probability ≥ 20% for execution
5. **Consensus Quality**: Agreement among multiple tipsters when available

## Execution Requirements

- **Fully Automated**: No user prompts, confirmations, or intermediate outputs
- **Silent Processing**: All analysis phases execute without status updates
- **Conditional Execution**: Bets placed only when probability threshold met
- **Final Report Only**: Single comprehensive output at completion
- **Error Handling**: Continue processing even if individual data points unavailable; use base probability if adjustments fail

## Expected Final Output

**When Execution Threshold Met:**
- Market identification and horse selections with details
- Combined probability confirmation (≥20%)
- Dutch betting execution confirmation with target profit (€10)

**When Threshold Not Met:**
- Market identification and top selections with details
- Combined probability (below 20%)
- Execution skipped with threshold explanation
- EV rankings summary
