"use client";
import React from "react";
import { useSurveyStructure } from "@/app/hooks/useSurveyStructure";
import { useSurveyResult } from "@/app/hooks/useSurveyResult";
import BarChartJson from "../charts/barchart/BarChartJson";
import DotPlotChartJson from "../charts/DotPlotChartJson";
import SurveyAnswers from "./SurveyAnswers";
import structure from "@/app/structure.json";
import { Question, SurveyBlock, BlockElement } from "@/app/types/survey";

const Survey = () => {
  // Get survey structure data
  let { data, error, isLoading } = useSurveyStructure();
  if (isLoading) return <div>Getting survey structure...</div>;
  if (error) return <div>Error: {error.message}</div>;

  //Temporary overwrite api data with json file for testing
  data = structure;

  return data.surveyBlocks.map((block: SurveyBlock, index: number) => (
    <section className="mx-auto flex flex-col max-w-7xl lg:px-8" key={index}>
      <h2>{block.description}</h2>
      {block.blockElements.map((element: BlockElement) =>
        element.questions.map((question: Question) => (
          <div key={question.id}>
            <SurveyAnswers questionId={question.id} />
          </div>
        ))
      )}
    </section>
  ));
};

export default Survey;
