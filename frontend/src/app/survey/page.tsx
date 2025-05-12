"use client";
import { MsalAuthenticationTemplate } from "@azure/msal-react";
import { InteractionType } from "@azure/msal-browser";
import SurveyPage from './SurveyPage';

export default function CustomSurveyPage() {
  return (
    <main>
      <MsalAuthenticationTemplate interactionType={InteractionType.Redirect}>
        <SurveyPage />
      </MsalAuthenticationTemplate>
    </main>
  );
}
