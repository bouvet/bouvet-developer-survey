"use client";
import dynamic from "next/dynamic";
import chartData from "./chartData.json";
import { chartConfig } from "./chartConfig";

// lazy load 'react-plotly.js'
const Plot = dynamic(() => import("react-plotly.js"), {
  ssr: false,
  loading: () => <p>Laster...</p>,
});

const DotPlotChartJson = () => {
  const traces = chartData.data.map((trace) => {
    return {
      x: [trace.xMin, trace.xMax],
      y: new Array(2).fill(trace.label), // create an array with the same y label for each value
      mode: "lines+markers+text",
      name: trace.label,
      type: "scatter",
      text: [trace.xMin + " %", trace.xMax + " %"],
      textposition: ["left", "right"],
      marker: {
        color: [chartConfig.dotMinColor, chartConfig.dotMaxColor],
        symbol: "circle",
        size: chartConfig.markerSize,
      },
      line: {
        color: chartConfig.thickLineColor,
        width: chartConfig.thickLineWidth,
      },
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
      gridwidth: chartConfig.dottedLineWidth,
      gridcolor: chartConfig.dottedLineColor,
    },
    margin: {
      l: 80,
      r: 40,
      b: 50,
      t: 80,
    },
    legend: {
      visible: false,
    },

    font: {
      color: chartConfig.fontColor,
      size: chartConfig.fontSize,
    },
    autosize: true,
    paper_bgcolor: chartConfig.graphBackgroundColor,
    plot_bgcolor: chartConfig.graphBackgroundColor,
  };

  const config = { responsive: true, displayModeBar: false };

  return (
    <Plot className="w-full" data={traces} layout={layout} config={config} />
  );
};

export default DotPlotChartJson;
