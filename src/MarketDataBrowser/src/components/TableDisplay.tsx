import React, { useMemo } from 'react';
import { AgGridReact } from 'ag-grid-react';
import { useStore } from '../store/useStore';
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-alpine.css';
import './TableDisplay.css';

// Convert camelCase to Title Case
const camelToTitle = (str: string): string => {
  return str
    .replace(/([A-Z])/g, ' $1')
    .replace(/^./, (s) => s.toUpperCase())
    .trim();
};

// Value formatter for boolean fields
const booleanFormatter = (params: any) => {
  if (typeof params.value === 'boolean') {
    return params.value ? '✓' : '✗';
  }
  return params.value;
};

export const TableDisplay: React.FC = () => {
  const { tableData, isLoading } = useStore();

  const columnDefs = useMemo(() => {
    if (!tableData || tableData.length === 0) return [];

    // Automatically generate columns from data keys
    const keys = Object.keys(tableData[0]);
    const allColumns = keys.length;
    
    return keys.map((key) => {
      // Special handling for comment-type fields (wrap text)
      const isCommentField = key.toLowerCase().includes('comment') || 
                            key.toLowerCase().includes('description') ||
                            key.toLowerCase().includes('prerace') ||
                            key.toLowerCase().endsWith('desc');
      
      // Check if this is a description field that should span full width
      const isFullWidthDesc = key.toLowerCase().endsWith('desc') || 
                             key.toLowerCase().includes('racedesc');
      
      return {
        field: key,
        headerName: camelToTitle(key),
        sortable: true,
        filter: true,
        resizable: true,
        valueFormatter: booleanFormatter,
        // Pin 'name' column to the left (except for full-width fields)
        pinned: (key === 'name' && !isFullWidthDesc) ? 'left' : undefined,
        width: key === 'name' ? 150 : isCommentField ? 350 : undefined,
        wrapText: isCommentField,
        autoHeight: isCommentField,
        cellStyle: isCommentField ? { lineHeight: '1.5', paddingTop: '8px', paddingBottom: '8px' } : undefined,
        // Make description fields span all columns
        colSpan: (params: any) => {
          if (isFullWidthDesc && params.data) {
            return allColumns;
          }
          return 1;
        },
        cellClass: isFullWidthDesc ? 'full-width-cell' : undefined,
      };
    });
  }, [tableData]);

  const defaultColDef = useMemo(() => ({
    flex: 1,
    minWidth: 100,
  }), []);

  if (isLoading) {
    return <div className="table-loading">Loading data...</div>;
  }

  if (!tableData || tableData.length === 0) {
    return <div className="table-empty">No data available</div>;
  }

  return (
    <div className="table-display ag-theme-alpine">
      <AgGridReact
        rowData={tableData}
        columnDefs={columnDefs}
        defaultColDef={defaultColDef}
        pagination={true}
        paginationPageSize={50}
        domLayout="autoHeight"
      />
    </div>
  );
};
