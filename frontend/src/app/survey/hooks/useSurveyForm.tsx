import { useForm } from "react-hook-form";
import { Question, SurveyFormState, SurveyResponseAnswer, SurveyResponseDto } from "../types";
import crypto from "crypto";
import { surveyData } from '../surveyData';
import { useSession } from 'next-auth/react';

export const useSurveyForm = () => {
  const methods = useForm<SurveyFormState>();
  const { data } = useSession();
console.log(data)
  const hashUserId = (userId: string) => {
    return crypto.createHash("sha256").update(userId).digest("hex");
  };
  const submitForm = async (formData: SurveyFormState) => {
    const userId = hashUserId(accounts[0].localAccountId);

    const answers: SurveyResponseAnswer[] = surveyData.questions.reduce(
      (acc: SurveyResponseAnswer[], question: Question) => {
        const fieldName = `question_${question.id}`;
        const value = formData[fieldName as `question_${string}`];

        if (value === undefined) return acc;

        if (question.type === "likert") {
          const optionIds: string[] = [];

          // value is { optionId: { column: boolean } }
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
      surveyId: surveyData.id,
      answers,
    };

    console.log("✅ Final Payload:", payload);

    // TODO: Send to backend
    // try {
    //   const account = accounts[0];
    //   const accessToken = (await instance.acquireTokenSilent({
    //     account: account,
    //     scopes: scopes, // Adjust if needed
    //   })).accessToken;

    //   const response = await fetch(`https://localhost:5001/api/v1/SurveyResponse`, {
    //     method: "POST",
    //     headers: {
    //       "Content-Type": "application/json",
    //       "Authorization": `Bearer ${accessToken}`, // ✅ ADD this
    //     },
    //     body: JSON.stringify(payload),
    //   });

    //   if (!response.ok) {
    //     throw new Error("Failed to submit survey response");
    //   }

    //   console.log("✅ Survey response submitted successfully!");
    // } catch (error) {
    //   console.error("❌ Error submitting survey response:", error);
    // }
  };

  return { methods, onSubmit: methods.handleSubmit(submitForm) };
};
