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
export enum QuestionType {
  SINGLE_CHOICE = "single-choice",
  DROPDOWN = "dropdown",
  MULTIPLE_CHOICE = "multiple-choice",
  LIKERT = "likert",
  INPUT = "input",
}

// Base type for all questions
export type BaseQuestion = {
  id: string;
  type: QuestionType;
  title: string;
  description?: string;
  sectionId?: string | null;
};

export type SingleChoiceQuestion = BaseQuestion & {
  type: QuestionType.SINGLE_CHOICE;
  options: Option[];
};

export type DropdownQuestion = BaseQuestion & {
  type: QuestionType.DROPDOWN;
  options: Option[];
};

export type MultipleChoiceQuestion = BaseQuestion & {
  type: QuestionType.MULTIPLE_CHOICE;
  options: Option[];
};

export type LikertQuestion = BaseQuestion & {
  type: QuestionType.LIKERT;
  options: Option[];
  columns: string[];
};
export type InputQuestion = BaseQuestion & {
  type: QuestionType.INPUT;
  options: Option[];
};

// Union type for all supported questions
export type Question =
  | SingleChoiceQuestion
  | DropdownQuestion
  | MultipleChoiceQuestion
  | LikertQuestion
  | InputQuestion;

export type Option = {
  id: string;
  value: string;
};

export type LikertAnswer = {
  optionId: string;
  admired: boolean;
  desired: boolean;
};

export type SurveyQuestionAnswer = {
  optionIds?: string[];
  likertAnswers?: LikertAnswer[];
  freeTextAnswer?: string;
};

export type SurveyResponseDto = {
  respondentId: string;
  surveyId: string;
  answers: SurveyQuestionAnswer[];
};

// React Hook Form state type
export type SurveyFormState = {
  [questionId: string]: SurveyQuestionAnswer;
};
