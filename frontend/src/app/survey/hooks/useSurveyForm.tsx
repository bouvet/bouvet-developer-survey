import { useForm } from "react-hook-form";
import {
  LikertAnswer,
  LikertFormValue,
  QuestionType,
  Survey,
  SurveyFormState,
  SurveyQuestionAnswer,
  SurveyResponseDto,
} from "../types";
import crypto from "crypto";
import { useSession } from "next-auth/react";
import { fetcher } from "@/app/lib/fetcher";
import useSWR from "swr";

export const useSurveyForm = () => {
  const methods = useForm<SurveyFormState>({defaultValues: {}});
  const { formState } = methods;
  console.log(formState)
  const { data: session } = useSession();
  const url = `${process.env.NEXT_PUBLIC_API_URL}/survey-definitions/2025`;

  const accessToken = session?.accessToken;
  const {
    data,
    isLoading,
    isValidating,
  }: { data: Survey; isLoading: boolean; isValidating: boolean } = useSWR(
    [url, accessToken],
    ([url, accessToken]) => fetcher({ url, accessToken })
  );

  const hashUserId = (userId: string) => {
    return "17da6c9d-3f33-4521-91c2-8963ab3b4f22";
  };

  const submitForm = async (formData: SurveyFormState) => {
    if (!session?.userId) return;

    const userId = hashUserId(session.userId);

    const answers: SurveyQuestionAnswer[] = Object.entries(formData).map(
      ([questionId, answer]) => {
        const type = data.questions.find((q) => q.id === questionId)?.type;
        if (type === QuestionType.INPUT) {
          return {
            questionId,
            freeTextAnswer: answer,
          } as SurveyQuestionAnswer;
        }
        if (type === QuestionType.SINGLE_CHOICE) {
          return {
            questionId,
            optionIds: [answer],
          } as SurveyQuestionAnswer;
        }

        if (type === QuestionType.MULTIPLE_CHOICE) {
          return {
            questionId,
            optionIds: answer,
          } as SurveyQuestionAnswer;
        }

        if (type === QuestionType.LIKERT) {
          return {
            questionId,
            likertAnswers:
              (answer as { likertAnswers: LikertAnswer[] })?.likertAnswers ||
              [],
          } as SurveyQuestionAnswer;
        }

        return { questionId } as SurveyQuestionAnswer;
      }
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
    isLoading,
    isValidating,
    formState,
  };
};
