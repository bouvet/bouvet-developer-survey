import { Metadata } from "next";
import { Header } from "@/app/components/Header";
import Footer from "@/app/components/Footer/Footer";

import "./globals.css";
import { ThemeProvider } from "next-themes";

export const metadata: Metadata = {
  title: "Bouvet Developer Survey",
  icons: {
    icon: [{ url: "/favicon.ico", type: "image/x-icon" }],
    shortcut: ["/favicon.ico"],
  },
};

export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <html lang="no" suppressHydrationWarning>
      <body>
        <ThemeProvider attribute="data-mode">{children}</ThemeProvider>
      </body>
    </html>
  );
}
