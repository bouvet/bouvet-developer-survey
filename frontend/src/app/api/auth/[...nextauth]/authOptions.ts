import { AuthOptions } from "next-auth";
import AzureADProvider from "next-auth/providers/azure-ad";

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

const authOptions: AuthOptions = {
  providers: [
    AzureADProvider({
      clientId: process.env.AZURE_AD_CLIENT_ID as string,
      clientSecret: process.env.AZURE_AD_CLIENT_SECRET as string,
      tenantId: process.env.AZURE_AD_TENANT_ID,
      authorization: {
        params: {
          scope: `openid profile email offline_access api://${process.env.AZURE_AD_BACKEND_CLIENT_ID}/user_impersonation`,
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
            `https://login.microsoftonline.com/${process.env.AZURE_AD_TENANT_ID}/oauth2/v2.0/token`,
            {
              method: "POST",
              body: new URLSearchParams({
                client_id: process.env.AZURE_AD_CLIENT_ID as string,
                client_secret: process.env.AZURE_AD_CLIENT_SECRET as string,
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
