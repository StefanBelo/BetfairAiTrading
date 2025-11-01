# BetfairAiTrading Weekly Report (42)

## Topic: Horse Racing Modelling Metrics & Retraining Frequency

### Summary of Group Discussion

#### Key Points Raised
- One participant working on horse racing shared their journey after two months of model development, expressing optimism but seeking advice from more experienced colleagues.
- They filter selections based on top probability results, typically resulting in 20–30 selections per day, and manage risk by adjusting probability thresholds.
- **Key metrics tracked:**
  - Daily profitability (level stakes win bets), monitored with a 7-day rolling average.
  - Pivot table analysis of predicted rank vs. actual finish, including win% for top selections and heatmap visualizations to check for expected patterns.
  - Average Brier score and log loss, tracked daily and as rolling averages (7-day, 30-day) to monitor predictive performance.
- There was concern about seasonality in horse racing and questions about what should trigger retraining or further feature engineering.
- The group discussed what additional metrics or early warning signs might indicate a model is underperforming, especially given the chaotic nature of horse racing.

#### Additional Insights from the Group
- **Profitability and rolling averages** were widely agreed upon as essential, but several participants stressed the importance of tracking metrics by segment (e.g., by track, distance, or season) to catch hidden weaknesses.
- **Calibration plots** and **log loss** were recommended for monitoring probability accuracy, with some suggesting the use of reliability diagrams.
- **Feature drift** and **data drift** detection were mentioned as important for knowing when to retrain, especially in a seasonal sport.
- Some recommended tracking **return by odds band** (e.g., favorites vs. outsiders) to spot if the model is only working in certain market segments.
- **Backtesting** and **out-of-sample validation** were highlighted as critical for robust model evaluation.
- A few cautioned that short-term swings are normal and that retraining too frequently can be counterproductive; instead, focus on longer-term trends and statistical significance.
- There was consensus that no single metric is sufficient—combining profitability, calibration, and error metrics gives the best picture.

### Opinion & Recommendations
The discussion shows that successful horse racing modelling requires a multi-metric approach. Profitability, calibration, and error metrics (like log loss and Brier score) should be tracked both overall and by segment. Monitoring for data/feature drift and using backtesting are key for knowing when to retrain. Short-term variance is inevitable, so focus on longer-term trends and avoid overreacting to daily swings. Community advice emphasizes blending statistical rigor with practical experience.

---

*Prepared by GitHub Copilot for BetfairAiTrading Weekly Report, October 18, 2025.*
