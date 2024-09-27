"use client";

import { Header } from "@/app/components/Header";
import { HomeContextProvider } from "./context";
import DotPlotChart from "@/app/components/charts/DotPlotChart";

// Component
export default function Home() {
  // Render
  return (
    <HomeContextProvider>
      <Header />
      <h1 className="text-2xl underline">Bouvet Developer Survey</h1>
      <DotPlotChart />
    </HomeContextProvider>
  );
}
