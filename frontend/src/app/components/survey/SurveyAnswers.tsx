"use client";
import React from "react";
import { useSurveyResult } from "@/app/hooks/useSurveyResult";
import { Choice } from "@/app/types/survey";
import { DotPlot } from "@/app/types/plot";
import DotPlotChartJson from "../charts/DotPlotChartJson";

const DEFAULT_PARTICPIANTS: number = 255;

const SurveyAnswers = ({ questionId }: { questionId: string }) => {

  // get survey answers for question id
  const { data, error, isLoading } = useSurveyResult(questionId);
  if (isLoading) return <div>Henter resultater...</div>;
  if (error) return <div>Error: {error.message}</div>;

  // get the plot title
  const plotTitle: string = data.dateExportTag;

  // temporary calculate percentage before we get it from the API
  const calculatePercentage = (value: number): number => {
    return Math.round((value / DEFAULT_PARTICPIANTS) * 100);
  }

  // initialize the plot array
  const plot: DotPlot = [];

  data.choices.forEach((choice: Choice) => {

    const responses = choice.responses;
    
    if(responses.length < 2) return; // dont plot if there are less than 2 values

    // Extract the properties
    let [x1Percent, x2Percent] = responses.map(item => item.value);
    let [x1Label, x2Label] = responses.map(item => item.answerOption.text);

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
  plot.sort((a, b) => (a.x1 !== null && b.x1 !== null) ? a.x1 - b.x1 : 0);

  return DotPlotChartJson(plotTitle, plot);
};

export default SurveyAnswers;



