"use client";

import useSWR from 'swr';
import CustomSurvey from "./CustomSurvey"; // this is your form component
// import { Survey } from "./types";
// import useSWR from "swr";
// import { environment } from "../lib/env";
// import { fetcher } from "../lib/fetcher";
import { fetcher } from '../lib/fetcher';
import { useSession } from 'next-auth/react';
import { environment } from '../lib/env';

export default function SurveyPage() {
  const { data: user } = useSession();
  const url = `https://localhost:5001/api/v1/SurveyStructure/2025`
   
  const accessToken = user?.accessToken;
  const { data, error, isLoading, isValidating } = useSWR(
    [url, accessToken],
    ([url, accessToken]) => fetcher({ url, accessToken })
  );

  // const data = surveyData;

  if (isLoading) return <div>Laster inn skjema...</div>;
  if (!data) return <div>Klarte ikke hente undersøkelsen.</div>;

  return <CustomSurvey surveyData={data} />;
}
