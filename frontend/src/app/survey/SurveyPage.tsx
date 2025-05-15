"use client";

import CustomSurvey from "./CustomSurvey"; // this is your form component
// import { Survey } from "./types";
// import useSWR from "swr";
// import { environment } from "../lib/env";
// import { fetcher } from "../lib/fetcher";
import { surveyData } from './surveyData';

export default function SurveyPage() {
  // const {
  //   data,
  //   error,
  //   isLoading,
  // }: { data: Survey; error: any; isLoading: boolean } = useSWR(
  //   `https://localhost:5001/api/v1/SurveyStructure/2028`,
  //   fetcher
  // );

  const data = surveyData;

  // if (isLoading) return <div>Laster inn skjema...</div>;
  // if (!data) return <div>Klarte ikke hente undersøkelsen.</div>;
  console.log(data);
  return <CustomSurvey surveyData={data} />;
}
