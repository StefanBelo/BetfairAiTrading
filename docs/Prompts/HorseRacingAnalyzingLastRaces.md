# Performance-Based Horse Racing Strategy: Analyzing Last Races to Identify Trading, Back Betting, and Dutching Opportunities

```
As a horse racing expert AI, your task is to analyze the performance data of horses in a given race. Follow these steps:

1. Activate the market monitoring in BFExplore for the specified horse racing market.

2. Load the corresponding JSON file from the `data/TestData` folder. Use the market ID from BFExplorer as the filename to locate the correct file.

3. Focus your analysis on the 'last races description' field for each horse. This field provides critical insights into recent performance trends.

4. Ignore the 'prediction score' field as it is not relevant for this analysis.

5. Based on the 'last races description' data, recommend horses suitable for the following strategies:

   - **Trading**: Identify horses with fluctuating performance trends that may present trading opportunities.
   - **Back Betting**: Highlight horses with consistently strong performance indicators.
   - **Dutching**: Suggest groups of horses with complementary strengths to maximize coverage.
```

## Claude 3.5 Sonnet

Based on the workspace content and your question, I can suggest several additions to make the AI agent's analysis more comprehensive. Here's what you could add to your prompt:

1. Form Analysis Extensions:

Also analyze:
- Pace analysis: Identify potential front-runners, hold-up horses, and how the race might be run
- Going preferences: Check each horse's performance on different ground conditions
- Class analysis: Compare previous race classes with current race
- Distance suitability: Review performance at today's distance
- Track record: Analyze course and distance form
- Jockey/trainer combinations: Note any significant partnerships

For each horse provide a confidence score (1-10) based on:
- Recent form consistency
- Course suitability
- Distance aptitude
- Going preference match
- Class level performance


2. Risk Assessment Component:

For each betting strategy recommendation, provide:
- Risk level (Low/Medium/High)
- Potential warning signs to watch for
- Key price points for entry/exit
- Suggested stake sizing based on confidence level


3. Market Dynamics:

Monitor and report on:
- Steam moves (significant market support)
- Drifting prices and possible reasons
- Market over-rounds per section of the market
- Comparative analysis with traditional bookmaker prices

These additions would force the AI to provide a more structured and thorough analysis, helping make more informed trading decisions. You might want to save these as templates in your workspace, perhaps in a new file under Prompts directory for future use.

Would you like me to help you create a formal template file with these additions?