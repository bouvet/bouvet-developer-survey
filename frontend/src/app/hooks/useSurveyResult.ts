import useSWR from "swr";
import { fetcher } from "../lib/fetcher";
import { Answer } from "../types/survey";

export const useSurveyResult = (
  questionId: string
): { data: Answer; error: { message: string }; isLoading: boolean } => {
  const { data, error, isLoading } = useSWR(
    questionId
      ? `${process.env.NEXT_PUBLIC_SURVEY_ANSWERS_ENDPOINT}/?questionId=${questionId}`
      : null,
    fetcher
  );
  return {
    data,
    error,
    isLoading,
  };
};
