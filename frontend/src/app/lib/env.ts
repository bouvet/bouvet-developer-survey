import { z } from "zod";

const envSchema = z.object({
  apiUrl: z
    .string({
      required_error:
        "NEXT_PUBLIC_API_URL is required in your env.local file",
    })
    .url("NEXT_PUBLIC_API_URL must be an URL"),
});

const apiUrl = process.env.NEXT_PUBLIC_API_URL;

export const environment = envSchema.parse({
  surveyStructureEndpoint: `${apiUrl}/v1/results`,
  surveyAnswersEndpoint: `${apiUrl}/v1/results/getQuestionById`
});
