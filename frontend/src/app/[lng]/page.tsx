"use client";

import { Suspense } from "react";
import HomeClient from "./client";

export default function HomePage() {
  return (
    <Suspense fallback={null}>
      <HomeClient />
    </Suspense>
  );
}
