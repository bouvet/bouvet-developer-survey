"use client";

import { Header } from "@/app/components/Header";
import { HomeContextProvider } from "./context/HomeContextProvider";
import Survey from "@/app/components/survey/SurveyWithMockData";
import Hero from "@/app/components/hero/Hero";
import Top10 from "@/app/components/survey/Top10";

// Component
export default function Home() {
  // Render
  return (
    <HomeContextProvider>
      <Header />
      {/* Remove tailwind scrolling on main element to enable testing */}
      <main className="">
        <Hero />
        <Survey />
        <Top10 />
      </main>
    </HomeContextProvider>
  );
}
