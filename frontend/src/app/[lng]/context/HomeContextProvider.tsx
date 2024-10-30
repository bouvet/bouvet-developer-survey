"use client";

import { createContext, useContext, useMemo, type ReactNode } from "react";

// Create context
type ContextProps = {};

const HomeContext = createContext<ContextProps | undefined>(undefined);

// Context provider
export function HomeContextProvider({ children }: { children: ReactNode }) {
  // Create context value
  const contextValue: ContextProps = useMemo(() => ({}), []);

  // Render
  return (
    <HomeContext.Provider value={contextValue}>{children}</HomeContext.Provider>
  );
}

// Hook to get the context value
export function useHomeContext() {
  const context = useContext(HomeContext);
  if (!context) {
    throw new Error("useHomeContext must be used within a HomeContextProvider");
  }
  return context;
}
