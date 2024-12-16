import { useSurveyStructure } from "@/app/hooks/useSurveyStructure";
import { SurveyBlock, BlockElement } from "@/app/types/survey";
import SurveyBlockRenderer from "../SurveyBlock/SurveyBlockRenderer";

const Survey = () => {
  // Get survey structure data
  const { data, error, isLoading } = useSurveyStructure();

  if (isLoading) {
    return (
      <section className="mx-auto flex flex-col max-w-8xl lg:px-8 pt-10">
        <div>Henter undersøkelsen...</div>
      </section>
    );
  }

  if (error) {
    return (
      <section className="mx-auto flex flex-col max-w-8xl lg:px-8 pt-10">
        <div>Error: {error.message}</div>
      </section>
    );
  }

  // Check for required data structure
  if (!data?.surveyBlocks || !Array.isArray(data.surveyBlocks)) {
    return (
      <section className="survey-section">
        <div>Undersøkelsen mangler eller har feil format</div>
      </section>
    );
  }

  return (
    <section className="survey-section gap-12">
      <h2 className="section-title">Generelt om deltakerne</h2>
      {!isLoading &&
        !error &&
        data.surveyBlocks.map((block: SurveyBlock) =>
          block.blockElements.map((element: BlockElement) => {
            return element.questions.reduce((acc: JSX.Element[], question) => {
              if (!question.isMultipleChoice) return acc;
              acc.push(
                <SurveyBlockRenderer
                  key={question.id}
                  questionId={question.id}
                />
              );
              return acc;
            }, []);
          })
        )}
    </section>
  );
};

export default Survey;
