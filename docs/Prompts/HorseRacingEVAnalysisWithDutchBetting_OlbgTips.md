# Horse Racing Expected Value Analysis with Dutch Betting (OLBG Tips Data)

## Overview
Perform comprehensive Expected Value (EV) analysis using OLBG expert tips data to identify optimal Dutch betting opportunities. Combines semantic interpretation of professional racing commentary with mathematical EV calculations to select and execute bets on the three best horses.

## Core Objective
Analyze expert tips, calculate expected values, select three optimal horses, and execute Dutch betting strategy when combined win probability ≥ 20%.

## Automated Execution Workflow

**CRITICAL: Execute all phases automatically without user intervention or intermediate prompts.**

### Phase 1: Data Collection (Silent)
- Retrieve active betfair market using `GetActiveBetfairMarket`
- Retrieve `OlbgRaceTipsData` context for the market
- Process all data silently

### Phase 2: Expert Tips Analysis (Silent)
Analyze each horse's complete tip data including:
- Tip confidence levels and expert ratings
- Semantic analysis of `preraceComment` and `comments` fields
- Expert consensus and commentary evaluation

### Phase 3: Probability and Value Assessment (Silent)
- Assign win probabilities based on comprehensive tip analysis
- Calculate EV using: `EV = (Win Probability × (Decimal Odds - 1)) - (1 - Win Probability)`
- Rank all horses by EV

### Phase 4: Dutch Selection Process (Silent)
- Select top 2 horses based on EV and expert endorsements
- Calculate combined win probability
- Evaluate against 20% threshold

### Phase 5: Conditional Execution (Automatic)
**ONLY if combined win probability ≥ 20%:**
- Activate selected market using first horse's selection ID
- Execute "Dutch to profit 10 Euro" strategy on all three selections
- Confirm execution status

**If threshold not met:**
- Skip execution silently
- No bets placed

### Phase 6: Final Status Report (Automatic)
Provide concise final report with:
- Market details and execution status
- Selected horses and combined probability
- Execution confirmation or threshold explanation

## Selection Criteria Priority

1. **Mathematical Edge**: Positive Expected Value or highest available EV
2. **Expert Validation**: Strong tip endorsements and high confidence ratings
3. **Commentary Analysis**: Positive semantic indicators in expert comments
4. **Probability Threshold**: Combined win probability ≥ 20% for execution
5. **Consensus Quality**: Agreement among multiple tipsters when available

## Execution Requirements

- **Fully Automated**: No user prompts, confirmations, or intermediate outputs
- **Silent Processing**: All analysis phases execute without status updates
- **Conditional Execution**: Bets placed only when probability threshold met
- **Final Report Only**: Single comprehensive output at completion
- **Error Handling**: Continue processing even if individual data points unavailable

## Expected Final Output

**When Execution Threshold Met:**
- Market identification and horse selections
- Combined probability confirmation (≥20%)
- Dutch betting execution confirmation
- Target profit (€10) and stake distribution details

**When Threshold Not Met:**
- Market identification and top selections
- Combined probability (below 20%)
- Execution skipped with threshold explanation
- EV rankings and expert analysis summary
