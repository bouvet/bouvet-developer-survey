"use client";

import Survey from "@/app/components/survey/Survey";
import Hero from "@/app/components/hero/Hero";
import Summary from "@/app/components/summary/Summary";
import { useParams } from "next/navigation";
import LegacyResultsPage from "./2024/legacyResultspage";

export default function SurveyPage() {
  const { year } = useParams<{ year: string }>();
  console.log("year: ", year);
  return (
    <main>
      {year === "2024" ? (
        <LegacyResultsPage />
      ) : (
        <>
          <Hero />
          <Summary />
          <Survey />
        </>
      )}
    </main>
  );
}
