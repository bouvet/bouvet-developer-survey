import useSWR from "swr";
import { Answer } from "../types/survey";
import { environment } from "../lib/env";
import { msalInstance } from "../auth/authProvider";
import { scopes } from "../auth/authConfig";

export const postFetcher = async (
  url: string,
  filters: Record<string, string[]>
) => {
  try {
    const accounts = msalInstance.getAllAccounts();
    if (accounts.length === 0) {
      throw new Error("No active account found. Please sign in.");
    }

    const accessTokenResponse = await msalInstance.acquireTokenSilent({
      account: accounts[0],
      scopes,
    });

    const response = await fetch(url, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${accessTokenResponse.accessToken}`,
      },
      body: JSON.stringify(filters),
    });

    if (!response.ok) {
      throw new Error("Failed to fetch data");
    }

    return await response.json();
  } catch (error: unknown) {
    if (error instanceof Error) {
      return { error: error.message };
    }
    return { error: "Failed to fetch data" };
  }
};

export const useSurveyResult = (
  questionId: string,
  filters?: Record<string, string[]>
): { data: Answer; error: { message: string } | null; isLoading: boolean } => {
  const url = `${environment.apiUrl}/v1/results/questions/${questionId}`;

  const { data, error, isLoading } = useSWR(
    questionId ? [url, filters] : null,
    ([url, filters]) => postFetcher(url, filters || {})
  );

  return {
    data,
    error,
    isLoading,
  };
};
