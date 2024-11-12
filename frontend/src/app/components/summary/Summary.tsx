import React from "react";
import BarChart from "../charts/barchart/BarChart";
import BarChartJson from "../charts/barchart/BarChartJson";
import { render } from "react-dom";


const Summary = () => {

    return (
    <section id="about" className="mx-auto flex flex-col max-w-7xl lg:px-8 pt-40">
        <h2 className="w-full text-3xl font-bold mb-5">Generelt om deltakerne</h2>
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