import { Client } from '@modelcontextprotocol/sdk/client/index.js';
import { SSEClientTransport } from '@modelcontextprotocol/sdk/client/sse.js';
import type { 
  ClientCapabilities, 
  Implementation,
  Tool
} from '@modelcontextprotocol/sdk/types.js';
// --- ModelClient integration for chat completions ---
import ModelClient, { isUnexpected, ChatCompletionsOutput } from "@azure-rest/ai-inference";
import { AzureKeyCredential } from "@azure/core-auth";

const token: string | undefined = process.env["GITHUB_TOKEN"];
const endpoint: string = "https://models.github.ai/inference";
const modelName: string = "openai/gpt-4.1"; // Renamed from model to avoid conflict with module

interface ChatMessage {
  role: "system" | "user" | "assistant";
  content: string;
}

class MCPAgent {
  private client: Client;
  private transport: SSEClientTransport;
  constructor(serverUrl: string, clientName: string = "ai-agent") {
    // Initialize SSE transport
    this.transport = new SSEClientTransport(new URL(serverUrl));
    
    // Client implementation info
    const implementation: Implementation = {
      name: clientName,
      version: "1.0.0"
    };

    // Define client capabilities
    const capabilities: ClientCapabilities = {
      experimental: {},
      sampling: {}
    };

    // Create MCP client
    this.client = new Client(implementation, { capabilities });
  }

  async connect(): Promise<void> {
    try {
      await this.client.connect(this.transport);
      console.log('Connected to MCP server');
    } catch (error) {
      console.error('Failed to connect:', error);
      throw error;
    }
  }

  async getAvailableTools(): Promise<Tool[]> {
    const response = await this.client.listTools();
    return response.tools;
  }
  async callTool(name: string, arguments_: Record<string, unknown>): Promise<any> {
    return await this.client.callTool({
      name,
      arguments: arguments_
    });
  }

  async getResources(): Promise<any> {
    return await this.client.listResources();
  }

  async disconnect(): Promise<void> {
    if (this.transport) {
      await this.transport.close();
    }
  }

  async decideAndCallTool(prompt: string): Promise<any> {
    const tools = await this.getAvailableTools();

    // Map prompt keywords to tool names
    const toolMapping: Record<string, string> = {
      "active market": "mcp_bfexplorerapp_GetActiveBetfairMarket",
      "monitored markets": "mcp_bfexplorerapp_GetBetfairMonitoredMarkets",
      "strategy settings": "mcp_bfexplorerapp_GetBfexplorerStrategySettings",
      "strategy templates": "mcp_bfexplorerapp_GetBfexplorerStrategyTemplates"
    };

    // Find matching tool
    const matchedTool = Object.keys(toolMapping).find(keyword => prompt.toLowerCase().includes(keyword));

    if (!matchedTool) {
      throw new Error("No matching tool found for the given prompt.");
    }

    const toolName = toolMapping[matchedTool];

    // Check if the tool is available
    const tool = tools.find(t => t.name === toolName);
    if (!tool) {
      throw new Error(`Tool '${toolName}' is not available.`);
    }

    // Call the tool
    return await this.callTool(tool.name, {});
  }
}

class ModelChatAgent {
  private client: ReturnType<typeof ModelClient>;
  private messages: ChatMessage[];

  constructor() {
    if (!token) {
      throw new Error("GITHUB_TOKEN environment variable is not set.");
    }
    this.client = ModelClient(
      endpoint,
      new AzureKeyCredential(token!)
    );
    this.messages = [
      {
        role: "system",
        content: `You are a helpful AI Agent assistant for BfexplorerApp, specialized in betting market analysis and trading strategies.\nYou can help with:\n- Analyzing betting markets and odds\n- Discussing trading strategies\n- Expected Value (EV) calculations\n- Risk management\n- General betting and trading questions\n\nAlways provide responsible betting advice and remind users to bet within their means.`
      }
    ];
  }

  async sendMessage(userInput: string): Promise<string> {
    this.messages.push({ role: "user", content: userInput });
    const response = await this.client.path("/chat/completions").post({
      body: {
        messages: this.messages,
        temperature: 0.7,
        top_p: 0.9,
        model: modelName,
        max_tokens: 1000
      }
    });
    if (isUnexpected(response)) {
      const error = response.body.error as { message?: string; code?: string; [key: string]: any };
      throw new Error(error.message || `API Error: ${error.code || 'Unknown error'}`);
    }
    const assistantMessageContent = (response.body as ChatCompletionsOutput).choices[0]?.message?.content;
    if (typeof assistantMessageContent !== 'string') {
      throw new Error("Assistant message content is missing or not a string.");
    }
    const assistantMessage: ChatMessage = { role: "assistant", content: assistantMessageContent };
    this.messages.push(assistantMessage);
    return assistantMessage.content;
  }
}

// Usage example
async function main() {
  // --- MCPAgent usage ---
  const agent = new MCPAgent('http://localhost:10043/sse');
  // --- ModelChatAgent usage ---
  let modelAgent: ModelChatAgent | undefined = undefined;
  try {
    await agent.connect();
    // Get available tools
    const tools = await agent.getAvailableTools();
    console.log('Available tools:', tools);
    // Call a tool
    if (tools.length > 0) {
      const result = await agent.callTool(tools[0].name, {
        // tool arguments
      });
      console.log('Tool result:', result);
    }
    // Use ModelChatAgent to send a message to the AI model
    modelAgent = new ModelChatAgent();
    const aiResponse = await modelAgent.sendMessage("Get me an active betfair market in BfexplorerApp.");
    console.log('AI model response:', aiResponse);
  } catch (error) {
    console.error('Error:', error);
  } finally {
    await agent.disconnect();
  }
}

// Run the main function
main().catch(console.error);

// npm run agent