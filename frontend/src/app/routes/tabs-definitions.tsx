import { ReactElement } from "react";
import { ROUTES, RouteParams } from "./route-definitions";


// Tabs definition type
export type TabsDefinitionType = {
  name: string;
  path: string | ((routePrams: RouteParams) => string);
  showHeader: boolean;
  description?: string;
  icon?: ReactElement | JSX.Element;
  children?: [];
};

// Tabs definition
const tabs: TabsDefinitionType[] = [
  {
    name: "Intro",
    path: ROUTES.INTRO,
    showHeader: true
  },
  {
    name: "Om deltakerne",
    path: ROUTES.ABOUT,
    showHeader: true
  },
  {
    name: "Språk",
    path: ROUTES.PROGRAMMING_LANGUAGES,
    showHeader: true
  },
  {
    name: "Rammeverk",
    path: ROUTES.WEB_FRAMEWORKS,
    showHeader: true
  },
  {
    name: "AI",
    path: ROUTES.AI_SEARCH,
    showHeader: true
  },
  {
    name: "Databaser",
    path: ROUTES.DATABASES,
    showHeader: true
  },
  {
    name: "Kompiler og test",
    path: ROUTES.COMPILER_AND_TEST,
    showHeader: true
  },
  {
    name: "Sikkerhet",
    path: ROUTES.SECURITY,
    showHeader: true
  },
  {
    name: "Annet",
    path: ROUTES.OTHER_TOOLS,
    showHeader: true
  },

];

// Tabs definition
export const tabsDefinition: TabsDefinitionType[] = [...tabs];
