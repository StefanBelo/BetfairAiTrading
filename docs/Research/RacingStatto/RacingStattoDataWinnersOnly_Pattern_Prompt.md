# RacingStattoData Pattern Identification Prompt

## Objective
Identify patterns or thresholds in RacingStattoData (such as low `rank`, `timeRank`, or `fastestTimeRank`) that frequently correspond to race winners. Use these insights to propose a potentially profitable betting rule. **Note:** Only the winner's RacingStattoData is available for each race; data for other runners is not present.

## Data Context
- Use the data context: `RacesWinnersForRacingStattoData`, which is retrieved using the `get_ai_agent_data_context_feedback` tool.
- For each race, only the winner's RacingStattoData is available. Analysis will be based on the distribution of these winner values across races.

## Steps
1. **Data Extraction**
   - For each race, extract the following for the winner only:
     - `rank`
     - `timeRank`
     - `fastestTimeRank`
     - `averageRank`

2. **Pattern Analysis**
   - Calculate the frequency with which the winner had:
     - `rank = 1`, `rank ≤ 2`, `rank ≤ 3`, etc.
     - `timeRank = 1`, `timeRank ≤ 2`, etc.
     - `fastestTimeRank = 1`, `fastestTimeRank ≤ 2`, etc.
   - (Optional) If data for all runners becomes available, compare these frequencies to the overall distribution among all runners.

3. **Threshold Identification**
   - Identify thresholds (e.g., `rank ≤ X`, `timeRank ≤ Y`) that capture a high proportion of winners.
   - Consider trade-offs between selectivity (few bets) and hit rate (winners found).

4. **Rule Proposal**
   - Propose a rule such as:
     - "Back horses with RacingStattoData `rank ≤ X` or `timeRank ≤ Y`"
   - Justify the rule with observed frequencies and expected profitability.

## Example Rule
> "Back horses with RacingStattoData fastestTimeRank ≤ 2, as this threshold captured the highest proportion of winners in the recent sample."

## Output
- Summary of findings (frequency of winner values for each threshold)
- The proposed rule and rationale
- Suggestions for further testing or refinement
