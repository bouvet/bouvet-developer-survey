import { Metadata } from 'next'
import { Header } from "@/app/components/Header";

import "./globals.css";

export const metadata: Metadata = {
  title: 'Bouvet Developer Survey',
  icons: {
    icon: [
      { url: '/favicon.ico', type: 'image/x-icon' },
    ],
    shortcut: ['/favicon.ico']
  }
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
