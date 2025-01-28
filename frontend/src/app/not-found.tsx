import Link from "next/link";
import { Header } from "@/app/components/Header";
import Footer from "@/app/components/Footer/Footer";

export default function NotFound() {
  return (
    <>
      <Header />
      <main>
        <section id="intro" className="survey-section">
          <h3 className="mt-10 font-bold text-left">
            Ingen data for denne perioden
          </h3>
          <Link href="/" className="underline">
            Gå tilbake
          </Link>
        </section>
      </main>
      <Footer />
    </>
  );
}
