import dynamic from "next/dynamic";
import { chartConfig, useChartTheme } from "./chartConfig";
import { BarChartData } from "@/app/hooks/useGetBarChartData";

// lazy load 'react-plotly.js'
const Plot = dynamic(() => import("react-plotly.js"), { ssr: false });

const BarChart = ({ title, x, y }: BarChartData) => {
  //check darmode and set graph theme
  const theme = useChartTheme();
  const chartData: Partial<Plotly.Data>[] = [
    {
      x,
      y,
      type: "bar",
      text: x.map((value) => value + " %"),
      textposition: "outside",
      textfont: { size: 12 },
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
      automargin: true,
    },
    autosize: false,
    margin: {
      l: 0,
      r: 0,
      t: title?.length ? 40 : 0,
      b: 0,
    },
    yaxis: {
      automargin: true,
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
    fillFrame: false,
    autosizable: true
  };


  return (
    <Plot className="w-full h-full" data={chartData} layout={layout} config={config} />
  );
};

export default BarChart;
