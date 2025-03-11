import { InteractionRequiredAuthError } from '@azure/msal-browser';
import { scopes } from '../auth/authConfig';
import { msalInstance } from '../auth/authProvider';

export const fetcher = async (url: string) => {
  try {
    const accounts = msalInstance.getAllAccounts();
    if (accounts.length === 0) {
      throw new Error("No active account found. Please sign in.");
    }

    let accessTokenResponse;

    try {
      accessTokenResponse = await msalInstance.acquireTokenSilent({
        account: accounts[0],
        scopes,
      });
    } catch (silentError) {
      if (silentError instanceof InteractionRequiredAuthError) {
        console.log('Token expired or invalid. Redirecting to login...');
        accessTokenResponse = await msalInstance.acquireTokenPopup({
          scopes,
        });
      } else {
        console.log(silentError)
        throw silentError;
      }
    }

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
      console.error('Error in fetcher:', error.message);
      return { error: error.message };
    }
    return { error: "Failed to fetch data" };
  }
};
