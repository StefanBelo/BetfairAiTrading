---
title: Community Discussion, What are you testing in Betfair AI Trading?
date: 2026-08-19
tags: [betfair, ai, strategy, community]
status: discussion
---

# 🧠 Strategy Show & Tell: What's Working (or Failing!) on the Exchange?

Hey everyone! 👋

It feels like we all have a unique edge or a fascinating corner of the market to exploit. The sheer complexity and speed of Betfair make it an endless playground for quantitative strategies, which is both exciting and overwhelming sometimes!

I wanted to open up a discussion: **What kind of betting strategies are you currently working on, testing, or refining?** I'm genuinely curious about what approaches others in the community are finding success with right now.

### My Current Focus (A Peek Behind the Curtain)
For the last couple of weeks, my primary focus has been on a strategy that relies almost exclusively on **pure market price dynamics**. Instead of incorporating external factors or complex bookmaker biases, I'm building a system that:

1.  Calculates signals across a wide array of metrics—we're talking about tracking and scoring over 16+ distinct market parameters (from liquidity depth to micro-price deviations).
2.  These individual signals are then fed into a **composite scoring mechanism**. This score acts as the primary trigger, determining when the confluence of these technical indicators suggests an optimal bet entry point.

It's all about finding patterns in the *movement* itself, rather than just the static odds. It’s complex, and I'm still tuning the weights!

### 💡 Your Turn: What are you testing?
Are you deep into:
*   **Volume/Liquidity Analysis?** (e.g., tracking specific order book imbalances)
*   **Time-Series Modeling?** (e.g., using advanced ARIMA or LSTM models on historical odds?)
*   **Behavioral Finance Angles?** (e.g., modeling crowd psychology or overreaction?)
*   **Or something completely different?**

Drop your strategies, concepts, or even just the *type* of data you are focusing on below! Let's share knowledge and help each other avoid common pitfalls. 👇

### 🤖 The Agentic Loop: Data Context & Live Testing
Beyond pure signal generation, my current development focus is heavily on the *data pipeline* itself—what I call the **Data Context**.

Because the system is agentic, it allows me to retrieve and synthesize all kinds of real-time data streams for AI models. This means I can prompt an AI agent to:
1.  Generate a runnable script tailored for the live trading session (e.g., fetching specific order book snapshots or historical volatility metrics).
2.  Execute that script in a controlled environment, generating a rich "data context."
3.  Feed this entire data context back into the AI model for deep analysis and feedback on my existing algorithms.

This iterative loop—**Data Retrieval $\rightarrow$ Script Generation $\rightarrow$ Contextual Analysis $\rightarrow$ Algorithm Fine-Tuning**—is where I feel the most significant gains are coming from.

I'm also building out a dedicated Notebook environment that connects directly to the running application, allowing me to visualize and chart these diverse data streams in real time, which is crucial for validating model assumptions visually.