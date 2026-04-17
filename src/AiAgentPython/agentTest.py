import asyncio
from fast_agent import FastAgent

import time
start = time.perf_counter()

# Create the application
fast = FastAgent("Bfexplorer Assistant")

# Define the agent
@fast.agent(name="BfexplorerApp", 
    instruction="You are a helpful AI Agent executing betting/trading strategies on bfexplorer.", 
    #model="deepseek-chat",
    #model="generic.xai/grok-3",
    #model="generic.openai/gpt-5-mini",
    #model="generic.openai/gpt-4.1",
    model="generic.openai/google/gemma-4-e4b",
    #model="generic.copilot:gpt-4.1",
    #model="generic.deepseek/DeepSeek-V3-0324",
    #model="generic.0fc0e51e-3bec-4033-b2c9-1c69af62d4a3:gpt-4.1",
    #model="generic.deepseek:deepseek-chat",
    servers=["BfexplorerApp"]
)
async def main():
    async with fast.run() as agent:
        await agent("Get active market.")

if __name__ == "__main__":
    asyncio.run(main())

    end = time.perf_counter()
    print(f"Execution time: {(end - start):.2f} s")