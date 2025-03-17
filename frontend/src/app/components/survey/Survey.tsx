"use client";
import { useSurveyStructure } from "@/app/hooks/useSurveyStructure";
import { BlockElement, SurveyBlock } from "@/app/types/survey";
import SurveyBlockRenderer from "../SurveyBlock/SurveyBlockRenderer";

const Survey = () => {
  // Get survey structure data
  const { data, error, isLoading } = useSurveyStructure();

  if (isLoading) {
    return (
      <section id="technology" className="survey-section">
        <div>Henter undersøkelsen...</div>
      </section>
    );
  }

  if (error) {
    return (
      <section id="technology" className="survey-section">
        <div>Error: {error.message}</div>
      </section>
    );
  }

  // Check for required data structure
  if (!data?.surveyBlocks || !Array.isArray(data.surveyBlocks)) {
    return (
      <section id="technology" className="survey-section">
        <div>Undersøkelsen mangler eller har feil format</div>
      </section>
    );
  }

  return (
    <section id="technology" className="survey-section gap-12">
      <h2 className="section-title">Teknologi</h2>
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
                  hrefId={block.description.replaceAll(/\s/g, "-")}
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
