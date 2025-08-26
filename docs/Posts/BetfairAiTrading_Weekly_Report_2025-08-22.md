## Betfair AI Trading Weekly Report (34)

### Summary

This week's discussion compared two common approaches to finding trading edges: (A) idea-driven signal discovery followed by quick validation and lightweight backtests, and (B) model-driven research where a fundamental or temporal model is developed first and strategies are derived from what the model does best. Participants described recent shifts toward faster, signal-first experimentation enabled by easier tooling and multi-horizon forecasting advances.

### Positive reactions (generalised)

- Appreciation for quick, pragmatic workflows: small, testable ideas that can be implemented and backtested in hours rather than weeks.
- Increased motivation and renewed interest from embracing newer tools and skills (temporal models, Python interop, multi-horizon forecasting).
- Recognition that both approaches have merit; experience with multiple methodologies is valuable.
- Community value in sharing perspectives: different views on markets and signals lead to distinct edges and unexpected gains.

### Negative reactions / concerns (generalised)

- Risk of overfitting or chasing short-lived signals when favouring fast idea-to-backtest cycles.
- Boredom or loss of progress when one methodology stalls (e.g., long barren patches after heavy model-building).
- Communication friction: difficulty conveying nuanced ideas without being co-located (which reduces transfer of tacit knowledge).
- Debate on how to characterise price dynamics (e.g., single-market vs. aggregate behavior) exposes differing intuitions that can slow consensus-building.

### Core methodology question — described approach

The current, described core methodology is primarily idea-first and pragmatic:

- Generate a simple, testable hypothesis or signal that might indicate positive expectation.
- Attempt a very quick data check or "sanity" test (for example, a quick backtest on available BSP or coarse data) to see if the idea is immediately falsified.
- If the signal survives the quick check and is straightforward to implement, write the strategy and run a proper backtest — the turnaround is typically short (implement in ~1 hour; backtest in ~20 minutes for simple ideas).
- This contrasts with an earlier, model-first practice where a comprehensive fundamental model was built first and then used to identify positive-EV opportunities.

In short: idea → fast validation → rapid implementation/backtest. Model-first work still has a place, but it tends to be more time-consuming and is now often reserved for problems that truly need deep temporal modeling or when the signal-engineering route fails.

### Notes on multi-horizon forecasting and feature engineering

- Multi-horizon forecasting has improved; short-horizon predictions (e.g., 0–20s) can be produced from earlier timestamps, but operationalising them often reduces to heavy feature engineering and hyperparameter tuning.
- When forecasting becomes highly performant, strategy design can become a byproduct of model strengths (i.e., models suggest entry/timing rules), rather than the other way around. That can be effective, but it pushes work toward engineering and tuning rather than strategy creativity.

### Practical takeaways and recommendations

- Use a two-track workflow: quick signal testing for high-throughput exploration, and a model-first track for promising, structural problems that justify longer investment.
- Prioritise simple, falsifiable signals that can be implemented and tested rapidly to avoid wasted effort.
- Preserve disciplined evaluation: ensure quick tests use appropriate holdouts and sanity checks to reduce false positives.
- Share methods and tooling notes (interop patterns, data slices used) to reduce duplicated effort across the group.

### Next steps

- Continue logging short experiments (idea, quick-test result, full-backtest outcome) so the team can compare signal-first and model-first workflows empirically.
- For promising temporal/model-driven efforts, allocate a dedicated timebox for feature engineering and hyperparameter search rather than ad-hoc work.

---

This report condenses themes and viewpoints expressed in the recent conversation and recommends a pragmatic hybrid workflow: fast idea validation plus selective deep modeling.
