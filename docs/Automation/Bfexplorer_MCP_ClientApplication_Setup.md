---
title: Connecting External Applications to the MCP Server
tags: [betfair, mcp, llm, setup]
date: 2026-08-27
aliases: BfExplorer MCP Client Setup
---

# Connecting External Applications to the MCP Server

Several external applications can connect to an MCP server and utilize Large Language Models (LLMs). This guide outlines the necessary steps for configuring popular tools, including Claude Desktop, Antigravity, LM Studio, Cherry Studio, Reasonix, and Visual Studio Code.

The foundational step is ensuring that `bfexplorer` is running with agentic support. This process starts the MCP server that all external applications must connect to.

## 🖥️ Claude Desktop Setup

In Claude Desktop, local MCP servers are managed under "Connectors" rather than being added directly. To configure this connection:

1.  Navigate to **Settings** > **Developer**.
2.  Under **Local MCP Servers**, click **Edit Config**.
3.  Open the `claude_desktop_config.json` file in Notepad and add the following entry:

```json
"mcpServers": {
    "bfexplorer": {
      "command": "npx",
      "args": [
        "-y",
        "mcp-remote",
        "http://localhost:10043/"
      ]
    }
  },
```

After adding the entry, **restart** the Claude Desktop application.

![ClaudeDesktop_MCP](/docs/Automation/images/ClaudeDesktop_MCP.png)

## ⚙️ Antigravity Setup

In the Antigravity application:

1.  Go to **Settings** > **Customization**.
2.  Locate the **Installed MCP Server** section and click **Open MCP Config**.
3.  Open the `mcp_config.json` file in Notepad and add the following entry:

```json
{
  "mcpServers": {
    "bfexplorer": {
      "serverUrl": "http://localhost:10043/"
    }
  }
}
```

![Antigravity_MCP](/docs/Automation/images/Antigravity_MCP.png)

## 💡 LM Studio Setup

Open a New Chat and, in the right pane, click on the Tool icon. In the Integrations section, select **Install / Edit mcp.json**.

```json
{
  "mcpServers": {
    "bfexplorer": {
      "url": "http://localhost:10043"
    }
  }
}
```

Click the Save button.

![LMStudio_MCP](/docs/Automation/images/LMStudion_MCP.png)

*Note: When using `bfexplorer` in a prompt, ensure that bfexplorer is active.*

![LMStudion_MCP_Integrations](/docs/Automation/images/LMStudion_MCP_Integrations.png)

## 🍒 Cherry Studio Setup

In the Cherry Studio app, navigate to **Settings** / **MCP** and click **Add**.

Enter the following details:
*   **Name:** `bfexplorer`
*   **Type:** `Streamable HTTP`
*   **URL:** `http://localhost:10043`

Click the save button.

![CherryStudio_MCP](/docs/Automation/images/CherryStudio_MCP.png)

When using any Chat, always verify that your bfexplorer is active and enabled.

![CherryStudio_MCP](/docs/Automation/images/CherryStudio_MCP_Integrations.png)

## 🛠️ Reasonix Setup

In the Reasonix app, navigate to **Settings** / **MCP & Tools**, click **Add server**, select **Manual Setup**, and enter the following:

*   **Name:** `bfexplorer`
*   **Protocol:** `http`
*   **URL:** `http://localhost:10043`

Click **Add** and then **Connect**.

![Reasonix_MCP](/docs/Automation/images/Reasonix_MCP.png)

## 💻 Visual Studio Code Setup

As a developer IDE, VS Code allows for project-level support by creating an `mcp.json` file in the root of your project:

```json
{
  "mcpServers": {
    "bfexplorer": {
      "url": "http://localhost:10043"
    }
  }
}
```

After adding this, you should see the `bfexplorer` listed under **MCP Servers - Installed** in the Extensions view.

![VisualStudioCode_MCP](/docs/Automation/images/VisualStudioCode_MCP.png)

