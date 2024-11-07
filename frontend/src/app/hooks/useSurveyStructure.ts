import { fetcher } from "../lib/fetcher";
import useSWR from "swr";

export const useSurveyStructure = () => {
    const { data, error, isLoading } = useSWR(
        process.env.NEXT_PUBLIC_SURVEY_STRUCTURE_ENDPOINT,
        fetcher
    )
    return { 
        data,
        error,
        isLoading
    }
};


