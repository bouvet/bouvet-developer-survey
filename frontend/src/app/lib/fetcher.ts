import { scopes } from '../auth/authConfig';
import { msalInstance } from '../auth/authProvider';

export const fetcher = async (url: string) => {
  try {
    const accounts = msalInstance.getAllAccounts();
    if (accounts.length === 0) {
      throw new Error("No active account found. Please sign in.");
    }

    const accessTokenResponse = await msalInstance.acquireTokenSilent({
      account: accounts[0],
      scopes,
    });

    const res = await fetch(url, {
      headers: {
        Authorization: `Bearer ${accessTokenResponse.accessToken}`,
      },
    });

    if (!res.ok) {
      throw new Error(`API Error: ${res.statusText}`);
    }

    return await res.json();
  } catch (error: unknown) {
    if (error instanceof Error) {
      return { error: error.message };
    }
    return { error: "Failed to fetch data" };
  }
};
