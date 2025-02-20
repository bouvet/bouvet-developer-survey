import useSWR from "swr";
import { fetcher } from "../lib/fetcher";
import { Answer } from "../types/survey";
import { environment } from "../lib/env";

const buildQueryParams = (filters: Record<string, string[]>): string => {
  const params = new URLSearchParams();

  Object.entries(filters).forEach(([key, values]) => {
    values.forEach((value) => {
      params.append(key, value);
    });
  });

  return params.toString();
};

export const useSurveyResult = (
  questionId: string,
  filters: Record<string, string[]> = {} // Default to an empty object
): { data: Answer; error: { message: string } | null; isLoading: boolean } => {
  const queryParams = buildQueryParams(filters);
  const url = questionId
    ? `${environment.apiUrl}/v1/results/questions/${questionId}${
        queryParams ? `?${queryParams}` : ""
      }`
    : null;

  const { data, error, isLoading } = useSWR(url, fetcher);

  return {
    data,
    error,
    isLoading,
  };
};
