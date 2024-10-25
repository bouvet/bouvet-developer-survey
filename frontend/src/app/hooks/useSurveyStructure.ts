import { fetcher } from "../lib/fetcher";
import useSWR from "swr";

export const useSurveyStructure = () => {
    const endpoint = process.env.NEXT_PUBLIC_SURVEY_STRUCTURE_ENDPOINT

    // TODO: Use surveyStructure type when api structure is ready
    const { data, error, isLoading } = useSWR(endpoint, fetcher)
    return { 
        data,
        error,
        isLoading
    }
};


