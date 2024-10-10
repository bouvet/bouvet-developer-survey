import React from 'react';
import DotPlotChart from '@/app/components/charts/DotPlotChart';
import DotPlotChartJson from '@/app/components/charts/DotPlotChartJson';
import { useClientTranslation } from "../../../../shared/i18n/src";

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
        <section className="mx-auto flex flex-col max-w-7xl lg:px-8">
            <div className="w-full">
                {/*
                <DotPlotChart {...chartData} />
                */}
                <DotPlotChartJson/>
            </div>
        </section>
    );

};

export default Survey;