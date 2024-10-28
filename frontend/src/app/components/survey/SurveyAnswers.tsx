"use client";
import React from "react";
import { useSurveyResult } from "@/app/hooks/useSurveyResult";
import { Question, SurveyBlock, BlockElement } from "@/app/types/survey";

const SurveyAnswers = (questionId: string) => {
    
    let { data, error, isLoading } = useSurveyResult(questionId);
    if (isLoading) return <div>Getting answers...</div>;
    if (error) return <div>Error: {error.message}</div>;
  
    return (
      <div>{JSON.stringify(data.id)}</div>
    );
  };
  
  export default SurveyAnswers;