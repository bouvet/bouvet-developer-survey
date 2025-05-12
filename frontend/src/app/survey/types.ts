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
  id: string; // was externalId
  title: string;
  description: string;
};

// Enum for question types
export type QuestionType =
  | "single-choice"
  | "dropdown"
  | "multiple-choice"
  | "likert";

// Base type for all questions
export type BaseQuestion = {
  id: string; // was externalId
  type: QuestionType;
  title: string;
  description?: string;
  sectionId?: string | null; // matches backend: optional section reference
};

// Single-choice question
export type SingleChoiceQuestion = BaseQuestion & {
  type: "single-choice";
  options: Option[];
};

// Dropdown question
export type DropdownQuestion = BaseQuestion & {
  type: "dropdown";
  options: Option[];
};

// Multiple-choice question
export type MultipleChoiceQuestion = BaseQuestion & {
  type: "multiple-choice";
  options: Option[];
};

export type LikertQuestion = BaseQuestion & {
  type: "likert";
  options: Option[];
  columns: string[]; // ← new field for Admired / Desired
};

// Union type for all supported questions
export type Question =
  | SingleChoiceQuestion
  | DropdownQuestion
  | MultipleChoiceQuestion
  | LikertQuestion;

// Option type
export type Option = {
  id: string; // was externalId
  value: string;
};

export type SurveyResponseAnswer = {
  questionId: string;
  optionIds: string[];
};

export type SurveyResponseDto = {
  respondentId: string;
  surveyId: string;
  answers: SurveyResponseAnswer[];
};
export type SurveyFormState = {
  [key: `question_${string}`]: any;
};



