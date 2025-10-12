## How Grok LLM Interacted With My Bfexplorer Project

Today I added a new data context to my bfexplorer app, specifically for bookmaker odds analysis. To test this feature, I created a prompt and looked for ways to optimize it using Grok LLM.

Interestingly, because the data context name included "BookmakersOdds," Grok LLM scanned my solution folder and detected an F# script with "BookmakersOdds" in its filename. Without explicit instruction, it proceeded to update the script code to match the prompt criteria. The changes were made without errors, which was impressive.

This happened because I use Visual Studio Code and GitHub Copilot to manage my prompts, and Grok Code Fast 1 LLM treats everything as part of the codebase. Even though I wasn't asking for script updates, it attempted them automatically.

Takeaway: If you're using LLMs like Grok for prompt engineering or code review, be aware that they may proactively edit related scripts if your naming conventions overlap. This can be helpful, but also surprising if you only intended to work on documentation or prompts.

Anyone else experienced LLMs making unexpected but correct code changes? Share your stories!

#AI #LLM #Grok #Copilot #Bfexplorer #PromptEngineering #Reddit