"use client";

import { useParams } from "next/navigation";
import { RouteParams } from "../routes/_route-definitions";

/**
 * @returns the router params.
 */
export function useRouteParams<
  T extends Record<string, string> = RouteParams,
>() {
  return useParams<T>();
}
