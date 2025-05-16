# How Non-Developers Can Update Code with AI Assistance

In today's rapidly evolving technological landscape, many non-developers find themselves needing to modify or create code. Whether you're updating a betting script, customizing a website template, or adjusting automation tools, AI language models can bridge the gap between your functional needs and technical implementation. This guide will help you effectively communicate with AI assistants to modify code, even if you have limited programming knowledge.

## Prerequisites

Before you begin working with AI to modify code, gather these essentials:

1. **The existing code files** you need to modify
2. **Interface definition files** (.fsi for F#, .h for C++, etc.) if they exist
3. **A clear idea of what you want to change** functionally

## Setting the Context for AI

The key to getting useful code modifications is providing the right context. AI models can't see your files unless you share them, and they don't know what your code does unless you explain it.

### Essential Context Elements

1. **Share the Main Code File**
   - Upload or paste the script/code you want to modify
   - Include comments if they explain what the code does

2. **Share Interface/Definition Files**
   - For F#: Share .fsi files that define the types/functions
   - For other languages: Share header files, type definitions, etc.
   - Example: If you're filtering football matches, share the definition of the FootballMatch type

3. **Explain the Current Functionality**
   - In simple terms, describe what the code currently does
   - Example: "This script filters football matches and opens markets for certain conditions"

4. **Define External Dependencies**
   - Mention libraries or services the code uses
   - Example: "This script references Betfair API libraries located at C:\Program Files\BeloSoft\Bfexplorer\"

## How to Ask for Code Changes

Non-developers should focus on describing the *what* (desired outcome) not the *how* (implementation details).

### Effective Prompting Techniques

1. **Describe the Desired Behavior**
   ```
   "I need to change the script to filter football matches where the away team is leading by 2 goals and there are less than 10 minutes left in the game."
   ```

2. **Reference Existing Functions**
   ```
   "Please modify the isAwayTeamDominatingEarly function to check for matches where the away team is leading by 2 goals and the match time is more than 80 minutes."
   ```

3. **Provide Input/Output Examples**
   ```
   "For example, if there's a match with score 1-3 at 85 minutes, it should be included. But a match with score 0-1 at 82 minutes should not."
   ```

4. **Ask for Explanations**
   ```
   "Please explain what each part of the new code does so I understand how it works."
   ```

5. **Request Step-by-Step Implementation Instructions**
   ```
   "Can you tell me exactly where to put this new code and what, if anything, I need to remove?"
   ```

### Real-World Example

**Bad Request:**
```
"I need to update the code for scoring"
```

**Good Request:**
```
"I've shared my FootballMatch.fsi file which shows the properties available. I need to modify the 'isAwayTeamDominatingEarly' function in my script to instead find matches where the away team is leading by exactly 2 goals and the match time is greater than 80 minutes. Please show me the updated function and explain how to integrate it into my existing code."
```

- **File path**: [/src/Strategies/Football/OpenMyFootballMarketsByScore.fsx](/src/Strategies/Football/OpenMyFootballMarketsByScore.fsx)

```F#
// Original function
let isAwayTeamDominatingEarly (match: FootballMatch) =
    match.ScoreDifference >= 1y && match.Minute < 30

// Modified function (AI-generated)
let isAwayTeamDominatingLate (match: FootballMatch) =
    match.ScoreDifference = 2y && match.Minute > 80
```    

## Common Pitfalls to Avoid

1. **Assuming the AI Knows Your System**
   - Always explicitly share relevant files and explain their purpose
   - Don't assume the AI remembers your setup from previous conversations

2. **Being Too Vague**
   - "Make my script better" doesn't provide enough direction
   - Specify exactly what "better" means in your context

3. **Ignoring Library Limitations**
   - AI can't modify external libraries or DLLs
   - It can only suggest changes to the code you provide

4. **Not Testing the Generated Code**
   - Always test code changes in a safe environment before deploying
   - Verify the code behaves as expected with different inputs

## Tips for Specific Languages

### F# Specific Tips

1. **Share .fsi Files:** These interface files tell the AI what properties and methods are available
2. **Mention Type Signatures:** F# is strongly typed, so be clear about the types you're working with
3. **Explain List Processing:** Many F# operations involve list processing with filters, maps, etc.

## When to Seek Professional Help

While AI is powerful, some tasks may require professional development assistance:

1. **Complex System Changes:** Major architectural changes to large systems
2. **Performance Optimization:** When efficiency is critical
3. **Security-Critical Code:** When code handles sensitive data or authentication
4. **Integration with Complex Systems:** When connecting to enterprise systems

## Conclusion

Non-developers can successfully modify code with AI assistance by providing clear context, describing desired outcomes functionally, and asking specific questions. Remember that the AI works best when you explain what you want to achieve, rather than how to achieve it. With practice, this approach can empower you to make code changes that would otherwise require developer expertise.

Always test any code modifications thoroughly before implementing them in production environments, especially for financial applications like betting systems.