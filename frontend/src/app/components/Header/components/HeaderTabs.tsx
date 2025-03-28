"use client";
import { useActiveSectionId } from "@/app/hooks/useActiveSectionId";
import { HeaderTab } from "./HeaderTab";
import { menuItems } from "./menuItems";
import { HeaderDropdownNavigationProps } from "@/app/components/Header/components/HeaderDropdownNavigation";

export default function HeaderTabs({
  subNavigationItems,
  isLoading,
}: {
  subNavigationItems: {
    technology: HeaderDropdownNavigationProps[];
    aboutParticipants: HeaderDropdownNavigationProps[];
  };
  isLoading: boolean;
}) {
  const activeId = useActiveSectionId();

  return (
    <>
      <ul
        className={`hidden lg:flex w-full justify-center items-center space-x-6 font-bold text-sm ${isLoading && "animate-pulse"}`}
        role={isLoading ? "status" : undefined}
      >
        {!isLoading ? (
          <>
            {menuItems.map((item) => (
              <HeaderTab
                key={item.id}
                item={item}
                isActive={activeId === item.id}
                subNavigationItems={subNavigationItems}
              />
            ))}
          </>
        ) : (
          <>
            <li className="sr-only">
              <span>Laster...</span>
            </li>
            {["24", "40", "20"].map((width) => (
              <li
                key={width}
                className={`h-4 bg-gray-400 rounded-full dark:bg-gray-50 w-${width}`}
              ></li>
            ))}
          </>
        )}
      </ul>
    </>
  );
}
