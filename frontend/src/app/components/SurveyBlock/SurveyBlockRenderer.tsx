import { useSurveyResult } from "@/app/hooks/useSurveyResult";
import AnswerContainer from "./AnswerContainer";
import QuestionContainer from "./QuestionContainer";
import BarChart from "../charts/BarChart";
import useGetBarChartData from "@/app/hooks/useGetBarChartData";
import useGetDotPlotData from "@/app/hooks/useGetDotPlotData";
import DotPlotChart from "../charts/DotPlotChart";

const SurveyBlockRenderer = ({ questionId }: { questionId: string }) => {
  const { data, error, isLoading } = useSurveyResult(questionId);
  const barChartData = useGetBarChartData(data);
  const dotPlot = useGetDotPlotData(data);
  const tabs = data?.isMultipleChoice ? ["Top 10", "Ønsket og beundret"] : [];

  if (isLoading) return <div>Henter resultater...</div>;
  if (error) return <div>Error: {error.message}</div>;
  return (
    <section className="flex text-black gap-4 flex-col lg:flex-row lg:gap-6">
      <QuestionContainer data={data} />
      <AnswerContainer tabs={tabs}>
        <BarChart {...barChartData} />
        {data.isMultipleChoice && <DotPlotChart data={dotPlot} />}
      </AnswerContainer>
    </section>
  );
};

export default SurveyBlockRenderer;
