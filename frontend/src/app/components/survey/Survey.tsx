"use client";
import React from "react";
import { useSurveyStructure } from "@/app/hooks/useSurveyStructure";
import SurveyAnswers from "./SurveyAnswers";
import { Question, SurveyBlock, BlockElement } from "@/app/types/survey";

const Survey = () => {  
  
  // Get survey structure data
  const { data, error, isLoading } = useSurveyStructure();
  if (isLoading) return <div>Getting survey structure...</div>;
  if (error) return <div>Error: {error.message}</div>;

  return (
    <div>
      {data.surveyBlocks.map((block: SurveyBlock, index: number) => {
        return (
          <section className="mx-auto flex flex-col max-w-7xl lg:px-8" key={index}>
            <h2>{block.description}</h2>
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
    </div>
  );
};

export default Survey;
