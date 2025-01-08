"use client";
import { useActiveSectionId } from "@/app/hooks/useActiveSectionId";
import { HeaderTab } from "./HeaderTab";
import { menuItems } from "./menuItems";

export default function HeaderTabs() {
  const activeId = useActiveSectionId();
  return (
    <div className="hidden lg:flex w-full justify-center items-center space-x-6 font-bold text-sm">
      {menuItems.map((item) => (
        <HeaderTab item={item} isActive={activeId === item.id} />
      ))}
    </div>
  );
}
