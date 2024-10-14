"use client";

import { Header } from "@/app/components/Header";
import { HomeContextProvider } from "./context";
import Survey from "@/app/components/survey/Survey";

// Component
export default function Home() {
  // Render
  return (
    <HomeContextProvider>
      <Header />
      <Survey />
    </HomeContextProvider>
  );
}
