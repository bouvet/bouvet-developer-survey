import { fetcher } from "../lib/fetcher";
import useSWR from "swr";
import { Survey } from '../types/survey';
import { environment } from "../lib/env";

export const useSurveyStructure = (): { data: Survey, error: {message: string}, isLoading: boolean} => {
    const { data, error, isLoading } = useSWR(
        environment.surveyStructureEndpoint,
        fetcher
    )
    return { 
        data,
        error,
        isLoading
    }
};


