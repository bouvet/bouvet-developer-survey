"use client";

import Survey from "@/app/components/survey/Survey";
import Hero from "@/app/components/hero/Hero";
import Summary from "@/app/components/summary/Summary";
import { useParams } from "next/navigation";
import LegacyResultsPage from "./2024/legacyResultspage";
import { Suspense } from "react";
import SkeletonLoading from "@/app/components/loading/SkeletonLoading";

export default function SurveyPage() {
  const { year } = useParams<{ year: string }>();
  console.log("year: ", year);
  return (
    <main>
      {year === "2024" ? (
        <LegacyResultsPage />
      ) : (
        //TODO: Implement new survey results components or adapt to new data structure from 2025 and up 
        <>
          <Suspense fallback={<SkeletonLoading />}>
            <Hero />
          </Suspense>
          <Suspense fallback={<SkeletonLoading />}>
            <Summary />
          </Suspense>
          <Suspense fallback={<SkeletonLoading />}>
            <Survey />
          </Suspense>
        </>
      )}
    </main>
  );
}
