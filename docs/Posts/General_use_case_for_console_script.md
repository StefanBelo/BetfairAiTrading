# 🤖 Automating Data Analysis with Console Scripts in BFExplorer!

Hey r/\[CommunityName]! 👋

I wanted to share a quick update on a new capability I've integrated into my bfexplorer agentic trading app: **"Execute Console Script."**

This tool is a game-changer because it gives the AI agent full autonomy to run complex data analysis scripts directly against live, running strategy data within the bfexplorer console. Previously, this required manual steps; now, the agent can handle it end-to-end!

### 🚀 What does this mean for users?
Instead of me having to manually activate markets, pull contexts, and execute multiple scripts one by one, the AI agent can now:
1.  **Self-Service Analysis:** Automatically run a sequence of data analysis scripts on a given market or set of historical data.
2.  **Context Aggregation:** Pull together all necessary stored contexts (like trigger events and score data) from these multiple runs into one cohesive report.
3.  **Deep Insights:** Provide a comprehensive, actionable summary that points out exactly where the system succeeded and, more importantly, where it failed or missed opportunities.

### 💡 Use Case Scenario:
Imagine needing to debug why a strategy fired too many signals during a volatile period. Instead of me running five different scripts (one for momentum, one for volume, etc.), the agent can run them all sequentially via this tool and give you *the combined story*—highlighting which signal was responsible for the over-triggering or which profitable move was completely missed.

It moves us from a "manual debugging" workflow to a fully **autonomous diagnostic** process!

*(Source: Internal bfexplorer agentic app development)*
![ExecuteConsoleScript](/docs/Automation/images/ExecuteConsoleScript.png)