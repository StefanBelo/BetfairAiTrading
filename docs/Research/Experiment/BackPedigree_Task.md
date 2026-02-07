# BackPedigree Task

## Objective

Execute automated betting strategies based on pedigree analysis recommendations.

---

## 2. Decision Logic

### Inputs
- **Analyst Report:** Output from pedigree analysis recommendations.
- **Target Runners:** List of runners where `SuggestedAction` = `Back`.

### Selection Rules
| Trigger Condition | Action | Strategy Name | MCP Tool |
| :--- | :--- | :--- | :--- |
| **1 Runner** | Place single fixed bet | `Bet 10 Euro` | `ExecuteStrategySettings` |
| **≥ 2 Runners** | Dutch position across runners | `Dutch for 10 Euro` | `ExecuteStrategySettingsOnSelections` |
| **0 Runners** | No action | *N/A* | *N/A* |

## 3. Execution Protocol

**CRITICAL:** This task operates in **IMMEDIATE AUTO-EXECUTION** mode.
- **Behavior:** If pre-execution checks pass, bets are placed programmatically without user confirmation.
- **Stake Calculation:**
  - Single Bet: Fixed 10 EUR.
  - Dutching: Managed by external service (total risk ~10 EUR).