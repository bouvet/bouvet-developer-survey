"use client";

import { usePathname } from "next/navigation";
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
import { resolveRoute } from "@/app/routes/route-definitions";
import { useClientTranslation } from "../../../../shared/i18n/src";

// Component
export default function Header() {
  const { t } = useClientTranslation();
  const [menuOpen, setMenuOpen] = useState(false);
  const [currentTab, setCurrentTab] = useState(null);
  const redirect = useRedirect();
  const pathname = usePathname();
  const params = useRouteParams();
  const [mobileMenuOpen, setMobileMenuOpen] = useState(false);

  // ! todo
  const selectedTabPosition = 1;
  const resolvedRoute = resolveRoute(pathname, params);

  // Methods
  const changePage = (route: TabsDefinitionType) => () => {
    if (route.path) {
      const path =
        typeof route.path === "function" ? route.path(params) : route.path;
      redirect(path);
      if (menuOpen) setMenuOpen(false);
    }
  };

  // Render
  if (currentTab) return null;
  return (
    <header className="bg-inherit">
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
        />
        <HeaderUser title={t(["login"])} />
      </nav>
      <HeaderMobileMenu
        title={t(["b"])}
        mobileMenuOpen={mobileMenuOpen}
        onClick={() => setMobileMenuOpen(false)}
      />
    </header>
  );
}
