import { PropsWithChildren } from "react";
import Footer from "../components/Footer/Footer";
import { Header } from "../components/Header";
import SessionWrapper from "@/app/api/auth/[...nextauth]/SessionWrapper";

export default function Layout({ children }: PropsWithChildren) {
  return (
    <SessionWrapper>
      <Header />
      {children}
      <Footer />
    </SessionWrapper>
  );
}
