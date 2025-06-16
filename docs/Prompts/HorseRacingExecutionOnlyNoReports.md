# Horse Racing Strategy Execution Only - No AI Reports

## Minimal Execution Prompt (Final Action Report Only)

```
Task: Execute horse racing betting strategy with no AI analysis reports - only provide final strategy execution confirmation.

Instructions:

1. **Silent Market Data Collection**
   - Retrieve active betfair market using tool: GetActiveBetfairMarket
   - Retrieve data contexts: 'MarketSelectionsTradedPricesData' and 'RacingpostDataForHorsesInfo'
   - Complete all analysis internally without any reports

2. **Silent Analysis Phase**
   - Perform complete EV analysis using trading data and performance data
   - Evaluate market favorite against conservative criteria
   - Assess alternatives if favorite doesn't meet standards
   - Make selection decision (favorite, alternative, or no-bet)
   - NO ANALYSIS REPORTS - work silently

3. **Strategy Execution Decision**
   - **IF** selection meets conservative criteria:
     - Activate selected horse using tool: ActivateBetfairMarketSelection
     - Execute appropriate strategy:
       * "Back trailing stop loss trading" for high confidence selections
       * "Trade 20% profit" for moderate confidence selections
     - Use tool: ExecuteBfexplorerStrategySettings
   
   - **IF** no selection meets criteria:
     - Activate market favorite using tool: ActivateBetfairMarketSelection  
     - Execute "Lay 10 Euro" strategy using tool: ExecuteBfexplorerStrategySettings

4. **Final Execution Report Only**

   Provide ONLY this brief final report:

   **STRATEGY EXECUTION RESULT:**
   
   **Market:** [Market Name/Race Details]
   
   **Action Taken:**
   - **Selection:** [Horse Name] at [Current Price]
   - **Strategy Executed:** [Strategy Name]
   - **Reasoning:** [Brief 1-2 sentence justification]
   
   **OR**
   
   - **Action:** Lay strategy executed on [Favorite Name] at [Price]
   - **Reasoning:** No selections met conservative criteria
   
   **Execution Status:** [Confirmed/Failed]

Format: Keep the final report to maximum 5 lines. NO detailed analysis, rankings, tables, or explanations. Only state what action was taken and on which selection.

Selection Criteria (Internal Use Only):
- Market favorite with Fair+ EV and positive trading signals
- Alternative with EV > +0.10 and strong combined indicators  
- Lay favorite if no selections meet conservative standards

Strategy Selection (Internal Use Only):
- "Back trailing stop loss trading": High confidence (Good+ EV)
- "Trade 20% profit": Moderate confidence (Fair EV)
- "Lay 10 Euro": No acceptable selections

Execute strategy immediately after selection decision. Report only the final execution result.
```

## Usage Instructions

1. **Input Requirements:**
   - Active Betfair market with horse racing data
   - Access to trading data and performance contexts
   - Bfexplorer betting functionality

2. **Expected Output:**
   - Brief execution report only (maximum 5 lines)
   - No analysis, tables, rankings, or detailed explanations
   - Only final action taken and strategy executed

3. **Process:**
   - Complete analysis silently
   - Make selection decision internally
   - Execute appropriate strategy
   - Report only final execution result

## Example Output Format

**STRATEGY EXECUTION RESULT:**

**Market:** Newmarket 3:40 - Class 2 Handicap

**Action Taken:**
- **Selection:** Thunder Bay at 3.75
- **Strategy Executed:** Back trailing stop loss trading
- **Reasoning:** Market favorite with good EV and strong trading signals

**Execution Status:** Confirmed

---

**OR**

**STRATEGY EXECUTION RESULT:**

**Market:** Kempton 4:15 - Maiden Stakes

**Action Taken:**
- **Action:** Lay strategy executed on Royal Command at 2.50
- **Reasoning:** No selections met conservative criteria

**Execution Status:** Confirmed
