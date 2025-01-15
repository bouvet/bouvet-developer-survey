import useSWR from "swr";
import { fetcher } from "../lib/fetcher";
import { Answer } from "../types/survey";
import { environment } from "../lib/env";

export const useSurveyResult = (
  questionId: string
): { data: Answer; error: { message: string }; isLoading: boolean } => {
  const { data, error, isLoading } = useSWR(
    questionId
      ? `${environment.apiUrl}/v1/results/questions/${questionId}`
      : null,
    fetcher
  );

  return {
    data,
    error,
    isLoading,
  };
};
