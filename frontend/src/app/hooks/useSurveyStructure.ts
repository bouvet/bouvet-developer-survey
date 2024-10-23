import { useState, useEffect } from "react";
import { surveyStructure } from "../types/survey";

export const useSurveyStructure = () => {

    const [structure, setStructure] = useState<surveyStructure | null>(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<Error | null>(null);

    useEffect(() => {
        const fetchSurveyStructure = async () => {
            try {
                setLoading(true);
                const response = await fetch("/api/survey/structure");
                if(!response.ok) throw new Error('Failed to fetch survey structure');
                const data = await response.json();
                setStructure(data);
            } catch (err) {
                setError(err instanceof Error ? err : new Error('Unknown error'));
            } finally {
                setLoading(false);
            }
        }
        fetchSurveyStructure();
    }, []);

    return { structure, loading, error };
};


