"use client";

import Survey from "@/app/components/survey/Survey";
import Hero from "@/app/components/hero/Hero";
import Summary from "@/app/components/summary/Summary";

export default function SurveyPage() {
  return (
    <main>
      <Hero />
      <Summary />
      <Survey />
    </main>
  );
}
