import React from 'react';
import { MarketSelector } from './components/MarketSelector';
import { DataContextSelector } from './components/DataContextSelector';
import { SelectionPicker } from './components/SelectionPicker';
import { TableDisplay } from './components/TableDisplay';
import { ChartDisplay } from './components/ChartDisplay';
import { useStore } from './store/useStore';
import './App.css';

function App() {
  const { selectedDataContext, error } = useStore();

  return (
    <div className="app">
      <header className="app-header">
        <div className="header-content">
          <div className="logo">📊</div>
          <h1>Market Data Browser</h1>
        </div>
      </header>

      <div className="app-container">
        <aside className="sidebar">
          <MarketSelector />
        </aside>

        <main className="main-content">
          <div className="controls">
            <DataContextSelector />
            {selectedDataContext?.type === 'chart' && <SelectionPicker />}
          </div>

          {error && (
            <div className="error-message">
              <strong>Error:</strong> {error}
            </div>
          )}

          <div className="data-display">
            {!selectedDataContext && (
              <div className="welcome-message">
                <h2>Welcome to Market Data Browser</h2>
                <p>Select a market from the sidebar and choose a data context to get started.</p>
              </div>
            )}

            {selectedDataContext?.type === 'table' && <TableDisplay />}
            {selectedDataContext?.type === 'chart' && <ChartDisplay />}
          </div>
        </main>
      </div>
    </div>
  );
}

export default App;
