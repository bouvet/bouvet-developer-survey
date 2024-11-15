import dynamic from 'next/dynamic';

// lazy load 'react-plotly.js'
const Plot = dynamic(() => import('react-plotly.js'), { ssr: false });

interface chartProps {
    title: string;
    x1: number[];
    x2: number[];
    y: string[];
  }

export default function DotPlotChart(data: chartProps) {
    
    const trace1 = {
      type: 'scatter',
      x: data.x1,
      y: data.y,
      mode: 'markers+text',
      name: 'Ønsket',
      text: data.x1.map(value => value + ' %'),
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
      x: data.x2,
      y: data.y,
      mode: 'markers+text',
      name: 'Beundret',
      text: data.x2.map(value => value + ' %'),
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
      title: data.title,
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
    
    const config = {responsive: true, displayModeBar: false};
    const plot = [trace1, trace2];

    return (
        // @ts-expect-error Disable type check for Plot
        <Plot className="w-full" data={plot} layout={layout} config={config} />
    );
}
