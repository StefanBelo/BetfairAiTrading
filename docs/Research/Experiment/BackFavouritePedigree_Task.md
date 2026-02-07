# BackFavouritePedigree_Task.md

## Objective

Execute automated betting strategies on the race favorite based on pedigree analysis recommendations from [TheExpertHorseRacingPedigreeAnalyst.md].

---

## 2. Decision Logic

### Inputs
- **Analyst Report:** Output from `TheExpertHorseRacingPedigreeAnalyst.md`.
- **Favorite Runner:** The runner with the lowest price (best odds) in the market.

### Selection Rules (Contrarian Signal Validation)
| Pedigree Support for Favorite | Field Condition | Action | Strategy Name | MCP Tool |
| :--- | :--- | :--- | :--- | :--- |
| PedigreeData = Missing | - | SKIP (No Action) | - | - |
| SuggestedAction = Back | At least 1 runner with SuggestedAction = Lay | Bet on favorite | `Bet 10 Euro` | `ExecuteStrategySettings` |
| SuggestedAction = Back | No runners with SuggestedAction = Lay | SKIP (No Action) | - | - |
| SuggestedAction ≠ Back | At least 1 runner with SuggestedAction = Back | Lay favorite | `Lay 10 Euro` | `ExecuteStrategySettings` |
| SuggestedAction ≠ Back | No runners with SuggestedAction = Back | SKIP (No Action) | - | - |

**Rationale:** Execute only when pedigree signals show clear divergence across the field (contrarian opportunity).

## 3. Execution Protocol

**CRITICAL:** This task operates in **IMMEDIATE AUTO-EXECUTION** mode.
- **Behavior:** If pre-execution checks pass, bets are placed programmatically without user confirmation.
- **Stake Calculation:** Fixed 10 EUR liability for both bet and lay.