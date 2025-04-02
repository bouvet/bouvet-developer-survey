import { useSurveyResult } from "@/app/hooks/useSurveyResult";
import AnswerContainer from "./AnswerContainer";
import QuestionContainer from "./QuestionContainer";
import BarChart from "../charts/BarChart";
import useGetBarChartData from "@/app/hooks/useGetBarChartData";
import useGetDotPlotData from "@/app/hooks/useGetDotPlotData";
import DotPlotChart from "../charts/DotPlotChart";
import SkeletonLoading from "@/app/components/loading/SkeletonLoading";

const SurveyBlockRenderer = ({
  questionId,
  hrefId,
}: {
  questionId: string;
  hrefId: string;
}) => {
  const { data, error, isLoading } = useSurveyResult(questionId);
  const barChartData = useGetBarChartData(data);
  const dotPlot = useGetDotPlotData(data);
  const tabs = data?.isMultipleChoice ? ["Top 10", "Ønsket og beundret"] : [];

  if (isLoading) return <SkeletonLoading />;
  if (error) return <div>Error: {error.message}</div>;

  return (
    <section
      id={hrefId.replaceAll(/\s/g, "-")}
      className="flex text-black gap-4 flex-col lg:flex-row lg:gap-6 scroll-mt-32"
    >
      <QuestionContainer data={data} />
      <AnswerContainer tabs={tabs}>
        <BarChart
          {...barChartData}
          numberOfRespondents={data.numberOfRespondents}
        />
        {data.isMultipleChoice && (
          <DotPlotChart
            data={dotPlot}
            numberOfRespondents={data?.numberOfRespondents || 0}
          />
        )}
      </AnswerContainer>
    </section>
  );
};

export default SurveyBlockRenderer;
