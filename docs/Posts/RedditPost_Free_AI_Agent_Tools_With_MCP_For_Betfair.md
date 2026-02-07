## Free AI Agent Tools (Free Tiers) You Can Use for Betfair AI Trading + MCP

If you’re building Betfair AI trading workflows, the biggest unlock right now is using an **AI agent UI/CLI** that can call an **MCP server** (so the model can pull live market context and optionally trigger actions).

I’m especially interested in tools that:
- have a **free LLM tier** (at least for light usage), and
- can act as an **MCP client** (or can be wired up to one without too much glue).

Below is a practical list. Please add what you’re using.

---

### 1) Tools I’m seeing people use (free tier + good “agent” UX)

**Claude Desktop (Anthropic)**
- Why it’s interesting: one of the cleanest “desktop agent” experiences.
- MCP: **supports MCP** (you can connect to local MCP servers and expose tools).
- Good for: interactive research, strategy prototyping, tool-driven workflows.
- Watch-outs: free tiers often have limits (rate/usage/capabilities can change over time).

**Antigravity**
- Why it’s interesting: a lightweight “agent shell” people are using for tool-driven flows.
- MCP: if you’re using it with MCP, share your config/approach in the comments (there are a few variants/forks floating around and I don’t want to claim the wrong integration path).
- Good for: quick experimentation with tool calling and local services.

**Gemini CLI**
- Why it’s interesting: fast terminal workflow; good for repeatable prompts.
- MCP: can be configured as an MCP client; I’ve shared a setup write-up previously in this repo.
- Good for: batch-ish evaluation, scripted agent loops.

**Qwen Code (CLI)**
- Why it’s interesting: a CLI-first agent workflow that many devs find efficient.
- MCP: if you’re using it with MCP, I’d love to see the pattern (native support vs wrapper/proxy vs custom tool adapter).
- Good for: terminal-driven strategy development and rapid iteration.

---

### 2) “Free” options that are solid, but usually mean BYO model

These are often free to run as apps, but you’ll either:
- connect to a local model (free as in no API bill, but you pay in GPU/CPU), or
- bring your own API key (not a free LLM tier).

**Cline (VS Code extension)**
- Why it’s interesting: strong agent workflow inside VS Code; good at multi-step tasks.
- MCP: **supports MCP servers** (so it can call your tooling cleanly).
- Model access: depends on what you connect it to (hosted or local).

**Ollama (local models)**
- Why it’s interesting: fast local inference; great for short-latency “thinking” loops.
- MCP: not an MCP client by itself, but easy to use as the model behind an MCP-enabled agent.
- Good for: cheap iteration, privacy, offline resilience.

---

### 3) How this fits Betfair AI trading (simple mental model)

An MCP-enabled agent can do a loop like:
1) Call MCP tools to pull **market + selection context** (prices, volumes, time-to-off, metadata).
2) Produce a **structured decision** (JSON) like: strategy name, entry price, exit rules, risk flags.
3) Optional: call an execution tool (or queue it for manual confirmation).

Key idea: your “edge” should still come from your **strategy + risk gates + evaluation**, not from the UI. But the UI matters because it determines how quickly you can test, iterate, and keep logs.

---

### Questions for the community

1) What AI agent app/CLI are you using right now for tool-driven workflows?
2) Which ones have a genuinely usable **free tier** (not just a short trial)?
3) If you’re using MCP: what’s your cleanest pattern for connecting an agent to your MCP server?
4) Do you prefer **desktop UIs** (Claude Desktop style) or **CLI** (Gemini/Qwen style) for trading research and iteration?

Drop your stack + the reason you chose it.
