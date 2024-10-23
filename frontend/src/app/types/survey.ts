export interface surveyQquestion  {
    id: string;
    text: string;
    type: "multiple" | "single" | "text";
}

export interface surveyStructure {
    id: string;
    title: string;
    questions: question[];
}