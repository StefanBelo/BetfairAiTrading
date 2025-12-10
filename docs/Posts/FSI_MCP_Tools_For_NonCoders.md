# Using FSI MCP Tools: F# Coding Made Easy for Non-Developers

Are you not a coder, but want to tweak or understand F# scripts for Betfair or other .NET projects? FSI MCP tools let you ask questions about your code and get instant, clear answers—no programming experience needed!

## What Are FSI MCP Tools?
- They let you (or an AI assistant) ask: "What can I use from this type?"
- Instantly see all the properties and options available in your data.
- No more guessing or trial-and-error!

## Example: FootballMatch Filtering
Want to filter football matches in your script? Just ask what you can use:

**Sample question:**
> What can I use from type FootballMatch to create different rules for filtering?

**Sample answer:**
- HomeScore (goals for home team)
- AwayScore (goals for away team)
- MatchTime (minutes played)
- HomeNumberOfYellowCards (yellow cards)
- ...and more!

**Sample filter:**
```fsharp
let isExcitingMatch match =
    match.MatchTime > 80 && (int match.HomeScore + int match.AwayScore) >= 3
```

## Why Use This?
- No coding background needed
- Get the info you need, fast
- Make your scripts smarter and more useful

**Try it out and make F# work for you!**

*Full guide in project docs: "Using FSI MCP Tools to Create Better F# Code for Non-Developers"*