// Route params types
export type RouteParams = {
  lng?: string;
};

// Define Routes
export enum ROUTES {
  ROOT = "/:lng",
  HOME = "/:lng/home",
  DEVELOPER_PROFILE = "/:lng/developer_profile",
  TECHNOLOGY = "/:lng/technology",
  AI = "/:lng/ai",
  WORK = "/:lng/work",
  COMMUNITY = "/:lng/community",
  PROFESSIONAL_DEVELOPERS = "/:lng/professional_developers",
  METHODOLOGY = "/:lng/methodology",
}

/**
 * Resolves a route based on a pathname and the route params.
 *
 * @param pathname - The pathname to match against the defined routes.
 * @param params - An object containing route params.
 * @returns The matched route or undefined if no match is found.
 */
export function resolveRoute(
  pathname: string,
  params: RouteParams
): ROUTES | undefined {
  return Object.values(ROUTES).find((route) => {
    const routePath = route.replace(":lng", params.lng || "");
    return pathname.replace(/\/$/, "") === routePath.replace(/\/$/, "");
  });
}
