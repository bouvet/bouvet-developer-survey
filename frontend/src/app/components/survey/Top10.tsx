import React, { useRef } from "react";
import { useClientTranslation } from "../../../../shared/i18n/src";
import BarChart from "../charts/barchart/BarChart";

const Top10 = () => {
  const { t } = useClientTranslation();
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
    <section id="languages_and_frameworks"
      className="flex flex-col mx-auto max-w-7xl lg:px-8 pt-40">
      <h2 className="text-3xl font-bold mb-5">Programmeringsspråk</h2>
        <BarChart {...barchartData} />
    </section>
  );
};

export default Top10;
