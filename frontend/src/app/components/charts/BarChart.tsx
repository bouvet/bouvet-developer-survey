"use client";
import dynamic from "next/dynamic";
import barChartData from "./barChartData.json";
import { chartConfig } from "./chartConfig";

const Plot = dynamic(() => import("react-plotly.js"), {
  ssr: false,
  loading: () => <p>Laster...</p>,
})
const BarChartJsonTEST = () => {
const data = barChartData.data.map((trace) => {  
  return {
    type: 'bar',
    x: trace.x,
    y: trace.y,
    orientation: 'h',
    text: trace.x.map(value => value + ' %'), // another way to write the text
    textposition: 'outside',  // Puts the text behind the bar ex: [] 0%
    marker: {
      color: 'rgb(68,68,100)',
      width: 10 // Sets the widt of the bar to 10px -- not working as it should because of bargap in layout
    },
    width: 0.5 // Sets the width of the graph
  }
})

  const layout = {
    title: barChartData.name,
    barmode: 'stack', // stacks the bars on each other
    xaxis: {
      showgrid: false,
      visible: false,
      // The code under is suppose to help make the bars similar size
      tickmode: 'linear',
      tick0: 1,
      dtick: 1
    },
    yaxis: {
      showline: false,
      autorange: "reversed", // Because my order came out opposite by default
    },
    margin: {
      pad: 10, // Makes whitespace between the text on the left side and the graph (should work on the other graphs as well)
      l: 100,
      r: 50,
      b: 50,
      t: 80
    },
    bargap: 0.5, // Gives whitespace between the bars
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
  }
  const config = { responsive: true, displayModeBar: false };
 
  return (
    <Plot className="w-full" data={data} layout={layout}  config={config} />
  );
}
export default BarChartJsonTEST