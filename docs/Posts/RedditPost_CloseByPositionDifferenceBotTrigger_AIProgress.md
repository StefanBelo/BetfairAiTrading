# Reddit Post: From One Prompt to 8 F# Bot Variants - AI Code Generation Experiment

## Post Title Options:
1. "🤖 I Fed the Same F# Trading Bot Spec to 4 AI Models - Here's What Each Generated"
2. "From Single Prompt to 8 Working F# Variants: AI Code Generation Battle Royale"
3. "AI Code Generation Experiment: 4 Models, 1 Betfair Bot Spec, Surprising Results"

## Reddit Post Content:

**Platform**: r/fsharp, r/MachineLearning, r/algotrading, r/programming

---

### Post Body:

**TL;DR: Gave the same F# trading bot specification to 4 different AI models. Got 8 working variants with wildly different architectures. Cross-model comparison revealed subtle bugs that single-model development would have missed.**

Just wrapped up an interesting experiment in my Betfair automation work and wanted to share the results with the dev community.

## The Challenge 🎯
Simple spec: *"Monitor live horse racing markets, close positions when a selection's favourite rank drops by N positions OR when the current favourite's odds fall below a threshold."*

Seemed straightforward enough for a bot trigger script in F#.

## The AI Model Lineup 🤖
- **Human Baseline** (R1) - Control implementation
- **DeepSeek** (DS_R1, DS_R2) - Functional approach with immutable state
- **Claude** (CS_R1, CS_R2) - Rich telemetry and explicit state transitions  
- **Grok Code** (GC_R1, GC_R2) - Production-lean with performance optimizations
- **GPT-5 Preview** (G5_R2, G5_R3) - Stable ordering and advanced error handling

## What Emerged 📊
**3 Distinct Architectural Styles:**
1. **Minimal mutable loops** - Fast, simple, harder to extend
2. **Functional state passing** - Pure, testable, but API mismatches
3. **Explicit phase transitions** - Verbose but excellent for complex logic

## The Gotchas That Surprised Me 😅
- **DeepSeek R2**: Fixed the favourite logic but inverted the rank direction (triggers on *improvements* not deteriorations)
- **API Interpretation**: Models made different assumptions about `TriggerResult` signatures
- **Semantic Edge Cases**: `<=` vs `<` comparisons, `0.0` vs `NaN` disable patterns

## Key Discovery 💡
**Cross-model validation is gold.** Each AI caught different edge cases:
- Claude added rich audit trails I hadn't considered
- Grok introduced throttling for performance
- GPT-5 handled tie-breaking in rank calculations
- DeepSeek's bug revealed my spec ambiguity

## The Synthesis 🔗
Best unified approach combines:
- GPT-5 R3's stable ordering logic
- Claude's telemetry depth  
- Grok's production simplicity
- Human baseline's clarity

## Lessons for AI-Assisted Development 📚
1. **Multiple models > single model** - Diversity exposes blind spots fast
2. **Build comparison matrices early** - Prevents feature regression
3. **Normalize semantics before merging** - Small differences compound
4. **Log strategy matters** - Lightweight live vs rich post-analysis

## Next Steps 🚀
- Fix the rank inversion bug in DS_R2
- Implement unified version with best-of-breed features
- Add JSON export for ML dataset building

**Anyone else experimenting with multi-model code generation? Would love to hear about your approaches and what you've discovered!**

**Sample code and full analysis:** [GitHub - BetfairAiTrading](https://github.com/StefanBelo/BetfairAiTrading)

---
