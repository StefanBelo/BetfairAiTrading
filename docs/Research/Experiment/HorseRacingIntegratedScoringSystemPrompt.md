## Horse Racing — Integrated Scoring System (Optimized Prompt)

### Purpose
Perform a concise, reproducible runner analysis for the active Betfair horse-racing market using `RacingpostDataForHorses` and `AtTheRacesDataForHorses`. Produce normalized feature scores (0–10), an integrated score per runner, a ranked results table, and short rationale for top candidates.

### Inputs (automated)
- Call `GetActiveMarket` to obtain `marketId`, market metadata, and selections (`selectionId`, `name`, `price`).
- Call `GetAllDataContextForMarket` with `dataContextNames: ["RacingpostDataForHorses", "AtTheRacesDataForHorses"]`.

### Per-runner fields to extract
- Recent finishes and average finish position
- Speed / TopSpeed ratings
- Days since last run
- Course & distance form (last N starts on same course/distance)
- Sectional times and estimated closing speed
- Running style (front/mid/closer) and pace suitability
- Weight carried and handicap adjustments
- Official rating
- Star / expert ratings and narrative commentary (sentiment)

### Analytical workflow
1. Data cleaning: normalize units, impute or mark missing values, cap outliers.
2. Feature engineering: derive rolling metrics (e.g., avg last 3 finishes), normalized speed z-scores, recency decay for form.
3. Per-feature normalization: map raw metrics to 0–10 using percentile or min/max clipping with documented bounds.
4. Weighted aggregation: combine normalized features into an `IntegratedScore` using the weights below.
5. Win Probability Normalization: convert `IntegratedScore` into a `WinProbability` (0.0 to 1.0) such that the sum of `WinProbability` for all runners equals 1.0. (Method: divide each runner's `IntegratedScore` by the sum of all runner's scores, or use a softmax approach if preferred).
6. Ranking & filtering: sort by `WinProbability`, compute implied value vs market price, flag trade/bet candidates.
7. Short rationale: list top 3 reasons each top selection ranks highly and note key weaknesses.

### Scoring weights (recommended)
| Feature               | Weight |
|-----------------------|--------:|
| Average finish pos.   | 15%    |
| Speed rating          | 15%    |
| Days since last run   | 10%    |
| Course/Dist form      | 10%    |
| Sectionals            | 10%    |
| Running style         | 8%     |
| Weight/Handicap       | 8%     |
| Official rating       | 6%     |
| Star rating           | 8%     |
| Sentiment commentary  | 10%    |

Notes: weights can be tuned per meeting type (flat, jumps) or user preference. Ensure weights sum to 100%.

### Output format (required)
- Primary: a Markdown summary table (see schema below) representing every runner with numeric columns rounded to two decimals.
- Secondary: a JSON file containing **all calculated fields** (including raw features, normalized component scores, and metadata) saved to E:\Projects\BetfairAiTrading\docs\Research\Experiment\reports for archival and downstream processing.

Table columns (summary view in Markdown)
| name | price | SpeedRating | RunningStyle | SentimentScore | IntegratedScore | WinProbability | Rank |

Output requirements
- Provide both the Markdown table and the JSON file path used for saving.
- For each top-ranked runner include a 1–2 sentence rationale and which features contributed most to the score.
- The JSON output MUST include all intermediate fields used for scoring (e.g., `AvgFinishPos`, `OfficialRating`, `CourseDistForm`).
- JSON generation rules to prevent parsing errors (e.g. "Bad Unicode escape"):
  - JSON MUST be valid and UTF-8 encoded. All keys and string values use double quotes.
  - Do not include raw control characters or unescaped backslashes inside string values (these trigger Unicode-escape errors).
  - Dates must use ISO-8601, numbers must be numeric types (no quotes), booleans are true/false, missing values are null.
  - No trailing commas; validate JSON with a strict parser before saving.
- If data is missing for a runner, explicitly set fields to `null` in JSON and `N/A` in the table.

### Save location
Store outputs and data extracts here:
E:\Projects\BetfairAiTrading\docs\Research\Experiment\reports

Suggested filenames
- `HorseRacing_Scored_{marketId}.md`
- `HorseRacing_Scored_{marketId}.json`

### Prompt-engineering tips (for consistent runs)
- Be explicit about numeric ranges (0–10) and rounding rules.
- Request deterministic methods for normalization (percentile or z-score) and specify tie-breakers for rank equalities.
- Require both human-readable Markdown and machine-readable JSON outputs, and the exact save path.
- JSON-safety: explicitly instruct the agent to produce strictly valid UTF-8 JSON (double-quoted keys/strings, escaped backslashes or forward-slash file paths, no control characters) and validate output before saving.

### Quick example instruction (copy/paste)
"Using the active market and the RacingPost + AtTheRaces contexts, extract all listed fields, normalize each to a 0–10 scale, compute the weighted IntegratedScore, and normalize to WinProbability (summing to 1.0). Produce a summary Markdown table (name, price, SpeedRating, RunningStyle, SentimentScore, IntegratedScore, WinProbability, Rank) and a full JSON file (all fields) saved to E:\\Projects\\BetfairAiTrading\\docs\\Research\\Experiment, then list the top 3 rationale."

---