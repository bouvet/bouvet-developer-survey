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
    <div
      id="database"
      className="mx-auto relative h-screen flex justify-center flex-col max-w-7xl lg:px-8 snap-center"
    >
      <div className="text-lg pb-20">
        Lorem ipsum dolor sit amet consectetur adipisicing elit. Culpa ipsum
        amet veniam mollitia eveniet porro nulla, beatae esse perspiciatis
        blanditiis in quia magnam facere, velit aliquam labore magni dolor
        similique ad illum eaque deleniti. Aliquam obcaecati necessitatibus
        voluptas quas, possimus quae voluptate quos rerum impedit doloremque
        dolorem, atque delectus neque at cumque vero cupiditate dignissimos. Qui
        tenetur asperiores dolorum molestias repudiandae, commodi ipsa quod,
        expedita corrupti eveniet alias nostrum deleniti ullam non dicta. Unde
        veritatis molestias tempore sapiente? Ex, quas!
      </div>
      <div className="w-full">
        <BarChart {...barchartData} />

        {/* <BarChartJson /> */}
      </div>
    </div>
  );
};

export default Top10;
