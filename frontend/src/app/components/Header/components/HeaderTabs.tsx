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
          <Loading />
        )}
      </ul>
    </>
  );
}

export const Loading = () => (
  <>
    <li className="sr-only">
      <span>Laster...</span>
    </li>
    {["w-24", "w-40", "w-20"].map((width) => (
      <li
        key={width}
        className={`h-4 bg-gray-400 rounded-full dark:bg-gray-50 ${width}`}
      ></li>
    ))}
  </>
);
