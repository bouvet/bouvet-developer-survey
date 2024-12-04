import dynamic from "next/dynamic";
import { chartConfig } from "./chartConfig";
import { DotPlot } from "@/app/types/plot";

// lazy load 'react-plotly.js'
const Plot = dynamic(() => import("react-plotly.js"), {
  ssr: false,
  loading: () => <div>Lager diagrammer...</div>,
});

interface DotPlotChartJsonProps {
  plotTitle: string;
  data: DotPlot;
}

const DotPlotChartJson = ({ plotTitle, data }: DotPlotChartJsonProps) => {

  // calculate the height of the plot based on the number of choices
  const numberOfChoices = data.length;
  const plotHeight = numberOfChoices * chartConfig.yItemHeight;

  const traces = data.map((trace) => {
    return {
      x: [trace.x1, trace.x2],
      y: [trace.yLabel, trace.yLabel], // create an array with the same y label for each value
      mode: "lines+markers+text",
      name: trace.yLabel,
      type: "scatter",
      text: [trace.x1 + " %", trace.x2 + " %"],
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
    title: plotTitle +  ": Ønsket og Beundret",
    xaxis: {
      showgrid: false,
      visible: false,
      range: [-5, 100] //range is set from -5 to achieve padding between y values and chart.
    },
    yaxis: {
      automaring: true,
      showline: false,
      gridwidth: chartConfig.dottedLineWidth,
      gridcolor: chartConfig.dottedLineColor,

    },
    margin: {
      l: 100,
      r: 40,
      b: 50,
      t: 40,
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
    // @ts-expect-error Disable type check for Plot
    <Plot data={traces} layout={layout} config={config} useResizeHandler={true} style={{ width: '100%', height: plotHeight }} />  
  );
};

export default DotPlotChartJson;
