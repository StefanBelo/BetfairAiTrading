# Real-World Test: Python vs C# Performance for Betfair AI Agents

## Introduction

Performance debates between Python and C# are common, but meaningful comparisons require fair, transparent methodology. After reading Liam Pauling’s article “HFT: Can Python execute faster than C#?” and our discussion, I set out to objectively test language performance for a real Betfair AI agent task—using open code and reproducible results.

## How Did I Test Python and C# Performance?

I already wrote about Liam’s blog post and really tried to be objective, mentioning anything positive. Since our discussion was mainly about comparing programming language performance—as I mentioned several times—what he did was a very strange performance test with his python code and desktop application. What I was emphasizing in my claims was the fact that, for similar task execution, Python will always lose because of differences in performance between dynamically typed, interpreted languages and statically typed, compiled languages. What Liam did was not a code comparison at all. There are quite a few different libraries in C# or F# for the Betfair API, but he did not use any of them to compare code execution performance.

My approach was different: I used two very popular code libraries for AI agent development—one for Python and one for C#. I tested them using the Github Copilot LLM provider and also on my local machine with LM Studio – Local LLM Service.

I provided the code for testing so anyone could replicate my results. The screenshot below shows the results from my test: Python always lost in the same way, even if Liam had used the correct methodology for testing.

You can review and run the exact code I used for these tests:
	- Python: [agentTest.py](https://github.com/StefanBelo/BetfairAiTrading/blob/main/src/AiAgentPython/agentTest.py)
	- C#: [Program.cs](https://github.com/StefanBelo/BetfairAiTrading/blob/main/src/AiAgentCSharp/Program.cs)

For instance, my Betfair code implements wrappers for both the Betfair REST and streaming APIs, as well as domain application services that any desktop application would implement.

---

![Python vs C# Betfair AI Agent Performance Test](/docs/Posts/images/AiAgentPerformanceTest.png)

*In the screenshot: .NET code execution is shown in the Command Prompt window (top right), Python test is in the TERMINAL panel in Visual Studio Code (bottom).*

---

## My Opinion: Methodology Matters

My test focused on language performance, not application-level differences. I used equivalent AI agent tasks, the same Betfair API interaction, and provided all code for transparency and reproducibility. Anyone can review or rerun my tests. This is the only way to make a fair, evidence-based comparison.

Liam’s test, by contrast, compared a Python event-driven framework (flumine) to a full-featured desktop trading app (BetAngel Automation), not the languages themselves. He did not use any open-source C# or F# Betfair libraries, nor did he provide code or raw data. As a result, his findings are anecdotal and not verifiable or generalizable to language performance.

When discussing programming language performance, only direct, code-level comparisons using open, reproducible tests are meaningful. My results confirm what most developers expect: for the same task, C# (or F#) will always outperform Python due to fundamental differences in language design and execution.

---

*See also: [Full analysis and discussion of Liam’s article and methodology](HFT_Can_Python_Execute_Faster_Than_CSharp_Analysis.md)*
