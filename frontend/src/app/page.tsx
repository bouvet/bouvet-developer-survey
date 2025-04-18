"use client";

import { Header } from "./components/Header";
import { YearPickerCircle } from "./components/landing/YearPickerCircle";
import { WaveFooter } from "./components/landing/WaveFooter";
import { MsalAuthenticationTemplate } from "@azure/msal-react";
import { InteractionType } from "@azure/msal-browser";

export default function HomePage() {
  return (
    <MsalAuthenticationTemplate interactionType={InteractionType.Redirect}>
      <Header simple />
      <main className={`justify-center content-center h-[calc(100dvh-20rem)]`}>
        <section className="flex items-center z-10 h-full">
          <div className="flex flex-col md:flex-row items-center justify-center gap-6 sm:px-40 px-8 w-full h-full">
            <div className="flex flex-col max-w-lg ">
              <h2 className="text-4xl font-bold mb-5 sm:text-5xl text-nowrap">
                Developer Survey
              </h2>
              <p>
                En løsning som kartlegger teknologier og erfaringer blant
                utviklere i våre prosjekter. Denne innsikten gir oss verdifull
                kunnskap om teknologiske trender og bidrar til at vi kan ta
                informerte valg som styrker fremtidige leveranser.
              </p>
            </div>
            <YearPickerCircle />
          </div>
        </section>
      </main>
      <WaveFooter />
    </MsalAuthenticationTemplate>
  );
}
