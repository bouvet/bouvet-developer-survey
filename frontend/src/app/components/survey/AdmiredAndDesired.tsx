import { DotPlot } from "@/app/types/plot";
import { Answer } from "@/app/types/survey";
import DotPlotChart from "../charts/DotPlotChart";
import ChartCounter from "../charts/ChartCounter";

interface Props {
  data: Answer;
}

const AdmiredAndDesired = ({ data }: Props) => {
  const plot: DotPlot = [];
  const title: string = data.dateExportTag;

  data.choices.forEach((choice) => {
    const responses = choice.responses;
    if (responses.length < 2) return;

    let [x1Percent, x2Percent] = responses.map((item) => item.percentage);
    let [x1Label, x2Label] = responses.map((item) => item.answerOption.text);

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

  return (
    <div>
      <DotPlotChart plotTitle={title} data={plot} />
      <ChartCounter number={50} total={200} />
    </div>
  );
};

export default AdmiredAndDesired;
