---
title: "Analysis of "HFT: Can Python execute faster than C#?" by Liam Pauling"
aliases: ["Analysis of "HFT: Can Python execute faster than C#?" by Liam Pauling"]
type: post
tags: [automation, fsharp, horse-racing, mcp, post, python, trading]
---

# Analysis of "HFT: Can Python execute faster than C#?" by Liam Pauling

**Original article:** [HFT: Can python execute faster than C#?](https://betcode.substack.com/p/hft-can-python-execute-faster-than)

---

## 1. My Comment: Key Points
- My original discussion with Liam was about language performance, not about comparing different applications. I specifically asked: How could Python serialization/deserialization be quicker than any .NET implementation, since we all use the Betfair API?
- Python is slower and uses more memory for object and attribute access compared to C#/F#, because it handles these at runtime instead of compile time. Even with Python optimizations to reduce memory usage, it cannot match the efficiency of statically typed, compiled languages.
- In C#/F#, the compiler resolves memory layout and property access at compile time, resulting in faster, more predictable performance.
- Real-world code: Python took 10s, F# took 3s for the same task.
- In my discussion with Liam, I explained that my comparison was not based on custom code, but on running the same AI agent task using both Python and C# AI agent libraries. The task was designed to simulate Betfair API interaction, using a prompt that exercised MCP tools in both environments.
- Any fair performance comparison must control for concurrency and Betfair server latency. If tests are not run at the same time, results can be skewed by fluctuating network conditions, server load, and the volume of bets being processed by other users.
- Betfair's matching engine processes thousands of bets every 50ms, and its internal processing time is typically a few milliseconds, but total latency can vary due to network, queuing, and market load.
- Without concurrent testing on the same market, from the same machine and network, results are not directly comparable and may be misleading.

---

## 2. Liam Pauling's Article: Key Points
- Although the discussion started about language performance, Liam shifted the focus to comparing the BetAngel app (BA) and the Flumine app, rather than directly comparing Python and C# or .NET language performance.
- The test compared Python (flumine+betfairlightweight) to BetAngel Automation (BA), a desktop trading app (likely written in C# or similar).
- Both ran on the same machine/account/network, with similar refresh intervals and streaming settings.
- The test measured bet placement latency using Betfair's order stream and betId sequencing.
- Result: Python/flumine was faster >95% of the time, with BA showing a longer latency tail.
- Liam notes that flumine is event-driven and purpose-built for execution, while BA is a full-featured trading package with automation as a layer on top.
- He explicitly states: "Do I believe python is faster than C#? No."

---

## 3. Analysis: Where You Are Right
- **Attribute Access:** Your technical explanation of Python's dynamic attribute access vs. C#/F#'s static, compiled access is correct. Python's dynamic nature incurs overhead that statically typed languages avoid. Even with Python optimizations to reduce memory usage, it cannot match the efficiency of C#/F#.
- **General Performance:** For equivalent code, C#/F# will almost always outperform Python in raw execution speed and memory efficiency, especially for attribute-heavy or tight-loop code.
- **Testing Validity Caveat:** You are also correct to question the validity of any performance comparison that does not control for concurrency and Betfair server latency. If tests are not run at the same time, results can be skewed by fluctuating network conditions, server load, and the volume of bets being processed by other users. For a fair comparison, both systems should place bets on the same market at the exact same time, ideally from the same machine and network.
- **Language vs. App Comparison:** Your original point was about the underlying language and serialization/deserialization performance, not about comparing two different trading applications. This distinction is important and was lost when the discussion shifted to app-level comparisons.

---

## 4. Analysis: Where Liam Is Right
- **Test Scope:** Liam's test does not claim Python is inherently faster than C#. He compares a Python event-driven framework (flumine) to a desktop trading app (BA) with automation scripting. The result is that flumine is faster in this specific, practical scenario.
- **Framework vs. Language:** The test is not a language benchmark but a comparison of two trading solutions. BA's automation is not optimized for HFT, while flumine is purpose-built for low-latency execution.
- **Caveats:** Liam is clear that the result is due to architecture, not language speed. He acknowledges that a C# or F# solution designed for HFT would likely be faster.
- **Testing Limitation:** However, his results are only meaningful if both systems were tested concurrently. If not, differences in Betfair server latency, network conditions, or market load could explain the observed performance differences. Without strict control of these variables, the comparison cannot be considered definitive.

---

## 5. Methodology Critique
- **Not a Language Benchmark:** Comparing Python/flumine to BA Automation is not a fair language comparison. BA is a closed-source, feature-rich desktop app, not a minimal C#/F# HFT engine.
- **Lack of C#/F# Reference:** As you noted, there are open-source C#/F# Betfair libraries (e.g., BetfairSharp, BetfairNG, etc.) that could be used for a more direct comparison. Liam did not include these in his test.
- **Black Box:** Without access to BA's source or API, it's impossible to know where the latency comes from (UI, automation engine, network, etc.).
- **Best Practices:** Liam did try to use each system as intended, but the comparison is more about frameworks than languages.

---

## 6. My Opinion and Additional Notes
Another critical factor is whether the tests were run concurrently. If the Python and BA tests were not executed at the same time, the results are not directly comparable. Betfair server latency can fluctuate significantly due to network conditions, server load, and the volume of bets being processed at any given moment. The Betfair API is used by thousands of users simultaneously, and even within a 50ms window, the internal state and queue of the matching engine can change.

If one test is run after the other, differences in network latency, server load, or the number of concurrent bets from other users can skew the results. For a fair comparison, both systems should place bets on the same market at the exact same time, ideally from the same machine and network.

### Betfair Matching Engine Internal Processing Time
The internal processing time of the Betfair matching engine is not publicly documented in detail, but it is known to be extremely fast—typically on the order of a few milliseconds under normal conditions. However, the total round-trip latency (from sending a bet to receiving confirmation) can be affected by:
- Network latency between client and Betfair servers
- Betfair's own queuing and rate-limiting
- The volume of bets being processed globally
- Market-specific load (e.g., popular races or events)

Therefore, any test that does not control for these variables (especially concurrency) cannot be considered a definitive performance comparison between frameworks or languages.

---

## 7. Summary
- You are correct on language-level performance.
- While Liam claims that, in his test, Python/flumine outperformed BA Automation, this result is not verifiable or testable by others, as he did not provide code, raw data, or automation files. Therefore, his claim cannot be independently confirmed and should be treated as anecdotal rather than evidence-based. This does not generalize to Python vs. C#/F# language performance.
- The methodology is not a fair language comparison; a direct test with open-source C#/F# Betfair libraries would be more meaningful.
- However, it is important to note that Liam's results are not fully verifiable or reproducible, as he did not provide raw data, code, or automation files. In contrast, when I tested language performance, I provided two open code samples for transparency and reproducibility:
    - Python: [agentTest.py](https://github.com/StefanBelo/BetfairAiTrading/blob/main/src/AiAgentPython/agentTest.py)
    - C#: [Program.cs](https://github.com/StefanBelo/BetfairAiTrading/blob/main/src/AiAgentCSharp/Program.cs)
- My tests used the same AI agent task in both languages, simulating Betfair API interaction, and demonstrated a clear performance difference (Python: 10s, F#: 3s) under controlled conditions. This approach allows others to review, reproduce, and verify the results, unlike the black-box comparison in Liam's article.
