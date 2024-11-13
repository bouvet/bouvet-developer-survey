"use client";
import { useState } from "react";
import { useRedirect } from "@/app/hooks";
import { useRouteParams } from "@/app/hooks";
import HeaderTitle from "./components/HeaderTitle";
import HeaderTabs from "./components/HeaderTabs";
import HeaderUser from "./components/HeaderUser";
import HeaderMobileMenu from "./components/HeaderMobileMenu";
import HeaderMobileMenuButton from "./components/HeaderMobileMenuButton";
import {
  TabsDefinitionType,
  tabsDefinition,
} from "@/app/routes/tabs-definitions";
import { useClientTranslation } from "../../../../shared/i18n/src";

// Component
export default function Header() {
  const { t } = useClientTranslation();
  const [menuOpen, setMenuOpen] = useState(false);
  const [currentTab] = useState(null);
  const [activeTab, setActiveTab] = useState("");

  const redirect = useRedirect();
  const params = useRouteParams();
  const [mobileMenuOpen, setMobileMenuOpen] = useState(false);

  // ! todo
  const selectedTabPosition = 1;

  // Methods
  const changePage = (route: TabsDefinitionType) => () => {
    if (route.path) {
      const path =
        typeof route.path === "function" ? route.path(params) : route.path;
      setActiveTab(route.name);
      redirect(path);
      if (menuOpen) setMenuOpen(false);
    }
  };

  console.log(currentTab);

  // Render
  if (currentTab) return null;
  return (
    <header className="bg-inherit fixed w-full z-50">
      <nav
        aria-label="Global"
        className="mx-auto flex max-w-7xl items-center justify-between p-6 lg:px-8"
      >
        <HeaderTitle title={t(["bds"])} />
        <HeaderMobileMenuButton
          title={t(["open-menu"])}
          onClick={() => setMobileMenuOpen(true)}
        />
        <HeaderTabs
          selectedTabPosition={selectedTabPosition}
          tabs={tabsDefinition}
          onClickTab={changePage}
          activeTab={activeTab}
        />
        <HeaderUser />
      </nav>
      <HeaderMobileMenu
        title={t(["b"])}
        mobileMenuOpen={mobileMenuOpen}
        onClick={() => setMobileMenuOpen(false)}
      />
    </header>
  );
}
