# Source Code Directory Index

This directory contains the complete source code implementation for the Betfair AI Trading system, including AI agents, analysis tools, trading strategies, and supporting applications.

## 📁 Directory Structure & Components

### 🤖 AI Agent Implementations

#### [AiAgentCSharp/](AiAgentCSharp/)
**C# AI Agent with MCP Integration**
- **Purpose**: Production-ready C# AI agent using Model Context Protocol
- **Key Files**:
  - `Program.cs` - Main application entry point
  - `AiAgentHelpers.cs` - Helper functions and utilities
  - `MyPrompts.cs` - Predefined AI prompts for trading strategies
  - `README.md` - Detailed implementation guide and architecture
- **Features**: Multi-AI provider support (OpenAI GPT-4.1, DeepSeek), MCP server integration, automated strategy execution
- **Best For**: Production deployment and enterprise-level AI trading systems

#### [AiAgentPython/](AiAgentPython/)
**Python AI Agent Implementation**
- **Purpose**: Python-based AI agent with FastAgent framework
- **Key Files**:
  - `agent.py` - Basic AI agent implementation
  - `agentAdvance.py` - Advanced features and capabilities
  - `agentBet.py` - Betting-specific AI agent
  - `agentTest.py` - Testing and validation scripts
  - `agent_interactive.py` - Interactive AI agent interface
  - `requirements.txt` - Python dependencies
  - `fastagent.config.yaml` - Configuration settings
- **Features**: FastAgent integration, interactive mode, betting automation, comprehensive testing framework
- **Best For**: Rapid prototyping, research, and Python-based AI development

#### [AiAgentTypeScript/](AiAgentTypeScript/)
**TypeScript/Node.js AI Agent**
- **Purpose**: Modern TypeScript implementation for web-based AI agents
- **Key Files**:
  - `agent.ts` - Core AI agent implementation
  - `agent_interactive.ts` - Interactive web interface
  - `package.json` - Node.js dependencies and scripts
  - `tsconfig.json` - TypeScript configuration
  - `start_agent_interactive.ps1` - PowerShell startup script
- **Features**: Modern TypeScript architecture, web interface, Node.js ecosystem integration
- **Best For**: Web applications, modern JavaScript environments, and browser-based trading interfaces

### 📊 Analysis & Monitoring Tools

#### [BetfairMarketAnalyzer/](BetfairMarketAnalyzer/)
**Professional Technical Analysis System**
- **Purpose**: Comprehensive technical analysis using financial market indicators
- **Architecture**:
  - `Models/` - Data models and structures
    - `AnalysisModels.cs` - Analysis result models
    - `BetfairModels.cs` - Betfair market data models
  - `Services/` - Core analysis services
    - `TechnicalAnalysisService.cs` - Technical indicator calculations
    - `ReportGeneratorService.cs` - Report generation and formatting
  - `Program.cs` - Main analysis application
- **Generated Reports**: Live analysis files (`BetfairAnalysis_*.md`)
- **Features**: RSI, MACD, Bollinger Bands, Support/Resistance detection, Volume analysis, Professional markdown reports
- **Best For**: Professional market analysis, technical trading strategies, and detailed market reporting

#### [App/](App/)
**Core Application Framework**
- **Purpose**: Main application framework and shared components
- **Components**:
  - `TechnicalAnalysis/` - Technical analysis library
    - `EnhancedTechnicalAnalyzer.cs` - Advanced technical analysis algorithms
    - `SupportResistanceCalculator.cs` - Support and resistance level detection
    - `TechnicalAnalysis.csproj` - Project configuration
  - `App.fsproj` - F# application project
- **Features**: Enhanced technical analysis, mathematical calculations, shared utilities
- **Best For**: Core functionality, shared libraries, and mathematical analysis components

### 🎯 Trading Strategies

#### [Strategies/](Strategies/)
**Complete Strategy Implementation Library**

##### [HorseRacing/](Strategies/HorseRacing/)
**Horse Racing Strategy Collection**
- **Core Strategies**:
  - `CloseByPositionDifferenceBotTrigger.md` - Strategy documentation
  - `CloseByPositionDifferenceBotTrigger_R1.fsx` - Basic implementation
  - `CloseByPositionDifferenceBotTrigger_CS_R1.fsx` - Close Spread version R1
  - `CloseByPositionDifferenceBotTrigger_CS_R2.fsx` - Close Spread version R2  
  - `CloseByPositionDifferenceBotTrigger_DS_R1.fsx` - Data Science version R1
  - `HorseRacingStrategyR6.fsx` - Latest R6 strategy implementation
- **Specialized Strategies**:
  - `HorseRacingBookmakersOddsBotTrigger.fsx` - Bookmaker odds analysis
  - `HorseRacingRaceDistanceBotTrigger.fsx` - Race distance optimization
- **Features**: Position difference analysis, odds movement tracking, multiple strategy versions, comprehensive horse racing automation

##### [Football/](Strategies/Football/)
**Football Trading Strategies**
- **Available Strategies**:
  - `OpenMyFootballMarketsByScore.fsx` - Score-based market opening strategy
- **Features**: Football-specific market analysis, score-based trading logic

##### [Tennis/](Strategies/Tennis/)
**Tennis Trading Strategies**
- **Available Strategies**:
  - `OpenMyTennisMarketsByScore.fsx` - Tennis score-based trading
  - `TennisDataToSpreadsheet.fsx` - Data export and analysis
- **Features**: Tennis market automation, data collection, spreadsheet integration

##### [General/](Strategies/General/)
**Universal Trading Strategies**
- **Documentation**:
  - `GeneralBotTrigger.md` - General strategy framework documentation
- **Features**: Sport-agnostic trading logic, universal bot triggers

### 💡 Examples & Demonstrations

#### [Examples/](Examples/)
**Implementation Examples and Tutorials**
- **Available Examples**:
  - `BetfairTechnicalAnalysisExample.cs` - Basic technical analysis implementation
  - `EnhancedAnalysisExample.cs` - Advanced analysis techniques and patterns
- **Features**: Code examples, implementation patterns, best practices demonstrations
- **Best For**: Learning, getting started, and understanding implementation approaches

## 🔧 Technology Stack

### **Languages & Frameworks**
- **C#**: Production AI agents, technical analysis, market analyzers
- **F#**: Advanced trading strategies, mathematical calculations, functional programming approaches
- **Python**: Research and prototyping, FastAgent framework integration
- **TypeScript/Node.js**: Modern web interfaces, browser-based applications

### **Key Libraries & Dependencies**
- **Skender.Stock.Indicators**: Professional technical analysis indicators
- **ModelContextProtocol.Client**: MCP integration for AI agents
- **FastAgent**: Python AI agent framework
- **BfexplorerApp MCP Server**: Core trading platform integration

### **AI Integration**
- **OpenAI GPT-4.1**: Advanced language model integration
- **DeepSeek**: Alternative AI model support
- **Multi-provider Support**: Flexible AI backend configuration

## 🚀 Getting Started

### **Quick Start Paths**

1. **AI Agent Development**: Start with `AiAgentCSharp/` for production or `AiAgentPython/` for research
2. **Technical Analysis**: Explore `BetfairMarketAnalyzer/` for market analysis capabilities
3. **Strategy Development**: Check `Strategies/` for sport-specific implementations
4. **Learning & Examples**: Review `Examples/` for implementation patterns

### **Development Workflow**

1. **Choose Your Technology**: Select C#, Python, or TypeScript based on your needs
2. **Review Examples**: Study implementation patterns in the Examples directory
3. **Explore Strategies**: Examine existing strategies for your target sport
4. **Build & Test**: Use the technical analysis tools for validation
5. **Deploy AI Agents**: Implement automated execution through AI agents

## 📚 Documentation & Resources

Each directory contains detailed README files with:
- **Setup Instructions**: Environment configuration and dependencies
- **Architecture Diagrams**: System design and component relationships  
- **Implementation Guides**: Step-by-step development instructions
- **API Documentation**: Method signatures and usage examples
- **Best Practices**: Recommended patterns and approaches

## 🔄 Integration Points

### **Data Flow Architecture**
```
Betfair Markets → MCP Server → AI Agents → Strategy Execution
                      ↓
              Technical Analysis → Reports & Insights
                      ↓
              Strategy Library → Automated Trading
```

### **Cross-Component Integration**
- **AI Agents** leverage **Technical Analysis** for market insights
- **Strategies** use **Analysis Tools** for decision-making
- **Examples** demonstrate integration between multiple components
- **All Components** share common **Models** and **Utilities**

This comprehensive source code directory provides everything needed to build, deploy, and maintain professional-grade AI-powered Betfair trading systems across multiple technology platforms and trading strategies.