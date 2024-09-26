import { ReactElement } from "react";
import { ROUTES, RouteParams } from "./route-definitions";
import {
  ArrowPathIcon,
  ChartPieIcon,
  CursorArrowRaysIcon,
  FingerPrintIcon,
  SquaresPlusIcon,
  GlobeAsiaAustraliaIcon,
} from "@heroicons/react/24/outline";
import { useClientTranslation } from '../../../shared/i18n/src';


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
    name: "Developer profile",
    path: ROUTES.DEVELOPER_PROFILE,
    showHeader: true,
    icon: <ChartPieIcon />,
  },
  {
    name: "Languages and frameworks",
    path: ROUTES.LANGUAGES_AND_FRAMEWORKS,
    showHeader: true,
    icon: <CursorArrowRaysIcon />,
  },
  {
    name: "Database",
    path: ROUTES.DATABASE,
    showHeader: true,
    icon: <SquaresPlusIcon />,
  },
  {
    name: "AI",
    path: ROUTES.AI,
    showHeader: true,
    icon: <FingerPrintIcon />,
  },
  {
    name: "Security",
    path: ROUTES.SECURITY,
    showHeader: true,
    icon: <ArrowPathIcon />,
  },
  {
    name: "Tools",
    path: ROUTES.TOOLS,
    showHeader: true,
    icon: <GlobeAsiaAustraliaIcon />,
  },
  {
    name: "About the Survey",
    path: ROUTES.ABOUT,
    showHeader: true,
    icon: <GlobeAsiaAustraliaIcon />,
  },
];

// Tabs definition
export const tabsDefinition: TabsDefinitionType[] = [...tabs];
