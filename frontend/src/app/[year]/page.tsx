"use client";

import Survey from "@/app/components/survey/Survey";
import Hero from "@/app/components/hero/Hero";
import Summary from "@/app/components/summary/Summary";
import { useParams } from "next/navigation";
import Link from "next/link";

export default function SurveyPage() {
  const { year } = useParams<{ year: string }>();
  if (year !== "2024")
    return (
      <main>
        <section id="intro" className="survey-section">
          <h3 className="mt-10 font-bold text-left">
            Ingen data for denne perioden
          </h3>
          <Link href="/" className="underline">
            Gå tilbake
          </Link>
        </section>
      </main>
    );
  return (
    <main>
      <Hero />
      <Summary />
      <Survey />
    </main>
  );
}
