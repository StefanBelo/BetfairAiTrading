# Using FSI MCP Tools to Create Better F# Code for Non-Developers

> **Main source:** [FSI MCP: Injecting AI Coding Agents into My F# REPL Workflow](https://jvaneyck.wordpress.com/2025/12/04/fsi-mcp-injecting-ai-coding-agents-into-my-f-repl-workflow/)

## Introduction

F# Interactive (FSI) Model Context Protocol (MCP) tools provide a powerful way for non-developers to explore and understand .NET types, enabling them to write better F# code without deep programming expertise. This document explains how these tools work and demonstrates their value through a real-world example.

## What Are FSI MCP Tools?

FSI MCP tools allow AI coding assistants to interact directly with an F# REPL (Read-Eval-Print Loop) environment. This enables the AI to:

- Load and reference external assemblies (DLLs)
- Inspect .NET types using reflection
- Query properties, methods, and other members of types
- Test code snippets in real-time
- Provide accurate, up-to-date information about available APIs

## Why This Matters for Non-Developers

When working with complex libraries like the Betfair API or other .NET assemblies, it's often unclear what properties and methods are available on types. Traditional approaches require:

- Reading extensive documentation
- Guessing based on similar code
- Trial-and-error compilation
- Consulting with developers

FSI MCP tools eliminate this uncertainty by allowing you to ask questions like "What can I use from this type?" and get precise answers.

## Real-World Example: Exploring the FootballMatch Type

### Scenario

You're modifying a football betting automation script and want to create new filtering criteria for matches. You know there's a `FootballMatch` type from the `BeloSoft.Bfexplorer.FootballScoreProvider.Models` namespace, but you're not sure what properties it exposes.

### The Question

"What can I use from type FootballMatch to create different rules/criteria for filtering?"

### How FSI MCP Tools Work Behind the Scenes

1. **Assembly Loading:** The AI loads the necessary references:
   ```fsharp
   #I @"C:\Program Files\BeloSoft\Bfexplorer\"
   #r "BeloSoft.Data.dll"
   #r "BeloSoft.Bfexplorer.Domain.dll"
   #r "BeloSoft.Bfexplorer.Service.Core.dll"
   #r "BeloSoft.Bfexplorer.FootballScoreProvider.dll"
   open BeloSoft.Bfexplorer.FootballScoreProvider.Models
   ```

2. **Type Inspection:** Using .NET reflection to examine the type:
   ```fsharp
   let footballMatchType = typeof<FootballMatch>
   let members = footballMatchType.GetMembers(BindingFlags.Public ||| BindingFlags.Instance ||| BindingFlags.Static)
   ```

3. **Result Presentation:** The AI returns a comprehensive list of available members.

### Discovered Properties

Through FSI MCP exploration, you learn that `FootballMatch` has these key properties:

**Score-Related:**
- `HomeScore` (int16) - Goals scored by home team
- `AwayScore` (int16) - Goals scored by away team
- `Score` (string) - Current score as "X-Y"
- `ScoreDifference` (int16) - Home score minus away score

**Time-Related:**
- `MatchTime` (int) - Minutes elapsed in the match
- `IsInProgess` (bool) - Whether match is currently playing
- `Status` (string) - Match status ("InPlay", "Finished", etc.)

**Cards and Discipline:**
- `HomeNumberOfYellowCards` (int)
- `AwayNumberOfYellowCards` (int)
- `HomeNumberOfRedCards` (int)
- `AwayNumberOfRedCards` (int)

**Corners:**
- `HomeNumberOfCorners` (int)
- `AwayNumberOfCorners` (int)
- `CornersDifference` (int)

**Team and League Info:**
- `HomeTeam` (string)
- `AwayTeam` (string)
- `League` (string)
- `Country` (string)

## Applying the Knowledge: Creating Better Filters

### Before FSI MCP Exploration

Your original filter might be simple:
```fsharp
let isMyFootballMatch (footballMatch : FootballMatch) =
    footballMatch.ScoreDifference > 0y  // Home team leading
```

### After FSI MCP Exploration

With the discovered properties, you can create sophisticated filters:

```fsharp
// Filter for high-scoring late games
let isHighScoringLateGame (footballMatch : FootballMatch) =
    footballMatch.MatchTime > 75 && 
    (int footballMatch.HomeScore + int footballMatch.AwayScore) >= 3

// Filter for matches with disciplinary issues
let hasDisciplinaryProblems (footballMatch : FootballMatch) =
    (footballMatch.HomeNumberOfYellowCards + footballMatch.AwayNumberOfYellowCards) >= 5

// Filter for corner-heavy matches
let isCornerRich (footballMatch : FootballMatch) =
    abs footballMatch.CornersDifference >= 5

// Combined advanced filter
let isInterestingMatch (footballMatch : FootballMatch) =
    footballMatch.IsInProgess &&
    footballMatch.MatchTime > 60 &&
    footballMatch.ScoreDifference <> 0y &&  // Not a draw
    (footballMatch.HomeNumberOfYellowCards + footballMatch.AwayNumberOfYellowCards) >= 3
```

## How to Use FSI MCP Tools in Your Workflow

### 1. Identify the Type You Need to Explore
- Look at your existing code for type names
- Check function parameters and return types
- Note namespaces from `open` statements

### 2. Ask Specific Questions
Instead of vague requests, ask:
- "What properties does the [TypeName] type have?"
- "What methods are available on [TypeName]?"
- "What type is the [PropertyName] property?"
- "Can you show me all members of [TypeName]?"

### 3. Provide Context
- Share the assembly references your code uses
- Mention the namespaces being opened
- Include any existing code snippets for context

### 4. Apply the Results
- Use the discovered properties to enhance your logic
- Create more precise filtering conditions
- Build more robust data processing functions

## Benefits for Non-Developers

### Accuracy
- No more guessing about available properties
- Eliminate compilation errors from non-existent members
- Get exact type information for proper usage

### Efficiency
- Quickly discover new filtering options
- Reduce back-and-forth with developers
- Speed up code modification tasks

### Confidence
- Understand what you're working with
- Make informed decisions about filtering criteria
- Create more effective automation rules

### Learning
- Gradually build understanding of the codebase
- Learn about available data through exploration
- Become more independent in code modifications

## Example Integration in Your Code

Here's how you might update your football market opening script:

```fsharp
let Execute (bfexplorerConsole : IBfexplorerConsole) =
    // ... existing setup code ...

    // Original simple filter
    let isMyFootballMatch (footballMatch : FootballMatch) =
        footballMatch.ScoreDifference > 0y

    // New advanced filters discovered via FSI MCP
    let isLateGameUnderdog (footballMatch : FootballMatch) =
        footballMatch.MatchTime > 70 && footballMatch.ScoreDifference < -1y

    let isCardProne (footballMatch : FootballMatch) =
        (footballMatch.HomeNumberOfYellowCards + footballMatch.AwayNumberOfYellowCards) >= 4

    // Use multiple criteria
    let marketsToOpen =
        footballMatches
        |> List.filter (fun m -> isMyFootballMatch m || isLateGameUnderdog m)
        |> List.filter isCardProne
        |> List.map (fun footballMatch -> footballMatch.Market)
```

## Conclusion

FSI MCP tools bridge the gap between non-developers and complex .NET codebases. By enabling direct exploration of types and their members, these tools empower you to write better, more sophisticated F# code without requiring extensive programming knowledge.

The key is to ask specific questions about the types you're working with, and let the AI's access to FSI provide the detailed information you need to make informed coding decisions.

This approach transforms code modification from a guessing game into an informed, exploratory process that yields more reliable and effective results.