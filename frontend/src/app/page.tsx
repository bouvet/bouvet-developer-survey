"use client";

import Survey from "@/app/components/survey/SurveyWithMockData";
import Hero from "@/app/components/hero/Hero";
import Top10 from "@/app/components/survey/Top10";
import Summary from "@/app/components/summary/Summary";

export default function HomePage() {
  return (
    <main>
      <Hero />
      <Summary />
      <Top10 />
      <Survey />
    </main>
  );
}