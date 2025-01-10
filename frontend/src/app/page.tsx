import { Fragment } from "react";
import { Header } from "./components/Header";
import { YearPickerCircle } from "./components/landing/YearPickerCircle";
import Wave from "./components/landing/Wave";

export default function HomePage() {
  return (
    <Fragment>
      <Header simple />
      <main>
        <section className="flex items-center h-screen z-10">
          <div className="flex flex-col md:flex-row items-center justify-center gap-6 sm:px-40 px-8 w-full">
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
      <div className="absolute bottom-0 w-full z-0">
        <Wave />
      </div>
    </Fragment>
  );
}
