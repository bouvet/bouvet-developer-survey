import { Fragment, PropsWithChildren } from "react";
import Footer from "../components/Footer/Footer";
import { Header } from "../components/Header";

export default function Layout({ children }: PropsWithChildren) {
  return (
    <Fragment>
      <Header />
      {children}
      <Footer />
    </Fragment>
  );
}
