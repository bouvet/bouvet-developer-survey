import useSWR from "swr";
import { fetcher } from "../lib/fetcher";

export const useSurveyResult = (questionId: string) => {
    const { data, error, isLoading } = useSWR(
        questionId ? `${process.env.NEXT_PUBLIC_SURVEY_ANSWERS_ENDPOINT}/?${questionId}` : null,
        fetcher
    )
    return { 
        data,
        error,
        isLoading
    }
};