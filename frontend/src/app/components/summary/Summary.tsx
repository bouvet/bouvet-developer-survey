import { useSurveyStructure } from "@/app/hooks/useSurveyStructure";
import { ReactNode } from "react";
import SurveyBlockRenderer from '../SurveyBlock/SurveyBlockRenderer';

const Summary = () => {
  const { data, error, isLoading } = useSurveyStructure();
  if (isLoading) return <div className='survey-section'>Henter resultater...</div>;
  if (error) return <div className='survey-section'>Error: {error.message}</div>;

  const answers = data?.surveyBlocks?.reduce(
    (acc: ReactNode[], surveyBlock) => {
      surveyBlock.blockElements.forEach((blockElement) => {
        blockElement.questions.forEach((question) => {
          if (!question.isMultipleChoice) {
            acc.push(
              <SurveyBlockRenderer key={question.id} questionId={question.id} />
            );
          }
        });
      });
      return acc;
    },
    []
  );
  return (
    <section id="about" className="survey-section gap-12">
      <h2 className="section-title">Generelt om deltakerne</h2>
      {answers}
    </section>
  );
};
export default Summary;
