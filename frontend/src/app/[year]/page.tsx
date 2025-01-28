"use client";

import Survey from "@/app/components/survey/Survey";
import Hero from "@/app/components/hero/Hero";
import Summary from "@/app/components/summary/Summary";
import { notFound, useParams } from "next/navigation";
import { MsalAuthenticationTemplate } from '@azure/msal-react';
import { InteractionType } from '@azure/msal-browser';

export default function SurveyPage() {
  const { year } = useParams<{ year: string }>();
  if (year !== "2024") notFound();
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
