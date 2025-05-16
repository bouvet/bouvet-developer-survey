import useSWR from "swr";
import { fetcher } from "../lib/fetcher";
import { Answer } from "../types/survey";
import { environment } from "../lib/env";
import { useSession } from "next-auth/react";

export const useSurveyResult = (
  questionId: string
): {
  data: Answer;
  error: { message: string };
  isLoading: boolean;
  isValidating: boolean;
} => {
  const { data: user } = useSession();
  const url = questionId
    ? `${environment.apiUrl}/v1/results/questions/${questionId}`
    : null;
  const accessToken = user?.accessToken;
  const { data, error, isLoading, isValidating } = useSWR(
    [url, accessToken],
    ([url, accessToken]) => fetcher({ url, accessToken })
  );

  return {
    data,
    error,
    isLoading,
    isValidating,
  };
};
