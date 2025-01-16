import { Answer } from "../types/survey";

export interface BarChartData {
  title?: string;
  y: string[];
  x: number[];
}

const useGetBarChartData = (data: Answer): BarChartData => {
  if (!data) {
    return { y: [], x: [], title: "" };
  }

  const results = data.choices
    .reduce(
      (acc, choice) => {
        if (data.isMultipleChoice && choice.responses.length === 2) {
          const admiredResponse = choice.responses[0].text.includes("SISTE")
            ? choice.responses[0]
            : choice.responses[1];

          acc.push({
            text: choice.text || "",
            percentage: Math.round(admiredResponse.percentage),
          });
        } else if (!data.isMultipleChoice && choice.responses.length === 1) {
          const percentage = Math.round(choice.responses[0].percentage);
          if (percentage) {
            acc.push({
              text: choice.text || "",
              percentage,
            });
          }
        }
        return acc;
      },
      [] as { text: string; percentage: number }[]
    )
    .sort((a, b) => a.percentage - b.percentage);

  const slicedResults = data?.isMultipleChoice ? results.slice(-10) : results;

  const y = slicedResults.map((choice) => choice.text);
  const x = slicedResults.map((choice) => choice.percentage);
  const title = data.dataExportTag;
  return { y, x, title };
};

export default useGetBarChartData;
