# Betfair AI Trading Weekly Report (36)

### Live Betting Models for Next-Goal Markets

**Ever wondered if a few-second odds lag can be exploited in live betting?**

Short summary
The conversation explored building live models for next-goal markets in soccer by detecting real-time momentum shifts (attacking bursts, shot frequency, corner pressure). The aim is to capture odds mispricing during short windows where market prices lag observable game signals. Approaches discussed include manual tracking, scraping live feeds, time-series anomaly detection, and automated alerting.

Positive reactions
- Interest in the technical idea and practical potential to capture short-lived value.  
- Shared prototypes and early projects (Discord alerts, scraping pipelines).  
- Examples of people getting usable live signals after months of careful data work.

Negative reactions
- Data is hard: feed delays, API limits, and obtaining truly real-time signals.  
- Competing against professional syndicates and bookmakers with faster infrastructure.  
- Human bias risk when evaluating live footage (confirmation bias) and scaling issues.

Opinion & recommendations
- Short-window edges exist but are fragile. Prioritize data latency and throughput first.  
- Start small: build a reliable low-latency pipeline (one sport, one league), validate signals offline, then run small-scale live tests.  
- Combine automated signals with strict execution rules (max bet size, latency cap, stop conditions) and rigorous logging for backtests.

Actionable next steps
1. Prototype: collect synchronized timestamps for event feeds vs. bookmaker updates for 50 matches.  
2. Measure: quantify average feed lag and hit-rate for simple momentum features.  
3. Pilot: run a disciplined, small-money live pilot with automated alerts and forced cooldowns.

Question for readers
What’s one low-latency data source you trust for live match events, and why?
