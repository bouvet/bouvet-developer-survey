// Base interface for common fields
export interface BaseEntity {
    id: string;
    createdAt: string;
    updatedAt: string | null;
  }
  
  // Question interface
  export interface Question extends BaseEntity {
    blockElementId: string;
    dateExportTag: string;
    questionText: string;
    questionDescription: string;
    isMultipleChoice: boolean;
  }
  
  // BlockElement interface
  export interface BlockElement extends BaseEntity {
    type: string;
    questions: Question[];
  }
  
  // SurveyBlock interface
  export interface SurveyBlock extends BaseEntity {
    surveyId: string;
    type: string;
    description: string;
    blockElements: BlockElement[];
  }
  
  // Main Survey interface
  export interface Survey extends BaseEntity {
    name: string;
    surveyId: string;
    lastSyncedAt: string;
    surveyBlocks: SurveyBlock[];
  }