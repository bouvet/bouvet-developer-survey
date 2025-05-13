import { fetcher } from "../lib/fetcher";
import useSWR from "swr";
import { Survey } from "../types/survey";
import { environment } from "../lib/env";
import { useSession } from "next-auth/react";

export const useSurveyStructure = (): {
  data: Survey;
  error: { message: string };
  isLoading: boolean;
  isValidating: boolean;
} => {
  const { data: user } = useSession();
  const url = `${environment.apiUrl}/v1/results/surveys/year/2024`;
  const accessToken = user?.accessToken;
  const { data, error, isLoading, isValidating } = useSWR(
    [url, accessToken],
    ([url, accessToken]) => fetcher(url, accessToken)
  );
  return {
    data,
    error,
    isLoading,
    isValidating,
  };
};
