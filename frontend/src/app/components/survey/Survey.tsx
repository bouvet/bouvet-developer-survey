"use client";
import { useSurveyStructure } from "@/app/hooks/useSurveyStructure";
import { BlockElement, SurveyBlock } from "@/app/types/survey";
import SurveyBlockRenderer from "@/app/components/SurveyBlock/SurveyBlockRenderer";
import SkeletonLoading from "@/app/components/loading/SkeletonLoading";

const Survey = () => {
  // Get survey structure data
  const { data, error, isLoading, isValidating } = useSurveyStructure();

  if (isLoading) return <SkeletonLoading />;

  if (error && !isLoading) {
    return (
      <section id="technology" className="survey-section">
        <div>Error: {error.message}</div>
      </section>
    );
  }

  // Check for required data structure
  if (
    (!data?.surveyBlocks || !Array.isArray(data.surveyBlocks)) &&
    !isValidating
  ) {
    return (
      <section id="technology" className="survey-section">
        <div>Undersøkelsen mangler eller har feil format</div>
      </section>
    );
  }

  const answer = data?.surveyBlocks?.map((block: SurveyBlock) =>
    block.blockElements.map((element: BlockElement) => {
      return element.questions.reduce((acc: JSX.Element[], question) => {
        if (!question.isMultipleChoice) return acc;
        acc.push(
          <SurveyBlockRenderer
            key={question.id}
            questionId={question.id}
            hrefId={block?.description?.replaceAll(/\s/g, "-")}
          />
        );
        return acc;
      }, []);
    })
  );

  return (
    <section id="technology" className="survey-section gap-12">
      <h2 className="section-title">Teknologi</h2>
      {answer}
    </section>
  );
};

export default Survey;
