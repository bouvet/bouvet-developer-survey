import { Metadata } from 'next'
import { Header } from "@/app/components/Header";

import "./globals.css";

export const metadata: Metadata = {
  title: 'Bouvet Developer Survey'
}

export default function RootLayout({
  children
}: {
  children: React.ReactNode
}) {
  return (
    <html lang="no">
      <body>
        <Header />
        {children}
      </body>
    </html>
  )
}
