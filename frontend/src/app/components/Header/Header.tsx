"use client";
import { Fragment, useState } from "react";
import HeaderTitle from "./components/HeaderTitle";
import HeaderTabs from "./components/HeaderTabs";
import HeaderMobileMenu from "./components/HeaderMobileMenu";
import HeaderMobileMenuButton from "./components/HeaderMobileMenuButton";
import UserMenu from "./UserMenu";
import FilterMenu from "./DataFilter/filterMenu";

export type HeaderProps = {
  /**
   * Simple variant will only have the bouvet logo and user menu
   */
  simple?: boolean;
};
export default function Header({ simple }: HeaderProps) {
  const [mobileMenuOpen, setMobileMenuOpen] = useState(false);

  return (
    <header className="shadow-md bg-inherit fixed w-full z-50">
      <nav
        aria-label="Global"
        className="mx-auto flex max-w-8xl p-6 lg:px-app-padding-x items-center"
      >
        <HeaderTitle />
        {!simple && (
          <Fragment>
            <HeaderMobileMenuButton onClick={() => setMobileMenuOpen(true)} />
            <HeaderTabs />
          </Fragment>
        )}
        <FilterMenu />
        <UserMenu />
      </nav>
      <HeaderMobileMenu
        mobileMenuOpen={mobileMenuOpen}
        onClick={() => setMobileMenuOpen(false)}
      />
    </header>
  );
}
