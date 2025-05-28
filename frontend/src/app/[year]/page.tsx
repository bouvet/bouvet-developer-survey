"use client";

import Hero from "@/app/components/hero/Hero";
import Summary from "@/app/components/summary/Summary";
import Survey from "@/app/components/survey/Survey";
import { notFound, useParams } from "next/navigation";
import { Suspense } from "react";
import SkeletonLoading from "@/app/components/loading/SkeletonLoading";

export default function SurveyPage() {
  const { year } = useParams<{ year: string }>();
  if (year !== "2024") notFound();
  return (
    <>
      <Hero />
      <Suspense fallback={<SkeletonLoading />}>
        <Summary />
      </Suspense>
      <Suspense fallback={<SkeletonLoading />}>
        <Survey />
      </Suspense>
    </>
  );
}
