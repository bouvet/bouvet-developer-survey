import Link from "next/link";
import { Header } from "@/app/components/Header";
import Footer from "@/app/components/Footer/Footer";
import { headers } from "next/headers";
import { redirect } from "next/navigation";

export default async function NotFound() {
  const headersList = await headers();
  const cookie = headersList.get("cookie");
  if (!cookie?.includes("next-auth.session-token")) {
    redirect("/signin");
  }

  return (
    <>
      <Header simple />
      <main style={{ height: "calc(100dvh - 15rem)" }}>
        <section id="intro" className="survey-section">
          <h1 className="mt-10 font-bold text-left">
            Ingen data for denne perioden
          </h1>
          <Link href="/" className="underline">
            Gå tilbake
          </Link>
        </section>
      </main>
      <Footer />
    </>
  );
}
