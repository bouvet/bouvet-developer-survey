"use client";
import { useSurveyStructure } from "@/app/hooks/useSurveyStructure";
import { ReactNode } from "react";
import SurveyBlockRenderer from "../SurveyBlock/SurveyBlockRenderer";
import SkeletonLoading from "@/app/components/loading/SkeletonLoading";

const Summary = () => {
  const { data, error, isLoading, isValidating } = useSurveyStructure();

  if (isLoading || isValidating) return <SkeletonLoading />;
  if (error)
    return (
      <section id="about_participants" className="survey-section">
        Error: {error.message}
      </section>
    );

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
