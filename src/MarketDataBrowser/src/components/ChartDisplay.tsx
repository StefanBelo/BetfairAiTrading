import React, { useEffect, useRef } from 'react';
import { createChart, IChartApi, ISeriesApi } from 'lightweight-charts';
import { useStore } from '../store/useStore';
import './ChartDisplay.css';

export const ChartDisplay: React.FC = () => {
  const { chartData, isLoading, selectedSelection } = useStore();
  const chartContainerRef = useRef<HTMLDivElement>(null);
  const chartRef = useRef<IChartApi | null>(null);

  useEffect(() => {
    if (!chartContainerRef.current || !chartData) {
      return;
    }

    // Clean up previous chart
    if (chartRef.current) {
      chartRef.current.remove();
      chartRef.current = null;
    }

    try {
      // Extract the actual price data
      let dataArray: any[] = [];
      if (chartData.selectionsData && chartData.selectionsData.length > 0) {
        const selectionData = chartData.selectionsData[0].data;
        dataArray = selectionData?.timePriceVolumes || [];
      }

      if (dataArray.length === 0) {
        console.warn('No price data available');
        return;
      }

      // Transform and sort data
      const formattedData = dataArray
        .map((point: any) => {
          const timeInSeconds = Math.floor(new Date(point.time).getTime() / 1000);
          return {
            time: timeInSeconds,
            price: point.price || 0,
            volume: point.volume || 0,
          };
        })
        .sort((a, b) => a.time - b.time);

      // Remove duplicates (keep highest price and sum volumes for same timestamp)
      const uniqueData = new Map();
      formattedData.forEach(point => {
        if (!uniqueData.has(point.time)) {
          uniqueData.set(point.time, { ...point });
        } else {
          const existing = uniqueData.get(point.time);
          if (existing.price < point.price) {
            existing.price = point.price;
          }
          existing.volume += point.volume;
        }
      });
      
      const finalData = Array.from(uniqueData.values());

      if (finalData.length === 0) {
        return;
      }

      // Prepare price and volume data
      const priceData = finalData.map(d => ({ time: d.time, value: d.price }));
      const volumeData = finalData.map(d => ({ 
        time: d.time, 
        value: d.volume,
        color: 'rgba(41, 98, 255, 0.5)' // Semi-transparent blue
      }));

      // Create chart with more height for volume
      const chart = createChart(chartContainerRef.current, {
        width: chartContainerRef.current.clientWidth,
        height: 500,
        layout: {
          background: { color: '#ffffff' },
          textColor: '#333',
        },
        grid: {
          vertLines: { color: '#e1e1e1' },
          horzLines: { color: '#e1e1e1' },
        },
        timeScale: {
          timeVisible: true,
          secondsVisible: false,
          borderColor: '#d1d4dc',
        },
        rightPriceScale: {
          borderColor: '#d1d4dc',
        },
        leftPriceScale: {
          visible: false,
        },
        crosshair: {
          mode: 1, // CrosshairMode.Normal
        },
      });

      // Add price line series
      const lineSeries = chart.addLineSeries({
        color: '#2962FF',
        lineWidth: 2,
        title: 'Price',
        priceLineVisible: false,
        lastValueVisible: true,
      });
      lineSeries.setData(priceData);

      // Add volume histogram series
      const volumeSeries = chart.addHistogramSeries({
        color: '#26a69a',
        priceFormat: {
          type: 'volume',
        },
        priceScaleId: 'volume',
        title: 'Volume',
        priceLineVisible: false,
        lastValueVisible: true,
      });
      
      volumeSeries.priceScale().applyOptions({
        scaleMargins: {
          top: 0.7, // Volume takes bottom 30%
          bottom: 0,
        },
        visible: true,
        alignLabels: false,
        borderColor: '#d1d4dc',
        scaleLabels: {
          visible: true,
        },
      });

      volumeSeries.setData(volumeData);
      chart.timeScale().fitContent();

      chartRef.current = chart;

      // Handle window resize
      const resizeObserver = new ResizeObserver(() => {
        if (chartContainerRef.current && chartRef.current) {
          chartRef.current.applyOptions({
            width: chartContainerRef.current.clientWidth,
          });
        }
      });

      resizeObserver.observe(chartContainerRef.current);

      return () => {
        resizeObserver.disconnect();
        if (chartRef.current) {
          chartRef.current.remove();
          chartRef.current = null;
        }
      };
    } catch (error) {
      console.error('Error rendering chart:', error);
    }
  }, [chartData]);

  if (isLoading) {
    return <div className="chart-loading">Loading chart data...</div>;
  }

  if (!selectedSelection) {
    return <div className="chart-empty">Select a runner to view price history</div>;
  }

  if (!chartData) {
    return <div className="chart-empty">No chart data available</div>;
  }

  const dataCount = chartData?.selectionsData?.[0]?.data?.timePriceVolumes?.length || 0;
  
  if (dataCount === 0) {
    return <div className="chart-empty">No price history available for this selection</div>;
  }

  return (
    <div className="chart-display">
      <div className="chart-header">
        <h3>{selectedSelection.name || selectedSelection.selectionName}</h3>
        <p>Price & Volume History - {dataCount} data points</p>
      </div>
      <div ref={chartContainerRef} className="chart-container" />
    </div>
  );
};
