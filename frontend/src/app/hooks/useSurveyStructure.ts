import { fetcher } from "../lib/fetcher";
import useSWR from "swr";

export const useSurveyStructure = () => {

    // TODO: Use surveyStructure type when api structure is ready
    const { data, error, isLoading } = useSWR('https://httpbin.org/get', fetcher)
    return { 
        data,
        error,
        isLoading
    }
};


