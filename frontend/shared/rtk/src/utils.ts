// The rtk query result type
export type RtkQueryResultType = {
  isError: boolean;
  isLoading: boolean;
  isFetching: boolean;
  error: unknown;
  endpointName: string;
  [key: string]: unknown;
};

/**
 * Check if in a list of queries passed as parameter some has status error.
 *
 * @param queries - the rtk queries to check.
 * @returns true if at least one rtk query has status error.
 */
export function isRtkQueryError(queries: RtkQueryResultType[]) {
  return queries.some((q) => q.isError);
}

/**
 * Check if in a list of queries passed as parameter some has status loading.
 *
 * @param queries - The rtk queries to check.
 * @returns true if at least one rtk query has status loading.
 */
export function isRtkQueryLoading(queries: RtkQueryResultType[]) {
  return queries.some((q) => q.isLoading);
}

/**
 * Get the error of the first query that has errors.
 *
 * @param queries - The rtk queries to check.
 * @returns the error of the first query that has errors.
 */
export function getFirstRtkQueryError(queries: RtkQueryResultType[]) {
  const errorQuery = queries.find((q) => q.isError);
  if (errorQuery) return errorQuery.error;
  return null;
}

/**
 * Get the error of the first mutation that has errors.
 *
 * @param mutations - The rtk mutations to check.
 * @returns the error of the first mutation that has errors or undefined.
 */
export function getFirstRtkMutationError(
  ...mutations: Omit<RtkQueryResultType, "isFetching">[]
) {
  const errorMutation = Array.from(mutations).find((q) => q.isError);
  if (errorMutation) {
    return { error: errorMutation, endpointName: errorMutation.endpointName };
  }
  return undefined;
}

/**
 * Get all the queries with error.
 *
 * @param queries - the rtk queries to check.
 * @returns the list of error queries.
 */
export function getAllRtkQueryError(queries: RtkQueryResultType[]) {
  return queries.filter((q) => q.isError);
}

/**
 * ! Todo add application insight logging
 * @returns a rtk query logger middleware to be used in redux store with redux toolkit (rtk)
 */
// export const rtkQueryErrorLogger: Middleware =
//   () => (next: any) => (action: any) => {
//     if (isRejectedWithValue(action)) {
//       applicationInsightWebInstance.trackException({
//         id: action.type,
//         exception: action.error as Error,
//       });
//     }
//     return next(action);
//   };
