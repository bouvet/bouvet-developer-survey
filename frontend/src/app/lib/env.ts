import { z } from "zod";

const envSchema = z.object({
  apiUrl: z
    .string({
      required_error:
        "NEXT_PUBLIC_API_URL is required in your .env.local/production file",
    })
    .url("NEXT_PUBLIC_API_URL must be an URL"),
});

export const environment = envSchema.parse({
  apiUrl: process.env.NEXT_PUBLIC_API_URL,
  AZURE_AD_CLIENT_SECRET: process.env.AZURE_AD_CLIENT_SECRET,
});
