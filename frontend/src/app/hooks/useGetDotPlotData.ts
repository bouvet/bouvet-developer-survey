import { DotPlot } from "../types/plot";
import { Answer } from "../types/survey";

const useGetDotPlotData = (data: Answer): DotPlot => {
  const plot: DotPlot = [];
  if (!data) return plot;

  data.choices?.forEach((choice) => {
    const responses = choice.responses;
    if (responses.length < 2) return;

    let [x1Percent, x2Percent] = responses.map((item) => item.percentage);
    let [x1Label, x2Label] = responses.map((item) => item.text);

    if (x2Label.includes("Ønsker")) {
      [x1Percent, x2Percent] = [x2Percent, x1Percent];
      [x1Label, x2Label] = [x2Label, x1Label];
    }

    x1Percent = Math.round(x1Percent);
    x2Percent = Math.round(x2Percent);
    if (x1Percent)
      plot.push({
        x1: x1Percent,
        x2: x2Percent,
        x1Label,
        x2Label,
        yLabel: choice.text,
      });
  });

  plot.sort((a, b) => (a.x1 !== null && b.x1 !== null ? a.x1 - b.x1 : 0));
  return plot;
};

export default useGetDotPlotData;
