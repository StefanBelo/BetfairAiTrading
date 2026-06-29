---
title: "Volatility Term Structure Arbitrage — Concept and Betfair Trading Notes"
aliases: ["Volatility Term Structure Arbitrage — Concept and Betfair Trading Notes"]
type: idea
date: 2026-05-06
tags: [automation, bfexplorer, trading, research, volatility, strategy]
source: "https://valuelytica.substack.com/p/volatility-term-structure-arbitrage"
---

# Volatility Term Structure Arbitrage — Concept and Betfair Trading Notes

**Source:** Valuelytica Research, "Volatility Term Structure Arbitrage" (April 17, 2026)

## Summary

This post describes a trade that exploits the slope of the VIX futures curve rather than trying to predict market direction. The main idea is:

- The front of the VIX curve is usually cheaper and decays faster than the mid-term portion.
- Short-term volatility products like VIXY suffer a large negative roll yield because they roll into more expensive future contracts frequently.
- Mid-term products like VIXM roll less often and therefore lose less to roll decay.
- By shorting the short-term product and going long the mid-term product, a trader can capture the spread in decay rates while reducing directional exposure.

The strategy is presented as a term-structure spread rather than a naked short volatility position.

## Core points from the article

- The primary edge is roll decay differential, not being long or short volatility outright.
- A beta-neutral construction is essential to isolate the term-structure inefficiency from broad market moves.
- Using ETFs such as VIXY and VIXM is one practical implementation of the concept.
- The relationship between the two instruments is strong, but the hedge ratio changes in crises.
- A fixed hedge ratio is a simplification and can be dangerous during tail events; this is labeled "beta convexity." 
- Capital allocation must be disciplined: the strategy is aggressive, and the article suggests a maximum allocation around 50% because of comparable volatility to equity markets.
- Performance from 2019–2025 shows good Sharpe ratios and increasing returns with allocation, but the figures are gross and exclude transaction costs, slippage, and shorting costs.
- The most valuable further work is a dynamic hedge ratio to address regime shifts when the front-end of the curve decouples.

## Key takeaways

- Exploit decay rather than prediction: short the faster-decaying front-end and long the slower-decaying mid-term exposure.
- Beta-neutrality is the concept that makes the trade a relative-value strategy, not a directional one.
- Constant hedging is easy to model but risky; dynamic hedging is required for tail-event robustness.
- The strategy is a structural volatility arbitrage trade aligned with volatility risk premia harvesting.

## Betfair exchange applicability

This idea is not directly transferable to Betfair because Betfair does not offer VIX futures or term-structure futures products. However, the concept can still inspire exchange-market approaches:

- **Relative-value trades:** Look for pairs of correlated markets where short-term implied risk is rich relative to longer-term expectation. The objective is to profit from the spread, not from the underlying outcome.
- **Cross-market curve analogies:** Use the idea of a price curve across related Betfair markets, such as:
  - consecutive races on the same card,
  - short-term live odds versus pre-off odds,
  - correlated markets on the same event (match odds vs. total goals, first-half vs. full-time, etc.).
- **Synthetic volatility premium:** On Betfair, selling short-term probability risk while hedging with longer-term exposure could mimic the role of shorting VIXY and buying VIXM.
- **Dynamic hedge ratio:** Use historical co-movement between paired markets to derive a hedge ratio, and adapt it if the price relationship changes.
- **Market decay analog:** The analog of roll decay on Betfair is the systematic drift in prices caused by short-term money pressure or market maker behaviour. Trading the relative drift between two connected markets may capture a similar premium.

## Possible Betfair research directions

- Test whether a pair trade between related markets can be constructed so the combined position is less sensitive to broad market moves.
- Model a hedge ratio between adjacent market odds or related event probabilities, then trade deviations from that relationship.
- Evaluate how frequently the short-term market “decays” relative to the longer-term market and whether that decay is persistent enough to harvest.
- Consider in-play versus pre-off price structure as a crude term curve: if in-play reaction is excessively volatile, a hedged spread may capture mean-reversion.

## Risks and caveats for Betfair use

- Betfair liquidity and commission change the edge calculus compared to futures and ETFs.
- There is no exact volatility term-structure instrument, so the strategy must be adapted to broader relative-value or dispersion themes.
- Tail events can break correlations quickly, so any hedge ratio must be stress-tested for regime shifts.
- Execution costs and market impact are likely higher in Betfair microstructures than in liquid VIX ETFs.

## Conclusion

The article provides a strong conceptual framework: harvest structure in a risk premium curve while minimizing directional exposure. On Betfair, the useful part is the mindset of trading spreads and relative value, not the specific VIX futures trade. A Betfair implementation would need to identify a reliable curved relationship between related market prices and build a dynamically hedged spread around it.
