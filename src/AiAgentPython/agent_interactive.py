import asyncio
from mcp_agent.core.fastagent import FastAgent

# Create the application
fast = FastAgent("Bfexplorer Assistant")

# Define the agent
#@fast.agent(name="BfexplorerApp", instruction="You are a helpful AI Agent", model="deepseek-chat", servers=["BfexplorerApp"])
#@fast.agent(name="BfexplorerApp", instruction="You are a helpful AI Agent", model="gemini-2.5-pro", servers=["BfexplorerApp"])
#@fast.agent(name="BfexplorerApp", instruction="You are a helpful AI Agent", model="generic.mistral-small:latest", servers=["BfexplorerApp"])
#@fast.agent(name="BfexplorerApp", instruction="You are a helpful AI Agent", model="mistral-ai/Mistral-Nemo", servers=["BfexplorerApp"])
#@fast.agent(name="BfexplorerApp", instruction="You are a helpful AI Agent", model="generic.openai/gpt-4.1", servers=["BfexplorerApp"])
#@fast.agent(name="BfexplorerApp", instruction="You are a helpful AI Agent", model="generic.deepseek-chat", servers=["BfexplorerApp"])
@fast.agent(name="BfexplorerApp", instruction="You are a helpful AI Agent", model="generic.xai/grok-3", servers=["BfexplorerApp"])
async def main():
    # use the --model command line switch or agent arguments to change model
    async with fast.run() as agent:
        await agent.interactive()

if __name__ == "__main__":
    asyncio.run(main())