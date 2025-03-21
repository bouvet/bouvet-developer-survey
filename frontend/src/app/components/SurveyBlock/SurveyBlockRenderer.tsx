"use client";

import { useSurveyResult } from "@/app/hooks/useSurveyResult";
import AnswerContainer from "./AnswerContainer";
import QuestionContainer from "./QuestionContainer";
import BarChart from "../charts/BarChart";
import useGetBarChartData from "@/app/hooks/useGetBarChartData";
import useGetDotPlotData from "@/app/hooks/useGetDotPlotData";
import DotPlotChart from "../charts/DotPlotChart";
import { useSurveyFilters } from "@/app/Context/SurveyFilterContext";
import { Answer } from "@/app/types/survey";

const SurveyBlockRenderer = ({ questionId }: { questionId: string }) => {
  const { data, error, isLoading } = useSurveyResult(questionId);
  const { filters } = useSurveyFilters();

  const filteredData = applyFilters(data, filters);

  const barChartData = useGetBarChartData(filteredData); // Use filteredData
  const dotPlot = useGetDotPlotData(filteredData); // Use filteredData

  const tabs = filteredData?.isMultipleChoice ? ["Top 10", "Ønsket og beundret"] : [];

  if (isLoading) return <div>Henter resultater...</div>;
  if (error) return <div>Error: {error.message}</div>;
  if (!filteredData) return <div>No data available</div>;

  return (
    <section className="flex text-black gap-4 flex-col lg:flex-row lg:gap-6">
      <QuestionContainer data={filteredData} />
      <AnswerContainer tabs={tabs}>
        <BarChart
          {...barChartData}
          numberOfRespondents={filteredData.numberOfRespondents} // Bruker filteredData over data
        />
        {filteredData.isMultipleChoice && (
          <DotPlotChart
            data={dotPlot}
            numberOfRespondents={filteredData?.numberOfRespondents || 0}
          />
        )}
      </AnswerContainer>
    </section>
  );
};

const applyFilters = (data: Answer, filters: Record<string, string[]>): Answer => {
  if (!data || !filters) return data;

  const filteredChoices = data.choices.filter(choice => {
    for (const [category, selectedOptions] of Object.entries(filters)) {
      // Om filter er tom her, behold
      if (selectedOptions.length === 0) return true;
      
      // Kun filtrer den kategorien som det blir lagt inn filter på
      if (category === data.id) {
        return selectedOptions.includes(choice.text);
      }
    }

    return true;
  });

  console.log("Filtered Choices:", filteredChoices); // logger de filtrede valgene

  return { ...data, choices: filteredChoices };
};







export default SurveyBlockRenderer;