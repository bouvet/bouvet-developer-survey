// Base interface for common fields
export interface BaseEntity {
    id: string;
    createdAt: string;
    updatedAt: string | null;
  }

  export interface Question extends BaseEntity {
    blockElementId: string;
    dateExportTag: string;
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