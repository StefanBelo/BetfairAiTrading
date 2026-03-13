# Research: Sectional Times and Margins (Alan Potts)

This note summarises ideas gleaned from the Simon Nott article "Professional Punter Alan Potts on Sectional Times and Margins" and suggests potential betting strategy concepts.

## Key Observations from Potts

1. **Tiny margins matter**  
   - Potts compares racing to Olympic sprint events: winners are often separated by fractions of a second. A flat race is frequently won by less than 1 second (≈6 lengths).  
   - Even differences of 0.1 – 0.3 seconds (a length or two) can be pivotal across a multi‑minute contest.

2. **Consistency of outcomes**  
   - Re‑runs of the same horses in identical conditions often produce almost identical results, despite changes in weight, jockey, draw, ground, etc.  
   - He cites a Wolverhampton example where two horses met twice two years apart; the winning margins and race dynamics were nearly the same.

3. **Sectional times as descriptive tools**  
   - Modern availability of sectional timing makes it possible to see how a race was run.  
   - Potts is sceptical that comparing sectionals between races reliably finds winners, but they provide context for *why* a horse won or lost.

4. **Pace in the first furlong is crucial**  
   - In the 2022 1,000 Guineas, only three runners clocked <15 sec for the opening furlong; the top two finished first and second.  
   - A fast start can steal the race, but if the early speed is excessive the horse may tire.  
   - Identifying horses likely to lead early and sustain pace (without over‑cooking it) is a challenge.

5. **Practical limitations of time‑based margins**  
   - Race margins are usually expressed in lengths; converting to hundredths of a second is abstract and hard to visualise.  
   - Potts suggests sectionals are most useful for evaluating past performances rather than predicting future ones.

## Strategy Ideas

Based on Potts's insights, here are a few avenues worth exploring:

1. **First‑furlong pace profiling**  
   - Build a database of horses' early furlong splits and track whether they habitually go under certain thresholds (14.5, 15.0s etc.).  
   - Combine this with draw/track bias and jockey styles to estimate the probability each runner will be among the leaders.  
   - Generate a *pace‑pressure* factor to adjust win probability or to identify over‑priced front‑runners.

2. **Sectional consistency metrics**  
   - Measure how consistent each horse's sectional pattern is across its last N runs.  
   - Horses with very stable patterns might be priced more accurately; look for horses that deviate and use the deviation to spot value (i.e. a horse that usually sits midfield but has an anomalously fast opening furlong at good odds).

3. **Margin‑based probability calibration**  
   - Use tiny time margins to calibrate bookmakers' implicit probabilities.  
   - For example, check races where the winner prevailed by <0.2 s and see if the favourite's implied win rate was significantly higher than the actual percentage of such tight wins.  
   - This could lead to a *margin adjustment factor* applied to expected return models.

4. **Race‑by‑race descriptive models**  
   - Since Potts argues sectionals describe how a race was run, create automated reports after each race: identify which runner improved/declined relative to the field via sectionals.  
   - Feed these signals into a predictive model as features (e.g. `late_speed_delta`, `early_speed_delta`).

5. **Spotting conditional surface/ground effects**  
   - The Eagle Court / Vega Sicilia example shows ground affecting how speed converts to winning.  
   - Use sectionals across different ground conditions for the same horse to estimate its fatigue curve and adjust price expectations accordingly.

6. **Time‑margin visualisation tools**  
   - Build charts that convert lengths to hundredths of seconds and display the evolution of a race in that scale; this could help model trainers'/jockeys' pace strategies quantitatively.

## Next Steps

- Extract sectional data from race tapes within the workspace (existing datasets in `data/` may help).  
- Prototype code to compute early‑furlong speed profiles and consistency metrics.  
- Run back‑tests to see if first‑furlong leaders at longer odds win more often than the market implies.  
- Integrate descriptive sectional features into ML models already used elsewhere in the repo.

---

This document sits in **docs/Ideas** and can be updated as research progresses. It is intended as a foothold for strategy development centered on small timing margins and depth‑of‑pace analysis.