"use client";
import dynamic from "next/dynamic";
import { barchartConfig } from "./barchartConfig";

// lazy load 'react-plotly.js'
const Plot = dynamic(() => import("react-plotly.js"), { ssr: false });

interface chartProps {
  title: string;
  y: string[];
  x: number[];
}

export default function BarChart(props: chartProps) {
  const chartData = [
    {
      x: props.x,
      y: props.y,
      type: "bar",
      text: props.x.map((value) => value + " %"),
      marker: {
        color: barchartConfig.barColor,
      },
      orientation: "h",
    },
  ];

  const layout = {
    title: props.title,
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
    <Plot className="w-full" data={chartData} layout={layout} config={config} />
  );
}
