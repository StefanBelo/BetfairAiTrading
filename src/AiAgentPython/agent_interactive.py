import asyncio
from fast_agent import FastAgent

# Create the application
fast = FastAgent("Bfexplorer Assistant")

# Define the agent
#@fast.agent(name="BfexplorerApp", instruction="You are a helpful AI Agent", model="gpt-4.1", servers=["BfexplorerApp"])
#@fast.agent(name="BfexplorerApp", instruction="You are a helpful AI Agent", model="deepseek-chat", servers=["BfexplorerApp"])
#@fast.agent(name="BfexplorerApp", instruction="You are a helpful AI Agent", model="gemini-2.5-pro", servers=["BfexplorerApp"])
#@fast.agent(name="BfexplorerApp", instruction="You are a helpful AI Agent", model="generic.mistral-small:latest", servers=["BfexplorerApp"])
#@fast.agent(name="BfexplorerApp", instruction="You are a helpful AI Agent", model="mistral-ai/Mistral-Nemo", servers=["BfexplorerApp"])
@fast.agent(name="BfexplorerApp", instruction="You are a helpful AI Agent", model="generic.openai/gpt-4.1", servers=["BfexplorerApp"])
#@fast.agent(name="BfexplorerApp", instruction="You are a helpful AI Agent", model="generic.deepseek-chat", servers=["BfexplorerApp"])
#@fast.agent(name="BfexplorerApp", instruction="You are a helpful AI Agent", model="generic.xai/grok-3", servers=["BfexplorerApp"])
#@fast.agent(name="BfexplorerApp", instruction="You are a helpful AI Agent", model="generic.deepseek/DeepSeek-V3-0324", servers=["BfexplorerApp"])
#@fast.agent(name="BfexplorerApp", instruction="You are a helpful AI Agent", model="generic.openai/gpt-5", servers=["BfexplorerApp"])
#@fast.agent(name="BfexplorerApp", instruction="You are a helpful AI Agent", model="generic.openai/gpt-5-mini", servers=["BfexplorerApp"])
#@fast.agent(name="BfexplorerApp", instruction="You are a helpful AI Agent", model="generic.copilot:gpt-4.1", servers=["BfexplorerApp"])
#@fast.agent(name="BfexplorerApp", instruction="You are a helpful AI Agent", model="generic.aihubmix:gpt-4.1", servers=["BfexplorerApp"])
async def main():
    # use the --model command line switch or agent arguments to change model
    async with fast.run() as agent:
        await agent.interactive()

if __name__ == "__main__":
    asyncio.run(main())