"use client";
import React from "react";
import { useSurveyResult } from "@/app/hooks/useSurveyResult";
import { Answer, Choice, Response } from "@/app/types/survey";
import { Plot } from "@/app/types/plot";
import DotPlotChartJson from "../charts/DotPlotChartJson";

const SurveyAnswers = (questionId: string) => {
  // get survey answers for question id
  const { data, error, isLoading } = useSurveyResult(questionId);
  if (isLoading) return <div>Getting answers...</div>;
  if (error) return <div>Error: {error.message}</div>;

  // dot plot chart values

  const plot: Plot = [];

  data.choices.forEach((choice: Choice) => {
    plot.push({
      x1: choice.responses.length > 0 ? choice.responses[0].value : 0,
      x2: choice.responses.length > 1 ? choice.responses[1].value : 0,
      label: choice.text,
    });
  });

  console.log(plot);


  return DotPlotChartJson(plot);
};

export default SurveyAnswers;
