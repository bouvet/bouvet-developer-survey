"use client";
import React from "react";
import { useSurveyResult } from "@/app/hooks/useSurveyResult";
import { Choice } from "@/app/types/survey";
import { Plot } from "@/app/types/plot";
import DotPlotChartJson from "../charts/DotPlotChartJson";

const DEFAULT_PARTICPIANTS: number = 255;

const SurveyAnswers = (questionId: string) => {

  // get survey answers for question id
  const { data, error, isLoading } = useSurveyResult(questionId);
  if (isLoading) return <div>Henter resultater...</div>;
  if (error) return <div>Error: {error.message}</div>;

  // get the plot title
  const plotTitle: string = data.dateExportTag;

  // temporary calculate percentage before we get it from the API
  let x1Percent: number, x2Percent: number;
  let x1Label: string, x2Label: string;



  const calculatePercentage = (value: number): number => {
    return Math.round((value / DEFAULT_PARTICPIANTS) * 100);
  }

  // initialize the plot array
  const plot: Plot = [];

  data.choices.forEach((choice: Choice) => {

    const responses = choice.responses;
    
    if(responses.length < 2) return; // dont plot if there are less than 2 values

    x1Percent = responses[0].value;
    x2Percent = responses[1].value;
    x1Label = responses[0].answerOption.text;
    x2Label = responses[1].answerOption.text;

    // if the value for "Ønsket" is not the first value, swap the values to give a consistent plot
    if(x2Label.includes("Ønsker")) {
      [x1Percent, x2Percent] = [x2Percent, x1Percent];
      [x1Label, x2Label] = [x2Label, x1Label];
    }
    
    // convert response value to percentage. 
    x1Percent = calculatePercentage(x1Percent);
    x2Percent = calculatePercentage(x2Percent);

    plot.push({
      x1: x1Percent,
      x2: x2Percent,
      x1Label: x1Label,
      x2Label: x2Label,
      yLabel: choice.text,
    });
  });

  // sort the data based on x1 value (Most desired first)
  plot.sort((a, b) => a.x1 - b.x1);


  return DotPlotChartJson(plotTitle, plot);
};

export default SurveyAnswers;



