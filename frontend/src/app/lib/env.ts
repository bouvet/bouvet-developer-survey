"use client";
import { z } from "zod";

const envSchema = z.object({
  surveyStructureEndpoint: z
    .string({
      required_error:
        "NEXT_PUBLIC_SURVEY_STRUCTURE_ENDPOINT is required in your env.local file",
    })
    .url("NEXT_PUBLIC_SURVEY_STRUCTURE_ENDPOINT must be an URL"),
  surveyAnswersEndpoint: z
    .string({
      required_error:
        "NEXT_PUBLIC_SURVEY_ANSWERS_ENDPOINT is required in your env.local file",
    })
    .url("NEXT_PUBLIC_SURVEY_ANSWERS_ENDPOINT must be an URL"),
});

export const environment = envSchema.parse({
  surveyStructureEndpoint: process.env.NEXT_PUBLIC_SURVEY_STRUCTURE_ENDPOINT,
  surveyAnswersEndpoint: process.env.NEXT_PUBLIC_SURVEY_ANSWERS_ENDPOINT,
});
