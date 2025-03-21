import { PropsWithChildren } from "react";
import Footer from "../components/Footer/Footer";
import { Header } from "../components/Header";
import AuthProvider from "../auth/authProvider";
import { SurveyFilterProvider } from "../Context/SurveyFilterContext";

export default function Layout({ children }: PropsWithChildren) {
  return (
    <AuthProvider>
      <SurveyFilterProvider>
        <Header />
        {children}
        <Footer />
      </SurveyFilterProvider>
    </AuthProvider>
  );
}