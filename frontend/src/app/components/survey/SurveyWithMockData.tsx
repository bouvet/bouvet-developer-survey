import { useSurveyStructure } from "@/app/hooks/useSurveyStructure";
import SurveyAnswers from "./SurveyAnswers";
import { Question, SurveyBlock, BlockElement } from "@/app/types/survey";

const Survey = () => {
  // Get survey structure data
  const { data, error, isLoading } = useSurveyStructure();

  if (isLoading) {
    return (
      <section className="mx-auto flex flex-col max-w-7xl lg:px-8 pt-10">
        <div>Henter undersøkelsen...</div>
      </section>
    );
  }

  if (error) {
    return (
      <section className="mx-auto flex flex-col max-w-7xl lg:px-8 pt-10">
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
    <article>
      {!isLoading && !error && (
        <>
          {data.surveyBlocks.map((block: SurveyBlock, index: number) =>
            block.blockElements.map((element: BlockElement) => {
              return element.questions.map((question: Question) => (
                <SurveyAnswers key={question.id} questionId={question.id} />
              ));
            })
          )}
        </>
      )}
    </article>
  );
};

export default Survey;
