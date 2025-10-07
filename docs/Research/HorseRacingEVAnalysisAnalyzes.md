# Horse Racing EV Analysis Approaches

This document elaborates on various approaches to calculating Expected Value (EV) in horse racing betting, using Timeform data fields such as `ratingStars`, `horseWinnerLastTimeOut`, `horseInForm`, `suitedByGoing`, `suitedByCourse`, `suitedByDistance`, `trainerInForm`, `trainerCourseRecord`, `jockeyInForm`, `jockeyWonOnHorse`, `timeformTopRated`, and `timeformImprover`. Each approach is described in detail, including methodology, implementation considerations, positive and negative points, and examples where relevant.

## 1. Weighted Scoring and Probability Normalization (Baseline Approach)

### Description
This approach, similar to the one outlined in the Horse Racing EV Analysis Prompt, assigns predefined weights to each Timeform factor based on perceived importance. Points are summed for each horse to create a composite score, which is then normalized across all horses in the race to estimate true win probabilities. EV is calculated by comparing these true probabilities to implied probabilities derived from market odds.

- **Scoring System Example:**
  - Base Score (35%): `ratingStars` (5★ = 35 pts, 4★ = 28 pts, etc.)
  - Form (40%): `horseWinnerLastTimeOut` (+10), `horseInForm` (+7), `horseBeatenFavouriteLTO` (+3)
  - Suitability (15%): +5 each for `suitedByGoing`, `suitedByCourse`, `suitedByDistance`
  - Connections (10%): `trainerInForm` (+4), `trainerCourseRecord` (+3), `jockeyInForm` (+3), `jockeyWonOnHorse` (+2)
  - Bonuses: +5 for `timeformTopRated` or `timeformImprover`

- **Probability Calculation:** True Probability = Horse Score / Sum of All Horse Scores
- **EV Formula:** EV = ((True Probability - Implied Probability) / max(True Probability, Implied Probability)) × 100 (bounded -100% to +100%)

### Implementation Considerations
- No training data required; relies on expert-defined weights.
- Easily adjustable for different race types (e.g., increase form weight for sprints).
- Requires at least 65% data completeness per the prompt.

### Positive Points
- Highly interpretable: Clear why a horse scores high (e.g., "Azure Angel has 5★ rating and won last time").
- Fast and lightweight: Suitable for real-time analysis without computational overhead.
- Comprehensive data usage: Incorporates all available Timeform fields directly.
- Transparent decision-making: Facilitates manual review and strategy refinement.

### Negative Points
- Subjectivity in weighting: Weights are based on assumptions that may not hold across all races (e.g., overvaluing `ratingStars` in handicaps).
- Ignores interactions: Doesn't account for how factors combine (e.g., a top-rated horse with poor form).
- Static nature: Doesn't adapt to market changes or historical performance feedback.
- Potential bias: Incomplete data can skew scores, leading to unreliable probabilities.

### Example Application
For a race with 8 horses, Horse A scores 85/100, total scores sum to 600. True Prob = 85/600 ≈ 14.2%. If odds imply 10% prob (odds 10.0), EV ≈ ((0.142 - 0.10) / 0.142) × 100 ≈ 29.6% (positive value bet).

## 2. Linear Regression Modeling

### Description
This statistical approach models win probability as a function of Timeform factors using logistic regression. Categorical fields (e.g., `horseWinnerLastTimeOut`) are converted to binary (0/1), while ordinal fields like `ratingStars` are treated as numerical. The model is trained on historical race data to learn coefficients, then predicts probabilities for new races.

- **Model Setup:** Dependent variable = Win (1) or Loss (0). Independent variables = Encoded Timeform fields.
- **Training:** Use historical data (e.g., past races with outcomes) to fit the model via maximum likelihood.
- **Prediction:** For a new race, input horse data to get predicted win prob, then EV = (Predicted Prob × (Odds - 1)) - 1.

### Implementation Considerations
- Requires historical dataset with Timeform data and outcomes (e.g., from Racing Post or Betfair archives).
- Feature engineering: Create interaction terms if needed (e.g., `ratingStars` × `horseInForm`).
- Tools: Python's scikit-learn for fitting, or R's glm for logistic regression.
- Validation: Use cross-validation to assess model accuracy (e.g., AUC-ROC > 0.7 for good performance).

### Positive Points
- Statistical foundation: Quantifies factor importance (e.g., coefficient shows `trainerInForm` impact).
- Handles numerical data well: Suitable for scaling `ratingStars` or adding derived metrics.
- Confidence intervals: Provides uncertainty estimates for probabilities.
- Adaptable: Easy to retrain with new data for improved accuracy.

### Negative Points
- Assumes linearity: May fail if relationships are non-linear (e.g., exponential effect of high ratings).
- Data hungry: Needs substantial historical data to avoid overfitting.
- Sensitive to outliers: A single anomalous race can skew coefficients.
- Preprocessing required: Categorical encoding and missing value handling add complexity.

### Example Application
Train on 1000 past races. Model coefficients: `ratingStars` = 0.5, `horseWinnerLastTimeOut` = 0.8. For Horse B: ratingStars=4, winnerLastTime=1 → Predicted Prob ≈ 0.25. At odds 4.0 (implied prob 0.25), EV = (0.25 × 3) - 1 = 0.75 - 1 = -0.25 (negative, avoid bet).

## 3. Machine Learning Classification (e.g., Random Forest or Gradient Boosting)

### Description
Ensemble methods like random forest or XGBoost classify wins by building multiple decision trees on bootstrapped data subsets. They handle mixed data types (categorical and numerical) and capture non-linear interactions. The model outputs win probabilities, which are used for EV calculation.

- **Model Setup:** Target = Win/Loss. Features = All Timeform fields (one-hot encoded for categoricals).
- **Training:** Fit on historical data, tune hyperparameters (e.g., tree depth, number of estimators).
- **Prediction:** Ensemble predictions give probabilities; EV as (Prob × Payout) - 1.

### Implementation Considerations
- Libraries: scikit-learn (RandomForestClassifier) or XGBoost in Python.
- Data needs: Large historical dataset (e.g., 5000+ races) for robust training.
- Feature importance: Built-in metrics to identify key factors (e.g., `ratingStars` often tops).
- Overfitting mitigation: Use regularization and validation sets.

### Positive Points
- High accuracy: Captures complex patterns (e.g., how `suitedByDistance` interacts with connections).
- Robust to data types: Handles missing values and outliers better than linear models.
- Scalable: Performs well with large datasets and can incorporate additional features (e.g., market volume).
- Predictive power: Often outperforms simpler methods in backtesting.

### Negative Points
- Black box: Hard to interpret (e.g., why Horse C is favored beyond feature importance).
- Computational cost: Training and prediction are resource-intensive.
- Data dependency: Requires extensive, high-quality historical data.
- Overfitting risk: Can memorize noise if not tuned properly.

### Example Application
Train random forest on 2000 races. Feature importance: `ratingStars` (30%), `horseInForm` (20%). Horse D predicted prob = 0.35 at odds 3.0 (implied 0.33), EV = (0.35 × 2) - 1 = 0.7 - 1 = -0.3 (slight negative).

## 4. Bayesian Probabilistic Modeling

### Description
Bayesian networks model probabilistic relationships between factors. Nodes represent variables (e.g., `ratingStars` → `horseInForm`), with conditional probabilities learned from data. Priors are updated with current race data to compute posterior win probabilities.

- **Network Structure:** Define dependencies (e.g., `trainerInForm` influences win prob conditionally on `ratingStars`).
- **Inference:** Use algorithms like Markov Chain Monte Carlo to estimate probabilities.
- **EV Calculation:** Use posterior probs in standard EV formula.

### Implementation Considerations
- Tools: Python's pgmpy or PyMC3 for Bayesian modeling.
- Data: Historical frequencies for priors (e.g., P(win | ratingStars=5) from past data).
- Complexity: Requires domain knowledge to define network structure.

### Positive Points
- Uncertainty handling: Provides credible intervals for probs, aiding risk assessment.
- Incremental learning: Easily updates with new data without full retraining.
- Flexible: Models dependencies explicitly (e.g., `jockeyWonOnHorse` conditional on `jockeyInForm`).
- Robust to small data: Works with fewer examples than ML methods.

### Negative Points
- Setup complexity: Designing the network is expert-intensive.
- Computational: Inference can be slow for real-time use.
- Assumptions: Requires accurate dependency assumptions.
- Interpretability: Better than ML but still abstract.

### Example Application
Network: `ratingStars` → Win, with P(win | ratingStars=5) = 0.4. Update with race data; posterior prob = 0.42. At odds 2.5 (implied 0.4), EV ≈ (0.42 × 1.5) - 1 = 0.63 - 1 = -0.37.

## 5. Rule-Based Expert System

### Description
Define explicit if-then rules based on horse racing expertise (e.g., "If ratingStars ≥4 AND horseWinnerLastTimeOut AND suitedByGoing, then prob = 0.3"). Rules are evaluated per horse, scores aggregated, and calibrated to probabilities.

- **Rule Examples:** High prob if multiple conditions met; low if key factors absent.
- **Calibration:** Map rule scores to historical win rates.
- **EV:** Standard calculation on calibrated probs.

### Implementation Considerations
- Tools: Simple Python scripts or rule engines like Drools.
- Maintenance: Rules updated manually based on performance.
- Data: Minimal; works with current race data.

### Positive Points
- Transparency: Clear reasoning (e.g., "Horse E meets 3/4 rules").
- No training needed: Relies on expert knowledge.
- Adaptable: Easy to tweak rules for specific scenarios.
- Works with sparse data: Handles incompleteness via rule fallbacks.

### Negative Points
- Subjectivity: Rules reflect biases of rule creators.
- Scalability issues: Hard to cover all factor combinations.
- Static: Doesn't learn from outcomes.
- Limited nuance: Ignores quantitative degrees.

### Example Application
Rules: 4+ for high prob. Horse F meets 3 rules → prob = 0.25. At odds 4.0, EV = (0.25 × 3) - 1 = 0.75 - 1 = -0.25.

## 6. Ensemble/Weighted Combination of Models

### Description
Combine predictions from multiple approaches (e.g., 40% scoring, 30% regression, 30% ML) via weighted averaging. Weights optimized via backtesting on historical data.

- **Integration:** Average probs, then EV.
- **Optimization:** Use grid search on weights for best historical ROI.

### Implementation Considerations
- Tools: Python for model stacking.
- Validation: Extensive backtesting required.
- Complexity: Highest among approaches.

### Positive Points
- Robustness: Mitigates weaknesses of individual methods.
- Accuracy boost: Often superior in practice.
- Flexibility: Adapts to data quality.
- Comprehensive: Leverages all techniques.

### Negative Points
- High complexity: Integration and tuning are challenging.
- Computational: Runs multiple models.
- Conflicting outputs: Requires careful weighting.
- Maintenance: Harder to update.

### Example Application
Ensemble: Scoring (0.4) + Regression (0.3) + ML (0.3) → Avg prob = 0.28. At odds 3.5, EV = (0.28 × 2.5) - 1 = 0.7 - 1 = -0.3.

## Additional Notes
- **EV Variations:** Absolute EV = (Prob × Payout) - Stake; Kelly for sizing.
- **Validation:** Backtest on historical races; aim for positive ROI.
- **Data Quality:** Ensure ≥65% completeness; impute missing values.
- **Tools Recommendation:** Python (pandas, scikit-learn) for implementation.

This elaboration draws from standard betting analytics and can be refined with your project's historical data.