"use client";

import { useEffect } from "react";
import DefaultLoader from "./DefaultLoader";
import {
  getAllRtkQueryError,
  getFirstRtkQueryError,
  isRtkQueryLoading,
} from "./utils";
import { useClientTranslation } from "../../i18n/src";
import { RtkQueryFetcherStatusProps } from "../types/types";

// Component
export default function RtkQueryFetcherStatus({
  children,
  queries,
  showProgress,
  queriesProgressText,
  loader = undefined,
  forceLoading,
  error: errorComponent,
  errorNotificationQueries = [],
  queriesErrorText = [],
}: RtkQueryFetcherStatusProps) {
  // !todo fix hook
  // const { t } = useClientTranslation(["translation", "errors"]);
  const isLoading = isRtkQueryLoading(queries);
  const error = getFirstRtkQueryError(queries);
  const hasProgress =
    showProgress &&
    queriesProgressText &&
    queriesProgressText.length > 0 &&
    queriesProgressText.length <= queries.length;

  // Error notifications
  useEffect(() => {
    getAllRtkQueryError(errorNotificationQueries).forEach((errorQuery) => {
      // !todo orchestrate error message
      // const generalMessage = t("generalErrorDesc", {
      //   value: errorQuery.endpointName,
      //   ns: "errors",
      // });
    });
  }, [errorNotificationQueries, queriesErrorText]);

  // Render
  if (error) {
    if (errorComponent) return errorComponent;
    throw error as Error;
  }
  if (isLoading || forceLoading) {
    return (
      loader ||
      (hasProgress ? (
        // <ProgressLoader !todo create new component to show progress
        //   queries={queries}
        //   queriesProgressText={queriesProgressText}
        // />
        <>{children}</>
      ) : (
        // <DefaultLoader title={t("loading")} />
        <>{children}</>
      ))
    );
  }
  return <>{children}</>;
}
