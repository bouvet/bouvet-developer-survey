import { useActiveSectionId } from "@/app/hooks/useActiveSectionId";
import { Dialog, DialogPanel } from "@headlessui/react";
import { MenuItemId, menuItems } from "./menuItems";
import { HeaderDropdownNavigationProps } from "@/app/components/Header/components/HeaderDropdownNavigation";
import HeaderMobileDropdownNavigation from "@/app/components/Header/components/HeaderMobileDropdownNavigation";
import Link from "next/link";

export default function MobileMenu({
  mobileMenuOpen,
  onClick,
  subNavigationItems,
}: {
  mobileMenuOpen: boolean;
  onClick: (bool: boolean) => void;
  subNavigationItems: {
    technology: HeaderDropdownNavigationProps[];
    aboutParticipants: HeaderDropdownNavigationProps[];
  };
}) {
  const activeId = useActiveSectionId();

  return (
    <Dialog
      open={mobileMenuOpen}
      onClose={() => onClick(false)}
      className="lg:hidden"
    >
      <div className="fixed inset-0 z-10 bg-black/30" aria-hidden="true" />
      <DialogPanel className="fixed inset-x-0 top-28 z-20 w-full bg-white dark:bg-slate-800">
        <nav className="px-4 py-2 border-t border-gray-200">
          <ul className="space-y-3">
            {menuItems.map((item) => (
              <li key={item.id} className="flex gap-x-1.5">
                <Link
                  href={`#${item.id}`}
                  onClick={() => {
                    onClick(false);
                  }}
                  className={`
                  px-3 py-1 text-sm font-bold
                  hover:underline decoration-2 underline-offset-
                  ${
                    activeId === item.id
                      ? "text-[var(--foreground)] underline"
                      : "text-gray-600"
                  }
                  dark:${
                    activeId === item.id
                      ? "text-[var(--foreground)] underline"
                      : "text-white"
                  }
                `}
                >
                  {item.label}
                </Link>

                {item.id === MenuItemId.TECHNOLOGY && (
                  <HeaderMobileDropdownNavigation
                    items={subNavigationItems.technology}
                    onClick={onClick}
                  />
                )}
                {item.id === MenuItemId.ABOUT_PARTICIPANTS && (
                  <HeaderMobileDropdownNavigation
                    items={subNavigationItems.aboutParticipants}
                    onClick={onClick}
                  />
                )}
              </li>
            ))}
          </ul>
        </nav>
      </DialogPanel>
    </Dialog>
  );
}
