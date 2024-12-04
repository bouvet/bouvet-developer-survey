import React from "react";
import { Answer } from "@/app/types/survey";
import BarChart from "../charts/barchart/BarChart";

interface Props {
  data: Answer;
}

const Top10: React.FC<Props> = ({ data }) => {
  if (!data || !data.isMultipleChoice) {
    return null;
  }
  const top10 = data.choices
    .reduce((acc, choice) => {
      if (choice.responses.length === 2) {
        // TODO: Enforce response order in backend to avoid check?
        const admiredResponse = choice.responses[0].answerOption.text.includes(
          "SISTE"
        )
          ? choice.responses[0]
          : choice.responses[1];

        acc.push({
          text: choice.text || "",
          percentage: Math.round(admiredResponse.percentage),
        });
      }
      return acc;
    }, [] as { text: string; percentage: number }[])
    .sort((a, b) => a.percentage - b.percentage)
    .slice(-10);

  const chartData = {
    y: top10.map((choice) => choice.text),
    x: top10.map((choice) => choice.percentage),
    title: `${data.dateExportTag}: Top 10`,
  };

  return <BarChart {...chartData} />;
};

export default Top10;
