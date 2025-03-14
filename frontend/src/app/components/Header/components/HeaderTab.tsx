"use client";
import Link from "next/link";
import HeaderDropdownNavigation from "@/app/components/Header/components/HeaderDropdownNavigation";

export type HeaderTabProps = {
  item: { id: string; label: string };
  isActive: boolean;
};
export const HeaderTab = ({ item, isActive }: HeaderTabProps) => {
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
      {item.id === "technology" && <HeaderDropdownNavigation />}
    </li>
  );
};
