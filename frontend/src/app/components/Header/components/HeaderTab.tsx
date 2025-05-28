"use client";
import Link from "next/link";
import HeaderDropdownNavigation, {
  HeaderDropdownNavigationProps,
} from "@/app/components/Header/components/HeaderDropdownNavigation";
import { MenuItemId } from "@/app/components/Header/components/menuItems";
import { Loading } from "@/app/components/Header/components/HeaderTabs";
import { Suspense } from "react";

export type HeaderTabProps = {
  item: { id: string; label: string };
  isActive: boolean;
  subNavigationItems: {
    technology: HeaderDropdownNavigationProps[];
    aboutParticipants: HeaderDropdownNavigationProps[];
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
          prefetch={true}
          href={`#${item.id}`}
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
      </li>
    </Suspense>
  );
};
