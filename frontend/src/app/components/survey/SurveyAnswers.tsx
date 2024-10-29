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


  // temporary calculate percentage before we get it from the API
  let x1Percent: number, x2Percent: number;
  let x1Label: string, x2Label: string;
  const totalSurveyParticipants: number = 255;

  // initialize the plot array
  const plot: Plot = [];

  data.choices.forEach((choice: Choice) => {
    
    if(choice.responses.length < 2) return; // dont plot if there are less than 2 values
      
    x1Percent = choice.responses[0].value;
    x2Percent = choice.responses[1].value;
    x1Label = choice.responses[0].answerOption.text;
    x2Label = choice.responses[1].answerOption.text;

    // if the value for "Ønsket" is not the first value, swap the values
    if(x2Label.includes("Ønsker")) {
      [x1Percent, x2Percent] = [x2Percent, x1Percent];
      [x1Label, x2Label] = [x2Label, x1Label];
    }
    
    // convert response value to percentage 
    x1Percent = Math.round((x1Percent / totalSurveyParticipants) * 100);
    x2Percent = Math.round((x2Percent / totalSurveyParticipants) * 100);

    plot.push({
      x1Label: x1Label,
      x2Label: x2Label,
      x1: x1Percent,
      x2: x2Percent,
      yLabel: choice.text,
    });
  });


  // sort the data based on 
  plot.sort((a, b) => a.x1 - b.x1);

  console.log(plot);

  return DotPlotChartJson(plot);
};

export default SurveyAnswers;
