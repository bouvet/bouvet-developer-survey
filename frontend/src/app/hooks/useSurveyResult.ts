import useSWR from "swr";
import { fetcher } from "../lib/fetcher";

export const useSurveyResult = (questionId: string) => {

    const endpoint = `${process.env.NEXT_PUBLIC_SURVEY_STRUCTURE_ENDPOINT}/?${questionId}`;

    //TODO: Use surveyAnswers type when api structure is ready
    const { data, error, isLoading } = useSWR(endpoint, fetcher)
    return { 
        data,
        error,
        isLoading
    }
};