import { PropsWithChildren } from "react";
import Footer from "../../components/Footer/Footer";
import { Header } from "../../components/Header";
export default function Layout({ children }: PropsWithChildren) {
  return (
    <>
      <Header />
      {children}
      <Footer />
    </>
  );
}
