import React from 'react';
import { useStore } from '../store/useStore';
import './SelectionPicker.css';

export const SelectionPicker: React.FC = () => {
  const { selectedMarket, selectedSelection, selectSelection } = useStore();

  if (!selectedMarket || !selectedMarket.selections || selectedMarket.selections.length === 0) {
    return null;
  }

  return (
    <div className="selection-picker">
      <label htmlFor="selection-dropdown">Select Runner:</label>
      <select
        id="selection-dropdown"
        value={selectedSelection?.selectionId || ''}
        onChange={(e) => {
          const selection = selectedMarket.selections.find(
            (s) => s.selectionId === e.target.value
          );
          if (selection) {
            selectSelection(selection);
          }
        }}
      >
        <option value="">-- Choose a selection --</option>
        {selectedMarket.selections.map((selection) => (
          <option key={selection.selectionId} value={selection.selectionId}>
            {selection.name || selection.selectionName}
            {(selection.price || selection.odds) ? ` (${selection.price || selection.odds})` : ''}
          </option>
        ))}
      </select>
    </div>
  );
};
