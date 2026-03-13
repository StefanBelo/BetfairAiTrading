# Tutorial: Building Effective AI Prompts for Horse Racing Betting Strategies
**Reference:** [Google Workspace AI Prompt Guide](https://workspace.google.com/resources/ai/writing-effective-prompts/)

This tutorial guides you through designing high-quality prompts for AI-powered betting strategy analysis, using horse racing as the domain. We use "The Advanced Form and In-Play Analyst V2" as a reference example and apply best practices from Google's prompt engineering resources.

---

## 1. Introduction

Effective prompts unlock the full potential of AI assistants for sports betting analysis. By combining domain expertise with proven prompt design principles, you can create systems that deliver actionable, reliable insights.

---

## 2. Core Principles from Google Workspace AI Prompting Guide

### Four Key Elements for Every Prompt
- **Persona:** Define the AI's role (e.g., "Elite Horse Racing Quantitative Analyst & In-Play Specialist").
- **Task:** Clearly state the desired outcome (e.g., "Calculate adjusted win probabilities and identify value bets").
- **Context:** Provide relevant data, constraints, and domain specifics (e.g., "Use Timeform metrics, Betfair prices, and in-play efficiency data").
- **Format:** Specify output requirements (e.g., "Output a markdown table, bullet points, and trade parameters").

### Additional Best Practices
- **Use natural language:** Write prompts as you would speak to a colleague.
- **Be specific and iterate:** Give clear instructions, refine based on results.
- **Be concise:** Avoid unnecessary complexity and jargon.
- **Make it a conversation:** Use follow-up prompts to improve outputs.
- **Personalize with your data:** Reference your own files and datasets.
- **Leverage AI as a prompt editor:** Ask the AI to optimize your prompt for clarity and effectiveness.

---

## 3. Example: The Advanced Form and In-Play Analyst V2 Prompt

This prompt demonstrates the application of all the above principles:

- **Persona:** Elite Horse Racing Quantitative Analyst & In-Play Specialist
- **Task:** Calculate "Adjusted Win Probabilities" and identify value betting opportunities
- **Context:** Uses Timeform trajectory metrics, Betfair in-play efficiency, and market prices
- **Format:** Outputs a markdown table, bullet points, trade parameters, and stores results in JSON

**Prompt Structure:**
1. Identity & Mission
2. Data Acquisition & Validation
3. Calculation Engine (Parsing & Feature Engineering)
4. Probability Blending & Scoring
5. Strategic Decision Rules
6. Output Specification (Mandatory Format)
7. Hard Constraints & Safety Protocols

---

## 4. Applying Subcategory Guidance from Google Workspace

### Administrative Support
- Automate data validation and flagging missing data for review.

### Communications
- Structure outputs for clear communication (tables, bullet points, concise summaries).

### Customer Service
- Include safety protocols and caution flags to ensure responsible recommendations.

### Project Management
- Sequence workflow steps (data retrieval, validation, analysis, output) for repeatable processes.

### Marketing & Sales
- Highlight value opportunities and probabilistic edges, not guarantees.

### Small Business & Startups
- Use modular prompt components for scalability and adaptability to new sports or data sources.

---

## 5. Step-by-Step: Building Your Own Effective Prompt

1. **Define the AI's role and mission.**
2. **List the data sources and validation steps.**
3. **Describe the calculation logic and feature engineering.**
4. **Blend probabilities using both market and form data.**
5. **Set clear decision rules for betting actions.**
6. **Specify output format and safety constraints.**
7. **Iterate and refine based on results.**

---

## 6. Template for Your Own Prompts

```
You are a [role] specializing in [domain]. Your mission is to [task].

Data sources: [list sources]
Validation: [describe checks]
Calculation logic: [feature engineering steps]
Probability blending: [weights and formulas]
Decision rules: [criteria for actions]
Output format: [table, summary, parameters]
Safety protocols: [constraints, flags]
```

---

## 7. Conclusion

By following these principles and using the provided example, you can build robust, effective prompts for AI-driven betting strategy analysis in horse racing and beyond. Iterate, personalize, and communicate clearly for best results.

---

**Reference Example:** See "The Advanced Form and In-Play Analyst V2" in the Research/Experiment folder for a full implementation.
