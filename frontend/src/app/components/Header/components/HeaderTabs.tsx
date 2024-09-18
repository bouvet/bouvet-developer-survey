"use client";

import { useEffect, useRef, useState } from "react";
import {
  Popover,
  PopoverButton,
  PopoverGroup,
  PopoverPanel,
} from "@headlessui/react";
import { ChevronDownIcon } from "@heroicons/react/24/outline";
import { TabsDefinitionType } from "@/app/routes/tabs-definitions";
import { useClientTranslation } from "../../../../../shared/i18n/src";

// Component
export default function HeaderTabs({
  selectedTabPosition,
  tabs,
  onClickTab,
}: {
  selectedTabPosition: number;
  tabs: TabsDefinitionType[];
  onClickTab: (tab: TabsDefinitionType) => () => void;
}) {
  const { t } = useClientTranslation();
  // Render
  return (
    <PopoverGroup className="hidden lg:flex lg:gap-x-12">
      {tabs.map((tab) => (
        <Popover className="relative">
          <PopoverButton
            onClick={() => onClickTab}
            className="flex items-center gap-x-1 text-sm font-semibold leading-6 text-gray-900"
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
