"use client";
import React from "react";
import { useSurveyStructure } from "@/app/hooks/useSurveyStructure";
import SurveyAnswers from "./SurveyAnswers";
import { Question, SurveyBlock, BlockElement } from "@/app/types/survey";

const Survey = () => {  
  // Get survey structure data
  const { data, error, isLoading } = useSurveyStructure();

  return (
    <section className="mx-auto flex flex-col max-w-7xl lg:px-8 pt-10">
      {isLoading && <div>Henter undersøkelsen...</div>}
      {error && <div>Error: {error.message}</div>}
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
