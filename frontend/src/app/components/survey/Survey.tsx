import React, { useRef } from "react";
import DotPlotChart from "@/app/components/charts/DotPlotChart";
import DotPlotChartJson from "@/app/components/charts/DotPlotChartJson";
import { useClientTranslation } from "../../../../shared/i18n/src";
import BarChart from "../charts/barchart/BarChart";
import BarChartJson from "../charts/barchart/BarChartJson";
import useOnScreen from "@/app/hooks/useOnScreen";

const Survey = () => {
  const { t } = useClientTranslation();

  // Chart data example
  const chartData = {
    title: t("admired-and-desired"),
    x1: [20, 30, 10],
    x2: [60, 50, 80],
    y: ["C#", "Typescript", "Python"],
  };

  return (
    <div
      id="languages_and_frameworks"
      className="mx-auto relative h-screen flex justify-center flex-col max-w-7xl lg:px-8 snap-center"
    >
      <div className="text-lg pb-20">
        Lorem ipsum dolor sit amet consectetur adipisicing elit. Minus velit
        consectetur possimus at illo sit aliquam maiores non animi assumenda ab,
        eum fuga quo, rerum repudiandae alias. Nesciunt rerum in deleniti
        suscipit doloremque ut explicabo consequatur magni voluptas eaque
        aperiam, facere iusto molestias reprehenderit quam quae unde officia
        quod minus quis temporibus! Ratione, impedit quia suscipit nobis magni
        distinctio at explicabo maiores in labore ipsam dolore nisi accusamus id
        quasi vel earum delectus, reprehenderit corporis quis, ex beatae minima.
        Tempora.
      </div>
      <div className="w-full">
        {/*
                <DotPlotChart {...chartData} />
                */}
        <DotPlotChartJson />
      </div>
    </div>
  );
};

export default Survey;