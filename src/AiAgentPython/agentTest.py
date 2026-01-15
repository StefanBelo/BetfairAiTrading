import asyncio
from fast_agent import FastAgent

# Create the application
fast = FastAgent("Bfexplorer Assistant")

# Define the agent
@fast.agent(name="BfexplorerApp", 
    instruction="You are a helpful AI Agent executing betting/trading strategies on bfexplorer.", 
    #model="deepseek-chat",
    #model="generic.xai/grok-3",
    #model="generic.openai/gpt-5.0-mini",
    model="generic.openai/gpt-4.1",
    #model="generic.copilot:gpt-4.1",
    #model="generic.deepseek/DeepSeek-V3-0324",
    servers=["BfexplorerApp"]
)
async def main():
    async with fast.run() as agent:
        await agent("Get me an active betfair market in BfexplorerApp.")

if __name__ == "__main__":
    asyncio.run(main())
