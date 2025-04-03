import Link from "next/link";
import { HeaderDropdownNavigationProps } from "@/app/components/Header/components/HeaderDropdownNavigation";
import { ChevronDownIcon } from "@heroicons/react/24/outline";
import { Menu, MenuButton, MenuItem, MenuItems } from "@headlessui/react";

export default function HeaderMobileDropdownNavigation({
  items,
  onClick,
}: {
  items: HeaderDropdownNavigationProps[];
  onClick: (bool: boolean) => void;
}) {
  return (
    <>
      {items?.length > 0 && (
        <Menu as="div" className="relative">
          <MenuButton aria-haspopup="true">
            {({ open }) => (
              <ChevronDownIcon
                className={`h-6 w-6 transition ${open && "rotate-180"}`}
                aria-hidden="true"
              />
            )}
          </MenuButton>
          <MenuItems
            anchor="bottom end"
            as="ul"
            aria-orientation="vertical"
            className="flex flex-col transition gap-2 w-fit z-20 p-4 dark:bg-slate-800 bg-white rounded-lg shadow-lg border dark:border-gray-600"
          >
            {items.map((item: HeaderDropdownNavigationProps) => (
              <MenuItem as="li" key={item.id}>
                {({ close }) => (
                  <Link
                    href={`#${item?.text?.replaceAll(/\s/g, "-")}`}
                    className="hover:underline decoration-2 underline-offset-4"
                    onClick={() => {
                      close();
                      onClick(false);
                    }}
                  >
                    {item.text}
                  </Link>
                )}
              </MenuItem>
            ))}
          </MenuItems>
        </Menu>
      )}
    </>
  );
}
