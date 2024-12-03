import { useSurveyResult } from "@/app/hooks/useSurveyResult";
import AdmiredAndDesired from "./AdmiredAndDesired";
import Top10 from "./Top10";

const SurveyAnswers = ({ questionId }: { questionId: string }) => {
  const { data, error, isLoading } = useSurveyResult(questionId);
  if (isLoading) return <div>Henter resultater...</div>;
  if (error) return <div>Error: {error.message}</div>;
  if (!data.isMultipleChoice) return null;
  return (
    <section id={data.dateExportTag} className="survey-section">
      <h2 className="text-3xl font-bold mb-5">{data.dateExportTag}</h2>
      <Top10 data={data} />
      <AdmiredAndDesired data={data} />
    </section>
  );
};

export default SurveyAnswers;
