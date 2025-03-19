"use client";
import { useActiveSectionId } from "@/app/hooks/useActiveSectionId";
import { HeaderTab } from "./HeaderTab";
import { menuItems } from "./menuItems";
import { HeaderDropdownNavigationProps } from "@/app/components/Header/components/HeaderDropdownNavigation";

export default function HeaderTabs({
  subNavigationItems,
}: {
  subNavigationItems: {
    technology: HeaderDropdownNavigationProps[];
    aboutParticipants: HeaderDropdownNavigationProps[];
  };
}) {
  const activeId = useActiveSectionId();

  return (
    <ul className="hidden lg:flex w-full justify-center items-center space-x-6 font-bold text-sm">
      {menuItems.map((item) => (
        <HeaderTab
          key={item.id}
          item={item}
          isActive={activeId === item.id}
          subNavigationItems={subNavigationItems}
        />
      ))}
    </ul>
  );
}
