---
title: "Signal In The Wires — Liam Pauling"
aliases: ["Signal In The Wires"]
type: idea
tags: [automation, market-data, latency, trading, betfair, research]
---

# Signal In The Wires — Liam Pauling

Original article: https://betcode.substack.com/p/signal-in-the-wires?triedRedirect=true

## Summary

Liam Pauling describes a practical approach to breaking through latency walls by stepping back and searching for a faster information source upstream rather than simply trying to beat competitors with incremental execution speed.

The article focuses on an ATR horse racing tip feed, where the author finds a faster signal by inspecting the ATR iOS app traffic and locating a `twoup.io` JSON API endpoint. This endpoint appears to publish updates earlier than the rendered HTML page, giving a multi-second edge relative to the Betfair market move.

## Key takeaways

- The biggest edge may come from finding a better source of the same signal, not from chasing every millisecond in the existing pipeline.
- The ATR website HTML updates are slower than the underlying API calls used by the app.
- The author identified multiple possible endpoints:
  - `api.atr.k8s`
  - `api.atr.stg`
  - `api.atr.prod`
- Timing logs showed the upstream API source could be 10+ seconds ahead of the Betfair move.
- There does not appear to be a consistent fastest subdomain; a resilient strategy should monitor multiple endpoints and use whichever updates first.

## Process described

1. Recognize the problem as an information availability wall rather than a pure betting or edge problem.
2. Review the target site and infer it is a CMS-backed page.
3. Download the ATR mobile app and inspect network traffic with a proxy.
4. Discover a JSON API from the app that may publish content earlier than the public HTML page.
5. Probe related subdomains and measure update latency against the actual market move.
6. Use the app-derived signal as a faster feed, even if automation skills are required.

## Implications for BetfairAiTrading

- Investigate upstream and app-level APIs for other data sources rather than relying only on HTML or published feeds.
- Build a multi-source feed monitor that can choose the earliest reliable update.
- Record and compare latency from alternate sources to market move times to quantify value.
- Treat edge discovery as an "information engineering" problem, not just a latency race.
- Use this case as an example: sometimes the best improvement is a new signal path, not a faster execution strategy.

## Note

The author also recommends the book *Ghost In The Wires* as additional reading for readers interested in unconventional problem-solving and exploring systems from different angles.
