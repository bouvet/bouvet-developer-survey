// Route params types
export type RouteParams = {
  lng?: string;
};

// Define Routes
export enum ROUTES {
  ROOT = "/:lng",
  HOME = "/:lng/",
  INTRO = "/:lng/#intro",
  ABOUT = "/:lng/#about",
  PROGRAMMING_LANGUAGES = "/:lng/#languages_and_frameworks",
  WEB_FRAMEWORKS = "/:lng/#web_frameworks",
  AI_SEARCH = "/:lng/#ai",
  DATABASES = "/:lng/#databases",
  COMPILER_AND_TEST = "/:lng/#compiler_and_test",
  SECURITY = "/:lng/#security",
  OTHER_TOOLS = "/:lng/#other_tools",
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
