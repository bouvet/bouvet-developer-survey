import { fetcher } from "../lib/fetcher";
import useSWR from "swr";
import { Survey } from "../types/survey";
import { environment } from "../lib/env";

export const useSurveyStructure = (): {
  data: Survey;
  error: { message: string };
  isLoading: boolean;
} => {
  const { data, error, isLoading } = useSWR(
    `${environment.apiUrl}/v1/results/surveys/year/2024`,
    fetcher
  );
  return {
    data,
    error,
    isLoading,
  };
};
