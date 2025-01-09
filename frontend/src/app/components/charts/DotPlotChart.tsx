"use client";
import dynamic from "next/dynamic";
import { chartConfig, getGridShapes, useChartTheme } from "./chartConfig";
import { DotPlot } from "@/app/types/plot";
import ChartCounter from "./ChartCounter";

// lazy load 'react-plotly.js'
const Plot = dynamic(() => import("react-plotly.js"), {
  ssr: false,
  loading: () => <div>Lager diagrammer...</div>,
});

interface DotPlotChartJsonProps {
  title?: string;
  data: DotPlot;
}

const DotPlotChart = ({ title, data }: DotPlotChartJsonProps) => {
  //check darkmode and set graph theme
  const theme = useChartTheme();

  // calculate the height of the plot based on the number of choices
  const numberOfChoices = data.length;
  const plotHeight = numberOfChoices * chartConfig.yItemHeight;
  const shapes = getGridShapes(data);

  const traces = data.map((trace) => {
    if (trace.x1 === null || trace.x2 === null) return null;
    // If the first value is greater than the second, swap the values
    const isX1Greater = trace.x1 > trace.x2;

    const xValues = isX1Greater ? [trace.x2, trace.x1] : [trace.x1, trace.x2];
    const text = isX1Greater
      ? [`${trace.x2} %`, `${trace.x1} %`]
      : [`${trace.x1} %`, `${trace.x2} %`];
    const markerColor = isX1Greater
      ? [theme.dotMinColor, theme.dotMaxColor]
      : [theme.dotMaxColor, theme.dotMinColor];

    return {
      x: xValues,
      y: [trace.yLabel, trace.yLabel], // create an array with the same y label for each value
      mode: "lines+markers+text",
      name: trace.yLabel,
      type: "scatter",
      text: text,
      textposition: ["left", "right"],
      marker: {
        color: markerColor,
        symbol: "circle",
        size: chartConfig.markerSize,
      },
      line: {
        color: theme.thickLineColor,
        width: chartConfig.thickLineWidth,
      },
    };
  });

  const layout: Partial<Plotly.Layout> = {
    title,
    xaxis: {
      showgrid: false,
      visible: false,
      range: [-15, 100],
      domain: [0, 1],
      fixedrange: true,
      automargin: true,
    },
    yaxis: {
      automargin: false,
      showgrid: false,
    },
    margin: {
      l: 2,
      r: 0,
      b: 0,
      t: 0,
    },
    showlegend: false,
    font: {
      color: theme.fontColor,
      size: chartConfig.fontSize,
    },
    autosize: true,
    paper_bgcolor: theme.graphBackgroundColor,
    plot_bgcolor: theme.graphBackgroundColor,
    shapes,
  };

  const config: Partial<Plotly.Config> = {
    responsive: true,
    displayModeBar: false,
  };

  return (
    <div className="chart-container dotplot pb-4">
      <div className="flex flex-1 justify-center text-white space-x-4">
        <div className="flex space-x-1 items-baseline">
          <span
            className={`block rounded-full bg-[#1D43C6] w-3 h-3 z-10`}
          ></span>
          <p>Ønsket</p>
        </div>
        <div className="flex space-x-1 items-baseline">
          <span
            className={`block rounded-full bg-[#F9A86F] w-3 h-3 z-10`}
          ></span>
          <p>Beundret</p>
        </div>
      </div>
      <Plot // @ts-expect-error Disable type check for Plot
        data={traces}
        layout={layout}
        config={config}
        useResizeHandler={true}
        style={{ width: "100%", height: plotHeight }}
      />
      <div className="mt-auto mb-2 ml-auto">
        <ChartCounter number={50} total={200} />
      </div>
    </div>
  );
};

export default DotPlotChart;
