import React from "react";
import BarChart from "../charts/barchart/BarChart";
import BarChartJson from "../charts/barchart/BarChartJson";
import { render } from "react-dom";


const Summary = () => {

    return (
    <section className="mx-auto relative justify-center max-w-7xl">
        <h2 className="w-full text-3xl font-bold text-center">Summary</h2>
        <div className="grid grid-cols-2 grid-rows-2 gap-4 w-full h-full">
        <div className="p-4">
            chart
        </div>
        <div className="p-4">
            chart
        </div>
        <div className="p-4">
            chart
        </div>
        <div className="p-4">
            chart
        </div>
        </div>
    </section>
    );
};

export default Summary;