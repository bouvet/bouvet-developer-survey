import useSWR from "swr";
import { fetcher } from "../lib/fetcher";
import { Answer } from "../types/survey";
import { environment } from "../lib/env";

export const useSurveyResult = (
  questionId: string
): { data: Answer; error: { message: string }; isLoading: boolean } => {
  const { data, error, isLoading } = useSWR(
    questionId
      ? `${environment.apiUrl}/v1/results/questions/${questionId}`
      : null,
    fetcher
  );

  console.log("Survey Data Håvard:", data);
  console.log("Error Håvard:", error);
  console.log("Loading Håvard:", isLoading);

  return {
    data,
    error,
    isLoading,
  };
};

// import useSWR from "swr";
// import { Answer } from "../types/survey";
// import { environment } from "../lib/env";

// export const postFetcher = async (
//   url: string,
//   filters: Record<string, string[]>
// ) => {
//   try {
//     const response = await fetch(url, {
//       method: "POST",
//       headers: {
//         "Content-Type": "application/json",
//       },
//       body: JSON.stringify(filters),
//     });

//     if (!response.ok) {
//       throw new Error("Failed to fetch data");
//     }

//     return await response.json();
//   } catch (error: unknown) {
//     if (error instanceof Error) {
//       return { error: error.message };
//     }
//     return { error: "Failed to fetch data" };
//   }
// };

// export const useSurveyResult = (
//   questionId: string,
//   filters?: Record<string, string[]>
// ): { data: Answer | null; error: { message: string } | null; isLoading: boolean } => {
//   const url = `${environment.apiUrl}/v1/results/questions/${questionId}`;

//   const { data, error, isLoading } = useSWR(
//     questionId ? [url, filters] : null,
//     ([url, filters]) => postFetcher(url, filters || {})
//   );

//   return {
//     data: data || null,
//     error: error || null,
//     isLoading,
//   };
// };