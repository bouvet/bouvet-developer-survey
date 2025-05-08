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

const clientId = process.env.AZURE_AD_CLIENT_ID as string;
const clientSecret = process.env.AZURE_AD_CLIENT_SECRET as string;
const tenantId = process.env.AZURE_AD_TENANT_ID as string;
const backendClientId = process.env.AZURE_AD_BACKEND_CLIENT_ID as string;
const nextAuthSecret = process.env.NEXTAUTH_SECRET as string;

const authOptions : AuthOptions = {
      secret: nextAuthSecret,
      providers: [
        AzureADProvider({
          clientId: clientId,
          clientSecret: clientSecret,
          tenantId: tenantId,
          authorization: {
            params: {
              scope: `openid profile email offline_access api://${backendClientId}/user_impersonation`,
              prompt: "login",
            },
          },
        }),
      ],
      callbacks: {
        async session({ session, token }) {
          session.accessToken = token.accessToken as string;
          console.log("session accessToken", session.accessToken);
      if (token.error)
            console.error(new Date(), "Error token in Session", token.error);

          return session;
        },
        async jwt({ token, account }) {
          if (account) {
            console.log("IN ACCOUNT");
        token.accessToken = account.access_token as string;
            token.expiresAt = account.expires_at as number;
            token.refreshToken = account.refresh_token;
            return token;
          } else if (Date.now() < token.expiresAt! * 1000) {
            return token;
          } else {
            if (!token.refreshToken) throw new Error("Invalid refresh token");
        console.log("IN REFRESH TOKEN", token.refreshToken);
            try {
              const response: Response = await fetch(
                `https://login.microsoftonline.com/${tenantId}/oauth2/v2.0/token`,
                {
                  method: "POST",
                  body: new URLSearchParams({
                    client_id: clientId,
                    client_secret: clientSecret,
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
        async redirect({ url, baseUrl }) {
      console.log("REDIRECT URL", url);
      console.log("REDIRECT BASEURL", baseUrl);
      if (process.env.NODE_ENV === "development") {
        return url;
      }
      const primaryDomain = "survey.bouvetapps.io/";
      let newUrl = url;
      if (url.startsWith("/")) {
        newUrl = `https://${primaryDomain}${url}`;
      } else if (url.startsWith("http")) {
        newUrl = rewriteUrl(url, primaryDomain);
      }

      return newUrl;
    },
  },
};

function rewriteUrl(url: string, domain: string) {
  const parsed = new URL(url);
  const params = new URLSearchParams(parsed.search);

  // Replace the domain in the callbackUrl
  if (params.has("callbackUrl")) {
    const callbackUrl = params.get("callbackUrl");
    if (callbackUrl?.startsWith("http")) {
      const newCallBackUrl = new URL(String(callbackUrl));

      const newUrl = `https://${domain}${newCallBackUrl.pathname}?${newCallBackUrl.searchParams}`;
      params.set("callbackUrl", newUrl);
    }
  }

  // Reconstruct the URL without the token
  const newUrl = `https://${domain}${parsed.pathname}?${params.toString()}`;
  return newUrl;
}

export default authOptions;
