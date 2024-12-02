"use client";

import Survey from "@/app/components/survey/SurveyWithMockData";
import Hero from "@/app/components/hero/Hero";
import Summary from "@/app/components/summary/Summary";

export default function HomePage() {
  return (
    <main>
      <Hero />
      <Summary />
      <Survey />
    </main>
  );
}