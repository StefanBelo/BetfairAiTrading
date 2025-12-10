## Back or Lay Task: Data-Driven Market Recommendations

On the active Betfair market, use the full data ingestion and analysis workflow described in "The Expert Horse Racing Analyst" prompt to process all available data for each horse. For each runner:

1. **Comprehensive Data Analysis:**
	- Gather and process all horse, jockey, trainer, and market data using the prescribed workflow:
	  - Recent form (exponential smoothing of last 3-5 runs)
	  - Performance metrics (topSpeed, officialRating, racingpostRating, etc.)
	  - Qualitative insights (race comments, sentiment, NER)
	  - Timeform and Racing Post flags
	  - Jockey form and course fit
	  - Market context (favourite status, crowd wisdom)
	- Calculate a weighted Composite Score for each horse as described.

2. **Implied Odds & Value Calculation:**
	- Convert each horse's Composite Score into data-driven Implied Odds (Score 100 = odds 1.0, 50 = 2.0, 25 = 4.0, etc.).
	- Compare Implied Odds to current Market Odds.
	- Calculate the Value Score: ((Market Odds - Implied Odds) / Market Odds) x 100%.

3. **Recommendation Logic:**
	- Recommend **Back** if the Value Score is positive and supported by strong evidence from the full analysis (form, ratings, sentiment, etc.).
	- Recommend **Lay** if the Value Score is negative and the data analysis reveals significant weaknesses or overestimation by the market.
	- Recommend **No Bet** if the evidence is inconclusive, the value is marginal, or there are conflicting signals.
	- If a recommendation contradicts the Value Score, provide a clear rationale based on deeper data insights (e.g., hidden risks, market anomalies, or qualitative red flags).

4. **Output Table:**
	- Present all findings in a clear table:
	  | Horse Name | Market Odds | Implied Odds | Value Score (%) | Recommendation (Back/Lay/No Bet) | Rationale |

5. **Summary & Consistency Check:**
	- Double-check for contradictions between Value Score and recommendations.
	- Add a summary line stating if any recommendations do not match the value assessment, and explain why (e.g., strong qualitative evidence, market manipulation, or late-breaking news).

**Note:** Always leverage the best available data and advanced analysis logic from "The Expert Horse Racing Analyst" to ensure recommendations are robust, evidence-based, and actionable for the current market/race.