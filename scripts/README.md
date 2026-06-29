# Scripts Directory

This directory contains helper scripts for maintaining the `BetfairAiTrading` project.

## Documentation Maintenance

### `maintain_docs_metadata.py`
This Python script is used to automatically manage the YAML frontmatter (metadata) for all markdown files in the `docs/` directory. 

**Features:**
- **Automatic Title Extraction**: Uses the first H1 header as the file title.
- **Context-Aware Typing**: Assigns a `type` (e.g., `strategy`, `prompt`, `guide`, `post`) based on content and file path.
- **Smart Tagging**: Extracts tags for sports (`horse-racing`, `football`), technologies (`fsharp`, `mcp`, `python`), and strategies (`scalping`, `ev-analysis`).
- **MCP Integration Tracking**: Automatically lists the MCP tools and Data Contexts used in each prompt or guide.

**Usage:**
Run this script whenever you add new documentation or want to refresh the metadata for Obsidian:
```bash
python scripts/maintain_docs_metadata.py
```

### `ingest_docs.py`
This script implements the **Karpathy Ingest** pattern. It flattens all markdown documentation (excluding READMEs) into a single, high-density text file (`docs_context.txt`) located in the project root.

**Why use this?**
- **LLM Super-Context**: Copy-paste the entire content of `docs_context.txt` into an AI chat to give it full knowledge of every strategy, prompt, and research note in your library.
- **Data Portability**: Easily move your documentation knowledge to other tools or LLM providers.

**Usage:**
```bash
python scripts/ingest_docs.py
```

---
*Note: This script was created to improve documentation organization and searchability within the Obsidian knowledge management app.*
