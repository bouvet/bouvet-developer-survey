"use client";

import { Header } from "@/app/components/Header";
import { HomeContextProvider } from "./context";
import DotPlotChart from "@/app/components/charts/DotPlotChart";

// Chart data
const chartData = {
  x1: [20, 30, 10],
  x2: [60, 50, 80],
  y: ["C#", "Typescript", "Python"],
};

// Component
export default function Home() {
  // Render
  return (
    <HomeContextProvider>
      <Header />
      <section className="mx-auto flex flex-col max-w-7xl lg:px-8">
        <h1 className="text-3xl w-full text-center">Teknologier</h1>
        <div className="w-full">
          <DotPlotChart {...chartData} />
        </div>
      </section>
    </HomeContextProvider>
  );
}
