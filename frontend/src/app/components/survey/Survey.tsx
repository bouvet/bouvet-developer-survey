import React from "react";
import DotPlotChart from "@/app/components/charts/DotPlotChart";
import DotPlotChartJson from "@/app/components/charts/DotPlotChartJson";
import { useClientTranslation } from "../../../../shared/i18n/src";
import BarChart from "../charts/barchart/BarChart";
import BarChartJson from "../charts/barchart/BarChartJson";
import BarChartJsonTEST from "../charts/BarChart";

const Survey = () => {
  const { t } = useClientTranslation();

  // Chart data example
  const chartData = {
    title: t("admired-and-desired"),
    x1: [20, 30, 10],
    x2: [60, 50, 80],
    y: ["C#", "Typescript", "Python"],
  };

  const barchartData = {
    title: "Programmeringsspråk: Topp 10",
    x: [10, 12, 36, 40, 43, 51, 52, 57, 58, 65],
    y: [
      "Go",
      "Java",
      "Bash/Shell (all shells)",
      "PowerShell",
      "Python",
      "HTML/CSS",
      "TypeScript",
      "C#",
      "SQL",
      "JavaScript",
    ],
  };

  return (
    <div>
      <section className="mx-auto flex flex-col max-w-7xl lg:px-8">
        <div className="w-full">
          {/*
                <DotPlotChart {...chartData} />
                */}
          <DotPlotChartJson />
        </div>
      </section>
      <section className="mx-auto flex flex-col max-w-7xl lg:px-8">
        <div className="w-full">
          <BarChart {...barchartData} />

          {/* <BarChartJson /> */}

          {/* <BarChartJsonTEST /> Uncomment to see design of the bar char test with more air between the elements*/} 
        </div>
      </section>
    </div>
  );
};

export default Survey;
