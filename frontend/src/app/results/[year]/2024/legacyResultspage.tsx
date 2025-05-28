"use client";

import Hero from "@/app/components/hero/Hero";
import Summary from "@/app/components/summary/Summary";
import Survey from "@/app/components/survey/Survey";
import { Suspense } from "react";
import SkeletonLoading from "@/app/components/loading/SkeletonLoading";

export default function LegacyResultsPage() {
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
