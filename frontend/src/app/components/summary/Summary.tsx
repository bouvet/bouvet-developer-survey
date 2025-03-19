"use client";
import { useSurveyStructure } from "@/app/hooks/useSurveyStructure";
import { ReactNode } from "react";
import SurveyBlockRenderer from "../SurveyBlock/SurveyBlockRenderer";

const Summary = () => {
  const { data, error, isLoading } = useSurveyStructure();
  if (isLoading)
    return (
      <section id="about_participants" className="survey-section">
        Henter resultater...
      </section>
    );
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
                hrefId={question.dateExportTag!}
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
