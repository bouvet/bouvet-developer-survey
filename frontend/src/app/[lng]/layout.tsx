"use client";

import { Header } from "@/app/components/Header";
import Survey from "@/app/components/survey/SurveyWithMockData";
import Hero from "@/app/components/hero/Hero";
import Top10 from "@/app/components/survey/Top10";
import Summary from "@/app/components/summary/Summary";

// Component
export default function Home() {
  // Render
  return (
      <>
        <Header />
        <main className="scroll-smooth">
          <Hero />
          <Summary />
          <Top10 />
          <Survey />
      </main>
    </>
  );
}
