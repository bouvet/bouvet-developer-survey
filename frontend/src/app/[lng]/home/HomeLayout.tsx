"use client";

import { Header } from "@/app/components/Header";
import { HomeContextProvider } from "./context";
import Survey from "@/app/components/survey/Survey";
import Hero from "@/app/components/hero/Hero";

// Component
export default function Home() {
  // Render
  return (
    <HomeContextProvider>
      <Header />
      <Hero />
      <Survey />
    </HomeContextProvider>
  );
}
