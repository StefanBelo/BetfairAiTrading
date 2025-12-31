import React from 'react';
import { useStore } from '../store/useStore';
import './SelectionPicker.css';

export const SelectionPicker: React.FC = () => {
  const { selectedMarket, selectedSelection, selectSelection } = useStore();

  if (!selectedMarket || !selectedMarket.selections || selectedMarket.selections.length === 0) {
    return null;
  }

  return (
    <div className="card">
      <div className="card-body">
        <div className="row align-items-center">
          <div className="col-md-3">
            <label htmlFor="selection-dropdown" className="form-label fw-semibold mb-md-0 fs-md">
              <i className="ti ti-user me-2 fs-lg"></i>Select Runner:
            </label>
          </div>
          <div className="col-md-9">
            <select
              id="selection-dropdown"
              className="form-select"
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
                  {(selection.price || selection.odds) ? ` @ ${selection.price || selection.odds}` : ''}
                </option>
              ))}
            </select>
          </div>
        </div>
      </div>
    </div>
  );
};
