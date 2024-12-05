import { useSurveyResult } from "@/app/hooks/useSurveyResult";
import AdmiredAndDesired from "./AdmiredAndDesired";
import Top10 from "./Top10";
import TabbedContainer from "../TabbedContainer";
import QuestionContainer from "../QuestionContainer";

const SurveyAnswers = ({ questionId }: { questionId: string }) => {
  const { data, error, isLoading } = useSurveyResult(questionId);
  if (isLoading) return <div>Henter resultater...</div>;
  if (error) return <div>Error: {error.message}</div>;
  if (!data.isMultipleChoice) return null;
  return (
    <section id={data.dateExportTag} className="survey-section">
      <div className="flex text-black gap-4 flex-col lg:flex-row lg:gap-6">
        <QuestionContainer data={data} />
        <TabbedContainer tabs={["Top 10", "Ønsket og beundret"]}>
          <Top10 data={data} />
          <AdmiredAndDesired data={data} />
        </TabbedContainer>
      </div>
    </section>
  );
};

export default SurveyAnswers;
