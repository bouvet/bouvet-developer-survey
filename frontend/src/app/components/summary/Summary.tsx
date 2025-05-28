"use client";
import { useSurveyStructure } from "@/app/hooks/useSurveyStructure";
import { ReactNode } from "react";
import SurveyBlockRenderer from "@/app/components/SurveyBlock/SurveyBlockRenderer";
import SkeletonLoading from "@/app/components/loading/SkeletonLoading";

const Summary = () => {
  const { data, error, isLoading, isValidating } = useSurveyStructure();

  if (isLoading) return <SkeletonLoading />;

  if (error && !isLoading)
    return (
      <section id="about_participants" className="survey-section">
        Error: {error.message}
      </section>
    );

  // Check for required data structure
  if (
    (!data?.surveyBlocks || !Array.isArray(data.surveyBlocks)) &&
    !isValidating
  ) {
    return (
      <section id="about_participants" className="survey-section">
        <div>Undersøkelsen mangler eller har feil format</div>
      </section>
    );
  }

  const answers = data?.surveyBlocks?.reduce(
    (acc: ReactNode[], surveyBlock) => {
      surveyBlock.blockElements.forEach((blockElement) => {
        blockElement.questions.forEach((question) => {
          if (!question.isMultipleChoice) {
            acc.push(
              <SurveyBlockRenderer
                key={question.id}
                questionId={question.id}
                hrefId={question.dataExportTag}
              />
            );
          }
        });
      });
      return acc;
    },
    []
  );

  return (
    <section id="about_participants" className="survey-section gap-12">
      <h2 className="section-title">Generelt om deltakerne</h2>
      {answers}
    </section>
  );
};
export default Summary;
