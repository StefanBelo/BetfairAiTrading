# CLI Tools for AI Coding (verified list)

This file lists widely-used, verified command-line tools and frameworks that enable AI-assisted coding workflows. Each entry includes a short description, a usage/install hint, and authoritative documentation or repository links.

---

## GitHub Copilot (Copilot in the CLI)
- **Description:** Terminal extension for GitHub CLI that suggests and explains shell commands and small code snippets. Requires GitHub CLI and a Copilot subscription for full features.
- **Quick use:** `gh extension install github/gh-copilot` then `gh copilot suggest "..."
- **Official docs:** [GitHub Copilot in the CLI](https://docs.github.com/en/copilot/github-copilot-in-the-cli)
- **Repo:** [github/gh-copilot](https://github.com/github/gh-copilot)

---

## OpenAI CLI
- **Description:** Official CLI for interacting with OpenAI platform (API keys, models, and simple prompt workflows).
- **Quick use:** follow the platform docs; useful for scripting and local testing against OpenAI APIs.
- **Docs:** [OpenAI CLI Documentation](https://platform.openai.com/docs/guides/cli)
- **Repo / tooling:** [openai/openai-cli](https://github.com/openai/openai-cli)

---

## Ollama
- **Description:** Local model runner and manager with a full-featured CLI (`ollama`) to pull, run, and manage LLMs locally (supports many community models).
- **Quick use:** install from [Ollama Download](https://ollama.com/download) then `ollama run <model>`
- **Site & downloads:** [ollama.com](https://ollama.com/)
- **Repo:** [ollama/ollama](https://github.com/ollama/ollama)

---

## Hugging Face Hub CLI (huggingface_hub)
- **Description:** CLI to manage models, datasets, and Spaces from Hugging Face; useful for pulling models and interacting with the hub from scripts.
- **Quick use:** `pip install huggingface_hub` then `huggingface-cli login` / `huggingface-cli repo` commands
- **Docs:** [Hugging Face Hub CLI](https://huggingface.co/docs/huggingface_hub/main/en/how-to/cli)

---

## LangChain (tooling / CLI)
- **Description:** Developer tooling and utilities for building agentic apps and chains; docs and CLI/tooling for scaffolding and local runs.
- **Docs & landing:** [LangChain Python](https://python.langchain.com/)
- **GitHub org:** [langchain-ai](https://github.com/langchain-ai)

---

## Auto-GPT
- **Description:** Autonomous agent framework with CLI tooling used for building and running multi-step agents; popular for experimentation and automation.
- **Repo:** [Significant-Gravitas/AutoGPT](https://github.com/Significant-Gravitas/AutoGPT)

---

## gpt-engineer (gpte)
- **Description:** Opinionated CLI for generating full projects or improving codebases from natural-language prompts; designed for coding workflows.
- **Quick use:** see repo instructions for `gpte` CLI usage.
- **Repo / docs:** [gpt-engineer-org/gpt-engineer](https://github.com/gpt-engineer-org/gpt-engineer)

---

## GPT4All
- **Description:** Local chatbot/assistant ecosystem with desktop and CLI tooling to run local models (good for private, offline experimentation).
- **Site & downloads:** [gpt4all.io](https://gpt4all.io/)
- **Repo:** [nomic-ai/gpt4all](https://github.com/nomic-ai/gpt4all)

---

## llama.cpp (llama-cli and tools)
- **Description:** Lightweight C/C++ LLM inference library with CLI tools for running and serving local GGUF models (foundation for many local CLIs).
- **Repo & docs:** [ggml-org/llama.cpp](https://github.com/ggml-org/llama.cpp)

---

## Warp / Warp Code
- **Description:** Terminal + agentic coding environment (Warp Code) that integrates agent-driven code generation, diffs and code review workflows; useful if you use Warp as your terminal.
- **Blog / overview:** [Introducing Warp Code: Prompt to Prod](https://www.warp.dev/blog/introducing-warp-code-prompt-to-prod)
- **Docs (agents/AI):** [Warp AI Features](https://docs.warp.dev/features/warp-ai)

---

## Notes on entries removed / corrected
- Several draft entries in earlier versions were removed or replaced because authoritative documentation or repos could not be verified. The current list links to official docs or primary repositories where possible.

---

If you want, I can:
- add `Verified: 2025-09-05` stamps to each entry,
- include one-line install commands for each platform (Windows/macOS/Linux), or
- commit a separate small README excerpt that summarizes CLI install commands.

Which of the three follow-ups should I do now?
