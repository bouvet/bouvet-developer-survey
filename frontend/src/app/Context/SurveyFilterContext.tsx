"use client"
import { createContext, ReactNode, useContext, useState } from "react";

type FilterContextType = {
  filters: Record<string, string[]>; // Filters stored by questionId
  setFilters: (filters: Record<string, string[]>) => void;
};

const SurveyFilterContext = createContext<FilterContextType | undefined>(undefined);

export const SurveyFilterProvider = ({ children }: {children: ReactNode}) => {
  const [filters, setFilters] = useState<Record<string, string[]>>({});

  return (
    <SurveyFilterContext.Provider value={{ filters, setFilters }}>
      {children}
    </SurveyFilterContext.Provider>
  );
};

export const useSurveyFilters = () => {
  const context = useContext(SurveyFilterContext);
  if (!context) {
    throw new Error("useSurveyFilters must be used within a SurveyFilterProvider");
  }
  return context;
};

