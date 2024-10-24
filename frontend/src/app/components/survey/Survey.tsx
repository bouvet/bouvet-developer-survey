
"use client";
import React from "react";
import { useSurveyStructure } from "@/app/hooks/useSurveyStructure";
import { useSurveyResults } from "@/app/hooks/useSurveyResults";
import BarChartJson from "../charts/barchart/BarChartJson";
import DotPlotChartJson from "../charts/DotPlotChartJson";

const Survey = () => {

  // Get survey structure data
  const {data, error, isLoading} = useSurveyStructure();
  if (isLoading) return <div>Getting survey structure...</div>;
  if (error) return <div>Error: {error.message}</div>;


  /* TOTO: Map through structure data to identify the question ids
  and fetch the survey results for each question */
  

  return (
    <div>
      <section className="mx-auto flex flex-col max-w-7xl lg:px-8">
        <BarChartJson />
        <DotPlotChartJson />
      </section>
    </div>
  );
};

export default Survey;
