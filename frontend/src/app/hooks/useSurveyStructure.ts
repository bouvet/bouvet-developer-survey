import { fetcher } from "../lib/fetcher";
import useSWR from "swr";
import { Survey } from '../types/survey';
import { environment } from "../lib/env";

export const useSurveyStructure = (): { data: Survey, error: {message: string}, isLoading: boolean} => {
    const { data, error, isLoading } = useSWR(
        `${environment.surveyStructureEndpoint}/d01e692d-0374-4629-a3a9-52a16aa0acf1`,
        fetcher
    )
    return { 
        data,
        error,
        isLoading
    }
};




