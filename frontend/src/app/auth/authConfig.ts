const scope = 'api://62b05a5f-4b57-4bc2-99d7-af91cc1694c8/user_impersonation';
export const scopes = [scope];

export const msalConfig = {
  auth: {
    clientId: '2f3f1b63-3f3d-4389-bf65-c43d09ff8aa5', 
    authority: `https://login.microsoftonline.com/c317fa72-b393-44ea-a87c-ea272e8d963d`, 
    redirectUri: process.env.NEXT_PUBLIC_REDIRECT_URI, 
  },
  cache: {
    cacheLocation: "localStorage", 
    storeAuthStateInCookie: true, 
  },
  request: {
    scopes,
  },
};

export const request = {
  scopes,
};
