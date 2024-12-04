import { useSurveyStructure } from "@/app/hooks/useSurveyStructure";
import SummaryAnswers from "./SummaryAnswers";
import { ReactNode } from "react";

const Summary = () => {
  const { data, error, isLoading } = useSurveyStructure();
  if (isLoading) return <div>Henter resultater...</div>;
  if (error) return <div>Error: {error.message}</div>;

  const answers = data?.surveyBlocks?.reduce(
    (acc: ReactNode[], surveyBlock) => {
      surveyBlock.blockElements.forEach((blockElement) => {
        blockElement.questions.forEach((question) => {
          if (!question.isMultipleChoice) {
            acc.push(
              <SummaryAnswers key={question.id} questionId={question.id} />
            );
          }
        });
      });
      return acc;
    },
    []
  );
  return (
    <section id="about" className="survey-section">
      <h2 className="w-full text-3xl font-bold mb-5">Generelt om deltakerne</h2>
      <div className="grid lg:grid-cols-2 grid-cols-1 grid-rows-3 gap-4 w-full h-full ">
        {answers}
      </div>
    </section>
  );
};
export default Summary;
