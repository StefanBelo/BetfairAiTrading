# BackFavouritePedigree_Task (System G3P)

## Objective
Execute automated betting actions based on the JSON output from `TheExpertHorseRacingPedigreeAnalyst_G3P.md`.

## 1. Input Data
- **Analyst Output:** JSON object from the previous step.
- **Market State:** Current market prices (to confirm favorite).

## 2. Execution Logic

**Step 1: Identify Target**
1. Parse the `runners` array from the Analyst JSON.
2. Sort runners by `price` (ascending).
3. Select the first runner (The Favorite).

**Step 2: Decision Matrix**
Evaluate the `suggestedAction` valid for the Favorite in the JSON.

| Analyst Suggestion | Condition | Action | Strategy Name |
| :--- | :--- | :--- | :--- |
| `Back` | `metrics.confidence` > 0.5 | **BACK** | `Bet 10 Euro` |
| `Lay` | `metrics.confidence` > 0.4 | **LAY** | `Lay 10 Euro` |
| `Ignore` | - | **SKIP** | - |
| `Back` | `metrics.confidence` <= 0.5 | **SKIP** (Low Conf) | - |

**Step 3: MCP Execution**
If Action is not SKIP:
1. Call `ExecuteStrategySettings`.
   - `marketId`: from JSON.
   - `selectionId`: Favorite's ID.
   - `strategyName`: `Bet 10 Euro` or `Lay 10 Euro`.
