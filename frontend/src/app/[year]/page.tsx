"use client";

import Survey from "@/app/components/survey/Survey";
import Hero from "@/app/components/hero/Hero";
import Summary from "@/app/components/summary/Summary";
<<<<<<< HEAD
import { useParams } from "next/navigation";
import Link from "next/link";
import { MsalAuthenticationTemplate } from "@azure/msal-react";
import { InteractionType } from "@azure/msal-browser";

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
=======
import { notFound, useParams } from "next/navigation";

export default function SurveyPage() {
  const { year } = useParams<{ year: string }>();
  if (year !== "2024") notFound();
>>>>>>> feat/92-change-logo-to-support-dynamic-year
  return (
    <main>
      <MsalAuthenticationTemplate interactionType={InteractionType.Redirect}>
        <Hero />
        <Summary />
        <Survey />
      </MsalAuthenticationTemplate>
    </main>
  );
}
