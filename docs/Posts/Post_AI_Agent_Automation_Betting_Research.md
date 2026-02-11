# How AI Agents and Automation Can Accelerate Betting Research

Today, I asked my AI agent to scan the latest posts from a popular trading forum and summarize new ideas for automation in horse racing and football markets. The agent quickly grouped topics by strategy and gave opinions on their practical value.

Here are the key topics from today's posts:

1. **Thing to consider on a dobbing system?**  
   Discussion about the DOB (Double or Bust) strategy, its mechanics, selection criteria, and possible variations. Covers whether to use stop losses, how to filter selections, and the importance of historical in-play price drops.  
   Opinion: This is a practical and insightful thread for those interested in in-play horse racing strategies. The focus on selection and data-driven filters is valuable, but users should be cautious about overfitting and always backtest their ideas.

2. **Green up/remove liability after a goal**  
   Thread about automating the process of greening up or removing liability after a goal is scored in football markets.  
   Opinion: Automating liability management is a smart move for football traders, especially in volatile markets. The discussion likely provides useful tips for both beginners and experienced users.

3. **auto keypress trouble**  
   User reports issues with automating keypresses, possibly for triggering actions or bots.  
   Opinion: Automation of keypresses can be tricky due to OS and software limitations. The thread may help users troubleshoot and find workarounds, but native automation features are usually more reliable.

4. **Auto Importing Markets**  
   Discussion on how to automatically import markets into automation tools.  
   Opinion: Automating market import is essential for efficient trading, especially for those managing many markets. This topic is highly relevant for advanced users looking to streamline their workflow.

With bfexplorer MCP tools, these strategies can be automated step-by-step. For example, to automate market import using bfexplorer MCP tools, you can use the following sequence:

- Get a list of markets to monitor: Use `get_my_favourite_bet_event_markets` or `get_my_favourite_bet_events` to retrieve your preferred markets or events.
- Open each market for monitoring: Use `open_market` to add each selected market for automation.
- (Optional) Activate a specific market or selection: Use `activate_market_selection` to set focus for further automation or strategy execution.

Order of use: Get favourite events/markets, open each market, (optional) activate market/selection. This workflow ensures all relevant markets are loaded and ready for your automated strategies.

So, where do you browse for new betting ideas? Or do you just prefer to be lazy and avoid reading sport betting articles online?