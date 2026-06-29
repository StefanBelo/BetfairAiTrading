---
title: "Strategy Builder Technology Stack - TypeScript/React Implementation"
description: "Complete guide to building a strategy builder UI with TypeScript, including libraries for flows, forms, validation, and state management"
date: 2026-04-28
tags: [research, strategy-builder, typescript, react, tech-stack, architecture, ui-libraries]
---

# Strategy Builder Technology Stack - TypeScript/React Implementation

## Executive Summary

This document provides a **comprehensive technology stack for building a visual strategy builder** in TypeScript/React, with specific focus on:
- Node-based flow/diagram builders
- Parameter form generation
- Strategy composition and validation
- Real-time preview and execution
- Integration with Bfexplorer MCP API

**Recommended Stack:**
```
Frontend Framework:    React 18+ with TypeScript
Flow Builder:         React Flow v11+
UI Component Library: shadcn/ui + Radix UI
Forms:               React Hook Form + Zod validation
State Management:    TanStack Query + Zustand
Backend Integration: TypeScript API client
Visualization:       Recharts + Visx
```

---

## Part 1: Core Frontend Framework

### React + TypeScript

**Why React?**
- Industry standard for interactive UIs
- Excellent TypeScript support
- Rich ecosystem of flow builder libraries
- Strong community and documentation

**Setup:**
```bash
# Using Vite (faster than CRA)
npm create vite@latest strategy-builder -- --template react-ts
cd strategy-builder
npm install
npm run dev

# Or using Create React App
npx create-react-app strategy-builder --template typescript
```

**Key Dependencies:**
```json
{
  "dependencies": {
    "react": "^18.2.0",
    "react-dom": "^18.2.0",
    "typescript": "^5.3.0"
  },
  "devDependencies": {
    "@types/react": "^18.2.0",
    "@types/react-dom": "^18.2.0",
    "vite": "^5.0.0"
  }
}
```

**Recommended Project Structure:**
```
src/
├── components/
│   ├── StrategyBuilder/
│   │   ├── FlowCanvas.tsx
│   │   ├── NodePalette.tsx
│   │   ├── PropertiesPanel.tsx
│   │   └── PreviewPane.tsx
│   ├── Forms/
│   │   ├── ParameterForm.tsx
│   │   ├── ConditionBuilder.tsx
│   │   └── RiskManagementForm.tsx
│   ├── Common/
│   │   ├── Header.tsx
│   │   ├── Sidebar.tsx
│   │   └── StatusBar.tsx
│   └── ui/ (shadcn/ui components)
├── hooks/
│   ├── useStrategy.ts
│   ├── useFlow.ts
│   └── useMcpClient.ts
├── services/
│   ├── mcpClient.ts
│   ├── strategyValidator.ts
│   └── api.ts
├── store/
│   ├── strategyStore.ts
│   ├── uiStore.ts
│   └── notificationStore.ts
├── types/
│   ├── strategy.ts
│   ├── templates.ts
│   └── api.ts
├── utils/
│   ├── validators.ts
│   ├── transformers.ts
│   └── helpers.ts
├── App.tsx
└── main.tsx
```

---

## Part 2: Flow/Node-Based Builders (Core UI)

### 2.1 React Flow - **RECOMMENDED**

**Overview**: Industry-leading library for building interactive node-based UIs

```bash
npm install reactflow
```

**Pros:**
- ✅ Excellent documentation and examples
- ✅ Handles complex node interactions smoothly
- ✅ Built-in zoom, pan, selection
- ✅ TypeScript-first design
- ✅ Active community and frequent updates
- ✅ Free and open-source

**Cons:**
- ⚠️ Learning curve for complex customizations
- ⚠️ Bundle size (~150KB gzipped)

**Example - Basic Flow Setup:**
```typescript
import React, { useCallback } from 'react';
import ReactFlow, {
  Node,
  Edge,
  addEdge,
  Connection,
  useNodesState,
  useEdgesState,
  Background,
  Controls,
} from 'reactflow';
import 'reactflow/dist/style.css';

const initialNodes: Node[] = [
  {
    id: '1',
    data: { label: 'Entry Signal' },
    position: { x: 0, y: 0 },
    type: 'default',
  },
  {
    id: '2',
    data: { label: 'Position Sizing' },
    position: { x: 250, y: 0 },
    type: 'default',
  },
];

const initialEdges: Edge[] = [
  { id: 'e1-2', source: '1', target: '2' },
];

export default function StrategyBuilder() {
  const [nodes, setNodes, onNodesChange] = useNodesState(initialNodes);
  const [edges, setEdges, onEdgesChange] = useEdgesState(initialEdges);

  const onConnect = useCallback(
    (connection: Connection) => 
      setEdges((eds) => addEdge(connection, eds)),
    [setEdges]
  );

  return (
    <div style={{ width: '100vw', height: '100vh' }}>
      <ReactFlow
        nodes={nodes}
        edges={edges}
        onNodesChange={onNodesChange}
        onEdgesChange={onEdgesChange}
        onConnect={onConnect}
      >
        <Background />
        <Controls />
      </ReactFlow>
    </div>
  );
}
```

**Custom Node Example - Strategy Template Node:**
```typescript
import { Handle, Position, NodeProps } from 'reactflow';
import { StrategyTemplate } from '@/types/strategy';

interface StrategyNodeData {
  template: StrategyTemplate;
  instanceName: string;
  parameters: Record<string, any>;
}

export function StrategyNode({ data }: NodeProps<StrategyNodeData>) {
  return (
    <div className="strategy-node">
      <Handle type="target" position={Position.Top} />
      
      <div className="node-header">
        <div className="node-icon">{data.template.icon}</div>
        <div className="node-info">
          <p className="node-title">{data.instanceName}</p>
          <p className="node-template">{data.template.name}</p>
        </div>
      </div>

      <div className="node-params">
        {Object.entries(data.parameters).length > 0 && (
          <ul>
            {Object.entries(data.parameters).slice(0, 2).map(([key, val]) => (
              <li key={key}>
                <span className="param-key">{key}:</span>
                <span className="param-value">{String(val)}</span>
              </li>
            ))}
            {Object.entries(data.parameters).length > 2 && (
              <li>+{Object.entries(data.parameters).length - 2} more</li>
            )}
          </ul>
        )}
      </div>

      <Handle type="source" position={Position.Bottom} />
    </div>
  );
}
```

**Advantages for Strategy Builder:**
- Handles complex multi-node strategies
- Excellent handling of conditional flows
- Built-in validation for edge connections
- Great performance even with 50+ nodes
- Easy to add custom node types (Entry, Exit, RiskManagement, etc.)

---

### 2.2 Rete.js - Advanced Alternative

```bash
npm install rete rete-react-plugin rete-connection-plugin
```

**When to use Rete:**
- Need visual code/plugin system
- Want more control over rendering
- Building complex data flow systems
- Need socket-based connections (typed inputs/outputs)

**Example:**
```typescript
import { createNodeEditor, GetSchemes, ClassicPreset } from "rete";
import { ReactPlugin, Presets } from "rete-react-plugin";
import { ConnectionPlugin } from "rete-connection-plugin";

type Schemes = GetSchemes<
  ClassicPreset.Node,
  ClassicPreset.Connection<ClassicPreset.Node, ClassicPreset.Node>
>;

async function createEditor() {
  const socket = new ClassicPreset.Socket("socket");

  const editor = new createNodeEditor<Schemes>();

  // Create typed nodes
  const nodeA = new ClassicPreset.Node("Entry Signal");
  nodeA.addOutput("signal", new ClassicPreset.Output(socket));
  await editor.addNode(nodeA);

  // React plugin for rendering
  const react = new ReactPlugin<Schemes>();
  const connection = new ConnectionPlugin<Schemes>();

  editor.use(react);
  editor.use(connection);

  return { editor, react };
}
```

**Pros:**
- Typed socket system
- Highly customizable
- Great for complex workflows

**Cons:**
- Steeper learning curve
- Smaller community than React Flow
- More boilerplate code

**Verdict:** Use React Flow for simplicity, Rete for advanced control.

---

### 2.3 XYFlow / TipTap Flow - Lightweight Alternatives

```bash
# Lighter alternative (good for simple flows)
npm install xy-flow
```

**When to use:**
- Minimal bundle size is critical
- Simple linear flows only
- Mobile-optimized apps

Not recommended for strategy builder (limited expressiveness).

---

## Part 3: UI Component Library

### shadcn/ui + Radix UI - **RECOMMENDED**

**What is shadcn/ui?**
- Copy-paste component library built on Radix UI
- Unstyled, fully customizable with Tailwind CSS
- Type-safe with full TypeScript support
- Not a package, but a CLI that copies components into your project

**Installation:**
```bash
npm install -D shadcn-ui
npx shadcn-ui@latest init

# Install components as needed
npx shadcn-ui@latest add button
npx shadcn-ui@latest add form
npx shadcn-ui@latest add input
npx shadcn-ui@latest add select
npx shadcn-ui@latest add dialog
npx shadcn-ui@latest add tabs
npx shadcn-ui@latest add sidebar
npx shadcn-ui@latest add card
```

**Recommended Components for Strategy Builder:**

| Component | Use Case | Import |
|-----------|----------|--------|
| **Button** | Actions (Save, Deploy, Run) | `@/components/ui/button` |
| **Input** | Text parameters | `@/components/ui/input` |
| **Select** | Template/template selection | `@/components/ui/select` |
| **Form** | Parameter forms with validation | `@/components/ui/form` |
| **Dialog** | Strategy details modal | `@/components/ui/dialog` |
| **Tabs** | UI sections (Builder, Preview, JSON) | `@/components/ui/tabs` |
| **Card** | Strategy cards, node properties | `@/components/ui/card` |
| **Sidebar** | Template palette, properties panel | `@/components/ui/sidebar` |
| **Breadcrumb** | Navigation hierarchy | `@/components/ui/breadcrumb` |
| **AlertDialog** | Confirm destructive actions | `@/components/ui/alert-dialog` |
| **Popover** | Rich tooltips, quick settings | `@/components/ui/popover` |
| **Tooltip** | Inline help text | `@/components/ui/tooltip` |
| **Scroll Area** | Long lists, template palette | `@/components/ui/scroll-area` |

**Example - Using shadcn Components:**
```typescript
import { Button } from "@/components/ui/button";
import {
  Form,
  FormControl,
  FormDescription,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";

export function ParameterForm({ template }: { template: StrategyTemplate }) {
  const form = useForm<ParameterValues>({
    resolver: zodResolver(parameterSchema),
    defaultValues: template.defaultParameters,
  });

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-8">
        {template.parameters.map((param) => (
          <FormField
            key={param.name}
            control={form.control}
            name={param.name}
            render={({ field }) => (
              <FormItem>
                <FormLabel>{param.displayName}</FormLabel>
                <FormControl>
                  {param.type === 'select' ? (
                    <Select {...field}>
                      <SelectTrigger>
                        <SelectValue placeholder="Select an option" />
                      </SelectTrigger>
                      <SelectContent>
                        {param.options?.map((opt) => (
                          <SelectItem key={opt} value={opt}>
                            {opt}
                          </SelectItem>
                        ))}
                      </SelectContent>
                    </Select>
                  ) : (
                    <Input type={param.type} placeholder={param.description} {...field} />
                  )}
                </FormControl>
                <FormDescription>{param.description}</FormDescription>
                <FormMessage />
              </FormItem>
            )}
          />
        ))}
        <Button type="submit">Save Parameters</Button>
      </form>
    </Form>
  );
}
```

**Why shadcn/ui?**
- ✅ Fully customizable (you own the code)
- ✅ Excellent TypeScript support
- ✅ Beautiful default styling with Tailwind
- ✅ Accessible (built on Radix)
- ✅ Responsive by default
- ✅ No dependencies bloat (only Radix, React)

---

## Part 4: Form Handling & Validation

### React Hook Form + Zod - **RECOMMENDED**

**Why this combination?**
- Lightweight forms with minimal re-renders
- Type-safe validation with Zod
- Excellent error handling
- Works perfectly with shadcn/ui

**Installation:**
```bash
npm install react-hook-form zod @hookform/resolvers
```

**Example - Complete Parameter Form:**
```typescript
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { z } from 'zod';

// Define schema
const backBetParameterSchema = z.object({
  betType: z.enum(['Back', 'Lay']),
  odds: z.number().min(1.01).max(1000),
  stake: z.number().positive(),
  maxLiability: z.number().positive().optional(),
  betPersistence: z.enum(['Lapse', 'Persist', 'Cancel']).optional(),
});

type BackBetParameters = z.infer<typeof backBetParameterSchema>;

export function PlaceBetForm({ onSubmit }: { onSubmit: (params: BackBetParameters) => void }) {
  const form = useForm<BackBetParameters>({
    resolver: zodResolver(backBetParameterSchema),
    defaultValues: {
      betType: 'Back',
      odds: 2.5,
      stake: 10,
    },
  });

  return (
    <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-4">
      {/* Form fields with validation */}
      <div>
        <label>Bet Type</label>
        <select {...form.register('betType')}>
          <option value="Back">Back</option>
          <option value="Lay">Lay</option>
        </select>
        {form.formState.errors.betType && (
          <span className="error">{form.formState.errors.betType.message}</span>
        )}
      </div>

      <div>
        <label>Odds</label>
        <input
          type="number"
          step="0.01"
          {...form.register('odds', { valueAsNumber: true })}
        />
        {form.formState.errors.odds && (
          <span className="error">{form.formState.errors.odds.message}</span>
        )}
      </div>

      <button type="submit" disabled={form.formState.isSubmitting}>
        {form.formState.isSubmitting ? 'Saving...' : 'Save Parameters'}
      </button>
    </form>
  );
}
```

**Schema Examples by Template Type:**

```typescript
// Entry Condition
const EntryConditionSchema = z.object({
  field: z.enum(['price', 'odds', 'volume', 'rating']),
  operator: z.enum(['>', '<', '=', '>=', '<=', 'in', 'not in']),
  value: z.union([z.number(), z.string()]),
  combineWith: z.enum(['AND', 'OR']).optional(),
});

// Risk Management
const RiskManagementSchema = z.object({
  dailyLossLimit: z.number().positive().optional(),
  maxConcurrentBets: z.number().int().positive().optional(),
  stakeSize: z.number().positive(),
  stopLoss: z.number().positive().optional(),
  profitTarget: z.number().positive().optional(),
});

// Ladder Strategy
const LadderStrategySchema = z.object({
  entryOdds: z.number().min(1.01),
  ladderLevels: z.number().int().min(2).max(10),
  levelStake: z.number().positive(),
  profitTargetPerLevel: z.number().positive(),
  lossTolerance: z.number().positive(),
});
```

---

## Part 5: State Management

### TanStack Query (React Query) + Zustand

**TanStack Query** - for server state (API data)
```bash
npm install @tanstack/react-query
```

**Zustand** - for client state (UI state)
```bash
npm install zustand
```

**TanStack Query - Fetching Strategies:**
```typescript
import { useQuery, useMutation } from '@tanstack/react-query';
import { queryClient } from '@/services/queryClient';

// Fetch all strategy templates
export function useStrategyTemplates() {
  return useQuery({
    queryKey: ['templates'],
    queryFn: async () => {
      const response = await fetch('/api/templates');
      return response.json();
    },
  });
}

// Create new strategy
export function useCreateStrategy() {
  return useMutation({
    mutationFn: async (strategy: Strategy) => {
      const response = await fetch('/api/strategies', {
        method: 'POST',
        body: JSON.stringify(strategy),
      });
      return response.json();
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['strategies'] });
    },
  });
}

// Component usage
export function StrategyManager() {
  const { data: templates, isLoading } = useStrategyTemplates();
  const createMutation = useCreateStrategy();

  if (isLoading) return <div>Loading...</div>;

  return (
    <div>
      {templates?.map((template) => (
        <TemplateCard key={template.id} template={template} />
      ))}
    </div>
  );
}
```

**Zustand - UI State Management:**
```typescript
import { create } from 'zustand';
import { devtools } from 'zustand/middleware';

// UI Store
interface UIStore {
  selectedNodeId: string | null;
  setSelectedNodeId: (id: string | null) => void;
  isPreviewOpen: boolean;
  setIsPreviewOpen: (open: boolean) => void;
  activeTab: 'builder' | 'preview' | 'json';
  setActiveTab: (tab: 'builder' | 'preview' | 'json') => void;
}

export const useUIStore = create<UIStore>()(
  devtools((set) => ({
    selectedNodeId: null,
    setSelectedNodeId: (id) => set({ selectedNodeId: id }),
    isPreviewOpen: false,
    setIsPreviewOpen: (open) => set({ isPreviewOpen: open }),
    activeTab: 'builder',
    setActiveTab: (tab) => set({ activeTab: tab }),
  }))
);

// Strategy Store
interface StrategyStore {
  strategy: Strategy | null;
  setStrategy: (strategy: Strategy) => void;
  updateNode: (nodeId: string, data: any) => void;
  addNode: (node: Node) => void;
  removeNode: (nodeId: string) => void;
}

export const useStrategyStore = create<StrategyStore>()(
  devtools((set) => ({
    strategy: null,
    setStrategy: (strategy) => set({ strategy }),
    updateNode: (nodeId, data) =>
      set((state) => {
        if (!state.strategy) return {};
        return {
          strategy: {
            ...state.strategy,
            strategies: state.strategy.strategies.map((s) =>
              s.id === nodeId ? { ...s, ...data } : s
            ),
          },
        };
      }),
    addNode: (node) =>
      set((state) => {
        if (!state.strategy) return {};
        return {
          strategy: {
            ...state.strategy,
            strategies: [...state.strategy.strategies, node],
          },
        };
      }),
    removeNode: (nodeId) =>
      set((state) => {
        if (!state.strategy) return {};
        return {
          strategy: {
            ...state.strategy,
            strategies: state.strategy.strategies.filter((s) => s.id !== nodeId),
          },
        };
      }),
  }))
);

// Usage in components
function StrategyBuilder() {
  const strategy = useStrategyStore((state) => state.strategy);
  const updateNode = useStrategyStore((state) => state.updateNode);

  return (
    <div>
      {strategy?.strategies.map((node) => (
        <NodeEditor
          key={node.id}
          node={node}
          onUpdate={(data) => updateNode(node.id, data)}
        />
      ))}
    </div>
  );
}
```

---

## Part 6: Styling & Design System

### Tailwind CSS + CSS Modules

**Tailwind Installation:**
```bash
npm install -D tailwindcss postcss autoprefixer
npx tailwindcss init -p
```

**tailwind.config.ts:**
```typescript
import type { Config } from 'tailwindcss';

export default {
  content: [
    './index.html',
    './src/**/*.{js,ts,jsx,tsx}',
  ],
  theme: {
    extend: {
      colors: {
        // Strategy builder specific colors
        'strategy-primary': 'hsl(var(--color-strategy-primary))',
        'strategy-success': 'hsl(var(--color-strategy-success))',
        'strategy-warning': 'hsl(var(--color-strategy-warning))',
        'strategy-error': 'hsl(var(--color-strategy-error))',
      },
      spacing: {
        'canvas': '16px',
      },
    },
  },
  plugins: [require('@tailwindcss/typography'), require('@tailwindcss/forms')],
} satisfies Config;
```

**Global Styles (globals.css):**
```css
@tailwind base;
@tailwind components;
@tailwind utilities;

@layer components {
  .strategy-node {
    @apply bg-white border-2 border-gray-300 rounded-lg shadow-md p-3;
  }

  .strategy-node.selected {
    @apply border-blue-500 shadow-lg;
  }

  .node-header {
    @apply flex items-center gap-2 pb-2 border-b;
  }

  .node-title {
    @apply font-semibold text-sm;
  }

  .node-template {
    @apply text-xs text-gray-600;
  }
}
```

---

## Part 7: Data Visualization

### Recharts - For Strategy Analytics

```bash
npm install recharts
```

**Backtest Results Visualization:**
```typescript
import {
  LineChart,
  Line,
  BarChart,
  Bar,
  XAxis,
  YAxis,
  CartesianGrid,
  Tooltip,
  Legend,
  ResponsiveContainer,
} from 'recharts';

export function BacktestChart({ data }: { data: BacktestResult[] }) {
  return (
    <ResponsiveContainer width="100%" height={300}>
      <LineChart data={data}>
        <CartesianGrid strokeDasharray="3 3" />
        <XAxis dataKey="date" />
        <YAxis yAxisId="left" />
        <YAxis yAxisId="right" orientation="right" />
        <Tooltip />
        <Legend />
        <Line
          yAxisId="left"
          type="monotone"
          dataKey="cumulativeProfit"
          stroke="#8884d8"
          name="Cumulative P&L"
        />
        <Line
          yAxisId="right"
          type="monotone"
          dataKey="winRate"
          stroke="#82ca9d"
          name="Win Rate %"
        />
      </LineChart>
    </ResponsiveContainer>
  );
}

export function StrategyMetricsPanel({ metrics }: { metrics: StrategyMetrics }) {
  return (
    <div className="grid grid-cols-4 gap-4">
      <MetricCard
        label="Total Profit"
        value={`€${metrics.totalProfit.toFixed(2)}`}
        color="green"
      />
      <MetricCard
        label="Win Rate"
        value={`${(metrics.winRate * 100).toFixed(1)}%`}
        color="blue"
      />
      <MetricCard
        label="Max Drawdown"
        value={`${(metrics.maxDrawdown * 100).toFixed(1)}%`}
        color="orange"
      />
      <MetricCard
        label="Profit Factor"
        value={metrics.profitFactor.toFixed(2)}
        color="purple"
      />
    </div>
  );
}
```

### Visx (from Airbnb) - Advanced Visualizations

```bash
npm install @visx/visx
```

**Flow Diagram with Visx:**
```typescript
import { Group } from '@visx/group';
import { HierarchyPointNode, hierarchy, tree } from '@visx/hierarchy';

export function StrategyFlowDiagram({ strategy }: { strategy: Strategy }) {
  const data = hierarchyFromStrategy(strategy);
  const treeLayout = tree<StrategyNode>({ width: 800, height: 400 });
  const root = hierarchy(data);
  const treeData = treeLayout(root);

  return (
    <svg width={800} height={400}>
      <Group top={20} left={40}>
        {treeData.links().map((link, i) => (
          <line
            key={`link-${i}`}
            x1={link.source.x}
            y1={link.source.y}
            x2={link.target.x}
            y2={link.target.y}
            stroke="rgba(0,0,0,0.2)"
            strokeWidth={1}
          />
        ))}
        {treeData.descendants().map((node, i) => (
          <Group key={`node-${i}`} left={node.x} top={node.y}>
            <circle r={8} fill="steelblue" />
            <text y={20} textAnchor="middle" fontSize={10}>
              {node.data.name}
            </text>
          </Group>
        ))}
      </Group>
    </svg>
  );
}
```

---

## Part 8: Backend Integration

### TypeScript API Client

**services/mcpClient.ts:**
```typescript
import { z } from 'zod';

// Type-safe MCP client
export class McpClient {
  private baseUrl = process.env.REACT_APP_API_URL || 'http://localhost:3000';

  async getStrategyTemplates(): Promise<StrategyTemplate[]> {
    const response = await fetch(`${this.baseUrl}/api/templates`);
    return response.json();
  }

  async createStrategy(strategy: Strategy): Promise<Strategy> {
    const response = await fetch(`${this.baseUrl}/api/strategies`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(strategy),
    });
    return response.json();
  }

  async executeStrategy(
    strategyId: string,
    marketId: string,
    selectionId: string
  ): Promise<ExecutionResult> {
    const response = await fetch(
      `${this.baseUrl}/api/strategies/${strategyId}/execute`,
      {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ marketId, selectionId }),
      }
    );
    return response.json();
  }

  async validateStrategy(strategy: Strategy): Promise<ValidationResult> {
    const response = await fetch(`${this.baseUrl}/api/strategies/validate`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(strategy),
    });
    return response.json();
  }

  async backtest(strategy: Strategy, params: BacktestParams): Promise<BacktestResult> {
    const response = await fetch(`${this.baseUrl}/api/strategies/backtest`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ strategy, ...params }),
    });
    return response.json();
  }
}

export const mcpClient = new McpClient();
```

**hooks/useMcpClient.ts:**
```typescript
import { useQuery, useMutation } from '@tanstack/react-query';
import { mcpClient } from '@/services/mcpClient';

export function useStrategyTemplates() {
  return useQuery({
    queryKey: ['templates'],
    queryFn: () => mcpClient.getStrategyTemplates(),
    staleTime: 1000 * 60 * 5, // 5 minutes
  });
}

export function useExecuteStrategy() {
  return useMutation({
    mutationFn: ({
      strategyId,
      marketId,
      selectionId,
    }: {
      strategyId: string;
      marketId: string;
      selectionId: string;
    }) => mcpClient.executeStrategy(strategyId, marketId, selectionId),
  });
}

export function useBacktestStrategy() {
  return useMutation({
    mutationFn: ({
      strategy,
      params,
    }: {
      strategy: Strategy;
      params: BacktestParams;
    }) => mcpClient.backtest(strategy, params),
  });
}
```

---

## Part 9: Build & Deployment

### Vite - Fast Build Tool

**vite.config.ts:**
```typescript
import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react-swc';
import path from 'path';

export default defineConfig({
  plugins: [react()],
  resolve: {
    alias: {
      '@': path.resolve(__dirname, './src'),
    },
  },
  build: {
    target: 'ES2020',
    minify: 'terser',
    sourcemap: true,
    rollupOptions: {
      output: {
        manualChunks: {
          'react-flow': ['reactflow'],
          'ui-components': ['@radix-ui/react-dialog', '@radix-ui/react-tabs'],
        },
      },
    },
  },
  server: {
    proxy: {
      '/api': {
        target: 'http://localhost:3000',
        changeOrigin: true,
      },
    },
  },
});
```

**Build Commands:**
```json
{
  "scripts": {
    "dev": "vite",
    "build": "tsc && vite build",
    "preview": "vite preview",
    "lint": "eslint . --ext .ts,.tsx",
    "type-check": "tsc --noEmit",
    "format": "prettier --write ."
  }
}
```

---

## Part 10: Complete Library Dependency List

### Recommended `package.json`

```json
{
  "name": "strategy-builder",
  "version": "1.0.0",
  "type": "module",
  "scripts": {
    "dev": "vite",
    "build": "tsc && vite build",
    "preview": "vite preview",
    "lint": "eslint . --ext .ts,.tsx",
    "type-check": "tsc --noEmit",
    "format": "prettier --write ."
  },
  "dependencies": {
    "react": "^18.2.0",
    "react-dom": "^18.2.0",
    "reactflow": "^11.11.0",
    "react-hook-form": "^7.51.0",
    "zod": "^3.22.0",
    "@hookform/resolvers": "^3.3.0",
    "@tanstack/react-query": "^5.28.0",
    "zustand": "^4.4.1",
    "recharts": "^2.10.0",
    "@visx/visx": "^3.10.0",
    "shadcn-ui": "^0.7.0",
    "@radix-ui/react-dialog": "^1.1.1",
    "@radix-ui/react-tabs": "^1.0.4",
    "@radix-ui/react-select": "^2.0.0",
    "@radix-ui/react-form": "^0.0.1",
    "@radix-ui/react-popover": "^1.0.7",
    "@radix-ui/react-tooltip": "^1.0.7",
    "class-variance-authority": "^0.7.0",
    "clsx": "^2.0.0",
    "tailwind-merge": "^2.2.0"
  },
  "devDependencies": {
    "@types/react": "^18.2.0",
    "@types/react-dom": "^18.2.0",
    "@types/node": "^20.0.0",
    "typescript": "^5.3.0",
    "vite": "^5.0.0",
    "@vitejs/plugin-react-swc": "^3.0.0",
    "tailwindcss": "^3.3.0",
    "postcss": "^8.4.0",
    "autoprefixer": "^10.4.0",
    "eslint": "^8.50.0",
    "eslint-plugin-react": "^7.33.0",
    "@typescript-eslint/eslint-plugin": "^6.0.0",
    "@typescript-eslint/parser": "^6.0.0",
    "prettier": "^3.0.0"
  }
}
```

---

## Part 11: Example Project Structure with All Libraries

```
strategy-builder/
├── src/
│   ├── components/
│   │   ├── StrategyBuilder/
│   │   │   ├── FlowCanvas.tsx         (React Flow)
│   │   │   ├── NodePalette.tsx        (Template library)
│   │   │   ├── PropertiesPanel.tsx    (Node properties - React Hook Form)
│   │   │   ├── PreviewPane.tsx        (Recharts visualizations)
│   │   │   └── StrategyBuilder.tsx    (Container component)
│   │   ├── Forms/
│   │   │   ├── ParameterForm.tsx      (React Hook Form + Zod)
│   │   │   ├── ConditionBuilder.tsx   (Complex conditions)
│   │   │   └── RiskManagementForm.tsx
│   │   ├── ui/                         (shadcn/ui components)
│   │   │   ├── button.tsx
│   │   │   ├── input.tsx
│   │   │   ├── select.tsx
│   │   │   ├── form.tsx
│   │   │   ├── dialog.tsx
│   │   │   ├── tabs.tsx
│   │   │   └── card.tsx
│   │   └── Common/
│   │       ├── Header.tsx
│   │       ├── Sidebar.tsx
│   │       └── StatusBar.tsx
│   ├── hooks/
│   │   ├── useStrategy.ts             (Business logic)
│   │   ├── useFlow.ts                 (React Flow integration)
│   │   ├── useMcpClient.ts            (API integration - React Query)
│   │   └── useNotifications.ts
│   ├── services/
│   │   ├── mcpClient.ts               (API client)
│   │   ├── strategyValidator.ts
│   │   └── api.ts
│   ├── store/
│   │   ├── strategyStore.ts           (Zustand)
│   │   ├── uiStore.ts                 (Zustand)
│   │   └── notificationStore.ts
│   ├── types/
│   │   ├── strategy.ts
│   │   ├── templates.ts
│   │   └── api.ts
│   ├── utils/
│   │   ├── validators.ts
│   │   ├── transformers.ts
│   │   └── helpers.ts
│   ├── styles/
│   │   ├── globals.css                (Tailwind + custom)
│   │   └── variables.css
│   ├── App.tsx
│   └── main.tsx
├── public/
│   └── index.html
├── vite.config.ts
├── tailwind.config.ts
├── postcss.config.js
├── tsconfig.json
├── .eslintrc.json
├── .prettierrc
└── package.json
```

---

## Part 12: Quick Start Guide

### 1. Create Project
```bash
npm create vite@latest strategy-builder -- --template react-ts
cd strategy-builder
npm install
```

### 2. Install Core Dependencies
```bash
npm install react-hook-form zod @hookform/resolvers
npm install @tanstack/react-query zustand
npm install reactflow recharts
npm install -D tailwindcss postcss autoprefixer
npm install -D shadcn-ui
```

### 3. Setup Tailwind
```bash
npx tailwindcss init -p
npx shadcn-ui@latest init
```

### 4. Install shadcn Components
```bash
npx shadcn-ui@latest add button form input select tabs dialog card
```

### 5. Basic App Structure
```typescript
// src/App.tsx
import { QueryClientProvider } from '@tanstack/react-query';
import { queryClient } from '@/services/queryClient';
import { StrategyBuilder } from '@/components/StrategyBuilder/StrategyBuilder';

export default function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <StrategyBuilder />
    </QueryClientProvider>
  );
}
```

### 6. Run Development Server
```bash
npm run dev
```

---

## Comparison with Alternatives

| Library | Purpose | Size | Learning Curve | Verdict |
|---------|---------|------|-----------------|---------|
| **React Flow** | Node-based UI | 150KB | Medium | ⭐ BEST |
| Rete.js | Advanced flows | 200KB | High | Alternative |
| Blockly | Visual blocks | 300KB | High | For simple flows |
| **React Hook Form** | Forms | 30KB | Low | ⭐ BEST |
| Formik | Forms | 100KB | Medium | Older alternative |
| **Zod** | Validation | 20KB | Low | ⭐ BEST |
| Yup | Validation | 30KB | Low | Alternative |
| **Zustand** | State | 1KB | Very Low | ⭐ BEST |
| Redux | State | 50KB | High | Overkill for this |
| **Recharts** | Charts | 100KB | Low | ⭐ BEST |
| Chart.js | Charts | 80KB | Low | Alternative |
| **shadcn/ui** | Components | Copy-paste | Very Low | ⭐ BEST |
| Material-UI | Components | 500KB | Medium | Too heavy |

---

## Performance Optimization Tips

### 1. Code Splitting
```typescript
// Lazy load heavy components
const StrategyBuilder = lazy(() => import('@/components/StrategyBuilder'));
const BacktestResults = lazy(() => import('@/components/BacktestResults'));

export function App() {
  return (
    <Suspense fallback={<LoadingSpinner />}>
      <Routes>
        <Route path="/builder" element={<StrategyBuilder />} />
        <Route path="/results" element={<BacktestResults />} />
      </Routes>
    </Suspense>
  );
}
```

### 2. Memoization
```typescript
import { memo } from 'react';

const StrategyNode = memo(({ data, isSelected }: NodeProps) => {
  return <div>{/* node content */}</div>;
}, (prevProps, nextProps) => {
  return prevProps.isSelected === nextProps.isSelected &&
         prevProps.data === nextProps.data;
});
```

### 3. Query Optimization
```typescript
// Cache templates for 5 minutes
useQuery({
  queryKey: ['templates'],
  queryFn: fetchTemplates,
  staleTime: 1000 * 60 * 5,
  gcTime: 1000 * 60 * 10,
});
```

---

## Recommended Tech Stack Summary

```
✅ Frontend:         React 18 + TypeScript
✅ Flow Builder:     React Flow v11+
✅ Forms:           React Hook Form + Zod
✅ State:           Zustand + React Query
✅ UI Components:   shadcn/ui + Radix UI
✅ Styling:         Tailwind CSS
✅ Visualization:   Recharts
✅ Build Tool:      Vite
✅ API Client:      TypeScript fetch client
✅ Testing:         Vitest + React Testing Library (optional)
```

This stack provides:
- ⚡ Fast development with Vite
- 🎯 Type-safe with TypeScript
- 📦 Small bundle size (~250KB gzipped)
- 🎨 Beautiful, customizable UI
- 🔄 Excellent DX with hot reload
- 📱 Responsive and accessible
- ♿ WCAG 2.1 compliant components

---

**Document Version**: 1.0  
**Last Updated**: April 28, 2026  
**Technology Versions**: React 18, Vite 5, TypeScript 5, React Flow 11
