// Base interface for common fields
export interface BaseEntity {
    id: string;
    createdAt: string;
    updatedAt: string | null;
  }

  export interface Question extends BaseEntity {
    blockElementId: string;
    dateExportTag: string; // TODO: fix typo when fixed in backend 
    questionText: string;
    questionDescription: string;
    isMultipleChoice: boolean;
  }

  export interface BlockElement extends BaseEntity {
    type: string;
    questions: Question[];
  }

  export interface SurveyBlock extends BaseEntity {
    surveyId: string;
    type: string;
    description: string;
    blockElements: BlockElement[];
  }
  
  export interface Survey extends BaseEntity {
    name: string;
    surveyId: string;
    lastSyncedAt: string;
    surveyBlocks: SurveyBlock[];
  }

  export interface Answer extends BaseEntity {
    id: string;
    dateExportTag: string; // TODO: fix typo when fixed in backend 
    questionText: string;
    questionDescription: string;
    isMultipleChoice: boolean;
    createdAt: string;
    updatedAt: string | null;
    choices: Choice[];
  }

export interface Choice extends BaseEntity {
    id: string;
    text: string;
    indexId: number;
    createdAt: string;
    updatedAt: string | null;
    responses: Response[];
  }

export interface Response extends BaseEntity {
  id: string;
  percentage: number;
  createdAt: string;
  answerOption: {
    text: string;
    createdAt: string;
    updatedAt: string | null;
  }
}