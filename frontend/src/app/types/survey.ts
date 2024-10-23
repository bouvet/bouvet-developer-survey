export interface surveyQuestion  {
    id: string;
    text: string;
    type: "multiple" | "single" | "text";
}

export interface surveyStructure {
    id: string;
    title: string;
    questions: surveyQuestion[];
}

export interface surveyAnswer  {
    answer: string | number;
}

export interface surveyAnswers  {
    questionId: string;
    answers: surveyAnswer[];
    count?: number;
}