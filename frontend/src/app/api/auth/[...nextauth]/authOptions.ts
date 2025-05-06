import { AuthOptions } from "next-auth";
import AzureADProvider from "next-auth/providers/azure-ad";
import AzureCredentialsProvider from "@/app/api/auth/AzureCredentialsProvider";

declare module "next-auth" {
  interface Session {
    accessToken?: string;
    error?: "RefreshTokenError";
  }
}

declare module "next-auth/jwt" {
  interface JWT {
    accessToken: string;
    expiresAt: number;
    refreshToken?: string;
    error?: "RefreshTokenError";
  }
}
const credentials = await AzureCredentialsProvider();
console.log("CREDENTIALS", credentials);
const authOptions: AuthOptions = {
  secret: credentials.nextAuthSecret,
  providers: [
    AzureADProvider({
      clientId: credentials.clientId,
      clientSecret: credentials.clientSecret,
      tenantId: credentials.tenantId,
      authorization: {
        params: {
          scope: `openid profile email offline_access api://${credentials.backendClientId}/user_impersonation`,
          prompt: "login",
        },
      },
    }),
  ],
  callbacks: {
    async session({ session, token }) {
      session.accessToken = token.accessToken as string;
      if (token.error)
        console.error(new Date(), "Error token in Session", token.error);

      return session;
    },
    async jwt({ token, account }) {
      if (account) {
        token.accessToken = account.access_token as string;
        token.expiresAt = account.expires_at as number;
        token.refreshToken = account.refresh_token;
        return token;
      } else if (Date.now() < token.expiresAt! * 1000) {
        return token;
      } else {
        if (!token.refreshToken) throw new Error("Invalid refresh token");

        try {
          const response: Response = await fetch(
            `https://login.microsoftonline.com/${credentials.tenantId}/oauth2/v2.0/token`,
            {
              method: "POST",
              body: new URLSearchParams({
                client_id: credentials.clientId,
                client_secret: credentials.clientSecret,
                grant_type: "refresh_token",
                refresh_token: token.refreshToken!,
              }),
            }
          );

          const tokensOrError = await response.json();

          if (!response.ok) throw tokensOrError;

          const newTokens = tokensOrError as {
            access_token: string;
            expires_in: number;
            refresh_token?: string;
          };
          token.error = undefined;

          return {
            ...token,
            accessToken: newTokens.access_token,
            expiresAt: Math.floor(Date.now() / 1000 + newTokens.expires_in),
            refreshToken: newTokens.refresh_token
              ? newTokens.refresh_token
              : token.refreshToken,
          };
        } catch (error) {
          console.error(new Date(), "Error refreshing accessToken", error);
          token.error = "RefreshTokenError";
          return token;
        }
      }
    },
  },
};

export default authOptions;
