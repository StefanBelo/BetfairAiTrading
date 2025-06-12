# AI Agent JavaScript

A JavaScript implementation for AI-powered interaction with BfexplorerApp.

## Requirements

### Prerequisites
- **Node.js 14+** (for ES6 module support)
- **GitHub Personal Access Token** (for AI model access)

### Installation

1. **Install dependencies:**
   ```powershell
   npm install
   ```

2. **Set up environment variable:**
   ```powershell
   # In PowerShell (temporary)
   $env:GITHUB_TOKEN = "your-github-token-here"
   
   # Or set permanently
   [Environment]::SetEnvironmentVariable("GITHUB_TOKEN", "your-github-token-here", "User")
   ```

### GitHub Token Setup
1. Go to https://github.com/settings/tokens
2. Generate a new token with appropriate permissions
3. Copy the token and set it as the `GITHUB_TOKEN` environment variable

## Running the Agent

```powershell
# Option 1: Use the startup script (recommended)
.\start.ps1

# Option 2: Run directly with Node.js
node agent_interactive.js

# Option 3: Use npm scripts
npm start
# or
npm run interactive
```

### Quick Start
1. Set your GitHub token: `$env:GITHUB_TOKEN = "your-token-here"`
2. Run: `.\start.ps1`
3. Start chatting with the AI assistant!

## Dependencies

- `@azure-rest/ai-inference`: Azure REST client for AI inference
- `@azure/core-auth`: Azure authentication library

## Current Functionality

The `agent_interactive.js` now provides:
- **Interactive Chat Console**: Continuous conversation with AI
- **Conversation History**: Maintains context across messages
- **Specialized System Prompt**: Focused on betting and trading assistance
- **Built-in Commands**: 
  - `help` - Show available commands
  - `clear` - Clear conversation history
  - `exit/quit/bye` - Exit the application
- **Error Handling**: Graceful error management
- **BfexplorerApp Context**: AI assistant specialized for betting market analysis

## Interactive Commands

Once running, you can use these commands:
- **Regular chat**: Just type your questions about betting, trading, or markets
- **`help`**: Show available commands and capabilities
- **`clear`**: Reset conversation history
- **`exit`**, **`quit`**, or **`bye`**: Exit the application

## Example Usage

```
🤖 Bfexplorer AI Agent - Interactive Mode
=============================================
Type 'exit', 'quit', or 'bye' to end the conversation
Type 'clear' to clear conversation history
Type 'help' for available commands

You: What is Expected Value in betting?

🤖 Assistant: Expected Value (EV) in betting is a mathematical concept...

You: How do I calculate EV for a horse race?

🤖 Assistant: To calculate EV for a horse race, you need...
```

## Next Steps

To integrate with BfexplorerApp like the Python version, you would need to add:
- MCP (Model Context Protocol) client functionality
- Connection to BfexplorerApp MCP server (localhost:10043)
- Betting strategy logic
- Market data analysis capabilities