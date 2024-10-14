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
    name: "Developer Profile",
    path: ROUTES.DEVELOPER_PROFILE,
    showHeader: true,
    icon: <ChartPieIcon />,
  },
  {
    name: "Technology",
    path: ROUTES.TECHNOLOGY,
    showHeader: true,
    icon: <CursorArrowRaysIcon />,
  },
  {
    name: "AI",
    path: ROUTES.AI,
    showHeader: true,
    icon: <FingerPrintIcon />,
  },
  {
    name: "Work",
    path: ROUTES.WORK,
    showHeader: true,
    icon: <SquaresPlusIcon />,
  },
  {
    name: "Community",
    path: ROUTES.COMMUNITY,
    showHeader: true,
    icon: <ArrowPathIcon />,
  },
  {
    name: "Professional Developers",
    path: ROUTES.PROFESSIONAL_DEVELOPERS,
    showHeader: true,
    icon: <GlobeAsiaAustraliaIcon />,
  },
  {
    name: "Methodology",
    path: ROUTES.METHODOLOGY,
    showHeader: true,
    icon: <GlobeAsiaAustraliaIcon />,
  },
];

// Tabs definition
export const tabsDefinition: TabsDefinitionType[] = [...tabs];
