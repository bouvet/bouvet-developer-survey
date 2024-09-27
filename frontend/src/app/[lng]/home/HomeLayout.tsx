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
      <h1 className="text-2xl underline">Bouvet Developer Survey</h1>
      <DotPlotChart {...chartData} />
    </HomeContextProvider>
  );
}
