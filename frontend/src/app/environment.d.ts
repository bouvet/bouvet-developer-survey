declare namespace NodeJS {
  interface ProcessEnv {
    NEXT_PUBLIC_REDIRECT_URI: string;
    NEXT_PUBLIC_API_URL: string;
    AZURE_AD_CLIENT_SECRET: string;
  }
}
