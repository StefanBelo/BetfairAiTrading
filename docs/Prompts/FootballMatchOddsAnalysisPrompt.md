# Football Match Odds Analysis Prompt

## Purpose
Reusable prompt for AI agents to perform comprehensive market analysis on football Match Odds markets. Analyzes both pre-kick and in-play traded prices to identify favourites, anomalies, and current market sentiment. **Note:** Full in-play analysis requires the match to be live (for `FootballMatchScoreData`); pre-kick analysis works on any open market.

## Usage
Paste into an AI agent interface or prompt runner. Replace `{{marketId}}` with a specific market ID or omit to use the active market.

## Instructions
1. **Retrieve Market Data**: Call `GetActiveMarket` to get the active market (or use specified `{{marketId}}`).
2. **Fetch Data Contexts**: Call `GetAllDataContextForMarket` with the marketId and dataContextNames: `["FootballMatchScoreData", "MarketSelectionsPriceHistoryData"]`.
3. **Perform Analysis**: Analyze the retrieved data according to the methodology below.
4. **Output Results**: Structure output in the specified format.

## Analysis Methodology
- **Pre-Kick Window**: Last 15 minutes before kickoff (or market start if not specified).
- **In-Play Windows**: Since kickoff, with rolling windows (last 5 and 15 minutes).
- **Favourite Determination**: Lowest traded price indicates strongest favouritism.
- **Price Movement**: Falling price = increasing favouritism; rising price = decreasing favouritism.
- **Confidence Weighting**: Prioritize recent high-volume trades; sustained movements over sporadic ones.
- **Anomalies**: Sharp price changes (>10%) or unusually large volumes (>2x average) in short periods.

## Output Format (Strict)
Produce exactly four sections:

- **Score**: `[Away Team] vs [Home Team] — [AwayScore]-[HomeScore]` (include match time/status). List goals: `minute - team - scorer`.

- **Favourite at Kickoff**: Team with lowest traded price in pre-kick window. Include key prices/volumes justifying choice.

- **Pre-Kick Anomaly Analysis**: Identify unusual movements/stakes in pre-kick window. State if they biased towards a team/outcome and rationale.

- **In-Play Traded Price Analysis**: Current favourite based on in-play prices. Include: kickoff price, current price, 5/15-min averages, key volumes/timestamps. Describe movement direction/sustainability and confidence level (High/Medium/Low) with one-line rationale.

## Example Prompt
```
Retrieve active market. Fetch FootballMatchScoreData and MarketSelectionsPriceHistoryData.

Report:
1. Score: [Away] vs [Home] — A-H with time/status and goals (min-team-scorer).
2. Favourite at kickoff: Team with lowest pre-kick price; key prices/volumes.
3. Pre-kick anomalies: Unusual moves/stakes; bias and why.
4. In-play: Current favourite; kickoff/current prices, 5/15-min averages, key trades (timestamps/volumes), movement direction/sustainability, confidence (High/Med/Low) with rationale.

Keep concise; use traded data over odds.
```

## Notes & Tips
- Default windows: 15 min pre-kick; 5/15 min in-play.
- If not in-play, skip score details; focus on pre-kick/in-play as available.
- Weight recent volumes for conviction; sustained trends > spikes.
- Output: 3-6 bullet points total; machine-parseable.
- Prefer evidence-based analysis; avoid speculation.