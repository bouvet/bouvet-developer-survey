import { useRouter } from "next/navigation";
import { useRouteParams } from "./useRouteParams";
import { useCallback } from "react";

/**
 * Generate a function to redirect to an internal page.
 *
 * @returns a function that redirect to an internal page of the application.
 */
export function useRedirect() {
  const router = useRouter();
  const params = useRouteParams();
  return useCallback(
    (url: string) => {
      router.push(url.replace(":lng", params.lng || ""));
    },
    [params.lng, router]
  );
}
