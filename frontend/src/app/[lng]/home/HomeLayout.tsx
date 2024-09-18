"use client";

import { Header } from "@/app/components/Header";
import { HomeContextProvider } from "./context";

// Component
export default function Home() {
  // Render
  return (
    <HomeContextProvider>
      <Header />
      <h1 className="text-2xl underline">Bouvet Developer Survey</h1>
    </HomeContextProvider>
  );
}
