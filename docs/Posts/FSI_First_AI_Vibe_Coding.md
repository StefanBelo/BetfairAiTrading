# FSI First: Why AI Should Query Types Directly When Vibe Coding

**Date:** December 27, 2025  
**Topic:** AI-Assisted Development, F# Interactive, Type Exploration

## Background: Discovering FSI MCP

My app uses F# scripting extensively, so I regularly read F# Advent articles organized by Sergey Tihon at the end of the year to check for new ideas. Since my app already uses MCP (Model Context Protocol), I was particularly inspired by jvaneyck's article: [FSI MCP: Injecting AI Coding Agents into My F# REPL Workflow](https://jvaneyck.wordpress.com/2025/12/04/fsi-mcp-injecting-ai-coding-agents-into-my-f-repl-workflow/).

This article opened my eyes to the possibilities of FSI MCP in my use case, and I decided to explore how it could improve my AI-assisted workflow. What I discovered was both enlightening and concerning—AI assistants don't always use the best tools available to them.

## The Incident

I asked my AI assistant a simple question while working on a football betting script:

> "What properties can I use to create rules for the FootballMatch type?"

The AI responded with a comprehensive list of properties... but got the types wrong. It said `HomeScore` was `int16` when it's actually `Byte`. It searched documentation and made assumptions instead of checking the actual source.

When I pushed back with "Really do check all types," the AI finally used FSI (F# Interactive) to inspect the actual assembly and gave me the correct answer.

## What Went Wrong

The AI fell into a common trap when working with .NET types:

1. **Searched documentation first** - Found older/incomplete info
2. **Made assumptions** - Guessed at types based on similar code
3. **Provided plausible but wrong answers** - Looked correct but wasn't accurate

This is the opposite of what should happen when you have FSI MCP tools available.

## What Should Have Happened

When I asked about `FootballMatch` properties, the AI should have **immediately**:

```fsharp
#I @"C:\Program Files\BeloSoft\Bfexplorer\";;
#r "BeloSoft.Bfexplorer.FootballScoreProvider.dll";;

open System.Reflection;;
open BeloSoft.Bfexplorer.FootballScoreProvider.Models;;

let footballMatchType = typeof<FootballMatch>;;
let properties = footballMatchType.GetProperties(
    BindingFlags.Public ||| BindingFlags.Instance);;

properties |> Array.iter (fun p -> 
    printfn "%s : %s" p.Name p.PropertyType.Name);;
```

This gives the **authoritative, accurate, current** answer directly from the loaded assembly.

## The Correct Answer

Using FSI revealed the actual types:

```fsharp
// CORRECT (via FSI):
HomeScore : Byte          // Not int16!
AwayScore : Byte
ScoreDifference : SByte   // Signed byte, not int16
Goals : Byte
GoalBeingScored : Boolean // Bonus property I missed!
MatchTime : Int32
Status : String
// ... etc
```

## Why FSI First Matters for "Vibe Coding"

"Vibe coding" with AI means working fluidly, asking questions, and letting the AI figure out implementation details. But **accuracy matters**:

### ❌ Documentation-First Approach
- Documentation can be outdated
- Assumptions lead to subtle bugs
- Wrong types cause runtime errors
- Wastes time fixing mistakes

### ✅ FSI-First Approach
- **Authoritative**: Queries the actual loaded assembly
- **Current**: Always reflects the real code
- **Complete**: Shows all members, even undocumented ones
- **Fast**: Instant feedback from REPL

## The FSI-First Rule for AI Assistants

When a user asks about .NET types in a workspace with FSI MCP tools:

**ALWAYS:**
1. Use FSI to inspect the type first
2. Get the actual properties/methods/types
3. Then provide the answer with confidence

**NEVER:**
1. Search documentation first
2. Assume types based on similar code
3. Guess at property names or types

## Lessons for Non-Developers

If you're working with an AI to explore F#/.NET code:

### 1. Demand FSI Verification
When asking about types, explicitly request:
> "Use FSI to show me the actual properties of [TypeName]"

### 2. Question Assumptions
If the AI provides type information without showing FSI output, ask:
> "Did you check this with FSI, or are you guessing?"

### 3. Trust But Verify
Even experienced AIs can fall into documentation traps. FSI is your ground truth.

## The Broader Principle

This incident reveals a key insight about AI-assisted development:

**Tools exist for a reason.** When you have FSI MCP tools, they're not just for convenience—they're for **accuracy**. The AI should prioritize direct type inspection over documentation search, every time.

Think of it like this:
- Documentation = "Someone told me about this"
- FSI = "Let me look at the actual source code right now"

Which would you trust more?

## Practical Example: Building Better Filters

With the correct FSI-verified types, I can write accurate filters:

```fsharp
// Now I know Goals is Byte, not int16
let isHighScoring (match: FootballMatch) =
    match.Goals >= 5uy  // uy suffix for Byte, not y for int16

// And I discovered GoalBeingScored exists!
let isLiveGoal (match: FootballMatch) =
    match.GoalBeingScored && match.MatchTime > 70

// Correct type for ScoreDifference (SByte)
let isCloseMatch (match: FootballMatch) =
    abs match.ScoreDifference <= 1y  // y suffix for SByte
```

## Conclusion

When vibe coding with AI in F#/.NET environments:

1. **FSI First** - Always query types directly with FSI MCP tools
2. **Trust the REPL** - It's the authoritative source
3. **Document Later** - Use docs for concepts, FSI for implementation
4. **Call Out Mistakes** - When AI doesn't use FSI, push back

The FSI MCP tools exist to eliminate guesswork. Use them first, not as a fallback.

---

**Related Resources:**
- [Using FSI MCP Tools to Create Better F# Code for Non-Developers](https://github.com/StefanBelo/BetfairAiTrading/blob/267d4e18f01bd4873bdebfc14c06d47dda818303/docs/Using_FSI_MCP_Tools_for_FSharp_Code.md)
- [FSI MCP Tools For NonCoders](https://github.com/StefanBelo/BetfairAiTrading/blob/267d4e18f01bd4873bdebfc14c06d47dda818303/docs/Posts/FSI_MCP_Tools_For_NonCoders.md)
- [FSI MCP: Injecting AI Coding Agents into My F# REPL Workflow](https://jvaneyck.wordpress.com/2025/12/04/fsi-mcp-injecting-ai-coding-agents-into-my-f-repl-workflow/)

**TL;DR:** When AI has FSI tools, it should use them FIRST for type inspection, not search docs and guess. FSI = truth. Docs = hints.
