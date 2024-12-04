import React from "react";

interface ChartCounterProps {
  number: number;
  total: number;
}

const ChartCounter = ({ number, total }: ChartCounterProps) => {
  const percentage = total > 0 ? ((number / total) * 100).toFixed(0) : "0";

  return (
    <div className="flex flex-row items-center space-x-1">
      <div>Antall svar:</div>
      <div className="font-bold">{number.toString()}</div>
      <div className="font-bold">({percentage}%)</div>
    </div>
  );
};

export default ChartCounter;
