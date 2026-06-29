import os
import re

"""
This script maintains the metadata (YAML frontmatter) for the BetfairAiTrading documentation.
It automatically extracts titles, types, tags, MCP tools, and Data Contexts from .md files.
Run this script whenever you add new documentation to keep the library organized and searchable in Obsidian.
"""

docs_dir = os.path.join(os.path.dirname(os.path.abspath(__file__)), "..", "docs")

def extract_metadata(content, title, filepath):
    tags = set()
    content_lower = content.lower()
    title_lower = title.lower()
    path_lower = filepath.lower()

    # --- Type Determination ---
    file_type = "note"
    
    if "readme.md" in path_lower:
        file_type = "index"
    elif any(kw in title_lower for kw in ["tutorial", "how to", "lesson", "testing", "setup"]) or "Tutorials" in filepath:
        file_type = "tutorial"
    elif "strategy" in title_lower or "system" in title_lower or "back-lay" in title_lower:
        file_type = "strategy"
    elif "prompt" in title_lower or "ai analysis" in title_lower or "Prompts" in path_lower:
        file_type = "prompt"
    elif "report" in title_lower or "posts" in path_lower:
        file_type = "post"
    elif "idea" in title_lower or "ideas" in path_lower:
        file_type = "idea"
    elif "research" in path_lower or "analysis" in title_lower:
        file_type = "research"
    elif "guide" in title_lower or "documentation" in title_lower or "using" in title_lower:
        file_type = "guide"
    elif "template" in title_lower or "Templates" in filepath:
        file_type = "template"

    # --- Tag Extraction ---
    # Sport tags
    if any(kw in content_lower or kw in title_lower for kw in ["horse racing", "horseracing", "race", "jockey", "trainer"]):
        tags.add("horse-racing")
    if any(kw in content_lower or kw in title_lower for kw in ["football", "soccer", "match odds"]):
        tags.add("football")
    if any(kw in content_lower or kw in title_lower for kw in ["tennis"]):
        tags.add("tennis")

    # Technology tags
    if any(kw in content_lower for kw in ["f#", "fsharp", "fsi", ".net", "repl"]):
        tags.add("fsharp")
    if "mcp" in content_lower or "model context protocol" in content_lower:
        tags.add("mcp")
    if any(kw in content_lower for kw in ["automation", "agent", "bot", "automated"]):
        tags.add("automation")
    if "python" in content_lower:
        tags.add("python")
    if "bfexplorer" in content_lower or "belosoft" in content_lower:
        tags.add("bfexplorer")

    # Strategy & Analysis tags
    if "scalping" in content_lower: tags.add("scalping")
    if "dutching" in content_lower: tags.add("dutching")
    if "expected value" in content_lower or " ev " in content_lower or "value betting" in content_lower: tags.add("ev-analysis")
    if "staking" in content_lower or "kelly criterion" in content_lower: tags.add("staking")
    if "trading" in content_lower: tags.add("trading")
    if "sentiment" in content_lower or "weight of money" in content_lower: tags.add("market-sentiment")
    if "back-lay" in content_lower: tags.add("back-lay")
    
    # Execution mode
    if "silent" in title_lower or "silent" in content_lower:
        tags.add("silent-execution")

    # Source tags
    if "reddit" in content_lower or "r/algobetting" in content_lower: tags.add("reddit")
    if "forum" in content_lower or "ukbettingforum" in content_lower: tags.add("forum")

    # Always add the type as a tag for easy filtering
    tags.add(file_type)
    
    return file_type, sorted(list(tags))

def process_file(filepath):
    try:
        with open(filepath, 'r', encoding='utf-8') as f:
            content = f.read()
    except Exception as e:
        print(f"Error reading {filepath}: {e}")
        return
    
    # Remove existing YAML block if it exists
    if content.startswith('---'):
        parts = content.split('---\n', 2)
        if len(parts) >= 3:
            content = parts[2]

    lines = content.splitlines()
    
    # Extract Title (First H1)
    title = ""
    for line in lines:
        if line.startswith('# '):
            title = line.strip('# ').strip()
            break
    if not title:
        title = os.path.basename(filepath).replace('.md', '').replace('-', ' ').replace('_', ' ').title()

    # Extract Metadata
    file_type, tags = extract_metadata(content, title, filepath)
    
    # MCP Tools detection
    mcp_tools = []
    common_tools = [
        "GetActiveMarket", "GetAllDataContextForMarket", "GetMarket", 
        "GetMonitoredMarkets", "ExecuteStrategySettings", "PlaceBet", 
        "SetAIAgentDataContextForMarket", "GetAiAgentDataContextFeedback",
        "ActivateMarketSelection", "ExecuteStrategySettingsOnSelections"
    ]
    for tool in common_tools:
        if tool in content:
            mcp_tools.append(tool)

    # Data Contexts detection
    data_contexts = set()
    known_contexts = [
        "MarketSelectionsTradedPricesData", "RacingpostDataForHorses", 
        "TimeformDataForHorses", "AtTheRacesDataForHorses", "OlbgRaceTipsData", 
        "MarketSelectionsPriceHistoryData", "WeightOfMoneyData", "FootballMatchScoreData",
        "MarketSelectionsTradedPricesData", "RacingpostDataForHorses"
    ]
    for ctx in known_contexts:
        if ctx in content:
            data_contexts.add(ctx)
            
    # Also look for context patterns like "contextName" or 'contextName'
    context_patterns = [
        r'dataContextNames?\s*[:=]?\s*["\']([\w]{8,})["\']',
        r'tool: GetAllDataContextForMarket.*?names:\s*["\']([\w]{8,})["\']'
    ]
    for pattern in context_patterns:
        for match in re.finditer(pattern, content, re.IGNORECASE):
            data_contexts.add(match.group(1))

    # Construct YAML Frontmatter
    yaml = "---\n"
    yaml += f'title: "{title}"\n'
    yaml += f'aliases: ["{title}"]\n'
    yaml += f"type: {file_type}\n"
    if tags:
        yaml += f"tags: [{', '.join(tags)}]\n"
    if mcp_tools:
        yaml += f"mcp_tools: [{', '.join(sorted(mcp_tools))}]\n"
    if data_contexts:
        yaml += f"data_contexts: [{', '.join(sorted(list(data_contexts)))}]\n"
    yaml += "---\n\n"
    
    # Write back to file
    with open(filepath, 'w', encoding='utf-8') as f:
        f.write(yaml + content.lstrip())

if __name__ == "__main__":
    count = 0
    root_dir = os.path.join(docs_dir, "..")
    
    # Scan Project Root
    print(f"Scanning project root: {root_dir}")
    for file in os.listdir(root_dir):
        if file.endswith(".md"):
            process_file(os.path.join(root_dir, file))
            count += 1

    # Scan Docs Directory
    print(f"Scanning docs directory: {docs_dir}")
    for root, dirs, files in os.walk(docs_dir):
        for file in files:
            if file.endswith(".md"):
                process_file(os.path.join(root, file))
                count += 1
    print(f"Finished! Processed {count} documentation files.")
