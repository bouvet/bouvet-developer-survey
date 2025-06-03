import { useForm } from "react-hook-form";
import {
  Question,
  Survey,
  SurveyFormState,
  SurveyResponseAnswer,
  SurveyResponseDto,
} from "../types";
import crypto from "crypto";
import { useSession } from "next-auth/react";
import { fetcher } from "@/app/lib/fetcher";
import useSWR from "swr";

export const useSurveyForm = () => {
  const methods = useForm<SurveyFormState>();

  const { data: session } = useSession();
  const url = `${process.env.NEXT_PUBLIC_API_URL}/survey-definitions/2025`;

  const accessToken = session?.accessToken;
  const { data }: { data: Survey } = useSWR(
    [url, accessToken],
    ([url, accessToken]) => fetcher({ url, accessToken })
  );
  const hashUserId = (userId: string) => {
    return crypto.createHash("sha256").update(userId).digest("hex");
  };
  const submitForm = async (formData: SurveyFormState) => {
    console.log("submit");
    if (!session?.userId) return;
    const userId = hashUserId(session?.userId);
    const answers: SurveyResponseAnswer[] = data.questions.reduce(
      (acc: SurveyResponseAnswer[], question: Question) => {
        const fieldName = question.id;
        const value = formData[fieldName];

        if (value === undefined) return acc;

        if (question.type === "likert") {
          const optionIds: string[] = [];

          for (const optionId in value) {
            for (const colId in value[optionId]) {
              if (value[optionId][colId]) {
                optionIds.push(`${optionId}-${colId}`);
              }
            }
          }

          if (optionIds.length > 0) {
            acc.push({
              questionId: question.id,
              optionIds,
            });
          }

          return acc;
        }

        if (question.type === "multiple-choice") {
          acc.push({
            questionId: question.id,
            optionIds: Array.isArray(value) ? value : [value],
          });
          return acc;
        }

        acc.push({
          questionId: question.id,
          optionIds: [value],
        });
        return acc;
      },
      []
    );

    const payload: SurveyResponseDto = {
      respondentId: userId,
      surveyId: data.id,
      answers,
    };

    console.log("✅ Final Payload:", payload);

    try {
      const response = await fetch(
        `${process.env.NEXT_PUBLIC_API_URL}/bouvet/survey-responses/submit`,
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${accessToken}`,
          },
          body: JSON.stringify(payload),
        }
      );

      if (!response.ok) {
        throw new Error("Failed to submit survey response");
      }

      console.log("✅ Survey response submitted successfully!");
    } catch (error) {
      console.error("❌ Error submitting survey response:", error);
    }
  };

  return {
    methods,
    onSubmit: methods.handleSubmit(submitForm),
    surveyData: data,
  };
};
