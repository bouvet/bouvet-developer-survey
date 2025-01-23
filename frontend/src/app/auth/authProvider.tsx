"use client";

import React, { useEffect, useState } from "react";
import { MsalProvider } from "@azure/msal-react";
import { PublicClientApplication } from "@azure/msal-browser";
import { msalConfig } from "./authConfig";

export const msalInstance = new PublicClientApplication(msalConfig);

export default function AuthProvider({
  children,
}: {
  children: React.ReactNode;
}) {
  
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const handleRedirect = async () => {
      try {
        await msalInstance.initialize();
        const response = await msalInstance.handleRedirectPromise();
        if (response) {
          console.log("Login successful:", response);
        }
      } catch (error) {
        console.error("Error during redirect handling:", error);
      } finally {
        setLoading(false);
      }
    };

    handleRedirect();
  }, []);

  if (loading) {
    return <div>Loading...</div>;
  }

  return <MsalProvider instance={msalInstance}>{children}</MsalProvider>;
}
