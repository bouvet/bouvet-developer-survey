import { useSurveyStructure } from "@/app/hooks/useSurveyStructure";
import React from "react";

interface ChartCounterProps {
  numberOfRespondents: number;
}

const ChartCounter = ({ numberOfRespondents }: ChartCounterProps) => {
  const { data } = useSurveyStructure();
  const percentage =
    data.totalParticipants > 0
      ? ((numberOfRespondents / data.totalParticipants) * 100).toFixed(0)
      : "0";

  return (
    <div className="flex items-center space-x-1 text-white">
      <div>Antall svar:</div>
      <div className="font-bold">{numberOfRespondents}</div>
      <div className="font-bold">({percentage}%)</div>
    </div>
  );
};

export default ChartCounter;
