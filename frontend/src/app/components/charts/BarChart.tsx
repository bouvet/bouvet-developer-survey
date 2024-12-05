import dynamic from "next/dynamic";
import { chartConfig, useChartTheme } from "./chartConfig";

// lazy load 'react-plotly.js'
const Plot = dynamic(() => import("react-plotly.js"), { ssr: false });

interface ChartProps {
  title?: string;
  y: string[];
  x: number[];
}

const BarChart = ({ title, x, y }: ChartProps) => {

  //check darmode and set graph theme
  const theme = useChartTheme();

  const chartData: Partial<Plotly.Data>[] = [
    {
      x,
      y,
      type: "bar",
      text: x.map((value) => value + " %"),
      marker: {
        color: theme.barColor,
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
      t: title?.length ? 40 : 0,
      b: 70,
    },
    showlegend: false,
    font: {
      color: theme.fontColor,
      size: chartConfig.fontSize,
    },
    paper_bgcolor: theme.graphBackgroundColor,
    plot_bgcolor: theme.graphBackgroundColor,
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
