import React from "react";
import BarChart from "../charts/BarChart";
import { useSurveyResult } from "@/app/hooks/useSurveyResult";

interface Props {
  questionId: string;
}

const SummaryAnswers = ({ questionId }: Props) => {
  const { data, error, isLoading } = useSurveyResult(questionId);
  if (isLoading) return <div>Henter resultater...</div>;
  if (error) return <div>Error: {error.message}</div>;

  const results =
    data?.choices
      .reduce((acc, choice) => {
        if (choice.responses.length === 1) {
          const percentage = Math.round(choice.responses[0].percentage);
          if (percentage) {
            acc.push({
              text: choice.text || "",
              percentage,
            });
          }
        }
        return acc;
      }, [] as { text: string; percentage: number }[])
      .sort((a, b) => a.percentage - b.percentage) || [];

  const y = results.map((choice) => choice.text);
  const x = results.map((choice) => choice.percentage);
  const title = data?.dateExportTag;
  const chartData = { y, x, title };

  return (
    <div className="w-full">
      <h3 className="text-2xl font-bold mb-5 text-center"></h3>
      <BarChart {...chartData} />
    </div>
  );
};

export default SummaryAnswers;
