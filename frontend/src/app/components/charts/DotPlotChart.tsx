"use client";
import Plot from 'react-plotly.js';

/*
type chartProps = {
    data: { name: string; value: number };
};
*/

export default function DotPlotChart() {

    const tech = ['C#','Typescript','Python','Javascript','SQL','HTML/CSS','Rust','Bash/Shell','PoweShell','Go'].reverse();
    const admired = [46,40].reverse();
    const desired = [68,65,68,44,53,53,86,52,39,35].reverse();
    
    const trace1 = {
      type: 'scatter',
      x: admired,
      y: tech,
      mode: 'markers+text',
      name: 'Ønsket',
      text: admired.map(value => value + ' %'),
      textposition: 'left',
      marker: {
        color: 'rgba(156, 165, 196, 0.95)',
        line: {
          color: 'rgba(156, 165, 196, 1.0)',
          width: 1,
        },
        symbol: 'circle',
        size: 12
      }
    };
    
    const trace2 = {
      x: desired,
      y: tech,
      mode: 'markers+text',
      name: 'Beundret',
      text: desired.map(value => value + ' %'),
      textposition: 'right',
      marker: {
        color: 'rgba(204, 204, 204, 0.95)',
        line: {
          color: 'rgba(217, 217, 217, 1.0)',
          width: 1,
        },
        symbol: 'circle',
        size: 12
      }
    };
    
    const layout = {
      title: 'Beundret og ønsket',
      xaxis: {
        visible:false,
        range: [0, 100]
      },
      margin: {
        l: 80,
        r: 40,
        b: 50,
        t: 80
      },
      legend: {
        font: {
          size: 10,
          xanchor:'right'
        },
    
        orientation: 'h'
      },
      autosize: true,
      paper_bgcolor: 'rgb(254, 247, 234)',
      plot_bgcolor: 'rgb(254, 247, 234)',
    };
    
    const config = {responsive: true}
    const data = [trace1, trace2];

    return (
        <Plot
            data={data}
            layout={layout}
            config={config}
        />
    );
}
