import { AuthOptions } from "next-auth";
import AzureADProvider from "next-auth/providers/azure-ad";

declare module "next-auth" {
  interface Session {
    accessToken?: string;
    idToken?: string;
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
          scope: `openid profile email api://${process.env.AZURE_AD_BACKEND_CLIENT_ID}/user_impersonation`,
          prompt: "login",
        },
      },
    }),
  ],
  callbacks: {
    async session({ session, token }) {
      session.accessToken = token.accessToken as string;

      return session;
    },
    async jwt({ token, account }) {
      if (account) {
        token.accessToken = account.access_token;
      }
      return token;
    },
  },
};

export default authOptions;
