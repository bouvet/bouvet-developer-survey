import { useSurveyResult } from "@/app/hooks/useSurveyResult";
import AdmiredAndDesired from "../survey/AdmiredAndDesired";
import AnswerContainer from "./AnswerContainer";
import QuestionContainer from "./QuestionContainer";
import BarChart from "../charts/BarChart";
import useGetBarChartData from "@/app/hooks/useGetBarChartData";
import ChartCounter from "../charts/ChartCounter";

const SurveyBlockRenderer = ({ questionId }: { questionId: string }) => {
  const { data, error, isLoading } = useSurveyResult(questionId);
  if (isLoading) return <div>Henter resultater...</div>;
  if (error) return <div>Error: {error.message}</div>;
  const barChartData = useGetBarChartData(data);
  const tabs = data.isMultipleChoice ? ["Top 10", "Ønsket og beundret"] : [];

  return (
    <section className="flex text-black gap-4 flex-col lg:flex-row lg:gap-6">
      <QuestionContainer data={data} />
      <AnswerContainer tabs={tabs}>
        <div className='relative'>
          <BarChart {...barChartData} />
          <ChartCounter number={50} total={200} />
        </div>
        {data.isMultipleChoice && <AdmiredAndDesired data={data} />}
      </AnswerContainer>
    </section>
  );
};

export default SurveyBlockRenderer;
