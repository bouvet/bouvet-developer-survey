import React from 'react';
import DotPlotChart from '@/app/components/charts/DotPlotChart';
import { useClientTranslation } from "../../../../shared/i18n/src";

const Survey = () => {
    const { t } = useClientTranslation();

    // Chart data example
    const chartData = {
        title: t('admired-and-desired'),
        x1: [20, 30, 10],
        x2: [60, 50, 80],
        y: ["C#", "Typescript", "Python"],
    };

    return (
        <section className="mx-auto p flex flex-col max-w-7xl lg:px-8">
            <h1 className="text-3xl w-full text-center mb-8">{t('langs-and-frameworks')}</h1>
            <div className="w-full">
                <DotPlotChart {...chartData} />
            </div>
        </section>
    );
};

export default Survey;