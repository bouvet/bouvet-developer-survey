import { useState, useEffect } from "react";
import { surveyAnswer, surveyAnswers } from "../types/survey";

export const useSurveyResult = (questionId: string) => {

    const [answers, setAnswers] = useState<surveyAnswers | null>(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<Error | null>(null);

    useEffect(() => {
        const fetchSurveyResult = async () => {
            try {
                const response = await fetch(`/api/survey/result/${questionId}`);
                if (!response.ok) throw new Error('Failed to fetch answers');
                
                const data = await response.json();
                setAnswers(data);
            } catch (err) {
                setError(err instanceof Error ? err : new Error('Unknown error'));
            } finally {
                setLoading(false);
            }
        }
        fetchSurveyResult();
    }, [questionId]);
    return { answers, loading, error }; 
};