export type BouvetSurveyResultsDto = {
    surveyId: string;
    year: number;
    title: string;
    standaloneQuestions: QuestionResultDto[];
    sections: SectionResultDto[]
  };

  export type SectionResultDto = {
    id: string;
    title: string
    description: string;
    questions: QuestionResultDto[];
  }
  
  export type QuestionResultDto = {
    id: string;
    title: string;
    type: "single-choice" | "multiple-choice" | "likert";
    description?: string;
    chart: BarChartDto | DumbbellChartDto;
  };
  export type BarChartDto = {
    type: "bar";
    data: {
      label: string; // option.value
      value: number; // percentage of respondents
    }[];
  };
  export type DumbbellChartDto = {
    type: "dumbbell";
    data: {
      label: string; // option.value
      workedWith: number; // % checked "HasWorkedWith"
      wantsToWorkWith: number; // % checked "WantsToWorkWith"
    }[];
  };
  export type TopTenBarChartDto = {
    questionId: string;
    title: string;
    chart: BarChartDto;
  };
  