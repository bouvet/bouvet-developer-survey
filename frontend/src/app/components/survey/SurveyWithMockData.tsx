"use client";
import React from "react";
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
      <section className="mx-auto flex flex-col max-w-7xl lg:px-8 pt-10">
        <div>Undersøkelsen mangler eller har feil format</div>
      </section>
    );
  }

  return (
    <section className="mx-auto flex flex-col max-w-7xl lg:px-8 pt-10">
      {!isLoading && !error && (
        <>
          {data.surveyBlocks.map((block: SurveyBlock, index: number) => {
            return (
              <section key={index}>
                {block.blockElements.map((element: BlockElement) => {
                  return element.questions.map((question: Question) => (
                    <div key={question.id}>
                      <SurveyAnswers questionId={question.id} />
                    </div>
                  ));
                })}
              </section>
            );
          })}
        </>
      )}
    </section>
  );
};

export default Survey;
