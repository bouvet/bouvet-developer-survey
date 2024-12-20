import Link from "next/link";
import { Fragment } from "react";
import { Header } from "./components/Header";

export default function HomePage() {
  return (
    <Fragment>
      <Header simple />
      <main>
        <section className="survey-section">
          <div className="flex items-center">
            <div>
              <h3 className="mt-10 font-bold text-left">Velg år</h3>
              <Link href="/2024" className="underline">2024</Link>
            </div>
          </div>
        </section>
      </main>
    </Fragment>
  );
}
