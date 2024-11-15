"use client";
import { useState } from "react";
import HeaderTitle from "./components/HeaderTitle";
import HeaderTabs from "./components/HeaderTabs";
import HeaderMobileMenu from "./components/HeaderMobileMenu";
import HeaderMobileMenuButton from "./components/HeaderMobileMenuButton";

export default function Header() {

  const [mobileMenuOpen, setMobileMenuOpen] = useState(false);

  return (
    <header className="shadow-md bg-inherit fixed w-full z-50">
      <nav
        aria-label="Global"
        className="mx-auto flex justify-between max-w-7xl p-6 lg:px-8"
      >
        <HeaderTitle />
        <HeaderMobileMenuButton onClick={() => setMobileMenuOpen(true)} />
        <HeaderTabs />
      </nav>
      <HeaderMobileMenu 
        mobileMenuOpen={mobileMenuOpen}
        onClick={() => setMobileMenuOpen(false)}
      />
    </header>
  );
}
