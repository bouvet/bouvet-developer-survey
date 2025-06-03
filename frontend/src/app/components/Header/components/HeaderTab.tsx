"use client";
import Link from "next/link";
import HeaderDropdownNavigation, {
  HeaderDropdownNavigationProps,
} from "@/app/components/Header/components/HeaderDropdownNavigation";
import { MenuItemId } from "@/app/components/Header/components/menuItems";
import { Loading } from "@/app/components/Header/components/HeaderTabs";
import { Suspense } from "react";
import { Years } from "@/app/components/landing/YearPickerCircle";

export type HeaderTabProps = {
  item: { id: string; label: string };
  isActive: boolean;
  subNavigationItems: {
    technology: HeaderDropdownNavigationProps[];
    aboutParticipants: HeaderDropdownNavigationProps[];
    years: Years[];
  };
};
export const HeaderTab = ({
  item,
  isActive,
  subNavigationItems,
}: HeaderTabProps) => {
  return (
    <Suspense fallback={<Loading />}>
      <li className="group relative">
        <Link
          href={item.id !== MenuItemId.YEARS ? `#${item.id}` : ""}
          className={`
    hover:underline 
    decoration-2
    underline-offset-
    menu-hover
    ${isActive ? "underline" : ""}
  `}
        >
          {item.label}
        </Link>
        {item.id === MenuItemId.TECHNOLOGY && (
          <HeaderDropdownNavigation items={subNavigationItems.technology} />
        )}
        {item.id === MenuItemId.ABOUT_PARTICIPANTS && (
          <HeaderDropdownNavigation
            items={subNavigationItems.aboutParticipants}
          />
        )}
        {item.id === MenuItemId.YEARS && (
          <HeaderDropdownNavigation items={subNavigationItems.years} />
        )}
      </li>
    </Suspense>
  );
};
