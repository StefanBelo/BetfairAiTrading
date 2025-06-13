import ModelClient, { isUnexpected, ChatCompletionsOutput } from "@azure-rest/ai-inference";
import { AzureKeyCredential } from "@azure/core-auth";
import * as readline from 'readline';

const token: string | undefined = process.env["GITHUB_TOKEN"];
const endpoint: string = "https://models.github.ai/inference";
const modelName: string = "openai/gpt-4.1"; // Renamed from model to avoid conflict with module

interface ChatMessage {
  role: "system" | "user" | "assistant";
  content: string;
}

class BfexplorerAgent {
  private client: ReturnType<typeof ModelClient>;
  private messages: ChatMessage[];
  private rl: readline.Interface;

  constructor() {
    // The GITHUB_TOKEN is checked in the main() function before this constructor is called.
    // If token were null here, main() would have already exited.
    this.client = ModelClient(
      endpoint,
      // Using non-null assertion operator as token is guaranteed to be set here by main().
      new AzureKeyCredential(token!) 
    );
    
    this.messages = [
      { 
        role: "system", 
        content: `You are a helpful AI Agent assistant for BfexplorerApp, specialized in betting market analysis and trading strategies. 
        You can help with:
        - Analyzing betting markets and odds
        - Discussing trading strategies
        - Expected Value (EV) calculations
        - Risk management
        - General betting and trading questions
        
        Always provide responsible betting advice and remind users to bet within their means.` 
      }
    ];
    
    this.rl = readline.createInterface({
      input: process.stdin,
      output: process.stdout,
      prompt: '\nYou: '
    });
  }

  async sendMessage(userInput: string): Promise<string> {
    try {
      this.messages.push({ role: "user", content: userInput });

      const response = await this.client.path("/chat/completions").post({
        body: {
          messages: this.messages,
          temperature: 0.7,
          top_p: 0.9,
          model: modelName, // Use renamed variable
          max_tokens: 1000
        }
      });

      if (isUnexpected(response)) {
        // Assuming response.body.error has a structure like { message: string }
        // You might need to adjust the type based on actual error structure
        const error = response.body.error as { message?: string; code?: string; [key: string]: any };
        throw new Error(error.message || `API Error: ${error.code || 'Unknown error'}`);
      }

      // Assuming ChatCompletionsOutput is the correct type from the SDK
      const assistantMessageContent = (response.body as ChatCompletionsOutput).choices[0]?.message?.content;
      
      if (typeof assistantMessageContent !== 'string') {
        throw new Error("Assistant message content is missing or not a string.");
      }
      
      const assistantMessage: ChatMessage = { role: "assistant", content: assistantMessageContent };
      this.messages.push(assistantMessage);
      
      return assistantMessage.content;
    } catch (error: any) {
      return `Error: ${error.message}`;
    }
  }

  async interactive(): Promise<void> {
    console.log("🤖 Bfexplorer AI Agent - Interactive Mode (TypeScript)");
    console.log("=============================================");
    console.log("Type 'exit', 'quit', or 'bye' to end the conversation");
    console.log("Type 'clear' to clear conversation history");
    console.log("Type 'help' for available commands\n");

    this.rl.prompt();

    this.rl.on('line', async (input: string) => {
      const trimmedInput = input.trim();
      
      if (trimmedInput === '') {
        this.rl.prompt();
        return;
      }

      if (['exit', 'quit', 'bye'].includes(trimmedInput.toLowerCase())) {
        console.log("\n👋 Goodbye! Happy trading!");
        this.rl.close();
        return;
      }

      if (trimmedInput.toLowerCase() === 'clear') {
        this.messages = this.messages.slice(0, 1); 
        console.log("\n🧹 Conversation history cleared!");
        this.rl.prompt();
        return;
      }

      if (trimmedInput.toLowerCase() === 'help') {
        console.log("\n📋 Available Commands:");
        console.log("  help  - Show this help message");
        console.log("  clear - Clear conversation history");
        console.log("  exit  - Exit the application");
        console.log("\n💡 You can ask me about:");
        console.log("  - Betting market analysis");
        console.log("  - Trading strategies");
        console.log("  - Expected Value calculations");
        console.log("  - Risk management");
        console.log("  - General betting questions");
        this.rl.prompt();
        return;
      }

      process.stdout.write("\n🤔 Thinking...");
      
      try {
        const responseText = await this.sendMessage(trimmedInput);
        process.stdout.write("\r                    \r"); 
        console.log(`\n🤖 Assistant: ${responseText}`);
      } catch (error: any) {
        process.stdout.write("\r                    \r");
        console.log(`\n❌ Error: ${error.message}`);
      }

      this.rl.prompt();
    });

    this.rl.on('close', () => {
      console.log("\n👋 Session ended. Goodbye!");
      process.exit(0);
    });
  }
}

export async function main(): Promise<void> {
  if (!token) {
    console.error("❌ Error: GITHUB_TOKEN environment variable is not set.");
    console.log("Please set your GitHub token:");
    console.log("PowerShell: $env:GITHUB_TOKEN = 'your-token-here'");
    process.exit(1);
  }

  const agent = new BfexplorerAgent();
  await agent.interactive();
}

main().catch((err: any) => {
  console.error("❌ The application encountered an error:", err);
  process.exit(1);
});
