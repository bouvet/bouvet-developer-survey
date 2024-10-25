
"use client";
import React from "react";
import { useSurveyStructure } from "@/app/hooks/useSurveyStructure";
import { useSurveyResults } from "@/app/hooks/useSurveyResults";
import BarChartJson from "../charts/barchart/BarChartJson";
import DotPlotChartJson from "../charts/DotPlotChartJson";
import structure from "@/app/structure.json";
import { Question, SurveyBlock, BlockElement } from "@/app/types/survey";
import { get } from "http";


const Survey = () => {

  const getSurvey = () => {

  // Get survey structure data
  let {data, error, isLoading} = useSurveyStructure();
  if (isLoading) return <div>Getting survey structure...</div>;
  if (error) return <div>Error: {error.message}</div>;

  //Temporary overwrite api data with json file for testing
  data = structure;

    return (
      data.surveyBlocks.map((block: SurveyBlock, index: number) => (
        <section className="mx-auto flex flex-col max-w-7xl lg:px-8" key={index}>
          <h2>{block.description}</h2>
          {block.blockElements.map((element: BlockElement) => (
            <div key={element.id}>
              {element.questions.map((question: Question) => (
                <div key={question.id}>
                  <h3>{question.id}</h3>
                </div>
              ))}
            </div>
          ))}
        </section>
      ))
    );
  }

  return (
      getSurvey()
    );
};

export default Survey;
