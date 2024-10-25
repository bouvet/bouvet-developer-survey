import useSWR from "swr";
import { fetcher } from "../lib/fetcher";

export const useSurveyResult = (questionId: string) => {

    //TODO: Use surveyAnswers type when api structure is ready
    const { data, error, isLoading } = useSWR('https://httpbin.org/get', fetcher)
    return { 
        data,
        error,
        isLoading
    }
};