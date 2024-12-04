import dynamic from "next/dynamic";
import { barchartConfig } from "./barchartConfig";

// lazy load 'react-plotly.js'
const Plot = dynamic(() => import("react-plotly.js"), { ssr: false });

interface ChartProps {
  title: string;
  y: string[];
  x: number[];
}

const BarChart = ({ title, x, y }: ChartProps) => {
  const chartData: Partial<Plotly.Data>[] = [
    {
      x,
      y,
      type: "bar",
      text: x.map((value) => value + " %"),
      marker: {
        color: barchartConfig.barColor,
      },
      orientation: "h",
    },
  ];

  const layout: Partial<Plotly.Layout> = {
    title,
    xaxis: {
      range: [0, 100],
      visible: false,
    },
    margin: {
      l: 140,
      t: 70,
      b: 70,
    },
    showlegend: false,
    font: {
      color: barchartConfig.fontColor,
      size: barchartConfig.fontSize,
    },
    paper_bgcolor: barchartConfig.graphBackgroundColor,
    plot_bgcolor: barchartConfig.graphBackgroundColor,
  };

  const config: Partial<Plotly.Config> = {
    responsive: true,
    displayModeBar: false,
  };

  return (
    <Plot className="w-full" data={chartData} layout={layout} config={config} />
  );
};

export default BarChart;
