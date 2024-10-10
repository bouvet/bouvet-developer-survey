"use client";
import dynamic from 'next/dynamic';
import chartData from './chartData.json';

// lazy load 'react-plotly.js'
const Plot = dynamic(() => import('react-plotly.js'), { ssr: false });

// Graph colors
const dotMinColor =     '#F9A86F';
const dotMaxColor =     '#1D43C6';
const dottedLineColor = '#ddd';
const thickLineColor =  '#ccc';
const dottedLineWidth =  1;
const thickLineWidth =   3;
const markerSize =       15;
const graphBackgroundColor = '#faf1e3';
const fontColor =        '#555';

const DotPlotChartJson = () => {

    const traces = chartData.data.map((trace) => {
      return {
        x: [trace.xMin, trace.xMax],
        y: new Array(2).fill(trace.label), // create an array with the same y label for each value
        mode: 'lines+markers+text',
        name: trace.label,
        type: 'scatter',
        text: [trace.xMin + " %", trace.xMax + " %"],
        textposition: ["left", "right"],
        marker: {
          color: [dotMinColor, dotMaxColor],
          symbol: 'circle',
          size: markerSize
        },
        line: { 
          color: thickLineColor,
          width: thickLineWidth
        }
      };
    });

    const layout = {
      title: chartData.name,
      xaxis: {
          showgrid: false,
          visible: false,
      },
      yaxis: {
        showline: false,
        gridwidth: dottedLineWidth,
        gridcolor: dottedLineColor,

      },
      margin: {
        l: 80,
        r: 40,
        b: 50,
        t: 80
      },
      legend: {
        visible: false
      },

      font: {
        color: fontColor
      },
      autosize: true,
      paper_bgcolor: graphBackgroundColor,
      plot_bgcolor: graphBackgroundColor,
    };

    const config = {responsive: true, displayModeBar: false};

    return (
        <Plot
            className="w-full"
            data={traces}
            layout={layout}
            config={config}
        />
    );
}

export default DotPlotChartJson;
