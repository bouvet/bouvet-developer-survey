"use client";
import dynamic from "next/dynamic";
import chartData from "./barchartData.json";
import { barchartConfig } from "./barchartConfig";

// lazy load 'react-plotly.js'
const Plot = dynamic(() => import("react-plotly.js"), {
  ssr: false,
  loading: () => <p>Laster...</p>,
});

const BarChartJson = () => {
  const data = chartData.data.map((trace) => {
    return {
      x: [trace.x],
      y: [trace.y],
      type: "bar",
      text: [trace.x + "%"],
      marker: {
        color: barchartConfig.barColor,
      },
      orientation: "h",
    };
  });

  const layout = {
    title: chartData.name,
    xaxis: {
      range: [0, 100],
      visible: false,
    },
    margin: {
      l: 140,
      t: 70,
      b: 70,
    },
    legend: {
      visible: false,
    },
    font: {
      color: barchartConfig.fontColor,
      size: barchartConfig.fontSize,
    },
    paper_bgcolor: barchartConfig.graphBackgroundColor,
    plot_bgcolor: barchartConfig.graphBackgroundColor,
  };

  const config = { responsive: true, displayModeBar: false };

  return (
    <Plot className="w-full" data={data} layout={layout} config={config} />
  );
};

export default BarChartJson;
