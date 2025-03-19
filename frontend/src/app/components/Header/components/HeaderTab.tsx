"use client";
import Link from "next/link";
import HeaderDropdownNavigation, {
  HeaderDropdownNavigationProps,
} from "@/app/components/Header/components/HeaderDropdownNavigation";
import { MenuItemId } from "@/app/components/Header/components/menuItems";

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
    <li className="group relative">
      <Link
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
  );
};
