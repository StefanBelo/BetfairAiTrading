# Finding Profitable Horse Racing Rules with RacingStattoData

In this post, I describe my process for identifying potentially profitable betting rules using historical race results and RacingStattoData. The goal is to find simple, selective rules that could be used to back 1-2 horses per race, maximizing both selectivity and odds.

## Data Source: RacesResultsForRacingStattoData
I use the `RacesResultsForRacingStattoData` context, which contains detailed results for each race. For every runner, the data includes a `racingStattoData` object (with fields like `rank`, `timeRank`, `fastestTimeRank`, and `averageRank`) and an `isWinner` flag. This allows me to extract the RacingStattoData for each race winner and analyze the distribution of these values across many races.

## Analysis Steps
1. **Extract Winner Data:** For each race, I select the runner marked as the winner and record their RacingStattoData values.
2. **Frequency Analysis:** I calculate how often the winner had a low `rank`, `timeRank`, or `fastestTimeRank` (e.g., rank ≤ 2, fastestTimeRank ≤ 3, etc.).
3. **Threshold and Combination Testing:** I look for thresholds or combinations (like `rank ≤ 3 AND fastestTimeRank ≤ 3`) that capture a high proportion of winners while typically qualifying only 1-2 horses per race.
4. **Rule Proposal:** Based on the findings, I propose a rule and justify it with observed frequencies and selectivity.

## Example Finding
For a recent sample of 26 races, the combination `rank ≤ 3 AND fastestTimeRank ≤ 3` captured 26.9% of winners and usually selects just 1-2 horses per race. This kind of rule balances hit rate and selectivity, making it a promising candidate for further live testing.

## Next Steps
- Test the rule on new races and compare its performance to random or naive strategies.
- Refine the rule for different race types or field sizes.
- If possible, compare the distribution of these stats among all runners, not just winners, for deeper insight.

This approach provides a data-driven foundation for developing and refining betting strategies in horse racing.