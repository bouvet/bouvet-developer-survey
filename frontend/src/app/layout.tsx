import { Metadata } from "next";
import { Inter } from "next/font/google";
import "./globals.css";
import { ThemeProvider } from "next-themes";
import AuthProvider from './auth/authProvider';
import { SurveyFilterProvider } from "./Context/SurveyFilterContext";


export const metadata: Metadata = {
  title: "Bouvet Developer Survey",
  icons: {
    icon: [{ url: "/favicon.ico", type: "image/x-icon" }],
    shortcut: ["/favicon.ico"],
  },
};

const inter = Inter({
  subsets: ["latin"],
  display: "swap",
  weight: ["400", "500", "700"],
  variable: "--font-inter",
});

export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <html lang="no" suppressHydrationWarning>
      <body className={inter.className}>
        <AuthProvider>
          <SurveyFilterProvider>
          <ThemeProvider attribute="data-mode">{children}</ThemeProvider>
          </SurveyFilterProvider>
        </AuthProvider>
      </body>
    </html>
  );
}