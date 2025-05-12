import { PropsWithChildren } from "react";
import Footer from "../components/Footer/Footer";
import { Header } from "../components/Header";
import AuthProvider from "../auth/authProvider";

export default function Layout({ children }: PropsWithChildren) {
  return (
    <AuthProvider>
      <Header />
      {children}
      <Footer />
    </AuthProvider>
  );
}