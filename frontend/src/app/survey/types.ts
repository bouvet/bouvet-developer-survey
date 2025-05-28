// The main survey structure
export type Survey = {
  title: string;
  id: string;
  startDate: string;
  endDate: string;
  year: number;
  sections: SurveySection[];
  questions: Question[];
};

// Sections within the survey
export type SurveySection = {
  id: string;
  title: string;
  description: string;
};

// Enum for question types
export type QuestionType =
  | "single-choice"
  | "dropdown"
  | "multiple-choice"
  | "likert"
  | "input";

// Base type for all questions
export type BaseQuestion = {
  id: string; 
  type: QuestionType;
  title: string;
  description?: string;
  sectionId?: string | null; 
};

export type SingleChoiceQuestion = BaseQuestion & {
  type: "single-choice";
  options: Option[];
};

export type DropdownQuestion = BaseQuestion & {
  type: "dropdown";
  options: Option[];
};

export type MultipleChoiceQuestion = BaseQuestion & {
  type: "multiple-choice";
  options: Option[];
};

export type LikertQuestion = BaseQuestion & {
  type: "likert";
  options: Option[];
  columns: string[];
};
export type InputQuestion = BaseQuestion & {
  type: "input";
  options: Option[];
};

// Union type for all supported questions
export type Question =
  | SingleChoiceQuestion
  | DropdownQuestion
  | MultipleChoiceQuestion
  | LikertQuestion
  | InputQuestion

export type Option = {
  id: string; 
  value: string;
};

export type SurveyResponseAnswer = {
  questionId: string;
  optionIds: string[];
  freeTextAnswer?: string;
};

export type SurveyResponseDto = {
  respondentId: string;
  surveyId: string;
  answers: SurveyResponseAnswer[];
};
export type SurveyFormState = {
  [key: string]: any;
};




