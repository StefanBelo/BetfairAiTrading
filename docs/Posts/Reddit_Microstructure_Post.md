---
title: "Reddit Post Draft: Trading the Tape on Betfair"
aliases: ["Reddit Post Draft: Trading the Tape on Betfair"]
type: post
tags: [betfair, market-microstructure, reddit, community, fsharp]
status: draft
created: 2026-05-06
---

# Reddit Post Draft: Trading the Tape on Betfair

**Title: Anyone else trading Betfair using purely Microstructure / Traded Price History?**

**Body:**

I’ve spent the last few days moving away from traditional "Form-based" horse racing analysis and diving deep into **Market Microstructure**. Specifically, I’m building an engine in F# that ignores the horses entirely and focuses purely on the physics of the Betfair Matching Engine.

The goal is to identify "Syndicate Signatures" by analyzing the **Historic Traded Price Data** (the sequence of every match, not just the current LTP).

Here is the general architecture I’m exploring:

1.  **Defining the Anchor:** Using a combination of Volume Weighted Average Price (VWAP) and Volume Profile Nodes to find where the "Fair Value" actually sits. 
2.  **Detection of Aggressive Capital:** Implementing algorithms to catch "Price Sweeps"—where a single participant clears 3-5+ price levels in a single 50ms matching cycle.
3.  **The Probability vs. Tick Dilemma:** I've moved all my internal logic to Implied Probability (%) rather than Price Ticks. It seems to be the only way to get a consistent "Edge" that doesn't fall apart when a horse moves across the 4.0 boundary, where the tick size jumps from **0.05** to **0.10**.
4.  **Initiator Inference:** Looking at the sequencing of matches within a second to determine if the "Aggressor" was a Backer (pushing the price down) or a Layer (pushing it up), and then slipstreaming behind that momentum.

**My Question to the Community:**

Has anyone else successfully implemented a strategy that ignores the "Horse" and just "Trades the Tape"? 

I’m finding that the biggest hurdle isn't the data, but the "Skepticism" needed to filter out liquidity gaps. A price sweep on low volume is usually just noise, but a sweep backed by a volume burst is a very different animal.

*   How do you guys differentiate between a genuine syndicate move and a simple lack of liquidity? 
*   Do you find that anchors like VWAP still hold weight in the final 2 minutes before the off?

Looking forward to hearing from anyone else playing in the "Market Physics" space!

***

*Tags: #Betfair #AlgorithmicTrading #MarketMicrostructure #FSharp #TradingTech*
