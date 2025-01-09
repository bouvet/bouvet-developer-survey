import dynamic from "next/dynamic";
import { chartConfig, getGridShapes, useChartTheme } from "./chartConfig";
import { BarChartData } from "@/app/hooks/useGetBarChartData";
import ChartCounter from "./ChartCounter";

// lazy load 'react-plotly.js'
const Plot = dynamic(() => import("react-plotly.js"), { ssr: false });

const BarChart = ({ x, y }: BarChartData) => {
  // check dark mode and set graph theme
  const theme = useChartTheme();
  const numberOfChoices = y.length;
  const plotHeight = numberOfChoices * 30;
  const shapes = getGridShapes(y);

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
    bargap: 0.4,
    xaxis: {
      autorange: false,
      range: [0, 100],
      domain: [0.3, 1],
      visible: false,
      fixedrange: true,
    },
    autosize: true,
    margin: {
      l: 0,
      r: 0,
      t: 0,
      b: 0,
      pad: 7,
    },
    yaxis: {
      automargin: true,
      visible: true,
    },
    showlegend: false,
    font: {
      color: theme.fontColor,
      size: chartConfig.fontSize,
    },
    paper_bgcolor: theme.graphBackgroundColor,
    plot_bgcolor: theme.graphBackgroundColor,
    shapes,
  };

  const config: Partial<Plotly.Config> = {
    responsive: true,
    displayModeBar: false,
    fillFrame: false,
    autosizable: true,
  };

  return (
    <div className="chart-container py-6">
      <div className="flex-1">
        <Plot
          data={chartData}
          layout={layout}
          config={config}
          style={{ width: "100%", height: plotHeight }}
        />
      </div>
      <div className="mt-auto ml-auto">
        <ChartCounter number={50} total={200} />
      </div>
    </div>
  );
};

export default BarChart;
