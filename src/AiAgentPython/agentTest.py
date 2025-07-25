import asyncio
from mcp_agent.core.fastagent import FastAgent

# Create the application
fast = FastAgent("Bfexplorer Assistant")

# Define the agent
@fast.agent(name="BfexplorerApp", 
    instruction="You are a helpful AI Agent executing betting/trading strategies on bfexplorer.", 
    model="deepseek-chat", 
    servers=["BfexplorerApp"]
)
async def main():
    async with fast.run() as agent:
        await agent("Who are you?")
        await agent("Get me an active betfair market in BfexplorerApp.")

if __name__ == "__main__":
    asyncio.run(main())
