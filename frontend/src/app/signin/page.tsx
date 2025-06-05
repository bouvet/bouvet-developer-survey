"use client";

import { useSearchParams } from "next/navigation";
import { signIn } from "next-auth/react";
import React, { Suspense } from "react";
import Spinner from "@/app/components/loading/Spinner";

const Login = () => {
  const searchParams = useSearchParams();
  const doSignIn = async () => {
    const callbackUrl = searchParams.get("callbackUrl") ?? "/";
    await signIn("azure-ad", {
      callbackUrl,
    });
  };
  void doSignIn();

  return (
    <main className="w-full h-dvh">
      <Spinner />
    </main>
  );
};

const LoginPage = () => {
  return (
    <Suspense>
      <Login />
    </Suspense>
  );
};

export default LoginPage;
