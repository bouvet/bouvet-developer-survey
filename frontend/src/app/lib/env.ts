import { z } from "zod";

const envSchema = z.object({
  apiUrl: z
    .string({
      required_error: "NEXT_PUBLIC_API_URL is required your .env.local file",
    })
    .url("NEXT_PUBLIC_API_URL must be an URL"),
  AZURE_AD_CLIENT_SECRET: z
    .string({
      required_error: "AZURE_AD_CLIENT_SECRET is required your .env.local file",
    })
    .url("AZURE_AD_CLIENT_SECRET must be an value"),
});

export const environment = envSchema.parse({
  apiUrl: process.env.NEXT_PUBLIC_API_URL,
  AZURE_AD_CLIENT_SECRET: process.env.AZURE_AD_CLIENT_SECRET as string,
});
