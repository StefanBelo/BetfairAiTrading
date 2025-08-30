# F# Programmers & LLMs: What's Your Experience? 🤖

Following up on my recent [F# bot generation experiment](https://www.reddit.com/r/BetfairAiTrading/comments/1n3cbjd/from_one_prompt_to_8_f_bot_variants_ai_code/) where I tested 4 different AI models to generate F# trading bots, I'm curious about the broader F# community's experience with LLMs.

## My Quick Findings 📊

From testing DeepSeek, Claude, Grok, and GPT-5 on the same F# bot specification, I got wildly different approaches:

- **DeepSeek**: Loved functional approaches with immutable state
- **Claude**: Generated rich telemetry and explicit state transitions  
- **Grok**: Focused on production-lean code with performance optimizations
- **GPT-5**: Delivered stable ordering logic and advanced error handling

Each had different "personalities" for F# code generation, but all produced working solutions.

## Questions for F# Devs 🤔

**Which LLMs are you using for F# development?**
- Are you sticking with one model or mixing multiple?
- Any standout experiences (good or bad)?

**F# Coding Style Preferences:**
- Which models seem to "get" the F# functional paradigm best?
- Do any generate more idiomatic F# than others?
- How do they handle F# pattern matching, computation expressions, etc.?

**Practical Development Workflow:**
- Are you using LLMs for initial scaffolding, debugging, or full development?
- How do you handle the inevitable API mismatches and edge cases?
- Any models particularly good at F# type inference and domain modeling?

## My Current Stack 🛠️

Right now I'm bouncing between:
- **Claude** for complex logic (great at understanding domain requirements)
- **DeepSeek** for functional patterns (surprisingly good at F# idioms)
- **GPT-5** for production polish (excellent error handling)

But I'm always looking to optimize this workflow.

## The Multi-Model Approach 💡

One big takeaway from my experiment: using multiple models for the same task revealed bugs and edge cases that single-model development missed. Each AI caught different issues:

- Semantic ambiguities in my specifications
- Different interpretations of the same API
- Varying approaches to error handling and state management

**Are any of you doing cross-model validation?** Or am I overthinking this?

## Looking Forward 🚀

The F# ecosystem feels like it's in a sweet spot for AI-assisted development - functional paradigms seem to translate well to LLM understanding, but there's still room for improvement in idiomatic code generation.

**What's your take on the current state of LLMs for F# development?** 

Any models I should be testing? Workflows that have worked particularly well? Horror stories to avoid?

---

*Context: This follows my recent experiment generating 8 different F# bot variants from a single specification using 4 different AI models. Full details in [the original post](https://www.reddit.com/r/BetfairAiTrading/comments/1n3cbjd/from_one_prompt_to_8_f_bot_variants_ai_code/).*
