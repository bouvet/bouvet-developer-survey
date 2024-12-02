import { fetcher } from "../lib/fetcher";
import useSWR from "swr";
import { Survey } from '../types/survey';

export const useSurveyStructure = (): { data: Survey, error: {message: string}, isLoading: boolean} => {
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


