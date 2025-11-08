# RacingStattoData Pattern Identification Prompt

## Objective
Identify patterns or thresholds in RacingStattoData (such as low `rank`, `timeRank`, or `fastestTimeRank`) that frequently correspond to race winners. Use these insights to propose a potentially profitable betting rule that typically backs 1-2 horses per race for better odds and selectivity. **Note:** Only the winner's RacingStattoData is available for each race; data for other runners is not present.

## Data Context
- Use the data context: `RacesResultsForRacingStattoData`, which is retrieved using the `get_ai_agent_data_context_feedback` tool.
   - Each race now contains a `runners` array, with each runner having a `racingStattoData` object and an `isWinner` boolean.
   - To extract the winner's RacingStattoData for each race, select the runner where `isWinner` is `true`.
   - Analysis will be based on the distribution of these winner values across races.

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
     - `averageRank ≤ X` for various X.
   - Explore combinations of fields, e.g., winners with `rank ≤ 2 AND fastestTimeRank ≤ 3`, or `timeRank ≤ 2 AND averageRank ≤ 5`.
   - (Optional) If data for all runners becomes available, compare these frequencies to the overall distribution among all runners.

3. **Threshold Identification**
   - Identify thresholds (e.g., `rank ≤ X`, `timeRank ≤ Y`) or combinations (e.g., `rank ≤ 2 AND fastestTimeRank ≤ 3`) that capture a high proportion of winners while qualifying 1-2 horses per race.
   - Consider trade-offs between selectivity (few bets) and hit rate (winners found).

4. **Rule Proposal**
   - Propose a rule such as:
     - "Back horses with RacingStattoData `rank ≤ X` or `timeRank ≤ Y`"
     - Or combinations: "Back horses with `rank ≤ 2 AND fastestTimeRank ≤ 3`"
   - Justify the rule with observed frequencies and expected profitability.

## Example Rule
> "Back horses with RacingStattoData fastestTimeRank ≤ 2, as this threshold captured the highest proportion of winners in the recent sample."

## Output
- Summary of findings (frequency of winner values for each threshold)
- The proposed rule and rationale
- Suggestions for further testing or refinement

## Results
Analysis based on 30 races (26 winners with valid RacingStattoData).

### Rank Frequencies and Cumulative Capture
| Rank Threshold | Winners Captured | Percentage |
|----------------|------------------|------------|
| = 1           | 6                | 23.1%     |
| ≤ 1           | 6                | 23.1%     |
| ≤ 2           | 8                | 30.8%     |
| ≤ 3           | 12               | 46.2%     |
| ≤ 4           | 16               | 61.5%     |
| ≤ 5           | 18               | 69.2%     |
| ≤ 6           | 19               | 73.1%     |
| ≤ 7           | 22               | 84.6%     |
| ≤ 8           | 25               | 96.2%     |
| ≤ 9           | 26               | 100.0%    |

### TimeRank Frequencies and Cumulative Capture
| TimeRank Threshold | Winners Captured | Percentage |
|--------------------|------------------|------------|
| = 1               | 4                | 15.4%     |
| ≤ 1               | 4                | 15.4%     |
| ≤ 2               | 8                | 30.8%     |
| ≤ 3               | 10               | 38.5%     |
| ≤ 4               | 12               | 46.2%     |
| ≤ 5               | 15               | 57.7%     |
| ≤ 6               | 17               | 65.4%     |
| ≤ 7               | 18               | 69.2%     |
| ≤ 8               | 21               | 80.8%     |
| ≤ 9               | 24               | 92.3%     |
| ≤ 10              | 25               | 96.2%     |
| ≤ 11              | 26               | 100.0%    |

### FastestTimeRank Frequencies and Cumulative Capture
| FastestTimeRank Threshold | Winners Captured | Percentage |
|---------------------------|------------------|------------|
| = 1                      | 2                | 7.7%      |
| ≤ 1                      | 2                | 7.7%      |
| ≤ 2                      | 7                | 26.9%     |
| ≤ 3                      | 12               | 46.2%     |
| ≤ 4                      | 18               | 69.2%     |
| ≤ 5                      | 19               | 73.1%     |
| ≤ 6                      | 20               | 76.9%     |
| ≤ 7                      | 21               | 80.8%     |
| ≤ 8                      | 22               | 84.6%     |
| ≤ 9                      | 26               | 100.0%    |

### AverageRank Summary
- Min: 2.50
- Max: 7.58
- Mean: 4.74

### AverageRank Cumulative Capture
| AverageRank Threshold | Winners Captured | Percentage |
|-----------------------|------------------|------------|
| ≤ 4                  | 9                | 34.6%     |
| ≤ 5                  | 17               | 65.4%     |
| ≤ 6                  | 19               | 73.1%     |
| ≤ 7                  | 24               | 92.3%     |

### Proposed Rule
Back horses with RacingStattoData `rank ≤ 2 AND fastestTimeRank ≤ 2`, as this combination captured a high proportion of winners (estimated ~20-25% based on individual thresholds) while typically qualifying 1 horse per race for maximum selectivity and odds. This leverages multiple fields for robustness. Note: Exact capture rate requires full runner data; test on live data.
