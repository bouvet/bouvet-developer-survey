"use client";

import { Popover, PopoverButton, PopoverGroup } from "@headlessui/react";
import { TabsDefinitionType } from "@/app/routes/tabs-definitions";
import { useClientTranslation } from "../../../../../shared/i18n/src";

// Component
export default function HeaderTabs({
  selectedTabPosition,
  tabs,
  onClickTab,
  activeTab,
}: {
  selectedTabPosition: number;
  tabs: TabsDefinitionType[];
  onClickTab: (tab: TabsDefinitionType) => () => void;
  activeTab: string;
}) {
  const { t } = useClientTranslation();

  // Render
  return (
    <PopoverGroup className="hidden lg:flex lg:gap-x-12">
      {tabs.map((tab) => (
        <Popover className="relative" key={tab.name}>
          <PopoverButton
            onClick={onClickTab(tab)}
            className={`flex items-center gap-x-1 text-sm font-semibold leading-6 text-gray-900 ${
              activeTab === tab.name ? "underline" : ""
            }`}
          >
            <a
              href="#"
              className="text-sm font-semibold leading-6 text-gray-900"
            >
              {tab.name}
            </a>
          </PopoverButton>
        </Popover>
      ))}
    </PopoverGroup>
  );
}
