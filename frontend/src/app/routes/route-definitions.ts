// Route params types
export type RouteParams = {
  lng?: string;
};

// Define Routes
export enum ROUTES {
  ROOT = "/:lng",
  HOME = "/:lng/",
  DEVELOPER_PROFILE = "/:lng/#developer_profile",
  LANGUAGES_AND_FRAMEWORKS = "/:lng/#languages_and_frameworks",
  DATABASE = "/:lng/#database",
  AI = "/:lng/#ai",
  SECURITY = "/:lng/#security",
  TOOLS = "/:lng/#tools",
  ABOUT = "/:lng/#about",
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
